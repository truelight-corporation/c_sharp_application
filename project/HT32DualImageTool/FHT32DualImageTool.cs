using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using I2cMasterInterface;

namespace HT32DualImageTool
{
    public partial class FHT32DualImageTool : Form
    {
        private I2cMaster i2cMaster = new I2cMaster();

        private int _I2cMasterConnect()
        {
            if (i2cMaster.ConnectApi(400) < 0)
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

        private int _I2cRead(byte devAddr, byte length, byte[] data)
        {
            int rv;
            if (i2cMaster.connected == false) {
                if (_I2cMasterConnect() < 0)
                    return -1;
            }

            rv = i2cMaster.ReadRawApi(devAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _I2cMasterDisconnect();
            }
            else if (rv != length)
                MessageBox.Show("Only read " + rv + " not " + length + " byte Error!!");

            return rv;
        }

        private int _I2cWrite(byte devAddr, byte length, byte[] data)
        {
            int rv;

            if (i2cMaster.connected == false) {
                if (_I2cMasterConnect() < 0)
                    return -1;
            }

            rv = i2cMaster.WriteRawApi(devAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                _I2cMasterDisconnect();
            }

            return rv;
        }

        public FHT32DualImageTool()
        {
            InitializeComponent();

            if (ucFirmwareTool.SetI2cReadCBApi(_I2cRead) < 0) {
                MessageBox.Show("ucFirmwareTool.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucFirmwareTool.SetI2cWriteCBApi(_I2cWrite) < 0) {
                MessageBox.Show("ucFirmwareTool.SetI2cReadCBApi() faile Error!!");
                return;
            }
        }

        private void cbConnected_CheckedChanged(object sender, EventArgs e)
        {
            int rv;
            if (cbConnected.Checked == true) {
                rv = i2cMaster.ConnectApi(400);
                if (rv < 0)
                    return;
            }
            else
                i2cMaster.DisconnectApi();
        }
    }
}
