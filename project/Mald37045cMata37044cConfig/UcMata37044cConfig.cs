using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Mald37045cMata37044c
{
    public partial class UcMata37044cConfig : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private const byte devAddr = 11;
        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private bool reading = false;

        public UcMata37044cConfig()
        {
            ComboboxItem item;
            double dTmp;
            int i;

            InitializeComponent();

            item = new ComboboxItem();
            item.Text = "0:Ch0";
            item.Value = 0;
            cbRssiChannelSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Ch1";
            item.Value = 1;
            cbRssiChannelSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:Ch2";
            item.Value = 2;
            cbRssiChannelSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:Ch3";
            item.Value = 3;
            cbRssiChannelSelect.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:F/8";
            item.Value = 0;
            cbDividedClockOutput.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:F/16";
            item.Value = 1;
            cbDividedClockOutput.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:Active High";
            item.Value = 0;
            cbInterruptPol.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Active Low";
            item.Value = 1;
            cbInterruptPol.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:Open-Drain (Active Low Only)";
            item.Value = 0;
            cbInterruptOutputType.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:CMOS (Active High/Low)";
            item.Value = 1;
            cbInterruptOutputType.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:40MHz";
            item.Value = 0;
            cbCdrLbw.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:30MHz";
            item.Value = 1;
            cbCdrLbw.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:20MHz";
            item.Value = 2;
            cbCdrLbw.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:10MHz";
            item.Value = 3;
            cbCdrLbw.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:80MHz";
            item.Value = 4;
            cbCdrLbw.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:70MHz";
            item.Value = 5;
            cbCdrLbw.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:60MHz";
            item.Value = 6;
            cbCdrLbw.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:50MHz";
            item.Value = 7;
            cbCdrLbw.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:Inc ACK only";
            item.Value = 0;
            cbI2cAddressMode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Inc ACK/NACK";
            item.Value = 1;
            cbI2cAddressMode.Items.Add(item);

            for (i = 0, dTmp = 4; i < 64; i++, dTmp+=8) {
                item = new ComboboxItem();
                if (i < 63)
                    item.Text = i + ":" + dTmp.ToString() + "uA";
                else
                    item.Text = i + ":504uA";
                item.Value = i;
                cbRssi.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0:-11.5%";
            item.Value = 0;
            cbAdcSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:-8%";
            item.Value = 1;
            cbAdcSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:-4.5%";
            item.Value = 2;
            cbAdcSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:-1%";
            item.Value = 3;
            cbAdcSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:2.5%";
            item.Value = 4;
            cbAdcSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:6%";
            item.Value = 5;
            cbAdcSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:9.5%";
            item.Value = 6;
            cbAdcSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:12%";
            item.Value = 7;
            cbAdcSelect.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:260uA";
            item.Value = 0;
            cbTiaAgcThreshold.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:310uA";
            item.Value = 1;
            cbTiaAgcThreshold.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:370uA";
            item.Value = 2;
            cbTiaAgcThreshold.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:440uA";
            item.Value = 3;
            cbTiaAgcThreshold.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:Low";
            item.Value = 0;
            cbCdrLockThreshold.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Recommended";
            item.Value = 1;
            cbCdrLockThreshold.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:Minimum";
            item.Value = 2;
            cbCdrLockThreshold.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:Medium";
            item.Value = 3;
            cbCdrLockThreshold.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:26G";
            item.Value = 0;
            cbRateSelectL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:28G";
            item.Value = 1;
            cbRateSelectL0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:26G";
            item.Value = 0;
            cbRateSelectL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:28G";
            item.Value = 1;
            cbRateSelectL1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:26G";
            item.Value = 0;
            cbRateSelectL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:28G";
            item.Value = 1;
            cbRateSelectL2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:26G";
            item.Value = 0;
            cbRateSelectL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:28G";
            item.Value = 1;
            cbRateSelectL3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:26G/28G";
            item.Value = 0;
            cbTiaRateL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Low Rate";
            item.Value = 1;
            cbTiaRateL0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:26G/28G";
            item.Value = 0;
            cbTiaRateL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Low Rate";
            item.Value = 1;
            cbTiaRateL1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:26G/28G";
            item.Value = 0;
            cbTiaRateL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Low Rate";
            item.Value = 1;
            cbTiaRateL2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:26G/28G";
            item.Value = 0;
            cbTiaRateL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Low Rate";
            item.Value = 1;
            cbTiaRateL3.Items.Add(item);

            for (i = 0, dTmp = 10; i < 4; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "uApp";
                item.Value = i;
                cbLosThresholdL0.Items.Add(item);
            }

            for (i = 0, dTmp = 10; i < 4; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "uApp";
                item.Value = i;
                cbLosThresholdL1.Items.Add(item);
            }

            for (i = 0, dTmp = 10; i < 4; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "uApp";
                item.Value = i;
                cbLosThresholdL2.Items.Add(item);
            }

            for (i = 0, dTmp = 10; i < 4; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "uApp";
                item.Value = i;
                cbLosThresholdL3.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0:~1.5dB";
            item.Value = 0;
            cbLosHysteresisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:~2.5dB";
            item.Value = 1;
            cbLosHysteresisL0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:~1.5dB";
            item.Value = 0;
            cbLosHysteresisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:~2.5dB";
            item.Value = 1;
            cbLosHysteresisL1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:~1.5dB";
            item.Value = 0;
            cbLosHysteresisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:~2.5dB";
            item.Value = 1;
            cbLosHysteresisL2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:~1.5dB";
            item.Value = 0;
            cbLosHysteresisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:~2.5dB";
            item.Value = 1;
            cbLosHysteresisL3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:LA";
            item.Value = 0;
            cbOffsetCorrectionSelectL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:TIA";
            item.Value = 1;
            cbOffsetCorrectionSelectL0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:LA";
            item.Value = 0;
            cbOffsetCorrectionSelectL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:TIA";
            item.Value = 1;
            cbOffsetCorrectionSelectL1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:LA";
            item.Value = 0;
            cbOffsetCorrectionSelectL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:TIA";
            item.Value = 1;
            cbOffsetCorrectionSelectL2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:LA";
            item.Value = 0;
            cbOffsetCorrectionSelectL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:TIA";
            item.Value = 1;
            cbOffsetCorrectionSelectL3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:-14.9 or 15.1uA";
            item.Value = 0;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:-14 or 14.2uA";
            item.Value = 1;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:-13.1 or 13.2uA";
            item.Value = 2;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:-12.1 or 12.2uA";
            item.Value = 3;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:-11.2 or 11.2uA";
            item.Value = 4;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:-10.3 or 10.2uA";
            item.Value = 5;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:-9.3 or 9.3uA";
            item.Value = 6;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:-8.4 or 8.3uA";
            item.Value = 7;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:-7.5 or 7.3uA";
            item.Value = 8;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:-6.5 or 6.3uA";
            item.Value = 6;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:-5.6 or 5.4uA";
            item.Value = 10;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:-4.7 or 4.4uA";
            item.Value = 11;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:-3.7 or 3.4uA";
            item.Value = 12;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:-2.8 or 2.4uA";
            item.Value = 13;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:-1.9 or 1.5uA";
            item.Value = 14;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:-0.9 or 0.5uA";
            item.Value = 15;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "16:0.9 or -0.5uA";
            item.Value = 16;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "17:1.9 or -1.4uA";
            item.Value = 17;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "18:2.8 or -2.4uA";
            item.Value = 18;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "19:3.7 or -3.4uA";
            item.Value = 19;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "20:4.6 or -4.3uA";
            item.Value = 20;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "21:5.6 or -5.3uA";
            item.Value = 21;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "22:6.5 or -6.3uA";
            item.Value = 22;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "23:7.5 or -7.3uA";
            item.Value = 23;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "24:8.4 or -8.2uA";
            item.Value = 24;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "25:9.3 or -9.2uA";
            item.Value = 25;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "26:10.3 or -10.2uA";
            item.Value = 26;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "27:11.2 or -11.2uA";
            item.Value = 27;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "28:12.1 or -12.2uA";
            item.Value = 28;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "29:13.1 or -13.1uA";
            item.Value = 29;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "30:14 or -14.1uA";
            item.Value = 30;
            cbSlaL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "31:14.9 or -15.1uA";
            item.Value = 31;
            cbSlaL0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:-14.9 or 15.1uA";
            item.Value = 0;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:-14 or 14.2uA";
            item.Value = 1;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:-13.1 or 13.2uA";
            item.Value = 2;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:-12.1 or 12.2uA";
            item.Value = 3;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:-11.2 or 11.2uA";
            item.Value = 4;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:-10.3 or 10.2uA";
            item.Value = 5;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:-9.3 or 9.3uA";
            item.Value = 6;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:-8.4 or 8.3uA";
            item.Value = 7;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:-7.5 or 7.3uA";
            item.Value = 8;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:-6.5 or 6.3uA";
            item.Value = 6;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:-5.6 or 5.4uA";
            item.Value = 10;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:-4.7 or 4.4uA";
            item.Value = 11;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:-3.7 or 3.4uA";
            item.Value = 12;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:-2.8 or 2.4uA";
            item.Value = 13;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:-1.9 or 1.5uA";
            item.Value = 14;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:-0.9 or 0.5uA";
            item.Value = 15;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "16:0.9 or -0.5uA";
            item.Value = 16;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "17:1.9 or -1.4uA";
            item.Value = 17;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "18:2.8 or -2.4uA";
            item.Value = 18;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "19:3.7 or -3.4uA";
            item.Value = 19;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "20:4.6 or -4.3uA";
            item.Value = 20;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "21:5.6 or -5.3uA";
            item.Value = 21;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "22:6.5 or -6.3uA";
            item.Value = 22;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "23:7.5 or -7.3uA";
            item.Value = 23;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "24:8.4 or -8.2uA";
            item.Value = 24;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "25:9.3 or -9.2uA";
            item.Value = 25;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "26:10.3 or -10.2uA";
            item.Value = 26;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "27:11.2 or -11.2uA";
            item.Value = 27;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "28:12.1 or -12.2uA";
            item.Value = 28;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "29:13.1 or -13.1uA";
            item.Value = 29;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "30:14 or -14.1uA";
            item.Value = 30;
            cbSlaL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "31:14.9 or -15.1uA";
            item.Value = 31;
            cbSlaL1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:-14.9 or 15.1uA";
            item.Value = 0;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:-14 or 14.2uA";
            item.Value = 1;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:-13.1 or 13.2uA";
            item.Value = 2;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:-12.1 or 12.2uA";
            item.Value = 3;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:-11.2 or 11.2uA";
            item.Value = 4;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:-10.3 or 10.2uA";
            item.Value = 5;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:-9.3 or 9.3uA";
            item.Value = 6;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:-8.4 or 8.3uA";
            item.Value = 7;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:-7.5 or 7.3uA";
            item.Value = 8;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:-6.5 or 6.3uA";
            item.Value = 6;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:-5.6 or 5.4uA";
            item.Value = 10;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:-4.7 or 4.4uA";
            item.Value = 11;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:-3.7 or 3.4uA";
            item.Value = 12;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:-2.8 or 2.4uA";
            item.Value = 13;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:-1.9 or 1.5uA";
            item.Value = 14;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:-0.9 or 0.5uA";
            item.Value = 15;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "16:0.9 or -0.5uA";
            item.Value = 16;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "17:1.9 or -1.4uA";
            item.Value = 17;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "18:2.8 or -2.4uA";
            item.Value = 18;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "19:3.7 or -3.4uA";
            item.Value = 19;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "20:4.6 or -4.3uA";
            item.Value = 20;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "21:5.6 or -5.3uA";
            item.Value = 21;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "22:6.5 or -6.3uA";
            item.Value = 22;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "23:7.5 or -7.3uA";
            item.Value = 23;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "24:8.4 or -8.2uA";
            item.Value = 24;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "25:9.3 or -9.2uA";
            item.Value = 25;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "26:10.3 or -10.2uA";
            item.Value = 26;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "27:11.2 or -11.2uA";
            item.Value = 27;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "28:12.1 or -12.2uA";
            item.Value = 28;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "29:13.1 or -13.1uA";
            item.Value = 29;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "30:14 or -14.1uA";
            item.Value = 30;
            cbSlaL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "31:14.9 or -15.1uA";
            item.Value = 31;
            cbSlaL2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:-14.9 or 15.1uA";
            item.Value = 0;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:-14 or 14.2uA";
            item.Value = 1;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:-13.1 or 13.2uA";
            item.Value = 2;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:-12.1 or 12.2uA";
            item.Value = 3;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:-11.2 or 11.2uA";
            item.Value = 4;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:-10.3 or 10.2uA";
            item.Value = 5;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:-9.3 or 9.3uA";
            item.Value = 6;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:-8.4 or 8.3uA";
            item.Value = 7;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:-7.5 or 7.3uA";
            item.Value = 8;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:-6.5 or 6.3uA";
            item.Value = 6;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:-5.6 or 5.4uA";
            item.Value = 10;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:-4.7 or 4.4uA";
            item.Value = 11;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:-3.7 or 3.4uA";
            item.Value = 12;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:-2.8 or 2.4uA";
            item.Value = 13;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:-1.9 or 1.5uA";
            item.Value = 14;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:-0.9 or 0.5uA";
            item.Value = 15;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "16:0.9 or -0.5uA";
            item.Value = 16;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "17:1.9 or -1.4uA";
            item.Value = 17;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "18:2.8 or -2.4uA";
            item.Value = 18;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "19:3.7 or -3.4uA";
            item.Value = 19;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "20:4.6 or -4.3uA";
            item.Value = 20;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "21:5.6 or -5.3uA";
            item.Value = 21;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "22:6.5 or -6.3uA";
            item.Value = 22;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "23:7.5 or -7.3uA";
            item.Value = 23;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "24:8.4 or -8.2uA";
            item.Value = 24;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "25:9.3 or -9.2uA";
            item.Value = 25;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "26:10.3 or -10.2uA";
            item.Value = 26;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "27:11.2 or -11.2uA";
            item.Value = 27;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "28:12.1 or -12.2uA";
            item.Value = 28;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "29:13.1 or -13.1uA";
            item.Value = 29;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "30:14 or -14.1uA";
            item.Value = 30;
            cbSlaL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "31:14.9 or -15.1uA";
            item.Value = 31;
            cbSlaL3.Items.Add(item);

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbOutputSwingL0.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbOutputSwingL1.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbOutputSwingL2.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbOutputSwingL3.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbOutputDeEmphasisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbOutputDeEmphasisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbOutputDeEmphasisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbOutputDeEmphasisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbOutputDeEmphasisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbOutputDeEmphasisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbOutputDeEmphasisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbOutputDeEmphasisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbOutputDeEmphasisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbOutputDeEmphasisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbOutputDeEmphasisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbOutputDeEmphasisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbOutputDeEmphasisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbOutputDeEmphasisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbOutputDeEmphasisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbOutputDeEmphasisL0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbOutputDeEmphasisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbOutputDeEmphasisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbOutputDeEmphasisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbOutputDeEmphasisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbOutputDeEmphasisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbOutputDeEmphasisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbOutputDeEmphasisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbOutputDeEmphasisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbOutputDeEmphasisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbOutputDeEmphasisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbOutputDeEmphasisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbOutputDeEmphasisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbOutputDeEmphasisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbOutputDeEmphasisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbOutputDeEmphasisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbOutputDeEmphasisL1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbOutputDeEmphasisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbOutputDeEmphasisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbOutputDeEmphasisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbOutputDeEmphasisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbOutputDeEmphasisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbOutputDeEmphasisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbOutputDeEmphasisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbOutputDeEmphasisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbOutputDeEmphasisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbOutputDeEmphasisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbOutputDeEmphasisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbOutputDeEmphasisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbOutputDeEmphasisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbOutputDeEmphasisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbOutputDeEmphasisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbOutputDeEmphasisL2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbOutputDeEmphasisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbOutputDeEmphasisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbOutputDeEmphasisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbOutputDeEmphasisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbOutputDeEmphasisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbOutputDeEmphasisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbOutputDeEmphasisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbOutputDeEmphasisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbOutputDeEmphasisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbOutputDeEmphasisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbOutputDeEmphasisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbOutputDeEmphasisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbOutputDeEmphasisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbOutputDeEmphasisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbOutputDeEmphasisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbOutputDeEmphasisL3.Items.Add(item);
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

        private void _ParseAddr00(byte data)
        {
            tbChipId.Text = "0x" + data.ToString("X2");
        }

        private void _ParseAddr01(byte data)
        {
            tbRevId.Text = "0x" + data.ToString("X2");
        }

        private void _ParseAddr03(byte data)
        {
            if ((data & 0x80) == 0)
                cbRssiAdcEn.Checked = false;
            else
                cbRssiAdcEn.Checked = true;

            if ((data & 0x40) == 0)
                cbRssiEnable.Checked = false;
            else
                cbRssiEnable.Checked = true;

            foreach (ComboboxItem item in cbRssiChannelSelect.Items) {
                if (item.Value == ((data & 0x30) >> 4)) {
                    cbRssiChannelSelect.SelectedItem = item;
                }
            }

            if ((data & 0x08) == 0)
                cbEnableDividedClock.Checked = false;
            else
                cbEnableDividedClock.Checked = true;

            if ((data & 0x04) == 0)
                cbDividedClockOutput.SelectedItem = 0;
            else
                cbDividedClockOutput.SelectedItem = 1;

            if ((data & 0x02) == 0)
                cbInterruptPol.SelectedItem = 0;
            else
                cbInterruptPol.SelectedItem = 1;

            if ((data & 0x01) == 0)
                cbInterruptOutputType.SelectedItem = 0;
            else
                cbInterruptOutputType.SelectedItem = 1;
        }

        private void _ParseAddr04(byte data)
        {
            foreach (ComboboxItem item in cbCdrLbw.Items) {
                if (item.Value == (data & 0x07)) {
                    cbCdrLbw.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr05(byte data)
        {
            if ((data & 0x01) == 0)
                cbI2cAddressMode.SelectedIndex = 0;
            else
                cbI2cAddressMode.SelectedIndex = 1;
        }

        private void _ParseAddr10(byte data)
        {
            if ((data & 0x80) == 0)
                cbPowerDownChannelL3.Checked = false;
            else
                cbPowerDownChannelL3.Checked = true;

            if ((data & 0x40) == 0)
                cbPowerDownChannelL2.Checked = false;
            else
                cbPowerDownChannelL2.Checked = true;

            if ((data & 0x20) == 0)
                cbPowerDownChannelL1.Checked = false;
            else
                cbPowerDownChannelL1.Checked = true;

            if ((data & 0x10) == 0)
                cbPowerDownChannelL0.Checked = false;
            else
                cbPowerDownChannelL0.Checked = true;

            if ((data & 0x08) == 0)
                cbCdrBypassL3.Checked = false;
            else
                cbCdrBypassL3.Checked = true;

            if ((data & 0x04) == 0)
                cbCdrBypassL2.Checked = false;
            else
                cbCdrBypassL2.Checked = true;

            if ((data & 0x02) == 0)
                cbCdrBypassL1.Checked = false;
            else
                cbCdrBypassL1.Checked = true;

            if ((data & 0x01) == 0)
                cbCdrBypassL0.Checked = false;
            else
                cbCdrBypassL0.Checked = true;
        }

        private void _ParseAddr11(byte data)
        {
            if ((data & 0x80) == 0)
                cbRateSelectL3.SelectedIndex = 0;
            else
                cbRateSelectL3.SelectedIndex = 1;

            if ((data & 0x40) == 0)
                cbRateSelectL2.SelectedIndex = 0;
            else
                cbRateSelectL2.SelectedIndex = 1;

            if ((data & 0x20) == 0)
                cbRateSelectL1.SelectedIndex = 0;
            else
                cbRateSelectL1.SelectedIndex = 1;

            if ((data & 0x10) == 0)
                cbRateSelectL0.SelectedIndex = 0;
            else
                cbRateSelectL0.SelectedIndex = 1;
        }

        private void _ParseAddr16(byte data)
        {
            if ((data & 0x80) == 0)
                cbLolMaskL3.Checked = false;
            else
                cbLolMaskL3.Checked = true;

            if ((data & 0x40) == 0)
                cbLolMaskL2.Checked = false;
            else
                cbLolMaskL2.Checked = true;

            if ((data & 0x20) == 0)
                cbLolMaskL1.Checked = false;
            else
                cbLolMaskL1.Checked = true;

            if ((data & 0x10) == 0)
                cbLolMaskL0.Checked = false;
            else
                cbLolMaskL0.Checked = true;

            if ((data & 0x08) == 0)
                cbLosMaskL3.Checked = false;
            else
                cbLosMaskL3.Checked = true;

            if ((data & 0x04) == 0)
                cbLosMaskL2.Checked = false;
            else
                cbLosMaskL2.Checked = true;

            if ((data & 0x02) == 0)
                cbLosMaskL1.Checked = false;
            else
                cbLosMaskL1.Checked = true;

            if ((data & 0x01) == 0)
                cbLosMaskL0.Checked = false;
            else
                cbLosMaskL0.Checked = true;
        }

        private void _ParseAddr17(byte data)
        {
            if ((data & 0x80) == 0)
                cbLolL3.Checked = false;
            else
                cbLolL3.Checked = true;

            if ((data & 0x40) == 0)
                cbLolL2.Checked = false;
            else
                cbLolL2.Checked = true;

            if ((data & 0x20) == 0)
                cbLolL1.Checked = false;
            else
                cbLolL1.Checked = true;

            if ((data & 0x10) == 0)
                cbLolL0.Checked = false;
            else
                cbLolL0.Checked = true;

            if ((data & 0x08) == 0)
                cbLosL3.Checked = false;
            else
                cbLosL3.Checked = true;

            if ((data & 0x04) == 0)
                cbLosL2.Checked = false;
            else
                cbLosL2.Checked = true;

            if ((data & 0x02) == 0)
                cbLosL1.Checked = false;
            else
                cbLosL1.Checked = true;

            if ((data & 0x01) == 0)
                cbLosL0.Checked = false;
            else
                cbLosL0.Checked = true;
        }

        private void _ParseAddr18(byte data)
        {
            if ((data & 0x80) == 0)
                cbLolOrLosL3.Checked = false;
            else
                cbLolOrLosL3.Checked = true;

            if ((data & 0x40) == 0)
                cbLolOrLosL2.Checked = false;
            else
                cbLolOrLosL2.Checked = true;

            if ((data & 0x20) == 0)
                cbLolOrLosL1.Checked = false;
            else
                cbLolOrLosL1.Checked = true;

            if ((data & 0x10) == 0)
                cbLolOrLosL0.Checked = false;
            else
                cbLolOrLosL0.Checked = true;

            if ((data & 0x08) == 0)
                cbLosAssertedL3.Checked = false;
            else
                cbLosAssertedL3.Checked = true;

            if ((data & 0x04) == 0)
                cbLosAssertedL2.Checked = false;
            else
                cbLosAssertedL2.Checked = true;

            if ((data & 0x02) == 0)
                cbLosAssertedL1.Checked = false;
            else
                cbLosAssertedL1.Checked = true;

            if ((data & 0x01) == 0)
                cbLosAssertedL0.Checked = false;
            else
                cbLosAssertedL0.Checked = true;
        }

        private void _ParseAddr19(byte data)
        {
            if ((data & 0x80) == 0)
                cbLolAlarmL3.Checked = false;
            else
                cbLolAlarmL3.Checked = true;

            if ((data & 0x40) == 0)
                cbLolAlarmL2.Checked = false;
            else
                cbLolAlarmL2.Checked = true;

            if ((data & 0x20) == 0)
                cbLolAlarmL1.Checked = false;
            else
                cbLolAlarmL1.Checked = true;

            if ((data & 0x10) == 0)
                cbLolAlarmL0.Checked = false;
            else
                cbLolAlarmL0.Checked = true;

            if ((data & 0x08) == 0)
                cbLosAlarmL3.Checked = false;
            else
                cbLosAlarmL3.Checked = true;

            if ((data & 0x04) == 0)
                cbLosAlarmL2.Checked = false;
            else
                cbLosAlarmL2.Checked = true;

            if ((data & 0x02) == 0)
                cbLosAlarmL1.Checked = false;
            else
                cbLosAlarmL1.Checked = true;

            if ((data & 0x01) == 0)
                cbLosAlarmL0.Checked = false;
            else
                cbLosAlarmL0.Checked = true;
        }

        private void _ParseAddr1A(byte data)
        {
            foreach (ComboboxItem item in cbRssi.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbRssi.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr1B(byte data)
        {
            foreach (ComboboxItem item in cbAdcSelect.Items) {
                if (item.Value == (data & 0x07)) {
                    cbAdcSelect.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr20(byte data)
        {
            if ((data & 0x08) == 0)
                cbTiaRateL3.SelectedIndex = 0;
            else
                cbTiaRateL3.SelectedIndex = 1;

            if ((data & 0x04) == 0)
                cbTiaRateL2.SelectedIndex = 0;
            else
                cbTiaRateL2.SelectedIndex = 1;

            if ((data & 0x02) == 0)
                cbTiaRateL1.SelectedIndex = 0;
            else
                cbTiaRateL1.SelectedIndex = 1;

            if ((data & 0x01) == 0)
                cbTiaRateL0.SelectedIndex = 0;
            else
                cbTiaRateL0.SelectedIndex = 1;
        }

        private void _ParseAddr21(byte data)
        {
            foreach (ComboboxItem item in cbLosThresholdL3.Items) {
                if (item.Value == ((data & 0xC0) >> 6)) {
                    cbLosThresholdL3.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbLosThresholdL2.Items) {
                if (item.Value == ((data & 0x30) >> 4)) {
                    cbLosThresholdL2.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbLosThresholdL1.Items) {
                if (item.Value == ((data & 0x0C) >> 2)) {
                    cbLosThresholdL1.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbLosThresholdL0.Items) {
                if (item.Value == (data & 0x03)) {
                    cbLosThresholdL0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr22(byte data)
        {
            if ((data & 0x80) == 0)
                cbLosHysteresisL3.SelectedIndex = 0;
            else
                cbLosHysteresisL3.SelectedIndex = 1;

            if ((data & 0x40) == 0)
                cbLosHysteresisL2.SelectedIndex = 0;
            else
                cbLosHysteresisL2.SelectedIndex = 1;

            if ((data & 0x20) == 0)
                cbLosHysteresisL1.SelectedIndex = 0;
            else
                cbLosHysteresisL1.SelectedIndex = 1;

            if ((data & 0x10) == 0)
                cbLosHysteresisL0.SelectedIndex = 0;
            else
                cbLosHysteresisL0.SelectedIndex = 1;
        }

        private void _ParseAddr23(byte data)
        {
            if ((data & 0x80) == 0)
                cbInvertPolarityL3.Checked = false;
            else
                cbInvertPolarityL3.Checked = true;

            if ((data & 0x40) == 0)
                cbInvertPolarityL2.Checked = false;
            else
                cbInvertPolarityL2.Checked = true;

            if ((data & 0x20) == 0)
                cbInvertPolarityL1.Checked = false;
            else
                cbInvertPolarityL1.Checked = true;

            if ((data & 0x10) == 0)
                cbInvertPolarityL0.Checked = false;
            else
                cbInvertPolarityL0.Checked = true;
        }

        private void _ParseAddr24(byte data)
        {
            if ((data & 0x80) == 0)
                cbOffsetCorrectionSelectL3.SelectedIndex = 0;
            else
                cbOffsetCorrectionSelectL3.SelectedIndex = 1;

            if ((data & 0x40) == 0)
                cbOffsetCorrectionSelectL2.SelectedIndex = 0;
            else
                cbOffsetCorrectionSelectL2.SelectedIndex = 1;

            if ((data & 0x20) == 0)
                cbOffsetCorrectionSelectL1.SelectedIndex = 0;
            else
                cbOffsetCorrectionSelectL1.SelectedIndex = 1;

            if ((data & 0x10) == 0)
                cbOffsetCorrectionSelectL0.SelectedIndex = 0;
            else
                cbOffsetCorrectionSelectL0.SelectedIndex = 1;

            if ((data & 0x08) == 0)
                cbSlaEnableL3.Checked = false;
            else
                cbSlaEnableL3.Checked = true;

            if ((data & 0x04) == 0)
                cbSlaEnableL2.Checked = false;
            else
                cbSlaEnableL2.Checked = true;

            if ((data & 0x02) == 0)
                cbSlaEnableL1.Checked = false;
            else
                cbSlaEnableL1.Checked = true;

            if ((data & 0x01) == 0)
                cbSlaEnableL0.Checked = false;
            else
                cbSlaEnableL0.Checked = true;
        }

        private void _ParseAddr25(byte data)
        {
            foreach (ComboboxItem item in cbSlaL0.Items) {
                if (item.Value == (data & 0x1F)) {
                    cbSlaL0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr26(byte data)
        {
            foreach (ComboboxItem item in cbSlaL1.Items) {
                if (item.Value == (data & 0x1F)) {
                    cbSlaL1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr27(byte data)
        {
            foreach (ComboboxItem item in cbSlaL2.Items) {
                if (item.Value == (data & 0x1F)) {
                    cbSlaL2.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr28(byte data)
        {
            foreach (ComboboxItem item in cbSlaL3.Items) {
                if (item.Value == (data & 0x1F)) {
                    cbSlaL3.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr2D(byte data)
        {
            foreach (ComboboxItem item in cbTiaAgcThreshold.Items) {
                if (item.Value == ((data & 0x30) >> 4)) {
                    cbTiaAgcThreshold.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr40(byte data)
        {
            if ((data & 0x80) == 0)
                cbDisableAutoMuteL3.Checked = false;
            else
                cbDisableAutoMuteL3.Checked = true;

            if ((data & 0x40) == 0)
                cbDisableAutoMuteL2.Checked = false;
            else
                cbDisableAutoMuteL2.Checked = true;

            if ((data & 0x20) == 0)
                cbDisableAutoMuteL1.Checked = false;
            else
                cbDisableAutoMuteL1.Checked = true;

            if ((data & 0x10) == 0)
                cbDisableAutoMuteL0.Checked = false;
            else
                cbDisableAutoMuteL0.Checked = true;

            if ((data & 0x08) == 0)
                cbMuteForceL3.Checked = false;
            else
                cbMuteForceL3.Checked = true;

            if ((data & 0x04) == 0)
                cbMuteForceL2.Checked = false;
            else
                cbMuteForceL2.Checked = true;

            if ((data & 0x02) == 0)
                cbMuteForceL1.Checked = false;
            else
                cbMuteForceL1.Checked = true;

            if ((data & 0x01) == 0)
                cbMuteForceL0.Checked = false;
            else
                cbMuteForceL0.Checked = true;
        }

        private void _ParseAddr41(byte data)
        {
            if ((data & 0x80) == 0)
                cbSlowSlewL3.Checked = false;
            else
                cbSlowSlewL3.Checked = true;

            if ((data & 0x40) == 0)
                cbSlowSlewL2.Checked = false;
            else
                cbSlowSlewL2.Checked = true;

            if ((data & 0x20) == 0)
                cbSlowSlewL1.Checked = false;
            else
                cbSlowSlewL1.Checked = true;

            if ((data & 0x10) == 0)
                cbSlowSlewL0.Checked = false;
            else
                cbSlowSlewL0.Checked = true;
        }

        private void _ParseAddr42(byte data)
        {
            foreach (ComboboxItem item in cbOutputSwingL0.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbOutputSwingL0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr43(byte data)
        {
            foreach (ComboboxItem item in cbOutputSwingL1.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbOutputSwingL1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr44(byte data)
        {
            foreach (ComboboxItem item in cbOutputSwingL2.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbOutputSwingL2.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr45(byte data)
        {
            foreach (ComboboxItem item in cbOutputSwingL3.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbOutputSwingL3.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr46(byte data)
        {
            foreach (ComboboxItem item in cbOutputDeEmphasisL1.Items) {
                if (item.Value == ((data & 0xF0) >> 4)) {
                    cbOutputDeEmphasisL1.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbOutputDeEmphasisL0.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbOutputDeEmphasisL0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr47(byte data)
        {
            foreach (ComboboxItem item in cbOutputDeEmphasisL3.Items) {
                if (item.Value == ((data & 0xF0) >> 4)) {
                    cbOutputDeEmphasisL3.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbOutputDeEmphasisL2.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbOutputDeEmphasisL2.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr82(byte data)
        {
            foreach (ComboboxItem item in cbCdrLockThreshold.Items) {
                if (item.Value == ((data & 0x30) >> 4)) {
                    cbCdrLockThreshold.SelectedItem = item;
                }
            }
        }

        private void bReadAll_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[11];
            int rv;

            if (reading == true)
                return;

            reading = true;
            bReadAll.Enabled = false;

            if (i2cReadCB == null)
                goto exit;

            rv = i2cReadCB(devAddr, 0x00, 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr00(data[0]);
            _ParseAddr01(data[1]);

            rv = i2cReadCB(devAddr, 0x03, 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr03(data[0]);
            _ParseAddr04(data[1]);
            _ParseAddr05(data[2]);

            rv = i2cReadCB(devAddr, 0x10, 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr10(data[0]);
            _ParseAddr11(data[1]);

            rv = i2cReadCB(devAddr, 0x16, 6, data);
            if (rv != 6)
                goto exit;

            _ParseAddr16(data[0]);
            _ParseAddr17(data[1]);
            _ParseAddr18(data[2]);
            _ParseAddr19(data[3]);
            _ParseAddr1A(data[4]);
            _ParseAddr1B(data[5]);

            rv = i2cReadCB(devAddr, 0x20, 9, data);
            if (rv != 9)
                goto exit;

            _ParseAddr20(data[0]);
            _ParseAddr21(data[1]);
            _ParseAddr22(data[2]);
            _ParseAddr23(data[3]);
            _ParseAddr24(data[4]);
            _ParseAddr25(data[5]);
            _ParseAddr26(data[6]);
            _ParseAddr27(data[7]);
            _ParseAddr28(data[8]);

            rv = i2cReadCB(devAddr, 0x2D, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr2D(data[0]);

            rv = i2cReadCB(devAddr, 0x40, 9, data);
            if (rv != 9)
                goto exit;

            _ParseAddr40(data[0]);
            _ParseAddr41(data[1]);
            _ParseAddr42(data[2]);
            _ParseAddr43(data[3]);
            _ParseAddr44(data[4]);
            _ParseAddr45(data[5]);
            _ParseAddr46(data[6]);
            _ParseAddr47(data[7]);

            rv = i2cReadCB(devAddr, 0x82, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr82(data[0]);

        exit:
            reading = false;
            bReadAll.Enabled = true;
        }

        private int _WriteAddr02()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0xAA;

            rv = i2cWriteCB(devAddr, 0x02, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr03()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (cbRssiAdcEn.Checked == true)
                data[0] |= 0x80;

            if (cbRssiEnable.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbRssiChannelSelect.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            if (cbEnableDividedClock.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbDividedClockOutput.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbInterruptPol.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbInterruptOutputType.SelectedIndex);
            data[0] |= bTmp;
            
            rv = i2cWriteCB(devAddr, 0x03, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr04()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbCdrLbw.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x04, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr05()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (cbI2cAddressMode.SelectedIndex == 1)
                data[0] |= 0x01;

            rv = i2cWriteCB(devAddr, 0x05, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr10()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (cbPowerDownChannelL3.Checked == true)
                data[0] |= 0x80;

            if (cbPowerDownChannelL2.Checked == true)
                data[0] |= 0x40;

            if (cbPowerDownChannelL1.Checked == true)
                data[0] |= 0x20;

            if (cbPowerDownChannelL0.Checked == true)
                data[0] |= 0x10;

            if (cbCdrBypassL3.Checked == true)
                data[0] |= 0x08;

            if (cbCdrBypassL2.Checked == true)
                data[0] |= 0x04;

            if (cbCdrBypassL1.Checked == true)
                data[0] |= 0x02;

            if (cbCdrBypassL0.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(devAddr, 0x10, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr11()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (cbRateSelectL3.SelectedIndex == 1)
                data[0] |= 0x80;

            if (cbRateSelectL2.SelectedIndex == 1)
                data[0] |= 0x40;

            if (cbRateSelectL1.SelectedIndex == 1)
                data[0] |= 0x20;

            if (cbRateSelectL0.SelectedIndex == 1)
                data[0] |= 0x10;

            rv = i2cWriteCB(devAddr, 0x11, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr16()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (cbLolMaskL3.Checked == true)
                data[0] |= 0x80;

            if (cbLolMaskL2.Checked == true)
                data[0] |= 0x40;

            if (cbLolMaskL1.Checked == true)
                data[0] |= 0x20;

            if (cbLolMaskL0.Checked == true)
                data[0] |= 0x10;

            if (cbLosMaskL3.Checked == true)
                data[0] |= 0x08;

            if (cbLosMaskL2.Checked == true)
                data[0] |= 0x04;

            if (cbLosMaskL1.Checked == true)
                data[0] |= 0x02;

            if (cbLosMaskL0.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(devAddr, 0x16, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr19()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (cbLolAlarmL3.Checked == true)
                data[0] |= 0x80;

            if (cbLolAlarmL2.Checked == true)
                data[0] |= 0x40;

            if (cbLolAlarmL1.Checked == true)
                data[0] |= 0x20;

            if (cbLolAlarmL0.Checked == true)
                data[0] |= 0x10;

            if (cbLosAlarmL3.Checked == true)
                data[0] |= 0x08;

            if (cbLosAlarmL2.Checked == true)
                data[0] |= 0x04;

            if (cbLosAlarmL1.Checked == true)
                data[0] |= 0x02;

            if (cbLosAlarmL0.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(devAddr, 0x19, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr1B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbAdcSelect.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x1B, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr20()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbTiaRateL3.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbTiaRateL2.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbTiaRateL1.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbTiaRateL0.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x20, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr21()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbLosThresholdL3.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosThresholdL2.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosThresholdL1.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosThresholdL0.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x21, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr22()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbLosHysteresisL3.SelectedIndex);
            bTmp <<= 7;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosHysteresisL2.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosHysteresisL1.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosHysteresisL0.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x22, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr23()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (cbInvertPolarityL3.Checked == true)
                data[0] |= 0x80;

            if (cbInvertPolarityL2.Checked == true)
                data[0] |= 0x40;

            if (cbInvertPolarityL1.Checked == true)
                data[0] |= 0x20;

            if (cbInvertPolarityL0.Checked == true)
                data[0] |= 0x10;

            rv = i2cWriteCB(devAddr, 0x23, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr24()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOffsetCorrectionSelectL3.SelectedIndex);
            bTmp <<= 7;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbOffsetCorrectionSelectL2.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbOffsetCorrectionSelectL1.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbOffsetCorrectionSelectL0.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            if (cbSlaEnableL3.Checked == true)
                data[0] |= 0x08;

            if (cbSlaEnableL2.Checked == true)
                data[0] |= 0x04;

            if (cbSlaEnableL1.Checked == true)
                data[0] |= 0x02;

            if (cbSlaEnableL0.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(devAddr, 0x24, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr25()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbSlaL0.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x25, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr26()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbSlaL1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x26, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr27()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbSlaL2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x27, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr28()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbSlaL3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x28, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr2D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbTiaAgcThreshold.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x2D, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr40()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (cbDisableAutoMuteL3.Checked == true)
                data[0] |= 0x80;

            if (cbDisableAutoMuteL2.Checked == true)
                data[0] |= 0x40;

            if (cbDisableAutoMuteL1.Checked == true)
                data[0] |= 0x20;

            if (cbDisableAutoMuteL0.Checked == true)
                data[0] |= 0x10;

            if (cbMuteForceL3.Checked == true)
                data[0] |= 0x08;

            if (cbMuteForceL2.Checked == true)
                data[0] |= 0x04;

            if (cbMuteForceL1.Checked == true)
                data[0] |= 0x02;

            if (cbMuteForceL0.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(devAddr, 0x40, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr41()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (cbSlowSlewL3.Checked == true)
                data[0] |= 0x80;

            if (cbSlowSlewL2.Checked == true)
                data[0] |= 0x40;

            if (cbSlowSlewL1.Checked == true)
                data[0] |= 0x20;

            if (cbSlowSlewL0.Checked == true)
                data[0] |= 0x10;

            rv = i2cWriteCB(devAddr, 0x41, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr42()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputSwingL0.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x42, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr43()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputSwingL1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x43, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr44()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputSwingL2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x44, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr45()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputSwingL3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x45, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr46()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputDeEmphasisL1.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbOutputDeEmphasisL0.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x46, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr47()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputDeEmphasisL3.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbOutputDeEmphasisL2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x47, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr82()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbCdrLockThreshold.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x82, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void bDeviceReset_Click(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr02() < 0)
                return;
        }

        private void cbRssiAdcEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr03() < 0)
                return;
        }

        private void cbRssiEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr03() < 0)
                return;
        }

        private void cbRssiChannelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr03() < 0)
                return;
        }

        private void cbEnableDividedClock_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr03() < 0)
                return;
        }

        private void cbDividedClockOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr03() < 0)
                return;
        }

        private void cbInterruptPol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr03() < 0)
                return;
        }

        private void cbInterruptOutputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr03() < 0)
                return;
        }

        private void cbCdrLbw_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr04() < 0)
                return;
        }

        private void cbI2cAddressMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr05() < 0)
                return;
        }

        private void cbPowerDownChannelL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbPowerDownChannelL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbPowerDownChannelL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbPowerDownChannelL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbCdrBypassL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbCdrBypassL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbCdrBypassL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbCdrBypassL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbRateSelectL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr11() < 0)
                return;
        }

        private void cbRateSelectL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr11() < 0)
                return;
        }

        private void cbRateSelectL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr11() < 0)
                return;
        }

        private void cbRateSelectL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr11() < 0)
                return;
        }

        private void cbLolMaskL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr16() < 0)
                return;
        }

        private void cbLolMaskL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr16() < 0)
                return;
        }

        private void cbLolMaskL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr16() < 0)
                return;
        }

        private void cbLolMaskL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr16() < 0)
                return;
        }

        private void cbLosMaskL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr16() < 0)
                return;
        }

        private void cbLosMaskL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr16() < 0)
                return;
        }

        private void cbLosMaskL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr16() < 0)
                return;
        }

        private void cbLosMaskL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr16() < 0)
                return;
        }

        private void cbLolAlarmL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19() < 0)
                return;
        }

        private void cbLolAlarmL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19() < 0)
                return;
        }

        private void cbLolAlarmL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19() < 0)
                return;
        }

        private void cbLolAlarmL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19() < 0)
                return;
        }

        private void cbLosAlarmL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19() < 0)
                return;
        }

        private void cbLosAlarmL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19() < 0)
                return;
        }

        private void cbLosAlarmL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19() < 0)
                return;
        }

        private void cbLosAlarmL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19() < 0)
                return;
        }

        private void cbAdcSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1B() < 0)
                return;
        }

        private void cbTiaRateL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr20() < 0)
                return;
        }

        private void cbTiaRateL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr20() < 0)
                return;
        }

        private void cbTiaRateL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr20() < 0)
                return;
        }

        private void cbTiaRateL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr20() < 0)
                return;
        }

        private void cbLosThresholdL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21() < 0)
                return;
        }

        private void cbLosThresholdL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21() < 0)
                return;
        }

        private void cbLosThresholdL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21() < 0)
                return;
        }

        private void cbLosThresholdL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21() < 0)
                return;
        }

        private void cbLosHysteresisL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22() < 0)
                return;
        }

        private void cbLosHysteresisL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22() < 0)
                return;
        }

        private void cbLosHysteresisL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22() < 0)
                return;
        }

        private void cbLosHysteresisL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22() < 0)
                return;
        }

        private void cbInvertPolarityL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr23() < 0)
                return;
        }

        private void cbInvertPolarityL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr23() < 0)
                return;
        }

        private void cbInvertPolarityL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr23() < 0)
                return;
        }

        private void cbInvertPolarityL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr23() < 0)
                return;
        }

        private void cbOffsetCorrectionSelectL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr24() < 0)
                return;
        }

        private void cbOffsetCorrectionSelectL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr24() < 0)
                return;
        }

        private void cbOffsetCorrectionSelectL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr24() < 0)
                return;
        }

        private void cbOffsetCorrectionSelectL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr24() < 0)
                return;
        }

        private void cbSlaEnableL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr24() < 0)
                return;
        }

        private void cbSlaEnableL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr24() < 0)
                return;
        }

        private void cbSlaEnableL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr24() < 0)
                return;
        }

        private void cbSlaEnableL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr24() < 0)
                return;
        }

        private void cbSlaL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr25() < 0)
                return;
        }

        private void cbSlaL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr26() < 0)
                return;
        }

        private void cbSlaL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr27() < 0)
                return;
        }

        private void cbSlaL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr28() < 0)
                return;
        }

        private void cbTiaAgcThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2D() < 0)
                return;
        }

        private void cbDisableAutoMuteL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40() < 0)
                return;
        }

        private void cbDisableAutoMuteL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40() < 0)
                return;
        }

        private void cbDisableAutoMuteL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40() < 0)
                return;
        }

        private void cbDisableAutoMuteL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40() < 0)
                return;
        }

        private void cbMuteForceL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40() < 0)
                return;
        }

        private void cbMuteForceL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40() < 0)
                return;
        }

        private void cbMuteForceL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40() < 0)
                return;
        }

        private void cbMuteForceL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr40() < 0)
                return;
        }

        private void cbSlowSlewL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr41() < 0)
                return;
        }

        private void cbSlowSlewL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr41() < 0)
                return;
        }

        private void cbSlowSlewL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr41() < 0)
                return;
        }

        private void cbSlowSlewL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr41() < 0)
                return;
        }

        private void cbOutputSwingL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr42() < 0)
                return;
        }

        private void cbOutputSwingL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43() < 0)
                return;
        }

        private void cbOutputSwingL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr44() < 0)
                return;
        }

        private void cbOutputSwingL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr45() < 0)
                return;
        }

        private void cbOutputDeEmphasisL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr46() < 0)
                return;
        }

        private void cbOutputDeEmphasisL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr46() < 0)
                return;
        }

        private void cbOutputDeEmphasisL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr47() < 0)
                return;
        }

        private void cbOutputDeEmphasisL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr47() < 0)
                return;
        }

        private void cbCdrLockThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr82() < 0)
                return;
        }

        private void cbOutputSwingAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte[] data = new byte[4];
            int rv;
            byte bTmp;

            if (reading == true)
                return;

            bTmp = data[0] = data[1] = data[2] = data[3] = 0;

            bTmp = Convert.ToByte(cbOutputSwingAll.SelectedIndex);
            data[0] |= bTmp;
            data[1] |= bTmp;
            data[2] |= bTmp;
            data[3] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x42, 4, data);
            if (rv < 0)
                return;

            return;
        }

        private void cbOutputDeEmphasisAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte[] data = new byte[2];
            int rv;
            byte bTmp;

            if (reading == true)
                return;

            bTmp = data[0] = data[1] = 0;

            bTmp = Convert.ToByte(cbOutputDeEmphasisAll.SelectedIndex);
            data[0] |= bTmp;
            data[1] |= bTmp;
            bTmp <<= 4;
            data[0] |= bTmp;
            data[1] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x46, 2, data);
            if (rv < 0)
                return;

            return;
        }

        private void bStoreIntoFlash_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1] { 0xA0 };
            int rv;

            bStoreIntoFlash.Enabled = false;
            reading = true;
            rv = i2cWriteCB(devAddr, 0xAA, 1, data);
            Thread.Sleep(1000);
            bStoreIntoFlash.Enabled = true;
            reading = false;
        }
    }
}
