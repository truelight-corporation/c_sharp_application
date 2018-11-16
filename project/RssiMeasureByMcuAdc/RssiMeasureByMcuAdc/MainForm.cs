using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using I2cMasterInterface;

namespace RssiMeasureByMcuAdc
{
    public partial class MainForm : Form
    {
        private I2cMaster lightSourceI2cAdapter = new I2cMaster();
        private I2cMaster rssiMeasureI2cAdapter = new I2cMaster();

        private int _LightSourceI2cConnect()
        {
            if (lightSourceI2cAdapter.ConnectApi(100) < 0)
                return -1;

            if (lightSourceI2cAdapter.connected == true)
            {
                lightSourceI2cAdapter.SetTimeoutApi(50);
                cbLightSourceI2cConnected.Checked = true;
            }
            else
                cbLightSourceI2cConnected.Checked = false;

            return 0;
        }

        private int _LightSourceI2cDisconnect()
        {
            if (lightSourceI2cAdapter.DisconnectApi() < 0)
                return -1;

            cbLightSourceI2cConnected.Checked = false;

            return 0;
        }

        private int _LightSourceI2cRead(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (lightSourceI2cAdapter.connected == false)
            {
                if (_LightSourceI2cConnect() < 0)
                    return -1;
            }

            rv = lightSourceI2cAdapter.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0)
            {
                MessageBox.Show("Light Source module no response!!");
                return -1;
            }

            return rv;
        }

        private int _LightSourceI2cWrite(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (lightSourceI2cAdapter.connected == false)
            {
                if (_LightSourceI2cConnect() < 0)
                    return -1;
            }

            rv = lightSourceI2cAdapter.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0)
            {
                MessageBox.Show("Light Source module no response!!");
                return -1;
            }

            return rv;
        }

        private int _RssiMeasureI2cConnect()
        {
            if (rssiMeasureI2cAdapter.ConnectApi(100) < 0)
                return -1;

            if (rssiMeasureI2cAdapter.connected == true)
            {
                rssiMeasureI2cAdapter.SetTimeoutApi(50);
                cbRssiMeasureI2cConnected.Checked = true;
            }
            else
                cbRssiMeasureI2cConnected.Checked = false;

            return 0;
        }

        private int _RssiMeasureI2cDisconnect()
        {
            if (rssiMeasureI2cAdapter.DisconnectApi() < 0)
                return -1;

            cbRssiMeasureI2cConnected.Checked = false;

            return 0;
        }

        private int _RssiMeasureI2cRead(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (rssiMeasureI2cAdapter.connected == false)
            {
                if (_RssiMeasureI2cConnect() < 0)
                    return -1;
            }

            rv = rssiMeasureI2cAdapter.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0)
            {
                MessageBox.Show("RSSI Measure module no response!!");
                return -1;
            }

            return rv;
        }

        private int _RssiMeasureI2cWrite(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (rssiMeasureI2cAdapter.connected == false)
            {
                if (_RssiMeasureI2cConnect() < 0)
                    return -1;
            }

            rv = rssiMeasureI2cAdapter.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0)
            {
                MessageBox.Show("RSSI Measure module no response!!");
                return -1;
            }

            return rv;
        }

        public MainForm()
        {
            InitializeComponent();

            if (ucRssiMeasure.SetLightSourceI2cReadCBApi(_LightSourceI2cRead) < 0)
            {
                MessageBox.Show("ucRssiMeasure.SetLightSourceI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucRssiMeasure.SetLightSourceI2cWriteCBApi(_LightSourceI2cWrite) < 0)
            {
                MessageBox.Show("ucRssiMeasure.SetLightSourceI2cWriteCBApi() faile Error!!");
                return;
            }
            if (ucRssiMeasure.SetRssiMeasureI2cReadCBApi(_RssiMeasureI2cRead) < 0)
            {
                MessageBox.Show("ucRssiMeasure.SetRssiMeasureI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucRssiMeasure.SetRssiMeasureI2cWriteCBApi(_RssiMeasureI2cWrite) < 0)
            {
                MessageBox.Show("ucRssiMeasure.SetRssiMeasureI2cWriteCBApi() faile Error!!");
                return;
            }

        }

        private void cbRssiMeasureI2cConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRssiMeasureI2cConnected.Checked == true)
            {
                _RssiMeasureI2cConnect();
                cbAutoMonitor.Enabled = true;
                if (cbLightSourceI2cConnected.Checked == true)
                    cbAutoMonitor.Checked = true;
            }
            else
            {
                if (cbLightSourceI2cConnected.Checked == false)
                {
                    //ucQsfpPlus40gSr4DcTest.StopMonitorApi();
                    cbAutoMonitor.Checked = false;
                    cbAutoMonitor.Enabled = false;
                }
                _RssiMeasureI2cDisconnect();
            }
        }

        private void cbLightSourceI2cConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLightSourceI2cConnected.Checked == true)
            {
                _LightSourceI2cConnect();
            }
            else
            {
                if (cbRssiMeasureI2cConnected.Checked == false)
                {
                    //ucQsfpPlus40gSr4DcTest.StopMonitorApi();
                    cbAutoMonitor.Checked = false;
                    cbAutoMonitor.Enabled = false;
                }
                _LightSourceI2cDisconnect();
            }
        }

        private void cbAutoMonitor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoMonitor.Checked == true)
                ucRssiMeasure.StartMonitorApi();
            else
                ucRssiMeasure.StopMonitorApi();
        }
 
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ucRssiMeasure.StopMonitorApi();
            ucRssiMeasure.SaveFileCheckApi(sender, e);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Space":
                    ucRssiMeasure.SetFocusOnLogFilePathApi();
                    ucRssiMeasure.SpaceKeyDownApi(sender, e);
                    break;

                case "Right":
                    ucRssiMeasure.RightKeyDownApi(sender, e);
                    break;

                case "Left":
                    ucRssiMeasure.LeftKeyDownApi(sender, e);
                    break;

                default:
                    break;
            }
        }
    }
}
