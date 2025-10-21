using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using I2cMasterInterface;

namespace QsfpDigitalDiagnosticMonitoring
{
    public partial class UCDigitalDiagnosticsMonitoring : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int WritePasswordCB();

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private WritePasswordCB writePasswordCB = null;

        public UCDigitalDiagnosticsMonitoring()
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

        private int _LosAndFaultRead()
        {
            byte[] data = new byte[3];

            if (i2cReadCB == null)
                return -1;

            if (i2cReadCB(80, 3, 3, data) != 3)
                return -1;

            if ((data[0] & 0x80) != 0)
                cbLosTx4.Checked = true;
            else
                cbLosTx4.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbLosTx3.Checked = true;
            else
                cbLosTx3.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbLosTx2.Checked = true;
            else
                cbLosTx2.Checked = false;

            if ((data[0] & 0x10) != 0)
                cbLosTx1.Checked = true;
            else
                cbLosTx1.Checked = false;

            if ((data[0] & 0x08) != 0)
                cbLosRx4.Checked = true;
            else
                cbLosRx4.Checked = false;

            if ((data[0] & 0x04) != 0)
                cbLosRx3.Checked = true;
            else
                cbLosRx3.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbLosRx2.Checked = true;
            else
                cbLosRx2.Checked = false;

            if ((data[0] & 0x01) != 0)
                cbLosRx1.Checked = true;
            else
                cbLosRx1.Checked = false;

            if ((data[1] & 0x80) != 0)
                cbAdaptEqFaultTx4.Checked = true;
            else
                cbAdaptEqFaultTx4.Checked = false;

            if ((data[1] & 0x40) != 0)
                cbAdaptEqFaultTx3.Checked = true;
            else
                cbAdaptEqFaultTx3.Checked = false;

            if ((data[1] & 0x20) != 0)
                cbAdaptEqFaultTx2.Checked = true;
            else
                cbAdaptEqFaultTx2.Checked = false;

            if ((data[1] & 0x10) != 0)
                cbAdaptEqFaultTx1.Checked = true;
            else
                cbAdaptEqFaultTx1.Checked = false;

            if ((data[1] & 0x08) != 0)
                cbFaultTx4.Checked = true;
            else
                cbFaultTx4.Checked = false;

            if ((data[1] & 0x04) != 0)
                cbFaultTx3.Checked = true;
            else
                cbFaultTx3.Checked = false;

            if ((data[1] & 0x02) != 0)
                cbFaultTx2.Checked = true;
            else
                cbFaultTx2.Checked = false;

            if ((data[1] & 0x01) != 0)
                cbFaultTx1.Checked = true;
            else
                cbFaultTx1.Checked = false;

            if ((data[2] & 0x80) != 0)
                cbLolTx4.Checked = true;
            else
                cbLolTx4.Checked = false;

            if ((data[2] & 0x40) != 0)
                cbLolTx3.Checked = true;
            else
                cbLolTx3.Checked = false;

            if ((data[2] & 0x20) != 0)
                cbLolTx2.Checked = true;
            else
                cbLolTx2.Checked = false;

            if ((data[2] & 0x10) != 0)
                cbLolTx1.Checked = true;
            else
                cbLolTx1.Checked = false;

            if ((data[2] & 0x08) != 0)
                cbLolRx4.Checked = true;
            else
                cbLolRx4.Checked = false;

            if ((data[2] & 0x04) != 0)
                cbLolRx3.Checked = true;
            else
                cbLolRx3.Checked = false;

            if ((data[2] & 0x02) != 0)
                cbLolRx2.Checked = true;
            else
                cbLolRx2.Checked = false;

            if ((data[2] & 0x01) != 0)
                cbLolRx1.Checked = true;
            else
                cbLolRx1.Checked = false;

            if (i2cReadCB(80, 100, 3, data) != 3)
                return -1;

            if ((data[0] & 0x80) != 0)
                cbLosTx4Mask.Checked = true;
            else
                cbLosTx4Mask.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbLosTx3Mask.Checked = true;
            else
                cbLosTx3Mask.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbLosTx2Mask.Checked = true;
            else
                cbLosTx2Mask.Checked = false;

            if ((data[0] & 0x10) != 0)
                cbLosTx1Mask.Checked = true;
            else
                cbLosTx1Mask.Checked = false;

            if ((data[0] & 0x08) != 0)
                cbLosRx4Mask.Checked = true;
            else
                cbLosRx4Mask.Checked = false;

            if ((data[0] & 0x04) != 0)
                cbLosRx3Mask.Checked = true;
            else
                cbLosRx3Mask.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbLosRx2Mask.Checked = true;
            else
                cbLosRx2Mask.Checked = false;

            if ((data[0] & 0x01) != 0)
                cbLosRx1Mask.Checked = true;
            else
                cbLosRx1Mask.Checked = false;

            if ((data[1] & 0x80) != 0)
                cbAdaptEqFaultTx1Mask.Checked = true;
            else
                cbAdaptEqFaultTx1Mask.Checked = false;

            if ((data[1] & 0x40) != 0)
                cbAdaptEqFaultTx2Mask.Checked = true;
            else
                cbAdaptEqFaultTx2Mask.Checked = false;

            if ((data[1] & 0x20) != 0)
                cbAdaptEqFaultTx3Mask.Checked = true;
            else
                cbAdaptEqFaultTx3Mask.Checked = false;

            if ((data[1] & 0x10) != 0)
                cbAdaptEqFaultTx4Mask.Checked = true;
            else
                cbAdaptEqFaultTx4Mask.Checked = false;

            if ((data[1] & 0x08) != 0)
                cbFaultTx4Mask.Checked = true;
            else
                cbFaultTx4Mask.Checked = false;

            if ((data[1] & 0x04) != 0)
                cbFaultTx3Mask.Checked = true;
            else
                cbFaultTx3Mask.Checked = false;

            if ((data[1] & 0x02) != 0)
                cbFaultTx2Mask.Checked = true;
            else
                cbFaultTx2Mask.Checked = false;

            if ((data[1] & 0x01) != 0)
                cbFaultTx1Mask.Checked = true;
            else
                cbFaultTx1Mask.Checked = false;

             if ((data[2] & 0x80) != 0)
                cbLolTx4Mask.Checked = true;
            else
                cbLolTx4Mask.Checked = false;

            if ((data[2] & 0x40) != 0)
                cbLolTx3Mask.Checked = true;
            else
                cbLolTx3Mask.Checked = false;

            if ((data[2] & 0x20) != 0)
                cbLolTx2Mask.Checked = true;
            else
                cbLolTx2Mask.Checked = false;

            if ((data[2] & 0x10) != 0)
                cbLolTx1Mask.Checked = true;
            else
                cbLolTx1Mask.Checked = false;

            if ((data[2] & 0x08) != 0)
                cbLolRx4Mask.Checked = true;
            else
                cbLolRx4Mask.Checked = false;

            if ((data[2] & 0x04) != 0)
                cbLolRx3Mask.Checked = true;
            else
                cbLolRx3Mask.Checked = false;

            if ((data[2] & 0x02) != 0)
                cbLolRx2Mask.Checked = true;
            else
                cbLolRx2Mask.Checked = false;

            if ((data[2] & 0x01) != 0)
                cbLolRx1Mask.Checked = true;
            else
                cbLolRx1Mask.Checked = false;

            return 0;
        }

        private void _bLosAndFaultRead_Click(object sender, EventArgs e)
        {
            if (_LosAndFaultRead() < 0)
                return;
        }

        private int _LosAndFaultWrite()
        {
            byte[] data = new byte[3];

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (i2cWriteCB == null)
                return -1;

            data[0] = data[1] = data[2] = 0;
            if (cbLosTx4Mask.Checked == true)
                data[0] |= 0x80;
            if (cbLosTx3Mask.Checked == true)
                data[0] |= 0x40;
            if (cbLosTx2Mask.Checked == true)
                data[0] |= 0x20;
            if (cbLosTx1Mask.Checked == true)
                data[0] |= 0x10;
            if (cbLosRx4Mask.Checked == true)
                data[0] |= 0x08;
            if (cbLosRx3Mask.Checked == true)
                data[0] |= 0x04;
            if (cbLosRx2Mask.Checked == true)
                data[0] |= 0x02;
            if (cbLosRx1Mask.Checked == true)
                data[0] |= 0x01;
            if (cbAdaptEqFaultTx4Mask.Checked == true)
                data[1] |= 0x80;
            if (cbAdaptEqFaultTx3Mask.Checked == true)
                data[1] |= 0x40;
            if (cbAdaptEqFaultTx2Mask.Checked == true)
                data[1] |= 0x20;
            if (cbAdaptEqFaultTx1Mask.Checked == true)
                data[1] |= 0x10;
            if (cbFaultTx4Mask.Checked == true)
                data[1] |= 0x08;
            if (cbFaultTx3Mask.Checked == true)
                data[1] |= 0x04;
            if (cbFaultTx2Mask.Checked == true)
                data[1] |= 0x02;
            if (cbFaultTx1Mask.Checked == true)
                data[1] |= 0x01;
            if (cbLolTx4Mask.Checked == true)
                data[2] |= 0x80;
            if (cbLolTx3Mask.Checked == true)
                data[2] |= 0x40;
            if (cbLolTx2Mask.Checked == true)
                data[2] |= 0x20;
            if (cbLolTx1Mask.Checked == true)
                data[2] |= 0x10;
            if (cbLolRx4Mask.Checked == true)
                data[2] |= 0x08;
            if (cbLolRx3Mask.Checked == true)
                data[2] |= 0x04;
            if (cbLolRx2Mask.Checked == true)
                data[2] |= 0x02;
            if (cbLolRx1Mask.Checked == true)
                data[2] |= 0x01;

            if (i2cWriteCB(80, 100, 3, data) < 0)
                return -1;

            return 0;
        }

        private void bLosAndFaultWrite_Click(object sender, EventArgs e)
        {
            if (_LosAndFaultWrite() < 0)
                return;
        }

        private int _MiscRead()
        {
            byte[] data = new byte[8];

            if (i2cReadCB == null)
                return -1;

            if (i2cReadCB(80, 6, 1, data) != 1)
                return -1;

            if ((data[0] & 0x01) != 0)
                cbInitComplete.Checked = true;
            else
                cbInitComplete.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbIntLState.Checked = true;
            else
                cbIntLState.Checked = false;

            if ((data[0] & 0x04) != 0)
                cbFlatMem.Checked = true;
            else
                cbFlatMem.Checked = false;

            if (i2cReadCB(80, 103, 1, data) != 1)
                return -1;

            if ((data[0] & 0x01) != 0)
                cbInitCompleteMask.Checked = true;
            else
                cbInitCompleteMask.Checked = false;

            return 0;
        }

        private void _bMiscRead_Click(object sender, EventArgs e)
        {
            if (_MiscRead() < 0)
                return;
        }

        private int _MiscWrite()
        {
            byte[] data = new byte[1];

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (i2cReadCB == null)
                return -1;

            if (i2cReadCB(80, 103, 1, data) != 1)
                return -1;

            if (i2cWriteCB == null)
                return -1;

            data[0] &= 0xF0;

            if (cbInitCompleteMask.Checked == true)
                data[0] |= 0x01;

            if (i2cWriteCB(80, 103, 1, data) < 0)
                return -1;

            return 0;
        }

        private void _bMiscWrite_Click(object sender, EventArgs e)
        {
            if (_MiscWrite() < 0)
                return;
        }

        private int _TemperatureRead()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float temperature;
            int tmp;

            if (i2cReadCB == null)
                return -1;

            tbTemperature.Text = "";

            if (i2cReadCB(80, 6, 1, data) != 1)
                return -1;

            if ((data[0] & 0x80) != 0)
                cbTemperatureHighAlarm.Checked = true;
            else
                cbTemperatureHighAlarm.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbTemperatureLowAlarm.Checked = true;
            else
                cbTemperatureLowAlarm.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbTemperatureHighWarning.Checked = true;
            else
                cbTemperatureHighWarning.Checked = false;

            if ((data[0] & 0x10) != 0)
                cbTemperatureLowWarning.Checked = true;
            else
                cbTemperatureLowWarning.Checked = false;

            if (i2cReadCB(80, 22, 2, data) != 2)
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

            if (i2cReadCB(80, 103, 1, data) != 1)
                return -1;

            if ((data[0] & 0x80) != 0)
                cbTemperatureHighAlarmMask.Checked = true;
            else
                cbTemperatureHighAlarmMask.Checked = false;
            if ((data[0] & 0x40) != 0)
                cbTemperatureLowAlarmMask.Checked = true;
            else
                cbTemperatureLowAlarmMask.Checked = false;
            if ((data[0] & 0x20) != 0)
                cbTemperatureHighWarningMask.Checked = true;
            else
                cbTemperatureHighWarningMask.Checked = false;
            if ((data[0] & 0x10) != 0)
                cbTemperatureLowWarningMask.Checked = true;
            else
                cbTemperatureLowWarningMask.Checked = false;

            if (i2cWriteCB == null)
                return -1;

            data[0] = 3;
            i2cWriteCB(80, 127, 1, data);
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

        private void _bTemperatureRead_Click(object sender, EventArgs e)
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

            if (i2cReadCB == null)
                return -1;

            if (i2cReadCB(80, 103, 1, data) != 1)
                return -1;

            data[0] &= 0x0F;
            if (cbTemperatureHighAlarmMask.Checked == true)
                data[0] |= 0x80;
            if (cbTemperatureLowAlarmMask.Checked == true)
                data[0] |= 0x40;
            if (cbTemperatureHighWarningMask.Checked == true)
                data[0] |= 0x20;
            if (cbTemperatureLowWarningMask.Checked == true)
                data[0] |= 0x10;

            if (i2cWriteCB == null)
                return -1;

            if (i2cWriteCB(80, 103, 1, data) < 0)
                return -1;

            if (writePasswordCB == null)
                return -1;

            if (writePasswordCB() < 0)
                return -1;

            data[0] = 3;
            if (i2cWriteCB(80, 127, 1, data) < 0)
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

        private void _bTemperatureWrite_Click(object sender, EventArgs e)
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

            if (i2cReadCB == null)
                return -1;

            tbVcc.Text = "";

            if (i2cReadCB(80, 7, 1, data) != 1)
                return -1;

            if ((data[0] & 0x80) != 0)
                cbVccHighAlarm.Checked = true;
            else
                cbVccHighAlarm.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbVccLowAlarm.Checked = true;
            else
                cbVccLowAlarm.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbVccHighWarning.Checked = true;
            else
                cbVccHighWarning.Checked = false;

            if ((data[0] & 0x10) != 0)
                cbVccLowWarning.Checked = true;
            else
                cbVccLowWarning.Checked = false;

            if (i2cReadCB(80, 26, 2, data) != 2)
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

            if (i2cReadCB(80, 104, 1, data) != 1)
                return -1;

            if ((data[0] & 0x80) != 0)
                cbVccHighAlarmMask.Checked = true;
            else
                cbVccHighAlarmMask.Checked = false;
            if ((data[0] & 0x40) != 0)
                cbVccLowAlarmMask.Checked = true;
            else
                cbVccLowAlarmMask.Checked = false;
            if ((data[0] & 0x20) != 0)
                cbVccHighWarningMask.Checked = true;
            else
                cbVccHighWarningMask.Checked = false;
            if ((data[0] & 0x10) != 0)
                cbVccLowWarningMask.Checked = true;
            else
                cbVccLowWarningMask.Checked = false;

            if (i2cWriteCB == null)
                return -1;

            data[0] = 3;
            i2cWriteCB(80, 127, 1, data);
            if (i2cReadCB(80, 144, 8, data) != 8)
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

        private void _bVccRead_Click(object sender, EventArgs e)
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

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (i2cWriteCB == null)
                return -1;

            if (cbVccHighAlarmMask.Checked == true)
                data[0] |= 0x80;
            if (cbVccLowAlarmMask.Checked == true)
                data[0] |= 0x40;
            if (cbVccHighWarningMask.Checked == true)
                data[0] |= 0x20;
            if (cbVccLowWarningMask.Checked == true)
                data[0] |= 0x10;

            if (i2cWriteCB(80, 104, 1, data) < 0)
                return -1;

            if (writePasswordCB == null)
                return -1;

            if (writePasswordCB() < 0)
                return -1;

            data[0] = 3;
            if (i2cWriteCB(80, 127, 1, data) < 0)
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

            if (i2cWriteCB(80, 144, 8, data) < 0)
                return -1;

            return 0;
        }

        private void _bVccWrite_Click(object sender, EventArgs e)
        {
            if (_VccWrite() < 0)
                return;
        }

        private int _RxPowerRead()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float power;
            int tmp;

            tbRxPower1.Text = tbRxPower2.Text = tbRxPower3.Text =
                tbRxPower4.Text = "";

            if (i2cReadCB == null)
                return -1;

            if (i2cReadCB(80, 9, 2, data) != 2)
                return -1;

            if ((data[0] & 0x80) != 0)
                cbRxPower1HighAlarm.Checked = true;
            else
                cbRxPower1HighAlarm.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbRxPower1LowAlarm.Checked = true;
            else
                cbRxPower1LowAlarm.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbRxPower1HighWarning.Checked = true;
            else
                cbRxPower1HighWarning.Checked = false;

            if ((data[0] & 0x10) != 0)
                cbRxPower1LowWarning.Checked = true;
            else
                cbRxPower1LowWarning.Checked = false;

            if ((data[0] & 0x08) != 0)
                cbRxPower2HighAlarm.Checked = true;
            else
                cbRxPower2HighAlarm.Checked = false;

            if ((data[0] & 0x04) != 0)
                cbRxPower2LowAlarm.Checked = true;
            else
                cbRxPower2LowAlarm.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbRxPower2HighWarning.Checked = true;
            else
                cbRxPower2HighWarning.Checked = false;

            if ((data[0] & 0x01) != 0)
                cbRxPower2LowWarning.Checked = true;
            else
                cbRxPower2LowWarning.Checked = false;

            if ((data[1] & 0x80) != 0)
                cbRxPower3HighAlarm.Checked = true;
            else
                cbRxPower3HighAlarm.Checked = false;

            if ((data[1] & 0x40) != 0)
                cbRxPower3LowAlarm.Checked = true;
            else
                cbRxPower3LowAlarm.Checked = false;

            if ((data[1] & 0x20) != 0)
                cbRxPower3HighWarning.Checked = true;
            else
                cbRxPower3HighWarning.Checked = false;

            if ((data[1] & 0x10) != 0)
                cbRxPower3LowWarning.Checked = true;
            else
                cbRxPower3LowWarning.Checked = false;

            if ((data[1] & 0x08) != 0)
                cbRxPower4HighAlarm.Checked = true;
            else
                cbRxPower4HighAlarm.Checked = false;

            if ((data[1] & 0x04) != 0)
                cbRxPower4LowAlarm.Checked = true;
            else
                cbRxPower4LowAlarm.Checked = false;

            if ((data[1] & 0x02) != 0)
                cbRxPower4HighWarning.Checked = true;
            else
                cbRxPower4HighWarning.Checked = false;

            if ((data[1] & 0x01) != 0)
                cbRxPower4LowWarning.Checked = true;
            else
                cbRxPower4LowWarning.Checked = false;


            if (i2cReadCB(80, 34, 8, data) != 8)
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

            if (i2cWriteCB == null)
                return -1;

            data[0] = 3;
            i2cWriteCB(80, 127, 1, data);
            if (i2cReadCB(80, 176, 8, data) != 8)
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

            data[0] = 3;
            i2cWriteCB(80, 127, 1, data);
            if (i2cReadCB(80, 242, 2, data) != 2)
                return -1;

            if ((data[0] & 0x80) != 0)
                cbRxPower1HighAlarmMask.Checked = true;
            else
                cbRxPower1HighAlarmMask.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbRxPower1LowAlarmMask.Checked = true;
            else
                cbRxPower1LowAlarmMask.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbRxPower1HighWarningMask.Checked = true;
            else
                cbRxPower1HighWarningMask.Checked = false;

            if ((data[0] & 0x10) != 0)
                cbRxPower1LowWarningMask.Checked = true;
            else
                cbRxPower1LowWarningMask.Checked = false;

            if ((data[0] & 0x08) != 0)
                cbRxPower2HighAlarmMask.Checked = true;
            else
                cbRxPower2HighAlarmMask.Checked = false;

            if ((data[0] & 0x04) != 0)
                cbRxPower2LowAlarmMask.Checked = true;
            else
                cbRxPower2LowAlarmMask.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbRxPower2HighWarningMask.Checked = true;
            else
                cbRxPower2HighWarningMask.Checked = false;

            if ((data[0] & 0x01) != 0)
                cbRxPower2LowWarningMask.Checked = true;
            else
                cbRxPower2LowWarningMask.Checked = false;

            if ((data[1] & 0x80) != 0)
                cbRxPower3HighAlarmMask.Checked = true;
            else
                cbRxPower3HighAlarmMask.Checked = false;

            if ((data[1] & 0x40) != 0)
                cbRxPower3LowAlarmMask.Checked = true;
            else
                cbRxPower3LowAlarmMask.Checked = false;

            if ((data[1] & 0x20) != 0)
                cbRxPower3HighWarningMask.Checked = true;
            else
                cbRxPower3HighWarningMask.Checked = false;

            if ((data[1] & 0x10) != 0)
                cbRxPower3LowWarningMask.Checked = true;
            else
                cbRxPower3LowWarningMask.Checked = false;

            if ((data[1] & 0x08) != 0)
                cbRxPower4HighAlarmMask.Checked = true;
            else
                cbRxPower4HighAlarmMask.Checked = false;

            if ((data[1] & 0x04) != 0)
                cbRxPower4LowAlarmMask.Checked = true;
            else
                cbRxPower4LowAlarmMask.Checked = false;

            if ((data[1] & 0x02) != 0)
                cbRxPower4HighWarningMask.Checked = true;
            else
                cbRxPower4HighWarningMask.Checked = false;

            if ((data[1] & 0x01) != 0)
                cbRxPower4LowWarningMask.Checked = true;
            else
                cbRxPower4LowWarningMask.Checked = false;

            return 0;
        }

        private void _bRxPowerReadClick(object sender, EventArgs e)
        {
            if (_RxPowerRead() < 0)
                return;
        }

        private int _RxPowerWrite()
        {
            byte[] data = new byte[8];
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

            data[0] = 3;
            if (i2cWriteCB(80, 127, 1, data) < 0)
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

            if (i2cWriteCB(80, 176, 8, data) < 0)
                return -1;

            data[0] = data[1] = 0;
            if (cbRxPower1HighAlarmMask.Checked == true)
                data[0] |= 0x80;
            if (cbRxPower1LowAlarmMask.Checked == true)
                data[0] |= 0x40;
            if (cbRxPower1HighWarningMask.Checked == true)
                data[0] |= 0x20;
            if (cbRxPower1LowWarningMask.Checked == true)
                data[0] |= 0x10;
            if (cbRxPower2HighAlarmMask.Checked == true)
                data[0] |= 0x08;
            if (cbRxPower2LowAlarmMask.Checked == true)
                data[0] |= 0x04;
            if (cbRxPower2HighWarningMask.Checked == true)
                data[0] |= 0x02;
            if (cbRxPower2LowWarningMask.Checked == true)
                data[0] |= 0x01;
            if (cbRxPower3HighAlarmMask.Checked == true)
                data[1] |= 0x80;
            if (cbRxPower3LowAlarmMask.Checked == true)
                data[1] |= 0x40;
            if (cbRxPower3HighWarningMask.Checked == true)
                data[1] |= 0x20;
            if (cbRxPower3LowWarningMask.Checked == true)
                data[1] |= 0x10;
            if (cbRxPower4HighAlarmMask.Checked == true)
                data[1] |= 0x08;
            if (cbRxPower4LowAlarmMask.Checked == true)
                data[1] |= 0x04;
            if (cbRxPower4HighWarningMask.Checked == true)
                data[1] |= 0x02;
            if (cbRxPower4LowWarningMask.Checked == true)
                data[1] |= 0x01;

            if (i2cWriteCB(80, 242, 2, data) < 0)
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
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float bias;
            int tmp;

            tbTxBias1.Text = tbTxBias2.Text = tbTxBias3.Text =
                tbTxBias4.Text = "";

            if (i2cReadCB == null)
                return -1;

            if (i2cReadCB(80, 11, 2, data) != 2)
                return -1;
            
            if ((data[0] & 0x80) != 0)
                cbTxBias1HighAlarm.Checked = true;
            else
                cbTxBias1HighAlarm.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbTxBias1LowAlarm.Checked = true;
            else
                cbTxBias1LowAlarm.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbTxBias1HighWarning.Checked = true;
            else
                cbTxBias1HighWarning.Checked = false;

            if ((data[0] & 0x10) != 0)
                cbTxBias1LowWarning.Checked = true;
            else
                cbTxBias1LowWarning.Checked = false;

            if ((data[0] & 0x08) != 0)
                cbTxBias2HighAlarm.Checked = true;
            else
                cbTxBias2HighAlarm.Checked = false;

            if ((data[0] & 0x04) != 0)
                cbTxBias2LowAlarm.Checked = true;
            else
                cbTxBias2LowAlarm.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbTxBias2HighWarning.Checked = true;
            else
                cbTxBias2HighWarning.Checked = false;

            if ((data[0] & 0x01) != 0)
                cbTxBias2LowWarning.Checked = true;
            else
                cbTxBias2LowWarning.Checked = false;

            if ((data[1] & 0x80) != 0)
                cbTxBias3HighAlarm.Checked = true;
            else
                cbTxBias3HighAlarm.Checked = false;

            if ((data[1] & 0x40) != 0)
                cbTxBias3LowAlarm.Checked = true;
            else
                cbTxBias3LowAlarm.Checked = false;

            if ((data[1] & 0x20) != 0)
                cbTxBias3HighWarning.Checked = true;
            else
                cbTxBias3HighWarning.Checked = false;

            if ((data[1] & 0x10) != 0)
                cbTxBias3LowWarning.Checked = true;
            else
                cbTxBias3LowWarning.Checked = false;

            if ((data[1] & 0x08) != 0)
                cbTxBias4HighAlarm.Checked = true;
            else
                cbTxBias4HighAlarm.Checked = false;

            if ((data[1] & 0x04) != 0)
                cbTxBias4LowAlarm.Checked = true;
            else
                cbTxBias4LowAlarm.Checked = false;

            if ((data[1] & 0x02) != 0)
                cbTxBias4HighWarning.Checked = true;
            else
                cbTxBias4HighWarning.Checked = false;

            if ((data[1] & 0x01) != 0)
                cbTxBias4LowWarning.Checked = true;
            else
                cbTxBias4LowWarning.Checked = false;

            if (i2cReadCB(80, 42, 8, data) != 8)
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

            if (i2cWriteCB == null)
                return -1;

            data[0] = 3;
            i2cWriteCB(80, 127, 1, data);
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

            data[0] = 3;
            i2cWriteCB(80, 127, 1, data);
            if (i2cReadCB(80, 244, 2, data) != 2)
                return -1;

            if ((data[0] & 0x80) != 0)
                cbTxBias1HighAlarmMask.Checked = true;
            else
                cbTxBias1HighAlarmMask.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbTxBias1LowAlarmMask.Checked = true;
            else
                cbTxBias1LowAlarmMask.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbTxBias1HighWarningMask.Checked = true;
            else
                cbTxBias1HighWarningMask.Checked = false;

            if ((data[0] & 0x10) != 0)
                cbTxBias1LowWarningMask.Checked = true;
            else
                cbTxBias1LowWarningMask.Checked = false;

            if ((data[0] & 0x08) != 0)
                cbTxBias2HighAlarmMask.Checked = true;
            else
                cbTxBias2HighAlarmMask.Checked = false;

            if ((data[0] & 0x04) != 0)
                cbTxBias2LowAlarmMask.Checked = true;
            else
                cbTxBias2LowAlarmMask.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbTxBias2HighWarningMask.Checked = true;
            else
                cbTxBias2HighWarningMask.Checked = false;

            if ((data[0] & 0x01) != 0)
                cbTxBias2LowWarningMask.Checked = true;
            else
                cbTxBias2LowWarningMask.Checked = false;

            if ((data[1] & 0x80) != 0)
                cbTxBias3HighAlarmMask.Checked = true;
            else
                cbTxBias3HighAlarmMask.Checked = false;

            if ((data[1] & 0x40) != 0)
                cbTxBias3LowAlarmMask.Checked = true;
            else
                cbTxBias3LowAlarmMask.Checked = false;

            if ((data[1] & 0x20) != 0)
                cbTxBias3HighWarningMask.Checked = true;
            else
                cbTxBias3HighWarningMask.Checked = false;

            if ((data[1] & 0x10) != 0)
                cbTxBias3LowWarningMask.Checked = true;
            else
                cbTxBias3LowWarningMask.Checked = false;

            if ((data[1] & 0x08) != 0)
                cbTxBias4HighAlarmMask.Checked = true;
            else
                cbTxBias4HighAlarmMask.Checked = false;

            if ((data[1] & 0x04) != 0)
                cbTxBias4LowAlarmMask.Checked = true;
            else
                cbTxBias4LowAlarmMask.Checked = false;

            if ((data[1] & 0x02) != 0)
                cbTxBias4HighWarningMask.Checked = true;
            else
                cbTxBias4HighWarningMask.Checked = false;

            if ((data[1] & 0x01) != 0)
                cbTxBias4LowWarningMask.Checked = true;
            else
                cbTxBias4LowWarningMask.Checked = false;

            return 0;
        }

        private void _bTxBiasRead_Click(object sender, EventArgs e)
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

            if (writePasswordCB == null)
                return -1;

            if (writePasswordCB() < 0)
                return -1;

            data[0] = 3;
            if (i2cWriteCB(80, 127, 1, data) < 0)
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

            data[0] = data[1] = 0;
            if (cbTxBias1HighAlarmMask.Checked == true)
                data[0] |= 0x80;
            if (cbTxBias1LowAlarmMask.Checked == true)
                data[0] |= 0x40;
            if (cbTxBias1HighWarningMask.Checked == true)
                data[0] |= 0x20;
            if (cbTxBias1LowWarningMask.Checked == true)
                data[0] |= 0x10;
            if (cbTxBias2HighAlarmMask.Checked == true)
                data[0] |= 0x08;
            if (cbTxBias2LowAlarmMask.Checked == true)
                data[0] |= 0x04;
            if (cbTxBias2HighWarningMask.Checked == true)
                data[0] |= 0x02;
            if (cbTxBias2LowWarningMask.Checked == true)
                data[0] |= 0x01;
            if (cbTxBias3HighAlarmMask.Checked == true)
                data[1] |= 0x80;
            if (cbTxBias3LowAlarmMask.Checked == true)
                data[1] |= 0x40;
            if (cbTxBias3HighWarningMask.Checked == true)
                data[1] |= 0x20;
            if (cbTxBias3LowWarningMask.Checked == true)
                data[1] |= 0x10;
            if (cbTxBias4HighAlarmMask.Checked == true)
                data[1] |= 0x08;
            if (cbTxBias4LowAlarmMask.Checked == true)
                data[1] |= 0x04;
            if (cbTxBias4HighWarningMask.Checked == true)
                data[1] |= 0x02;
            if (cbTxBias4LowWarningMask.Checked == true)
                data[1] |= 0x01;

            if (i2cWriteCB(80, 244, 2, data) < 0)
                return -1;

            return 0;
        }

        private void bTxBiasWrite_Click(object sender, EventArgs e)
        {
            if (_TxBiasWrite() < 0)
                return;
        }

        private void bRead_Click(object sender, EventArgs e)
        {
            bRead.Enabled = false;
            if (_LosAndFaultRead() < 0)
                goto exit;

            if (_MiscRead() < 0)
                goto exit;

            if (_TemperatureRead() < 0)
                goto exit;

            if (_VccRead() < 0)
                goto exit;

            if (_RxPowerRead() < 0)
                goto exit;

            if (_TxBiasRead() < 0)
                goto exit;

            if (_MpdPowerRead() < 0)
                goto exit;

            exit:
            bRead.Enabled = true;
        }

        private void bWrite_Click(object sender, EventArgs e)
        {
            bWrite.Enabled = false;

            if (_LosAndFaultWrite() < 0)
                goto exit;

            if (_MiscWrite() < 0)
                goto exit;

            if (_TemperatureWrite() < 0)
                goto exit;

            if (_VccWrite() < 0)
                goto exit;

            if (_RxPowerWrite() < 0)
                goto exit;

            if (_TxBiasWrite() < 0)
                goto exit;

            if (_MpdPowerWrite() < 0)
                goto exit;

            exit:
            bWrite.Enabled = true;
        }

        private void bStoreIntoFlash_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1];

            bStoreIntoFlash.Enabled = false;

            if (writePasswordCB == null)
                goto exit;

            if (writePasswordCB() < 0)
                goto exit;

            if (_SetQsfpMode(0x4D) < 0)
                goto exit;

            if (i2cWriteCB == null)
                goto exit;


            /* old version */
            data[0] = 0x32;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                goto exit;

            data[0] = 0xAA;
            if (i2cWriteCB(80, 162, 1, data) < 0)
                goto exit;

            /* new firmware */
            data[0] = 0xAA;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                goto exit;

            data[0] = 0xAA;
            if (i2cWriteCB(80, 162, 1, data) < 0)
                goto exit;

            Thread.Sleep(1000);

        exit:
            _SetQsfpMode(0);
            bStoreIntoFlash.Enabled = true;
        }

        private int _MpdPowerRead()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float power;
            int tmp;

            tbRxPower1.Text = tbRxPower2.Text = tbRxPower3.Text =
                tbRxPower4.Text = "";

            if (i2cReadCB == null)
                return -1;

            if (i2cReadCB(80, 13, 2, data) != 2)
                return -1;

            if ((data[0] & 0x80) != 0)
                cbMpdPower1HighAlarm.Checked = true;
            else
                cbMpdPower1HighAlarm.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbMpdPower1LowAlarm.Checked = true;
            else
                cbMpdPower1LowAlarm.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbMpdPower1HighWarning.Checked = true;
            else
                cbMpdPower1HighWarning.Checked = false;

            if ((data[0] & 0x10) != 0)
                cbMpdPower1LowWarning.Checked = true;
            else
                cbMpdPower1LowWarning.Checked = false;

            if ((data[0] & 0x08) != 0)
                cbMpdPower2HighAlarm.Checked = true;
            else
                cbMpdPower2HighAlarm.Checked = false;

            if ((data[0] & 0x04) != 0)
                cbMpdPower2LowAlarm.Checked = true;
            else
                cbMpdPower2LowAlarm.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbMpdPower2HighWarning.Checked = true;
            else
                cbMpdPower2HighWarning.Checked = false;

            if ((data[0] & 0x01) != 0)
                cbMpdPower2LowWarning.Checked = true;
            else
                cbMpdPower2LowWarning.Checked = false;

            if ((data[1] & 0x80) != 0)
                cbMpdPower3HighAlarm.Checked = true;
            else
                cbMpdPower3HighAlarm.Checked = false;

            if ((data[1] & 0x40) != 0)
                cbMpdPower3LowAlarm.Checked = true;
            else
                cbMpdPower3LowAlarm.Checked = false;

            if ((data[1] & 0x20) != 0)
                cbMpdPower3HighWarning.Checked = true;
            else
                cbMpdPower3HighWarning.Checked = false;

            if ((data[1] & 0x10) != 0)
                cbMpdPower3LowWarning.Checked = true;
            else
                cbMpdPower3LowWarning.Checked = false;

            if ((data[1] & 0x08) != 0)
                cbMpdPower4HighAlarm.Checked = true;
            else
                cbMpdPower4HighAlarm.Checked = false;

            if ((data[1] & 0x04) != 0)
                cbMpdPower4LowAlarm.Checked = true;
            else
                cbMpdPower4LowAlarm.Checked = false;

            if ((data[1] & 0x02) != 0)
                cbMpdPower4HighWarning.Checked = true;
            else
                cbMpdPower4HighWarning.Checked = false;

            if ((data[1] & 0x01) != 0)
                cbMpdPower4LowWarning.Checked = true;
            else
                cbMpdPower4LowWarning.Checked = false;


            if (i2cReadCB(80, 50, 8, data) != 8)
                return -1;

            try
            {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC)
            {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbMpdPower1.Text = power.ToString("#0.0");

            try
            {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC)
            {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbMpdPower2.Text = power.ToString("#0.0");

            try
            {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC)
            {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbMpdPower3.Text = power.ToString("#0.0");

            try
            {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC)
            {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbMpdPower4.Text = power.ToString("#0.0");

            if (i2cWriteCB == null)
                return -1;

            data[0] = 3;
            i2cWriteCB(80, 127, 1, data);
            if (i2cReadCB(80, 192, 8, data) != 8)
                return -1;

            try
            {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC)
            {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbMpdPowerHighAlarmThreshold.Text = power.ToString("#0.0");

            try
            {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC)
            {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbMpdPowerLowAlarmThreshold.Text = power.ToString("#0.0");

            try
            {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC)
            {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbMpdPowerHighWarningThreshold.Text = power.ToString("#0.0");

            try
            {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC)
            {
                MessageBox.Show(eBC.ToString());
                return -1;
            }
            reverseData = bATmp.Reverse().ToArray();
            tmp = BitConverter.ToUInt16(reverseData, 0);
            power = tmp;
            power = power / 10;
            tbMpdPowerLowWarningThreshold.Text = power.ToString("#0.0");

            data[0] = 3;
            i2cWriteCB(80, 127, 1, data);
            if (i2cReadCB(80, 246, 2, data) != 2)
                return -1;

            if ((data[0] & 0x80) != 0)
                cbMpdPower1HighAlarmMask.Checked = true;
            else
                cbMpdPower1HighAlarmMask.Checked = false;

            if ((data[0] & 0x40) != 0)
                cbMpdPower1LowAlarmMask.Checked = true;
            else
                cbMpdPower1LowAlarmMask.Checked = false;

            if ((data[0] & 0x20) != 0)
                cbMpdPower1HighWarningMask.Checked = true;
            else
                cbMpdPower1HighWarningMask.Checked = false;

            if ((data[0] & 0x10) != 0)
                cbMpdPower1LowWarningMask.Checked = true;
            else
                cbMpdPower1LowWarningMask.Checked = false;

            if ((data[0] & 0x08) != 0)
                cbMpdPower2HighAlarmMask.Checked = true;
            else
                cbMpdPower2HighAlarmMask.Checked = false;

            if ((data[0] & 0x04) != 0)
                cbMpdPower2LowAlarmMask.Checked = true;
            else
                cbMpdPower2LowAlarmMask.Checked = false;

            if ((data[0] & 0x02) != 0)
                cbMpdPower2HighWarningMask.Checked = true;
            else
                cbMpdPower2HighWarningMask.Checked = false;

            if ((data[0] & 0x01) != 0)
                cbMpdPower2LowWarningMask.Checked = true;
            else
                cbMpdPower2LowWarningMask.Checked = false;

            if ((data[1] & 0x80) != 0)
                cbMpdPower3HighAlarmMask.Checked = true;
            else
                cbMpdPower3HighAlarmMask.Checked = false;

            if ((data[1] & 0x40) != 0)
                cbMpdPower3LowAlarmMask.Checked = true;
            else
                cbMpdPower3LowAlarmMask.Checked = false;

            if ((data[1] & 0x20) != 0)
                cbMpdPower3HighWarningMask.Checked = true;
            else
                cbMpdPower3HighWarningMask.Checked = false;

            if ((data[1] & 0x10) != 0)
                cbMpdPower3LowWarningMask.Checked = true;
            else
                cbMpdPower3LowWarningMask.Checked = false;

            if ((data[1] & 0x08) != 0)
                cbMpdPower4HighAlarmMask.Checked = true;
            else
                cbMpdPower4HighAlarmMask.Checked = false;

            if ((data[1] & 0x04) != 0)
                cbMpdPower4LowAlarmMask.Checked = true;
            else
                cbMpdPower4LowAlarmMask.Checked = false;

            if ((data[1] & 0x02) != 0)
                cbMpdPower4HighWarningMask.Checked = true;
            else
                cbMpdPower4HighWarningMask.Checked = false;

            if ((data[1] & 0x01) != 0)
                cbMpdPower4LowWarningMask.Checked = true;
            else
                cbMpdPower4LowWarningMask.Checked = false;

            return 0;
        }

        private void bMpdPowerRead_Click(object sender, EventArgs e)
        {
            if (_MpdPowerRead() < 0)
                return;
        }

        private int _MpdPowerWrite()
        {
            byte[] data = new byte[8];
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

            data[0] = 3;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            try
            {
                power = Convert.ToSingle(tbMpdPowerHighAlarmThreshold.Text);
            }
            catch (Exception eTS)
            {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            power *= 10;
            try
            {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI)
            {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[0] = bATmp[1];
            data[1] = bATmp[0];

            try
            {
                power = Convert.ToSingle(tbMpdPowerLowAlarmThreshold.Text);
            }
            catch (Exception eTS)
            {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            power *= 10;
            try
            {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI)
            {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[2] = bATmp[1];
            data[3] = bATmp[0];

            try
            {
                power = Convert.ToSingle(tbMpdPowerHighWarningThreshold.Text);
            }
            catch (Exception eTS)
            {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            power *= 10;
            try
            {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI)
            {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[4] = bATmp[1];
            data[5] = bATmp[0];

            try
            {
                power = Convert.ToSingle(tbMpdPowerLowWarningThreshold.Text);
            }
            catch (Exception eTS)
            {
                MessageBox.Show(eTS.ToString());
                return -1;
            }

            power *= 10;
            try
            {
                uSTmp = Convert.ToUInt16(power);
            }
            catch (Exception eTI)
            {
                MessageBox.Show(eTI.ToString());
                return -1;
            }

            bATmp = BitConverter.GetBytes(uSTmp);
            data[6] = bATmp[1];
            data[7] = bATmp[0];

            if (i2cWriteCB(80, 192, 8, data) < 0)
                return -1;

            data[0] = data[1] = 0;
            if (cbMpdPower1HighAlarmMask.Checked == true)
                data[0] |= 0x80;
            if (cbMpdPower1LowAlarmMask.Checked == true)
                data[0] |= 0x40;
            if (cbMpdPower1HighWarningMask.Checked == true)
                data[0] |= 0x20;
            if (cbMpdPower1LowWarningMask.Checked == true)
                data[0] |= 0x10;
            if (cbMpdPower2HighAlarmMask.Checked == true)
                data[0] |= 0x08;
            if (cbMpdPower2LowAlarmMask.Checked == true)
                data[0] |= 0x04;
            if (cbMpdPower2HighWarningMask.Checked == true)
                data[0] |= 0x02;
            if (cbMpdPower2LowWarningMask.Checked == true)
                data[0] |= 0x01;
            if (cbMpdPower3HighAlarmMask.Checked == true)
                data[1] |= 0x80;
            if (cbMpdPower3LowAlarmMask.Checked == true)
                data[1] |= 0x40;
            if (cbMpdPower3HighWarningMask.Checked == true)
                data[1] |= 0x20;
            if (cbMpdPower3LowWarningMask.Checked == true)
                data[1] |= 0x10;
            if (cbMpdPower4HighAlarmMask.Checked == true)
                data[1] |= 0x08;
            if (cbMpdPower4LowAlarmMask.Checked == true)
                data[1] |= 0x04;
            if (cbMpdPower4HighWarningMask.Checked == true)
                data[1] |= 0x02;
            if (cbMpdPower4LowWarningMask.Checked == true)
                data[1] |= 0x01;

            if (i2cWriteCB(80, 246, 2, data) < 0)
                return -1;

            return 0;
        }

        private void bMpdPowerWrite_Click(object sender, EventArgs e)
        {
            if (_MpdPowerWrite() < 0)
                return;
        }
    }
}
