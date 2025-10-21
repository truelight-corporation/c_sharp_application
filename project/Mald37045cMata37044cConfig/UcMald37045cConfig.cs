using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

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
        private bool debugMode = false;

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

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL0InputEqualization0db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL0InputEqualization1db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL0InputEqualization2db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL0InputEqualization3db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL0InputEqualization4db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL0InputEqualization5db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL0InputEqualization6db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL0InputEqualization7db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL0InputEqualization8db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL0InputEqualization9db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL0InputEqualization10db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL0InputEqualizationReserved0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL0InputEqualizationReserved1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL1InputEqualization0db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL1InputEqualization1db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL1InputEqualization2db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL1InputEqualization3db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL1InputEqualization4db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL1InputEqualization5db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL1InputEqualization6db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL1InputEqualization7db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL1InputEqualization8db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL1InputEqualization9db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL1InputEqualization10db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL1InputEqualizationReserved0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL1InputEqualizationReserved1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL2InputEqualization0db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL2InputEqualization1db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL2InputEqualization2db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL2InputEqualization3db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL2InputEqualization4db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL2InputEqualization5db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL2InputEqualization6db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL2InputEqualization7db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL2InputEqualization8db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL2InputEqualization9db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL2InputEqualization10db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL2InputEqualizationReserved0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL2InputEqualizationReserved1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL3InputEqualization0db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL3InputEqualization1db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL3InputEqualization2db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL3InputEqualization3db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL3InputEqualization4db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL3InputEqualization5db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL3InputEqualization6db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL3InputEqualization7db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL3InputEqualization8db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL3InputEqualization9db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL3InputEqualization10db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL3InputEqualizationReserved0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbL3InputEqualizationReserved1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cbCtleAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cbCtleAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cbCtleAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cbCtleAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cbCtleAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cbCtleAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cbCtleAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cbCtleAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cbCtleAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cbCtleAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cbCtleAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cbCtleAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cbCtleAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cbCtleAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cbCtleAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cbCtleAll.Items.Add(item);

            for (i = 0, dTmp = 40; i < 128; i++, dTmp++) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "mA";
                item.Value = i;
                cbIBiasAll.Items.Add(item);
            }

            for (i = 0, dTmp = 1; i < 128; i++, dTmp++) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "mA";
                item.Value = i;
                cbIModAll.Items.Add(item);
            }

            for (i = 0; i < 16; i++) {
                item = new ComboboxItem();
                item.Text = i.ToString();
                item.Value = i;
                cbPreFallAll.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0:Minimum";
            item.Value = 0;
            cbTdeEyeShapingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Low";
            item.Value = 1;
            cbTdeEyeShapingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:Medium";
            item.Value = 2;
            cbTdeEyeShapingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:Maximum";
            item.Value = 3;
            cbTdeEyeShapingAll.Items.Add(item);

            for (i = 0, dTmp = 570; i < 8; i++, dTmp += 15) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "%";
                item.Value = i;
                cbCrossingAdjAll.Items.Add(item);
            }
            for (i = 8, dTmp = 450; i < 16; i++, dTmp += 15) {
                item = new ComboboxItem();
                item.Text = i + ":" + (dTmp / 10).ToString("F1") + "%";
                item.Value = i;
                cbCrossingAdjAll.Items.Add(item);
            }
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

        public int ReadAllApi()
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _ReadAll()));
            else
                return _ReadAll();
        }

        public int WriteAllApi()
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _WriteAll()));
            else
                return _WriteAll();
        }

        public string ReadAllRegisterApi()
        {
            if (this.InvokeRequired)
                return (string)this.Invoke(new Action(() => _ReadAllRegister()));
            else
                return _ReadAllRegister();
        }

        public int WriteAllRegisterApi(string targetPage, int delayTime, string registerFilePath)
        {
            if (this.InvokeRequired)
                 return (int)this.Invoke(new Action(() => _WriteAllRegister(targetPage, delayTime, registerFilePath)));
            else
                return _WriteAllRegister(targetPage, delayTime, registerFilePath);
        }
        
        public string GetChipId()
        {
            if (this.InvokeRequired)
                return (string)this.Invoke(new Action(() => _GetChipId()));
            else
                return _GetChipId();
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

        private string _GetChipId()
        {
            byte[] data = new byte[1];
            int rv;

            if (reading == true)
                return "";

            reading = true;

            try {
                if (i2cReadCB == null)
                    return "";

                rv = i2cReadCB(devAddr, 0x00, 1, data);
                if (rv != 1)
                    return "";


            }
            finally {
                reading = false;
            }

            return "0x" + data[0].ToString("X2");
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
            foreach (ComboboxItem item in cbCdrLockThreshold.Items) {
                if (item.Value == ((data & 0x30) >> 4)) {
                    cbCdrLockThreshold.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr90(byte data)
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

        private void _ParseAddr92(byte data)
        {
            foreach (ComboboxItem item in cbProbeNodeSelect.Items) {
                if (item.Value == (data & 0x0F)) {
                    cbProbeNodeSelect.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA0(byte data)
        {
            foreach (ComboboxItem item in cbL0InputEqualization0db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL0InputEqualization0db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA1(byte data)
        {
            foreach (ComboboxItem item in cbL0InputEqualization1db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL0InputEqualization1db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA2(byte data)
        {
            foreach (ComboboxItem item in cbL0InputEqualization2db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL0InputEqualization2db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA3(byte data)
        {
            foreach (ComboboxItem item in cbL0InputEqualization3db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL0InputEqualization3db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA4(byte data)
        {
            foreach (ComboboxItem item in cbL0InputEqualization4db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL0InputEqualization4db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA5(byte data)
        {
            foreach (ComboboxItem item in cbL0InputEqualization5db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL0InputEqualization5db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA6(byte data)
        {
            foreach (ComboboxItem item in cbL0InputEqualization6db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL0InputEqualization6db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA7(byte data)
        {
            foreach (ComboboxItem item in cbL0InputEqualization7db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL0InputEqualization7db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA8(byte data)
        {
            foreach (ComboboxItem item in cbL0InputEqualization8db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL0InputEqualization8db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrA9(byte data)
        {
            foreach (ComboboxItem item in cbL0InputEqualization9db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL0InputEqualization9db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrAA(byte data)
        {
            foreach (ComboboxItem item in cbL0InputEqualization10db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL0InputEqualization10db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrAB(byte data)
        {
            foreach (ComboboxItem item in cbL0InputEqualizationReserved0.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL0InputEqualizationReserved0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrAC(byte data)
        {
            foreach (ComboboxItem item in cbL0InputEqualizationReserved1.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL0InputEqualizationReserved1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrAD(byte data)
        {
            foreach (ComboboxItem item in cbL1InputEqualization0db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL1InputEqualization0db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrAE(byte data)
        {
            foreach (ComboboxItem item in cbL1InputEqualization1db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL1InputEqualization1db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrAF(byte data)
        {
            foreach (ComboboxItem item in cbL1InputEqualization2db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL1InputEqualization2db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB0(byte data)
        {
            foreach (ComboboxItem item in cbL1InputEqualization3db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL1InputEqualization3db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB1(byte data)
        {
            foreach (ComboboxItem item in cbL1InputEqualization4db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL1InputEqualization4db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB2(byte data)
        {
            foreach (ComboboxItem item in cbL1InputEqualization5db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL1InputEqualization5db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB3(byte data)
        {
            foreach (ComboboxItem item in cbL1InputEqualization6db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL1InputEqualization6db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB4(byte data)
        {
            foreach (ComboboxItem item in cbL1InputEqualization7db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL1InputEqualization7db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB5(byte data)
        {
            foreach (ComboboxItem item in cbL1InputEqualization8db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL1InputEqualization8db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB6(byte data)
        {
            foreach (ComboboxItem item in cbL1InputEqualization9db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL1InputEqualization9db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB7(byte data)
        {
            foreach (ComboboxItem item in cbL1InputEqualization10db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL1InputEqualization10db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB8(byte data)
        {
            foreach (ComboboxItem item in cbL1InputEqualizationReserved0.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL1InputEqualizationReserved0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrB9(byte data)
        {
            foreach (ComboboxItem item in cbL1InputEqualizationReserved1.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL1InputEqualizationReserved1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrBA(byte data)
        {
            foreach (ComboboxItem item in cbL2InputEqualization0db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL2InputEqualization0db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrBB(byte data)
        {
            foreach (ComboboxItem item in cbL2InputEqualization1db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL2InputEqualization1db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrBC(byte data)
        {
            foreach (ComboboxItem item in cbL2InputEqualization2db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL2InputEqualization2db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrBD(byte data)
        {
            foreach (ComboboxItem item in cbL2InputEqualization3db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL2InputEqualization3db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrBE(byte data)
        {
            foreach (ComboboxItem item in cbL2InputEqualization4db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL2InputEqualization4db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrBF(byte data)
        {
            foreach (ComboboxItem item in cbL2InputEqualization5db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL2InputEqualization5db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC0(byte data)
        {
            foreach (ComboboxItem item in cbL2InputEqualization6db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL2InputEqualization6db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC1(byte data)
        {
            foreach (ComboboxItem item in cbL2InputEqualization7db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL2InputEqualization7db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC2(byte data)
        {
            foreach (ComboboxItem item in cbL2InputEqualization8db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL2InputEqualization8db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC3(byte data)
        {
            foreach (ComboboxItem item in cbL2InputEqualization9db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL2InputEqualization9db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC4(byte data)
        {
            foreach (ComboboxItem item in cbL2InputEqualization10db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL2InputEqualization10db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC5(byte data)
        {
            foreach (ComboboxItem item in cbL2InputEqualizationReserved0.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL2InputEqualizationReserved0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC6(byte data)
        {
            foreach (ComboboxItem item in cbL2InputEqualizationReserved1.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL2InputEqualizationReserved1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC7(byte data)
        {
            foreach (ComboboxItem item in cbL3InputEqualization0db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL3InputEqualization0db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC8(byte data)
        {
            foreach (ComboboxItem item in cbL3InputEqualization1db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL3InputEqualization1db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrC9(byte data)
        {
            foreach (ComboboxItem item in cbL3InputEqualization2db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL3InputEqualization2db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrCA(byte data)
        {
            foreach (ComboboxItem item in cbL3InputEqualization3db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL3InputEqualization3db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrCB(byte data)
        {
            foreach (ComboboxItem item in cbL3InputEqualization4db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL3InputEqualization4db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrCC(byte data)
        {
            foreach (ComboboxItem item in cbL3InputEqualization5db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL3InputEqualization5db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrCD(byte data)
        {
            foreach (ComboboxItem item in cbL3InputEqualization6db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL3InputEqualization6db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrCE(byte data)
        {
            foreach (ComboboxItem item in cbL3InputEqualization7db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL3InputEqualization7db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrCF(byte data)
        {
            foreach (ComboboxItem item in cbL3InputEqualization8db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL3InputEqualization8db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrD0(byte data)
        {
            foreach (ComboboxItem item in cbL3InputEqualization9db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL3InputEqualization9db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrD1(byte data)
        {
            foreach (ComboboxItem item in cbL3InputEqualization10db.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL3InputEqualization10db.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrD2(byte data)
        {
            foreach (ComboboxItem item in cbL3InputEqualizationReserved0.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL3InputEqualizationReserved0.SelectedItem = item;
                }
            }
        }

        private void _ParseAddrD3(byte data)
        {
            foreach (ComboboxItem item in cbL3InputEqualizationReserved1.Items) {
                if (item.Value == (data & 0x7F)) {
                    cbL3InputEqualizationReserved1.SelectedItem = item;
                }
            }
        }

        private string _ReadAllRegister()
        {
            byte[] data1 = new byte[128];
            byte[] data2 = new byte[128];
            byte[] data = new byte[256];
            int rv;

            if (reading == true)
                return "";

            reading = true;

            try {
                if (i2cReadCB == null)
                    return "";

                rv = i2cReadCB(devAddr, 0x00, 128, data1);
                if (rv != 128)
                    return "";

                Thread.Sleep(10);

                rv = i2cReadCB(devAddr, 0x80, 128, data2);
                if (rv != 128)
                    return "";

                Buffer.BlockCopy(data1, 0, data, 0, data1.Length);
                Buffer.BlockCopy(data2, 0, data, data1.Length, data2.Length);
            }
            finally {
                reading = false;
            }

            return _FormatData(data);
        }

        private string _FormatData(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i += 16) {
                sb.Append($"\"Tx\",\"{i:X2}\"");
                for (int j = 0; j < 16; j++) {
                    if (i + j < data.Length) {
                        sb.Append($",\"{data[i + j]:X2}\"");
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private string _FormatData1(byte[] data)
        {
            HashSet<int> RxRegMap = new HashSet<int> {
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x10, 0x11, 0x16, 0x17,
                0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x21, 0x22, 0x23, 0x25,
                0x26, 0x27, 0x28, 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46,
                0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F, 0x50,
                0x51, 0x52, 0x5E, 0x5F, 0x60, 0x62, 0x68, 0x69, 0x82, 0x90,
                0x92
                };

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i += 16) {
                sb.Append($"\"Tx\",\"{i:X2}\"");
                for (int j = 0; j < 16; j++) {
                    if (i + j < data.Length) {
                        if (RxRegMap.Contains(i + j)) {
                            sb.Append($",\"{data[i + j]:X2}\"");
                        }
                        else {
                            sb.Append(",\"xx\"");
                        }
                    }
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private void bReadAll_Click(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            reading = true;
            bReadAll.Enabled = false;
            _ReadMultipleAddresses();
            reading = false;
            bReadAll.Enabled = true;
        }

        private int _ReadAll()
        {
            int result;

            if (reading == true)
                return -1;

            reading = true;
            bReadAll.Enabled = false;
            result = _ReadMultipleAddresses();
            reading = false;
            bReadAll.Enabled = true;
            return result;
        }

        private int _ReadMultipleAddresses()
        {
            byte[] data = new byte[52];
            int rv;

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

            rv = i2cReadCB(devAddr, 0xA0, 52, data);
            if (rv != 52)
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
            return 0;

        exit:
            return -1;
        }

        private int _WriteAllRegister(string targetPage, int delayTime, string registerFilePath)
        {
            string filePath;
            int bytesToRead = 256; // 128 or 256 bytes
            int bytesPerRow = 16; // 16 bytes per row
            int rowsToRead;
            List<string[]> dataToWrite;

            if (!string.IsNullOrEmpty(registerFilePath))
                filePath = registerFilePath;
            else {
                MessageBox.Show("The file path for the write operation is incorrect or empty.");
                return -1;
            }

            rowsToRead = bytesToRead / bytesPerRow;
            dataToWrite = ReadCsvData(filePath, targetPage, rowsToRead);

            if (debugMode) {
                MessageBox.Show("filePath: \n" + filePath +
                            "\n\ndataToWrite: \n" + FormatDataToWrite(dataToWrite));
            }

            if (dataToWrite == null)
                return -1;

            byte[] data = FormatDataForWrite(dataToWrite, bytesToRead);// Prepare data to be written as byte array

            if (debugMode) {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Data:");
                for (int i = 0; i < data.Length; i++) {
                    sb.Append(data[i].ToString("X2")); // Convert byte to hex string representation
                    sb.Append(" ");
                    if ((i + 1) % 16 == 0)
                        sb.AppendLine(); // New line every 16 bytes
                }

                MessageBox.Show(sb.ToString());
            }

            return PerformWriteOperation(data, delayTime);
        }

        private string FormatDataToWrite(List<string[]> dataToWrite)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var row in dataToWrite) {
                sb.AppendLine(string.Join(", ", row)); // 將每行陣列轉為逗號分隔的字串
            }

            return sb.ToString();
        }

        private List<string[]> ReadCsvData(string filePath, string targetPage, int rowsToRead)
        {
            List<string[]> dataToWrite = new List<string[]>();

            try {
                using (var reader = new StreamReader(filePath)) {
                    bool foundHeader = false;
                    bool foundTargetPage = false;
                    int rowsRead = 0;
                    string line;

                    while ((line = reader.ReadLine()) != null) {
                        if (!foundHeader) {
                            if (line.StartsWith("Page,Row")) {
                                foundHeader = true;
                            }
                            continue;
                        }

                        string[] parts = line.Split(',');
                        if (parts.Length >= 18) {
                            if (foundTargetPage) {
                                if (rowsRead < rowsToRead) {
                                    string[] rowData = new string[16];
                                    Array.Copy(parts, 2, rowData, 0, 16); // Copy only the data columns
                                    dataToWrite.Add(rowData);
                                    rowsRead++;
                                }
                                else {
                                    break; // Read enough rows
                                }
                            }
                            else if (parts[0].Trim('"') == targetPage) {
                                foundTargetPage = true;
                                string[] rowData = new string[16];
                                Array.Copy(parts, 2, rowData, 0, 16); // Copy only the data columns
                                dataToWrite.Add(rowData);
                                rowsRead++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Error reading CSV file: {ex.Message}");
                return null;
            }

            return dataToWrite;
        }

        private byte[] FormatDataForWrite(List<string[]> dataToWrite, int totalBytes)
        {
            byte[] data = new byte[totalBytes]; // Dynamic range for byte array

            try {
                int index = 0;

                foreach (var row in dataToWrite) {
                    foreach (var value in row) {
                        data[index++] = Convert.ToByte(value.Trim('"'), 16);
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Error formatting data for write: {ex.Message}");
                return null;
            }

            return data;
        }

        private int PerformWriteOperation(byte[] data, int delayTime)
        {
            byte[] key = new byte[1] { 0xA0 };

            if (i2cWriteCB == null)
                return -1;

            if (i2cWriteCB(devAddr, 0x00, 128, data.Take(128).ToArray()) < 0)
                return -1;

            Thread.Sleep(delayTime); 

            if (i2cWriteCB(devAddr, 0x80, 128, data.Skip(128).Take(128).ToArray()) < 0)
                return -1;
            /*
            //bStoreIntoFlash_Click
            reading = true;
            i2cWriteCB(devAddr, 0xFA, 1, key);
            Thread.Sleep(1000);
            reading = false;
            */
            return 0;
        }

        private int PerformWriteOperation1(byte[] data)
        {
            //byte starAddr = 0;


            if (i2cWriteCB == null)
                return -1;
            /*
            //if (i2cWriteCB(80, starAddr, 128, data) < 0)

            i2cWriteCB(devAddr, 0x02, 1, data[2]);

            i2cWriteCB(devAddr, 0x03, 1, data[3]);
            */
            return 0;
        }

        private int _WriteAll()
        {
            if (_WriteAddr03() < 0)
                return -1;
            if (_WriteAddr04() < 0)
                return -1;
            if (_WriteAddr05() < 0)
                return -1;
            if (_WriteAddr10() < 0)
                return -1;
            if (_WriteAddr11() < 0)
                return -1;
            if (_WriteAddr16() < 0)
                return -1;
            if (_WriteAddr19() < 0)
                return -1;
            if (_WriteAddr1B() < 0)
                return -1;
            if (_WriteAddr1C() < 0)
                return -1;
            if (_WriteAddr1D() < 0)
                return -1;
            if (_WriteAddr21() < 0)
                return -1;
            if (_WriteAddr22() < 0)
                return -1;
            if (_WriteAddr23() < 0)
                return -1;
            if (_WriteAddr25() < 0)
                return -1;
            if (_WriteAddr26() < 0)
                return -1;
            if (_WriteAddr27() < 0)
                return -1;
            if (_WriteAddr28() < 0)
                return -1;
            if (_WriteAddr40() < 0)
                return -1;
            if (_WriteAddr41() < 0)
                return -1;
            if (_WriteAddr42() < 0)
                return -1;
            if (_WriteAddr43() < 0)
                return -1;
            if (_WriteAddr44() < 0)
                return -1;
            if (_WriteAddr45() < 0)
                return -1;
            if (_WriteAddr46() < 0)
                return -1;
            if (_WriteAddr47() < 0)
                return -1;
            if (_WriteAddr48() < 0)
                return -1;
            if (_WriteAddr49() < 0)
                return -1;
            if (_WriteAddr4A() < 0)
                return -1;
            if (_WriteAddr4B() < 0)
                return -1;
            if (_WriteAddr4C() < 0)
                return -1;
            if (_WriteAddr4D() < 0)
                return -1;
            if (_WriteAddr4E() < 0)
                return -1;
            if (_WriteAddr4F() < 0)
                return -1;
            if (_WriteAddr50() < 0)
                return -1;
            if (_WriteAddr51() < 0)
                return -1;
            if (_WriteAddr52() < 0)
                return -1;
            if (_WriteAddr5E() < 0)
                return -1;
            if (_WriteAddr5F() < 0)
                return -1;
            if (_WriteAddr60() < 0)
                return -1;
            if (_WriteAddr62() < 0)
                return -1;
            if (_WriteAddr82() < 0)
                return -1;
            if (_WriteAddr90() < 0)
                return -1;
            if (_WriteAddr92() < 0)
                return -1;
            //if (_WriteAddr4A_4D() < 0)
            //  return -1;
            if (_WriteAddrA0() < 0)
                return -1;
            if (_WriteAddrA1() < 0)
                return -1;
            if (_WriteAddrA2() < 0)
                return -1;
            if (_WriteAddrA3() < 0)
                return -1;
            if (_WriteAddrA4() < 0)
                return -1;
            if (_WriteAddrA5() < 0)
                return -1;
            if (_WriteAddrA6() < 0)
                return -1;
            if (_WriteAddrA7() < 0)
                return -1;
            if (_WriteAddrA8() < 0)
                return -1;
            if (_WriteAddrA9() < 0)
                return -1;
            if (_WriteAddrAA() < 0)
                return -1;
            if (_WriteAddrAB() < 0)
                return -1;
            if (_WriteAddrAC() < 0)
                return -1;
            if (_WriteAddrAD() < 0)
                return -1;
            if (_WriteAddrAE() < 0)
                return -1;
            if (_WriteAddrAF() < 0)
                return -1;
            if (_WriteAddrB0() < 0)
                return -1;
            if (_WriteAddrB1() < 0)
                return -1;
            if (_WriteAddrB2() < 0)
                return -1;
            if (_WriteAddrB3() < 0)
                return -1;
            if (_WriteAddrB4() < 0)
                return -1;
            if (_WriteAddrB5() < 0)
                return -1;
            if (_WriteAddrB6() < 0)
                return -1;
            if (_WriteAddrB7() < 0)
                return -1;
            if (_WriteAddrB8() < 0)
                return -1;
            if (_WriteAddrB9() < 0)
                return -1;
            if (_WriteAddrBA() < 0)
                return -1;
            if (_WriteAddrBB() < 0)
                return -1;
            if (_WriteAddrBC() < 0)
                return -1;
            if (_WriteAddrBD() < 0)
                return -1;
            if (_WriteAddrBE() < 0)
                return -1;
            if (_WriteAddrBF() < 0)
                return -1;
            if (_WriteAddrC0() < 0)
                return -1;
            if (_WriteAddrC1() < 0)
                return -1;
            if (_WriteAddrC2() < 0)
                return -1;
            if (_WriteAddrC3() < 0)
                return -1;
            if (_WriteAddrC4() < 0)
                return -1;
            if (_WriteAddrC5() < 0)
                return -1;
            if (_WriteAddrC6() < 0)
                return -1;
            if (_WriteAddrC7() < 0)
                return -1;
            if (_WriteAddrC8() < 0)
                return -1;
            if (_WriteAddrC9() < 0)
                return -1;
            if (_WriteAddrCA() < 0)
                return -1;
            if (_WriteAddrCB() < 0)
                return -1;
            if (_WriteAddrCC() < 0)
                return -1;
            if (_WriteAddrCD() < 0)
                return -1;
            if (_WriteAddrCE() < 0)
                return -1;
            if (_WriteAddrCF() < 0)
                return -1;
            if (_WriteAddrD0() < 0)
                return -1;
            if (_WriteAddrD1() < 0)
                return -1;
            if (_WriteAddrD2() < 0)
                return -1;
            if (_WriteAddrD3() < 0)
                return -1;

            return 0;
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

        private int _WriteAddrA0()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbL0InputEqualization0db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL0InputEqualization1db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL0InputEqualization2db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL0InputEqualization3db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL0InputEqualization4db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL0InputEqualization5db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL0InputEqualization6db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL0InputEqualization7db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL0InputEqualization8db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL0InputEqualization9db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL0InputEqualization10db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL0InputEqualizationReserved0.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL0InputEqualizationReserved1.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL1InputEqualization0db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL1InputEqualization1db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL1InputEqualization2db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL1InputEqualization3db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL1InputEqualization4db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL1InputEqualization5db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL1InputEqualization6db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL1InputEqualization7db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL1InputEqualization8db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL1InputEqualization9db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL1InputEqualization10db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL1InputEqualizationReserved0.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL1InputEqualizationReserved1.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL2InputEqualization0db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL2InputEqualization1db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL2InputEqualization2db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL2InputEqualization3db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL2InputEqualization4db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL2InputEqualization5db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL2InputEqualization6db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL2InputEqualization7db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL2InputEqualization8db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL2InputEqualization9db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL2InputEqualization10db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL2InputEqualizationReserved0.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL2InputEqualizationReserved1.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL3InputEqualization0db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL3InputEqualization1db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL3InputEqualization2db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL3InputEqualization3db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL3InputEqualization4db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL3InputEqualization5db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL3InputEqualization6db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL3InputEqualization7db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL3InputEqualization8db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL3InputEqualization9db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL3InputEqualization10db.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL3InputEqualizationReserved0.SelectedIndex);
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
            bTmp = Convert.ToByte(cbL3InputEqualizationReserved1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xD3, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbL0InputEqualization0db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA0() < 0)
                return;
        }

        private void cbL0InputEqualization1db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA1() < 0)
                return;
        }

        private void cbL0InputEqualization2db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA2() < 0)
                return;
        }

        private void cbL0InputEqualization3db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA3() < 0)
                return;
        }

        private void cbL0InputEqualization4db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA4() < 0)
                return;
        }

        private void cbL0InputEqualization5db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA5() < 0)
                return;
        }

        private void cbL0InputEqualization6db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA6() < 0)
                return;
        }

        private void cbL0InputEqualization7db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA7() < 0)
                return;
        }

        private void cbL0InputEqualization8db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA8() < 0)
                return;
        }

        private void cbL0InputEqualization9db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA9() < 0)
                return;
        }

        private void cbL0InputEqualization10db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrAA() < 0)
                return;
        }

        private void cbL0InputEqualizationReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrAB() < 0)
                return;
        }

        private void cbL0InputEqualizationReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrAC() < 0)
                return;
        }

        private void cbL1InputEqualization0db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrAD() < 0)
                return;
        }

        private void cbL1InputEqualization1db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrAE() < 0)
                return;
        }

        private void cbL1InputEqualization2db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrAF() < 0)
                return;
        }

        private void cbL1InputEqualization3db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB0() < 0)
                return;
        }

        private void cbL1InputEqualization4db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB1() < 0)
                return;
        }

        private void cbL1InputEqualization5db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB2() < 0)
                return;
        }

        private void cbL1InputEqualization6db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB3() < 0)
                return;
        }

        private void cbL1InputEqualization7db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB4() < 0)
                return;
        }

        private void cbL1InputEqualization8db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB5() < 0)
                return;
        }

        private void cbL1InputEqualization9db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB6() < 0)
                return;
        }

        private void cbL1InputEqualization10db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB7() < 0)
                return;
        }

        private void cbL1InputEqualizationReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB8() < 0)
                return;
        }

        private void cbL1InputEqualizationReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB9() < 0)
                return;
        }

        private void cbL2InputEqualization0db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBA() < 0)
                return;
        }

        private void cbL2InputEqualization1db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBB() < 0)
                return;
        }

        private void cbL2InputEqualization2db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBC() < 0)
                return;
        }

        private void cbL2InputEqualization3db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBD() < 0)
                return;
        }

        private void cbL2InputEqualization4db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBE() < 0)
                return;
        }

        private void cbL2InputEqualization5db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBF() < 0)
                return;
        }

        private void cbL2InputEqualization6db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC0() < 0)
                return;
        }

        private void cbL2InputEqualization7db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC1() < 0)
                return;
        }

        private void cbL2InputEqualization8db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC2() < 0)
                return;
        }

        private void cbL2InputEqualization9db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC3() < 0)
                return;
        }

        private void cbL2InputEqualization10db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC4() < 0)
                return;
        }

        private void cbL2InputEqualizationReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC5() < 0)
                return;
        }

        private void cbL2InputEqualizationReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC6() < 0)
                return;
        }

        private void cbL3InputEqualization0db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC7() < 0)
                return;
        }

        private void cbL3InputEqualization1db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC8() < 0)
                return;
        }

        private void cbL3InputEqualization2db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC9() < 0)
                return;
        }

        private void cbL3InputEqualization3db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCA() < 0)
                return;
        }

        private void cbL3InputEqualization4db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCB() < 0)
                return;
        }

        private void cbL3InputEqualization5db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCC() < 0)
                return;
        }

        private void cbL3InputEqualization6db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCD() < 0)
                return;
        }

        private void cbL3InputEqualization7db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCE() < 0)
                return;
        }

        private void cbL3InputEqualization8db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCF() < 0)
                return;
        }

        private void cbL3InputEqualization9db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD0() < 0)
                return;
        }

        private void cbL3InputEqualization10db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD1() < 0)
                return;
        }

        private void cbL3InputEqualizationReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD2() < 0)
                return;
        }

        private void cbL3InputEqualizationReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD3() < 0)
                return;
        }
    }
}
