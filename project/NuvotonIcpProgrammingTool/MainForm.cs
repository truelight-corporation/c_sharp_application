using I2cMasterInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NuvotonIcpTool
{
    public partial class MainForm : Form
    {
        //private int SequenceIndexA = 0;
        private UcNuvotonIcpTool ucNuvotonIcpTool;

        public MainForm()
        {
            InitializeComponent();
            CenterToScreen();
            this.Size = new System.Drawing.Size(770, 300);
            ucNuvotonIcpTool = new UcNuvotonIcpTool();
            ucNuvotonIcpTool.IcpChannelSetApi(0);
            _InitialComponents();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ucNuvotonIcpTool.FormClosing();
        }

        private void cbChannelSetToOne_CheckedChanged(object sender, EventArgs e)
        {
            if (cbChannelSet.Checked)
                ucNuvotonIcpTool.IcpChannelSetApi(1);
            else if (!cbChannelSet.Checked)
                ucNuvotonIcpTool.IcpChannelSetApi(0);
            
        }

        private void _InitialComponents()
        {
            ucNuvotonIcpTool.cbAutoReconnect.Enabled = true;
            ucNuvotonIcpTool.cbAutoReconnect.Visible = true;
            ucNuvotonIcpTool.cbBypassEraseAllCheck.Enabled = true;
            ucNuvotonIcpTool.cbBypassEraseAllCheck.Visible = true;
        }
    }
}
