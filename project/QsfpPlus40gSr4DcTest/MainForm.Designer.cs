namespace QsfpPlus40gSr4DcTest
{
    partial class MainForm
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
            this.cbPowerMeterConnected = new System.Windows.Forms.CheckBox();
            this.tbPowerMeterIpAddr = new System.Windows.Forms.TextBox();
            this.lPowerMeterIpAddr = new System.Windows.Forms.Label();
            this.cbAutoMonitor = new System.Windows.Forms.CheckBox();
            this.ucQsfpPlus40gSr4DcTest = new QsfpPlus40gSr4DcTest.UcQsfpPlus40gSr4DcTest();
            this.SuspendLayout();
            // 
            // cbI2cAdapterConnected
            // 
            this.cbI2cAdapterConnected.AutoSize = true;
            this.cbI2cAdapterConnected.Location = new System.Drawing.Point(366, 13);
            this.cbI2cAdapterConnected.Name = "cbI2cAdapterConnected";
            this.cbI2cAdapterConnected.Size = new System.Drawing.Size(114, 16);
            this.cbI2cAdapterConnected.TabIndex = 0;
            this.cbI2cAdapterConnected.Text = "I2C轉接器已連結";
            this.cbI2cAdapterConnected.UseVisualStyleBackColor = true;
            this.cbI2cAdapterConnected.CheckedChanged += new System.EventHandler(this.cbI2cAdapterConnected_CheckedChanged);
            // 
            // cbPowerMeterConnected
            // 
            this.cbPowerMeterConnected.AutoSize = true;
            this.cbPowerMeterConnected.BackColor = System.Drawing.SystemColors.Control;
            this.cbPowerMeterConnected.Location = new System.Drawing.Point(12, 12);
            this.cbPowerMeterConnected.Name = "cbPowerMeterConnected";
            this.cbPowerMeterConnected.Size = new System.Drawing.Size(132, 16);
            this.cbPowerMeterConnected.TabIndex = 1;
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
            this.tbPowerMeterIpAddr.TabIndex = 3;
            this.tbPowerMeterIpAddr.Text = "172.16.102.50";
            // 
            // lPowerMeterIpAddr
            // 
            this.lPowerMeterIpAddr.AutoSize = true;
            this.lPowerMeterIpAddr.Location = new System.Drawing.Point(150, 13);
            this.lPowerMeterIpAddr.Name = "lPowerMeterIpAddr";
            this.lPowerMeterIpAddr.Size = new System.Drawing.Size(104, 12);
            this.lPowerMeterIpAddr.TabIndex = 4;
            this.lPowerMeterIpAddr.Text = "光強度量測儀位址:";
            // 
            // cbAutoMonitor
            // 
            this.cbAutoMonitor.AutoSize = true;
            this.cbAutoMonitor.BackColor = System.Drawing.SystemColors.Control;
            this.cbAutoMonitor.Enabled = false;
            this.cbAutoMonitor.Location = new System.Drawing.Point(1213, 13);
            this.cbAutoMonitor.Name = "cbAutoMonitor";
            this.cbAutoMonitor.Size = new System.Drawing.Size(72, 16);
            this.cbAutoMonitor.TabIndex = 5;
            this.cbAutoMonitor.Text = "開始量測";
            this.cbAutoMonitor.UseVisualStyleBackColor = false;
            this.cbAutoMonitor.CheckedChanged += new System.EventHandler(this.cbAutoMonitor_CheckedChanged);
            // 
            // ucQsfpPlus40gSr4DcTest
            // 
            this.ucQsfpPlus40gSr4DcTest.Location = new System.Drawing.Point(12, 38);
            this.ucQsfpPlus40gSr4DcTest.Name = "ucQsfpPlus40gSr4DcTest";
            this.ucQsfpPlus40gSr4DcTest.Size = new System.Drawing.Size(1280, 698);
            this.ucQsfpPlus40gSr4DcTest.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1297, 741);
            this.Controls.Add(this.cbAutoMonitor);
            this.Controls.Add(this.cbI2cAdapterConnected);
            this.Controls.Add(this.cbPowerMeterConnected);
            this.Controls.Add(this.tbPowerMeterIpAddr);
            this.Controls.Add(this.lPowerMeterIpAddr);
            this.Controls.Add(this.ucQsfpPlus40gSr4DcTest);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1305, 768);
            this.MinimumSize = new System.Drawing.Size(1305, 766);
            this.Name = "MainForm";
            this.Text = "QSFP+ 40G SR4 DC Test 20171201";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbI2cAdapterConnected;
        private System.Windows.Forms.CheckBox cbPowerMeterConnected;
        private UcQsfpPlus40gSr4DcTest ucQsfpPlus40gSr4DcTest;
        private System.Windows.Forms.TextBox tbPowerMeterIpAddr;
        private System.Windows.Forms.Label lPowerMeterIpAddr;
        private System.Windows.Forms.CheckBox cbAutoMonitor;
    }
}