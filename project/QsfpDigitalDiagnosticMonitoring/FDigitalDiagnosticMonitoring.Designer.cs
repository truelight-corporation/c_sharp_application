namespace QsfpDigitalDiagnosticMonitoring
{
    partial class FDigitalDiagnosticMonitoring
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbConnected = new System.Windows.Forms.CheckBox();
            this.tcDdmAndInformation = new System.Windows.Forms.TabControl();
            this.tpDigitalDiagnosticMonitoring = new System.Windows.Forms.TabPage();
            this.tpInformation = new System.Windows.Forms.TabPage();
            this.tpMemoryDump = new System.Windows.Forms.TabPage();
            this.lFirmwareVersion = new System.Windows.Forms.Label();
            this.tbFirmwareVersion = new System.Windows.Forms.TextBox();
            this.lFirmwareDate = new System.Windows.Forms.Label();
            this.tbFirmwareDate = new System.Windows.Forms.TextBox();
            this.tbFirmwareVersionCheck = new System.Windows.Forms.TextBox();
            this.tbFirmwareDateCheck = new System.Windows.Forms.TextBox();
            this.cbFirmwareVersionCheck = new System.Windows.Forms.CheckBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.tbPasswordB0 = new System.Windows.Forms.TextBox();
            this.tbPasswordB1 = new System.Windows.Forms.TextBox();
            this.tbPasswordB2 = new System.Windows.Forms.TextBox();
            this.tbPasswordB3 = new System.Windows.Forms.TextBox();
            this.tpI2cActionLog = new System.Windows.Forms.TabPage();
            this.tbI2cActionLog = new System.Windows.Forms.TextBox();
            this.bClearI2cActionLog = new System.Windows.Forms.Button();
            this.cbI2cActionLog = new System.Windows.Forms.CheckBox();
            this.ucDigitalDiagnosticsMonitoring = new QsfpDigitalDiagnosticMonitoring.UCDigitalDiagnosticsMonitoring();
            this.ucInformation = new QsfpDigitalDiagnosticMonitoring.UcInformation();
            this.ucMemoryDump = new QsfpDigitalDiagnosticMonitoring.UCMemoryDump();
            this.tcDdmAndInformation.SuspendLayout();
            this.tpDigitalDiagnosticMonitoring.SuspendLayout();
            this.tpInformation.SuspendLayout();
            this.tpMemoryDump.SuspendLayout();
            this.tpI2cActionLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbConnected
            // 
            this.cbConnected.AutoSize = true;
            this.cbConnected.Location = new System.Drawing.Point(855, 12);
            this.cbConnected.Name = "cbConnected";
            this.cbConnected.Size = new System.Drawing.Size(74, 16);
            this.cbConnected.TabIndex = 1;
            this.cbConnected.Text = "Connected";
            this.cbConnected.UseVisualStyleBackColor = true;
            this.cbConnected.CheckedChanged += new System.EventHandler(this._cbConnected_CheckedChanged);
            // 
            // tcDdmAndInformation
            // 
            this.tcDdmAndInformation.Controls.Add(this.tpDigitalDiagnosticMonitoring);
            this.tcDdmAndInformation.Controls.Add(this.tpInformation);
            this.tcDdmAndInformation.Controls.Add(this.tpMemoryDump);
            this.tcDdmAndInformation.Controls.Add(this.tpI2cActionLog);
            this.tcDdmAndInformation.Location = new System.Drawing.Point(12, 38);
            this.tcDdmAndInformation.Name = "tcDdmAndInformation";
            this.tcDdmAndInformation.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tcDdmAndInformation.SelectedIndex = 0;
            this.tcDdmAndInformation.Size = new System.Drawing.Size(921, 745);
            this.tcDdmAndInformation.TabIndex = 2;
            // 
            // tpDigitalDiagnosticMonitoring
            // 
            this.tpDigitalDiagnosticMonitoring.Controls.Add(this.ucDigitalDiagnosticsMonitoring);
            this.tpDigitalDiagnosticMonitoring.Location = new System.Drawing.Point(4, 22);
            this.tpDigitalDiagnosticMonitoring.Name = "tpDigitalDiagnosticMonitoring";
            this.tpDigitalDiagnosticMonitoring.Padding = new System.Windows.Forms.Padding(3);
            this.tpDigitalDiagnosticMonitoring.Size = new System.Drawing.Size(913, 719);
            this.tpDigitalDiagnosticMonitoring.TabIndex = 0;
            this.tpDigitalDiagnosticMonitoring.Text = "DDM";
            this.tpDigitalDiagnosticMonitoring.UseVisualStyleBackColor = true;
            // 
            // tpInformation
            // 
            this.tpInformation.Controls.Add(this.ucInformation);
            this.tpInformation.Location = new System.Drawing.Point(4, 22);
            this.tpInformation.Name = "tpInformation";
            this.tpInformation.Padding = new System.Windows.Forms.Padding(3);
            this.tpInformation.Size = new System.Drawing.Size(913, 719);
            this.tpInformation.TabIndex = 1;
            this.tpInformation.Text = "Information";
            this.tpInformation.UseVisualStyleBackColor = true;
            // 
            // tpMemoryDump
            // 
            this.tpMemoryDump.Controls.Add(this.ucMemoryDump);
            this.tpMemoryDump.Location = new System.Drawing.Point(4, 22);
            this.tpMemoryDump.Name = "tpMemoryDump";
            this.tpMemoryDump.Padding = new System.Windows.Forms.Padding(3);
            this.tpMemoryDump.Size = new System.Drawing.Size(913, 719);
            this.tpMemoryDump.TabIndex = 2;
            this.tpMemoryDump.Text = "MemDump";
            this.tpMemoryDump.UseVisualStyleBackColor = true;
            // 
            // lFirmwareVersion
            // 
            this.lFirmwareVersion.AutoSize = true;
            this.lFirmwareVersion.Location = new System.Drawing.Point(562, 13);
            this.lFirmwareVersion.Name = "lFirmwareVersion";
            this.lFirmwareVersion.Size = new System.Drawing.Size(44, 12);
            this.lFirmwareVersion.TabIndex = 3;
            this.lFirmwareVersion.Text = "Version:";
            // 
            // tbFirmwareVersion
            // 
            this.tbFirmwareVersion.Location = new System.Drawing.Point(612, 10);
            this.tbFirmwareVersion.Name = "tbFirmwareVersion";
            this.tbFirmwareVersion.ReadOnly = true;
            this.tbFirmwareVersion.Size = new System.Drawing.Size(30, 22);
            this.tbFirmwareVersion.TabIndex = 4;
            // 
            // lFirmwareDate
            // 
            this.lFirmwareDate.AutoSize = true;
            this.lFirmwareDate.Location = new System.Drawing.Point(684, 13);
            this.lFirmwareDate.Name = "lFirmwareDate";
            this.lFirmwareDate.Size = new System.Drawing.Size(29, 12);
            this.lFirmwareDate.TabIndex = 5;
            this.lFirmwareDate.Text = "Date:";
            // 
            // tbFirmwareDate
            // 
            this.tbFirmwareDate.Location = new System.Drawing.Point(719, 10);
            this.tbFirmwareDate.Name = "tbFirmwareDate";
            this.tbFirmwareDate.ReadOnly = true;
            this.tbFirmwareDate.Size = new System.Drawing.Size(60, 22);
            this.tbFirmwareDate.TabIndex = 6;
            // 
            // tbFirmwareVersionCheck
            // 
            this.tbFirmwareVersionCheck.Location = new System.Drawing.Point(648, 10);
            this.tbFirmwareVersionCheck.Name = "tbFirmwareVersionCheck";
            this.tbFirmwareVersionCheck.Size = new System.Drawing.Size(30, 22);
            this.tbFirmwareVersionCheck.TabIndex = 7;
            // 
            // tbFirmwareDateCheck
            // 
            this.tbFirmwareDateCheck.Location = new System.Drawing.Point(785, 10);
            this.tbFirmwareDateCheck.Name = "tbFirmwareDateCheck";
            this.tbFirmwareDateCheck.Size = new System.Drawing.Size(60, 22);
            this.tbFirmwareDateCheck.TabIndex = 8;
            // 
            // cbFirmwareVersionCheck
            // 
            this.cbFirmwareVersionCheck.AutoSize = true;
            this.cbFirmwareVersionCheck.Location = new System.Drawing.Point(416, 12);
            this.cbFirmwareVersionCheck.Name = "cbFirmwareVersionCheck";
            this.cbFirmwareVersionCheck.Size = new System.Drawing.Size(140, 16);
            this.cbFirmwareVersionCheck.TabIndex = 9;
            this.cbFirmwareVersionCheck.Text = "Firmware Version Check";
            this.cbFirmwareVersionCheck.UseVisualStyleBackColor = true;
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point(188, 13);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(78, 12);
            this.lPassword.TabIndex = 10;
            this.lPassword.Text = "Password(Hex):";
            // 
            // tbPasswordB0
            // 
            this.tbPasswordB0.Location = new System.Drawing.Point(272, 10);
            this.tbPasswordB0.Name = "tbPasswordB0";
            this.tbPasswordB0.Size = new System.Drawing.Size(30, 22);
            this.tbPasswordB0.TabIndex = 11;
            this.tbPasswordB0.Text = "33";
            this.tbPasswordB0.UseSystemPasswordChar = true;
            // 
            // tbPasswordB1
            // 
            this.tbPasswordB1.Location = new System.Drawing.Point(308, 10);
            this.tbPasswordB1.Name = "tbPasswordB1";
            this.tbPasswordB1.Size = new System.Drawing.Size(30, 22);
            this.tbPasswordB1.TabIndex = 12;
            this.tbPasswordB1.Text = "32";
            this.tbPasswordB1.UseSystemPasswordChar = true;
            // 
            // tbPasswordB2
            // 
            this.tbPasswordB2.Location = new System.Drawing.Point(344, 10);
            this.tbPasswordB2.Name = "tbPasswordB2";
            this.tbPasswordB2.Size = new System.Drawing.Size(30, 22);
            this.tbPasswordB2.TabIndex = 13;
            this.tbPasswordB2.Text = "33";
            this.tbPasswordB2.UseSystemPasswordChar = true;
            // 
            // tbPasswordB3
            // 
            this.tbPasswordB3.Location = new System.Drawing.Point(380, 10);
            this.tbPasswordB3.Name = "tbPasswordB3";
            this.tbPasswordB3.Size = new System.Drawing.Size(30, 22);
            this.tbPasswordB3.TabIndex = 14;
            this.tbPasswordB3.Text = "34";
            this.tbPasswordB3.UseSystemPasswordChar = true;
            // 
            // tpI2cActionLog
            // 
            this.tpI2cActionLog.Controls.Add(this.bClearI2cActionLog);
            this.tpI2cActionLog.Controls.Add(this.tbI2cActionLog);
            this.tpI2cActionLog.Location = new System.Drawing.Point(4, 22);
            this.tpI2cActionLog.Name = "tpI2cActionLog";
            this.tpI2cActionLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpI2cActionLog.Size = new System.Drawing.Size(913, 719);
            this.tpI2cActionLog.TabIndex = 3;
            this.tpI2cActionLog.Text = "I2cActionLog";
            this.tpI2cActionLog.UseVisualStyleBackColor = true;
            // 
            // tbI2cActionLog
            // 
            this.tbI2cActionLog.Location = new System.Drawing.Point(6, 6);
            this.tbI2cActionLog.Multiline = true;
            this.tbI2cActionLog.Name = "tbI2cActionLog";
            this.tbI2cActionLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbI2cActionLog.Size = new System.Drawing.Size(820, 675);
            this.tbI2cActionLog.TabIndex = 0;
            // 
            // bClearI2cActionLog
            // 
            this.bClearI2cActionLog.Location = new System.Drawing.Point(832, 6);
            this.bClearI2cActionLog.Name = "bClearI2cActionLog";
            this.bClearI2cActionLog.Size = new System.Drawing.Size(75, 685);
            this.bClearI2cActionLog.TabIndex = 1;
            this.bClearI2cActionLog.Text = "Clear";
            this.bClearI2cActionLog.UseVisualStyleBackColor = true;
            this.bClearI2cActionLog.Click += new System.EventHandler(this.bClearI2cActionLog_Click);
            // 
            // cbI2cActionLog
            // 
            this.cbI2cActionLog.AutoSize = true;
            this.cbI2cActionLog.Location = new System.Drawing.Point(12, 12);
            this.cbI2cActionLog.Name = "cbI2cActionLog";
            this.cbI2cActionLog.Size = new System.Drawing.Size(91, 16);
            this.cbI2cActionLog.TabIndex = 15;
            this.cbI2cActionLog.Text = "I2C action log";
            this.cbI2cActionLog.UseVisualStyleBackColor = true;
            // 
            // ucDigitalDiagnosticsMonitoring
            // 
            this.ucDigitalDiagnosticsMonitoring.Location = new System.Drawing.Point(6, 4);
            this.ucDigitalDiagnosticsMonitoring.Name = "ucDigitalDiagnosticsMonitoring";
            this.ucDigitalDiagnosticsMonitoring.Size = new System.Drawing.Size(823, 659);
            this.ucDigitalDiagnosticsMonitoring.TabIndex = 0;
            // 
            // ucInformation
            // 
            this.ucInformation.Location = new System.Drawing.Point(3, 3);
            this.ucInformation.Name = "ucInformation";
            this.ucInformation.Size = new System.Drawing.Size(907, 714);
            this.ucInformation.TabIndex = 0;
            // 
            // ucMemoryDump
            // 
            this.ucMemoryDump.Location = new System.Drawing.Point(6, 6);
            this.ucMemoryDump.Name = "ucMemoryDump";
            this.ucMemoryDump.Size = new System.Drawing.Size(901, 708);
            this.ucMemoryDump.TabIndex = 0;
            // 
            // FDigitalDiagnosticMonitoring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(945, 786);
            this.Controls.Add(this.cbI2cActionLog);
            this.Controls.Add(this.tbPasswordB3);
            this.Controls.Add(this.tbPasswordB2);
            this.Controls.Add(this.tbPasswordB1);
            this.Controls.Add(this.tbPasswordB0);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.cbFirmwareVersionCheck);
            this.Controls.Add(this.tbFirmwareDateCheck);
            this.Controls.Add(this.tbFirmwareVersionCheck);
            this.Controls.Add(this.tbFirmwareDate);
            this.Controls.Add(this.lFirmwareDate);
            this.Controls.Add(this.tbFirmwareVersion);
            this.Controls.Add(this.lFirmwareVersion);
            this.Controls.Add(this.tcDdmAndInformation);
            this.Controls.Add(this.cbConnected);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FDigitalDiagnosticMonitoring";
            this.Text = "Digital Diagnostic Monitoring 20240708_1953";
            this.tcDdmAndInformation.ResumeLayout(false);
            this.tpDigitalDiagnosticMonitoring.ResumeLayout(false);
            this.tpInformation.ResumeLayout(false);
            this.tpMemoryDump.ResumeLayout(false);
            this.tpI2cActionLog.ResumeLayout(false);
            this.tpI2cActionLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCDigitalDiagnosticsMonitoring ucDigitalDiagnosticsMonitoring;
        private System.Windows.Forms.CheckBox cbConnected;
        private System.Windows.Forms.TabControl tcDdmAndInformation;
        private System.Windows.Forms.TabPage tpDigitalDiagnosticMonitoring;
        private System.Windows.Forms.TabPage tpInformation;
        private UcInformation ucInformation;
        private System.Windows.Forms.Label lFirmwareVersion;
        private System.Windows.Forms.TextBox tbFirmwareVersion;
        private System.Windows.Forms.Label lFirmwareDate;
        private System.Windows.Forms.TextBox tbFirmwareDate;
        private System.Windows.Forms.TextBox tbFirmwareVersionCheck;
        private System.Windows.Forms.TextBox tbFirmwareDateCheck;
        private System.Windows.Forms.CheckBox cbFirmwareVersionCheck;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.TextBox tbPasswordB0;
        private System.Windows.Forms.TextBox tbPasswordB1;
        private System.Windows.Forms.TextBox tbPasswordB2;
        private System.Windows.Forms.TextBox tbPasswordB3;
        private System.Windows.Forms.TabPage tpMemoryDump;
        private UCMemoryDump ucMemoryDump;
        private System.Windows.Forms.TabPage tpI2cActionLog;
        private System.Windows.Forms.Button bClearI2cActionLog;
        private System.Windows.Forms.TextBox tbI2cActionLog;
        private System.Windows.Forms.CheckBox cbI2cActionLog;
    }
}

