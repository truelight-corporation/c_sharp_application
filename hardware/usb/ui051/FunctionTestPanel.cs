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
        private IntPtr hUsb2io = System.IntPtr.Zero;
        private DataTable dtValue = new DataTable();

        public FunctionTestPanel()
        {
            InitializeComponent();
            dtValue.Columns.Add("Addr", typeof(Byte));
            dtValue.Columns.Add("Value", typeof(Byte));
            dgvMultiRegister.DataSource = dtValue;
        }

        private int _DisconnectDevice()
        {
            int iRv;

            if (hUsb2io.ToInt32() > 0) {
                iRv = ui051.usb2io_dll.Usb2ioClose(hUsb2io);
                if (iRv != 0) {
                    MessageBox.Show("Usb2ioClose() fail: " + iRv + "!!");
                    return -1;
                }

                hUsb2io = IntPtr.Zero;
            }

            return 0;
        }

        ~FunctionTestPanel()
        {
            if (_DisconnectDevice() < 0)
                MessageBox.Show("_DisconnectDevice() fail!!");
        }

        private int _ConnectDevice()
        {
            int iRv;

            if (hUsb2io.ToInt32() > 0)
                return 1;

            hUsb2io = ui051.usb2io_dll.Usb2ioOpen(1);
            if (hUsb2io.ToInt32() < 0)
            {
                MessageBox.Show("Open device fail!!\n" +
                    "Please check device connect");
                return -1;
            }

            iRv = ui051.usb2io_dll.Usb2ioEnableI2c(hUsb2io);
            if (iRv != 0)
            {
                MessageBox.Show("Usb2ioEnableI2c() fail: " + iRv + "!!");
                return -1;
            }

            iRv = ui051.usb2io_dll.Usb2ioSetI2cSpeed(hUsb2io, 0);
            if (iRv != 0)
            {
                MessageBox.Show("Usb2ioSetI2cSpeed() fail: " + iRv + "!!");
                return -1;
            }

            return 0;
        }

        private int _CheckSignalReadInput()
        {
            if (tbDevAddr.Text.Length == 0)
            {
                MessageBox.Show("Please set DevAddr!");
                return -1;
            }

            if ((System.Int32.Parse(tbDevAddr.Text) < 0) || (System.Int32.Parse(tbDevAddr.Text) > 127))
            {
                MessageBox.Show("DevAddr: " + tbDevAddr.Text + " out of ranage (0 ~ 127) Error!!");
                return -1;
            }

            if (tbRegAddr.Text.Length == 0)
            {
                MessageBox.Show("Please set RegAddr!");
                return -1;
            }

            if ((System.Int32.Parse(tbRegAddr.Text) < 0) || (System.Int32.Parse(tbRegAddr.Text) > 255))
            {
                MessageBox.Show("RegAddr: " + tbRegAddr.Text + " out of ranage (0 ~ 255) Error!!");
                return -1;
            }

            return 0;
        }

        private void _bSingleReadClick(object sender, EventArgs e)
        {
            int iRv;
            byte[] bBuf = new byte[256];

            tbValue.Text = "";
            if (cbConnected.Checked == false) {
                if (_ConnectDevice() < 0)
                    return;
                cbConnected.Checked = true;
            }

            if (_CheckSignalReadInput() < 0)
                return;

            iRv = ui051.usb2io_dll.Usb2ioI2cRead(hUsb2io, System.Int32.Parse(tbDevAddr.Text), System.Int32.Parse(tbRegAddr.Text), 1, 1, ref bBuf[0]);
            if (iRv != 0) {
                MessageBox.Show("Usb2ioI2cRead() iRv: " + iRv);
                if (_DisconnectDevice() < 0)
                    return;
                cbConnected.Checked = false;
            }
            else
                tbValue.Text = bBuf[0].ToString();
        }

        private int _CheckWriteInput()
        {
            if (_CheckSignalReadInput() < 0)
                return -1;

            if (tbValue.Text.Length == 0) {
                MessageBox.Show("Please input value!!");
                return -1;
            }

            return 0;
        }

        private void _bSignalWriteClick(object sender, EventArgs e)
        {
            int iRv;
            byte[] bBuf = new byte[256];

            if (cbConnected.Checked == false) {
                if (_ConnectDevice() < 0)
                    return;
                cbConnected.Checked = true;
            }

            if (_CheckWriteInput() < 0)
                return;

            bBuf[0] = Convert.ToByte(tbValue.Text);

            iRv = ui051.usb2io_dll.Usb2ioI2cWrite(hUsb2io, System.Int32.Parse(tbDevAddr.Text), System.Int32.Parse(tbRegAddr.Text), 1, 1, ref bBuf[0]);
            if (iRv != 0) {
                MessageBox.Show("Usb2ioI2cRead() iRv: " + iRv);
                if (_DisconnectDevice() < 0)
                    return;
                cbConnected.Checked = false;
            }
        }

        private void _cbConnectedCheckedChanged(object sender, EventArgs e)
        {
            if (cbConnected.Checked) {
                if (_ConnectDevice() < 0) {
                    MessageBox.Show("_ConnectDevice() fail");
                    cbConnected.Checked = false;
                    return;
                }
            }
            else {
                if (_DisconnectDevice() < 0) {
                    MessageBox.Show("_DisconnectDevice() fail!!");
                    return;
                }
            }
        }

        private int _CheckMultiReadInput()
        {
            if (_CheckSignalReadInput() < 0)
                return -1;

            if (tbLength.Text.Length == 0) {
                MessageBox.Show("Please input length!!");
                return -1;
            }

            if ((Convert.ToInt32(tbLength.Text) + Convert.ToInt32(tbRegAddr.Text)) > 255) {
                MessageBox.Show("Read out of range (0 ~ 255)!!");
                return -1;
            }

            return 0;
        }

        private void _bMultiReadClick(object sender, EventArgs e)
        {
            int iRv;
            Byte[] bBuf = new Byte[256];

            dtValue.Clear();
            if (cbConnected.Checked == false)
            {
                if (_ConnectDevice() < 0)
                    return;
                cbConnected.Checked = true;
            }

            if (_CheckMultiReadInput() < 0)
                return;

            iRv = ui051.usb2io_dll.Usb2ioI2cRead(hUsb2io, Convert.ToInt32(tbDevAddr.Text), Convert.ToInt32(tbRegAddr.Text), 1, Convert.ToInt32(tbLength.Text), ref bBuf[0]);
            if (iRv != 0) {
                MessageBox.Show("Usb2ioI2cRead() iRv: " + iRv);
                if (_DisconnectDevice() < 0)
                    return;
                cbConnected.Checked = false;
                return;
            }

            for (int i = 0; i < Convert.ToInt32(tbLength.Text); i++)
                dtValue.Rows.Add(Convert.ToByte(tbRegAddr.Text) + i, bBuf[i]);
            
            if (Convert.ToInt32(tbLength.Text) == 128)
                dgvMultiRegister.AllowUserToAddRows = false;
            else
                dgvMultiRegister.AllowUserToAddRows = true;
        }

        private void _bMultiWriteClick(object sender, EventArgs e)
        {

        }

        private void _dgvMultiRegisterUserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (dgvMultiRegister.RowCount == 129) {
                dgvMultiRegister.AllowUserToAddRows = false;
                tbLength.Text = Convert.ToString(dgvMultiRegister.RowCount);
            }
            else
                tbLength.Text = Convert.ToString(dgvMultiRegister.RowCount - 1);
        }

        private void _dgvMultiRegisterUserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            dgvMultiRegister.AllowUserToAddRows = true;
            tbLength.Text = Convert.ToString(dgvMultiRegister.RowCount - 1);
        }
    }
}
