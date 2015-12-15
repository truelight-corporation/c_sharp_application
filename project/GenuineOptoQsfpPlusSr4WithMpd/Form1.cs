using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using I2cMasterInterface;

namespace GenuineOptoQsfpPlusSr4WithMpd
{
    public partial class fMain : Form
    {
        private I2cMaster i2cMaster = new I2cMaster();

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

        private int _I2cMasterDisconnect()
        {
            if (i2cMaster.DisconnectApi() < 0)
                return -1;

            cbConnected.Checked = false;

            return 0;
        }

        private int _I2cMasterConnect()
        {
            byte[] buf = new byte[5];

            if ((tbPassword123.Text.Length == 0) || (tbPassword124.Text.Length == 0) ||
                (tbPassword125.Text.Length == 0) || (tbPassword126.Text.Length == 0)) {
                MessageBox.Show("Before connect need input 4 int password!!");
                return -1;
            }

            if (i2cMaster.ConnectApi(100) < 0)
                return -1;

            if (_I2cRead(80, 0, 1, buf) != 1)
                goto Disconnect;

            buf[0] = Convert.ToByte(tbPassword123.Text);
            buf[1] = Convert.ToByte(tbPassword124.Text);
            buf[2] = Convert.ToByte(tbPassword125.Text);
            buf[3] = Convert.ToByte(tbPassword126.Text);
            buf[4] = 8;
            if (_I2cWrite(80, 123, 5, buf) < 0)
                goto Disconnect;

            cbConnected.Checked = true;

            return 0;

        Disconnect:
            _I2cMasterDisconnect();
            cbConnected.Checked = false;
            return -1;
        }

        public fMain()
        {
            InitializeComponent();
            if (ucMpdAndPdLoger.SetI2cReadCBApi(_I2cRead) < 0) {
                MessageBox.Show("ucMpdAndPdLoger.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucMpdAndPdLoger.SetI2cWriteCBApi(_I2cWrite) < 0) {
                MessageBox.Show("ucMpdAndPdLoger.SetI2cWriteCBApi() faile Error!!");
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
