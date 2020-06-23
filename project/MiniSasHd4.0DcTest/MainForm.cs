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

namespace MiniSasHd4Dot0DcTest
{
    public partial class MainForm : Form
    {
        private I2cMaster measuredObjectI2cAdapter = new I2cMaster();
        private I2cMaster powerMeterQsfpI2cAdapter = new I2cMaster();

        private ExfoIqs1600Scpi powerMeter = new ExfoIqs1600Scpi();
        private String powerMeterSelect = "Power Meter";

        private int _MeasuredObjectI2cConnect()
        {
            if (measuredObjectI2cAdapter.ConnectApi(100) < 0)    
                return -1;

            if (measuredObjectI2cAdapter.connected == true) {
                measuredObjectI2cAdapter.SetTimeoutApi(50);
                cbMeasuredObjectI2cAdapterConnected.Checked = true;
            }
            else
                cbMeasuredObjectI2cAdapterConnected.Checked = false;

            return 0;
        }

        private int _MeasuredObjectI2cDisconnect()
        {
            if (measuredObjectI2cAdapter.DisconnectApi() < 0)
                return -1;

            cbMeasuredObjectI2cAdapterConnected.Checked = false;

            return 0;
        }

        private int _MeasuredObjectI2cRead(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (measuredObjectI2cAdapter.connected == false)
                return 0;

            rv = measuredObjectI2cAdapter.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                return -1;
            }
            
            return rv;
        }

        private int _MeasuredObjectI2cWrite(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (measuredObjectI2cAdapter.connected == false)
                    return 0;

            rv = measuredObjectI2cAdapter.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("QSFP+ module no response!!");
                return -1;
            }
            
            return rv;
        }

        private int _PowerMeterQsfpI2cConnect()
        {
            if (powerMeterQsfpI2cAdapter.ConnectApi(100) < 0)
                return -1;

            if (powerMeterQsfpI2cAdapter.connected == true)
            {
                powerMeterQsfpI2cAdapter.SetTimeoutApi(50);
                cbPowerMeterQsfpConnected.Checked = true;
            }
            else
                cbMeasuredObjectI2cAdapterConnected.Checked = false;

            return 0;
        }

        private int _PowerMeterQsfpI2cDisconnect()
        {
            if (powerMeterQsfpI2cAdapter.DisconnectApi() < 0)
                return -1;

            cbPowerMeterQsfpConnected.Checked = false;

            return 0;
        }

        private int _PowerMeterQsfpI2cRead(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (powerMeterQsfpI2cAdapter.connected == false)
                return 0;

            rv = powerMeterQsfpI2cAdapter.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0)
            {
                MessageBox.Show("Power Meter QSFP module no response!!");
                return -1;
            }

            return rv;
        }

        private int _PowerMeterQsfpI2cWrite(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (powerMeterQsfpI2cAdapter.connected == false)
                return 0;

            rv = powerMeterQsfpI2cAdapter.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0)
            {
                MessageBox.Show("Power Meter QSFP module no response!!");
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
            if (cbPowerMeterConnected.Checked == false)
            {
                buffer[0] = buffer[1] = buffer[2] = buffer[3] = "NA";
                return -1;
            }

            if (powerMeter.ReadPower(buffer) < 0)
                return -1;

            return 0;
        }

        private int _PowerMeterQsfpRead(string[] buffer)
        {
            byte[] data = new byte[2];
            byte[] reverseData;
            int tmp;
            float power;

            if (cbPowerMeterQsfpConnected.Checked == false)
                goto error;

            if (_PowerMeterQsfpI2cRead(80, 34, 2, data) != 2)
                goto error;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            buffer[0] = power.ToString();

            if (_PowerMeterQsfpI2cRead(80, 36, 2, data) != 2)
                goto error;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            buffer[1] = power.ToString();

            if (_PowerMeterQsfpI2cRead(80, 38, 2, data) != 2)
                goto error;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            buffer[2] = power.ToString();

            if (_PowerMeterQsfpI2cRead(80, 40, 2, data) != 2)
                goto error;

            reverseData = data.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            power = tmp / 10;
            buffer[3] = power.ToString();

            return 0;

        error:
            buffer[0] = buffer[1] = buffer[2] = buffer[3] = "NA";
            return -1;
        }

        private int _PowerMeterReadCb(string[] buffer)
        {
            switch (powerMeterSelect)
            {
                case "Power Meter":
                    _PowerMeterRead(buffer);
                    break;

                case "QSFP":
                    _PowerMeterQsfpRead(buffer);
                    break;

                default:
                    buffer[0] = buffer[1] = buffer[2] = buffer[3] = "NA";
                    return -1;
            }

            return 0;
        }

        public MainForm()
        {
            InitializeComponent();
            
            if (ucMiniSsaHd4Dot0DcTest.SetI2cReadCBApi(_MeasuredObjectI2cRead) < 0) {
                MessageBox.Show("ucQsfpPlus40gSr4DcTest.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucMiniSsaHd4Dot0DcTest.SetI2cWriteCBApi(_MeasuredObjectI2cWrite) < 0) {
                MessageBox.Show("ucQsfpPlus40gSr4DcTest.SetI2cWriteCBApi() faile Error!!");
                return;
            }
            if (ucMiniSsaHd4Dot0DcTest.SetPowerMeterReadCBApi(_PowerMeterReadCb) < 0) {
                MessageBox.Show("ucQsfpPlus40gSr4DcTest.SetPowerMeterReadCBApi() faile Error!!");
                return;
            }
        }

        private void cbI2cAdapterConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMeasuredObjectI2cAdapterConnected.Checked == true) {
                _MeasuredObjectI2cConnect();
                cbAutoMonitor.Enabled = true;
                if (cbPowerMeterConnected.Checked == true)
                    cbAutoMonitor.Checked = true;
            }
            else {
                if (cbPowerMeterConnected.Checked == false) {
                    ucMiniSsaHd4Dot0DcTest.StopMonitorApi();
                    cbAutoMonitor.Checked = false;
                    cbAutoMonitor.Enabled = false;
                }
                _MeasuredObjectI2cDisconnect();
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
                cbPowerMeterSelect.Enabled = false;
            }
            else {

                cbPowerMeterConnected.Checked = false;
                if (cbAutoMonitor.Checked == true) {
                    ucMiniSsaHd4Dot0DcTest.StopMonitorApi();
                }
                if (cbMeasuredObjectI2cAdapterConnected.Checked == false)
                    cbAutoMonitor.Enabled = false;
                tbPowerMeterIpAddr.Enabled = true;
                cbPowerMeterSelect.Enabled = true;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ucMiniSsaHd4Dot0DcTest.StopMonitorApi();
            ucMiniSsaHd4Dot0DcTest.SaveFileCheckApi(sender, e);
        }

        private void cbAutoMonitor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoMonitor.Checked == true)
                ucMiniSsaHd4Dot0DcTest.StartMonitorApi();
            else
                ucMiniSsaHd4Dot0DcTest.StopMonitorApi();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.Space:
                case Keys.Enter:
                    ucMiniSsaHd4Dot0DcTest.SetFocusOnLogFilePathApi();
                    ucMiniSsaHd4Dot0DcTest.SpaceKeyDownApi(sender, e);
                    break;
                
                case Keys.Right:
                    ucMiniSsaHd4Dot0DcTest.RightKeyDownApi(sender, e);
                    break;

                case Keys.Left:
                    ucMiniSsaHd4Dot0DcTest.LeftKeyDownApi(sender, e);
                    break;

                default:
                    break;
            }
            
        }

        private void cbPowerMeterSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbPowerMeterSelect.SelectedItem.ToString())
            {
                case "Power Meter":
                    cbPowerMeterConnected.Visible = true;
                    lPowerMeterIpAddr.Visible = true;
                    tbPowerMeterIpAddr.Visible = true;
                    cbPowerMeterQsfpConnected.Visible = false;
                    powerMeterSelect = "Power Meter";
                    break;

                case "QSFP":
                    cbPowerMeterConnected.Visible = false;
                    lPowerMeterIpAddr.Visible = false;
                    tbPowerMeterIpAddr.Visible = false;
                    cbPowerMeterQsfpConnected.Visible = true;
                    powerMeterSelect = "QSFP";
                    break;

                default:
                    break;
            }
        }

        private void cbPowerMeterQsfpConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPowerMeterQsfpConnected.Checked == true) {
                _PowerMeterQsfpI2cConnect();
                cbAutoMonitor.Enabled = true;
                cbPowerMeterSelect.Enabled = false;
            }
            else {
                if (cbAutoMonitor.Checked == true)
                    ucMiniSsaHd4Dot0DcTest.StopMonitorApi();
               
                if (cbMeasuredObjectI2cAdapterConnected.Checked == false)
                    cbAutoMonitor.Enabled = false;
                cbPowerMeterSelect.Enabled = true;
                _PowerMeterQsfpI2cDisconnect();
            }
        }
    }
}
