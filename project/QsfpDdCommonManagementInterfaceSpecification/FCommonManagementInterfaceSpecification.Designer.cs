namespace QsfpDdCommonManagementInterfaceSpecification
{
    partial class FCommonManagementInterfaceSpecification
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cbFirmwareVersionCheck = new System.Windows.Forms.CheckBox();
            this.tbFirmwareDateCheck = new System.Windows.Forms.TextBox();
            this.tbFirmwareVersionCheck = new System.Windows.Forms.TextBox();
            this.tbFirmwareDate = new System.Windows.Forms.TextBox();
            this.lFirmwareDate = new System.Windows.Forms.Label();
            this.tbFirmwareVersion = new System.Windows.Forms.TextBox();
            this.lFirmwareVersion = new System.Windows.Forms.Label();
            this.cbConnected = new System.Windows.Forms.CheckBox();
            this.tcCmisReg = new System.Windows.Forms.TabControl();
            this.tpDDM = new System.Windows.Forms.TabPage();
            this.tpLowPage = new System.Windows.Forms.TabPage();
            this.tpUpPage00h = new System.Windows.Forms.TabPage();
            this.tpUpPage01h = new System.Windows.Forms.TabPage();
            this.tpUpPage02h = new System.Windows.Forms.TabPage();
            this.tpUpPage03h = new System.Windows.Forms.TabPage();
            this.tpUpPage10h = new System.Windows.Forms.TabPage();
            this.tpUpPage11h = new System.Windows.Forms.TabPage();
            this.tpUpPage9Fh = new System.Windows.Forms.TabPage();
            this.tpUpPageB0h = new System.Windows.Forms.TabPage();
            this.ucDigitalDiagnosticsMonitoring = new QsfpDdCommonManagementInterfaceSpecification.ucDigitalDiagnosticsMonitoring();
            this.ucUpPage111 = new QsfpDdCommonManagementInterfaceSpecification.ucUpPage11();
            this.tcCmisReg.SuspendLayout();
            this.tpDDM.SuspendLayout();
            this.tpUpPage11h.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbFirmwareVersionCheck
            // 
            this.cbFirmwareVersionCheck.AutoSize = true;
            this.cbFirmwareVersionCheck.Location = new System.Drawing.Point(483, 12);
            this.cbFirmwareVersionCheck.Name = "cbFirmwareVersionCheck";
            this.cbFirmwareVersionCheck.Size = new System.Drawing.Size(140, 16);
            this.cbFirmwareVersionCheck.TabIndex = 17;
            this.cbFirmwareVersionCheck.Text = "Firmware Version Check";
            this.cbFirmwareVersionCheck.UseVisualStyleBackColor = true;
            this.cbFirmwareVersionCheck.Visible = false;
            // 
            // tbFirmwareDateCheck
            // 
            this.tbFirmwareDateCheck.Location = new System.Drawing.Point(852, 10);
            this.tbFirmwareDateCheck.Name = "tbFirmwareDateCheck";
            this.tbFirmwareDateCheck.Size = new System.Drawing.Size(60, 22);
            this.tbFirmwareDateCheck.TabIndex = 16;
            this.tbFirmwareDateCheck.Visible = false;
            // 
            // tbFirmwareVersionCheck
            // 
            this.tbFirmwareVersionCheck.Location = new System.Drawing.Point(715, 10);
            this.tbFirmwareVersionCheck.Name = "tbFirmwareVersionCheck";
            this.tbFirmwareVersionCheck.Size = new System.Drawing.Size(30, 22);
            this.tbFirmwareVersionCheck.TabIndex = 15;
            this.tbFirmwareVersionCheck.Visible = false;
            // 
            // tbFirmwareDate
            // 
            this.tbFirmwareDate.Location = new System.Drawing.Point(786, 10);
            this.tbFirmwareDate.Name = "tbFirmwareDate";
            this.tbFirmwareDate.ReadOnly = true;
            this.tbFirmwareDate.Size = new System.Drawing.Size(60, 22);
            this.tbFirmwareDate.TabIndex = 14;
            this.tbFirmwareDate.Visible = false;
            // 
            // lFirmwareDate
            // 
            this.lFirmwareDate.AutoSize = true;
            this.lFirmwareDate.Location = new System.Drawing.Point(751, 13);
            this.lFirmwareDate.Name = "lFirmwareDate";
            this.lFirmwareDate.Size = new System.Drawing.Size(29, 12);
            this.lFirmwareDate.TabIndex = 13;
            this.lFirmwareDate.Text = "Date:";
            this.lFirmwareDate.Visible = false;
            // 
            // tbFirmwareVersion
            // 
            this.tbFirmwareVersion.Location = new System.Drawing.Point(679, 10);
            this.tbFirmwareVersion.Name = "tbFirmwareVersion";
            this.tbFirmwareVersion.ReadOnly = true;
            this.tbFirmwareVersion.Size = new System.Drawing.Size(30, 22);
            this.tbFirmwareVersion.TabIndex = 12;
            this.tbFirmwareVersion.Visible = false;
            // 
            // lFirmwareVersion
            // 
            this.lFirmwareVersion.AutoSize = true;
            this.lFirmwareVersion.Location = new System.Drawing.Point(629, 13);
            this.lFirmwareVersion.Name = "lFirmwareVersion";
            this.lFirmwareVersion.Size = new System.Drawing.Size(44, 12);
            this.lFirmwareVersion.TabIndex = 11;
            this.lFirmwareVersion.Text = "Version:";
            this.lFirmwareVersion.Visible = false;
            // 
            // cbConnected
            // 
            this.cbConnected.AutoSize = true;
            this.cbConnected.Location = new System.Drawing.Point(922, 12);
            this.cbConnected.Name = "cbConnected";
            this.cbConnected.Size = new System.Drawing.Size(74, 16);
            this.cbConnected.TabIndex = 10;
            this.cbConnected.Text = "Connected";
            this.cbConnected.UseVisualStyleBackColor = true;
            this.cbConnected.CheckedChanged += new System.EventHandler(this.cbConnected_CheckedChanged);
            // 
            // tcCmisReg
            // 
            this.tcCmisReg.Controls.Add(this.tpDDM);
            this.tcCmisReg.Controls.Add(this.tpLowPage);
            this.tcCmisReg.Controls.Add(this.tpUpPage00h);
            this.tcCmisReg.Controls.Add(this.tpUpPage01h);
            this.tcCmisReg.Controls.Add(this.tpUpPage02h);
            this.tcCmisReg.Controls.Add(this.tpUpPage03h);
            this.tcCmisReg.Controls.Add(this.tpUpPage10h);
            this.tcCmisReg.Controls.Add(this.tpUpPage11h);
            this.tcCmisReg.Controls.Add(this.tpUpPage9Fh);
            this.tcCmisReg.Controls.Add(this.tpUpPageB0h);
            this.tcCmisReg.Location = new System.Drawing.Point(12, 38);
            this.tcCmisReg.Name = "tcCmisReg";
            this.tcCmisReg.SelectedIndex = 0;
            this.tcCmisReg.Size = new System.Drawing.Size(984, 734);
            this.tcCmisReg.TabIndex = 18;
            // 
            // tpDDM
            // 
            this.tpDDM.Controls.Add(this.ucDigitalDiagnosticsMonitoring);
            this.tpDDM.Location = new System.Drawing.Point(4, 22);
            this.tpDDM.Name = "tpDDM";
            this.tpDDM.Padding = new System.Windows.Forms.Padding(3);
            this.tpDDM.Size = new System.Drawing.Size(976, 708);
            this.tpDDM.TabIndex = 9;
            this.tpDDM.Text = "DDM";
            this.tpDDM.UseVisualStyleBackColor = true;
            // 
            // tpLowPage
            // 
            this.tpLowPage.Location = new System.Drawing.Point(4, 22);
            this.tpLowPage.Name = "tpLowPage";
            this.tpLowPage.Padding = new System.Windows.Forms.Padding(3);
            this.tpLowPage.Size = new System.Drawing.Size(976, 708);
            this.tpLowPage.TabIndex = 0;
            this.tpLowPage.Text = "LP";
            this.tpLowPage.UseVisualStyleBackColor = true;
            // 
            // tpUpPage00h
            // 
            this.tpUpPage00h.Location = new System.Drawing.Point(4, 22);
            this.tpUpPage00h.Name = "tpUpPage00h";
            this.tpUpPage00h.Padding = new System.Windows.Forms.Padding(3);
            this.tpUpPage00h.Size = new System.Drawing.Size(976, 708);
            this.tpUpPage00h.TabIndex = 1;
            this.tpUpPage00h.Text = "UP00h";
            this.tpUpPage00h.UseVisualStyleBackColor = true;
            // 
            // tpUpPage01h
            // 
            this.tpUpPage01h.Location = new System.Drawing.Point(4, 22);
            this.tpUpPage01h.Name = "tpUpPage01h";
            this.tpUpPage01h.Padding = new System.Windows.Forms.Padding(3);
            this.tpUpPage01h.Size = new System.Drawing.Size(976, 708);
            this.tpUpPage01h.TabIndex = 2;
            this.tpUpPage01h.Text = "UP01h";
            this.tpUpPage01h.UseVisualStyleBackColor = true;
            // 
            // tpUpPage02h
            // 
            this.tpUpPage02h.Location = new System.Drawing.Point(4, 22);
            this.tpUpPage02h.Name = "tpUpPage02h";
            this.tpUpPage02h.Padding = new System.Windows.Forms.Padding(3);
            this.tpUpPage02h.Size = new System.Drawing.Size(976, 708);
            this.tpUpPage02h.TabIndex = 3;
            this.tpUpPage02h.Text = "UP02h";
            this.tpUpPage02h.UseVisualStyleBackColor = true;
            // 
            // tpUpPage03h
            // 
            this.tpUpPage03h.Location = new System.Drawing.Point(4, 22);
            this.tpUpPage03h.Name = "tpUpPage03h";
            this.tpUpPage03h.Padding = new System.Windows.Forms.Padding(3);
            this.tpUpPage03h.Size = new System.Drawing.Size(976, 708);
            this.tpUpPage03h.TabIndex = 4;
            this.tpUpPage03h.Text = "UP03h";
            this.tpUpPage03h.UseVisualStyleBackColor = true;
            // 
            // tpUpPage10h
            // 
            this.tpUpPage10h.Location = new System.Drawing.Point(4, 22);
            this.tpUpPage10h.Name = "tpUpPage10h";
            this.tpUpPage10h.Padding = new System.Windows.Forms.Padding(3);
            this.tpUpPage10h.Size = new System.Drawing.Size(976, 708);
            this.tpUpPage10h.TabIndex = 5;
            this.tpUpPage10h.Text = "UP10h";
            this.tpUpPage10h.UseVisualStyleBackColor = true;
            // 
            // tpUpPage11h
            // 
            this.tpUpPage11h.Controls.Add(this.ucUpPage111);
            this.tpUpPage11h.Location = new System.Drawing.Point(4, 22);
            this.tpUpPage11h.Name = "tpUpPage11h";
            this.tpUpPage11h.Padding = new System.Windows.Forms.Padding(3);
            this.tpUpPage11h.Size = new System.Drawing.Size(976, 708);
            this.tpUpPage11h.TabIndex = 6;
            this.tpUpPage11h.Text = "UP11h";
            this.tpUpPage11h.UseVisualStyleBackColor = true;
            // 
            // tpUpPage9Fh
            // 
            this.tpUpPage9Fh.Location = new System.Drawing.Point(4, 22);
            this.tpUpPage9Fh.Name = "tpUpPage9Fh";
            this.tpUpPage9Fh.Padding = new System.Windows.Forms.Padding(3);
            this.tpUpPage9Fh.Size = new System.Drawing.Size(976, 708);
            this.tpUpPage9Fh.TabIndex = 7;
            this.tpUpPage9Fh.Text = "UP9Fh";
            this.tpUpPage9Fh.UseVisualStyleBackColor = true;
            // 
            // tpUpPageB0h
            // 
            this.tpUpPageB0h.Location = new System.Drawing.Point(4, 22);
            this.tpUpPageB0h.Name = "tpUpPageB0h";
            this.tpUpPageB0h.Padding = new System.Windows.Forms.Padding(3);
            this.tpUpPageB0h.Size = new System.Drawing.Size(976, 708);
            this.tpUpPageB0h.TabIndex = 8;
            this.tpUpPageB0h.Text = "UPB0h~";
            this.tpUpPageB0h.UseVisualStyleBackColor = true;
            // 
            // ucDigitalDiagnosticsMonitoring
            // 
            this.ucDigitalDiagnosticsMonitoring.Location = new System.Drawing.Point(6, 6);
            this.ucDigitalDiagnosticsMonitoring.Name = "ucDigitalDiagnosticsMonitoring";
            this.ucDigitalDiagnosticsMonitoring.Size = new System.Drawing.Size(964, 696);
            this.ucDigitalDiagnosticsMonitoring.TabIndex = 0;
            // 
            // ucUpPage111
            // 
            this.ucUpPage111.Location = new System.Drawing.Point(6, 6);
            this.ucUpPage111.Name = "ucUpPage111";
            this.ucUpPage111.Size = new System.Drawing.Size(964, 696);
            this.ucUpPage111.TabIndex = 0;
            // 
            // FCommonManagementInterfaceSpecification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 784);
            this.Controls.Add(this.tcCmisReg);
            this.Controls.Add(this.cbFirmwareVersionCheck);
            this.Controls.Add(this.tbFirmwareDateCheck);
            this.Controls.Add(this.tbFirmwareVersionCheck);
            this.Controls.Add(this.tbFirmwareDate);
            this.Controls.Add(this.lFirmwareDate);
            this.Controls.Add(this.tbFirmwareVersion);
            this.Controls.Add(this.lFirmwareVersion);
            this.Controls.Add(this.cbConnected);
            this.Name = "FCommonManagementInterfaceSpecification";
            this.Text = "CMIS 20221104";
            this.tcCmisReg.ResumeLayout(false);
            this.tpDDM.ResumeLayout(false);
            this.tpUpPage11h.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbFirmwareVersionCheck;
        private System.Windows.Forms.TextBox tbFirmwareDateCheck;
        private System.Windows.Forms.TextBox tbFirmwareVersionCheck;
        private System.Windows.Forms.TextBox tbFirmwareDate;
        private System.Windows.Forms.Label lFirmwareDate;
        private System.Windows.Forms.TextBox tbFirmwareVersion;
        private System.Windows.Forms.Label lFirmwareVersion;
        private System.Windows.Forms.CheckBox cbConnected;
        private System.Windows.Forms.TabControl tcCmisReg;
        private System.Windows.Forms.TabPage tpDDM;
        private System.Windows.Forms.TabPage tpLowPage;
        private System.Windows.Forms.TabPage tpUpPage00h;
        private System.Windows.Forms.TabPage tpUpPage01h;
        private System.Windows.Forms.TabPage tpUpPage02h;
        private System.Windows.Forms.TabPage tpUpPage03h;
        private System.Windows.Forms.TabPage tpUpPage10h;
        private System.Windows.Forms.TabPage tpUpPage11h;
        private System.Windows.Forms.TabPage tpUpPage9Fh;
        private System.Windows.Forms.TabPage tpUpPageB0h;
        private ucUpPage11 ucUpPage111;
        private ucDigitalDiagnosticsMonitoring ucDigitalDiagnosticsMonitoring;
    }
}

