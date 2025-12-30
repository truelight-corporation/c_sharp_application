using AardvarkAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Mald39435Mata39434aConfig
{
    public partial class UcMald39435cConfig : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        //private int iHandle = 0;
        private const byte devAddr = 28; // 設定裝置位址.
        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private bool reading = false;


        // 設定Mald-39435c page
        private static readonly byte[] Page00 = { 0x00 };
        private static readonly byte[] Page0F = { 0x0F };
        private static readonly byte[] Page10 = { 0x10 };
        private static readonly byte[] Page11 = { 0x11 };
        private static readonly byte[] Page12 = { 0x12 };
        private static readonly byte[] Page13 = { 0x13 };
        private static readonly byte[] Page30 = { 0x30 };

        // 新增Customer Page SFF-8636 CTLE function 2025.06.04
        private static readonly byte[] PageAA = { 0xAA };      
        

        public UcMald39435cConfig()
        {
            ComboboxItem item;           
            InitializeComponent();

            //debug 
            grpUtilitiesAll.Enabled = false;
            grpRealTimeAll.Enabled = false;


            #region Page L0
            // ComboBox VgaTailStg1 Addr = 0x10:0x01[7:6]
            for (int j = 0; j < 4; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(2, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboVgaTailStg1L0.Items.Add(item);
            }

            // ComboBox VgaGainStg1 Addr = 0x10:0x01[5:0]
            for (int j = 0; j < 64; j++)
            {
                item = new ComboboxItem();
                if (j == 0)
                {
                    item.Text = "0 (Minimum)";
                }
                else if (j == 31)
                {
                    item.Text = "31 (Default)";
                }
                else if (j == 63)
                {
                    item.Text = "63 (Maximum)";
                }
                else
                {
                    item.Text = j.ToString();
                }
                item.Value = j;

                cboVgaGainStg1L0.Items.Add(item);
            }


            // ComboBox VgaGainChangePolarity Addr = 0x10:0x02[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Gain";
            item.Value = 0;
            cboVga1GainChangePolarityL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Gain";
            item.Value = 1;
            cboVga1GainChangePolarityL0.Items.Add(item);

            // ComboBox Vga1GainChange Addr = 0x10:0x02[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga1GainChangeL0.Items.Add(item);
            }

            // ComboBox Vga1BandwidthChangePolarity Addr = 0x10:0x03[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Bandwidth";
            item.Value = 0;
            cboVga1BandwidthChangePolarityL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Bandwidth";
            item.Value = 1;
            cboVga1BandwidthChangePolarityL0.Items.Add(item);

            // ComboBox Vga1BandwidthChange Addr = 0x10:0x03[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga1BandwidthChangeL0.Items.Add(item);
            }

            // ComboBox VgaTailStg2 Addr = 0x10:0x04[7:6]
            for (int j = 0; j < 4; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(2, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboVgaTailStg2L0.Items.Add(item);
            }

            // ComboBox VgaGainStg2 Addr = 0x10:0x04[5:0]
            for (int j = 0; j < 64; j++)
            {
                item = new ComboboxItem();
                if (j == 0)
                {
                    item.Text = "0 (Minimum)";
                }
                else if (j == 31)
                {
                    item.Text = "31 (Default)";
                }
                else if (j == 63)
                {
                    item.Text = "63 (Maximum)";
                }
                else
                {
                    item.Text = j.ToString();
                }
                item.Value = j;

                cboVgaGainStg2L0.Items.Add(item);
            }

            // ComboBox VgaGainChangePolarity Addr = 0x10:0x05[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Gain";
            item.Value = 0;
            cboVga2GainChangePolarityL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Gain";
            item.Value = 1;
            cboVga2GainChangePolarityL0.Items.Add(item);

            // ComboBox Vga2GainChange Addr = 0x10:0x05[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga2GainChangeL0.Items.Add(item);
            }

            // ComboBox Vga2BandwidthChangePolarity Addr = 0x10:0x06[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Bandwidth";
            item.Value = 0;
            cboVga2BandwidthChangePolarityL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Bandwidth";
            item.Value = 1;
            cboVga2BandwidthChangePolarityL0.Items.Add(item);

            // ComboBox Vga2BandwidthChange Addr = 0x10:0x06[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga2BandwidthChangeL0.Items.Add(item);
            }

            // ComboBox VGA VCM Adj Addr = 0x10:0x08[7:6]
            item = new ComboboxItem();
            item.Text = "0 [00b] Normal";
            item.Value = 0;
            cboVgaVcmAdjL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1 [01b] Highest";
            item.Value = 1;
            cboVgaVcmAdjL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2 [10b] Lowest";
            item.Value = 2;
            cboVgaVcmAdjL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3 [11b] Mediuml";
            item.Value = 3;
            cboVgaVcmAdjL0.Items.Add(item);

            // ComboBox Los Threshold level Hysteresis Addr = 0x10:0x09[5]
            item = new ComboboxItem();
            item.Text = "0 [~1.5dB]";
            item.Value = 0;
            cboLosthrshystL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1 [~2.5dB]";
            item.Value = 1;
            cboLosthrshystL0.Items.Add(item);

            // ComboBox Los Threshold Addr = 0x10:0x09[4:3]
            item = new ComboboxItem();
            item.Text = "65mVppd";
            item.Value = 0;
            cboLosthrsL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "95mVppd";
            item.Value = 1;
            cboLosthrsL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "160mVppd";
            item.Value = 2;
            cboLosthrsL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "250mVppd";
            item.Value = 3;
            cboLosthrsL0.Items.Add(item);

            // ComboBox Tx Fault State Addr = 0x10:0x0E[3:2]
            item = new ComboboxItem();
            item.Text = "0:operating normally";
            item.Value = 0;
            cboTxFaultStateL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:no load";
            item.Value = 1;
            cboTxFaultStateL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:shorted to VCC33";
            item.Value = 2;
            cboTxFaultStateL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:shorted to GND";
            item.Value = 3;
            cboTxFaultStateL0.Items.Add(item);

            cboTxFaultStateL0.Enabled = false;
            chkTxFaultL0.Enabled = false;


            // ComboBox TxRVCSEL Addr = 0x10:0x10[6:3]
            item = new ComboboxItem();
            item.Text = "100 ohms";
            item.Value = 1;
            cboRVcselL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "94 ohms";
            item.Value = 2;
            cboRVcselL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "87 ohms";
            item.Value = 3;
            cboRVcselL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "82 ohms";
            item.Value = 4;
            cboRVcselL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "76 ohms";
            item.Value = 5;
            cboRVcselL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "71 ohms";
            item.Value = 6;
            cboRVcselL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "67 ohms";
            item.Value = 7;
            cboRVcselL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "63 ohms";
            item.Value = 8;
            cboRVcselL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "59 ohms";
            item.Value = 9;
            cboRVcselL0.Items.Add(item);

            // ComboBox Ibias Addr = 0x10:0x11[6:0]
            for (int j = 0; j < 128; j++)
            {
                item = new ComboboxItem();
                double rv = (double)(j + 20) / 10;
                string formattedResult = rv.ToString("0.0");
                item.Text = formattedResult + " mA";
                item.Value = j;
                cboIBiasL0.Items.Add(item);
            }

            // ComboBox IBurnIn Addr = 0x10:0x12[6:0]
            for (int j = 0; j < 128; j++)
            {
                item = new ComboboxItem();
                double rv = (double)(j + 20) / 10;
                string formattedResult = rv.ToString("0.0");
                item.Text = formattedResult + " mA";
                item.Value = j;
                cboIBurnInL0.Items.Add(item);
            }

            // ComboBox TailCurrent Addr = 0x10:0x14[6:0]
            for (int j = 0; j < 128; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(7, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboTailCurrentL0.Items.Add(item);

            }

            // ComboBox LowGainEnable Addr = 0x10:0x15[4]
            item = new ComboboxItem();
            item.Text = "Low Gain mode.";
            item.Value = 0;
            cboLowGainEnableL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "High Gain mode.";
            item.Value = 1;
            cboLowGainEnableL0.Items.Add(item);

            // ComboBox Tx EQ Addr = 0x10:0x15[3:0]
            for (int j = 0; j < 16; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(4, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboTxEqL0.Items.Add(item);
            }

            // ComboBox Tx QT Range Addr = 0x10:0x16[0]
            item = new ComboboxItem();
            item.Text = "Low Tx EQ Range.";
            item.Value = 0;
            cboTxEqGainRangeL0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "High Tx EQ Range.";
            item.Value = 1;
            cboTxEqGainRangeL0.Items.Add(item);            

            chkLosL0.Enabled = false;



            #endregion

            #region Page L1
            // ComboBox VgaTailStg1 Addr = 0x11:0x01[7:6]
            for (int j = 0; j < 4; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(2, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboVgaTailStg1L1.Items.Add(item);
            }

            // ComboBox VgaGainStg1 Addr = 0x11:0x01[5:0]
            for (int j = 0; j < 64; j++)
            {
                item = new ComboboxItem();
                if (j == 0)
                {
                    item.Text = "0 (Minimum)";
                }
                else if (j == 31)
                {
                    item.Text = "31 (Default)";
                }
                else if (j == 63)
                {
                    item.Text = "63 (Maximum)";
                }
                else
                {
                    item.Text = j.ToString();
                }
                item.Value = j;

                cboVgaGainStg1L1.Items.Add(item);
            }

            // ComboBox VgaGainChangePolarity Addr = 0x11:0x02[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Gain";
            item.Value = 0;
            cboVga1GainChangePolarityL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Gain";
            item.Value = 1;
            cboVga1GainChangePolarityL1.Items.Add(item);

            // ComboBox Vga1GainChange Addr = 0x11:0x03[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga1GainChangeL1.Items.Add(item);
            }

            // ComboBox Vga1BandwidthChangePolarity Addr = 0x11:0x03[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Bandwidth";
            item.Value = 0;
            cboVga1BandwidthChangePolarityL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Bandwidth";
            item.Value = 1;
            cboVga1BandwidthChangePolarityL1.Items.Add(item);

            // ComboBox Vga1BandwidthChange Addr = 0x11:0x03[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga1BandwidthChangeL1.Items.Add(item);
            }

            // ComboBox VgaTailStg2 Addr = 0x11:0x04[7:6]
            for (int j = 0; j < 4; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(2, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboVgaTailStg2L1.Items.Add(item);
            }

            // ComboBox VgaGainStg2 Addr = 0x11:0x04[5:0]
            for (int j = 0; j < 64; j++)
            {
                item = new ComboboxItem();
                if (j == 0)
                {
                    item.Text = "0 (Minimum)";
                }
                else if (j == 31)
                {
                    item.Text = "31 (Default)";
                }
                else if (j == 63)
                {
                    item.Text = "63 (Maximum)";
                }
                else
                {
                    item.Text = j.ToString();
                }
                item.Value = j;

                cboVgaGainStg2L1.Items.Add(item);
            }

            // ComboBox VgaGainChangePolarity Addr = 0x11:0x05[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Gain";
            item.Value = 0;
            cboVga2GainChangePolarityL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Gain";
            item.Value = 1;
            cboVga2GainChangePolarityL1.Items.Add(item);

            // ComboBox Vga2GainChange Addr = 0x11:0x05[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga2GainChangeL1.Items.Add(item);
            }

            // ComboBox Vga2BandwidthChangePolarity Addr = 0x11:0x06[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Bandwidth";
            item.Value = 0;
            cboVga2BandwidthChangePolarityL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Bandwidth";
            item.Value = 1;
            cboVga2BandwidthChangePolarityL1.Items.Add(item);

            // ComboBox Vga2BandwidthChange Addr = 0x11:0x06[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga2BandwidthChangeL1.Items.Add(item);
            }

            // ComboBox VGA VCM Adj Addr = 0x11:0x08[7:6]
            item = new ComboboxItem();
            item.Text = "0 [00b] Normal";
            item.Value = 0;
            cboVgaVcmAdjL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1 [01b] Highest";
            item.Value = 1;
            cboVgaVcmAdjL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2 [10b] Lowest";
            item.Value = 2;
            cboVgaVcmAdjL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3 [11b] Mediuml";
            item.Value = 3;
            cboVgaVcmAdjL1.Items.Add(item);

            // ComboBox Los Threshold level Hysteresis Addr = 0x11:0x09[5]
            item = new ComboboxItem();
            item.Text = "0 [~1.5dB]";
            item.Value = 0;
            cboLosthrshystL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1 [~2.5dB]";
            item.Value = 1;
            cboLosthrshystL1.Items.Add(item);

            // ComboBox Los Threshold Addr = 0x11:0x09[4:3]
            item = new ComboboxItem();
            item.Text = "65mVppd";
            item.Value = 0;
            cboLosthrsL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "95mVppd";
            item.Value = 1;
            cboLosthrsL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "160mVppd";
            item.Value = 2;
            cboLosthrsL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "250mVppd";
            item.Value = 3;
            cboLosthrsL1.Items.Add(item);

            // ComboBox Tx Fault State Addr = 0x11:0x0E[3:2]
            item = new ComboboxItem();
            item.Text = "0:operating normally";
            item.Value = 0;
            cboTxFaultStateL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:no load";
            item.Value = 1;
            cboTxFaultStateL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:shorted to VCC33";
            item.Value = 2;
            cboTxFaultStateL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:shorted to GND";
            item.Value = 3;
            cboTxFaultStateL1.Items.Add(item);

            cboTxFaultStateL1.Enabled = false;
            chkTxFaultL1.Enabled = false;

            // ComboBox TxRVCSEL Addr = 0x11:0x10[6:3]
            item = new ComboboxItem();
            item.Text = "100 ohms";
            item.Value = 1;
            cboRVcselL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "94 ohms";
            item.Value = 2;
            cboRVcselL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "87 ohms";
            item.Value = 3;
            cboRVcselL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "82 ohms";
            item.Value = 4;
            cboRVcselL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "76 ohms";
            item.Value = 5;
            cboRVcselL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "71 ohms";
            item.Value = 6;
            cboRVcselL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "67 ohms";
            item.Value = 7;
            cboRVcselL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "63 ohms";
            item.Value = 8;
            cboRVcselL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "59 ohms";
            item.Value = 9;
            cboRVcselL1.Items.Add(item);


            // ComboBox IBias Addr = 0x11:0x11[6:0]
            for (int j = 0; j < 128; j++)
            {
                item = new ComboboxItem();
                double rv = (double)(j + 20) / 10;
                string formattedResult = rv.ToString("0.0");
                item.Text = formattedResult + " mA";
                item.Value = j;
                cboIBiasL1.Items.Add(item);
            }

            // ComboBox IBurnIn Addr = 0x11:0x12[6:0]
            for (int j = 0; j < 128; j++)
            {
                item = new ComboboxItem();
                double rv = (double)(j + 20) / 10;
                string formattedResult = rv.ToString("0.0");
                item.Text = formattedResult + " mA";
                item.Value = j;
                cboIBurnInL1.Items.Add(item);
            }

            // ComboBox TailCurrent Addr = 0x11:0x14[6:0]
            for (int j = 0; j < 128; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(7, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboTailCurrentL1.Items.Add(item);

            }

            // ComboBox LowGainEnable Addr = 0x11:0x15[4]
            item = new ComboboxItem();
            item.Text = "Low Gain mode.";
            item.Value = 0;
            cboLowGainEnableL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "High Gain mode.";
            item.Value = 1;
            cboLowGainEnableL1.Items.Add(item);
            

            // ComboBox Tx EQ Addr = 0x11:0x15[3:0]
            for (int j = 0; j < 16; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(4, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboTxEqL1.Items.Add(item);
            }

            // ComboBox Tx QT Range Addr = 0x11:0x16[0]
            item = new ComboboxItem();
            item.Text = "Low Tx EQ Range.";
            item.Value = 0;
            cboTxEqGainRangeL1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "High Tx EQ Range.";
            item.Value = 1;
            cboTxEqGainRangeL1.Items.Add(item);

            chkLosL1.Enabled = false;

            #endregion

            #region Page L2
            // ComboBox VgaTailStg1 Addr = 0x12:0x01[7:6]
            for (int j = 0; j < 4; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(2, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboVgaTailStg1L2.Items.Add(item);
            }

            // ComboBox VgaGainStg1 Addr = 0x12:0x01[5:0]
            for (int j = 0; j < 64; j++)
            {
                item = new ComboboxItem();
                if (j == 0)
                {
                    item.Text = "0 (Minimum)";
                }
                else if (j == 31)
                {
                    item.Text = "31 (Default)";
                }
                else if (j == 63)
                {
                    item.Text = "63 (Maximum)";
                }
                else
                {
                    item.Text = j.ToString();
                }
                item.Value = j;

                cboVgaGainStg1L2.Items.Add(item);
            }

            // ComboBox VgaGainChangePolarity Addr = 0x12:0x02[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Gain";
            item.Value = 0;
            cboVga1GainChangePolarityL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Gain";
            item.Value = 1;
            cboVga1GainChangePolarityL2.Items.Add(item);

            // ComboBox Vga1GainChange Addr = 0x12:0x03[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga1GainChangeL2.Items.Add(item);
            }

            // ComboBox Vga1BandwidthChangePolarity Addr = 0x12:0x03[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Bandwidth";
            item.Value = 0;
            cboVga1BandwidthChangePolarityL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Bandwidth";
            item.Value = 1;
            cboVga1BandwidthChangePolarityL2.Items.Add(item);

            // ComboBox Vga1BandwidthChange Addr = 0x12:0x03[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga1BandwidthChangeL2.Items.Add(item);
            }

            // ComboBox VgaTailStg2 Addr = 0x12:0x04[7:6]
            for (int j = 0; j < 4; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(2, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboVgaTailStg2L2.Items.Add(item);
            }

            // ComboBox VgaGainStg2 Addr = 0x12:0x04[5:0]
            for (int j = 0; j < 64; j++)
            {
                item = new ComboboxItem();
                if (j == 0)
                {
                    item.Text = "0 (Minimum)";
                }
                else if (j == 31)
                {
                    item.Text = "31 (Default)";
                }
                else if (j == 63)
                {
                    item.Text = "63 (Maximum)";
                }
                else
                {
                    item.Text = j.ToString();
                }
                item.Value = j;

                cboVgaGainStg2L2.Items.Add(item);
            }

            // ComboBox VgaGainChangePolarity Addr = 0x12:0x05[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Gain";
            item.Value = 0;
            cboVga2GainChangePolarityL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Gain";
            item.Value = 1;
            cboVga2GainChangePolarityL2.Items.Add(item);

            // ComboBox Vga2GainChange Addr = 0x12:0x05[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga2GainChangeL2.Items.Add(item);
            }

            // ComboBox Vga2BandwidthChangePolarity Addr = 0x12:0x06[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Bandwidth";
            item.Value = 0;
            cboVga2BandwidthChangePolarityL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Bandwidth";
            item.Value = 1;
            cboVga2BandwidthChangePolarityL2.Items.Add(item);

            // ComboBox Vga2BandwidthChange Addr = 0x12:0x06[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga2BandwidthChangeL2.Items.Add(item);
            }

            // ComboBox VGA VCM Adj Addr = 0x12:0x08[7:6]
            item = new ComboboxItem();
            item.Text = "0 [00b] Normal";
            item.Value = 0;
            cboVgaVcmAdjL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1 [01b] Highest";
            item.Value = 1;
            cboVgaVcmAdjL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2 [10b] Lowest";
            item.Value = 2;
            cboVgaVcmAdjL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3 [11b] Mediuml";
            item.Value = 3;
            cboVgaVcmAdjL2.Items.Add(item);

            // ComboBox Los Threshold level Hysteresis Addr = 0x12:0x09[5]
            item = new ComboboxItem();
            item.Text = "0 [~1.5dB]";
            item.Value = 0;
            cboLosthrshystL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1 [~2.5dB]";
            item.Value = 1;
            cboLosthrshystL2.Items.Add(item);

            // ComboBox Los Threshold Addr = 0x12:0x09[4:3]
            item = new ComboboxItem();
            item.Text = "65mVppd";
            item.Value = 0;
            cboLosthrsL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "95mVppd";
            item.Value = 1;
            cboLosthrsL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "160mVppd";
            item.Value = 2;
            cboLosthrsL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "250mVppd";
            item.Value = 3;
            cboLosthrsL2.Items.Add(item);

            // ComboBox Tx Fault State Addr = 0x12:0x0E[3:2]
            item = new ComboboxItem();
            item.Text = "0:operating normally";
            item.Value = 0;
            cboTxFaultStateL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:no load";
            item.Value = 1;
            cboTxFaultStateL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:shorted to VCC33";
            item.Value = 2;
            cboTxFaultStateL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:shorted to GND";
            item.Value = 3;
            cboTxFaultStateL2.Items.Add(item);

            cboTxFaultStateL2.Enabled = false;
            chkTxFaultL2.Enabled = false;

            // ComboBox TxRVCSEL Addr = 0x12:0x10[6:3]
            item = new ComboboxItem();
            item.Text = "100 ohms";
            item.Value = 1;
            cboRVcselL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "94 ohms";
            item.Value = 2;
            cboRVcselL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "87 ohms";
            item.Value = 3;
            cboRVcselL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "82 ohms";
            item.Value = 4;
            cboRVcselL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "76 ohms";
            item.Value = 5;
            cboRVcselL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "71 ohms";
            item.Value = 6;
            cboRVcselL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "67 ohms";
            item.Value = 7;
            cboRVcselL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "63 ohms";
            item.Value = 8;
            cboRVcselL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "59 ohms";
            item.Value = 9;
            cboRVcselL2.Items.Add(item);

            // ComboBox IBias Addr = 0x12:0x11[6:0]
            for (int j = 0; j < 128; j++)
            {
                item = new ComboboxItem();
                double rv = (double)(j + 20) / 10;
                string formattedResult = rv.ToString("0.0");
                item.Text = formattedResult + " mA";
                item.Value = j;
                cboIBiasL2.Items.Add(item);
            }

            // ComboBox IBurnIn Addr = 0x12:0x12[6:0]
            for (int j = 0; j < 128; j++)
            {
                item = new ComboboxItem();
                double rv = (double)(j + 20) / 10;
                string formattedResult = rv.ToString("0.0");
                item.Text = formattedResult + " mA";
                item.Value = j;
                cboIBurnInL2.Items.Add(item);
            }

            // ComboBox TailCurrent Addr = 0x12:0x14[6:0]
            for (int j = 0; j < 128; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(7, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboTailCurrentL2.Items.Add(item);

            }

            // ComboBox LowGainEnable Addr = 0x12:0x15[4]
            item = new ComboboxItem();
            item.Text = "Low Gain mode.";
            item.Value = 0;
            cboLowGainEnableL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "High Gain mode.";
            item.Value = 1;
            cboLowGainEnableL2.Items.Add(item);

            // ComboBox Tx EQ Addr = 0x12:0x15[3:0]
            for (int j = 0; j < 16; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(4, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboTxEqL2.Items.Add(item);
            }

            // ComboBox Tx QT Range Addr = 0x12:0x16[0]
            item = new ComboboxItem();
            item.Text = "Low Tx EQ Range.";
            item.Value = 0;
            cboTxEqGainRangeL2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "High Tx EQ Range.";
            item.Value = 1;
            cboTxEqGainRangeL2.Items.Add(item);

            chkLosL2.Enabled = false;

            #endregion

            #region Page L3
            // ComboBox VgaTailStg1 Addr = 0x13:0x01[7:6]
            for (int j = 0; j < 4; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(2, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboVgaTailStg1L3.Items.Add(item);
            }

            // ComboBox VgaGainStg1 Addr = 0x13:0x01[5:0]
            for (int j = 0; j < 64; j++)
            {
                item = new ComboboxItem();
                if (j == 0)
                {
                    item.Text = "0 (Minimum)";
                }
                else if (j == 31)
                {
                    item.Text = "31 (Default)";
                }
                else if (j == 63)
                {
                    item.Text = "63 (Maximum)";
                }
                else
                {
                    item.Text = j.ToString();
                }
                item.Value = j;

                cboVgaGainStg1L3.Items.Add(item);
            }

            // ComboBox VgaGainChangePolarity Addr = 0x13:0x02[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Gain";
            item.Value = 0;
            cboVga1GainChangePolarityL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Gain";
            item.Value = 1;
            cboVga1GainChangePolarityL3.Items.Add(item);

            // ComboBox Vga1GainChange Addr = 0x13:0x03[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga1GainChangeL3.Items.Add(item);
            }

            // ComboBox Vga1BandwidthChangePolarity Addr = 0x13:0x03[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Bandwidth";
            item.Value = 0;
            cboVga1BandwidthChangePolarityL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Bandwidth";
            item.Value = 1;
            cboVga1BandwidthChangePolarityL3.Items.Add(item);

            // ComboBox Vga1BandwidthChange Addr = 0x13:0x03[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga1BandwidthChangeL3.Items.Add(item);
            }

            // ComboBox VgaTailStg2 Addr = 0x13:0x04[7:6]
            for (int j = 0; j < 4; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(2, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboVgaTailStg2L3.Items.Add(item);
            }

            // ComboBox VgaGainStg2 Addr = 0x13:0x04[5:0]
            for (int j = 0; j < 64; j++)
            {
                item = new ComboboxItem();
                if (j == 0)
                {
                    item.Text = "0 (Minimum)";
                }
                else if (j == 31)
                {
                    item.Text = "31 (Default)";
                }
                else if (j == 63)
                {
                    item.Text = "63 (Maximum)";
                }
                else
                {
                    item.Text = j.ToString();
                }
                item.Value = j;

                cboVgaGainStg2L3.Items.Add(item);
            }

            // ComboBox VgaGainChangePolarity Addr = 0x13:0x05[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Gain";
            item.Value = 0;
            cboVga2GainChangePolarityL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Gain";
            item.Value = 1;
            cboVga2GainChangePolarityL3.Items.Add(item);

            // ComboBox Vga2GainChange Addr = 0x13:0x05[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga2GainChangeL3.Items.Add(item);
            }

            // ComboBox Vga2BandwidthChangePolarity Addr = 0x13:0x06[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Bandwidth";
            item.Value = 0;
            cboVga2BandwidthChangePolarityL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Bandwidth";
            item.Value = 1;
            cboVga2BandwidthChangePolarityL3.Items.Add(item);

            // ComboBox Vga2BandwidthChange Addr = 0x13:0x06[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga2BandwidthChangeL3.Items.Add(item);
            }

            // ComboBox VGA VCM Adj Addr = 0x13:0x08[7:6]
            item = new ComboboxItem();
            item.Text = "0 [00b] Normal";
            item.Value = 0;
            cboVgaVcmAdjL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1 [01b] Highest";
            item.Value = 1;
            cboVgaVcmAdjL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2 [10b] Lowest";
            item.Value = 2;
            cboVgaVcmAdjL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3 [11b] Mediuml";
            item.Value = 3;
            cboVgaVcmAdjL3.Items.Add(item);


            // ComboBox Los Threshold level Hysteresis Addr = 0x13:0x09[5]
            item = new ComboboxItem();
            item.Text = "0 [~1.5dB]";
            item.Value = 0;
            cboLosthrshystL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1 [~2.5dB]";
            item.Value = 1;
            cboLosthrshystL3.Items.Add(item);

            // ComboBox Los Threshold Addr = 0x13:0x09[4:3]
            item = new ComboboxItem();
            item.Text = "65mVppd";
            item.Value = 0;
            cboLosthrsL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "95mVppd";
            item.Value = 1;
            cboLosthrsL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "160mVppd";
            item.Value = 2;
            cboLosthrsL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "250mVppd";
            item.Value = 3;
            cboLosthrsL3.Items.Add(item);

            // ComboBox Tx Fault State Addr = 0x13:0x0E[3:2]
            item = new ComboboxItem();
            item.Text = "0:operating normally";
            item.Value = 0;
            cboTxFaultStateL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:no load";
            item.Value = 1;
            cboTxFaultStateL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:shorted to VCC33";
            item.Value = 2;
            cboTxFaultStateL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:shorted to GND";
            item.Value = 3;
            cboTxFaultStateL3.Items.Add(item);

            cboTxFaultStateL3.Enabled = false;
            chkTxFaultL3.Enabled = false;

            // ComboBox TxRVCSEL Addr = 0x13:0x10[6:3]
            item = new ComboboxItem();
            item.Text = "100 ohms";
            item.Value = 1;
            cboRVcselL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "94 ohms";
            item.Value = 2;
            cboRVcselL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "87 ohms";
            item.Value = 3;
            cboRVcselL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "82 ohms";
            item.Value = 4;
            cboRVcselL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "76 ohms";
            item.Value = 5;
            cboRVcselL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "71 ohms";
            item.Value = 6;
            cboRVcselL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "67 ohms";
            item.Value = 7;
            cboRVcselL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "63 ohms";
            item.Value = 8;
            cboRVcselL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "59 ohms";
            item.Value = 9;
            cboRVcselL3.Items.Add(item);

            // ComboBox IBias Addr = 0x13:0x11[6:0]
            for (int j = 0; j < 128; j++)
            {
                item = new ComboboxItem();
                double rv = (double)(j + 20) / 10;
                string formattedResult = rv.ToString("0.0");
                item.Text = formattedResult + " mA";
                item.Value = j;
                cboIBiasL3.Items.Add(item);
            }

            // ComboBox IBurnIn Addr = 0x13:0x12[6:0]
            for (int j = 0; j < 128; j++)
            {
                item = new ComboboxItem();
                double rv = (double)(j + 20) / 10;
                string formattedResult = rv.ToString("0.0");
                item.Text = formattedResult + " mA";
                item.Value = j;
                cboIBurnInL3.Items.Add(item);
            }

            // ComboBox TailCurrent Addr = 0x13:0x14[6:0]
            for (int j = 0; j < 128; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(7, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboTailCurrentL3.Items.Add(item);

            }

            // ComboBox LowGainEnable Addr = 0x13:0x15[4]            
            item = new ComboboxItem();
            item.Text = "Low Gain mode.";
            item.Value = 0;
            cboLowGainEnableL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "High Gain mode.";
            item.Value = 1;
            cboLowGainEnableL3.Items.Add(item);

            // ComboBox Tx EQ Addr = 0x13:0x15[3:0]
            for (int j = 0; j < 16; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(4, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboTxEqL3.Items.Add(item);
            }

            // ComboBox Tx QT Range Addr = 0x0F:0x16[0]
            item = new ComboboxItem();
            item.Text = "Low Tx EQ Range.";
            item.Value = 0;
            cboTxEqGainRangeL3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "High Tx EQ Range.";
            item.Value = 1;
            cboTxEqGainRangeL3.Items.Add(item);

            chkLosL3.Enabled = false;


            #endregion

            #region Page All

            // ComboBox Addr = 0x00:0x03 [7] Intrpt Pad Polarity
            item = new ComboboxItem();
            item.Text = "0:CMOS & Open drain type";
            item.Value = 0;
            cboIntrptPadPolarity.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:CMOS only";
            item.Value = 1;
            cboIntrptPadPolarity.Items.Add(item);


            // ComboBox Addr = 0x00:0x03[6] Intrpt oen
            item = new ComboboxItem();
            item.Text = "0:Config as open drain";
            item.Value = 0;
            cboIntrptoen.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Config as CMOS";
            item.Value = 1;
            cboIntrptoen.Items.Add(item);

            // ComboBox Addr = 0x00:0x03[4] I2C Address Mode
            item = new ComboboxItem();
            item.Text = "0:Inc ACK only";
            item.Value = 0;
            cboI2cAddressMode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:Inc ACK/NACK";
            item.Value = 1;
            cboI2cAddressMode.Items.Add(item);

            // ComboBox VgaTailStg1 Addr = 0x0F:0x01[7:6]
            for (int j = 0; j < 4; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(2, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboVgaTailStg1All.Items.Add(item);
            }

            // ComboBox VgaGainStg1 Addr = 0x0F:0x01[5:0]
            for (int j = 0; j < 64; j++)
            {
                item = new ComboboxItem();
                if (j == 0)
                {
                    item.Text = "0 (Minimum)";
                }
                else if (j == 31)
                {
                    item.Text = "31 (Default)";
                }
                else if (j == 63)
                {
                    item.Text = "63 (Maximum)";
                }
                else
                {
                    item.Text = j.ToString();
                }
                item.Value = j;

                cboVgaGainStg1All.Items.Add(item);
            }

            // ComboBox VgaGainChangePolarity Addr = 0x0F:0x02[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Gain";
            item.Value = 0;
            cboVga1GainChangePolarityAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Gain";
            item.Value = 1;
            cboVga1GainChangePolarityAll.Items.Add(item);

            // ComboBox Vga1GainChange Addr = 0x0F:0x02[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga1GainChangeAll.Items.Add(item);
            }

            // ComboBox Vga1BandwidthChangePolarity Addr = 0x0F:0x03[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Bandwidth";
            item.Value = 0;
            cboVga1BandwidthChangePolarityAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Bandwidth";
            item.Value = 1;
            cboVga1BandwidthChangePolarityAll.Items.Add(item);

            // ComboBox Vga1BandwidthChange Addr = 0x0F:0x03[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga1BandwidthChangeAll.Items.Add(item);
            }

            // ComboBox VgaTailStg2 Addr = 0x0F:0x04[7:6]
            for (int j = 0; j < 4; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(2, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboVgaTailStg2All.Items.Add(item);
            }

            // ComboBox VgaGainStg2 Addr = 0x0F:0x04[5:0]
            for (int j = 0; j < 64; j++)
            {
                item = new ComboboxItem();
                if (j == 0)
                {
                    item.Text = "0 (Minimum)";
                }
                else if (j == 31)
                {
                    item.Text = "31 (Default)";
                }
                else if (j == 63)
                {
                    item.Text = "63 (Maximum)";
                }
                else
                {
                    item.Text = j.ToString();
                }
                item.Value = j;

                cboVgaGainStg2All.Items.Add(item);
            }


            // ComboBox VgaGainChangePolarity Addr = 0x0F:0x05[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Gain";
            item.Value = 0;
            cboVga2GainChangePolarityAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Gain";
            item.Value = 1;
            cboVga2GainChangePolarityAll.Items.Add(item);

            // ComboBox Vga2GainChange Addr = 0x0F:0x05[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga2GainChangeAll.Items.Add(item);
            }

            // ComboBox Vga2BandwidthChangePolarity Addr = 0x0F:0x06[5]
            item = new ComboboxItem();
            item.Text = "0b: Increases Bandwidth";
            item.Value = 0;
            cboVga2BandwidthChangePolarityAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1b: Decreases Bandwidth";
            item.Value = 1;
            cboVga2BandwidthChangePolarityAll.Items.Add(item);

            // ComboBox Vga2BandwidthChange Addr = 0x0F:0x06[4:0]
            for (int j = 0; j < 32; j++)
            {
                item = new ComboboxItem();
                item.Text = j.ToString();
                item.Value = j;
                cboVga2BandwidthChangeAll.Items.Add(item);
            }

            // ComboBox VGA VCM Adj Addr = 0x0F:0x08[7:6]
            item = new ComboboxItem();
            item.Text = "0 [00b] Normal";
            item.Value = 0;
            cboVgaVcmAdjAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1 [01b] Highest";
            item.Value = 1;
            cboVgaVcmAdjAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2 [10b] Lowest";
            item.Value = 2;
            cboVgaVcmAdjAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3 [11b] Mediuml";
            item.Value = 3;
            cboVgaVcmAdjAll.Items.Add(item);

            // ComboBox Los Threshold level Hysteresis Addr = 0x0F:0x09[5]
            item = new ComboboxItem();
            item.Text = "0 [~1.5dB]";
            item.Value = 0;
            cboLosthrshystAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1 [~2.5dB]";
            item.Value = 1;
            cboLosthrshystAll.Items.Add(item);

            // ComboBox Los Threshold Addr = 0x0F:0x09[4:3]
            item = new ComboboxItem();
            item.Text = "65mVppd";
            item.Value = 0;
            cboLosthrsAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "95mVppd";
            item.Value = 1;
            cboLosthrsAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "160mVppd";
            item.Value = 2;
            cboLosthrsAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "250mVppd";
            item.Value = 3;
            cboLosthrsAll.Items.Add(item);

            // ComboBox Tx Fault State Addr = 0x0F:0x0E[3:2]
            item = new ComboboxItem();
            item.Text = "0:operating normally";
            item.Value = 0;
            cboTxFaultStateAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:no load";
            item.Value = 1;
            cboTxFaultStateAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:shorted to VCC33";
            item.Value = 2;
            cboTxFaultStateAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:shorted to GND";
            item.Value = 3;
            cboTxFaultStateAll.Items.Add(item);

            cboTxFaultStateAll.Enabled = false;
            chkTxFaultAll.Enabled = false;

            // ComboBox TxRVCSEL Addr = 0x0F:0x10[6:3]
            item = new ComboboxItem();
            item.Text = "100 ohms";
            item.Value = 1;
            cboRVcselAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "94 ohms";
            item.Value = 2;
            cboRVcselAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "87 ohms";
            item.Value = 3;
            cboRVcselAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "82 ohms";
            item.Value = 4;
            cboRVcselAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "76 ohms";
            item.Value = 5;
            cboRVcselAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "71 ohms";
            item.Value = 6;
            cboRVcselAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "67 ohms";
            item.Value = 7;
            cboRVcselAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "63 ohms";
            item.Value = 8;
            cboRVcselAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "59 ohms";
            item.Value = 9;
            cboRVcselAll.Items.Add(item);


            // ComboBox Ibias Addr = 0x0F:0x11[6:0]
            for (int j = 0; j < 128; j++)
            {
                item = new ComboboxItem();
                double rv = (double)(j + 20) / 10;
                string formattedResult = rv.ToString("0.0");
                item.Text = formattedResult + " mA";
                item.Value = j;
                cboIBiasAll.Items.Add(item);
            }

            // ComboBox IBurnIn Addr = 0x0F:0x12[6:0]
            for (int j = 0; j < 128; j++)
            {
                item = new ComboboxItem();
                double rv = (double)(j + 20) / 10;
                string formattedResult = rv.ToString("0.0");
                item.Text = formattedResult + " mA";
                item.Value = j;
                cboIBurnInAll.Items.Add(item);
            }

            // ComboBox TailCurrent Addr = 0x0F:0x14[6:0]
            for (int j = 0; j < 128; j++)
            {
                item = new ComboboxItem();
                string binRep = Convert.ToString(j, 2).PadLeft(7, '0');
                item.Text = $"{j} ({binRep}b)";
                item.Value = j;
                cboTailCurrentAll.Items.Add(item);

            }

            // ComboBox LowGainEnable Addr = 0x0F:0x15[4]
            item = new ComboboxItem();
            item.Text = "Low Gain mode.";
            item.Value = 0;
            cboLowGainEnableAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "High Gain mode.";
            item.Value = 1;
            cboLowGainEnableAll.Items.Add(item);


            // ComboBox Tx EQ Addr = 0x0Fx15[3:0]
            for (int j = 0; j < 16; j++)
            {
                item = new ComboboxItem();
                string binaryRepresentation = Convert.ToString(j, 2).PadLeft(4, '0');
                item.Text = $"{j} ({binaryRepresentation}b)";
                item.Value = j;
                cboTxEqAll.Items.Add(item);
            }


            // ComboBox Tx QT Range Addr = 0x0F:0x16[0]
            item = new ComboboxItem();
            item.Text = "Low Tx EQ Range.";
            item.Value = 0;
            cboTxEqGainRangeAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "High Tx EQ Range.";
            item.Value = 1;
            cboTxEqGainRangeAll.Items.Add(item);

            chkLosL3.Enabled = false;



            // ComboBox ADC Select Addr = 0x30:0x00[2:0]
            item = new ComboboxItem();
            item.Text = "000b ,ADC voltage probe.";
            item.Value = 0;
            cboAdcSelectAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "001b ,ADC Monitors temperature.";
            item.Value = 1;
            cboAdcSelectAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "010b ,ADC Monitors MON0 pad.";
            item.Value = 2;
            cboAdcSelectAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "011b ,ADC Monitors MON1 pad.";
            item.Value = 3;
            cboAdcSelectAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "100b ,ADC Monitors MON2 pad.";
            item.Value = 4;
            cboAdcSelectAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "101b ,ADC Monitors MON3 pad.";
            item.Value = 5;
            cboAdcSelectAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "110b ,ADC Monitors IBIAS current.";
            item.Value = 6;
            cboAdcSelectAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "111b ,Unused.";
            item.Value = 7;
            cboAdcSelectAll.Items.Add(item);



            #endregion

            #region Customer            
            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL0InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL0InputEqualization0db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL0InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL0InputEqualization1db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL0InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL0InputEqualization2db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL0InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL0InputEqualization3db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL0InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL0InputEqualization4db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL0InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL0InputEqualization5db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL0InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL0InputEqualization6db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL0InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL0InputEqualization7db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL0InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL0InputEqualization8db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL0InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL0InputEqualization9db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL0InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL0InputEqualization10db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL0InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL0InputEqualizationReserved0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL0InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL0InputEqualizationReserved1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL1InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL1InputEqualization0db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL1InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL1InputEqualization1db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL1InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL1InputEqualization2db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL1InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL1InputEqualization3db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL1InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL1InputEqualization4db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL1InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL1InputEqualization5db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL1InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL1InputEqualization6db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL1InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL1InputEqualization7db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL1InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL1InputEqualization8db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL1InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL1InputEqualization9db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL1InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL1InputEqualization10db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL1InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL1InputEqualizationReserved0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL1InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL1InputEqualizationReserved1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL2InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL2InputEqualization0db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL2InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL2InputEqualization1db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL2InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL2InputEqualization2db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL2InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL2InputEqualization3db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL2InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL2InputEqualization4db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL2InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL2InputEqualization5db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL2InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL2InputEqualization6db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL2InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL2InputEqualization7db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL2InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL2InputEqualization8db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL2InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL2InputEqualization9db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL2InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL2InputEqualization10db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL2InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL2InputEqualizationReserved0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL2InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL2InputEqualizationReserved1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL3InputEqualization0db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL3InputEqualization0db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL3InputEqualization1db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL3InputEqualization1db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL3InputEqualization2db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL3InputEqualization2db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL3InputEqualization3db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL3InputEqualization3db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL3InputEqualization4db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL3InputEqualization4db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL3InputEqualization5db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL3InputEqualization5db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL3InputEqualization6db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL3InputEqualization6db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL3InputEqualization7db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL3InputEqualization7db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL3InputEqualization8db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL3InputEqualization8db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL3InputEqualization9db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL3InputEqualization9db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL3InputEqualization10db.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL3InputEqualization10db.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL3InputEqualizationReserved0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL3InputEqualizationReserved0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0:1.3dB";
            item.Value = 0;
            cboL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "1:1.9dB";
            item.Value = 1;
            cboL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "2:2.7dB";
            item.Value = 2;
            cboL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "3:3.7dB";
            item.Value = 3;
            cboL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "4:4.8dB";
            item.Value = 4;
            cboL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "5:5.3dB";
            item.Value = 5;
            cboL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "6:5.9dB";
            item.Value = 6;
            cboL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "7:6.5dB";
            item.Value = 7;
            cboL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "8:7.2dB";
            item.Value = 8;
            cboL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "9:7.7dB";
            item.Value = 9;
            cboL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "10:8.2dB";
            item.Value = 10;
            cboL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "11:8.8dB";
            item.Value = 11;
            cboL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "12:9.4dB";
            item.Value = 12;
            cboL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "13:9.8dB";
            item.Value = 13;
            cboL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "14:10.3dB";
            item.Value = 14;
            cboL3InputEqualizationReserved1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "15:10.7dB";
            item.Value = 15;
            cboL3InputEqualizationReserved1.Items.Add(item);

            #endregion

        }

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

        public int SetPage(byte[] page)
        {
            byte[] setPage = new byte[1];
            setPage[0] = page[0];
            int rv;

            rv = i2cWriteCB(devAddr, 0xFF, 1, page);
            if (rv < 0)
                return -1;

            return 0;

        }

        /*
        public bool PageChecker()
        {
            byte[] CurrentPage = new byte[1];
            int rv;

            rv = i2cReadCB(devAddr, 0xFF, 1, CurrentPage);
            if (rv != 1)
                goto exit;

            int CurrentIndex = tabMald39435cConfig.SelectedIndex;

            if (CurrentIndex == CurrentPage[0])
                return true;           

        exit:
            reading = false;
            btnReadAll.Enabled = true;
            return false;
        }
        */

        #endregion

        #region 主畫面按鈕事件
        private void btnReadAll_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[10];           

            int rv;

            if (reading == true)
                return;

            reading = true;
            btnReadAll.Enabled = false;

            if (i2cReadCB == null)
                goto exit;

            #region Reading Page00

            SetPage(Page00);
            Thread.Sleep(100);

            rv = i2cReadCB(devAddr, 0x00, 4, data);
            if (rv != 4)
                goto exit;

            _ParsePage00Addr00(data[0]);
            _ParsePage00Addr01(data[1]);
            _ParsePage00Addr03(data[3]);

            /*
            _ParseAddr00(data[0], rPage00);
            _ParseAddr01(data[1], rPage00);
            _ParseAddr02(data[2], rPage00);
            _ParseAddr03(data[3], rPage00);
            */
            #endregion

            #region Reading Page0F

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cReadCB(devAddr, 0x00, 7, data);
            if (rv != 7)
                goto exit;

            _ParsePage0FAddr00(data[0]);
            _ParsePage0FAddr01(data[1]);
            _ParsePage0FAddr02(data[2]);
            _ParsePage0FAddr03(data[3]);
            _ParsePage0FAddr04(data[4]);
            _ParsePage0FAddr05(data[5]);
            _ParsePage0FAddr06(data[6]);

            /*
            _ParseAddr00(data[0], rPage0F);
            _ParseAddr01(data[1], rPage0F);
            _ParseAddr02(data[2], rPage0F);
            _ParseAddr03(data[3], rPage0F);
            _ParseAddr04(data[4], rPage0F);
            _ParseAddr05(data[5], rPage0F);
            _ParseAddr06(data[6], rPage0F);
            */

            rv = i2cReadCB(devAddr, 0x08, 2, data);
            if (rv != 2)
                goto exit;

            _ParsePage0FAddr08(data[0]);
            _ParsePage0FAddr09(data[1]);

            /*
            _ParseAddr08(data[0], rPage0F);
            _ParseAddr09(data[1], rPage0F);
            */


            rv = i2cReadCB(devAddr, 0x0D, 3, data);
            if (rv != 3)
                goto exit;

            _ParsePage0FAddr0D(data[0]);
            _ParsePage0FAddr0E(data[1]);
            _ParsePage0FAddr0F(data[2]);

            /*
            _ParseAddr0D(data[0], rPage0F);
            _ParseAddr0E(data[1], rPage0F);
            _ParseAddr0F(data[2], rPage0F);
            */



            rv = i2cReadCB(devAddr, 0x10, 3, data);
            if (rv != 3)
                goto exit;

            _ParsePage0FAddr10(data[0]);
            _ParsePage0FAddr11(data[1]);
            _ParsePage0FAddr12(data[2]);

            /*
            _ParseAddr10(data[0], rPage0F);
            _ParseAddr11(data[1], rPage0F);
            _ParseAddr12(data[2], rPage0F);
            */

            rv = i2cReadCB(devAddr, 0x14, 4, data);
            if (rv != 4)
                goto exit;

            _ParsePage0FAddr14(data[0]);
            _ParsePage0FAddr15(data[1]);
            _ParsePage0FAddr16(data[2]);
            _ParsePage0FAddr17(data[3]);


            /*
            _ParseAddr14(data[0], rPage0F);
            _ParseAddr15(data[1], rPage0F);
            _ParseAddr16(data[2], rPage0F);
            _ParseAddr17(data[3], rPage0F);
            */

            rv = i2cReadCB(devAddr, 0x1B, 1, data);
            if (rv != 1)
                goto exit;

            _ParsePage0FAddr1B(data[0]); 
            


            /*
            _ParseAddr1B(data[0], rPage0F);
            */
            #endregion

            #region Reading Page10

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cReadCB(devAddr, 0x00, 7, data);
            if (rv != 7)
                goto exit;

            _ParsePage10Addr00(data[0]);
            _ParsePage10Addr01(data[1]);
            _ParsePage10Addr02(data[2]);
            _ParsePage10Addr03(data[3]);
            _ParsePage10Addr04(data[4]);
            _ParsePage10Addr05(data[5]);
            _ParsePage10Addr06(data[6]);

            /*
            _ParseAddr00(data[0], rPage10);
            _ParseAddr01(data[1], rPage10);
            _ParseAddr02(data[2], rPage10);
            _ParseAddr03(data[3], rPage10);
            _ParseAddr04(data[4], rPage10);
            _ParseAddr05(data[5], rPage10);
            _ParseAddr06(data[6], rPage10);
            */

            rv = i2cReadCB(devAddr, 0x08, 2, data);
            if (rv != 2)
                goto exit;

            _ParsePage10Addr08(data[0]);
            _ParsePage10Addr09(data[1]);

            /*
            _ParseAddr08(data[0], rPage10);
            _ParseAddr09(data[1], rPage10);
            */

            rv = i2cReadCB(devAddr, 0x0C, 3, data);
            if (rv != 3)
                goto exit;

            _ParsePage10Addr0C(data[0]);
            _ParsePage10Addr0D(data[1]);
            _ParsePage10Addr0E(data[2]);

            /*
            _ParseAddr0C(data[0], rPage10);
            _ParseAddr0D(data[1], rPage10);
            _ParseAddr0E(data[2], rPage10);
            */

            rv = i2cReadCB(devAddr, 0x10, 3, data);
            if (rv != 3)
                goto exit;

            _ParsePage10Addr10(data[0]);
            _ParsePage10Addr11(data[1]);
            _ParsePage10Addr12(data[2]);

            /*
            _ParseAddr10(data[0], rPage10);
            _ParseAddr11(data[1], rPage10);
            _ParseAddr12(data[2], rPage10);
            */


            rv = i2cReadCB(devAddr, 0x14, 5, data);
            if (rv != 5)
                goto exit;

            _ParsePage10Addr14(data[0]);
            _ParsePage10Addr15(data[1]);
            _ParsePage10Addr16(data[2]);
            _ParsePage10Addr17(data[3]);
            _ParsePage10Addr18(data[4]);


            /*
            _ParseAddr14(data[0], rPage10);
            _ParseAddr15(data[1], rPage10);
            _ParseAddr16(data[2], rPage10);
            _ParseAddr17(data[3], rPage10);
            _ParseAddr18(data[4], rPage10);

            */

            #endregion

            #region Reading Page11

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cReadCB(devAddr, 0x00, 7, data);
            if (rv != 7)
                goto exit;

            _ParsePage11Addr00(data[0]);
            _ParsePage11Addr01(data[1]);
            _ParsePage11Addr02(data[2]);
            _ParsePage11Addr03(data[3]);
            _ParsePage11Addr04(data[4]);
            _ParsePage11Addr05(data[5]);
            _ParsePage11Addr06(data[6]);


            /*
            _ParseAddr00(data[0], rPage11);
            _ParseAddr01(data[1], rPage11);
            _ParseAddr02(data[2], rPage11);
            _ParseAddr03(data[3], rPage11);
            _ParseAddr04(data[4], rPage11);
            _ParseAddr05(data[5], rPage11);
            _ParseAddr06(data[6], rPage11);
            */


            rv = i2cReadCB(devAddr, 0x08, 2, data);
            if (rv != 2)
                goto exit;

            _ParsePage11Addr08(data[0]);
            _ParsePage11Addr09(data[1]);

            /*
            _ParseAddr08(data[0], rPage11);
            _ParseAddr09(data[1], rPage11);
            */

            rv = i2cReadCB(devAddr, 0x0C, 3, data);
            if (rv != 3)
                goto exit;

            _ParsePage11Addr0C(data[0]);
            _ParsePage11Addr0D(data[1]);
            _ParsePage11Addr0E(data[2]);

            /*
            _ParseAddr0C(data[0], rPage11);
            _ParseAddr0D(data[1], rPage11);
            _ParseAddr0E(data[2], rPage11);
            */

            rv = i2cReadCB(devAddr, 0x10, 3, data);
            if (rv != 3)
                goto exit;

            _ParsePage11Addr10(data[0]);
            _ParsePage11Addr11(data[1]);
            _ParsePage11Addr12(data[2]);


            /*
            _ParseAddr10(data[0], rPage11);
            _ParseAddr11(data[1], rPage11);
            _ParseAddr12(data[2], rPage11);
            */

            rv = i2cReadCB(devAddr, 0x14, 5, data);
            if (rv != 5)
                goto exit;

            _ParsePage11Addr14(data[0]);
            _ParsePage11Addr15(data[1]);
            _ParsePage11Addr16(data[2]);
            _ParsePage11Addr17(data[3]);
            _ParsePage11Addr18(data[4]);
            

            /*
            _ParseAddr14(data[0], rPage11);
            _ParseAddr15(data[1], rPage11);
            _ParseAddr16(data[2], rPage11);
            _ParseAddr17(data[3], rPage11);
            _ParseAddr18(data[4], rPage11);
            */

            #endregion

            #region Reading Page12

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cReadCB(devAddr, 0x00, 7, data);
            if (rv != 7)
                goto exit;

            _ParsePage12Addr00(data[0]);
            _ParsePage12Addr01(data[1]);
            _ParsePage12Addr02(data[2]);
            _ParsePage12Addr03(data[3]);
            _ParsePage12Addr04(data[4]);
            _ParsePage12Addr05(data[5]);
            _ParsePage12Addr06(data[6]);

            /*
            _ParseAddr00(data[0], rPage12);
            _ParseAddr01(data[1], rPage12);
            _ParseAddr02(data[2], rPage12);
            _ParseAddr03(data[3], rPage12);
            _ParseAddr04(data[4], rPage12);
            _ParseAddr05(data[5], rPage12);
            _ParseAddr06(data[6], rPage12);
            */


            rv = i2cReadCB(devAddr, 0x08, 2, data);
            if (rv != 2)
                goto exit;

            _ParsePage12Addr08(data[0]);
            _ParsePage12Addr09(data[1]);


            /*
            _ParseAddr08(data[0], rPage12);
            _ParseAddr09(data[1], rPage12);
            */

            rv = i2cReadCB(devAddr, 0x0C, 3, data);
            if (rv != 3)
                goto exit;

            _ParsePage12Addr0C(data[0]);
            _ParsePage12Addr0D(data[1]);
            _ParsePage12Addr0E(data[2]);

            /*
            _ParseAddr0C(data[0], rPage12);
            _ParseAddr0D(data[1], rPage12);
            _ParseAddr0E(data[2], rPage12);
            */

            rv = i2cReadCB(devAddr, 0x10, 3, data);
            if (rv != 3)
                goto exit;

            _ParsePage12Addr10(data[0]);
            _ParsePage12Addr11(data[1]);
            _ParsePage12Addr12(data[2]);


            /*
            _ParseAddr10(data[0], rPage12);
            _ParseAddr11(data[1], rPage12);
            _ParseAddr12(data[2], rPage12);
            */

            rv = i2cReadCB(devAddr, 0x14, 5, data);
            if (rv != 5)
                goto exit;

            _ParsePage12Addr14(data[0]);
            _ParsePage12Addr15(data[1]);
            _ParsePage12Addr16(data[2]);
            _ParsePage12Addr17(data[3]);
            _ParsePage12Addr18(data[4]);

            /*
            _ParseAddr14(data[0], rPage12);
            _ParseAddr15(data[1], rPage12);
            _ParseAddr16(data[2], rPage12);
            _ParseAddr17(data[3], rPage12);
            _ParseAddr18(data[4], rPage12);
            */

            #endregion

            #region Reading Page13

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cReadCB(devAddr, 0x00, 7, data);
            if (rv != 7)
                goto exit;

            _ParsePage13Addr00(data[0]);
            _ParsePage13Addr01(data[1]);
            _ParsePage13Addr02(data[2]);
            _ParsePage13Addr03(data[3]);
            _ParsePage13Addr04(data[4]);
            _ParsePage13Addr05(data[5]);
            _ParsePage13Addr06(data[6]);

            /*
            _ParseAddr00(data[0], rPage13);
            _ParseAddr01(data[1], rPage13);
            _ParseAddr02(data[2], rPage13);
            _ParseAddr03(data[3], rPage13);
            _ParseAddr04(data[4], rPage13);
            _ParseAddr05(data[5], rPage13);
            _ParseAddr06(data[6], rPage13);
            */


            rv = i2cReadCB(devAddr, 0x08, 2, data);
            if (rv != 2)
                goto exit;

            _ParsePage13Addr08(data[0]);
            _ParsePage13Addr09(data[1]);

            /*
            _ParseAddr08(data[0], rPage13);
            _ParseAddr09(data[1], rPage13);
            */

            rv = i2cReadCB(devAddr, 0x0C, 3, data);
            if (rv != 3)
                goto exit;

            _ParsePage13Addr0C(data[0]);
            _ParsePage13Addr0D(data[1]);
            _ParsePage13Addr0E(data[2]);

            /*
            _ParseAddr0C(data[0], rPage13);
            _ParseAddr0D(data[1], rPage13);
            _ParseAddr0E(data[2], rPage13);
            */

            rv = i2cReadCB(devAddr, 0x10, 3, data);
            if (rv != 3)
                goto exit;

            _ParsePage13Addr10(data[0]);
            _ParsePage13Addr11(data[1]);
            _ParsePage13Addr12(data[2]);

            /*
            _ParseAddr10(data[0], rPage13);
            _ParseAddr11(data[1], rPage13);
            _ParseAddr12(data[2], rPage13);
            */

            rv = i2cReadCB(devAddr, 0x14, 5, data);
            if (rv != 5)
                goto exit;

            _ParsePage13Addr14(data[0]);
            _ParsePage13Addr15(data[1]);
            _ParsePage13Addr16(data[2]);
            _ParsePage13Addr17(data[3]);
            _ParsePage13Addr18(data[4]);



            /*
            _ParseAddr14(data[0], rPage13);
            _ParseAddr15(data[1], rPage13);
            _ParseAddr16(data[2], rPage13);
            _ParseAddr17(data[3], rPage13);
            _ParseAddr18(data[4], rPage13);
            */


            #endregion

            #region Reading Page30
            SetPage(Page30);
            Thread.Sleep(100);

            rv = i2cReadCB(devAddr, 0x00, 2, data);
            if (rv != 2)
                goto exit;

            _ParsePage30Addr00(data[0]);
            _ParsePage30Addr01(data[1]);

            /*
            _ParseAddr00(data[0], rPage30);
            _ParseAddr01(data[1], rPage30);
            */

            rv = i2cReadCB(devAddr, 0x05, 2, data);
            if (rv != 2)
                goto exit;

            _ParsePage30Addr05_06(data[0], data[1]);

            /*
            _ParseAddr05(data[0], rPage30);
            _ParseAddr06(data[1], rPage30);
            */

            #endregion

            #region Reading PageAA

            SetPage(PageAA);
            Thread.Sleep(100);

            rv = i2cReadCB(devAddr, 0xA0, 52, data);
            if (rv != 52)
                goto exit;

            _ParsePageAAAddrA0(data[0]);
            _ParsePageAAAddrA1(data[1]);
            _ParsePageAAAddrA2(data[2]);
            _ParsePageAAAddrA3(data[3]);
            _ParsePageAAAddrA4(data[4]);
            _ParsePageAAAddrA5(data[5]);
            _ParsePageAAAddrA6(data[6]);
            _ParsePageAAAddrA7(data[7]);
            _ParsePageAAAddrA8(data[8]);
            _ParsePageAAAddrA9(data[9]);
            _ParsePageAAAddrAA(data[10]);
            _ParsePageAAAddrAB(data[11]);
            _ParsePageAAAddrAC(data[12]);
            _ParsePageAAAddrAD(data[13]);
            _ParsePageAAAddrAE(data[14]);
            _ParsePageAAAddrAF(data[15]);
            _ParsePageAAAddrB0(data[16]);
            _ParsePageAAAddrB1(data[17]);
            _ParsePageAAAddrB2(data[18]);
            _ParsePageAAAddrB3(data[19]);
            _ParsePageAAAddrB4(data[20]);
            _ParsePageAAAddrB5(data[21]);
            _ParsePageAAAddrB6(data[22]);
            _ParsePageAAAddrB7(data[23]);
            _ParsePageAAAddrB8(data[24]);
            _ParsePageAAAddrB9(data[25]);
            _ParsePageAAAddrBA(data[26]);
            _ParsePageAAAddrBB(data[27]);
            _ParsePageAAAddrBC(data[28]);
            _ParsePageAAAddrBD(data[29]);
            _ParsePageAAAddrBE(data[30]);
            _ParsePageAAAddrBF(data[31]);
            _ParsePageAAAddrC0(data[32]);
            _ParsePageAAAddrC1(data[33]);
            _ParsePageAAAddrC2(data[34]);
            _ParsePageAAAddrC3(data[35]);
            _ParsePageAAAddrC4(data[36]);
            _ParsePageAAAddrC5(data[37]);
            _ParsePageAAAddrC6(data[38]);
            _ParsePageAAAddrC7(data[39]);
            _ParsePageAAAddrC8(data[40]);
            _ParsePageAAAddrC9(data[41]);
            _ParsePageAAAddrCA(data[42]);
            _ParsePageAAAddrCB(data[43]);
            _ParsePageAAAddrCC(data[44]);
            _ParsePageAAAddrCD(data[45]);
            _ParsePageAAAddrCE(data[46]);
            _ParsePageAAAddrCF(data[47]);
            _ParsePageAAAddrD0(data[48]);
            _ParsePageAAAddrD1(data[49]);
            _ParsePageAAAddrD2(data[50]);
            _ParsePageAAAddrD3(data[51]);


        #endregion

        exit:
            reading = false;
            btnReadAll.Enabled = true;
        }

        //20250103 try to check ADC value , convert to Ibias current (Real-time)
        private void btnIbiasADCCheck_Click(object sender, EventArgs e)
        {
            /*
             * 1. Set 0x30:0x00[2:0] = 110b, select ADC IBIAS measurement.
             * 2. Set 0x0F:0x1B = 08h, enable IBIAS measurement.
             * 3. Set 0x1N:0x18:[0] = 1b, select channel. (only one channel at a time)
             * 4. Set 0x30:0x00:[3] = 1b, freeze ADC MSB and LSB value prior to reading.
             * 5. Read 0x30:0x05:[7:0] & 0x30:0x06:[3:0]. ADC output code bits [11:4] and [3:0]
             * 6. Calculate Ibias(mA) = (ADC code - 138) * 17.48E-3
             * 7. Set 0x30:0x00:[3] = 0b, unfreeze ADC read registers to allow for update of values
             *    (remember to freeze again prior to reading again.)
             * 8. Set 0x1N:0x18:[0] = 0b, 0x0F:0x1B = 00h. Set registers to default values.
             * 
             *  Note. It is recommended to take multiple readings of IBIAS and use the average to improve accuracy.
            */
                        
            if (reading == true)
                return;
            reading = true;
            btnIbiasADCCheck.Enabled = false;
            if (i2cReadCB == null)
                goto exit;

            _Write_ADC_Process_L0test();
            _Write_ADC_Process_L1test();
            _Write_ADC_Process_L2test();
            _Write_ADC_Process_L3test();

        exit:
            reading = false;
            btnIbiasADCCheck.Enabled = true;

        }      

        private void btnDeviceReset_Click(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page00);

            if (_WritePage00Addr02() < 0)
                return;

        }

        private void btnStoreIntoFlash_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1] { 0xA0 };
            int rv;

            btnStoreIntoFlash.Enabled = false;
            reading = true;
            rv = i2cWriteCB(devAddr, 0xFA, 1, data);
            Thread.Sleep(1000);
            btnStoreIntoFlash.Enabled = true;
            reading = false;
        }

        #endregion

        #region function for ADC check
        private int _Write_ADC_Process_L0test()
        {
            byte[] data = new byte[2];
            int rv;

            //1.Set 0x30:0x00[2:0] = 110b, select ADC IBIAS measurement.
            SetPage(Page30);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x00, 1, data);
            if (rv != 1)
                goto exit;

            data[0] &= 0xFE;
            data[0] |= 0x06;
            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            //2. Set 0x0F:0x1B = 08h, enable IBIAS measurement.
            SetPage(Page0F);
            Thread.Sleep(50);

            data[0] = 0x08;
            rv = i2cWriteCB(devAddr, 0x1B, 1, data);
            if (rv < 0)
                return -1;

            //3. Set 0x1N:0x18:[0] = 1b, select channel. (only one channel at a time)
            SetPage(Page10);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x18, 1, data);
            if (rv != 1)
                goto exit;

            data[0] |= 0x01;
            rv = i2cWriteCB(devAddr, 0x18, 1, data);
            if (rv < 0)
                return -1;

            //4. Set 0x30:0x00:[3] = 1b, freeze ADC MSB and LSB value prior to reading.
            SetPage(Page30);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x00, 1, data);
            if (rv != 1)
                goto exit;

            data[0] |= 0x08;
            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            //5. Read 0x30:0x05:[7:0] & 0x30:0x06:[3:0]. ADC output code bits [11:4] and [3:0]
            rv = i2cReadCB(devAddr, 0x05, 2, data);
            if (rv != 2)
                goto exit;

            _ParsePage30Addr05_06(data[0], data[1]);

            //6.Calculate Ibias(mA) = (ADC code - 138) *17.48E-3
            int ADC_code;
            ADC_code = (data[0] << 4) | data[1];
            double Ibias = (ADC_code - 138) * 17.48E-3;
            txtL0IbiasMonitor.Text = Ibias.ToString() + " mA";

            //7.Set 0x30:0x00:[3] = 0b, unfreeze ADC read registers to allow for update of values
            // (remember to freeze again prior to reading again.)
            rv = i2cReadCB(devAddr, 0x00, 1, data);
            if (rv != 1)
                goto exit;

            data[0] &= 0xF7;
            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            //8.Set 0x1N: 0x18:[0] = 0b, 0x0F:0x1B = 00h.Set registers to default values.
            SetPage(Page10);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x18, 1, data);
            if (rv != 1)
                goto exit;

            data[0] &= 0xFE;
            rv = i2cWriteCB(devAddr, 0x18, 1, data);
            if (rv < 0)
                return -1;

            //0x0F:0x1B = 00h.
            SetPage(Page0F);
            Thread.Sleep(50);

            data[0] = 0x00;
            rv = i2cWriteCB(devAddr, 0x1B, 1, data);
            if (rv < 0)
                return -1;


            exit:
            reading = false;
            btnIbiasADCCheck.Enabled = true;

            return 0;
        }
        private int _Write_ADC_Process_L1test()
        {
            byte[] data = new byte[2];
            int rv;

            //1.Set 0x30:0x00[2:0] = 110b, select ADC IBIAS measurement.
            SetPage(Page30);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x00, 1, data);
            if (rv != 1)
                goto exit;

            data[0] &= 0xFE;
            data[0] |= 0x06;
            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            //2. Set 0x0F:0x1B = 08h, enable IBIAS measurement.
            SetPage(Page0F);
            Thread.Sleep(50);

            data[0] = 0x08;
            rv = i2cWriteCB(devAddr, 0x1B, 1, data);
            if (rv < 0)
                return -1;

            //3. Set 0x1N:0x18:[0] = 1b, select channel. (only one channel at a time)
            SetPage(Page11);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x18, 1, data);
            if (rv != 1)
                goto exit;

            data[0] |= 0x01;
            rv = i2cWriteCB(devAddr, 0x18, 1, data);
            if (rv < 0)
                return -1;

            //4. Set 0x30:0x00:[3] = 1b, freeze ADC MSB and LSB value prior to reading.
            SetPage(Page30);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x00, 1, data);
            if (rv != 1)
                goto exit;

            data[0] |= 0x08;
            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            //5. Read 0x30:0x05:[7:0] & 0x30:0x06:[3:0]. ADC output code bits [11:4] and [3:0]
            rv = i2cReadCB(devAddr, 0x05, 2, data);
            if (rv != 2)
                goto exit;

            _ParsePage30Addr05_06(data[0], data[1]);

            //6.Calculate Ibias(mA) = (ADC code - 138) *17.48E-3
            int ADC_code;
            ADC_code = (data[0] << 4) | data[1];
            double Ibias = (ADC_code - 138) * 17.48E-3;
            txtL1IbiasMonitor.Text = Ibias.ToString() + " mA";

            //7.Set 0x30:0x00:[3] = 0b, unfreeze ADC read registers to allow for update of values
            // (remember to freeze again prior to reading again.)
            rv = i2cReadCB(devAddr, 0x00, 1, data);
            if (rv != 1)
                goto exit;

            data[0] &= 0xF7;
            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            //8.Set 0x1N: 0x18:[0] = 0b, 0x0F:0x1B = 00h.Set registers to default values.
            SetPage(Page11);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x18, 1, data);
            if (rv != 1)
                goto exit;

            data[0] &= 0xFE;
            rv = i2cWriteCB(devAddr, 0x18, 1, data);
            if (rv < 0)
                return -1;

            //0x0F:0x1B = 00h.
            SetPage(Page0F);
            Thread.Sleep(50);

            data[0] = 0x00;
            rv = i2cWriteCB(devAddr, 0x1B, 1, data);
            if (rv < 0)
                return -1;


            exit:
            reading = false;
            btnIbiasADCCheck.Enabled = true;

            return 0;
        }
        private int _Write_ADC_Process_L2test()
        {
            byte[] data = new byte[2];
            int rv;

            //1.Set 0x30:0x00[2:0] = 110b, select ADC IBIAS measurement.
            SetPage(Page30);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x00, 1, data);
            if (rv != 1)
                goto exit;

            data[0] &= 0xFE;
            data[0] |= 0x06;
            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            //2. Set 0x0F:0x1B = 08h, enable IBIAS measurement.
            SetPage(Page0F);
            Thread.Sleep(50);

            data[0] = 0x08;
            rv = i2cWriteCB(devAddr, 0x1B, 1, data);
            if (rv < 0)
                return -1;

            //3. Set 0x1N:0x18:[0] = 1b, select channel. (only one channel at a time)
            SetPage(Page12);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x18, 1, data);
            if (rv != 1)
                goto exit;

            data[0] |= 0x01;
            rv = i2cWriteCB(devAddr, 0x18, 1, data);
            if (rv < 0)
                return -1;

            //4. Set 0x30:0x00:[3] = 1b, freeze ADC MSB and LSB value prior to reading.
            SetPage(Page30);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x00, 1, data);
            if (rv != 1)
                goto exit;

            data[0] |= 0x08;
            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            //5. Read 0x30:0x05:[7:0] & 0x30:0x06:[3:0]. ADC output code bits [11:4] and [3:0]
            rv = i2cReadCB(devAddr, 0x05, 2, data);
            if (rv != 2)
                goto exit;

            _ParsePage30Addr05_06(data[0], data[1]);

            //6.Calculate Ibias(mA) = (ADC code - 138) *17.48E-3
            int ADC_code;
            ADC_code = (data[0] << 4) | data[1];
            double Ibias = (ADC_code - 138) * 17.48E-3;
            txtL2IbiasMonitor.Text = Ibias.ToString() + " mA";

            //7.Set 0x30:0x00:[3] = 0b, unfreeze ADC read registers to allow for update of values
            // (remember to freeze again prior to reading again.)
            rv = i2cReadCB(devAddr, 0x00, 1, data);
            if (rv != 1)
                goto exit;

            data[0] &= 0xF7;
            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            //8.Set 0x1N: 0x18:[0] = 0b, 0x0F:0x1B = 00h.Set registers to default values.
            SetPage(Page12);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x18, 1, data);
            if (rv != 1)
                goto exit;

            data[0] &= 0xFE;
            rv = i2cWriteCB(devAddr, 0x18, 1, data);
            if (rv < 0)
                return -1;

            //0x0F:0x1B = 00h.
            SetPage(Page0F);
            Thread.Sleep(50);

            data[0] = 0x00;
            rv = i2cWriteCB(devAddr, 0x1B, 1, data);
            if (rv < 0)
                return -1;


            exit:
            reading = false;
            btnIbiasADCCheck.Enabled = true;

            return 0;
        }
        private int _Write_ADC_Process_L3test()
        {
            byte[] data = new byte[2];
            int rv;

            //1.Set 0x30:0x00[2:0] = 110b, select ADC IBIAS measurement.
            SetPage(Page30);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x00, 1, data);
            if (rv != 1)
                goto exit;

            data[0] &= 0xFE;
            data[0] |= 0x06;
            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            //2. Set 0x0F:0x1B = 08h, enable IBIAS measurement.
            SetPage(Page0F);
            Thread.Sleep(50);

            data[0] = 0x08;
            rv = i2cWriteCB(devAddr, 0x1B, 1, data);
            if (rv < 0)
                return -1;

            //3. Set 0x1N:0x18:[0] = 1b, select channel. (only one channel at a time)
            SetPage(Page13);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x18, 1, data);
            if (rv != 1)
                goto exit;

            data[0] |= 0x01;
            rv = i2cWriteCB(devAddr, 0x18, 1, data);
            if (rv < 0)
                return -1;

            //4. Set 0x30:0x00:[3] = 1b, freeze ADC MSB and LSB value prior to reading.
            SetPage(Page30);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x00, 1, data);
            if (rv != 1)
                goto exit;

            data[0] |= 0x08;
            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            //5. Read 0x30:0x05:[7:0] & 0x30:0x06:[3:0]. ADC output code bits [11:4] and [3:0]
            rv = i2cReadCB(devAddr, 0x05, 2, data);
            if (rv != 2)
                goto exit;

            _ParsePage30Addr05_06(data[0], data[1]);

            //6.Calculate Ibias(mA) = (ADC code - 138) *17.48E-3
            int ADC_code;
            ADC_code = (data[0] << 4) | data[1];
            double Ibias = (ADC_code - 138) * 17.48E-3;
            txtL3IbiasMonitor.Text = Ibias.ToString() + " mA";

            //7.Set 0x30:0x00:[3] = 0b, unfreeze ADC read registers to allow for update of values
            // (remember to freeze again prior to reading again.)
            rv = i2cReadCB(devAddr, 0x00, 1, data);
            if (rv != 1)
                goto exit;

            data[0] &= 0xF7;
            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            //8.Set 0x1N: 0x18:[0] = 0b, 0x0F:0x1B = 00h.Set registers to default values.
            SetPage(Page13);
            Thread.Sleep(50);
            rv = i2cReadCB(devAddr, 0x18, 1, data);
            if (rv != 1)
                goto exit;

            data[0] &= 0xFE;
            rv = i2cWriteCB(devAddr, 0x18, 1, data);
            if (rv < 0)
                return -1;

            //0x0F:0x1B = 00h.
            SetPage(Page0F);
            Thread.Sleep(50);

            data[0] = 0x00;
            rv = i2cWriteCB(devAddr, 0x1B, 1, data);
            if (rv < 0)
                return -1;


            exit:
            reading = false;
            btnIbiasADCCheck.Enabled = true;

            return 0;
        }


        #endregion

        #region ParsePage00AddrXX

        // Page 00
        private void _ParsePage00Addr00(byte data)
        {
            txtChipID.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage00Addr01(byte data)
        {
            txtRevID.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage00Addr03(byte data)
        {
            if ((data & 0x80) == 0)
                cboIntrptPadPolarity.SelectedIndex = 0;
            else
                cboIntrptPadPolarity.SelectedIndex = 1;

            if ((data & 0x40) == 0)
                cboIntrptoen.SelectedIndex = 0;
            else
                cboIntrptoen.SelectedIndex = 1;

            if ((data & 0x10) == 0)
                cboI2cAddressMode.SelectedIndex = 0;
            else
                cboI2cAddressMode.SelectedIndex = 1;
        }

        #endregion

        #region ParsePage0FAddrXX
        // Page 0F
        private void _ParsePage0FAddr00(byte data)
        {
            if ((data & 0x01) == 0)
                chkPowerDownAll.Checked =
                chkPowerDownL0.Checked =
                chkPowerDownL1.Checked =
                chkPowerDownL2.Checked =
                chkPowerDownL3.Checked = false;
            else
                chkPowerDownAll.Checked =
                chkPowerDownL0.Checked =
                chkPowerDownL1.Checked =
                chkPowerDownL2.Checked =
                chkPowerDownL3.Checked = true;
        }

        private void _ParsePage0FAddr01(byte data)
        {
            foreach (ComboboxItem item in cboVgaTailStg1All.Items)
            {
                if (item.Value == ((data & 0xC0) >> 6))
                {
                    cboVgaTailStg1All.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVgaGainStg1All.Items)
            {
                if (item.Value == (data & 0x3F))
                {
                    cboVgaGainStg1All.SelectedItem = item;
                }
            }

        }

        private void _ParsePage0FAddr02(byte data)
        {
            foreach (ComboboxItem item in cboVga1GainChangePolarityAll.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga1GainChangePolarityAll.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga1GainChangeAll.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga1GainChangeAll.SelectedItem = item;
                }
            }
        }

        private void _ParsePage0FAddr03(byte data)
        {
            foreach (ComboboxItem item in cboVga1BandwidthChangePolarityAll.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga1BandwidthChangePolarityAll.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga1BandwidthChangeAll.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga1BandwidthChangeAll.SelectedItem = item;
                }
            }
        }

        private void _ParsePage0FAddr04(byte data)
        {
            foreach (ComboboxItem item in cboVgaTailStg2All.Items)
            {
                if (item.Value == ((data & 0xC0) >> 6))
                {
                    cboVgaTailStg2All.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVgaGainStg2All.Items)
            {
                if (item.Value == (data & 0x3F))
                {
                    cboVgaGainStg2All.SelectedItem = item;
                }
            }
        }

        private void _ParsePage0FAddr05(byte data)
        {
            foreach (ComboboxItem item in cboVga2GainChangePolarityAll.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga2GainChangePolarityAll.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga2GainChangeAll.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga2GainChangeAll.SelectedItem = item;
                }
            }
        }

        private void _ParsePage0FAddr06(byte data)
        {
            foreach (ComboboxItem item in cboVga2BandwidthChangePolarityAll.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga2BandwidthChangePolarityAll.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga2BandwidthChangeAll.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga2BandwidthChangeAll.SelectedItem = item;
                }
            }
        }

        private void _ParsePage0FAddr08(byte data)
        {
            foreach (ComboboxItem item in cboVgaVcmAdjAll.Items)
            {
                if (item.Value == (data & (0xC0)) >> 6)
                {
                    cboVgaVcmAdjAll.SelectedItem = item;
                }
            }
        }

        private void _ParsePage0FAddr09(byte data)
        {
            foreach (ComboboxItem item in cboLosthrshystAll.Items)
            {
                if (item.Value == (data & 0x20) >> 5)
                {
                    cboLosthrshystAll.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboLosthrsAll.Items)
            {
                if (item.Value == (data & 0x18) >> 3)
                {
                    cboLosthrsAll.SelectedItem = item;
                }
            }
            // LOS Alarm checkbox
            if ((data & 0x04) == 0)
            {
                chkLosAlarmAll.Checked =
                chkLosAlarmL0.Checked =
                chkLosAlarmL1.Checked =
                chkLosAlarmL2.Checked =
                chkLosAlarmL3.Checked = false;
            }
            else
            {
                chkLosAlarmAll.Checked =
                chkLosAlarmL0.Checked =
                chkLosAlarmL1.Checked =
                chkLosAlarmL2.Checked =
                chkLosAlarmL3.Checked = true;
            }

            // LOS Mask checkbox
            if ((data & 0x02) == 0)
            {
                chkLosMaskAll.Checked =
                chkLosMaskL0.Checked =
                chkLosMaskL1.Checked =
                chkLosMaskL2.Checked =
                chkLosMaskL3.Checked = false;
            }
            else
            {
                chkLosMaskAll.Checked =
                chkLosMaskL0.Checked =
                chkLosMaskL1.Checked =
                chkLosMaskL2.Checked =
                chkLosMaskL3.Checked = true;
            }

            // LOS Detector checkbox
            if ((data & 0x01) == 0)
            {
                chkPdLosAll.Checked =
                chkPdLosL0.Checked =
                chkPdLosL1.Checked =
                chkPdLosL2.Checked =
                chkPdLosL3.Checked = false;
            }
            else
            {
                chkPdLosAll.Checked =
                chkPdLosL0.Checked =
                chkPdLosL1.Checked =
                chkPdLosL2.Checked =
                chkPdLosL3.Checked = true;

            }
        }

        private void _ParsePage0FAddr0D(byte data)
        {
            if ((data & 0x02) == 0)
            {
                chkTxDisableAutoMuteAll.Checked =
                chkTxDisableAutoMuteL0.Checked =
                chkTxDisableAutoMuteL1.Checked =
                chkTxDisableAutoMuteL2.Checked =
                chkTxDisableAutoMuteL3.Checked = false;
            }
            else
            {
                chkTxDisableAutoMuteAll.Checked =
                chkTxDisableAutoMuteL0.Checked =
                chkTxDisableAutoMuteL1.Checked =
                chkTxDisableAutoMuteL2.Checked =
                chkTxDisableAutoMuteL3.Checked = true;
            }

            if ((data & 0x01) == 0)
            {
                chkTxDisableAll.Checked =
                chkTxDisableL0.Checked =
                chkTxDisableL1.Checked =
                chkTxDisableL2.Checked =
                chkTxDisableL3.Checked = false;
            }
            else
            {
                chkTxDisableAll.Checked =
                chkTxDisableL0.Checked =
                chkTxDisableL1.Checked =
                chkTxDisableL2.Checked =
                chkTxDisableL3.Checked = true;
            }
        }

        private void _ParsePage0FAddr0E(byte data)
        {
            // checkbox Ignore Tx Fault
            if ((data & 0x20) == 0)
            {
                chkTxIgnoreFaultAll.Checked =
                chkTxIgnoreFaultL0.Checked =
                chkTxIgnoreFaultL1.Checked =
                chkTxIgnoreFaultL2.Checked =
                chkTxIgnoreFaultL3.Checked = false;
            }
            else
            {
                chkTxIgnoreFaultAll.Checked =
                chkTxIgnoreFaultL0.Checked =
                chkTxIgnoreFaultL1.Checked =
                chkTxIgnoreFaultL2.Checked =
                chkTxIgnoreFaultL3.Checked = true;
            }

            // checkbox Tx Fault Alarm
            if ((data & 0x10) == 0)
            {
                chkTxFaultAlarmAll.Checked =
                chkTxFaultAlarmL0.Checked =
                chkTxFaultAlarmL1.Checked =
                chkTxFaultAlarmL2.Checked =
                chkTxFaultAlarmL3.Checked = false;
            }
            else
            {
                chkTxFaultAlarmAll.Checked =
                chkTxFaultAlarmL0.Checked =
                chkTxFaultAlarmL1.Checked =
                chkTxFaultAlarmL2.Checked =
                chkTxFaultAlarmL3.Checked = true;

            }

            foreach (ComboboxItem item in cboTxFaultStateAll.Items)
            {
                if (item.Value == (data & 0x0C) >> 2)
                {
                    cboTxFaultStateAll.SelectedItem = item;
                }
            }

            if ((data & 0x02) == 0)
            {
                chkTxFaultAll.Checked =
                chkTxFaultL0.Checked =
                chkTxFaultL1.Checked =
                chkTxFaultL2.Checked =
                chkTxFaultL3.Checked = false;
            }
            else
            {
                chkTxFaultAll.Checked =
                chkTxFaultL0.Checked =
                chkTxFaultL1.Checked =
                chkTxFaultL2.Checked =
                chkTxFaultL3.Checked = true;
            }

            if ((data & 0x01) == 0)
            {
                chkTxFaultMaskAll.Checked =
                chkTxFaultMaskL0.Checked =
                chkTxFaultMaskL1.Checked =
                chkTxFaultMaskL2.Checked =
                chkTxFaultMaskL3.Checked = false;
            }
            else
            {
                chkTxFaultMaskAll.Checked =
                chkTxFaultMaskL0.Checked =
                chkTxFaultMaskL1.Checked =
                chkTxFaultMaskL2.Checked =
                chkTxFaultMaskL3.Checked = true;
            }
        }

        private void _ParsePage0FAddr0F(byte data)
        {
            if ((data & 0x80) == 0)
            {
                chkIgnoreTxDisablePinAll.Checked = false;
            }
            else
            {
                chkIgnoreTxDisablePinAll.Checked = true;
            }

            if ((data & 0x40) == 0)
            {
                chkTxAlarmClearInterruptAll.Checked = false;
            }
            else
            {
                chkTxAlarmClearInterruptAll.Checked = true;
            }

            if ((data & 0x20) == 0)
            {
                chkTxAlarmClearPinAll.Checked = false;
            }
            else
            {
                chkTxAlarmClearPinAll.Checked = true;
            }

            if ((data & 0x10) == 0)
            {
                chkTxAlarmClearRegisterAll.Checked = false;
            }
            else
            {
                chkTxAlarmClearRegisterAll.Checked = true;
            }
        }

        private void _ParsePage0FAddr10(byte data)
        {
            foreach (ComboboxItem item in cboRVcselAll.Items)
            {
                if (item.Value == (data & 0x78) >> 3)
                {
                    cboRVcselAll.SelectedItem = item;
                }
            }
        }

        private void _ParsePage0FAddr11(byte data)
        {
            foreach (ComboboxItem item in cboIBiasAll.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboIBiasAll.SelectedItem = item;
                }
            }
        }

        private void _ParsePage0FAddr12(byte data)
        {
            if ((data & 0x80) == 0)
            {
                chkTxBurnInEnableAll.Checked =
                chkTxBurnInEnableL0.Checked =
                chkTxBurnInEnableL1.Checked =
                chkTxBurnInEnableL2.Checked =
                chkTxBurnInEnableL3.Checked = false;
            }
            else
            {
                chkTxBurnInEnableAll.Checked =
                chkTxBurnInEnableL0.Checked =
                chkTxBurnInEnableL1.Checked =
                chkTxBurnInEnableL2.Checked =
                chkTxBurnInEnableL3.Checked = true;

            }

            foreach (ComboboxItem item in cboIBurnInAll.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboIBurnInAll.SelectedItem = item;
                }
            }
        }

        private void _ParsePage0FAddr14(byte data)
        {
            foreach (ComboboxItem item in cboTailCurrentAll.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboTailCurrentAll.SelectedItem = item;
                }
            }
        }

        private void _ParsePage0FAddr15(byte data)
        {
            foreach (ComboboxItem item in cboLowGainEnableAll.Items)
            {
                if (item.Value == (data & 0x10) >> 4)
                {
                    cboLowGainEnableAll.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboTxEqAll.Items)
            {
                if (item.Value == (data & 0x0F))
                {
                    cboTxEqAll.SelectedItem = item;
                }
            }
        }

        private void _ParsePage0FAddr16(byte data)
        {
            foreach (ComboboxItem item in cboTxEqGainRangeAll.Items)
            {
                if (item.Value == (data & 0x01))
                {
                    cboTxEqGainRangeAll.SelectedItem = item;
                }
            }
        }

        private void _ParsePage0FAddr17(byte data)
        {
            if ((data & 0x01) == 0)
            {
                chkDriverPkAvgSelAll.Checked = false;
            }
            else
            {
                chkDriverPkAvgSelAll.Checked = true;
            }
        }

        private void _ParsePage0FAddr1B(byte data)
        {
            if ((data & 0x08) == 0)
            {
                chkTxIBiasAvgAll.Checked = false;
            }
            else
            {
                chkTxIBiasAvgAll.Checked = true;
            }



            if ((data & 0x04) == 0)
            {
                chkTxIBiasRefAll.Checked = false;
            }
            else
            {
                chkTxIBiasRefAll.Checked = true;
            }

            if ((data & 0x01) == 0)
            {
                chkTxVcc3V3All.Checked = false;
            }
            else
            {
                chkTxVcc3V3All.Checked = true;
            }
        }





        #endregion

        #region ParsePage10AddrXX
        
        private void _ParsePage10Addr00(byte data)
        {
            if ((data & 0x01) == 0)
                chkPowerDownL0.Checked = false;
            else
                chkPowerDownL0.Checked = true;
        }

        private void _ParsePage10Addr01(byte data)
        {
            foreach (ComboboxItem item in cboVgaTailStg1L0.Items)
            {
                if (item.Value == ((data & 0xC0) >> 6))
                {
                    cboVgaTailStg1L0.SelectedItem = item;
                }
            }


            foreach (ComboboxItem item in cboVgaGainStg1L0.Items)
            {
                if (item.Value == (data & 0x3F))
                {
                    cboVgaGainStg1L0.SelectedItem = item;
                }
            }
        }

        private void _ParsePage10Addr02(byte data)
        {
            foreach (ComboboxItem item in cboVga1GainChangePolarityL0.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga1GainChangePolarityL0.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga1GainChangeL0.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga1GainChangeL0.SelectedItem = item;
                }
            }
        }

        private void _ParsePage10Addr03(byte data)
        {
            foreach (ComboboxItem item in cboVga1BandwidthChangePolarityL0.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga1BandwidthChangePolarityL0.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga1BandwidthChangeL0.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga1BandwidthChangeL0.SelectedItem = item;
                }
            }
        }

        private void _ParsePage10Addr04(byte data)
        {
            foreach (ComboboxItem item in cboVgaTailStg2L0.Items)
            {
                if (item.Value == ((data & 0xC0) >> 6))
                {
                    cboVgaTailStg2L0.SelectedItem = item;
                }
            }


            foreach (ComboboxItem item in cboVgaGainStg2L0.Items)
            {
                if (item.Value == (data & 0x3F))
                {
                    cboVgaGainStg2L0.SelectedItem = item;
                }
            }
        }

        private void _ParsePage10Addr05(byte data)
        {
            foreach (ComboboxItem item in cboVga2GainChangePolarityL0.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga2GainChangePolarityL0.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga2GainChangeL0.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga2GainChangeL0.SelectedItem = item;
                }
            }
        }

        private void _ParsePage10Addr06(byte data)
        {
            foreach (ComboboxItem item in cboVga2BandwidthChangePolarityL0.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga2BandwidthChangePolarityL0.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga2BandwidthChangeL0.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga2BandwidthChangeL0.SelectedItem = item;
                }
            }
        }

        private void _ParsePage10Addr08(byte data)
        {
            foreach (ComboboxItem item in cboVgaVcmAdjL0.Items)
            {
                if (item.Value == (data & (0xC0)) >> 6)
                {
                    cboVgaVcmAdjL0.SelectedItem = item;
                }
            }
        }

        private void _ParsePage10Addr09(byte data)
        {
            foreach (ComboboxItem item in cboLosthrshystL0.Items)
            {
                if (item.Value == (data & 0x20) >> 5)
                {
                    cboLosthrshystL0.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboLosthrsL0.Items)
            {
                if (item.Value == (data & 0x18) >> 3)
                {
                    cboLosthrsL0.SelectedItem = item;
                }
            }

            // LOS Alarm checkbox
            if ((data & 0x04) == 0)
            {
                chkLosAlarmL0.Checked = false;
            }
            else
            {
                chkLosAlarmL0.Checked = true;
            }

            // LOS Mask checkbox
            if ((data & 0x02) == 0)
            {
                chkLosMaskL0.Checked = false;
            }
            else
            {
                chkLosMaskL0.Checked = true;
            }

            // LOS Detector checkbox
            if ((data & 0x01) == 0)
            {
                chkPdLosL0.Checked = false;
            }
            else
            {
                chkPdLosL0.Checked = true;
            }
        }

        private void _ParsePage10Addr0C(byte data)
        {
            if ((data & 0x40) == 0)
            {
                chkLosL0.Checked = false;
            }
            else
            {
                chkLosL0.Checked = true;
            }
        }

        private void _ParsePage10Addr0D(byte data)
        {
            if ((data & 0x02) == 0)
            {

                chkTxDisableAutoMuteL0.Checked = false;
            }
            else
            {
                chkTxDisableAutoMuteL0.Checked = true;
            }

            if ((data & 0x01) == 0)
            {
                chkTxDisableL0.Checked = false;
            }
            else
            {
                chkTxDisableL0.Checked = true;
            }

        }

        private void _ParsePage10Addr0E(byte data)
        {
            // checkbox Ignore Tx Fault
            if ((data & 0x20) == 0)
            {
                chkTxIgnoreFaultL0.Checked = false;
            }
            else
            {
                chkTxIgnoreFaultL0.Checked = true;
            }

            // checkbox Tx Fault Alarm
            if ((data & 0x10) == 0)
            {
                chkTxFaultAlarmL0.Checked = false;
            }
            else
            {
                chkTxFaultAlarmL0.Checked = true;
            }

            foreach (ComboboxItem item in cboTxFaultStateL0.Items)
            {
                if (item.Value == (data & 0x0C) >> 2)
                {
                    cboTxFaultStateL0.SelectedItem = item;
                }
            }

            if ((data & 0x02) == 0)
            {
                chkTxFaultL0.Checked = false;
            }
            else
            {
                chkTxFaultL0.Checked = true;
            }

            if ((data & 0x01) == 0)
            {
                chkTxFaultMaskL0.Checked = false;
            }
            else
            {
                chkTxFaultMaskL0.Checked = true;
            }
        }

        private void _ParsePage10Addr10(byte data)
        {
            foreach (ComboboxItem item in cboRVcselL0.Items)
            {
                if (item.Value == (data & 0x78) >> 3)
                {
                    cboRVcselL0.SelectedItem = item;
                }
            }

        }

        private void _ParsePage10Addr11(byte data)
        {
            foreach (ComboboxItem item in cboIBiasL0.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboIBiasL0.SelectedItem = item;
                }
            }
        }

        private void _ParsePage10Addr12(byte data)
        {
            if ((data & 0x80) == 0)
            {
                chkTxBurnInEnableL0.Checked = false;
            }
            else
            {
                chkTxBurnInEnableL0.Checked = true;
            }

            foreach (ComboboxItem item in cboIBurnInL0.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboIBurnInL0.SelectedItem = item;
                }
            }
        }

        private void _ParsePage10Addr14(byte data)
        {
            foreach (ComboboxItem item in cboTailCurrentL0.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboTailCurrentL0.SelectedItem = item;
                }
            }
        }

        private void _ParsePage10Addr15(byte data)
        {
            foreach (ComboboxItem item in cboLowGainEnableL0.Items)
            {
                if (item.Value == (data & 0x10) >> 4)
                {
                    cboLowGainEnableL0.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboTxEqL0.Items)
            {
                if (item.Value == (data & 0x0F))
                {
                    cboTxEqL0.SelectedItem = item;
                }
            }
        }

        private void _ParsePage10Addr16(byte data)
        {
            foreach (ComboboxItem item in cboTxEqGainRangeL0.Items)
            {
                if (item.Value == (data & 0x01))
                {
                    cboTxEqGainRangeL0.SelectedItem = item;
                }
            }
        }

        private void _ParsePage10Addr17(byte data)
        {
            if ((data & 0x01) == 0)
            {
                chkDriverPkAvgSelL0.Checked = false;
            }
            else
            {
                chkDriverPkAvgSelL0.Checked = true;
            }
        }

        private void _ParsePage10Addr18(byte data)
        {
            if ((data & 0x01) == 0)
            {
                chkChannelToAdcL0.Checked = false;
            }
            else
            {
                chkChannelToAdcL0.Checked = true;
            }
        }

        #endregion

        #region ParsePage11AddrXX

        private void _ParsePage11Addr00(byte data)
        {
            if ((data & 0x01) == 0)
                chkPowerDownL1.Checked = false;
            else
                chkPowerDownL1.Checked = true;
        }
        private void _ParsePage11Addr01(byte data)
        {
            foreach (ComboboxItem item in cboVgaTailStg1L1.Items)
            {
                if (item.Value == ((data & 0xC0) >> 6))
                {
                    cboVgaTailStg1L1.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVgaGainStg1L1.Items)
            {
                if (item.Value == (data & 0x3F))
                {
                    cboVgaGainStg1L1.SelectedItem = item;
                }
            }
        }
        private void _ParsePage11Addr02(byte data)
        {
            foreach (ComboboxItem item in cboVga1GainChangePolarityL1.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga1GainChangePolarityL1.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga1GainChangeL1.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga1GainChangeL1.SelectedItem = item;
                }
            }
        }
        private void _ParsePage11Addr03(byte data)
        {
            foreach (ComboboxItem item in cboVga1BandwidthChangePolarityL1.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga1BandwidthChangePolarityL1.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga1BandwidthChangeL1.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga1BandwidthChangeL1.SelectedItem = item;
                }
            }
        }
        private void _ParsePage11Addr04(byte data)
        {
            foreach (ComboboxItem item in cboVgaTailStg2L1.Items)
            {
                if (item.Value == ((data & 0xC0) >> 6))
                {
                    cboVgaTailStg2L1.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVgaGainStg2L1.Items)
            {
                if (item.Value == (data & 0x3F))
                {
                    cboVgaGainStg2L1.SelectedItem = item;
                }
            }
        }
        private void _ParsePage11Addr05(byte data)
        {
            foreach (ComboboxItem item in cboVga2GainChangePolarityL1.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga2GainChangePolarityL1.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga2GainChangeL1.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga2GainChangeL1.SelectedItem = item;
                }
            }
        }
        private void _ParsePage11Addr06(byte data)
        {
            foreach (ComboboxItem item in cboVga2BandwidthChangePolarityL1.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga2BandwidthChangePolarityL1.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga2BandwidthChangeL1.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga2BandwidthChangeL1.SelectedItem = item;
                }
            }
        }
        private void _ParsePage11Addr08(byte data)
        {
            foreach (ComboboxItem item in cboVgaVcmAdjL1.Items)
            {
                if (item.Value == (data & (0xC0)) >> 6)
                {
                    cboVgaVcmAdjL1.SelectedItem = item;
                }
            }
        }
        private void _ParsePage11Addr09(byte data)
        {
            foreach (ComboboxItem item in cboLosthrshystL1.Items)
            {
                if (item.Value == (data & 0x20) >> 5)
                {
                    cboLosthrshystL1.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboLosthrsL1.Items)
            {
                if (item.Value == (data & 0x18) >> 3)
                {
                    cboLosthrsL1.SelectedItem = item;
                }
            }

            // LOS Alarm checkbox
            if ((data & 0x04) == 0)
            {
                chkLosAlarmL1.Checked = false;
            }
            else
            {
                chkLosAlarmL1.Checked = true;
            }

            // LOS Mask checkbox
            if ((data & 0x02) == 0)
            {
                chkLosMaskL1.Checked = false;
            }
            else
            {
                chkLosMaskL1.Checked = true;
            }

            // LOS Detector checkbox
            if ((data & 0x01) == 0)
            {
                chkPdLosL1.Checked = false;
            }
            else
            {
                chkPdLosL1.Checked = true;
            }
        }
        private void _ParsePage11Addr0C(byte data)
        {
            if ((data & 0x40) == 0)
            {
                chkLosL1.Checked = false;
            }
            else
            {
                chkLosL1.Checked = true;
            }
        }
        private void _ParsePage11Addr0D(byte data)
        {
            if ((data & 0x02) == 0)
            {

                chkTxDisableAutoMuteL1.Checked = false;
            }
            else
            {
                chkTxDisableAutoMuteL1.Checked = true;
            }

            if ((data & 0x01) == 0)
            {
                chkTxDisableL1.Checked = false;
            }
            else
            {
                chkTxDisableL1.Checked = true;
            }
        }
        private void _ParsePage11Addr0E(byte data)
        {
            // checkbox Ignore Tx Fault
            if ((data & 0x20) == 0)
            {
                chkTxIgnoreFaultL1.Checked = false;
            }
            else
            {
                chkTxIgnoreFaultL1.Checked = true;
            }

            // checkbox Tx Fault Alarm
            if ((data & 0x10) == 0)
            {
                chkTxFaultAlarmL1.Checked = false;
            }
            else
            {
                chkTxFaultAlarmL1.Checked = true;
            }

            foreach (ComboboxItem item in cboTxFaultStateL1.Items)
            {
                if (item.Value == (data & 0x0C) >> 2)
                {
                    cboTxFaultStateL1.SelectedItem = item;
                }
            }

            if ((data & 0x02) == 0)
            {
                chkTxFaultL1.Checked = false;
            }
            else
            {
                chkTxFaultL1.Checked = true;
            }

            if ((data & 0x01) == 0)
            {
                chkTxFaultMaskL1.Checked = false;
            }
            else
            {
                chkTxFaultMaskL1.Checked = true;
            }
        }
        private void _ParsePage11Addr10(byte data)
        {
            foreach (ComboboxItem item in cboRVcselL1.Items)
            {
                if (item.Value == (data & 0x78) >> 3)
                {
                    cboRVcselL1.SelectedItem = item;
                }
            }
        }
        private void _ParsePage11Addr11(byte data)
        {
            foreach (ComboboxItem item in cboIBiasL1.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboIBiasL1.SelectedItem = item;
                }
            }
        }
        private void _ParsePage11Addr12(byte data)
        {
            if ((data & 0x80) == 0)
            {
                chkTxBurnInEnableL1.Checked = false;
            }
            else
            {
                chkTxBurnInEnableL1.Checked = true;
            }

            foreach (ComboboxItem item in cboIBurnInL1.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboIBurnInL1.SelectedItem = item;
                }
            }
        }
        private void _ParsePage11Addr14(byte data)
        {
            foreach (ComboboxItem item in cboTailCurrentL1.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboTailCurrentL1.SelectedItem = item;
                }
            }
        }
        private void _ParsePage11Addr15(byte data)
        {
            foreach (ComboboxItem item in cboLowGainEnableL1.Items)
            {
                if (item.Value == (data & 0x10) >> 4)
                {
                    cboLowGainEnableL1.SelectedItem = item;
                }
            }
            foreach (ComboboxItem item in cboTxEqL1.Items)
            {
                if (item.Value == (data & 0x0F))
                {
                    cboTxEqL1.SelectedItem = item;
                }
            }
        }
        private void _ParsePage11Addr16(byte data)
        {
            foreach (ComboboxItem item in cboTxEqGainRangeL1.Items)
            {
                if (item.Value == (data & 0x01))
                {
                    cboTxEqGainRangeL1.SelectedItem = item;
                }
            }
        }
        private void _ParsePage11Addr17(byte data)
        {
            if ((data & 0x01) == 0)
            {
                chkDriverPkAvgSelL1.Checked = false;
            }
            else
            {
                chkDriverPkAvgSelL1.Checked = true;
            }
        }
        private void _ParsePage11Addr18(byte data)
        {
            if ((data & 0x01) == 0)
            {
                chkChannelToAdcL1.Checked = false;
            }
            else
            {
                chkChannelToAdcL1.Checked = true;
            }
        }
        #endregion

        #region ParsePage12AddrXX

        private void _ParsePage12Addr00(byte data)
        {
            if ((data & 0x01) == 0)
                chkPowerDownL2.Checked = false;
            else
                chkPowerDownL2.Checked = true;
        }

        private void _ParsePage12Addr01(byte data)
        {
            foreach (ComboboxItem item in cboVgaTailStg1L2.Items)
            {
                if (item.Value == ((data & 0xC0) >> 6))
                {
                    cboVgaTailStg1L2.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVgaGainStg1L2.Items)
            {
                if (item.Value == (data & 0x3F))
                {
                    cboVgaGainStg1L2.SelectedItem = item;
                }
            }
        }

        private void _ParsePage12Addr02(byte data)
        {
            foreach (ComboboxItem item in cboVga1GainChangePolarityL2.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga1GainChangePolarityL2.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga1GainChangeL2.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga1GainChangeL2.SelectedItem = item;
                }
            }
        }

        private void _ParsePage12Addr03(byte data)
        {
            foreach (ComboboxItem item in cboVga1BandwidthChangePolarityL2.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga1BandwidthChangePolarityL2.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga1BandwidthChangeL2.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga1BandwidthChangeL2.SelectedItem = item;
                }
            }
        }

        private void _ParsePage12Addr04(byte data)
        {
            foreach (ComboboxItem item in cboVgaTailStg2L2.Items)
            {
                if (item.Value == ((data & 0xC0) >> 6))
                {
                    cboVgaTailStg2L2.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVgaGainStg2L2.Items)
            {
                if (item.Value == (data & 0x3F))
                {
                    cboVgaGainStg2L2.SelectedItem = item;
                }
            }
        }

        private void _ParsePage12Addr05(byte data)
        {
            foreach (ComboboxItem item in cboVga2GainChangePolarityL2.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga2GainChangePolarityL2.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga2GainChangeL2.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga2GainChangeL2.SelectedItem = item;
                }
            }
        }

        private void _ParsePage12Addr06(byte data)
        {
            foreach (ComboboxItem item in cboVga2BandwidthChangePolarityL2.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga2BandwidthChangePolarityL2.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga2BandwidthChangeL2.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga2BandwidthChangeL2.SelectedItem = item;
                }
            }
        }

        private void _ParsePage12Addr08(byte data)
        {
            foreach (ComboboxItem item in cboVgaVcmAdjL2.Items)
            {
                if (item.Value == (data & (0xC0)) >> 6)
                {
                    cboVgaVcmAdjL2.SelectedItem = item;
                }
            }
        }

        private void _ParsePage12Addr09(byte data)
        {
            foreach (ComboboxItem item in cboLosthrshystL2.Items)
            {
                if (item.Value == (data & 0x20) >> 5)
                {
                    cboLosthrshystL2.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboLosthrsL2.Items)
            {
                if (item.Value == (data & 0x18) >> 3)
                {
                    cboLosthrsL2.SelectedItem = item;
                }
            }

            // LOS Alarm checkbox
            if ((data & 0x04) == 0)
            {
                chkLosAlarmL2.Checked = false;
            }
            else
            {
                chkLosAlarmL2.Checked = true;
            }

            // LOS Mask checkbox
            if ((data & 0x02) == 0)
            {
                chkLosMaskL2.Checked = false;
            }
            else
            {
                chkLosMaskL2.Checked = true;
            }

            // LOS Detector checkbox
            if ((data & 0x01) == 0)
            {
                chkPdLosL2.Checked = false;
            }
            else
            {
                chkPdLosL2.Checked = true;
            }
        }

        private void _ParsePage12Addr0C(byte data)
        {
            if ((data & 0x40) == 0)
            {
                chkLosL2.Checked = false;
            }
            else
            {
                chkLosL2.Checked = true;
            }
        }

        private void _ParsePage12Addr0D(byte data)
        {
            if ((data & 0x02) == 0)
            {

                chkTxDisableAutoMuteL2.Checked = false;
            }
            else
            {
                chkTxDisableAutoMuteL2.Checked = true;
            }

            if ((data & 0x01) == 0)
            {
                chkTxDisableL2.Checked = false;
            }
            else
            {
                chkTxDisableL2.Checked = true;
            }
        }

        private void _ParsePage12Addr0E(byte data)
        {
            // checkbox Ignore Tx Fault
            if ((data & 0x20) == 0)
            {
                chkTxIgnoreFaultL2.Checked = false;
            }
            else
            {
                chkTxIgnoreFaultL2.Checked = true;
            }

            // checkbox Tx Fault Alarm
            if ((data & 0x10) == 0)
            {
                chkTxFaultAlarmL2.Checked = false;
            }
            else
            {
                chkTxFaultAlarmL2.Checked = true;
            }

            foreach (ComboboxItem item in cboTxFaultStateL2.Items)
            {
                if (item.Value == (data & 0x0C) >> 2)
                {
                    cboTxFaultStateL2.SelectedItem = item;
                }
            }

            if ((data & 0x02) == 0)
            {
                chkTxFaultL2.Checked = false;
            }
            else
            {
                chkTxFaultL2.Checked = true;
            }

            if ((data & 0x01) == 0)
            {
                chkTxFaultMaskL2.Checked = false;
            }
            else
            {
                chkTxFaultMaskL2.Checked = true;
            }
        }

        private void _ParsePage12Addr10(byte data)
        {
            foreach (ComboboxItem item in cboRVcselL2.Items)
            {
                if (item.Value == (data & 0x78) >> 3)
                {
                    cboRVcselL2.SelectedItem = item;
                }
            }
        }

        private void _ParsePage12Addr11(byte data)
        {
            foreach (ComboboxItem item in cboIBiasL2.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboIBiasL2.SelectedItem = item;
                }
            }
        }

        private void _ParsePage12Addr12(byte data)
        {
            if ((data & 0x80) == 0)
            {
                chkTxBurnInEnableL2.Checked = false;
            }
            else
            {
                chkTxBurnInEnableL2.Checked = true;
            }

            foreach (ComboboxItem item in cboIBurnInL2.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboIBurnInL2.SelectedItem = item;
                }
            }
        }

        private void _ParsePage12Addr14(byte data)
        {
            foreach (ComboboxItem item in cboTailCurrentL2.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboTailCurrentL2.SelectedItem = item;
                }
            }
        }

        private void _ParsePage12Addr15(byte data)
        {
            foreach (ComboboxItem item in cboLowGainEnableL2.Items)
            {
                if (item.Value == (data & 0x10) >> 4)
                {
                    cboLowGainEnableL2.SelectedItem = item;
                }
            }
            foreach (ComboboxItem item in cboTxEqL2.Items)
            {
                if (item.Value == (data & 0x0F))
                {
                    cboTxEqL2.SelectedItem = item;
                }
            }
        }

        private void _ParsePage12Addr16(byte data)
        {
            foreach (ComboboxItem item in cboTxEqGainRangeL2.Items)
            {
                if (item.Value == (data & 0x01))
                {
                    cboTxEqGainRangeL2.SelectedItem = item;
                }
            }
        }

        private void _ParsePage12Addr17(byte data)
        {
            if ((data & 0x01) == 0)
            {
                chkDriverPkAvgSelL2.Checked = false;
            }
            else
            {
                chkDriverPkAvgSelL2.Checked = true;
            }
        }

        private void _ParsePage12Addr18(byte data)
        {
            if ((data & 0x01) == 0)
            {
                chkChannelToAdcL2.Checked = false;
            }
            else
            {
                chkChannelToAdcL2.Checked = true;
            }
        }


        #endregion

        #region ParsePage13AddrXX

        private void _ParsePage13Addr00(byte data)
        {
            if ((data & 0x01) == 0)
                chkPowerDownL3.Checked = false;
            else
                chkPowerDownL3.Checked = true;
        }

        private void _ParsePage13Addr01(byte data)
        {
            foreach (ComboboxItem item in cboVgaTailStg1L3.Items)
            {
                if (item.Value == ((data & 0xC0) >> 6))
                {
                    cboVgaTailStg1L3.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVgaGainStg1L3.Items)
            {
                if (item.Value == (data & 0x3F))
                {
                    cboVgaGainStg1L3.SelectedItem = item;
                }
            }
        }

        private void _ParsePage13Addr02(byte data)
        {
            foreach (ComboboxItem item in cboVga1GainChangePolarityL3.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga1GainChangePolarityL3.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga1GainChangeL3.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga1GainChangeL3.SelectedItem = item;
                }
            }
        }

        private void _ParsePage13Addr03(byte data)
        {
            foreach (ComboboxItem item in cboVga1BandwidthChangePolarityL3.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga1BandwidthChangePolarityL3.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga1BandwidthChangeL3.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga1BandwidthChangeL3.SelectedItem = item;
                }
            }
        }

        private void _ParsePage13Addr04(byte data)
        {
            foreach (ComboboxItem item in cboVgaTailStg2L3.Items)
            {
                if (item.Value == ((data & 0xC0) >> 6))
                {
                    cboVgaTailStg2L3.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVgaGainStg2L3.Items)
            {
                if (item.Value == (data & 0x3F))
                {
                    cboVgaGainStg2L3.SelectedItem = item;
                }
            }
        }

        private void _ParsePage13Addr05(byte data)
        {
            foreach (ComboboxItem item in cboVga2GainChangePolarityL3.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga2GainChangePolarityL3.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga2GainChangeL3.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga2GainChangeL3.SelectedItem = item;
                }
            }
        }

        private void _ParsePage13Addr06(byte data)
        {
            foreach (ComboboxItem item in cboVga2BandwidthChangePolarityL3.Items)
            {
                if (item.Value == ((data & 0x20) >> 5))
                {
                    cboVga2BandwidthChangePolarityL3.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboVga2BandwidthChangeL3.Items)

            {
                if (item.Value == (data & 0x1F))
                {
                    cboVga2BandwidthChangeL3.SelectedItem = item;
                }
            }
        }

        private void _ParsePage13Addr08(byte data)
        {
            foreach (ComboboxItem item in cboVgaVcmAdjL3.Items)
            {
                if (item.Value == (data & (0xC0)) >> 6)
                {
                    cboVgaVcmAdjL3.SelectedItem = item;
                }
            }
        }

        private void _ParsePage13Addr09(byte data)
        {
            foreach (ComboboxItem item in cboLosthrshystL3.Items)
            {
                if (item.Value == (data & 0x20) >> 5)
                {
                    cboLosthrshystL3.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboLosthrsL3.Items)
            {
                if (item.Value == (data & 0x18) >> 3)
                {
                    cboLosthrsL3.SelectedItem = item;
                }
            }

            // LOS Alarm checkbox
            if ((data & 0x04) == 0)
            {
                chkLosAlarmL3.Checked = false;
            }
            else
            {
                chkLosAlarmL3.Checked = true;
            }

            // LOS Mask checkbox
            if ((data & 0x02) == 0)
            {
                chkLosMaskL3.Checked = false;
            }
            else
            {
                chkLosMaskL3.Checked = true;
            }

            // LOS Detector checkbox
            if ((data & 0x01) == 0)
            {
                chkPdLosL3.Checked = false;
            }
            else
            {
                chkPdLosL3.Checked = true;
            }
        }

        private void _ParsePage13Addr0C(byte data)
        {
            if ((data & 0x40) == 0)
            {
                chkLosL3.Checked = false;
            }
            else
            {
                chkLosL3.Checked = true;
            }
        }

        private void _ParsePage13Addr0D(byte data)
        {
            if ((data & 0x02) == 0)
            {

                chkTxDisableAutoMuteL3.Checked = false;
            }
            else
            {
                chkTxDisableAutoMuteL3.Checked = true;
            }

            if ((data & 0x01) == 0)
            {
                chkTxDisableL3.Checked = false;
            }
            else
            {
                chkTxDisableL3.Checked = true;
            }
        }

        private void _ParsePage13Addr0E(byte data)
        {
            // checkbox Ignore Tx Fault
            if ((data & 0x20) == 0)
            {
                chkTxIgnoreFaultL3.Checked = false;
            }
            else
            {
                chkTxIgnoreFaultL3.Checked = true;
            }

            // checkbox Tx Fault Alarm
            if ((data & 0x10) == 0)
            {
                chkTxFaultAlarmL3.Checked = false;
            }
            else
            {
                chkTxFaultAlarmL3.Checked = true;
            }

            foreach (ComboboxItem item in cboTxFaultStateL3.Items)
            {
                if (item.Value == (data & 0x0C) >> 2)
                {
                    cboTxFaultStateL3.SelectedItem = item;
                }
            }

            if ((data & 0x02) == 0)
            {
                chkTxFaultL3.Checked = false;
            }
            else
            {
                chkTxFaultL3.Checked = true;
            }

            if ((data & 0x01) == 0)
            {
                chkTxFaultMaskL3.Checked = false;
            }
            else
            {
                chkTxFaultMaskL3.Checked = true;
            }
        }

        private void _ParsePage13Addr10(byte data)
        {
            foreach (ComboboxItem item in cboRVcselL3.Items)
            {
                if (item.Value == (data & 0x78) >> 3)
                {
                    cboRVcselL3.SelectedItem = item;
                }
            }
        }

        private void _ParsePage13Addr11(byte data)
        {
            foreach (ComboboxItem item in cboIBiasL3.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboIBiasL3.SelectedItem = item;
                }
            }
        }

        private void _ParsePage13Addr12(byte data)
        {
            if ((data & 0x80) == 0)
            {
                chkTxBurnInEnableL3.Checked = false;
            }
            else
            {
                chkTxBurnInEnableL3.Checked = true;
            }

            foreach (ComboboxItem item in cboIBurnInL3.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboIBurnInL3.SelectedItem = item;
                }
            }
        }

        private void _ParsePage13Addr14(byte data)
        {
            foreach (ComboboxItem item in cboTailCurrentL3.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboTailCurrentL3.SelectedItem = item;
                }
            }
        }

        private void _ParsePage13Addr15(byte data)
        {
            foreach (ComboboxItem item in cboLowGainEnableL3.Items)
            {
                if (item.Value == (data & 0x10) >> 4)
                {
                    cboLowGainEnableL3.SelectedItem = item;
                }
            }

            foreach (ComboboxItem item in cboTxEqL3.Items)
            {
                if (item.Value == (data & 0x0F))
                {
                    cboTxEqL3.SelectedItem = item;
                }
            }
        }

        private void _ParsePage13Addr16(byte data)
        {
            foreach (ComboboxItem item in cboTxEqGainRangeL3.Items)
            {
                if (item.Value == (data & 0x01))
                {
                    cboTxEqGainRangeL3.SelectedItem = item;
                }
            }
        }

        private void _ParsePage13Addr17(byte data)
        {
            if ((data & 0x01) == 0)
            {
                chkDriverPkAvgSelL3.Checked = false;
            }
            else
            {
                chkDriverPkAvgSelL3.Checked = true;
            }
        }

        private void _ParsePage13Addr18(byte data)
        {
            if ((data & 0x01) == 0)
            {
                chkChannelToAdcL3.Checked = false;
            }
            else
            {
                chkChannelToAdcL3.Checked = true;
            }
        }

        #endregion

        #region ParsePage30AddrXX

        private void _ParsePage30Addr00(byte data)
        {
            if ((data & 0x08) == 0)
            {
                chkAdcReadFreezeAll.Checked = false;
            }
            else
            {
                chkAdcReadFreezeAll.Checked = true;
            }

            foreach (ComboboxItem item in cboAdcSelectAll.Items)
            {
                if (item.Value == (data & 0x07))
                {
                    cboAdcSelectAll.SelectedItem = item;
                }
            }
        }

        private void _ParsePage30Addr01(byte data)
        {
            if ((data & 0x01) == 0)
            {
                chkAdcPowerDownAll.Checked = false;
            }
            else
            {
                chkAdcPowerDownAll.Checked = true;
            }
        }

        private void _ParsePage30Addr05_06(byte data0, byte data1)
        {
            int iTmp;

            iTmp = (data0 << 4) | data1;

            txtAdcOutput.Text = "0x" + iTmp.ToString("X3");

        }

        private void _ParsePage30Addr06(byte data)
        {

        }

        #endregion

        #region ParsePageAAAddrXX        
        private void _ParsePageAAAddrA0(byte data)
        {
            foreach (ComboboxItem item in cboL0InputEqualization0db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL0InputEqualization0db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrA1(byte data)
        {
            foreach (ComboboxItem item in cboL0InputEqualization1db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL0InputEqualization1db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrA2(byte data)
        {
            foreach (ComboboxItem item in cboL0InputEqualization2db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL0InputEqualization2db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrA3(byte data)
        {
            foreach (ComboboxItem item in cboL0InputEqualization3db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL0InputEqualization3db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrA4(byte data)
        {
            foreach (ComboboxItem item in cboL0InputEqualization4db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL0InputEqualization4db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrA5(byte data)
        {
            foreach (ComboboxItem item in cboL0InputEqualization5db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL0InputEqualization5db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrA6(byte data)
        {
            foreach (ComboboxItem item in cboL0InputEqualization6db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL0InputEqualization6db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrA7(byte data)
        {
            foreach (ComboboxItem item in cboL0InputEqualization7db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL0InputEqualization7db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrA8(byte data)
        {
            foreach (ComboboxItem item in cboL0InputEqualization8db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL0InputEqualization8db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrA9(byte data)
        {
            foreach (ComboboxItem item in cboL0InputEqualization9db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL0InputEqualization9db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrAA(byte data)
        {
            foreach (ComboboxItem item in cboL0InputEqualization10db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL0InputEqualization10db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrAB(byte data)
        {
            foreach (ComboboxItem item in cboL0InputEqualizationReserved0.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL0InputEqualizationReserved0.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrAC(byte data)
        {
            foreach (ComboboxItem item in cboL0InputEqualizationReserved1.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL0InputEqualizationReserved1.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrAD(byte data)
        {
            foreach (ComboboxItem item in cboL1InputEqualization0db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL1InputEqualization0db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrAE(byte data)
        {
            foreach (ComboboxItem item in cboL1InputEqualization1db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL1InputEqualization1db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrAF(byte data)
        {
            foreach (ComboboxItem item in cboL1InputEqualization2db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL1InputEqualization2db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrB0(byte data)
        {
            foreach (ComboboxItem item in cboL1InputEqualization3db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL1InputEqualization3db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrB1(byte data)
        {
            foreach (ComboboxItem item in cboL1InputEqualization4db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL1InputEqualization4db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrB2(byte data)
        {
            foreach (ComboboxItem item in cboL1InputEqualization5db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL1InputEqualization5db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrB3(byte data)
        {
            foreach (ComboboxItem item in cboL1InputEqualization6db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL1InputEqualization6db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrB4(byte data)
        {
            foreach (ComboboxItem item in cboL1InputEqualization7db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL1InputEqualization7db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrB5(byte data)
        {
            foreach (ComboboxItem item in cboL1InputEqualization8db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL1InputEqualization8db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrB6(byte data)
        {
            foreach (ComboboxItem item in cboL1InputEqualization9db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL1InputEqualization9db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrB7(byte data)
        {
            foreach (ComboboxItem item in cboL1InputEqualization10db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL1InputEqualization10db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrB8(byte data)
        {
            foreach (ComboboxItem item in cboL1InputEqualizationReserved0.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL1InputEqualizationReserved0.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrB9(byte data)
        {
            foreach (ComboboxItem item in cboL1InputEqualizationReserved1.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL1InputEqualizationReserved1.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrBA(byte data)
        {
            foreach (ComboboxItem item in cboL2InputEqualization0db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL2InputEqualization0db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrBB(byte data)
        {
            foreach (ComboboxItem item in cboL2InputEqualization1db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL2InputEqualization1db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrBC(byte data)
        {
            foreach (ComboboxItem item in cboL2InputEqualization2db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL2InputEqualization2db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrBD(byte data)
        {
            foreach (ComboboxItem item in cboL2InputEqualization3db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL2InputEqualization3db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrBE(byte data)
        {
            foreach (ComboboxItem item in cboL2InputEqualization4db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL2InputEqualization4db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrBF(byte data)
        {
            foreach (ComboboxItem item in cboL2InputEqualization5db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL2InputEqualization5db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrC0(byte data)
        {
            foreach (ComboboxItem item in cboL2InputEqualization6db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL2InputEqualization6db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrC1(byte data)
        {
            foreach (ComboboxItem item in cboL2InputEqualization7db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL2InputEqualization7db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrC2(byte data)
        {
            foreach (ComboboxItem item in cboL2InputEqualization8db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL2InputEqualization8db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrC3(byte data)
        {
            foreach (ComboboxItem item in cboL2InputEqualization9db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL2InputEqualization9db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrC4(byte data)
        {
            foreach (ComboboxItem item in cboL2InputEqualization10db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL2InputEqualization10db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrC5(byte data)
        {
            foreach (ComboboxItem item in cboL2InputEqualizationReserved0.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL2InputEqualizationReserved0.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrC6(byte data)
        {
            foreach (ComboboxItem item in cboL2InputEqualizationReserved1.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL2InputEqualizationReserved1.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrC7(byte data)
        {
            foreach (ComboboxItem item in cboL3InputEqualization0db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL3InputEqualization0db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrC8(byte data)
        {
            foreach (ComboboxItem item in cboL3InputEqualization1db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL3InputEqualization1db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrC9(byte data)
        {
            foreach (ComboboxItem item in cboL3InputEqualization2db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL3InputEqualization2db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrCA(byte data)
        {
            foreach (ComboboxItem item in cboL3InputEqualization3db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL3InputEqualization3db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrCB(byte data)
        {
            foreach (ComboboxItem item in cboL3InputEqualization4db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL3InputEqualization4db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrCC(byte data)
        {
            foreach (ComboboxItem item in cboL3InputEqualization5db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL3InputEqualization5db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrCD(byte data)
        {
            foreach (ComboboxItem item in cboL3InputEqualization6db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL3InputEqualization6db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrCE(byte data)
        {
            foreach (ComboboxItem item in cboL3InputEqualization7db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL3InputEqualization7db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrCF(byte data)
        {
            foreach (ComboboxItem item in cboL3InputEqualization8db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL3InputEqualization8db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrD0(byte data)
        {
            foreach (ComboboxItem item in cboL3InputEqualization9db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL3InputEqualization9db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrD1(byte data)
        {
            foreach (ComboboxItem item in cboL3InputEqualization10db.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL3InputEqualization10db.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrD2(byte data)
        {
            foreach (ComboboxItem item in cboL3InputEqualizationReserved0.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL3InputEqualizationReserved0.SelectedItem = item;
                }
            }
        }

        private void _ParsePageAAAddrD3(byte data)
        {
            foreach (ComboboxItem item in cboL3InputEqualizationReserved1.Items)
            {
                if (item.Value == (data & 0x7F))
                {
                    cboL3InputEqualizationReserved1.SelectedItem = item;
                }
            }
        }

        #endregion

        #region _WritePageXXAddrXX

        private int _WritePage10Addr00()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (chkPowerDownL0.Checked == true)
                data[0] |= 0x01;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage11Addr00()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (chkPowerDownL1.Checked == true)
                data[0] |= 0x01;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage12Addr00()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (chkPowerDownL2.Checked == true)
                data[0] |= 0x01;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage13Addr00()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (chkPowerDownL3.Checked == true)
                data[0] |= 0x01;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage0FAddr00()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (chkPowerDownAll.Checked == true)
                data[0] |= 0x01;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage10Addr01()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cboVgaTailStg1L0.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVgaGainStg1L0.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x01, 1, data);
            if (rv < 0)
                return -1;

            return 0;            
        }

        private int _WritePage11Addr01()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cboVgaTailStg1L1.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVgaGainStg1L1.SelectedIndex);
            data[0] |= bTmp;


            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x01, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage12Addr01()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cboVgaTailStg1L2.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVgaGainStg1L2.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x01, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage13Addr01()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cboVgaTailStg1L3.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVgaGainStg1L3.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x01, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage0FAddr01()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cboVgaTailStg1All.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVgaGainStg1All.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x01, 1, data);
            if (rv < 0)
                return -1;

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
            if(rv < 0) 
                return -1;

            return 0;
        }

        private int _WritePage10Addr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga1GainChangePolarityL0.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga1GainChangeL0.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x02, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage11Addr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga1GainChangePolarityL1.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga1GainChangeL1.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x02, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage12Addr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga1GainChangePolarityL2.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga1GainChangeL2.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x02, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage13Addr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga1GainChangePolarityL3.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga1GainChangeL3.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x02, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage0FAddr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga1GainChangePolarityAll.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga1GainChangeAll.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x02, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage00Addr03()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboIntrptPadPolarity.SelectedIndex);            
            bTmp <<= 7;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboIntrptoen.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboI2cAddressMode.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            SetPage(Page00);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x03, 1, data);
            if (rv < 0)
                return -1;

            return 0;



        }

        private int _WritePage10Addr03()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga1BandwidthChangePolarityL0.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga1BandwidthChangeL0.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x03, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage11Addr03()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga1BandwidthChangePolarityL1.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga1BandwidthChangeL1.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x03, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage12Addr03()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga1BandwidthChangePolarityL2.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga1BandwidthChangeL2.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x03, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage13Addr03()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga1BandwidthChangePolarityL3.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga1BandwidthChangeL3.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x03, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage0FAddr03()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga1BandwidthChangePolarityAll.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga1BandwidthChangeAll.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x03, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage10Addr04()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;


            bTmp = Convert.ToByte(cboVgaTailStg2L0.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVgaGainStg2L0.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x04, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage11Addr04()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;


            bTmp = Convert.ToByte(cboVgaTailStg2L1.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVgaGainStg2L1.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x04, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage12Addr04()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;


            bTmp = Convert.ToByte(cboVgaTailStg2L2.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVgaGainStg2L2.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x04, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage13Addr04()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;


            bTmp = Convert.ToByte(cboVgaTailStg2L3.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVgaGainStg2L3.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x04, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage0FAddr04()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;


            bTmp = Convert.ToByte(cboVgaTailStg2All.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVgaGainStg2All.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x04, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage10Addr05()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga2GainChangePolarityL0.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga2GainChangeL0.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x05, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage11Addr05()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga2GainChangePolarityL1.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga2GainChangeL1.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x05, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage12Addr05()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga2GainChangePolarityL2.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga2GainChangeL2.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x05, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage13Addr05()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga2GainChangePolarityL3.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga2GainChangeL3.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x05, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage0FAddr05()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga2GainChangePolarityAll.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga2GainChangeAll.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x05, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage10Addr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga2BandwidthChangePolarityL0.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga2BandwidthChangeL0.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x06, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage11Addr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga2BandwidthChangePolarityL1.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga2BandwidthChangeL1.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x06, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage12Addr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga2BandwidthChangePolarityL2.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga2BandwidthChangeL2.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x06, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage13Addr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga2BandwidthChangePolarityL3.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga2BandwidthChangeL3.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x06, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage0FAddr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboVga2BandwidthChangePolarityAll.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboVga2BandwidthChangeAll.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x06, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage10Addr08()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cboVgaVcmAdjL0.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x08, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage11Addr08()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cboVgaVcmAdjL1.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x08, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage12Addr08()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cboVgaVcmAdjL2.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x08, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage13Addr08()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cboVgaVcmAdjL3.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x08, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage0FAddr08()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cboVgaVcmAdjAll.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x08, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage10Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cboLosthrshystL0.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboLosthrsL0.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;


            if (chkLosAlarmL0.Checked == true)
                data[0] |= 0x04;

            if (chkLosMaskL0.Checked == true)
                data[0] |= 0x02;

            if (chkPdLosL0.Checked == true) 
                data[0] |= 0x01;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x09, 1, data);
            if (rv < 0)
                return -1;

            return 0;



        }

        private int _WritePage11Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cboLosthrshystL1.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboLosthrsL1.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;


            if (chkLosAlarmL1.Checked == true)
                data[0] |= 0x04;

            if (chkLosMaskL1.Checked == true)
                data[0] |= 0x02;

            if (chkPdLosL1.Checked == true)
                data[0] |= 0x01;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x09, 1, data);
            if (rv < 0)
                return -1;

            return 0;



        }

        private int _WritePage12Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cboLosthrshystL2.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboLosthrsL2.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;


            if (chkLosAlarmL2.Checked == true)
                data[0] |= 0x04;

            if (chkLosMaskL2.Checked == true)
                data[0] |= 0x02;

            if (chkPdLosL2.Checked == true)
                data[0] |= 0x01;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x09, 1, data);
            if (rv < 0)
                return -1;

            return 0;



        }

        private int _WritePage13Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cboLosthrshystL3.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboLosthrsL3.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;


            if (chkLosAlarmL3.Checked == true)
                data[0] |= 0x04;

            if (chkLosMaskL3.Checked == true)
                data[0] |= 0x02;

            if (chkPdLosL3.Checked == true)
                data[0] |= 0x01;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x09, 1, data);
            if (rv < 0)
                return -1;

            return 0;



        }

        private int _WritePage0FAddr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cboLosthrshystAll.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboLosthrsAll.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;


            if (chkLosAlarmAll.Checked == true)
                data[0] |= 0x04;

            if (chkLosMaskAll.Checked == true)
                data[0] |= 0x02;

            if (chkPdLosAll.Checked == true)
                data[0] |= 0x01;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x09, 1, data);
            if (rv < 0)
                return -1;

            return 0;



        }

        private int _WritePage10Addr0C()
        {
            byte[] data = new byte[1];
            int rv;


            if (chkLosL0.Checked == true)
                data[0] |= 0x40;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0C, 1, data);
            if (rv < 0)
                return -1;

            return 0;


        }

        private int _WritePage11Addr0C()
        {
            byte[] data = new byte[1];
            int rv;


            if (chkLosL1.Checked == true)
                data[0] |= 0x40;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0C, 1, data);
            if (rv < 0)
                return -1;

            return 0;


        }

        private int _WritePage12Addr0C()
        {
            byte[] data = new byte[1];
            int rv;


            if (chkLosL2.Checked == true)
                data[0] |= 0x40;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0C, 1, data);
            if (rv < 0)
                return -1;

            return 0;


        }

        private int _WritePage13Addr0C()
        {
            byte[] data = new byte[1];
            int rv;


            if (chkLosL3.Checked == true)
                data[0] |= 0x40;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0C, 1, data);
            if (rv < 0)
                return -1;

            return 0;


        }

        private int _WritePage10Addr0D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (chkTxDisableAutoMuteL0.Checked == true)
                data[0] |= 0x02;

            if (chkTxDisableL0.Checked == true)
                data[0] |= 0x01;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0D, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage11Addr0D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (chkTxDisableAutoMuteL1.Checked == true)
                data[0] |= 0x02;

            if (chkTxDisableL1.Checked == true)
                data[0] |= 0x01;


            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0D, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage12Addr0D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (chkTxDisableAutoMuteL2.Checked == true)
                data[0] |= 0x02;

            if (chkTxDisableL2.Checked == true)
                data[0] |= 0x01;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0D, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage13Addr0D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;


            if (chkTxDisableAutoMuteL3.Checked == true)
                data[0] |= 0x02;

            if (chkTxDisableL3.Checked == true)
                data[0] |= 0x01;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0D, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage0FAddr0D()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;


            if (chkTxDisableAutoMuteAll.Checked == true)
                data[0] |= 0x02;

            if (chkTxDisableAll.Checked == true)
                data[0] |= 0x01;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0D, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage10Addr0E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;         

            bTmp = data[0] = 0;

            if (chkTxIgnoreFaultL0.Checked == true)
                data[0] |= 0x20;

            if (chkTxFaultAlarmL0.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cboTxFaultStateL0.SelectedIndex);
                bTmp <<= 2;
            data[0] |= bTmp;

            if (chkTxFaultL0.Checked == true)
                data[0] |= 0x02;

            if (chkTxFaultMaskL0.Checked == true)
                data[0] |= 0x01;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0E, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage11Addr0E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (chkTxIgnoreFaultL1.Checked == true)
                data[0] |= 0x20;

            if (chkTxFaultAlarmL1.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cboTxFaultStateL1.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            if (chkTxFaultL1.Checked == true)
                data[0] |= 0x02;

            if (chkTxFaultMaskL1.Checked == true)
                data[0] |= 0x01;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0E, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage12Addr0E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (chkTxIgnoreFaultL2.Checked == true)
                data[0] |= 0x20;

            if (chkTxFaultAlarmL2.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cboTxFaultStateL2.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            if (chkTxFaultL2.Checked == true)
                data[0] |= 0x02;

            if (chkTxFaultMaskL2.Checked == true)
                data[0] |= 0x01;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0E, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage13Addr0E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (chkTxIgnoreFaultL3.Checked == true)
                data[0] |= 0x20;

            if (chkTxFaultAlarmL3.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cboTxFaultStateL3.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            if (chkTxFaultL3.Checked == true)
                data[0] |= 0x02;

            if (chkTxFaultMaskL3.Checked == true)
                data[0] |= 0x01;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0E, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage0FAddr0E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (chkTxIgnoreFaultAll.Checked == true)
                data[0] |= 0x20;

            if (chkTxFaultAlarmAll.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cboTxFaultStateAll.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            if (chkTxFaultAll.Checked == true)
                data[0] |= 0x02;

            if (chkTxFaultMaskAll.Checked == true)
                data[0] |= 0x01;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0E, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage0FAddr0F()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 119; //default= 0x77

            if (chkIgnoreTxDisablePinAll.Checked == true)
                data[0] |= 0x80;

            if (chkTxAlarmClearInterruptAll.Checked == true)
                data[0] |= 0x40;

            if (chkTxAlarmClearPinAll.Checked == true)
                data[0] |= 0x20;

            if (chkTxAlarmClearRegisterAll.Checked == true)
                data[0] |= 0x10;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x0F, 1, data);
            if(rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage10Addr10()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0; 

            bTmp = Convert.ToByte(cboRVcselL0.SelectedIndex +1);
            bTmp <<= 3;
            data[0] |= bTmp;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x10, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage11Addr10()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;

            bTmp = Convert.ToByte(cboRVcselL1.SelectedIndex +1);
            bTmp <<= 3;
            data[0] |= bTmp;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x10, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage12Addr10()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0; 

            bTmp = Convert.ToByte(cboRVcselL2.SelectedIndex +1);
            bTmp <<= 3;
            data[0] |= bTmp;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x10, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage13Addr10()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0; 

            bTmp = Convert.ToByte(cboRVcselL3.SelectedIndex+1);
            bTmp <<= 3;
            data[0] |= bTmp;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x10, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage0FAddr10()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;

            bTmp = Convert.ToByte(cboRVcselAll.SelectedIndex+1);
            bTmp <<= 3;
            data[0] |= bTmp;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x10, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage10Addr11()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = Convert.ToByte(cboIBiasL0.SelectedIndex);
            data[0] = bTmp;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x11, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage11Addr11()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = Convert.ToByte(cboIBiasL1.SelectedIndex);
            data[0] = bTmp;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x11, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage12Addr11()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = Convert.ToByte(cboIBiasL2.SelectedIndex);
            data[0] = bTmp;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x11, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage13Addr11()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = Convert.ToByte(cboIBiasL3.SelectedIndex);
            data[0] = bTmp;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x11, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage0FAddr11()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = Convert.ToByte(cboIBiasAll.SelectedIndex);
            data[0] = bTmp;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x11, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage10Addr12()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (chkTxBurnInEnableL0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cboIBurnInL0.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x12, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage11Addr12()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (chkTxBurnInEnableL1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cboIBurnInL1.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x12, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage12Addr12()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (chkTxBurnInEnableL2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cboIBurnInL2.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x12, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage13Addr12()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (chkTxBurnInEnableL3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cboIBurnInL3.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x12, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage0FAddr12()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (chkTxBurnInEnableAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cboIBurnInAll.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x12, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage10Addr14()
        {
            byte[] data = new byte[1];
            int rv;

            byte bTmp;

            bTmp = Convert.ToByte(cboTailCurrentL0.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x14, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage11Addr14()
        {
            byte[] data = new byte[1];
            int rv;

            byte bTmp;

            bTmp = Convert.ToByte(cboTailCurrentL1.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x14, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage12Addr14()
        {
            byte[] data = new byte[1];
            int rv;

            byte bTmp;

            bTmp = Convert.ToByte(cboTailCurrentL2.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x14, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage13Addr14()
        {
            byte[] data = new byte[1];
            int rv;

            byte bTmp;

            bTmp = Convert.ToByte(cboTailCurrentL3.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x14, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage0FAddr14()
        {
            byte[] data = new byte[1];
            int rv;

            byte bTmp;

            bTmp = Convert.ToByte(cboTailCurrentAll.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x14, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage10Addr15()
        {
            byte[] data = new byte[1];
            int rv;

            byte bTmp;

            bTmp = Convert.ToByte(cboLowGainEnableL0.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboTxEqL0.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x15, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage11Addr15()
        {
            byte[] data = new byte[1];
            int rv;

            byte bTmp;

            bTmp = Convert.ToByte(cboLowGainEnableL1.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboTxEqL1.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x15, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage12Addr15()
        {
            byte[] data = new byte[1];
            int rv;

            byte bTmp;

            bTmp = Convert.ToByte(cboLowGainEnableL2.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboTxEqL2.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x15, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage13Addr15()
        {
            byte[] data = new byte[1];
            int rv;

            byte bTmp;

            bTmp = Convert.ToByte(cboLowGainEnableL3.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboTxEqL3.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x15, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage0FAddr15()
        {
            byte[] data = new byte[1];
            int rv;

            byte bTmp;

            bTmp = Convert.ToByte(cboLowGainEnableAll.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cboTxEqAll.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x15, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage10Addr16()
        {
            byte[] data = new byte[1];
            int rv;

            byte bTmp;

            bTmp = Convert.ToByte(cboTxEqGainRangeL0.SelectedIndex);            
            data[0] |= bTmp;

            SetPage(Page10);
            Thread.Sleep(100);


            rv = i2cWriteCB(devAddr, 0x16, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage11Addr16()
        {
            byte[] data = new byte[1];
            int rv;

            byte bTmp;

            bTmp = Convert.ToByte(cboTxEqGainRangeL1.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x16, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage12Addr16()
        {
            byte[] data = new byte[1];
            int rv;

            byte bTmp;

            bTmp = Convert.ToByte(cboTxEqGainRangeL2.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x16, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage13Addr16()
        {
            byte[] data = new byte[1];
            int rv;

            byte bTmp;

            bTmp = Convert.ToByte(cboTxEqGainRangeL3.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x16, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage0FAddr16()
        {
            byte[] data = new byte[1];
            int rv;

            byte bTmp;

            bTmp = Convert.ToByte(cboTxEqGainRangeAll.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x16, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage10Addr17()
        {
            byte[] data = new byte[1];
            int rv;
            

            if (chkDriverPkAvgSelL0.Checked == true)
                data[0] |= 0x01;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x17, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage11Addr17()
        {
            byte[] data = new byte[1];
            int rv;


            if (chkDriverPkAvgSelL1.Checked == true)
                data[0] |= 0x01;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x17, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage12Addr17()
        {
            byte[] data = new byte[1];
            int rv;


            if (chkDriverPkAvgSelL2.Checked == true)
                data[0] |= 0x01;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x17, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage13Addr17()
        {
            byte[] data = new byte[1];
            int rv;


            if (chkDriverPkAvgSelL3.Checked == true)
                data[0] |= 0x01;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x17, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage0FAddr17()
        {
            byte[] data = new byte[1];
            int rv;


            if (chkDriverPkAvgSelAll.Checked == true)
                data[0] |= 0x01;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x17, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage10Addr18()
        {
            byte[] data = new byte[1];
            int rv;

            if (chkChannelToAdcL0.Checked == true)
                data[0] |= 0x01;

            SetPage(Page10);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x18, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage11Addr18()
        {
            byte[] data = new byte[1];
            int rv;

            if (chkChannelToAdcL1.Checked == true)
            data[0] |= 0x01;

            SetPage(Page11);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x18, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage12Addr18()
        {
            byte[] data = new byte[1];
            int rv;

            if (chkChannelToAdcL2.Checked == true)
            data[0] |= 0x01;

            SetPage(Page12);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x18, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage13Addr18()
        {
            byte[] data = new byte[1];
            int rv;

            if (chkChannelToAdcL3.Checked == true)
            data[0] |= 0x01;

            SetPage(Page13);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x18, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage0FAddr1B()
        {
            byte[] data = new byte[1];
            int rv;

            if (chkTxIBiasAvgAll.Checked == true)
                data[0] |= 0x08;

            if (chkTxIBiasRefAll.Checked == true)
                data[0] |= 0x04;

            if (chkTxVcc3V3All.Checked == true)
                data[0] |= 0x01;

            SetPage(Page0F);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x1B, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage30Addr00()
        {

            byte[] data = new byte[1];
            int rv;

            byte bTmp;

            if (chkAdcReadFreezeAll.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cboAdcSelectAll.SelectedIndex);
            data[0] |= bTmp;

            SetPage(Page30);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x00, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePage30Addr01()
        {
            byte[] data = new byte[1];
            int rv;

            if (chkAdcPowerDownAll.Checked == true)
                data[0] |= 0x01;

            SetPage(Page30);
            Thread.Sleep(100);

            rv = i2cWriteCB(devAddr, 0x01, 1, data);
            if (rv < 0)
                return -1;

            return 0;

        }

        private int _WritePageAAAddrA0()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL0InputEqualization0db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xA0, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrA1()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL0InputEqualization1db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xA1, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrA2()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL0InputEqualization2db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xA2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrA3()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL0InputEqualization3db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xA3, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrA4()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL0InputEqualization4db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xA4, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrA5()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL0InputEqualization5db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xA5, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrA6()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL0InputEqualization6db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xA6, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrA7()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL0InputEqualization7db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xA7, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrA8()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL0InputEqualization8db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xA8, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrA9()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL0InputEqualization9db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xA9, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrAA()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL0InputEqualization10db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xAA, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrAB()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL0InputEqualizationReserved0.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xAB, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrAC()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL0InputEqualizationReserved1.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xAC, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrAD()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL1InputEqualization0db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xAD, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrAE()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL1InputEqualization1db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xAE, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrAF()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL1InputEqualization2db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xAF, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrB0()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL1InputEqualization3db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xB0, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrB1()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL1InputEqualization4db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xB1, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrB2()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL1InputEqualization5db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xB2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrB3()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL1InputEqualization6db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xB3, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrB4()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL1InputEqualization7db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xB4, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrB5()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL1InputEqualization8db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xB5, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrB6()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL1InputEqualization9db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xB6, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrB7()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL1InputEqualization10db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xB7, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrB8()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL1InputEqualizationReserved0.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xB8, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrB9()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL1InputEqualizationReserved1.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xB9, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrBA()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL2InputEqualization0db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xBA, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrBB()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL2InputEqualization1db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xBB, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrBC()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL2InputEqualization2db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xBC, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrBD()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL2InputEqualization3db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xBD, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrBE()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL2InputEqualization4db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xBE, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrBF()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL2InputEqualization5db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xBF, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrC0()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL2InputEqualization6db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xC0, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrC1()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL2InputEqualization7db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xC1, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrC2()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL2InputEqualization8db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xC2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrC3()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL2InputEqualization9db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xC3, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrC4()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL2InputEqualization10db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xC4, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrC5()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL2InputEqualizationReserved0.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xC5, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrC6()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL2InputEqualizationReserved1.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xC6, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrC7()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL3InputEqualization0db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xC7, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrC8()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL3InputEqualization1db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xC8, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrC9()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL3InputEqualization2db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xC9, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrCA()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL3InputEqualization3db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xCA, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrCB()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL3InputEqualization4db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xCB, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrCC()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL3InputEqualization5db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xCC, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrCD()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL3InputEqualization6db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xCD, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrCE()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL3InputEqualization7db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xCE, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrCF()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL3InputEqualization8db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xCF, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrD0()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL3InputEqualization9db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xD0, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrD1()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL3InputEqualization10db.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xD1, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrD2()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL3InputEqualizationReserved0.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xD2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePageAAAddrD3()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cboL3InputEqualizationReserved1.SelectedIndex);
            data[0] |= bTmp;

            SetPage(PageAA);
            rv = i2cWriteCB(devAddr, 0xD3, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }



        #endregion

        #region L0 Event


        private void chkPowerDownL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);

            if (_WritePage10Addr00() < 0)
                return;
        }

        // VGA Gain Mode :
        private void cboLowGainEnableL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return ;

            SetPage(Page10);
            if (_WritePage10Addr15() <0)
                return ;
        }

        private void cboVgaGainStg1L0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr01() < 0)
                return;

        }

        private void cboVgaGainStg2L0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr04() < 0)
                return;
        }

        private void cboVga1GainChangeL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr02() < 0)
                return;
        }

        private void cboVga2GainChangeL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr05() < 0)
                return;

        }

        private void cboVga1GainChangePolarityL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr02() < 0)
                return;
        }

        private void cboVga2GainChangePolarityL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;
            SetPage(Page10);
            if (_WritePage10Addr05() < 0)
                return;

        }

        private void cboVgaTailStg1L0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;
            SetPage(Page10);
            if (_WritePage10Addr01() < 0)
                return;

        }

        private void cboVgaTailStg2L0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;
            SetPage(Page10);
            if (_WritePage10Addr04() < 0)
                return;
        }

        private void cboVgaVcmAdjL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr08() < 0)
                return;

        }

        // VGA Bandwidth setting
        private void cboVga1BandwidthChangePolarityL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr03() < 0)
                return;

        }

        private void cboVga1BandwidthChangeL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr03() < 0)
                return;

        }

        private void cboVga2BandwidthChangePolarityL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr06() < 0)
                return;
        }

        private void cboVga2BandwidthChangeL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr06() < 0)
                return;
        }

        private void cboLosthrshystL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr09() < 0)
                return;

        }

        private void cboLosthrsL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr09() < 0)
                return;
        }

        private void chkLosAlarmL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr09() < 0)
                return;
        }

        private void chkLosMaskL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr09() < 0)
                return;
        }

        private void chkPdLosL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr09() < 0)
                return;
        }

        private void chkLosL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr0C() < 0)
                return;

        }

        private void chkTxDisableAutoMuteL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr0D() < 0)
                return;
        }

        private void chkTxDisableL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr0D() < 0)
                return;
        }

        private void chkDriverPkAvgSelL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr17() < 0)
                return;
        }

        private void chkChannelToAdcL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr18() < 0)
                return;
        }

        private void chkTxIgnoreFaultL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr0E() < 0)
                return;
        }

        private void chkTxFaultAlarmL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr0E() < 0)
                return;
        }

        private void cboTxFaultStateL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr0E() < 0)
                return;
        }

        private void chkTxFaultL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr0E() < 0)
                return;
        }

        private void cboRVcselL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr10() < 0)
                return;
        }

        private void cboIBiasL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr11() < 0)
                return;
            
        }


        private void chkTxBurnInEnableL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr12() < 0)
                return;
        }

        private void cboIBurnInL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr12() < 0)
                return;
        }

        private void cboTailCurrentL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr14() < 0)
                return;
        }

        private void cboTxEqL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr15() < 0)
                return;
        }

        private void cboTxEqGainRangeL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page10);
            if (_WritePage10Addr16() < 0)
                return;
        }

        private void chkTxFaultMaskL0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage10Addr0E() < 0)
                return;
        }

        #endregion

        #region L1 Event


        private void chkPowerDownL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr00() < 0)
                return;
        }

        private void cboLowGainEnableL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr15() < 0)
                return;
        }

        private void cboVgaGainStg1L1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr01() < 0)
                return;
        }

        private void cboVgaGainStg2L1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr04() < 0)
                return;
        }

        private void cboVga1GainChangeL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr02() < 0)
                return;
        }

        private void cboVga2GainChangeL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr05() < 0)
                return;
        }

        private void cboVga1GainChangePolarityL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr02() < 0)
                return;
        }

        private void cboVga2GainChangePolarityL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr05() < 0)
                return;
        }

        private void cboVgaTailStg1L1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr01() < 0)
                return;
        }

        private void cboVgaTailStg2L1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr04() < 0)
                return;
        }

        private void cboVgaVcmAdjL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr08() < 0)
                return;

        }

        private void cboVga1BandwidthChangePolarityL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr03() < 0)
                return;
        }

        private void cboVga1BandwidthChangeL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr03() < 0)
                return;
        }

        private void cboVga2BandwidthChangePolarityL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr06() < 0)
                return;
        }

        private void cboVga2BandwidthChangeL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr06() < 0)
                return;

        }

        private void cboLosthrshystL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr09() < 0)
                return;
        }

        private void cboLosthrsL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr09() < 0)
                return;
        }

        private void chkLosAlarmL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr09() < 0)
                return;
        }

        private void chkLosMaskL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr09() < 0)
                return;
        }

        private void chkPdLosL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr09() < 0)
                return;
        }

        private void chkLosL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr0C() < 0)
                return;
        }

        private void chkTxDisableAutoMuteL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr0D() < 0)
                return;
        }

        private void chkTxDisableL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr0D() < 0)
                return;
        }

        private void chkDriverPkAvgSelL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr17() < 0)
                return;
        }

        private void chkChannelToAdcL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr18() < 0)
                return;
        }

        private void chkTxIgnoreFaultL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr0E() < 0)
                return;
        }

        private void chkTxFaultAlarmL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr0E() < 0)
                return;
        }

        private void cboTxFaultStateL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr0E() < 0)
                return;
        }

        private void chkTxFaultL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr0E() < 0)
                return;
        }

        private void chkTxFaultMaskL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr0E() < 0)
                return;
        }

        private void cboRVcselL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr10() < 0)
                return;
        }

        private void cboIBiasL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr11() < 0)
                return;
        }

        private void cboTailCurrentL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr14() < 0)
                return;
        }

        private void cboIBurnInL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;
            
            SetPage(Page11);
            if (_WritePage11Addr12() < 0)
                return;
        }

        private void chkTxBurnInEnableL1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr12() < 0)
                return;
        }

        private void cboTxEqL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;
            
            SetPage(Page11);
            if (_WritePage11Addr15() < 0)
                return;
        }

        private void cboTxEqGainRangeL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page11);
            if (_WritePage11Addr16() < 0)
                return;
        }

        #endregion

        #region L2 Event

        private void chkPowerDownL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr00() < 0)
                return;
        }

        private void cboLowGainEnableL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr15() < 0)
                return;
        }

        private void cboVgaGainStg1L2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr01() < 0)
                return;
        }

        private void cboVgaGainStg2L2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr04() < 0)
                return;
        }

        private void cboVga1GainChangeL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr02() < 0)
                return;
        }

        private void cboVga2GainChangeL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr05() < 0)
                return;
        }

        private void cboVga1GainChangePolarityL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr02() < 0)
                return;
        }

        private void cboVga2GainChangePolarityL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr05() < 0)
                return;
        }

        private void cboVgaTailStg1L2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;
            SetPage(Page12);
            if (_WritePage12Addr01() < 0)
                return;
        }

        private void cboVgaTailStg2L2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr04() < 0)
                return;

        }

        private void cboVgaVcmAdjL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr08() < 0)
                return;
        }

        private void cboVga1BandwidthChangePolarityL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr03() < 0)
                return;
        }

        private void cboVga1BandwidthChangeL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr03() < 0)
                return;
        }

        private void cboVga2BandwidthChangePolarityL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr06() < 0)
                return;
        }

        private void cboVga2BandwidthChangeL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr06() < 0)
                return;
        }

        private void cboLosthrshystL2_Enter(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr09() < 0)
                return;
        }

        private void cboLosthrsL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr09() < 0)
                return;
        }

        private void chkLosAlarmL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr09() < 0)
                return;
        }

        private void chkLosMaskL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr09() < 0)
                return;
        }

        private void chkPdLosL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr09() < 0)
                return;
        }

        private void chkLosL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr0C() < 0)
                return;
        }

        private void chkTxDisableAutoMuteL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr0D() < 0)
                return;
        }

        private void chkTxDisableL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr0D() < 0)
                return;
        }

        private void chkDriverPkAvgSelL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr17() < 0)
                return;
        }

        private void chkChannelToAdcL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr18() < 0)
                return;
        }

        private void chkTxIgnoreFaultL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr0E() < 0)
                return;
        }

        private void chkTxFaultAlarmL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr0E() < 0)
                return;
        }

        private void cboTxFaultStateL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr0E() < 0)
                return;
        }

        private void chkTxFaultL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr0E() < 0)
                return;
        }

        private void chkTxFaultMaskL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr0E() < 0)
                return;
        }

        private void cboRVcselL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr10() < 0)
                return;

        }

        private void cboIBiasL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr11() < 0)
                return;
        }

        private void cboTailCurrentL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr14() < 0)
                return;
        }

        private void cboIBurnInL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr12() < 0)
                return;
        }

        private void chkTxBurnInEnableL2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr12() < 0)
                return;
        }

        private void cboTxEqL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr15() < 0)
                return;
        }

        private void cboTxEqGainRangeL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page12);
            if (_WritePage12Addr16() < 0)
                return;
        }



        #endregion

        #region L3 Event
        private void chkPowerDownL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr00() < 0)
                return;

        }

        private void cboLowGainEnableL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr15() < 0)
                return;
        }

        private void cboVgaGainStg1L3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr01() < 0)
                return;
        }

        private void cboVgaGainStg2L3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr04() < 0)
                return;
        }

        private void cboVga1GainChangeL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr02() < 0)
                return;
        }

        private void cboVga2GainChangeL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr05() < 0)
                return;
        }

        private void cboVga1GainChangePolarityL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr02() < 0)
                return;
        }

        private void cboVga2GainChangePolarityL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr05() < 0)
                return;
        }

        private void cboVgaTailStg1L3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr01() < 0)
                return;
        }

        private void cboVgaTailStg2L3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr04() < 0)
                return;
        }

        private void cboVgaVcmAdjL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr08() < 0)
                return;
        }

        private void cboVga1BandwidthChangePolarityL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr03() < 0)
                return;
        }

        private void cboVga1BandwidthChangeL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr03() < 0)
                return;
        }

        private void cboVga2BandwidthChangePolarityL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr06() < 0)
                return;
        }

        private void cboVga2BandwidthChangeL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr06() < 0)
                return;
        }

        private void cboLosthrshystL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr09() < 0)
                return;
        }

        private void cboLosthrsL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr09() < 0)
                return;
        }

        private void chkLosAlarmL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr09() < 0)
                return;
        }

        private void chkLosMaskL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr09() < 0)
                return;
        }

        private void chkPdLosL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr09() < 0)
                return;
        }

        private void chkLosL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr0C() < 0)
                return;
        }

        private void chkTxDisableAutoMuteL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr0D() < 0)
                return;
        }

        private void chkTxDisableL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr0D() < 0)
                return;
        }

        private void chkDriverPkAvgSelL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr17() < 0)
                return;
        }

        private void chkChannelToAdcL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr18() < 0)
                return;
        }

        private void chkTxIgnoreFaultL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr0E() < 0)
                return;
        }

        private void chkTxFaultAlarmL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr0E() < 0)
                return;
        }

        private void cboTxFaultStateL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr0E() < 0)
                return;
        }

        private void chkTxFaultL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr0E() < 0)
                return;
        }

        private void chkTxFaultMaskL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr0E() < 0)
                return;
        }

        private void cboRVcselL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr10() < 0)
                return;
        }

        private void cboIBiasL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr11() < 0)
                return;
        }

        private void cboTailCurrentL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr14 () < 0)
                return;
        }

        private void cboIBurnInL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr12() < 0)
                return;
        }

        private void chkTxBurnInEnableL3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr12() < 0)
                return;
        }

        private void cboTxEqL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr15() < 0)
                return;
        }

        private void cboTxEqGainRangeL3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page13);
            if (_WritePage13Addr16() < 0)
                return;
        }

        #endregion

        #region Page All Event

        private void cboIntrptPadPolarity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page00);
            if (_WritePage00Addr03() < 0)
                return;
        }

        private void cboIntrptoen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page00);
            if (_WritePage00Addr03() < 0)
                return;
        }

        private void cboI2cAddressMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page00);
            if (_WritePage00Addr03() < 0)
                return;
        }

        private void chkPowerDownAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr00() < 0)
                return;
        }

        private void cboVgaTailStg1All_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr01() < 0)
                return;
        }

        private void cboVgaGainStg1All_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr01() < 0)
                return;
        }

        private void cboVga1GainChangePolarityAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr02() < 0)
                return;
        }

        private void cboVga1GainChangeAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr02() < 0)
                return;
        }

        private void cboVga1BandwidthChangePolarityAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr03() < 0)
                return;
        }

        private void cboVga1BandwidthChangeAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr03() < 0)
                return;
        }

        private void cboVgaTailStg2All_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr04() < 0)
                return;
        }

        private void cboVgaGainStg2All_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr04() < 0)
                return;
        }

        private void cboVga2GainChangePolarityAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr05() < 0)
                return;
        }

        private void cboVga2GainChangeAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr05() < 0)
                return;
        }

        private void cboVga2BandwidthChangePolarityAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr06() < 0)
                return;
        }

        private void cboVga2BandwidthChangeAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr06() < 0)
                return;
        }

        private void cboVgaVcmAdjAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr08() < 0)
                return;
        }

        private void cboLosthrshystAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr09() < 0)
                return;
        }

        private void cboLosthrsAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr09() < 0)
                return;
        }

        private void chkLosAlarmAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr09() < 0)
                return;
        }

        private void chkLosMaskAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr09() < 0)
                return;
        }

        private void chkPdLosAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr09() < 0)
                return;
        }

        private void chkTxDisableAutoMuteAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr0D() < 0)
                return;
        }

        private void chkTxDisableAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr0D() < 0)
                return;
        }

        private void chkTxIgnoreFaultAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr0E() < 0)
                return;
        }

        private void chkTxFaultAlarmAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr0E() < 0)
                return;
        }

        private void cboTxFaultStateAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr0E() < 0)
                return;
        }

        private void chkTxFaultAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr0E() < 0)
                return;
        }

        private void chkTxFaultMaskAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr0E() < 0)
                return;
        }

        private void chkIgnoreTxDisablePinAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr0F() < 0)
                return;
        }

        private void chkTxAlarmClearInterruptAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr0F() < 0)
                return;
        }

        private void chkTxAlarmClearPinAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr0F() < 0)
                return;
        }

        private void chkTxAlarmClearRegisterAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr0F() < 0)
                return;
        }

        private void cboRVcselAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr10() < 0)
                return;
        }

        private void cboIBiasAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr11() < 0)
                return;
        }

        private void chkTxBurnInEnableAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr12() < 0)
                return;
        }

        private void cboIBurnInAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr12() < 0)
                return;
        }

        private void cboTailCurrentAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr14() < 0)
                return;
        }

        private void cboLowGainEnableAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr15() < 0)
                return;
        }

        private void cboTxEqAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr15() < 0)
                return;
        }

        private void cboTxEqGainRangeAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr16() < 0)
                return;
        }

        private void chkDriverPkAvgSelAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr17() < 0)
                return;
        }

        private void chkTxIBiasAvgAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr1B() < 0)
                return;
        }

        private void chkTxIBiasRefAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr1B() < 0)
                return;
        }

        private void chkTxVcc3V3All_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page0F);
            if (_WritePage0FAddr1B() < 0)
                return;
        }

        private void chkAdcReadFreezeAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page30);
            if (_WritePage30Addr00() < 0)
                return;
        }

        private void cboAdcSelectAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page30);
            if (_WritePage30Addr00() < 0)
                return;
        }

        private void chkAdcPowerDownAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            SetPage(Page30);
            if (_WritePage30Addr01() < 0)
                return;
        }



        #endregion

        #region Page Customer Event
        private void cboL0InputEqualization0db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrA0() < 0)
                return;
        }

        private void cboL0InputEqualization1db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrA1() < 0)
                return;

        }

        private void cboL0InputEqualization2db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrA2() < 0)
                return;
        }

        private void cboL0InputEqualization3db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrA3() < 0)
                return;
        }

        private void cboL0InputEqualization4db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrA4() < 0)
                return;
        }

        private void cboL0InputEqualization5db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrA5() < 0)
                return;
        }

        private void cboL0InputEqualization6db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrA6() < 0)
                return;
        }

        private void cboL0InputEqualization7db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrA7() < 0)
                return;
        }

        private void cboL0InputEqualization8db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrA8() < 0)
                return;
        }

        private void cboL0InputEqualization9db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrA9() < 0)
                return;
        }

        private void cboL0InputEqualization10db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrAA() < 0)
                return;
        }

        private void cboL0InputEqualizationReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrAB() < 0)
                return;
        }

        private void cboL0InputEqualizationReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrAC() < 0)
                return;
        }

        private void cboL1InputEqualization0db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrAD() < 0)
                return;
        }

        private void cboL1InputEqualization1db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrAE() < 0)
                return;
        }

        private void cboL1InputEqualization2db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrAF() < 0)
                return;
        }

        private void cboL1InputEqualization3db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrB0() < 0)
                return;
        }

        private void cboL1InputEqualization4db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrB1() < 0)
                return;
        }

        private void cboL1InputEqualization5db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrB2() < 0)
                return;
        }

        private void cboL1InputEqualization6db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrB3() < 0)
                return;
        }

        private void cboL1InputEqualization7db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrB4() < 0)
                return;
        }

        private void cboL1InputEqualization8db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrB5() < 0)
                return;
        }

        private void cboL1InputEqualization9db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrB6() < 0)
                return;
        }

        private void cboL1InputEqualization10db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrB7() < 0)
                return;
        }

        private void cboL1InputEqualizationReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrB8() < 0)
                return;
        }

        private void cboL1InputEqualizationReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrB9() < 0)
                return;
        }

        private void cboL2InputEqualization0db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrBA() < 0)
                return;
        }

        private void cboL2InputEqualization1db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrBB() < 0)
                return;
        }

        private void cboL2InputEqualization2db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrBC() < 0)
                return;
        }

        private void cboL2InputEqualization3db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrBD() < 0)
                return;
        }

        private void cboL2InputEqualization4db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrBE() < 0)
                return;
        }

        private void cboL2InputEqualization5db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrBF() < 0)
                return;
        }

        private void cboL2InputEqualization6db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrC0() < 0)
                return;
        }

        private void cboL2InputEqualization7db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrC1() < 0)
                return;
        }

        private void cboL2InputEqualization8db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrC2() < 0)
                return;
        }

        private void cboL2InputEqualization9db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrC3() < 0)
                return;
        }

        private void cboL2InputEqualization10db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrC4() < 0)
                return;
        }

        private void cboL2InputEqualizationReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrC5() < 0)
                return;
        }

        private void cboL2InputEqualizationReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrC6() < 0)
                return;
        }

        private void cboL3InputEqualization0db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrC7() < 0)
                return;
        }

        private void cboL3InputEqualization1db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrC8() < 0)
                return;
        }

        private void cboL3InputEqualization2db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrC9() < 0)
                return;
        }

        private void cboL3InputEqualization3db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrCA() < 0)
                return;
        }

        private void cboL3InputEqualization4db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrCB() < 0)
                return;
        }

        private void cboL3InputEqualization5db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrCC() < 0)
                return;
        }

        private void cboL3InputEqualization6db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrCD() < 0)
                return;
        }

        private void cboL3InputEqualization7db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrCE() < 0)
                return;
        }

        private void cboL3InputEqualization8db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrCF() < 0)
                return;
        }

        private void cboL3InputEqualization9db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrD0() < 0)
                return;
        }

        private void cboL3InputEqualization10db_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrD1() < 0)
                return;
        }

        private void cboL3InputEqualizationReserved0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrD2() < 0)
                return;
        }

        private void cboL3InputEqualizationReserved1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePageAAAddrD3() < 0)
                return;
        }

        #endregion
    }
}
