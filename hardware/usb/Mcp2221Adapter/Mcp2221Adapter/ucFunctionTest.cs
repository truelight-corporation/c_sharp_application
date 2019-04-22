using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Mcp2221Adapter
{
    public partial class ucFunctionTest : UserControl
    {
        
        

        private IntPtr mcp2221Adapter = IntPtr.Zero;
        private DataTable dtValue = new DataTable();

        public ucFunctionTest()
        {
            InitializeComponent();
            dtValue.Columns.Add("Addr", typeof(Byte));
            dtValue.Columns.Add("Value", typeof(Byte));
            dgvMultiRegister.DataSource = dtValue;
            dgvMultiRegister.Columns["Addr"].ReadOnly = true;
        }

        private int _ConnectDevice()
        {
            uint index, bitRate;
            var sbVersion = new StringBuilder(10);
            string version;
            int result;
            ushort[] deviceIndex = new ushort[16];
            string[] deviceSerialNumber = new string[16];

            version = Mcp2221Api.Mcp2221GetLibraryVersionApi();
            if (!version.Equals("2.2b")) {
                MessageBox.Show("MCP2221 library: " + sbVersion.ToString() + " != 2.2b !!");
                goto error;
            }

            Mcp2221Api.Mcp2221FindDevicesExtApi(16, deviceIndex, 16, deviceSerialNumber);

            if (mcp2221Adapter != IntPtr.Zero)
                return 0;

            if (Mcp2221Api.Mcp2221GetConnectedDevicesApi() < 0)
                goto error;

            uint.TryParse(tbBitrate.Text, out bitRate);
            bitRate = bitRate * 1000;

            if ((bitRate < 46875) || (bitRate > 500000)) {
                MessageBox.Show("bitRate out of range: " + bitRate);
                goto error;
            }

            uint.TryParse(tbPort.Text, out index);

            mcp2221Adapter = Mcp2221Api.Mcp2221OpenByIndexApi(index);

            if (mcp2221Adapter == IntPtr.Zero)
                goto error;

            result = Mcp2221Api.Mcp2221SetBitrateApi(mcp2221Adapter, bitRate);
            if (result < 0) {
                MessageBox.Show("Mcp2221_SetSpeed() fail: " + result);
                goto error;
            }

            return 0;

        error:
            return -1;
        }

        private int _DisconnectDevice()
        {
            if (mcp2221Adapter == IntPtr.Zero)
                return 0;

            Mcp2221Api.Mcp2221CloseApi(mcp2221Adapter);
            mcp2221Adapter = IntPtr.Zero;

            return 0;
        }

        private int _CheckSignalReadInput()
        {
            int devAddr, regAddr;

            if (tbDevAddr.Text.Length == 0) {
                MessageBox.Show("Please input DevAddr!!");
                return -1;
            }

            devAddr = Convert.ToInt32(tbDevAddr.Text);
            if ((devAddr < 0) || (devAddr > 127)) {
                MessageBox.Show("devAddr out of range (0 ~ 127)!!");
                return -1;
            }

            if (tbRegAddr.Text.Length == 0) {
                MessageBox.Show("Please input RegAddr!!");
                return -1;
            }

            regAddr = Convert.ToInt32(tbRegAddr.Text);
            if ((regAddr < 0) || (regAddr > 255)) {
                MessageBox.Show("regAddr out of range (0 ~ 255)!!");
                return -1;
            }

            return 0;
        }

        private int _SignalRead()
        {
            int result;
            byte[] data = new byte[1];
            byte devAddr, regAddr;


            if (mcp2221Adapter == IntPtr.Zero)
                return 0;

            if (_CheckSignalReadInput() < 0)
                return -1;

            devAddr = Convert.ToByte(Convert.ToInt32(tbDevAddr.Text));
            regAddr = Convert.ToByte(Convert.ToInt32(tbRegAddr.Text));
            data[0] = regAddr;
            
            result = Mcp2221Api.Mcp2221I2cWriteApi(mcp2221Adapter, 1, devAddr, 1, data);
            if (result < 0) {
                MessageBox.Show("Mcp2221I2cWriteApi() fail: " + result);
                return -1;
            }

            result = Mcp2221Api.Mcp2221I2cReadApi(mcp2221Adapter, 1, devAddr, 1, data);
            if (result < 0) {
                MessageBox.Show("Mcp2221I2cReadApi() fail: " + result);
                return -1;
            }

            tbValue.Text = Convert.ToString(data[0]);

            return 0;
        }

        private void bSignalRead_Click(object sender, EventArgs e)
        {
            if (cbConnectState.Checked != true) {
                if (_ConnectDevice() < 0)
                    return;

                cbConnectState.Checked = true;
            }

            if (_SignalRead() < 0)
                return;
        }

        private void cbConnectState_CheckedChanged(object sender, EventArgs e)
        {
            if (cbConnectState.Checked == true) {
                if (_ConnectDevice() < 0)
                    goto clear;
                tbPort.Enabled = false;
                tbBitrate.Enabled = false;
            }
            else {
                _DisconnectDevice();
                tbPort.Enabled = true;
                tbBitrate.Enabled = true;
            }

            return;

        clear:
            cbConnectState.Checked = false;
        }

        private int _CheckSignalWriteInput()
        {
            int data;

            if (_CheckSignalReadInput() < 0)
                return -1;

            if (tbDevAddr.Text.Length == 0) {
                MessageBox.Show("Please input DevAddr!!");
                return -1;
            }

            data = Convert.ToInt32(tbValue.Text);
            if ((data < 0) || (data > 255)) {
                MessageBox.Show("Value out of range (0 ~ 255)!!");
                return -1;
            }

            return 0;
        }

        private void bSignalWrite_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[2];
            int result;
            byte devAddr;
            
            if (cbConnectState.Checked != true) {
                if (_ConnectDevice() < 0)
                    return;
                cbConnectState.Checked = true;
            }

            if (_CheckSignalWriteInput() < 0)
                return;

            devAddr = Convert.ToByte(Convert.ToInt32(tbDevAddr.Text));
            data[0] = Convert.ToByte(Convert.ToInt32(tbRegAddr.Text));
            data[1] = Convert.ToByte(Convert.ToInt32(tbValue.Text));

            result = Mcp2221Api.Mcp2221I2cWriteApi(mcp2221Adapter, 2, devAddr, 1, data);
            if (result < 0) {
                MessageBox.Show("Mcp2221I2cWriteApi() fail: " + result);
                return;
            }
        }

        private int _CheckMultiReadInput()
        {
            int regAddr, length;

            if (_CheckSignalReadInput() < 0)
                return -1;

            if (tbLength.Text.Length == 0) {
                MessageBox.Show("Please input length!!");
                return -1;
            }
            regAddr = Convert.ToInt32(tbRegAddr.Text);
            length = Convert.ToInt32(tbLength.Text);
            if ((length + regAddr) > 255) {
                MessageBox.Show("Read out of range (0 ~ 255)!!");
                return -1;
            }

            return 0;
        }

        private int _MultiRead()
        {
            byte[] buf = new byte[256];
            byte[] regAddr = new byte[1];
            int result;
            byte devAddr, length;

            dtValue.Clear();

            if (_CheckMultiReadInput() < 0)
                return -1;

            devAddr = Convert.ToByte(Convert.ToInt32(tbDevAddr.Text));
            regAddr[0] = Convert.ToByte(Convert.ToInt32(tbRegAddr.Text));
            length = Convert.ToByte(Convert.ToInt32(tbLength.Text));

            if (length > 128) {
                MessageBox.Show("Length out of range: " + length);
                return -1;
            }

            result = Mcp2221Api.Mcp2221I2cWriteApi(mcp2221Adapter, 1, devAddr, 1, regAddr);
            if (result < 0) {
                MessageBox.Show("Mcp2221I2cWriteApi() fail: " + result);
                return -1;
            }

            result = Mcp2221Api.Mcp2221I2cReadApi(mcp2221Adapter, length, devAddr, 1, buf);
            if (result < 0) {
                MessageBox.Show("Mcp2221I2cReadApi() fail: " + result);
                return -1;
            }

            for (int i = 0; i < length; i++)
                dtValue.Rows.Add(regAddr[0] + i, buf[i]);

            return 0;
        }

        private void bMultiRead_Click(object sender, EventArgs e)
        {
            if (cbConnectState.Checked != true) {
                if (_ConnectDevice() < 0)
                    return;

                cbConnectState.Checked = true;
            }

            if (_MultiRead() < 0)
                return;
        }

        private int _MultiWrite()
        {
            byte[] data = new byte[256];
            int index = 0, result;
            byte devAddr;

            if (_CheckMultiReadInput() < 0)
                return -1;

            devAddr = Convert.ToByte(Convert.ToInt32(tbDevAddr.Text));

            index = 0;
            foreach (DataRow row in dtValue.Rows) {
                index = dtValue.Rows.IndexOf(row);
                if (index == 0)
                    data[0] = Convert.ToByte(row.ItemArray[0]);
                data[index + 1] = Convert.ToByte(row.ItemArray[1]);
            }

            result = Mcp2221Api.Mcp2221I2cWriteApi(mcp2221Adapter, 2, devAddr, 1, data);
            if (result < 0) {
                MessageBox.Show("Mcp2221I2cWriteApi() fail: " + result);
                return -1;
            }

            return 0;
        }

        private void bWrite_Click(object sender, EventArgs e)
        {
            if (cbConnectState.Checked != true)
            {
                if (_ConnectDevice() < 0)
                    return;

                cbConnectState.Checked = true;
            }

            if (_MultiWrite() < 0)
                return;
        }
    }
}
