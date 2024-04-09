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
            this.cbMeasuredObjectI2cAdapterConnected = new System.Windows.Forms.CheckBox();
            this.cbPowerMeterConnected = new System.Windows.Forms.CheckBox();
            this.tbPowerMeterIpAddr = new System.Windows.Forms.TextBox();
            this.lPowerMeterIpAddr = new System.Windows.Forms.Label();
            this.cbAutoMonitor = new System.Windows.Forms.CheckBox();
            this.cbPowerMeterSelect = new System.Windows.Forms.ComboBox();
            this.cbPowerMeterQsfpConnected = new System.Windows.Forms.CheckBox();
            this.ucQsfpPlus40gSr4DcTest = new QsfpPlus40gSr4DcTest.UcQsfpPlus40gSr4DcTest();
            this.SuspendLayout();
            // 
            // cbMeasuredObjectI2cAdapterConnected
            // 
            this.cbMeasuredObjectI2cAdapterConnected.AutoSize = true;
            this.cbMeasuredObjectI2cAdapterConnected.Location = new System.Drawing.Point(462, 13);
            this.cbMeasuredObjectI2cAdapterConnected.Name = "cbMeasuredObjectI2cAdapterConnected";
            this.cbMeasuredObjectI2cAdapterConnected.Size = new System.Drawing.Size(155, 16);
            this.cbMeasuredObjectI2cAdapterConnected.TabIndex = 0;
            this.cbMeasuredObjectI2cAdapterConnected.Text = "Be measured I2C connected";
            this.cbMeasuredObjectI2cAdapterConnected.UseVisualStyleBackColor = true;
            this.cbMeasuredObjectI2cAdapterConnected.CheckedChanged += new System.EventHandler(this.cbI2cAdapterConnected_CheckedChanged);
            // 
            // cbPowerMeterConnected
            // 
            this.cbPowerMeterConnected.AutoSize = true;
            this.cbPowerMeterConnected.BackColor = System.Drawing.SystemColors.Control;
            this.cbPowerMeterConnected.Location = new System.Drawing.Point(108, 13);
            this.cbPowerMeterConnected.Name = "cbPowerMeterConnected";
            this.cbPowerMeterConnected.Size = new System.Drawing.Size(114, 16);
            this.cbPowerMeterConnected.TabIndex = 1;
            this.cbPowerMeterConnected.Text = "EXFO power meter";
            this.cbPowerMeterConnected.UseVisualStyleBackColor = false;
            this.cbPowerMeterConnected.CheckedChanged += new System.EventHandler(this.cbPowerMeterConnected_CheckedChanged);
            // 
            // tbPowerMeterIpAddr
            // 
            this.tbPowerMeterIpAddr.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tbPowerMeterIpAddr.Location = new System.Drawing.Point(356, 11);
            this.tbPowerMeterIpAddr.Name = "tbPowerMeterIpAddr";
            this.tbPowerMeterIpAddr.Size = new System.Drawing.Size(100, 22);
            this.tbPowerMeterIpAddr.TabIndex = 3;
            this.tbPowerMeterIpAddr.Text = "172.16.102.50";
            // 
            // lPowerMeterIpAddr
            // 
            this.lPowerMeterIpAddr.AutoSize = true;
            this.lPowerMeterIpAddr.Location = new System.Drawing.Point(246, 14);
            this.lPowerMeterIpAddr.Name = "lPowerMeterIpAddr";
            this.lPowerMeterIpAddr.Size = new System.Drawing.Size(93, 12);
            this.lPowerMeterIpAddr.TabIndex = 4;
            this.lPowerMeterIpAddr.Text = "EXFO PM IP addr:";
            // 
            // cbAutoMonitor
            // 
            this.cbAutoMonitor.AutoSize = true;
            this.cbAutoMonitor.BackColor = System.Drawing.SystemColors.Control;
            this.cbAutoMonitor.Location = new System.Drawing.Point(1213, 13);
            this.cbAutoMonitor.Name = "cbAutoMonitor";
            this.cbAutoMonitor.Size = new System.Drawing.Size(81, 16);
            this.cbAutoMonitor.TabIndex = 5;
            this.cbAutoMonitor.Text = "Auto update";
            this.cbAutoMonitor.UseVisualStyleBackColor = false;
            this.cbAutoMonitor.CheckedChanged += new System.EventHandler(this.cbAutoMonitor_CheckedChanged);
            // 
            // cbPowerMeterSelect
            // 
            this.cbPowerMeterSelect.DropDownWidth = 90;
            this.cbPowerMeterSelect.FormattingEnabled = true;
            this.cbPowerMeterSelect.Items.AddRange(new object[] {
            "Power Meter",
            "QSFP"});
            this.cbPowerMeterSelect.Location = new System.Drawing.Point(12, 10);
            this.cbPowerMeterSelect.Name = "cbPowerMeterSelect";
            this.cbPowerMeterSelect.Size = new System.Drawing.Size(90, 20);
            this.cbPowerMeterSelect.TabIndex = 6;
            this.cbPowerMeterSelect.Text = "Power Meter";
            this.cbPowerMeterSelect.SelectedIndexChanged += new System.EventHandler(this.cbPowerMeterSelect_SelectedIndexChanged);
            // 
            // cbPowerMeterQsfpConnected
            // 
            this.cbPowerMeterQsfpConnected.AutoSize = true;
            this.cbPowerMeterQsfpConnected.Location = new System.Drawing.Point(108, 13);
            this.cbPowerMeterQsfpConnected.Name = "cbPowerMeterQsfpConnected";
            this.cbPowerMeterQsfpConnected.Size = new System.Drawing.Size(111, 16);
            this.cbPowerMeterQsfpConnected.TabIndex = 7;
            this.cbPowerMeterQsfpConnected.Text = "QSFP power meter";
            this.cbPowerMeterQsfpConnected.UseVisualStyleBackColor = true;
            this.cbPowerMeterQsfpConnected.Visible = false;
            this.cbPowerMeterQsfpConnected.CheckedChanged += new System.EventHandler(this.cbPowerMeterQsfpConnected_CheckedChanged);
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
            this.ClientSize = new System.Drawing.Size(1289, 729);
            this.Controls.Add(this.cbPowerMeterQsfpConnected);
            this.Controls.Add(this.cbPowerMeterSelect);
            this.Controls.Add(this.cbAutoMonitor);
            this.Controls.Add(this.cbMeasuredObjectI2cAdapterConnected);
            this.Controls.Add(this.cbPowerMeterConnected);
            this.Controls.Add(this.tbPowerMeterIpAddr);
            this.Controls.Add(this.lPowerMeterIpAddr);
            this.Controls.Add(this.ucQsfpPlus40gSr4DcTest);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1305, 768);
            this.MinimumSize = new System.Drawing.Size(1305, 766);
            this.Name = "MainForm";
            this.Text = "QSFP+ 40G SR4 DC Test 20240409_1148";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbMeasuredObjectI2cAdapterConnected;
        private System.Windows.Forms.CheckBox cbPowerMeterConnected;
        private UcQsfpPlus40gSr4DcTest ucQsfpPlus40gSr4DcTest;
        private System.Windows.Forms.TextBox tbPowerMeterIpAddr;
        private System.Windows.Forms.Label lPowerMeterIpAddr;
        private System.Windows.Forms.CheckBox cbAutoMonitor;
        private System.Windows.Forms.ComboBox cbPowerMeterSelect;
        private System.Windows.Forms.CheckBox cbPowerMeterQsfpConnected;
    }
}