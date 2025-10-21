using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace IntegratedGuiV2
{
    public partial class LoadingForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
            (
            int nLeftRect,
            int nTopRect,
            int RightRect,
            int BottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );

        public LoadingForm()
        {
            InitializeComponent();
            //this.TransparencyKey = BackColor;
            //this.Opacity = 0.5;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        public LoadingForm(Form parent)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            if (parent != null)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(parent.Location.X + parent.Width / 2 - this.Width / 2,
                    //parent.Location.Y + parent.Height / 2 + this.Height);
                    parent.Location.Y + this.Height );
            }
            else
                this.StartPosition = FormStartPosition.CenterParent;
        }

        internal void OnPluginWattingState()
        {
            BackColor = Color.HotPink;
            label1.Text = "Re-plug DUT\n重新插拔產品!";
            label1.Location = new System.Drawing.Point(65, 12);
            label1.ForeColor = Color.Black;
            this.Refresh();
        }

        internal void InitialState()
        {
            BackColor = Color.White;
            label1.Location = new System.Drawing.Point(65, 12);
            label1.Text = "Please wait...\n執行中...";
            label1.ForeColor = Color.Black;
            this.Refresh();
        }

        public void CloseWaitForm()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
            if (label1.Image != null)
            {
                label1.Image.Dispose();
            }
        }
    }
}
