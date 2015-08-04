using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AardvarkAdapter
{
    public partial class FunctionTestPanel : UserControl
    {
        private int iHandle = 0;
        private DataTable dtValue = new DataTable();

        public FunctionTestPanel()
        {
            InitializeComponent();
            dtValue.Columns.Add("Addr", typeof(Byte));
            dtValue.Columns.Add("Value", typeof(Byte));
            dgvMultiRegister.DataSource = dtValue;
            dgvMultiRegister.Columns["Addr"].ReadOnly = true;
        }

        private int _DisconnectDevice()
        {
            if (iHandle == 0)
                return 0;

            AardvarkApi.aa_close(iHandle);

            iHandle = 0;
            tbPort.ReadOnly = false;
            tbBitrate.ReadOnly = false;
            cbConnectState.Checked = false;

            return 0;
        }

        ~FunctionTestPanel()
        {
            if (_DisconnectDevice() < 0)
                MessageBox.Show("_DisconnectDevice() fail!!");
        }

        private int _ConnectDevice()
        {
            int port, bitrate;

            if (iHandle > 0)
                return 1;

            port = Convert.ToInt32(tbPort.Text);
            if ((port <= 0) && (port > 10)) {
                MessageBox.Show("Port out of range (1 ~ 10)!!");
                goto Error;
            }

            bitrate = Convert.ToInt32(tbBitrate.Text);
            if ((bitrate < 10) || (bitrate > 400)) {
                MessageBox.Show("Bitrate out of range (10 ~ 400)!!");
                goto Error;
            }

            // Open the device
            iHandle = AardvarkApi.aa_open(port);
            if (iHandle <= 0) {
                MessageBox.Show("Please check I2C adapter connect!!");
                goto Error;
            }

            // Ensure that the I2C subsystem is enabled
            AardvarkApi.aa_configure(iHandle, AardvarkConfig.AA_CONFIG_SPI_I2C);

            // Enable the I2C bus pullup resistors (2.2k resistors).
            // This command is only effective on v2.0 hardware or greater.
            // The pullup resistors on the v1.02 hardware are enabled by default.
            AardvarkApi.aa_i2c_pullup(iHandle, AardvarkApi.AA_I2C_PULLUP_BOTH);

            // Set the bitrate
            bitrate = AardvarkApi.aa_i2c_bitrate(iHandle, bitrate);

            tbPort.ReadOnly = true;
            tbBitrate.ReadOnly = true;
            cbConnectState.Checked = true;
            
            return 0;

        Error:
            if (_DisconnectDevice() < 0)
                MessageBox.Show("_DisconnectDevice() fail!!");
            
            return -1;

        }

        private void _cbConnectStateCheckedChanged(object sender, EventArgs e)
        {
            if (cbConnectState.Checked == true) {
                if (_ConnectDevice() < 0) {
                    MessageBox.Show("_ConnectDevice() fail!!");
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
            byte[] regAddr = new byte[1];
            byte[] data = new byte[1];
            int counter;
            byte devAddr;

            if (_CheckSignalReadInput() < 0)
                return -1;

            devAddr = Convert.ToByte(Convert.ToInt32(tbDevAddr.Text));
            regAddr[0] = Convert.ToByte(Convert.ToInt32(tbRegAddr.Text));

            AardvarkApi.aa_i2c_write(iHandle, devAddr, AardvarkI2cFlags.AA_I2C_NO_STOP, 1, regAddr);

            counter = AardvarkApi.aa_i2c_read(iHandle, devAddr, AardvarkI2cFlags.AA_I2C_NO_FLAGS, 1, data);
            if (counter < 0) {
                MessageBox.Show("AardvarkApi.aa_i2c_read() fail: " + counter);
                return -1;
            }
            else if (counter == 0) {
                MessageBox.Show("Read 0 byte.\n" +
                    "Please check devAddr!!");
                return -1;
            }

            tbValue.Text = Convert.ToString(data[0]);

            return 0;
        }

        private void bSignalRead_Click(object sender, EventArgs e)
        {
            if (_ConnectDevice() < 0)
                return;

            if (_SignalRead() < 0)
                return;
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
            byte devAddr;

            if (_ConnectDevice() < 0)
                return;

            if (_CheckSignalWriteInput() < 0)
                return;

            devAddr = Convert.ToByte(Convert.ToInt32(tbDevAddr.Text));
            data[0] = Convert.ToByte(Convert.ToInt32(tbRegAddr.Text));
            data[1] = Convert.ToByte(Convert.ToInt32(tbValue.Text));

            AardvarkApi.aa_i2c_write(iHandle, devAddr, AardvarkI2cFlags.AA_I2C_NO_FLAGS, 2, data);
            AardvarkApi.aa_sleep_ms(10);
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

        private void _bMultiReadClick(object sender, EventArgs e)
        {
            byte[] buf = new byte[256];
            int counter;
            byte[] regAddr = new byte[1];
            byte devAddr, length;

            dtValue.Clear();

            if (_ConnectDevice() < 0)
                return;

            if (_CheckMultiReadInput() < 0)
                return;
            devAddr = Convert.ToByte(Convert.ToInt32(tbDevAddr.Text));
            regAddr[0] = Convert.ToByte(Convert.ToInt32(tbRegAddr.Text));
            length = Convert.ToByte(Convert.ToInt32(tbLength.Text));

            AardvarkApi.aa_i2c_write(iHandle, devAddr, AardvarkI2cFlags.AA_I2C_NO_STOP, 1, regAddr);

            counter = AardvarkApi.aa_i2c_read(iHandle, devAddr, AardvarkI2cFlags.AA_I2C_NO_FLAGS, length, buf);
            if (counter < 0) {
                MessageBox.Show("AardvarkApi.aa_i2c_read() fail: " + counter);
                return;
            }
            else if (counter == 0) {
                MessageBox.Show("Read 0 byte.\n" +
                    "Please check devAddr!!");
                return;
            }

            for (int i = 0; i < length; i++)
                dtValue.Rows.Add(regAddr[0] + i, buf[i]);
        }

        private void bWrite_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[256];
            int index;
            byte devAddr, length;

            if (_ConnectDevice() < 0)
                return;

            if (_CheckMultiReadInput() < 0)
                return;

            devAddr = Convert.ToByte(Convert.ToInt32(tbDevAddr.Text));
            length = Convert.ToByte(Convert.ToInt32(tbLength.Text) + 1);
            
            foreach (DataRow row in dtValue.Rows) {
                index = dtValue.Rows.IndexOf(row);
                if (index == 0)
                    data[0] = Convert.ToByte(row.ItemArray[0]);
                data[index + 1] = Convert.ToByte(row.ItemArray[1]);
            }
            AardvarkApi.aa_i2c_write(iHandle, devAddr, AardvarkI2cFlags.AA_I2C_NO_FLAGS, length, data);
            AardvarkApi.aa_sleep_ms(10);
        }

        private int _WaitTriggerAndDelay()
        {
            int port, bitrate, delay;
            byte bTmp;

            AardvarkApi.AardvarkExt aaExt = new AardvarkApi.AardvarkExt();
            
            port = Convert.ToInt32(tbPort.Text);
            if ((port <= 0) && (port > 10)) {
                MessageBox.Show("Port out of range (1 ~ 10)!!");
                return -1;
            }

            bitrate = Convert.ToInt32(tbBitrate.Text);
            if ((bitrate < 10) || (bitrate > 400)) {
                MessageBox.Show("Bitrate out of range (10 ~ 400)!!");
                return -1;
            }

            delay = Convert.ToInt32(tbTriggerDelay.Text);

            iHandle = AardvarkApi.aa_open_ext(0, ref aaExt);

            if (iHandle <= 0) {
                MessageBox.Show("Unable to open Aardvark device on port 0");
                return -1;
            }

            AardvarkApi.aa_configure(iHandle, AardvarkConfig.AA_CONFIG_GPIO_ONLY);

            AardvarkApi.aa_i2c_pullup(iHandle, AardvarkApi.AA_I2C_PULLUP_NONE);

            AardvarkApi.aa_gpio_set(iHandle, 0x00);

            do {
                bTmp = (byte)AardvarkApi.aa_gpio_get(iHandle);
            } while (bTmp != 3);

            AardvarkApi.aa_i2c_pullup(iHandle, AardvarkApi.AA_I2C_PULLUP_BOTH);
            AardvarkApi.aa_configure(iHandle, AardvarkConfig.AA_CONFIG_SPI_I2C);

            bitrate = AardvarkApi.aa_i2c_bitrate(iHandle, bitrate);

            tbPort.ReadOnly = true;
            tbBitrate.ReadOnly = true;
            cbConnectState.Checked = true;

            System.Threading.Thread.Sleep(delay);

            return 0;
        }

        private void cbTriggerRead_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTriggerRead.Checked == false) {
                tbValue.Text = "";
                return;
            }

            if (cbConnectState.Checked == true)
                _DisconnectDevice();

            if (_WaitTriggerAndDelay() < 0)
                return;

            if (_SignalRead() < 0)
                return;
        }


    }
}
