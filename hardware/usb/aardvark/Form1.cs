using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace aardvark
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new System.Drawing.Size(this.functionTestPanel1.Size.Width, this.functionTestPanel1.Size.Height);
        }
    }
}
