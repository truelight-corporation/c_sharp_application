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
            this.tcMald37045cMata37044cConfig.SuspendLayout();
            this.tpMald37045cConfig.SuspendLayout();
            this.tpMald37044cConfig.SuspendLayout();
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
            this.tcMald37045cMata37044cConfig.Location = new System.Drawing.Point(12, 40);
            this.tcMald37045cMata37044cConfig.Name = "tcMald37045cMata37044cConfig";
            this.tcMald37045cMata37044cConfig.SelectedIndex = 0;
            this.tcMald37045cMata37044cConfig.Size = new System.Drawing.Size(657, 517);
            this.tcMald37045cMata37044cConfig.TabIndex = 73;
            // 
            // tpMald37045cConfig
            // 
            this.tpMald37045cConfig.Controls.Add(this.ucMald37045cConfig);
            this.tpMald37045cConfig.Location = new System.Drawing.Point(4, 22);
            this.tpMald37045cConfig.Name = "tpMald37045cConfig";
            this.tpMald37045cConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpMald37045cConfig.Size = new System.Drawing.Size(649, 491);
            this.tpMald37045cConfig.TabIndex = 0;
            this.tpMald37045cConfig.Text = "MALD37045C";
            this.tpMald37045cConfig.UseVisualStyleBackColor = true;
            // 
            // ucMald37045cConfig
            // 
            this.ucMald37045cConfig.Location = new System.Drawing.Point(6, 6);
            this.ucMald37045cConfig.Name = "ucMald37045cConfig";
            this.ucMald37045cConfig.Size = new System.Drawing.Size(642, 481);
            this.ucMald37045cConfig.TabIndex = 0;
            // 
            // tpMald37044cConfig
            // 
            this.tpMald37044cConfig.Controls.Add(this.ucMata37044cConfig);
            this.tpMald37044cConfig.Location = new System.Drawing.Point(4, 22);
            this.tpMald37044cConfig.Name = "tpMald37044cConfig";
            this.tpMald37044cConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpMald37044cConfig.Size = new System.Drawing.Size(649, 491);
            this.tpMald37044cConfig.TabIndex = 1;
            this.tpMald37044cConfig.Text = "MATA37044C";
            this.tpMald37044cConfig.UseVisualStyleBackColor = true;
            // 
            // ucMata37044cConfig
            // 
            this.ucMata37044cConfig.Location = new System.Drawing.Point(6, 6);
            this.ucMata37044cConfig.Name = "ucMata37044cConfig";
            this.ucMata37044cConfig.Size = new System.Drawing.Size(640, 480);
            this.ucMata37044cConfig.TabIndex = 0;
            // 
            // FMald37045cMata37044c
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 567);
            this.Controls.Add(this.tcMald37045cMata37044cConfig);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.cbConnected);
            this.Name = "FMald37045cMata37044c";
            this.Text = "MATA37045C & MATA37044C Config";
            this.tcMald37045cMata37044cConfig.ResumeLayout(false);
            this.tpMald37045cConfig.ResumeLayout(false);
            this.tpMald37044cConfig.ResumeLayout(false);
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
    }
}

