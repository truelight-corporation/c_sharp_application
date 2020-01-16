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
    public partial class UcMald37045cConfig : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private const byte devAddr = 12;
        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private bool reading = false;

        public UcMald37045cConfig() {
            ComboboxItem item;
            double dTmp;
            int i;
            
            InitializeComponent();

            item = new ComboboxItem();
            item.Text = "0:26G";
            item.Value = 0;
            cbRateSelectL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:28G";
            item.Value = 1;
            cbRateSelectL0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:operating normally";
            item.Value = 0;
            cbTxFaultStateL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:open circuit";
            item.Value = 1;
            cbTxFaultStateL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:shorted to VCC33";
            item.Value = 2;
            cbTxFaultStateL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:shorted to GND";
            item.Value = 3;
            cbTxFaultStateL0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:30mVppd";
            item.Value = 0;
            cbLosThresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:50mVppd";
            item.Value = 1;
            cbLosThresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:100mVppd";
            item.Value = 2;
            cbLosThresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:150mVppd";
            item.Value = 3;
            cbLosThresholdL0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:~1.5dB";
            item.Value = 0;
            cbLosHysteresisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:~2.5dB";
            item.Value = 1;
            cbLosHysteresisL0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbCtleL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbCtleL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbCtleL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbCtleL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbCtleL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbCtleL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbCtleL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbCtleL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbCtleL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbCtleL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbCtleL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbCtleL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbCtleL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbCtleL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbCtleL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbCtleL0.Items.Add(item);

            for (i = 0, dTmp = 40; i < 128; i++, dTmp++) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "mA";
                item.Value = i;
                cbIBiasL0.Items.Add(item);
            }

            for (i = 0, dTmp = 1; i < 128; i++, dTmp++) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "mA";
                item.Value = i;
                cbIModL0.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0:Minimum";
            item.Value = 0;
            cbTdeEyeShapingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Low";
            item.Value = 1;
            cbTdeEyeShapingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:Medium";
            item.Value = 2;
            cbTdeEyeShapingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:Maximum";
            item.Value = 3;
            cbTdeEyeShapingL0.Items.Add(item);

            for (i = 0; i < 16; i++) {
                item = new ComboboxItem();
                item.Text = i.ToString();
                item.Value = i;
                cbPreFallL0.Items.Add(item);
            }

            for (i = 0, dTmp = 570; i < 8; i++, dTmp+=15) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "%";
                item.Value = i;
                cbCrossingAdjL0.Items.Add(item);
            }
            for (i = 8, dTmp = 450; i < 16; i++, dTmp += 15) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "%";
                item.Value = i;
                cbCrossingAdjL0.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0:26G";
            item.Value = 0;
            cbRateSelectL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:28G";
            item.Value = 1;
            cbRateSelectL1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:operating normally";
            item.Value = 0;
            cbTxFaultStateL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:open circuit";
            item.Value = 1;
            cbTxFaultStateL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:shorted to VCC33";
            item.Value = 2;
            cbTxFaultStateL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:shorted to GND";
            item.Value = 3;
            cbTxFaultStateL1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:30mVppd";
            item.Value = 0;
            cbLosThresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:50mVppd";
            item.Value = 1;
            cbLosThresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:100mVppd";
            item.Value = 2;
            cbLosThresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:150mVppd";
            item.Value = 3;
            cbLosThresholdL1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:~1.5dB";
            item.Value = 0;
            cbLosHysteresisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:~2.5dB";
            item.Value = 1;
            cbLosHysteresisL1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbCtleL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbCtleL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbCtleL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbCtleL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbCtleL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbCtleL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbCtleL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbCtleL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbCtleL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbCtleL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbCtleL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbCtleL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbCtleL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbCtleL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbCtleL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbCtleL1.Items.Add(item);

            for (i = 0, dTmp = 40; i < 128; i++, dTmp++) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "mA";
                item.Value = i;
                cbIBiasL1.Items.Add(item);
            }

            for (i = 0, dTmp = 1; i < 128; i++, dTmp++) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "mA";
                item.Value = i;
                cbIModL1.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0:Minimum";
            item.Value = 0;
            cbTdeEyeShapingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Low";
            item.Value = 1;
            cbTdeEyeShapingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:Medium";
            item.Value = 2;
            cbTdeEyeShapingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:Maximum";
            item.Value = 3;
            cbTdeEyeShapingL1.Items.Add(item);

            for (i = 0; i < 16; i++) {
                item = new ComboboxItem();
                item.Text = i.ToString();
                item.Value = i;
                cbPreFallL1.Items.Add(item);
            }

            for (i = 0, dTmp = 570; i < 8; i++, dTmp += 15) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "%";
                item.Value = i;
                cbCrossingAdjL1.Items.Add(item);
            }
            for (i = 8, dTmp = 450; i < 16; i++, dTmp += 15) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "%";
                item.Value = i;
                cbCrossingAdjL1.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0:26G";
            item.Value = 0;
            cbRateSelectL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:28G";
            item.Value = 1;
            cbRateSelectL2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:operating normally";
            item.Value = 0;
            cbTxFaultStateL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:open circuit";
            item.Value = 1;
            cbTxFaultStateL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:shorted to VCC33";
            item.Value = 2;
            cbTxFaultStateL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:shorted to GND";
            item.Value = 3;
            cbTxFaultStateL2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:30mVppd";
            item.Value = 0;
            cbLosThresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:50mVppd";
            item.Value = 1;
            cbLosThresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:100mVppd";
            item.Value = 2;
            cbLosThresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:150mVppd";
            item.Value = 3;
            cbLosThresholdL2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:~1.5dB";
            item.Value = 0;
            cbLosHysteresisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:~2.5dB";
            item.Value = 1;
            cbLosHysteresisL2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbCtleL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbCtleL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbCtleL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbCtleL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbCtleL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbCtleL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbCtleL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbCtleL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbCtleL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbCtleL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbCtleL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbCtleL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbCtleL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbCtleL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbCtleL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbCtleL2.Items.Add(item);

            for (i = 0, dTmp = 40; i < 128; i++, dTmp++) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "mA";
                item.Value = i;
                cbIBiasL2.Items.Add(item);
            }

            for (i = 0, dTmp = 1; i < 128; i++, dTmp++) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "mA";
                item.Value = i;
                cbIModL2.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0:Minimum";
            item.Value = 0;
            cbTdeEyeShapingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Low";
            item.Value = 1;
            cbTdeEyeShapingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:Medium";
            item.Value = 2;
            cbTdeEyeShapingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:Maximum";
            item.Value = 3;
            cbTdeEyeShapingL2.Items.Add(item);

            for (i = 0; i < 16; i++) {
                item = new ComboboxItem();
                item.Text = i.ToString();
                item.Value = i;
                cbPreFallL2.Items.Add(item);
            }

            for (i = 0, dTmp = 570; i < 8; i++, dTmp += 15) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "%";
                item.Value = i;
                cbCrossingAdjL2.Items.Add(item);
            }
            for (i = 8, dTmp = 450; i < 16; i++, dTmp += 15) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "%";
                item.Value = i;
                cbCrossingAdjL2.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0:26G";
            item.Value = 0;
            cbRateSelectL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:28G";
            item.Value = 1;
            cbRateSelectL3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:operating normally";
            item.Value = 0;
            cbTxFaultStateL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:open circuit";
            item.Value = 1;
            cbTxFaultStateL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:shorted to VCC33";
            item.Value = 2;
            cbTxFaultStateL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:shorted to GND";
            item.Value = 3;
            cbTxFaultStateL3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:30mVppd";
            item.Value = 0;
            cbLosThresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:50mVppd";
            item.Value = 1;
            cbLosThresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:100mVppd";
            item.Value = 2;
            cbLosThresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:150mVppd";
            item.Value = 3;
            cbLosThresholdL3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:~1.5dB";
            item.Value = 0;
            cbLosHysteresisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:~2.5dB";
            item.Value = 1;
            cbLosHysteresisL3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbCtleL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbCtleL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbCtleL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbCtleL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbCtleL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbCtleL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbCtleL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbCtleL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbCtleL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbCtleL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbCtleL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbCtleL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbCtleL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbCtleL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbCtleL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbCtleL3.Items.Add(item);

            for (i = 0, dTmp = 40; i < 128; i++, dTmp++) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "mA";
                item.Value = i;
                cbIBiasL3.Items.Add(item);
            }

            for (i = 0, dTmp = 1; i < 128; i++, dTmp++) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "mA";
                item.Value = i;
                cbIModL3.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0:Minimum";
            item.Value = 0;
            cbTdeEyeShapingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Low";
            item.Value = 1;
            cbTdeEyeShapingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:Medium";
            item.Value = 2;
            cbTdeEyeShapingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:Maximum";
            item.Value = 3;
            cbTdeEyeShapingL3.Items.Add(item);

            for (i = 0; i < 16; i++) {
                item = new ComboboxItem();
                item.Text = i.ToString();
                item.Value = i;
                cbPreFallL3.Items.Add(item);
            }

            for (i = 0, dTmp = 570; i < 8; i++, dTmp += 15) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "%";
                item.Value = i;
                cbCrossingAdjL3.Items.Add(item);
            }
            for (i = 8, dTmp = 450; i < 16; i++, dTmp += 15) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "%";
                item.Value = i;
                cbCrossingAdjL3.Items.Add(item);
            }

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

            for (i = 0, dTmp = 40; i < 128; i++, dTmp++) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "mA";
                item.Value = i;
                cbIBurnIn.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0:Voltage";
            item.Value = 0;
            cbAdcSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Temperature";
            item.Value = 1;
            cbAdcSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:Mon0";
            item.Value = 2;
            cbAdcSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:Mon1";
            item.Value = 3;
            cbAdcSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:Mon2";
            item.Value = 4;
            cbAdcSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:Mon3";
            item.Value = 5;
            cbAdcSelect.Items.Add(item);

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

            for (i = 0; i < 4; i++) {
                item = new ComboboxItem();
                item.Text = i + ":Channel " + i;
                item.Value = i;
                cbAtpChannelSelect.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0:No node probed";
            item.Value = 0;
            cbProbeNodeSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:VCC33";
            item.Value = 1;
            cbProbeNodeSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:IBIAS reference";
            item.Value = 4;
            cbProbeNodeSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:IBIAS measurement";
            item.Value = 8;
            cbProbeNodeSelect.Items.Add(item);
        }

        public int SetI2cReadCBApi(I2cReadCB cb) {
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
            if ((data & 0x08) == 0)
                cbEnableDividedClock.Checked = false;
            else
                cbEnableDividedClock.Checked = true;

            if ((data & 0x04) == 0)
                cbDividedClockOutput.SelectedIndex = 0;
            else
                cbDividedClockOutput.SelectedIndex = 1;

            if ((data & 0x02) == 0)
                cbInterruptPol.SelectedIndex = 0;
            else
                cbInterruptPol.SelectedIndex = 1;

            if ((data & 0x01) == 0)
                cbInterruptOutputType.SelectedIndex = 0;
            else
                cbInterruptOutputType.SelectedIndex = 1;
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
                cbTxFaultL3.Checked = false;
            else
                cbTxFaultL3.Checked = true;

            if ((data & 0x04) == 0)
                cbTxFaultL2.Checked = false;
            else
                cbTxFaultL2.Checked = true;

            if ((data & 0x02) == 0)
                cbTxFaultL1.Checked = false;
            else
                cbTxFaultL1.Checked = true;

            if ((data & 0x01) == 0)
                cbTxFaultL0.Checked = false;
            else
                cbTxFaultL0.Checked = true;
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
            foreach(ComboboxItem item in cbTxFaultStateL3.Items) {
                if (item.Value == ((data & 0xC0) >> 6)) {
                    cbTxFaultStateL3.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbTxFaultStateL2.Items) {
                if (item.Value == ((data & 0x30) >> 4)) {
                    cbTxFaultStateL2.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbTxFaultStateL1.Items) {
                if (item.Value == ((data & 0x0C) >> 2)) {
                    cbTxFaultStateL1.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbTxFaultStateL0.Items) {
                if (item.Value == (data & 0x03)) {
                    cbTxFaultStateL0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr1B(byte data)
        {
            if ((data & 0x08) == 0)
                cbTxFaultAlarmMaskL3.Checked = false;
            else
                cbTxFaultAlarmMaskL3.Checked = true;

            if ((data & 0x04) == 0)
                cbTxFaultAlarmMaskL2.Checked = false;
            else
                cbTxFaultAlarmMaskL2.Checked = true;

            if ((data & 0x02) == 0)
                cbTxFaultAlarmMaskL1.Checked = false;
            else
                cbTxFaultAlarmMaskL1.Checked = true;

            if ((data & 0x01) == 0)
                cbTxFaultAlarmMaskL0.Checked = false;
            else
                cbTxFaultAlarmMaskL0.Checked = true;
        }

        private void _ParseAddr1C(byte data)
        {
            if ((data & 0x08) == 0)
                cbTxFaultAlarmL3.Checked = false;
            else
                cbTxFaultAlarmL3.Checked = true;

            if ((data & 0x04) == 0)
                cbTxFaultAlarmL2.Checked = false;
            else
                cbTxFaultAlarmL2.Checked = true;

            if ((data & 0x02) == 0)
                cbTxFaultAlarmL1.Checked = false;
            else
                cbTxFaultAlarmL1.Checked = true;

            if ((data & 0x01) == 0)
                cbTxFaultAlarmL0.Checked = false;
            else
                cbTxFaultAlarmL0.Checked = true;
        }

        private void _ParseAddr1D(byte data)
        {
            if ((data & 0x40) == 0)
                cbAlarmClearRestartTx.Checked = false;
            else
                cbAlarmClearRestartTx.Checked = true;

            if ((data & 0x20) == 0)
                cbTxDisablePinUpdateRestartTx.Checked = false;
            else
                cbTxDisablePinUpdateRestartTx.Checked = true;

            if ((data & 0x10) == 0)
                cbTxDisableRegUpdateRestartTx.Checked = false;
            else
                cbTxDisableRegUpdateRestartTx.Checked = true;

            if ((data & 0x08) == 0)
                cbIgnoreTxFaultL3.Checked = false;
            else
                cbIgnoreTxFaultL3.Checked = true;

            if ((data & 0x04) == 0)
                cbIgnoreTxFaultL2.Checked = false;
            else
                cbIgnoreTxFaultL2.Checked = true;

            if ((data & 0x02) == 0)
                cbIgnoreTxFaultL1.Checked = false;
            else
                cbIgnoreTxFaultL1.Checked = true;

            if ((data & 0x01) == 0)
                cbIgnoreTxFaultL0.Checked = false;
            else
                cbIgnoreTxFaultL0.Checked = true;
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

            if ((data & 0x40) == 0)
                cbLosHysteresisL2.SelectedIndex = 0;

            if ((data & 0x20) == 0)
                cbLosHysteresisL1.SelectedIndex = 0;

            if ((data & 0x10) == 0)
                cbLosHysteresisL0.SelectedIndex = 0;

            if ((data & 0x08) == 0)
                cbDisableAutoSquelchL3.Checked = false;
            else
                cbDisableAutoSquelchL3.Checked = true;

            if ((data & 0x04) == 0)
                cbDisableAutoSquelchL2.Checked = false;
            else
                cbDisableAutoSquelchL2.Checked = true;

            if ((data & 0x02) == 0)
                cbDisableAutoSquelchL1.Checked = false;
            else
                cbDisableAutoSquelchL1.Checked = true;

            if ((data & 0x01) == 0)
                cbDisableAutoSquelchL0.Checked = false;
            else
                cbDisableAutoSquelchL0.Checked = true;
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

        private void _ParseAddr25(byte data)
        {
            foreach (ComboboxItem item in cbCtleL0.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbCtleL0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr26(byte data)
        {
            foreach (ComboboxItem item in cbCtleL1.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbCtleL1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr27(byte data)
        {
            foreach (ComboboxItem item in cbCtleL2.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbCtleL2.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr28(byte data)
        {
            foreach (ComboboxItem item in cbCtleL3.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbCtleL3.SelectedItem = item;
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

            if ((data & 0x08) == 0)
                cbOutputDisableL3.Checked = false;
            else
                cbOutputDisableL3.Checked = true;

            if ((data & 0x04) == 0)
                cbOutputDisableL2.Checked = false;
            else
                cbOutputDisableL2.Checked = true;

            if ((data & 0x02) == 0)
                cbOutputDisableL1.Checked = false;
            else
                cbOutputDisableL1.Checked = true;

            if ((data & 0x01) == 0)
                cbOutputDisableL0.Checked = false;
            else
                cbOutputDisableL0.Checked = true;
        }

        private void _ParseAddr42(byte data)
        {
            foreach (ComboboxItem item in cbIBiasL0.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbIBiasL0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr43(byte data)
        {
            foreach (ComboboxItem item in cbIBiasL1.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbIBiasL1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr44(byte data)
        {
            foreach (ComboboxItem item in cbIBiasL2.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbIBiasL2.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr45(byte data)
        {
            foreach (ComboboxItem item in cbIBiasL3.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbIBiasL3.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr46(byte data)
        {
            foreach (ComboboxItem item in cbIModL0.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbIModL0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr47(byte data)
        {
            foreach (ComboboxItem item in cbIModL1.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbIModL1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr48(byte data)
        {
            foreach (ComboboxItem item in cbIModL2.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbIModL2.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr49(byte data)
        {
            foreach (ComboboxItem item in cbIModL3.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbIModL3.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr4A(byte data)
        {
            if ((data & 0x80) == 0)
                cbEnablePreFallL0.Checked = false;
            else
                cbEnablePreFallL0.Checked = true;

            foreach (ComboboxItem item in cbPreFallL0.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbPreFallL0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr4B(byte data)
        {
            if ((data & 0x80) == 0)
                cbEnablePreFallL1.Checked = false;
            else
                cbEnablePreFallL1.Checked = true;

            foreach (ComboboxItem item in cbPreFallL1.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbPreFallL1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr4C(byte data)
        {
            if ((data & 0x80) == 0)
                cbEnablePreFallL2.Checked = false;
            else
                cbEnablePreFallL2.Checked = true;

            foreach (ComboboxItem item in cbPreFallL2.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbPreFallL2.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr4D(byte data)
        {
            if ((data & 0x80) == 0)
                cbEnablePreFallL3.Checked = false;
            else
                cbEnablePreFallL3.Checked = true;

            foreach (ComboboxItem item in cbPreFallL3.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbPreFallL3.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr4E(byte data)
        {
            foreach (ComboboxItem item in cbTdeEyeShapingL3.Items) {
                if (item.Value == ((data & 0xC0) >> 6)) {
                    cbTdeEyeShapingL3.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbTdeEyeShapingL2.Items) {
                if (item.Value == ((data & 0x30) >> 4)) {
                    cbTdeEyeShapingL2.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbTdeEyeShapingL1.Items) {
                if (item.Value == ((data & 0x0C) >> 2)) {
                    cbTdeEyeShapingL1.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cbTdeEyeShapingL0.Items) {
                if (item.Value == (data & 0x03)) {
                    cbTdeEyeShapingL0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr4F(byte data)
        {
            foreach (ComboboxItem item in cbCrossingAdjL0.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbCrossingAdjL0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr50(byte data)
        {
            foreach (ComboboxItem item in cbCrossingAdjL1.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbCrossingAdjL1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr51(byte data)
        {
            foreach (ComboboxItem item in cbCrossingAdjL2.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbCrossingAdjL2.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr52(byte data)
        {
            foreach (ComboboxItem item in cbCrossingAdjL3.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbCrossingAdjL3.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr5E(byte data)
        {
            foreach (ComboboxItem item in cbIBurnIn.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbIBurnIn.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr5F(byte data)
        {
            if ((data & 0x80) == 0)
                cbBurnInL3.Checked = false;
            else
                cbBurnInL3.Checked = true;

            if ((data & 0x40) == 0)
                cbBurnInL2.Checked = false;
            else
                cbBurnInL2.Checked = true;

            if ((data & 0x20) == 0)
                cbBurnInL1.Checked = false;
            else
                cbBurnInL1.Checked = true;

            if ((data & 0x10) == 0)
                cbBurnInL0.Checked = false;
            else
                cbBurnInL0.Checked = true;

            if ((data & 0x0F) == 0x0B)
                cbBurnInEnable.Checked = true;
            else
                cbBurnInEnable.Checked = false;
        }

        private void _ParseAddr60(byte data)
        {
            foreach (ComboboxItem item in cbAdcSelect.Items) {
                if (item.Value == (data & 0x07)) {
                    cbAdcSelect.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr62(byte data)
        {
            if ((data & 0x01) == 0)
                cbAdcPowerDown.Checked = false;
            else
                cbAdcPowerDown.Checked = true;
        }

        private void _ParseAddr68_69(byte data0, byte data1)
        {
            int iTmp;

            iTmp = (data0 << 4) | data1;

            tbAdcOutput.Text = "0x" + iTmp.ToString("X3");
        }

        private void _ParseAddr82(byte data)
        {
            if ((data & 0x80) == 0)
                cbAtpEnable.Checked = false;
            else
                cbAtpEnable.Checked = true;

            foreach (ComboboxItem item in cbAtpChannelSelect.Items) {
                if (item.Value == ((data & 0x70) >> 4)) {
                    cbAtpChannelSelect.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr90(byte data)
        {
            foreach (ComboboxItem item in cbCdrLockThreshold.Items) {
                if (item.Value == ((data & 0x30) >> 4)) {
                    cbCdrLockThreshold.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr92(byte data)
        {
            foreach (ComboboxItem item in cbProbeNodeSelect.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbProbeNodeSelect.SelectedItem = item;
                }
            }
        }

        private void bReadAll_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[19];
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

            rv = i2cReadCB(devAddr, 0x16, 8, data);
            if (rv != 8)
                goto exit;

            _ParseAddr16(data[0]);
            _ParseAddr17(data[1]);
            _ParseAddr18(data[2]);
            _ParseAddr19(data[3]);
            _ParseAddr1A(data[4]);
            _ParseAddr1B(data[5]);
            _ParseAddr1C(data[6]);
            _ParseAddr1D(data[7]);

            rv = i2cReadCB(devAddr, 0x21, 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr21(data[0]);
            _ParseAddr22(data[1]);
            _ParseAddr23(data[2]);

            rv = i2cReadCB(devAddr, 0x25, 4, data);
            if (rv != 4)
                goto exit;

            _ParseAddr25(data[0]);
            _ParseAddr26(data[1]);
            _ParseAddr27(data[2]);
            _ParseAddr28(data[3]);

            rv = i2cReadCB(devAddr, 0x40, 19, data);
            if (rv != 19)
                goto exit;

            _ParseAddr40(data[0]);
            _ParseAddr41(data[1]);
            _ParseAddr42(data[2]);
            _ParseAddr43(data[3]);
            _ParseAddr44(data[4]);
            _ParseAddr45(data[5]);
            _ParseAddr46(data[6]);
            _ParseAddr47(data[7]);
            _ParseAddr48(data[8]);
            _ParseAddr49(data[9]);
            _ParseAddr4A(data[10]);
            _ParseAddr4B(data[11]);
            _ParseAddr4C(data[12]);
            _ParseAddr4D(data[13]);
            _ParseAddr4E(data[14]);
            _ParseAddr4F(data[15]);
            _ParseAddr50(data[16]);
            _ParseAddr51(data[17]);
            _ParseAddr52(data[18]);

            rv = i2cReadCB(devAddr, 0x5E, 3, data);
            if (rv != 3)
                goto exit;

            _ParseAddr5E(data[0]);
            _ParseAddr5F(data[1]);
            _ParseAddr60(data[2]);

            rv = i2cReadCB(devAddr, 0x62, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr62(data[0]);

            rv = i2cReadCB(devAddr, 0x68, 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr68_69(data[0], data[1]);

            rv = i2cReadCB(devAddr, 0x82, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr82(data[0]);

            rv = i2cReadCB(devAddr, 0x90, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr90(data[0]);

            rv = i2cReadCB(devAddr, 0x92, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr92(data[0]);

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

            data[0] = 0;

            if (cbEnableDividedClock.Checked == true)
                data[0] |= 0x08;

            if (cbDividedClockOutput.SelectedIndex == 1)
                data[0] |= 0x04;

            if (cbInterruptPol.SelectedIndex == 1)
                data[0] |= 0x02;

            if (cbInterruptOutputType.SelectedIndex == 1)
                data[0] |= 0x01;

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
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbI2cAddressMode.SelectedIndex);
            data[0] |= bTmp;

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

            data[0] = 0;

            if (cbTxFaultAlarmMaskL3.Checked == true)
                data[0] |= 0x08;

            if (cbTxFaultAlarmMaskL2.Checked == true)
                data[0] |= 0x04;

            if (cbTxFaultAlarmMaskL1.Checked == true)
                data[0] |= 0x02;

            if (cbTxFaultAlarmMaskL0.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(devAddr, 0x1B, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr1C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (cbTxFaultAlarmL3.Checked == true)
                data[0] |= 0x08;

            if (cbTxFaultAlarmL2.Checked == true)
                data[0] |= 0x04;

            if (cbTxFaultAlarmL1.Checked == true)
                data[0] |= 0x02;

            if (cbTxFaultAlarmL0.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(devAddr, 0x1C, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr1D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (cbAlarmClearRestartTx.Checked == true)
                data[0] |= 0x40;

            if (cbTxDisablePinUpdateRestartTx.Checked == true)
                data[0] |= 0x20;

            if (cbTxDisableRegUpdateRestartTx.Checked == true)
                data[0] |= 0x10;

            if (cbIgnoreTxFaultL3.Checked == true)
                data[0] |= 0x08;

            if (cbIgnoreTxFaultL2.Checked == true)
                data[0] |= 0x04;

            if (cbIgnoreTxFaultL1.Checked == true)
                data[0] |= 0x02;

            if (cbIgnoreTxFaultL0.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(devAddr, 0x1D, 1, data);
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

            data[0] = 0;

            if (cbLosHysteresisL3.SelectedIndex == 1)
                data[0] |= 0x80;

            if (cbLosHysteresisL2.SelectedIndex == 1)
                data[0] |= 0x40;

            if (cbLosHysteresisL1.SelectedIndex == 1)
                data[0] |= 0x20;

            if (cbLosHysteresisL0.SelectedIndex == 1)
                data[0] |= 0x10;

            if (cbDisableAutoSquelchL3.Checked == true)
                data[0] |= 0x08;

            if (cbDisableAutoSquelchL2.Checked == true)
                data[0] |= 0x04;

            if (cbDisableAutoSquelchL1.Checked == true)
                data[0] |= 0x02;

            if (cbDisableAutoSquelchL0.Checked == true)
                data[0] |= 0x01;

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

        private int _WriteAddr25()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbCtleL0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbCtleL1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbCtleL2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbCtleL3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x28, 1, data);
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

            if (cbOutputDisableL3.Checked == true)
                data[0] |= 0x08;

            if (cbOutputDisableL2.Checked == true)
                data[0] |= 0x04;

            if (cbOutputDisableL1.Checked == true)
                data[0] |= 0x02;

            if (cbOutputDisableL0.Checked == true)
                data[0] |= 0x01;

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

            bTmp = Convert.ToByte(cbIBiasL0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbIBiasL1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbIBiasL2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbIBiasL3.SelectedIndex);
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

            bTmp = Convert.ToByte(cbIModL0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbIModL1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x47, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr48()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbIModL2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x48, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr49()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbIModL3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x49, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr4A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (cbEnablePreFallL0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbPreFallL0.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x4A, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr4B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (cbEnablePreFallL1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbPreFallL1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x4B, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr4C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (cbEnablePreFallL2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbPreFallL2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x4C, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr4D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (cbEnablePreFallL3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbPreFallL3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x4D, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr4E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbTdeEyeShapingL3.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbTdeEyeShapingL2.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbTdeEyeShapingL1.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbTdeEyeShapingL0.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x4E, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr4F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbCrossingAdjL0.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x4F, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr50()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbCrossingAdjL1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x50, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr51()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbCrossingAdjL2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x51, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr52()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbCrossingAdjL3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x52, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr5E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbIBurnIn.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x5E, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr5F()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (cbBurnInL3.Checked == true)
                data[0] |= 0x80;

            if (cbBurnInL2.Checked == true)
                data[0] |= 0x40;

            if (cbBurnInL1.Checked == true)
                data[0] |= 0x20;

            if (cbBurnInL0.Checked == true)
                data[0] |= 0x10;

            if (cbBurnInEnable.Checked == true)
                data[0] |= 0x0B;

            rv = i2cWriteCB(devAddr, 0x5F, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr60()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbAdcSelect.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x60, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr62()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (cbAdcPowerDown.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(devAddr, 0x62, 1, data);
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

        private int _WriteAddr90()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (cbAtpEnable.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbAtpChannelSelect.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x90, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr92()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbProbeNodeSelect.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x92, 1, data);
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

        private void cbTxFaultAlarmMaskL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1B() < 0)
                return;
        }

        private void cbTxFaultAlarmMaskL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1B() < 0)
                return;
        }

        private void cbTxFaultAlarmMaskL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1B() < 0)
                return;
        }

        private void cbTxFaultAlarmMaskL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1B() < 0)
                return;
        }

        private void cbTxFaultAlarmL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1C() < 0)
                return;
        }

        private void cbTxFaultAlarmL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1C() < 0)
                return;
        }

        private void cbTxFaultAlarmL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1C() < 0)
                return;
        }

        private void cbTxFaultAlarmL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1C() < 0)
                return;
        }

        private void cbAlarmClearRestartTx_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D() < 0)
                return;
        }

        private void cbTxDisablePinUpdateRestartTx_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D() < 0)
                return;
        }

        private void cbTxDisableRegUpdateRestartTx_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D() < 0)
                return;
        }

        private void cbIgnoreTxFaultL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D() < 0)
                return;
        }

        private void cbIgnoreTxFaultL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D() < 0)
                return;
        }

        private void cbIgnoreTxFaultL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D() < 0)
                return;
        }

        private void cbIgnoreTxFaultL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D() < 0)
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

        private void cbDisableAutoSquelchL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22() < 0)
                return;
        }

        private void cbDisableAutoSquelchL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22() < 0)
                return;
        }

        private void cbDisableAutoSquelchL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22() < 0)
                return;
        }

        private void cbDisableAutoSquelchL3_CheckedChanged(object sender, EventArgs e)
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

        private void cbCtleL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr25() < 0)
                return;
        }

        private void cbCtleL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr26() < 0)
                return;
        }

        private void cbCtleL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr27() < 0)
                return;
        }

        private void cbCtleL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr28() < 0)
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

        private void cbOutputDisableL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr41() < 0)
                return;
        }

        private void cbOutputDisableL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr41() < 0)
                return;
        }

        private void cbOutputDisableL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr41() < 0)
                return;
        }

        private void cbOutputDisableL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr41() < 0)
                return;
        }

        private void cbIBiasL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr42() < 0)
                return;
        }

        private void cbIBiasL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr43() < 0)
                return;
        }

        private void cbIBiasL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr44() < 0)
                return;
        }

        private void cbIBiasL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr45() < 0)
                return;
        }

        private void cbIModL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr46() < 0)
                return;
        }

        private void cbIModL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr47() < 0)
                return;
        }

        private void cbIModL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr48() < 0)
                return;
        }

        private void cbIModL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr49() < 0)
                return;
        }

        private void cbEnablePreFallL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A() < 0)
                return;
        }

        private void cbPreFallL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A() < 0)
                return;
        }

        private void cbEnablePreFallL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4B() < 0)
                return;
        }

        private void cbPreFallL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4B() < 0)
                return;
        }

        private void cbEnablePreFallL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4C() < 0)
                return;
        }

        private void cbPreFallL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4C() < 0)
                return;
        }

        private void cbEnablePreFallL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4D() < 0)
                return;
        }

        private void cbPreFallL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4D() < 0)
                return;
        }

        private void cbTdeEyeShapingL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4E() < 0)
                return;
        }

        private void cbTdeEyeShapingL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4E() < 0)
                return;
        }

        private void cbTdeEyeShapingL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4E() < 0)
                return;
        }

        private void cbTdeEyeShapingL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4E() < 0)
                return;
        }

        private void cbCrossingAdjL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4F() < 0)
                return;
        }

        private void cbCrossingAdjL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr50() < 0)
                return;
        }

        private void cbCrossingAdjL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr51() < 0)
                return;
        }

        private void cbCrossingAdjL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr52() < 0)
                return;
        }

        private void cbIBurnIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr5E() < 0)
                return;
        }

        private void cbBurnInL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr5F() < 0)
                return;
        }

        private void cbBurnInL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr5F() < 0)
                return;
        }

        private void cbBurnInL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr5F() < 0)
                return;
        }

        private void cbBurnInL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr5F() < 0)
                return;
        }

        private void cbBurnInEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr5F() < 0)
                return;
        }

        private void cbAdcSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr60() < 0)
                return;
        }

        private void cbAdcPowerDown_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr62() < 0)
                return;
        }

        private void cbCdrLockThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr82() < 0)
                return;
        }

        private void cbAtpEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr90() < 0)
                return;
        }

        private void cbAtpChannelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr90() < 0)
                return;
        }

        private void cbProbeNodeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr92() < 0)
                return;
        }

        private void cbCtleAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte[] data = new byte[4];
            int rv;
            byte bTmp;

            if (reading == true)
                return;

            bTmp = data[0] = data[1] = data[2] = data[3] = 0;

            bTmp = Convert.ToByte(cbCtleAll.SelectedIndex);
            data[0] = data[1] = data[2] = data[3] = bTmp;

            rv = i2cWriteCB(devAddr, 0x25, 4, data);
        }

        private void cbIBiasAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte[] data = new byte[4];
            int rv;
            byte bTmp;

            if (reading == true)
                return;

            bTmp = data[0] = data[1] = data[2] = data[3] = 0;

            bTmp = Convert.ToByte(cbIBiasAll.SelectedIndex);
            data[0] = data[1] = data[2] = data[3] = bTmp;

            rv = i2cWriteCB(devAddr, 0x42, 4, data);
            if (rv < 0)
                return;

            return;
        }

        private void cbIModAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte[] data = new byte[4];
            int rv;
            byte bTmp;

            if (reading == true)
                return;

            bTmp = data[0] = data[1] = data[2] = data[3] = 0;

            bTmp = Convert.ToByte(cbIModAll.SelectedIndex);
            data[0] = data[1] = data[2] = data[3] = bTmp;

            rv = i2cWriteCB(devAddr, 0x46, 4, data);
            if (rv < 0)
                return;

            return;
        }

        private int _WriteAddr4A_4D()
        {
            byte[] data = new byte[4];
            int rv;
            byte bTmp;

            bTmp = data[0] = data[1] = data[2] = data[3] = 0;

            if (cbEnablePreFallAll.Checked == true) {
                data[0] |= 0x80;
                data[1] |= 0x80;
                data[2] |= 0x80;
                data[3] |= 0x80;
            }

            bTmp = Convert.ToByte(cbPreFallAll.SelectedIndex);
            data[0] |= bTmp;
            data[1] |= bTmp; 
            data[2] |= bTmp;
            data[3] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x4A, 4, data);
            if (rv < 0)
                return-1;

            return 0;
        }

        private void cbEnablePreFallAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4A_4D() < 0)
                return;
        }

        private void cbTdeEyeShapingAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte[] data = new byte[0];
            int rv;
            byte bTmp;

            if (reading == true)
                return;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbTdeEyeShapingAll.SelectedIndex);
            data[0] |= bTmp;
            bTmp <<= 2;
            data[0] |= bTmp;
            bTmp <<= 4;
            data[0] |= bTmp;
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x4E, 4, data);
            if (rv < 0)
                return;

            return;
        }

        private void cbCrossingAdjAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte[] data = new byte[4];
            int rv;
            byte bTmp;

            if (reading == true)
                return;

            bTmp = data[0] = data[1] = data[2] = data[3] = 0;

            bTmp = Convert.ToByte(cbCrossingAdjAll.SelectedIndex);
            data[0] = data[1] = data[2] = data[3] = bTmp;

            rv = i2cWriteCB(devAddr, 0x4F, 4, data);
            if (rv < 0)
                return;

            return;
        }
    }
}
