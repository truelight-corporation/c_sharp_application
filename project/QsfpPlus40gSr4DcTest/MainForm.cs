using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using I2cMasterInterface;
using ExfoIqs1600ScpiInterface;

namespace QsfpPlus40gSr4DcTest
{
    public partial class MainForm : Form
    {
        private I2cMaster i2cAdapter = new I2cMaster();
        private ExfoIqs1600Scpi powerMeter = new ExfoIqs1600Scpi();

        private int _I2cConnect()
        {
            if (i2cAdapter.ConnectApi(100) < 0)    
                return -1;

            if (i2cAdapter.connected == true) {
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

            if (i2cAdapter.connected == false) {
                if (_I2cConnect() < 0)
                    return -1;
            }

            rv = i2cAdapter.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                return -1;
            }
            
            return rv;
        }

        private int _I2cWrite(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (i2cAdapter.connected == false) {
                if (_I2cConnect() < 0)
                    return -1;
            }

            rv = i2cAdapter.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                return -1;
            }
            
            return rv;
        }

        private int _PowerMeterConnect()
        {
            tbPowerMeterIpAddr.Enabled = false;

            if (tbPowerMeterIpAddr.Text.Length < 7) {
                MessageBox.Show("Please input Power meter device IP address!!");
                tbPowerMeterIpAddr.Enabled = true;
                return -1;
            }

            if (powerMeter.ConnectApi(tbPowerMeterIpAddr.Text) < 0) {
                tbPowerMeterIpAddr.Enabled = true;
                return -1;
            }

            return 0;
        }

        private int _PowerMeterRead(string[] buffer)
        {
            if (cbPowerMeterConnected.Checked == false) {
                buffer[0] = buffer[1] = buffer[2] = buffer[3] = "NA";
                return -1;
            }

            if (powerMeter.ReadPower(buffer) < 0)
                return -1;

            return 0;
        }

        public MainForm()
        {
            InitializeComponent();

            if (ucQsfpPlus40gSr4DcTest.SetI2cReadCBApi(_I2cRead) < 0) {
                MessageBox.Show("ucQsfpPlus40gSr4DcTest.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucQsfpPlus40gSr4DcTest.SetI2cWriteCBApi(_I2cWrite) < 0) {
                MessageBox.Show("ucQsfpPlus40gSr4DcTest.SetI2cWriteCBApi() faile Error!!");
                return;
            }
            if (ucQsfpPlus40gSr4DcTest.SetPowerMeterReadCBApi(_PowerMeterRead) < 0) {
                MessageBox.Show("ucQsfpPlus40gSr4DcTest.SetPowerMeterReadCBApi() faile Error!!");
                return;
            }
        }

        private void cbI2cAdapterConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbI2cAdapterConnected.Checked == true) {
                _I2cConnect();
                cbAutoMonitor.Enabled = true;
                if (cbPowerMeterConnected.Checked == true)
                    cbAutoMonitor.Checked = true;
            }
            else {
                if (cbPowerMeterConnected.Checked == false) {
                    ucQsfpPlus40gSr4DcTest.StopMonitorApi();
                    cbAutoMonitor.Checked = false;
                    cbAutoMonitor.Enabled = false;
                }
                _I2cDisconnect();
            }
        }

        private void cbPowerMeterConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPowerMeterConnected.Checked == true) {
                if (_PowerMeterConnect() < 0) {
                    cbPowerMeterConnected.Checked = false;
                    return;
                }
                cbPowerMeterConnected.Enabled = false;
                cbAutoMonitor.Enabled = true;
            }
            else {

                cbPowerMeterConnected.Checked = false;
                if (cbAutoMonitor.Checked == true) {
                    
                    ucQsfpPlus40gSr4DcTest.StopMonitorApi();
                }
                if (cbI2cAdapterConnected.Checked == false)
                    cbAutoMonitor.Enabled = false;
                tbPowerMeterIpAddr.Enabled = true;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ucQsfpPlus40gSr4DcTest.StopMonitorApi();
            ucQsfpPlus40gSr4DcTest.SaveFileCheckApi(sender, e);
        }

        private void cbAutoMonitor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoMonitor.Checked == true)
                ucQsfpPlus40gSr4DcTest.StartMonitorApi();
            else
                ucQsfpPlus40gSr4DcTest.StopMonitorApi();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString()) {
                case "Space":
                    ucQsfpPlus40gSr4DcTest.SetFocusOnLogFilePathApi();
                    ucQsfpPlus40gSr4DcTest.SpaceKeyDownApi(sender, e);
                    break;
                
                case "Right":
                    ucQsfpPlus40gSr4DcTest.RightKeyDownApi(sender, e);
                    break;

                case "Left":
                    ucQsfpPlus40gSr4DcTest.LeftKeyDownApi(sender, e);
                    break;

                default:
                    break;
            }
            
        }


    }
}
