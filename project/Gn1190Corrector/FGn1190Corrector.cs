using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using I2cMasterInterface;

namespace Gn1190Corrector
{
    public partial class FGn1190Corrector : Form
    {
        private I2cMaster i2cMaster = new I2cMaster();

        private int _I2cConnect()
        {
            if (i2cMaster.ConnectApi(100) < 0)
                return -1;

            cbConnected.Checked = true;

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

        public FGn1190Corrector()
        {
            InitializeComponent();

            if (ucGn1190Corrector.SetQsfpI2cReadCBApi(_I2cRead) < 0) {
                MessageBox.Show("ucQsfpCorrector.SetQsfpI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucGn1190Corrector.SetQsfpI2cWriteCBApi(_I2cWrite) < 0) {
                MessageBox.Show("ucQsfpCorrector.SetQsfpI2cWriteCBApi() faile Error!!");
                return;
            }
        }

        private void cbConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbConnected.Checked == true)
                _I2cConnect();
            else
                _I2cDisconnect();
        }
    }
}
