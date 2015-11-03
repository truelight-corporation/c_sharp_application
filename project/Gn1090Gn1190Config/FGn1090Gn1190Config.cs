using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using I2cMasterInterface;

namespace Gn1090Gn1190Config
{
    public partial class FGn1090Gn1190Config : Form
    {
        private I2cMaster i2cMaster = new I2cMaster();

        private int _I2cMasterConnect()
        {
            if (i2cMaster.ConnectApi(100) < 0)
                return -1;

            cbConnected.Checked = true;

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

            rv = i2cMaster.ReadApi(devAddr, regAddr, length, data);
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

            rv = i2cMaster.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _I2cMasterDisconnect();
            }

            return rv;
        }

        public FGn1090Gn1190Config()
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
        }

        private void cbConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbConnected.Checked == true)
                _I2cMasterConnect();
            else
                _I2cMasterDisconnect();
        }
    }
}
