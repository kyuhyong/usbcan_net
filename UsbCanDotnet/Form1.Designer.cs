using System;
using System.Text;
using System.Collections;



namespace UsbCanDotnet
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDriverVersion = new System.Windows.Forms.TextBox();
            this.tbHardwareVersion = new System.Windows.Forms.TextBox();
            this.tbManufacturer = new System.Windows.Forms.TextBox();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.btnSendCanMsg = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblRxCount = new System.Windows.Forms.Label();
            this.cbAutoScroll = new System.Windows.Forms.CheckBox();
            this.lbRxConsole = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbSendPeriodic = new System.Windows.Forms.CheckBox();
            this.numPeriodSet = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPeriodSet)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbDriverVersion);
            this.groupBox1.Controls.Add(this.tbHardwareVersion);
            this.groupBox1.Controls.Add(this.tbManufacturer);
            this.groupBox1.Controls.Add(this.tbDescription);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1118, 401);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CAN Device";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 320);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 41);
            this.label4.TabIndex = 8;
            this.label4.Text = "Driver Ver";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 236);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 41);
            this.label3.TabIndex = 7;
            this.label3.Text = "Hardware Ver";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 155);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 41);
            this.label2.TabIndex = 6;
            this.label2.Text = "Manufacturer";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 70);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 41);
            this.label1.TabIndex = 5;
            this.label1.Text = "Description";
            // 
            // tbDriverVersion
            // 
            this.tbDriverVersion.Location = new System.Drawing.Point(241, 315);
            this.tbDriverVersion.Margin = new System.Windows.Forms.Padding(4);
            this.tbDriverVersion.Name = "tbDriverVersion";
            this.tbDriverVersion.ReadOnly = true;
            this.tbDriverVersion.Size = new System.Drawing.Size(404, 47);
            this.tbDriverVersion.TabIndex = 4;
            this.tbDriverVersion.Text = "Test";
            // 
            // tbHardwareVersion
            // 
            this.tbHardwareVersion.Location = new System.Drawing.Point(241, 231);
            this.tbHardwareVersion.Margin = new System.Windows.Forms.Padding(4);
            this.tbHardwareVersion.Name = "tbHardwareVersion";
            this.tbHardwareVersion.ReadOnly = true;
            this.tbHardwareVersion.Size = new System.Drawing.Size(404, 47);
            this.tbHardwareVersion.TabIndex = 3;
            this.tbHardwareVersion.Text = "Test";
            // 
            // tbManufacturer
            // 
            this.tbManufacturer.Location = new System.Drawing.Point(241, 150);
            this.tbManufacturer.Margin = new System.Windows.Forms.Padding(4);
            this.tbManufacturer.Name = "tbManufacturer";
            this.tbManufacturer.ReadOnly = true;
            this.tbManufacturer.Size = new System.Drawing.Size(693, 47);
            this.tbManufacturer.TabIndex = 2;
            this.tbManufacturer.Text = "Test";
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(241, 70);
            this.tbDescription.Margin = new System.Windows.Forms.Padding(4);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ReadOnly = true;
            this.tbDescription.Size = new System.Drawing.Size(693, 47);
            this.tbDescription.TabIndex = 1;
            this.tbDescription.Text = "Test";
            // 
            // btnSendCanMsg
            // 
            this.btnSendCanMsg.Location = new System.Drawing.Point(82, 118);
            this.btnSendCanMsg.Margin = new System.Windows.Forms.Padding(4);
            this.btnSendCanMsg.Name = "btnSendCanMsg";
            this.btnSendCanMsg.Size = new System.Drawing.Size(196, 59);
            this.btnSendCanMsg.TabIndex = 1;
            this.btnSendCanMsg.Text = "SEND";
            this.btnSendCanMsg.UseVisualStyleBackColor = true;
            this.btnSendCanMsg.Click += new System.EventHandler(this.btnSendCanMsg_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lblRxCount);
            this.groupBox2.Controls.Add(this.cbAutoScroll);
            this.groupBox2.Controls.Add(this.lbRxConsole);
            this.groupBox2.Location = new System.Drawing.Point(16, 424);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1118, 846);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RX Console";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(712, 765);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 41);
            this.label6.TabIndex = 12;
            this.label6.Text = "Rx Count:";
            // 
            // lblRxCount
            // 
            this.lblRxCount.AutoSize = true;
            this.lblRxCount.Location = new System.Drawing.Point(981, 765);
            this.lblRxCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRxCount.Name = "lblRxCount";
            this.lblRxCount.Size = new System.Drawing.Size(97, 41);
            this.lblRxCount.TabIndex = 12;
            this.lblRxCount.Text = "label5";
            // 
            // cbAutoScroll
            // 
            this.cbAutoScroll.AutoSize = true;
            this.cbAutoScroll.Checked = true;
            this.cbAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoScroll.Location = new System.Drawing.Point(22, 765);
            this.cbAutoScroll.Name = "cbAutoScroll";
            this.cbAutoScroll.Size = new System.Drawing.Size(200, 45);
            this.cbAutoScroll.TabIndex = 1;
            this.cbAutoScroll.Text = "Auto Scroll";
            this.cbAutoScroll.UseVisualStyleBackColor = true;
            this.cbAutoScroll.CheckedChanged += new System.EventHandler(this.cbAutoScroll_CheckedChanged);
            // 
            // lbRxConsole
            // 
            this.lbRxConsole.FormattingEnabled = true;
            this.lbRxConsole.ItemHeight = 41;
            this.lbRxConsole.Location = new System.Drawing.Point(22, 76);
            this.lbRxConsole.Margin = new System.Windows.Forms.Padding(4);
            this.lbRxConsole.Name = "lbRxConsole";
            this.lbRxConsole.ScrollAlwaysVisible = true;
            this.lbRxConsole.Size = new System.Drawing.Size(1066, 660);
            this.lbRxConsole.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.numPeriodSet);
            this.groupBox3.Controls.Add(this.cbSendPeriodic);
            this.groupBox3.Controls.Add(this.btnSendCanMsg);
            this.groupBox3.Location = new System.Drawing.Point(1141, 424);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(940, 846);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "TX Console";
            // 
            // cbSendPeriodic
            // 
            this.cbSendPeriodic.AutoSize = true;
            this.cbSendPeriodic.Location = new System.Drawing.Point(130, 354);
            this.cbSendPeriodic.Name = "cbSendPeriodic";
            this.cbSendPeriodic.Size = new System.Drawing.Size(237, 45);
            this.cbSendPeriodic.TabIndex = 2;
            this.cbSendPeriodic.Text = "Send Periodic";
            this.cbSendPeriodic.UseVisualStyleBackColor = true;
            this.cbSendPeriodic.CheckedChanged += new System.EventHandler(this.cbSendPeriodic_CheckedChanged);
            // 
            // numPeriodSet
            // 
            this.numPeriodSet.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPeriodSet.Location = new System.Drawing.Point(218, 275);
            this.numPeriodSet.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numPeriodSet.Name = "numPeriodSet";
            this.numPeriodSet.Size = new System.Drawing.Size(194, 47);
            this.numPeriodSet.TabIndex = 3;
            this.numPeriodSet.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(101, 281);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 41);
            this.label5.TabIndex = 13;
            this.label5.Text = "Period:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(428, 281);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 41);
            this.label7.TabIndex = 14;
            this.label7.Text = "ms";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 41F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2249, 1422);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPeriodSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Button btnSendCanMsg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDriverVersion;
        private System.Windows.Forms.TextBox tbHardwareVersion;
        private System.Windows.Forms.TextBox tbManufacturer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lbRxConsole;
        private System.Windows.Forms.CheckBox cbAutoScroll;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblRxCount;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbSendPeriodic;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numPeriodSet;
    }
}

