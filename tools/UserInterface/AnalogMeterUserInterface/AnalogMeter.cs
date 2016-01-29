using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Forms;

namespace AnalogMeterUserInterface
{
    public class AnalogMeter : Control
    {
        float r1x, r1y;
        Bitmap bgImage = null;
		Graphics realGraphics = null;
        Rectangle meterLocation;

        /// <summary>
        /// Title to display on the meter
        /// </summary>
        [Category("Meter"), Description("Title to display on the meter")]
        public override string Text
        {
            get {
                return base.Text;
            }
            set {
                base.Text = value;
                CreateBackground();
            }
        }

        private string valueUnit = "";

        /// <summary>
        /// Value unit to display on the meter
        /// </summary>
        [Category("Meter"), Description("Value unit to display on the meter")]
        public string ValueUnit
        {
            get {
                return this.valueUnit;
            }
            set {
                this.valueUnit = value;
            }
        }

        private float value = 0;

        /// <summary>
        /// Value of meter
        /// </summary>
        [DefaultValue(0), Category("Meter"), Description("Value of meter")]
        public float Value
        {
            get {
                return this.value;
            }
            set {
                if (this.value != value) {
                    this.value = value;
                    if (this.value > this.maxThreshold) {
                        if (this.BackColor != this.overMaxThresholdBackColor) {
                            this.BackColor = this.overMaxThresholdBackColor;
                            CreateBackground();
                        }
                    }
                    else {
                        if (this.BackColor != this.normalBackColor) {
                            this.BackColor = this.normalBackColor;
                            CreateBackground();
                        }
                    }
                    if (this.value > this.maxValue)
                        this.maxValue = this.value;
                    DrawMeter(realGraphics);
                }
            }
        }

        private float maxThreshold = 10;

        /// <summary>
        /// Maximum value threshold of meter
        /// </summary>
        [DefaultValue(10), Category("Meter"), Description("Maximum value threshold of meter")]
        public float MaxThreshold
        {
            get {
                return this.maxThreshold;
            }
            set {
                this.maxThreshold = value;
            }
        }

        private float maxValue = 0;

        /// <summary>
        /// Maximum value of meter
        /// </summary>
        [DefaultValue(0), Category("Meter"), Description("Maximum value of meter")]
        public float MaxValue
        {
            get {
                return this.maxValue;
            }
        }

        private float maxRange = 15;

        /// <summary>
        /// Maximum value of the meter
        /// </summary>
        [DefaultValue(15), Category("Meter"), Description("Maximum range of the meter")]
        public float MaxRange
        {
            get {
                return this.maxRange;
            }
            set {
                if (maxRange <= 0)
                    return;
                this.maxRange = value;
                CreateBackground();
            }
        }

        private float minRange = 0;

        /// <summary>
        /// Minimum value of the meter
        /// </summary>
        [DefaultValue(0), Category("Meter"), Description("Minimum range of the meter")]
        public float MinRange
        {
            get {
                return this.minRange;
            }
            set {
                this.minRange = value;
                CreateBackground();
            }
        }

        private float tickTinyFrequency = 0.2F;

        /// <summary>
        /// Frequency of tiny ticks (0 to disable)
        /// </summary>
        [DefaultValue(0.2F), Category("Meter"), Description("Frequency of tiny ticks (0 to disable)")]
        public float TickTinyFrequency
        {
            get {
                return tickTinyFrequency;
            }
            set {
                tickTinyFrequency = value;
                CreateBackground();
            }
        }

        private float tickSmallFrequency = 1;

        /// <summary>
        /// Frequency of small ticks (0 to disable)
        /// </summary>
        [DefaultValue(1F), Category("Meter"), Description("Frequency of small ticks (0 to disable)")]
        public float TickSmallFrequency
        {
            get {
                return tickSmallFrequency;
            }
            set {
                tickSmallFrequency = value;
                CreateBackground();
            }
        }

        private float tickLargeFrequency = 5F;

        /// <summary>
        /// Frequency of large ticks (0 to disable)
        /// </summary>
        [DefaultValue(5F), Category("Meter"), Description("Frequency of large ticks (0 to disable)")]
        public float TickLargeFrequency
        {
            get {
                return tickLargeFrequency;
            }
            set {
                tickLargeFrequency = value;
                CreateBackground();
            }
        }

        private float tickTinyWidth = 1F;
        [DefaultValue(1F), Category("Meter"), Description("Stroke width of tiny ticks")]
        public float TickTinyWidth
        {
            get {
                return tickTinyWidth;
            }
            set {
                tickTinyWidth = value;
                CreateBackground();
            }
        }

        private float tickSmallWidth = 1F;
        [DefaultValue(1F), Category("Meter"), Description("Stroke width of small ticks")]
        public float TickSmallWidth
        {
            get {
                return tickSmallWidth; 
            }
            set {
                tickSmallWidth = value;
                CreateBackground();
            }
        }

        private float tickLargeWidth = 2F;
        [DefaultValue(2F), Category("Meter"), Description("Stroke width of large ticks")]
        public float TickLargeWidth
        {
            get {
                return tickLargeWidth;
            }
            set {
                tickLargeWidth = value;
                CreateBackground();
            }
        }

        private float tickStartAngle = 20 * (float)(Math.PI / 180);

        [DefaultValue(20 * (float)(Math.PI / 180)), Category("Meter"), Description("Angle the meter starts display at in degrees")]
        public float TickStartAngle
        {
            get {
                return tickStartAngle * (float)(180 / Math.PI);
            }
            set {
                if (value < 0 || value > 75)
                    throw new Exception("The angle must be between a value of 0 and 75 degrees");
                tickStartAngle = value * (float)(Math.PI / 180);
                CreateBackground();
            }
        }

        private float tickTinySize = 5F;
        [DefaultValue(5F), Category("Meter")]
        public float TickTinySize
        {
            get {
                return tickTinySize;
            }
            set {
                tickTinySize = value;
                CreateBackground();
            }
        }

        private float tickSmallSize = 15F;

        [DefaultValue(15F), Category("Meter")]
        public float TickSmallSize
        {
            get {
                return tickSmallSize; 
            }
            set {
                tickSmallSize = value;
                CreateBackground();
            }
        }

        private float tickLargeSize = 20F;

        [DefaultValue(20F), Category("Meter")]
        public float TickLargeSize
        {
            get {
                return tickLargeSize;
            }
            set {
                tickLargeSize = value;
                CreateBackground();
            }
        }

        private float pointSize = 50F;
        [DefaultValue(60F), Category("Meter")]
        public float PointSize
        {
            get {
                return pointSize;
            }
            set {
                pointSize = value;
                CreateBackground();
            }
        }

        /// <summary>
        /// Background color of the meter
        /// </summary>
        [DefaultValue(typeof(Color), "White")]
        public override Color BackColor
        {
            get {
                return base.BackColor;
            }
            set {
                base.BackColor = value;
                CreateBackground();
            }
        }

        private Color normalBackColor = System.Drawing.Color.White;

        /// <summary>
        /// Normal background color of the meter
        /// </summary>
        [DefaultValue(typeof(Color), "White")]
        public Color NormalBackColor
        {
            get {
                return this.normalBackColor;
            }
            set {
                this.normalBackColor = value;
            }
        }

        private Color overMaxThresholdBackColor = Color.Green;

        /// <summary>
        /// Over max threshold background color of the meter
        /// </summary>
        [DefaultValue(typeof(Color), "Green")]
        public Color OverMaxThresholdBackColor
        {
            get {
                return this.overMaxThresholdBackColor;
            }
            set {
                this.overMaxThresholdBackColor = value;
            }
        }

        public override Font Font
        {
            get {
                return base.Font;
            }
            set {
                base.Font = value;
                CreateBackground();
            }
        }

        /// <summary>
        /// Color of tickmarks and text on meter
        /// </summary>
        public override Color ForeColor
        {
            get {
                return base.ForeColor;
            }
            set {
                base.ForeColor = value;
                CreateBackground();
            }
        }

        private Color normalValueColor = Color.ForestGreen;

        public Color NormalValueColor
        {
            get {
                return this.normalValueColor;
            }
            set {
                this.normalValueColor = value;
            }
        }

        private Color overMaxThresholdValueColor = Color.Red;

        public Color OverMaxThresholdValueColor
        {
            get {
                return this.overMaxThresholdValueColor;
            }
            set {
                this.overMaxThresholdValueColor = value;
            }
        }

        private Color pointerColor = Color.Red;

        [DefaultValue(typeof(Color), "Red"), Category("Meter"), Description("Color of the primary pointer")]
        public Color PointerColor
        {
            get {
                return pointerColor;
            }
            set {
                pointerColor = value;
                CreateBackground();
            }
        }

        private Color frameColor = Color.Black;

        [Category("Meter")]
        public Color FrameColor
        {
            get {
                return frameColor;
            }
            set {
                frameColor = value;
                CreateBackground();
            }
        }

        private Padding framePadding = new Padding(0);

        [Category("Meter")]
        public Padding FramePadding
        {
            get {
                return framePadding;
            }
            set {
                framePadding = value;
                CreateBackground();
            }
        }

        private Padding internalPadding = new Padding(5);

        [Category("Meter")]
        public Padding InternalPadding
        {
            get {
                return internalPadding; 
            }
            set {
                internalPadding = value;
                CreateBackground();
            }
        }

        private bool stretch = false;

        [DefaultValue(false), Category("Meter")]
        public bool Stretch
        {
            get {
                return stretch;
            }
            set {
                stretch = value;
                CreateBackground();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (bgImage != null)
                DrawMeter(e.Graphics);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.realGraphics != null)
                this.realGraphics.Dispose();
            this.realGraphics = this.CreateGraphics();
            CreateBackground();
        }

		public AnalogMeter() {
			BackColor = Color.White;
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
		}

        private PointF[] _GetLine(float value, float r1x, float r1y, float r2x, float r2y)
        {
            PointF[] p;
            float angle;

            p = new PointF[2];
            angle = value * (((float)Math.PI - tickStartAngle * 2) / (maxRange - minRange)) + tickStartAngle;

            // need to figure out where to calculate from
            p[0] = new PointF((float)(framePadding.Left + internalPadding.Left + (r1x - r1x * Math.Cos(angle))),
                (float)(framePadding.Top + internalPadding.Top + (r1y - r1y * Math.Sin(angle))));
            p[1] = new PointF((float)(framePadding.Left + internalPadding.Left + (r1x - r2x * Math.Cos(angle))),
                (float)(framePadding.Top + internalPadding.Top + (r1y - r2y * Math.Sin(angle))));

            return p;
        }

        private void DrawMeter(Graphics g)
        {
            Font fValue;
            float r2x, r2y;

            r2x = r1x - pointSize;
            r2y = r1y - pointSize;

            g.DrawImageUnscaled(bgImage, 0, 0);
            
            // draw value -- this can be optimized, draw this outside of this routine
            using (Pen p = new Pen(pointerColor, tickLargeWidth)) {
                PointF[] pts = _GetLine(value, r1x, r1y, r2x, r2y);
                g.SmoothingMode = SmoothingMode.HighQuality;
                
                g.DrawLine(p, pts[0], pts[1]);
            }

            if (value > maxThreshold) {
                using (Brush b = new SolidBrush(overMaxThresholdValueColor)) {
                    SizeF sz;

                    fValue = new Font(Font.Name, 14.0f, FontStyle.Bold);
                    sz = g.MeasureString(value.ToString() + " " + valueUnit, fValue);
                    g.DrawString(value.ToString() + " " + valueUnit, fValue, b,
                        framePadding.Left + (meterLocation.Width / 2) - sz.Width / 2,
                        framePadding.Top + (meterLocation.Height * 5) / 7 - sz.Height / 2);
                }
            }
            else {
                using (Brush b = new SolidBrush(normalValueColor)) {
                    SizeF sz;

                    fValue = new Font(Font.Name, 14.0f, FontStyle.Bold);
                    sz = g.MeasureString(value.ToString() + " " + valueUnit, fValue);
                    g.DrawString(value.ToString() + " " + valueUnit, fValue, b,
                        framePadding.Left + (meterLocation.Width / 2) - sz.Width / 2,
                        framePadding.Top + (meterLocation.Height * 5) / 7 - sz.Height / 2);
                }
            }

            using (Brush b = new SolidBrush(ForeColor)) {
                SizeF sz;

                sz = g.MeasureString("Max: " + maxValue.ToString() + " " + valueUnit, Font);
                g.DrawString("Max: " + maxValue.ToString() + " " + valueUnit, Font, b,
                    framePadding.Left + (meterLocation.Width / 2) - sz.Width / 2,
                    framePadding.Top + (meterLocation.Height * 10) / 11 - sz.Height / 2);
            }
        }

		/// <summary>
		/// This does all of the drawing, but it definitely could
		/// be optimized... the biggest one is that the value
		/// could be drawn seperately from this, since thats the value
		/// that would probably be updated the most.
		/// </summary>
		private void CreateBackground()
        {
            Bitmap bmp;
            Graphics g;
            
            float r2x, r2y;
			float i;
            int tmpI;

			if (Width < 1 || Height < 1)
				return;

            tmpI = 0;
            while (tmpI < maxRange)
                tmpI += 10;
            tickLargeFrequency = tmpI / 4;
            tickSmallFrequency = tickLargeFrequency / 5;
            tickTinyFrequency = tickSmallFrequency / 2;

			bmp = new Bitmap(Width, Height);
			g = Graphics.FromImage(bmp);
			g.SmoothingMode = SmoothingMode.HighQuality;

			// frame
			using (Brush b = new SolidBrush(frameColor)){
				g.FillRectangle(b, ClientRectangle);
			}

			// setup a clip rectangle for the meter itself
			if (stretch)
				meterLocation = new Rectangle(framePadding.Left,
                    framePadding.Top, Width - framePadding.Horizontal,
                    Height - framePadding.Vertical);
			else
				meterLocation = new Rectangle(framePadding.Left,
                    framePadding.Top, Width - framePadding.Horizontal,
                    internalPadding.Vertical + Width / 2 - framePadding.Top);

			// set the clip rectangle
			g.IntersectClip(meterLocation);

			// fill meter with its background
			using (Brush b = new SolidBrush(BackColor)){
				g.FillRectangle(b, meterLocation);
			}

			// 1 is outer point, 2 is inner point
			r1x = (float)( meterLocation.Width - internalPadding.Horizontal ) / 2;
			r1y = (float)( meterLocation.Height - internalPadding.Vertical );

			// draw tiny ticks
			if (tickTinyFrequency > 0) {
				using (Pen p = new Pen(ForeColor, tickTinyWidth)) {
					r2x = r1x - tickTinySize;
					r2y = r1y - tickTinySize;
					
					for (i = minRange; i <= maxRange; i += tickTinyFrequency) {
                        PointF[] pts;

						if ((tickSmallFrequency > 0 && (i - minRange) % tickSmallFrequency == 0) ||
                            (tickLargeFrequency > 0 && ( i - minRange ) % tickLargeFrequency == 0))
							continue;

						pts = _GetLine(i, r1x, r1y, r2x, r2y);
						g.DrawLine(p, pts[0], pts[1]);
					}
				}
			}

			// draw small ticks
			if (tickSmallFrequency > 0) {
				using (Pen p = new Pen(ForeColor, tickSmallWidth) ) {

					r2x = r1x - tickSmallSize;
					r2y = r1y - tickSmallSize;

					for (i = minRange; i <= maxRange; i += tickSmallFrequency) {
                        PointF[] pts;

						if ( tickLargeFrequency > 0 && (i - minRange) % tickLargeFrequency == 0 )
							continue;

						pts = _GetLine(i, r1x, r1y, r2x, r2y);
						g.DrawLine(p, pts[0], pts[1]);
					}
				}
			}

			// draw large ticks and numbers
			if (tickLargeFrequency > 0) {
				using (Pen p = new Pen(ForeColor, tickLargeWidth)) {
                    float r3x, r3y;

					r2x = r1x - tickLargeSize;
					r2y = r1y - tickLargeSize;

					r3x = r2x - Font.Height;
					r3y = r2y - Font.Height;

					for (i = minRange; i <= maxRange; i += tickLargeFrequency) {
						PointF[] pts;
                        SizeF sz;

                        pts = _GetLine(i, r1x, r1y, r2x, r2y);
						g.DrawLine(p, pts[0], pts[1]);

						sz = g.MeasureString(i.ToString(), Font);
						pts = _GetLine(i, r1x, r1y, r3x, r3y);
						g.DrawString(i.ToString(), Font, p.Brush, pts[1].X - sz.Width / 2,
                            pts[1].Y - sz.Height / 2);
					}
				}
			}

            // draw Text
            using (Brush b = new SolidBrush(ForeColor)) {
                SizeF sz;
                sz = g.MeasureString(Text, Font);
                g.DrawString(Text, Font, b,
                    framePadding.Left + (meterLocation.Width / 2) - sz.Width / 2,
                    framePadding.Top + (meterLocation.Height * 11) / 20 - sz.Height / 2);
            }

			g.Dispose();

			// done
			if (bgImage != null)
				bgImage.Dispose();

			bgImage = bmp;

			if (realGraphics != null)
				DrawMeter(realGraphics);
		}

        public void ClearMaxValueApi()
        {
            maxValue = 0;
            DrawMeter(realGraphics);
        }
    }
}
