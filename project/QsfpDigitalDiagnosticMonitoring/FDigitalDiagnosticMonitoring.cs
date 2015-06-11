using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QsfpDigitalDiagnosticMonitoring
{
    public partial class FDigitalDiagnosticMonitoring : Form
    {
        public FDigitalDiagnosticMonitoring()
        {
            InitializeComponent();
            this.ClientSize = new System.Drawing.Size(this.ucDigitalDiagnosticsMonitoring1.Size.Width, this.ucDigitalDiagnosticsMonitoring1.Size.Height);
        }
    }
}
