using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ixxat.Vci4;
using Ixxat.Vci4.Bal;
using Ixxat.Vci4.Bal.Can;
using CanConNet;
using MultiMediaTimer;
using System.Diagnostics;

namespace UsbCanDotnet
{
    public partial class Form1 : Form
    {
        Stopwatch sw = new Stopwatch();
        static MultimediaTimer mTimer;
        CanConNetPS can = new CanConNetPS();
        ICanMessage canTxMsg;
        bool isAutoScroll = true;
        int rxCount = 0;
        public Form1()
        {
            InitializeComponent();
            /// Initialize MultimediaTimer with 10ms interval and hookup a callback
            mTimer = new MultimediaTimer() { Interval = 10, Resolution = 0 };
            mTimer.Elapsed += MTimer_Elapsed;

            /// Initialize CAN bus with mode and bitrate
            can.SetOperatingModes(CanOperatingModes.Standard |
                                  CanOperatingModes.Extended |
                                  CanOperatingModes.ErrFrame);
            can.SetBitrate(CanBitrate.Cia1000KBit);
            can.NewCanMessageEvent += Can_NewCanMessageEvent;
            try
            {
                can.SelectDevice(); 
                can.InitSocket(0);
                /// Display selected device
                tbDescription.Text = can.GetDevice().Description.ToString();
                tbManufacturer.Text = can.GetDevice().Manufacturer.ToString();
                tbHardwareVersion.Text = can.GetDevice().HardwareVersion.ToString();
                tbDriverVersion.Text = can.GetDevice().DriverVersion.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            canTxMsg = can.CreateNewCanMsg(0x21, CanMsgFrameType.Data, 8, true, false);
            
        }


        private void MTimer_Elapsed(object sender, EventArgs e)
        {
            //double dt = sw.ElapsedMilliseconds;
            can.TransmitMsg(canTxMsg);
        }

        private void Can_NewCanMessageEvent(object sender, ICanMessage canMsg)
        {
            string strMsg = string.Format("T: {0} ID: {1} DLC: {2} DATA: ",
                canMsg.TimeStamp.ToString(),
                canMsg.Identifier.ToString("X4"),
                canMsg.DataLength.ToString());

            for (int index = 0; index < canMsg.DataLength; index++)
            {
                strMsg += " ";
                strMsg += canMsg[index].ToString("X2");
            }
            Trace.WriteLine(strMsg);
            rxCount++;
            UpdateListBoxAdd(lbRxConsole, strMsg);
        }

        #region Invoke Methods
        delegate void UpdateLabelCallback(System.Windows.Forms.Label lbl, string str);
        public void UpdateLabel(System.Windows.Forms.Label lbl, string str)
        {
            try
            {
                if (lbl.InvokeRequired)
                    Invoke(new UpdateLabelCallback(UpdateLabel), new object[] { lbl, str });
                else
                    lbl.Text = str;
            }
            catch (Exception)
            {
            }
        }
        delegate void UpdateListBoxAddCallback(System.Windows.Forms.ListBox lbox, string str);
        public void UpdateListBoxAdd(System.Windows.Forms.ListBox lbox, string str)
        {
            try
            {
                if (lbox.InvokeRequired)
                    Invoke(new UpdateListBoxAddCallback(UpdateListBoxAdd), new object[] { lbox, str });
                else
                {
                    if(lbox.Items.Count > 100)
                    {
                        lbox.Items.RemoveAt(0);
                    }
                    lbox.Items.Add(str);
                    if(isAutoScroll)
                    {
                        lbox.SelectedIndex = lbox.Items.Count - 1;
                        lbox.SelectedIndex = -1;
                    }
                    lblRxCount.Text = rxCount.ToString();
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            can.FinalizeApp();
        }

        private void cbAutoScroll_CheckedChanged(object sender, EventArgs e)
        {
            if(cbAutoScroll.Checked)
            {
                isAutoScroll = true;
            } else
            {
                isAutoScroll = false;
            }
        }

        private void btnSendCanMsg_Click(object sender, EventArgs e)
        {
            can.TransmitMsg(canTxMsg);
        }

        private void cbSendPeriodic_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSendPeriodic.Checked)
            {
                mTimer.Interval = (int)numPeriodSet.Value;
                mTimer.Start();
                sw.Start();
                numPeriodSet.Enabled = false;
            }
            else
            {
                sw.Stop();
                mTimer.Stop();
                numPeriodSet.Enabled = true;
            }
        }
    }
}
