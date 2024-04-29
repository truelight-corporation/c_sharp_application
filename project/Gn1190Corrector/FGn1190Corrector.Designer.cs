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
            this.cbPowerMeterConnected = new System.Windows.Forms.CheckBox();
            this.tbPowerMeterIpAddr = new System.Windows.Forms.TextBox();
            this.lPowerMeterIpAddr = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbConnected
            // 
            this.cbConnected.AutoSize = true;
            this.cbConnected.Location = new System.Drawing.Point(622, 12);
            this.cbConnected.Name = "cbConnected";
            this.cbConnected.Size = new System.Drawing.Size(114, 16);
            this.cbConnected.TabIndex = 0;
            this.cbConnected.Text = "I2C轉接器已連結";
            this.cbConnected.UseVisualStyleBackColor = true;
            this.cbConnected.CheckedChanged += new System.EventHandler(this.cbConnected_CheckedChanged);
            // 
            // ucGn1190Corrector
            // 
            this.ucGn1190Corrector.BackColor = System.Drawing.SystemColors.Control;
            this.ucGn1190Corrector.Location = new System.Drawing.Point(5, 34);
            this.ucGn1190Corrector.Name = "ucGn1190Corrector";
            this.ucGn1190Corrector.Size = new System.Drawing.Size(1069, 941);
            this.ucGn1190Corrector.TabIndex = 10;
            // 
            // cbPowerMeterConnected
            // 
            this.cbPowerMeterConnected.AutoSize = true;
            this.cbPowerMeterConnected.BackColor = System.Drawing.SystemColors.Control;
            this.cbPowerMeterConnected.Location = new System.Drawing.Point(12, 12);
            this.cbPowerMeterConnected.Name = "cbPowerMeterConnected";
            this.cbPowerMeterConnected.Size = new System.Drawing.Size(132, 16);
            this.cbPowerMeterConnected.TabIndex = 11;
            this.cbPowerMeterConnected.Text = "光強度量測儀已連結";
            this.cbPowerMeterConnected.UseVisualStyleBackColor = false;
            this.cbPowerMeterConnected.CheckedChanged += new System.EventHandler(this.cbPowerMeterConnected_CheckedChanged);
            // 
            // tbPowerMeterIpAddr
            // 
            this.tbPowerMeterIpAddr.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbPowerMeterIpAddr.Location = new System.Drawing.Point(260, 10);
            this.tbPowerMeterIpAddr.Name = "tbPowerMeterIpAddr";
            this.tbPowerMeterIpAddr.Size = new System.Drawing.Size(100, 22);
            this.tbPowerMeterIpAddr.TabIndex = 12;
            this.tbPowerMeterIpAddr.Text = "172.16.102.50";
            // 
            // lPowerMeterIpAddr
            // 
            this.lPowerMeterIpAddr.AutoSize = true;
            this.lPowerMeterIpAddr.Location = new System.Drawing.Point(150, 13);
            this.lPowerMeterIpAddr.Name = "lPowerMeterIpAddr";
            this.lPowerMeterIpAddr.Size = new System.Drawing.Size(104, 12);
            this.lPowerMeterIpAddr.TabIndex = 13;
            this.lPowerMeterIpAddr.Text = "光強度量測儀位址:";
            // 
            // FGn1190Corrector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 750);
            this.Controls.Add(this.cbPowerMeterConnected);
            this.Controls.Add(this.tbPowerMeterIpAddr);
            this.Controls.Add(this.lPowerMeterIpAddr);
            this.Controls.Add(this.ucGn1190Corrector);
            this.Controls.Add(this.cbConnected);
            this.Name = "FGn1190Corrector";
            this.Text = "GN1190 Corrector 20240429_1027";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbConnected;
        private UcGn1190Corrector ucGn1190Corrector;
        private System.Windows.Forms.CheckBox cbPowerMeterConnected;
        private System.Windows.Forms.TextBox tbPowerMeterIpAddr;
        private System.Windows.Forms.Label lPowerMeterIpAddr;
    }
}

