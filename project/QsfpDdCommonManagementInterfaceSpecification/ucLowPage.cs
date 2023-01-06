using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QsfpDdCommonManagementInterfaceSpecification
{
    public partial class ucLowPage : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;

        public ucLowPage()
        {
            InitializeComponent();
        }

        public int SetI2cReadCBApi(I2cReadCB cb)
        {
            if (cb == null)
                return -1;

            i2cReadCB = new I2cReadCB(cb);

            return 0;
        }

        public int SetI2cWriteCBApi(I2cWriteCB cb)
        {
            if (cb == null)
                return -1;

            i2cWriteCB = new I2cWriteCB(cb);

            return 0;
        }

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[] { 0x00, 0xBB };

            if (i2cWriteCB == null)
                return -1;

            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            data[0] = mode;

            if (i2cWriteCB(80, 164, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr122()
        {
            byte[] data;

            if (i2cWriteCB == null)
                return -1;

            data = Encoding.Default.GetBytes(tbPassword.Text);

            if (i2cWriteCB(80, 122, 4, data) < 0)
                return -1;

            return 0;
        }

        public int WritePassword()
        {
            return _WriteAddr122();
        }

        private void bRead_Click(object sender, EventArgs e)
        {

        }
    }
}
