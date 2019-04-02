namespace QsfpPlus40gSr4ConfigBackup
{
    partial class Form1
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
            this.cbI2cAdapterConnected = new System.Windows.Forms.CheckBox();
            this.cbStartReadSerialNumber = new System.Windows.Forms.CheckBox();
            this.ucConfigBackup = new QsfpPlus40gSr4SerialNumberWiter.UcConfigBackup();
            this.SuspendLayout();
            // 
            // cbI2cAdapterConnected
            // 
            this.cbI2cAdapterConnected.AutoSize = true;
            this.cbI2cAdapterConnected.Location = new System.Drawing.Point(12, 12);
            this.cbI2cAdapterConnected.Name = "cbI2cAdapterConnected";
            this.cbI2cAdapterConnected.Size = new System.Drawing.Size(90, 16);
            this.cbI2cAdapterConnected.TabIndex = 0;
            this.cbI2cAdapterConnected.Text = "I2C模組連結";
            this.cbI2cAdapterConnected.UseVisualStyleBackColor = true;
            this.cbI2cAdapterConnected.CheckedChanged += new System.EventHandler(this.cbI2cAdapterConnected_CheckedChanged);
            // 
            // cbStartReadSerialNumber
            // 
            this.cbStartReadSerialNumber.AutoSize = true;
            this.cbStartReadSerialNumber.Location = new System.Drawing.Point(108, 12);
            this.cbStartReadSerialNumber.Name = "cbStartReadSerialNumber";
            this.cbStartReadSerialNumber.Size = new System.Drawing.Size(72, 16);
            this.cbStartReadSerialNumber.TabIndex = 1;
            this.cbStartReadSerialNumber.Text = "讀取模組";
            this.cbStartReadSerialNumber.UseVisualStyleBackColor = true;
            this.cbStartReadSerialNumber.CheckedChanged += new System.EventHandler(this.cbStartReadSerialNumber_CheckedChanged);
            // 
            // ucConfigBackup
            // 
            this.ucConfigBackup.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ucConfigBackup.Location = new System.Drawing.Point(16, 37);
            this.ucConfigBackup.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.ucConfigBackup.Name = "ucConfigBackup";
            this.ucConfigBackup.Size = new System.Drawing.Size(719, 236);
            this.ucConfigBackup.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 282);
            this.Controls.Add(this.ucConfigBackup);
            this.Controls.Add(this.cbStartReadSerialNumber);
            this.Controls.Add(this.cbI2cAdapterConnected);
            this.Name = "Form1";
            this.Text = "QsfpPlus40gSr4ConfigBackup_20190401_1500";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbI2cAdapterConnected;
        private System.Windows.Forms.CheckBox cbStartReadSerialNumber;
        private QsfpPlus40gSr4SerialNumberWiter.UcConfigBackup ucConfigBackup;
    }
}

