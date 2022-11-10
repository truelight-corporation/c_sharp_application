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

        private int _RxPowerWrite()
        {
            byte[] data = new byte[16];
            byte[] bATmp;
            float power;
            ushort uSTmp;
            
            
            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (i2cWriteCB == null)
                return -1;

            if (writePasswordCB == null)
                return -1;

            if (writePasswordCB() < 0)
                return -1;

            data[0] = 0x00; //Bank
            data[1] = 0x02; //Page
            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            try {
                power = Convert.ToSingle(tbRxPowerHighAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            power *= 10;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[0] = bATmp[1];
            data[1] = bATmp[0];

            try {
                power = Convert.ToSingle(tbRxPowerLowAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            power *= 10;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[2] = bATmp[1];
            data[3] = bATmp[0];

            try {
                power = Convert.ToSingle(tbRxPowerHighWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            power *= 10;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[4] = bATmp[1];
            data[5] = bATmp[0];

            try {
                power = Convert.ToSingle(tbRxPowerLowWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            power *= 10;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[6] = bATmp[1];
            data[7] = bATmp[0];

            if (i2cWriteCB(80, 192, 8, data) < 0)
                return -1;

            data[0] = 0x00; //Bank
            data[1] = 0x10; //Page
            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            data[0] = data[1] = data[2] = data[3] = 0;
            if (cbRxPower8HighAlarmMask.Checked == true)
                data[0] |= 0x80;
            if (cbRxPower7HighAlarmMask.Checked == true)
                data[0] |= 0x40;
            if (cbRxPower6HighAlarmMask.Checked == true)
                data[0] |= 0x20;
            if (cbRxPower5HighAlarmMask.Checked == true)
                data[0] |= 0x10;
            if (cbRxPower4HighAlarmMask.Checked == true)
                data[0] |= 0x08;
            if (cbRxPower3HighAlarmMask.Checked == true)
                data[0] |= 0x04;
            if (cbRxPower2HighAlarmMask.Checked == true)
                data[0] |= 0x02;
            if (cbRxPower1HighAlarmMask.Checked == true)
                data[0] |= 0x01;
            if (cbRxPower8LowAlarmMask.Checked == true)
                data[1] |= 0x80;
            if (cbRxPower7LowAlarmMask.Checked == true)
                data[1] |= 0x40;
            if (cbRxPower6LowAlarmMask.Checked == true)
                data[1] |= 0x20;
            if (cbRxPower5LowAlarmMask.Checked == true)
                data[1] |= 0x10;
            if (cbRxPower4LowAlarmMask.Checked == true)
                data[1] |= 0x08;
            if (cbRxPower3LowAlarmMask.Checked == true)
                data[1] |= 0x04;
            if (cbRxPower2LowAlarmMask.Checked == true)
                data[1] |= 0x02;
            if (cbRxPower1LowAlarmMask.Checked == true)
                data[1] |= 0x01;
            if (cbRxPower8HighWarningMask.Checked == true)
                data[2] |= 0x80;
            if (cbRxPower7HighWarningMask.Checked == true)
                data[2] |= 0x40;
            if (cbRxPower6HighWarningMask.Checked == true)
                data[2] |= 0x20;
            if (cbRxPower5HighWarningMask.Checked == true)
                data[2] |= 0x10;
            if (cbRxPower4HighWarningMask.Checked == true)
                data[2] |= 0x08;
            if (cbRxPower3HighWarningMask.Checked == true)
                data[2] |= 0x04;
            if (cbRxPower2HighWarningMask.Checked == true)
                data[2] |= 0x02;
            if (cbRxPower1HighWarningMask.Checked == true)
                data[2] |= 0x01;
            if (cbRxPower8LowWarningMask.Checked == true)
                data[3] |= 0x80;
            if (cbRxPower7LowWarningMask.Checked == true)
                data[3] |= 0x40;
            if (cbRxPower6LowWarningMask.Checked == true)
                data[3] |= 0x20;
            if (cbRxPower5LowWarningMask.Checked == true)
                data[3] |= 0x10;
            if (cbRxPower4LowWarningMask.Checked == true)
                data[3] |= 0x08;
            if (cbRxPower3LowWarningMask.Checked == true)
                data[3] |= 0x04;
            if (cbRxPower2LowWarningMask.Checked == true)
                data[3] |= 0x02;
            if (cbRxPower1LowWarningMask.Checked == true)
                data[3] |= 0x01;

            if (i2cWriteCB(80, 228, 4, data) < 0)
                return -1;

            return 0;
        }

        private void bRxPowerWrite_Click(object sender, EventArgs e)
        {
            if (_RxPowerWrite() < 0)
                return;
        }

        private int _TxBiasRead()
        {
            byte[] data = new byte[16];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float bias;
            int tmp;

            tbTxBias1.Text = tbTxBias2.Text = tbTxBias3.Text = tbTxBias4.Text = tbTxBias5.Text = tbTxBias6.Text =
                tbTxBias7.Text = tbTxBias8.Text = "";

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            data[0] = 0x00; //bank
            data[1] = 0x11; //page
            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            if (i2cReadCB(80, 143, 4, data) != 4)
                return -1;

            if ((data[0] & 0x80) != 0)
                cbTxBias8HighAlarm.Checked = true;
            else
                cbTxBias8HighAlarm.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbTxBias7HighAlarm.Checked = true;
            else
                cbTxBias7HighAlarm.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbTxBias6HighAlarm.Checked = true;
            else
                cbTxBias6HighAlarm.Checked = false;

            if ((data[0] & 0x10) != 0)
                cbTxBias5HighAlarm.Checked = true;
            else
                cbTxBias5HighAlarm.Checked = false;

            if ((data[0] & 0x08) != 0)
                cbTxBias4HighAlarm.Checked = true;
            else
                cbTxBias4HighAlarm.Checked = false;

            if ((data[0] & 0x04) != 0)
                cbTxBias3HighAlarm.Checked = true;
            else
                cbTxBias3HighAlarm.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbTxBias2HighAlarm.Checked = true;
            else
                cbTxBias2HighAlarm.Checked = false;

            if ((data[0] & 0x01) != 0)
                cbTxBias1HighAlarm.Checked = true;
            else
                cbTxBias1HighAlarm.Checked = false;

            if ((data[1] & 0x80) != 0)
                cbTxBias8LowAlarm.Checked = true;
            else
                cbTxBias8LowAlarm.Checked = false;

            if ((data[1] & 0x40) != 0)
                cbTxBias7LowAlarm.Checked = true;
            else
                cbTxBias7LowAlarm.Checked = false;

            if ((data[1] & 0x20) != 0)
                cbTxBias6LowAlarm.Checked = true;
            else
                cbTxBias6LowAlarm.Checked = false;

            if ((data[1] & 0x10) != 0)
                cbTxBias5LowAlarm.Checked = true;
            else
                cbTxBias5LowAlarm.Checked = false;

            if ((data[1] & 0x08) != 0)
                cbTxBias4LowAlarm.Checked = true;
            else
                cbTxBias4LowAlarm.Checked = false;

            if ((data[1] & 0x04) != 0)
                cbTxBias3LowAlarm.Checked = true;
            else
                cbTxBias3LowAlarm.Checked = false;

            if ((data[1] & 0x02) != 0)
                cbTxBias2LowAlarm.Checked = true;
            else
                cbTxBias2LowAlarm.Checked = false;

            if ((data[1] & 0x01) != 0)
                cbTxBias1LowAlarm.Checked = true;
            else
                cbTxBias1LowAlarm.Checked = false;

            if ((data[2] & 0x80) != 0)
                cbTxBias8HighWarning.Checked = true;
            else
                cbTxBias8HighWarning.Checked = false;

            if ((data[2] & 0x40) != 0)
                cbTxBias7HighWarning.Checked = true;
            else
                cbTxBias7HighWarning.Checked = false;

            if ((data[2] & 0x20) != 0)
                cbTxBias6HighWarning.Checked = true;
            else
                cbTxBias6HighWarning.Checked = false;

            if ((data[2] & 0x10) != 0)
                cbTxBias5HighWarning.Checked = true;
            else
                cbTxBias5HighWarning.Checked = false;

            if ((data[2] & 0x08) != 0)
                cbTxBias4HighWarning.Checked = true;
            else
                cbTxBias4HighWarning.Checked = false;

            if ((data[2] & 0x04) != 0)
                cbTxBias3HighWarning.Checked = true;
            else
                cbTxBias3HighWarning.Checked = false;

            if ((data[2] & 0x02) != 0)
                cbTxBias2HighWarning.Checked = true;
            else
                cbTxBias2HighWarning.Checked = false;

            if ((data[2] & 0x01) != 0)
                cbTxBias1HighWarning.Checked = true;
            else
                cbTxBias1HighWarning.Checked = false;

            if ((data[3] & 0x80) != 0)
                cbTxBias8LowWarning.Checked = true;
            else
                cbTxBias8LowWarning.Checked = false;

            if ((data[3] & 0x40) != 0)
                cbTxBias7LowWarning.Checked = true;
            else
                cbTxBias7LowWarning.Checked = false;

            if ((data[3] & 0x20) != 0)
                cbTxBias6LowWarning.Checked = true;
            else
                cbTxBias6LowWarning.Checked = false;

            if ((data[3] & 0x10) != 0)
                cbTxBias5LowWarning.Checked = true;
            else
                cbTxBias5LowWarning.Checked = false;

            if ((data[3] & 0x08) != 0)
                cbTxBias4LowWarning.Checked = true;
            else
                cbTxBias4LowWarning.Checked = false;

            if ((data[3] & 0x04) != 0)
                cbTxBias3LowWarning.Checked = true;
            else
                cbTxBias3LowWarning.Checked = false;

            if ((data[3] & 0x02) != 0)
                cbTxBias2LowWarning.Checked = true;
            else
                cbTxBias2LowWarning.Checked = false;

            if ((data[3] & 0x01) != 0)
                cbTxBias1LowWarning.Checked = true;
            else
                cbTxBias1LowWarning.Checked = false;

            data[0] = 0x00; //bank
            data[1] = 0x11; //page
            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            if (i2cReadCB(80, 170, 16, data) != 16)
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
            bias = tmp;
            bias = bias / 500;
            tbTxBias1.Text = bias.ToString("#0.000");

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            bias = tmp;
            bias = bias / 500;
            tbTxBias2.Text = bias.ToString("#0.000");

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            bias = tmp;
            bias = bias / 500;
            tbTxBias3.Text = bias.ToString("#0.000");

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            bias = tmp;
            bias = bias / 500;
            tbTxBias4.Text = bias.ToString("#0.000");

            try {
                Buffer.BlockCopy(data, 8, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            bias = tmp;
            bias = bias / 500;
            tbTxBias5.Text = bias.ToString("#0.000");

            try {
                Buffer.BlockCopy(data, 10, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            bias = tmp;
            bias = bias / 500;
            tbTxBias6.Text = bias.ToString("#0.000");

            try {
                Buffer.BlockCopy(data, 12, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            bias = tmp;
            bias = bias / 500;
            tbTxBias7.Text = bias.ToString("#0.000");

            try {
                Buffer.BlockCopy(data, 14, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            bias = tmp;
            bias = bias / 500;
            tbTxBias8.Text = bias.ToString("#0.000");

            data[0] = 0x00; //bank
            data[1] = 0x02; //page
            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            if (i2cReadCB(80, 184, 8, data) != 8)
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
            bias = tmp;
            bias = bias / 500;
            tbTxBiasHighAlarmThreshold.Text = bias.ToString("#0.000");

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            bias = tmp;
            bias = bias / 500;
            tbTxBiasLowAlarmThreshold.Text = bias.ToString("#0.000");

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            bias = tmp;
            bias = bias / 500;
            tbTxBiasHighWarningThreshold.Text = bias.ToString("#0.000");

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            bias = tmp;
            bias = bias / 500;
            tbTxBiasLowWarningThreshold.Text = bias.ToString("#0.000");

            data[0] = 0x00; //bank
            data[1] = 0x10; //page
            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            if (i2cReadCB(80, 222, 4, data) != 4)
                return -1;

            if ((data[0] & 0x80) != 0)
                cbTxBias8HighAlarmMask.Checked = true;
            else
                cbTxBias8HighAlarmMask.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbTxBias7HighAlarmMask.Checked = true;
            else
                cbTxBias7HighAlarmMask.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbTxBias6HighAlarmMask.Checked = true;
            else
                cbTxBias6HighAlarmMask.Checked = false;

            if ((data[0] & 0x10) != 0)
                cbTxBias5HighAlarmMask.Checked = true;
            else
                cbTxBias5HighAlarmMask.Checked = false;

            if ((data[0] & 0x08) != 0)
                cbTxBias4HighAlarmMask.Checked = true;
            else
                cbTxBias4HighAlarmMask.Checked = false;

            if ((data[0] & 0x04) != 0)
                cbTxBias3HighAlarmMask.Checked = true;
            else
                cbTxBias3HighAlarmMask.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbTxBias2HighAlarmMask.Checked = true;
            else
                cbTxBias2HighAlarmMask.Checked = false;

            if ((data[0] & 0x01) != 0)
                cbTxBias1HighAlarmMask.Checked = true;
            else
                cbTxBias1HighAlarmMask.Checked = false;

            if ((data[1] & 0x80) != 0)
                cbTxBias8LowAlarmMask.Checked = true;
            else
                cbTxBias8LowAlarmMask.Checked = false;

            if ((data[1] & 0x40) != 0)
                cbTxBias7LowAlarmMask.Checked = true;
            else
                cbTxBias7LowAlarmMask.Checked = false;

            if ((data[1] & 0x20) != 0)
                cbTxBias6LowAlarmMask.Checked = true;
            else
                cbTxBias6LowAlarmMask.Checked = false;

            if ((data[1] & 0x10) != 0)
                cbTxBias5LowAlarmMask.Checked = true;
            else
                cbTxBias5LowAlarmMask.Checked = false;

            if ((data[1] & 0x08) != 0)
                cbTxBias4LowAlarmMask.Checked = true;
            else
                cbTxBias4LowAlarmMask.Checked = false;

            if ((data[1] & 0x04) != 0)
                cbTxBias3LowAlarmMask.Checked = true;
            else
                cbTxBias3LowAlarmMask.Checked = false;

            if ((data[1] & 0x02) != 0)
                cbTxBias2LowAlarmMask.Checked = true;
            else
                cbTxBias2LowAlarmMask.Checked = false;

            if ((data[1] & 0x01) != 0)
                cbTxBias1LowAlarmMask.Checked = true;
            else
                cbTxBias1LowAlarmMask.Checked = false;

            if ((data[2] & 0x80) != 0)
                cbTxBias8HighWarningMask.Checked = true;
            else
                cbTxBias8HighWarningMask.Checked = false;

            if ((data[2] & 0x40) != 0)
                cbTxBias7HighWarningMask.Checked = true;
            else
                cbTxBias7HighWarningMask.Checked = false;

            if ((data[2] & 0x20) != 0)
                cbTxBias6HighWarningMask.Checked = true;
            else
                cbTxBias6HighWarningMask.Checked = false;

            if ((data[2] & 0x10) != 0)
                cbTxBias5HighWarningMask.Checked = true;
            else
                cbTxBias5HighWarningMask.Checked = false;

            if ((data[2] & 0x08) != 0)
                cbTxBias4HighWarningMask.Checked = true;
            else
                cbTxBias4HighWarningMask.Checked = false;

            if ((data[2] & 0x04) != 0)
                cbTxBias3HighWarningMask.Checked = true;
            else
                cbTxBias3HighWarningMask.Checked = false;

            if ((data[2] & 0x02) != 0)
                cbTxBias2HighWarningMask.Checked = true;
            else
                cbTxBias2HighWarningMask.Checked = false;

            if ((data[2] & 0x01) != 0)
                cbTxBias1HighWarningMask.Checked = true;
            else
                cbTxBias1HighWarningMask.Checked = false;

            if ((data[3] & 0x80) != 0)
                cbTxBias8LowWarningMask.Checked = true;
            else
                cbTxBias8LowWarningMask.Checked = false;

            if ((data[3] & 0x40) != 0)
                cbTxBias7LowWarningMask.Checked = true;
            else
                cbTxBias7LowWarningMask.Checked = false;

            if ((data[3] & 0x20) != 0)
                cbTxBias6LowWarningMask.Checked = true;
            else
                cbTxBias6LowWarningMask.Checked = false;

            if ((data[3] & 0x10) != 0)
                cbTxBias5LowWarningMask.Checked = true;
            else
                cbTxBias5LowWarningMask.Checked = false;

            if ((data[3] & 0x08) != 0)
                cbTxBias4LowWarningMask.Checked = true;
            else
                cbTxBias4LowWarningMask.Checked = false;

            if ((data[3] & 0x04) != 0)
                cbTxBias3LowWarningMask.Checked = true;
            else
                cbTxBias3LowWarningMask.Checked = false;

            if ((data[3] & 0x02) != 0)
                cbTxBias2LowWarningMask.Checked = true;
            else
                cbTxBias2LowWarningMask.Checked = false;

            if ((data[3] & 0x01) != 0)
                cbTxBias1LowWarningMask.Checked = true;
            else
                cbTxBias1LowWarningMask.Checked = false;

            return 0;
        }

        private void bTxBiasRead_Click(object sender, EventArgs e)
        {
            if (_TxBiasRead() < 0)
                return;
        }

        private int _TxBiasWrite()
        {
            byte[] data = new byte[8];
            byte[] bATmp;
            float power;
            ushort uSTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            if (writePasswordCB == null)
                return -1;

            if (writePasswordCB() < 0)
                return -1;

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0x00; //bank
            data[1] = 0x02; //page
            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            try {
                power = Convert.ToSingle(tbTxBiasHighAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            power *= 500;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[0] = bATmp[1];
            data[1] = bATmp[0];

            try {
                power = Convert.ToSingle(tbTxBiasLowAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            power *= 500;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[2] = bATmp[1];
            data[3] = bATmp[0];

            try {
                power = Convert.ToSingle(tbTxBiasHighWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            power *= 500;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[4] = bATmp[1];
            data[5] = bATmp[0];

            try {
                power = Convert.ToSingle(tbTxBiasLowWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            power *= 500;
            try {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[6] = bATmp[1];
            data[7] = bATmp[0];

            if (i2cWriteCB(80, 184, 8, data) < 0)
                return -1;

            data[0] = 0x00; //bank
            data[1] = 0x10; //page
            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            data[0] = data[1] = data[2] = data[3] = 0;
            if (cbTxBias8HighAlarmMask.Checked == true)
                data[0] |= 0x80;
            if (cbTxBias7HighAlarmMask.Checked == true)
                data[0] |= 0x40;
            if (cbTxBias6HighAlarmMask.Checked == true)
                data[0] |= 0x20;
            if (cbTxBias5HighAlarmMask.Checked == true)
                data[0] |= 0x10;
            if (cbTxBias4HighAlarmMask.Checked == true)
                data[0] |= 0x08;
            if (cbTxBias3HighAlarmMask.Checked == true)
                data[0] |= 0x04;
            if (cbTxBias2HighAlarmMask.Checked == true)
                data[0] |= 0x02;
            if (cbTxBias1HighAlarmMask.Checked == true)
                data[0] |= 0x01;
            if (cbTxBias8LowAlarmMask.Checked == true)
                data[1] |= 0x80;
            if (cbTxBias7LowAlarmMask.Checked == true)
                data[1] |= 0x40;
            if (cbTxBias6LowAlarmMask.Checked == true)
                data[1] |= 0x20;
            if (cbTxBias5LowAlarmMask.Checked == true)
                data[1] |= 0x10;
            if (cbTxBias4LowAlarmMask.Checked == true)
                data[1] |= 0x08;
            if (cbTxBias3LowAlarmMask.Checked == true)
                data[1] |= 0x04;
            if (cbTxBias2LowAlarmMask.Checked == true)
                data[1] |= 0x02;
            if (cbTxBias1LowAlarmMask.Checked == true)
                data[1] |= 0x01;
            if (cbTxBias8HighWarningMask.Checked == true)
                data[2] |= 0x80;
            if (cbTxBias7HighWarningMask.Checked == true)
                data[2] |= 0x40;
            if (cbTxBias6HighWarningMask.Checked == true)
                data[2] |= 0x20;
            if (cbTxBias5HighWarningMask.Checked == true)
                data[2] |= 0x10;
            if (cbTxBias4HighWarningMask.Checked == true)
                data[2] |= 0x08;
            if (cbTxBias3HighWarningMask.Checked == true)
                data[2] |= 0x04;
            if (cbTxBias2HighWarningMask.Checked == true)
                data[2] |= 0x02;
            if (cbTxBias1HighWarningMask.Checked == true)
                data[2] |= 0x01;
            if (cbTxBias8LowWarningMask.Checked == true)
                data[3] |= 0x80;
            if (cbTxBias7LowWarningMask.Checked == true)
                data[3] |= 0x40;
            if (cbTxBias6LowWarningMask.Checked == true)
                data[3] |= 0x20;
            if (cbTxBias5LowWarningMask.Checked == true)
                data[3] |= 0x10;
            if (cbTxBias4LowWarningMask.Checked == true)
                data[3] |= 0x08;
            if (cbTxBias3LowWarningMask.Checked == true)
                data[3] |= 0x04;
            if (cbTxBias2LowWarningMask.Checked == true)
                data[3] |= 0x02;
            if (cbTxBias1LowWarningMask.Checked == true)
                data[3] |= 0x01;

            if (i2cWriteCB(80, 222, 4, data) < 0)
                return -1;

            return 0;
        }

        private void bTxBiasWrite_Click(object sender, EventArgs e)
        {
            if (_TxBiasWrite() < 0)
                return;
        }

        private int _TemperatureRead()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float temperature;
            int tmp;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            tbTemperature.Text = "";

            if (i2cReadCB(80, 9, 1, data) != 1)
                return -1;

            if ((data[0] & 0x01) != 0)
                cbTemperatureHighAlarm.Checked = true;
            else
                cbTemperatureHighAlarm.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbTemperatureLowAlarm.Checked = true;
            else
                cbTemperatureLowAlarm.Checked = false;

            if ((data[0] & 0x04) != 0)
                cbTemperatureHighWarning.Checked = true;
            else
                cbTemperatureHighWarning.Checked = false;

            if ((data[0] & 0x08) != 0)
                cbTemperatureLowWarning.Checked = true;
            else
                cbTemperatureLowWarning.Checked = false;

            if (i2cReadCB(80, 14, 2, data) != 2)
                return -1;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            temperature = tmp;
            temperature = temperature / 256;
            tbTemperature.Text = temperature.ToString("#0.0");

            if (i2cReadCB(80, 32, 1, data) != 1)
                return -1;

            if ((data[0] & 0x01) != 0)
                cbTemperatureHighAlarmMask.Checked = true;
            else
                cbTemperatureHighAlarmMask.Checked = false;
            if ((data[0] & 0x02) != 0)
                cbTemperatureLowAlarmMask.Checked = true;
            else
                cbTemperatureLowAlarmMask.Checked = false;
            if ((data[0] & 0x04) != 0)
                cbTemperatureHighWarningMask.Checked = true;
            else
                cbTemperatureHighWarningMask.Checked = false;
            if ((data[0] & 0x08) != 0)
                cbTemperatureLowWarningMask.Checked = true;
            else
                cbTemperatureLowWarningMask.Checked = false;

            data[0] = 0x00; //bank
            data[1] = 0x02; //page
            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            if (i2cReadCB(80, 128, 8, data) != 8)
                return -1;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            temperature = tmp;
            temperature = temperature / 256;
            tbTemperatureHighAlarmThreshold.Text = temperature.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            temperature = tmp;
            temperature = temperature / 256;
            tbTemperatureLowAlarmThreshold.Text = temperature.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            temperature = tmp;
            temperature = temperature / 256;
            tbTemperatureHighWarningThreshold.Text = temperature.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToInt16(reverseData, 0);
            temperature = tmp;
            temperature = temperature / 256;
            tbTemperatureLowWarningThreshold.Text = temperature.ToString("#0.0");

            return 0;
        }

        private void bTemperatureRead_Click(object sender, EventArgs e)
        {
            if (_TemperatureRead() < 0)
                return;
        }

        private int _TemperatureWrite()
        {
            byte[] data = new byte[8];
            byte[] bATmp;
            float temperature;
            short sTmp;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            if (writePasswordCB == null)
                return -1;

            if (writePasswordCB() < 0)
                return -1;

            if (i2cReadCB(80, 32, 1, data) != 1)
                return -1;

            data[0] &= 0xF0;
            if (cbTemperatureHighAlarmMask.Checked == true)
                data[0] |= 0x01;
            if (cbTemperatureLowAlarmMask.Checked == true)
                data[0] |= 0x02;
            if (cbTemperatureHighWarningMask.Checked == true)
                data[0] |= 0x04;
            if (cbTemperatureLowWarningMask.Checked == true)
                data[0] |= 0x08;

            if (i2cWriteCB(80, 32, 1, data) < 0)
                return -1;

            data[0] = 0x00; //bank
            data[1] = 0x02; //page
            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            try {
                temperature = Convert.ToSingle(tbTemperatureHighAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                goto Error;
            }

            temperature *= 256;
            try {
                sTmp = Convert.ToInt16(temperature);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                goto Error;
            }

            bATmp = BitConverter.GetBytes(sTmp);
            data[0] = bATmp[1];
            data[1] = bATmp[0];

            try {
                temperature = Convert.ToSingle(tbTemperatureLowAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                goto Error;
            }

            temperature *= 256;
            try {
                sTmp = Convert.ToInt16(temperature);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                goto Error;
            }

            bATmp = BitConverter.GetBytes(sTmp);
            data[2] = bATmp[1];
            data[3] = bATmp[0];

            try {
                temperature = Convert.ToSingle(tbTemperatureHighWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                goto Error;
            }

            temperature *= 256;
            try {
                sTmp = Convert.ToInt16(temperature);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                goto Error;
            }

            bATmp = BitConverter.GetBytes(sTmp);
            data[4] = bATmp[1];
            data[5] = bATmp[0];

            try {
                temperature = Convert.ToSingle(tbTemperatureLowWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                goto Error;
            }

            temperature *= 256;
            try {
                sTmp = Convert.ToInt16(temperature);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                goto Error;
            }

            bATmp = BitConverter.GetBytes(sTmp);
            data[6] = bATmp[1];
            data[7] = bATmp[0];

            if (i2cWriteCB(80, 128, 8, data) < 0)
                return -1;

            return 0;

        Error:
            return -1;
        }

        private void bTemperatureWrite_Click(object sender, EventArgs e)
        {
            if (_TemperatureWrite() < 0)
                return;
        }

        private int _VccRead()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float vcc;
            int tmp;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            tbVcc.Text = "";

            if (i2cReadCB(80, 9, 1, data) != 1)
                return -1;

            if ((data[0] & 0x10) != 0)
                cbVccHighAlarm.Checked = true;
            else
                cbVccHighAlarm.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbVccLowAlarm.Checked = true;
            else
                cbVccLowAlarm.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbVccHighWarning.Checked = true;
            else
                cbVccHighWarning.Checked = false;

            if ((data[0] & 0x80) != 0)
                cbVccLowWarning.Checked = true;
            else
                cbVccLowWarning.Checked = false;

            if (i2cReadCB(80, 16, 2, data) != 2)
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
            vcc = tmp;
            vcc = vcc / 10000;
            tbVcc.Text = vcc.ToString("#0.0000");

            if (i2cReadCB(80, 32, 1, data) != 1)
                return -1;

            if ((data[0] & 0x10) != 0)
                cbVccHighAlarmMask.Checked = true;
            else
                cbVccHighAlarmMask.Checked = false;
            if ((data[0] & 0x20) != 0)
                cbVccLowAlarmMask.Checked = true;
            else
                cbVccLowAlarmMask.Checked = false;
            if ((data[0] & 0x40) != 0)
                cbVccHighWarningMask.Checked = true;
            else
                cbVccHighWarningMask.Checked = false;
            if ((data[0] & 0x80) != 0)
                cbVccLowWarningMask.Checked = true;
            else
                cbVccLowWarningMask.Checked = false;

            data[0] = 0x00; //bank
            data[1] = 0x02; //page
            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            if (i2cReadCB(80, 136, 8, data) != 8)
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
            vcc = tmp;
            vcc = vcc / 10000;
            tbVccHighAlarmThreshold.Text = vcc.ToString("#0.0000");

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            vcc = tmp;
            vcc = vcc / 10000;
            tbVccLowAlarmThreshold.Text = vcc.ToString("#0.0000");

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            vcc = tmp;
            vcc = vcc / 10000;
            tbVccHighWarningThreshold.Text = vcc.ToString("#0.0000");

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            vcc = tmp;
            vcc = vcc / 10000;
            tbVccLowWarningThreshold.Text = vcc.ToString("#0.0000");

            return 0;
        }

        private void bVccRead_Click(object sender, EventArgs e)
        {
            if (_VccRead() < 0)
                return;
        }

        private int _VccWrite()
        {
            byte[] data = new byte[8];
            byte[] bATmp;
            float vcc;
            ushort uSTmp;

            if (i2cWriteCB == null)
                return -1;

            if (i2cReadCB == null)
                return -1;

            if (writePasswordCB == null)
                return -1;

            if (writePasswordCB() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (i2cReadCB(80, 32, 1, data) != 1)
                return -1;

            data[0] &= 0x0F;
            if (cbVccHighAlarmMask.Checked == true)
                data[0] |= 0x10;
            if (cbVccLowAlarmMask.Checked == true)
                data[0] |= 0x20;
            if (cbVccHighWarningMask.Checked == true)
                data[0] |= 0x40;
            if (cbVccLowWarningMask.Checked == true)
                data[0] |= 0x80;

            if (i2cWriteCB(80, 32, 1, data) < 0)
                return -1;

            data[0] = 0x00; //bank
            data[1] = 0x02; //page
            if (i2cWriteCB(80, 126, 2, data) < 0)
                return -1;

            try {
                vcc = Convert.ToSingle(tbVccHighAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            vcc *= 10000;
            try {
                uSTmp = Convert.ToUInt16(vcc);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[0] = bATmp[1];
            data[1] = bATmp[0];

            try {
                vcc = Convert.ToSingle(tbVccLowAlarmThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            vcc *= 10000;
            try {
                uSTmp = Convert.ToUInt16(vcc);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[2] = bATmp[1];
            data[3] = bATmp[0];

            try {
                vcc = Convert.ToSingle(tbVccHighWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            vcc *= 10000;
            try {
                uSTmp = Convert.ToUInt16(vcc);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[4] = bATmp[1];
            data[5] = bATmp[0];

            try {
                vcc = Convert.ToSingle(tbVccLowWarningThreshold.Text);
            }
            catch (Exception eTS) {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            vcc *= 10000;
            try {
                uSTmp = Convert.ToUInt16(vcc);
            }
            catch (Exception eTI) {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[6] = bATmp[1];
            data[7] = bATmp[0];

            if (i2cWriteCB(80, 136, 8, data) < 0)
                return -1;

            return 0;
        }

        private void bVccWrite_Click(object sender, EventArgs e)
        {
            if (_VccWrite() < 0)
                return;
        }
    }
}
