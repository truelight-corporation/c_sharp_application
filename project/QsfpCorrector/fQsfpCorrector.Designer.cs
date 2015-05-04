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
            this.ucQsfpCorrector = new QsfpCorrector.UcQsfpCorrector();
            this.SuspendLayout();
            // 
            // ucQsfpCorrector
            // 
            this.ucQsfpCorrector.Location = new System.Drawing.Point(0, 1);
            this.ucQsfpCorrector.Name = "ucQsfpCorrector";
            this.ucQsfpCorrector.Size = new System.Drawing.Size(620, 440);
            this.ucQsfpCorrector.TabIndex = 0;
            // 
            // FQsfpCorrector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.ucQsfpCorrector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FQsfpCorrector";
            this.Text = "QSFP+ Corrector";
            this.ResumeLayout(false);

        }

        #endregion

        private UcQsfpCorrector ucQsfpCorrector;
    }
}

