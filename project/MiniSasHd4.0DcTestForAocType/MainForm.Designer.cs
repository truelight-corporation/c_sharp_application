namespace MiniSasHd4Dot0DcTest
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
            this.cbMeasuredObjectI2cAdapterConnectedA = new System.Windows.Forms.CheckBox();
            this.cbAutoMonitor = new System.Windows.Forms.CheckBox();
            this.cbPowerMeterQsfpConnected = new System.Windows.Forms.CheckBox();
            this.cbMeasuredObjectI2cAdapterConnectedB = new System.Windows.Forms.CheckBox();
            this.ucMiniSsaHd4Dot0DcTest = new MiniSasHd4Dot0DcTest.UcMiniSsaHd4Dot0DcTest();
            this.SuspendLayout();
            // 
            // cbMeasuredObjectI2cAdapterConnectedA
            // 
            this.cbMeasuredObjectI2cAdapterConnectedA.AutoSize = true;
            this.cbMeasuredObjectI2cAdapterConnectedA.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMeasuredObjectI2cAdapterConnectedA.Location = new System.Drawing.Point(20, 6);
            this.cbMeasuredObjectI2cAdapterConnectedA.Margin = new System.Windows.Forms.Padding(4);
            this.cbMeasuredObjectI2cAdapterConnectedA.Name = "cbMeasuredObjectI2cAdapterConnectedA";
            this.cbMeasuredObjectI2cAdapterConnectedA.Size = new System.Drawing.Size(220, 25);
            this.cbMeasuredObjectI2cAdapterConnectedA.TabIndex = 0;
            this.cbMeasuredObjectI2cAdapterConnectedA.Text = "I2C connected for Part-A";
            this.cbMeasuredObjectI2cAdapterConnectedA.UseVisualStyleBackColor = true;
            this.cbMeasuredObjectI2cAdapterConnectedA.CheckedChanged += new System.EventHandler(this.cbI2cAdapterConnectedA_CheckedChanged);
            // 
            // cbAutoMonitor
            // 
            this.cbAutoMonitor.AutoSize = true;
            this.cbAutoMonitor.BackColor = System.Drawing.SystemColors.Control;
            this.cbAutoMonitor.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoMonitor.Location = new System.Drawing.Point(565, 6);
            this.cbAutoMonitor.Margin = new System.Windows.Forms.Padding(4);
            this.cbAutoMonitor.Name = "cbAutoMonitor";
            this.cbAutoMonitor.Size = new System.Drawing.Size(121, 25);
            this.cbAutoMonitor.TabIndex = 5;
            this.cbAutoMonitor.Text = "Auto update";
            this.cbAutoMonitor.UseVisualStyleBackColor = false;
            this.cbAutoMonitor.CheckedChanged += new System.EventHandler(this.cbAutoMonitor_CheckedChanged);
            // 
            // cbPowerMeterQsfpConnected
            // 
            this.cbPowerMeterQsfpConnected.AutoSize = true;
            this.cbPowerMeterQsfpConnected.Location = new System.Drawing.Point(108, 12);
            this.cbPowerMeterQsfpConnected.Name = "cbPowerMeterQsfpConnected";
            this.cbPowerMeterQsfpConnected.Size = new System.Drawing.Size(111, 16);
            this.cbPowerMeterQsfpConnected.TabIndex = 7;
            this.cbPowerMeterQsfpConnected.Text = "QSFP power meter";
            this.cbPowerMeterQsfpConnected.UseVisualStyleBackColor = true;
            this.cbPowerMeterQsfpConnected.Visible = false;
            // 
            // cbMeasuredObjectI2cAdapterConnectedB
            // 
            this.cbMeasuredObjectI2cAdapterConnectedB.AutoSize = true;
            this.cbMeasuredObjectI2cAdapterConnectedB.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMeasuredObjectI2cAdapterConnectedB.Location = new System.Drawing.Point(248, 6);
            this.cbMeasuredObjectI2cAdapterConnectedB.Margin = new System.Windows.Forms.Padding(4);
            this.cbMeasuredObjectI2cAdapterConnectedB.Name = "cbMeasuredObjectI2cAdapterConnectedB";
            this.cbMeasuredObjectI2cAdapterConnectedB.Size = new System.Drawing.Size(219, 25);
            this.cbMeasuredObjectI2cAdapterConnectedB.TabIndex = 7;
            this.cbMeasuredObjectI2cAdapterConnectedB.Text = "I2C connected for Part-B";
            this.cbMeasuredObjectI2cAdapterConnectedB.UseVisualStyleBackColor = true;
            this.cbMeasuredObjectI2cAdapterConnectedB.CheckedChanged += new System.EventHandler(this.cbI2cAdapterConnectedB_CheckedChanged);
            // 
            // ucMiniSsaHd4Dot0DcTest
            // 
            this.ucMiniSsaHd4Dot0DcTest.Location = new System.Drawing.Point(2, 26);
            this.ucMiniSsaHd4Dot0DcTest.Margin = new System.Windows.Forms.Padding(4);
            this.ucMiniSsaHd4Dot0DcTest.Name = "ucMiniSsaHd4Dot0DcTest";
            this.ucMiniSsaHd4Dot0DcTest.Size = new System.Drawing.Size(1489, 886);
            this.ucMiniSsaHd4Dot0DcTest.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1504, 909);
            this.Controls.Add(this.ucMiniSsaHd4Dot0DcTest);
            this.Controls.Add(this.cbMeasuredObjectI2cAdapterConnectedB);
            this.Controls.Add(this.cbAutoMonitor);
            this.Controls.Add(this.cbMeasuredObjectI2cAdapterConnectedA);
            this.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1520, 950);
            this.MinimumSize = new System.Drawing.Size(1520, 948);
            this.Name = "MainForm";
            this.Text = "Mini SAS HD 4.0 DC Test 20220725";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbMeasuredObjectI2cAdapterConnectedA;        
        private System.Windows.Forms.CheckBox cbAutoMonitor;
        private System.Windows.Forms.CheckBox cbPowerMeterQsfpConnected;
        private System.Windows.Forms.CheckBox cbMeasuredObjectI2cAdapterConnectedB;
        private UcMiniSsaHd4Dot0DcTest ucMiniSsaHd4Dot0DcTest;
    }
}