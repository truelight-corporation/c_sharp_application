namespace Gn1190Corrector
{
    partial class FGn1190Corrector
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
            this.cbConnected = new System.Windows.Forms.CheckBox();
            this.ucGn1190Corrector = new Gn1190Corrector.UcGn1190Corrector();
            this.SuspendLayout();
            // 
            // cbConnected
            // 
            this.cbConnected.AutoSize = true;
            this.cbConnected.Location = new System.Drawing.Point(724, 12);
            this.cbConnected.Name = "cbConnected";
            this.cbConnected.Size = new System.Drawing.Size(74, 16);
            this.cbConnected.TabIndex = 0;
            this.cbConnected.Text = "Connected";
            this.cbConnected.UseVisualStyleBackColor = true;
            this.cbConnected.CheckedChanged += new System.EventHandler(this.cbConnected_CheckedChanged);
            // 
            // ucGn1190Corrector
            // 
            this.ucGn1190Corrector.Location = new System.Drawing.Point(12, 34);
            this.ucGn1190Corrector.Name = "ucGn1190Corrector";
            this.ucGn1190Corrector.Size = new System.Drawing.Size(795, 445);
            this.ucGn1190Corrector.TabIndex = 10;
            // 
            // FGn1190Corrector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 482);
            this.Controls.Add(this.ucGn1190Corrector);
            this.Controls.Add(this.cbConnected);
            this.Name = "FGn1190Corrector";
            this.Text = "GN1190 Corrector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbConnected;
        private UcGn1190Corrector ucGn1190Corrector;
    }
}

