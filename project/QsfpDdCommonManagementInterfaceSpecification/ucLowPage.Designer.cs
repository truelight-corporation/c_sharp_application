namespace QsfpDdCommonManagementInterfaceSpecification
{
    partial class ucLowPage
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbNewPassword = new System.Windows.Forms.TextBox();
            this.lNewPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.bPasswordReset = new System.Windows.Forms.Button();
            this.bRead = new System.Windows.Forms.Button();
            this.tbUpPage0Identifier = new System.Windows.Forms.TextBox();
            this.lUpPage0Identifier = new System.Windows.Forms.Label();
            this.lRevisionCompliance = new System.Windows.Forms.Label();
            this.tbRevisionCompliance = new System.Windows.Forms.TextBox();
            this.cbFlatMem = new System.Windows.Forms.CheckBox();
            this.lTwiMaximumSpeed = new System.Windows.Forms.Label();
            this.tbTwiMaximumSpeed = new System.Windows.Forms.TextBox();
            this.tbModuleState = new System.Windows.Forms.TextBox();
            this.lModuleState = new System.Windows.Forms.Label();
            this.cbInterrupt = new System.Windows.Forms.CheckBox();
            this.tbBank0LansFlagSummary = new System.Windows.Forms.TextBox();
            this.lBank0LaneFlagSummary = new System.Windows.Forms.Label();
            this.tbBank1LansFlagSummary = new System.Windows.Forms.TextBox();
            this.lBank1LaneFlagSummary = new System.Windows.Forms.Label();
            this.tbBank3LansFlagSummary = new System.Windows.Forms.TextBox();
            this.lBank3LaneFlagSummary = new System.Windows.Forms.Label();
            this.tbBank2LansFlagSummary = new System.Windows.Forms.TextBox();
            this.lBank2LaneFlagSummary = new System.Windows.Forms.Label();
            this.cbLCdbBlock2Complete = new System.Windows.Forms.CheckBox();
            this.cbLCdbBlock1Complete = new System.Windows.Forms.CheckBox();
            this.cbDataPathFirmwareFault = new System.Windows.Forms.CheckBox();
            this.cbModuleFirmwareFault = new System.Windows.Forms.CheckBox();
            this.cbLModuleStateChangedFlag = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbNewPassword
            // 
            this.tbNewPassword.Location = new System.Drawing.Point(901, 625);
            this.tbNewPassword.Name = "tbNewPassword";
            this.tbNewPassword.Size = new System.Drawing.Size(60, 22);
            this.tbNewPassword.TabIndex = 73;
            this.tbNewPassword.UseSystemPasswordChar = true;
            // 
            // lNewPassword
            // 
            this.lNewPassword.AutoSize = true;
            this.lNewPassword.Location = new System.Drawing.Point(886, 610);
            this.lNewPassword.Name = "lNewPassword";
            this.lNewPassword.Size = new System.Drawing.Size(78, 12);
            this.lNewPassword.TabIndex = 72;
            this.lNewPassword.Text = "New Password :";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(901, 585);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(60, 22);
            this.tbPassword.TabIndex = 71;
            this.tbPassword.Text = "3234";
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point(886, 570);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(54, 12);
            this.lPassword.TabIndex = 70;
            this.lPassword.Text = "Password :";
            // 
            // bPasswordReset
            // 
            this.bPasswordReset.Location = new System.Drawing.Point(886, 653);
            this.bPasswordReset.Name = "bPasswordReset";
            this.bPasswordReset.Size = new System.Drawing.Size(75, 40);
            this.bPasswordReset.TabIndex = 74;
            this.bPasswordReset.Text = "Password Reset";
            this.bPasswordReset.UseVisualStyleBackColor = true;
            // 
            // bRead
            // 
            this.bRead.Location = new System.Drawing.Point(886, 3);
            this.bRead.Name = "bRead";
            this.bRead.Size = new System.Drawing.Size(75, 23);
            this.bRead.TabIndex = 75;
            this.bRead.Text = "Read All";
            this.bRead.UseVisualStyleBackColor = true;
            this.bRead.Click += new System.EventHandler(this.bRead_Click);
            // 
            // tbUpPage0Identifier
            // 
            this.tbUpPage0Identifier.Location = new System.Drawing.Point(60, 3);
            this.tbUpPage0Identifier.Name = "tbUpPage0Identifier";
            this.tbUpPage0Identifier.Size = new System.Drawing.Size(40, 22);
            this.tbUpPage0Identifier.TabIndex = 77;
            // 
            // lUpPage0Identifier
            // 
            this.lUpPage0Identifier.AutoSize = true;
            this.lUpPage0Identifier.Location = new System.Drawing.Point(3, 6);
            this.lUpPage0Identifier.Name = "lUpPage0Identifier";
            this.lUpPage0Identifier.Size = new System.Drawing.Size(51, 12);
            this.lUpPage0Identifier.TabIndex = 76;
            this.lUpPage0Identifier.Text = "Identifier:";
            // 
            // lRevisionCompliance
            // 
            this.lRevisionCompliance.AutoSize = true;
            this.lRevisionCompliance.Location = new System.Drawing.Point(106, 6);
            this.lRevisionCompliance.Name = "lRevisionCompliance";
            this.lRevisionCompliance.Size = new System.Drawing.Size(108, 12);
            this.lRevisionCompliance.TabIndex = 78;
            this.lRevisionCompliance.Text = "Revision Compliance:";
            // 
            // tbRevisionCompliance
            // 
            this.tbRevisionCompliance.Location = new System.Drawing.Point(220, 3);
            this.tbRevisionCompliance.Name = "tbRevisionCompliance";
            this.tbRevisionCompliance.Size = new System.Drawing.Size(40, 22);
            this.tbRevisionCompliance.TabIndex = 79;
            // 
            // cbFlatMem
            // 
            this.cbFlatMem.AutoSize = true;
            this.cbFlatMem.Enabled = false;
            this.cbFlatMem.Location = new System.Drawing.Point(266, 5);
            this.cbFlatMem.Name = "cbFlatMem";
            this.cbFlatMem.Size = new System.Drawing.Size(70, 16);
            this.cbFlatMem.TabIndex = 80;
            this.cbFlatMem.Text = "Flat_mem";
            this.cbFlatMem.UseVisualStyleBackColor = true;
            // 
            // lTwiMaximumSpeed
            // 
            this.lTwiMaximumSpeed.AutoSize = true;
            this.lTwiMaximumSpeed.Location = new System.Drawing.Point(342, 6);
            this.lTwiMaximumSpeed.Name = "lTwiMaximumSpeed";
            this.lTwiMaximumSpeed.Size = new System.Drawing.Size(110, 12);
            this.lTwiMaximumSpeed.TabIndex = 81;
            this.lTwiMaximumSpeed.Text = "TWI Maximum speed:";
            // 
            // tbTwiMaximumSpeed
            // 
            this.tbTwiMaximumSpeed.Location = new System.Drawing.Point(458, 3);
            this.tbTwiMaximumSpeed.Name = "tbTwiMaximumSpeed";
            this.tbTwiMaximumSpeed.Size = new System.Drawing.Size(40, 22);
            this.tbTwiMaximumSpeed.TabIndex = 82;
            // 
            // tbModuleState
            // 
            this.tbModuleState.Location = new System.Drawing.Point(577, 3);
            this.tbModuleState.Name = "tbModuleState";
            this.tbModuleState.Size = new System.Drawing.Size(40, 22);
            this.tbModuleState.TabIndex = 84;
            // 
            // lModuleState
            // 
            this.lModuleState.AutoSize = true;
            this.lModuleState.Location = new System.Drawing.Point(504, 6);
            this.lModuleState.Name = "lModuleState";
            this.lModuleState.Size = new System.Drawing.Size(67, 12);
            this.lModuleState.TabIndex = 83;
            this.lModuleState.Text = "Module state:";
            // 
            // cbInterrupt
            // 
            this.cbInterrupt.AutoSize = true;
            this.cbInterrupt.Enabled = false;
            this.cbInterrupt.Location = new System.Drawing.Point(623, 5);
            this.cbInterrupt.Name = "cbInterrupt";
            this.cbInterrupt.Size = new System.Drawing.Size(65, 16);
            this.cbInterrupt.TabIndex = 85;
            this.cbInterrupt.Text = "Interrupt";
            this.cbInterrupt.UseVisualStyleBackColor = true;
            // 
            // tbBank0LansFlagSummary
            // 
            this.tbBank0LansFlagSummary.Location = new System.Drawing.Point(827, 3);
            this.tbBank0LansFlagSummary.Name = "tbBank0LansFlagSummary";
            this.tbBank0LansFlagSummary.Size = new System.Drawing.Size(40, 22);
            this.tbBank0LansFlagSummary.TabIndex = 87;
            // 
            // lBank0LaneFlagSummary
            // 
            this.lBank0LaneFlagSummary.AutoSize = true;
            this.lBank0LaneFlagSummary.Location = new System.Drawing.Point(694, 6);
            this.lBank0LaneFlagSummary.Name = "lBank0LaneFlagSummary";
            this.lBank0LaneFlagSummary.Size = new System.Drawing.Size(127, 12);
            this.lBank0LaneFlagSummary.TabIndex = 86;
            this.lBank0LaneFlagSummary.Text = "Bank0 lans flag summary:";
            // 
            // tbBank1LansFlagSummary
            // 
            this.tbBank1LansFlagSummary.Location = new System.Drawing.Point(136, 31);
            this.tbBank1LansFlagSummary.Name = "tbBank1LansFlagSummary";
            this.tbBank1LansFlagSummary.Size = new System.Drawing.Size(40, 22);
            this.tbBank1LansFlagSummary.TabIndex = 89;
            // 
            // lBank1LaneFlagSummary
            // 
            this.lBank1LaneFlagSummary.AutoSize = true;
            this.lBank1LaneFlagSummary.Location = new System.Drawing.Point(3, 34);
            this.lBank1LaneFlagSummary.Name = "lBank1LaneFlagSummary";
            this.lBank1LaneFlagSummary.Size = new System.Drawing.Size(127, 12);
            this.lBank1LaneFlagSummary.TabIndex = 88;
            this.lBank1LaneFlagSummary.Text = "Bank1 lans flag summary:";
            // 
            // tbBank3LansFlagSummary
            // 
            this.tbBank3LansFlagSummary.Location = new System.Drawing.Point(494, 31);
            this.tbBank3LansFlagSummary.Name = "tbBank3LansFlagSummary";
            this.tbBank3LansFlagSummary.Size = new System.Drawing.Size(40, 22);
            this.tbBank3LansFlagSummary.TabIndex = 93;
            // 
            // lBank3LaneFlagSummary
            // 
            this.lBank3LaneFlagSummary.AutoSize = true;
            this.lBank3LaneFlagSummary.Location = new System.Drawing.Point(361, 34);
            this.lBank3LaneFlagSummary.Name = "lBank3LaneFlagSummary";
            this.lBank3LaneFlagSummary.Size = new System.Drawing.Size(127, 12);
            this.lBank3LaneFlagSummary.TabIndex = 92;
            this.lBank3LaneFlagSummary.Text = "Bank3 lans flag summary:";
            // 
            // tbBank2LansFlagSummary
            // 
            this.tbBank2LansFlagSummary.Location = new System.Drawing.Point(315, 31);
            this.tbBank2LansFlagSummary.Name = "tbBank2LansFlagSummary";
            this.tbBank2LansFlagSummary.Size = new System.Drawing.Size(40, 22);
            this.tbBank2LansFlagSummary.TabIndex = 91;
            // 
            // lBank2LaneFlagSummary
            // 
            this.lBank2LaneFlagSummary.AutoSize = true;
            this.lBank2LaneFlagSummary.Location = new System.Drawing.Point(182, 34);
            this.lBank2LaneFlagSummary.Name = "lBank2LaneFlagSummary";
            this.lBank2LaneFlagSummary.Size = new System.Drawing.Size(127, 12);
            this.lBank2LaneFlagSummary.TabIndex = 90;
            this.lBank2LaneFlagSummary.Text = "Bank2 lans flag summary:";
            // 
            // cbLCdbBlock2Complete
            // 
            this.cbLCdbBlock2Complete.AutoSize = true;
            this.cbLCdbBlock2Complete.Enabled = false;
            this.cbLCdbBlock2Complete.Location = new System.Drawing.Point(540, 33);
            this.cbLCdbBlock2Complete.Name = "cbLCdbBlock2Complete";
            this.cbLCdbBlock2Complete.Size = new System.Drawing.Size(139, 16);
            this.cbLCdbBlock2Complete.TabIndex = 94;
            this.cbLCdbBlock2Complete.Text = "L-CDB block2 complete";
            this.cbLCdbBlock2Complete.UseVisualStyleBackColor = true;
            // 
            // cbLCdbBlock1Complete
            // 
            this.cbLCdbBlock1Complete.AutoSize = true;
            this.cbLCdbBlock1Complete.Enabled = false;
            this.cbLCdbBlock1Complete.Location = new System.Drawing.Point(685, 33);
            this.cbLCdbBlock1Complete.Name = "cbLCdbBlock1Complete";
            this.cbLCdbBlock1Complete.Size = new System.Drawing.Size(139, 16);
            this.cbLCdbBlock1Complete.TabIndex = 95;
            this.cbLCdbBlock1Complete.Text = "L-CDB block1 complete";
            this.cbLCdbBlock1Complete.UseVisualStyleBackColor = true;
            // 
            // cbDataPathFirmwareFault
            // 
            this.cbDataPathFirmwareFault.AutoSize = true;
            this.cbDataPathFirmwareFault.Enabled = false;
            this.cbDataPathFirmwareFault.Location = new System.Drawing.Point(3, 59);
            this.cbDataPathFirmwareFault.Name = "cbDataPathFirmwareFault";
            this.cbDataPathFirmwareFault.Size = new System.Drawing.Size(137, 16);
            this.cbDataPathFirmwareFault.TabIndex = 96;
            this.cbDataPathFirmwareFault.Text = "Data Path firmware fault";
            this.cbDataPathFirmwareFault.UseVisualStyleBackColor = true;
            // 
            // cbModuleFirmwareFault
            // 
            this.cbModuleFirmwareFault.AutoSize = true;
            this.cbModuleFirmwareFault.Enabled = false;
            this.cbModuleFirmwareFault.Location = new System.Drawing.Point(146, 59);
            this.cbModuleFirmwareFault.Name = "cbModuleFirmwareFault";
            this.cbModuleFirmwareFault.Size = new System.Drawing.Size(129, 16);
            this.cbModuleFirmwareFault.TabIndex = 97;
            this.cbModuleFirmwareFault.Text = "Module firmware fault";
            this.cbModuleFirmwareFault.UseVisualStyleBackColor = true;
            // 
            // cbLModuleStateChangedFlag
            // 
            this.cbLModuleStateChangedFlag.AutoSize = true;
            this.cbLModuleStateChangedFlag.Enabled = false;
            this.cbLModuleStateChangedFlag.Location = new System.Drawing.Point(281, 59);
            this.cbLModuleStateChangedFlag.Name = "cbLModuleStateChangedFlag";
            this.cbLModuleStateChangedFlag.Size = new System.Drawing.Size(157, 16);
            this.cbLModuleStateChangedFlag.TabIndex = 98;
            this.cbLModuleStateChangedFlag.Text = "L-Module state changed flag";
            this.cbLModuleStateChangedFlag.UseVisualStyleBackColor = true;
            // 
            // ucLowPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbLModuleStateChangedFlag);
            this.Controls.Add(this.cbModuleFirmwareFault);
            this.Controls.Add(this.cbDataPathFirmwareFault);
            this.Controls.Add(this.cbLCdbBlock1Complete);
            this.Controls.Add(this.cbLCdbBlock2Complete);
            this.Controls.Add(this.tbBank3LansFlagSummary);
            this.Controls.Add(this.lBank3LaneFlagSummary);
            this.Controls.Add(this.tbBank2LansFlagSummary);
            this.Controls.Add(this.lBank2LaneFlagSummary);
            this.Controls.Add(this.tbBank1LansFlagSummary);
            this.Controls.Add(this.lBank1LaneFlagSummary);
            this.Controls.Add(this.tbBank0LansFlagSummary);
            this.Controls.Add(this.lBank0LaneFlagSummary);
            this.Controls.Add(this.cbInterrupt);
            this.Controls.Add(this.tbModuleState);
            this.Controls.Add(this.lModuleState);
            this.Controls.Add(this.tbTwiMaximumSpeed);
            this.Controls.Add(this.lTwiMaximumSpeed);
            this.Controls.Add(this.cbFlatMem);
            this.Controls.Add(this.tbRevisionCompliance);
            this.Controls.Add(this.lRevisionCompliance);
            this.Controls.Add(this.tbUpPage0Identifier);
            this.Controls.Add(this.lUpPage0Identifier);
            this.Controls.Add(this.bRead);
            this.Controls.Add(this.bPasswordReset);
            this.Controls.Add(this.tbNewPassword);
            this.Controls.Add(this.lNewPassword);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lPassword);
            this.Name = "ucLowPage";
            this.Size = new System.Drawing.Size(964, 696);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbNewPassword;
        private System.Windows.Forms.Label lNewPassword;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.Button bPasswordReset;
        private System.Windows.Forms.Button bRead;
        private System.Windows.Forms.TextBox tbUpPage0Identifier;
        private System.Windows.Forms.Label lUpPage0Identifier;
        private System.Windows.Forms.Label lRevisionCompliance;
        private System.Windows.Forms.TextBox tbRevisionCompliance;
        private System.Windows.Forms.CheckBox cbFlatMem;
        private System.Windows.Forms.Label lTwiMaximumSpeed;
        private System.Windows.Forms.TextBox tbTwiMaximumSpeed;
        private System.Windows.Forms.TextBox tbModuleState;
        private System.Windows.Forms.Label lModuleState;
        private System.Windows.Forms.CheckBox cbInterrupt;
        private System.Windows.Forms.TextBox tbBank0LansFlagSummary;
        private System.Windows.Forms.Label lBank0LaneFlagSummary;
        private System.Windows.Forms.TextBox tbBank1LansFlagSummary;
        private System.Windows.Forms.Label lBank1LaneFlagSummary;
        private System.Windows.Forms.TextBox tbBank3LansFlagSummary;
        private System.Windows.Forms.Label lBank3LaneFlagSummary;
        private System.Windows.Forms.TextBox tbBank2LansFlagSummary;
        private System.Windows.Forms.Label lBank2LaneFlagSummary;
        private System.Windows.Forms.CheckBox cbLCdbBlock2Complete;
        private System.Windows.Forms.CheckBox cbLCdbBlock1Complete;
        private System.Windows.Forms.CheckBox cbDataPathFirmwareFault;
        private System.Windows.Forms.CheckBox cbModuleFirmwareFault;
        private System.Windows.Forms.CheckBox cbLModuleStateChangedFlag;
    }
}
