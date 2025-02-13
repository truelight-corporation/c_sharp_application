namespace LiDAR_live_demo
{
    partial class FLiDARLiveDemo
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
            this.ucLiDARLiveDemo1 = new LiDAR_live_demo.UcLiDARLiveDemo();
            this.SuspendLayout();
            // 
            // ucLiDARLiveDemo1
            // 
            this.ucLiDARLiveDemo1.Location = new System.Drawing.Point(12, 12);
            this.ucLiDARLiveDemo1.Name = "ucLiDARLiveDemo1";
            this.ucLiDARLiveDemo1.Size = new System.Drawing.Size(600, 417);
            this.ucLiDARLiveDemo1.TabIndex = 0;
            // 
            // FLiDARLiveDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.ucLiDARLiveDemo1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(640, 480);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FLiDARLiveDemo";
            this.Text = "LiDAR live demo";
            this.ResumeLayout(false);

        }

        #endregion

        private UcLiDARLiveDemo ucLiDARLiveDemo1;
    }
}

