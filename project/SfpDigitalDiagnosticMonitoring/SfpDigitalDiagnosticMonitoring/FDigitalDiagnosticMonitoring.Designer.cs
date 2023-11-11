namespace SfpDigitalDiagnosticMonitoring
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
            this.TcSelect = new System.Windows.Forms.TabControl();
            this.tpA0h = new System.Windows.Forms.TabPage();
            this.tpA2h = new System.Windows.Forms.TabPage();
            this.tpMemoryDump = new System.Windows.Forms.TabPage();
            this.cbConnected = new System.Windows.Forms.CheckBox();
            this.cbFirmwareVersionCheck = new System.Windows.Forms.CheckBox();
            this.tbFirmwareDateCheck = new System.Windows.Forms.TextBox();
            this.tbFirmwareVersionCheck = new System.Windows.Forms.TextBox();
            this.tbFirmwareDate = new System.Windows.Forms.TextBox();
            this.lFirmwareDate = new System.Windows.Forms.Label();
            this.tbFirmwareVersion = new System.Windows.Forms.TextBox();
            this.lFirmwareVersion = new System.Windows.Forms.Label();
            this.tbPasswordB3 = new System.Windows.Forms.TextBox();
            this.tbPasswordB2 = new System.Windows.Forms.TextBox();
            this.tbPasswordB1 = new System.Windows.Forms.TextBox();
            this.tbPasswordB0 = new System.Windows.Forms.TextBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.ucA0h = new SfpDigitalDiagnosticMonitoring.UcA0h();
            this.ucA2h = new SfpDigitalDiagnosticMonitoring.UcA2h();
            this.ucMemoryDump = new SfpDigitalDiagnosticMonitoring.UcMemoryDump();
            this.TcSelect.SuspendLayout();
            this.tpA0h.SuspendLayout();
            this.tpA2h.SuspendLayout();
            this.tpMemoryDump.SuspendLayout();
            this.SuspendLayout();
            // 
            // TcSelect
            // 
            this.TcSelect.Controls.Add(this.tpA0h);
            this.TcSelect.Controls.Add(this.tpA2h);
            this.TcSelect.Controls.Add(this.tpMemoryDump);
            this.TcSelect.Location = new System.Drawing.Point(12, 34);
            this.TcSelect.Name = "TcSelect";
            this.TcSelect.SelectedIndex = 0;
            this.TcSelect.Size = new System.Drawing.Size(1034, 485);
            this.TcSelect.TabIndex = 0;
            // 
            // tpA0h
            // 
            this.tpA0h.Controls.Add(this.ucA0h);
            this.tpA0h.Location = new System.Drawing.Point(4, 22);
            this.tpA0h.Name = "tpA0h";
            this.tpA0h.Padding = new System.Windows.Forms.Padding(3);
            this.tpA0h.Size = new System.Drawing.Size(1026, 459);
            this.tpA0h.TabIndex = 0;
            this.tpA0h.Text = "A0h";
            this.tpA0h.UseVisualStyleBackColor = true;
            // 
            // tpA2h
            // 
            this.tpA2h.Controls.Add(this.ucA2h);
            this.tpA2h.Location = new System.Drawing.Point(4, 22);
            this.tpA2h.Name = "tpA2h";
            this.tpA2h.Padding = new System.Windows.Forms.Padding(3);
            this.tpA2h.Size = new System.Drawing.Size(1026, 459);
            this.tpA2h.TabIndex = 1;
            this.tpA2h.Text = "A2h";
            this.tpA2h.UseVisualStyleBackColor = true;
            // 
            // tpMemoryDump
            // 
            this.tpMemoryDump.Controls.Add(this.ucMemoryDump);
            this.tpMemoryDump.Location = new System.Drawing.Point(4, 22);
            this.tpMemoryDump.Name = "tpMemoryDump";
            this.tpMemoryDump.Padding = new System.Windows.Forms.Padding(3);
            this.tpMemoryDump.Size = new System.Drawing.Size(1026, 459);
            this.tpMemoryDump.TabIndex = 2;
            this.tpMemoryDump.Text = "MemDump";
            this.tpMemoryDump.UseVisualStyleBackColor = true;
            // 
            // cbConnected
            // 
            this.cbConnected.AutoSize = true;
            this.cbConnected.Location = new System.Drawing.Point(972, 12);
            this.cbConnected.Name = "cbConnected";
            this.cbConnected.Size = new System.Drawing.Size(74, 16);
            this.cbConnected.TabIndex = 1;
            this.cbConnected.Text = "Connected";
            this.cbConnected.UseVisualStyleBackColor = true;
            this.cbConnected.CheckedChanged += new System.EventHandler(this.cbI2cConnected_CheckedChanged);
            // 
            // cbFirmwareVersionCheck
            // 
            this.cbFirmwareVersionCheck.AutoSize = true;
            this.cbFirmwareVersionCheck.Location = new System.Drawing.Point(485, 12);
            this.cbFirmwareVersionCheck.Name = "cbFirmwareVersionCheck";
            this.cbFirmwareVersionCheck.Size = new System.Drawing.Size(140, 16);
            this.cbFirmwareVersionCheck.TabIndex = 16;
            this.cbFirmwareVersionCheck.Text = "Firmware Version Check";
            this.cbFirmwareVersionCheck.UseVisualStyleBackColor = true;
            // 
            // tbFirmwareDateCheck
            // 
            this.tbFirmwareDateCheck.Location = new System.Drawing.Point(854, 10);
            this.tbFirmwareDateCheck.Name = "tbFirmwareDateCheck";
            this.tbFirmwareDateCheck.Size = new System.Drawing.Size(60, 22);
            this.tbFirmwareDateCheck.TabIndex = 15;
            // 
            // tbFirmwareVersionCheck
            // 
            this.tbFirmwareVersionCheck.Location = new System.Drawing.Point(717, 10);
            this.tbFirmwareVersionCheck.Name = "tbFirmwareVersionCheck";
            this.tbFirmwareVersionCheck.Size = new System.Drawing.Size(30, 22);
            this.tbFirmwareVersionCheck.TabIndex = 14;
            // 
            // tbFirmwareDate
            // 
            this.tbFirmwareDate.Location = new System.Drawing.Point(788, 10);
            this.tbFirmwareDate.Name = "tbFirmwareDate";
            this.tbFirmwareDate.ReadOnly = true;
            this.tbFirmwareDate.Size = new System.Drawing.Size(60, 22);
            this.tbFirmwareDate.TabIndex = 13;
            // 
            // lFirmwareDate
            // 
            this.lFirmwareDate.AutoSize = true;
            this.lFirmwareDate.Location = new System.Drawing.Point(753, 13);
            this.lFirmwareDate.Name = "lFirmwareDate";
            this.lFirmwareDate.Size = new System.Drawing.Size(29, 12);
            this.lFirmwareDate.TabIndex = 12;
            this.lFirmwareDate.Text = "Date:";
            // 
            // tbFirmwareVersion
            // 
            this.tbFirmwareVersion.Location = new System.Drawing.Point(681, 10);
            this.tbFirmwareVersion.Name = "tbFirmwareVersion";
            this.tbFirmwareVersion.ReadOnly = true;
            this.tbFirmwareVersion.Size = new System.Drawing.Size(30, 22);
            this.tbFirmwareVersion.TabIndex = 11;
            // 
            // lFirmwareVersion
            // 
            this.lFirmwareVersion.AutoSize = true;
            this.lFirmwareVersion.Location = new System.Drawing.Point(631, 13);
            this.lFirmwareVersion.Name = "lFirmwareVersion";
            this.lFirmwareVersion.Size = new System.Drawing.Size(44, 12);
            this.lFirmwareVersion.TabIndex = 10;
            this.lFirmwareVersion.Text = "Version:";
            // 
            // tbPasswordB3
            // 
            this.tbPasswordB3.Location = new System.Drawing.Point(428, 10);
            this.tbPasswordB3.Name = "tbPasswordB3";
            this.tbPasswordB3.PasswordChar = '*';
            this.tbPasswordB3.Size = new System.Drawing.Size(25, 22);
            this.tbPasswordB3.TabIndex = 21;
            this.tbPasswordB3.Text = "34";
            // 
            // tbPasswordB2
            // 
            this.tbPasswordB2.Location = new System.Drawing.Point(397, 10);
            this.tbPasswordB2.Name = "tbPasswordB2";
            this.tbPasswordB2.PasswordChar = '*';
            this.tbPasswordB2.Size = new System.Drawing.Size(25, 22);
            this.tbPasswordB2.TabIndex = 20;
            this.tbPasswordB2.Text = "33";
            // 
            // tbPasswordB1
            // 
            this.tbPasswordB1.Location = new System.Drawing.Point(366, 10);
            this.tbPasswordB1.Name = "tbPasswordB1";
            this.tbPasswordB1.PasswordChar = '*';
            this.tbPasswordB1.Size = new System.Drawing.Size(25, 22);
            this.tbPasswordB1.TabIndex = 19;
            this.tbPasswordB1.Text = "32";
            // 
            // tbPasswordB0
            // 
            this.tbPasswordB0.Location = new System.Drawing.Point(335, 10);
            this.tbPasswordB0.Name = "tbPasswordB0";
            this.tbPasswordB0.PasswordChar = '*';
            this.tbPasswordB0.Size = new System.Drawing.Size(25, 22);
            this.tbPasswordB0.TabIndex = 18;
            this.tbPasswordB0.Text = "33";
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point(275, 13);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(54, 12);
            this.lPassword.TabIndex = 17;
            this.lPassword.Text = "Password :";
            // 
            // ucA0h
            // 
            this.ucA0h.Location = new System.Drawing.Point(6, 6);
            this.ucA0h.Name = "ucA0h";
            this.ucA0h.Size = new System.Drawing.Size(786, 385);
            this.ucA0h.TabIndex = 0;
            // 
            // ucA2h
            // 
            this.ucA2h.Location = new System.Drawing.Point(6, 6);
            this.ucA2h.Name = "ucA2h";
            this.ucA2h.Size = new System.Drawing.Size(1020, 453);
            this.ucA2h.TabIndex = 0;
            // 
            // ucMemoryDump
            // 
            this.ucMemoryDump.Location = new System.Drawing.Point(6, 6);
            this.ucMemoryDump.Name = "ucMemoryDump";
            this.ucMemoryDump.Size = new System.Drawing.Size(855, 385);
            this.ucMemoryDump.TabIndex = 0;
            // 
            // FDigitalDiagnosticMonitoring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 524);
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
            this.Controls.Add(this.cbConnected);
            this.Controls.Add(this.TcSelect);
            this.Name = "FDigitalDiagnosticMonitoring";
            this.Text = "Digital Diagnostic Monitoring 20230927_0806";
            this.TcSelect.ResumeLayout(false);
            this.tpA0h.ResumeLayout(false);
            this.tpA2h.ResumeLayout(false);
            this.tpMemoryDump.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl TcSelect;
        private System.Windows.Forms.TabPage tpA0h;
        private System.Windows.Forms.TabPage tpA2h;
        private System.Windows.Forms.CheckBox cbConnected;
        private UcA0h ucA0h;
        private UcA2h ucA2h;
        private System.Windows.Forms.CheckBox cbFirmwareVersionCheck;
        private System.Windows.Forms.TextBox tbFirmwareDateCheck;
        private System.Windows.Forms.TextBox tbFirmwareVersionCheck;
        private System.Windows.Forms.TextBox tbFirmwareDate;
        private System.Windows.Forms.Label lFirmwareDate;
        private System.Windows.Forms.TextBox tbFirmwareVersion;
        private System.Windows.Forms.Label lFirmwareVersion;
        private System.Windows.Forms.TextBox tbPasswordB3;
        private System.Windows.Forms.TextBox tbPasswordB2;
        private System.Windows.Forms.TextBox tbPasswordB1;
        private System.Windows.Forms.TextBox tbPasswordB0;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.TabPage tpMemoryDump;
        private UcMemoryDump ucMemoryDump;
    }
}

