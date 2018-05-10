namespace SfpCorrector
{
    partial class FSfpCorrector
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
            this.ucSfpCorrector = new SfpCorrector.UcSfpCorrector();
            this.cbConnected = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ucSfpCorrector
            // 
            this.ucSfpCorrector.Location = new System.Drawing.Point(12, 34);
            this.ucSfpCorrector.Name = "ucSfpCorrector";
            this.ucSfpCorrector.Size = new System.Drawing.Size(614, 496);
            this.ucSfpCorrector.TabIndex = 0;
            // 
            // cbConnected
            // 
            this.cbConnected.AutoSize = true;
            this.cbConnected.Location = new System.Drawing.Point(552, 12);
            this.cbConnected.Name = "cbConnected";
            this.cbConnected.Size = new System.Drawing.Size(74, 16);
            this.cbConnected.TabIndex = 1;
            this.cbConnected.Text = "Connected";
            this.cbConnected.UseVisualStyleBackColor = true;
            this.cbConnected.CheckedChanged += new System.EventHandler(this.cbConnected_CheckedChanged);
            // 
            // FSfpCorrector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 534);
            this.Controls.Add(this.cbConnected);
            this.Controls.Add(this.ucSfpCorrector);
            this.Name = "FSfpCorrector";
            this.Text = "SFP Corrector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UcSfpCorrector ucSfpCorrector;
        private System.Windows.Forms.CheckBox cbConnected;
    }
}

