using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rt145Rt146Config
{
    public partial class FRt145Rt146Config : Form
    {
        public FRt145Rt146Config()
        {
            InitializeComponent();
        }

        private void cbConnected_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

    public class ComboBoxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public override string ToString()
        {
            return Text;
        }

    }
}
