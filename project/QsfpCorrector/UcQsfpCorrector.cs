using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using System.Windows.Forms;

using I2cMasterInterface;

namespace QsfpCorrector
{
    public partial class UcQsfpCorrector : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private I2cReadCB qsfpI2cReadCB = null;
        private I2cWriteCB qsfpI2cWriteCB = null;

        private DataTable dtRegValueLowPage = new DataTable();
        private DataTable dtRegValueUpPage0 = new DataTable();
        private DataTable dtRegValueUpPage3 = new DataTable();
        private DataTable dtRegValueUpPage4 = new DataTable();
        private DataTable dtRegValueUpPage5 = new DataTable();
        private BackgroundWorker bwAutoRead;
        private String fileDirectory = "d:\\QsfpCorrectorLog";
        private volatile String voltageValue, temperatureValue, ycSerialNumber, xaSerialNumber, firmwareDataCode;
        private volatile String result;
        private volatile SByte voltageOffsetValue, temperatureOffsetValue;
        private volatile byte[] rxRate = new byte[4];
        private volatile String[] txBiasValue = new String[4];
        private volatile String[] rxPowerValue = new String[4];
        private volatile String[] rxRssiValue = new String[4];
        private volatile String[] rxPowerRate = new String[4];
        private bool autoReadStart = false;
        private bool corrector = false;

        public UcQsfpCorrector()
        {
            int i;

            InitializeComponent();

            result = ycSerialNumber = xaSerialNumber = "";

            bwAutoRead = new BackgroundWorker();
            bwAutoRead.WorkerReportsProgress = true;
            bwAutoRead.WorkerSupportsCancellation = false;
            bwAutoRead.DoWork += new DoWorkEventHandler(MonitorValueUpdateApi);
            bwAutoRead.ProgressChanged += new ProgressChangedEventHandler(MonitorProgressChangedApi);

            dtRegValueLowPage.Columns.Add("Addr", typeof(String));
            dtRegValueLowPage.Columns.Add("Value", typeof(String));
            dtRegValueLowPage.Columns.Add("Read", typeof(String));
            dtRegValueUpPage0.Columns.Add("Addr", typeof(String));
            dtRegValueUpPage0.Columns.Add("Value", typeof(String));
            dtRegValueUpPage0.Columns.Add("Read", typeof(String));
            dtRegValueUpPage3.Columns.Add("Addr", typeof(String));
            dtRegValueUpPage3.Columns.Add("Value", typeof(String));
            dtRegValueUpPage3.Columns.Add("Read", typeof(String));
            dtRegValueUpPage4.Columns.Add("Addr", typeof(String));
            dtRegValueUpPage4.Columns.Add("Value", typeof(String));
            dtRegValueUpPage4.Columns.Add("Read", typeof(String));
            dtRegValueUpPage5.Columns.Add("Addr", typeof(String));
            dtRegValueUpPage5.Columns.Add("Value", typeof(String));
            dtRegValueUpPage5.Columns.Add("Read", typeof(String));

            for (i = 0; i < 128; i++)
                dtRegValueLowPage.Rows.Add(i.ToString("000"), "NA");

            for (i = 128; i < 256; i++) {
                dtRegValueUpPage0.Rows.Add(i.ToString("000"), "NA");
                dtRegValueUpPage3.Rows.Add(i.ToString("000"), "NA");
                dtRegValueUpPage4.Rows.Add(i.ToString("000"), "NA");
                dtRegValueUpPage5.Rows.Add(i.ToString("000"), "NA");
            }

            dgvRegisterValueLowPage.DataSource = dtRegValueLowPage;
            dgvRegisterValueUpPage0.DataSource = dtRegValueUpPage0;
            dgvRegisterValueUpPage3.DataSource = dtRegValueUpPage3;
            dgvRegisterValueUpPage4.DataSource = dtRegValueUpPage4;
            dgvRegisterValueUpPage5.DataSource = dtRegValueUpPage5;
        }

        public int SetQsfpI2cReadCBApi(I2cReadCB cb)
        {
            if (cb == null)
                return -1;

            qsfpI2cReadCB = new I2cReadCB(cb);

            return 0;
        }

        public int SetQsfpI2cWriteCBApi(I2cWriteCB cb)
        {
            if (cb == null)
                return -1;

            qsfpI2cWriteCB = new I2cWriteCB(cb);

            return 0;
        }

        public int StartAutoReadApi()
        {
            if (autoReadStart == true)
                return 0;

            autoReadStart = true;
            bwAutoRead.RunWorkerAsync();

            if (tbConfigFilePath.Text.Length != 0)
                bCorrector.Enabled = true;
            else
                bCorrector.Enabled = false;

            return 0;
        }

        public int StopAutoReadApi()
        {
            if (autoReadStart == false)
                return 0;

            autoReadStart = false;

            return 0;
        }

        private int _WritePassword()
        {
            byte[] data = new byte[4];

            if (qsfpI2cWriteCB == null)
                return -1;

            if ((tbPassword123.Text.Length == 0) || (tbPassword124.Text.Length == 0) ||
                (tbPassword125.Text.Length == 0) || (tbPassword126.Text.Length == 0)) {
                MessageBox.Show("Please input 4 hex value password before write!!");
                return -1;
            }

            try {
                data[0] = (byte)Convert.ToInt32(tbPassword123.Text, 16);
                data[1] = (byte)Convert.ToInt32(tbPassword124.Text, 16);
                data[2] = (byte)Convert.ToInt32(tbPassword125.Text, 16);
                data[3] = (byte)Convert.ToInt32(tbPassword126.Text, 16);
            }
            catch (Exception e) {
                MessageBox.Show(e.ToString());
                return -1;
            }

            if (qsfpI2cWriteCB(80, 123, 4, data) < 0)
                return -1;

            return 0;
        }

        private int _ClearPassword()
        {
            byte[] data = new byte[4];

            if (qsfpI2cWriteCB == null)
                return -1;

            data[0] = data[1] = data[2] = data[3] = 0;

            if (qsfpI2cWriteCB(80, 123, 4, data) < 0)
                return -1;

            return 0;
        }

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[] {32};

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            data[0] = mode;

            if (qsfpI2cWriteCB(80, 164, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _ReadVoltageValue()
        {
            byte[] data = new byte[2];
            byte[] bATmp = new byte[2];
            sbyte[] sData = new sbyte[1];
            byte[] reverseData;
            float fTmp;

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 26, 2, data) != 2)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                goto clearData;
            }

            reverseData = bATmp.Reverse().ToArray();
            fTmp = BitConverter.ToUInt16(reverseData, 0);
            fTmp = fTmp / 10000;
            voltageValue = fTmp.ToString();

            data[0] = 4;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                goto clearData;

            Thread.Sleep(10); //Wait change page

            if (qsfpI2cReadCB(80, 240, 1, data) != 1)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, sData, 0, 1);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                goto clearData;
            }

            voltageOffsetValue = sData[0];

            return 0;

        clearData:
            voltageValue = "NA";
            voltageOffsetValue = 0;
            return -1;
        }

        private int _VoltageCorrector()
        {
            double dTmp, readVoltage, inputVoltage, offsetValue;
            byte[] data = new byte[1];
            sbyte[] sData = new sbyte[1];
            bool writeError;

            double.TryParse(voltageValue, out readVoltage);
            double.TryParse(tbInputVoltage.Text, out inputVoltage);

            dTmp = readVoltage - (voltageOffsetValue * 0.025);
            offsetValue = (float)((inputVoltage - dTmp) * 40);
            if ((offsetValue < -128) || (offsetValue > 127)) {
                result += "Voltage offset(" + offsetValue.ToString() + ") out of range error, ";
                return -1;
            }

            try {
                sData[0] = Convert.ToSByte(offsetValue);
            }
            catch (Exception eTSB) {
                result += "Convert.ToSByte(offsetValue) fail:" + eTSB.ToString() + ", ";
                return -1;
            }

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            do {
                writeError = false;

                data[0] = 4;
                if (qsfpI2cWriteCB(80, 127, 1, data) < 0) {
                    writeError = true;
                    continue;
                }

                try { 
                    Buffer.BlockCopy(sData, 0, data, 0, 1);
                }
                catch (Exception eBC) {
                    result += "Buffer.BlockCopy fail:" + eBC.ToString() + ", ";
                    return -1;
                }

                if (qsfpI2cWriteCB(80, 240, 1, data) < 0) {
                    writeError = true;
                    continue;
                }

            } while (writeError == true);
            
            return 0;
        }

        private int _ReadTemperatureValue()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            sbyte[] sData = new sbyte[1];
            byte[] reverseData;
            float fTmp;

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 22, 2, data) != 2)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            reverseData = bATmp.Reverse().ToArray();
            fTmp = BitConverter.ToInt16(reverseData, 0);
            fTmp = fTmp / 256;
            temperatureValue = fTmp.ToString();

            data[0] = 4;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                goto clearData;

            Thread.Sleep(10); //Wait change page

            if (qsfpI2cReadCB(80, 241, 1, data) != 1)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, sData, 0, 1);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                goto clearData;
            }
            temperatureOffsetValue = sData[0];
            
            return 0;

        clearData:
            temperatureValue = "NA";
            temperatureOffsetValue = 0;

            return 0;
        }

        private int _TemperatureCorrector()
        {
            float fTmp;
            byte[] data = new byte[1];
            sbyte[] sData = new sbyte[1];
            bool writeError;

            float.TryParse(tbDefaultTemperatureOffset.Text, out fTmp);

            try {
                sData[0] = Convert.ToSByte(tbDefaultTemperatureOffset.Text);
            }
            catch (Exception eTSB) {
                result += "Convert.ToSByte fail:" + eTSB.ToString() + ", ";
                return -1;
            }

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            do
            {
                writeError = false;

                data[0] = 4;
                if (qsfpI2cWriteCB(80, 127, 1, data) < 0) {
                    writeError = true;
                    continue;
                }

                try {
                    Buffer.BlockCopy(sData, 0, data, 0, 1);
                }
                catch (Exception eBC) {
                    result += "Buffer.BlockCopy fail:" + eBC.ToString() + ", ";
                    return -1;
                }

                if (qsfpI2cWriteCB(80, 241, 1, data) < 0) {
                    writeError = true;
                    continue;
                }

            } while (writeError == true);

            return 0;
        }

        private int _ReadTxBiasValue()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            float fBias;

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 42, 8, data) != 8)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                result += "Buffer.BlockCopy fail:" + eBC.ToString() + ", ";
                goto clearData;
            }

            reverseData = bATmp.Reverse().ToArray();
            fBias = BitConverter.ToUInt16(reverseData, 0);
            fBias = fBias / 500;
            txBiasValue[0] = fBias.ToString();

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                result += "Buffer.BlockCopy fail:" + eBC.ToString() + ", ";
                goto clearData;
            }

            reverseData = bATmp.Reverse().ToArray();
            fBias = BitConverter.ToUInt16(reverseData, 0);
            fBias = fBias / 500;
            txBiasValue[1] = fBias.ToString();

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                result += "Buffer.BlockCopy fail:" + eBC.ToString() + ", ";
                goto clearData;
            }

            reverseData = bATmp.Reverse().ToArray();
            fBias = BitConverter.ToUInt16(reverseData, 0);
            fBias = fBias / 500;
            txBiasValue[2] = fBias.ToString();

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                result += "Buffer.BlockCopy fail:" + eBC.ToString() + ", ";
                goto clearData;
            }

            reverseData = bATmp.Reverse().ToArray();
            fBias = BitConverter.ToUInt16(reverseData, 0);
            fBias = fBias / 500;
            txBiasValue[3] = fBias.ToString();

            return 0;

        clearData:
            txBiasValue[0] = txBiasValue[1] = txBiasValue[2] = txBiasValue[3] = "NA";
            
            return -1;
        }

        private int _ReadRxRssiValue()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            int page, addr;
            
            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            int.TryParse(tbRssiRegisterPage.Text, out page);
            int.TryParse(tbRssiRegisterAddr.Text, out addr);

            data[0] = (byte)page;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                goto clearData;

            Thread.Sleep(10); //Wait change page

            if (qsfpI2cReadCB(80, (byte)addr, 8, data) != 8)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                result += "Buffer.BlockCopy fail:" + eBC.ToString() + ", ";
                goto clearData;
            }
            reverseData = bATmp.Reverse().ToArray();
            rxRssiValue[0] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                result += "Buffer.BlockCopy fail:" + eBC.ToString() + ", ";
                goto clearData;
            }
            reverseData = bATmp.Reverse().ToArray();
            rxRssiValue[1] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                result += "Buffer.BlockCopy fail:" + eBC.ToString() + ", ";
                goto clearData;
            }
            reverseData = bATmp.Reverse().ToArray();
            rxRssiValue[2] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                result += "Buffer.BlockCopy fail:" + eBC.ToString() + ", ";
                goto clearData;
            }
            reverseData = bATmp.Reverse().ToArray();
            rxRssiValue[3] = (BitConverter.ToUInt16(reverseData, 0)).ToString();

            return 0;

        clearData:
            rxRssiValue[0] = rxRssiValue[1] = rxRssiValue[2] = rxRssiValue[3] = "NA";

            return -1;
        }
      
        private int _ReadRxPowerValue()
        {
            byte[] data = new byte[8];
            byte[] bATmp = new byte[2];
            byte[] reverseData;
            int iTmp;
            float fPower;

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 34, 8, data) != 8)
                goto clearData;

            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                result += "Buffer.BlockCopy fail:" + eBC.ToString() + ", ";
                goto clearData;
            }
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToInt16(reverseData, 0);
            fPower = iTmp / 10;
            rxPowerValue[0] = fPower.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                result += "Buffer.BlockCopy fail:" + eBC.ToString() + ", ";
                goto clearData;
            }
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToInt16(reverseData, 0);
            fPower = iTmp / 10;
            rxPowerValue[1] = fPower.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                result += "Buffer.BlockCopy fail:" + eBC.ToString() + ", ";
                goto clearData;
            }
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToInt16(reverseData, 0);
            fPower = iTmp / 10;
            rxPowerValue[2] = fPower.ToString("#0.0");

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                result += "Buffer.BlockCopy fail:" + eBC.ToString() + ", ";
                goto clearData;
            }
            reverseData = bATmp.Reverse().ToArray();
            iTmp = BitConverter.ToInt16(reverseData, 0);
            fPower = iTmp / 10;
            rxPowerValue[3] = fPower.ToString("#0.0");

            data[0] = 4;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                goto clearData;

            Thread.Sleep(10); //Wait change page

            if (qsfpI2cReadCB(80, 244, 4, data) != 4)
                goto clearData;

            rxRate[0] = data[0];
            rxRate[1] = data[1];
            rxRate[2] = data[2];
            rxRate[3] = data[3];

            return 0;

        clearData:
            rxPowerValue[0] = rxPowerValue[1] = rxPowerValue[2] = rxPowerValue[3] = "NA";
            rxRate[0] = rxRate[1] = rxRate[2] = rxRate[3] = 0;

            return -1;
        }

        private int _RxPowerCorrector()
        {
            byte[] data = new byte[1];
            float numerator, denominator, input, rssi;
            int rate;

            float.TryParse(tbRssiRateNumerator.Text, out numerator);
            float.TryParse(tbRssiRateDenominator.Text, out denominator);

            try {
                input = Convert.ToSingle(tbInputPower1.Text) * numerator / denominator;
                rssi = Convert.ToSingle(rxRssiValue[0]);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
            }
            catch (Exception eCT) {
                result += "Convert error: " + eCT.ToString() + ", ";
                return -1;
            }

            if (rate < 256)
                rxRate[0] = (byte)rate;

            try {
                input = Convert.ToSingle(tbInputPower2.Text) * numerator / denominator;
                rssi = Convert.ToSingle(rxRssiValue[1]);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
            }
            catch (Exception eCT) {
                result += "Convert error: " + eCT.ToString() + ", ";
                return -1;
            }

            if (rate < 256)
                rxRate[1] = (byte)rate;

            try {
                input = Convert.ToSingle(tbInputPower3.Text) * numerator / denominator;
                rssi = Convert.ToSingle(rxRssiValue[2]);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
            }
            catch (Exception eCT) {
                result += "Convert error: " + eCT.ToString() + ", ";
                return -1;
            }

            if (rate < 256)
                rxRate[2] = (byte)rate;

            try {
                input = Convert.ToSingle(tbInputPower4.Text) * numerator / denominator;
                rssi = Convert.ToSingle(rxRssiValue[3]);
                rate = Convert.ToInt32(Math.Ceiling(rssi * 100 / input));
            }
            catch (Exception eCT) {
                result += "Convert error: " + eCT.ToString() + ", ";
                return -1;
            }

            if (rate < 256)
                rxRate[3] = (byte)rate;

            data[0] = 4;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            Thread.Sleep(10); //Wait change page

            if (qsfpI2cWriteCB(80, 244, 4, rxRate) < 0)
                return -1;

            return 0;
        }

        private int _ReadSerialNumberValue()
        {
            byte[] data = new byte[16];

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            data[0] = 5;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                goto clearData;

            Thread.Sleep(10);

            if (qsfpI2cReadCB(80, 220, 16, data) != 16)
                goto clearData;

            ycSerialNumber = System.Text.Encoding.Default.GetString(data);

            if (qsfpI2cReadCB(80, 236, 16, data) != 16)
                goto clearData;

            xaSerialNumber = System.Text.Encoding.Default.GetString(data);

            data[0] = 32;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                goto clearData;

            Thread.Sleep(10);

            data = new byte[8];
            if (qsfpI2cReadCB(80, 167, 8, data) != 8)
                goto clearData;

            firmwareDataCode = System.Text.Encoding.Default.GetString(data);

            return 0;

        clearData:
            ycSerialNumber = xaSerialNumber = firmwareDataCode = "";
            return -1;
        }

        private int _WriteCustomerSn()
        {
            byte[] bTmp;
            byte[] data = new byte[16];
            int i;

            if (qsfpI2cWriteCB == null)
                return -1;

            data[0] = 0;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                goto clearData;

            bTmp = System.Text.Encoding.Default.GetBytes(tbCustomerSerialNumber.Text);
            for (i = 0; i < data.Length; i++) {
                if (i < bTmp.Length)
                    data[i] = bTmp[i];
                else
                    data[i] = 0;
            }

            if (qsfpI2cWriteCB(80, 196, 16, data) < 0)
                goto clearData;

            return 0;

        clearData:

            return -1;
        }

        private int _ClearAlarmValue()
        {
            byte[] data = new byte[8];

            if (qsfpI2cReadCB == null)
                return -1;

            //LOS&Fault
            if (qsfpI2cReadCB(80, 3, 2, data) != 2)
                goto clearData;
            
            //Alarm&Warning
            if (qsfpI2cReadCB(80, 6, 2, data) != 2)
                goto clearData;
            if (qsfpI2cReadCB(80, 9, 4, data) != 4)
                goto clearData;

            return 0;

        clearData:
            return -1;
        }

        private int _GetModuleMonitorValue()
        {
            if (_ReadVoltageValue() < 0)
                return -1;

            if (_ReadTemperatureValue() < 0)
                return -1;

            if (_ReadTxBiasValue() < 0)
                return -1;

            if (_ReadRxPowerValue() < 0)
                return -1;

            if (_ReadSerialNumberValue() < 0)
                return -1;

            return 0;
        }

        private int _StoreIntoFlash()
        {
            byte[] data = new byte[1];

            if (qsfpI2cWriteCB == null)
                return -1;

            data[0] = 32;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            data[0] = 0xAA;
            
            if (qsfpI2cWriteCB(80, 162, 1, data) < 0)
                return -1;

            return 0;
        }

        public void MonitorValueUpdateApi(object sender, DoWorkEventArgs e)
        {
            bool bGetModuleMonitorValueError;            

            while (autoReadStart) {
                bGetModuleMonitorValueError = false;

                if (corrector == true) {
                    bwAutoRead.ReportProgress(1, null);
                    if (_ReadVoltageValue() < 0) {
                        bGetModuleMonitorValueError = true;
                        break;
                    }

                    if (_ReadTemperatureValue() < 0) {
                        bGetModuleMonitorValueError = true;
                        break;
                    }

                    if (_ReadRxRssiValue() < 0) {
                        bGetModuleMonitorValueError = true;
                        break;
                    }

                    if (_WritePassword() < 0) {
                        bGetModuleMonitorValueError = true;
                        break;
                    }

                    if (_SetQsfpMode(0x4D) < 0) {
                        bGetModuleMonitorValueError = true;
                        break;
                    }

                    bwAutoRead.ReportProgress(2, null);
                    if (_VoltageCorrector() < 0) {
                        bGetModuleMonitorValueError = true;
                        break;
                    }

                    bwAutoRead.ReportProgress(3, null);
                    if (_TemperatureCorrector() < 0) {
                        bGetModuleMonitorValueError = true;
                        break;
                    }

                    bwAutoRead.ReportProgress(4, null);
                    if (_RxPowerCorrector() < 0) {
                        bGetModuleMonitorValueError = true;
                        break;
                    }

                    bwAutoRead.ReportProgress(5, null);
                    if (_WriteCustomerSn() < 0) {
                        bGetModuleMonitorValueError = true;
                        break;
                    }

                    bwAutoRead.ReportProgress(6, null);
                    if (_StoreIntoFlash() < 0) {
                        bGetModuleMonitorValueError = true;
                        break;
                    }
                
                    Thread.Sleep(100);

                    bwAutoRead.ReportProgress(7, null);
                    if (_ClearAlarmValue() < 0) {
                        bGetModuleMonitorValueError = true;
                        break;
                    }

                    if (_GetModuleMonitorValue() < 0) {
                        bGetModuleMonitorValueError = true;
                        break;
                    }

                    bwAutoRead.ReportProgress(99, null);
                    while (corrector == true)
                        Thread.Sleep(500);
                }
                else
                {
                    
                    if (_GetModuleMonitorValue() < 0)
                        bGetModuleMonitorValueError = true;
                    else
                        bwAutoRead.ReportProgress(0, null);
                }

                if (bGetModuleMonitorValueError == false)
                    Thread.Sleep(100);
                else
                    Thread.Sleep(500);
            }

            bwAutoRead.ReportProgress(100, null);
        }

        private int _UpdateVoltageValueGui()
        {
            float fTmp, fThresholdMax, fThresholdMin;
            bool pass = true;

            fTmp = fThresholdMax = fThresholdMin  = 0;
            if (!voltageValue.Equals("NA"))
            {
                float.TryParse(tbThresholdVoltageMin.Text, out fThresholdMin);
                float.TryParse(tbThresholdVoltageMax.Text, out fThresholdMax);
                float.TryParse(voltageValue, out fTmp);
                if ((fTmp < fThresholdMin) || (fTmp > fThresholdMax)) {
                    tbVoltage.ForeColor = System.Drawing.Color.Red;
                    pass = false;
                    result += "Voltage out of threshold!!, ";
                }
                else
                    tbVoltage.ForeColor = SystemColors.ControlText;
            }
            tbVoltage.Text = voltageValue;
            tbVoltage.Update();

            tbVoltageOffset.Text = voltageOffsetValue.ToString();
            tbVoltageOffset.Update();

            if (pass == true)
                return 0;
            else
                return -1;
        }

        private int _UpdateTemperatureValueGui()
        {
            float fTmp, fThresholdMax, fThresholdMin;
            bool pass = true;

            fTmp = fThresholdMax = fThresholdMin = 0;
            if (!temperatureValue.Equals("NA"))
            {
                float.TryParse(tbThresholdTemperatureMin.Text, out fThresholdMin);
                float.TryParse(tbThresholdTemperatureMax.Text, out fThresholdMax);
                float.TryParse(temperatureValue, out fTmp);
                if ((fTmp < fThresholdMin) || (fTmp > fThresholdMax)) {
                    tbTemperature.ForeColor = System.Drawing.Color.Red;
                    pass = false;
                    result += "Temperature out of threshold!!, ";
                }
                else
                    tbTemperature.ForeColor = SystemColors.ControlText;
            }
            tbTemperature.Text = temperatureValue;
            tbTemperature.Update();

            tbTemperatureOffset.Text = temperatureOffsetValue.ToString();
            tbTemperatureOffset.Update();

            if (pass == true)
                return 0;
            else
                return -1;
        }

        private int _UpdateTxBiasValueGui()
        {
            float fTmp, fThresholdMax, fThresholdMin;
            bool pass = true;

            fTmp = fThresholdMax = fThresholdMin = 0;
            if (!txBiasValue[0].Equals("NA"))
            {
                float.TryParse(tbThresholdBias1Min.Text, out fThresholdMin);
                float.TryParse(tbThresholdBias1Max.Text, out fThresholdMax);
                float.TryParse(txBiasValue[0], out fTmp);
                if ((fTmp < fThresholdMin) || (fTmp > fThresholdMax)) {
                    tbTxBias1.ForeColor = System.Drawing.Color.Red;
                    pass = false;
                    result += "TxBias1 out of threshold!!, ";
                }
                else
                    tbTxBias1.ForeColor = SystemColors.ControlText;
            }
            tbTxBias1.Text = txBiasValue[0];
            tbTxBias1.Update();

            if (!txBiasValue[1].Equals("NA"))
            {
                float.TryParse(tbThresholdBias2Min.Text, out fThresholdMin);
                float.TryParse(tbThresholdBias2Max.Text, out fThresholdMax);
                float.TryParse(txBiasValue[1], out fTmp);
                if ((fTmp < fThresholdMin) || (fTmp > fThresholdMax)) {
                    tbTxBias2.ForeColor = System.Drawing.Color.Red;
                    pass = false;
                    result += "TxBias2 out of threshold!!, ";
                }
                else
                    tbTxBias2.ForeColor = SystemColors.ControlText;
            }
            tbTxBias2.Text = txBiasValue[1];
            tbTxBias2.Update();

            if (!txBiasValue[2].Equals("NA"))
            {
                float.TryParse(tbThresholdBias3Min.Text, out fThresholdMin);
                float.TryParse(tbThresholdBias3Max.Text, out fThresholdMax);
                float.TryParse(txBiasValue[2], out fTmp);
                if ((fTmp < fThresholdMin) || (fTmp > fThresholdMax)) {
                    tbTxBias3.ForeColor = System.Drawing.Color.Red;
                    pass = false;
                    result += "TxBias3 out of threshold!!, ";
                }
                else
                    tbTxBias3.ForeColor = SystemColors.ControlText;
            }
            tbTxBias3.Text = txBiasValue[1];
            tbTxBias3.Update();

            if (!txBiasValue[3].Equals("NA"))
            {
                float.TryParse(tbThresholdBias2Min.Text, out fThresholdMin);
                float.TryParse(tbThresholdBias2Max.Text, out fThresholdMax);
                float.TryParse(txBiasValue[3], out fTmp);
                if ((fTmp < fThresholdMin) || (fTmp > fThresholdMax)) {
                    tbTxBias4.ForeColor = System.Drawing.Color.Red;
                    pass = false;
                    result += "TxBias4 out of threshold!!, ";
                }
                else
                    tbTxBias4.ForeColor = SystemColors.ControlText;
            }
            tbTxBias4.Text = txBiasValue[3];
            tbTxBias4.Update();

            if (pass == true)
                return 0;
            else
                return -1;
        }

        private int _UpdateRxPowerValueGui()
        {
            float fTmp, fThresholdMax, fThresholdMin;
            bool pass = true;

            fTmp = fThresholdMax = fThresholdMin = 0;

            if (!rxPowerValue[0].Equals("NA"))
            {
                float.TryParse(tbThresholdRx1Min.Text, out fThresholdMin);
                float.TryParse(tbThresholdRx1Max.Text, out fThresholdMax);
                float.TryParse(rxPowerValue[0], out fTmp);
                if ((fTmp < fThresholdMin) || (fTmp > fThresholdMax)) {
                    tbRxPower1.ForeColor = System.Drawing.Color.Red;
                    pass = false;
                    result += "RxPower1 out of threshold!!, ";
                }
                else
                    tbRxPower1.ForeColor = SystemColors.ControlText;
            }
            tbRxPower1.Text = rxPowerValue[0];
            tbRxPower1.Update();

            if (!rxPowerValue[1].Equals("NA"))
            {
                float.TryParse(tbThresholdRx2Min.Text, out fThresholdMin);
                float.TryParse(tbThresholdRx2Max.Text, out fThresholdMax);
                float.TryParse(rxPowerValue[1], out fTmp);
                if ((fTmp < fThresholdMin) || (fTmp > fThresholdMax)) {
                    tbRxPower2.ForeColor = System.Drawing.Color.Red;
                    pass = false;
                    result += "RxPower2 out of threshold!!, ";
                }
                else
                    tbRxPower2.ForeColor = SystemColors.ControlText;
            }
            tbRxPower2.Text = rxPowerValue[1];
            tbRxPower2.Update();

            if (!rxPowerValue[2].Equals("NA"))
            {
                float.TryParse(tbThresholdRx3Min.Text, out fThresholdMin);
                float.TryParse(tbThresholdRx3Max.Text, out fThresholdMax);
                float.TryParse(rxPowerValue[2], out fTmp);
                if ((fTmp < fThresholdMin) || (fTmp > fThresholdMax)) {
                    tbRxPower3.ForeColor = System.Drawing.Color.Red;
                    pass = false;
                    result += "RxPower3 out of threshold!!, ";
                }
                else
                    tbRxPower3.ForeColor = SystemColors.ControlText;
            }
            tbRxPower3.Text = rxPowerValue[2];
            tbRxPower3.Update();

            if (!rxPowerValue[3].Equals("NA"))
            {
                float.TryParse(tbThresholdRx4Min.Text, out fThresholdMin);
                float.TryParse(tbThresholdRx4Max.Text, out fThresholdMax);
                float.TryParse(rxPowerValue[3], out fTmp);
                if ((fTmp < fThresholdMin) || (fTmp > fThresholdMax)) {
                    tbRxPower4.ForeColor = System.Drawing.Color.Red;
                    pass = false;
                    result += "RxPower4 out of threshold!!, ";
                }
                else
                    tbRxPower4.ForeColor = SystemColors.ControlText;
            }
            tbRxPower4.Text = rxPowerValue[3];
            tbRxPower4.Update();

            if (pass == true)
                return 0;
            else
                return -1;
        }

        private int _UpdateRxRateValueGui()
        {
            int iTmp, iThresholdMax, iThresholdMin;
            bool pass = true;

            iTmp = iThresholdMax = iThresholdMin = 0;

            int.TryParse(tbRxRateMin.Text, out iThresholdMin);
            int.TryParse(tbRxRateMax.Text, out iThresholdMax);

            if (rxRate[0] != 0) {
                iTmp = rxRate[0];
                if ((iTmp < iThresholdMin) || (iTmp > iThresholdMax)) {
                    tbRxPowerRate1.ForeColor = System.Drawing.Color.Red;
                    pass = false;
                    result += "Rx1 rate out of threshold!!, ";
                }
                else
                    tbRxPowerRate1.ForeColor = SystemColors.ControlText;
            }
            tbRxPowerRate1.Text = rxRate[0].ToString();
            tbRxPowerRate1.Update();

            if (rxRate[1] != 0) {
                iTmp = rxRate[1];
                if ((iTmp < iThresholdMin) || (iTmp > iThresholdMax)) {
                    tbRxPowerRate2.ForeColor = System.Drawing.Color.Red;
                    pass = false;
                    result += "Rx2 rate out of threshold!!, ";
                }
                else
                    tbRxPowerRate2.ForeColor = SystemColors.ControlText;
            }
            tbRxPowerRate2.Text = rxRate[1].ToString();
            tbRxPowerRate2.Update();

            if (rxRate[2] != 0) {
                iTmp = rxRate[2];
                if ((iTmp < iThresholdMin) || (iTmp > iThresholdMax))
                {
                    tbRxPowerRate3.ForeColor = System.Drawing.Color.Red;
                    pass = false;
                    result += "Rx3 rate out of threshold!!, ";
                }
                else
                    tbRxPowerRate3.ForeColor = SystemColors.ControlText;
            }
            tbRxPowerRate3.Text = rxRate[2].ToString();
            tbRxPowerRate3.Update();

            if (rxRate[3] != 0) {
                iTmp = rxRate[3];
                if ((iTmp < iThresholdMin) || (iTmp > iThresholdMax)) {
                    tbRxPowerRate4.ForeColor = System.Drawing.Color.Red;
                    pass = false;
                    result += "Rx4 rate out of threshold!!, ";
                }
                else
                    tbRxPowerRate4.ForeColor = SystemColors.ControlText;
            }
            tbRxPowerRate4.Text = rxRate[3].ToString();
            tbRxPowerRate4.Update();

            if (pass == true)
                return 0;
            else
                return -1;
        }

        private int _UpdateSerialNumberGui()
        {
            tbYcSerialNumber.Text = ycSerialNumber;
            tbYcSerialNumber.Update();
            tbXaSerialNumber.Text = xaSerialNumber;
            tbXaSerialNumber.Update();
            tbDateCode.Text = firmwareDataCode;
            if (!firmwareDataCode.Equals(tbFirmwareDateCode.Text))
                tbDateCode.ForeColor = System.Drawing.Color.Red;
            else
                tbDateCode.ForeColor = SystemColors.ControlText;
            tbDateCode.Update();

            return 0;
        }

        private int _CheckFirmwareDataCode()
        {
            if (!firmwareDataCode.Equals(tbFirmwareDateCode.Text)) {
                result += "Firmware data code different!!, ";
                return -1;
            }

            return 0;
        }

        private int _UpdateTemperatureAlarmGui()
        {
            byte[] data = new byte[1];
            bool pass = true;

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 6, 1, data) != 1)
                goto clearData;

            if ((data[0] & 0x80) != 0) {
                cbTemperatureHighAlarm.BackColor = System.Drawing.Color.Red;
                cbTemperatureHighAlarm.Checked = true;
                pass = false;
                result += "Temperature high alarm!!, ";
            }
            else {
                cbTemperatureHighAlarm.BackColor = SystemColors.Control;
                cbTemperatureHighAlarm.Checked = false;
            }

            if ((data[0] & 0x40) != 0) {
                cbTemperatureLowAlarm.BackColor = System.Drawing.Color.Red;
                cbTemperatureLowAlarm.Checked = true;
                pass = false;
                result += "Temperature low alarm!!, ";
            }
            else {
                cbTemperatureLowAlarm.BackColor = SystemColors.Control;
                cbTemperatureLowAlarm.Checked = false;
            }

            if ((data[0] & 0x20) != 0) {
                cbTemperatureHighWarning.BackColor = System.Drawing.Color.Red;
                cbTemperatureHighWarning.Checked = true;
                pass = false;
                result += "Temperature high warning!!, ";
            }
            else
            {
                cbTemperatureHighWarning.BackColor = SystemColors.Control;
                cbTemperatureHighWarning.Checked = false;
            }

            if ((data[0] & 0x10) != 0) {
                cbTemperatureLowWarning.BackColor = System.Drawing.Color.Red;
                cbTemperatureLowWarning.Checked = true;
                pass = false;
                result += "Temperature low warning!!, ";
            }
            else {
                cbTemperatureLowWarning.BackColor = SystemColors.Control;
                cbTemperatureLowWarning.Checked = false;
            }

            if (pass == true)
                return 0;
            else
                return -1;

        clearData:
            cbTemperatureHighAlarm.BackColor = SystemColors.Control;
            cbTemperatureHighAlarm.Checked = false;
            cbTemperatureLowAlarm.BackColor = SystemColors.Control;
            cbTemperatureLowAlarm.Checked = false;
            cbTemperatureHighWarning.BackColor = SystemColors.Control;
            cbTemperatureHighWarning.Checked = false;
            cbTemperatureLowWarning.BackColor = SystemColors.Control;
            cbTemperatureLowWarning.Checked = false;
            return -1;
        }

        private int _UpdateVoltageAlarmGui()
        {
            byte[] data = new byte[1];
            bool pass = true;

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 7, 1, data) != 1)
                goto clearData;

            if ((data[0] & 0x80) != 0) {
                cbVoltageHighAlarm.BackColor = System.Drawing.Color.Red;
                cbVoltageHighAlarm.Checked = true;
                pass = false;
                result += "Voltage high alarm!!, ";
            }
            else {
                cbVoltageHighAlarm.BackColor = SystemColors.Control;
                cbVoltageHighAlarm.Checked = false;
            }

            if ((data[0] & 0x40) != 0) {
                cbVoltageLowAlarm.BackColor = System.Drawing.Color.Red;
                cbVoltageLowAlarm.Checked = true;
                pass = false;
                result += "Voltage low alarm!!, ";
            }
            else {
                cbVoltageLowAlarm.BackColor = SystemColors.Control;
                cbVoltageLowAlarm.Checked = false;
            }

            if ((data[0] & 0x20) != 0) {
                cbVoltageHighWarning.BackColor = System.Drawing.Color.Red;
                cbVoltageHighWarning.Checked = true;
                pass = false;
                result += "Voltage high warning!!, ";
            }
            else {
                cbVoltageHighWarning.BackColor = SystemColors.Control;
                cbVoltageHighWarning.Checked = false;
            }

            if ((data[0] & 0x10) != 0) {
                cbVoltageLowWarning.BackColor = System.Drawing.Color.Red;
                cbVoltageLowWarning.Checked = true;
                pass = false;
                result += "Voltage low warning!!, ";
            }
            else {
                cbVoltageLowWarning.BackColor = SystemColors.Control;
                cbVoltageLowWarning.Checked = false;
            }

            if (pass == true)
                return 0;
            else
                return -1;

        clearData:
            cbVoltageHighAlarm.BackColor = SystemColors.Control;
            cbVoltageHighAlarm.Checked = false;
            cbVoltageLowAlarm.BackColor = SystemColors.Control;
            cbVoltageLowAlarm.Checked = false;
            cbVoltageHighWarning.BackColor = SystemColors.Control;
            cbVoltageHighWarning.Checked = false;
            cbVoltageLowWarning.BackColor = SystemColors.Control;
            cbVoltageLowWarning.Checked = false;
            return -1;
        }
        
        private int _UpdateTxBiasAlarmGui()
        {
            byte[] data = new byte[2];
            bool pass = true;

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 11, 2, data) != 2)
                goto clearData;

            if ((data[0] & 0x80) != 0) {
                cbTxBias1HighAlarm.BackColor = System.Drawing.Color.Red;
                cbTxBias1HighAlarm.Checked = true;
                pass = false;
                result += "Tx1 bias high alarm!!, ";
            }
            else {
                cbTxBias1HighAlarm.BackColor = SystemColors.Control;
                cbTxBias1HighAlarm.Checked = false;
            }

            if ((data[0] & 0x40) != 0) {
                cbTxBias1LowAlarm.BackColor = System.Drawing.Color.Red;
                cbTxBias1LowAlarm.Checked = true;
                pass = false;
                result += "Tx1 bias low alarm!!, ";
            }
            else {
                cbTxBias1LowAlarm.BackColor = SystemColors.Control;
                cbTxBias1LowAlarm.Checked = false;
            }

            if ((data[0] & 0x20) != 0) {
                cbTxBias1HighWarning.BackColor = System.Drawing.Color.Red;
                cbTxBias1HighWarning.Checked = true;
                pass = false;
                result += "Tx1 bias high warning!!, ";
            }
            else {
                cbTxBias1HighWarning.BackColor = SystemColors.Control;
                cbTxBias1HighWarning.Checked = false;
            }

            if ((data[0] & 0x10) != 0) {
                cbTxBias1LowWarning.BackColor = System.Drawing.Color.Red;
                cbTxBias1LowWarning.Checked = true;
                pass = false;
                result += "Tx1 bias low warning!!, ";
            }
            else {
                cbTxBias1LowWarning.BackColor = SystemColors.Control;
                cbTxBias1LowWarning.Checked = false;
            }

            if ((data[0] & 0x08) != 0) {
                cbTxBias2HighAlarm.BackColor = System.Drawing.Color.Red;
                cbTxBias2HighAlarm.Checked = true;
                pass = false;
                result += "Tx2 bias high alarm!!, ";
            }
            else {
                cbTxBias2HighAlarm.BackColor = SystemColors.Control;
                cbTxBias2HighAlarm.Checked = false;
            }

            if ((data[0] & 0x04) != 0) {
                cbTxBias2LowAlarm.BackColor = System.Drawing.Color.Red;
                cbTxBias2LowAlarm.Checked = true;
                pass = false;
                result += "Tx2 bias low alarm!!, ";
            }
            else {
                cbTxBias2LowAlarm.BackColor = SystemColors.Control;
                cbTxBias2LowAlarm.Checked = false;
            }

            if ((data[0] & 0x02) != 0) {
                cbTxBias2HighWarning.BackColor = System.Drawing.Color.Red;
                cbTxBias2HighWarning.Checked = true;
                pass = false;
                result += "Tx2 bias high warning!!, ";
            }
            else {
                cbTxBias2HighWarning.BackColor = SystemColors.Control;
                cbTxBias2HighWarning.Checked = false;
            }

            if ((data[0] & 0x01) != 0) {
                cbTxBias2LowWarning.BackColor = System.Drawing.Color.Red;
                cbTxBias2LowWarning.Checked = true;
                pass = false;
                result += "Tx2 bias low warning!!, ";
            }
            else {
                cbTxBias2LowWarning.BackColor = SystemColors.Control;
                cbTxBias2LowWarning.Checked = false;
            }

            if ((data[1] & 0x80) != 0) {
                cbTxBias3HighAlarm.BackColor = System.Drawing.Color.Red;
                cbTxBias3HighAlarm.Checked = true;
                pass = false;
                result += "Tx3 bias high alarm!!, ";
            }
            else {
                cbTxBias3HighAlarm.BackColor = SystemColors.Control;
                cbTxBias3HighAlarm.Checked = false;
            }

            if ((data[1] & 0x40) != 0) {
                cbTxBias3LowAlarm.BackColor = System.Drawing.Color.Red;
                cbTxBias3LowAlarm.Checked = true;
                pass = false;
                result += "Tx3 bias low alarm!!, ";
            }
            else {
                cbTxBias3LowAlarm.BackColor = SystemColors.Control;
                cbTxBias3LowAlarm.Checked = false;
            }

            if ((data[1] & 0x20) != 0) {
                cbTxBias3HighWarning.BackColor = System.Drawing.Color.Red;
                cbTxBias3HighWarning.Checked = true;
                pass = false;
                result += "Tx3 bias high warning!!, ";
            }
            else {
                cbTxBias3HighWarning.BackColor = SystemColors.Control;
                cbTxBias3HighWarning.Checked = false;
            }

            if ((data[1] & 0x10) != 0) {
                cbTxBias3LowWarning.BackColor = System.Drawing.Color.Red;
                cbTxBias3LowWarning.Checked = true;
                pass = false;
                result += "Tx3 bias low warning!!, ";
            }
            else {
                cbTxBias3LowWarning.BackColor = SystemColors.Control;
                cbTxBias3LowWarning.Checked = false;
            }

            if ((data[1] & 0x08) != 0) {
                cbTxBias4HighAlarm.BackColor = System.Drawing.Color.Red;
                cbTxBias4HighAlarm.Checked = true;
                pass = false;
                result += "Tx4 bias high alarm!!, ";
            }
            else {
                cbTxBias4HighAlarm.BackColor = SystemColors.Control;
                cbTxBias4HighAlarm.Checked = false;
            }

            if ((data[1] & 0x04) != 0) {
                cbTxBias4LowAlarm.BackColor = System.Drawing.Color.Red;
                cbTxBias4LowAlarm.Checked = true;
                pass = false;
                result += "Tx4 bias low alarm!!, ";
            }
            else {
                cbTxBias4LowAlarm.BackColor = SystemColors.Control;
                cbTxBias4LowAlarm.Checked = false;
            }

            if ((data[1] & 0x02) != 0) {
                cbTxBias4HighWarning.BackColor = System.Drawing.Color.Red;
                cbTxBias4HighWarning.Checked = true;
                pass = false;
                result += "Tx4 bias high warning!!, ";
            }
            else {
                cbTxBias4HighWarning.BackColor = SystemColors.Control;
                cbTxBias4HighWarning.Checked = false;
            }

            if ((data[1] & 0x01) != 0) {
                cbTxBias4LowWarning.BackColor = System.Drawing.Color.Red;
                cbTxBias4LowWarning.Checked = true;
                pass = false;
                result += "Tx4 bias low warning!!, ";
            }
            else {
                cbTxBias4LowWarning.BackColor = SystemColors.Control;
                cbTxBias4LowWarning.Checked = false;
            }

            if (pass == true)
                return 0;
            else
                return -1;

        clearData:
            cbTxBias1HighAlarm.BackColor = SystemColors.Control;
            cbTxBias1HighAlarm.Checked = false;
            cbTxBias1LowAlarm.BackColor = SystemColors.Control;
            cbTxBias1LowAlarm.Checked = false;
            cbTxBias1HighWarning.BackColor = SystemColors.Control;
            cbTxBias1HighWarning.Checked = false;
            cbTxBias1LowWarning.BackColor = SystemColors.Control;
            cbTxBias1LowWarning.Checked = false;
            cbTxBias2HighAlarm.BackColor = SystemColors.Control;
            cbTxBias2HighAlarm.Checked = false;
            cbTxBias2LowAlarm.BackColor = SystemColors.Control;
            cbTxBias2LowAlarm.Checked = false;
            cbTxBias2HighWarning.BackColor = SystemColors.Control;
            cbTxBias2HighWarning.Checked = false;
            cbTxBias2LowWarning.BackColor = SystemColors.Control;
            cbTxBias2LowWarning.Checked = false;
            cbTxBias3HighAlarm.BackColor = SystemColors.Control;
            cbTxBias3HighAlarm.Checked = false;
            cbTxBias3LowAlarm.BackColor = SystemColors.Control;
            cbTxBias3LowAlarm.Checked = false;
            cbTxBias3HighWarning.BackColor = SystemColors.Control;
            cbTxBias3HighWarning.Checked = false;
            cbTxBias3LowWarning.BackColor = SystemColors.Control;
            cbTxBias3LowWarning.Checked = false;
            cbTxBias4HighAlarm.BackColor = SystemColors.Control;
            cbTxBias4HighAlarm.Checked = false;
            cbTxBias4LowAlarm.BackColor = SystemColors.Control;
            cbTxBias4LowAlarm.Checked = false;
            cbTxBias4HighWarning.BackColor = SystemColors.Control;
            cbTxBias4HighWarning.Checked = false;
            cbTxBias4LowWarning.BackColor = SystemColors.Control;
            cbTxBias4LowWarning.Checked = false;
            return -1;
        }

        private int _UpdateRxPowerAlarmGui()
        {
            byte[] data = new byte[2];
            bool pass = true;

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 9, 2, data) != 2)
                goto clearData;

            if ((data[0] & 0x80) != 0) {
                cbRx1PowerHighAlarm.BackColor = System.Drawing.Color.Red;
                cbRx1PowerHighAlarm.Checked = true;
                pass = false;
                result += "Rx1 power high alarm!!, ";
            }
            else {
                cbRx1PowerHighAlarm.BackColor = SystemColors.Control;
                cbRx1PowerHighAlarm.Checked = false;
            }

            if ((data[0] & 0x40) != 0) {
                cbRx1PowerLowAlarm.BackColor = System.Drawing.Color.Red;
                cbRx1PowerLowAlarm.Checked = true;
                pass = false;
                result += "Rx1 power low alarm!!, ";
            }
            else {
                cbRx1PowerLowAlarm.BackColor = SystemColors.Control;
                cbRx1PowerLowAlarm.Checked = false;
            }

            if ((data[0] & 0x20) != 0) {
                cbRx1PowerHighWarning.BackColor = System.Drawing.Color.Red;
                cbRx1PowerHighWarning.Checked = true;
                pass = false;
                result += "Rx1 power high warning!!, ";
            }
            else {
                cbRx1PowerHighWarning.BackColor = SystemColors.Control;
                cbRx1PowerHighWarning.Checked = false;
            }

            if ((data[0] & 0x10) != 0) {
                cbRx1PowerLowWarning.BackColor = System.Drawing.Color.Red;
                cbRx1PowerLowWarning.Checked = true;
                pass = false;
                result += "Rx1 power low warning!!, ";
            }
            else {
                cbRx1PowerLowWarning.BackColor = SystemColors.Control;
                cbRx1PowerLowWarning.Checked = false;
            }

            if ((data[0] & 0x08) != 0) {
                cbRx2PowerHighAlarm.BackColor = System.Drawing.Color.Red;
                cbRx2PowerHighAlarm.Checked = true;
                pass = false;
                result += "Rx2 power high alarm!!, ";
            }
            else {
                cbRx2PowerHighAlarm.BackColor = SystemColors.Control;
                cbRx2PowerHighAlarm.Checked = false;
            }

            if ((data[0] & 0x04) != 0) {
                cbRx2PowerLowAlarm.BackColor = System.Drawing.Color.Red;
                cbRx2PowerLowAlarm.Checked = true;
                pass = false;
                result += "Rx2 power low alarm!!, ";
            }
            else {
                cbRx2PowerLowAlarm.BackColor = SystemColors.Control;
                cbRx2PowerLowAlarm.Checked = false;
            }

            if ((data[0] & 0x02) != 0) {
                cbRx2PowerHighWarning.BackColor = System.Drawing.Color.Red;
                cbRx2PowerHighWarning.Checked = true;
                pass = false;
                result += "Rx2 power high warning!!, ";
            }
            else {
                cbRx2PowerHighWarning.BackColor = SystemColors.Control;
                cbRx2PowerHighWarning.Checked = false;
            }

            if ((data[0] & 0x01) != 0) {
                cbRx2PowerLowWarning.BackColor = System.Drawing.Color.Red;
                cbRx2PowerLowWarning.Checked = true;
                pass = false;
                result += "Rx2 power low warning!!, ";
            }
            else {
                cbRx2PowerLowWarning.BackColor = SystemColors.Control;
                cbRx2PowerLowWarning.Checked = false;
            }

            if ((data[1] & 0x80) != 0) {
                cbRx3PowerHighAlarm.BackColor = System.Drawing.Color.Red;
                cbRx3PowerHighAlarm.Checked = true;
                pass = false;
                result += "Rx3 power high alarm!!, ";
            }
            else {
                cbRx3PowerHighAlarm.BackColor = SystemColors.Control;
                cbRx3PowerHighAlarm.Checked = false;
            }

            if ((data[1] & 0x40) != 0) {
                cbRx3PowerLowAlarm.BackColor = System.Drawing.Color.Red;
                cbRx3PowerLowAlarm.Checked = true;
                pass = false;
                result += "Rx3 power low alarm!!, ";
            }
            else {
                cbRx3PowerLowAlarm.BackColor = SystemColors.Control;
                cbRx3PowerLowAlarm.Checked = false;
            }

            if ((data[1] & 0x20) != 0) {
                cbRx3PowerHighWarning.BackColor = System.Drawing.Color.Red;
                cbRx3PowerHighWarning.Checked = true;
                pass = false;
                result += "Rx3 power high warning!!, ";
            }
            else {
                cbRx3PowerHighWarning.BackColor = SystemColors.Control;
                cbRx3PowerHighWarning.Checked = false;
            }

            if ((data[1] & 0x10) != 0) {
                cbRx3PowerLowWarning.BackColor = System.Drawing.Color.Red;
                cbRx3PowerLowWarning.Checked = true;
                pass = false;
                result += "Rx3 power low warning!!, ";
            }
            else {
                cbRx3PowerLowWarning.BackColor = SystemColors.Control;
                cbRx3PowerLowWarning.Checked = false;
            }

            if ((data[1] & 0x08) != 0) {
                cbRx4PowerHighAlarm.BackColor = System.Drawing.Color.Red;
                cbRx4PowerHighAlarm.Checked = true;
                pass = false;
                result += "Rx4 power high alarm!!, ";
            }
            else {
                cbRx4PowerHighAlarm.BackColor = SystemColors.Control;
                cbRx4PowerHighAlarm.Checked = false;
            }

            if ((data[1] & 0x04) != 0) {
                cbRx4PowerLowAlarm.BackColor = System.Drawing.Color.Red;
                cbRx4PowerLowAlarm.Checked = true;
                pass = false;
                result += "Rx4 power low alarm!!, ";
            }
            else {
                cbRx4PowerLowAlarm.BackColor = SystemColors.Control;
                cbRx4PowerLowAlarm.Checked = false;
            }

            if ((data[1] & 0x02) != 0) {
                cbRx4PowerHighWarning.BackColor = System.Drawing.Color.Red;
                cbRx4PowerHighWarning.Checked = true;
                pass = false;
                result += "Rx4 power high warning!!, ";
            }
            else {
                cbRx4PowerHighWarning.BackColor = SystemColors.Control;
                cbRx4PowerHighWarning.Checked = false;
            }

            if ((data[1] & 0x01) != 0) {
                cbRx4PowerLowWarning.BackColor = System.Drawing.Color.Red;
                cbRx4PowerLowWarning.Checked = true;
                pass = false;
                result += "Rx4 power low warning!!, ";
            }
            else {
                cbRx4PowerLowWarning.BackColor = SystemColors.Control;
                cbRx4PowerLowWarning.Checked = false;
            }

            if (pass == true)
                return 0;
            else
                return -1;

        clearData:
            cbRx1PowerHighAlarm.BackColor = SystemColors.Control;
            cbRx1PowerHighAlarm.Checked = false;
            cbRx1PowerLowAlarm.BackColor = SystemColors.Control;
            cbRx1PowerLowAlarm.Checked = false;
            cbRx1PowerHighWarning.BackColor = SystemColors.Control;
            cbRx1PowerHighWarning.Checked = false;
            cbRx1PowerLowWarning.BackColor = SystemColors.Control;
            cbRx1PowerLowWarning.Checked = false;
            cbRx2PowerHighAlarm.BackColor = SystemColors.Control;
            cbRx2PowerHighAlarm.Checked = false;
            cbRx2PowerLowAlarm.BackColor = SystemColors.Control;
            cbRx2PowerLowAlarm.Checked = false;
            cbRx2PowerHighWarning.BackColor = SystemColors.Control;
            cbRx2PowerHighWarning.Checked = false;
            cbRx2PowerLowWarning.BackColor = SystemColors.Control;
            cbRx2PowerLowWarning.Checked = false;
            cbRx3PowerHighAlarm.BackColor = SystemColors.Control;
            cbRx3PowerHighAlarm.Checked = false;
            cbRx3PowerLowAlarm.BackColor = SystemColors.Control;
            cbRx3PowerLowAlarm.Checked = false;
            cbRx3PowerHighWarning.BackColor = SystemColors.Control;
            cbRx3PowerHighWarning.Checked = false;
            cbRx3PowerLowWarning.BackColor = SystemColors.Control;
            cbRx3PowerLowWarning.Checked = false;
            cbRx4PowerHighAlarm.BackColor = SystemColors.Control;
            cbRx4PowerHighAlarm.Checked = false;
            cbRx4PowerLowAlarm.BackColor = SystemColors.Control;
            cbRx4PowerLowAlarm.Checked = false;
            cbRx4PowerHighWarning.BackColor = SystemColors.Control;
            cbRx4PowerHighWarning.Checked = false;
            cbRx4PowerLowWarning.BackColor = SystemColors.Control;
            cbRx4PowerLowWarning.Checked = false;
            return -1;
        }

        private int _UpdataRegisterLowPageGui()
        {
            byte[] data = new byte[128];
            byte bTmp;
            int addr;
            bool pass = true;

            lStatus.Text = "記憶體檢查1/5 ...";
            lStatus.Update();

            if (qsfpI2cReadCB == null)
                return -1;

            if (qsfpI2cReadCB(80, 0, 128, data) != 128)
                goto clearData;

            foreach (DataGridViewRow row in dgvRegisterValueLowPage.Rows) {
                StringBuilder sbTmp = new StringBuilder();

                int.TryParse(row.Cells[0].Value.ToString(), out addr);

                if ((addr < 0) || (addr > 127))
                    continue;

                row.Cells[2].Value = "0x" + data[addr].ToString("X2");

                if (row.Cells[1].Value.ToString().Equals("NA"))
                    continue;

                bTmp = Convert.ToByte(row.Cells[1].Value.ToString().Substring(2, 2), 16);

                if (bTmp == data[addr])
                    continue;

                row.DefaultCellStyle.BackColor = Color.Red;
                pass = false;
                result += "Low page item:" + row.Index + " value different, ";
            }

            if (pass == true)
                return 0;
            else {
                lLowPage.BackColor = Color.Red;
                return -1;
            }

        clearData:
            return -1;
        }

        private int _UpdataRegisterUpPage0Gui()
        {
            byte[] data = new byte[128];
            byte bTmp;
            int addr;
            bool pass = true;

            lStatus.Text = "記憶體檢查2/5 ...";
            lStatus.Update();

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            data[0] = 0;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                goto clearData;

            Thread.Sleep(10);

            if (qsfpI2cReadCB(80, 128, 128, data) != 128)
                goto clearData;

            foreach (DataGridViewRow row in dgvRegisterValueUpPage0.Rows) {
                StringBuilder sbTmp = new StringBuilder();

                int.TryParse(row.Cells[0].Value.ToString(), out addr);

                if ((addr < 128) || (addr > 255))
                    continue;

                row.Cells[2].Value = "0x" + data[addr - 128].ToString("X2");

                if (row.Cells[1].Value.ToString().Equals("NA"))
                    continue;

                bTmp = Convert.ToByte(row.Cells[1].Value.ToString().Substring(2, 2), 16);

                if (bTmp == data[addr - 128])
                    continue;

                row.DefaultCellStyle.BackColor = Color.Red;
                pass = false;
                result += "Up page0 item:" + row.Index + " value different, ";
            }

            if (pass == true)
                return 0;
            else {
                lUpPage0.BackColor = Color.Red;
                return -1;
            }

        clearData:
            return -1;
        }

        private int _UpdataRegisterUpPage3Gui()
        {
            byte[] data = new byte[128];
            byte bTmp;
            int addr;
            bool pass = true;

            lStatus.Text = "記憶體檢查3/5 ...";
            lStatus.Update();

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            data[0] = 3;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                goto clearData;

            Thread.Sleep(10);

            if (qsfpI2cReadCB(80, 128, 128, data) != 128)
                goto clearData;

            foreach (DataGridViewRow row in dgvRegisterValueUpPage3.Rows) {
                StringBuilder sbTmp = new StringBuilder();

                int.TryParse(row.Cells[0].Value.ToString(), out addr);

                if ((addr < 128) || (addr > 255))
                    continue;

                row.Cells[2].Value = "0x" + data[addr - 128].ToString("X2");

                if (row.Cells[1].Value.ToString().Equals("NA"))
                    continue;

                bTmp = Convert.ToByte(row.Cells[1].Value.ToString().Substring(2, 2), 16);

                if (bTmp == data[addr - 128])
                    continue;

                row.DefaultCellStyle.BackColor = Color.Red;
                pass = false;
                result += "Up page3 item:" + row.Index + " value different, ";
            }

            if (pass == true)
                return 0;
            else {
                lUpPage3.BackColor = Color.Red;
                return -1;
            }

        clearData:
            return -1;
        }

        private int _UpdataRegisterUpPage4Gui()
        {
            byte[] data = new byte[128];
            byte bTmp;
            int addr;
            bool pass = true;

            lStatus.Text = "記憶體檢查4/5 ...";
            lStatus.Update();

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            data[0] = 4;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                goto clearData;

            Thread.Sleep(10);

            if (qsfpI2cReadCB(80, 128, 128, data) != 128)
                goto clearData;

            foreach (DataGridViewRow row in dgvRegisterValueUpPage4.Rows) {
                StringBuilder sbTmp = new StringBuilder();

                int.TryParse(row.Cells[0].Value.ToString(), out addr);

                if ((addr < 128) || (addr > 255))
                    continue;

                row.Cells[2].Value = "0x" + data[addr - 128].ToString("X2");

                if (row.Cells[1].Value.ToString().Equals("NA"))
                    continue;

                bTmp = Convert.ToByte(row.Cells[1].Value.ToString().Substring(2, 2), 16);

                if (bTmp == data[addr - 128])
                    continue;

                row.DefaultCellStyle.BackColor = Color.Red;
                pass = false;
                result += "Up page4 item:" + row.Index + " value different, ";
            }

            if (pass == true)
                return 0;
            else {
                lUpPage4.BackColor = Color.Red;
                return -1;
            }

        clearData:
            return -1;
        }

        private int _UpdataRegisterUpPage5Gui()
        {
            byte[] data = new byte[128];
            byte bTmp;
            int addr;
            bool pass = true;

            lStatus.Text = "記憶體檢查5/5 ...";
            lStatus.Update();

            if (qsfpI2cWriteCB == null)
                return -1;

            if (qsfpI2cReadCB == null)
                return -1;

            data[0] = 5;
            if (qsfpI2cWriteCB(80, 127, 1, data) < 0)
                goto clearData;

            Thread.Sleep(10);

            if (qsfpI2cReadCB(80, 128, 128, data) != 128)
                goto clearData;

            foreach (DataGridViewRow row in dgvRegisterValueUpPage5.Rows) {
                StringBuilder sbTmp = new StringBuilder();

                int.TryParse(row.Cells[0].Value.ToString(), out addr);

                if ((addr < 128) || (addr > 255))
                    continue;

                row.Cells[2].Value = "0x" + data[addr - 128].ToString("X2");

                if (row.Cells[1].Value.ToString().Equals("NA"))
                    continue;

                bTmp = Convert.ToByte(row.Cells[1].Value.ToString().Substring(2, 2), 16);

                if (bTmp == data[addr - 128])
                    continue;

                row.DefaultCellStyle.BackColor = Color.Red;
                pass = false;
                result += "Up page5 item:" + row.Index + " value different, ";
            }

            if (pass == true)
                return 0;
            else {
                lUpPage5.BackColor = Color.Red;
                return -1;
            }

        clearData:
            return -1;
        }

        private int _StoreLogFile()
        {
            StreamWriter swLog;
            String logFileName, logFileDirectory, logFilePath, sTmp;

            logFileName = tbXaSerialNumber.Text + ".log";
            logFileDirectory = fileDirectory + "\\" + tbXaSerialNumber.Text.Substring(0, 8);
            logFilePath = logFileDirectory + "\\" + logFileName;

            Directory.CreateDirectory(logFileDirectory);

            swLog = new StreamWriter(logFilePath);
            swLog.WriteLine("Operator:" + tbOperator.Text);
            swLog.WriteLine("YC SN:" + tbYcSerialNumber.Text);
            swLog.WriteLine("XA SN:" + tbXaSerialNumber.Text);
            swLog.WriteLine("Customer SN:" + tbCustomerSerialNumber.Text);
            swLog.WriteLine("Firwmare date code:" + tbDateCode.Text);
            swLog.WriteLine("Low page register:");
            sTmp = "";
            foreach (DataRow row in dtRegValueLowPage.Rows)
                sTmp += row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + ";";
            swLog.WriteLine(sTmp);
            
            swLog.WriteLine("Up page0 register:");
            sTmp = "";
            foreach (DataRow row in dtRegValueUpPage0.Rows)
                sTmp += row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + ";";
            swLog.WriteLine(sTmp);
            
            swLog.WriteLine("Up page3 register:");
            sTmp = "";
            foreach (DataRow row in dtRegValueUpPage3.Rows)
                sTmp += row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + ";";
            swLog.WriteLine(sTmp);
            
            swLog.WriteLine("Up page4 register:");
            sTmp = "";
            foreach (DataRow row in dtRegValueUpPage4.Rows)
                sTmp += row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + ";";
            swLog.WriteLine(sTmp);
            
            swLog.WriteLine("Up page5 register:");
            sTmp = "";
            foreach (DataRow row in dtRegValueUpPage5.Rows)
                sTmp += row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + ";";
            swLog.WriteLine(sTmp);
            sTmp = "";

            swLog.Close();

            return 0;
        }

        public void MonitorProgressChangedApi(object sender, ProgressChangedEventArgs e)
        {
            
            switch (e.ProgressPercentage)
            {
                case 1:
                case 7:
                    lStatus.Text = "取得模組數值 ...";
                    lStatus.Update();
                    return;

                case 2:
                    lStatus.Text = "電壓校正 ...";
                    lStatus.Update();
                    return;

                case 3:
                    lStatus.Text = "溫度校正 ...";
                    lStatus.Update();
                    return;

                case 4:
                    lStatus.Text = "接收功率校正 ...";
                    lStatus.Update();
                    return;

                case 5:
                    lStatus.Text = "寫入客戶序號 ...";
                    lStatus.Update();
                    return;

                case 6:
                    lStatus.Text = "儲存校正值 ...";
                    lStatus.Update();
                    return;

                case 99:
                    lStatus.Text = "門檻檢查 ...";
                    lStatus.Update();
                    break;

                default:
                    break;
            }

            if (result.Length != 0) {
                lStatus.Text = result;
                lStatus.Update();
                result = "";
            }

            _UpdateVoltageValueGui();
            _UpdateTemperatureValueGui();
            _UpdateTxBiasValueGui();
            _UpdateRxPowerValueGui();
            _UpdateRxRateValueGui();
            _UpdateSerialNumberGui();

            if ((corrector == true) && (e.ProgressPercentage == 99)) {
                _CheckFirmwareDataCode();
                _UpdateTemperatureAlarmGui();
                _UpdateVoltageAlarmGui();
                _UpdateTxBiasAlarmGui();
                _UpdateRxPowerAlarmGui();
                _UpdataRegisterLowPageGui();
                _UpdataRegisterUpPage0Gui();
                _UpdataRegisterUpPage3Gui();
                _UpdataRegisterUpPage4Gui();
                _UpdataRegisterUpPage5Gui();

                if (result.Length == 0) {
                    lClassification.ForeColor = System.Drawing.Color.White;
                    lClassification.BackColor = System.Drawing.Color.Green;
                    lClassification.Text = "A";
                }
                else {
                    lClassification.ForeColor = System.Drawing.Color.Red;
                    lClassification.BackColor = System.Drawing.Color.White;
                    lClassification.Text = "T";
                }

                lStatus.Text = "存檔 ...";
                lStatus.Update();
                _StoreLogFile();

                lStatus.Text = "校正完成.";
                lStatus.Update();
                corrector = false;
                bCorrector.Enabled = true;
            }

            result = "";
        }

        private void _bSaveFileClick(object sender, EventArgs e)
        {
            SaveFileDialog sfdSelectFile = new SaveFileDialog();
            string sTmp;

            sfdSelectFile.Filter = "xml files (*.xml)|*.xml";
            if (tbConfigFilePath.Text.Length != 0)
                sfdSelectFile.FileName = tbConfigFilePath.Text;

            if (sfdSelectFile.ShowDialog() != DialogResult.OK)
                return;

            using (XmlWriter xwConfig = XmlWriter.Create(sfdSelectFile.FileName))
            {
                xwConfig.WriteStartDocument();
                xwConfig.WriteStartElement("QsfpCorrectorConfig");
                {
                    xwConfig.WriteElementString("ModulePassword123", tbPassword123.Text);
                    xwConfig.WriteElementString("ModulePassword124", tbPassword124.Text);
                    xwConfig.WriteElementString("ModulePassword125", tbPassword125.Text);
                    xwConfig.WriteElementString("ModulePassword126", tbPassword126.Text);
                    xwConfig.WriteElementString("FirmwareDataCode", tbFirmwareDateCode.Text);

                    xwConfig.WriteStartElement("RssiRateConfig");
                    {
                        xwConfig.WriteElementString("RssiRegisterPage", tbRssiRegisterPage.Text);
                        xwConfig.WriteElementString("RssiRegisterAddr", tbRssiRegisterAddr.Text);
                        xwConfig.WriteElementString("RssiRateNumerator", tbRssiRateNumerator.Text);
                        xwConfig.WriteElementString("RssiRateDenominator", tbRssiRateDenominator.Text);
                        xwConfig.WriteElementString("RssiRateMin", tbRxRateMin.Text);
                        xwConfig.WriteElementString("RssiRateMax", tbRxRateMax.Text);
                    }
                    xwConfig.WriteEndElement(); //RssiRateConfig

                    xwConfig.WriteStartElement("ThresholdConfig");
                    {
                        xwConfig.WriteElementString("VoltageMin", tbThresholdVoltageMin.Text);
                        xwConfig.WriteElementString("VoltageMax", tbThresholdVoltageMax.Text);
                        xwConfig.WriteElementString("TemperatureMin", tbThresholdTemperatureMin.Text);
                        xwConfig.WriteElementString("TemperatureMax", tbThresholdTemperatureMax.Text);
                        xwConfig.WriteElementString("Bias1Min", tbThresholdBias1Min.Text);
                        xwConfig.WriteElementString("Bias1Max", tbThresholdBias1Max.Text);
                        xwConfig.WriteElementString("Bias2Min", tbThresholdBias1Min.Text);
                        xwConfig.WriteElementString("Bias2Max", tbThresholdBias1Max.Text);
                        xwConfig.WriteElementString("Bias3Min", tbThresholdBias1Min.Text);
                        xwConfig.WriteElementString("Bias3Max", tbThresholdBias1Max.Text);
                        xwConfig.WriteElementString("Bias4Min", tbThresholdBias1Min.Text);
                        xwConfig.WriteElementString("Bias4Max", tbThresholdBias1Max.Text);
                        xwConfig.WriteElementString("Rx1Min", tbThresholdRx1Min.Text);
                        xwConfig.WriteElementString("Rx1Max", tbThresholdRx1Max.Text);
                        xwConfig.WriteElementString("Rx2Min", tbThresholdRx2Min.Text);
                        xwConfig.WriteElementString("Rx2Max", tbThresholdRx2Max.Text);
                        xwConfig.WriteElementString("Rx3Min", tbThresholdRx3Min.Text);
                        xwConfig.WriteElementString("Rx3Max", tbThresholdRx3Max.Text);
                        xwConfig.WriteElementString("Rx4Min", tbThresholdRx4Min.Text);
                        xwConfig.WriteElementString("Rx4Max", tbThresholdRx4Max.Text);
                    }
                    xwConfig.WriteEndElement(); //ThresholdConfig

                    xwConfig.WriteStartElement("InputConfig");
                    {
                        xwConfig.WriteElementString("InputVoltage", tbInputVoltage.Text);
                        xwConfig.WriteElementString("InputPower1", tbInputPower1.Text);
                        xwConfig.WriteElementString("InputPower2", tbInputPower2.Text);
                        xwConfig.WriteElementString("InputPower3", tbInputPower3.Text);
                        xwConfig.WriteElementString("InputPower4", tbInputPower4.Text);
                    }
                    xwConfig.WriteEndElement(); //InputConfig

                    xwConfig.WriteStartElement("DefaultOffsetConfig");
                    {
                        xwConfig.WriteElementString("TemperatureOffset", tbDefaultTemperatureOffset.Text);
                    }
                    xwConfig.WriteEndElement(); //DefaultOffsetConfig

                    xwConfig.WriteStartElement("RegisterValueConfig");
                    {
                        sTmp = "";
                        foreach (DataRow row in dtRegValueLowPage.Rows) { 
                            sTmp += (row[0].ToString() + "," + row[1].ToString() + "\n");
                        }
                        xwConfig.WriteElementString("LowPage", sTmp);

                        sTmp = "";
                        foreach (DataRow row in dtRegValueUpPage0.Rows)
                        {
                            sTmp += (row[0].ToString() + "," + row[1].ToString() + "\n");
                        }
                        xwConfig.WriteElementString("UpPage0", sTmp);

                        sTmp = "";
                        foreach (DataRow row in dtRegValueUpPage3.Rows)
                        {
                            sTmp += (row[0].ToString() + "," + row[1].ToString() + "\n");
                        }
                        xwConfig.WriteElementString("UpPage3", sTmp);

                        sTmp = "";
                        foreach (DataRow row in dtRegValueUpPage4.Rows)
                        {
                            sTmp += (row[0].ToString() + "," + row[1].ToString() + "\n");
                        }
                        xwConfig.WriteElementString("UpPage4", sTmp);

                        sTmp = "";
                        foreach (DataRow row in dtRegValueUpPage5.Rows)
                        {
                            sTmp += (row[0].ToString() + "," + row[1].ToString() + "\n");
                        }
                        xwConfig.WriteElementString("UpPage5", sTmp);
                    }
                    xwConfig.WriteEndElement(); //RegisterValueConfig
                }
                xwConfig.WriteEndElement(); //DcTestConfig
                xwConfig.WriteEndDocument();
            }
        }

        private void _PaserRssiRateConfigXml(XmlReader reader)
        {
            reader.Read();
            while (true) {
                if (reader.IsStartElement()) {
                    switch (reader.Name) {
                        case "RssiRegisterPage":
                            tbRssiRegisterPage.Text = reader.ReadElementContentAsString();
                            break;

                        case "RssiRegisterAddr":
                            tbRssiRegisterAddr.Text = reader.ReadElementContentAsString();
                            break;

                        case "RssiRateNumerator":
                            tbRssiRateNumerator.Text = reader.ReadElementContentAsString();
                            break;

                        case "RssiRateDenominator":
                            tbRssiRateDenominator.Text = reader.ReadElementContentAsString();
                            break;

                        case "RssiRateMin":
                            tbRxRateMin.Text = reader.ReadElementContentAsString();
                            break;

                        case "RssiRateMax":
                            tbRxRateMax.Text = reader.ReadElementContentAsString();
                            return;

                        default:
                            reader.ReadElementContentAsString();
                            break;
                    }
                }
                else {
                    reader.MoveToContent();
                    reader.ReadEndElement();
                    break;
                }
            }
        }

        private void _PaserThresholdConfigXml(XmlReader reader)
        {
            reader.Read();
            while (true) {
                if (reader.IsStartElement()) {
                    switch (reader.Name) {

                        case "VoltageMin":
                            tbThresholdVoltageMin.Text = reader.ReadElementContentAsString();
                            break;

                        case "VoltageMax":
                            tbThresholdVoltageMax.Text = reader.ReadElementContentAsString();
                            break;

                        case "TemperatureMin":
                            tbThresholdTemperatureMin.Text = reader.ReadElementContentAsString();
                            break;

                        case "TemperatureMax":
                            tbThresholdTemperatureMax.Text = reader.ReadElementContentAsString();
                            break;

                        case "Bias1Min":
                            tbThresholdBias1Min.Text = reader.ReadElementContentAsString();
                            break;

                        case "Bias1Max":
                            tbThresholdBias1Max.Text = reader.ReadElementContentAsString();
                            break;

                        case "Bias2Min":
                            tbThresholdBias2Min.Text = reader.ReadElementContentAsString();
                            break;

                        case "Bias2Max":
                            tbThresholdBias2Max.Text = reader.ReadElementContentAsString();
                            break;

                        case "Bias3Min":
                            tbThresholdBias3Min.Text = reader.ReadElementContentAsString();
                            break;

                        case "Bias3Max":
                            tbThresholdBias3Max.Text = reader.ReadElementContentAsString();
                            break;

                        case "Bias4Min":
                            tbThresholdBias4Min.Text = reader.ReadElementContentAsString();
                            break;

                        case "Bias4Max":
                            tbThresholdBias4Max.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx1Min":
                            tbThresholdRx1Min.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx1Max":
                            tbThresholdRx1Max.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx2Min":
                            tbThresholdRx2Min.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx2Max":
                            tbThresholdRx2Max.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx3Min":
                            tbThresholdRx3Min.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx3Max":
                            tbThresholdRx3Max.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx4Min":
                            tbThresholdRx4Min.Text = reader.ReadElementContentAsString();
                            break;

                        case "Rx4Max":
                            tbThresholdRx4Max.Text = reader.ReadElementContentAsString();
                            return;

                        default:
                            reader.ReadElementContentAsString();
                            break;
                    }
                }
                else {
                    reader.MoveToContent();
                    reader.ReadEndElement();
                    break;
                }
            }
        }

        private void _PaserInputConfigXml(XmlReader reader)
        {
            reader.Read();
            while (true) {
                if (reader.IsStartElement()) {
                    switch (reader.Name) {
                        case "InputVoltage":
                            tbInputVoltage.Text = reader.ReadElementContentAsString();
                            break;

                        case "InputPower1":
                            tbInputPower1.Text = reader.ReadElementContentAsString();
                            break;

                        case "InputPower2":
                            tbInputPower2.Text = reader.ReadElementContentAsString();
                            break;

                        case "InputPower3":
                            tbInputPower3.Text = reader.ReadElementContentAsString();
                            break;

                        case "InputPower4":
                            tbInputPower4.Text = reader.ReadElementContentAsString();
                            return;

                        default:
                            reader.ReadElementContentAsString();
                            break;
                    }
                }
                else {
                    reader.MoveToContent();
                    reader.ReadEndElement();
                    break;
                }
            }
        }

        private void _PaserDefaultOffsetConfigXml(XmlReader reader)
        {
            reader.Read();
            while (true) {
                if (reader.IsStartElement()) {
                    switch (reader.Name) {
                        case "TemperatureOffset":
                            tbDefaultTemperatureOffset.Text = reader.ReadElementContentAsString();
                            return;

                        default:
                            reader.ReadElementContentAsString();
                            break;
                    }
                }
                else {
                    reader.MoveToContent();
                    reader.ReadEndElement();
                    break;
                }
            }
        }

        private void _PaserLowPageConfig(string cfg)
        {
            StringReader srReader;
            String[] saItems;
            string line;

            srReader = new StringReader(cfg);
            while ((line = srReader.ReadLine()) != null) {
                saItems = line.Split(',');
                dtRegValueLowPage.Rows.Add(saItems[0], saItems[1]);
            }
        }

        private void _PaserUpPage0Config(string cfg)
        {
            StringReader srReader;
            String[] saItems;
            string line;

            srReader = new StringReader(cfg);
            while ((line = srReader.ReadLine()) != null)
            {
                saItems = line.Split(',');
                dtRegValueUpPage0.Rows.Add(saItems[0], saItems[1]);
            }
        }

        private void _PaserUpPage3Config(string cfg)
        {
            StringReader srReader;
            String[] saItems;
            string line;

            srReader = new StringReader(cfg);
            while ((line = srReader.ReadLine()) != null)
            {
                saItems = line.Split(',');
                dtRegValueUpPage3.Rows.Add(saItems[0], saItems[1]);
            }
        }

        private void _PaserUpPage4Config(string cfg)
        {
            StringReader srReader;
            String[] saItems;
            string line;

            srReader = new StringReader(cfg);
            while ((line = srReader.ReadLine()) != null)
            {
                saItems = line.Split(',');
                dtRegValueUpPage4.Rows.Add(saItems[0], saItems[1]);
            }
        }

        private void _PaserUpPage5Config(string cfg)
        {
            StringReader srReader;
            String[] saItems;
            string line;

            srReader = new StringReader(cfg);
            while ((line = srReader.ReadLine()) != null)
            {
                saItems = line.Split(',');
                dtRegValueUpPage5.Rows.Add(saItems[0], saItems[1]);
            }
        }

        private void _PaserRegisterValueConfigXml(XmlReader reader)
        {
            reader.Read();
            while (true)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {

                        case "LowPage":
                            _PaserLowPageConfig(reader.ReadElementContentAsString());
                            break;

                        case "UpPage0":
                            _PaserUpPage0Config(reader.ReadElementContentAsString());
                            break;

                        case "UpPage3":
                            _PaserUpPage3Config(reader.ReadElementContentAsString());
                            break;

                        case "UpPage4":
                            _PaserUpPage4Config(reader.ReadElementContentAsString());
                            break;

                        case "UpPage5":
                            _PaserUpPage5Config(reader.ReadElementContentAsString());
                            break;

                        default:
                            return;
                    }
                }
                else
                {
                    reader.MoveToContent();
                    reader.ReadEndElement();
                    break;
                }
            }
        }

        private int _PaserQsfpCorrectorConfigXml(XmlReader reader)
        {
            while (true)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "ModulePassword123":
                            tbPassword123.Text = reader.ReadElementContentAsString();
                            break;

                        case "ModulePassword124":
                            tbPassword124.Text = reader.ReadElementContentAsString();
                            break;

                        case "ModulePassword125":
                            tbPassword125.Text = reader.ReadElementContentAsString();
                            break;

                        case "ModulePassword126":
                            tbPassword126.Text = reader.ReadElementContentAsString();
                            break;

                        case "FirmwareDataCode":
                            tbFirmwareDateCode.Text = reader.ReadElementContentAsString();
                            break;

                        case "RssiRateConfig":
                            _PaserRssiRateConfigXml(reader);
                            reader.Read();
                            break;

                        case "ThresholdConfig":
                            _PaserThresholdConfigXml(reader);
                            reader.Read();
                            break;

                        case "InputConfig":
                            _PaserInputConfigXml(reader);
                            reader.Read();
                            break;

                        case "DefaultOffsetConfig":
                            _PaserDefaultOffsetConfigXml(reader);
                            reader.Read();
                            break;

                        case "RegisterValueConfig":
                            _PaserRegisterValueConfigXml(reader);
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    reader.MoveToContent();
                    reader.ReadEndElement();
                    break;
                }
            }

            return 0;
        }

        private void bCorrector_Click(object sender, EventArgs e)
        {
            if (tbOperator.Text.Length == 0) {
                MessageBox.Show("請輸入工號!!\n");
                return;
            }

            if (tbXaSerialNumber.Text.Length != 15) {
                MessageBox.Show("模組XA序號異常!!\n");
                return;
            }

            if (tbCustomerSerialNumber.Text.Length > 16) {
                MessageBox.Show("客戶序號過長!!\n");
                return;
            }

            foreach (DataGridViewRow row in dgvRegisterValueLowPage.Rows)
                row.DefaultCellStyle.BackColor = SystemColors.Window;
            foreach (DataGridViewRow row in dgvRegisterValueUpPage0.Rows)
                row.DefaultCellStyle.BackColor = SystemColors.Window;
            foreach (DataGridViewRow row in dgvRegisterValueUpPage3.Rows)
                row.DefaultCellStyle.BackColor = SystemColors.Window;
            foreach (DataGridViewRow row in dgvRegisterValueUpPage4.Rows)
                row.DefaultCellStyle.BackColor = SystemColors.Window;
            foreach (DataGridViewRow row in dgvRegisterValueUpPage5.Rows)
                row.DefaultCellStyle.BackColor = SystemColors.Window;

            lLowPage.BackColor = SystemColors.Control;
            lUpPage0.BackColor = SystemColors.Control;
            lUpPage3.BackColor = SystemColors.Control;
            lUpPage4.BackColor = SystemColors.Control;
            lUpPage5.BackColor = SystemColors.Control;

            bCorrector.Enabled = false;
            lStatus.Text = "開始校正 ...";
            lClassification.Text = "";
            result = "";

            corrector = true;
        }

        private void _bLoadFileClick(object sender, EventArgs e)
        {
            OpenFileDialog ofdSelectFile = new OpenFileDialog();

            ofdSelectFile.Title = "選擇設定檔";
            ofdSelectFile.Filter = "xml files (*.xml)|*.xml";
            if (ofdSelectFile.ShowDialog() != DialogResult.OK)
                return;

            tbConfigFilePath.Text = ofdSelectFile.FileName;
            tbConfigFilePath.SelectionStart = tbConfigFilePath.Text.Length;
            tbConfigFilePath.ScrollToCaret();
            dtRegValueLowPage.Clear();
            dtRegValueUpPage0.Clear();
            dtRegValueUpPage3.Clear();
            dtRegValueUpPage4.Clear();
            dtRegValueUpPage5.Clear();

            using (XmlReader xrConfig = XmlReader.Create(ofdSelectFile.FileName))
            {
                while (xrConfig.Read())
                {
                    if (xrConfig.IsStartElement())
                    {
                        switch (xrConfig.Name)
                        {
                            case "QsfpCorrectorConfig":
                                xrConfig.Read();
                                _PaserQsfpCorrectorConfigXml(xrConfig);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            if (autoReadStart == true)
                bCorrector.Enabled = true;
        }

        private void cbModifyRegisterConfirmValue_CheckedChanged(object sender, EventArgs e)
        {
            if (cbModifyRegisterConfirmValue.Checked == true) {
                dgvRegisterValueLowPage.ReadOnly = false;
                dgvRegisterValueUpPage0.ReadOnly = false;
                dgvRegisterValueUpPage3.ReadOnly = false;
                dgvRegisterValueUpPage4.ReadOnly = false;
                dgvRegisterValueUpPage5.ReadOnly = false;
            }
            else {
                dgvRegisterValueLowPage.ReadOnly = true;
                dgvRegisterValueUpPage0.ReadOnly = true;
                dgvRegisterValueUpPage3.ReadOnly = true;
                dgvRegisterValueUpPage4.ReadOnly = true;
                dgvRegisterValueUpPage5.ReadOnly = true;
            }
        }
    }
}
