namespace Mald37045cMata37044c
{
    partial class FMald37045cMata37044cConfig
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
            if (disposing && (components != null))
            {
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
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.cbConnected = new System.Windows.Forms.CheckBox();
            this.tcMald37045cMata37044cConfig = new System.Windows.Forms.TabControl();
            this.tpMald37045cConfig = new System.Windows.Forms.TabPage();
            this.ucMald37045cConfig = new Mald37045cMata37044c.UcMald37045cConfig();
            this.tpMald37044cConfig = new System.Windows.Forms.TabPage();
            this.ucMata37044cConfig = new Mald37045cMata37044c.UcMata37044cConfig();
            this.tpI2cActionLog = new System.Windows.Forms.TabPage();
            this.bClearI2cActionLog = new System.Windows.Forms.Button();
            this.tbI2cActionLog = new System.Windows.Forms.TextBox();
            this.cbI2cActionLog = new System.Windows.Forms.CheckBox();
            this.tcMald37045cMata37044cConfig.SuspendLayout();
            this.tpMald37045cConfig.SuspendLayout();
            this.tpMald37044cConfig.SuspendLayout();
            this.tpI2cActionLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(72, 12);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(60, 22);
            this.tbPassword.TabIndex = 72;
            this.tbPassword.Text = "3234";
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point(12, 15);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(54, 12);
            this.lPassword.TabIndex = 71;
            this.lPassword.Text = "Password :";
            // 
            // cbConnected
            // 
            this.cbConnected.AutoSize = true;
            this.cbConnected.Location = new System.Drawing.Point(595, 11);
            this.cbConnected.Name = "cbConnected";
            this.cbConnected.Size = new System.Drawing.Size(74, 16);
            this.cbConnected.TabIndex = 70;
            this.cbConnected.Text = "Connected";
            this.cbConnected.UseVisualStyleBackColor = true;
            this.cbConnected.CheckedChanged += new System.EventHandler(this.cbConnected_CheckedChanged);
            // 
            // tcMald37045cMata37044cConfig
            // 
            this.tcMald37045cMata37044cConfig.Controls.Add(this.tpMald37045cConfig);
            this.tcMald37045cMata37044cConfig.Controls.Add(this.tpMald37044cConfig);
            this.tcMald37045cMata37044cConfig.Controls.Add(this.tpI2cActionLog);
            this.tcMald37045cMata37044cConfig.Location = new System.Drawing.Point(12, 40);
            this.tcMald37045cMata37044cConfig.Name = "tcMald37045cMata37044cConfig";
            this.tcMald37045cMata37044cConfig.SelectedIndex = 0;
            this.tcMald37045cMata37044cConfig.Size = new System.Drawing.Size(657, 582);
            this.tcMald37045cMata37044cConfig.TabIndex = 73;
            // 
            // tpMald37045cConfig
            // 
            this.tpMald37045cConfig.Controls.Add(this.ucMald37045cConfig);
            this.tpMald37045cConfig.Location = new System.Drawing.Point(4, 22);
            this.tpMald37045cConfig.Name = "tpMald37045cConfig";
            this.tpMald37045cConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpMald37045cConfig.Size = new System.Drawing.Size(649, 556);
            this.tpMald37045cConfig.TabIndex = 0;
            this.tpMald37045cConfig.Text = "MALD37045C";
            this.tpMald37045cConfig.UseVisualStyleBackColor = true;
            // 
            // ucMald37045cConfig
            // 
            this.ucMald37045cConfig.Location = new System.Drawing.Point(6, 6);
            this.ucMald37045cConfig.Name = "ucMald37045cConfig";
            this.ucMald37045cConfig.Size = new System.Drawing.Size(642, 550);
            this.ucMald37045cConfig.TabIndex = 0;
            // 
            // tpMald37044cConfig
            // 
            this.tpMald37044cConfig.Controls.Add(this.ucMata37044cConfig);
            this.tpMald37044cConfig.Location = new System.Drawing.Point(4, 22);
            this.tpMald37044cConfig.Name = "tpMald37044cConfig";
            this.tpMald37044cConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpMald37044cConfig.Size = new System.Drawing.Size(649, 556);
            this.tpMald37044cConfig.TabIndex = 1;
            this.tpMald37044cConfig.Text = "MATA37044C";
            this.tpMald37044cConfig.UseVisualStyleBackColor = true;
            // 
            // ucMata37044cConfig
            // 
            this.ucMata37044cConfig.Location = new System.Drawing.Point(6, 6);
            this.ucMata37044cConfig.Name = "ucMata37044cConfig";
            this.ucMata37044cConfig.Size = new System.Drawing.Size(640, 550);
            this.ucMata37044cConfig.TabIndex = 0;
            // 
            // tpI2cActionLog
            // 
            this.tpI2cActionLog.Controls.Add(this.bClearI2cActionLog);
            this.tpI2cActionLog.Controls.Add(this.tbI2cActionLog);
            this.tpI2cActionLog.Location = new System.Drawing.Point(4, 22);
            this.tpI2cActionLog.Name = "tpI2cActionLog";
            this.tpI2cActionLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpI2cActionLog.Size = new System.Drawing.Size(649, 556);
            this.tpI2cActionLog.TabIndex = 2;
            this.tpI2cActionLog.Text = "I2cActionLog";
            this.tpI2cActionLog.UseVisualStyleBackColor = true;
            // 
            // bClearI2cActionLog
            // 
            this.bClearI2cActionLog.Location = new System.Drawing.Point(603, 6);
            this.bClearI2cActionLog.Name = "bClearI2cActionLog";
            this.bClearI2cActionLog.Size = new System.Drawing.Size(40, 544);
            this.bClearI2cActionLog.TabIndex = 1;
            this.bClearI2cActionLog.Text = "Clear";
            this.bClearI2cActionLog.UseVisualStyleBackColor = true;
            this.bClearI2cActionLog.Click += new System.EventHandler(this.bClearI2cActionLog_Click);
            // 
            // tbI2cActionLog
            // 
            this.tbI2cActionLog.Location = new System.Drawing.Point(6, 6);
            this.tbI2cActionLog.Multiline = true;
            this.tbI2cActionLog.Name = "tbI2cActionLog";
            this.tbI2cActionLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbI2cActionLog.Size = new System.Drawing.Size(591, 544);
            this.tbI2cActionLog.TabIndex = 0;
            // 
            // cbI2cActionLog
            // 
            this.cbI2cActionLog.AutoSize = true;
            this.cbI2cActionLog.Location = new System.Drawing.Point(300, 11);
            this.cbI2cActionLog.Name = "cbI2cActionLog";
            this.cbI2cActionLog.Size = new System.Drawing.Size(91, 16);
            this.cbI2cActionLog.TabIndex = 74;
            this.cbI2cActionLog.Text = "I2C action log";
            this.cbI2cActionLog.UseVisualStyleBackColor = true;
            // 
            // FMald37045cMata37044cConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 625);
            this.Controls.Add(this.cbI2cActionLog);
            this.Controls.Add(this.tcMald37045cMata37044cConfig);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.cbConnected);
            this.Name = "FMald37045cMata37044cConfig";
            this.Text = "MATA37045C & MATA37044C Config_20240705";
            this.tcMald37045cMata37044cConfig.ResumeLayout(false);
            this.tpMald37045cConfig.ResumeLayout(false);
            this.tpMald37044cConfig.ResumeLayout(false);
            this.tpI2cActionLog.ResumeLayout(false);
            this.tpI2cActionLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.CheckBox cbConnected;
        private System.Windows.Forms.TabControl tcMald37045cMata37044cConfig;
        private System.Windows.Forms.TabPage tpMald37045cConfig;
        private UcMald37045cConfig ucMald37045cConfig;
        private System.Windows.Forms.TabPage tpMald37044cConfig;
        private UcMata37044cConfig ucMata37044cConfig;
        private System.Windows.Forms.TabPage tpI2cActionLog;
        private System.Windows.Forms.Button bClearI2cActionLog;
        private System.Windows.Forms.TextBox tbI2cActionLog;
        private System.Windows.Forms.CheckBox cbI2cActionLog;
    }
}

