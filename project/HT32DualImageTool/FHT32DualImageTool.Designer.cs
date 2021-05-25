namespace HT32DualImageTool
{
    partial class FHT32DualImageTool
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
            this.cbConnected = new System.Windows.Forms.CheckBox();
            this.ucFirmwareTool = new HT32DualImageTool.UCFirmwareTool();
            this.SuspendLayout();
            // 
            // cbConnected
            // 
            this.cbConnected.AutoSize = true;
            this.cbConnected.Location = new System.Drawing.Point(580, 12);
            this.cbConnected.Name = "cbConnected";
            this.cbConnected.Size = new System.Drawing.Size(74, 16);
            this.cbConnected.TabIndex = 2;
            this.cbConnected.Text = "Connected";
            this.cbConnected.UseVisualStyleBackColor = true;
            this.cbConnected.CheckedChanged += new System.EventHandler(this.cbConnected_CheckedChanged);
            // 
            // ucFirmwareTool
            // 
            this.ucFirmwareTool.Location = new System.Drawing.Point(12, 34);
            this.ucFirmwareTool.Name = "ucFirmwareTool";
            this.ucFirmwareTool.Size = new System.Drawing.Size(640, 480);
            this.ucFirmwareTool.TabIndex = 3;
            // 
            // FHT32DualImageTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 522);
            this.Controls.Add(this.ucFirmwareTool);
            this.Controls.Add(this.cbConnected);
            this.Name = "FHT32DualImageTool";
            this.Text = "HT32 Dual Image Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbConnected;
        private UCFirmwareTool ucFirmwareTool;
    }
}

