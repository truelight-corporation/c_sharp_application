using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnalogMeterUserInterface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            analogMeter.Value = (float)trackBar.Value;
        }

        private void tbMaxValue_TextChanged(object sender, EventArgs e)
        {
            try {
                analogMeter.MaxRange = int.Parse(tbMaxValue.Text);
            }
            catch (Exception e1) { 
                
            }
        }
    }
}
