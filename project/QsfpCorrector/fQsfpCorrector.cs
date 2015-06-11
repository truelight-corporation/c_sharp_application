using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QsfpCorrector
{
    public partial class FQsfpCorrector : Form
    {
        public FQsfpCorrector()
        {
            InitializeComponent();
            this.ClientSize = new System.Drawing.Size(this.ucQsfpCorrector.Size.Width, this.ucQsfpCorrector.Size.Height);
        }
    }
}
