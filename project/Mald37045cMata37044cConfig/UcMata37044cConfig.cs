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

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbOutputSwingAll.Items.Add(item);
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

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbOutputDeEmphasisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbOutputDeEmphasisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbOutputDeEmphasisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbOutputDeEmphasisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbOutputDeEmphasisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbOutputDeEmphasisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbOutputDeEmphasisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbOutputDeEmphasisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbOutputDeEmphasisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbOutputDeEmphasisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbOutputDeEmphasisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbOutputDeEmphasisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbOutputDeEmphasisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbOutputDeEmphasisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbOutputDeEmphasisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbOutputDeEmphasisAll.Items.Add(item);

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL0OutputAmplitude100400.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL0OutputAmplitude300600.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL0OutputAmplitude400800.Items.Add(item);
            }
            
            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL0OutputAmplitude6001200.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL0OutputAmplitudeReserved0.Items.Add(item);
            }
            
            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL0OutputAmplitudeReserved1.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL0OutputAmplitudeReserved2.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL0OutputAmplitudeReserved3.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL0OutputAmplitudeReserved4.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL1OutputAmplitude100400.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL1OutputAmplitude300600.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL1OutputAmplitude400800.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL1OutputAmplitude6001200.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL1OutputAmplitudeReserved0.Items.Add(item);
            }
            
            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL1OutputAmplitudeReserved1.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL1OutputAmplitudeReserved2.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL1OutputAmplitudeReserved3.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL1OutputAmplitudeReserved4.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL2OutputAmplitude100400.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL2OutputAmplitude300600.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL2OutputAmplitude400800.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL2OutputAmplitude6001200.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL2OutputAmplitudeReserved0.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL2OutputAmplitudeReserved1.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL2OutputAmplitudeReserved2.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL2OutputAmplitudeReserved3.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL2OutputAmplitudeReserved4.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL3OutputAmplitude100400.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL3OutputAmplitude300600.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL3OutputAmplitude400800.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL3OutputAmplitude6001200.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL3OutputAmplitudeReserved0.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL3OutputAmplitudeReserved1.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL3OutputAmplitudeReserved2.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL3OutputAmplitudeReserved3.Items.Add(item);
            }

            for (i = 0, dTmp = 300; i < 64; i++, dTmp += 10) {
                item = new ComboboxItem();
                item.Text = i + ":" + dTmp.ToString() + "mVppd";
                item.Value = i;
                cbL3OutputAmplitudeReserved4.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL0OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL0OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL0OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL0OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL0OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL0OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL0OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL0OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL0OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL0OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL0OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL0OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL0OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL0OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL0OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL0OutputEmphasis0Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL0OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL0OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL0OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL0OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL0OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL0OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL0OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL0OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL0OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL0OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL0OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL0OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL0OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL0OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL0OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL0OutputEmphasis1Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL0OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL0OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL0OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL0OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL0OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL0OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL0OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL0OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL0OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL0OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL0OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL0OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL0OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL0OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL0OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL0OutputEmphasis2Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL0OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL0OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL0OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL0OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL0OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL0OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL0OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL0OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL0OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL0OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL0OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL0OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL0OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL0OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL0OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL0OutputEmphasis3Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL0OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL0OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL0OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL0OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL0OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL0OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL0OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL0OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL0OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL0OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL0OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL0OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL0OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL0OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL0OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL0OutputEmphasis4Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL0OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL0OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL0OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL0OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL0OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL0OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL0OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL0OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL0OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL0OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL0OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL0OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL0OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL0OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL0OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL0OutputEmphasis5Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL0OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL0OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL0OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL0OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL0OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL0OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL0OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL0OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL0OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL0OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL0OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL0OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL0OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL0OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL0OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL0OutputEmphasis6Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL0OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL0OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL0OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL0OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL0OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL0OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL0OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL0OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL0OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL0OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL0OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL0OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL0OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL0OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL0OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL0OutputEmphasis7Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL0OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL0OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL0OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL0OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL0OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL0OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL0OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL0OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL0OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL0OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL0OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL0OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL0OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL0OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL0OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL0OutputEmphasisReserved0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL1OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL1OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL1OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL1OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL1OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL1OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL1OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL1OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL1OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL1OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL1OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL1OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL1OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL1OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL1OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL1OutputEmphasis0Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL1OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL1OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL1OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL1OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL1OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL1OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL1OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL1OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL1OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL1OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL1OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL1OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL1OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL1OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL1OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL1OutputEmphasis1Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL1OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL1OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL1OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL1OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL1OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL1OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL1OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL1OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL1OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL1OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL1OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL1OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL1OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL1OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL1OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL1OutputEmphasis2Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL1OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL1OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL1OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL1OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL1OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL1OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL1OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL1OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL1OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL1OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL1OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL1OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL1OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL1OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL1OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL1OutputEmphasis3Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL1OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL1OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL1OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL1OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL1OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL1OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL1OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL1OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL1OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL1OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL1OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL1OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL1OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL1OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL1OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL1OutputEmphasis4Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL1OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL1OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL1OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL1OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL1OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL1OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL1OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL1OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL1OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL1OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL1OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL1OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL1OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL1OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL1OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL1OutputEmphasis5Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL1OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL1OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL1OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL1OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL1OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL1OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL1OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL1OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL1OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL1OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL1OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL1OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL1OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL1OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL1OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL1OutputEmphasis6Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL1OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL1OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL1OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL1OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL1OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL1OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL1OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL1OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL1OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL1OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL1OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL1OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL1OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL1OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL1OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL1OutputEmphasis7Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL1OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL1OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL1OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL1OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL1OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL1OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL1OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL1OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL1OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL1OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL1OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL1OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL1OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL1OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL1OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL1OutputEmphasisReserved0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL2OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL2OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL2OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL2OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL2OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL2OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL2OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL2OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL2OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL2OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL2OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL2OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL2OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL2OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL2OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL2OutputEmphasis0Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL2OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL2OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL2OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL2OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL2OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL2OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL2OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL2OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL2OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL2OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL2OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL2OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL2OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL2OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL2OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL2OutputEmphasis1Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL2OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL2OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL2OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL2OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL2OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL2OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL2OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL2OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL2OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL2OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL2OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL2OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL2OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL2OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL2OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL2OutputEmphasis2Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL2OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL2OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL2OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL2OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL2OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL2OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL2OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL2OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL2OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL2OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL2OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL2OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL2OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL2OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL2OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL2OutputEmphasis3Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL2OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL2OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL2OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL2OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL2OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL2OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL2OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL2OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL2OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL2OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL2OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL2OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL2OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL2OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL2OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL2OutputEmphasis4Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL2OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL2OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL2OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL2OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL2OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL2OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL2OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL2OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL2OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL2OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL2OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL2OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL2OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL2OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL2OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL2OutputEmphasis5Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL2OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL2OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL2OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL2OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL2OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL2OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL2OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL2OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL2OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL2OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL2OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL2OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL2OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL2OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL2OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL2OutputEmphasis6Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL2OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL2OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL2OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL2OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL2OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL2OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL2OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL2OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL2OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL2OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL2OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL2OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL2OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL2OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL2OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL2OutputEmphasis7Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL2OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL2OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL2OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL2OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL2OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL2OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL2OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL2OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL2OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL2OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL2OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL2OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL2OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL2OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL2OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL2OutputEmphasisReserved0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL3OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL3OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL3OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL3OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL3OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL3OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL3OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL3OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL3OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL3OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL3OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL3OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL3OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL3OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL3OutputEmphasis0Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL3OutputEmphasis0Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL3OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL3OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL3OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL3OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL3OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL3OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL3OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL3OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL3OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL3OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL3OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL3OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL3OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL3OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL3OutputEmphasis1Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL3OutputEmphasis1Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL3OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL3OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL3OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL3OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL3OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL3OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL3OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL3OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL3OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL3OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL3OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL3OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL3OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL3OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL3OutputEmphasis2Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL3OutputEmphasis2Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL3OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL3OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL3OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL3OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL3OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL3OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL3OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL3OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL3OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL3OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL3OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL3OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL3OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL3OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL3OutputEmphasis3Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL3OutputEmphasis3Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL3OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL3OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL3OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL3OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL3OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL3OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL3OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL3OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL3OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL3OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL3OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL3OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL3OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL3OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL3OutputEmphasis4Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL3OutputEmphasis4Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL3OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL3OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL3OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL3OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL3OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL3OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL3OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL3OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL3OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL3OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL3OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL3OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL3OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL3OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL3OutputEmphasis5Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL3OutputEmphasis5Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL3OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL3OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL3OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL3OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL3OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL3OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL3OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL3OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL3OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL3OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL3OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL3OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL3OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL3OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL3OutputEmphasis6Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL3OutputEmphasis6Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL3OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL3OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL3OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL3OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL3OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL3OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL3OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL3OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL3OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL3OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL3OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL3OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL3OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL3OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL3OutputEmphasis7Db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL3OutputEmphasis7Db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:0.0dB";
            item.Value = 0;
            cbL3OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:0.4dB";
            item.Value = 1;
            cbL3OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:0.7dB";
            item.Value = 2;
            cbL3OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:1.0dB";
            item.Value = 3;
            cbL3OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:1.5dB";
            item.Value = 4;
            cbL3OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:2.0dB";
            item.Value = 5;
            cbL3OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:2.5dB";
            item.Value = 6;
            cbL3OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:3.0dB";
            item.Value = 7;
            cbL3OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:3.5dB";
            item.Value = 8;
            cbL3OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:4.0dB";
            item.Value = 9;
            cbL3OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:4.5dB";
            item.Value = 10;
            cbL3OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:5.0dB";
            item.Value = 11;
            cbL3OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:5.5dB";
            item.Value = 12;
            cbL3OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:6.0dB";
            item.Value = 13;
            cbL3OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:6.5dB";
            item.Value = 14;
            cbL3OutputEmphasisReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:7.5dB";
            item.Value = 15;
            cbL3OutputEmphasisReserved0.Items.Add(item);
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

            foreach (ComboboxItem item in cbDividedClockOutput.Items) {
                if (item.Value == ((data & 0x04) >> 2)) {
                    cbDividedClockOutput.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbInterruptPol.Items) {
                if (item.Value == ((data & 0x02) >> 1)) {
                    cbInterruptPol.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbInterruptOutputType.Items) {
                if (item.Value == (data & 0x01)) {
                    cbInterruptOutputType.SelectedItem = item;
                }
            }
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

            if ((data & 0x08) == 0)
                cbCdrAutoBypassL3.Checked = false;
            else
                cbCdrAutoBypassL3.Checked = true;

            if ((data & 0x04) == 0)
                cbCdrAutoBypassL2.Checked = false;
            else
                cbCdrAutoBypassL2.Checked = true;

            if ((data & 0x02) == 0)
                cbCdrAutoBypassL1.Checked = false;
            else
                cbCdrAutoBypassL1.Checked = true;

            if ((data & 0x01) == 0)
                cbCdrAutoBypassL0.Checked = false;
            else
                cbCdrAutoBypassL0.Checked = true;
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

        private void _ParseAddrA0(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputAmplitude100400.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL0OutputAmplitude100400.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA1(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputAmplitude300600.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL0OutputAmplitude300600.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA2(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputAmplitude400800.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL0OutputAmplitude400800.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA3(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputAmplitude6001200.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL0OutputAmplitude6001200.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA4(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputAmplitudeReserved0.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL0OutputAmplitudeReserved0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA5(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputAmplitudeReserved1.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL0OutputAmplitudeReserved1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA6(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputAmplitudeReserved2.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL0OutputAmplitudeReserved2.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA7(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputAmplitudeReserved3.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL0OutputAmplitudeReserved3.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA8(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputAmplitudeReserved4.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL0OutputAmplitudeReserved4.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA9(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputEmphasis0Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL0OutputEmphasis0Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrAA(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputEmphasis1Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL0OutputEmphasis1Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrAB(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputEmphasis2Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL0OutputEmphasis2Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrAC(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputEmphasis3Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL0OutputEmphasis3Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrAD(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputEmphasis4Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL0OutputEmphasis4Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrAE(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputEmphasis5Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL0OutputEmphasis5Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrAF(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputEmphasis6Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL0OutputEmphasis6Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB0(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputEmphasis7Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL0OutputEmphasis7Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB1(byte data)
        {
            foreach (ComboboxItem item in cbL0OutputEmphasisReserved0.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL0OutputEmphasisReserved0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB2(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputAmplitude100400.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL1OutputAmplitude100400.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB3(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputAmplitude300600.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL1OutputAmplitude300600.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB4(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputAmplitude400800.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL1OutputAmplitude400800.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB5(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputAmplitude6001200.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL1OutputAmplitude6001200.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB6(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputAmplitudeReserved0.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL1OutputAmplitudeReserved0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB7(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputAmplitudeReserved1.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL1OutputAmplitudeReserved1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB8(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputAmplitudeReserved2.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL1OutputAmplitudeReserved2.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB9(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputAmplitudeReserved3.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL1OutputAmplitudeReserved3.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrBA(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputAmplitudeReserved4.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL1OutputAmplitudeReserved4.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrBB(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputEmphasis0Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL1OutputEmphasis0Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrBC(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputEmphasis1Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL1OutputEmphasis1Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrBD(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputEmphasis2Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL1OutputEmphasis2Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrBE(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputEmphasis3Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL1OutputEmphasis3Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrBF(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputEmphasis4Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL1OutputEmphasis4Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC0(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputEmphasis5Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL1OutputEmphasis5Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC1(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputEmphasis6Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL1OutputEmphasis6Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC2(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputEmphasis7Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL1OutputEmphasis7Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC3(byte data)
        {
            foreach (ComboboxItem item in cbL1OutputEmphasisReserved0.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL1OutputEmphasisReserved0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC4(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputAmplitude100400.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL2OutputAmplitude100400.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC5(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputAmplitude300600.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL2OutputAmplitude300600.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC6(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputAmplitude400800.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL2OutputAmplitude400800.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC7(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputAmplitude6001200.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL2OutputAmplitude6001200.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC8(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputAmplitudeReserved0.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL2OutputAmplitudeReserved0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC9(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputAmplitudeReserved1.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL2OutputAmplitudeReserved1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrCA(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputAmplitudeReserved2.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL2OutputAmplitudeReserved2.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrCB(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputAmplitudeReserved3.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL2OutputAmplitudeReserved3.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrCC(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputAmplitudeReserved4.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL2OutputAmplitudeReserved4.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrCD(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputEmphasis0Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL2OutputEmphasis0Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrCE(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputEmphasis1Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL2OutputEmphasis1Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrCF(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputEmphasis2Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL2OutputEmphasis2Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrD0(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputEmphasis3Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL2OutputEmphasis3Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrD1(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputEmphasis4Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL2OutputEmphasis4Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrD2(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputEmphasis5Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL2OutputEmphasis5Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrD3(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputEmphasis6Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL2OutputEmphasis6Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrD4(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputEmphasis7Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL2OutputEmphasis7Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrD5(byte data)
        {
            foreach (ComboboxItem item in cbL2OutputEmphasisReserved0.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL2OutputEmphasisReserved0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrD6(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputAmplitude100400.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL3OutputAmplitude100400.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrD7(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputAmplitude300600.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL3OutputAmplitude300600.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrD8(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputAmplitude400800.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL3OutputAmplitude400800.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrD9(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputAmplitude6001200.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL3OutputAmplitude6001200.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrDA(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputAmplitudeReserved0.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL3OutputAmplitudeReserved0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrDB(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputAmplitudeReserved1.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL3OutputAmplitudeReserved1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrDC(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputAmplitudeReserved2.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL3OutputAmplitudeReserved2.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrDD(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputAmplitudeReserved3.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL3OutputAmplitudeReserved3.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrDE(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputAmplitudeReserved4.Items) {
                if (item.Value == (data & 0x3F)) {
                    cbL3OutputAmplitudeReserved4.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrDF(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputEmphasis0Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL3OutputEmphasis0Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrE0(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputEmphasis1Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL3OutputEmphasis1Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrE1(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputEmphasis2Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL3OutputEmphasis2Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrE2(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputEmphasis3Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL3OutputEmphasis3Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrE3(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputEmphasis4Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL3OutputEmphasis4Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrE4(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputEmphasis5Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL3OutputEmphasis5Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrE5(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputEmphasis6Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL3OutputEmphasis6Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrE6(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputEmphasis7Db.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL3OutputEmphasis7Db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrE7(byte data)
        {
            foreach (ComboboxItem item in cbL3OutputEmphasisReserved0.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbL3OutputEmphasisReserved0.SelectedItem = item;
                }
            }
        }

        private void bReadAll_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[72];
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

            rv = i2cReadCB(devAddr, 0x40, 8, data);
            if (rv != 8)
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

            rv = i2cReadCB(devAddr, 0xA0, 72, data);
            if (rv != 72)
                goto exit;

            _ParseAddrA0(data[0]);
            _ParseAddrA1(data[1]);
            _ParseAddrA2(data[2]);
            _ParseAddrA3(data[3]);
            _ParseAddrA4(data[4]);
            _ParseAddrA5(data[5]);
            _ParseAddrA6(data[6]);
            _ParseAddrA7(data[7]);
            _ParseAddrA8(data[8]);
            _ParseAddrA9(data[9]);
            _ParseAddrAA(data[10]);
            _ParseAddrAB(data[11]);
            _ParseAddrAC(data[12]);
            _ParseAddrAD(data[13]);
            _ParseAddrAE(data[14]);
            _ParseAddrAF(data[15]);
            _ParseAddrB0(data[16]);
            _ParseAddrB1(data[17]);
            _ParseAddrB2(data[18]);
            _ParseAddrB3(data[19]);
            _ParseAddrB4(data[20]);
            _ParseAddrB5(data[21]);
            _ParseAddrB6(data[22]);
            _ParseAddrB7(data[23]);
            _ParseAddrB8(data[24]);
            _ParseAddrB9(data[25]);
            _ParseAddrBA(data[26]);
            _ParseAddrBB(data[27]);
            _ParseAddrBC(data[28]);
            _ParseAddrBD(data[29]);
            _ParseAddrBE(data[30]);
            _ParseAddrBF(data[31]);
            _ParseAddrC0(data[32]);
            _ParseAddrC1(data[33]);
            _ParseAddrC2(data[34]);
            _ParseAddrC3(data[35]);
            _ParseAddrC4(data[36]);
            _ParseAddrC5(data[37]);
            _ParseAddrC6(data[38]);
            _ParseAddrC7(data[39]);
            _ParseAddrC8(data[40]);
            _ParseAddrC9(data[41]);
            _ParseAddrCA(data[42]);
            _ParseAddrCB(data[43]);
            _ParseAddrCC(data[44]);
            _ParseAddrCD(data[45]);
            _ParseAddrCE(data[46]);
            _ParseAddrCF(data[47]);
            _ParseAddrD0(data[48]);
            _ParseAddrD1(data[49]);
            _ParseAddrD2(data[50]);
            _ParseAddrD3(data[51]);
            _ParseAddrD4(data[52]);
            _ParseAddrD5(data[53]);
            _ParseAddrD6(data[54]);
            _ParseAddrD7(data[55]);
            _ParseAddrD8(data[56]);
            _ParseAddrD9(data[57]);
            _ParseAddrDA(data[58]);
            _ParseAddrDB(data[59]);
            _ParseAddrDC(data[60]);
            _ParseAddrDD(data[61]);
            _ParseAddrDE(data[62]);
            _ParseAddrDF(data[63]);
            _ParseAddrE0(data[64]);
            _ParseAddrE1(data[65]);
            _ParseAddrE2(data[66]);
            _ParseAddrE3(data[67]);
            _ParseAddrE4(data[68]);
            _ParseAddrE5(data[69]);
            _ParseAddrE6(data[70]);
            _ParseAddrE7(data[71]);

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

            data[0] = 0x00;

            if (cbRateSelectL3.SelectedIndex == 1)
                data[0] |= 0x80;

            if (cbRateSelectL2.SelectedIndex == 1)
                data[0] |= 0x40;

            if (cbRateSelectL1.SelectedIndex == 1)
                data[0] |= 0x20;

            if (cbRateSelectL0.SelectedIndex == 1)
                data[0] |= 0x10;

            if (cbCdrAutoBypassL3.Checked == true)
                data[0] |= 0x08;

            if (cbCdrAutoBypassL2.Checked == true)
                data[0] |= 0x04;

            if (cbCdrAutoBypassL1.Checked == true)
                data[0] |= 0x02;

            if (cbCdrAutoBypassL0.Checked == true)
                data[0] |= 0x01;

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

        private int _WriteAddrA0()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitude100400.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xA0, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrA1()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitude300600.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xA1, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrA2()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitude400800.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xA2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrA3()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitude6001200.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xA3, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrA4()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitudeReserved0.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xA4, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrA5()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitudeReserved1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xA5, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrA6()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitudeReserved2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xA6, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrA7()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitudeReserved3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xA7, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrA8()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitudeReserved4.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xA8, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrA9()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasis0Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xA9, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrAA()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasis1Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xAA, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrAB()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasis2Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xAB, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrAC()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasis3Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xAC, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrAD()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasis4Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xAD, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrAE()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasis5Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xAE, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrAF()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasis6Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xAF, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrB0()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasis7Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xB0, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrB1()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasisReserved0.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xB1, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrB2()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputAmplitude100400.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xB2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrB3()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputAmplitude300600.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xB3, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrB4()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputAmplitude400800.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xB4, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrB5()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputAmplitude6001200.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xB5, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrB6()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputAmplitudeReserved0.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xB6, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrB7()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputAmplitudeReserved1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xB7, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrB8()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputAmplitudeReserved2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xB8, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrB9()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputAmplitudeReserved3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xB9, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrBA()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputAmplitudeReserved4.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xBA, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrBB()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputEmphasis0Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xBB, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrBC()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputEmphasis1Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xBC, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrBD()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputEmphasis2Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xBD, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrBE()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputEmphasis3Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xBE, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrBF()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputEmphasis4Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xBF, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrC0()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputEmphasis5Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xC0, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrC1()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputEmphasis6Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xC1, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrC2()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputEmphasis7Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xC2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrC3()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL1OutputEmphasisReserved0.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xC3, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrC4()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputAmplitude100400.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xC4, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrC5()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputAmplitude300600.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xC5, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrC6()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputAmplitude400800.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xC6, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrC7()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputAmplitude6001200.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xC7, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrC8()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputAmplitudeReserved0.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xC8, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrC9()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputAmplitudeReserved1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xC9, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrCA()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputAmplitudeReserved2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xCA, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrCB()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputAmplitudeReserved3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xCB, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrCC()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputAmplitudeReserved4.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xCC, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrCD()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputEmphasis0Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xCD, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrCE()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputEmphasis1Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xCE, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrCF()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputEmphasis2Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xCF, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrD0()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputEmphasis3Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xD0, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrD1()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputEmphasis4Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xD1, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrD2()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputEmphasis5Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xD2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrD3()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputEmphasis6Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xD3, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrD4()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputEmphasis7Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xD4, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrD5()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL2OutputEmphasisReserved0.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xD5, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrD6()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitude100400.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xD6, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrD7()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitude300600.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xD7, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrD8()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitude400800.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xD8, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrD9()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitude6001200.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xD9, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrDA()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitudeReserved0.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xDA, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrDB()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitudeReserved1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xDB, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrDC()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitudeReserved2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xDC, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrDD()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitudeReserved3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xDD, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrDE()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputAmplitudeReserved4.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xDE, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrDF()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasis0Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xDF, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrE0()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasis1Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xE0, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrE1()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasis2Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xE1, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrE2()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasis3Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xE2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrE3()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasis4Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xE3, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrE4()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasis5Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xE4, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrE5()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasis6Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xE5, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrE6()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasis7Db.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xE6, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrE7()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbL0OutputEmphasisReserved0.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xE7, 1, data);
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

        private void cbCdrAutoBypassL0_CheckedChanged(object sender, EventArgs e)
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

        private void cbCdrAutoBypassL1_CheckedChanged(object sender, EventArgs e)
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

        private void cbCdrAutoBypassL2_CheckedChanged(object sender, EventArgs e)
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

        private void cbCdrAutoBypassL3_CheckedChanged(object sender, EventArgs e)
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
            rv = i2cWriteCB(devAddr, 0xFA, 1, data);
            Thread.Sleep(1000);
            bStoreIntoFlash.Enabled = true;
            reading = false;
        }

        private void cbL0OutputAmplitude100400_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA0() < 0)
                return;
        }

        private void cbL0OutputAmplitude300600_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA1() < 0)
                return;
        }

        private void cbL0OutputAmplitude400800_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA2() < 0)
                return;
        }

        private void cbL0OutputAmplitude6001200_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA3() < 0)
                return;
        }

        private void cbL0OutputAmplitudeReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA4() < 0)
                return;
        }

        private void cbL0OutputAmplitudeReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA5() < 0)
                return;
        }

        private void cbL0OutputAmplitudeReserved2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA6() < 0)
                return;
        }

        private void cbL0OutputAmplitudeReserved3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA7() < 0)
                return;
        }

        private void cbL0OutputAmplitudeReserved4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA8() < 0)
                return;
        }

        private void cbL0OutputEmphasis0Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA9() < 0)
                return;
        }

        private void cbL0OutputEmphasis1Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrAA() < 0)
                return;
        }

        private void cbL0OutputEmphasis2Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrAB() < 0)
                return;
        }

        private void cbL0OutputEmphasis3Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrAC() < 0)
                return;
        }

        private void cbL0OutputEmphasis4Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrAD() < 0)
                return;
        }

        private void cbL0OutputEmphasis5Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrAE() < 0)
                return;
        }

        private void cbL0OutputEmphasis6Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrAF() < 0)
                return;
        }

        private void cbL0OutputEmphasis7Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB0() < 0)
                return;
        }

        private void cbL0OutputEmphasisReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB1() < 0)
                return;
        }

        private void cbL1OutputAmplitude100400_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB2() < 0)
                return;
        }

        private void cbL1OutputAmplitude300600_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB3() < 0)
                return;
        }

        private void cbL1OutputAmplitude400800_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB4() < 0)
                return;
        }

        private void cbL1OutputAmplitude6001200_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB5() < 0)
                return;
        }

        private void cbL1OutputAmplitudeReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB6() < 0)
                return;
        }

        private void cbL1OutputAmplitudeReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB7() < 0)
                return;
        }

        private void cbL1OutputAmplitudeReserved2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB8() < 0)
                return;
        }

        private void cbL1OutputAmplitudeReserved3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB9() < 0)
                return;
        }

        private void cbL1OutputAmplitudeReserved4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBA() < 0)
                return;
        }

        private void cbL1OutputEmphasis0Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBB() < 0)
                return;
        }

        private void cbL1OutputEmphasis1Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBC() < 0)
                return;
        }

        private void cbL1OutputEmphasis2Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBD() < 0)
                return;
        }

        private void cbL1OutputEmphasis3Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBE() < 0)
                return;
        }

        private void cbL1OutputEmphasis4Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBF() < 0)
                return;
        }

        private void cbL1OutputEmphasis5Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC0() < 0)
                return;
        }

        private void cbL1OutputEmphasis6Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC1() < 0)
                return;
        }

        private void cbL1OutputEmphasis7Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC2() < 0)
                return;
        }

        private void cbL1OutputEmphasisReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC3() < 0)
                return;
        }

        private void cbL2OutputAmplitude100400_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC4() < 0)
                return;
        }

        private void cbL2OutputAmplitude300600_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC5() < 0)
                return;
        }

        private void cbL2OutputAmplitude400800_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC6() < 0)
                return;
        }

        private void cbL2OutputAmplitude6001200_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC7() < 0)
                return;
        }

        private void cbL2OutputAmplitudeReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC8() < 0)
                return;
        }

        private void cbL2OutputAmplitudeReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC9() < 0)
                return;
        }

        private void cbL2OutputAmplitudeReserved2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCA() < 0)
                return;
        }

        private void cbL2OutputAmplitudeReserved3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCB() < 0)
                return;
        }

        private void cbL2OutputAmplitudeReserved4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCC() < 0)
                return;
        }

        private void cbL2OutputEmphasis0Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCD() < 0)
                return;
        }

        private void cbL2OutputEmphasis1Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCE() < 0)
                return;
        }

        private void cbL2OutputEmphasis2Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCF() < 0)
                return;
        }

        private void cbL2OutputEmphasis3Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD0() < 0)
                return;
        }

        private void cbL2OutputEmphasis4Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD1() < 0)
                return;
        }

        private void cbL2OutputEmphasis5Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD2() < 0)
                return;
        }

        private void cbL2OutputEmphasis6Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD3() < 0)
                return;
        }

        private void cbL2OutputEmphasis7Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD4() < 0)
                return;
        }

        private void cbL2OutputEmphasisReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD5() < 0)
                return;
        }

        private void cbL3OutputAmplitude100400_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD6() < 0)
                return;
        }

        private void cbL3OutputAmplitude300600_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD7() < 0)
                return;
        }

        private void cbL3OutputAmplitude400800_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD8() < 0)
                return;
        }

        private void cbL3OutputAmplitude6001200_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD9() < 0)
                return;
        }

        private void cbL3OutputAmplitudeReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDA() < 0)
                return;
        }

        private void cbL3OutputAmplitudeReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDB() < 0)
                return;
        }

        private void cbL3OutputAmplitudeReserved2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDC() < 0)
                return;
        }

        private void cbL3OutputAmplitudeReserved3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDD() < 0)
                return;
        }

        private void cbL3OutputAmplitudeReserved4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDE() < 0)
                return;
        }

        private void cbL3OutputEmphasis0Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDF() < 0)
                return;
        }

        private void cbL3OutputEmphasis1Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE0() < 0)
                return;
        }

        private void cbL3OutputEmphasis2Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE1() < 0)
                return;
        }

        private void cbL3OutputEmphasis3Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE2() < 0)
                return;
        }

        private void cbL3OutputEmphasis4Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE3() < 0)
                return;
        }

        private void cbL3OutputEmphasis5Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE4() < 0)
                return;
        }

        private void cbL3OutputEmphasis6Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE5() < 0)
                return;
        }

        private void cbL3OutputEmphasis7Db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE6() < 0)
                return;
        }

        private void cbL3OutputEmphasisReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE7() < 0)
                return;
        }
    }
}
