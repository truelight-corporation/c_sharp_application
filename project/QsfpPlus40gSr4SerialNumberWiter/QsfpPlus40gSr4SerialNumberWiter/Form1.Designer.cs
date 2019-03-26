namespace QsfpPlus40gSr4SerialNumberWiter
{
    partial class Form1
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
            this.cbI2cAdapterConnected = new System.Windows.Forms.CheckBox();
            this.cbStartReadSerialNumber = new System.Windows.Forms.CheckBox();
            this.ucSerialNumberWriter = new QsfpPlus40gSr4SerialNumberWiter.UcSerialNumberWriter();
            this.SuspendLayout();
            // 
            // cbI2cAdapterConnected
            // 
            this.cbI2cAdapterConnected.AutoSize = true;
            this.cbI2cAdapterConnected.Location = new System.Drawing.Point(16, 10);
            this.cbI2cAdapterConnected.Margin = new System.Windows.Forms.Padding(1);
            this.cbI2cAdapterConnected.Name = "cbI2cAdapterConnected";
            this.cbI2cAdapterConnected.Size = new System.Drawing.Size(66, 16);
            this.cbI2cAdapterConnected.TabIndex = 2;
            this.cbI2cAdapterConnected.Text = "I2C連接";
            this.cbI2cAdapterConnected.UseVisualStyleBackColor = true;
            this.cbI2cAdapterConnected.CheckedChanged += new System.EventHandler(this.cbI2cAdapterConnected_CheckedChanged);
            // 
            // cbStartReadSerialNumber
            // 
            this.cbStartReadSerialNumber.AutoSize = true;
            this.cbStartReadSerialNumber.Location = new System.Drawing.Point(669, 10);
            this.cbStartReadSerialNumber.Margin = new System.Windows.Forms.Padding(1);
            this.cbStartReadSerialNumber.Name = "cbStartReadSerialNumber";
            this.cbStartReadSerialNumber.Size = new System.Drawing.Size(72, 16);
            this.cbStartReadSerialNumber.TabIndex = 4;
            this.cbStartReadSerialNumber.Text = "自動讀取";
            this.cbStartReadSerialNumber.UseVisualStyleBackColor = true;
            this.cbStartReadSerialNumber.CheckedChanged += new System.EventHandler(this.cbStartReadSerialNumber_CheckedChanged);
            // 
            // ucSerialNumberWriter
            // 
            this.ucSerialNumberWriter.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ucSerialNumberWriter.Location = new System.Drawing.Point(16, 33);
            this.ucSerialNumberWriter.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.ucSerialNumberWriter.Name = "ucSerialNumberWriter";
            this.ucSerialNumberWriter.Size = new System.Drawing.Size(715, 548);
            this.ucSerialNumberWriter.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 576);
            this.Controls.Add(this.cbStartReadSerialNumber);
            this.Controls.Add(this.ucSerialNumberWriter);
            this.Controls.Add(this.cbI2cAdapterConnected);
            this.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "XaSerialNumberWrite 20190304";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox cbI2cAdapterConnected;
        private UcSerialNumberWriter ucSerialNumberWriter;
        private System.Windows.Forms.CheckBox cbStartReadSerialNumber;
    }
}

