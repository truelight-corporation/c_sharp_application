using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using I2cMasterInterface;


namespace MiniSasHd4Dot0DcTest
{
    public partial class MainForm : Form
    {
        private I2cMaster measuredObjectI2cAdapterA = new I2cMaster();
        private I2cMaster measuredObjectI2cAdapterB = new I2cMaster();


        private int _MeasuredObjectI2cConnectA()
        {
            if (measuredObjectI2cAdapterA.ConnectApi(100) < 0)    
                return -1;

            if (measuredObjectI2cAdapterA.connected == true) {
                measuredObjectI2cAdapterA.SetTimeoutApi(50);
                cbMeasuredObjectI2cAdapterConnectedA.Checked = true;
            }
            else
                cbMeasuredObjectI2cAdapterConnectedA.Checked = false;            

            return 0;
        }

        private int _MeasuredObjectI2cConnectB()
        {           
            if (measuredObjectI2cAdapterB.ConnectApi(100) < 0)
                return -1;

            if (measuredObjectI2cAdapterB.connected == true)
            {
                measuredObjectI2cAdapterB.SetTimeoutApi(50);
                cbMeasuredObjectI2cAdapterConnectedB.Checked = true;
            }
            else
                cbMeasuredObjectI2cAdapterConnectedB.Checked = false;

            return 0;
        }

        private int _MeasuredObjectI2cDisconnectA()
        {
            if (measuredObjectI2cAdapterA.DisconnectApi() < 0)
                return -1;

            cbMeasuredObjectI2cAdapterConnectedA.Checked = false;

            return 0;
        }

        private int _MeasuredObjectI2cDisconnectB()
        {
            if (measuredObjectI2cAdapterB.DisconnectApi() < 0)
                return -1;

            cbMeasuredObjectI2cAdapterConnectedB.Checked = false;

            return 0;
        }

        private int _MeasuredObjectI2cReadA(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (measuredObjectI2cAdapterA.connected == false)
                return 0;

            rv = measuredObjectI2cAdapterA.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("Module no response!!");
                return -1;
            }
            
            return rv;
        }

        private int _MeasuredObjectI2cWriteA(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (measuredObjectI2cAdapterA.connected == false)
                    return 0;

            rv = measuredObjectI2cAdapterA.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0) {
                MessageBox.Show("Module no response!!");
                return -1;
            }
            
            return rv;
        }

        private int _MeasuredObjectI2cReadB(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (measuredObjectI2cAdapterB.connected == false)
                return 0;

            rv = measuredObjectI2cAdapterB.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0)
            {
                MessageBox.Show("Module no response!!");
                return -1;
            }

            return rv;
        }

        private int _MeasuredObjectI2cWriteB(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;

            if (measuredObjectI2cAdapterB.connected == false)
                return 0;

            rv = measuredObjectI2cAdapterB.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0)
            {
                MessageBox.Show("Module no response!!");
                return -1;
            }

            return rv;
        }

        public MainForm()
        {
            InitializeComponent();
            
            if (ucMiniSsaHd4Dot0DcTest.SetI2cReadACBApi(_MeasuredObjectI2cReadA) < 0) {
                MessageBox.Show("ucMiniSasDcTestA.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucMiniSsaHd4Dot0DcTest.SetI2cWriteACBApi(_MeasuredObjectI2cWriteA) < 0) {
                MessageBox.Show("ucMiniSasDcTestA.SetI2cWriteCBApi() faile Error!!");
                return;
            }
            if (ucMiniSsaHd4Dot0DcTest.SetI2cReadBCBApi(_MeasuredObjectI2cReadB) < 0)
            {
                MessageBox.Show("ucMiniSasDcTestB.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucMiniSsaHd4Dot0DcTest.SetI2cWriteBCBApi(_MeasuredObjectI2cWriteB) < 0)
            {
                MessageBox.Show("ucMiniSasDcTestB.SetI2cWriteCBApi() faile Error!!");
                return;
            }
        }

        private void cbI2cAdapterConnectedA_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMeasuredObjectI2cAdapterConnectedA.Checked == true) {
                _MeasuredObjectI2cConnectA();
                cbAutoMonitor.Enabled = true;
                cbAutoMonitor.Checked = true;
            }
            else {
                    
                if(cbMeasuredObjectI2cAdapterConnectedB.Enabled == true)
                {
                    cbAutoMonitor.Checked = true;
                    cbAutoMonitor.Enabled = true;
                }
                else
                {
                    ucMiniSsaHd4Dot0DcTest.StopMonitorApi();
                    cbAutoMonitor.Checked = false;
                    cbAutoMonitor.Enabled = false;
                }                   
                
                _MeasuredObjectI2cDisconnectA();
            }
        }

        private void cbI2cAdapterConnectedB_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMeasuredObjectI2cAdapterConnectedB.Checked == true)
            {
                _MeasuredObjectI2cConnectB();
                cbAutoMonitor.Enabled = true;
                cbAutoMonitor.Checked = true;
            }
            else
            {
                
                if (cbMeasuredObjectI2cAdapterConnectedA.Enabled == true)
                {
                    cbAutoMonitor.Checked = true;
                    cbAutoMonitor.Enabled = true;
                }
                else
                {
                    ucMiniSsaHd4Dot0DcTest.StopMonitorApi();
                    cbAutoMonitor.Checked = false;
                    cbAutoMonitor.Enabled = false;
                }

                _MeasuredObjectI2cDisconnectB();
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
       
    }
}
