namespace HT32DualImageTool
{
    partial class UCFirmwareTool
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
            this.bSend = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.cbFunctionSelect = new System.Windows.Forms.ComboBox();
            this.bClearLog = new System.Windows.Forms.Button();
            this.tbApFilePath = new System.Windows.Forms.TextBox();
            this.bSelectApFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bSend
            // 
            this.bSend.Location = new System.Drawing.Point(511, 3);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(60, 20);
            this.bSend.TabIndex = 0;
            this.bSend.Text = "Send";
            this.bSend.UseVisualStyleBackColor = true;
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(3, 29);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(634, 448);
            this.tbLog.TabIndex = 1;
            // 
            // cbFunctionSelect
            // 
            this.cbFunctionSelect.FormattingEnabled = true;
            this.cbFunctionSelect.Location = new System.Drawing.Point(384, 3);
            this.cbFunctionSelect.Name = "cbFunctionSelect";
            this.cbFunctionSelect.Size = new System.Drawing.Size(121, 20);
            this.cbFunctionSelect.TabIndex = 2;
            // 
            // bClearLog
            // 
            this.bClearLog.Location = new System.Drawing.Point(577, 3);
            this.bClearLog.Name = "bClearLog";
            this.bClearLog.Size = new System.Drawing.Size(60, 20);
            this.bClearLog.TabIndex = 3;
            this.bClearLog.Text = "Clear";
            this.bClearLog.UseVisualStyleBackColor = true;
            this.bClearLog.Click += new System.EventHandler(this.bClearLog_Click);
            // 
            // tbApFilePath
            // 
            this.tbApFilePath.Location = new System.Drawing.Point(3, 3);
            this.tbApFilePath.Name = "tbApFilePath";
            this.tbApFilePath.ReadOnly = true;
            this.tbApFilePath.Size = new System.Drawing.Size(309, 22);
            this.tbApFilePath.TabIndex = 4;
            // 
            // bSelectApFile
            // 
            this.bSelectApFile.Location = new System.Drawing.Point(318, 3);
            this.bSelectApFile.Name = "bSelectApFile";
            this.bSelectApFile.Size = new System.Drawing.Size(60, 20);
            this.bSelectApFile.TabIndex = 5;
            this.bSelectApFile.Text = "Open";
            this.bSelectApFile.UseVisualStyleBackColor = true;
            this.bSelectApFile.Click += new System.EventHandler(this.bSelectApFile_Click);
            // 
            // UCFirmwareTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bSelectApFile);
            this.Controls.Add(this.tbApFilePath);
            this.Controls.Add(this.bClearLog);
            this.Controls.Add(this.cbFunctionSelect);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.bSend);
            this.Name = "UCFirmwareTool";
            this.Size = new System.Drawing.Size(640, 480);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bSend;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.ComboBox cbFunctionSelect;
        private System.Windows.Forms.Button bClearLog;
        private System.Windows.Forms.TextBox tbApFilePath;
        private System.Windows.Forms.Button bSelectApFile;
    }
}
