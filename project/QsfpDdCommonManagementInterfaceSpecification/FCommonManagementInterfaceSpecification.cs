using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using I2cMasterInterface;

namespace QsfpDdCommonManagementInterfaceSpecification
{
    public partial class FCommonManagementInterfaceSpecification : Form
    {
        private I2cMaster i2cMaster = new I2cMaster();

        private int _I2cMasterConnect()
        {
            if (i2cMaster.ConnectApi() < 0)
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

        public FCommonManagementInterfaceSpecification()
        {
            InitializeComponent();
            if (ucDigitalDiagnosticsMonitoring.SetI2cReadCBApi(_I2cRead) < 0) {
                MessageBox.Show("ucDigitalDiagnosticsMonitoring.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucDigitalDiagnosticsMonitoring.SetI2cWriteCBApi(_I2cWrite) < 0) {
                MessageBox.Show("ucDigitalDiagnosticsMonitoring.SetI2cReadCBApi() faile Error!!");
                return;
            }
            /*
            if (ucDigitalDiagnosticsMonitoring.SetWritePasswordCBApi(ucInformation.WritePassword) < 0) {
                MessageBox.Show("ucDigitalDiagnosticsMonitoring.SetWritePasswordCBApi() faile Error!!");
                return;
            }
            */
        }

        private int _CheckFirmwareVersion()
        {
            /* ToDo@wood:
            byte[] data = new byte[10];
            byte[] bATmp = new byte[2];

            data[0] = 0xAA;
            if (_I2cWrite(80, 127, 1, data) < 0)
                return -1;

            if (_I2cRead(80, 165, 10, data) < 0)
                return -1;

            if ((data[0] == 0) && (data[1] == 0) && (data[2] == 0) && (data[3] == 0) &&
                (data[4] == 0) && (data[5] == 0) && (data[6] == 0) && (data[7] == 0) &&
                (data[8] == 0) && (data[9] == 0)) {
                data[0] = 32;
                if (_I2cWrite(80, 127, 1, data) < 0)
                    return -1;

                if (_I2cRead(80, 165, 10, data) < 0)
                    return -1;
            }

            bATmp = new byte[2];
            System.Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            tbFirmwareVersion.Text = Encoding.Default.GetString(bATmp);

            if (tbFirmwareVersionCheck.Text.Length != 0) {
                if (cbFirmwareVersionCheck.Checked == true) {
                    if (!tbFirmwareVersion.Text.Equals(tbFirmwareVersionCheck.Text))
                        MessageBox.Show("Firmware Version: " + tbFirmwareVersion.Text +
                            " != " + tbFirmwareVersionCheck.Text + " Error!!");
                }
            }
            else {
                tbFirmwareVersionCheck.Text = tbFirmwareVersion.Text;
            }

            bATmp = new byte[8];
            System.Buffer.BlockCopy(data, 2, bATmp, 0, 8);
            tbFirmwareDate.Text = Encoding.Default.GetString(bATmp);

            if (tbFirmwareDateCheck.Text.Length != 0) {
                if (cbFirmwareVersionCheck.Checked == true) {
                    if (!tbFirmwareDate.Text.Equals(tbFirmwareDateCheck.Text))
                        MessageBox.Show("Firmware Date: " + tbFirmwareDate.Text +
                            " != " + tbFirmwareDateCheck.Text + " Error!!");
                }
            }
            else {
                tbFirmwareDateCheck.Text = tbFirmwareDate.Text;
            }
            */
            return 0;
        }

        private void cbConnected_CheckedChanged(object sender, EventArgs e)
        {
            int rv;
            if (cbConnected.Checked == true) {
                rv = i2cMaster.ConnectApi(400);
                if (rv < 0)
                    return;
                _CheckFirmwareVersion();
            }
            else
                i2cMaster.DisconnectApi();
        }
    }
}
