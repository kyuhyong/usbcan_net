/*****************************************************************************
 * used namespaces
 ****************************************************************************/
using System;
using System.Text;
using System.Collections;
using System.Threading;
using Ixxat.Vci4;
using Ixxat.Vci4.Bal;
using Ixxat.Vci4.Bal.Can;
using System.Diagnostics;


/*****************************************************************************
 * namespace CanConNet
 ****************************************************************************/
namespace CanConNet
{
    //##########################################################################
    /// <summary>
    ///   This class provides the entry point for the IXXAT VCI .NET 2.0 API
    ///   demo application. 
    /// </summary>
    //##########################################################################
    class CanConNetPS
    {
        #region Member variables

        /// <summary>
        ///   Reference to the used VCI device.
        /// </summary>
        static IVciDevice mDevice;

        /// <summary>
        ///   Reference to the CAN controller.
        /// </summary>
        static ICanControl mCanCtl;

        /// <summary>
        ///   Reference to the CAN message communication channel.
        /// </summary>
        static ICanChannel mCanChn;

        /// <summary>
        ///   Reference to the CAN message scheduler.
        /// </summary>
        static ICanScheduler mCanSched;

        /// <summary>
        ///   Reference to the message writer of the CAN message channel.
        /// </summary>
        static ICanMessageWriter mWriter;

        /// <summary>
        ///   Reference to the message reader of the CAN message channel.
        /// </summary>
        static ICanMessageReader mReader;

        /// <summary>
        ///   Thread that handles the message reception.
        /// </summary>
        static Thread rxThread;

        /// <summary>
        ///   Quit flag for the receive thread.
        /// </summary>
        static long mMustQuit = 0;

        /// <summary>
        ///   Event that's set if at least one message was received.
        /// </summary>
        static AutoResetEvent mRxEvent;

        static CanBitrate mCanBitrate;

        static CanOperatingModes mOpMode;

        #endregion

        #region Application entry point

        #endregion

        #region Event Handler

        public delegate void NewCanMessageDelegate(object sender, ICanMessage canMsg);
        public event NewCanMessageDelegate NewCanMessageEvent;

        #endregion

        public void SetBitrate(CanBitrate bitrate)
        {
            mCanBitrate = bitrate;
        }
        public void SetOperatingModes(CanOperatingModes mode)
        {
            mOpMode = mode;
        }

        #region Device selection

        //************************************************************************
        /// <summary>
        ///   Selects the first CAN adapter.
        /// </summary>
        //************************************************************************
        public void SelectDevice()
        {
            IVciDeviceManager deviceManager = null;
            IVciDeviceList deviceList = null;
            IEnumerator deviceEnum = null;

            try
            {
                //
                // Get device manager from VCI server
                //
                deviceManager = VciServer.Instance().DeviceManager;

                //
                // Get the list of installed VCI devices
                //
                deviceList = deviceManager.GetDeviceList();

                //
                // Get enumerator for the list of devices
                //
                deviceEnum = deviceList.GetEnumerator();

                //
                // Get first device
                //
                deviceEnum.MoveNext();
                mDevice = deviceEnum.Current as IVciDevice;

                //
                // print bus type and controller type of first controller
                //
                IVciCtrlInfo info = mDevice.Equipment[0];
                Trace.Write(" BusType    : ");
                Trace.WriteLine(info.BusType.ToString());
                Trace.Write(" CtrlType   : ");
                Trace.WriteLine(info.ControllerType.ToString());

                // show the device name and serial number
                object serialNumberGuid = mDevice.UniqueHardwareId;
                string serialNumberText = GetSerialNumberText(ref serialNumberGuid);
                Trace.Write(" Interface    : ");
                Trace.WriteLine(mDevice.Description.ToString());
                Trace.Write(" Serial number: ");
                Trace.WriteLine(serialNumberText.ToString());
            }
            catch (Exception exc)
            {
                Trace.Write("Error: ");
                Trace.WriteLine(exc.Message);
                throw new Exception("Select Device Error. ", exc);
            }
            finally
            {
                //
                // Dispose device manager ; it's no longer needed.
                //
                DisposeVciObject(deviceManager);

                //
                // Dispose device list ; it's no longer needed.
                //
                DisposeVciObject(deviceList);

                //
                // Dispose device list ; it's no longer needed.
                //
                DisposeVciObject(deviceEnum);
            }
        }

        public IVciDevice GetDevice()
        {
            return mDevice;
        }

        #endregion

        #region Opening socket

        //************************************************************************
        /// <summary>
        ///   Opens the specified socket, creates a message channel, initializes
        ///   and starts the CAN controller.
        /// </summary>
        /// <param name="canNo">
        ///   Number of the CAN controller to open.
        /// </param>
        /// <returns>
        ///   A value indicating if the socket initialization succeeded or failed.
        /// </returns>
        //************************************************************************
        public bool InitSocket(Byte canNo)
        {
            IBalObject bal = null;
            bool succeeded = false;

            try
            {
                //
                // Open bus access layer
                //
                bal = mDevice.OpenBusAccessLayer();

                //
                // Open a message channel for the CAN controller
                //
                mCanChn = bal.OpenSocket(canNo, typeof(ICanChannel)) as ICanChannel;

                //
                // Open the scheduler of the CAN controller
                //
                mCanSched = bal.OpenSocket(canNo, typeof(ICanScheduler)) as ICanScheduler;

                // Initialize the message channel
                mCanChn.Initialize(1024, 128, false);

                // Get a message reader object
                mReader = mCanChn.GetMessageReader();

                // Initialize message reader
                mReader.Threshold = 1;

                // Create and assign the event that's set if at least one message
                // was received.
                mRxEvent = new AutoResetEvent(false);
                mReader.AssignEvent(mRxEvent);

                // Get a message wrtier object
                mWriter = mCanChn.GetMessageWriter();

                // Initialize message writer
                mWriter.Threshold = 1;

                // Activate the message channel
                mCanChn.Activate();


                //
                // Open the CAN controller
                //
                mCanCtl = bal.OpenSocket(canNo, typeof(ICanControl)) as ICanControl;

                if(mOpMode == 0)
                {
                    mOpMode = CanOperatingModes.Standard |
                            CanOperatingModes.Extended |
                            CanOperatingModes.ErrFrame;
                }
                if(mCanBitrate == CanBitrate.Empty)
                {
                    mCanBitrate = CanBitrate.Cia1000KBit;
                }
                // Initialize the CAN controller
                mCanCtl.InitLine(mOpMode, mCanBitrate);

                //
                // print line status
                //
                Trace.Write("Line Status:");
                Trace.WriteLine(mCanCtl.LineStatus.ToString());

                // Set the acceptance filter for std identifiers
                mCanCtl.SetAccFilter(CanFilter.Std,
                                     (uint)CanAccCode.All, (uint)CanAccMask.All);

                // Set the acceptance filter for ext identifiers
                mCanCtl.SetAccFilter(CanFilter.Ext,
                                     (uint)CanAccCode.All, (uint)CanAccMask.All);

                //
                // start the receive thread
                //
                rxThread = new Thread(new ThreadStart(ReceiveThreadFunc));
                rxThread.Start();

                // Start the CAN controller
                mCanCtl.StartLine();

                succeeded = true;
            }
            catch (Exception exc)
            {
                Trace.WriteLine("Error: Initializing socket failed : " + exc.Message);
                succeeded = false;
                throw new Exception("Error socket open.", exc);
            }
            finally
            {
                //
                // Dispose bus access layer
                //
                DisposeVciObject(bal);
            }

            return succeeded;
        }

        #endregion

        #region Message transmission

        /// <summary>
        ///   Transmits a CAN message with ID 0x100.
        /// </summary>
        public void TransmitData()
        {
            IMessageFactory factory = VciServer.Instance().MsgFactory;
            ICanMessage canMsg = (ICanMessage)factory.CreateMsg(typeof(ICanMessage));

            canMsg.TimeStamp = 0;
            canMsg.Identifier = 0x100;
            canMsg.FrameType = CanMsgFrameType.Data;
            canMsg.DataLength = 8;
            canMsg.SelfReceptionRequest = true;  // show this message in the console window

            for (Byte i = 0; i < canMsg.DataLength; i++)
            {
                canMsg[i] = i;
            }

            // Write the CAN message into the transmit FIFO
            mWriter.SendMessage(canMsg);
        }

        public void TransmitMsg(ICanMessage canMsg)
        {
            mWriter.SendMessage(canMsg);
        }

        #endregion

        #region Message reception

        //************************************************************************
        /// <summary>
        /// Print a CAN message
        /// </summary>
        /// <param name="canMessage"></param>
        //************************************************************************
        public void PrintMessage(ICanMessage canMessage)
        {
            switch (canMessage.FrameType)
            {
                //
                // show data frames
                //
                case CanMsgFrameType.Data:
                    {
                        if (!canMessage.RemoteTransmissionRequest)
                        {
                            /*
                            Console.Write("\nTime: {0,10}  ID: {1,3:X}  DLC: {2,1}  Data:",
                                          canMessage.TimeStamp,
                                          canMessage.Identifier,
                                          canMessage.DataLength);
                            Trace.Write("\nTime:");
                            Trace.Write(canMessage.TimeStamp.ToString());
                            Trace.Write(" ID: ");
                            Trace.Write(canMessage.Identifier.ToString());
                            Trace.Write(" DLC: ");
                            Trace.Write(canMessage.DataLength.ToString());
                            
                            for (int index = 0; index < canMessage.DataLength; index++)
                            {
                                Console.Write(" {0,2:X}", canMessage[index]);
                            }
                            */
                        }
                        else
                        {
                            Console.Write("\nTime: {0,10}  ID: {1,3:X}  DLC: {2,1}  Remote Frame",
                                          canMessage.TimeStamp,
                                          canMessage.Identifier,
                                          canMessage.DataLength);
                        }
                        break;
                    }

                //
                // show informational frames
                //
                case CanMsgFrameType.Info:
                    {
                        switch ((CanMsgInfoValue)canMessage[0])
                        {
                            case CanMsgInfoValue.Start:
                                Console.Write("\nCAN started...");
                                break;
                            case CanMsgInfoValue.Stop:
                                Console.Write("\nCAN stopped...");
                                break;
                            case CanMsgInfoValue.Reset:
                                Console.Write("\nCAN reseted...");
                                break;
                        }
                        break;
                    }

                //
                // show error frames
                //
                case CanMsgFrameType.Error:
                    {
                        switch ((CanMsgError)canMessage[0])
                        {
                            case CanMsgError.Stuff:
                                Console.Write("\nstuff error...");
                                break;
                            case CanMsgError.Form:
                                Console.Write("\nform error...");
                                break;
                            case CanMsgError.Acknowledge:
                                Console.Write("\nacknowledgment error...");
                                break;
                            case CanMsgError.Bit:
                                Console.Write("\nbit error...");
                                break;
                            case CanMsgError.Fdb:
                                Console.Write("\nfast data bit error...");
                                break;
                            case CanMsgError.Crc:
                                Console.Write("\nCRC error...");
                                break;
                            case CanMsgError.Dlc:
                                Console.Write("\nData length error...");
                                break;
                            case CanMsgError.Other:
                                Console.Write("\nother error...");
                                break;
                        }
                        break;
                    }
            }
        }

        //************************************************************************
        /// <summary>
        /// Demonstrate reading messages via MsgReader::ReadMessages() function
        /// </summary>
        //************************************************************************
        public void ReadMultipleMsgsViaReadMessages()
        {
            ICanMessage[] msgArray;

            do
            {
                // Wait 100 msec for a message reception
                if (mRxEvent.WaitOne(100, false))
                {
                    if (mReader.ReadMessages(out msgArray) > 0)
                    {
                        foreach (ICanMessage entry in msgArray)
                        {
                            PrintMessage(entry);
                        }
                    }
                }
            } while (0 == mMustQuit);
        }

        //************************************************************************
        /// <summary>
        /// Demonstrate reading messages via MsgReader::ReadMessage() function
        /// </summary>
        //************************************************************************
        public void ReadMsgsViaReadMessage()
        {
            ICanMessage canMessage;

            do
            {
                // Wait 100 msec for a message reception
                try
                {
                    if (mRxEvent.WaitOne(1, false))
                    {
                        // read a CAN message from the receive FIFO
                        while (mReader.ReadMessage(out canMessage))
                        {
                            PrintMessage(canMessage);
                            NewCanMessageEvent(this, canMessage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    break;
                }
            } while (0 == mMustQuit);
        }

        //************************************************************************
        /// <summary>
        ///   This method is the works as receive thread.
        /// </summary>
        //************************************************************************
        public void ReceiveThreadFunc()
        {
            ReadMsgsViaReadMessage();
            //
            // alternative: use ReadMultipleMsgsViaReadMessages();
            //
        }

        #endregion

        #region Utility methods

        public  ICanMessage CreateNewCanMsg(uint idx, CanMsgFrameType frameType, byte dataLength, bool isExtendedFrame, bool isRCR)
        {
            IMessageFactory factory = VciServer.Instance().MsgFactory;
            ICanMessage canMsg = (ICanMessage)factory.CreateMsg(typeof(ICanMessage));
            canMsg.TimeStamp = 0;
            canMsg.Identifier = idx;
            canMsg.ExtendedFrameFormat = isExtendedFrame;
            canMsg.DataLength = dataLength;
            canMsg.SelfReceptionRequest = isRCR;
            for (Byte i = 0; i < canMsg.DataLength; i++)
            {
                canMsg[i] = 0;
            }
            return canMsg;
        }

        /// <summary>
        /// Returns the UniqueHardwareID GUID number as string which
        /// shows the serial number.
        /// Note: This function will be obsolete in later version of the VCI.
        /// Until VCI Version 3.1.4.1784 there is a bug in the .NET API which
        /// returns always the GUID of the interface. In later versions there
        /// the serial number itself will be returned by the UniqueHardwareID property.
        /// </summary>
        /// <param name="serialNumberGuid">Data read from the VCI.</param>
        /// <returns>The GUID as string or if possible the  serial number as string.</returns>
        public string GetSerialNumberText(ref object serialNumberGuid)
        {
            string resultText;

            // check if the object is really a GUID type
            if (serialNumberGuid.GetType() == typeof(System.Guid))
            {
                // convert the object type to a GUID
                System.Guid tempGuid = (System.Guid)serialNumberGuid;

                // copy the data into a byte array
                byte[] byteArray = tempGuid.ToByteArray();

                // serial numbers starts always with "HW"
                if (((char)byteArray[0] == 'H') && ((char)byteArray[1] == 'W'))
                {
                    // run a loop and add the byte data as char to the result string
                    resultText = "";
                    int i = 0;
                    while (true)
                    {
                        // the string stops with a zero
                        if (byteArray[i] != 0)
                            resultText += (char)byteArray[i];
                        else
                            break;
                        i++;

                        // stop also when all bytes are converted to the string
                        // but this should never happen
                        if (i == byteArray.Length)
                            break;
                    }
                }
                else
                {
                    // if the data did not start with "HW" convert only the GUID to a string
                    resultText = serialNumberGuid.ToString();
                }
            }
            else
            {
                // if the data is not a GUID convert it to a string
                string tempString = (string)(string)serialNumberGuid;
                resultText = "";
                for (int i = 0; i < tempString.Length; i++)
                {
                    if (tempString[i] != 0)
                        resultText += tempString[i];
                    else
                        break;
                }
            }

            return resultText;
        }


        //************************************************************************
        /// <summary>
        ///   Finalizes the application 
        /// </summary>
        //************************************************************************
        public void FinalizeApp()
        {
            //
            // Dispose all hold VCI objects.
            //
            mMustQuit = 1;
            // Dispose message reader
            DisposeVciObject(mReader);

            // Dispose message writer 
            DisposeVciObject(mWriter);

            // Dispose CAN channel
            DisposeVciObject(mCanChn);

            // Dispose CAN controller
            DisposeVciObject(mCanCtl);

            // Dispose VCI device
            DisposeVciObject(mDevice);
        }


        //************************************************************************
        /// <summary>
        ///   This method tries to dispose the specified object.
        /// </summary>
        /// <param name="obj">
        ///   Reference to the object to be disposed.
        /// </param>
        /// <remarks>
        ///   The VCI interfaces provide access to native driver resources. 
        ///   Because the .NET garbage collector is only designed to manage memory, 
        ///   but not native OS and driver resources the application itself is 
        ///   responsible to release these resources via calling 
        ///   IDisposable.Dispose() for the obects obtained from the VCI API 
        ///   when these are no longer needed. 
        ///   Otherwise native memory and resource leaks may occure.  
        /// </remarks>
        //************************************************************************
        static void DisposeVciObject(object obj)
        {
            if (null != obj)
            {
                IDisposable dispose = obj as IDisposable;
                if (null != dispose)
                {
                    dispose.Dispose();
                    obj = null;
                }
            }
        }

        #endregion
    }
}
