using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections.ObjectModel;
using LiveCharts;
using LiveCharts.Wpf;

namespace LiDARFunctionTest
{
    public partial class UcLiDARFunctionTest : UserControl
    {
        public delegate int GetPasswordCB(int length, byte[] data);
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate bool I2cConnectStatusCB();

        private GetPasswordCB getPasswordCB = null;
        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private I2cConnectStatusCB i2cConnectStatusCB = null;
        private SeriesCollection scTec1PwmDuty { get; set; }
        private SeriesCollection scTec2PwmDuty { get; set; }
        private SeriesCollection scNtc1Temperature { get; set; }
        private SeriesCollection scNtc2Temperature { get; set; }
        private ChartValues<int> cvTec1PwmDuty, cvTec1PwmDutyMax, cvTec1PwmDutyMin;
        private ChartValues<int> cvTec2PwmDuty, cvTec2PwmDutyMax, cvTec2PwmDutyMin;
        private ChartValues<float> cvNtc1Temperature, cvNtc1TemperatureMax, cvNtc1TemperatureMin;
        private ChartValues<float> cvNtc2Temperature, cvNtc2TemperatureMax, cvNtc2TemperatureMin;
        private BackgroundWorker bwMonitor;
        private SignalGeneratorConfig[] singleGeneratorConfig = new SignalGeneratorConfig[8];
        private volatile UInt16[] ntcResistance = new UInt16[2];
        private volatile UInt16[] mpdRssi = new UInt16[2];
        private volatile float[] ntcTemperature = new float[2];
        private volatile sbyte[] tecPwmDuty = new sbyte[2];
        private volatile UInt32 frequenceCounter;
        private volatile string errorLog;
        private volatile bool monitorStart = false;

        private int _GetMonitorValue()
        {
            byte[] data = new byte[22];
            byte[] bATmp;

            if (i2cReadCB == null)
                goto exit;

            if (i2cReadCB(0x51, 0, 22, data) != 22)
                goto exit;

            bATmp = new byte[2];
            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                errorLog += eBC.ToString() + "\n";
                goto exit;
            }

            ntcResistance[0] = BitConverter.ToUInt16(bATmp, 0);

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                errorLog += eBC.ToString() + "\n";
                goto exit;
            }

            ntcResistance[1] = BitConverter.ToUInt16(bATmp, 0);

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                errorLog += eBC.ToString() + "\n";
                goto exit;
            }

            mpdRssi[0] = BitConverter.ToUInt16(bATmp, 0);

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                errorLog += eBC.ToString() + "\n";
                goto exit;
            }

            mpdRssi[1] = BitConverter.ToUInt16(bATmp, 0);

            bATmp = new byte[4];
            try {
                Buffer.BlockCopy(data, 8, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                errorLog += eBC.ToString() + "\n";
                goto exit;
            }

            frequenceCounter = BitConverter.ToUInt32(bATmp, 0);

            try {
                Buffer.BlockCopy(data, 12, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                errorLog += eBC.ToString() + "\n";
                goto exit;
            }

            ntcTemperature[0] = BitConverter.ToSingle(bATmp, 0);

            try {
                Buffer.BlockCopy(data, 16, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                errorLog += eBC.ToString() + "\n";
                goto exit;
            }

            ntcTemperature[1] = BitConverter.ToSingle(bATmp, 0);

            tecPwmDuty[0] = (sbyte)data[20];
            tecPwmDuty[1] = (sbyte)data[21];

            return 0;

        exit:
            return -1;
        }

        public void MonitorValueUpdateApi(object sender, DoWorkEventArgs e)
        {
            while (monitorStart) {
                bwMonitor.ReportProgress(0, null);
                Thread.Sleep(400);
                if (_GetMonitorValue() < 0) {
                    bwMonitor.ReportProgress(50, null);
                    goto exit;
                }
                bwMonitor.ReportProgress(1, null);
                Thread.Sleep(200);
                bwMonitor.ReportProgress(2, null);
                Thread.Sleep(400);
            }
        exit:
            bwMonitor.ReportProgress(100, null);
        }

        private int _UpdateMonitorValue()
        {
            tbNtc1Resistance.Text = ntcResistance[0].ToString();
            tbNtc1Resistance.Update();
            tbNtc2Resistance.Text = ntcResistance[1].ToString();
            tbNtc2Resistance.Update();

            tbMpd1Rssi.Text = mpdRssi[0].ToString();
            tbMpd1Rssi.Update();
            tbMpd2Rssi.Text = mpdRssi[1].ToString();
            tbMpd2Rssi.Update();

            tbFrequenceCounter.Text = frequenceCounter.ToString();
            tbFrequenceCounter.Update();

            tbNtc1Temperature.Text = ntcTemperature[0].ToString();
            tbNtc1Temperature.Update();

            tbNtc2Temperature.Text = ntcTemperature[1].ToString();
            tbNtc2Temperature.Update();

            cvTec1PwmDuty.Add(tecPwmDuty[0]);
            if (cvTec1PwmDuty.Count > 30)
                cvTec1PwmDuty.RemoveAt(0);
            if (cvTec1PwmDutyMax.Count == 0)
                cvTec1PwmDutyMax.Add(tecPwmDuty[0]);
            else {
                if (cvTec1PwmDutyMax.Max() < tecPwmDuty[0]) {
                    cvTec1PwmDutyMax.RemoveAt(0);
                    cvTec1PwmDutyMax.Add(tecPwmDuty[0]);
                }
            }
            if (cvTec1PwmDutyMin.Count == 0)
                cvTec1PwmDutyMin.Add(tecPwmDuty[0]);
            else {
                if (cvTec1PwmDutyMin.Min() > tecPwmDuty[0]) {
                    cvTec1PwmDutyMin.RemoveAt(0);
                    cvTec1PwmDutyMin.Add(tecPwmDuty[0]);
                }
            }

            cvNtc1Temperature.Add(ntcTemperature[0]);
            if (cvNtc1Temperature.Count > 30)
                cvNtc1Temperature.RemoveAt(0);
            if (cvNtc1TemperatureMax.Count == 0)
                cvNtc1TemperatureMax.Add(ntcTemperature[0]);
            else {
                if (cvNtc1TemperatureMax.Max() < ntcTemperature[0]) {
                    cvNtc1TemperatureMax.RemoveAt(0);
                    cvNtc1TemperatureMax.Add(ntcTemperature[0]);
                }
            }
            if (cvNtc1TemperatureMin.Count == 0)
                cvNtc1TemperatureMin.Add(ntcTemperature[0]);
            else {
                if (cvNtc1TemperatureMin.Min() > ntcTemperature[0]) {
                    cvNtc1TemperatureMin.RemoveAt(0);
                    cvNtc1TemperatureMin.Add(ntcTemperature[0]);
                }
            }

            cvTec2PwmDuty.Add(tecPwmDuty[1]);
            if (cvTec2PwmDuty.Count > 30)
                cvTec2PwmDuty.RemoveAt(0);
            if (cvTec2PwmDutyMax.Count == 0)
                cvTec2PwmDutyMax.Add(tecPwmDuty[1]);
            else {
                if (cvTec2PwmDutyMax.Max() < tecPwmDuty[1]) {
                    cvTec2PwmDutyMax.RemoveAt(0);
                    cvTec2PwmDutyMax.Add(tecPwmDuty[1]);
                }
            }
            if (cvTec2PwmDutyMin.Count == 0)
                cvTec2PwmDutyMin.Add(tecPwmDuty[1]);
            else {
                if (cvTec2PwmDutyMin.Min() > tecPwmDuty[1]) {
                    cvTec2PwmDutyMin.RemoveAt(0);
                    cvTec2PwmDutyMin.Add(tecPwmDuty[1]);
                }
            }

            cvNtc2Temperature.Add(ntcTemperature[1]);
            if (cvNtc2Temperature.Count > 30)
                cvNtc2Temperature.RemoveAt(0);
            if (cvNtc2TemperatureMax.Count == 0)
                cvNtc2TemperatureMax.Add(ntcTemperature[1]);
            else {
                if (cvNtc2TemperatureMax.Max() < ntcTemperature[1]) {
                    cvNtc2TemperatureMax.RemoveAt(0);
                    cvNtc2TemperatureMax.Add(ntcTemperature[1]);
                }
            }
            if (cvNtc2TemperatureMin.Count == 0)
                cvNtc2TemperatureMin.Add(ntcTemperature[1]);
            else {
                if (cvNtc2TemperatureMin.Min() > ntcTemperature[1]) {
                    cvNtc2TemperatureMin.RemoveAt(0);
                    cvNtc2TemperatureMin.Add(ntcTemperature[1]);
                }
            }

            return 0;
        }

        public void MonitorProgressChangedApi(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage) {
                case 0:
                    lAction.Text = "Update .";
                    lAction.Update();
                    break;

                case 1:
                    lAction.Text = "Update ..";
                    lAction.Update();
                    break;

                case 2:
                    _UpdateMonitorValue();
                    lAction.Text = "Update ...";
                    lAction.Update();
                    break;

                case 100:
                    lAction.Text = "Stop.";
                    lAction.Update();
                    if (cbMoniter.Checked == true)
                        cbMoniter.Checked = false;
                    if (i2cConnectStatusCB()) {
                        bRead.Enabled = true;
                        bWrite.Enabled = true;
                        bSave.Enabled = true;
                        bSignalGeneratorSet.Enabled = true;
                    }
                    else
                        cbMoniter.Enabled = false;
                    break;

                default:
                    break;
            }
        }

        public UcLiDARFunctionTest()
        {
            ComboboxItem item;
            float fTmp;
            int i;

            InitializeComponent();

            cvTec1PwmDuty = new ChartValues<int> { };
            cvTec1PwmDutyMax = new ChartValues<int> { };
            cvTec1PwmDutyMin = new ChartValues<int> { };
            scTec1PwmDuty = new SeriesCollection {
                new LineSeries
                {
                    Name = "Tec1",
                    Values = cvTec1PwmDuty,
                    Fill = null
                },
                new LineSeries
                {
                    Name = "Max",
                    Values = cvTec1PwmDutyMax,
                    Fill = null
                },
                new LineSeries
                {
                    Name = "Min",
                    Values = cvTec1PwmDutyMin,
                    Fill = null
                },
            };
            ccTec1PwmDuty.Series = scTec1PwmDuty;

            cvNtc1Temperature = new ChartValues<float> { };
            cvNtc1TemperatureMax = new ChartValues<float> { };
            cvNtc1TemperatureMin = new ChartValues<float> { };
            scNtc1Temperature = new SeriesCollection {
                new LineSeries
                {
                    Name = "NTC1",
                    Values = cvNtc1Temperature,
                    Fill = null
                },
                new LineSeries
                {
                    Name = "Max",
                    Values = cvNtc1TemperatureMax,
                    Fill = null
                },
                new LineSeries
                {
                    Name = "Min",
                    Values = cvNtc1TemperatureMin,
                    Fill = null
                },
            };
            ccNtc1Temperature.Series = scNtc1Temperature;

            cvTec2PwmDuty = new ChartValues<int> { };
            cvTec2PwmDutyMax = new ChartValues<int> { };
            cvTec2PwmDutyMin = new ChartValues<int> { };
            scTec2PwmDuty = new SeriesCollection {
                new LineSeries
                {
                    Name = "Tec1",
                    Values = cvTec2PwmDuty,
                    Fill = null
                },
                new LineSeries
                {
                    Name = "Max",
                    Values = cvTec2PwmDutyMax,
                    Fill = null
                },
                new LineSeries
                {
                    Name = "Min",
                    Values = cvTec2PwmDutyMin,
                    Fill = null
                },
            };
            ccTec2PwmDuty.Series = scTec2PwmDuty;

            cvNtc2Temperature = new ChartValues<float> { };
            cvNtc2TemperatureMax = new ChartValues<float> { };
            cvNtc2TemperatureMin = new ChartValues<float> { };
            scNtc2Temperature = new SeriesCollection {
                new LineSeries
                {
                    Name = "NTC2",
                    Values = cvNtc2Temperature,
                    Fill = null
                },
                new LineSeries
                {
                    Name = "Max",
                    Values = cvNtc2TemperatureMax,
                    Fill = null
                },
                new LineSeries
                {
                    Name = "Min",
                    Values = cvNtc2TemperatureMin,
                    Fill = null
                },
            };
            ccNtc2Temperature.Series = scNtc2Temperature;

            item = new ComboboxItem();
            item.Text = "0: Sinusoidal";
            item.Value = 0;
            cbSignalGeneratorMode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1: Triangular";
            item.Value = 1;
            cbSignalGeneratorMode.Items.Add(item);
            cbSignalGeneratorMode.SelectedItem = item;
            //item = new ComboboxItem();
            //item.Text = "2: Square";
            //item.Value = 2;
            //cbSignalGeneratorMode.Items.Add(item);

            fTmp = 360;
            fTmp /= 4096;
            for (i = 0; i < 4096; i++) {
                item = new ComboboxItem();
                item.Text = i + ": " + (fTmp * i).ToString("F2");
                item.Value = i;
                cbSingleGeneratorPhaseShift.Items.Add(item);
                if (i == 0)
                    cbSingleGeneratorPhaseShift.SelectedItem = item;
            }
            
            bwMonitor = new BackgroundWorker();
            bwMonitor.WorkerReportsProgress = true;
            bwMonitor.WorkerSupportsCancellation = false;
            bwMonitor.DoWork += new DoWorkEventHandler(MonitorValueUpdateApi);
            bwMonitor.ProgressChanged += new ProgressChangedEventHandler(MonitorProgressChangedApi);
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

        public int SetI2cConnectStatusCBApi(I2cConnectStatusCB cb)
        {
            if (cb == null)
                return -1;

            i2cConnectStatusCB = new I2cConnectStatusCB(cb);

            return 0;
        }

        public int SetGetPasswordCBApi(GetPasswordCB cb)
        {
            if (cb == null)
                return -1;

            getPasswordCB = new GetPasswordCB(cb);

            return 0;
        }

        public int I2cConnectedNotifyApi()
        {
            cbMoniter.Enabled = true;
            bRead.Enabled = true;
            bWrite.Enabled = true;
            bSave.Enabled = true;
            bSignalGeneratorSet.Enabled = true;

            return 0;
        }

        private void bSignalGeneratorSet_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[72];
            byte[] bATmp;
            sbyte sbTmp;
            int i, id;
            UInt16 ui16Tmp;
            float fTmp;

            if (i2cConnectStatusCB == null) {
                MessageBox.Show("i2cConnectStatusCB == null");
                return;
            }
            if (i2cConnectStatusCB() == false) {
                MessageBox.Show("Need connect I2C first!!");
                return;
            }

            bSignalGeneratorSet.Enabled = false;

            if (getPasswordCB == null)
                goto exit;

            bATmp = new byte[4];
            getPasswordCB(bATmp.Length, bATmp);

            if (i2cWriteCB == null)
                goto exit;

            if (i2cWriteCB(0x51, 123, 4, bATmp) < 0)
                goto exit;

            data[0] = 0;
            if (i2cWriteCB(0x51, 127, 1, data) < 0)
                goto exit;

            id = cbSignalGeneratorId.SelectedIndex;
            data[0] = Convert.ToByte(cbSignalGeneratorMode.SelectedIndex);
            if (i2cWriteCB(0x51, Convert.ToByte(160 + id), 1, data) < 0)
                goto exit;

            UInt16.TryParse(tbSignalGeneratorFrequency.Text, out ui16Tmp);
            bATmp = BitConverter.GetBytes(ui16Tmp);
            if (i2cWriteCB(0x51, Convert.ToByte(168 + (id * 4)), 2, bATmp) < 0)
                goto exit;

            ui16Tmp = Convert.ToUInt16(cbSingleGeneratorPhaseShift.SelectedIndex);
            bATmp = BitConverter.GetBytes(ui16Tmp);
            if (i2cWriteCB(0x51, Convert.ToByte(170 + (id * 4)), 2, bATmp) < 0)
                goto exit;

        exit:
            bSignalGeneratorSet.Enabled = true;
        }

        private void cbSignalGeneratorId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx;

            idx = cbSignalGeneratorId.SelectedIndex;

            if (idx < singleGeneratorConfig.Count()) {
                foreach (ComboboxItem item in cbSignalGeneratorMode.Items) {
                    if (item.Value == singleGeneratorConfig[idx].Mode)
                        cbSignalGeneratorMode.SelectedItem = item;
                }
                tbSignalGeneratorFrequency.Text = singleGeneratorConfig[idx].Frequence.ToString();
                foreach (ComboboxItem item in cbSingleGeneratorPhaseShift.Items) {
                    if (item.Value == singleGeneratorConfig[idx].PhaseShift) {
                        cbSingleGeneratorPhaseShift.SelectedItem = item;
                    }
                }
            }
        }

        public int I2cDisconnectedNotifyApi()
        {
            if (cbMoniter.Checked == true)
                cbMoniter.Checked = false;
            else {
                cbMoniter.Enabled = false;
                bRead.Enabled = false;
                bWrite.Enabled = false;
                bSave.Enabled = false;
                bSignalGeneratorSet.Enabled = false;
            }

            return 0;
        }


        private int _LowPageRead()
        {
            byte[] data = new byte[64];
            byte[] bATmp;
            int  iTmp;
            uint uiTmp;
            float fTmp;

            if (i2cReadCB == null)
                goto exit;

            if (i2cReadCB(0x51, 0, 64, data) != 64)
                goto exit;

            bATmp = new byte[2];
            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            iTmp = BitConverter.ToUInt16(bATmp, 0);
            tbNtc1Resistance.Text = iTmp.ToString();

            try {
                Buffer.BlockCopy(data, 2, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            iTmp = BitConverter.ToUInt16(bATmp, 0);
            tbNtc2Resistance.Text = iTmp.ToString();

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            iTmp = BitConverter.ToUInt16(bATmp, 0);
            tbMpd1Rssi.Text = iTmp.ToString();

            try {
                Buffer.BlockCopy(data, 6, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            iTmp = BitConverter.ToUInt16(bATmp, 0);
            tbMpd2Rssi.Text = iTmp.ToString();

            bATmp = new byte[4];
            try {
                Buffer.BlockCopy(data, 8, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            uiTmp = BitConverter.ToUInt32(bATmp, 0);
            tbFrequenceCounter.Text = uiTmp.ToString();

            try {
                Buffer.BlockCopy(data, 12, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            fTmp = BitConverter.ToSingle(bATmp, 0);
            tbNtc1Temperature.Text = fTmp.ToString();

            try {
                Buffer.BlockCopy(data, 16, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            fTmp = BitConverter.ToSingle(bATmp, 0);
            tbNtc2Temperature.Text = fTmp.ToString();

            return 0;

        exit:
            return -1;
        }

        private int _UpPage00Read()
        {
            byte[] data = new byte[72];
            byte[] bATmp;
            int iTmp, i;
            UInt16 ui16Tmp;
            float fTmp;

            if (i2cWriteCB == null)
                goto exit;

            data[0] = 0;
            if (i2cWriteCB(0x51, 127, 1, data) < 0)
                goto exit;

            Thread.Sleep(100);

            if (i2cReadCB == null)
                goto exit;

            if (i2cReadCB(0x51, 128, 72, data) != 72)
                goto exit;

            bATmp = new byte[4];
            try {
                Buffer.BlockCopy(data, 0, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            fTmp = BitConverter.ToSingle(bATmp, 0);
            tbPid1Proportional.Text = fTmp.ToString();

            try {
                Buffer.BlockCopy(data, 4, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            fTmp = BitConverter.ToSingle(bATmp, 0);
            tbPid1Integral.Text = fTmp.ToString();

            try {
                Buffer.BlockCopy(data, 8, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            fTmp = BitConverter.ToSingle(bATmp, 0);
            tbPid1Derivative.Text = fTmp.ToString();

            try {
                Buffer.BlockCopy(data, 12, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            fTmp = BitConverter.ToSingle(bATmp, 0);
            tbPid2Proportional.Text = fTmp.ToString();

            try {
                Buffer.BlockCopy(data, 16, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            fTmp = BitConverter.ToSingle(bATmp, 0);
            tbPid2Integral.Text = fTmp.ToString();

            try {
                Buffer.BlockCopy(data, 20, bATmp, 0, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            fTmp = BitConverter.ToSingle(bATmp, 0);
            tbPid2Derivative.Text = fTmp.ToString();

            tbPid1TargetTemperature.Text = ((sbyte)data[24]).ToString();
            tbPid2TargetTemperature.Text = ((sbyte)data[25]).ToString();

            bATmp = new byte[2];
            try {
                Buffer.BlockCopy(data, 26, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            iTmp = BitConverter.ToUInt16(bATmp, 0);
            tbNtc1Beta.Text = iTmp.ToString();

            try {
                Buffer.BlockCopy(data, 28, bATmp, 0, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            iTmp = BitConverter.ToUInt16(bATmp, 0);
            tbNtc2Beta.Text = iTmp.ToString();

            fTmp = (sbyte)data[30];
            fTmp /= 10;
            tbNtc1TemperatureOffset.Text = fTmp.ToString();
            fTmp = (sbyte)data[31];
            fTmp /= 10;
            tbNtc2TemperatureOffset.Text = fTmp.ToString();

            for (i = 0; i < 8; i++) {
                singleGeneratorConfig[i].Mode = data[32 + i];
                try {
                    Buffer.BlockCopy(data, 40 + (i * 4), bATmp, 0, 2);
                }
                catch (Exception eBC) {
                    MessageBox.Show(eBC.ToString());
                    return -1;
                }
                ui16Tmp = BitConverter.ToUInt16(bATmp, 0);
                singleGeneratorConfig[i].Frequence = ui16Tmp;
                try {
                    Buffer.BlockCopy(data, 42 + (i * 4), bATmp, 0, 2);
                }
                catch (Exception eBC) {
                    MessageBox.Show(eBC.ToString());
                    return -1;
                }
                ui16Tmp = BitConverter.ToUInt16(bATmp, 0);
                singleGeneratorConfig[i].PhaseShift = ui16Tmp;
            }

            cbSignalGeneratorId.Text = "0";
            foreach (ComboboxItem item in cbSignalGeneratorMode.Items) {
                if (item.Value == singleGeneratorConfig[0].Mode) {
                    cbSignalGeneratorMode.SelectedItem = item;
                }
            }
            tbSignalGeneratorFrequency.Text = singleGeneratorConfig[0].Frequence.ToString();
            foreach (ComboboxItem item in cbSingleGeneratorPhaseShift.Items) {
                if (item.Value == singleGeneratorConfig[0].PhaseShift) {
                    cbSingleGeneratorPhaseShift.SelectedItem = item;
                }
            }

            return 0;

        exit:
            return -1;
        }

        private void bRead_Click(object sender, EventArgs e)
        {
            if (i2cConnectStatusCB == null) {
                MessageBox.Show("i2cConnectStatusCB == null");
                return;
            }
            if (i2cConnectStatusCB() == false) {
                MessageBox.Show("Need connect I2C first!!");
                return;
            }

            bRead.Enabled = false;

            if (_LowPageRead() < 0) {
                MessageBox.Show("_LowPageRead() fail!!");
                goto exit;
            }

            if (_UpPage00Read() < 0) {
                MessageBox.Show("_UpPage00Read() fail!!");
                goto exit;
            }

        exit:
            bRead.Enabled = true;
        }

        private void cbMoniter_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMoniter.Checked == true) {
                if (i2cConnectStatusCB == null) {
                    MessageBox.Show("i2cConnectStatusCB == null");
                    return;
                }
                if (i2cConnectStatusCB() == false) {
                    MessageBox.Show("Need connect I2C first!!");
                    return;
                }
                if (bwMonitor.IsBusy == true) {
                    MessageBox.Show("Need wait monitor thread stop!!");
                    cbMoniter.Checked = false;
                    return;
                }
                while (cvTec1PwmDuty.Count() > 0)
                    cvTec1PwmDuty.RemoveAt(0);
                while (cvTec1PwmDutyMax.Count() > 0)
                    cvTec1PwmDutyMax.RemoveAt(0);
                while (cvTec1PwmDutyMin.Count() > 0)
                    cvTec1PwmDutyMin.RemoveAt(0);
                while (cvTec2PwmDuty.Count() > 0)
                    cvTec2PwmDuty.RemoveAt(0);
                while (cvTec2PwmDutyMax.Count() > 0)
                    cvTec2PwmDutyMax.RemoveAt(0);
                while (cvTec2PwmDutyMin.Count() > 0)
                    cvTec2PwmDutyMin.RemoveAt(0);
                while (cvNtc1Temperature.Count() > 0)
                    cvNtc1Temperature.RemoveAt(0);
                while (cvNtc1TemperatureMax.Count() > 0)
                    cvNtc1TemperatureMax.RemoveAt(0);
                while (cvNtc1TemperatureMin.Count() > 0)
                    cvNtc1TemperatureMin.RemoveAt(0);
                while (cvNtc2Temperature.Count() > 0)
                    cvNtc2Temperature.RemoveAt(0);
                while (cvNtc2TemperatureMax.Count() > 0)
                    cvNtc2TemperatureMax.RemoveAt(0);
                while (cvNtc2TemperatureMin.Count() > 0)
                    cvNtc2TemperatureMin.RemoveAt(0);
                bRead.Enabled = false;
                bWrite.Enabled = false;
                bSave.Enabled = false;
                bSignalGeneratorSet.Enabled = false;
                monitorStart = true;
                bwMonitor.RunWorkerAsync();
            }
            else {
                if (monitorStart == false)
                    return;

                monitorStart = false;
            }
        }

        private int _UpPage00Write()
        {
            byte[] data = new byte[72];
            byte[] bATmp;
            sbyte sbTmp;
            int i;
            UInt16 ui16Tmp;
            float fTmp;

            if (getPasswordCB == null)
                goto exit;

            bATmp = new byte[4];
            getPasswordCB(bATmp.Length, bATmp);

            if (i2cWriteCB == null)
                goto exit;

            if (i2cWriteCB(0x51, 123, 4, bATmp) < 0)
                goto exit;

            data[0] = 0;
            if (i2cWriteCB(0x51, 127, 1, data) < 0)
                goto exit;

            if (float.TryParse(tbPid1Proportional.Text, out fTmp) == false) {
                MessageBox.Show("float.TryParse(" + tbPid1Proportional.Text + ") false!!");
                goto exit;
            }
            bATmp = BitConverter.GetBytes(fTmp);
            try {
                Buffer.BlockCopy(bATmp, 0, data, 0, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            if (float.TryParse(tbPid1Integral.Text, out fTmp) == false) {
                MessageBox.Show("float.TryParse(" + tbPid1Integral.Text + ") false!!");
                goto exit;
            }
            bATmp = BitConverter.GetBytes(fTmp);
            try {
                Buffer.BlockCopy(bATmp, 0, data, 4, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            if (float.TryParse(tbPid1Derivative.Text, out fTmp) == false) {
                MessageBox.Show("float.TryParse(" + tbPid1Derivative.Text + ") false!!");
                goto exit;
            }
            bATmp = BitConverter.GetBytes(fTmp);
            try {
                Buffer.BlockCopy(bATmp, 0, data, 8, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            if (float.TryParse(tbPid2Proportional.Text, out fTmp) == false) {
                MessageBox.Show("float.TryParse(" + tbPid2Proportional.Text + ") false!!");
                goto exit;
            }
            bATmp = BitConverter.GetBytes(fTmp);
            try {
                Buffer.BlockCopy(bATmp, 0, data, 12, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            if (float.TryParse(tbPid2Integral.Text, out fTmp) == false) {
                MessageBox.Show("float.TryParse(" + tbPid2Integral.Text + ") false!!");
                goto exit;
            }
            bATmp = BitConverter.GetBytes(fTmp);
            try {
                Buffer.BlockCopy(bATmp, 0, data, 16, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            if (float.TryParse(tbPid2Derivative.Text, out fTmp) == false) {
                MessageBox.Show("float.TryParse(" + tbPid2Derivative.Text + ") false!!");
                goto exit;
            }
            bATmp = BitConverter.GetBytes(fTmp);
            try {
                Buffer.BlockCopy(bATmp, 0, data, 20, 4);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            if (sbyte.TryParse(tbPid1TargetTemperature.Text, out sbTmp) == false) {
                MessageBox.Show("float.TryParse(" + tbPid1TargetTemperature.Text + ") false!!");
                goto exit;
            }                
            data[24] = (byte)sbTmp;

            if (sbyte.TryParse(tbPid2TargetTemperature.Text, out sbTmp) == false) {
                MessageBox.Show("sbyte.TryParse(" + tbPid2TargetTemperature.Text + ") false!!");
                goto exit;
            }
            data[25] = (byte)sbTmp;

            if (UInt16.TryParse(tbNtc1Beta.Text, out ui16Tmp) == false) {
                MessageBox.Show("UInt16.TryParse(" + tbNtc1Beta.Text + ") false!!");
                goto exit;
            }
            bATmp = BitConverter.GetBytes(ui16Tmp);
            try {
                Buffer.BlockCopy(bATmp, 0, data, 26, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            if (UInt16.TryParse(tbNtc2Beta.Text, out ui16Tmp) == false) {
                MessageBox.Show("UInt16.TryParse(" + tbNtc2Beta.Text + ") false!!");
                goto exit;
            }
            bATmp = BitConverter.GetBytes(ui16Tmp);
            try {
                Buffer.BlockCopy(bATmp, 0, data, 28, 2);
            }
            catch (Exception eBC) {
                MessageBox.Show(eBC.ToString());
                return -1;
            }

            if (float.TryParse(tbNtc1TemperatureOffset.Text, out fTmp) == false) {
                MessageBox.Show("float.TryParse(" + tbNtc1TemperatureOffset.Text + ") false!!");
                goto exit;
            }
            fTmp *= 10;
            if ((fTmp > 127) || (fTmp < -128)) {
                MessageBox.Show("NTC1 temperature offset out of range (12.7 ~ -12.8): " + tbNtc1TemperatureOffset.Text);
                goto exit;
            }
            sbTmp = (sbyte)fTmp;
            data[30] = (byte)sbTmp;

            if (float.TryParse(tbNtc2TemperatureOffset.Text, out fTmp) == false) {
                MessageBox.Show("sbyte.TryParse(" + tbNtc2TemperatureOffset.Text + ") false!!");
                goto exit;
            }
            fTmp *= 10;
            if ((fTmp > 127) || (fTmp < -128)) {
                MessageBox.Show("NTC2 temperature offset out of range (12.7 ~ -12.8): " + tbNtc2TemperatureOffset.Text);
                goto exit;
            }
            sbTmp = (sbyte)fTmp;
            data[31] = (byte)sbTmp;

            for (i = 0; i < 8; i++) {
                data[32 + i] = singleGeneratorConfig[i].Mode;

                bATmp = BitConverter.GetBytes(singleGeneratorConfig[i].Frequence);
                try {
                    Buffer.BlockCopy(bATmp, 0, data, 40 + (i * 4), 2);
                }
                catch (Exception eBC) {
                    MessageBox.Show(eBC.ToString());
                    goto exit;
                }

                bATmp = BitConverter.GetBytes(singleGeneratorConfig[i].PhaseShift);
                try {
                    Buffer.BlockCopy(bATmp, 0, data, 42 + (i * 4), 2);
                }
                catch (Exception eBC) {
                    MessageBox.Show(eBC.ToString());
                    goto exit;
                }
            }

            if (i2cWriteCB(0x51, 128, 72, data) < 0)
                goto exit;

            return 0;

        exit:
            return -1;
        }

        private void bWrite_Click(object sender, EventArgs e)
        {
            if (i2cConnectStatusCB == null) {
                MessageBox.Show("i2cConnectStatusCB == null");
                return;
            }
            if (i2cConnectStatusCB() == false) {
                MessageBox.Show("Need connect I2C first!!");
                return;
            }

            bWrite.Enabled = false;
            if (_UpPage00Write() < 0) {
                MessageBox.Show("_LowPageWrite() fail!!");
                goto exit;
            }

        exit:
            bWrite.Enabled = true;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            byte[] bATmp = new byte[4];

            if (i2cConnectStatusCB == null) {
                MessageBox.Show("i2cConnectStatusCB == null");
                return;
            }
            if (i2cConnectStatusCB() == false) {
                MessageBox.Show("Need connect I2C first!!");
                return;
            }

            bSave.Enabled = false;

            if (getPasswordCB == null) {
                MessageBox.Show("getPasswordCB == null");
                goto exit;
            }

            bATmp = new byte[4];
            getPasswordCB(bATmp.Length, bATmp);

            if (i2cWriteCB == null)
                goto exit;

            if (i2cWriteCB(0x51, 123, 4, bATmp) < 0)
                goto exit;

            bATmp[0] = 0xAA;
            if (i2cWriteCB(0x51, 127, 1, bATmp) < 0)
                goto exit;

            bATmp[0] = 0xAA;
            if (i2cWriteCB(0x51, 162, 1, bATmp) < 0)
                goto exit;

            Thread.Sleep(1000);

        exit:
            bSave.Enabled = true;
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public struct SignalGeneratorConfig
    {
        public SignalGeneratorConfig(byte mode, UInt16 frequence, UInt16 phaseShift)
        {
            Mode = mode;
            Frequence = frequence;
            PhaseShift = phaseShift;
        }

        public byte Mode { get; set; }
        public UInt16 Frequence { get; set; }
        public UInt16 PhaseShift { get; set; }

        public override string ToString() => $"Mode:{Mode} Frequence:{Frequence} PhaseShift:{PhaseShift}";

        public byte[] ToByteArray()
        {
            byte[] bArray = new byte[5];
            byte[] bTmp;

            bArray[0] = Mode;
            bTmp = BitConverter.GetBytes(Frequence);
            bArray[1] = bTmp[0];
            bArray[2] = bTmp[1];
            bTmp = BitConverter.GetBytes(PhaseShift);
            bArray[3] = bTmp[0];
            bArray[4] = bTmp[1];

            return bArray;
        }
    }
}
