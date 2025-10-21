namespace NuvotonIcpTool
{
    partial class UcNuvotonIcpTool
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bDataFlash = new System.Windows.Forms.Button();
            this.bAPROM = new System.Windows.Forms.Button();
            this.bLDROM = new System.Windows.Forms.Button();
            this.lLinkState = new System.Windows.Forms.Label();
            this.lMcuPnContent = new System.Windows.Forms.Label();
            this.bLink = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tbCommand = new System.Windows.Forms.TextBox();
            this.cbLDROM = new System.Windows.Forms.CheckBox();
            this.cbAPROM = new System.Windows.Forms.CheckBox();
            this.cbDataFlash = new System.Windows.Forms.CheckBox();
            this.tbLDROM = new System.Windows.Forms.TextBox();
            this.tbAPROM = new System.Windows.Forms.TextBox();
            this.tbDataFlash = new System.Windows.Forms.TextBox();
            this.gbPathConfig = new System.Windows.Forms.GroupBox();
            this.cbSecurityLock = new System.Windows.Forms.CheckBox();
            this.lCfg1 = new System.Windows.Forms.Label();
            this.lCfg0 = new System.Windows.Forms.Label();
            this.tbCfg1 = new System.Windows.Forms.TextBox();
            this.tbCfg0 = new System.Windows.Forms.TextBox();
            this.cbConfig = new System.Windows.Forms.CheckBox();
            this.bStart = new System.Windows.Forms.Button();
            this.lMessage = new System.Windows.Forms.Label();
            this.tbErase = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbWrite = new System.Windows.Forms.TextBox();
            this.tbVefiry = new System.Windows.Forms.TextBox();
            this.tbReset = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbEncryption = new System.Windows.Forms.TextBox();
            this.tbDone = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbDecrypt = new System.Windows.Forms.TextBox();
            this.cbAutoReconnect = new System.Windows.Forms.CheckBox();
            this.cbBypassEraseAllCheck = new System.Windows.Forms.CheckBox();
            this.gbPathConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // bDataFlash
            // 
            this.bDataFlash.Enabled = false;
            this.bDataFlash.Location = new System.Drawing.Point(79, 73);
            this.bDataFlash.Name = "bDataFlash";
            this.bDataFlash.Size = new System.Drawing.Size(75, 23);
            this.bDataFlash.TabIndex = 23;
            this.bDataFlash.Text = "DataFlash";
            this.bDataFlash.UseVisualStyleBackColor = true;
            this.bDataFlash.Click += new System.EventHandler(this.bDataFlash_Click);
            // 
            // bAPROM
            // 
            this.bAPROM.Enabled = false;
            this.bAPROM.Location = new System.Drawing.Point(79, 44);
            this.bAPROM.Name = "bAPROM";
            this.bAPROM.Size = new System.Drawing.Size(75, 23);
            this.bAPROM.TabIndex = 22;
            this.bAPROM.Text = "APROM";
            this.bAPROM.UseVisualStyleBackColor = true;
            this.bAPROM.Click += new System.EventHandler(this.bAPROM_Click);
            // 
            // bLDROM
            // 
            this.bLDROM.Enabled = false;
            this.bLDROM.Location = new System.Drawing.Point(79, 15);
            this.bLDROM.Name = "bLDROM";
            this.bLDROM.Size = new System.Drawing.Size(75, 23);
            this.bLDROM.TabIndex = 21;
            this.bLDROM.Text = "LDROM";
            this.bLDROM.UseVisualStyleBackColor = true;
            this.bLDROM.Click += new System.EventHandler(this.bLDROM_Click);
            // 
            // lLinkState
            // 
            this.lLinkState.AutoSize = true;
            this.lLinkState.ForeColor = System.Drawing.Color.Red;
            this.lLinkState.Location = new System.Drawing.Point(87, 8);
            this.lLinkState.Name = "lLinkState";
            this.lLinkState.Size = new System.Drawing.Size(56, 12);
            this.lLinkState.TabIndex = 20;
            this.lLinkState.Text = "Disconnect";
            // 
            // lMcuPnContent
            // 
            this.lMcuPnContent.AutoSize = true;
            this.lMcuPnContent.ForeColor = System.Drawing.Color.Green;
            this.lMcuPnContent.Location = new System.Drawing.Point(87, 28);
            this.lMcuPnContent.Name = "lMcuPnContent";
            this.lMcuPnContent.Size = new System.Drawing.Size(14, 12);
            this.lMcuPnContent.TabIndex = 19;
            this.lMcuPnContent.Text = "   ";
            // 
            // bLink
            // 
            this.bLink.Font = new System.Drawing.Font("PMingLiU", 10F);
            this.bLink.Location = new System.Drawing.Point(3, 3);
            this.bLink.Name = "bLink";
            this.bLink.Size = new System.Drawing.Size(83, 57);
            this.bLink.TabIndex = 1;
            this.bLink.Text = "Connect";
            this.bLink.UseVisualStyleBackColor = true;
            this.bLink.Click += new System.EventHandler(this.bIcpConnect_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(460, 36);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(280, 201);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.Text = "";
            // 
            // tbCommand
            // 
            this.tbCommand.Location = new System.Drawing.Point(460, 8);
            this.tbCommand.Name = "tbCommand";
            this.tbCommand.Size = new System.Drawing.Size(280, 22);
            this.tbCommand.TabIndex = 14;
            this.tbCommand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbRun_KeyDown);
            // 
            // cbLDROM
            // 
            this.cbLDROM.AutoSize = true;
            this.cbLDROM.Location = new System.Drawing.Point(8, 19);
            this.cbLDROM.Name = "cbLDROM";
            this.cbLDROM.Size = new System.Drawing.Size(65, 16);
            this.cbLDROM.TabIndex = 25;
            this.cbLDROM.Text = "LDROM";
            this.cbLDROM.UseVisualStyleBackColor = true;
            this.cbLDROM.CheckedChanged += new System.EventHandler(this.cbLDROM_CheckedChanged);
            // 
            // cbAPROM
            // 
            this.cbAPROM.AutoSize = true;
            this.cbAPROM.Checked = true;
            this.cbAPROM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAPROM.Location = new System.Drawing.Point(8, 48);
            this.cbAPROM.Name = "cbAPROM";
            this.cbAPROM.Size = new System.Drawing.Size(64, 16);
            this.cbAPROM.TabIndex = 26;
            this.cbAPROM.Text = "APROM";
            this.cbAPROM.UseVisualStyleBackColor = true;
            this.cbAPROM.CheckedChanged += new System.EventHandler(this.cbAPROM_CheckedChanged);
            // 
            // cbDataFlash
            // 
            this.cbDataFlash.AutoSize = true;
            this.cbDataFlash.Location = new System.Drawing.Point(8, 77);
            this.cbDataFlash.Name = "cbDataFlash";
            this.cbDataFlash.Size = new System.Drawing.Size(69, 16);
            this.cbDataFlash.TabIndex = 27;
            this.cbDataFlash.Text = "DataFlash";
            this.cbDataFlash.UseVisualStyleBackColor = true;
            this.cbDataFlash.CheckedChanged += new System.EventHandler(this.cbDataFlash_CheckedChanged);
            // 
            // tbLDROM
            // 
            this.tbLDROM.Enabled = false;
            this.tbLDROM.Location = new System.Drawing.Point(160, 15);
            this.tbLDROM.Name = "tbLDROM";
            this.tbLDROM.Size = new System.Drawing.Size(256, 22);
            this.tbLDROM.TabIndex = 29;
            // 
            // tbAPROM
            // 
            this.tbAPROM.Enabled = false;
            this.tbAPROM.Location = new System.Drawing.Point(160, 44);
            this.tbAPROM.Name = "tbAPROM";
            this.tbAPROM.Size = new System.Drawing.Size(256, 22);
            this.tbAPROM.TabIndex = 30;
            // 
            // tbDataFlash
            // 
            this.tbDataFlash.Enabled = false;
            this.tbDataFlash.Location = new System.Drawing.Point(160, 73);
            this.tbDataFlash.Name = "tbDataFlash";
            this.tbDataFlash.Size = new System.Drawing.Size(256, 22);
            this.tbDataFlash.TabIndex = 31;
            // 
            // gbPathConfig
            // 
            this.gbPathConfig.Controls.Add(this.cbSecurityLock);
            this.gbPathConfig.Controls.Add(this.lCfg1);
            this.gbPathConfig.Controls.Add(this.lCfg0);
            this.gbPathConfig.Controls.Add(this.tbCfg1);
            this.gbPathConfig.Controls.Add(this.bLDROM);
            this.gbPathConfig.Controls.Add(this.bAPROM);
            this.gbPathConfig.Controls.Add(this.bDataFlash);
            this.gbPathConfig.Controls.Add(this.tbCfg0);
            this.gbPathConfig.Controls.Add(this.tbDataFlash);
            this.gbPathConfig.Controls.Add(this.cbLDROM);
            this.gbPathConfig.Controls.Add(this.tbAPROM);
            this.gbPathConfig.Controls.Add(this.cbAPROM);
            this.gbPathConfig.Controls.Add(this.tbLDROM);
            this.gbPathConfig.Controls.Add(this.cbDataFlash);
            this.gbPathConfig.Location = new System.Drawing.Point(3, 72);
            this.gbPathConfig.Name = "gbPathConfig";
            this.gbPathConfig.Size = new System.Drawing.Size(427, 136);
            this.gbPathConfig.TabIndex = 33;
            this.gbPathConfig.TabStop = false;
            this.gbPathConfig.Text = "Programming and File Paths";
            // 
            // cbSecurityLock
            // 
            this.cbSecurityLock.AutoSize = true;
            this.cbSecurityLock.Checked = true;
            this.cbSecurityLock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSecurityLock.Location = new System.Drawing.Point(68, 106);
            this.cbSecurityLock.Name = "cbSecurityLock";
            this.cbSecurityLock.Size = new System.Drawing.Size(89, 16);
            this.cbSecurityLock.TabIndex = 36;
            this.cbSecurityLock.Text = "Security Lock";
            this.cbSecurityLock.UseVisualStyleBackColor = true;
            this.cbSecurityLock.CheckedChanged += new System.EventHandler(this.cbSecurityLock_CheckedChanged);
            // 
            // lCfg1
            // 
            this.lCfg1.AutoSize = true;
            this.lCfg1.Location = new System.Drawing.Point(303, 107);
            this.lCfg1.Name = "lCfg1";
            this.lCfg1.Size = new System.Drawing.Size(36, 12);
            this.lCfg1.TabIndex = 35;
            this.lCfg1.Text = "CFG1:";
            // 
            // lCfg0
            // 
            this.lCfg0.AutoSize = true;
            this.lCfg0.Location = new System.Drawing.Point(182, 107);
            this.lCfg0.Name = "lCfg0";
            this.lCfg0.Size = new System.Drawing.Size(36, 12);
            this.lCfg0.TabIndex = 34;
            this.lCfg0.Text = "CFG0:";
            // 
            // tbCfg1
            // 
            this.tbCfg1.Location = new System.Drawing.Point(339, 102);
            this.tbCfg1.Name = "tbCfg1";
            this.tbCfg1.Size = new System.Drawing.Size(77, 22);
            this.tbCfg1.TabIndex = 33;
            this.tbCfg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbCfg0
            // 
            this.tbCfg0.Location = new System.Drawing.Point(218, 102);
            this.tbCfg0.Name = "tbCfg0";
            this.tbCfg0.Size = new System.Drawing.Size(77, 22);
            this.tbCfg0.TabIndex = 32;
            this.tbCfg0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cbConfig
            // 
            this.cbConfig.Location = new System.Drawing.Point(0, 0);
            this.cbConfig.Name = "cbConfig";
            this.cbConfig.Size = new System.Drawing.Size(104, 24);
            this.cbConfig.TabIndex = 0;
            // 
            // bStart
            // 
            this.bStart.Enabled = false;
            this.bStart.Font = new System.Drawing.Font("PMingLiU", 10F);
            this.bStart.Location = new System.Drawing.Point(3, 214);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(113, 23);
            this.bStart.TabIndex = 36;
            this.bStart.Text = "Start";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // lMessage
            // 
            this.lMessage.AutoSize = true;
            this.lMessage.Location = new System.Drawing.Point(87, 48);
            this.lMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lMessage.Name = "lMessage";
            this.lMessage.Size = new System.Drawing.Size(20, 12);
            this.lMessage.TabIndex = 35;
            this.lMessage.Text = "     ";
            // 
            // tbErase
            // 
            this.tbErase.Enabled = false;
            this.tbErase.Font = new System.Drawing.Font("PMingLiU", 1F);
            this.tbErase.Location = new System.Drawing.Point(183, 224);
            this.tbErase.Margin = new System.Windows.Forms.Padding(2);
            this.tbErase.Name = "tbErase";
            this.tbErase.Size = new System.Drawing.Size(35, 9);
            this.tbErase.TabIndex = 56;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("PMingLiU", 7F);
            this.label3.Location = new System.Drawing.Point(180, 214);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 10);
            this.label3.TabIndex = 37;
            this.label3.Text = "Erase";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("PMingLiU", 7F);
            this.label4.Location = new System.Drawing.Point(218, 214);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 10);
            this.label4.TabIndex = 38;
            this.label4.Text = "Write";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("PMingLiU", 7F);
            this.label5.Location = new System.Drawing.Point(254, 214);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 10);
            this.label5.TabIndex = 39;
            this.label5.Text = "Verify";
            // 
            // tbWrite
            // 
            this.tbWrite.Enabled = false;
            this.tbWrite.Font = new System.Drawing.Font("PMingLiU", 1F);
            this.tbWrite.Location = new System.Drawing.Point(220, 224);
            this.tbWrite.Margin = new System.Windows.Forms.Padding(2);
            this.tbWrite.Name = "tbWrite";
            this.tbWrite.Size = new System.Drawing.Size(35, 9);
            this.tbWrite.TabIndex = 57;
            // 
            // tbVefiry
            // 
            this.tbVefiry.Enabled = false;
            this.tbVefiry.Font = new System.Drawing.Font("PMingLiU", 1F);
            this.tbVefiry.Location = new System.Drawing.Point(256, 224);
            this.tbVefiry.Margin = new System.Windows.Forms.Padding(2);
            this.tbVefiry.Name = "tbVefiry";
            this.tbVefiry.Size = new System.Drawing.Size(35, 9);
            this.tbVefiry.TabIndex = 58;
            // 
            // tbReset
            // 
            this.tbReset.Enabled = false;
            this.tbReset.Font = new System.Drawing.Font("PMingLiU", 1F);
            this.tbReset.Location = new System.Drawing.Point(330, 224);
            this.tbReset.Margin = new System.Windows.Forms.Padding(2);
            this.tbReset.Name = "tbReset";
            this.tbReset.Size = new System.Drawing.Size(35, 9);
            this.tbReset.TabIndex = 60;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("PMingLiU", 7F);
            this.label6.Location = new System.Drawing.Point(329, 214);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 10);
            this.label6.TabIndex = 43;
            this.label6.Text = "Reset";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("PMingLiU", 7F);
            this.label7.Location = new System.Drawing.Point(292, 214);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 10);
            this.label7.TabIndex = 45;
            this.label7.Text = "Encrypt";
            // 
            // tbEncryption
            // 
            this.tbEncryption.Enabled = false;
            this.tbEncryption.Font = new System.Drawing.Font("PMingLiU", 1F);
            this.tbEncryption.Location = new System.Drawing.Point(293, 224);
            this.tbEncryption.Margin = new System.Windows.Forms.Padding(2);
            this.tbEncryption.Name = "tbEncryption";
            this.tbEncryption.Size = new System.Drawing.Size(35, 9);
            this.tbEncryption.TabIndex = 59;
            // 
            // tbDone
            // 
            this.tbDone.Enabled = false;
            this.tbDone.Font = new System.Drawing.Font("PMingLiU", 1F);
            this.tbDone.Location = new System.Drawing.Point(367, 224);
            this.tbDone.Margin = new System.Windows.Forms.Padding(2);
            this.tbDone.Name = "tbDone";
            this.tbDone.Size = new System.Drawing.Size(35, 9);
            this.tbDone.TabIndex = 61;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("PMingLiU", 7F);
            this.label8.Location = new System.Drawing.Point(365, 214);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 10);
            this.label8.TabIndex = 1000;
            this.label8.Text = "Finish";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("PMingLiU", 7F);
            this.label9.Location = new System.Drawing.Point(143, 214);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 10);
            this.label9.TabIndex = 1002;
            this.label9.Text = "Decrypt";
            // 
            // tbDecrypt
            // 
            this.tbDecrypt.Enabled = false;
            this.tbDecrypt.Font = new System.Drawing.Font("PMingLiU", 1F);
            this.tbDecrypt.Location = new System.Drawing.Point(146, 224);
            this.tbDecrypt.Margin = new System.Windows.Forms.Padding(2);
            this.tbDecrypt.Name = "tbDecrypt";
            this.tbDecrypt.Size = new System.Drawing.Size(35, 9);
            this.tbDecrypt.TabIndex = 47;
            // 
            // cbAutoReconnect
            // 
            this.cbAutoReconnect.AutoSize = true;
            this.cbAutoReconnect.Font = new System.Drawing.Font("PMingLiU", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbAutoReconnect.Location = new System.Drawing.Point(195, 3);
            this.cbAutoReconnect.Name = "cbAutoReconnect";
            this.cbAutoReconnect.Size = new System.Drawing.Size(130, 17);
            this.cbAutoReconnect.TabIndex = 1021;
            this.cbAutoReconnect.Text = "AutoReconnect Mode";
            this.cbAutoReconnect.UseVisualStyleBackColor = true;
            this.cbAutoReconnect.Visible = false;
            this.cbAutoReconnect.CheckedChanged += new System.EventHandler(this.cbAutoReconnect_CheckedChanged);
            // 
            // cbBypassEraseAllCheck
            // 
            this.cbBypassEraseAllCheck.AutoSize = true;
            this.cbBypassEraseAllCheck.Font = new System.Drawing.Font("PMingLiU", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbBypassEraseAllCheck.Location = new System.Drawing.Point(195, 19);
            this.cbBypassEraseAllCheck.Name = "cbBypassEraseAllCheck";
            this.cbBypassEraseAllCheck.Size = new System.Drawing.Size(259, 17);
            this.cbBypassEraseAllCheck.TabIndex = 1023;
            this.cbBypassEraseAllCheck.Text = "Bypass confirm button before EraseAll operation";
            this.cbBypassEraseAllCheck.UseVisualStyleBackColor = true;
            this.cbBypassEraseAllCheck.Visible = false;
            this.cbBypassEraseAllCheck.CheckedChanged += new System.EventHandler(this.cbBypassEraseAllCheck_CheckedChanged);
            // 
            // UcNuvotonIcpTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbBypassEraseAllCheck);
            this.Controls.Add(this.cbAutoReconnect);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbDecrypt);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbDone);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbEncryption);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbReset);
            this.Controls.Add(this.tbVefiry);
            this.Controls.Add(this.tbWrite);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbErase);
            this.Controls.Add(this.lMessage);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.gbPathConfig);
            this.Controls.Add(this.lLinkState);
            this.Controls.Add(this.lMcuPnContent);
            this.Controls.Add(this.bLink);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.tbCommand);
            this.Name = "UcNuvotonIcpTool";
            this.Size = new System.Drawing.Size(740, 250);
            this.Load += new System.EventHandler(this.UcNuvotonIcpTool_Load);
            this.gbPathConfig.ResumeLayout(false);
            this.gbPathConfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bDataFlash;
        private System.Windows.Forms.Button bAPROM;
        private System.Windows.Forms.Button bLDROM;
        private System.Windows.Forms.Label lLinkState;
        private System.Windows.Forms.Label lMcuPnContent;
        private System.Windows.Forms.Button bLink;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox tbCommand;
        private System.Windows.Forms.CheckBox cbLDROM;
        private System.Windows.Forms.CheckBox cbAPROM;
        private System.Windows.Forms.CheckBox cbDataFlash;
        private System.Windows.Forms.TextBox tbLDROM;
        private System.Windows.Forms.TextBox tbAPROM;
        private System.Windows.Forms.TextBox tbDataFlash;
        private System.Windows.Forms.GroupBox gbPathConfig;
        private System.Windows.Forms.CheckBox cbConfig;
        private System.Windows.Forms.Label lCfg0;
        private System.Windows.Forms.TextBox tbCfg1;
        private System.Windows.Forms.TextBox tbCfg0;
        private System.Windows.Forms.Label lCfg1;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Label lMessage;
        private System.Windows.Forms.TextBox tbErase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbWrite;
        private System.Windows.Forms.TextBox tbVefiry;
        private System.Windows.Forms.TextBox tbReset;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbEncryption;
        private System.Windows.Forms.TextBox tbDone;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbDecrypt;
        private System.Windows.Forms.CheckBox cbSecurityLock;
        internal System.Windows.Forms.CheckBox cbAutoReconnect;
        internal System.Windows.Forms.CheckBox cbBypassEraseAllCheck;
    }
}
