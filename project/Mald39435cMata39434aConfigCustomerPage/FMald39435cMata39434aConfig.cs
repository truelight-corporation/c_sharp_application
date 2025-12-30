using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// reference dll
using I2cMasterInterface;


namespace Mald39435Mata39434aConfig
{
    public partial class FMald39435cMata39434aConfig : Form
    {
        private I2cMaster i2cMaster = new I2cMaster();
        public FMald39435cMata39434aConfig()
        {
            InitializeComponent();
            if(ucMald39435cConfig.SetI2cReadCBApi(_I2cRead) < 0)
            {
                MessageBox.Show("ucMald39435cConfig.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if(ucMald39435cConfig.SetI2cWriteCBApi(_I2cWrite) < 0)
            {
                MessageBox.Show("ucMald39435cConfig.SetI2cWriteCBApi() faile Error!!");
                return;
            }
            if (ucMata39434aConfig.SetI2cWriteCBApi(_I2cWrite) < 0)
            {
                MessageBox.Show("ucMata39434aConfig.SetI2cWriteCBApi() faile Error!!");
                return;
            }
            if (ucMata39434aConfig.SetI2cReadCBApi(_I2cRead) < 0)
            {
                MessageBox.Show("ucMata39434aConfig.SetI2cReadCBApi() faile Error!!");
                return;
            }

        }

        private int _I2cRead(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;
            if(i2cMaster.connected == false)
            {
                if (_I2cMasterConnect() < 0)
                    return -1;
            }

            rv = i2cMaster.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0)
            {
                MessageBox.Show("QSFP + module no response!!");
                _I2cMasterDisconnect();
            }
            else if (rv != length)
                MessageBox.Show("Only read " + rv + " not " + length + " byte Error!!");           

            return rv;

        }

        private int _I2cWrite(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv = 0, i;
            if (i2cMaster.connected == false)
            {
                if(_I2cMasterConnect() < 0)
                    return -1;
            }

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (chkI2cActionLog.Checked == false)
            {
                rv = i2cMaster.WriteApi(devAddr,regAddr, length, data);
                if (rv < 0)
                {
                    MessageBox.Show("QSFP + module no response!!");
                    _I2cMasterDisconnect();
                }
            }
            else
            {
                if (length == 1)
                {
                    txtI2cActionLog.AppendText("Write,0x" + devAddr.ToString("X2") + ",0x" + regAddr.ToString("X2") + ",0x" + data[0].ToString("X2"));
                    txtI2cActionLog.AppendText(Environment.NewLine);
                }
                else
                {
                    txtI2cActionLog.AppendText("WriteMulti,0x" + devAddr.ToString("X2") + ",0x" + regAddr.ToString("X2") + ",0x" + length.ToString("X2"));
                    for (i = 0; i < length; i++)
                        txtI2cActionLog.AppendText(",0x" + data[i].ToString("X2"));
                    txtI2cActionLog.AppendText(Environment.NewLine);
                }
            }
            return rv;
        }

        private int _I2cMasterConnect()
        {
            if (i2cMaster.ConnectApi(100) < 0)
                return -1;

            chkConnected.Checked = true;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            return 0;
        }

        private int _I2cMasterDisconnect()
        {
            if (i2cMaster.DisconnectApi() < 0)
                return -1;

            chkConnected.Checked = false;
            return 0;
        }

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[] { 0xaa };
            if (i2cMaster.WriteApi(80, 127, 1, data) < 0)
                return -1;

            data[0] = mode;
            if (i2cMaster.WriteApi(80, 164, 1, data) < 0)
                return -1;
            return 0;
        }

        private int _WriteModulePassword()
        {
            byte[] data;
            data = Encoding.Default.GetBytes(txtPassword.Text);

            if (i2cMaster.WriteApi(80, 123, 4, data) < 0)
                return -1;

            return 0;
        }

        private void chkConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConnected.Checked == true)
            {
                if (_I2cMasterConnect() < 0)
                    return;
                _WriteModulePassword();
            }
            else
                _I2cMasterDisconnect();
        }

        private void btnClearI2cActionLog_Click(object sender, EventArgs e)
        {
            txtI2cActionLog.Text = "";
        }

        private void btnClearActionLog_Click(object sender, EventArgs e)
        {
            txtI2cActionLog.Text = "";
        }
    }

    // create a class for initial all the comboBox in the usercontrol
    public class ComboboxItem
    {
        public string Text { get; set; }

        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }

    }
}

