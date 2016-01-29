using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AnalogMeterUserInterface;

namespace LensAlignment
{
    public partial class UcLensAlignment : UserControl
    {
        public delegate int LightSourceI2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int LightSourceI2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int BeAlignmentI2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int BeAlignmentI2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private LightSourceI2cReadCB lightSourceI2cReadCB = null;
        private LightSourceI2cWriteCB lightSourceI2cWriteCB = null;
        private BeAlignmentI2cReadCB beAlignmentI2cReadCB = null;
        private BeAlignmentI2cWriteCB beAlignmentI2cWriteCB = null;
        private BackgroundWorker bwMonitor;
        private volatile float[] faBeAlignmentRxValue = new float[4];
        private volatile float[] faBeAlignmentMpdValue = new float[4];
        private volatile float[] faLightSourceRxValue = new float[4];
        private volatile float[] faBeAlignmentInfoValue = new float[6];
        private volatile float[] faLightSourceInfoValue = new float[6];
        private volatile bool startMonitor = false;

        private int _ReadLightSourceInfoValue()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float vcc, temperature, bias;
            int devAddr;

            int.TryParse(tbLightSourceDeviceAddr.Text, out devAddr);

            if (lightSourceI2cReadCB((byte)devAddr, 26, 2, data) != 2)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            vcc = BitConverter.ToUInt16(reverseData, 0);
            vcc = vcc / 10000;
            faLightSourceInfoValue[0] = vcc;

            if (lightSourceI2cReadCB((byte)devAddr, 22, 2, data) != 2)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            temperature = BitConverter.ToInt16(reverseData, 0);
            temperature = temperature / 256;
            faLightSourceInfoValue[1] = temperature;

            if (lightSourceI2cReadCB((byte)devAddr, 42, 8, data) != 8)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            bias = BitConverter.ToUInt16(reverseData, 0);
            bias = bias / 500;
            faLightSourceInfoValue[2] = bias;

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            bias = BitConverter.ToUInt16(reverseData, 0);
            bias = bias / 500;
            faLightSourceInfoValue[3] = bias;

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            bias = BitConverter.ToUInt16(reverseData, 0);
            bias = bias / 500;
            faLightSourceInfoValue[4] = bias;

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            bias = BitConverter.ToUInt16(reverseData, 0);
            bias = bias / 500;
            faLightSourceInfoValue[5] = bias;

            return 0;

        clearData:
            faLightSourceInfoValue[0] = faLightSourceInfoValue[1] =
                faLightSourceInfoValue[2] = faLightSourceInfoValue[3] =
                faLightSourceInfoValue[4] = faLightSourceInfoValue[5] = 0;
            return -1;
        }

        private int _ReadLightSourceRxValue()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float power;
            int devAddr, page, regAddr;

            int.TryParse(tbLightSourceDeviceAddr.Text, out devAddr);
            int.TryParse(tbLightSourceRxRegisterPage.Text, out page);
            int.TryParse(tbLightSourceRxRegisterAddr.Text, out regAddr);

            if (page > 0) {
                data[0] = (byte)page;

                if (lightSourceI2cWriteCB((byte)devAddr, 127, 1, data) < 0)
                    return -1;
            }

            if (lightSourceI2cReadCB((byte)devAddr, (byte)regAddr, 8, data) != 8)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            power = BitConverter.ToUInt16(reverseData, 0);
            power = power / 10;
            faLightSourceRxValue[0] = power;

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            power = BitConverter.ToUInt16(reverseData, 0);
            power = power / 10;
            faLightSourceRxValue[1] = power;

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            power = BitConverter.ToUInt16(reverseData, 0);
            power = power / 10;
            faLightSourceRxValue[2] = power;

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            power = BitConverter.ToUInt16(reverseData, 0);
            power = power / 10;
            faLightSourceRxValue[3] = power;

            return 0;

        clearData:
            faLightSourceRxValue[0] = faLightSourceRxValue[1] =
                faLightSourceRxValue[2] = faLightSourceRxValue[3] = 0;

            return -1;
        }

        private int _ReadBeAlignmentInfoValue()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float vcc, temperature, bias;
            int devAddr;

            int.TryParse(tbLightSourceDeviceAddr.Text, out devAddr);

            if (beAlignmentI2cReadCB((byte)devAddr, 26, 2, data) != 2)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            vcc = BitConverter.ToUInt16(reverseData, 0);
            vcc = vcc / 10000;
            faBeAlignmentInfoValue[0] = vcc;

            if (beAlignmentI2cReadCB((byte)devAddr, 22, 2, data) != 2)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            temperature = BitConverter.ToInt16(reverseData, 0);
            temperature = temperature / 256;
            faBeAlignmentInfoValue[1] = temperature;

            if (beAlignmentI2cReadCB((byte)devAddr, 42, 8, data) != 8)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            bias = BitConverter.ToUInt16(reverseData, 0);
            bias = bias / 500;
            faBeAlignmentInfoValue[2] = bias;

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            bias = BitConverter.ToUInt16(reverseData, 0);
            bias = bias / 500;
            faBeAlignmentInfoValue[3] = bias;

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            bias = BitConverter.ToUInt16(reverseData, 0);
            bias = bias / 500;
            faBeAlignmentInfoValue[4] = bias;

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            bias = BitConverter.ToUInt16(reverseData, 0);
            bias = bias / 500;
            faBeAlignmentInfoValue[5] = bias;

            return 0;

        clearData:
            faBeAlignmentInfoValue[0] = faBeAlignmentInfoValue[1] =
                faBeAlignmentInfoValue[2] = faBeAlignmentInfoValue[3] =
                faBeAlignmentInfoValue[4] = faBeAlignmentInfoValue[5] = 0;

            return -1;
        }

        private int _ReadBeAlignmentRxValue()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            int devAddr, page, regAddr;

            int.TryParse(tbBeAlignmentDeviceAddr.Text, out devAddr);
            int.TryParse(tbBeAlignmentRxRegisterPage.Text, out page);
            int.TryParse(tbBeAlignmentRxRegisterAddr.Text, out regAddr);

            if (page > 0) {
                data[0] = (byte)page;

                if (beAlignmentI2cWriteCB((byte)devAddr, 127, 1, data) < 0)
                    return -1;
            }

            if (beAlignmentI2cReadCB((byte)devAddr, (byte)regAddr, 8, data) != 8)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            faBeAlignmentRxValue[0] = BitConverter.ToUInt16(reverseData, 0);

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            faBeAlignmentRxValue[1] = BitConverter.ToUInt16(reverseData, 0);

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            faBeAlignmentRxValue[2] = BitConverter.ToUInt16(reverseData, 0);

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            faBeAlignmentRxValue[3] = BitConverter.ToUInt16(reverseData, 0);

            return 0;

        clearData:
            faBeAlignmentRxValue[0] = faBeAlignmentRxValue[1] =
                faBeAlignmentRxValue[2] = faBeAlignmentRxValue[3] = 0;

            return -1;
        }

        private int _ReadBeAlignmentMpdValue()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            int devAddr, page, regAddr;

            int.TryParse(tbBeAlignmentDeviceAddr.Text, out devAddr);
            int.TryParse(tbBeAlignmentMpdRegisterPage.Text, out page);
            int.TryParse(tbBeAlignmentMpdRegisterAddr.Text, out regAddr);

            if (page > 0) {
                data[0] = (byte)page;

                if (beAlignmentI2cWriteCB((byte)devAddr, 127, 1, data) < 0)
                    return -1;
            }

            if (beAlignmentI2cReadCB((byte)devAddr, (byte)regAddr, 8, data) != 8)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            faBeAlignmentMpdValue[0] = BitConverter.ToUInt16(reverseData, 0);

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            faBeAlignmentMpdValue[1] = BitConverter.ToUInt16(reverseData, 0);

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            faBeAlignmentMpdValue[2] = BitConverter.ToUInt16(reverseData, 0);

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            faBeAlignmentMpdValue[3] = BitConverter.ToUInt16(reverseData, 0);

            return 0;

        clearData:
            faBeAlignmentMpdValue[0] = faBeAlignmentMpdValue[1] =
                faBeAlignmentMpdValue[2] = faBeAlignmentMpdValue[3] = 0;

            return -1;
        }

        public void MonitorValueUpdateApi(object sender, DoWorkEventArgs e)
        {
            bool lightSourecReadError, beAlignmentReadError;
            int i, delay;

            i = 0;
            delay = 100;

            while (startMonitor) {
                lightSourecReadError = beAlignmentReadError = false;
                bwMonitor.ReportProgress(0, null);

                if (_ReadLightSourceRxValue() < 0)
                    lightSourecReadError = true;
    
                if (_ReadBeAlignmentRxValue() < 0)
                        beAlignmentReadError = true;
                else if (_ReadBeAlignmentMpdValue() < 0)
                        beAlignmentReadError = true;

                if (i++ >= 5) {
                    i = 0;
                    if (lightSourecReadError == false)
                        _ReadLightSourceInfoValue();

                    if (beAlignmentReadError == false)
                        _ReadBeAlignmentInfoValue();
                }

                if ((lightSourecReadError == false) && (beAlignmentReadError == false))
                    System.Threading.Thread.Sleep(delay);
            }
            bwMonitor.ReportProgress(100, null);
        }

        public void MonitorProgressChangedApi(object sender, ProgressChangedEventArgs e)
        {
            amBeAlignmentMpd1.Value = faBeAlignmentMpdValue[0];
            amBeAlignmentMpd2.Value = faBeAlignmentMpdValue[1];
            amBeAlignmentMpd3.Value = faBeAlignmentMpdValue[2];
            amBeAlignmentMpd4.Value = faBeAlignmentMpdValue[3];

            amLightSourceRx1.Value = faLightSourceRxValue[0];
            amLightSourceRx2.Value = faLightSourceRxValue[1];
            amLightSourceRx3.Value = faLightSourceRxValue[2];
            amLightSourceRx4.Value = faLightSourceRxValue[3];

            amBeAlignmentRx1.Value = faBeAlignmentRxValue[0];
            amBeAlignmentRx2.Value = faBeAlignmentRxValue[1];
            amBeAlignmentRx3.Value = faBeAlignmentRxValue[2];
            amBeAlignmentRx4.Value = faBeAlignmentRxValue[3];

            tbBeAlignmentVcc.Text = faBeAlignmentInfoValue[0].ToString();
            tbBeAlignmentTemperature.Text = faBeAlignmentInfoValue[1].ToString();
            tbBeAlignmentTx1Bias.Text = faBeAlignmentInfoValue[2].ToString();
            tbBeAlignmentTx2Bias.Text = faBeAlignmentInfoValue[3].ToString();
            tbBeAlignmentTx3Bias.Text = faBeAlignmentInfoValue[4].ToString();
            tbBeAlignmentTx4Bias.Text = faBeAlignmentInfoValue[5].ToString();

            tbLightSourceVcc.Text = faLightSourceInfoValue[0].ToString();
            tbLightSourceTemperature.Text = faLightSourceInfoValue[1].ToString();
            tbLightSourceTx1Bias.Text = faLightSourceInfoValue[2].ToString();
            tbLightSourceTx2Bias.Text = faLightSourceInfoValue[3].ToString();
            tbLightSourceTx3Bias.Text = faLightSourceInfoValue[4].ToString();
            tbLightSourceTx4Bias.Text = faLightSourceInfoValue[5].ToString();
        }

        public UcLensAlignment()
        {
            InitializeComponent();

            bwMonitor = new BackgroundWorker();
            bwMonitor.WorkerReportsProgress = true;
            bwMonitor.WorkerSupportsCancellation = false;
            bwMonitor.DoWork += new DoWorkEventHandler(MonitorValueUpdateApi);
            bwMonitor.ProgressChanged += new ProgressChangedEventHandler(MonitorProgressChangedApi);
        }

        public int SetLightSourceI2cReadCBApi(LightSourceI2cReadCB cb)
        {
            if (cb == null)
                return -1;

            lightSourceI2cReadCB = new LightSourceI2cReadCB(cb);

            return 0;
        }

        public int SetLightSourceI2cWriteCBApi(LightSourceI2cWriteCB cb)
        {
            if (cb == null)
                return -1;

            lightSourceI2cWriteCB = new LightSourceI2cWriteCB(cb);

            return 0;
        }

        public int SetBeAlignmentI2cReadCBApi(BeAlignmentI2cReadCB cb)
        {
            if (cb == null)
                return -1;

            beAlignmentI2cReadCB = new BeAlignmentI2cReadCB(cb);

            return 0;
        }

        public int SetBeAlignmentI2cWriteCBApi(BeAlignmentI2cWriteCB cb)
        {
            if (cb == null)
                return -1;

            beAlignmentI2cWriteCB = new BeAlignmentI2cWriteCB(cb);

            return 0;
        }

        public float GetBeAlignmentMpd1ValueApi()
        {
            return amBeAlignmentMpd1.Value;
        }

        public float GetBeAlignmentMpd2ValueApi()
        {
            return amBeAlignmentMpd2.Value;
        }

        public float GetBeAlignmentMpd3ValueApi()
        {
            return amBeAlignmentMpd3.Value;
        }

        public float GetBeAlignmentMpd4ValueApi()
        {
            return amBeAlignmentMpd4.Value;
        }

        public float GetBeAlignmentRx1ValueApi()
        {
            return amBeAlignmentRx1.Value;
        }

        public float GetBeAlignmentRx2ValueApi()
        {
            return amBeAlignmentRx2.Value;
        }

        public float GetBeAlignmentRx3ValueApi()
        {
            return amBeAlignmentRx3.Value;
        }

        public float GetBeAlignmentRx4ValueApi()
        {
            return amBeAlignmentRx4.Value;
        }

        public float GetLightSourceRx1ValueApi()
        {
            return amLightSourceRx1.Value;
        }

        public float GetLightSourceRx2ValueApi()
        {
            return amLightSourceRx2.Value;
        }

        public float GetLightSourceRx3ValueApi()
        {
            return amLightSourceRx3.Value;
        }

        public float GetLightSourceRx4ValueApi()
        {
            return amLightSourceRx4.Value;
        }

        public void MonitorUpdateStartApi()
        {
            if ((bwMonitor.IsBusy != true) && (startMonitor == false)) {
                startMonitor = true;
                bwMonitor.RunWorkerAsync();
            }
        }

        public void MonitorUpdateStopApi()
        {
            startMonitor = false;
        }

        public int GetBeAlignmentDeviceAddrApi()
        { 
            int devAddr;

            int.TryParse(tbBeAlignmentDeviceAddr.Text, out devAddr);

            return devAddr;
        }

        private void tbBeAlignmentMpdLimit_TextChanged(object sender, EventArgs e)
        {
            float tmpF;
            if (tbBeAlignmentMpdLimit.Text.Length == 0)
                return;
            float.TryParse(tbBeAlignmentMpdLimit.Text, out tmpF);
            amBeAlignmentMpd1.MaxThreshold = amBeAlignmentMpd2.MaxThreshold = amBeAlignmentMpd3.MaxThreshold = amBeAlignmentMpd4.MaxThreshold = tmpF;
        }

        private void tbBeAlignmentRxLimit_TextChanged(object sender, EventArgs e)
        {
            float tmpF;
            if (tbBeAlignmentRxLimit.Text.Length == 0)
                return;
            float.TryParse(tbBeAlignmentRxLimit.Text, out tmpF);
            amBeAlignmentRx4.MaxThreshold = amBeAlignmentRx3.MaxThreshold = amBeAlignmentRx2.MaxThreshold = amBeAlignmentRx1.MaxThreshold = tmpF;
        }

        private void tbLightSourceRxLimit_TextChanged(object sender, EventArgs e)
        {
            float tmpF;
            if (tbLightSourceRxLimit.Text.Length == 0)
                return;
            float.TryParse(tbLightSourceRxLimit.Text, out tmpF);
            amLightSourceRx1.MaxThreshold = amLightSourceRx2.MaxThreshold = amLightSourceRx3.MaxThreshold = amLightSourceRx4.MaxThreshold = tmpF;
        }

        private void bClearAllMaxValue_Click(object sender, EventArgs e)
        {
            amBeAlignmentMpd1.ClearMaxValueApi();
            amBeAlignmentMpd2.ClearMaxValueApi();
            amBeAlignmentMpd3.ClearMaxValueApi();
            amBeAlignmentMpd4.ClearMaxValueApi();

            amLightSourceRx1.ClearMaxValueApi();
            amLightSourceRx2.ClearMaxValueApi();
            amLightSourceRx3.ClearMaxValueApi();
            amLightSourceRx4.ClearMaxValueApi();

            amBeAlignmentRx1.ClearMaxValueApi();
            amBeAlignmentRx2.ClearMaxValueApi();
            amBeAlignmentRx3.ClearMaxValueApi();
            amBeAlignmentRx4.ClearMaxValueApi();
        }

        private void tbBeAlignmentMpdMaxRange_TextChanged(object sender, EventArgs e)
        {
            float tmpF;
            if (tbBeAlignmentMpdMaxRange.Text.Length == 0)
                return;
            float.TryParse(tbBeAlignmentMpdMaxRange.Text, out tmpF);
            amBeAlignmentMpd1.MaxRange = amBeAlignmentMpd2.MaxRange = amBeAlignmentMpd3.MaxRange = amBeAlignmentMpd4.MaxRange = tmpF;
        }

        private void tbLightSourceRxMaxRange_TextChanged(object sender, EventArgs e)
        {
            float tmpF;
            if (tbLightSourceRxMaxRange.Text.Length == 0)
                return;
            float.TryParse(tbLightSourceRxMaxRange.Text, out tmpF);
            amLightSourceRx1.MaxRange = amLightSourceRx2.MaxRange = amLightSourceRx3.MaxRange = amLightSourceRx4.MaxRange = tmpF;
        }

        private void tbBeAlignmentRxMaxRange_TextChanged(object sender, EventArgs e)
        {
            float tmpF;
            if (tbBeAlignmentRxMaxRange.Text.Length == 0)
                return;
            float.TryParse(tbBeAlignmentRxMaxRange.Text, out tmpF);
            amBeAlignmentRx4.MaxRange = amBeAlignmentRx3.MaxRange = amBeAlignmentRx2.MaxRange = amBeAlignmentRx1.MaxRange = tmpF;
        }
    }
}
