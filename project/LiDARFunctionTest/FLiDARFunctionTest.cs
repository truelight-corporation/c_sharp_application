using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using I2cMasterInterface;

namespace LiDARFunctionTest
{
    public partial class FLiDARFunctionTest : Form
    {
        private I2cMaster i2cMaster = new I2cMaster();

        private int _I2cMasterConnect()
        {
            if (i2cMaster.ConnectApi() < 0)
                return -1;

            cbI2cConnected.Checked = true;

            return 0;
        }

        private int _I2cMasterDisconnect()
        {
            if (i2cMaster.DisconnectApi() < 0)
                return -1;

            cbI2cConnected.Checked = false;

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
            int rv = 0;

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

        private bool _I2cConnectStatus()
        {
            return cbI2cConnected.Checked;
        }

        private int _GetPassword(int length, byte[] data)
        {
            byte[] bATmp = new byte[4];

            if (length < 4)
                return -1;

            if (data == null)
                return -1;

            if (tbPassword.Text.Length == 0)
                return 0;

            if (tbPassword.Text.Length != 4) {
                tbPassword.Text = "";
                MessageBox.Show("Please input 4 char in new password!!");
                return -1;
            }

            bATmp = Encoding.Default.GetBytes(tbPassword.Text);

            try {
                Buffer.BlockCopy(bATmp, 0, data, 0, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            return 4;
        }

        public FLiDARFunctionTest()
        {
            InitializeComponent();
            if (ucLiDARFunctionTest.SetI2cReadCBApi(_I2cRead) < 0) {
                MessageBox.Show("ucLiDARFunctionTest.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucLiDARFunctionTest.SetI2cWriteCBApi(_I2cWrite) < 0) {
                MessageBox.Show("ucLiDARFunctionTest.SetI2cWriteCBApi() faile Error!!");
                return;
            }
            if (ucLiDARFunctionTest.SetI2cConnectStatusCBApi(_I2cConnectStatus) < 0) {
                MessageBox.Show("ucLiDARFunctionTest.SetI2cConnectStatusCBApi() faile Error!!");
                return;
            }
            if (ucLiDARFunctionTest.SetGetPasswordCBApi(_GetPassword) < 0) {
                MessageBox.Show("ucLiDARFunctionTest.SetGetPasswordCBApi() faile Error!!");
                return;
            }
        }

        private void cbI2cConnected_CheckedChanged(object sender, EventArgs e)
        {
            int rv;
            if (cbI2cConnected.Checked == true) {
                rv = i2cMaster.ConnectApi(100);
                if (rv < 0)
                    return;
                ucLiDARFunctionTest.I2cConnectedNotifyApi();
            }
            else {
                i2cMaster.DisconnectApi();
                ucLiDARFunctionTest.I2cDisconnectedNotifyApi();
            }
        }
    }
}
