using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using I2cMasterInterface;

namespace Rt145Rt146Config
{
    public partial class FRt145Rt146Config : Form
    {
        private I2cMaster i2cMaster = new I2cMaster();

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

        private int _I2cMasterConnect()
        {
            if (i2cMaster.ConnectApi(100) < 0)
                return -1;

            cbConnected.Checked = true;
            if (_WriteModulePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

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
            if (i2cMaster.connected == false)
            {
                if (_I2cMasterConnect() < 0)
                    return -1;
            }

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            rv = i2cMaster.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0)
            {
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

            if (i2cMaster.connected == false)
            {
                if (_I2cMasterConnect() < 0)
                    return -1;
            }

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            rv = i2cMaster.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0)
            {
                MessageBox.Show("QSFP+ module no response!!");
                _I2cMasterDisconnect();
            }

            return rv;
        }

        private int _WriteModulePassword()
        {
            byte[] data;

            data = Encoding.Default.GetBytes(tbPassword.Text);

            if (i2cMaster.WriteApi(80, 123, 4, data) < 0)
                return -1;

            return 0;
        }


        public FRt145Rt146Config()
        {
            InitializeComponent();
            if (ucRt145Config.SetI2cReadCBApi(_I2cRead) < 0)
            {
                MessageBox.Show("ucRt145Config.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucRt145Config.SetI2cWriteCBApi(_I2cWrite) < 0)
            {
                MessageBox.Show("ucRt145Config.SetI2cWriteCBApi() faile Error!!");
                return;
            }

            if (ucRt146Config.SetI2cReadCBApi(_I2cRead) < 0)
            {
                MessageBox.Show("ucRt145Config.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucRt146Config.SetI2cWriteCBApi(_I2cWrite) < 0)
            {
                MessageBox.Show("ucRt145Config.SetI2cWriteCBApi() faile Error!!");
                return;
            }
        }

        private void cbConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbConnected.Checked == true)
            {
                if (_I2cMasterConnect() < 0)
                    return;
                _WriteModulePassword();
            }
            else
                _I2cMasterDisconnect();
        }
    }

    public class ComboBoxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public override string ToString()
        {
            return Text;
        }

    }
}
