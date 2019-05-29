namespace FirmwareFlashLead
{
    partial class FFirmwareFlashLead
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
            if (disposing && (components != null))
            {
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
            this.ucFirmwareFlashLead1 = new FirmwareFlashLead.UcFirmwareFlashLead();
            this.SuspendLayout();
            // 
            // ucFirmwareFlashLead1
            // 
            this.ucFirmwareFlashLead1.Location = new System.Drawing.Point(12, 12);
            this.ucFirmwareFlashLead1.Name = "ucFirmwareFlashLead1";
            this.ucFirmwareFlashLead1.Size = new System.Drawing.Size(658, 358);
            this.ucFirmwareFlashLead1.TabIndex = 0;
            // 
            // FFirmwareFlashLead
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 382);
            this.Controls.Add(this.ucFirmwareFlashLead1);
            this.Name = "FFirmwareFlashLead";
            this.Text = "Firmware Flash Lead";
            this.ResumeLayout(false);

        }

        #endregion

        private UcFirmwareFlashLead ucFirmwareFlashLead1;
    }
}

