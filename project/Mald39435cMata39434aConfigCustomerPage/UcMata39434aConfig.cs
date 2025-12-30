using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Mald39435Mata39434aConfig
{
    public partial class UcMata39434aConfig : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private const byte devAddr = 16;   //// 設定裝置位址.
        private static readonly byte[] Page00 = { 0x00 };
        private static readonly byte[] Page1F = { 0x1F };
        private static readonly byte[] Page20 = { 0x20 };
        private static readonly byte[] Page21 = { 0x21 };
        private static readonly byte[] Page22 = { 0x22 };
        private static readonly byte[] Page23 = { 0x23 };

        // 新增Customer Page SFF-8636 CTLE function 2025.06.04
        private static readonly byte[] PageAA = { 0xAA };

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private bool reading = false;

        public UcMata39434aConfig()
        {
            ComboboxItem item;
            

            InitializeComponent();
            #region Page L0

            // ComboBox AGCAaptationModeL0 Addr = 0x20:0xC2[0]
            cbAGCAdaptationModeL0.Items.Add(new ComboboxItem { Text = "Manual", Value = 0 });
            cbAGCAdaptationModeL0.Items.Add(new ComboboxItem { Text = "Adaptive", Value = 1 });
            // ComboBox AGCValueL0 Addr = 0x20:0xC1[2:0] 0xC0[7:0]
            for (int i = 0; i < 8; i++) // C1[2:0]
            {
                for (int j = 0; j < 256; j++) // C0[7:0]
                {
                    int value = (i << 8) | j;
                    if (value <= 2016)
                    {
                        cbAGCValueL0.Items.Add(new ComboboxItem
                        {
                            Text = value.ToString(),
                            Value = value
                        });
                    }
                }
            }
            // ComboBox ManualVGA0FilterL0 Addr = 0x20:0x03[5:0]
            for (int j = 0; j < 64; j++) // C0[5:0]
            {
                cbManualVGA0FilterL0.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox ManualVGA1FilterL0 Addr = 0x20:0x04[5:0]
            for (int j = 0; j < 64; j++)
            {
                cbManualVGA1FilterL0.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox ManualVGA2FilterL0 Addr = 0x20:0x05[5:0]
            for (int j = 0; j < 64; j++)
            {
                cbManualVGA2FilterL0.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox TIABWCtrlSwingL0 Addr = 0x20:0x0D[5:0]
            for (int j = 0; j < 64; j++)
            {
                cbTIABWCtrlSwingL0.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox LOSDeassertthresholdL0 Addr = 0x20:0x1A[3:0]
            item = new ComboboxItem();
            item.Text = "33uA";
            item.Value = 0;
            cbLOSDeassertthresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "36uA";
            item.Value = 1;
            cbLOSDeassertthresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "40uA";
            item.Value = 2;
            cbLOSDeassertthresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "43uA";
            item.Value = 3;
            cbLOSDeassertthresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "47uA";
            item.Value = 4;
            cbLOSDeassertthresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "50uA";
            item.Value = 5;
            cbLOSDeassertthresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "54uA";
            item.Value = 6;
            cbLOSDeassertthresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "57uA";
            item.Value = 7;
            cbLOSDeassertthresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10uA";
            item.Value = 8;
            cbLOSDeassertthresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13uA";
            item.Value = 9;
            cbLOSDeassertthresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "17uA";
            item.Value = 10;
            cbLOSDeassertthresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "20uA";
            item.Value = 11;
            cbLOSDeassertthresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "23uA";
            item.Value = 12;
            cbLOSDeassertthresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "27uA";
            item.Value = 13;
            cbLOSDeassertthresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "30uA";
            item.Value = 14;
            cbLOSDeassertthresholdL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "33uA";
            item.Value = 15;
            cbLOSDeassertthresholdL0.Items.Add(item);

            // ComboBox RSSILOSHysteresisL0 Addr = 0x20:0x19[3:2]
            item = new ComboboxItem();
            item.Text = "1dB";
            item.Value = 0;
            cbRSSILOSHysteresisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2dB";
            item.Value = 1;
            cbRSSILOSHysteresisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3dB";
            item.Value = 2;
            cbRSSILOSHysteresisL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4dB";
            item.Value = 3;
            cbRSSILOSHysteresisL0.Items.Add(item);

            // ComboBox SquelchOnLOSL0 Addr = 0x20:0x19[7]
            cbSquelchOnLOSL0.Items.Add(new ComboboxItem { Text = "Checked", Value = 0 });
            cbSquelchOnLOSL0.Items.Add(new ComboboxItem { Text = "Onchecked", Value = 1 });
            // ComboBox BiasAdaptationModeL0 Addr = 0x20:0xE2[0]
            cbBiasAdaptationModeL0.Items.Add(new ComboboxItem { Text = "Manual", Value = 0 });
            cbBiasAdaptationModeL0.Items.Add(new ComboboxItem { Text = "Adaptive", Value = 1 });
            // ComboBox BiasValueL0 Addr = 0x20:0xE1[2:0] 0xE0[7:0]
            for (int i = 0; i < 8; i++) // C1[2:0]
            {
                for (int j = 0; j < 256; j++) // C0[7:0]
                {
                    int value = (i << 8) | j;
                    if (value <= 1008)
                    {
                        cbBiasValueL0.Items.Add(new ComboboxItem
                        {
                            Text = value.ToString(),
                            Value = value
                        });
                    }
                }
            }
            // ComboBox OutputPreEmphasisL0 Addr = 0x20:0x00[7:2]
            double totalRange = 1.5;
            int divisions = 64;
            double stepSize = totalRange / (divisions-1);

            for (int i = 0; i < divisions; i++)
            {
                double value = i * stepSize;
                cbOutputPreEmphasisL0.Items.Add(new ComboboxItem
                {
                    Text = $"{value:F3} dB",
                    Value = i
                });
            }
            // ComboBox TargetOutputSwingL0 Addr = 0x20:0x02[3:0]
            item = new ComboboxItem();
            item.Text = "54%";
            item.Value = 0;
            cbTargetOutputSwingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "61%";
            item.Value = 1;
            cbTargetOutputSwingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "67%";
            item.Value = 2;
            cbTargetOutputSwingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "74%";
            item.Value = 3;
            cbTargetOutputSwingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "80%";
            item.Value = 4;
            cbTargetOutputSwingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "87%";
            item.Value = 5;
            cbTargetOutputSwingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "93%";
            item.Value = 6;
            cbTargetOutputSwingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "100%";
            item.Value = 7;
            cbTargetOutputSwingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "107%";
            item.Value = 8;
            cbTargetOutputSwingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "113%";
            item.Value = 9;
            cbTargetOutputSwingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "120%";
            item.Value = 10;
            cbTargetOutputSwingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "126%";
            item.Value = 11;
            cbTargetOutputSwingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "133%";
            item.Value = 12;
            cbTargetOutputSwingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "139%";
            item.Value = 13;
            cbTargetOutputSwingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "146%";
            item.Value = 14;
            cbTargetOutputSwingL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "152%";
            item.Value = 15;
            cbTargetOutputSwingL0.Items.Add(item);

            // ComboBox TIARegulatorsRefL0 Addr = 0x20:0x01[5:3]
            item = new ComboboxItem();
            item.Text = "2.23V";
            item.Value = 0;
            cbTIARegulatorsRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.3V";
            item.Value = 1;
            cbTIARegulatorsRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.36V";
            item.Value = 2;
            cbTIARegulatorsRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.43V";
            item.Value = 3;
            cbTIARegulatorsRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.5V";
            item.Value = 4;
            cbTIARegulatorsRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.57V";
            item.Value = 5;
            cbTIARegulatorsRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.63V";
            item.Value = 6;
            cbTIARegulatorsRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.7V";
            item.Value = 7;
            cbTIARegulatorsRefL0.Items.Add(item);

            // ComboBox VGARegulatorsRefL0 Addr = 0x20:0x01[2:0]
            item = new ComboboxItem();
            item.Text = "2.6V";
            item.Value = 0;
            cbVGARegulatorsRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.63V";
            item.Value = 1;
            cbVGARegulatorsRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.67V";
            item.Value = 2;
            cbVGARegulatorsRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.7V";
            item.Value = 3;
            cbVGARegulatorsRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.74V";
            item.Value = 4;
            cbVGARegulatorsRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.77V";
            item.Value = 5;
            cbVGARegulatorsRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.81V";
            item.Value = 6;
            cbVGARegulatorsRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.84V";
            item.Value = 7;
            cbVGARegulatorsRefL0.Items.Add(item);

            // ComboBox DisableVREGTIAAutoSweepL0 Addr = 0x20:0xOC[7]
            cbDisableVREGTIAAutoSweepL0.Items.Add(new ComboboxItem { Text = "Checked", Value = 1 });
            cbDisableVREGTIAAutoSweepL0.Items.Add(new ComboboxItem { Text = "Onchecked", Value = 0 });

            // ComboBox DisableVREGVGAAutoSweepL0 Addr = 0x20:0xOC[6]
            cbDisableVREGVGAAutoSweepL0.Items.Add(new ComboboxItem { Text = "Checked", Value = 1 });
            cbDisableVREGVGAAutoSweepL0.Items.Add(new ComboboxItem { Text = "Onchecked", Value = 0 });

            // ComboBox TIARegSlopeCTRLL0 Addr = 0x20:0xO8[7:6]
            item = new ComboboxItem();
            item.Text = "2.65V";
            item.Value = 0;
            cbTIARegSlopeCTRLL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.7V";
            item.Value = 1;
            cbTIARegSlopeCTRLL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.75V";
            item.Value = 2;
            cbTIARegSlopeCTRLL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.8V";
            item.Value = 3;
            cbTIARegSlopeCTRLL0.Items.Add(item);

            // ComboBox VGARegSlopeCTRLL0 Addr = 0x20:0xO8[5:4]
            item = new ComboboxItem();
            item.Text = "2.85V";
            item.Value = 0;
            cbVGARegSlopeCTRLL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.9V";
            item.Value = 1;
            cbVGARegSlopeCTRLL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.95V";
            item.Value = 2;
            cbVGARegSlopeCTRLL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3V";
            item.Value = 3;
            cbVGARegSlopeCTRLL0.Items.Add(item);

            // ComboBox DCDAdaptationModeL0 Addr = 0x20:0xD2[0]
            cbDCDAdaptationModeL0.Items.Add(new ComboboxItem { Text = "Manual", Value = 0 });
            cbDCDAdaptationModeL0.Items.Add(new ComboboxItem { Text = "Adaptive", Value = 1 });

            // ComboBox DCDValueL0 Addr = 0x20:0xD1[2:0] 0xD0[7:0]
            for (int i = 0; i < 8; i++) // D1[2:0]
            {
                for (int j = 0; j < 256; j++) // D0[7:0]
                {
                    int value = (i << 8) | j;
                    if (value <= 256)
                    {
                        cbDCDValueL0.Items.Add(new ComboboxItem
                        {
                            Text = value.ToString(),
                            Value = value
                        });
                    }
                }
            }

            // ComboBox cbBGlpolyCodeL0 Addr = 0x20:0x0B[7:4]
            for (int j = 0; j < 16; j++)
            {
                cbBGlpolyCodeL0.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }

            // ComboBox TuneRefL0 Addr = 0x20:0x0A[4:1]
            item = new ComboboxItem();
            item.Text = "2.078V";
            item.Value = 0;
            cbTuneRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.111V";
            item.Value = 1;
            cbTuneRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.143V";
            item.Value = 2;
            cbTuneRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.176V";
            item.Value = 3;
            cbTuneRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.208V";
            item.Value = 4;
            cbTuneRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.241V";
            item.Value = 5;
            cbTuneRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.273V";
            item.Value = 6;
            cbTuneRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.306V";
            item.Value = 7;
            cbTuneRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.847V";
            item.Value = 8;
            cbTuneRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.879V";
            item.Value = 9;
            cbTuneRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.912V";
            item.Value = 10;
            cbTuneRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.945V";
            item.Value = 11;
            cbTuneRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.978V";
            item.Value = 12;
            cbTuneRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.011V";
            item.Value = 13;
            cbTuneRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.044V";
            item.Value = 14;
            cbTuneRefL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.078V";
            item.Value = 15;
            cbTuneRefL0.Items.Add(item);

            // ComboBox cbSelRefLowL0 Addr = 0x20:0xOA[0]
            cbSelRefLowL0.Items.Add(new ComboboxItem { Text = "VCC_EXT-0.3V", Value = 0 });
            cbSelRefLowL0.Items.Add(new ComboboxItem { Text = "Low PD Bias", Value = 1 });

            #endregion

            #region Page L1

            // ComboBox AGCAaptationModeL1 Addr = 0x21:0xC2[0]
            cbAGCAdaptationModeL1.Items.Add(new ComboboxItem { Text = "Manual", Value = 0 });
            cbAGCAdaptationModeL1.Items.Add(new ComboboxItem { Text = "Adaptive", Value = 1 });
            // ComboBox AGCValueL1 Addr = 0x21:0xC1[2:0] 0xC0[7:0]
            for (int i = 0; i < 8; i++) // C1[2:0]
            {
                for (int j = 0; j < 256; j++) // C0[7:0]
                {
                    int value = (i << 8) | j;
                    if (value <= 2016)
                    {
                        cbAGCValueL1.Items.Add(new ComboboxItem
                        {
                            Text = value.ToString(),
                            Value = value
                        });
                    }
                }
            }
            // ComboBox ManualVGA0FilterL1 Addr = 0x21:0x03[5:0]
            for (int j = 0; j < 64; j++) // C0[5:0]
            {
                cbManualVGA0FilterL1.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox ManualVGA1FilterL1 Addr = 0x21:0x04[5:0]
            for (int j = 0; j < 64; j++)
            {
                cbManualVGA1FilterL1.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox ManualVGA2FilterL1 Addr = 0x21:0x05[5:0]
            for (int j = 0; j < 64; j++)
            {
                cbManualVGA2FilterL1.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox TIABWCtrlSwingL1 Addr = 0x21:0x0D[5:0]
            for (int j = 0; j < 64; j++)
            {
                cbTIABWCtrlSwingL1.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox LOSDeassertthresholdL1 Addr = 0x21:0x1A[3:0]
            item = new ComboboxItem();
            item.Text = "33uA";
            item.Value = 0;
            cbLOSDeassertthresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "36uA";
            item.Value = 1;
            cbLOSDeassertthresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "40uA";
            item.Value = 2;
            cbLOSDeassertthresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "43uA";
            item.Value = 3;
            cbLOSDeassertthresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "47uA";
            item.Value = 4;
            cbLOSDeassertthresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "50uA";
            item.Value = 5;
            cbLOSDeassertthresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "54uA";
            item.Value = 6;
            cbLOSDeassertthresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "57uA";
            item.Value = 7;
            cbLOSDeassertthresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10uA";
            item.Value = 8;
            cbLOSDeassertthresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13uA";
            item.Value = 9;
            cbLOSDeassertthresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "17uA";
            item.Value = 10;
            cbLOSDeassertthresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "20uA";
            item.Value = 11;
            cbLOSDeassertthresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "23uA";
            item.Value = 12;
            cbLOSDeassertthresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "27uA";
            item.Value = 13;
            cbLOSDeassertthresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "30uA";
            item.Value = 14;
            cbLOSDeassertthresholdL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "33uA";
            item.Value = 15;
            cbLOSDeassertthresholdL1.Items.Add(item);

            // ComboBox RSSILOSHysteresisL1 Addr = 0x21:0x19[3:2]
            item = new ComboboxItem();
            item.Text = "1dB";
            item.Value = 0;
            cbRSSILOSHysteresisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2dB";
            item.Value = 1;
            cbRSSILOSHysteresisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3dB";
            item.Value = 2;
            cbRSSILOSHysteresisL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4dB";
            item.Value = 3;
            cbRSSILOSHysteresisL1.Items.Add(item);

            // ComboBox SquelchOnLOSL1 Addr = 0x21:0x19[7]
            cbSquelchOnLOSL1.Items.Add(new ComboboxItem { Text = "Checked", Value = 0 });
            cbSquelchOnLOSL1.Items.Add(new ComboboxItem { Text = "Onchecked", Value = 1 });
            // ComboBox BiasAaptationModeL1 Addr = 0x21:0xE2[0]
            cbBiasAdaptationModeL1.Items.Add(new ComboboxItem { Text = "Manual", Value = 0 });
            cbBiasAdaptationModeL1.Items.Add(new ComboboxItem { Text = "Adaptive", Value = 1 });
            // ComboBox BiasValueL1 Addr = 0x21:0xE1[2:0] 0xE0[7:0]
            for (int i = 0; i < 8; i++) // C1[2:0]
            {
                for (int j = 0; j < 256; j++) // C0[7:0]
                {
                    int value = (i << 8) | j;
                    if (value <= 1008)
                    {
                        cbBiasValueL1.Items.Add(new ComboboxItem
                        {
                            Text = value.ToString(),
                            Value = value
                        });
                    }
                }
            }
            // ComboBox OutputPreEmphasisL1 Addr = 0x21:0x00[7:2]
            for (int i = 0; i < divisions; i++)
            {
                double value = i * stepSize;
                cbOutputPreEmphasisL1.Items.Add(new ComboboxItem
                {
                    Text = $"{value:F3} dB",
                    Value = i
                });
            }
            // ComboBox TargetOutputSwingL1 Addr = 0x21:0x02[3:0]
            item = new ComboboxItem();
            item.Text = "54%";
            item.Value = 0;
            cbTargetOutputSwingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "61%";
            item.Value = 1;
            cbTargetOutputSwingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "67%";
            item.Value = 2;
            cbTargetOutputSwingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "74%";
            item.Value = 3;
            cbTargetOutputSwingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "80%";
            item.Value = 4;
            cbTargetOutputSwingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "87%";
            item.Value = 5;
            cbTargetOutputSwingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "93%";
            item.Value = 6;
            cbTargetOutputSwingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "100%";
            item.Value = 7;
            cbTargetOutputSwingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "107%";
            item.Value = 8;
            cbTargetOutputSwingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "113%";
            item.Value = 9;
            cbTargetOutputSwingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "120%";
            item.Value = 10;
            cbTargetOutputSwingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "126%";
            item.Value = 11;
            cbTargetOutputSwingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "133%";
            item.Value = 12;
            cbTargetOutputSwingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "139%";
            item.Value = 13;
            cbTargetOutputSwingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "146%";
            item.Value = 14;
            cbTargetOutputSwingL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "152%";
            item.Value = 15;
            cbTargetOutputSwingL1.Items.Add(item);

            // ComboBox TIARegulatorsRefL1 Addr = 0x21:0x01[5:3]
            item = new ComboboxItem();
            item.Text = "2.23V";
            item.Value = 0;
            cbTIARegulatorsRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.3V";
            item.Value = 1;
            cbTIARegulatorsRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.36V";
            item.Value = 2;
            cbTIARegulatorsRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.43V";
            item.Value = 3;
            cbTIARegulatorsRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.5V";
            item.Value = 4;
            cbTIARegulatorsRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.57V";
            item.Value = 5;
            cbTIARegulatorsRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.63V";
            item.Value = 6;
            cbTIARegulatorsRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.7V";
            item.Value = 7;
            cbTIARegulatorsRefL1.Items.Add(item);

            // ComboBox VGARegulatorsRefL1 Addr = 0x21:0x01[2:0]
            item = new ComboboxItem();
            item.Text = "2.6V";
            item.Value = 0;
            cbVGARegulatorsRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.63V";
            item.Value = 1;
            cbVGARegulatorsRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.67V";
            item.Value = 2;
            cbVGARegulatorsRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.7V";
            item.Value = 3;
            cbVGARegulatorsRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.74V";
            item.Value = 4;
            cbVGARegulatorsRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.77V";
            item.Value = 5;
            cbVGARegulatorsRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.81V";
            item.Value = 6;
            cbVGARegulatorsRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.84V";
            item.Value = 7;
            cbVGARegulatorsRefL1.Items.Add(item);

            // ComboBox DisableVREGTIAAutoSweepL1 Addr = 0x21:0xOC[7]
            cbDisableVREGTIAAutoSweepL1.Items.Add(new ComboboxItem { Text = "Checked", Value = 0 });
            cbDisableVREGTIAAutoSweepL1.Items.Add(new ComboboxItem { Text = "Onchecked", Value = 1 });

            // ComboBox DisableVREGVGAAutoSweepL1 Addr = 0x21:0xOC[6]
            cbDisableVREGVGAAutoSweepL1.Items.Add(new ComboboxItem { Text = "Checked", Value = 0 });
            cbDisableVREGVGAAutoSweepL1.Items.Add(new ComboboxItem { Text = "Onchecked", Value = 1 });

            // ComboBox TIARegSlopeCTRLL1 Addr = 0x21:0xO8[7:6]
            item = new ComboboxItem();
            item.Text = "2.65V";
            item.Value = 0;
            cbTIARegSlopeCTRLL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.7V";
            item.Value = 1;
            cbTIARegSlopeCTRLL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.75V";
            item.Value = 2;
            cbTIARegSlopeCTRLL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.8V";
            item.Value = 3;
            cbTIARegSlopeCTRLL1.Items.Add(item);

            // ComboBox VGARegSlopeCTRLL1 Addr = 0x21:0xO8[5:4]
            item = new ComboboxItem();
            item.Text = "2.85V";
            item.Value = 0;
            cbVGARegSlopeCTRLL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.9V";
            item.Value = 1;
            cbVGARegSlopeCTRLL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.95V";
            item.Value = 2;
            cbVGARegSlopeCTRLL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3V";
            item.Value = 3;
            cbVGARegSlopeCTRLL1.Items.Add(item);

            // ComboBox DCDAdaptationModeL1 Addr = 0x21:0xD2[0]
            cbDCDAdaptationModeL1.Items.Add(new ComboboxItem { Text = "Manual", Value = 0 });
            cbDCDAdaptationModeL1.Items.Add(new ComboboxItem { Text = "Adaptive", Value = 1 });

            // ComboBox DCDValueL1 Addr = 0x21:0xD1[2:0] 0xD0[7:0]
            for (int i = 0; i < 8; i++) // D1[2:0]
            {
                for (int j = 0; j < 256; j++) // D0[7:0]
                {
                    int value = (i << 8) | j;
                    if (value <= 256)
                    {
                        cbDCDValueL1.Items.Add(new ComboboxItem
                        {
                            Text = value.ToString(),
                            Value = value
                        });
                    }
                }
            }

            // ComboBox cbBGlpolyCodeL1 Addr = 0x21:0x0B[7:4]
            for (int j = 0; j < 16; j++)
            {
                cbBGlpolyCodeL1.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }

            // ComboBox TuneRefL1 Addr = 0x21:0x0A[4:1]
            item = new ComboboxItem();
            item.Text = "2.078V";
            item.Value = 0;
            cbTuneRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.111V";
            item.Value = 1;
            cbTuneRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.143V";
            item.Value = 2;
            cbTuneRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.176V";
            item.Value = 3;
            cbTuneRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.208V";
            item.Value = 4;
            cbTuneRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.241V";
            item.Value = 5;
            cbTuneRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.273V";
            item.Value = 6;
            cbTuneRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.306V";
            item.Value = 7;
            cbTuneRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.847V";
            item.Value = 8;
            cbTuneRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.879V";
            item.Value = 9;
            cbTuneRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.912V";
            item.Value = 10;
            cbTuneRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.945V";
            item.Value = 11;
            cbTuneRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.978V";
            item.Value = 12;
            cbTuneRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.011V";
            item.Value = 13;
            cbTuneRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.044V";
            item.Value = 14;
            cbTuneRefL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.078V";
            item.Value = 15;
            cbTuneRefL1.Items.Add(item);

            // ComboBox cbSelRefLowL1 Addr = 0x21:0xOA[0]
            cbSelRefLowL1.Items.Add(new ComboboxItem { Text = "VCC_EXT-0.3V", Value = 0 });
            cbSelRefLowL1.Items.Add(new ComboboxItem { Text = "Low PD Bias", Value = 1 });

            #endregion

            #region Page L2

            // ComboBox AGCAaptationModeL2 Addr = 0x22:0xC2[0]
            cbAGCAdaptationModeL2.Items.Add(new ComboboxItem { Text = "Manual", Value = 0 });
            cbAGCAdaptationModeL2.Items.Add(new ComboboxItem { Text = "Adaptive", Value = 1 });
            // ComboBox AGCValueL2 Addr = 0x22:0xC1[2:0] 0xC0[7:0]
            for (int i = 0; i < 8; i++) // C1[2:0]
            {
                for (int j = 0; j < 256; j++) // C0[7:0]
                {
                    int value = (i << 8) | j;
                    if (value <= 2016)
                    {
                        cbAGCValueL2.Items.Add(new ComboboxItem
                        {
                            Text = value.ToString(),
                            Value = value
                        });
                    }
                }
            }
            // ComboBox ManualVGA0FilterL2 Addr = 0x22:0x03[5:0]
            for (int j = 0; j < 64; j++) // C0[5:0]
            {
                cbManualVGA0FilterL2.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox ManualVGA1FilterL2 Addr = 0x22:0x04[5:0]
            for (int j = 0; j < 64; j++)
            {
                cbManualVGA1FilterL2.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox ManualVGA2FilterL2 Addr = 0x22:0x05[5:0]
            for (int j = 0; j < 64; j++)
            {
                cbManualVGA2FilterL2.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox TIABWCtrlSwingL2 Addr = 0x22:0x0D[5:0]
            for (int j = 0; j < 64; j++)
            {
                cbTIABWCtrlSwingL2.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox LOSDeassertthresholdL2 Addr = 0x22:0x1A[3:0]
            item = new ComboboxItem();
            item.Text = "33uA";
            item.Value = 0;
            cbLOSDeassertthresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "36uA";
            item.Value = 1;
            cbLOSDeassertthresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "40uA";
            item.Value = 2;
            cbLOSDeassertthresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "43uA";
            item.Value = 3;
            cbLOSDeassertthresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "47uA";
            item.Value = 4;
            cbLOSDeassertthresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "50uA";
            item.Value = 5;
            cbLOSDeassertthresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "54uA";
            item.Value = 6;
            cbLOSDeassertthresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "57uA";
            item.Value = 7;
            cbLOSDeassertthresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10uA";
            item.Value = 8;
            cbLOSDeassertthresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13uA";
            item.Value = 9;
            cbLOSDeassertthresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "17uA";
            item.Value = 10;
            cbLOSDeassertthresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "20uA";
            item.Value = 11;
            cbLOSDeassertthresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "23uA";
            item.Value = 12;
            cbLOSDeassertthresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "27uA";
            item.Value = 13;
            cbLOSDeassertthresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "30uA";
            item.Value = 14;
            cbLOSDeassertthresholdL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "33uA";
            item.Value = 15;
            cbLOSDeassertthresholdL2.Items.Add(item);

            // ComboBox RSSILOSHysteresisL2 Addr = 0x22:0x19[3:2]
            item = new ComboboxItem();
            item.Text = "1dB";
            item.Value = 0;
            cbRSSILOSHysteresisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2dB";
            item.Value = 1;
            cbRSSILOSHysteresisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3dB";
            item.Value = 2;
            cbRSSILOSHysteresisL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4dB";
            item.Value = 3;
            cbRSSILOSHysteresisL2.Items.Add(item);

            // ComboBox SquelchOnLOSL2 Addr = 0x22:0x19[7]
            cbSquelchOnLOSL2.Items.Add(new ComboboxItem { Text = "Checked", Value = 0 });
            cbSquelchOnLOSL2.Items.Add(new ComboboxItem { Text = "Onchecked", Value = 1 });
            // ComboBox BiasAdaptationModeL2 Addr = 0x20:0xE2[0]
            cbBiasAdaptationModeL2.Items.Add(new ComboboxItem { Text = "Manual", Value = 0 });
            cbBiasAdaptationModeL2.Items.Add(new ComboboxItem { Text = "Adaptive", Value = 1 });
            // ComboBox BiasValueL2 Addr = 0x22:0xE1[2:0] 0xE0[7:0]
            for (int i = 0; i < 8; i++) // C1[2:0]
            {
                for (int j = 0; j < 256; j++) // C0[7:0]
                {
                    int value = (i << 8) | j;
                    if (value <= 1008)
                    {
                        cbBiasValueL2.Items.Add(new ComboboxItem
                        {
                            Text = value.ToString(),
                            Value = value
                        });
                    }
                }
            }
            // ComboBox OutputPreEmphasisL2 Addr = 0x22:0x00[7:2]
            for (int i = 0; i < divisions; i++)
            {
                double value = i * stepSize;
                cbOutputPreEmphasisL2.Items.Add(new ComboboxItem
                {
                    Text = $"{value:F3} dB",
                    Value = i
                });
            }
            // ComboBox TargetOutputSwingL2 Addr = 0x22:0x02[3:0]
            item = new ComboboxItem();
            item.Text = "54%";
            item.Value = 0;
            cbTargetOutputSwingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "61%";
            item.Value = 1;
            cbTargetOutputSwingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "67%";
            item.Value = 2;
            cbTargetOutputSwingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "74%";
            item.Value = 3;
            cbTargetOutputSwingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "80%";
            item.Value = 4;
            cbTargetOutputSwingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "87%";
            item.Value = 5;
            cbTargetOutputSwingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "93%";
            item.Value = 6;
            cbTargetOutputSwingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "100%";
            item.Value = 7;
            cbTargetOutputSwingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "107%";
            item.Value = 8;
            cbTargetOutputSwingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "113%";
            item.Value = 9;
            cbTargetOutputSwingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "120%";
            item.Value = 10;
            cbTargetOutputSwingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "126%";
            item.Value = 11;
            cbTargetOutputSwingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "133%";
            item.Value = 12;
            cbTargetOutputSwingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "139%";
            item.Value = 13;
            cbTargetOutputSwingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "146%";
            item.Value = 14;
            cbTargetOutputSwingL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "152%";
            item.Value = 15;
            cbTargetOutputSwingL2.Items.Add(item);

            // ComboBox TIARegulatorsRefL2 Addr = 0x22:0x01[5:3]
            item = new ComboboxItem();
            item.Text = "2.23V";
            item.Value = 0;
            cbTIARegulatorsRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.3V";
            item.Value = 1;
            cbTIARegulatorsRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.36V";
            item.Value = 2;
            cbTIARegulatorsRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.43V";
            item.Value = 3;
            cbTIARegulatorsRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.5V";
            item.Value = 4;
            cbTIARegulatorsRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.57V";
            item.Value = 5;
            cbTIARegulatorsRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.63V";
            item.Value = 6;
            cbTIARegulatorsRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.7V";
            item.Value = 7;
            cbTIARegulatorsRefL2.Items.Add(item);

            // ComboBox VGARegulatorsRefL2 Addr = 0x22:0x01[2:0]
            item = new ComboboxItem();
            item.Text = "2.6V";
            item.Value = 0;
            cbVGARegulatorsRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.63V";
            item.Value = 1;
            cbVGARegulatorsRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.67V";
            item.Value = 2;
            cbVGARegulatorsRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.7V";
            item.Value = 3;
            cbVGARegulatorsRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.74V";
            item.Value = 4;
            cbVGARegulatorsRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.77V";
            item.Value = 5;
            cbVGARegulatorsRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.81V";
            item.Value = 6;
            cbVGARegulatorsRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.84V";
            item.Value = 7;
            cbVGARegulatorsRefL2.Items.Add(item);

            // ComboBox DisableVREGTIAAutoSweepL2 Addr = 0x22:0xOC[7]
            cbDisableVREGTIAAutoSweepL2.Items.Add(new ComboboxItem { Text = "Checked", Value = 0 });
            cbDisableVREGTIAAutoSweepL2.Items.Add(new ComboboxItem { Text = "Onchecked", Value = 1 });

            // ComboBox DisableVREGVGAAutoSweepL2 Addr = 0x22:0xOC[6]
            cbDisableVREGVGAAutoSweepL2.Items.Add(new ComboboxItem { Text = "Checked", Value = 0 });
            cbDisableVREGVGAAutoSweepL2.Items.Add(new ComboboxItem { Text = "Onchecked", Value = 1 });

            // ComboBox TIARegSlopeCTRLL2 Addr = 0x22:0xO8[7:6]
            item = new ComboboxItem();
            item.Text = "2.65V";
            item.Value = 0;
            cbTIARegSlopeCTRLL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.7V";
            item.Value = 1;
            cbTIARegSlopeCTRLL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.75V";
            item.Value = 2;
            cbTIARegSlopeCTRLL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.8V";
            item.Value = 3;
            cbTIARegSlopeCTRLL2.Items.Add(item);

            // ComboBox VGARegSlopeCTRLL2 Addr = 0x22:0xO8[5:4]
            item = new ComboboxItem();
            item.Text = "2.85V";
            item.Value = 0;
            cbVGARegSlopeCTRLL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.9V";
            item.Value = 1;
            cbVGARegSlopeCTRLL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.95V";
            item.Value = 2;
            cbVGARegSlopeCTRLL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3V";
            item.Value = 3;
            cbVGARegSlopeCTRLL2.Items.Add(item);

            // ComboBox DCDAdaptationModeL2 Addr = 0x22:0xD2[0]
            cbDCDAdaptationModeL2.Items.Add(new ComboboxItem { Text = "Manual", Value = 0 });
            cbDCDAdaptationModeL2.Items.Add(new ComboboxItem { Text = "Adaptive", Value = 1 });

            // ComboBox DCDValueL2 Addr = 0x22:0xD1[2:0] 0xD0[7:0]
            for (int i = 0; i < 8; i++) // D1[2:0]
            {
                for (int j = 0; j < 256; j++) // D0[7:0]
                {
                    int value = (i << 8) | j;
                    if (value <= 256)
                    {
                        cbDCDValueL2.Items.Add(new ComboboxItem
                        {
                            Text = value.ToString(),
                            Value = value
                        });
                    }
                }
            }

            // ComboBox cbBGlpolyCodeL2 Addr = 0x22:0x0B[7:4]
            for (int j = 0; j < 16; j++)
            {
                cbBGlpolyCodeL2.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }

            // ComboBox TuneRefL2 Addr = 0x22:0x0A[4:1]
            item = new ComboboxItem();
            item.Text = "2.078V";
            item.Value = 0;
            cbTuneRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.111V";
            item.Value = 1;
            cbTuneRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.143V";
            item.Value = 2;
            cbTuneRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.176V";
            item.Value = 3;
            cbTuneRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.208V";
            item.Value = 4;
            cbTuneRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.241V";
            item.Value = 5;
            cbTuneRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.273V";
            item.Value = 6;
            cbTuneRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.306V";
            item.Value = 7;
            cbTuneRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.847V";
            item.Value = 8;
            cbTuneRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.879V";
            item.Value = 9;
            cbTuneRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.912V";
            item.Value = 10;
            cbTuneRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.945V";
            item.Value = 11;
            cbTuneRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.978V";
            item.Value = 12;
            cbTuneRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.011V";
            item.Value = 13;
            cbTuneRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.044V";
            item.Value = 14;
            cbTuneRefL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.078V";
            item.Value = 15;
            cbTuneRefL2.Items.Add(item);

            // ComboBox cbSelRefLowL2 Addr = 0x22:0xOA[0]
            cbSelRefLowL2.Items.Add(new ComboboxItem { Text = "VCC_EXT-0.3V", Value = 0 });
            cbSelRefLowL2.Items.Add(new ComboboxItem { Text = "Low PD Bias", Value = 1 });

            #endregion

            #region Page L3

            // ComboBox AGCAaptationModeL3 Addr = 0x20:0xC2[0]
            cbAGCAdaptationModeL3.Items.Add(new ComboboxItem { Text = "Manual", Value = 0 });
            cbAGCAdaptationModeL3.Items.Add(new ComboboxItem { Text = "Adaptive", Value = 1 });
            // ComboBox AGCValueL3 Addr = 0x20:0xC1[2:0] 0xC0[7:0]
            for (int i = 0; i < 8; i++) // C1[2:0]
            {
                for (int j = 0; j < 256; j++) // C0[7:0]
                {
                    int value = (i << 8) | j;
                    if (value <= 2016)
                    {
                        cbAGCValueL3.Items.Add(new ComboboxItem
                        {
                            Text = value.ToString(),
                            Value = value
                        });
                    }
                }
            }
            // ComboBox ManualVGA0FilterL3 Addr = 0x20:0x03[5:0]
            for (int j = 0; j < 64; j++) // C0[5:0]
            {
                cbManualVGA0FilterL3.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox ManualVGA1FilterL3 Addr = 0x20:0x04[5:0]
            for (int j = 0; j < 64; j++)
            {
                cbManualVGA1FilterL3.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox ManualVGA2FilterL3 Addr = 0x20:0x05[5:0]
            for (int j = 0; j < 64; j++)
            {
                cbManualVGA2FilterL3.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox TIABWCtrlSwingL3 Addr = 0x20:0x0D[5:0]
            for (int j = 0; j < 64; j++)
            {
                cbTIABWCtrlSwingL3.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox LOSDeassertthresholdL3 Addr = 0x20:0x1A[3:0]
            item = new ComboboxItem();
            item.Text = "33uA";
            item.Value = 0;
            cbLOSDeassertthresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "36uA";
            item.Value = 1;
            cbLOSDeassertthresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "40uA";
            item.Value = 2;
            cbLOSDeassertthresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "43uA";
            item.Value = 3;
            cbLOSDeassertthresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "47uA";
            item.Value = 4;
            cbLOSDeassertthresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "50uA";
            item.Value = 5;
            cbLOSDeassertthresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "54uA";
            item.Value = 6;
            cbLOSDeassertthresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "57uA";
            item.Value = 7;
            cbLOSDeassertthresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10uA";
            item.Value = 8;
            cbLOSDeassertthresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13uA";
            item.Value = 9;
            cbLOSDeassertthresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "17uA";
            item.Value = 10;
            cbLOSDeassertthresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "20uA";
            item.Value = 11;
            cbLOSDeassertthresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "23uA";
            item.Value = 12;
            cbLOSDeassertthresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "27uA";
            item.Value = 13;
            cbLOSDeassertthresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "30uA";
            item.Value = 14;
            cbLOSDeassertthresholdL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "33uA";
            item.Value = 15;
            cbLOSDeassertthresholdL3.Items.Add(item);

            // ComboBox RSSILOSHysteresisL3 Addr = 0x20:0x19[3:2]
            item = new ComboboxItem();
            item.Text = "1dB";
            item.Value = 0;
            cbRSSILOSHysteresisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2dB";
            item.Value = 1;
            cbRSSILOSHysteresisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3dB";
            item.Value = 2;
            cbRSSILOSHysteresisL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4dB";
            item.Value = 3;
            cbRSSILOSHysteresisL3.Items.Add(item);

            // ComboBox SquelchOnLOSL3 Addr = 0x20:0x19[7]
            cbSquelchOnLOSL3.Items.Add(new ComboboxItem { Text = "Checked", Value = 0 });
            cbSquelchOnLOSL3.Items.Add(new ComboboxItem { Text = "Onchecked", Value = 1 });
            // ComboBox BiasAdaptationModeL1 Addr = 0x20:0xE2[0]
            cbBiasAdaptationModeL3.Items.Add(new ComboboxItem { Text = "Manual", Value = 0 });
            cbBiasAdaptationModeL3.Items.Add(new ComboboxItem { Text = "Adaptive", Value = 1 });
            // ComboBox BiasValueL3 Addr = 0x20:0xE1[2:0] 0xE0[7:0]
            for (int i = 0; i < 8; i++) // C1[2:0]
            {
                for (int j = 0; j < 256; j++) // C0[7:0]
                {
                    int value = (i << 8) | j;
                    if (value <= 1008)
                    {
                        cbBiasValueL3.Items.Add(new ComboboxItem
                        {
                            Text = value.ToString(),
                            Value = value
                        });
                    }
                }
            }
            // ComboBox OutputPreEmphasisL3 Addr = 0x20:0x00[7:2]
            for (int i = 0; i < divisions; i++)
            {
                double value = i * stepSize;
                cbOutputPreEmphasisL3.Items.Add(new ComboboxItem
                {
                    Text = $"{value:F3} dB",
                    Value = i
                });
            }
            // ComboBox TargetOutputSwingL3 Addr = 0x20:0x02[3:0]
            item = new ComboboxItem();
            item.Text = "54%";
            item.Value = 0;
            cbTargetOutputSwingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "61%";
            item.Value = 1;
            cbTargetOutputSwingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "67%";
            item.Value = 2;
            cbTargetOutputSwingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "74%";
            item.Value = 3;
            cbTargetOutputSwingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "80%";
            item.Value = 4;
            cbTargetOutputSwingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "87%";
            item.Value = 5;
            cbTargetOutputSwingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "93%";
            item.Value = 6;
            cbTargetOutputSwingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "100%";
            item.Value = 7;
            cbTargetOutputSwingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "107%";
            item.Value = 8;
            cbTargetOutputSwingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "113%";
            item.Value = 9;
            cbTargetOutputSwingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "120%";
            item.Value = 10;
            cbTargetOutputSwingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "126%";
            item.Value = 11;
            cbTargetOutputSwingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "133%";
            item.Value = 12;
            cbTargetOutputSwingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "139%";
            item.Value = 13;
            cbTargetOutputSwingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "146%";
            item.Value = 14;
            cbTargetOutputSwingL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "152%";
            item.Value = 15;
            cbTargetOutputSwingL3.Items.Add(item);

            // ComboBox TIARegulatorsRefL3 Addr = 0x20:0x01[5:3]
            item = new ComboboxItem();
            item.Text = "2.23V";
            item.Value = 0;
            cbTIARegulatorsRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.3V";
            item.Value = 1;
            cbTIARegulatorsRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.36V";
            item.Value = 2;
            cbTIARegulatorsRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.43V";
            item.Value = 3;
            cbTIARegulatorsRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.5V";
            item.Value = 4;
            cbTIARegulatorsRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.57V";
            item.Value = 5;
            cbTIARegulatorsRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.63V";
            item.Value = 6;
            cbTIARegulatorsRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.7V";
            item.Value = 7;
            cbTIARegulatorsRefL3.Items.Add(item);

            // ComboBox VGARegulatorsRefL3 Addr = 0x20:0x01[2:0]
            item = new ComboboxItem();
            item.Text = "2.6V";
            item.Value = 0;
            cbVGARegulatorsRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.63V";
            item.Value = 1;
            cbVGARegulatorsRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.67V";
            item.Value = 2;
            cbVGARegulatorsRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.7V";
            item.Value = 3;
            cbVGARegulatorsRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.74V";
            item.Value = 4;
            cbVGARegulatorsRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.77V";
            item.Value = 5;
            cbVGARegulatorsRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.81V";
            item.Value = 6;
            cbVGARegulatorsRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.84V";
            item.Value = 7;
            cbVGARegulatorsRefL3.Items.Add(item);

            // ComboBox DisableVREGTIAAutoSweepL3 Addr = 0x20:0xOC[7]
            cbDisableVREGTIAAutoSweepL3.Items.Add(new ComboboxItem { Text = "Checked", Value = 0 });
            cbDisableVREGTIAAutoSweepL3.Items.Add(new ComboboxItem { Text = "Onchecked", Value = 1 });

            // ComboBox DisableVREGVGAAutoSweepL3 Addr = 0x20:0xOC[6]
            cbDisableVREGVGAAutoSweepL3.Items.Add(new ComboboxItem { Text = "Checked", Value = 0 });
            cbDisableVREGVGAAutoSweepL3.Items.Add(new ComboboxItem { Text = "Onchecked", Value = 1 });

            // ComboBox TIARegSlopeCTRLL3 Addr = 0x20:0xO8[7:6]
            item = new ComboboxItem();
            item.Text = "2.65V";
            item.Value = 0;
            cbTIARegSlopeCTRLL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.7V";
            item.Value = 1;
            cbTIARegSlopeCTRLL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.75V";
            item.Value = 2;
            cbTIARegSlopeCTRLL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.8V";
            item.Value = 3;
            cbTIARegSlopeCTRLL3.Items.Add(item);

            // ComboBox VGARegSlopeCTRLL3 Addr = 0x20:0xO8[5:4]
            item = new ComboboxItem();
            item.Text = "2.85V";
            item.Value = 0;
            cbVGARegSlopeCTRLL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.9V";
            item.Value = 1;
            cbVGARegSlopeCTRLL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.95V";
            item.Value = 2;
            cbVGARegSlopeCTRLL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3V";
            item.Value = 3;
            cbVGARegSlopeCTRLL3.Items.Add(item);

            // ComboBox DCDAdaptationModeL3 Addr = 0x20:0xD2[0]
            cbDCDAdaptationModeL3.Items.Add(new ComboboxItem { Text = "Manual", Value = 0 });
            cbDCDAdaptationModeL3.Items.Add(new ComboboxItem { Text = "Adaptive", Value = 1 });

            // ComboBox DCDValueL3 Addr = 0x20:0xD1[2:0] 0xD0[7:0]
            for (int i = 0; i < 8; i++) // D1[2:0]
            {
                for (int j = 0; j < 256; j++) // D0[7:0]
                {
                    int value = (i << 8) | j;
                    if (value <= 256)
                    {
                        cbDCDValueL3.Items.Add(new ComboboxItem
                        {
                            Text = value.ToString(),
                            Value = value
                        });
                    }
                }
            }

            // ComboBox cbBGlpolyCodeL3 Addr = 0x20:0x0B[7:4]
            for (int j = 0; j < 16; j++)
            {
                cbBGlpolyCodeL3.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }

            // ComboBox TuneRefL3 Addr = 0x20:0x0A[4:1]
            item = new ComboboxItem();
            item.Text = "2.078V";
            item.Value = 0;
            cbTuneRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.111V";
            item.Value = 1;
            cbTuneRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.143V";
            item.Value = 2;
            cbTuneRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.176V";
            item.Value = 3;
            cbTuneRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.208V";
            item.Value = 4;
            cbTuneRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.241V";
            item.Value = 5;
            cbTuneRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.273V";
            item.Value = 6;
            cbTuneRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.306V";
            item.Value = 7;
            cbTuneRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.847V";
            item.Value = 8;
            cbTuneRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.879V";
            item.Value = 9;
            cbTuneRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.912V";
            item.Value = 10;
            cbTuneRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.945V";
            item.Value = 11;
            cbTuneRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.978V";
            item.Value = 12;
            cbTuneRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.011V";
            item.Value = 13;
            cbTuneRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.044V";
            item.Value = 14;
            cbTuneRefL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.078V";
            item.Value = 15;
            cbTuneRefL3.Items.Add(item);

            // ComboBox cbSelRefLowL3 Addr = 0x20:0xOA[0]
            cbSelRefLowL3.Items.Add(new ComboboxItem { Text = "VCC_EXT-0.3V", Value = 0 });
            cbSelRefLowL3.Items.Add(new ComboboxItem { Text = "Low PD Bias", Value = 1 });

            #endregion

            #region Page All

            // ComboBox AGCAaptationModeAll Addr = 0x20:0xC2[0]
            cbAGCAdaptationModeAll.Items.Add(new ComboboxItem { Text = "Manual", Value = 0 });
            cbAGCAdaptationModeAll.Items.Add(new ComboboxItem { Text = "Adaptive", Value = 1 });
            // ComboBox AGCValueAll Addr = 0x20:0xC1[2:0] 0xC0[7:0]
            for (int i = 0; i < 8; i++) // C1[2:0]
            {
                for (int j = 0; j < 256; j++) // C0[7:0]
                {
                    int value = (i << 8) | j;
                    if (value <= 2016)
                    {
                        cbAGCValueAll.Items.Add(new ComboboxItem
                        {
                            Text = value.ToString(),
                            Value = value
                        });
                    }
                }
            }
            // ComboBox ManualVGA0FilterAll Addr = 0x20:0x03[5:0]
            for (int j = 0; j < 64; j++) // C0[5:0]
            {
                cbManualVGA0FilterAll.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox ManualVGA1FilterAll Addr = 0x20:0x04[5:0]
            for (int j = 0; j < 64; j++)
            {
                cbManualVGA1FilterAll.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox ManualVGA2FilterAll Addr = 0x20:0x05[5:0]
            for (int j = 0; j < 64; j++)
            {
                cbManualVGA2FilterAll.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox TIABWCtrlSwingAll Addr = 0x20:0x0D[5:0]
            for (int j = 0; j < 64; j++)
            {
                cbTIABWCtrlSwingAll.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }
            // ComboBox LOSDeassertthresholdAll Addr = 0x20:0x1A[3:0]
            item = new ComboboxItem();
            item.Text = "33uA";
            item.Value = 0;
            cbLOSDeassertthresholdAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "36uA";
            item.Value = 1;
            cbLOSDeassertthresholdAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "40uA";
            item.Value = 2;
            cbLOSDeassertthresholdAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "43uA";
            item.Value = 3;
            cbLOSDeassertthresholdAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "47uA";
            item.Value = 4;
            cbLOSDeassertthresholdAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "50uA";
            item.Value = 5;
            cbLOSDeassertthresholdAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "54uA";
            item.Value = 6;
            cbLOSDeassertthresholdAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "57uA";
            item.Value = 7;
            cbLOSDeassertthresholdAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10uA";
            item.Value = 8;
            cbLOSDeassertthresholdAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13uA";
            item.Value = 9;
            cbLOSDeassertthresholdAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "17uA";
            item.Value = 10;
            cbLOSDeassertthresholdAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "20uA";
            item.Value = 11;
            cbLOSDeassertthresholdAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "23uA";
            item.Value = 12;
            cbLOSDeassertthresholdAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "27uA";
            item.Value = 13;
            cbLOSDeassertthresholdAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "30uA";
            item.Value = 14;
            cbLOSDeassertthresholdAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "33uA";
            item.Value = 15;
            cbLOSDeassertthresholdAll.Items.Add(item);

            // ComboBox RSSILOSHysteresisAll Addr = 0x20:0x19[3:2]
            item = new ComboboxItem();
            item.Text = "1dB";
            item.Value = 0;
            cbRSSILOSHysteresisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2dB";
            item.Value = 1;
            cbRSSILOSHysteresisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3dB";
            item.Value = 2;
            cbRSSILOSHysteresisAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4dB";
            item.Value = 3;
            cbRSSILOSHysteresisAll.Items.Add(item);

            // ComboBox SquelchOnLOSAll Addr = 0x20:0x19[7]
            cbSquelchOnLOSAll.Items.Add(new ComboboxItem { Text = "Checked", Value = 0 });
            cbSquelchOnLOSAll.Items.Add(new ComboboxItem { Text = "Onchecked", Value = 1 });
            // ComboBox BiasAdaptationModeAll Addr = 0x20:0xE2[0]
            cbBiasAdaptationModeAll.Items.Add(new ComboboxItem { Text = "Manual", Value = 0 });
            cbBiasAdaptationModeAll.Items.Add(new ComboboxItem { Text = "Adaptive", Value = 1 });
            // ComboBox BiasValueAll Addr = 0x20:0xE1[2:0] 0xE0[7:0]
            for (int i = 0; i < 8; i++) // C1[2:0]
            {
                for (int j = 0; j < 256; j++) // C0[7:0]
                {
                    int value = (i << 8) | j;
                    if (value <= 1008)
                    {
                        cbBiasValueAll.Items.Add(new ComboboxItem
                        {
                            Text = value.ToString(),
                            Value = value
                        });
                    }
                }
            }
            // ComboBox OutputPreEmphasisAll Addr = 0x20:0x00[7:2]
            for (int i = 0; i < divisions; i++)
            {
                double value = i * stepSize;
                cbOutputPreEmphasisAll.Items.Add(new ComboboxItem
                {
                    Text = $"{value:F3} dB",
                    Value = i
                });
            }
            // ComboBox TargetOutputSwingAll Addr = 0x20:0x02[3:0]
            item = new ComboboxItem();
            item.Text = "54%";
            item.Value = 0;
            cbTargetOutputSwingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "61%";
            item.Value = 1;
            cbTargetOutputSwingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "67%";
            item.Value = 2;
            cbTargetOutputSwingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "74%";
            item.Value = 3;
            cbTargetOutputSwingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "80%";
            item.Value = 4;
            cbTargetOutputSwingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "87%";
            item.Value = 5;
            cbTargetOutputSwingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "93%";
            item.Value = 6;
            cbTargetOutputSwingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "100%";
            item.Value = 7;
            cbTargetOutputSwingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "107%";
            item.Value = 8;
            cbTargetOutputSwingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "113%";
            item.Value = 9;
            cbTargetOutputSwingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "120%";
            item.Value = 10;
            cbTargetOutputSwingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "126%";
            item.Value = 11;
            cbTargetOutputSwingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "133%";
            item.Value = 12;
            cbTargetOutputSwingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "139%";
            item.Value = 13;
            cbTargetOutputSwingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "146%";
            item.Value = 14;
            cbTargetOutputSwingAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "152%";
            item.Value = 15;
            cbTargetOutputSwingAll.Items.Add(item);

            // ComboBox TIARegulatorsRefAll Addr = 0x20:0x01[5:3]
            item = new ComboboxItem();
            item.Text = "2.23V";
            item.Value = 0;
            cbTIARegulatorsRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.3V";
            item.Value = 1;
            cbTIARegulatorsRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.36V";
            item.Value = 2;
            cbTIARegulatorsRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.43V";
            item.Value = 3;
            cbTIARegulatorsRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.5V";
            item.Value = 4;
            cbTIARegulatorsRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.57V";
            item.Value = 5;
            cbTIARegulatorsRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.63V";
            item.Value = 6;
            cbTIARegulatorsRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.7V";
            item.Value = 7;
            cbTIARegulatorsRefAll.Items.Add(item);

            // ComboBox VGARegulatorsRefAll Addr = 0x20:0x01[2:0]
            item = new ComboboxItem();
            item.Text = "2.6V";
            item.Value = 0;
            cbVGARegulatorsRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.63V";
            item.Value = 1;
            cbVGARegulatorsRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.67V";
            item.Value = 2;
            cbVGARegulatorsRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.7V";
            item.Value = 3;
            cbVGARegulatorsRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.74V";
            item.Value = 4;
            cbVGARegulatorsRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.77V";
            item.Value = 5;
            cbVGARegulatorsRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.81V";
            item.Value = 6;
            cbVGARegulatorsRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.84V";
            item.Value = 7;
            cbVGARegulatorsRefAll.Items.Add(item);

            // ComboBox DisableVREGTIAAutoSweepAll Addr = 0x20:0xOC[7]
            cbDisableVREGTIAAutoSweepAll.Items.Add(new ComboboxItem { Text = "Checked", Value = 0 });
            cbDisableVREGTIAAutoSweepAll.Items.Add(new ComboboxItem { Text = "Onchecked", Value = 1 });

            // ComboBox DisableVREGVGAAutoSweepAll Addr = 0x20:0xOC[6]
            cbDisableVREGVGAAutoSweepAll.Items.Add(new ComboboxItem { Text = "Checked", Value = 0 });
            cbDisableVREGVGAAutoSweepAll.Items.Add(new ComboboxItem { Text = "Onchecked", Value = 1 });

            // ComboBox TIARegSlopeCTRLAll Addr = 0x20:0xO8[7:6]
            item = new ComboboxItem();
            item.Text = "2.65V";
            item.Value = 0;
            cbTIARegSlopeCTRLAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.7V";
            item.Value = 1;
            cbTIARegSlopeCTRLAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.75V";
            item.Value = 2;
            cbTIARegSlopeCTRLAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.8V";
            item.Value = 3;
            cbTIARegSlopeCTRLAll.Items.Add(item);

            // ComboBox VGARegSlopeCTRLAll Addr = 0x20:0xO8[5:4]
            item = new ComboboxItem();
            item.Text = "2.85V";
            item.Value = 0;
            cbVGARegSlopeCTRLAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.9V";
            item.Value = 1;
            cbVGARegSlopeCTRLAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.95V";
            item.Value = 2;
            cbVGARegSlopeCTRLAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3V";
            item.Value = 3;
            cbVGARegSlopeCTRLAll.Items.Add(item);

            // ComboBox DCDAdaptationModeAll Addr = 0x20:0xD2[0]
            cbDCDAdaptationModeAll.Items.Add(new ComboboxItem { Text = "Manual", Value = 0 });
            cbDCDAdaptationModeAll.Items.Add(new ComboboxItem { Text = "Adaptive", Value = 1 });

            // ComboBox DCDValueAll Addr = 0x20:0xD1[2:0] 0xD0[7:0]
            for (int i = 0; i < 8; i++) // D1[2:0]
            {
                for (int j = 0; j < 256; j++) // D0[7:0]
                {
                    int value = (i << 8) | j;
                    if (value <= 256)
                    {
                        cbDCDValueAll.Items.Add(new ComboboxItem
                        {
                            Text = value.ToString(),
                            Value = value
                        });
                    }
                }
            }

            // ComboBox cbBGlpolyCodeAll Addr = 0x20:0x0B[7:4]
            for (int j = 0; j < 16; j++)
            {
                cbBGlpolyCodeAll.Items.Add(new ComboboxItem
                {
                    Text = j.ToString(),
                    Value = j
                });
            }

            // ComboBox TuneRefAll Addr = 0x20:0x0A[4:1]
            item = new ComboboxItem();
            item.Text = "2.078V";
            item.Value = 0;
            cbTuneRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.111V";
            item.Value = 1;
            cbTuneRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.143V";
            item.Value = 2;
            cbTuneRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.176V";
            item.Value = 3;
            cbTuneRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.208V";
            item.Value = 4;
            cbTuneRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.241V";
            item.Value = 5;
            cbTuneRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.273V";
            item.Value = 6;
            cbTuneRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.306V";
            item.Value = 7;
            cbTuneRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.847V";
            item.Value = 8;
            cbTuneRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.879V";
            item.Value = 9;
            cbTuneRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.912V";
            item.Value = 10;
            cbTuneRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.945V";
            item.Value = 11;
            cbTuneRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.978V";
            item.Value = 12;
            cbTuneRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.011V";
            item.Value = 13;
            cbTuneRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.044V";
            item.Value = 14;
            cbTuneRefAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.078V";
            item.Value = 15;
            cbTuneRefAll.Items.Add(item);

            // ComboBox cbSelRefLowAll Addr = 0x20:0xOA[0]
            cbSelRefLowAll.Items.Add(new ComboboxItem { Text = "VCC_EXT-0.3V", Value = 0 });
            cbSelRefLowAll.Items.Add(new ComboboxItem { Text = "Low PD Bias", Value = 1 });

            #endregion

            #region Page Control
            // ComboBox OscillatorFreq Addr = 0x00:0x03[4:3]
            item = new ComboboxItem();
            item.Text = "16 MHz";
            item.Value = 0;
            cbOscillatorFreq.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8 MHz";
            item.Value = 1;
            cbOscillatorFreq.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4 MHz";
            item.Value = 2;
            cbOscillatorFreq.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2 MHz";
            item.Value = 3;
            cbOscillatorFreq.Items.Add(item);

            // ComboBox 1VRegulator Addr = 0x00:0x04[1:0]
            item = new ComboboxItem();
            item.Text = "1.05 V";
            item.Value = 0;
            cb1VRegulator.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.10 V";
            item.Value = 1;
            cb1VRegulator.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.15 V";
            item.Value = 2;
            cb1VRegulator.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1.20 V";
            item.Value = 3;
            cb1VRegulator.Items.Add(item);

            // ComboBox lntClkTestMode Addr = 0x00:0x08[3]
            cblntClkTestMode.Items.Add(new ComboboxItem { Text = "Not Brought out", Value = 0 });
            cblntClkTestMode.Items.Add(new ComboboxItem { Text = "Brought out", Value = 1 });

            // ComboBox Polarity Addr = 0x00:0x03[1]
            cbPolarity.Items.Add(new ComboboxItem { Text = "Active Low", Value = 0 });
            cbPolarity.Items.Add(new ComboboxItem { Text = "Active High", Value = 1 });

            // ComboBox DriverMode Addr = 0x00:0x03[2]
            cbDriverMode.Items.Add(new ComboboxItem { Text = "Open Drain", Value = 0 });
            cbDriverMode.Items.Add(new ComboboxItem { Text = "COMS", Value = 1 });

            // ComboBox ATPCore Addr = 0x00:0x08[2:0]
            item = new ComboboxItem();
            item.Text = "No signal on ATP bus";
            item.Value = 0;
            cbATPCore.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP_2P5_channel/ATP_2P9_channel";
            item.Value = 1;
            cbATPCore.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "POR_ATP/V3P3_probe";
            item.Value = 2;
            cbATPCore.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "OTP Reg Voltage";
            item.Value = 3;
            cbATPCore.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "Reference for 1V LDO";
            item.Value = 4;
            cbATPCore.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2.5V Regulated Voltage for core";
            item.Value = 5;
            cbATPCore.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "No signal on ATP bus";
            item.Value = 6;
            cbATPCore.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "No signal on ATP bus";
            item.Value = 7;
            cbATPCore.Items.Add(item);

            // ComboBox ATPChannel Addr = 0x00:0xOA[1:0]
            item = new ComboboxItem();
            item.Text = "channel 0";
            item.Value = 0;
            cbATPChannel.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "channel 1";
            item.Value = 1;
            cbATPChannel.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "channel 2";
            item.Value = 2;
            cbATPChannel.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "channel 3";
            item.Value = 3;
            cbATPChannel.Items.Add(item);

            // ComboBox ATPCode Addr = 0x00:0xOA[7:2]
            item = new ComboboxItem();
            item.Text = "ATP 0 -Nothing";
            item.Value = 0;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 1 -Nothing";
            item.Value = 1;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 2 Signal";
            item.Value = 2;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 3 Signal";
            item.Value = 3;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 4 Signal";
            item.Value = 4;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 5 Signal";
            item.Value = 5;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 6 Signal";
            item.Value = 6;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 7 Signal";
            item.Value = 7;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 8 Signal";
            item.Value = 8;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 9 Signal";
            item.Value = 9;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 10 Signal";
            item.Value = 10;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 11 Signal";
            item.Value = 11;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 12 Signal";
            item.Value = 12;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 13 Signal";
            item.Value = 13;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 14 Signal";
            item.Value = 14;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 15 Signal";
            item.Value = 15;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 16 Signal";
            item.Value = 16;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 17 Signal";
            item.Value = 17;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 18 Signal";
            item.Value = 18;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 19 Signal";
            item.Value = 19;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 20 Signal";
            item.Value = 20;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 21 Signal";
            item.Value = 21;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 22 Signal";
            item.Value = 22;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 23 Signal";
            item.Value = 23;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 24 Signal";
            item.Value = 24;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 25 Signal";
            item.Value = 25;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 26 Signal";
            item.Value = 26;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 27 Signal";
            item.Value = 27;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 28 Signal";
            item.Value = 28;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 29 Signal";
            item.Value = 29;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 30 Signal";
            item.Value = 30;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 31 Signal";
            item.Value = 31;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 32 Signal";
            item.Value = 32;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 33 Signal";
            item.Value = 33;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 34 Signal";
            item.Value = 34;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 35 Signal";
            item.Value = 35;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 36 Signal";
            item.Value = 36;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 37 Signal";
            item.Value = 37;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 38 Signal";
            item.Value = 38;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 39 Signal";
            item.Value = 39;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 40 Signal";
            item.Value = 40;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 41 Signal";
            item.Value = 41;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 42 Signal";
            item.Value = 42;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 43 Signal";
            item.Value = 43;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 44 Signal";
            item.Value = 44;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 45 Signal";
            item.Value = 45;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 46 Signal";
            item.Value = 46;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 47 Signal";
            item.Value = 47;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 48 Signal";
            item.Value = 48;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 49 Signal";
            item.Value = 49;
            cbATPCode.Items.Add(item);
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 50 Signal";
            item.Value = 50;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 51 Signal";
            item.Value = 51;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 52 Signal";
            item.Value = 52;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 53 Signal";
            item.Value = 53;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 54 Signal";
            item.Value = 54;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 55 Signal";
            item.Value = 55;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "ATP 56 Signal";
            item.Value = 56;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "No ATP(0x39)";//GUI Chosen back to (0x03~0x09)
            item.Value = 57;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "No ATP(0x3A)";
            item.Value = 58;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "No ATP(0x3B)";
            item.Value = 59;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "No ATP(0x3C)";
            item.Value = 60;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "No ATP(0x3D)";
            item.Value = 61;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "No ATP(0x3E)";
            item.Value = 62;
            cbATPCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "No ATP(0x3F)";
            item.Value = 63;
            cbATPCode.Items.Add(item);

            // ComboBox SAATP2P9 Addr = 0x00:0x10[2]
            cbSAATP2P9.Items.Add(new ComboboxItem { Text = "Input", Value = 0 });
            cbSAATP2P9.Items.Add(new ComboboxItem { Text = "Output", Value = 1 });

            // ComboBox LPModePin Addr = 0x00:0x11[0]
            //cbLPModePin.Items.Add(new ComboboxItem { Text = "High", Value = 0 });
            //cbLPModePin.Items.Add(new ComboboxItem { Text = "Low", Value = 1 });

            // ComboBox RssiChannelSelect Addr = 0x00:0xO3[6:5]
            item = new ComboboxItem();
            item.Text = "CH0 RSSI";
            item.Value = 0;
            cbRssiChannelSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "CH1 RSSI";
            item.Value = 1;
            cbRssiChannelSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "CH2 RSSI";
            item.Value = 2;
            cbRssiChannelSelect.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "CH3 RSSI";
            item.Value = 3;
            cbRssiChannelSelect.Items.Add(item);

            // ComboBox VCCEXTVoltage Addr = 0x00:0x03[7]
            cbVCCEXTVoltage.Items.Add(new ComboboxItem { Text = "3.3V", Value = 0 });
            cbVCCEXTVoltage.Items.Add(new ComboboxItem { Text = "5.5V", Value = 1 });

            #endregion

            // 新增Customer Page SFF-8636 CTLE function 2025.06.04
            #region Page Customer

            #endregion

        }

        #region Contral page needed checked
        private static byte ReadByteFromDevice(byte page, byte address)
        {
            // 假設硬體返回的數據（可替換為實際讀取邏輯）
            if (page == 0x00 && address == 0x03)
            {
                return 0b10000000; // 返回數據，bit[7] = 1
            }
            return 0x00; // 默認返回值
        }
        #endregion        

        #region CallBack function
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
        private byte currentPage = 0xFF;

        public int SetPage(byte[] page)
        {
            if (currentPage == page[0]) return 0;

            int rv = i2cWriteCB(devAddr, 0xFF, 1, page);
            if (rv < 0) return -1;

            Thread.Sleep(100);
            currentPage = page[0];
            return 0;
        }
        #endregion

        #region _WritePageXXAddrXX
        private int _WritePage1FAddr00()
        {
            byte[] data = new byte[1];
            data[0] = 0;


            if (cbPowerDownChannelAll.Checked)
            {
                data[0] |= 0x01; // 設置位 0 為 1
            }

            if (cbOutputPreEmphasisAll.SelectedIndex < 0 || cbOutputPreEmphasisAll.SelectedIndex > 63)
            {
                return -1;
            }
            data[0] |= (byte)((cbOutputPreEmphasisAll.SelectedIndex & 0x3F) << 2);


            SetPage(Page1F);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;

        }
        private int _WritePage20Addr00()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbPowerDownChannelL0.Checked)
            {
                data[0] |= 0x01; // 設置位 0 為 1
            }

            if (cbOutputPreEmphasisL0.SelectedIndex < 0 || cbOutputPreEmphasisL0.SelectedIndex > 63)
            {
                return -1;
            }
            data[0] |= (byte)((cbOutputPreEmphasisL0.SelectedIndex & 0x3F) << 2);

            SetPage(Page20);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage21Addr00()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbPowerDownChannelL1.Checked)
            {
                data[0] |= 0x01; // 設置位 0 為 1
            }

            if (cbOutputPreEmphasisL1.SelectedIndex < 0 || cbOutputPreEmphasisL1.SelectedIndex > 63)
            {
                return -1;
            }
            data[0] |= (byte)((cbOutputPreEmphasisL1.SelectedIndex & 0x3F) << 2);

            SetPage(Page21);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage22Addr00()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbPowerDownChannelL2.Checked)
            {
                data[0] |= 0x01; // 設置位 0 為 1
            }

            if (cbOutputPreEmphasisL2.SelectedIndex < 0 || cbOutputPreEmphasisL2.SelectedIndex > 63)
            {
                return -1;
            }
            data[0] |= (byte)((cbOutputPreEmphasisL2.SelectedIndex & 0x3F) << 2);

            SetPage(Page22);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage23Addr00()
        {
            byte[] data = new byte[1];
            data[0] = 0;


            if (cbPowerDownChannelL3.Checked)
            {
                data[0] |= 0x01; // 設置位 0 為 1
            }

            if (cbOutputPreEmphasisL3.SelectedIndex < 0 || cbOutputPreEmphasisL3.SelectedIndex > 63)
            {
                return -1;
            }
            data[0] |= (byte)((cbOutputPreEmphasisL3.SelectedIndex & 0x3F) << 2);


            SetPage(Page23);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage1FAddr01()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbVGARegulatorsRefAll.SelectedIndex < 0 || cbVGARegulatorsRefAll.SelectedIndex > 7)
            {
                return -1;
            }
            data[0] |= (byte)(cbVGARegulatorsRefAll.SelectedIndex);

            if (cbTIARegulatorsRefAll.SelectedIndex < 0 || cbTIARegulatorsRefAll.SelectedIndex > 7)
            {
                return -1;
            }
            data[0] |= (byte)(cbTIARegulatorsRefAll.SelectedIndex << 3);

            SetPage(Page1F);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x01, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage20Addr01()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbVGARegulatorsRefL0.SelectedIndex < 0 || cbVGARegulatorsRefL0.SelectedIndex > 7)
            {
                return -1;
            }
            data[0] |= (byte)(cbVGARegulatorsRefL0.SelectedIndex);

            if (cbTIARegulatorsRefL0.SelectedIndex < 0 || cbTIARegulatorsRefL0.SelectedIndex > 7)
            {
                return -1;
            }
            data[0] |= (byte)(cbTIARegulatorsRefL0.SelectedIndex << 3);

            SetPage(Page20);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x01, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage21Addr01()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbVGARegulatorsRefL1.SelectedIndex < 0 || cbVGARegulatorsRefL1.SelectedIndex > 7)
            {
                return -1;
            }
            data[0] |= (byte)(cbVGARegulatorsRefL1.SelectedIndex);

            if (cbTIARegulatorsRefL1.SelectedIndex < 0 || cbTIARegulatorsRefL1.SelectedIndex > 7)
            {
                return -1;
            }
            data[0] |= (byte)(cbTIARegulatorsRefL1.SelectedIndex << 3);

            SetPage(Page21);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x01, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage22Addr01()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbVGARegulatorsRefL2.SelectedIndex < 0 || cbVGARegulatorsRefL2.SelectedIndex > 7)
            {
                return -1;
            }
            data[0] |= (byte)(cbVGARegulatorsRefL2.SelectedIndex);

            if (cbTIARegulatorsRefL2.SelectedIndex < 0 || cbTIARegulatorsRefL2.SelectedIndex > 7)
            {
                return -1;
            }
            data[0] |= (byte)(cbTIARegulatorsRefL2.SelectedIndex << 3);

            SetPage(Page22);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x01, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage23Addr01()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbVGARegulatorsRefL3.SelectedIndex < 0 || cbVGARegulatorsRefL3.SelectedIndex > 7)
            {
                return -1;
            }
            data[0] |= (byte)(cbVGARegulatorsRefL3.SelectedIndex);

            if (cbTIARegulatorsRefL3.SelectedIndex < 0 || cbTIARegulatorsRefL3.SelectedIndex > 7)
            {
                return -1;
            }
            data[0] |= (byte)(cbTIARegulatorsRefL3.SelectedIndex << 3);

            SetPage(Page23);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x01, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage00Addr02()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0xAA; //1010 1010b: Software reset without OTP download

            SetPage(Page00);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x02, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage1FAddr02()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbTargetOutputSwingAll.SelectedIndex < 0 || cbTargetOutputSwingAll.SelectedIndex > 15)
            {
                return -1;
            }
            data[0] |= (byte)(cbTargetOutputSwingAll.SelectedIndex);

            SetPage(Page1F);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x02, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage20Addr02()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbTargetOutputSwingL0.SelectedIndex < 0 || cbTargetOutputSwingL0.SelectedIndex > 15)
            {
                return -1;
            }
            data[0] |= (byte)(cbTargetOutputSwingL0.SelectedIndex);

            SetPage(Page20);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x02, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage21Addr02()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbTargetOutputSwingL1.SelectedIndex < 0 || cbTargetOutputSwingL1.SelectedIndex > 15)
            {
                return -1;
            }
            data[0] |= (byte)(cbTargetOutputSwingL1.SelectedIndex);

            SetPage(Page21);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x02, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage22Addr02()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbTargetOutputSwingL2.SelectedIndex < 0 || cbTargetOutputSwingL2.SelectedIndex > 15)
            {
                return -1;
            }
            data[0] |= (byte)(cbTargetOutputSwingL2.SelectedIndex);

            SetPage(Page22);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x02, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage23Addr02()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbTargetOutputSwingL3.SelectedIndex < 0 || cbTargetOutputSwingL3.SelectedIndex > 15)
            {
                return -1;
            }
            data[0] |= (byte)(cbTargetOutputSwingL3.SelectedIndex);

            SetPage(Page23);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x02, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage00Addr03()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbPolarity.SelectedIndex < 0 || cbPolarity.SelectedIndex > 1)
            {
                return -1;
            }
            data[0] |= (byte)(cbPolarity.SelectedIndex << 1);

            if (cbDriverMode.SelectedIndex < 0 || cbDriverMode.SelectedIndex > 1)
            {
                return -1;
            }
            data[0] |= (byte)(cbDriverMode.SelectedIndex << 2);

            if (cbOscillatorFreq.SelectedIndex < 0 || cbOscillatorFreq.SelectedIndex > 3)
            {
                return -1;
            }
            data[0] |= (byte)(cbOscillatorFreq.SelectedIndex << 3);

            if (cbRssiChannelSelect.SelectedIndex < 0 || cbRssiChannelSelect.SelectedIndex > 3)
            {
                return -1;
            }
            data[0] |= (byte)(cbRssiChannelSelect.SelectedIndex << 5);


            if (cbVCCEXTVoltage.SelectedIndex < 0 || cbVCCEXTVoltage.SelectedIndex > 1)
            {
                return -1;
            }
            data[0] |= (byte)(cbVCCEXTVoltage.SelectedIndex << 7);

            SetPage(Page00);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x03, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage1FAddr03()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbManualVGA0FilterAll.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page1F);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x03, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage20Addr03()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbManualVGA0FilterL0.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }

            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page20);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x03, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage21Addr03()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbManualVGA0FilterL1.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page21);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x03, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage22Addr03()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbManualVGA0FilterL2.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page22);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x03, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage23Addr03()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbManualVGA0FilterL3.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page23);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x03, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage00Addr04()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = Convert.ToByte(cb1VRegulator.SelectedIndex);
            data[0] = bTmp;


            SetPage(Page00);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x04, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage1FAddr04()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbManualVGA1FilterAll.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page1F);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x04, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage20Addr04()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbManualVGA1FilterL0.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page20);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x04, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage21Addr04()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbManualVGA1FilterL1.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page21);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x04, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage22Addr04()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbManualVGA1FilterL2.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page22);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x04, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage23Addr04()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbManualVGA1FilterL3.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page23);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x04, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage1FAddr05()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbManualVGA2FilterAll.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page1F);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x05, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage20Addr05()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbManualVGA2FilterL0.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page20);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x05, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage21Addr05()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbManualVGA2FilterL1.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page21);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x05, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage22Addr05()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbManualVGA2FilterL2.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page22);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x05, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage23Addr05()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbManualVGA2FilterL3.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page23);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x05, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage00Addr08()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbATPCore.SelectedIndex < 0 || cbATPCore.SelectedIndex > 7)
            {
                return -1;
            }

            data[0] |= (byte)(cbATPCore.SelectedIndex);

            if (cblntClkTestMode.SelectedIndex < 0 || cblntClkTestMode.SelectedIndex > 1)
            {
                return -1;
            }
            data[0] |= (byte)(cblntClkTestMode.SelectedIndex << 3);

            SetPage(Page00);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x08, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage1FAddr08()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbVGARegSlopeCTRLAll.SelectedIndex < 0 || cbVGARegSlopeCTRLAll.SelectedIndex > 3)
            {
                return -1;
            }
            data[0] |= (byte)(cbVGARegSlopeCTRLAll.SelectedIndex << 4);

            if (cbTIARegSlopeCTRLAll.SelectedIndex < 0 || cbTIARegSlopeCTRLAll.SelectedIndex > 3)
            {
                return -1;
            }
            data[0] |= (byte)(cbTIARegSlopeCTRLAll.SelectedIndex << 6);

            SetPage(Page1F);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x08, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage20Addr08()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbVGARegSlopeCTRLL0.SelectedIndex < 0 || cbVGARegSlopeCTRLL0.SelectedIndex > 3)
            {
                return -1;
            }
            data[0] |= (byte)(cbVGARegSlopeCTRLL0.SelectedIndex << 4);

            if (cbTIARegSlopeCTRLL0.SelectedIndex < 0 || cbTIARegSlopeCTRLL0.SelectedIndex > 3)
            {
                return -1;
            }
            data[0] |= (byte)(cbTIARegSlopeCTRLL0.SelectedIndex << 6);

            SetPage(Page20);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x08, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage21Addr08()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbVGARegSlopeCTRLL1.SelectedIndex < 0 || cbVGARegSlopeCTRLL1.SelectedIndex > 3)
            {
                return -1;
            }
            data[0] |= (byte)(cbVGARegSlopeCTRLL1.SelectedIndex << 4);

            if (cbTIARegSlopeCTRLL1.SelectedIndex < 0 || cbTIARegSlopeCTRLL1.SelectedIndex > 3)
            {
                return -1;
            }
            data[0] |= (byte)(cbTIARegSlopeCTRLL1.SelectedIndex << 6);

            SetPage(Page21);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x08, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage22Addr08()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbVGARegSlopeCTRLL2.SelectedIndex < 0 || cbVGARegSlopeCTRLL2.SelectedIndex > 3)
            {
                return -1;
            }
            data[0] |= (byte)(cbVGARegSlopeCTRLL2.SelectedIndex << 4);

            if (cbTIARegSlopeCTRLL2.SelectedIndex < 0 || cbTIARegSlopeCTRLL2.SelectedIndex > 3)
            {
                return -1;
            }
            data[0] |= (byte)(cbTIARegSlopeCTRLL2.SelectedIndex << 6);

            SetPage(Page22);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x08, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage23Addr08()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbVGARegSlopeCTRLL3.SelectedIndex < 0 || cbVGARegSlopeCTRLL3.SelectedIndex > 3)
            {
                return -1;
            }
            data[0] |= (byte)(cbVGARegSlopeCTRLL3.SelectedIndex << 4);

            if (cbTIARegSlopeCTRLL3.SelectedIndex < 0 || cbTIARegSlopeCTRLL3.SelectedIndex > 3)
            {
                return -1;
            }
            data[0] |= (byte)(cbTIARegSlopeCTRLL3.SelectedIndex << 6);

            SetPage(Page23);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x08, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage00Addr0A()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbATPChannel.SelectedIndex < 0 || cbATPChannel.SelectedIndex > 3)
            {
                return -1;
            }
            data[0] |= (byte)(cbATPChannel.SelectedIndex);

            if (cbATPCode.SelectedIndex < 0 || cbATPCode.SelectedIndex > 63)
            {
                return -1;
            }
            data[0] |= (byte)(cbATPCode.SelectedIndex << 2);

            SetPage(Page00);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x0A, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage1FAddr0A()
        {
            byte[] data = new byte[1];
            int rv;

            byte selRefLow = (byte)(cbSelRefLowAll.SelectedIndex & 0x01);

            int tuneRef = cbTuneRefAll.SelectedIndex;
            if (tuneRef < 0 || tuneRef > 15)
                return -1;

            byte tuneRefBits = (byte)((tuneRef & 0x0F) << 1);

            data[0] = (byte)(tuneRefBits | selRefLow); //merge

            SetPage(Page1F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0A, 1, data);
            return (rv < 0) ? -1 : 0;
        }
        private int _WritePage20Addr0A()
        {
            byte[] data = new byte[1];
            int rv;

            byte selRefLow = (byte)(cbSelRefLowL0.SelectedIndex & 0x01);

            int tuneRef = cbTuneRefL0.SelectedIndex;
            if (tuneRef < 0 || tuneRef > 15)
                return -1;

            byte tuneRefBits = (byte)((tuneRef & 0x0F) << 1);

            data[0] = (byte)(tuneRefBits | selRefLow); //merge

            SetPage(Page20);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0A, 1, data);
            return (rv < 0) ? -1 : 0;
        }
        private int _WritePage21Addr0A()
        {
            byte[] data = new byte[1];
            int rv;

            byte selRefLow = (byte)(cbSelRefLowL1.SelectedIndex & 0x01);

            int tuneRef = cbTuneRefL1.SelectedIndex;
            if (tuneRef < 0 || tuneRef > 15)
                return -1;

            byte tuneRefBits = (byte)((tuneRef & 0x0F) << 1);

            data[0] = (byte)(tuneRefBits | selRefLow); //merge

            SetPage(Page21);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0A, 1, data);
            return (rv < 0) ? -1 : 0;
        }
        private int _WritePage22Addr0A()
        {
            byte[] data = new byte[1];
            int rv;

            byte selRefLow = (byte)(cbSelRefLowL2.SelectedIndex & 0x01);

            int tuneRef = cbTuneRefL2.SelectedIndex;
            if (tuneRef < 0 || tuneRef > 15)
                return -1;

            byte tuneRefBits = (byte)((tuneRef & 0x0F) << 1);

            data[0] = (byte)(tuneRefBits | selRefLow); //merge

            SetPage(Page22);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0A, 1, data);
            return (rv < 0) ? -1 : 0;
        }
        private int _WritePage23Addr0A()
        {
            byte[] data = new byte[1];
            int rv;

            byte selRefLow = (byte)(cbSelRefLowL3.SelectedIndex & 0x01);

            int tuneRef = cbTuneRefL3.SelectedIndex;
            if (tuneRef < 0 || tuneRef > 15)
                return -1;

            byte tuneRefBits = (byte)((tuneRef & 0x0F) << 1);

            data[0] = (byte)(tuneRefBits | selRefLow); //merge

            SetPage(Page23);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0A, 1, data);
            return (rv < 0) ? -1 : 0;
        }
        private int _WritePage1FAddr0B()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbBGlpolyCodeAll.SelectedIndex < 0 || cbBGlpolyCodeAll.SelectedIndex > 15)
            {
                return -1;
            }
            data[0] |= (byte)(cbBGlpolyCodeAll.SelectedIndex << 4);

            SetPage(Page1F);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x0B, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage20Addr0B()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbBGlpolyCodeL0.SelectedIndex < 0 || cbBGlpolyCodeL0.SelectedIndex > 15)
            {
                return -1;
            }
            data[0] |= (byte)(cbBGlpolyCodeL0.SelectedIndex << 4);

            SetPage(Page20);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x0B, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage21Addr0B()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbBGlpolyCodeL1.SelectedIndex < 0 || cbBGlpolyCodeL1.SelectedIndex > 15)
            {
                return -1;
            }
            data[0] |= (byte)(cbBGlpolyCodeL1.SelectedIndex << 4);

            SetPage(Page21);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x0B, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage22Addr0B()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbBGlpolyCodeL2.SelectedIndex < 0 || cbBGlpolyCodeL2.SelectedIndex > 15)
            {
                return -1;
            }
            data[0] |= (byte)(cbBGlpolyCodeL2.SelectedIndex << 4);

            SetPage(Page22);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x0B, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage23Addr0B()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbBGlpolyCodeL3.SelectedIndex < 0 || cbBGlpolyCodeL3.SelectedIndex > 15)
            {
                return -1;
            }
            data[0] |= (byte)(cbBGlpolyCodeL3.SelectedIndex << 4);

            SetPage(Page23);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x0B, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage1FAddr0C()
        {
            byte vga = (byte)(cbDisableVREGVGAAutoSweepAll.SelectedIndex & 0x01);
            byte tia = (byte)(cbDisableVREGTIAAutoSweepAll.SelectedIndex & 0x01);

            byte[] data = new byte[1];
            data[0] = (byte)((vga << 6) | (tia << 7));

            SetPage(Page1F);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x0C, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }
        private int _WritePage20Addr0C()
        {
            byte vga = (byte)(cbDisableVREGVGAAutoSweepL0.SelectedIndex & 0x01);
            byte tia = (byte)(cbDisableVREGTIAAutoSweepL0.SelectedIndex & 0x01);

            byte[] data = new byte[1];
            data[0] = (byte)((vga << 6) | (tia << 7));

            SetPage(Page20);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x0C, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage21Addr0C()
        {
            byte vga = (byte)(cbDisableVREGVGAAutoSweepL1.SelectedIndex & 0x01);
            byte tia = (byte)(cbDisableVREGTIAAutoSweepL1.SelectedIndex & 0x01);

            byte[] data = new byte[1];
            data[0] = (byte)((vga << 6) | (tia << 7));

            SetPage(Page21);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x0C, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage22Addr0C()
        {
            byte vga = (byte)(cbDisableVREGVGAAutoSweepL2.SelectedIndex & 0x01);
            byte tia = (byte)(cbDisableVREGTIAAutoSweepL2.SelectedIndex & 0x01);

            byte[] data = new byte[1];
            data[0] = (byte)((vga << 6) | (tia << 7));

            SetPage(Page22);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x0C, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage23Addr0C()
        {
            byte vga = (byte)(cbDisableVREGVGAAutoSweepL3.SelectedIndex & 0x01);
            byte tia = (byte)(cbDisableVREGTIAAutoSweepL3.SelectedIndex & 0x01);

            byte[] data = new byte[1];
            data[0] = (byte)((vga << 6) | (tia << 7));

            SetPage(Page23);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x0C, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage1FAddr0D()
        {
            byte[] data = new byte[1];
            data[0] = 32;

            int targetIndex = cbTIABWCtrlSwingAll.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page1F);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x0D, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage20Addr0D()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbTIABWCtrlSwingL0.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page20);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x0D, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage21Addr0D()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbTIABWCtrlSwingL1.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page21);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x0D, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage22Addr0D()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbTIABWCtrlSwingL2.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page22);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x0D, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage23Addr0D()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            int targetIndex = cbTIABWCtrlSwingL3.SelectedIndex;
            if (targetIndex < 0 || targetIndex > 63)
            {
                return -1;
            }
            // Set bits [5:0] with the selected index value
            data[0] |= (byte)(targetIndex & 0x3F);

            SetPage(Page23);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x0D, 1, data);
            if (rv < 0)
            {
                return -1;
            }
            return 0;
        }
        private int _WritePage00Addr10()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbSAATP2P9.SelectedIndex < 0 || cbSAATP2P9.SelectedIndex > 1)
            {
                return -1;
            }
            data[0] |= (byte)(cbSAATP2P9.SelectedIndex << 2);

            SetPage(Page00);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x10, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage1FAddr19()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbRSSILOSHysteresisAll.SelectedIndex < 0 || cbRSSILOSHysteresisAll.SelectedIndex > 4)
            {
                return -1;
            }
            byte bTmp = Convert.ToByte(cbRSSILOSHysteresisAll.SelectedIndex);
            data[0] |= (byte)(bTmp << 2);

            if (cbSquelchOnLOSAll.SelectedIndex < 0 || cbSquelchOnLOSAll.SelectedIndex > 1)
            {
                return -1;
            }
            bTmp = Convert.ToByte(cbSquelchOnLOSAll.SelectedIndex);
            data[0] |= (byte)(bTmp << 7);

            SetPage(Page1F);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x19, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage20Addr19()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbRSSILOSHysteresisL0.SelectedIndex < 0 || cbRSSILOSHysteresisL0.SelectedIndex > 4)
            {
                return -1;
            }
            byte bTmp = Convert.ToByte(cbRSSILOSHysteresisL0.SelectedIndex);
            data[0] |= (byte)(bTmp << 2);

            if (cbSquelchOnLOSL0.SelectedIndex < 0 || cbSquelchOnLOSL0.SelectedIndex > 1)
            {
                return -1;
            }
            bTmp = Convert.ToByte(cbSquelchOnLOSL0.SelectedIndex);
            data[0] |= (byte)(bTmp << 7);

            SetPage(Page20);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x19, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage21Addr19()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbRSSILOSHysteresisL1.SelectedIndex < 0 || cbRSSILOSHysteresisL1.SelectedIndex > 4)
            {
                return -1;
            }
            byte bTmp = Convert.ToByte(cbRSSILOSHysteresisL1.SelectedIndex);
            data[0] |= (byte)(bTmp << 2);

            if (cbSquelchOnLOSL1.SelectedIndex < 0 || cbSquelchOnLOSL1.SelectedIndex > 1)
            {
                return -1;
            }
            bTmp = Convert.ToByte(cbSquelchOnLOSL1.SelectedIndex);
            data[0] |= (byte)(bTmp << 7);

            SetPage(Page21);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x19, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage22Addr19()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbRSSILOSHysteresisL3.SelectedIndex < 0 || cbRSSILOSHysteresisL3.SelectedIndex > 4)
            {
                return -1;
            }
            byte bTmp = Convert.ToByte(cbRSSILOSHysteresisL3.SelectedIndex);
            data[0] |= (byte)(bTmp << 2);

            if (cbSquelchOnLOSL3.SelectedIndex < 0 || cbSquelchOnLOSL3.SelectedIndex > 1)
            {
                return -1;
            }
            bTmp = Convert.ToByte(cbSquelchOnLOSL3.SelectedIndex);
            data[0] |= (byte)(bTmp << 7);

            SetPage(Page23);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x19, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage23Addr19()
        {
            byte[] data = new byte[1];
            data[0] = 0;

            if (cbRSSILOSHysteresisL3.SelectedIndex < 0 || cbRSSILOSHysteresisL3.SelectedIndex > 4)
            {
                return -1;
            }
            byte bTmp = Convert.ToByte(cbRSSILOSHysteresisL3.SelectedIndex);
            data[0] |= (byte)(bTmp << 2);

            if (cbSquelchOnLOSL3.SelectedIndex < 0 || cbSquelchOnLOSL3.SelectedIndex > 1)
            {
                return -1;
            }
            bTmp = Convert.ToByte(cbSquelchOnLOSL3.SelectedIndex);
            data[0] |= (byte)(bTmp << 7);

            SetPage(Page23);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0x19, 1, data);
            if (rv < 0)
            {
                return -1;
            }

            return 0;
        }
        private int _WritePage1FAddr1A()
        {
            byte[] data = new byte[1];
            int rv;

            if (cbLOSDeassertthresholdAll.SelectedIndex < 0 || cbLOSDeassertthresholdAll.SelectedIndex > 15)
            {
                return -1;
            }
            byte bTmp = Convert.ToByte(cbLOSDeassertthresholdAll.SelectedIndex);
            data[0] = bTmp;


            SetPage(Page1F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x1A, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage20Addr1A()
        {
            byte[] data = new byte[1];
            int rv;

            if (cbLOSDeassertthresholdL0.SelectedIndex < 0 || cbLOSDeassertthresholdL0.SelectedIndex > 15)
            {
                return -1;
            }
            byte bTmp = Convert.ToByte(cbLOSDeassertthresholdL0.SelectedIndex);
            data[0] = bTmp;


            SetPage(Page20);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x1A, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage21Addr1A()
        {
            byte[] data = new byte[1];
            int rv;

            if (cbLOSDeassertthresholdL1.SelectedIndex < 0 || cbLOSDeassertthresholdL1.SelectedIndex > 15)
            {
                return -1;
            }
            byte bTmp = Convert.ToByte(cbLOSDeassertthresholdL1.SelectedIndex);
            data[0] = bTmp;


            SetPage(Page21);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x1A, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage22Addr1A()
        {
            byte[] data = new byte[1];
            int rv;

            if (cbLOSDeassertthresholdL2.SelectedIndex < 0 || cbLOSDeassertthresholdL2.SelectedIndex > 15)
            {
                return -1; // 錯誤代碼：無效索引
            }
            byte bTmp = Convert.ToByte(cbLOSDeassertthresholdL2.SelectedIndex);
            data[0] = bTmp;


            SetPage(Page22);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x1A, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage23Addr1A()
        {
            byte[] data = new byte[1];
            int rv;

            if (cbLOSDeassertthresholdL3.SelectedIndex < 0 || cbLOSDeassertthresholdL3.SelectedIndex > 15)
            {
                return -1;
            }
            byte bTmp = Convert.ToByte(cbLOSDeassertthresholdL3.SelectedIndex);
            data[0] = bTmp;


            SetPage(Page23);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x1A, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage1FAddrC0()
        {
            ComboboxItem selectedItem = cbAGCValueAll.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)(value & 0xFF);

            //MessageBox.Show($"Write Page 0x1F Addr 0xC0, Value = {value:X2}", "Debug All", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetPage(Page1F);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xC0, 1, data);
        }
        private int _WritePage20AddrC0()
        {
            ComboboxItem selectedItem = cbAGCValueL0.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)(value & 0xFF);

            //MessageBox.Show($"Write Page 0x20 Addr 0xC0, Value = {value:X2}", "Debug L0", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetPage(Page20);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xC0, 1, data);
        }
        private int _WritePage21AddrC0()
        {
            ComboboxItem selectedItem = cbAGCValueL1.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)(value & 0xFF);

            //MessageBox.Show($"Write Page 0x21 Addr 0xC0, Value = {value:X2}", "Debug L1", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetPage(Page21);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xC0, 1, data);
        }
        private int _WritePage22AddrC0()
        {
            ComboboxItem selectedItem = cbAGCValueL2.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)(value & 0xFF);

            //MessageBox.Show($"Write Page 0x22 Addr 0xC0, Value = {value:X2}", "Debug L2", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetPage(Page22);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xC0, 1, data);
        }
        private int _WritePage23AddrC0()
        {
            ComboboxItem selectedItem = cbAGCValueL3.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)(value & 0xFF);

            //MessageBox.Show($"Write Page 0x23 Addr 0xC0, Value = {value:X2}", "Debug L3", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetPage(Page23);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xC0, 1, data);
        }
        private int _WritePage1FAddrC1()
        {
            ComboboxItem selectedItem = cbAGCValueAll.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)((value >> 8) & 0x07);

            //MessageBox.Show($"Write Page 0x1F Addr 0xC1, Value = {value:X2}", "Debug All", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetPage(Page1F);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xC1, 1, data);
        }
        private int _WritePage20AddrC1()
        {
            ComboboxItem selectedItem = cbAGCValueL0.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)((value >> 8) & 0x07);

            //MessageBox.Show($"Write Page 0x20 Addr 0xC1, Value = {value:X2}", "Debug L0", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetPage(Page20);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xC1, 1, data);
        }
        private int _WritePage21AddrC1()
        {
            ComboboxItem selectedItem = cbAGCValueL1.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)((value >> 8) & 0x07);

            //MessageBox.Show($"Write Page 0x21 Addr 0xC1, Value = {value:X2}", "Debug L1", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetPage(Page21);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xC1, 1, data);
        }
        private int _WritePage22AddrC1()
        {
            ComboboxItem selectedItem = cbAGCValueL2.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)((value >> 8) & 0x07);

            //MessageBox.Show($"Write Page 0x22 Addr 0xC1, Value = {value:X2}", "Debug L2", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetPage(Page22);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xC1, 1, data);
        }
        private int _WritePage23AddrC1()
        {
            ComboboxItem selectedItem = cbAGCValueL3.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)((value >> 8) & 0x07);

            //MessageBox.Show($"Write Page 0x23 Addr 0xC1, Value = {value:X2}", "Debug L3", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetPage(Page23);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xC1, 1, data);
        }
        private int _WritePage1FAddrC2()
        {
            byte[] data = new byte[1];
            ComboboxItem selectedItem = cbAGCAdaptationModeAll.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            data[0] = Convert.ToByte(selectedItem.Value);

            SetPage(Page1F);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0xC2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage20AddrC2()
        {
            byte[] data = new byte[1];
            ComboboxItem selectedItem = cbAGCAdaptationModeL0.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            data[0] = Convert.ToByte(selectedItem.Value);

            SetPage(Page20);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0xC2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage21AddrC2()
        {
            byte[] data = new byte[1];
            ComboboxItem selectedItem = cbAGCAdaptationModeL1.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            data[0] = Convert.ToByte(selectedItem.Value);

            SetPage(Page21);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0xC2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage22AddrC2()
        {
            byte[] data = new byte[1];
            ComboboxItem selectedItem = cbAGCAdaptationModeL2.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            data[0] = Convert.ToByte(selectedItem.Value);

            SetPage(Page22);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0xC2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage23AddrC2()
        {
            byte[] data = new byte[1];
            ComboboxItem selectedItem = cbAGCAdaptationModeL3.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            data[0] = Convert.ToByte(selectedItem.Value);

            SetPage(Page23);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0xC2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage1FAddrD0()
        {
            ComboboxItem selectedItem = cbDCDValueAll.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)(value & 0xFF);

            SetPage(Page1F);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xD0, 1, data);
        }
        private int _WritePage20AddrD0()
        {
            ComboboxItem selectedItem = cbDCDValueL0.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)(value & 0xFF);

            SetPage(Page20);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xD0, 1, data);
        }
        private int _WritePage21AddrD0()
        {
            ComboboxItem selectedItem = cbDCDValueL1.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)(value & 0xFF);

            SetPage(Page21);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xD0, 1, data);
        }
        private int _WritePage22AddrD0()
        {
            ComboboxItem selectedItem = cbDCDValueL2.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)(value & 0xFF);

            SetPage(Page22);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xD0, 1, data);
        }
        private int _WritePage23AddrD0()
        {
            ComboboxItem selectedItem = cbDCDValueL3.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)(value & 0xFF);

            SetPage(Page23);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xD0, 1, data);
        }
        private int _WritePage1FAddrD1()
        {
            ComboboxItem selectedItem = cbDCDValueAll.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)((value >> 8) & 0x07);

            SetPage(Page1F);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xD1, 1, data);
        }
        private int _WritePage20AddrD1()
        {
            ComboboxItem selectedItem = cbDCDValueL0.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)((value >> 8) & 0x07);

            SetPage(Page20);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xD1, 1, data);
        }
        private int _WritePage21AddrD1()
        {
            ComboboxItem selectedItem = cbDCDValueL1.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)((value >> 8) & 0x07);

            SetPage(Page21);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xD1, 1, data);
        }
        private int _WritePage22AddrD1()
        {
            ComboboxItem selectedItem = cbDCDValueL2.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)((value >> 8) & 0x07);

            SetPage(Page22);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xD1, 1, data);
        }
        private int _WritePage23AddrD1()
        {
            ComboboxItem selectedItem = cbDCDValueL3.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)((value >> 8) & 0x07);

            SetPage(Page23);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xD1, 1, data);
        }
        private int _WritePage1FAddrD2()
        {
            byte[] data = new byte[1];
            ComboboxItem selectedItem = cbDCDAdaptationModeAll.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            data[0] = Convert.ToByte(selectedItem.Value);

            SetPage(Page1F);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0xD2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage20AddrD2()
        {
            byte[] data = new byte[1];
            ComboboxItem selectedItem = cbDCDAdaptationModeL0.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            data[0] = Convert.ToByte(selectedItem.Value);

            SetPage(Page20);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0xD2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage21AddrD2()
        {
            byte[] data = new byte[1];
            ComboboxItem selectedItem = cbDCDAdaptationModeL1.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            data[0] = Convert.ToByte(selectedItem.Value);

            SetPage(Page21);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0xD2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage22AddrD2()
        {
            byte[] data = new byte[1];
            ComboboxItem selectedItem = cbDCDAdaptationModeL2.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            data[0] = Convert.ToByte(selectedItem.Value);

            SetPage(Page22);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0xD2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage23AddrD2()
        {
            byte[] data = new byte[1];
            ComboboxItem selectedItem = cbDCDAdaptationModeL3.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            data[0] = Convert.ToByte(selectedItem.Value);

            SetPage(Page23);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0xD2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage1FAddrE0()
        {
            ComboboxItem selectedItem = cbBiasValueAll.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)(value & 0xFF);

            SetPage(Page1F);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xE0, 1, data);
        }
        private int _WritePage20AddrE0()
        {
            ComboboxItem selectedItem = cbBiasValueL0.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)(value & 0xFF);

            SetPage(Page20);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xE0, 1, data);
        }
        private int _WritePage21AddrE0()
        {
            ComboboxItem selectedItem = cbBiasValueL1.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)(value & 0xFF);

            SetPage(Page21);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xE0, 1, data);
        }
        private int _WritePage22AddrE0()
        {
            ComboboxItem selectedItem = cbBiasValueL2.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)(value & 0xFF);

            SetPage(Page22);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xE0, 1, data);
        }
        private int _WritePage23AddrE0()
        {
            ComboboxItem selectedItem = cbBiasValueL3.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)(value & 0xFF);

            SetPage(Page23);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xE0, 1, data);
        }
        private int _WritePage1FAddrE1()
        {
            ComboboxItem selectedItem = cbBiasValueAll.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)((value >> 8) & 0x07);

            SetPage(Page1F);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xE1, 1, data);
        }
        private int _WritePage20AddrE1()
        {
            ComboboxItem selectedItem = cbBiasValueL0.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)((value >> 8) & 0x07);

            SetPage(Page20);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xE1, 1, data);
        }
        private int _WritePage21AddrE1()
        {
            ComboboxItem selectedItem = cbBiasValueL1.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)((value >> 8) & 0x07);

            SetPage(Page21);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xE1, 1, data);
        }
        private int _WritePage22AddrE1()
        {
            ComboboxItem selectedItem = cbBiasValueL2.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)((value >> 8) & 0x07);

            SetPage(Page22);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xE1, 1, data);
        }
        private int _WritePage23AddrE1()
        {
            ComboboxItem selectedItem = cbBiasValueL3.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            int value = (int)selectedItem.Value;
            byte[] data = new byte[1];
            data[0] = (byte)((value >> 8) & 0x07);

            SetPage(Page23);
            Thread.Sleep(100);
            return i2cWriteCB(devAddr, 0xE1, 1, data);
        }
        private int _WritePage1FAddrE2()
        {
            byte[] data = new byte[1];
            ComboboxItem selectedItem = cbBiasAdaptationModeAll.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            data[0] = Convert.ToByte(selectedItem.Value);

            SetPage(Page1F);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0xE2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage20AddrE2()
        {
            byte[] data = new byte[1];
            ComboboxItem selectedItem = cbBiasAdaptationModeL0.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            data[0] = Convert.ToByte(selectedItem.Value);

            SetPage(Page20);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0xE2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage21AddrE2()
        {
            byte[] data = new byte[1];
            ComboboxItem selectedItem = cbBiasAdaptationModeL1.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            data[0] = Convert.ToByte(selectedItem.Value);

            SetPage(Page21);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0xE2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage22AddrE2()
        {
            byte[] data = new byte[1];
            ComboboxItem selectedItem = cbBiasAdaptationModeL2.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            data[0] = Convert.ToByte(selectedItem.Value);

            SetPage(Page22);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0xE2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        private int _WritePage23AddrE2()
        {
            byte[] data = new byte[1];
            ComboboxItem selectedItem = cbBiasAdaptationModeL3.SelectedItem as ComboboxItem;
            if (selectedItem == null)
                return -1;

            data[0] = Convert.ToByte(selectedItem.Value);

            SetPage(Page23);
            Thread.Sleep(100);

            int rv = i2cWriteCB(devAddr, 0xE2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }
        #endregion

        #region Event L0
        private void cbPowerDownChannelL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr00() < 0)
                return;
        }
        private void cbOutputPreEmphasisL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr00() < 0)
                return;
        }
        private void cbTargetOutputSwingL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr02() < 0)
                return;
        }
        private void cbTIARegulatorsRefL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr01() < 0)
                return;
        }
        private void cbVGARegulatorsRefL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr01() < 0)
                return;
        }
        private void cbManualVGA0FilterL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr03() < 0)
                return;
        }
        private void cbManualVGA1FilterL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr04() < 0)
                return;
        }
        private void cbManualVGA2FilterL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr05() < 0)
                return;
        }
        private void cbVGARegSlopeCTRLL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr08() < 0)
                return;
        }
        private void cbTIARegSlopeCTRLL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr08() < 0)
                return;
        }
        private void cbSelRefLowL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr0A() < 0)
                return;
        }
        private void cbTuneRefL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr0A() < 0)
                return;
        }
        private void cbBGlpolyCodeL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr0B() < 0)
                return;
        }
        private void cbDisableVREGVGAAutoSweepL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr0C() < 0)
                return;
        }
        private void cbDisableVREGTIAAutoSweepL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr0C() < 0)
                return;
        }
        private void cbTIABWCtrlSwingL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr0D() < 0)
                return;
        }
        private void cbRSSILOSHysteresisL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr19() < 0)
                return;
        }
        private void cbSquelchOnLOSL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr19() < 0)
                return;
        }
        private void cbLOSDeassertthresholdL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page20);
            if (_WritePage20Addr1A() < 0)
                return;
        }
        private void cbAGCValueL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading) return;

            SetPage(Page20);
            if (_WritePage20AddrC0() < 0 || _WritePage20AddrC1() < 0)
                return;
        }
        private void cbAGCAdaptationModeL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbAGCValueL0.Enabled = (cbAGCAdaptationModeL0.SelectedIndex == 0);

            SetPage(Page20);
            if (_WritePage20AddrC2() < 0)
                return;
        }
        private void cbDCDValueL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading) return;

            if (_WritePage20AddrD0() < 0 || _WritePage20AddrD1() < 0)
                return;
        }
        private void cbDCDAdaptationModeL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbDCDValueL0.Enabled = cbDCDAdaptationModeL0.SelectedIndex == 0;

            SetPage(Page20);
            if (_WritePage20AddrD2() < 0)
                return;
        }
        private void cbBiasValueL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading) return;

            if (_WritePage20AddrE0() < 0||  _WritePage20AddrE1() < 0)
                return;
        }
        private void cbBiasAdaptationModeL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbBiasValueL0.Enabled = cbBiasAdaptationModeL0.SelectedIndex == 0;

            SetPage(Page20);
            if (_WritePage20AddrE2() < 0)
                return;
        }
        #endregion

        #region Event L1
        private void cbPowerDownChannelL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr00() < 0)
                return;
        }
        private void cbOutputPreEmphasisL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr00() < 0)
                return;
        }
        private void cbTargetOutputSwingL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr02() < 0)
                return;
        }
        private void cbTIARegulatorsRefL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr01() < 0)
                return;
        }
        private void cbVGARegulatorsRefL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr01() < 0)
                return;
        }
        private void cbManualVGA0FilterL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr03() < 0)
                return;
        }
        private void cbManualVGA1FilterL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr04() < 0)
                return;
        }
        private void cbManualVGA2FilterL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr05() < 0)
                return;
        }
        private void cbVGARegSlopeCTRLL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr08() < 0)
                return;
        }
        private void cbTIARegSlopeCTRLL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr08() < 0)
                return;
        }
        private void cbSelRefLowL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr0A() < 0)
                return;
        }
        private void cbTuneRefL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr0A() < 0)
                return;
        }
        private void cbBGlpolyCodeL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr0B() < 0)
                return;
        }
        private void cbDisableVREGVGAAutoSweepL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr0C() < 0)
                return;
        }
        private void cbDisableVREGTIAAutoSweepL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr0C() < 0)
                return;
        }
        private void cbTIABWCtrlSwingL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr0D() < 0)
                return;
        }
        private void cbRSSILOSHysteresisL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr19() < 0)
                return;
        }
        private void cbSquelchOnLOSL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr19() < 0)
                return;
        }
        private void cbLOSDeassertthresholdL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21Addr1A() < 0)
                return;
        }
        private void cbAGCValueL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading) return;

            SetPage(Page21);
            if (_WritePage21AddrC0() < 0 || _WritePage21AddrC1() < 0)
                return;
        }
        private void cbAGCAaptationModeL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbAGCValueL1.Enabled = (cbAGCAdaptationModeL1.SelectedIndex == 0);

            SetPage(Page21);
            if (_WritePage21AddrC2() < 0)
                return;
        }
        private void cbDCDValueL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21AddrD0() < 0||  _WritePage21AddrD1() < 0)
                return;
        }
        private void cbDCDAdaptationModeL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbDCDValueL1.Enabled = cbDCDAdaptationModeL1.SelectedIndex == 0;

            SetPage(Page21);
            if (_WritePage21AddrD2() < 0)
                return;
        }
        private void cbBiasValueL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page21);
            if (_WritePage21AddrE0() < 0||  _WritePage21AddrE1() < 0)
                return;
        }
        private void cbBiasAdaptationModeL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbBiasValueL1.Enabled = cbBiasAdaptationModeL1.SelectedIndex == 0;

            SetPage(Page21);
            if (_WritePage21AddrE2() < 0)
                return;
        }
        #endregion

        #region Event L2
        private void cbPowerDownChannelL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr00() < 0)
                return;
        }
        private void cbOutputPreEmphasisL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr00() < 0)
                return;
        }
        private void cbTargetOutputSwingL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr02() < 0)
                return;
        }
        private void cbTIARegulatorsRefL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr01() < 0)
                return;
        }
        private void cbVGARegulatorsRefL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr01() < 0)
                return;
        }
        private void cbManualVGA0FilterL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr03() < 0)
                return;
        }
        private void cbManualVGA1FilterL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr04() < 0)
                return;
        }
        private void cbManualVGA2FilterL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr05() < 0)
                return;
        }
        private void cbVGARegSlopeCTRLL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr08() < 0)
                return;
        }
        private void cbTIARegSlopeCTRLL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr08() < 0)
                return;
        }
        private void cbSelRefLowL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr0A() < 0)
                return;
        }
        private void cbTuneRefL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr0A() < 0)
                return;
        }
        private void cbBGlpolyCodeL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr0B() < 0)
                return;
        }
        private void cbDisableVREGVGAAutoSweepL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr0C() < 0)
                return;
        }
        private void cbDisableVREGTIAAutoSweepL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr0C() < 0)
                return;
        }
        private void cbTIABWCtrlSwingL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr0D() < 0)
                return;
        }
        private void cbRSSILOSHysteresisL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr19() < 0)
                return;
        }
        private void cbSquelchOnLOSL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr19() < 0)
                return;
        }
        private void cbLOSDeassertthresholdL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22Addr1A() < 0)
                return;
        }
        private void cbAGCValueL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading) return;

            SetPage(Page22);
            if (_WritePage22AddrC0() < 0 || _WritePage22AddrC1() < 0)
                return;
        }
        private void cbAGCAaptationModeL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbAGCValueL2.Enabled = (cbAGCAdaptationModeL2.SelectedIndex == 0);

            SetPage(Page22);
            if (_WritePage22AddrC2() < 0)
                return;
        }
        private void cbDCDValueL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22AddrD0() < 0||  _WritePage22AddrD1() < 0)
                return;
        }
        private void cbDCDAdaptationModeL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbDCDValueL2.Enabled = cbDCDAdaptationModeL2.SelectedIndex == 0;

            SetPage(Page22);
            if (_WritePage22AddrD2() < 0)
                return;
        }
        private void cbBiasValueL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page22);
            if (_WritePage22AddrE0() < 0||  _WritePage22AddrE1() < 0)
                return;
        }
        private void cbBiasAdaptationModeL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbBiasValueL2.Enabled = cbBiasAdaptationModeL2.SelectedIndex == 0;

            SetPage(Page22);
            if (_WritePage22AddrE2() < 0)
                return;
        }
        #endregion

        #region Event L3
        private void cbPowerDownChannelL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr00() < 0)
                return;
        }
        private void cbOutputPreEmphasisL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr00() < 0)
                return;
        }
        private void cbTargetOutputSwingL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr02() < 0)
                return;
        }
        private void cbTIARegulatorsRefL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr01() < 0)
                return;
        }
        private void cbVGARegulatorsRefL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr01() < 0)
                return;
        }
        private void cbManualVGA0FilterL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr03() < 0)
                return;
        }
        private void cbManualVGA1FilterL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr04() < 0)
                return;
        }
        private void cbManualVGA2FilterL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr05() < 0)
                return;
        }
        private void cbVGARegSlopeCTRLL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr08() < 0)
                return;
        }
        private void cbTIARegSlopeCTRLL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr08() < 0)
                return;
        }
        private void cbSelRefLowL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr0A() < 0)
                return;
        }
        private void cbTuneRefL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr0A() < 0)
                return;
        }
        private void cbBGlpolyCodeL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr0B() < 0)
                return;
        }
        private void cbDisableVREGVGAAutoSweepL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr0C() < 0)
                return;
        }
        private void cbDisableVREGTIAAutoSweepL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr0C() < 0)
                return;
        }
        private void cbTIABWCtrlSwingL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr0D() < 0)
                return;
        }
        private void cbRSSILOSHysteresisL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr19() < 0)
                return;
        }
        private void cbSquelchOnLOSL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr19() < 0)
                return;
        }
        private void cbLOSDeassertthresholdL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23Addr1A() < 0)
                return;
        }
        private void cbAGCValueL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading) return;

            SetPage(Page23);
            if (_WritePage23AddrC0() < 0 || _WritePage23AddrC1() < 0)
                return;
        }
        private void cbAGCAaptationModeL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbAGCValueL3.Enabled = (cbAGCAdaptationModeL3.SelectedIndex == 0);

            SetPage(Page23);
            if (_WritePage23AddrC2() < 0)
                return;
        }
        private void cbDCDValueL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23AddrD0() < 0||  _WritePage23AddrD1() < 0)
                return;
        }
        private void cbDCDAdaptationModeL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbDCDValueL3.Enabled = cbDCDAdaptationModeL3.SelectedIndex == 0;

            SetPage(Page23);
            if (_WritePage23AddrD2() < 0)
                return;
        }
        private void cbBiasValueL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page23);
            if (_WritePage23AddrE0() < 0||  _WritePage23AddrE1() < 0)
                return;
        }
        private void cbBiasAdaptationModeL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbBiasValueL3.Enabled = cbBiasAdaptationModeL3.SelectedIndex == 0;

            SetPage(Page23);
            if (_WritePage23AddrE2() < 0)
                return;
        }
        #endregion

        #region Event All
        private void cbPowerDownChannelAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr00() < 0)
                return;
        }
        private void cbOutputPreEmphasisAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr00() < 0)
                return;
        }
        private void cbTargetOutputSwingAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr02() < 0)
                return;
        }
        private void cbTIARegulatorsRefAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr01() < 0)
                return;
        }
        private void cbVGARegulatorsRefAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr01() < 0)
                return;
        }
        private void cbManualVGA0FilterAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr03() < 0)
                return;
        }
        private void cbManualVGA1FilterAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr04() < 0)
                return;
        }
        private void cbManualVGA2FilterAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr05() < 0)
                return;
        }
        private void cbVGARegSlopeCTRLAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr08() < 0)
                return;
        }
        private void cbTIARegSlopeCTRLAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr08() < 0)
                return;
        }
        private void cbSelRefLowAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr0A() < 0)
                return;
        }
        private void cbTuneRefAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr0A() < 0)
                return;
        }
        private void cbBGlpolyCodeAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr0B() < 0)
                return;
        }
        private void cbDisableVREGVGAAutoSweepAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr0C() < 0)
                return;
        }
        private void cbDisableVREGTIAAutoSweepAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr0C() < 0)
                return;
        }
        private void cbTIABWCtrlSwingAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr0D() < 0)
                return;
        }
        private void cbRSSILOSHysteresisAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr19() < 0)
                return;
        }
        private void cbSquelchOnLOSAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr19() < 0)
                return;
        }
        private void cbLOSDeassertthresholdAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddr1A() < 0)
                return;
        }
        private void cbAGCValueAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading) return;

            SetPage(Page1F);
            if (_WritePage1FAddrC0() < 0 || _WritePage1FAddrC1() < 0)
                return;
        }
        private void cbAGCAadptationModeAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbAGCValueAll.Enabled = (cbAGCAdaptationModeAll.SelectedIndex == 0);

            SetPage(Page1F);
            if (_WritePage1FAddrC2() < 0)
                return;
        }
        private void cbDCDValueAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddrD0() < 0||  _WritePage1FAddrD1() < 0)
                return;
        }
        private void cbDCDAdaptationModeAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbDCDValueAll.Enabled = cbDCDAdaptationModeAll.SelectedIndex == 0;

            SetPage(Page1F);
            if (_WritePage1FAddrD2() < 0)
                return;
        }
        private void cbBiasValueAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page1F);
            if (_WritePage1FAddrE0() < 0||  _WritePage1FAddrE1() < 0)
                return;
        }
        private void cbBiasAdaptationModeAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            cbBiasValueAll.Enabled = cbBiasAdaptationModeAll.SelectedIndex == 0;

            SetPage(Page1F);
            if (_WritePage1FAddrE2() < 0)
                return;
        }
        #endregion

        #region Event Control
        private void cbPolarity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page00);
            if (_WritePage00Addr03() < 0)
                return;
        }
        private void cbDriverMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page00);
            if (_WritePage00Addr03() < 0)
                return;
        }
        private void cbOscillatorFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page00);
            if (_WritePage00Addr03() < 0)
                return;
        }
        private void cbVCCEXTVoltage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page00);
            if (_WritePage00Addr03() < 0)
                return;
        }
        private void cblntClkTestMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page00);
            if (_WritePage00Addr08() < 0)
                return;
        }
        private void cbRssiChannelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page00);
            if (_WritePage00Addr03() < 0)
                return;
        }
        private void cb1VRegulator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page00);
            if (_WritePage00Addr04() < 0)
                return;
        }
        private void cbATPCore_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page00);
            if (_WritePage00Addr08() < 0)
                return;
        }
        private void cbATPChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page00);
            if (_WritePage00Addr0A() < 0)
                return;
        }
        private void cbATPCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page00);
            if (_WritePage00Addr0A() < 0)
                return;
        }
        private void cbSAATP2P9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page00);
            if (_WritePage00Addr10() < 0)
                return;
        }
        #endregion



        #region 改寫_ParsePage00AddrXX
        private void _ParsePage00Addr00(byte data)
        {
            tbChipID.Text = "0x" + data.ToString("X2");
        }
        private void _ParsePage00Addr01(byte data)
        {
            tbRevID.Text = "0x" + data.ToString("X2");
        }
        private void _ParsePage00Addr03(byte data)
        {
            if ((data & 0x02) == 0)
                cbPolarity.SelectedIndex = 0;
            else
                cbPolarity.SelectedIndex = 1;

            if ((data & 0x04) == 0)
                cbDriverMode.SelectedIndex = 0;
            else
                cbDriverMode.SelectedIndex = 1;

            if ((data & 0x18) == 0)
                cbOscillatorFreq.SelectedIndex = 0;
            else if ((data & 0x18) == 0x08)
                cbOscillatorFreq.SelectedIndex = 1;
            else if ((data & 0x18) == 0x10)
                cbOscillatorFreq.SelectedIndex = 2;
            else if ((data & 0x18) == 0x18)
                cbOscillatorFreq.SelectedIndex = 3;

            if ((data & 0x60) == 0)
                cbRssiChannelSelect.SelectedIndex = 0;
            else if ((data & 0x60) == 0x20)
                cbRssiChannelSelect.SelectedIndex = 1;
            else if ((data & 0x60) == 0x40)
                cbRssiChannelSelect.SelectedIndex = 2;
            else if ((data & 0x60) == 0x60)
                cbRssiChannelSelect.SelectedIndex = 3;

            if ((data & 0x80) == 0)
                cbVCCEXTVoltage.SelectedIndex = 0;
            else
                cbVCCEXTVoltage.SelectedIndex = 1;
        }
        private void _ParsePage00Addr04(byte data)
        {
            if ((data & 0x03) == 0)
                cb1VRegulator.SelectedIndex = 0;
            else if ((data & 0x03) == 0x01)
                cb1VRegulator.SelectedIndex = 1;
            else if ((data & 0x03) == 0x02)
                cb1VRegulator.SelectedIndex = 2;
            else if ((data & 0x03) == 0x03)
                cb1VRegulator.SelectedIndex = 3;
        }
        private void _ParsePage00Addr08(byte data)
        {
            if ((data & 0x07) == 0)
                cbATPCore.SelectedIndex = 0;
            else if ((data & 0x07) == 0x01)
                cbATPCore.SelectedIndex = 1;
            else if ((data & 0x07) == 0x02)
                cbATPCore.SelectedIndex = 2;
            else if ((data & 0x07) == 0x03)
                cbATPCore.SelectedIndex = 3;
            else if ((data & 0x07) == 0x04)
                cbATPCore.SelectedIndex = 4;
            else if ((data & 0x07) == 0x05)
                cbATPCore.SelectedIndex = 5;
            else if ((data & 0x07) == 0x06)
                cbATPCore.SelectedIndex = 6;
            else if ((data & 0x07) == 0x07)
                cbATPCore.SelectedIndex = 7;

            if ((data & 0x08) == 0)
                cblntClkTestMode.SelectedIndex = 0;
            else
                cblntClkTestMode.SelectedIndex = 1;
        }
        private void _ParsePage00Addr10(byte data)
        {
            if ((data & 0x04) == 0)
                cbSAATP2P9.SelectedIndex = 0;
            else
                cbSAATP2P9.SelectedIndex = 1;
        }
        private void _ParsePage00Addr0A(byte data)
        {
            if ((data & 0x03) == 0x00)
                cbATPChannel.SelectedIndex = 0;
            else if ((data & 0x03) == 0x01)
                cbATPChannel.SelectedIndex = 1;
            else if ((data & 0x03) == 0x02)
                cbATPChannel.SelectedIndex = 2;
            else if ((data & 0x03) == 0x03)
                cbATPChannel.SelectedIndex = 3;

            if ((data & 0xFC) == 0x00)
                cbATPCode.SelectedIndex = 0;
            else if ((data & 0xFC) == 0x04)
                cbATPCode.SelectedIndex = 1;
            else if ((data & 0xFC) == 0x08)
                cbATPCode.SelectedIndex = 2;
            else if ((data & 0xFC) == 0x0C)
                cbATPCode.SelectedIndex = 3;
            else if ((data & 0xFC) == 0x10)
                cbATPCode.SelectedIndex = 4;
            else if ((data & 0xFC) == 0x14)
                cbATPCode.SelectedIndex = 5;
            else if ((data & 0xFC) == 0x18)
                cbATPCode.SelectedIndex = 6;
            else if ((data & 0xFC) == 0x1C)
                cbATPCode.SelectedIndex = 7;
            else if ((data & 0xFC) == 0x20)
                cbATPCode.SelectedIndex = 8;
            else if ((data & 0xFC) == 0x24)
                cbATPCode.SelectedIndex = 9;
            else if ((data & 0xFC) == 0x28)
                cbATPCode.SelectedIndex = 10;
            else if ((data & 0xFC) == 0x2C)
                cbATPCode.SelectedIndex = 11;
            else if ((data & 0xFC) == 0x30)
                cbATPCode.SelectedIndex = 12;
            else if ((data & 0xFC) == 0x34)
                cbATPCode.SelectedIndex = 13;
            else if ((data & 0xFC) == 0x38)
                cbATPCode.SelectedIndex = 14;
            else if ((data & 0xFC) == 0x3C)
                cbATPCode.SelectedIndex = 15;
            else if ((data & 0xFC) == 0x40)
                cbATPCode.SelectedIndex = 16;
            else if ((data & 0xFC) == 0x44)
                cbATPCode.SelectedIndex = 17;
            else if ((data & 0xFC) == 0x48)
                cbATPCode.SelectedIndex = 18;
            else if ((data & 0xFC) == 0x4C)
                cbATPCode.SelectedIndex = 19;
            else if ((data & 0xFC) == 0x50)
                cbATPCode.SelectedIndex = 20;
            else if ((data & 0xFC) == 0x54)
                cbATPCode.SelectedIndex = 21;
            else if ((data & 0xFC) == 0x58)
                cbATPCode.SelectedIndex = 22;
            else if ((data & 0xFC) == 0x5C)
                cbATPCode.SelectedIndex = 23;
            else if ((data & 0xFC) == 0x60)
                cbATPCode.SelectedIndex = 24;
            else if ((data & 0xFC) == 0x64)
                cbATPCode.SelectedIndex = 25;
            else if ((data & 0xFC) == 0x68)
                cbATPCode.SelectedIndex = 26;
            else if ((data & 0xFC) == 0x6C)
                cbATPCode.SelectedIndex = 27;
            else if ((data & 0xFC) == 0x70)
                cbATPCode.SelectedIndex = 28;
            else if ((data & 0xFC) == 0x74)
                cbATPCode.SelectedIndex = 29;
            else if ((data & 0xFC) == 0x78)
                cbATPCode.SelectedIndex = 30;
            else if ((data & 0xFC) == 0x7C)
                cbATPCode.SelectedIndex = 31;
            else if ((data & 0xFC) == 0x80)
                cbATPCode.SelectedIndex = 32;
            else if ((data & 0xFC) == 0x84)
                cbATPCode.SelectedIndex = 33;
            else if ((data & 0xFC) == 0x88)
                cbATPCode.SelectedIndex = 34;
            else if ((data & 0xFC) == 0x8C)
                cbATPCode.SelectedIndex = 35;
            else if ((data & 0xFC) == 0x90)
                cbATPCode.SelectedIndex = 36;
            else if ((data & 0xFC) == 0x94)
                cbATPCode.SelectedIndex = 37;
            else if ((data & 0xFC) == 0x98)
                cbATPCode.SelectedIndex = 38;
            else if ((data & 0xFC) == 0x9C)
                cbATPCode.SelectedIndex = 39;
            else if ((data & 0xFC) == 0xA0)
                cbATPCode.SelectedIndex = 40;
            else if ((data & 0xFC) == 0xA4)
                cbATPCode.SelectedIndex = 41;
            else if ((data & 0xFC) == 0xA8)
                cbATPCode.SelectedIndex = 42;
            else if ((data & 0xFC) == 0xAC)
                cbATPCode.SelectedIndex = 43;
            else if ((data & 0xFC) == 0xB0)
                cbATPCode.SelectedIndex = 44;
            else if ((data & 0xFC) == 0xB4)
                cbATPCode.SelectedIndex = 45;
            else if ((data & 0xFC) == 0xB8)
                cbATPCode.SelectedIndex = 46;
            else if ((data & 0xFC) == 0xBC)
                cbATPCode.SelectedIndex = 47;
            else if ((data & 0xFC) == 0xC0)
                cbATPCode.SelectedIndex = 48;

        }
        #endregion

        #region 改寫_ParsePage1FAddrXX
        private void _ParsePage1FAddr00(byte data)
        {
            if ((data & 0x01) == 0)
                cbPowerDownChannelAll.Checked = false;
            else
                cbPowerDownChannelAll.Checked = true;

            if ((data & 0xFC) == 0x00)
                cbOutputPreEmphasisAll.SelectedIndex = 0;
            else if ((data & 0xFC) == 0x04)
                cbOutputPreEmphasisAll.SelectedIndex = 1;
            else if ((data & 0xFC) == 0x08)
                cbOutputPreEmphasisAll.SelectedIndex = 2;
            else if ((data & 0xFC) == 0x0C)
                cbOutputPreEmphasisAll.SelectedIndex = 3;
            else if ((data & 0xFC) == 0x10)
                cbOutputPreEmphasisAll.SelectedIndex = 4;
            else if ((data & 0xFC) == 0x14)
                cbOutputPreEmphasisAll.SelectedIndex = 5;
            else if ((data & 0xFC) == 0x18)
                cbOutputPreEmphasisAll.SelectedIndex = 6;
            else if ((data & 0xFC) == 0x1C)
                cbOutputPreEmphasisAll.SelectedIndex = 7;
            else if ((data & 0xFC) == 0x20)
                cbOutputPreEmphasisAll.SelectedIndex = 8;
            else if ((data & 0xFC) == 0x24)
                cbOutputPreEmphasisAll.SelectedIndex = 9;
            else if ((data & 0xFC) == 0x28)
                cbOutputPreEmphasisAll.SelectedIndex = 10;
            else if ((data & 0xFC) == 0x2C)
                cbOutputPreEmphasisAll.SelectedIndex = 11;
            else if ((data & 0xFC) == 0x30)
                cbOutputPreEmphasisAll.SelectedIndex = 12;
            else if ((data & 0xFC) == 0x34)
                cbOutputPreEmphasisAll.SelectedIndex = 13;
            else if ((data & 0xFC) == 0x38)
                cbOutputPreEmphasisAll.SelectedIndex = 14;
            else if ((data & 0xFC) == 0x3C)
                cbOutputPreEmphasisAll.SelectedIndex = 15;
            else if ((data & 0xFC) == 0x40)
                cbOutputPreEmphasisAll.SelectedIndex = 16;
            else if ((data & 0xFC) == 0x44)
                cbOutputPreEmphasisAll.SelectedIndex = 17;
            else if ((data & 0xFC) == 0x48)
                cbOutputPreEmphasisAll.SelectedIndex = 18;
            else if ((data & 0xFC) == 0x4C)
                cbOutputPreEmphasisAll.SelectedIndex = 19;
            else if ((data & 0xFC) == 0x50)
                cbOutputPreEmphasisAll.SelectedIndex = 20;
            else if ((data & 0xFC) == 0x54)
                cbOutputPreEmphasisAll.SelectedIndex = 21;
            else if ((data & 0xFC) == 0x58)
                cbOutputPreEmphasisAll.SelectedIndex = 22;
            else if ((data & 0xFC) == 0x5C)
                cbOutputPreEmphasisAll.SelectedIndex = 23;
            else if ((data & 0xFC) == 0x60)
                cbOutputPreEmphasisAll.SelectedIndex = 24;
            else if ((data & 0xFC) == 0x64)
                cbOutputPreEmphasisAll.SelectedIndex = 25;
            else if ((data & 0xFC) == 0x68)
                cbOutputPreEmphasisAll.SelectedIndex = 26;
            else if ((data & 0xFC) == 0x6C)
                cbOutputPreEmphasisAll.SelectedIndex = 27;
            else if ((data & 0xFC) == 0x70)
                cbOutputPreEmphasisAll.SelectedIndex = 28;
            else if ((data & 0xFC) == 0x74)
                cbOutputPreEmphasisAll.SelectedIndex = 29;
            else if ((data & 0xFC) == 0x78)
                cbOutputPreEmphasisAll.SelectedIndex = 30;
            else if ((data & 0xFC) == 0x7C)
                cbOutputPreEmphasisAll.SelectedIndex = 31;
            else if ((data & 0xFC) == 0x80)
                cbOutputPreEmphasisAll.SelectedIndex = 32;
            else if ((data & 0xFC) == 0x84)
                cbOutputPreEmphasisAll.SelectedIndex = 33;
            else if ((data & 0xFC) == 0x88)
                cbOutputPreEmphasisAll.SelectedIndex = 34;
            else if ((data & 0xFC) == 0x8C)
                cbOutputPreEmphasisAll.SelectedIndex = 35;
            else if ((data & 0xFC) == 0x90)
                cbOutputPreEmphasisAll.SelectedIndex = 36;
            else if ((data & 0xFC) == 0x94)
                cbOutputPreEmphasisAll.SelectedIndex = 37;
            else if ((data & 0xFC) == 0x98)
                cbOutputPreEmphasisAll.SelectedIndex = 38;
            else if ((data & 0xFC) == 0x9C)
                cbOutputPreEmphasisAll.SelectedIndex = 39;
            else if ((data & 0xFC) == 0xA0)
                cbOutputPreEmphasisAll.SelectedIndex = 40;
            else if ((data & 0xFC) == 0xA4)
                cbOutputPreEmphasisAll.SelectedIndex = 41;
            else if ((data & 0xFC) == 0xA8)
                cbOutputPreEmphasisAll.SelectedIndex = 42;
            else if ((data & 0xFC) == 0xAC)
                cbOutputPreEmphasisAll.SelectedIndex = 43;
            else if ((data & 0xFC) == 0xB0)
                cbOutputPreEmphasisAll.SelectedIndex = 44;
            else if ((data & 0xFC) == 0xB4)
                cbOutputPreEmphasisAll.SelectedIndex = 45;
            else if ((data & 0xFC) == 0xB8)
                cbOutputPreEmphasisAll.SelectedIndex = 46;
            else if ((data & 0xFC) == 0xBC)
                cbOutputPreEmphasisAll.SelectedIndex = 47;
            else if ((data & 0xFC) == 0xC0)
                cbOutputPreEmphasisAll.SelectedIndex = 48;
            else if ((data & 0xFC) == 0xC4)
                cbOutputPreEmphasisAll.SelectedIndex = 49;
            else if ((data & 0xFC) == 0xC8)
                cbOutputPreEmphasisAll.SelectedIndex = 50;
            else if ((data & 0xFC) == 0xCC)
                cbOutputPreEmphasisAll.SelectedIndex = 51;
            else if ((data & 0xFC) == 0xD0)
                cbOutputPreEmphasisAll.SelectedIndex = 52;
            else if ((data & 0xFC) == 0xD4)
                cbOutputPreEmphasisAll.SelectedIndex = 53;
            else if ((data & 0xFC) == 0xD8)
                cbOutputPreEmphasisAll.SelectedIndex = 54;
            else if ((data & 0xFC) == 0xDC)
                cbOutputPreEmphasisAll.SelectedIndex = 55;
            else if ((data & 0xFC) == 0xE0)
                cbOutputPreEmphasisAll.SelectedIndex = 56;
            else if ((data & 0xFC) == 0xE4)
                cbOutputPreEmphasisAll.SelectedIndex = 57;
            else if ((data & 0xFC) == 0xE8)
                cbOutputPreEmphasisAll.SelectedIndex = 58;
            else if ((data & 0xFC) == 0xEC)
                cbOutputPreEmphasisAll.SelectedIndex = 59;
            else if ((data & 0xFC) == 0xF0)
                cbOutputPreEmphasisAll.SelectedIndex = 60;
            else if ((data & 0xFC) == 0xF4)
                cbOutputPreEmphasisAll.SelectedIndex = 61;
            else if ((data & 0xFC) == 0xF8)
                cbOutputPreEmphasisAll.SelectedIndex = 62;
            else if ((data & 0xFC) == 0xFC)
                cbOutputPreEmphasisAll.SelectedIndex = 63;
        }
        private void _ParsePage1FAddr01(byte data)
        {
            if ((data & 0x07) == 0)
                cbVGARegulatorsRefAll.SelectedIndex = 0;
            else if ((data & 0x07) == 0x01)
                cbVGARegulatorsRefAll.SelectedIndex = 1;
            else if ((data & 0x07) == 0x02)
                cbVGARegulatorsRefAll.SelectedIndex = 2;
            else if ((data & 0x07) == 0x03)
                cbVGARegulatorsRefAll.SelectedIndex = 3;
            else if ((data & 0x07) == 0x04)
                cbVGARegulatorsRefAll.SelectedIndex = 4;
            else if ((data & 0x07) == 0x05)
                cbVGARegulatorsRefAll.SelectedIndex = 5;
            else if ((data & 0x07) == 0x06)
                cbVGARegulatorsRefAll.SelectedIndex = 6;
            else if ((data & 0x07) == 0x07)
                cbVGARegulatorsRefAll.SelectedIndex = 7;

            if ((data & 0x38) == 0)
                cbTIARegulatorsRefAll.SelectedIndex = 0;
            else if ((data & 0x38) == 0x08)
                cbTIARegulatorsRefAll.SelectedIndex = 1;
            else if ((data & 0x38) == 0x10)
                cbTIARegulatorsRefAll.SelectedIndex = 2;
            else if ((data & 0x38) == 0x18)
                cbTIARegulatorsRefAll.SelectedIndex = 3;
            else if ((data & 0x38) == 0x20)
                cbTIARegulatorsRefAll.SelectedIndex = 4;
            else if ((data & 0x38) == 0x28)
                cbTIARegulatorsRefAll.SelectedIndex = 5;
            else if ((data & 0x38) == 0x30)
                cbTIARegulatorsRefAll.SelectedIndex = 6;
            else if ((data & 0x38) == 0x38)
                cbTIARegulatorsRefAll.SelectedIndex = 7;

        }
        private void _ParsePage1FAddr02(byte data)
        {
            if ((data & 0x0F) == 0)
                cbTargetOutputSwingAll.SelectedIndex = 0;
            else if ((data & 0x0F) == 0x01)
                cbTargetOutputSwingAll.SelectedIndex = 1;
            else if ((data & 0x0F) == 0x02)
                cbTargetOutputSwingAll.SelectedIndex = 2;
            else if ((data & 0x0F) == 0x03)
                cbTargetOutputSwingAll.SelectedIndex = 3;
            else if ((data & 0x0F) == 0x04)
                cbTargetOutputSwingAll.SelectedIndex = 4;
            else if ((data & 0x0F) == 0x05)
                cbTargetOutputSwingAll.SelectedIndex = 5;
            else if ((data & 0x0F) == 0x06)
                cbTargetOutputSwingAll.SelectedIndex = 6;
            else if ((data & 0x0F) == 0x07)
                cbTargetOutputSwingAll.SelectedIndex = 7;
            else if ((data & 0x0F) == 0x08)
                cbTargetOutputSwingAll.SelectedIndex = 8;
            else if ((data & 0x0F) == 0x09)
                cbTargetOutputSwingAll.SelectedIndex = 9;
            else if ((data & 0x0F) == 0x0A)
                cbTargetOutputSwingAll.SelectedIndex = 10;
            else if ((data & 0x0F) == 0x0B)
                cbTargetOutputSwingAll.SelectedIndex = 11;
            else if ((data & 0x0F) == 0x0C)
                cbTargetOutputSwingAll.SelectedIndex = 12;
            else if ((data & 0x0F) == 0x0D)
                cbTargetOutputSwingAll.SelectedIndex = 13;
            else if ((data & 0x0F) == 0x0E)
                cbTargetOutputSwingAll.SelectedIndex = 14;
            else if ((data & 0x0F) == 0x0F)
                cbTargetOutputSwingAll.SelectedIndex = 15;
        }
        private void _ParsePage1FAddr03(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbManualVGA0FilterAll.SelectedIndex = index;
            }
            else
            {
                cbManualVGA0FilterAll.SelectedIndex = -1;
            }
        }
        private void _ParsePage1FAddr04(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbManualVGA1FilterAll.SelectedIndex = index;
            }
            else
            {
                cbManualVGA1FilterAll.SelectedIndex = -1;
            }
        }
        private void _ParsePage1FAddr05(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbManualVGA2FilterAll.SelectedIndex = index;
            }
            else
            {
                cbManualVGA2FilterAll.SelectedIndex = -1;
            }
        }
        private void _ParsePage1FAddr08(byte data)
        {
            if ((data & 0x30) == 0)
                cbVGARegSlopeCTRLAll.SelectedIndex = 0;
            else if ((data & 0x30) == 0x10)
                cbVGARegSlopeCTRLAll.SelectedIndex = 1;
            else if ((data & 0x30) == 0x20)
                cbVGARegSlopeCTRLAll.SelectedIndex = 2;
            else if ((data & 0x30) == 0x30)
                cbVGARegSlopeCTRLAll.SelectedIndex = 3;

            if ((data & 0xC0) == 0)
                cbTIARegSlopeCTRLAll.SelectedIndex = 0;
            else if ((data & 0xC0) == 0x40)
                cbTIARegSlopeCTRLAll.SelectedIndex = 1;
            else if ((data & 0xC0) == 0x80)
                cbTIARegSlopeCTRLAll.SelectedIndex = 2;
            else if ((data & 0xC0) == 0xC0)
                cbTIARegSlopeCTRLAll.SelectedIndex = 3;
        }
        private void _ParsePage1FAddr0A(byte data)
        {
            if ((data & 0x01) == 0)
                cbSelRefLowAll.SelectedIndex = 0;
            else
                cbSelRefLowAll.SelectedIndex = 1;

            int index1E = (data & 0x1E) >> 1;
            cbTuneRefAll.SelectedIndex = index1E;
        }
        private void _ParsePage1FAddr0B(byte data)
        {
            if ((data & 0xF0) == 0)
                cbBGlpolyCodeAll.SelectedIndex = 0;
            else if ((data & 0xF0) == 0x10)
                cbBGlpolyCodeAll.SelectedIndex = 1;
            else if ((data & 0xF0) == 0x20)
                cbBGlpolyCodeAll.SelectedIndex = 2;
            else if ((data & 0xF0) == 0x30)
                cbBGlpolyCodeAll.SelectedIndex = 3;
            else if ((data & 0xF0) == 0x40)
                cbBGlpolyCodeAll.SelectedIndex = 4;
            else if ((data & 0xF0) == 0x50)
                cbBGlpolyCodeAll.SelectedIndex = 5;
            else if ((data & 0xF0) == 0x60)
                cbBGlpolyCodeAll.SelectedIndex = 6;
            else if ((data & 0xF0) == 0x70)
                cbBGlpolyCodeAll.SelectedIndex = 7;
            else if ((data & 0xF0) == 0x80)
                cbBGlpolyCodeAll.SelectedIndex = 8;
            else if ((data & 0xF0) == 0x90)
                cbBGlpolyCodeAll.SelectedIndex = 9;
            else if ((data & 0xF0) == 0xA0)
                cbBGlpolyCodeAll.SelectedIndex = 10;
            else if ((data & 0xF0) == 0xB0)
                cbBGlpolyCodeAll.SelectedIndex = 11;
            else if ((data & 0xF0) == 0xC0)
                cbBGlpolyCodeAll.SelectedIndex = 12;
            else if ((data & 0xF0) == 0xD0)
                cbBGlpolyCodeAll.SelectedIndex = 13;
            else if ((data & 0xF0) == 0xE0)
                cbBGlpolyCodeAll.SelectedIndex = 14;
            else if ((data & 0xF0) == 0xF0)
                cbBGlpolyCodeAll.SelectedIndex = 15;
        }
        private void _ParsePage1FAddr0C(byte data)
        {
            if ((data & 0x80) == 0x80)
                cbDisableVREGTIAAutoSweepAll.SelectedIndex = 1;
            else
                cbDisableVREGTIAAutoSweepAll.SelectedIndex = 0;

            if ((data & 0x40) == 0x40)
                cbDisableVREGVGAAutoSweepAll.SelectedIndex = 1;
            else
                cbDisableVREGVGAAutoSweepAll.SelectedIndex = 0;
        }
        private void _ParsePage1FAddr0D(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbTIABWCtrlSwingAll.SelectedIndex = index;
            }
            else
            {
                cbTIABWCtrlSwingAll.SelectedIndex = -1;
            }
        }
        private void _ParsePage1FAddr19(byte data)
        {
            int index0C = (data & 0x0C) >> 2;
            cbRSSILOSHysteresisAll.SelectedIndex = index0C;

            if ((data & 0x80) == 0)
                cbSquelchOnLOSAll.SelectedIndex = 0;
            else
                cbSquelchOnLOSAll.SelectedIndex = 1;
        }
        private void _ParsePage1FAddr1A(byte data)
        {
            int index = data & 0x0F;
            if (index >= 0 && index <= 15)
            {
                cbLOSDeassertthresholdAll.SelectedIndex = index;
            }
            else
            {
                cbLOSDeassertthresholdAll.SelectedIndex = -1;
            }
        }
        private void _ParsePage1FAddrC0(byte data)
        {
            _page1FC0 = data;
            _UpdateAGCComboBox_Page1F();
        }
        private void _ParsePage1FAddrC1(byte data)
        {
            _page1FC1 = (byte)(data & 0x07);
            _UpdateAGCComboBox_Page1F();
        }
        private void _ParsePage1FAddrC2(byte data)
        {
            cbAGCAdaptationModeAll.SelectedIndex = ((data & 0x01) == 0) ? 0 : 1;
            cbAGCValueAll.Enabled = (cbAGCAdaptationModeAll.SelectedIndex == 0);
        }
        private void _ParsePage1FAddrD0(byte data)
        {
            _page1FD0 = data;
            _UpdateAGCComboBox_Page1F();
        }
        private void _ParsePage1FAddrD1(byte data)
        {
            _page1FD1 = (byte)(data & 0x07);
            _UpdateDCDComboBox_Page1F();
        }
        private void _ParsePage1FAddrD2(byte data)
        {
            cbDCDAdaptationModeAll.SelectedIndex = ((data & 0x01) == 0) ? 0 : 1;
            cbDCDValueAll.Enabled = (cbDCDAdaptationModeAll.SelectedIndex == 0);
        }
        private void _ParsePage1FAddrE0(byte data)
        {
            _page1FE0 = data;
            _UpdateBiasComboBox_Page1F();
        }
        private void _ParsePage1FAddrE1(byte data)
        {
            _page1FE1 = (byte)(data & 0x07);
            _UpdateBiasComboBox_Page1F();
        }
        private void _ParsePage1FAddrE2(byte data)
        {
            cbBiasAdaptationModeAll.SelectedIndex = ((data & 0x01) == 0) ? 0 : 1;
            cbBiasValueAll.Enabled = (cbBiasAdaptationModeAll.SelectedIndex == 0);
        }
        // ---- 實際更新 ComboBox 選項 ----
        private void _UpdateAGCComboBox_Page1F()
        {
            int agcValue = (_page1FC1 << 8) | _page1FC0;

            for (int i = 0; i < cbAGCValueAll.Items.Count; i++)
            {
                if (((ComboboxItem)cbAGCValueAll.Items[i]).Value.Equals(agcValue))
                {
                    cbAGCValueAll.SelectedIndex = i;
                    return;
                }
            }

            cbAGCValueL0.SelectedIndex = -1; // 無匹配項
        }
        private void _UpdateDCDComboBox_Page1F()
        {
            int DCDvalue = (_page1FD1 << 8) | _page1FD0;

            for (int i = 0; i < cbDCDValueAll.Items.Count; i++)
            {
                if (((ComboboxItem)cbDCDValueAll.Items[i]).Value.Equals(DCDvalue))
                {
                    cbDCDValueAll.SelectedIndex = i;
                    return;
                }
            }

            cbDCDValueAll.SelectedIndex = -1; // 未找到對應值
        }
        private void _UpdateBiasComboBox_Page1F()
        {
            int Biasvalue = (_page1FE1 << 8) | _page1FE0;

            for (int i = 0; i < cbBiasValueAll.Items.Count; i++)
            {
                if (((ComboboxItem)cbBiasValueAll.Items[i]).Value.Equals(Biasvalue))
                {
                    cbBiasValueAll.SelectedIndex = i;
                    return;
                }
            }

            cbBiasValueAll.SelectedIndex = -1; // 未找到對應值
        }
        #endregion

        #region 改寫_ParsePage20AddrXX
        private void _ParsePage20Addr00(byte data)
        {
            if ((data & 0x01) == 0)
                cbPowerDownChannelL0.Checked = false;
            else
                cbPowerDownChannelL0.Checked = true;

            if ((data & 0xFC) == 0)
                cbOutputPreEmphasisL0.SelectedIndex = 0;
            else if ((data & 0xFC) == 0x04)
                cbOutputPreEmphasisL0.SelectedIndex = 1;
            else if ((data & 0xFC) == 0x08)
                cbOutputPreEmphasisL0.SelectedIndex = 2;
            else if ((data & 0xFC) == 0x0C)
                cbOutputPreEmphasisL0.SelectedIndex = 3;
            else if ((data & 0xFC) == 0x10)
                cbOutputPreEmphasisL0.SelectedIndex = 4;
            else if ((data & 0xFC) == 0x14)
                cbOutputPreEmphasisL0.SelectedIndex = 5;
            else if ((data & 0xFC) == 0x18)
                cbOutputPreEmphasisL0.SelectedIndex = 6;
            else if ((data & 0xFC) == 0x1C)
                cbOutputPreEmphasisL0.SelectedIndex = 7;
            else if ((data & 0xFC) == 0x20)
                cbOutputPreEmphasisL0.SelectedIndex = 8;
            else if ((data & 0xFC) == 0x24)
                cbOutputPreEmphasisL0.SelectedIndex = 9;
            else if ((data & 0xFC) == 0x28)
                cbOutputPreEmphasisL0.SelectedIndex = 10;
            else if ((data & 0xFC) == 0x2C)
                cbOutputPreEmphasisL0.SelectedIndex = 11;
            else if ((data & 0xFC) == 0x30)
                cbOutputPreEmphasisL0.SelectedIndex = 12;
            else if ((data & 0xFC) == 0x34)
                cbOutputPreEmphasisL0.SelectedIndex = 13;
            else if ((data & 0xFC) == 0x38)
                cbOutputPreEmphasisL0.SelectedIndex = 14;
            else if ((data & 0xFC) == 0x3C)
                cbOutputPreEmphasisL0.SelectedIndex = 15;
            else if ((data & 0xFC) == 0x40)
                cbOutputPreEmphasisL0.SelectedIndex = 16;
            else if ((data & 0xFC) == 0x44)
                cbOutputPreEmphasisL0.SelectedIndex = 17;
            else if ((data & 0xFC) == 0x48)
                cbOutputPreEmphasisL0.SelectedIndex = 18;
            else if ((data & 0xFC) == 0x4C)
                cbOutputPreEmphasisL0.SelectedIndex = 19;
            else if ((data & 0xFC) == 0x50)
                cbOutputPreEmphasisL0.SelectedIndex = 20;
            else if ((data & 0xFC) == 0x54)
                cbOutputPreEmphasisL0.SelectedIndex = 21;
            else if ((data & 0xFC) == 0x58)
                cbOutputPreEmphasisL0.SelectedIndex = 22;
            else if ((data & 0xFC) == 0x5C)
                cbOutputPreEmphasisL0.SelectedIndex = 23;
            else if ((data & 0xFC) == 0x60)
                cbOutputPreEmphasisL0.SelectedIndex = 24;
            else if ((data & 0xFC) == 0x64)
                cbOutputPreEmphasisL0.SelectedIndex = 25;
            else if ((data & 0xFC) == 0x68)
                cbOutputPreEmphasisL0.SelectedIndex = 26;
            else if ((data & 0xFC) == 0x6C)
                cbOutputPreEmphasisL0.SelectedIndex = 27;
            else if ((data & 0xFC) == 0x70)
                cbOutputPreEmphasisL0.SelectedIndex = 28;
            else if ((data & 0xFC) == 0x74)
                cbOutputPreEmphasisL0.SelectedIndex = 29;
            else if ((data & 0xFC) == 0x78)
                cbOutputPreEmphasisL0.SelectedIndex = 30;
            else if ((data & 0xFC) == 0x7C)
                cbOutputPreEmphasisL0.SelectedIndex = 31;
            else if ((data & 0xFC) == 0x80)
                cbOutputPreEmphasisL0.SelectedIndex = 32;
            else if ((data & 0xFC) == 0x84)
                cbOutputPreEmphasisL0.SelectedIndex = 33;
            else if ((data & 0xFC) == 0x88)
                cbOutputPreEmphasisL0.SelectedIndex = 34;
            else if ((data & 0xFC) == 0x8C)
                cbOutputPreEmphasisL0.SelectedIndex = 35;
            else if ((data & 0xFC) == 0x90)
                cbOutputPreEmphasisL0.SelectedIndex = 36;
            else if ((data & 0xFC) == 0x94)
                cbOutputPreEmphasisL0.SelectedIndex = 37;
            else if ((data & 0xFC) == 0x98)
                cbOutputPreEmphasisL0.SelectedIndex = 38;
            else if ((data & 0xFC) == 0x9C)
                cbOutputPreEmphasisL0.SelectedIndex = 39;
            else if ((data & 0xFC) == 0xA0)
                cbOutputPreEmphasisL0.SelectedIndex = 40;
            else if ((data & 0xFC) == 0xA4)
                cbOutputPreEmphasisL0.SelectedIndex = 41;
            else if ((data & 0xFC) == 0xA8)
                cbOutputPreEmphasisL0.SelectedIndex = 42;
            else if ((data & 0xFC) == 0xAC)
                cbOutputPreEmphasisL0.SelectedIndex = 43;
            else if ((data & 0xFC) == 0xB0)
                cbOutputPreEmphasisL0.SelectedIndex = 44;
            else if ((data & 0xFC) == 0xB4)
                cbOutputPreEmphasisL0.SelectedIndex = 45;
            else if ((data & 0xFC) == 0xB8)
                cbOutputPreEmphasisL0.SelectedIndex = 46;
            else if ((data & 0xFC) == 0xBC)
                cbOutputPreEmphasisL0.SelectedIndex = 47;
            else if ((data & 0xFC) == 0xC0)
                cbOutputPreEmphasisL0.SelectedIndex = 48;
            else if ((data & 0xFC) == 0xC4)
                cbOutputPreEmphasisL0.SelectedIndex = 49;
            else if ((data & 0xFC) == 0xC8)
                cbOutputPreEmphasisL0.SelectedIndex = 50;
            else if ((data & 0xFC) == 0xCC)
                cbOutputPreEmphasisL0.SelectedIndex = 51;
            else if ((data & 0xFC) == 0xD0)
                cbOutputPreEmphasisL0.SelectedIndex = 52;
            else if ((data & 0xFC) == 0xD4)
                cbOutputPreEmphasisL0.SelectedIndex = 53;
            else if ((data & 0xFC) == 0xD8)
                cbOutputPreEmphasisL0.SelectedIndex = 54;
            else if ((data & 0xFC) == 0xDC)
                cbOutputPreEmphasisL0.SelectedIndex = 55;
            else if ((data & 0xFC) == 0xE0)
                cbOutputPreEmphasisL0.SelectedIndex = 56;
            else if ((data & 0xFC) == 0xE4)
                cbOutputPreEmphasisL0.SelectedIndex = 57;
            else if ((data & 0xFC) == 0xE8)
                cbOutputPreEmphasisL0.SelectedIndex = 58;
            else if ((data & 0xFC) == 0xEC)
                cbOutputPreEmphasisL0.SelectedIndex = 59;
            else if ((data & 0xFC) == 0xF0)
                cbOutputPreEmphasisL0.SelectedIndex = 60;
            else if ((data & 0xFC) == 0xF4)
                cbOutputPreEmphasisL0.SelectedIndex = 61;
            else if ((data & 0xFC) == 0xF8)
                cbOutputPreEmphasisL0.SelectedIndex = 62;
            else if ((data & 0xFC) == 0xFC)
                cbOutputPreEmphasisL0.SelectedIndex = 63;
        }
        private void _ParsePage20Addr01(byte data)
        {
            if ((data & 0x07) == 0)
                cbVGARegulatorsRefL0.SelectedIndex = 0;
            else if ((data & 0x07) == 0x01)
                cbVGARegulatorsRefL0.SelectedIndex = 1;
            else if ((data & 0x07) == 0x02)
                cbVGARegulatorsRefL0.SelectedIndex = 2;
            else if ((data & 0x07) == 0x03)
                cbVGARegulatorsRefL0.SelectedIndex = 3;
            else if ((data & 0x07) == 0x04)
                cbVGARegulatorsRefL0.SelectedIndex = 4;
            else if ((data & 0x07) == 0x05)
                cbVGARegulatorsRefL0.SelectedIndex = 5;
            else if ((data & 0x07) == 0x06)
                cbVGARegulatorsRefL0.SelectedIndex = 6;
            else if ((data & 0x07) == 0x07)
                cbVGARegulatorsRefL0.SelectedIndex = 7;

            if ((data & 0x38) == 0)
                cbTIARegulatorsRefL0.SelectedIndex = 0;
            else if ((data & 0x38) == 0x08)
                cbTIARegulatorsRefL0.SelectedIndex = 1;
            else if ((data & 0x38) == 0x10)
                cbTIARegulatorsRefL0.SelectedIndex = 2;
            else if ((data & 0x38) == 0x18)
                cbTIARegulatorsRefL0.SelectedIndex = 3;
            else if ((data & 0x38) == 0x20)
                cbTIARegulatorsRefL0.SelectedIndex = 4;
            else if ((data & 0x38) == 0x28)
                cbTIARegulatorsRefL0.SelectedIndex = 5;
            else if ((data & 0x38) == 0x30)
                cbTIARegulatorsRefL0.SelectedIndex = 6;
            else if ((data & 0x38) == 0x38)
                cbTIARegulatorsRefL0.SelectedIndex = 7;
        }
        private void _ParsePage20Addr02(byte data)
        {
            if ((data & 0x0F) == 0)
                cbTargetOutputSwingL0.SelectedIndex = 0;
            else if ((data & 0x0F) == 0x01)
                cbTargetOutputSwingL0.SelectedIndex = 1;
            else if ((data & 0x0F) == 0x02)
                cbTargetOutputSwingL0.SelectedIndex = 2;
            else if ((data & 0x0F) == 0x03)
                cbTargetOutputSwingL0.SelectedIndex = 3;
            else if ((data & 0x0F) == 0x04)
                cbTargetOutputSwingL0.SelectedIndex = 4;
            else if ((data & 0x0F) == 0x05)
                cbTargetOutputSwingL0.SelectedIndex = 5;
            else if ((data & 0x0F) == 0x06)
                cbTargetOutputSwingL0.SelectedIndex = 6;
            else if ((data & 0x0F) == 0x07)
                cbTargetOutputSwingL0.SelectedIndex = 7;
            else if ((data & 0x0F) == 0x08)
                cbTargetOutputSwingL0.SelectedIndex = 8;
            else if ((data & 0x0F) == 0x09)
                cbTargetOutputSwingL0.SelectedIndex = 9;
            else if ((data & 0x0F) == 0x0A)
                cbTargetOutputSwingL0.SelectedIndex = 10;
            else if ((data & 0x0F) == 0x0B)
                cbTargetOutputSwingL0.SelectedIndex = 11;
            else if ((data & 0x0F) == 0x0C)
                cbTargetOutputSwingL0.SelectedIndex = 12;
            else if ((data & 0x0F) == 0x0D)
                cbTargetOutputSwingL0.SelectedIndex = 13;
            else if ((data & 0x0F) == 0x0E)
                cbTargetOutputSwingL0.SelectedIndex = 14;
            else if ((data & 0x0F) == 0x0F)
                cbTargetOutputSwingL0.SelectedIndex = 15;
        }
        private void _ParsePage20Addr03(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbManualVGA0FilterL0.SelectedIndex = index;
            }
            else
            {
                cbManualVGA0FilterL0.SelectedIndex = -1;
            }
        }
        private void _ParsePage20Addr04(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbManualVGA1FilterL0.SelectedIndex = index;
            }
            else
            {
                cbManualVGA1FilterL0.SelectedIndex = -1;
            }
        }
        private void _ParsePage20Addr05(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbManualVGA2FilterL0.SelectedIndex = index;
            }
            else
            {
                cbManualVGA2FilterL0.SelectedIndex = -1;
            }
        }
        private void _ParsePage20Addr08(byte data)
        {
            if ((data & 0x30) == 0)
                cbVGARegSlopeCTRLL0.SelectedIndex = 0;
            else if ((data & 0x30) == 0x10)
                cbVGARegSlopeCTRLL0.SelectedIndex = 1;
            else if ((data & 0x30) == 0x20)
                cbVGARegSlopeCTRLL0.SelectedIndex = 2;
            else if ((data & 0x30) == 0x30)
                cbVGARegSlopeCTRLL0.SelectedIndex = 3;

            if ((data & 0xC0) == 0)
                cbTIARegSlopeCTRLL0.SelectedIndex = 0;
            else if ((data & 0xC0) == 0x40)
                cbTIARegSlopeCTRLL0.SelectedIndex = 1;
            else if ((data & 0xC0) == 0x80)
                cbTIARegSlopeCTRLL0.SelectedIndex = 2;
            else if ((data & 0xC0) == 0xC0)
                cbTIARegSlopeCTRLL0.SelectedIndex = 3;
        }
        private void _ParsePage20Addr0A(byte data)
        {
            if ((data & 0x01) == 0)
                cbSelRefLowL0.SelectedIndex = 0;
            else
                cbSelRefLowL0.SelectedIndex = 1;

            int index1E = (data & 0x1E) >> 1;
            cbTuneRefL0.SelectedIndex = index1E;
        }
        private void _ParsePage20Addr0B(byte data)
        {
            if ((data & 0xF0) == 0)
                cbBGlpolyCodeL0.SelectedIndex = 0;
            else if ((data & 0xF0) == 0x10)
                cbBGlpolyCodeL0.SelectedIndex = 1;
            else if ((data & 0xF0) == 0x20)
                cbBGlpolyCodeL0.SelectedIndex = 2;
            else if ((data & 0xF0) == 0x30)
                cbBGlpolyCodeL0.SelectedIndex = 3;
            else if ((data & 0xF0) == 0x40)
                cbBGlpolyCodeL0.SelectedIndex = 4;
            else if ((data & 0xF0) == 0x50)
                cbBGlpolyCodeL0.SelectedIndex = 5;
            else if ((data & 0xF0) == 0x60)
                cbBGlpolyCodeL0.SelectedIndex = 6;
            else if ((data & 0xF0) == 0x70)
                cbBGlpolyCodeL0.SelectedIndex = 7;
            else if ((data & 0xF0) == 0x80)
                cbBGlpolyCodeL0.SelectedIndex = 8;
            else if ((data & 0xF0) == 0x90)
                cbBGlpolyCodeL0.SelectedIndex = 9;
            else if ((data & 0xF0) == 0xA0)
                cbBGlpolyCodeL0.SelectedIndex = 10;
            else if ((data & 0xF0) == 0xB0)
                cbBGlpolyCodeL0.SelectedIndex = 11;
            else if ((data & 0xF0) == 0xC0)
                cbBGlpolyCodeL0.SelectedIndex = 12;
            else if ((data & 0xF0) == 0xD0)
                cbBGlpolyCodeL0.SelectedIndex = 13;
            else if ((data & 0xF0) == 0xE0)
                cbBGlpolyCodeL0.SelectedIndex = 14;
            else if ((data & 0xF0) == 0xF0)
                cbBGlpolyCodeL0.SelectedIndex = 15;
        }
        private void _ParsePage20Addr0C(byte data)
        {
            if ((data & 0x80) == 0x80)
                cbDisableVREGTIAAutoSweepL0.SelectedIndex = 1;
            else
                cbDisableVREGTIAAutoSweepL0.SelectedIndex = 0;

            if ((data & 0x40) == 0x40)
                cbDisableVREGVGAAutoSweepL0.SelectedIndex = 1;
            else
                cbDisableVREGVGAAutoSweepL0.SelectedIndex = 0;
        }
        private void _ParsePage20Addr0D(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbTIABWCtrlSwingL0.SelectedIndex = index;
            }
            else
            {
                cbTIABWCtrlSwingL0.SelectedIndex = -1;
            }
        }
        private void _ParsePage20Addr19(byte data)
        {
            int index0C = (data & 0x0C) >> 2;
            cbRSSILOSHysteresisL0.SelectedIndex = index0C;

            if ((data & 0x80) == 0)
                cbSquelchOnLOSL0.SelectedIndex = 0;
            else
                cbSquelchOnLOSL0.SelectedIndex = 1;
        }
        private void _ParsePage20Addr1A(byte data)
        {
            int index = data & 0x0F;
            if (index >= 0 && index <= 15)
            {
                cbLOSDeassertthresholdL0.SelectedIndex = index;
            }
            else
            {
                cbLOSDeassertthresholdL0.SelectedIndex = -1;
            }
        }
        private void _ParsePage20AddrC0(byte data)
        {
            _page20C0 = data;
            _UpdateAGCComboBox_Page20();
        }
        private void _ParsePage20AddrC1(byte data)
        {
            _page20C1 = (byte)(data & 0x07);
            _UpdateAGCComboBox_Page20();
        }
        private void _ParsePage20AddrC2(byte data)
        {
            cbAGCAdaptationModeL0.SelectedIndex = ((data & 0x01) == 0) ? 0 : 1;
            cbAGCValueL0.Enabled = (cbAGCAdaptationModeL0.SelectedIndex == 0);
        }
        private void _ParsePage20AddrD0(byte data)
        {
            _page20D0 = data;
            _UpdateDCDComboBox_Page20();
        }
        private void _ParsePage20AddrD1(byte data)
        {
            _page20D1 = (byte)(data & 0x07);
            _UpdateDCDComboBox_Page20();
        }
        private void _ParsePage20AddrD2(byte data)
        {
            cbDCDAdaptationModeL0.SelectedIndex = ((data & 0x01) == 0) ? 0 : 1;
            cbDCDValueL0.Enabled = (cbDCDAdaptationModeL0.SelectedIndex == 0);
        }
        private void _ParsePage20AddrE0(byte data)
        {
            _page20E0 = data;
            _UpdateBiasComboBox_Page20();
        }
        private void _ParsePage20AddrE1(byte data)
        {
            _page20E1 = (byte)(data & 0x07);
            _UpdateBiasComboBox_Page20();
        }
        private void _ParsePage20AddrE2(byte data)
        {
            cbBiasAdaptationModeL0.SelectedIndex = ((data & 0x01) == 0) ? 0 : 1;
            cbBiasValueL0.Enabled = (cbBiasAdaptationModeL0.SelectedIndex == 0);
        }
        // ---- 實際更新 ComboBox 選項 ----
        private void _UpdateAGCComboBox_Page20()
        {
            int agcValue = (_page20C1 << 8) | _page20C0;

            for (int i = 0; i < cbAGCValueL0.Items.Count; i++)
            {
                if (((ComboboxItem)cbAGCValueL0.Items[i]).Value.Equals(agcValue))
                {
                    cbAGCValueL0.SelectedIndex = i;
                    return;
                }
            }

            cbAGCValueL0.SelectedIndex = -1;
        }
        private void _UpdateDCDComboBox_Page20()
        {
            int value = (_page20D1 << 8) | _page20D0;

            for (int i = 0; i < cbDCDValueL0.Items.Count; i++)
            {
                if (((ComboboxItem)cbDCDValueL0.Items[i]).Value.Equals(value))
                {
                    cbDCDValueL0.SelectedIndex = i;
                    return;
                }
            }

            cbDCDValueL0.SelectedIndex = -1;
        }
        private void _UpdateBiasComboBox_Page20()
        {
            int value = (_page20E1 << 8) | _page20E0;

            for (int i = 0; i < cbBiasValueL0.Items.Count; i++)
            {
                if (((ComboboxItem)cbBiasValueL0.Items[i]).Value.Equals(value))
                {
                    cbBiasValueL0.SelectedIndex = i;
                    return;
                }
            }

            cbBiasValueL0.SelectedIndex = -1;
        }
        #endregion

        #region 改寫_ParsePage21AddrXX
        private void _ParsePage21Addr00(byte data)
        {
            if ((data & 0x01) == 0)
                cbPowerDownChannelL1.Checked = false;
            else
                cbPowerDownChannelL1.Checked = true;

            if ((data & 0xFC) == 0x00)
                cbOutputPreEmphasisL1.SelectedIndex = 0;
            else if ((data & 0xFC) == 0x04)
                cbOutputPreEmphasisL1.SelectedIndex = 1;
            else if ((data & 0xFC) == 0x08)
                cbOutputPreEmphasisL1.SelectedIndex = 2;
            else if ((data & 0xFC) == 0x0C)
                cbOutputPreEmphasisL1.SelectedIndex = 3;
            else if ((data & 0xFC) == 0x10)
                cbOutputPreEmphasisL1.SelectedIndex = 4;
            else if ((data & 0xFC) == 0x14)
                cbOutputPreEmphasisL1.SelectedIndex = 5;
            else if ((data & 0xFC) == 0x18)
                cbOutputPreEmphasisL1.SelectedIndex = 6;
            else if ((data & 0xFC) == 0x1C)
                cbOutputPreEmphasisL1.SelectedIndex = 7;
            else if ((data & 0xFC) == 0x20)
                cbOutputPreEmphasisL1.SelectedIndex = 8;
            else if ((data & 0xFC) == 0x24)
                cbOutputPreEmphasisL1.SelectedIndex = 9;
            else if ((data & 0xFC) == 0x28)
                cbOutputPreEmphasisL1.SelectedIndex = 10;
            else if ((data & 0xFC) == 0x2C)
                cbOutputPreEmphasisL1.SelectedIndex = 11;
            else if ((data & 0xFC) == 0x30)
                cbOutputPreEmphasisL1.SelectedIndex = 12;
            else if ((data & 0xFC) == 0x34)
                cbOutputPreEmphasisL1.SelectedIndex = 13;
            else if ((data & 0xFC) == 0x38)
                cbOutputPreEmphasisL1.SelectedIndex = 14;
            else if ((data & 0xFC) == 0x3C)
                cbOutputPreEmphasisL1.SelectedIndex = 15;
            else if ((data & 0xFC) == 0x40)
                cbOutputPreEmphasisL1.SelectedIndex = 16;
            else if ((data & 0xFC) == 0x44)
                cbOutputPreEmphasisL1.SelectedIndex = 17;
            else if ((data & 0xFC) == 0x48)
                cbOutputPreEmphasisL1.SelectedIndex = 18;
            else if ((data & 0xFC) == 0x4C)
                cbOutputPreEmphasisL1.SelectedIndex = 19;
            else if ((data & 0xFC) == 0x50)
                cbOutputPreEmphasisL1.SelectedIndex = 20;
            else if ((data & 0xFC) == 0x54)
                cbOutputPreEmphasisL1.SelectedIndex = 21;
            else if ((data & 0xFC) == 0x58)
                cbOutputPreEmphasisL1.SelectedIndex = 22;
            else if ((data & 0xFC) == 0x5C)
                cbOutputPreEmphasisL1.SelectedIndex = 23;
            else if ((data & 0xFC) == 0x60)
                cbOutputPreEmphasisL1.SelectedIndex = 24;
            else if ((data & 0xFC) == 0x64)
                cbOutputPreEmphasisL1.SelectedIndex = 25;
            else if ((data & 0xFC) == 0x68)
                cbOutputPreEmphasisL1.SelectedIndex = 26;
            else if ((data & 0xFC) == 0x6C)
                cbOutputPreEmphasisL1.SelectedIndex = 27;
            else if ((data & 0xFC) == 0x70)
                cbOutputPreEmphasisL1.SelectedIndex = 28;
            else if ((data & 0xFC) == 0x74)
                cbOutputPreEmphasisL1.SelectedIndex = 29;
            else if ((data & 0xFC) == 0x78)
                cbOutputPreEmphasisL1.SelectedIndex = 30;
            else if ((data & 0xFC) == 0x7C)
                cbOutputPreEmphasisL1.SelectedIndex = 31;
            else if ((data & 0xFC) == 0x80)
                cbOutputPreEmphasisL1.SelectedIndex = 32;
            else if ((data & 0xFC) == 0x84)
                cbOutputPreEmphasisL1.SelectedIndex = 33;
            else if ((data & 0xFC) == 0x88)
                cbOutputPreEmphasisL1.SelectedIndex = 34;
            else if ((data & 0xFC) == 0x8C)
                cbOutputPreEmphasisL1.SelectedIndex = 35;
            else if ((data & 0xFC) == 0x90)
                cbOutputPreEmphasisL1.SelectedIndex = 36;
            else if ((data & 0xFC) == 0x94)
                cbOutputPreEmphasisL1.SelectedIndex = 37;
            else if ((data & 0xFC) == 0x98)
                cbOutputPreEmphasisL1.SelectedIndex = 38;
            else if ((data & 0xFC) == 0x9C)
                cbOutputPreEmphasisL1.SelectedIndex = 39;
            else if ((data & 0xFC) == 0xA0)
                cbOutputPreEmphasisL1.SelectedIndex = 40;
            else if ((data & 0xFC) == 0xA4)
                cbOutputPreEmphasisL1.SelectedIndex = 41;
            else if ((data & 0xFC) == 0xA8)
                cbOutputPreEmphasisL1.SelectedIndex = 42;
            else if ((data & 0xFC) == 0xAC)
                cbOutputPreEmphasisL1.SelectedIndex = 43;
            else if ((data & 0xFC) == 0xB0)
                cbOutputPreEmphasisL1.SelectedIndex = 44;
            else if ((data & 0xFC) == 0xB4)
                cbOutputPreEmphasisL1.SelectedIndex = 45;
            else if ((data & 0xFC) == 0xB8)
                cbOutputPreEmphasisL1.SelectedIndex = 46;
            else if ((data & 0xFC) == 0xBC)
                cbOutputPreEmphasisL1.SelectedIndex = 47;
            else if ((data & 0xFC) == 0xC0)
                cbOutputPreEmphasisL1.SelectedIndex = 48;
            else if ((data & 0xFC) == 0xC4)
                cbOutputPreEmphasisL1.SelectedIndex = 49;
            else if ((data & 0xFC) == 0xC8)
                cbOutputPreEmphasisL1.SelectedIndex = 50;
            else if ((data & 0xFC) == 0xCC)
                cbOutputPreEmphasisL1.SelectedIndex = 51;
            else if ((data & 0xFC) == 0xD0)
                cbOutputPreEmphasisL1.SelectedIndex = 52;
            else if ((data & 0xFC) == 0xD4)
                cbOutputPreEmphasisL1.SelectedIndex = 53;
            else if ((data & 0xFC) == 0xD8)
                cbOutputPreEmphasisL1.SelectedIndex = 54;
            else if ((data & 0xFC) == 0xDC)
                cbOutputPreEmphasisL1.SelectedIndex = 55;
            else if ((data & 0xFC) == 0xE0)
                cbOutputPreEmphasisL1.SelectedIndex = 56;
            else if ((data & 0xFC) == 0xE4)
                cbOutputPreEmphasisL1.SelectedIndex = 57;
            else if ((data & 0xFC) == 0xE8)
                cbOutputPreEmphasisL1.SelectedIndex = 58;
            else if ((data & 0xFC) == 0xEC)
                cbOutputPreEmphasisL1.SelectedIndex = 59;
            else if ((data & 0xFC) == 0xF0)
                cbOutputPreEmphasisL1.SelectedIndex = 60;
            else if ((data & 0xFC) == 0xF4)
                cbOutputPreEmphasisL1.SelectedIndex = 61;
            else if ((data & 0xFC) == 0xF8)
                cbOutputPreEmphasisL1.SelectedIndex = 62;
            else if ((data & 0xFC) == 0xFC)
                cbOutputPreEmphasisL1.SelectedIndex = 63;
        }
        private void _ParsePage21Addr01(byte data)
        {
            if ((data & 0x07) == 0)
                cbVGARegulatorsRefL1.SelectedIndex = 0;
            else if ((data & 0x07) == 0x01)
                cbVGARegulatorsRefL1.SelectedIndex = 1;
            else if ((data & 0x07) == 0x02)
                cbVGARegulatorsRefL1.SelectedIndex = 2;
            else if ((data & 0x07) == 0x03)
                cbVGARegulatorsRefL1.SelectedIndex = 3;
            else if ((data & 0x07) == 0x04)
                cbVGARegulatorsRefL1.SelectedIndex = 4;
            else if ((data & 0x07) == 0x05)
                cbVGARegulatorsRefL1.SelectedIndex = 5;
            else if ((data & 0x07) == 0x06)
                cbVGARegulatorsRefL1.SelectedIndex = 6;
            else if ((data & 0x07) == 0x07)
                cbVGARegulatorsRefL1.SelectedIndex = 7;

            if ((data & 0x38) == 0)
                cbTIARegulatorsRefL1.SelectedIndex = 0;
            else if ((data & 0x38) == 0x08)
                cbTIARegulatorsRefL1.SelectedIndex = 1;
            else if ((data & 0x38) == 0x10)
                cbTIARegulatorsRefL1.SelectedIndex = 2;
            else if ((data & 0x38) == 0x18)
                cbTIARegulatorsRefL1.SelectedIndex = 3;
            else if ((data & 0x38) == 0x20)
                cbTIARegulatorsRefL1.SelectedIndex = 4;
            else if ((data & 0x38) == 0x28)
                cbTIARegulatorsRefL1.SelectedIndex = 5;
            else if ((data & 0x38) == 0x30)
                cbTIARegulatorsRefL1.SelectedIndex = 6;
            else if ((data & 0x38) == 0x38)
                cbTIARegulatorsRefL1.SelectedIndex = 7;
        }
        private void _ParsePage21Addr02(byte data)
        {
            if ((data & 0x0F) == 0)
                cbTargetOutputSwingL1.SelectedIndex = 0;
            else if ((data & 0x0F) == 0x01)
                cbTargetOutputSwingL1.SelectedIndex = 1;
            else if ((data & 0x0F) == 0x02)
                cbTargetOutputSwingL1.SelectedIndex = 2;
            else if ((data & 0x0F) == 0x03)
                cbTargetOutputSwingL1.SelectedIndex = 3;
            else if ((data & 0x0F) == 0x04)
                cbTargetOutputSwingL1.SelectedIndex = 4;
            else if ((data & 0x0F) == 0x05)
                cbTargetOutputSwingL1.SelectedIndex = 5;
            else if ((data & 0x0F) == 0x06)
                cbTargetOutputSwingL1.SelectedIndex = 6;
            else if ((data & 0x0F) == 0x07)
                cbTargetOutputSwingL1.SelectedIndex = 7;
            else if ((data & 0x0F) == 0x08)
                cbTargetOutputSwingL1.SelectedIndex = 8;
            else if ((data & 0x0F) == 0x09)
                cbTargetOutputSwingL1.SelectedIndex = 9;
            else if ((data & 0x0F) == 0x0A)
                cbTargetOutputSwingL1.SelectedIndex = 10;
            else if ((data & 0x0F) == 0x0B)
                cbTargetOutputSwingL1.SelectedIndex = 11;
            else if ((data & 0x0F) == 0x0C)
                cbTargetOutputSwingL1.SelectedIndex = 12;
            else if ((data & 0x0F) == 0x0D)
                cbTargetOutputSwingL1.SelectedIndex = 13;
            else if ((data & 0x0F) == 0x0E)
                cbTargetOutputSwingL1.SelectedIndex = 14;
            else if ((data & 0x0F) == 0x0F)
                cbTargetOutputSwingL1.SelectedIndex = 15;
        }
        private void _ParsePage21Addr03(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbManualVGA0FilterL1.SelectedIndex = index;
            }
            else
            {
                cbManualVGA0FilterL1.SelectedIndex = -1;
            }
        }
        private void _ParsePage21Addr04(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbManualVGA1FilterL1.SelectedIndex = index;
            }
            else
            {
                cbManualVGA1FilterL1.SelectedIndex = -1;
            }
        }
        private void _ParsePage21Addr05(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbManualVGA2FilterL1.SelectedIndex = index;
            }
            else
            {
                cbManualVGA2FilterL1.SelectedIndex = -1;
            }
        }
        private void _ParsePage21Addr08(byte data)
        {
            if ((data & 0x30) == 0)
                cbVGARegSlopeCTRLL1.SelectedIndex = 0;
            else if ((data & 0x30) == 0x10)
                cbVGARegSlopeCTRLL1.SelectedIndex = 1;
            else if ((data & 0x30) == 0x20)
                cbVGARegSlopeCTRLL1.SelectedIndex = 2;
            else if ((data & 0x30) == 0x30)
                cbVGARegSlopeCTRLL1.SelectedIndex = 3;

            if ((data & 0xC0) == 0)
                cbTIARegSlopeCTRLL1.SelectedIndex = 0;
            else if ((data & 0xC0) == 0x40)
                cbTIARegSlopeCTRLL1.SelectedIndex = 1;
            else if ((data & 0xC0) == 0x80)
                cbTIARegSlopeCTRLL1.SelectedIndex = 2;
            else if ((data & 0xC0) == 0xC0)
                cbTIARegSlopeCTRLL1.SelectedIndex = 3;
        }
        private void _ParsePage21Addr0A(byte data)
        {
            if ((data & 0x01) == 0)
                cbSelRefLowL1.SelectedIndex = 0;
            else
                cbSelRefLowL1.SelectedIndex = 1;

            int index1E = (data & 0x1E) >> 1;
            cbTuneRefL1.SelectedIndex = index1E;
        }
        private void _ParsePage21Addr0B(byte data)
        {
            if ((data & 0xF0) == 0)
                cbBGlpolyCodeL1.SelectedIndex = 0;
            else if ((data & 0xF0) == 0x10)
                cbBGlpolyCodeL1.SelectedIndex = 1;
            else if ((data & 0xF0) == 0x20)
                cbBGlpolyCodeL1.SelectedIndex = 2;
            else if ((data & 0xF0) == 0x30)
                cbBGlpolyCodeL1.SelectedIndex = 3;
            else if ((data & 0xF0) == 0x40)
                cbBGlpolyCodeL1.SelectedIndex = 4;
            else if ((data & 0xF0) == 0x50)
                cbBGlpolyCodeL1.SelectedIndex = 5;
            else if ((data & 0xF0) == 0x60)
                cbBGlpolyCodeL1.SelectedIndex = 6;
            else if ((data & 0xF0) == 0x70)
                cbBGlpolyCodeL1.SelectedIndex = 7;
            else if ((data & 0xF0) == 0x80)
                cbBGlpolyCodeL1.SelectedIndex = 8;
            else if ((data & 0xF0) == 0x90)
                cbBGlpolyCodeL1.SelectedIndex = 9;
            else if ((data & 0xF0) == 0xA0)
                cbBGlpolyCodeL1.SelectedIndex = 10;
            else if ((data & 0xF0) == 0xB0)
                cbBGlpolyCodeL1.SelectedIndex = 11;
            else if ((data & 0xF0) == 0xC0)
                cbBGlpolyCodeL1.SelectedIndex = 12;
            else if ((data & 0xF0) == 0xD0)
                cbBGlpolyCodeL1.SelectedIndex = 13;
            else if ((data & 0xF0) == 0xE0)
                cbBGlpolyCodeL1.SelectedIndex = 14;
            else if ((data & 0xF0) == 0xF0)
                cbBGlpolyCodeL1.SelectedIndex = 15;
        }
        private void _ParsePage21Addr0C(byte data)
        {
            if ((data & 0x80) == 0x80)
                cbDisableVREGTIAAutoSweepL1.SelectedIndex = 1;
            else
                cbDisableVREGTIAAutoSweepL1.SelectedIndex = 0;

            if ((data & 0x40) == 0x40)
                cbDisableVREGVGAAutoSweepL1.SelectedIndex = 1;
            else
                cbDisableVREGVGAAutoSweepL1.SelectedIndex = 0;
        }
        private void _ParsePage21Addr0D(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbTIABWCtrlSwingL1.SelectedIndex = index;
            }
            else
            {
                cbTIABWCtrlSwingL1.SelectedIndex = -1;
            }
        }
        private void _ParsePage21Addr19(byte data)
        {
            int index0C = (data & 0x0C) >> 2;
            cbRSSILOSHysteresisL1.SelectedIndex = index0C;

            if ((data & 0x80) == 0)
                cbSquelchOnLOSL1.SelectedIndex = 0;
            else
                cbSquelchOnLOSL1.SelectedIndex = 1;
        }
        private void _ParsePage21Addr1A(byte data)
        {
            int index = data & 0x0F;
            if (index >= 0 && index <= 15)
            {
                cbLOSDeassertthresholdL1.SelectedIndex = index;
            }
            else
            {
                cbLOSDeassertthresholdL1.SelectedIndex = -1;
            }
        }
        private void _ParsePage21AddrC0(byte data)
        {
            _page21C0 = data;
            _UpdateAGCComboBox_Page21();
        }
        private void _ParsePage21AddrC1(byte data)
        {
            _page21C1 = (byte)(data & 0x07);
            _UpdateAGCComboBox_Page21();
        }
        private void _ParsePage21AddrC2(byte data)
        {
            cbAGCAdaptationModeL1.SelectedIndex = ((data & 0x01) == 0) ? 0 : 1;
            cbAGCValueL1.Enabled = (cbAGCAdaptationModeL1.SelectedIndex == 0);
        }
        private void _ParsePage21AddrD0(byte data)
        {
            _page21D0 = data;
            _UpdateAGCComboBox_Page21();
        }
        private void _ParsePage21AddrD1(byte data)
        {
            _page21D1 = (byte)(data & 0x07);
            _UpdateDCDComboBox_Page21();
        }
        private void _ParsePage21AddrD2(byte data)
        {
            cbDCDAdaptationModeL1.SelectedIndex = ((data & 0x01) == 0) ? 0 : 1;
            cbDCDValueL1.Enabled = (cbDCDAdaptationModeL1.SelectedIndex == 0);
        }
        private void _ParsePage21AddrE0(byte data)
        {
            _page21E0 = data;
            _UpdateBiasComboBox_Page21();
        }
        private void _ParsePage21AddrE1(byte data)
        {
            _page21E1 = (byte)(data & 0x07);
            _UpdateBiasComboBox_Page21();
        }
        private void _ParsePage21AddrE2(byte data)
        {
            cbBiasAdaptationModeL1.SelectedIndex = ((data & 0x01) == 0) ? 0 : 1;
            cbBiasValueL1.Enabled = (cbBiasAdaptationModeL1.SelectedIndex == 0);
        }
        // ---- 實際更新 ComboBox 選項 ----
        private void _UpdateAGCComboBox_Page21()
        {
            int AGCValue = (_page21C1 << 8) | _page21C0;

            for (int i = 0; i < cbAGCValueL1.Items.Count; i++)
            {
                if (((ComboboxItem)cbAGCValueL1.Items[i]).Value.Equals(AGCValue))
                {
                    cbAGCValueL1.SelectedIndex = i;
                    return;
                }
            }

            cbAGCValueL1.SelectedIndex = -1; // 無匹配項
        }
        private void _UpdateDCDComboBox_Page21()
        {
            int value = (_page21D1 << 8) | _page21D0;

            for (int i = 0; i < cbDCDValueL1.Items.Count; i++)
            {
                if (((ComboboxItem)cbDCDValueL1.Items[i]).Value.Equals(value))
                {
                    cbDCDValueL1.SelectedIndex = i;
                    return;
                }
            }

            cbDCDValueL1.SelectedIndex = -1; // 未找到對應值
        }
        private void _UpdateBiasComboBox_Page21()
        {
            int value = (_page21E1 << 8) | _page21E0;

            for (int i = 0; i < cbBiasValueL1.Items.Count; i++)
            {
                if (((ComboboxItem)cbBiasValueL1.Items[i]).Value.Equals(value))
                {
                    cbBiasValueL1.SelectedIndex = i;
                    return;
                }
            }

            cbBiasValueL1.SelectedIndex = -1; // 未找到對應值
        }
        #endregion

        #region 改寫_ParsePage22AddrXX
        private void _ParsePage22Addr00(byte data)
        {
            if ((data & 0x01) == 0)
                cbPowerDownChannelL2.Checked = false;
            else
                cbPowerDownChannelL2.Checked = true;

            if ((data & 0xFC) == 0x00)
                cbOutputPreEmphasisL2.SelectedIndex = 0;
            else if ((data & 0xFC) == 0x04)
                cbOutputPreEmphasisL2.SelectedIndex = 1;
            else if ((data & 0xFC) == 0x08)
                cbOutputPreEmphasisL2.SelectedIndex = 2;
            else if ((data & 0xFC) == 0x0C)
                cbOutputPreEmphasisL2.SelectedIndex = 3;
            else if ((data & 0xFC) == 0x10)
                cbOutputPreEmphasisL2.SelectedIndex = 4;
            else if ((data & 0xFC) == 0x14)
                cbOutputPreEmphasisL2.SelectedIndex = 5;
            else if ((data & 0xFC) == 0x18)
                cbOutputPreEmphasisL2.SelectedIndex = 6;
            else if ((data & 0xFC) == 0x1C)
                cbOutputPreEmphasisL2.SelectedIndex = 7;
            else if ((data & 0xFC) == 0x20)
                cbOutputPreEmphasisL2.SelectedIndex = 8;
            else if ((data & 0xFC) == 0x24)
                cbOutputPreEmphasisL2.SelectedIndex = 9;
            else if ((data & 0xFC) == 0x28)
                cbOutputPreEmphasisL2.SelectedIndex = 10;
            else if ((data & 0xFC) == 0x2C)
                cbOutputPreEmphasisL2.SelectedIndex = 11;
            else if ((data & 0xFC) == 0x30)
                cbOutputPreEmphasisL2.SelectedIndex = 12;
            else if ((data & 0xFC) == 0x34)
                cbOutputPreEmphasisL2.SelectedIndex = 13;
            else if ((data & 0xFC) == 0x38)
                cbOutputPreEmphasisL2.SelectedIndex = 14;
            else if ((data & 0xFC) == 0x3C)
                cbOutputPreEmphasisL2.SelectedIndex = 15;
            else if ((data & 0xFC) == 0x40)
                cbOutputPreEmphasisL2.SelectedIndex = 16;
            else if ((data & 0xFC) == 0x44)
                cbOutputPreEmphasisL2.SelectedIndex = 17;
            else if ((data & 0xFC) == 0x48)
                cbOutputPreEmphasisL2.SelectedIndex = 18;
            else if ((data & 0xFC) == 0x4C)
                cbOutputPreEmphasisL2.SelectedIndex = 19;
            else if ((data & 0xFC) == 0x50)
                cbOutputPreEmphasisL2.SelectedIndex = 20;
            else if ((data & 0xFC) == 0x54)
                cbOutputPreEmphasisL2.SelectedIndex = 21;
            else if ((data & 0xFC) == 0x58)
                cbOutputPreEmphasisL2.SelectedIndex = 22;
            else if ((data & 0xFC) == 0x5C)
                cbOutputPreEmphasisL2.SelectedIndex = 23;
            else if ((data & 0xFC) == 0x60)
                cbOutputPreEmphasisL2.SelectedIndex = 24;
            else if ((data & 0xFC) == 0x64)
                cbOutputPreEmphasisL2.SelectedIndex = 25;
            else if ((data & 0xFC) == 0x68)
                cbOutputPreEmphasisL2.SelectedIndex = 26;
            else if ((data & 0xFC) == 0x6C)
                cbOutputPreEmphasisL2.SelectedIndex = 27;
            else if ((data & 0xFC) == 0x70)
                cbOutputPreEmphasisL2.SelectedIndex = 28;
            else if ((data & 0xFC) == 0x74)
                cbOutputPreEmphasisL2.SelectedIndex = 29;
            else if ((data & 0xFC) == 0x78)
                cbOutputPreEmphasisL2.SelectedIndex = 30;
            else if ((data & 0xFC) == 0x7C)
                cbOutputPreEmphasisL2.SelectedIndex = 31;
            else if ((data & 0xFC) == 0x80)
                cbOutputPreEmphasisL2.SelectedIndex = 32;
            else if ((data & 0xFC) == 0x84)
                cbOutputPreEmphasisL2.SelectedIndex = 33;
            else if ((data & 0xFC) == 0x88)
                cbOutputPreEmphasisL2.SelectedIndex = 34;
            else if ((data & 0xFC) == 0x8C)
                cbOutputPreEmphasisL2.SelectedIndex = 35;
            else if ((data & 0xFC) == 0x90)
                cbOutputPreEmphasisL2.SelectedIndex = 36;
            else if ((data & 0xFC) == 0x94)
                cbOutputPreEmphasisL2.SelectedIndex = 37;
            else if ((data & 0xFC) == 0x98)
                cbOutputPreEmphasisL2.SelectedIndex = 38;
            else if ((data & 0xFC) == 0x9C)
                cbOutputPreEmphasisL2.SelectedIndex = 39;
            else if ((data & 0xFC) == 0xA0)
                cbOutputPreEmphasisL2.SelectedIndex = 40;
            else if ((data & 0xFC) == 0xA4)
                cbOutputPreEmphasisL2.SelectedIndex = 41;
            else if ((data & 0xFC) == 0xA8)
                cbOutputPreEmphasisL2.SelectedIndex = 42;
            else if ((data & 0xFC) == 0xAC)
                cbOutputPreEmphasisL2.SelectedIndex = 43;
            else if ((data & 0xFC) == 0xB0)
                cbOutputPreEmphasisL2.SelectedIndex = 44;
            else if ((data & 0xFC) == 0xB4)
                cbOutputPreEmphasisL2.SelectedIndex = 45;
            else if ((data & 0xFC) == 0xB8)
                cbOutputPreEmphasisL2.SelectedIndex = 46;
            else if ((data & 0xFC) == 0xBC)
                cbOutputPreEmphasisL2.SelectedIndex = 47;
            else if ((data & 0xFC) == 0xC0)
                cbOutputPreEmphasisL2.SelectedIndex = 48;
            else if ((data & 0xFC) == 0xC4)
                cbOutputPreEmphasisL2.SelectedIndex = 49;
            else if ((data & 0xFC) == 0xC8)
                cbOutputPreEmphasisL2.SelectedIndex = 50;
            else if ((data & 0xFC) == 0xCC)
                cbOutputPreEmphasisL2.SelectedIndex = 51;
            else if ((data & 0xFC) == 0xD0)
                cbOutputPreEmphasisL2.SelectedIndex = 52;
            else if ((data & 0xFC) == 0xD4)
                cbOutputPreEmphasisL2.SelectedIndex = 53;
            else if ((data & 0xFC) == 0xD8)
                cbOutputPreEmphasisL2.SelectedIndex = 54;
            else if ((data & 0xFC) == 0xDC)
                cbOutputPreEmphasisL2.SelectedIndex = 55;
            else if ((data & 0xFC) == 0xE0)
                cbOutputPreEmphasisL2.SelectedIndex = 56;
            else if ((data & 0xFC) == 0xE4)
                cbOutputPreEmphasisL2.SelectedIndex = 57;
            else if ((data & 0xFC) == 0xE8)
                cbOutputPreEmphasisL2.SelectedIndex = 58;
            else if ((data & 0xFC) == 0xEC)
                cbOutputPreEmphasisL2.SelectedIndex = 59;
            else if ((data & 0xFC) == 0xF0)
                cbOutputPreEmphasisL2.SelectedIndex = 60;
            else if ((data & 0xFC) == 0xF4)
                cbOutputPreEmphasisL2.SelectedIndex = 61;
            else if ((data & 0xFC) == 0xF8)
                cbOutputPreEmphasisL2.SelectedIndex = 62;
            else if ((data & 0xFC) == 0xFC)
                cbOutputPreEmphasisL2.SelectedIndex = 63;
        }
        private void _ParsePage22Addr01(byte data)
        {
            if ((data & 0x07) == 0)
                cbVGARegulatorsRefL2.SelectedIndex = 0;
            else if ((data & 0x07) == 0x01)
                cbVGARegulatorsRefL2.SelectedIndex = 1;
            else if ((data & 0x07) == 0x02)
                cbVGARegulatorsRefL2.SelectedIndex = 2;
            else if ((data & 0x07) == 0x03)
                cbVGARegulatorsRefL2.SelectedIndex = 3;
            else if ((data & 0x07) == 0x04)
                cbVGARegulatorsRefL2.SelectedIndex = 4;
            else if ((data & 0x07) == 0x05)
                cbVGARegulatorsRefL2.SelectedIndex = 5;
            else if ((data & 0x07) == 0x06)
                cbVGARegulatorsRefL2.SelectedIndex = 6;
            else if ((data & 0x07) == 0x07)
                cbVGARegulatorsRefL2.SelectedIndex = 7;

            if ((data & 0x38) == 0)
                cbTIARegulatorsRefL2.SelectedIndex = 0;
            else if ((data & 0x38) == 0x08)
                cbTIARegulatorsRefL2.SelectedIndex = 1;
            else if ((data & 0x38) == 0x10)
                cbTIARegulatorsRefL2.SelectedIndex = 2;
            else if ((data & 0x38) == 0x18)
                cbTIARegulatorsRefL2.SelectedIndex = 3;
            else if ((data & 0x38) == 0x20)
                cbTIARegulatorsRefL2.SelectedIndex = 4;
            else if ((data & 0x38) == 0x28)
                cbTIARegulatorsRefL2.SelectedIndex = 5;
            else if ((data & 0x38) == 0x30)
                cbTIARegulatorsRefL2.SelectedIndex = 6;
            else if ((data & 0x38) == 0x38)
                cbTIARegulatorsRefL2.SelectedIndex = 7;
        }
        private void _ParsePage22Addr02(byte data)
        {
            if ((data & 0x0F) == 0)
                cbTargetOutputSwingL2.SelectedIndex = 0;
            else if ((data & 0x0F) == 0x01)
                cbTargetOutputSwingL2.SelectedIndex = 1;
            else if ((data & 0x0F) == 0x02)
                cbTargetOutputSwingL2.SelectedIndex = 2;
            else if ((data & 0x0F) == 0x03)
                cbTargetOutputSwingL2.SelectedIndex = 3;
            else if ((data & 0x0F) == 0x04)
                cbTargetOutputSwingL2.SelectedIndex = 4;
            else if ((data & 0x0F) == 0x05)
                cbTargetOutputSwingL2.SelectedIndex = 5;
            else if ((data & 0x0F) == 0x06)
                cbTargetOutputSwingL2.SelectedIndex = 6;
            else if ((data & 0x0F) == 0x07)
                cbTargetOutputSwingL2.SelectedIndex = 7;
            else if ((data & 0x0F) == 0x08)
                cbTargetOutputSwingL2.SelectedIndex = 8;
            else if ((data & 0x0F) == 0x09)
                cbTargetOutputSwingL2.SelectedIndex = 9;
            else if ((data & 0x0F) == 0x0A)
                cbTargetOutputSwingL2.SelectedIndex = 10;
            else if ((data & 0x0F) == 0x0B)
                cbTargetOutputSwingL2.SelectedIndex = 11;
            else if ((data & 0x0F) == 0x0C)
                cbTargetOutputSwingL2.SelectedIndex = 12;
            else if ((data & 0x0F) == 0x0D)
                cbTargetOutputSwingL2.SelectedIndex = 13;
            else if ((data & 0x0F) == 0x0E)
                cbTargetOutputSwingL2.SelectedIndex = 14;
            else if ((data & 0x0F) == 0x0F)
                cbTargetOutputSwingL2.SelectedIndex = 15;
        }
        private void _ParsePage22Addr03(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbManualVGA0FilterL2.SelectedIndex = index;
            }
            else
            {
                cbManualVGA0FilterL2.SelectedIndex = -1;
            }
        }
        private void _ParsePage22Addr04(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbManualVGA1FilterL2.SelectedIndex = index;
            }
            else
            {
                cbManualVGA1FilterL2.SelectedIndex = -1;
            }
        }
        private void _ParsePage22Addr05(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbManualVGA2FilterL2.SelectedIndex = index;
            }
            else
            {
                cbManualVGA2FilterL2.SelectedIndex = -1;
            }
        }
        private void _ParsePage22Addr08(byte data)
        {
            if ((data & 0x30) == 0)
                cbVGARegSlopeCTRLL2.SelectedIndex = 0;
            else if ((data & 0x30) == 0x10)
                cbVGARegSlopeCTRLL2.SelectedIndex = 1;
            else if ((data & 0x30) == 0x20)
                cbVGARegSlopeCTRLL2.SelectedIndex = 2;
            else if ((data & 0x30) == 0x30)
                cbVGARegSlopeCTRLL2.SelectedIndex = 3;

            if ((data & 0xC0) == 0)
                cbTIARegSlopeCTRLL2.SelectedIndex = 0;
            else if ((data & 0xC0) == 0x40)
                cbTIARegSlopeCTRLL2.SelectedIndex = 1;
            else if ((data & 0xC0) == 0x80)
                cbTIARegSlopeCTRLL2.SelectedIndex = 2;
            else if ((data & 0xC0) == 0xC0)
                cbTIARegSlopeCTRLL2.SelectedIndex = 3;
        }
        private void _ParsePage22Addr0A(byte data)
        {
            if ((data & 0x01) == 0)
                cbSelRefLowL2.SelectedIndex = 0;
            else
                cbSelRefLowL2.SelectedIndex = 1;

            int index1E = (data & 0x1E) >> 1;
            cbTuneRefL2.SelectedIndex = index1E;
        }
        private void _ParsePage22Addr0B(byte data)
        {
            if ((data & 0xF0) == 0)
                cbBGlpolyCodeL2.SelectedIndex = 0;
            else if ((data & 0xF0) == 0x10)
                cbBGlpolyCodeL2.SelectedIndex = 1;
            else if ((data & 0xF0) == 0x20)
                cbBGlpolyCodeL2.SelectedIndex = 2;
            else if ((data & 0xF0) == 0x30)
                cbBGlpolyCodeL2.SelectedIndex = 3;
            else if ((data & 0xF0) == 0x40)
                cbBGlpolyCodeL2.SelectedIndex = 4;
            else if ((data & 0xF0) == 0x50)
                cbBGlpolyCodeL2.SelectedIndex = 5;
            else if ((data & 0xF0) == 0x60)
                cbBGlpolyCodeL2.SelectedIndex = 6;
            else if ((data & 0xF0) == 0x70)
                cbBGlpolyCodeL2.SelectedIndex = 7;
            else if ((data & 0xF0) == 0x80)
                cbBGlpolyCodeL2.SelectedIndex = 8;
            else if ((data & 0xF0) == 0x90)
                cbBGlpolyCodeL2.SelectedIndex = 9;
            else if ((data & 0xF0) == 0xA0)
                cbBGlpolyCodeL2.SelectedIndex = 10;
            else if ((data & 0xF0) == 0xB0)
                cbBGlpolyCodeL2.SelectedIndex = 11;
            else if ((data & 0xF0) == 0xC0)
                cbBGlpolyCodeL2.SelectedIndex = 12;
            else if ((data & 0xF0) == 0xD0)
                cbBGlpolyCodeL2.SelectedIndex = 13;
            else if ((data & 0xF0) == 0xE0)
                cbBGlpolyCodeL2.SelectedIndex = 14;
            else if ((data & 0xF0) == 0xF0)
                cbBGlpolyCodeL2.SelectedIndex = 15;
        }
        private void _ParsePage22Addr0C(byte data)
        {
            if ((data & 0x80) == 0x80)
                cbDisableVREGTIAAutoSweepL2.SelectedIndex = 1;
            else
                cbDisableVREGTIAAutoSweepL2.SelectedIndex = 0;

            if ((data & 0x40) == 0x40)
                cbDisableVREGVGAAutoSweepL2.SelectedIndex = 1;
            else
                cbDisableVREGVGAAutoSweepL2.SelectedIndex = 0;
        }
        private void _ParsePage22Addr0D(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbTIABWCtrlSwingL2.SelectedIndex = index;
            }
            else
            {
                cbTIABWCtrlSwingL2.SelectedIndex = -1;
            }
        }
        private void _ParsePage22Addr19(byte data)
        {
            int index0C = (data & 0x0C) >> 2;
            cbRSSILOSHysteresisL2.SelectedIndex = index0C;

            if ((data & 0x80) == 0)
                cbSquelchOnLOSL2.SelectedIndex = 0;
            else
                cbSquelchOnLOSL2.SelectedIndex = 1;
        }
        private void _ParsePage22Addr1A(byte data)
        {
            int index = data & 0x0F;
            if (index >= 0 && index <= 15)
            {
                cbLOSDeassertthresholdL2.SelectedIndex = index;
            }
            else
            {
                cbLOSDeassertthresholdL2.SelectedIndex = -1;
            }
        }
        private void _ParsePage22AddrC0(byte data)
        {
            _page22C0 = data;
            _UpdateAGCComboBox_Page22();
        }
        private void _ParsePage22AddrC1(byte data)
        {
            _page22C1 = (byte)(data & 0x07);
            _UpdateAGCComboBox_Page22();
        }
        private void _ParsePage22AddrC2(byte data)
        {
            cbAGCAdaptationModeL2.SelectedIndex = ((data & 0x01) == 0) ? 0 : 1;
            cbAGCValueL2.Enabled = (cbAGCAdaptationModeL2.SelectedIndex == 0);
        }
        private void _ParsePage22AddrD0(byte data)
        {
            _page22D0 = data;
            _UpdateAGCComboBox_Page22();
        }
        private void _ParsePage22AddrD1(byte data)
        {
            _page22D1 = (byte)(data & 0x07);
            _UpdateDCDComboBox_Page22();
        }
        private void _ParsePage22AddrD2(byte data)
        {
            cbDCDAdaptationModeL2.SelectedIndex = ((data & 0x01) == 0) ? 0 : 1;
            cbDCDValueL2.Enabled = (cbDCDAdaptationModeL2.SelectedIndex == 0);
        }
        private void _ParsePage22AddrE0(byte data)
        {
            _page22E0 = data;
            _UpdateBiasComboBox_Page22();
        }
        private void _ParsePage22AddrE1(byte data)
        {
            _page22E1 = (byte)(data & 0x07);
            _UpdateBiasComboBox_Page22();
        }
        private void _ParsePage22AddrE2(byte data)
        {
            cbBiasAdaptationModeL2.SelectedIndex = ((data & 0x01) == 0) ? 0 : 1;
            cbBiasValueL2.Enabled = (cbBiasAdaptationModeL2.SelectedIndex == 0);
        }
        // ---- 實際更新 ComboBox 選項 ----
        private void _UpdateAGCComboBox_Page22()
        {
            int agcValue = (_page22C1 << 8) | _page22C0;

            for (int i = 0; i < cbAGCValueL2.Items.Count; i++)
            {
                if (((ComboboxItem)cbAGCValueL2.Items[i]).Value.Equals(agcValue))
                {
                    cbAGCValueL2.SelectedIndex = i;
                    return;
                }
            }

            cbAGCValueL2.SelectedIndex = -1;
        }
        private void _UpdateDCDComboBox_Page22()
        {
            int value = (_page22D1 << 8) | _page22D0;

            for (int i = 0; i < cbDCDValueL2.Items.Count; i++)
            {
                if (((ComboboxItem)cbDCDValueL2.Items[i]).Value.Equals(value))
                {
                    cbDCDValueL2.SelectedIndex = i;
                    return;
                }
            }

            cbDCDValueL2.SelectedIndex = -1;
        }
        private void _UpdateBiasComboBox_Page22()
        {
            int value = (_page22E1 << 8) | _page22E0;

            for (int i = 0; i < cbBiasValueL2.Items.Count; i++)
            {
                if (((ComboboxItem)cbBiasValueL2.Items[i]).Value.Equals(value))
                {
                    cbBiasValueL2.SelectedIndex = i;
                    return;
                }
            }

            cbBiasValueL2.SelectedIndex = -1;
        }
        #endregion

        #region 改寫_ParsePage23AddrXX
        private void _ParsePage23Addr00(byte data)
        {
            if ((data & 0x01) == 0)
                cbPowerDownChannelL3.Checked = false;
            else
                cbPowerDownChannelL3.Checked = true;

            if ((data & 0xFC) == 0x00)
                cbOutputPreEmphasisL3.SelectedIndex = 0;
            else if ((data & 0xFC) == 0x04)
                cbOutputPreEmphasisL3.SelectedIndex = 1;
            else if ((data & 0xFC) == 0x08)
                cbOutputPreEmphasisL3.SelectedIndex = 2;
            else if ((data & 0xFC) == 0x0C)
                cbOutputPreEmphasisL3.SelectedIndex = 3;
            else if ((data & 0xFC) == 0x10)
                cbOutputPreEmphasisL3.SelectedIndex = 4;
            else if ((data & 0xFC) == 0x14)
                cbOutputPreEmphasisL3.SelectedIndex = 5;
            else if ((data & 0xFC) == 0x18)
                cbOutputPreEmphasisL3.SelectedIndex = 6;
            else if ((data & 0xFC) == 0x1C)
                cbOutputPreEmphasisL3.SelectedIndex = 7;
            else if ((data & 0xFC) == 0x20)
                cbOutputPreEmphasisL3.SelectedIndex = 8;
            else if ((data & 0xFC) == 0x24)
                cbOutputPreEmphasisL3.SelectedIndex = 9;
            else if ((data & 0xFC) == 0x28)
                cbOutputPreEmphasisL3.SelectedIndex = 10;
            else if ((data & 0xFC) == 0x2C)
                cbOutputPreEmphasisL3.SelectedIndex = 11;
            else if ((data & 0xFC) == 0x30)
                cbOutputPreEmphasisL3.SelectedIndex = 12;
            else if ((data & 0xFC) == 0x34)
                cbOutputPreEmphasisL3.SelectedIndex = 13;
            else if ((data & 0xFC) == 0x38)
                cbOutputPreEmphasisL3.SelectedIndex = 14;
            else if ((data & 0xFC) == 0x3C)
                cbOutputPreEmphasisL3.SelectedIndex = 15;
            else if ((data & 0xFC) == 0x40)
                cbOutputPreEmphasisL3.SelectedIndex = 16;
            else if ((data & 0xFC) == 0x44)
                cbOutputPreEmphasisL3.SelectedIndex = 17;
            else if ((data & 0xFC) == 0x48)
                cbOutputPreEmphasisL3.SelectedIndex = 18;
            else if ((data & 0xFC) == 0x4C)
                cbOutputPreEmphasisL3.SelectedIndex = 19;
            else if ((data & 0xFC) == 0x50)
                cbOutputPreEmphasisL3.SelectedIndex = 20;
            else if ((data & 0xFC) == 0x54)
                cbOutputPreEmphasisL3.SelectedIndex = 21;
            else if ((data & 0xFC) == 0x58)
                cbOutputPreEmphasisL3.SelectedIndex = 22;
            else if ((data & 0xFC) == 0x5C)
                cbOutputPreEmphasisL3.SelectedIndex = 23;
            else if ((data & 0xFC) == 0x60)
                cbOutputPreEmphasisL3.SelectedIndex = 24;
            else if ((data & 0xFC) == 0x64)
                cbOutputPreEmphasisL3.SelectedIndex = 25;
            else if ((data & 0xFC) == 0x68)
                cbOutputPreEmphasisL3.SelectedIndex = 26;
            else if ((data & 0xFC) == 0x6C)
                cbOutputPreEmphasisL3.SelectedIndex = 27;
            else if ((data & 0xFC) == 0x70)
                cbOutputPreEmphasisL3.SelectedIndex = 28;
            else if ((data & 0xFC) == 0x74)
                cbOutputPreEmphasisL3.SelectedIndex = 29;
            else if ((data & 0xFC) == 0x78)
                cbOutputPreEmphasisL3.SelectedIndex = 30;
            else if ((data & 0xFC) == 0x7C)
                cbOutputPreEmphasisL3.SelectedIndex = 31;
            else if ((data & 0xFC) == 0x80)
                cbOutputPreEmphasisL3.SelectedIndex = 32;
            else if ((data & 0xFC) == 0x84)
                cbOutputPreEmphasisL3.SelectedIndex = 33;
            else if ((data & 0xFC) == 0x88)
                cbOutputPreEmphasisL3.SelectedIndex = 34;
            else if ((data & 0xFC) == 0x8C)
                cbOutputPreEmphasisL3.SelectedIndex = 35;
            else if ((data & 0xFC) == 0x90)
                cbOutputPreEmphasisL3.SelectedIndex = 36;
            else if ((data & 0xFC) == 0x94)
                cbOutputPreEmphasisL3.SelectedIndex = 37;
            else if ((data & 0xFC) == 0x98)
                cbOutputPreEmphasisL3.SelectedIndex = 38;
            else if ((data & 0xFC) == 0x9C)
                cbOutputPreEmphasisL3.SelectedIndex = 39;
            else if ((data & 0xFC) == 0xA0)
                cbOutputPreEmphasisL3.SelectedIndex = 40;
            else if ((data & 0xFC) == 0xA4)
                cbOutputPreEmphasisL3.SelectedIndex = 41;
            else if ((data & 0xFC) == 0xA8)
                cbOutputPreEmphasisL3.SelectedIndex = 42;
            else if ((data & 0xFC) == 0xAC)
                cbOutputPreEmphasisL3.SelectedIndex = 43;
            else if ((data & 0xFC) == 0xB0)
                cbOutputPreEmphasisL3.SelectedIndex = 44;
            else if ((data & 0xFC) == 0xB4)
                cbOutputPreEmphasisL3.SelectedIndex = 45;
            else if ((data & 0xFC) == 0xB8)
                cbOutputPreEmphasisL3.SelectedIndex = 46;
            else if ((data & 0xFC) == 0xBC)
                cbOutputPreEmphasisL3.SelectedIndex = 47;
            else if ((data & 0xFC) == 0xC0)
                cbOutputPreEmphasisL3.SelectedIndex = 48;
            else if ((data & 0xFC) == 0xC4)
                cbOutputPreEmphasisL3.SelectedIndex = 49;
            else if ((data & 0xFC) == 0xC8)
                cbOutputPreEmphasisL3.SelectedIndex = 50;
            else if ((data & 0xFC) == 0xCC)
                cbOutputPreEmphasisL3.SelectedIndex = 51;
            else if ((data & 0xFC) == 0xD0)
                cbOutputPreEmphasisL3.SelectedIndex = 52;
            else if ((data & 0xFC) == 0xD4)
                cbOutputPreEmphasisL3.SelectedIndex = 53;
            else if ((data & 0xFC) == 0xD8)
                cbOutputPreEmphasisL3.SelectedIndex = 54;
            else if ((data & 0xFC) == 0xDC)
                cbOutputPreEmphasisL3.SelectedIndex = 55;
            else if ((data & 0xFC) == 0xE0)
                cbOutputPreEmphasisL3.SelectedIndex = 56;
            else if ((data & 0xFC) == 0xE4)
                cbOutputPreEmphasisL3.SelectedIndex = 57;
            else if ((data & 0xFC) == 0xE8)
                cbOutputPreEmphasisL3.SelectedIndex = 58;
            else if ((data & 0xFC) == 0xEC)
                cbOutputPreEmphasisL3.SelectedIndex = 59;
            else if ((data & 0xFC) == 0xF0)
                cbOutputPreEmphasisL3.SelectedIndex = 60;
            else if ((data & 0xFC) == 0xF4)
                cbOutputPreEmphasisL3.SelectedIndex = 61;
            else if ((data & 0xFC) == 0xF8)
                cbOutputPreEmphasisL3.SelectedIndex = 62;
            else if ((data & 0xFC) == 0xFC)
                cbOutputPreEmphasisL3.SelectedIndex = 63;
        }
        private void _ParsePage23Addr01(byte data)
        {
            if ((data & 0x07) == 0)
                cbVGARegulatorsRefL3.SelectedIndex = 0;
            else if ((data & 0x07) == 0x01)
                cbVGARegulatorsRefL3.SelectedIndex = 1;
            else if ((data & 0x07) == 0x02)
                cbVGARegulatorsRefL3.SelectedIndex = 2;
            else if ((data & 0x07) == 0x03)
                cbVGARegulatorsRefL3.SelectedIndex = 3;
            else if ((data & 0x07) == 0x04)
                cbVGARegulatorsRefL3.SelectedIndex = 4;
            else if ((data & 0x07) == 0x05)
                cbVGARegulatorsRefL3.SelectedIndex = 5;
            else if ((data & 0x07) == 0x06)
                cbVGARegulatorsRefL3.SelectedIndex = 6;
            else if ((data & 0x07) == 0x07)
                cbVGARegulatorsRefL3.SelectedIndex = 7;

            if ((data & 0x38) == 0)
                cbTIARegulatorsRefL3.SelectedIndex = 0;
            else if ((data & 0x38) == 0x08)
                cbTIARegulatorsRefL3.SelectedIndex = 1;
            else if ((data & 0x38) == 0x10)
                cbTIARegulatorsRefL3.SelectedIndex = 2;
            else if ((data & 0x38) == 0x18)
                cbTIARegulatorsRefL3.SelectedIndex = 3;
            else if ((data & 0x38) == 0x20)
                cbTIARegulatorsRefL3.SelectedIndex = 4;
            else if ((data & 0x38) == 0x28)
                cbTIARegulatorsRefL3.SelectedIndex = 5;
            else if ((data & 0x38) == 0x30)
                cbTIARegulatorsRefL3.SelectedIndex = 6;
            else if ((data & 0x38) == 0x38)
                cbTIARegulatorsRefL3.SelectedIndex = 7;
        }
        private void _ParsePage23Addr02(byte data)
        {
            if ((data & 0x0F) == 0)
                cbTargetOutputSwingL3.SelectedIndex = 0;
            else if ((data & 0x0F) == 0x01)
                cbTargetOutputSwingL3.SelectedIndex = 1;
            else if ((data & 0x0F) == 0x02)
                cbTargetOutputSwingL3.SelectedIndex = 2;
            else if ((data & 0x0F) == 0x03)
                cbTargetOutputSwingL3.SelectedIndex = 3;
            else if ((data & 0x0F) == 0x04)
                cbTargetOutputSwingL3.SelectedIndex = 4;
            else if ((data & 0x0F) == 0x05)
                cbTargetOutputSwingL3.SelectedIndex = 5;
            else if ((data & 0x0F) == 0x06)
                cbTargetOutputSwingL3.SelectedIndex = 6;
            else if ((data & 0x0F) == 0x07)
                cbTargetOutputSwingL3.SelectedIndex = 7;
            else if ((data & 0x0F) == 0x08)
                cbTargetOutputSwingL3.SelectedIndex = 8;
            else if ((data & 0x0F) == 0x09)
                cbTargetOutputSwingL3.SelectedIndex = 9;
            else if ((data & 0x0F) == 0x0A)
                cbTargetOutputSwingL3.SelectedIndex = 10;
            else if ((data & 0x0F) == 0x0B)
                cbTargetOutputSwingL3.SelectedIndex = 11;
            else if ((data & 0x0F) == 0x0C)
                cbTargetOutputSwingL3.SelectedIndex = 12;
            else if ((data & 0x0F) == 0x0D)
                cbTargetOutputSwingL3.SelectedIndex = 13;
            else if ((data & 0x0F) == 0x0E)
                cbTargetOutputSwingL3.SelectedIndex = 14;
            else if ((data & 0x0F) == 0x0F)
                cbTargetOutputSwingL3.SelectedIndex = 15;
        }
        private void _ParsePage23Addr03(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbManualVGA0FilterL3.SelectedIndex = index;
            }
            else
            {
                cbManualVGA0FilterL3.SelectedIndex = -1;
            }
        }
        private void _ParsePage23Addr04(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbManualVGA1FilterL3.SelectedIndex = index;
            }
            else
            {
                cbManualVGA1FilterL3.SelectedIndex = -1;
            }
        }
        private void _ParsePage23Addr05(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbManualVGA2FilterL3.SelectedIndex = index;
            }
            else
            {
                cbManualVGA2FilterL3.SelectedIndex = -1;
            }
        }
        private void _ParsePage23Addr08(byte data)
        {
            if ((data & 0x30) == 0)
                cbVGARegSlopeCTRLL3.SelectedIndex = 0;
            else if ((data & 0x30) == 0x10)
                cbVGARegSlopeCTRLL3.SelectedIndex = 1;
            else if ((data & 0x30) == 0x20)
                cbVGARegSlopeCTRLL3.SelectedIndex = 2;
            else if ((data & 0x30) == 0x30)
                cbVGARegSlopeCTRLL3.SelectedIndex = 3;

            if ((data & 0xC0) == 0)
                cbTIARegSlopeCTRLL3.SelectedIndex = 0;
            else if ((data & 0xC0) == 0x40)
                cbTIARegSlopeCTRLL3.SelectedIndex = 1;
            else if ((data & 0xC0) == 0x80)
                cbTIARegSlopeCTRLL3.SelectedIndex = 2;
            else if ((data & 0xC0) == 0xC0)
                cbTIARegSlopeCTRLL3.SelectedIndex = 3;
        }
        private void _ParsePage23Addr0A(byte data)
        {
            if ((data & 0x01) == 0)
                cbSelRefLowL3.SelectedIndex = 0;
            else
                cbSelRefLowL3.SelectedIndex = 1;

            int index1E = (data & 0x1E) >> 1;
            cbTuneRefL3.SelectedIndex = index1E;
        }
        private void _ParsePage23Addr0B(byte data)
        {
            if ((data & 0xF0) == 0)
                cbBGlpolyCodeL3.SelectedIndex = 0;
            else if ((data & 0xF0) == 0x10)
                cbBGlpolyCodeL3.SelectedIndex = 1;
            else if ((data & 0xF0) == 0x20)
                cbBGlpolyCodeL3.SelectedIndex = 2;
            else if ((data & 0xF0) == 0x30)
                cbBGlpolyCodeL3.SelectedIndex = 3;
            else if ((data & 0xF0) == 0x40)
                cbBGlpolyCodeL3.SelectedIndex = 4;
            else if ((data & 0xF0) == 0x50)
                cbBGlpolyCodeL3.SelectedIndex = 5;
            else if ((data & 0xF0) == 0x60)
                cbBGlpolyCodeL3.SelectedIndex = 6;
            else if ((data & 0xF0) == 0x70)
                cbBGlpolyCodeL3.SelectedIndex = 7;
            else if ((data & 0xF0) == 0x80)
                cbBGlpolyCodeL3.SelectedIndex = 8;
            else if ((data & 0xF0) == 0x90)
                cbBGlpolyCodeL3.SelectedIndex = 9;
            else if ((data & 0xF0) == 0xA0)
                cbBGlpolyCodeAll.SelectedIndex = 10;
            else if ((data & 0xF0) == 0xB0)
                cbBGlpolyCodeL3.SelectedIndex = 11;
            else if ((data & 0xF0) == 0xC0)
                cbBGlpolyCodeL3.SelectedIndex = 12;
            else if ((data & 0xF0) == 0xD0)
                cbBGlpolyCodeL3.SelectedIndex = 13;
            else if ((data & 0xF0) == 0xE0)
                cbBGlpolyCodeL3.SelectedIndex = 14;
            else if ((data & 0xF0) == 0xF0)
                cbBGlpolyCodeL3.SelectedIndex = 15;
        }
        private void _ParsePage23Addr0C(byte data)
        {
            if ((data & 0x80) == 0x80)
                cbDisableVREGTIAAutoSweepL3.SelectedIndex = 1;
            else
                cbDisableVREGTIAAutoSweepL3.SelectedIndex = 0;

            if ((data & 0x40) == 0x40)
                cbDisableVREGVGAAutoSweepL3.SelectedIndex = 1;
            else
                cbDisableVREGVGAAutoSweepL3.SelectedIndex = 0;
        }
        private void _ParsePage23Addr0D(byte data)
        {
            int index = data & 0x3F;
            if (index >= 0 && index <= 63)
            {
                cbTIABWCtrlSwingL3.SelectedIndex = index;
            }
            else
            {
                cbTIABWCtrlSwingL3.SelectedIndex = -1;
            }
        }
        private void _ParsePage23Addr19(byte data)
        {
            int index0C = (data & 0x0C) >> 2;
            cbRSSILOSHysteresisL3.SelectedIndex = index0C;

            if ((data & 0x80) == 0)
                cbSquelchOnLOSL3.SelectedIndex = 0;
            else
                cbSquelchOnLOSL3.SelectedIndex = 1;
        }
        private void _ParsePage23Addr1A(byte data)
        {
            int index = data & 0x0F;
            if (index >= 0 && index <= 15)
            {
                cbLOSDeassertthresholdL3.SelectedIndex = index;
            }
            else
            {
                cbLOSDeassertthresholdL3.SelectedIndex = -1;
            }
        }
        private void _ParsePage23AddrC0(byte data)
        {
            _page23C0 = data;
            _UpdateAGCComboBox_Page23();
        }
        private void _ParsePage23AddrC1(byte data)
        {
            _page23C1 = (byte)(data & 0x07);
            _UpdateAGCComboBox_Page23();
        }
        private void _ParsePage23AddrC2(byte data)
        {
            cbAGCAdaptationModeL3.SelectedIndex = ((data & 0x01) == 0) ? 0 : 1;
            cbAGCValueL3.Enabled = (cbAGCAdaptationModeL3.SelectedIndex == 0);
        }
        private void _ParsePage23AddrD0(byte data)
        {
            _page23D0 = data;
            _UpdateAGCComboBox_Page23();
        }
        private void _ParsePage23AddrD1(byte data)
        {
            _page23D1 = (byte)(data & 0x07);
            _UpdateDCDComboBox_Page23();
        }
        private void _ParsePage23AddrD2(byte data)
        {
            cbDCDAdaptationModeL3.SelectedIndex = ((data & 0x01) == 0) ? 0 : 1;
            cbDCDValueL3.Enabled = (cbDCDAdaptationModeL3.SelectedIndex == 0);
        }
        private void _ParsePage23AddrE0(byte data)
        {
            _page23E0 = data;
            _UpdateBiasComboBox_Page23();
        }
        private void _ParsePage23AddrE1(byte data)
        {
            _page23E1 = (byte)(data & 0x07);
            _UpdateBiasComboBox_Page23();
        }
        private void _ParsePage23AddrE2(byte data)
        {
            cbBiasAdaptationModeL3.SelectedIndex = ((data & 0x01) == 0) ? 0 : 1;
            cbBiasValueL3.Enabled = (cbBiasAdaptationModeL3.SelectedIndex == 0);
        }
        // ---- 實際更新 ComboBox 選項 ----
        private void _UpdateAGCComboBox_Page23()
        {
            int agcValue = (_page23C1 << 8) | _page23C0;

            for (int i = 0; i < cbAGCValueL3.Items.Count; i++)
            {
                if (((ComboboxItem)cbAGCValueL3.Items[i]).Value.Equals(agcValue))
                {
                    cbAGCValueL3.SelectedIndex = i;
                    return;
                }
            }

            cbAGCValueL3.SelectedIndex = -1; // 無匹配項
        }
        private void _UpdateDCDComboBox_Page23()
        {
            int value = (_page23D1 << 8) | _page23D0;

            for (int i = 0; i < cbDCDValueL3.Items.Count; i++)
            {
                if (((ComboboxItem)cbDCDValueL3.Items[i]).Value.Equals(value))
                {
                    cbDCDValueL3.SelectedIndex = i;
                    return;
                }
            }

            cbDCDValueL3.SelectedIndex = -1; // 未找到對應值
        }
        private void _UpdateBiasComboBox_Page23()
        {
            int value = (_page23E1 << 8) | _page23E0;

            for (int i = 0; i < cbBiasValueL3.Items.Count; i++)
            {
                if (((ComboboxItem)cbBiasValueL3.Items[i]).Value.Equals(value))
                {
                    cbBiasValueL3.SelectedIndex = i;
                    return;
                }
            }

            cbBiasValueL3.SelectedIndex = -1; // 未找到對應值
        }
        #endregion

        #region 暫存read C0 C1 資料
        private byte _page1FC0 = 0;
        private byte _page1FC1 = 0;
        private byte _page20C0 = 0;
        private byte _page20C1 = 0;
        private byte _page21C0 = 0;
        private byte _page21C1 = 0;
        private byte _page22C0 = 0;
        private byte _page22C1 = 0;
        private byte _page23C0 = 0;
        private byte _page23C1 = 0;
        #endregion

        #region 暫存read D0 D1 資料
        private byte _page1FD0 = 0;
        private byte _page1FD1 = 0;
        private byte _page20D0 = 0;
        private byte _page20D1 = 0;
        private byte _page21D0 = 0;
        private byte _page21D1 = 0;
        private byte _page22D0 = 0;
        private byte _page22D1 = 0;
        private byte _page23D0 = 0;
        private byte _page23D1 = 0;
        #endregion

        #region 暫存read E0 E1 資料
        private byte _page1FE0 = 0;
        private byte _page1FE1 = 0;
        private byte _page20E0 = 0;
        private byte _page20E1 = 0;
        private byte _page21E0 = 0;
        private byte _page21E1 = 0;
        private byte _page22E0 = 0;
        private byte _page22E1 = 0;
        private byte _page23E0 = 0;
        private byte _page23E1 = 0;
        #endregion

        #region 主畫面按鈕事件

        private void bReadAll_Click_1(object sender, EventArgs e)
        {
            byte[] data = new byte[10];

            int rv;

            if (reading == true)
                return;

            reading = true;
            bReadAll.Enabled = false;

            if (i2cReadCB == null)
                goto exit;

            #region Reading Page00

            SetPage(Page00);
            Thread.Sleep(50);

            rv = i2cReadCB(devAddr, 0x00, 2, data);
            if (rv != 2)
                goto exit;
            _ParsePage00Addr00(data[0]);
            _ParsePage00Addr01(data[1]);

            rv = i2cReadCB(devAddr, 0x03, 2, data);
            if (rv != 2)
                goto exit;
            _ParsePage00Addr03(data[0]);
            _ParsePage00Addr04(data[1]);

            rv = i2cReadCB(devAddr, 0x08, 1, data);
            if (rv != 1)
                goto exit;
            _ParsePage00Addr08(data[0]);

            rv = i2cReadCB(devAddr, 0x10, 1, data);
            if (rv != 1)
                goto exit;
            _ParsePage00Addr10(data[0]);

            rv = i2cReadCB(devAddr, 0x0A, 1, data);
            if (rv != 1)
                goto exit;
            _ParsePage00Addr0A(data[0]);

            #endregion

            #region Reading Page1F

            SetPage(Page1F);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x00, 6, data);
            if (rv != 6)
                goto exit;
            _ParsePage1FAddr00(data[0]);
            _ParsePage1FAddr01(data[1]);
            _ParsePage1FAddr02(data[2]);
            _ParsePage1FAddr03(data[3]);
            _ParsePage1FAddr04(data[4]);
            _ParsePage1FAddr05(data[5]);

            rv = i2cReadCB(devAddr, 0x08, 1, data);
            if (rv != 1)

                goto exit;
            _ParsePage1FAddr08(data[0]);

            rv = i2cReadCB(devAddr, 0x0A, 4, data);
            if (rv != 4)
                goto exit;
            _ParsePage1FAddr0A(data[0]);
            _ParsePage1FAddr0B(data[1]);
            _ParsePage1FAddr0C(data[2]);
            _ParsePage1FAddr0D(data[3]);

            rv = i2cReadCB(devAddr, 0x19, 2, data);
            if (rv != 2)
                goto exit;
            _ParsePage1FAddr19(data[0]);
            _ParsePage1FAddr1A(data[1]);

            rv = i2cReadCB(devAddr, 0xC0, 3, data);
            if (rv != 3)
                goto exit;
            _ParsePage1FAddrC0(data[0]);
            _ParsePage1FAddrC1(data[1]);
            _ParsePage1FAddrC2(data[2]);

            rv = i2cReadCB(devAddr, 0xD0, 3, data);
            if (rv != 3)
                goto exit;
            _ParsePage1FAddrD0(data[0]);
            _ParsePage1FAddrD1(data[1]);
            _ParsePage1FAddrD2(data[2]);

            rv = i2cReadCB(devAddr, 0xE0, 3, data);
            if (rv != 3)
                goto exit;
            _ParsePage1FAddrE0(data[0]);
            _ParsePage1FAddrE1(data[1]);
            _ParsePage1FAddrE2(data[2]);

            #endregion

            #region Reading Page20

            SetPage(Page20);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x00, 6, data);
            if (rv != 6)
                goto exit;
            _ParsePage20Addr00(data[0]);
            _ParsePage20Addr01(data[1]);
            _ParsePage20Addr02(data[2]);
            _ParsePage20Addr03(data[3]);
            _ParsePage20Addr04(data[4]);
            _ParsePage20Addr05(data[5]);


            rv = i2cReadCB(devAddr, 0x08, 1, data);
            if (rv != 1)

                goto exit;
            _ParsePage20Addr08(data[0]);

            rv = i2cReadCB(devAddr, 0x0A, 4, data);
            if (rv != 4)
                goto exit;
            _ParsePage20Addr0A(data[0]);
            _ParsePage20Addr0B(data[1]);
            _ParsePage20Addr0C(data[2]);
            _ParsePage20Addr0D(data[3]);

            rv = i2cReadCB(devAddr, 0x19, 2, data);
            if (rv != 2)
                goto exit;
            _ParsePage20Addr19(data[0]);
            _ParsePage20Addr1A(data[1]);

            rv = i2cReadCB(devAddr, 0xC0, 3, data);
            if (rv != 3)
                goto exit;
            _ParsePage20AddrC0(data[0]);
            _ParsePage20AddrC1(data[1]);
            _ParsePage20AddrC2(data[2]);

            rv = i2cReadCB(devAddr, 0xD0, 3, data);
            if (rv != 3)
                goto exit;
            _ParsePage20AddrD0(data[0]);
            _ParsePage20AddrD1(data[1]);
            _ParsePage20AddrD2(data[2]);

            rv = i2cReadCB(devAddr, 0xE0, 3, data);
            if (rv != 3)
                goto exit;
            _ParsePage20AddrE0(data[0]);
            _ParsePage20AddrE1(data[1]);
            _ParsePage20AddrE2(data[2]);
            #endregion

            #region Reading Page21

            SetPage(Page21);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x00, 6, data);
            if (rv != 6)
                goto exit;
            _ParsePage21Addr00(data[0]);
            _ParsePage21Addr01(data[1]);
            _ParsePage21Addr02(data[2]);
            _ParsePage21Addr03(data[3]);
            _ParsePage21Addr04(data[4]);
            _ParsePage21Addr05(data[5]);


            rv = i2cReadCB(devAddr, 0x08, 1, data);
            if (rv != 1)

                goto exit;
            _ParsePage21Addr08(data[0]);

            rv = i2cReadCB(devAddr, 0x0A, 4, data);
            if (rv != 4)
                goto exit;
            _ParsePage21Addr0A(data[0]);
            _ParsePage21Addr0B(data[1]);
            _ParsePage21Addr0C(data[2]);
            _ParsePage21Addr0D(data[3]);

            rv = i2cReadCB(devAddr, 0x19, 2, data);
            if (rv != 2)
                goto exit;
            _ParsePage21Addr19(data[0]);
            _ParsePage21Addr1A(data[1]);

            rv = i2cReadCB(devAddr, 0xC0, 3, data);
            if (rv != 3)
                goto exit;
            _ParsePage21AddrC0(data[0]);
            _ParsePage21AddrC1(data[1]);
            _ParsePage21AddrC2(data[2]);

            rv = i2cReadCB(devAddr, 0xD0, 3, data);
            if (rv != 3)
                goto exit;
            _ParsePage21AddrD0(data[0]);
            _ParsePage21AddrD1(data[1]);
            _ParsePage21AddrD2(data[2]);

            rv = i2cReadCB(devAddr, 0xE0, 3, data);
            if (rv != 3)
                goto exit;
            _ParsePage21AddrE0(data[0]);
            _ParsePage21AddrE1(data[1]);
            _ParsePage21AddrE2(data[2]);
            #endregion

            #region Reading Page22
            SetPage(Page22);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x00, 6, data);
            if (rv != 6)
                goto exit;
            _ParsePage22Addr00(data[0]);
            _ParsePage22Addr01(data[1]);
            _ParsePage22Addr02(data[2]);
            _ParsePage22Addr03(data[3]);
            _ParsePage22Addr04(data[4]);
            _ParsePage22Addr05(data[5]);


            rv = i2cReadCB(devAddr, 0x08, 1, data);
            if (rv != 1)

                goto exit;
            _ParsePage22Addr08(data[0]);

            rv = i2cReadCB(devAddr, 0x0A, 4, data);
            if (rv != 4)
                goto exit;
            _ParsePage22Addr0A(data[0]);
            _ParsePage22Addr0B(data[1]);
            _ParsePage22Addr0C(data[2]);
            _ParsePage22Addr0D(data[3]);

            rv = i2cReadCB(devAddr, 0x19, 2, data);
            if (rv != 2)
                goto exit;
            _ParsePage22Addr19(data[0]);
            _ParsePage22Addr1A(data[1]);

            rv = i2cReadCB(devAddr, 0xC0, 3, data);
            if (rv != 3)
                goto exit;
            _ParsePage22AddrC0(data[0]);
            _ParsePage22AddrC1(data[1]);
            _ParsePage22AddrC2(data[2]);

            rv = i2cReadCB(devAddr, 0xD0, 3, data);
            if (rv != 3)
                goto exit;
            _ParsePage22AddrD0(data[0]);
            _ParsePage22AddrD1(data[1]);
            _ParsePage22AddrD2(data[2]);

            rv = i2cReadCB(devAddr, 0xE0, 3, data);
            if (rv != 3)
                goto exit;
            _ParsePage22AddrE0(data[0]);
            _ParsePage22AddrE1(data[1]);
            _ParsePage22AddrE2(data[2]);
            #endregion

            #region Reading Page23
            SetPage(Page23);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x00, 6, data);
            if (rv != 6)
                goto exit;
            _ParsePage23Addr00(data[0]);
            _ParsePage23Addr01(data[1]);
            _ParsePage23Addr02(data[2]);
            _ParsePage23Addr03(data[3]);
            _ParsePage23Addr04(data[4]);
            _ParsePage23Addr05(data[5]);


            rv = i2cReadCB(devAddr, 0x08, 1, data);
            if (rv != 1)

                goto exit;
            _ParsePage23Addr08(data[0]);

            rv = i2cReadCB(devAddr, 0x0A, 4, data);
            if (rv != 4)
                goto exit;
            _ParsePage23Addr0A(data[0]);
            _ParsePage23Addr0B(data[1]);
            _ParsePage23Addr0C(data[2]);
            _ParsePage23Addr0D(data[3]);

            rv = i2cReadCB(devAddr, 0x19, 2, data);
            if (rv != 2)
                goto exit;
            _ParsePage23Addr19(data[0]);
            _ParsePage23Addr1A(data[1]);

            rv = i2cReadCB(devAddr, 0xC0, 3, data);
            if (rv != 3)
                goto exit;
            _ParsePage23AddrC0(data[0]);
            _ParsePage23AddrC1(data[1]);
            _ParsePage23AddrC2(data[2]);

            rv = i2cReadCB(devAddr, 0xD0, 3, data);
            if (rv != 3)
                goto exit;
            _ParsePage23AddrD0(data[0]);
            _ParsePage23AddrD1(data[1]);
            _ParsePage23AddrD2(data[2]);

            rv = i2cReadCB(devAddr, 0xE0, 3, data);
            if (rv != 3)
                goto exit;
            _ParsePage23AddrE0(data[0]);
            _ParsePage23AddrE1(data[1]);
            _ParsePage23AddrE2(data[2]);
#endregion

         exit:
            reading = false;
            bReadAll.Enabled = true;
            
        }

        private void bDeviceReset_Click(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page00);

            if (_WritePage00Addr02() < 0)
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

        #endregion

        private void tbRSSI_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbChipID_TextChanged(object sender, EventArgs e)
        {

        }        
        
    }
}