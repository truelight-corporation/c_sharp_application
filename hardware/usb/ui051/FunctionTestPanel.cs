using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ui051
{
    public partial class FunctionTestPanel : UserControl
    {
        

        public FunctionTestPanel()
        {
            InitializeComponent();
        }

        private void bRead_Click(object sender, EventArgs e)
        {
            IntPtr hUsb2io = System.IntPtr.Zero;
            int iRv;
            byte[] bBuf = new byte[256];

            if (tbDevAddr.Text.Length == 0) {
                MessageBox.Show("Please set DevAddr!");
                return;
            }

            if ((System.Int32.Parse(tbDevAddr.Text) < 0) || (System.Int32.Parse(tbDevAddr.Text) > 127)) {
                MessageBox.Show("DevAddr: " + tbDevAddr.Text + " out of ranage (0 ~ 127) Error!!");
                return;
            }

            if (tbRegAddr.Text.Length == 0) {
                MessageBox.Show("Please set RegAddr!");
                return;
            }

            if ((System.Int32.Parse(tbRegAddr.Text) < 0) || (System.Int32.Parse(tbRegAddr.Text) > 255)) {
                MessageBox.Show("RegAddr: " + tbRegAddr.Text + " out of ranage (0 ~ 255) Error!!");
                return;
            }

            hUsb2io = ui051.usb2io_dll.Usb2ioOpen(1);
            if (hUsb2io == System.IntPtr.Zero) {
                MessageBox.Show("Open device fail!!\n" +
                    "Please check device connect");
                return;
            }

            iRv = ui051.usb2io_dll.Usb2ioEnableI2c(hUsb2io);
            if (iRv != 0) {
                MessageBox.Show("Usb2ioEnableI2c() fail: " + iRv + "!!");
                return;
            }

            iRv = ui051.usb2io_dll.Usb2ioSetI2cSpeed(hUsb2io, 1);
            if (iRv != 0) {
                MessageBox.Show("Usb2ioSetI2cSpeed() fail: " + iRv + "!!");
                return;
            }

            iRv = ui051.usb2io_dll.Usb2ioI2cRead(hUsb2io, System.Int32.Parse(tbDevAddr.Text), System.Int32.Parse(tbRegAddr.Text), 1, 1, ref bBuf[0]);
            if (iRv != 1) {
                MessageBox.Show("Usb2ioI2cRead() iRv: " + iRv);
            }
            tbValue.Text = Convert.ToInt32(bBuf[0]).ToString();

            iRv = ui051.usb2io_dll.Usb2ioClose(hUsb2io);
            if (iRv != 0) {
                MessageBox.Show("Usb2ioClose() fail: " + iRv + "!!");
                return;
            }
        }
    }
}
