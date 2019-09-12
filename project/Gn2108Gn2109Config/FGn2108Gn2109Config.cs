using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using I2cMasterInterface;

namespace Gn2108Gn2109Config
{
    public partial class FGn2108Gn2109Config : Form
    {
        private I2cMaster i2cMaster = new I2cMaster();

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[] { 0xaa };

            if (i2cMaster.WriteApi(80, 127, 1, data) < 0)
                return -1;

            data[0] = mode;

            if (i2cMaster.WriteApi(80, 164, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _I2cMasterConnect()
        {
            if (i2cMaster.ConnectApi(100) < 0)
                return -1;

            cbConnected.Checked = true;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            return 0;
        }

        private int _I2cMasterDisconnect()
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
                if (_I2cMasterConnect() < 0)
                    return -1;
            }

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            rv = i2cMaster.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _I2cMasterDisconnect();
            }
            else if (rv != length)
                MessageBox.Show("Only read " + rv + " not " + length + " byte Error!!");

            return rv;
        }

        private int _I2cRead16(byte devAddr, byte[] regAddr, byte length, byte[] data)
        {
            int rv;
            if (i2cMaster.connected == false) {
                if (_I2cMasterConnect() < 0)
                    return -1;
            }

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            rv = i2cMaster.Read16Api(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _I2cMasterDisconnect();
            }
            else if (rv != length)
                MessageBox.Show("Only read " + rv + " not " + length + " byte Error!!");

            return rv;
        }

        private int _I2cWrite(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (i2cMaster.connected == false) {
                if (_I2cMasterConnect() < 0)
                    return -1;
            }

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            rv = i2cMaster.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _I2cMasterDisconnect();
            }

            return rv;
        }

        private int _I2cWrite16(byte devAddr, byte[] regAddr, byte length, byte[] data)
        {
            int rv;

            if (i2cMaster.connected == false) {
                if (_I2cMasterConnect() < 0)
                    return -1;
            }

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            rv = i2cMaster.Write16Api(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _I2cMasterDisconnect();
            }

            return rv;
        }

        public FGn2108Gn2109Config()
        {
            InitializeComponent();
            if (ucGn2108Config.SetI2cReadCBApi(_I2cRead) < 0) {
                MessageBox.Show("ucGn1190Config.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucGn2108Config.SetI2cWriteCBApi(_I2cWrite) < 0) {
                MessageBox.Show("ucGn1190Config.SetI2cWriteCBApi() faile Error!!");
                return;
            }
            if (ucGn2108Config.SetI2cRead16CBApi(_I2cRead16) < 0) {
                MessageBox.Show("ucGn1190Config.SetI2cRead16CBApi() faile Error!!");
                return;
            }
            if (ucGn2108Config.SetI2cWrite16CBApi(_I2cWrite16) < 0) {
                MessageBox.Show("ucGn1190Config.SetI2cWrite16CBApi() faile Error!!");
                return;
            }

            if (ucGn2109Config.SetI2cReadCBApi(_I2cRead) < 0) {
                MessageBox.Show("ucGn1190Config.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucGn2109Config.SetI2cWriteCBApi(_I2cWrite) < 0) {
                MessageBox.Show("ucGn1190Config.SetI2cWriteCBApi() faile Error!!");
                return;
            }
            if (ucGn2109Config.SetI2cRead16CBApi(_I2cRead16) < 0) {
                MessageBox.Show("ucGn1190Config.SetI2cRead16CBApi() faile Error!!");
                return;
            }
            if (ucGn2109Config.SetI2cWrite16CBApi(_I2cWrite16) < 0) {
                MessageBox.Show("ucGn1190Config.SetI2cWrite16CBApi() faile Error!!");
                return;
            }
        }

        private int _WriteModulePassword()
        {
            byte[] data;

            data = Encoding.Default.GetBytes(tbPassword.Text);

            if (i2cMaster.WriteApi(80, 123, 4, data) < 0)
                return -1;

            return 0;
        }

        private void cbConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbConnected.Checked == true) {
                if (_I2cMasterConnect() < 0)
                    return;
                _WriteModulePassword();
            }
            else
                _I2cMasterDisconnect();
        }
    }
}
