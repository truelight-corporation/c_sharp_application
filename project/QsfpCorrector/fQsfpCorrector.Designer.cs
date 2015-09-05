namespace QsfpCorrector
{
    partial class FQsfpCorrector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbQsfpConnected = new System.Windows.Forms.CheckBox();
            this.cbMonitorConnected = new System.Windows.Forms.CheckBox();
            this.ucQsfpCorrector = new QsfpCorrector.UcQsfpCorrector();
            this.SuspendLayout();
            // 
            // cbQsfpConnected
            // 
            this.cbQsfpConnected.AutoSize = true;
            this.cbQsfpConnected.Location = new System.Drawing.Point(395, 10);
            this.cbQsfpConnected.Name = "cbQsfpConnected";
            this.cbQsfpConnected.Size = new System.Drawing.Size(103, 16);
            this.cbQsfpConnected.TabIndex = 1;
            this.cbQsfpConnected.Text = "QSFP Connected";
            this.cbQsfpConnected.UseVisualStyleBackColor = true;
            this.cbQsfpConnected.CheckedChanged += new System.EventHandler(this._cbQsfpConnected_CheckedChanged);
            // 
            // cbMonitorConnected
            // 
            this.cbMonitorConnected.AutoSize = true;
            this.cbMonitorConnected.Location = new System.Drawing.Point(504, 10);
            this.cbMonitorConnected.Name = "cbMonitorConnected";
            this.cbMonitorConnected.Size = new System.Drawing.Size(115, 16);
            this.cbMonitorConnected.TabIndex = 2;
            this.cbMonitorConnected.Text = "Monitor Connected";
            this.cbMonitorConnected.UseVisualStyleBackColor = true;
            this.cbMonitorConnected.Visible = false;
            // 
            // ucQsfpCorrector
            // 
            this.ucQsfpCorrector.Location = new System.Drawing.Point(3, 32);
            this.ucQsfpCorrector.Name = "ucQsfpCorrector";
            this.ucQsfpCorrector.Size = new System.Drawing.Size(627, 523);
            this.ucQsfpCorrector.TabIndex = 0;
            // 
            // FQsfpCorrector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 553);
            this.Controls.Add(this.cbMonitorConnected);
            this.Controls.Add(this.cbQsfpConnected);
            this.Controls.Add(this.ucQsfpCorrector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FQsfpCorrector";
            this.Text = "QSFP+ Corrector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UcQsfpCorrector ucQsfpCorrector;
        private System.Windows.Forms.CheckBox cbQsfpConnected;
        private System.Windows.Forms.CheckBox cbMonitorConnected;
    }
}

