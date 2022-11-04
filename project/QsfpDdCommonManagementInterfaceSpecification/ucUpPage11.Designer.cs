namespace QsfpDdCommonManagementInterfaceSpecification
{
    partial class ucUpPage11
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
            this.bRead = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bRead
            // 
            this.bRead.Location = new System.Drawing.Point(871, 3);
            this.bRead.Name = "bRead";
            this.bRead.Size = new System.Drawing.Size(90, 21);
            this.bRead.TabIndex = 3;
            this.bRead.Text = "Read All";
            this.bRead.UseVisualStyleBackColor = true;
            // 
            // ucUpPage11
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bRead);
            this.Name = "ucUpPage11";
            this.Size = new System.Drawing.Size(964, 696);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bRead;
    }
}
