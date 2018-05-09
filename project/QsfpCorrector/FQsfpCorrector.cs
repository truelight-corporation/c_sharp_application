using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using I2cMasterInterface;

namespace QsfpCorrector
{
    public partial class FQsfpCorrector : Form
    {
        private I2cMaster qsfpI2cMaster = new I2cMaster();

        private int _QsfpI2cConnect()
        {
            if (qsfpI2cMaster.ConnectApi() < 0)
                return -1;

            cbQsfpConnected.Checked = true;

            return 0;
        }

        private int _QsfpI2cDisconnect()
        {
            if (qsfpI2cMaster.DisconnectApi() < 0)
                return -1;

            cbQsfpConnected.Checked = false;

            return 0;
        }

        private int _QsfpI2cRead(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;
            if (qsfpI2cMaster.connected == false) {
                if (_QsfpI2cConnect() < 0)
                    return -1;
            }

            rv = qsfpI2cMaster.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _QsfpI2cDisconnect();
            }
            else if (rv != length)
                MessageBox.Show("Only read " + rv + " not " + length + " byte Error!!");

            return rv;
        }

        private int _QsfpI2cWrite(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (qsfpI2cMaster.connected == false) {
                if (_QsfpI2cConnect() < 0)
                    return -1;
            }

            rv = qsfpI2cMaster.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _QsfpI2cDisconnect();
            }

            return rv;
        }

        public FQsfpCorrector()
        {
            InitializeComponent();
            if (ucQsfpCorrector.SetQsfpI2cReadCBApi(_QsfpI2cRead) < 0) {
                MessageBox.Show("ucQsfpCorrector.SetQsfpI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucQsfpCorrector.SetQsfpI2cWriteCBApi(_QsfpI2cWrite) < 0) {
                MessageBox.Show("ucQsfpCorrector.SetQsfpI2cWriteCBApi() faile Error!!");
                return;
            }
        }

        private void _cbQsfpConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbQsfpConnected.Checked == true)
                qsfpI2cMaster.ConnectApi();
            else
                qsfpI2cMaster.DisconnectApi();
        }

    }
}
