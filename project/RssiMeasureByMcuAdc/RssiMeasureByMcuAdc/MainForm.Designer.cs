namespace RssiMeasureByMcuAdc
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
            this.cbLightSourceI2cConnected = new System.Windows.Forms.CheckBox();
            this.cbRssiMeasureI2cConnected = new System.Windows.Forms.CheckBox();
            this.cbAutoMonitor = new System.Windows.Forms.CheckBox();
            this.ucRssiMeasure = new RssiMeasureByMcuAdc.UcRssiMeasure();
            this.SuspendLayout();
            // 
            // cbLightSourceI2cConnected
            // 
            this.cbLightSourceI2cConnected.AutoSize = true;
            this.cbLightSourceI2cConnected.Location = new System.Drawing.Point(12, 12);
            this.cbLightSourceI2cConnected.Name = "cbLightSourceI2cConnected";
            this.cbLightSourceI2cConnected.Size = new System.Drawing.Size(102, 16);
            this.cbLightSourceI2cConnected.TabIndex = 0;
            this.cbLightSourceI2cConnected.Text = "光源I2C已連結";
            this.cbLightSourceI2cConnected.UseVisualStyleBackColor = true;
            this.cbLightSourceI2cConnected.CheckedChanged += new System.EventHandler(this.cbLightSourceI2cConnected_CheckedChanged);
            // 
            // cbRssiMeasureI2cConnected
            // 
            this.cbRssiMeasureI2cConnected.AutoSize = true;
            this.cbRssiMeasureI2cConnected.Location = new System.Drawing.Point(120, 12);
            this.cbRssiMeasureI2cConnected.Name = "cbRssiMeasureI2cConnected";
            this.cbRssiMeasureI2cConnected.Size = new System.Drawing.Size(90, 16);
            this.cbRssiMeasureI2cConnected.TabIndex = 1;
            this.cbRssiMeasureI2cConnected.Text = "RSSI測量I2C";
            this.cbRssiMeasureI2cConnected.UseVisualStyleBackColor = true;
            this.cbRssiMeasureI2cConnected.CheckedChanged += new System.EventHandler(this.cbRssiMeasureI2cConnected_CheckedChanged);
            // 
            // cbAutoMonitor
            // 
            this.cbAutoMonitor.AutoSize = true;
            this.cbAutoMonitor.Enabled = false;
            this.cbAutoMonitor.Location = new System.Drawing.Point(1042, 12);
            this.cbAutoMonitor.Name = "cbAutoMonitor";
            this.cbAutoMonitor.Size = new System.Drawing.Size(72, 16);
            this.cbAutoMonitor.TabIndex = 2;
            this.cbAutoMonitor.Text = "開始量測";
            this.cbAutoMonitor.UseVisualStyleBackColor = true;
            this.cbAutoMonitor.CheckedChanged += new System.EventHandler(this.cbAutoMonitor_CheckedChanged);
            // 
            // ucRssiMeasure
            // 
            this.ucRssiMeasure.Location = new System.Drawing.Point(12, 34);
            this.ucRssiMeasure.Name = "ucRssiMeasure";
            this.ucRssiMeasure.Size = new System.Drawing.Size(1113, 634);
            this.ucRssiMeasure.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 671);
            this.Controls.Add(this.ucRssiMeasure);
            this.Controls.Add(this.cbAutoMonitor);
            this.Controls.Add(this.cbRssiMeasureI2cConnected);
            this.Controls.Add(this.cbLightSourceI2cConnected);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "RSSI Measure 20190201";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbLightSourceI2cConnected;
        private System.Windows.Forms.CheckBox cbRssiMeasureI2cConnected;
        private System.Windows.Forms.CheckBox cbAutoMonitor;
        private UcRssiMeasure ucRssiMeasure;
    }
}

