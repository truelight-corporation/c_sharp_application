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
            this.cbI2cAdapterConnected = new System.Windows.Forms.CheckBox();
            this.cbAutoRead = new System.Windows.Forms.CheckBox();
            this.ucQsfpCorrector = new QsfpCorrector.UcQsfpCorrector();
            this.SuspendLayout();
            // 
            // cbI2cAdapterConnected
            // 
            this.cbI2cAdapterConnected.AutoSize = true;
            this.cbI2cAdapterConnected.Location = new System.Drawing.Point(12, 10);
            this.cbI2cAdapterConnected.Name = "cbI2cAdapterConnected";
            this.cbI2cAdapterConnected.Size = new System.Drawing.Size(78, 16);
            this.cbI2cAdapterConnected.TabIndex = 1;
            this.cbI2cAdapterConnected.Text = "I2C已連接";
            this.cbI2cAdapterConnected.UseVisualStyleBackColor = true;
            this.cbI2cAdapterConnected.CheckedChanged += new System.EventHandler(this._cbQsfpConnected_CheckedChanged);
            // 
            // cbAutoRead
            // 
            this.cbAutoRead.AutoSize = true;
            this.cbAutoRead.Location = new System.Drawing.Point(934, 10);
            this.cbAutoRead.Name = "cbAutoRead";
            this.cbAutoRead.Size = new System.Drawing.Size(72, 16);
            this.cbAutoRead.TabIndex = 2;
            this.cbAutoRead.Text = "自動讀取";
            this.cbAutoRead.UseVisualStyleBackColor = true;
            this.cbAutoRead.CheckedChanged += new System.EventHandler(this.cbAutoRead_CheckedChanged);
            // 
            // ucQsfpCorrector
            // 
            this.ucQsfpCorrector.Location = new System.Drawing.Point(3, 32);
            this.ucQsfpCorrector.Name = "ucQsfpCorrector";
            this.ucQsfpCorrector.Size = new System.Drawing.Size(1010, 653);
            this.ucQsfpCorrector.TabIndex = 0;
            // 
            // FQsfpCorrector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 686);
            this.Controls.Add(this.cbAutoRead);
            this.Controls.Add(this.cbI2cAdapterConnected);
            this.Controls.Add(this.ucQsfpCorrector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FQsfpCorrector";
            this.Text = "QSFP+ Corrector 20200602";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UcQsfpCorrector ucQsfpCorrector;
        private System.Windows.Forms.CheckBox cbI2cAdapterConnected;
        private System.Windows.Forms.CheckBox cbAutoRead;
    }
}

