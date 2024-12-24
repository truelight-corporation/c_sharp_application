namespace LiDARFunctionTest
{
    partial class FLiDARFunctionTest
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
            this.cbI2cConnected = new System.Windows.Forms.CheckBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.ucLiDARFunctionTest = new LiDARFunctionTest.UcLiDARFunctionTest();
            this.SuspendLayout();
            // 
            // cbI2cConnected
            // 
            this.cbI2cConnected.AutoSize = true;
            this.cbI2cConnected.Location = new System.Drawing.Point(12, 12);
            this.cbI2cConnected.Name = "cbI2cConnected";
            this.cbI2cConnected.Size = new System.Drawing.Size(92, 16);
            this.cbI2cConnected.TabIndex = 0;
            this.cbI2cConnected.Text = "I2C connected";
            this.cbI2cConnected.UseVisualStyleBackColor = true;
            this.cbI2cConnected.CheckedChanged += new System.EventHandler(this.cbI2cConnected_CheckedChanged);
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point(110, 13);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(51, 12);
            this.lPassword.TabIndex = 2;
            this.lPassword.Text = "Password:";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(167, 10);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.ReadOnly = true;
            this.tbPassword.Size = new System.Drawing.Size(30, 22);
            this.tbPassword.TabIndex = 3;
            this.tbPassword.Text = "3234";
            // 
            // ucLiDARFunctionTest
            // 
            this.ucLiDARFunctionTest.Location = new System.Drawing.Point(12, 34);
            this.ucLiDARFunctionTest.Name = "ucLiDARFunctionTest";
            this.ucLiDARFunctionTest.Size = new System.Drawing.Size(700, 340);
            this.ucLiDARFunctionTest.TabIndex = 1;
            // 
            // FLiDARFunctionTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 384);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.ucLiDARFunctionTest);
            this.Controls.Add(this.cbI2cConnected);
            this.Name = "FLiDARFunctionTest";
            this.Text = "LiDAR function test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbI2cConnected;
        private UcLiDARFunctionTest ucLiDARFunctionTest;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.TextBox tbPassword;
    }
}

