namespace NuvotonIcpTool
{
    partial class MainForm
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ucNuvotonIcpTool1 = new NuvotonIcpTool.UcNuvotonIcpTool();
            this.cbChannelSet = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ucNuvotonIcpTool1
            // 
            this.ucNuvotonIcpTool1.Location = new System.Drawing.Point(12, 12);
            this.ucNuvotonIcpTool1.Margin = new System.Windows.Forms.Padding(4);
            this.ucNuvotonIcpTool1.Name = "ucNuvotonIcpTool1";
            this.ucNuvotonIcpTool1.Size = new System.Drawing.Size(801, 244);
            this.ucNuvotonIcpTool1.TabIndex = 0;
            // 
            // cbChannelSet
            // 
            this.cbChannelSet.AutoSize = true;
            this.cbChannelSet.Location = new System.Drawing.Point(440, 247);
            this.cbChannelSet.Name = "cbChannelSet";
            this.cbChannelSet.Size = new System.Drawing.Size(15, 14);
            this.cbChannelSet.TabIndex = 1;
            this.cbChannelSet.UseVisualStyleBackColor = true;
            this.cbChannelSet.CheckedChanged += new System.EventHandler(this.cbChannelSetToOne_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 267);
            this.Controls.Add(this.cbChannelSet);
            this.Controls.Add(this.ucNuvotonIcpTool1);
            this.Name = "MainForm";
            this.Text = "NuvotonTool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private UcNuvotonIcpTool ucNuvotonIcpTool1;
        private System.Windows.Forms.CheckBox cbChannelSet;
    }
}

