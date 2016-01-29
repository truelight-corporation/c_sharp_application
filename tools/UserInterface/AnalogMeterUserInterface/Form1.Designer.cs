namespace AnalogMeterUserInterface
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
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.analogMeter = new AnalogMeterUserInterface.AnalogMeter();
            this.tbMaxValue = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(12, 128);
            this.trackBar.Maximum = 2000;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(200, 42);
            this.trackBar.TabIndex = 1;
            this.trackBar.TickFrequency = 100;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // analogMeter
            // 
            this.analogMeter.BackColor = System.Drawing.Color.Black;
            this.analogMeter.ForeColor = System.Drawing.Color.White;
            this.analogMeter.FrameColor = System.Drawing.Color.Black;
            this.analogMeter.FramePadding = new System.Windows.Forms.Padding(0);
            this.analogMeter.InternalPadding = new System.Windows.Forms.Padding(5);
            this.analogMeter.Location = new System.Drawing.Point(12, 12);
            this.analogMeter.MaxRange = 2000F;
            this.analogMeter.MaxThreshold = 1000F;
            this.analogMeter.MinRange = 0F;
            this.analogMeter.Name = "analogMeter";
            this.analogMeter.NormalBackColor = System.Drawing.Color.Black;
            this.analogMeter.NormalValueColor = System.Drawing.Color.ForestGreen;
            this.analogMeter.OverMaxThresholdBackColor = System.Drawing.Color.DarkBlue;
            this.analogMeter.OverMaxThresholdValueColor = System.Drawing.Color.Orange;
            this.analogMeter.PointSize = 50F;
            this.analogMeter.Size = new System.Drawing.Size(200, 110);
            this.analogMeter.TabIndex = 0;
            this.analogMeter.Text = "Text";
            this.analogMeter.TickLargeFrequency = 500F;
            this.analogMeter.TickSmallFrequency = 100F;
            this.analogMeter.TickStartAngle = 20F;
            this.analogMeter.TickTinyFrequency = 50F;
            this.analogMeter.Value = 0F;
            this.analogMeter.ValueUnit = "uW";
            // 
            // tbMaxValue
            // 
            this.tbMaxValue.Location = new System.Drawing.Point(12, 176);
            this.tbMaxValue.Name = "tbMaxValue";
            this.tbMaxValue.Size = new System.Drawing.Size(100, 22);
            this.tbMaxValue.TabIndex = 2;
            this.tbMaxValue.TextChanged += new System.EventHandler(this.tbMaxValue_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 258);
            this.Controls.Add(this.tbMaxValue);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.analogMeter);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AnalogMeter analogMeter;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.TextBox tbMaxValue;
    }
}

