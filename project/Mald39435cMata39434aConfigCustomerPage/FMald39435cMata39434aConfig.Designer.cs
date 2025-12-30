namespace Mald39435Mata39434aConfig
{
    partial class FMald39435cMata39434aConfig
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
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkI2cActionLog = new System.Windows.Forms.CheckBox();
            this.chkConnected = new System.Windows.Forms.CheckBox();
            this.tabMald39435cMata39434aConfig = new System.Windows.Forms.TabControl();
            this.tabMald39435cConfig = new System.Windows.Forms.TabPage();
            this.ucMald39435cConfig = new Mald39435Mata39434aConfig.UcMald39435cConfig();
            this.tabMata39434aConfig = new System.Windows.Forms.TabPage();
            this.ucMata39434aConfig = new Mald39435Mata39434aConfig.UcMata39434aConfig();
            this.tabI2CActionLog = new System.Windows.Forms.TabPage();
            this.btnClearI2cActionLog = new System.Windows.Forms.Button();
            this.txtI2cActionLog = new System.Windows.Forms.TextBox();
            this.btnClearActionLog = new System.Windows.Forms.Button();
            this.tabMald39435cMata39434aConfig.SuspendLayout();
            this.tabMald39435cConfig.SuspendLayout();
            this.tabMata39434aConfig.SuspendLayout();
            this.tabI2CActionLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(12, 15);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(60, 12);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "Password：";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(72, 10);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(60, 22);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.Text = "3234";
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // chkI2cActionLog
            // 
            this.chkI2cActionLog.AutoSize = true;
            this.chkI2cActionLog.Location = new System.Drawing.Point(165, 13);
            this.chkI2cActionLog.Name = "chkI2cActionLog";
            this.chkI2cActionLog.Size = new System.Drawing.Size(91, 16);
            this.chkI2cActionLog.TabIndex = 2;
            this.chkI2cActionLog.Text = "I2C action log";
            this.chkI2cActionLog.UseVisualStyleBackColor = true;
            // 
            // chkConnected
            // 
            this.chkConnected.AutoSize = true;
            this.chkConnected.Location = new System.Drawing.Point(291, 13);
            this.chkConnected.Name = "chkConnected";
            this.chkConnected.Size = new System.Drawing.Size(74, 16);
            this.chkConnected.TabIndex = 3;
            this.chkConnected.Text = "Connected";
            this.chkConnected.UseVisualStyleBackColor = true;
            this.chkConnected.CheckedChanged += new System.EventHandler(this.chkConnected_CheckedChanged);
            // 
            // tabMald39435cMata39434aConfig
            // 
            this.tabMald39435cMata39434aConfig.Controls.Add(this.tabMald39435cConfig);
            this.tabMald39435cMata39434aConfig.Controls.Add(this.tabMata39434aConfig);
            this.tabMald39435cMata39434aConfig.Controls.Add(this.tabI2CActionLog);
            this.tabMald39435cMata39434aConfig.Location = new System.Drawing.Point(12, 38);
            this.tabMald39435cMata39434aConfig.Name = "tabMald39435cMata39434aConfig";
            this.tabMald39435cMata39434aConfig.SelectedIndex = 0;
            this.tabMald39435cMata39434aConfig.Size = new System.Drawing.Size(945, 796);
            this.tabMald39435cMata39434aConfig.TabIndex = 4;
            // 
            // tabMald39435cConfig
            // 
            this.tabMald39435cConfig.Controls.Add(this.ucMald39435cConfig);
            this.tabMald39435cConfig.Location = new System.Drawing.Point(4, 22);
            this.tabMald39435cConfig.Name = "tabMald39435cConfig";
            this.tabMald39435cConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabMald39435cConfig.Size = new System.Drawing.Size(937, 770);
            this.tabMald39435cConfig.TabIndex = 0;
            this.tabMald39435cConfig.Text = "MALD-39435C";
            this.tabMald39435cConfig.UseVisualStyleBackColor = true;
            // 
            // ucMald39435cConfig
            // 
            this.ucMald39435cConfig.Location = new System.Drawing.Point(6, 6);
            this.ucMald39435cConfig.Name = "ucMald39435cConfig";
            this.ucMald39435cConfig.Size = new System.Drawing.Size(820, 760);
            this.ucMald39435cConfig.TabIndex = 0;
            // 
            // tabMata39434aConfig
            // 
            this.tabMata39434aConfig.Controls.Add(this.ucMata39434aConfig);
            this.tabMata39434aConfig.Location = new System.Drawing.Point(4, 22);
            this.tabMata39434aConfig.Name = "tabMata39434aConfig";
            this.tabMata39434aConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabMata39434aConfig.Size = new System.Drawing.Size(937, 770);
            this.tabMata39434aConfig.TabIndex = 1;
            this.tabMata39434aConfig.Text = "MATA-39434A";
            this.tabMata39434aConfig.UseVisualStyleBackColor = true;
            // 
            // ucMata39434aConfig
            // 
            this.ucMata39434aConfig.Location = new System.Drawing.Point(6, 3);
            this.ucMata39434aConfig.Name = "ucMata39434aConfig";
            this.ucMata39434aConfig.Size = new System.Drawing.Size(926, 574);
            this.ucMata39434aConfig.TabIndex = 0;
            // 
            // tabI2CActionLog
            // 
            this.tabI2CActionLog.Controls.Add(this.btnClearActionLog);
            this.tabI2CActionLog.Controls.Add(this.btnClearI2cActionLog);
            this.tabI2CActionLog.Controls.Add(this.txtI2cActionLog);
            this.tabI2CActionLog.Location = new System.Drawing.Point(4, 22);
            this.tabI2CActionLog.Name = "tabI2CActionLog";
            this.tabI2CActionLog.Size = new System.Drawing.Size(937, 770);
            this.tabI2CActionLog.TabIndex = 2;
            this.tabI2CActionLog.Text = "I2CActionLog";
            this.tabI2CActionLog.UseVisualStyleBackColor = true;
            // 
            // btnClearI2cActionLog
            // 
            this.btnClearI2cActionLog.Location = new System.Drawing.Point(1146, 3);
            this.btnClearI2cActionLog.Name = "btnClearI2cActionLog";
            this.btnClearI2cActionLog.Size = new System.Drawing.Size(123, 566);
            this.btnClearI2cActionLog.TabIndex = 1;
            this.btnClearI2cActionLog.Text = "Clear Log";
            this.btnClearI2cActionLog.UseVisualStyleBackColor = true;
            this.btnClearI2cActionLog.Click += new System.EventHandler(this.btnClearI2cActionLog_Click);
            // 
            // txtI2cActionLog
            // 
            this.txtI2cActionLog.Location = new System.Drawing.Point(3, 3);
            this.txtI2cActionLog.Multiline = true;
            this.txtI2cActionLog.Name = "txtI2cActionLog";
            this.txtI2cActionLog.Size = new System.Drawing.Size(691, 764);
            this.txtI2cActionLog.TabIndex = 0;
            // 
            // btnClearActionLog
            // 
            this.btnClearActionLog.Location = new System.Drawing.Point(700, 3);
            this.btnClearActionLog.Name = "btnClearActionLog";
            this.btnClearActionLog.Size = new System.Drawing.Size(224, 764);
            this.btnClearActionLog.TabIndex = 2;
            this.btnClearActionLog.Text = "Clear Log";
            this.btnClearActionLog.UseVisualStyleBackColor = true;
            this.btnClearActionLog.Click += new System.EventHandler(this.btnClearActionLog_Click);
            // 
            // FMald39435cMata39434aConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 841);
            this.Controls.Add(this.tabMald39435cMata39434aConfig);
            this.Controls.Add(this.chkConnected);
            this.Controls.Add(this.chkI2cActionLog);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Name = "FMald39435cMata39434aConfig";
            this.Text = "MALD39435C & MATA39434A Config";
            this.tabMald39435cMata39434aConfig.ResumeLayout(false);
            this.tabMald39435cConfig.ResumeLayout(false);
            this.tabMata39434aConfig.ResumeLayout(false);
            this.tabI2CActionLog.ResumeLayout(false);
            this.tabI2CActionLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkI2cActionLog;
        private System.Windows.Forms.CheckBox chkConnected;
        private System.Windows.Forms.TabControl tabMald39435cMata39434aConfig;
        private System.Windows.Forms.TabPage tabMald39435cConfig;
        private System.Windows.Forms.TabPage tabMata39434aConfig;
        private System.Windows.Forms.TabPage tabI2CActionLog;
        private System.Windows.Forms.Button btnClearI2cActionLog;
        private System.Windows.Forms.TextBox txtI2cActionLog;
        private UcMald39435cConfig ucMald39435cConfig;
        private UcMata39434aConfig ucMata39434aConfig;
        private System.Windows.Forms.Button btnClearActionLog;
    }
}

