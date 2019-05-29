using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using I2cMasterInterface;

namespace SfpDigitalDiagnosticMonitoring
{
    public partial class FDigitalDiagnosticMonitoring : Form
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

        private int _WritePassword()
        {
            byte[] data = new byte[4];
            int tmpI;

            if ((tbPasswordB0.Text.Length != 2) || (tbPasswordB1.Text.Length != 2) ||
                (tbPasswordB2.Text.Length != 2) || (tbPasswordB3.Text.Length != 2)) {
                MessageBox.Show("Please input 4 hex value password before write!!");
                return -1;
            }

            data[0] = 82;
            if (_I2cWrite(81, 127, 1, data) < 0)
                return -1;

            try {
                tmpI = Convert.ToInt32(tbPasswordB0.Text, 16);
                data[0] = (byte)tmpI;
                tmpI = Convert.ToInt32(tbPasswordB1.Text, 16);
                data[1] = (byte)tmpI;
                tmpI = Convert.ToInt32(tbPasswordB2.Text, 16);
                data[2] = (byte)tmpI;
                tmpI = Convert.ToInt32(tbPasswordB3.Text, 16);
                data[3] = (byte)tmpI;
            }
            catch (Exception e) {
                MessageBox.Show(e.ToString());
                return -1;
            }

            if (_I2cWrite(81, 158, 4, data) < 0)
                return -1;

            return 0;
        }

        public FDigitalDiagnosticMonitoring()
        {
            InitializeComponent();
			if (ucA0h.SetI2cReadCBApi(_I2cRead) < 0) {
				MessageBox.Show("ucA0h.SetI2cReadCBApi() faile Error!!");
				return;
			}
			if (ucA0h.SetI2cWriteCBApi(_I2cWrite) < 0) {
				MessageBox.Show("ucA0h.SetI2cReadCBApi() faile Error!!");
				return;
			}
            if (ucA0h.SetI2cWritePasswordCBApi(_WritePassword) < 0) {
                MessageBox.Show("ucA0h.SetI2cWritePasswordCBApi() faile Error!!");
                return;
            }
			if (ucA2h.SetI2cReadCBApi(_I2cRead) < 0) {
				MessageBox.Show("ucA2h.SetI2cReadCBApi() faile Error!!");
				return;
			}
			if (ucA2h.SetI2cWriteCBApi(_I2cWrite) < 0) {
				MessageBox.Show("ucA2h.SetI2cReadCBApi() faile Error!!");
				return;
			}
            if (ucA2h.SetI2cWritePasswordCBApi(_WritePassword) < 0) {
                MessageBox.Show("ucA2h.SetI2cWritePasswordCBApi() faile Error!!");
                return;
            }
        }

        private int _CheckFirmwareVersion()
        {
            byte[] data = new byte[10];
            byte[] bATmp = new byte[2];

            data[0] = 82;
            if (_I2cWrite(81, 127, 1, data) < 0)
                return -1;

            if (_I2cRead(81, 165, 10, data) < 0)
                return -1;

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

            return 0;
        }

        private void cbI2cConnected_CheckedChanged(object sender, EventArgs e)
        {
            int rv;
            if (cbConnected.Checked == true) {
                rv = i2cMaster.ConnectApi();
                if (rv < 0)
                    return;
                _CheckFirmwareVersion();
            }
            else
                i2cMaster.DisconnectApi();
        }

    }
}
