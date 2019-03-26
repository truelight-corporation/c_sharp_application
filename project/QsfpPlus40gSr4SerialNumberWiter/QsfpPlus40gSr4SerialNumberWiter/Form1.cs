using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using I2cMasterInterface;

namespace QsfpPlus40gSr4SerialNumberWiter
{
    public partial class Form1 : Form
    {
        private I2cMaster i2cAdapter = new I2cMaster();

        private int _I2cConnect()
        {
            if (i2cAdapter.ConnectApi(100) < 0)
                return -1;

            if (i2cAdapter.connected == true)
            {
                i2cAdapter.SetTimeoutApi(50);
                cbI2cAdapterConnected.Checked = true;
            }
            else
                cbI2cAdapterConnected.Checked = false;

            return 0;
        }

        private int _I2cDisconnect()
        {
            if (i2cAdapter.DisconnectApi() < 0)
                return -1;

            cbI2cAdapterConnected.Checked = false;

            return 0;
        }

        private int _I2cRead(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (i2cAdapter.connected == false)
            {
                if (_I2cConnect() < 0)
                    return -1;
            }

            rv = i2cAdapter.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0)
            {
                MessageBox.Show("QSFP+ module no response!!");
                return -1;
            }

            return rv;
        }

        private int _I2cWrite(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (i2cAdapter.connected == false)
            {
                if (_I2cConnect() < 0)
                    return -1;
            }

            rv = i2cAdapter.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0)
            {
                MessageBox.Show("QSFP+ module no response!!");
                return -1;
            }

            return rv;
        }

        public Form1()
        {
            InitializeComponent();

            
            if (ucSerialNumberWriter.SetI2cReadCBApi(_I2cRead) < 0)
            {
                MessageBox.Show("ucQsfpPlus40gSr4DcTest.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucSerialNumberWriter.SetI2cWriteCBApi(_I2cWrite) < 0)
            {
                MessageBox.Show("ucQsfpPlus40gSr4DcTest.SetI2cWriteCBApi() faile Error!!");
                return;
            }
        }

        private void cbI2cAdapterConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbI2cAdapterConnected.Checked == true)
            {
                _I2cConnect();
                /*
                cbAutoMonitor.Enabled = true;
                if (cbPowerMeterConnected.Checked == true)
                    cbAutoMonitor.Checked = true;
                */
            }
            else
            {
                /*
                if (cbPowerMeterConnected.Checked == false)
                {
                    ucQsfpPlus40gSr4DcTest.StopMonitorApi();
                    cbAutoMonitor.Checked = false;
                    cbAutoMonitor.Enabled = false;
                }
                */
                _I2cDisconnect();
            }
        }

        private void cbStartReadSerialNumber_CheckedChanged(object sender, EventArgs e)
        {
            if (cbStartReadSerialNumber.Checked == true)
                ucSerialNumberWriter.StartMonitorApi();
            else
                ucSerialNumberWriter.StopMonitorApi();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ucSerialNumberWriter.StopMonitorApi();
            ucSerialNumberWriter.SaveFileCheckApi(sender, e);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Space":
                    ucSerialNumberWriter.SetFocusOnLogFilePathApi();
                    ucSerialNumberWriter.SpaceKeyDownApi(sender, e);
                    break;

                default:
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
