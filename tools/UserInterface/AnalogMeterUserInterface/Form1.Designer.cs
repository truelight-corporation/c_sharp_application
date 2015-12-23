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
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(12, 104);
            this.trackBar.Maximum = 150;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(172, 42);
            this.trackBar.TabIndex = 1;
            this.trackBar.TickFrequency = 10;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // analogMeter
            // 
            this.analogMeter.FrameColor = System.Drawing.Color.Black;
            this.analogMeter.FramePadding = new System.Windows.Forms.Padding(0);
            this.analogMeter.InternalPadding = new System.Windows.Forms.Padding(5);
            this.analogMeter.Location = new System.Drawing.Point(12, 12);
            this.analogMeter.MaxValue = 15F;
            this.analogMeter.MinValue = 0F;
            this.analogMeter.Name = "analogMeter";
            this.analogMeter.Size = new System.Drawing.Size(172, 86);
            this.analogMeter.TabIndex = 0;
            this.analogMeter.Text = "uW";
            this.analogMeter.TickStartAngle = 20F;
            this.analogMeter.Value = 0F;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(196, 143);
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
    }
}

