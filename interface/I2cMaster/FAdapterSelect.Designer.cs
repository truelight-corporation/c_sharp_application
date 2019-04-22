namespace I2cMasterInterface
{
    partial class FAdapterSelect
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
            this.adapterSelector = new I2cMasterInterface.AdapterSelector();
            this.SuspendLayout();
            // 
            // adapterSelector
            // 
            this.adapterSelector.Location = new System.Drawing.Point(-1, 0);
            this.adapterSelector.Name = "adapterSelector";
            this.adapterSelector.Size = new System.Drawing.Size(320, 238);
            this.adapterSelector.TabIndex = 0;
            // 
            // FAdapterSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 237);
            this.Controls.Add(this.adapterSelector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FAdapterSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Adapter select";
            this.ResumeLayout(false);

        }

        #endregion

        public AdapterSelector adapterSelector;


    }
}

