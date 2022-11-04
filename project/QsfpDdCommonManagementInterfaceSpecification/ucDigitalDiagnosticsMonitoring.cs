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
    public partial class ucDigitalDiagnosticsMonitoring : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int WritePasswordCB();

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private WritePasswordCB writePasswordCB = null;

        public ucDigitalDiagnosticsMonitoring()
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

        public int SetWritePasswordCBApi(WritePasswordCB cb)
        {
            if (cb == null)
                return -1;

            writePasswordCB = new WritePasswordCB(cb);

            return 0;
        }

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[] { 32 };

            if (i2cWriteCB == null)
                return -1;

            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            data[0] = mode;

            if (i2cWriteCB(80, 164, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _RxPowerRead()
        {
            byte[] data = new byte[16];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float power;
            int tmp;

            tbRxPower1.Text = tbRxPower2.Text = tbRxPower3.Text = tbRxPower4.Text = tbRxPower5.Text = tbRxPower6.Text =
                tbRxPower7.Text = tbRxPower8.Text = "";

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;
            
            data[0] = 0x00; //bank
            data[1] = 0x11; //page
            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            if (i2cReadCB(80, 149, 4, data) != 4)
                return -1;

            if ((data[0] & 0x80) != 0)
                cbRxPower8HighAlarm.Checked = true;
            else
                cbRxPower8HighAlarm.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbRxPower7HighAlarm.Checked = true;
            else
                cbRxPower7HighAlarm.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbRxPower6HighAlarm.Checked = true;
            else
                cbRxPower6HighAlarm.Checked = false;

            if ((data[0] & 0x10) != 0)
                cbRxPower5HighAlarm.Checked = true;
            else
                cbRxPower5HighAlarm.Checked = false;

            if ((data[0] & 0x08) != 0)
                cbRxPower4HighAlarm.Checked = true;
            else
                cbRxPower4HighAlarm.Checked = false;

            if ((data[0] & 0x04) != 0)
                cbRxPower3HighAlarm.Checked = true;
            else
                cbRxPower3HighAlarm.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbRxPower2HighAlarm.Checked = true;
            else
                cbRxPower2HighAlarm.Checked = false;

            if ((data[0] & 0x01) != 0)
                cbRxPower1HighAlarm.Checked = true;
            else
                cbRxPower1HighAlarm.Checked = false;

            if ((data[1] & 0x80) != 0)
                cbRxPower8LowAlarm.Checked = true;
            else
                cbRxPower8LowAlarm.Checked = false;

            if ((data[1] & 0x40) != 0)
                cbRxPower7LowAlarm.Checked = true;
            else
                cbRxPower7LowAlarm.Checked = false;

            if ((data[1] & 0x20) != 0)
                cbRxPower6LowAlarm.Checked = true;
            else
                cbRxPower6LowAlarm.Checked = false;

            if ((data[1] & 0x10) != 0)
                cbRxPower5LowAlarm.Checked = true;
            else
                cbRxPower5LowAlarm.Checked = false;

            if ((data[1] & 0x08) != 0)
                cbRxPower4LowAlarm.Checked = true;
            else
                cbRxPower4LowAlarm.Checked = false;

            if ((data[1] & 0x04) != 0)
                cbRxPower3LowAlarm.Checked = true;
            else
                cbRxPower3LowAlarm.Checked = false;

            if ((data[1] & 0x02) != 0)
                cbRxPower2LowAlarm.Checked = true;
            else
                cbRxPower2LowAlarm.Checked = false;

            if ((data[1] & 0x01) != 0)
                cbRxPower1LowAlarm.Checked = true;
            else
                cbRxPower1LowAlarm.Checked = false;

            if ((data[2] & 0x80) != 0)
                cbRxPower8HighWarning.Checked = true;
            else
                cbRxPower8HighWarning.Checked = false;

            if ((data[2] & 0x40) != 0)
                cbRxPower7HighWarning.Checked = true;
            else
                cbRxPower7HighWarning.Checked = false;

            if ((data[2] & 0x20) != 0)
                cbRxPower6HighWarning.Checked = true;
            else
                cbRxPower6HighWarning.Checked = false;

            if ((data[2] & 0x10) != 0)
                cbRxPower5HighWarning.Checked = true;
            else
                cbRxPower5HighWarning.Checked = false;

            if ((data[2] & 0x08) != 0)
                cbRxPower4HighWarning.Checked = true;
            else
                cbRxPower4HighWarning.Checked = false;

            if ((data[2] & 0x04) != 0)
                cbRxPower3HighWarning.Checked = true;
            else
                cbRxPower3HighWarning.Checked = false;

            if ((data[2] & 0x02) != 0)
                cbRxPower2HighWarning.Checked = true;
            else
                cbRxPower2HighWarning.Checked = false;

            if ((data[2] & 0x01) != 0)
                cbRxPower1HighWarning.Checked = true;
            else
                cbRxPower1HighWarning.Checked = false;

            if ((data[3] & 0x80) != 0)
                cbRxPower8LowWarning.Checked = true;
            else
                cbRxPower8LowWarning.Checked = false;

            if ((data[3] & 0x40) != 0)
                cbRxPower7LowWarning.Checked = true;
            else
                cbRxPower7LowWarning.Checked = false;

            if ((data[3] & 0x20) != 0)
                cbRxPower6LowWarning.Checked = true;
            else
                cbRxPower6LowWarning.Checked = false;

            if ((data[3] & 0x10) != 0)
                cbRxPower5LowWarning.Checked = true;
            else
                cbRxPower5LowWarning.Checked = false;

            if ((data[3] & 0x08) != 0)
                cbRxPower4LowWarning.Checked = true;
            else
                cbRxPower4LowWarning.Checked = false;

            if ((data[3] & 0x04) != 0)
                cbRxPower3LowWarning.Checked = true;
            else
                cbRxPower3LowWarning.Checked = false;

            if ((data[3] & 0x02) != 0)
                cbRxPower2LowWarning.Checked = true;
            else
                cbRxPower2LowWarning.Checked = false;

            if ((data[3] & 0x01) != 0)
                cbRxPower1LowWarning.Checked = true;
            else
                cbRxPower1LowWarning.Checked = false;

            data[0] = 0x00; //bank
            data[1] = 0x11; //page
            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            if (i2cReadCB(80, 186, 16, data) != 16)
                return -1;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbRxPower1.Text = power.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbRxPower2.Text = power.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbRxPower3.Text = power.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbRxPower4.Text = power.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 8, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbRxPower5.Text = power.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 10, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbRxPower6.Text = power.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 12, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbRxPower7.Text = power.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 14, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbRxPower8.Text = power.ToString("#0.0");

            data[0] = 0x00; //Bank
            data[1] = 0x02; //Page
            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            if (i2cReadCB(80, 192, 8, data) != 8)
                return -1;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbRxPowerHighAlarmThreshold.Text = power.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbRxPowerLowAlarmThreshold.Text = power.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbRxPowerHighWarningThreshold.Text = power.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbRxPowerLowWarningThreshold.Text = power.ToString("#0.0");

            data[0] = 0x00; //Bank
            data[1] = 0x10; //Page
            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            if (i2cReadCB(80, 228, 4, data) != 4)
                return -1;

            if ((data[0] & 0x80) != 0)
                cbRxPower8HighAlarmMask.Checked = true;
            else
                cbRxPower8HighAlarmMask.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbRxPower7HighAlarmMask.Checked = true;
            else
                cbRxPower7HighAlarmMask.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbRxPower6HighAlarmMask.Checked = true;
            else
                cbRxPower6HighAlarmMask.Checked = false;

            if ((data[0] & 0x10) != 0)
                cbRxPower5HighAlarmMask.Checked = true;
            else
                cbRxPower5HighAlarmMask.Checked = false;

            if ((data[0] & 0x08) != 0)
                cbRxPower4HighAlarmMask.Checked = true;
            else
                cbRxPower4HighAlarmMask.Checked = false;

            if ((data[0] & 0x04) != 0)
                cbRxPower3HighAlarmMask.Checked = true;
            else
                cbRxPower3HighAlarmMask.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbRxPower2HighAlarmMask.Checked = true;
            else
                cbRxPower2HighAlarmMask.Checked = false;

            if ((data[0] & 0x01) != 0)
                cbRxPower1HighAlarmMask.Checked = true;
            else
                cbRxPower1HighAlarmMask.Checked = false;

            if ((data[1] & 0x80) != 0)
                cbRxPower8LowAlarmMask.Checked = true;
            else
                cbRxPower8LowAlarmMask.Checked = false;

            if ((data[1] & 0x40) != 0)
                cbRxPower7LowAlarmMask.Checked = true;
            else
                cbRxPower7LowAlarmMask.Checked = false;

            if ((data[1] & 0x20) != 0)
                cbRxPower6LowAlarmMask.Checked = true;
            else
                cbRxPower6LowAlarmMask.Checked = false;

            if ((data[1] & 0x10) != 0)
                cbRxPower5LowAlarmMask.Checked = true;
            else
                cbRxPower5LowAlarmMask.Checked = false;

            if ((data[1] & 0x08) != 0)
                cbRxPower4LowAlarmMask.Checked = true;
            else
                cbRxPower4LowAlarmMask.Checked = false;

            if ((data[1] & 0x04) != 0)
                cbRxPower3LowAlarmMask.Checked = true;
            else
                cbRxPower3LowAlarmMask.Checked = false;

            if ((data[1] & 0x02) != 0)
                cbRxPower2LowAlarmMask.Checked = true;
            else
                cbRxPower2LowAlarmMask.Checked = false;

            if ((data[1] & 0x01) != 0)
                cbRxPower1LowAlarmMask.Checked = true;
            else
                cbRxPower1LowAlarmMask.Checked = false;

            if ((data[2] & 0x80) != 0)
                cbRxPower8HighWarningMask.Checked = true;
            else
                cbRxPower8HighWarningMask.Checked = false;

            if ((data[2] & 0x40) != 0)
                cbRxPower7HighWarningMask.Checked = true;
            else
                cbRxPower7HighWarningMask.Checked = false;

            if ((data[2] & 0x20) != 0)
                cbRxPower6HighWarningMask.Checked = true;
            else
                cbRxPower6HighWarningMask.Checked = false;

            if ((data[2] & 0x10) != 0)
                cbRxPower5HighWarningMask.Checked = true;
            else
                cbRxPower5HighWarningMask.Checked = false;

            if ((data[2] & 0x08) != 0)
                cbRxPower4HighWarningMask.Checked = true;
            else
                cbRxPower4HighWarningMask.Checked = false;

            if ((data[2] & 0x04) != 0)
                cbRxPower3HighWarningMask.Checked = true;
            else
                cbRxPower3HighWarningMask.Checked = false;

            if ((data[2] & 0x02) != 0)
                cbRxPower2HighWarningMask.Checked = true;
            else
                cbRxPower2HighWarningMask.Checked = false;

            if ((data[2] & 0x01) != 0)
                cbRxPower1HighWarningMask.Checked = true;
            else
                cbRxPower1HighWarningMask.Checked = false;

            if ((data[3] & 0x80) != 0)
                cbRxPower8LowWarningMask.Checked = true;
            else
                cbRxPower8LowWarningMask.Checked = false;

            if ((data[3] & 0x40) != 0)
                cbRxPower7LowWarningMask.Checked = true;
            else
                cbRxPower7LowWarningMask.Checked = false;

            if ((data[3] & 0x20) != 0)
                cbRxPower6LowWarningMask.Checked = true;
            else
                cbRxPower6LowWarningMask.Checked = false;

            if ((data[3] & 0x10) != 0)
                cbRxPower5LowWarningMask.Checked = true;
            else
                cbRxPower5LowWarningMask.Checked = false;

            if ((data[3] & 0x08) != 0)
                cbRxPower4LowWarningMask.Checked = true;
            else
                cbRxPower4LowWarningMask.Checked = false;

            if ((data[3] & 0x04) != 0)
                cbRxPower3LowWarningMask.Checked = true;
            else
                cbRxPower3LowWarningMask.Checked = false;

            if ((data[3] & 0x02) != 0)
                cbRxPower2LowWarningMask.Checked = true;
            else
                cbRxPower2LowWarningMask.Checked = false;

            if ((data[3] & 0x01) != 0)
                cbRxPower1LowWarningMask.Checked = true;
            else
                cbRxPower1LowWarningMask.Checked = false;

            return 0;
        }

        private void bRxPowerRead_Click(object sender, EventArgs e)
        {
            if (_RxPowerRead() < 0)
                return;
        }
    }
}
