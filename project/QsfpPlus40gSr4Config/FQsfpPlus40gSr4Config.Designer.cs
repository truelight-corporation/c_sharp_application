namespace QsfpPlus40gSr4Config
{
    partial class fQsfpPlus40gSr4Config
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
            this.tcFunctionSelect = new System.Windows.Forms.TabControl();
            this.tpGn1090 = new System.Windows.Forms.TabPage();
            this.ucGn1090Config = new Gn1090Gn1190Config.UcGn1090Config();
            this.tpGn1190 = new System.Windows.Forms.TabPage();
            this.ucGn1190Config = new Gn1090Gn1190Config.UcGn1190Config();
            this.tpCorrector = new System.Windows.Forms.TabPage();
            this.ucGn1190Corrector = new Gn1190Corrector.UcGn1190Corrector();
            this.tbFirmwareDate = new System.Windows.Forms.TextBox();
            this.lFirmwareDate = new System.Windows.Forms.Label();
            this.tbFirmwareVersion = new System.Windows.Forms.TextBox();
            this.lFirmwareVersion = new System.Windows.Forms.Label();
            this.tbSerialNumber = new System.Windows.Forms.TextBox();
            this.lSerialNumber = new System.Windows.Forms.Label();
            this.tcFunctionSelect.SuspendLayout();
            this.tpGn1090.SuspendLayout();
            this.tpGn1190.SuspendLayout();
            this.tpCorrector.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbConnected
            // 
            this.cbConnected.AutoSize = true;
            this.cbConnected.Location = new System.Drawing.Point(706, 12);
            this.cbConnected.Name = "cbConnected";
            this.cbConnected.Size = new System.Drawing.Size(74, 16);
            this.cbConnected.TabIndex = 0;
            this.cbConnected.Text = "Connected";
            this.cbConnected.UseVisualStyleBackColor = true;
            this.cbConnected.CheckedChanged += new System.EventHandler(this.cbConnected_CheckedChanged);
            // 
            // tcFunctionSelect
            // 
            this.tcFunctionSelect.Controls.Add(this.tpGn1090);
            this.tcFunctionSelect.Controls.Add(this.tpGn1190);
            this.tcFunctionSelect.Controls.Add(this.tpCorrector);
            this.tcFunctionSelect.Location = new System.Drawing.Point(12, 34);
            this.tcFunctionSelect.Name = "tcFunctionSelect";
            this.tcFunctionSelect.SelectedIndex = 0;
            this.tcFunctionSelect.Size = new System.Drawing.Size(778, 792);
            this.tcFunctionSelect.TabIndex = 1;
            // 
            // tpGn1090
            // 
            this.tpGn1090.Controls.Add(this.ucGn1090Config);
            this.tpGn1090.Location = new System.Drawing.Point(4, 22);
            this.tpGn1090.Name = "tpGn1090";
            this.tpGn1090.Padding = new System.Windows.Forms.Padding(3);
            this.tpGn1090.Size = new System.Drawing.Size(770, 840);
            this.tpGn1090.TabIndex = 0;
            this.tpGn1090.Text = "GN1090";
            this.tpGn1090.UseVisualStyleBackColor = true;
            // 
            // ucGn1090Config
            // 
            this.ucGn1090Config.Location = new System.Drawing.Point(6, 6);
            this.ucGn1090Config.Name = "ucGn1090Config";
            this.ucGn1090Config.Size = new System.Drawing.Size(643, 314);
            this.ucGn1090Config.TabIndex = 0;
            // 
            // tpGn1190
            // 
            this.tpGn1190.Controls.Add(this.ucGn1190Config);
            this.tpGn1190.Location = new System.Drawing.Point(4, 22);
            this.tpGn1190.Name = "tpGn1190";
            this.tpGn1190.Padding = new System.Windows.Forms.Padding(3);
            this.tpGn1190.Size = new System.Drawing.Size(770, 840);
            this.tpGn1190.TabIndex = 1;
            this.tpGn1190.Text = "GN1190";
            this.tpGn1190.UseVisualStyleBackColor = true;
            // 
            // ucGn1190Config
            // 
            this.ucGn1190Config.Location = new System.Drawing.Point(6, 6);
            this.ucGn1190Config.Name = "ucGn1190Config";
            this.ucGn1190Config.Size = new System.Drawing.Size(643, 523);
            this.ucGn1190Config.TabIndex = 0;
            // 
            // tpCorrector
            // 
            this.tpCorrector.Controls.Add(this.ucGn1190Corrector);
            this.tpCorrector.Location = new System.Drawing.Point(4, 22);
            this.tpCorrector.Name = "tpCorrector";
            this.tpCorrector.Padding = new System.Windows.Forms.Padding(3);
            this.tpCorrector.Size = new System.Drawing.Size(770, 766);
            this.tpCorrector.TabIndex = 2;
            this.tpCorrector.Text = "Corrector";
            this.tpCorrector.UseVisualStyleBackColor = true;
            // 
            // ucGn1190Corrector
            // 
            this.ucGn1190Corrector.Location = new System.Drawing.Point(6, 6);
            this.ucGn1190Corrector.Name = "ucGn1190Corrector";
            this.ucGn1190Corrector.Size = new System.Drawing.Size(758, 755);
            this.ucGn1190Corrector.TabIndex = 0;
            // 
            // tbFirmwareDate
            // 
            this.tbFirmwareDate.Location = new System.Drawing.Point(640, 10);
            this.tbFirmwareDate.Name = "tbFirmwareDate";
            this.tbFirmwareDate.ReadOnly = true;
            this.tbFirmwareDate.Size = new System.Drawing.Size(60, 22);
            this.tbFirmwareDate.TabIndex = 10;
            // 
            // lFirmwareDate
            // 
            this.lFirmwareDate.AutoSize = true;
            this.lFirmwareDate.Location = new System.Drawing.Point(605, 13);
            this.lFirmwareDate.Name = "lFirmwareDate";
            this.lFirmwareDate.Size = new System.Drawing.Size(29, 12);
            this.lFirmwareDate.TabIndex = 9;
            this.lFirmwareDate.Text = "Date:";
            // 
            // tbFirmwareVersion
            // 
            this.tbFirmwareVersion.Location = new System.Drawing.Point(569, 10);
            this.tbFirmwareVersion.Name = "tbFirmwareVersion";
            this.tbFirmwareVersion.ReadOnly = true;
            this.tbFirmwareVersion.Size = new System.Drawing.Size(30, 22);
            this.tbFirmwareVersion.TabIndex = 8;
            // 
            // lFirmwareVersion
            // 
            this.lFirmwareVersion.AutoSize = true;
            this.lFirmwareVersion.Location = new System.Drawing.Point(519, 13);
            this.lFirmwareVersion.Name = "lFirmwareVersion";
            this.lFirmwareVersion.Size = new System.Drawing.Size(44, 12);
            this.lFirmwareVersion.TabIndex = 7;
            this.lFirmwareVersion.Text = "Version:";
            // 
            // tbSerialNumber
            // 
            this.tbSerialNumber.Location = new System.Drawing.Point(433, 10);
            this.tbSerialNumber.Name = "tbSerialNumber";
            this.tbSerialNumber.ReadOnly = true;
            this.tbSerialNumber.Size = new System.Drawing.Size(80, 22);
            this.tbSerialNumber.TabIndex = 12;
            // 
            // lSerialNumber
            // 
            this.lSerialNumber.AutoSize = true;
            this.lSerialNumber.Location = new System.Drawing.Point(352, 13);
            this.lSerialNumber.Name = "lSerialNumber";
            this.lSerialNumber.Size = new System.Drawing.Size(75, 12);
            this.lSerialNumber.TabIndex = 11;
            this.lSerialNumber.Text = "Serial Number:";
            // 
            // fQsfpPlus40gSr4Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 829);
            this.Controls.Add(this.tbSerialNumber);
            this.Controls.Add(this.lSerialNumber);
            this.Controls.Add(this.tbFirmwareDate);
            this.Controls.Add(this.lFirmwareDate);
            this.Controls.Add(this.tbFirmwareVersion);
            this.Controls.Add(this.lFirmwareVersion);
            this.Controls.Add(this.tcFunctionSelect);
            this.Controls.Add(this.cbConnected);
            this.Name = "fQsfpPlus40gSr4Config";
            this.Text = "QSFP+ 40G SR4 Config 20170926 (Firmware version >= 9)";
            this.tcFunctionSelect.ResumeLayout(false);
            this.tpGn1090.ResumeLayout(false);
            this.tpGn1190.ResumeLayout(false);
            this.tpCorrector.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbConnected;
        private System.Windows.Forms.TabControl tcFunctionSelect;
        private System.Windows.Forms.TabPage tpGn1090;
        private System.Windows.Forms.TabPage tpGn1190;
        private Gn1090Gn1190Config.UcGn1090Config ucGn1090Config;
        private Gn1090Gn1190Config.UcGn1190Config ucGn1190Config;
        private System.Windows.Forms.TabPage tpCorrector;
        private Gn1190Corrector.UcGn1190Corrector ucGn1190Corrector;
        private System.Windows.Forms.TextBox tbFirmwareDate;
        private System.Windows.Forms.Label lFirmwareDate;
        private System.Windows.Forms.TextBox tbFirmwareVersion;
        private System.Windows.Forms.Label lFirmwareVersion;
        private System.Windows.Forms.TextBox tbSerialNumber;
        private System.Windows.Forms.Label lSerialNumber;
    }
}

