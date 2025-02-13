using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;

namespace LiDAR_live_demo
{
    public partial class UcLiDARLiveDemo : UserControl
    {
        // delegate is used to write to a UI control from a non-UI thread
        private delegate void SetTextDeleg(string text);

        const int monitorCommandDelay = 50;
        const int monitorCommandChangePageDelay = 100;

        private volatile int waitResponseCounter = 0;
        private volatile float[] ntcTemperature = new float[3];
        private volatile UInt16[] ntcResistance = new UInt16[3];
        private volatile UInt16[] ntcBeta = new UInt16[3];
        private volatile UInt16[] tiaRssi = new UInt16[1];
        private volatile byte[] ntcTemperatureOffset = new byte[3];
        private volatile bool monitorStart = false;

        private SeriesCollection scNtc1Temperature { get; set; }
        private SeriesCollection scNtc2Temperature { get; set; }
        private SeriesCollection scNtc3Temperature { get; set; }
        private SeriesCollection scTiaRssi { get; set; }
        private ChartValues<float> cvNtc1Temperature, cvNtc1TemperatureMax, cvNtc1TemperatureMin;
        private ChartValues<float> cvNtc2Temperature, cvNtc2TemperatureMax, cvNtc2TemperatureMin;
        private ChartValues<float> cvNtc3Temperature, cvNtc3TemperatureMax, cvNtc3TemperatureMin;
        private ChartValues<float> cvTiaRssi, cvTiaRssiMax, cvTiaRssiMin;
        private BackgroundWorker bwMonitor;
        private SerialPort serialPort;

        private int _GetMonitorValue()
        {
            String buf;
            int i;

            if (cbSerialPortConnected.Checked == false) {
                MessageBox.Show("Please connect serial port first!!");
                return -1;
            }

            try {
                while (waitResponseCounter > 0) {
                    if (monitorStart == false)
                        return 0;
                    Thread.Sleep(monitorCommandDelay);
                }

                for (i = 0; i < 20; i ++) {
                    if (monitorStart == false)
                        return 0;
                    waitResponseCounter++;
                    bwMonitor.ReportProgress(i, null);
                    buf = "r 0x" + i.ToString("X2") + "\r";
                    serialPort.Write(buf);
                    Thread.Sleep(monitorCommandDelay);
                }
            }
            catch (IOException ioE) {
                MessageBox.Show(ioE.ToString());
                return -1;
            }

            return 0;
        }

        public void MonitorValueUpdateApi(object sender, DoWorkEventArgs e)
        {
            while (monitorStart) {
                if (_GetMonitorValue() < 0) {
                    bwMonitor.ReportProgress(50, null);
                    goto exit;
                }
                bwMonitor.ReportProgress(20, null);
            }
        exit:
            bwMonitor.ReportProgress(100, null);
        }

        private int _UpdateMonitorValue()
        {
            float fTmp;

            if (monitorStart == false) {
                tbNtc1Beta.Text = ntcBeta[0].ToString();
                tbNtc1Beta.Update();
                tbNtc2Beta.Text = ntcBeta[1].ToString();
                tbNtc2Beta.Update();
                tbNtc3Beta.Text = ntcBeta[2].ToString();
                tbNtc3Beta.Update();
                fTmp = (sbyte)ntcTemperatureOffset[0];
                fTmp /= 10;
                tbNtc1TemperatureOffset.Text = fTmp.ToString("F1");
                tbNtc1TemperatureOffset.Update();
                fTmp = (sbyte)ntcTemperatureOffset[1];
                fTmp /= 10;
                tbNtc2TemperatureOffset.Text = fTmp.ToString("F1");
                tbNtc2TemperatureOffset.Update();
                fTmp = (sbyte)ntcTemperatureOffset[2];
                fTmp /= 10;
                tbNtc3TemperatureOffset.Text = fTmp.ToString("F1");
                tbNtc3TemperatureOffset.Update();
            }

            tbNtc1Resistance.Text = ntcResistance[0].ToString();
            tbNtc1Resistance.Update();
            tbNtc2Resistance.Text = ntcResistance[1].ToString();
            tbNtc2Resistance.Update();
            tbNtc3Resistance.Text = ntcResistance[2].ToString();
            tbNtc3Resistance.Update();

            tbTiaRssi.Text = tiaRssi[0].ToString();
            tbTiaRssi.Update();

            tbNtc1Temperature.Text = ntcTemperature[0].ToString();
            tbNtc1Temperature.Update();

            tbNtc2Temperature.Text = ntcTemperature[1].ToString();
            tbNtc2Temperature.Update();

            tbNtc3Temperature.Text = ntcTemperature[2].ToString();
            tbNtc3Temperature.Update();

            if (monitorStart == false)
                return 0;

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

            cvNtc3Temperature.Add(ntcTemperature[2]);
            if (cvNtc3Temperature.Count > 30)
                cvNtc3Temperature.RemoveAt(0);
            if (cvNtc3TemperatureMax.Count == 0)
                cvNtc3TemperatureMax.Add(ntcTemperature[2]);
            else {
                if (cvNtc3TemperatureMax.Max() < ntcTemperature[2]) {
                    cvNtc3TemperatureMax.RemoveAt(0);
                    cvNtc3TemperatureMax.Add(ntcTemperature[2]);
                }
            }
            if (cvNtc3TemperatureMin.Count == 0)
                cvNtc3TemperatureMin.Add(ntcTemperature[2]);
            else {
                if (cvNtc3TemperatureMin.Min() > ntcTemperature[2]) {
                    cvNtc3TemperatureMin.RemoveAt(0);
                    cvNtc3TemperatureMin.Add(ntcTemperature[2]);
                }
            }

            cvTiaRssi.Add(tiaRssi[0]);
            if (cvTiaRssi.Count > 30)
                cvTiaRssi.RemoveAt(0);
            if (cvTiaRssiMax.Count == 0)
                cvTiaRssiMax.Add(tiaRssi[0]);
            else {
                if (cvTiaRssiMax.Max() < tiaRssi[0]) {
                    cvTiaRssiMax.RemoveAt(0);
                    cvTiaRssiMax.Add(tiaRssi[0]);
                }
            }
            if (cvTiaRssiMin.Count == 0)
                cvTiaRssiMin.Add(tiaRssi[0]);
            else {
                if (cvTiaRssiMin.Min() > tiaRssi[0]) {
                    cvTiaRssiMin.RemoveAt(0);
                    cvTiaRssiMin.Add(tiaRssi[0]);
                }
            }

            waitResponseCounter -= 20;

            return 0;
        }

        public void MonitorProgressChangedApi(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage) {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                    cbMoniter.Text = "Monitor " + e.ProgressPercentage.ToString("D2") + "/20";
                    cbMoniter.Update();
                    break;

                case 20:
                    cbMoniter.Text = "Monitor 20/20";
                    cbMoniter.Update();
                    _UpdateMonitorValue();
                    break;

                case 100:
                    if (cbMoniter.Checked == true)
                        cbMoniter.Checked = false;
                    if (cbSerialPortConnected.Checked == true) {
                        bLiveDemoRead.Enabled = true;
                        bLiveDemoWrite.Enabled = true;
                        bLiveDemoSave.Enabled = true;
                    }
                    else
                        cbMoniter.Enabled = false;
                    cbMoniter.Text = "Monitor";
                    waitResponseCounter = 0;
                    break;

                default:
                    break;
            }
        }

        public UcLiDARLiveDemo()
        {
            ComboboxItem item;
            string[] ports = SerialPort.GetPortNames();

            InitializeComponent();
            
            foreach (string port in ports)
                cbSerialPorts.Items.Add(port);

            item = new ComboboxItem();
            item.Text = "0:40k ohm";
            item.Value = 0;
            cbTn581GainCh0.Items.Add(item);
            cbTn581GainCh1.Items.Add(item);
            cbTn581GainCh2.Items.Add(item);
            cbTn581GainCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:12k ohm";
            item.Value = 1;
            cbTn581GainCh0.Items.Add(item);
            cbTn581GainCh1.Items.Add(item);
            cbTn581GainCh2.Items.Add(item);
            cbTn581GainCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:4k ohm";
            item.Value = 2;
            cbTn581GainCh0.Items.Add(item);
            cbTn581GainCh1.Items.Add(item);
            cbTn581GainCh2.Items.Add(item);
            cbTn581GainCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:2k ohm";
            item.Value = 3;
            cbTn581GainCh0.Items.Add(item);
            cbTn581GainCh1.Items.Add(item);
            cbTn581GainCh2.Items.Add(item);
            cbTn581GainCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:900kHz";
            item.Value = 0;
            cbTn581LfcCh0.Items.Add(item);
            cbTn581LfcCh1.Items.Add(item);
            cbTn581LfcCh2.Items.Add(item);
            cbTn581LfcCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:600kHz";
            item.Value = 1;
            cbTn581LfcCh0.Items.Add(item);
            cbTn581LfcCh1.Items.Add(item);
            cbTn581LfcCh2.Items.Add(item);
            cbTn581LfcCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:300kHz";
            item.Value = 2;
            cbTn581LfcCh0.Items.Add(item);
            cbTn581LfcCh1.Items.Add(item);
            cbTn581LfcCh2.Items.Add(item);
            cbTn581LfcCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:Invalid";
            item.Value = 3;
            cbTn581LfcCh0.Items.Add(item);
            cbTn581LfcCh1.Items.Add(item);
            cbTn581LfcCh2.Items.Add(item);
            cbTn581LfcCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:Ch0";
            item.Value = 0;
            cbTn581RssiSel.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Ch1";
            item.Value = 1;
            cbTn581RssiSel.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:Ch2";
            item.Value = 2;
            cbTn581RssiSel.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:Ch3";
            item.Value = 3;
            cbTn581RssiSel.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:reserved";
            item.Value = 4;
            cbTn581RssiSel.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:reserved";
            item.Value = 5;
            cbTn581RssiSel.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:reserved";
            item.Value = 6;
            cbTn581RssiSel.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:reserved";
            item.Value = 7;
            cbTn581RssiSel.Items.Add(item);

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

            cvNtc3Temperature = new ChartValues<float> { };
            cvNtc3TemperatureMax = new ChartValues<float> { };
            cvNtc3TemperatureMin = new ChartValues<float> { };
            scNtc3Temperature = new SeriesCollection {
                new LineSeries
                {
                    Name = "NTC3",
                    Values = cvNtc3Temperature,
                    Fill = null
                },
                new LineSeries
                {
                    Name = "Max",
                    Values = cvNtc3TemperatureMax,
                    Fill = null
                },
                new LineSeries
                {
                    Name = "Min",
                    Values = cvNtc3TemperatureMin,
                    Fill = null
                },
            };
            ccNtc3Temperature.Series = scNtc3Temperature;

            cvTiaRssi = new ChartValues<float> { };
            cvTiaRssiMax = new ChartValues<float> { };
            cvTiaRssiMin = new ChartValues<float> { };
            scTiaRssi = new SeriesCollection {
                new LineSeries
                {
                    Name = "TiaRssi",
                    Values = cvTiaRssi,
                    Fill = null
                },
                new LineSeries
                {
                    Name = "Max",
                    Values = cvTiaRssiMax,
                    Fill = null
                },
                new LineSeries
                {
                    Name = "Min",
                    Values = cvTiaRssiMin,
                    Fill = null
                },
            };
            ccTiaRssi.Series = scTiaRssi;

            bwMonitor = new BackgroundWorker();
            bwMonitor.WorkerReportsProgress = true;
            bwMonitor.WorkerSupportsCancellation = false;
            bwMonitor.DoWork += new DoWorkEventHandler(MonitorValueUpdateApi);
            bwMonitor.ProgressChanged += new ProgressChangedEventHandler(MonitorProgressChangedApi);
        }

        private void bSerialPortReflash_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();

            if (cbSerialPortConnected.Checked == false) {
                cbSerialPorts.Items.Clear();
                foreach (string port in ports)
                    cbSerialPorts.Items.Add(port);
            }
        }

        private void cbSerialPortConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSerialPortConnected.Checked == true) {
                if (cbSerialPorts.Text.Length != 0) {
                    cbSerialPorts.Enabled = false;
                    bSerialPortReflash.Enabled = false;
                    try {
                        serialPort = new SerialPort(cbSerialPorts.Text, 9600, Parity.None, 8, StopBits.One);
                        serialPort.Open();
                        serialPort.DataReceived += new SerialDataReceivedEventHandler(ConsoleRxHandler);
                    }
                    catch (IOException ioE) {
                        MessageBox.Show(ioE.ToString());
                        cbSerialPortConnected.Checked = false;
                        return;
                    }
                    tbConsoleRx.AppendText("Serial port " + cbSerialPorts.Text + " opened." + Environment.NewLine);
                    cbMoniter.Enabled = true;
                    bLiveDemoSave.Enabled = true;
                    bLiveDemoRead.Enabled = true;
                    bLiveDemoWrite.Enabled = true;
                    bConsoleTx.Enabled = true;
                }
                else
                    cbSerialPortConnected.Checked = false;
            }
            else {
                cbSerialPorts.Enabled = true;
                bSerialPortReflash.Enabled = true;
                cbMoniter.Checked = false;
                cbMoniter.Enabled = false;
                bLiveDemoSave.Enabled = false;
                bLiveDemoRead.Enabled = false;
                bLiveDemoWrite.Enabled = false;
                bConsoleTx.Enabled = false;
                if ((serialPort != null) && (serialPort.IsOpen)) {
                    serialPort.Close();
                    tbConsoleRx.AppendText("Serial port " + cbSerialPorts.Text + " closed." + Environment.NewLine);
                }
            }
        }

        private void bConsoleTx_Click(object sender, EventArgs e)
        {
            String buf;

            if (cbSerialPortConnected.Checked == false) {
                tbConsoleRx.AppendText("Please connect serial port first!!\n");
                return;
            }

            if (tbConsoleTx.Text.Length == 0)
                return;

            try {
                buf = tbConsoleTx.Text + "\r";
                serialPort.Write(buf);
            }
            catch (IOException ioE) {
                MessageBox.Show(ioE.ToString());
            }
        }

        private void ConsoleRxHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string data;

            try {
                data = sp.ReadLine();
                this.BeginInvoke(new SetTextDeleg(DisplayRxData), new object[] { data });
            } catch (IOException ioE) {
                MessageBox.Show(ioE.ToString());
            }
        }

        private void LiveDemoParserAddr0x00(byte value)
        {
            byte[] bATmp = new byte[2];
            
            bATmp = BitConverter.GetBytes(ntcResistance[0]);

            bATmp[0] = value;

            ntcResistance[0] = BitConverter.ToUInt16(bATmp, 0);
        }

        private void LiveDemoParserAddr0x01(byte value)
        {
            byte[] bATmp = new byte[2];

            bATmp = BitConverter.GetBytes(ntcResistance[0]);

            bATmp[1] = value;

            ntcResistance[0] = BitConverter.ToUInt16(bATmp, 0);
        }

        private void LiveDemoParserAddr0x02(byte value)
        {
            byte[] bATmp = new byte[2];

            bATmp = BitConverter.GetBytes(ntcResistance[1]);

            bATmp[0] = value;

            ntcResistance[1] = BitConverter.ToUInt16(bATmp, 0);
        }

        private void LiveDemoParserAddr0x03(byte value)
        {
            byte[] bATmp = new byte[2];

            bATmp = BitConverter.GetBytes(ntcResistance[1]);

            bATmp[1] = value;

            ntcResistance[1] = BitConverter.ToUInt16(bATmp, 0);
        }

        private void LiveDemoParserAddr0x04(byte value)
        {
            byte[] bATmp = new byte[2];

            bATmp = BitConverter.GetBytes(ntcResistance[2]);

            bATmp[0] = value;

            ntcResistance[2] = BitConverter.ToUInt16(bATmp, 0);
        }

        private void LiveDemoParserAddr0x05(byte value)
        {
            byte[] bATmp = new byte[2];

            bATmp = BitConverter.GetBytes(ntcResistance[2]);

            bATmp[1] = value;

            ntcResistance[2] = BitConverter.ToUInt16(bATmp, 0);
        }

        private void LiveDemoParserAddr0x06(byte value)
        {
            byte[] bATmp = new byte[2];

            bATmp = BitConverter.GetBytes(tiaRssi[0]);

            bATmp[0] = value;

            tiaRssi[0] = BitConverter.ToUInt16(bATmp, 0);
        }

        private void LiveDemoParserAddr0x07(byte value)
        {
            byte[] bATmp = new byte[2];

            bATmp = BitConverter.GetBytes(tiaRssi[0]);

            bATmp[1] = value;

            tiaRssi[0] = BitConverter.ToUInt16(bATmp, 0);
        }

        private void LiveDemoParserAddr0x08(byte value)
        {
            byte[] bATmp = new byte[4];

            bATmp = BitConverter.GetBytes(ntcTemperature[0]);

            bATmp[0] = value;

            ntcTemperature[0] = BitConverter.ToSingle(bATmp, 0);
        }

        private void LiveDemoParserAddr0x09(byte value)
        {
            byte[] bATmp = new byte[4];

            bATmp = BitConverter.GetBytes(ntcTemperature[0]);

            bATmp[1] = value;

            ntcTemperature[0] = BitConverter.ToSingle(bATmp, 0);
        }

        private void LiveDemoParserAddr0x0A(byte value)
        {
            byte[] bATmp = new byte[4];

            bATmp = BitConverter.GetBytes(ntcTemperature[0]);

            bATmp[2] = value;

            ntcTemperature[0] = BitConverter.ToSingle(bATmp, 0);
        }

        private void LiveDemoParserAddr0x0B(byte value)
        {
            byte[] bATmp = new byte[4];

            bATmp = BitConverter.GetBytes(ntcTemperature[0]);

            bATmp[3] = value;

            ntcTemperature[0] = BitConverter.ToSingle(bATmp, 0);
        }

        private void LiveDemoParserAddr0x0C(byte value)
        {
            byte[] bATmp = new byte[4];

            bATmp = BitConverter.GetBytes(ntcTemperature[1]);

            bATmp[0] = value;

            ntcTemperature[1] = BitConverter.ToSingle(bATmp, 0);
        }

        private void LiveDemoParserAddr0x0D(byte value)
        {
            byte[] bATmp = new byte[4];

            bATmp = BitConverter.GetBytes(ntcTemperature[1]);

            bATmp[1] = value;

            ntcTemperature[1] = BitConverter.ToSingle(bATmp, 0);
        }

        private void LiveDemoParserAddr0x0E(byte value)
        {
            byte[] bATmp = new byte[4];

            bATmp = BitConverter.GetBytes(ntcTemperature[1]);

            bATmp[2] = value;

            ntcTemperature[1] = BitConverter.ToSingle(bATmp, 0);
        }

        private void LiveDemoParserAddr0x0F(byte value)
        {
            byte[] bATmp = new byte[4];

            bATmp = BitConverter.GetBytes(ntcTemperature[1]);

            bATmp[3] = value;

            ntcTemperature[1] = BitConverter.ToSingle(bATmp, 0);
        }

        private void LiveDemoParserAddr0x10(byte value)
        {
            byte[] bATmp = new byte[4];

            bATmp = BitConverter.GetBytes(ntcTemperature[2]);

            bATmp[0] = value;

            ntcTemperature[2] = BitConverter.ToSingle(bATmp, 0);
        }

        private void LiveDemoParserAddr0x11(byte value)
        {
            byte[] bATmp = new byte[4];

            bATmp = BitConverter.GetBytes(ntcTemperature[2]);

            bATmp[1] = value;

            ntcTemperature[2] = BitConverter.ToSingle(bATmp, 0);
        }

        private void LiveDemoParserAddr0x12(byte value)
        {
            byte[] bATmp = new byte[4];

            bATmp = BitConverter.GetBytes(ntcTemperature[2]);

            bATmp[2] = value;

            ntcTemperature[2] = BitConverter.ToSingle(bATmp, 0);
        }

        private void LiveDemoParserAddr0x13(byte value)
        {
            byte[] bATmp = new byte[4];

            bATmp = BitConverter.GetBytes(ntcTemperature[2]);

            bATmp[3] = value;

            ntcTemperature[2] = BitConverter.ToSingle(bATmp, 0);
        }

        private void LiveDemoParserAddr0x80(byte value)
        {
            byte[] bATmp = new byte[2];

            bATmp = BitConverter.GetBytes(ntcBeta[0]);

            bATmp[0] = value;

            ntcBeta[0] = BitConverter.ToUInt16(bATmp, 0);
        }

        private void LiveDemoParserAddr0x81(byte value)
        {
            byte[] bATmp = new byte[2];

            bATmp = BitConverter.GetBytes(ntcBeta[0]);

            bATmp[1] = value;

            ntcBeta[0] = BitConverter.ToUInt16(bATmp, 0);
        }

        private void LiveDemoParserAddr0x82(byte value)
        {
            byte[] bATmp = new byte[2];

            bATmp = BitConverter.GetBytes(ntcBeta[1]);

            bATmp[0] = value;

            ntcBeta[1] = BitConverter.ToUInt16(bATmp, 0);
        }

        private void LiveDemoParserAddr0x83(byte value)
        {
            byte[] bATmp = new byte[2];

            bATmp = BitConverter.GetBytes(ntcBeta[1]);

            bATmp[1] = value;

            ntcBeta[1] = BitConverter.ToUInt16(bATmp, 0);
        }

        private void LiveDemoParserAddr0x84(byte value)
        {
            byte[] bATmp = new byte[2];

            bATmp = BitConverter.GetBytes(ntcBeta[2]);

            bATmp[0] = value;

            ntcBeta[2] = BitConverter.ToUInt16(bATmp, 0);
        }

        private void LiveDemoParserAddr0x85(byte value)
        {
            byte[] bATmp = new byte[2];

            bATmp = BitConverter.GetBytes(ntcBeta[2]);

            bATmp[1] = value;

            ntcBeta[2] = BitConverter.ToUInt16(bATmp, 0);
        }

        private void LiveDemoParserAddr0x86(byte value)
        {
            ntcTemperatureOffset[0] = value;
        }

        private void LiveDemoParserAddr0x87(byte value)
        {
            ntcTemperatureOffset[1] = value;
        }

        private void LiveDemoParserAddr0x88(byte value)
        {
            ntcTemperatureOffset[2] = value;
        }

        private void LiveDemoHandleRxData(string rxData)
        {
            string[] parts;
            byte addr, value;

            if ((rxData == null) || (rxData.Length <= 1) || (rxData.IndexOf("LIVE DEMO") >= 0))
                return;

            parts = rxData.Split(' ');

            if ((parts.Length > 1) && (parts[0] == "w"))
                return;

            if (parts.Length == 3 && parts[1].StartsWith("0x") && parts[2].StartsWith("0x")) {
                addr = Convert.ToByte(parts[1].Substring(2, 2), 16); // 去掉 "0x" 取2位並轉換
                value = Convert.ToByte(parts[2].Substring(2, 2), 16);

                switch (addr) {
                    case 0x00:
                        LiveDemoParserAddr0x00(value);
                        return;

                    case 0x01:
                        LiveDemoParserAddr0x01(value);
                        return;

                    case 0x02:
                        LiveDemoParserAddr0x02(value);
                        return;

                    case 0x03:
                        LiveDemoParserAddr0x03(value);
                        return;

                    case 0x04:
                        LiveDemoParserAddr0x04(value);
                        return;

                    case 0x05:
                        LiveDemoParserAddr0x05(value);
                        return;

                    case 0x06:
                        LiveDemoParserAddr0x06(value);
                        return;

                    case 0x07:
                        LiveDemoParserAddr0x07(value);
                        return;

                    case 0x08:
                        LiveDemoParserAddr0x08(value);
                        return;

                    case 0x09:
                        LiveDemoParserAddr0x09(value);
                        return;

                    case 0x0A:
                        LiveDemoParserAddr0x0A(value);
                        return;

                    case 0x0B:
                        LiveDemoParserAddr0x0B(value);
                        return;

                    case 0x0C:
                        LiveDemoParserAddr0x0C(value);
                        return;

                    case 0x0D:
                        LiveDemoParserAddr0x0D(value);
                        return;

                    case 0x0E:
                        LiveDemoParserAddr0x0E(value);
                        return;

                    case 0x0F:
                        LiveDemoParserAddr0x0F(value);
                        return;

                    case 0x10:
                        LiveDemoParserAddr0x10(value);
                        return;

                    case 0x11:
                        LiveDemoParserAddr0x11(value);
                        return;

                    case 0x12:
                        LiveDemoParserAddr0x12(value);
                        return;

                    case 0x13:
                        LiveDemoParserAddr0x13(value);
                        return;

                    case 0x80:
                        LiveDemoParserAddr0x80(value);
                        return;

                    case 0x81:
                        LiveDemoParserAddr0x81(value);
                        return;

                    case 0x82:
                        LiveDemoParserAddr0x82(value);
                        return;

                    case 0x83:
                        LiveDemoParserAddr0x83(value);
                        return;

                    case 0x84:
                        LiveDemoParserAddr0x84(value);
                        return;

                    case 0x85:
                        LiveDemoParserAddr0x85(value);
                        return;

                    case 0x86:
                        LiveDemoParserAddr0x86(value);
                        return;

                    case 0x87:
                        LiveDemoParserAddr0x87(value);
                        return;

                    case 0x88:
                        LiveDemoParserAddr0x88(value);
                        _UpdateMonitorValue();
                        return;

                    default:
                        MessageBox.Show("Unhandle addr: " + parts[1] + " Error!!");
                        break;
                }
            }
            
            MessageBox.Show("Parser error!!\n" +
                    "rxData: " + rxData);
        }

        private void bLiveDemoRead_Click(object sender, EventArgs e)
        {
            String buf;
            int i;

            if (cbSerialPortConnected.Checked == false) {
                MessageBox.Show("Please connect serial port first!!");
                return;
            }

            bLiveDemoRead.Enabled = false;
            bLiveDemoRead.Text = "Reading";
            bLiveDemoRead.Update();

            try {
                buf = "w 0x7F 0x00\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);

                for (i = 0; i < 20; i++) {
                    buf = "r 0x" + i.ToString("X2") + "\r";
                    serialPort.Write(buf);
                    Thread.Sleep(monitorCommandDelay);
                }

                buf = "r 0x80\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0x81\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0x82\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0x83\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0x84\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0x85\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0x86\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0x87\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0x88\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
            }
            catch (IOException ioE) {
                MessageBox.Show(ioE.ToString());
            }

            bLiveDemoRead.Text = "Read";
            bLiveDemoRead.Enabled = true;
        }

        private void TiaTn581ParserAddr0x00(byte value)
        {
            if ((value & 0x02) != 0)
                cbTn581Standby.Checked = true;
            else
                cbTn581Standby.Checked = false;

            if ((value & 0x80) != 0)
                cbTn581ChenPinStandbyEn.Checked = true;
            else
                cbTn581ChenPinStandbyEn.Checked = false;
        }

        private void TiaTn581ParserAddr0x02(byte value)
        {
            if ((value & 0x01) != 0)
                cbTn581ChenPinStatus.Checked = true;
            else
                cbTn581ChenPinStatus.Checked = false;
        }

        private void TiaTn581ParserAddr0x11(byte value)
        {
            if ((value & 0x01) != 0)
                cbTn581RxEnCh0.Checked = true;
            else
                cbTn581RxEnCh0.Checked = false;

            if ((value & 0x02) != 0)
                cbTn581RxEnCh1.Checked = true;
            else
                cbTn581RxEnCh1.Checked = false;

            if ((value & 0x04) != 0)
                cbTn581RxEnCh2.Checked = true;
            else
                cbTn581RxEnCh2.Checked = false;

            if ((value & 0x08) != 0)
                cbTn581RxEnCh3.Checked = true;
            else
                cbTn581RxEnCh3.Checked = false;
        }

        private void TiaTn581ParserAddr0x14(byte value)
        {
            foreach (ComboboxItem item in cbTn581GainCh3.Items) {
                if (item.Value == (value & 0xC0) >> 6) {
                    cbTn581GainCh3.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbTn581GainCh2.Items) {
                if (item.Value == (value & 0x30) >> 4) {
                    cbTn581GainCh2.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbTn581GainCh1.Items) {
                if (item.Value == (value & 0x0C) >> 2) {
                    cbTn581GainCh1.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbTn581GainCh0.Items) {
                if (item.Value == (value & 0xC0)) {
                    cbTn581GainCh0.SelectedItem = item;
                }
            }
        }

        private void TiaTn581ParserAddr0x17(byte value)
        {
            foreach (ComboboxItem item in cbTn581LfcCh3.Items) {
                if (item.Value == (value & 0xC0) >> 6) {
                    cbTn581LfcCh3.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbTn581LfcCh2.Items) {
                if (item.Value == (value & 0x30) >> 4) {
                    cbTn581LfcCh2.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbTn581LfcCh1.Items) {
                if (item.Value == (value & 0x0C) >> 2) {
                    cbTn581LfcCh1.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbTn581LfcCh0.Items) {
                if (item.Value == (value & 0xC0)) {
                    cbTn581LfcCh0.SelectedItem = item;
                }
            }
        }

        private void TiaTn581ParserAddr0x19(byte value)
        {
            foreach (ComboboxItem item in cbTn581RssiSel.Items) {
                if (item.Value == (value & 0x70) >> 4) {
                    cbTn581RssiSel.SelectedItem = item;
                }
            }

            if ((value & 0x01) != 0)
                cbTn581RssiEnable.Checked = true;
            else
                cbTn581RssiEnable.Checked = false;
        }

        private void TiaTn581ParserAddr0x1B(byte value)
        {
            if ((value & 0x01) != 0)
                cbTn581DpolCh0.Checked = true;
            else
                cbTn581DpolCh0.Checked = false;

            if ((value & 0x02) != 0)
                cbTn581DpolCh1.Checked = true;
            else
                cbTn581DpolCh1.Checked = false;

            if ((value & 0x04) != 0)
                cbTn581DpolCh2.Checked = true;
            else
                cbTn581DpolCh2.Checked = false;

            if ((value & 0x08) != 0)
                cbTn581DpolCh3.Checked = true;
            else
                cbTn581DpolCh3.Checked = false;
        }

        private void cbMoniter_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMoniter.Checked == true) {
                if (cbSerialPortConnected.Checked == false) {
                    MessageBox.Show("Please connect serial port first!!");
                    return;
                }
                if (bwMonitor.IsBusy == true) {
                    MessageBox.Show("Need wait monitor thread stop!!");
                    cbMoniter.Checked = false;
                    return;
                }
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
                while (cvNtc3Temperature.Count() > 0)
                    cvNtc3Temperature.RemoveAt(0);
                while (cvNtc3TemperatureMax.Count() > 0)
                    cvNtc3TemperatureMax.RemoveAt(0);
                while (cvNtc3TemperatureMin.Count() > 0)
                    cvNtc3TemperatureMin.RemoveAt(0);
                while (cvTiaRssi.Count() > 0)
                    cvTiaRssi.RemoveAt(0);
                while (cvTiaRssiMax.Count() > 0)
                    cvTiaRssiMax.RemoveAt(0);
                while (cvTiaRssiMin.Count() > 0)
                    cvTiaRssiMin.RemoveAt(0);
                bLiveDemoRead.Enabled = false;
                bLiveDemoWrite.Enabled = false;
                bLiveDemoSave.Enabled = false;
                monitorStart = true;
                bwMonitor.RunWorkerAsync();
            }
            else {
                if (monitorStart == false)
                    return;

                monitorStart = false;
            }
        }

        private void bLiveDemoWrite_Click(object sender, EventArgs e)
        {
            String buf;
            byte[] bATmp;
            float fTmp;
            UInt16 ui16Tmp;
            sbyte sbTmp;

            if (cbSerialPortConnected.Checked == false) {
                MessageBox.Show("Please connect serial port first!!");
                return;
            }

            bLiveDemoWrite.Enabled = false;
            bLiveDemoWrite.Text = "Writing";
            bLiveDemoWrite.Update();

            try {
                bATmp = new byte[2];

                buf = "w 0x7F 0x00\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "w 0x7B 0x33\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "w 0x7C 0x32\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "w 0x7D 0x33\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "w 0x7E 0x34\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                
                if (UInt16.TryParse(tbNtc1Beta.Text, out ui16Tmp) == false) {
                    MessageBox.Show("UInt16.TryParse(" + tbNtc1Beta.Text + ") false!!");
                    goto exit;
                }
                bATmp = BitConverter.GetBytes(ui16Tmp);
                buf = "w 0x80 0x" + bATmp[0].ToString("X2") + "\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "w 0x81 0x" + bATmp[1].ToString("X2") + "\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);

                if (UInt16.TryParse(tbNtc2Beta.Text, out ui16Tmp) == false) {
                    MessageBox.Show("UInt16.TryParse(" + tbNtc2Beta.Text + ") false!!");
                    goto exit;
                }
                bATmp = BitConverter.GetBytes(ui16Tmp);
                buf = "w 0x82 0x" + bATmp[0].ToString("X2") + "\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "w 0x83 0x" + bATmp[1].ToString("X2") + "\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);

                if (UInt16.TryParse(tbNtc3Beta.Text, out ui16Tmp) == false) {
                    MessageBox.Show("UInt16.TryParse(" + tbNtc3Beta.Text + ") false!!");
                    goto exit;
                }
                bATmp = BitConverter.GetBytes(ui16Tmp);
                buf = "w 0x84 0x" + bATmp[0].ToString("X2") + "\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "w 0x85 0x" + bATmp[1].ToString("X2") + "\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);

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
                buf = "w 0x86 0x" + sbTmp.ToString("X2") + "\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);

                if (float.TryParse(tbNtc2TemperatureOffset.Text, out fTmp) == false) {
                    MessageBox.Show("float.TryParse(" + tbNtc2TemperatureOffset.Text + ") false!!");
                    goto exit;
                }
                fTmp *= 10;
                if ((fTmp > 127) || (fTmp < -128)) {
                    MessageBox.Show("NTC2 temperature offset out of range (12.7 ~ -12.8): " + tbNtc1TemperatureOffset.Text);
                    goto exit;
                }
                sbTmp = (sbyte)fTmp;
                buf = "w 0x87 0x" + sbTmp.ToString("X2") + "\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);

                if (float.TryParse(tbNtc3TemperatureOffset.Text, out fTmp) == false) {
                    MessageBox.Show("float.TryParse(" + tbNtc3TemperatureOffset.Text + ") false!!");
                    goto exit;
                }
                fTmp *= 10;
                if ((fTmp > 127) || (fTmp < -128)) {
                    MessageBox.Show("NTC3 temperature offset out of range (12.7 ~ -12.8): " + tbNtc1TemperatureOffset.Text);
                    goto exit;
                }
                sbTmp = (sbyte)fTmp;
                buf = "w 0x88 0x" + sbTmp.ToString("X2") + "\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
            }
            catch (IOException ioE) {
                MessageBox.Show(ioE.ToString());
            }
        exit:
            bLiveDemoWrite.Text = "Write";
            bLiveDemoWrite.Enabled = true;
        }

        private void bLiveDemoSave_Click(object sender, EventArgs e)
        {
            String buf;
            byte[] bATmp;

            if (cbSerialPortConnected.Checked == false) {
                MessageBox.Show("Please connect serial port first!!");
                return;
            }

            bLiveDemoSave.Enabled = false;
            bLiveDemoSave.Text = "Saving";
            bLiveDemoSave.Update();

            try {
                bATmp = new byte[2];

                buf = "w 0x7F 0xAA\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "w 0x7B 0x33\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "w 0x7C 0x32\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "w 0x7D 0x33\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "w 0x7E 0x34\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);

                buf = "w 0xA2 0xAA\r";
                serialPort.Write(buf);
                Thread.Sleep(1000);
            }
            catch (IOException ioE) {
                MessageBox.Show(ioE.ToString());
            }
            bLiveDemoSave.Text = "Save";
            bLiveDemoSave.Enabled = true;
        }

        private void tcFunctionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tcFunctionSelect.SelectedTab.Text) {
                case "Live DEMO":
                    break;

                case "TIA TN581":
                case "Console Test":
                    if (monitorStart == true)
                        monitorStart = false;
                    break;

                default:
                    MessageBox.Show("DisplayRxData() unhandle tcFunctionSelect.SelectedTab.Text: " +
                        tcFunctionSelect.SelectedTab.Text + " Error!!");
                    break;
            }
        }

        private void bConsoleLogClear_Click(object sender, EventArgs e)
        {
            tbConsoleRx.Text = "";
        }

        private void TiaTn581ParserAddr0x21(byte value)
        {
            tbTn581ChipNameB0.Text = "0x" + value.ToString("X2");
        }

        private void TiaTn581ParserAddr0x22(byte value)
        {
            tbTn581ChipNameB1.Text = "0x" + value.ToString("X2");
        }

        private void TiaTn581ParserAddr0x23(byte value)
        {
            tbTn581ChipRevision.Text = "0x" + value.ToString("X2");
        }
        private void TiaTn581ParserAddr0x24(byte value)
        {
            if ((value & 0x01) != 0)
                cbTn581I2cSlaRstMstNack.Checked = true;
            else
                cbTn581I2cSlaRstMstNack.Checked = false;
        }

        private void TiaTn581HandleRxData(string rxData)
        {
            string[] parts;
            byte addr, value;

            if ((rxData == null) || (rxData.Length < 2))
                return;

            parts = rxData.Split(' ');

            if (parts.Length > 1 && parts[0] == "w")
                return;

            if (parts.Length == 3 && parts[1].StartsWith("0x") && parts[2].StartsWith("0x")) {
                addr = Convert.ToByte(parts[1].Substring(2, 2), 16); // 去掉 "0x" 取2位並轉換
                value = Convert.ToByte(parts[2].Substring(2, 2), 16);

                switch (addr - 0x80) {
                    case 0x00:
                        TiaTn581ParserAddr0x00(value);
                        return;

                    case 0x02:
                        TiaTn581ParserAddr0x02(value);
                        return;

                    case 0x11:
                        TiaTn581ParserAddr0x11(value);
                        return;

                    case 0x14:
                        TiaTn581ParserAddr0x14(value);
                        return;

                    case 0x17:
                        TiaTn581ParserAddr0x17(value);
                        return;

                    case 0x19:
                        TiaTn581ParserAddr0x19(value);
                        return;

                    case 0x1B:
                        TiaTn581ParserAddr0x1B(value);
                        return;

                    case 0x21:
                        TiaTn581ParserAddr0x21(value);
                        return;

                    case 0x22:
                        TiaTn581ParserAddr0x22(value);
                        return;

                    case 0x23:
                        TiaTn581ParserAddr0x23(value);
                        return;

                    case 0x24:
                        TiaTn581ParserAddr0x24(value);
                        return;

                    default:
                        MessageBox.Show("Unhandle addr: " + parts[1] + " Error!!");
                        break;
                }
            }
            
            MessageBox.Show("Parser error!!\n" + 
                    "rxData: " + rxData);
        }

        private void DisplayRxData(string data)
        {
            if (monitorStart == true) {
                LiveDemoHandleRxData(data);
                return;
            }

            switch (tcFunctionSelect.SelectedTab.Text) {
                case "Live DEMO":
                    LiveDemoHandleRxData(data);
                    break;

                case "TIA TN581":
                    TiaTn581HandleRxData(data);
                    tbConsoleRx.AppendText(data + Environment.NewLine);
                    break;

                case "Console Test":
                    tbConsoleRx.AppendText(data + Environment.NewLine);
                    break;         

                default:
                    MessageBox.Show("DisplayRxData() unhandle tcFunctionSelect.SelectedTab.Text: " + 
                        tcFunctionSelect.SelectedTab.Text + " Error!!");
                    break;
            }
        }

        private void bTn581Read_Click(object sender, EventArgs e)
        {
            String buf;

            if (cbSerialPortConnected.Checked == false) {
                MessageBox.Show("Please connect serial port first!!");
                return;
            }

            bTn581Read.Enabled = false;

            try {
                buf = "w 0x7F 0x04\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandChangePageDelay);
                buf = "r 0x80\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0x82\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0x91\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0x94\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0x97\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0x99\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0x9b\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0xa1\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0xa2\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0xa3\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
                buf = "r 0xa4\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandDelay);
            }
            catch (IOException ioE) {
                MessageBox.Show(ioE.ToString());
            }

            bTn581Read.Enabled = true;
        }

        private void bTn581Save_Click(object sender, EventArgs e)
        {
            String buf;

            if (cbSerialPortConnected.Checked == false) {
                MessageBox.Show("Please connect serial port first!!");
                return;
            }

            bTn581Save.Enabled = false;

            try {
                buf = "w 0x7F 0x04\r";
                serialPort.Write(buf);
                Thread.Sleep(monitorCommandChangePageDelay);
                buf = "w 0xFF 0x55\r";
                serialPort.Write(buf);
                Thread.Sleep(1000);
            }
            catch (IOException ioE) {
                MessageBox.Show(ioE.ToString());
            }

            bTn581Save.Enabled = true;
        }
    }
}
