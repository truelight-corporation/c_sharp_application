using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using I2cMasterInterface;


namespace QsfpPlus40gSr4Config
{
    public partial class fQsfpPlus40gSr4Config : Form
    {
        private I2cMaster i2cMaster = new I2cMaster();

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[] { 32 };

            if (_I2cWrite(80, 127, 1, data) < 0)
                return -1;

            data[0] = mode;

            if (_I2cWrite(80, 164, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _I2cConnect()
        {
            if (i2cMaster.ConnectApi(100) < 0)
                return -1;

            cbConnected.Checked = true;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            return 0;
        }

        private int _I2cDisconnect()
        {
            if (i2cMaster.DisconnectApi() < 0)
                return -1;

            cbConnected.Checked = false;

            return 0;
        }

        private int _I2cRead(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;
            if (i2cMaster.connected == false) {
                if (_I2cConnect() < 0)
                    return -1;
            }

            rv = i2cMaster.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _I2cDisconnect();
            }
            else if (rv != length)
                MessageBox.Show("Only read " + rv + " not " + length + " byte Error!!");

            return rv;
        }

        private int _I2cWrite(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (i2cMaster.connected == false) {
                if (_I2cConnect() < 0)
                    return -1;
            }

            rv = i2cMaster.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _I2cDisconnect();
            }

            return rv;
        }

        public fQsfpPlus40gSr4Config()
        {
            InitializeComponent();

            if (ucGn1190Config.SetI2cReadCBApi(_I2cRead) < 0) {
                MessageBox.Show("ucGn1190Config.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucGn1190Config.SetI2cWriteCBApi(_I2cWrite) < 0) {
                MessageBox.Show("ucGn1190Config.SetI2cWriteCBApi() faile Error!!");
                return;
            }
            if (ucGn1090Config.SetI2cReadCBApi(_I2cRead) < 0) {
                MessageBox.Show("ucGn1190Config.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucGn1090Config.SetI2cWriteCBApi(_I2cWrite) < 0) {
                MessageBox.Show("ucGn1190Config.SetI2cWriteCBApi() faile Error!!");
                return;
            }
            if (ucGn1190Corrector.SetQsfpI2cReadCBApi(_I2cRead) < 0) {
                MessageBox.Show("ucQsfpCorrector.SetQsfpI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucGn1190Corrector.SetQsfpI2cWriteCBApi(_I2cWrite) < 0) {
                MessageBox.Show("ucQsfpCorrector.SetQsfpI2cWriteCBApi() faile Error!!");
                return;
            }
        }

        private int _CheckFirmwareVersion()
        {
            byte[] data = new byte[12];
            byte[] bATmp = new byte[2];
            String sTmp = "170001001001";
            int iTmp;

            data[0] = 32;
            if (_I2cWrite(80, 127, 1, data) < 0)
                return -1;

            if (_I2cRead(80, 165, 10, data) < 0)
                return -1;

            bATmp = new byte[2];
            System.Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            tbFirmwareVersion.Text = Encoding.Default.GetString(bATmp);
            iTmp = Convert.ToInt32(tbFirmwareVersion.Text);
            if (iTmp < 9)
                MessageBox.Show("GUI cannot support Version: " + tbFirmwareVersion.Text + " < 9!!");

            bATmp = new byte[8];
            System.Buffer.BlockCopy(data, 2, bATmp, 0, 8);
            tbFirmwareDate.Text = Encoding.Default.GetString(bATmp);

            data[0] = 5;
            if (_I2cWrite(80, 127, 1, data) < 0)
                return -1;

            data = System.Text.Encoding.Default.GetBytes(sTmp);

            if (_I2cWrite(80, 240, 12, data) < 0)
                return -1;

            if (_I2cRead(80, 240, 12, data) < 0)
                return -1;

            sTmp = System.Text.Encoding.Default.GetString(data);

            tbSerialNumber.Text = sTmp;

            return 0;
        }

        private void cbConnected_CheckedChanged(object sender, EventArgs e)
        {
            int rv;

            if (cbConnected.Checked == true) {
                rv = _I2cConnect();
                if (rv < 0)
                    return;

                _CheckFirmwareVersion();
            }
            else {
                _I2cDisconnect();
                tbSerialNumber.Text = tbFirmwareVersion.Text = tbFirmwareDate.Text = "";
            }
        }
    }
}
