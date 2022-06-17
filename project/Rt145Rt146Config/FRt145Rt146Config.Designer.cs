namespace Rt145Rt146Config
{
    partial class FRt145Rt146Config
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
            this.tpRt145Rt146Config = new System.Windows.Forms.TabControl();
            this.tpRt145Config = new System.Windows.Forms.TabPage();
            this.tpRt146Config = new System.Windows.Forms.TabPage();
            this.cbConnected = new System.Windows.Forms.CheckBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.ucRt145Config = new Rt145Rt146Config.UcRt145Config();
            this.ucRt146Config = new Rt145Rt146Config.UcRt146Config();
            this.tpRt145Rt146Config.SuspendLayout();
            this.tpRt145Config.SuspendLayout();
            this.tpRt146Config.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRt145Rt146Config
            // 
            this.tpRt145Rt146Config.Controls.Add(this.tpRt145Config);
            this.tpRt145Rt146Config.Controls.Add(this.tpRt146Config);
            this.tpRt145Rt146Config.Font = new System.Drawing.Font("Arial", 12F);
            this.tpRt145Rt146Config.Location = new System.Drawing.Point(12, 36);
            this.tpRt145Rt146Config.Name = "tpRt145Rt146Config";
            this.tpRt145Rt146Config.SelectedIndex = 0;
            this.tpRt145Rt146Config.Size = new System.Drawing.Size(1460, 771);
            this.tpRt145Rt146Config.TabIndex = 0;
            // 
            // tpRt145Config
            // 
            this.tpRt145Config.Controls.Add(this.ucRt145Config);
            this.tpRt145Config.Location = new System.Drawing.Point(4, 27);
            this.tpRt145Config.Name = "tpRt145Config";
            this.tpRt145Config.Padding = new System.Windows.Forms.Padding(3);
            this.tpRt145Config.Size = new System.Drawing.Size(1452, 740);
            this.tpRt145Config.TabIndex = 0;
            this.tpRt145Config.Text = "RT145 (RX)";
            this.tpRt145Config.UseVisualStyleBackColor = true;
            // 
            // tpRt146Config
            // 
            this.tpRt146Config.Controls.Add(this.ucRt146Config);
            this.tpRt146Config.Location = new System.Drawing.Point(4, 27);
            this.tpRt146Config.Name = "tpRt146Config";
            this.tpRt146Config.Padding = new System.Windows.Forms.Padding(3);
            this.tpRt146Config.Size = new System.Drawing.Size(1452, 740);
            this.tpRt146Config.TabIndex = 1;
            this.tpRt146Config.Text = "RT146 (TX)";
            this.tpRt146Config.UseVisualStyleBackColor = true;
            // 
            // cbConnected
            // 
            this.cbConnected.AutoSize = true;
            this.cbConnected.Font = new System.Drawing.Font("Arial", 14F);
            this.cbConnected.Location = new System.Drawing.Point(1323, 12);
            this.cbConnected.Name = "cbConnected";
            this.cbConnected.Size = new System.Drawing.Size(122, 26);
            this.cbConnected.TabIndex = 1;
            this.cbConnected.Text = "Connected";
            this.cbConnected.UseVisualStyleBackColor = true;
            this.cbConnected.CheckedChanged += new System.EventHandler(this.cbConnected_CheckedChanged);
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Font = new System.Drawing.Font("Arial", 14F);
            this.lPassword.Location = new System.Drawing.Point(12, 9);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(114, 22);
            this.lPassword.TabIndex = 2;
            this.lPassword.Text = "Password：";
            // 
            // tbPassword
            // 
            this.tbPassword.Font = new System.Drawing.Font("Arial", 12F);
            this.tbPassword.Location = new System.Drawing.Point(121, 5);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(59, 26);
            this.tbPassword.TabIndex = 3;
            this.tbPassword.Text = "3234";
            this.tbPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // ucRt145Config
            // 
            this.ucRt145Config.Location = new System.Drawing.Point(-1, -2);
            this.ucRt145Config.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.ucRt145Config.Name = "ucRt145Config";
            this.ucRt145Config.Size = new System.Drawing.Size(1454, 744);
            this.ucRt145Config.TabIndex = 1;
            // 
            // ucRt146Config
            // 
            this.ucRt146Config.Location = new System.Drawing.Point(-1, -2);
            this.ucRt146Config.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.ucRt146Config.Name = "ucRt146Config";
            this.ucRt146Config.Size = new System.Drawing.Size(1454, 744);
            this.ucRt146Config.TabIndex = 1;
            // 
            // FRt145Rt146Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 801);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.cbConnected);
            this.Controls.Add(this.tpRt145Rt146Config);
            this.Name = "FRt145Rt146Config";
            this.Text = "Form1";
            this.tpRt145Rt146Config.ResumeLayout(false);
            this.tpRt145Config.ResumeLayout(false);
            this.tpRt146Config.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tpRt145Rt146Config;
        private System.Windows.Forms.TabPage tpRt145Config;
        private System.Windows.Forms.TabPage tpRt146Config;
        private System.Windows.Forms.CheckBox cbConnected;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.TextBox tbPassword;
        private UcRt145Config ucRt145Config;
        private UcRt146Config ucRt146Config;
    }
}

