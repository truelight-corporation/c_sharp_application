using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rt145Rt146Config
{
    public partial class UcRt146Config : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private const byte devAddr = 0x3A;
        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private bool reading = false;

        public string GetComboBoxSelectedValue(string comboBoxId)
        {
            ComboBox comboBox = this.Controls.Find(comboBoxId, true).FirstOrDefault() as ComboBox;
            if (comboBox != null)
                return comboBox.SelectedItem?.ToString();
            else
                throw new ArgumentException("Invalid ComboBox ID");
        }

        public UcRt146Config()
        {
            InitializeComponent();

            ComboBoxItem item;
            double dTmp , dTmp2;
            int i;

            item = new ComboBoxItem();
            item.Text = "nomal";
            item.Value = 0;
            cbNlatFaultSTCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "low";
            item.Value = 1;
            cbNlatFaultSTCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "high";
            item.Value = 2;
            cbNlatFaultSTCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "nc";
            item.Value = 3;
            cbNlatFaultSTCh0.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "nomal";
            item.Value = 0;
            cbNlatFaultSTCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "low";
            item.Value = 1;
            cbNlatFaultSTCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "high";
            item.Value = 2;
            cbNlatFaultSTCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "nc";
            item.Value = 3;
            cbNlatFaultSTCh1.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "nomal";
            item.Value = 0;
            cbNlatFaultSTCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "low";
            item.Value = 1;
            cbNlatFaultSTCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "high";
            item.Value = 2;
            cbNlatFaultSTCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "nc";
            item.Value = 3;
            cbNlatFaultSTCh2.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "nomal";
            item.Value = 0;
            cbNlatFaultSTCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "low";
            item.Value = 1;
            cbNlatFaultSTCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "high";
            item.Value = 2;
            cbNlatFaultSTCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "nc";
            item.Value = 3;
            cbNlatFaultSTCh3.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：10~30u";
            item.Value = 0;
            cbAgcRssi.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：20~60u";
            item.Value = 1;
            cbAgcRssi.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：40~120u";
            item.Value = 2;
            cbAgcRssi.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：80~240u";
            item.Value = 3;
            cbAgcRssi.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：160~480u";
            item.Value = 4;
            cbAgcRssi.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：320~960u";
            item.Value = 5;
            cbAgcRssi.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：640~1920u";
            item.Value = 6;
            cbAgcRssi.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：1280~3840u";
            item.Value = 7;
            cbAgcRssi.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：open drain";
            item.Value = 0;
            cbIntrptType.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：cmos";
            item.Value = 1;
            cbIntrptType.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：active high";
            item.Value = 0;
            cbIntrptPolarity.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：active low";
            item.Value = 1;
            cbIntrptPolarity.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：lane0";
            item.Value = 0;
            cbSelectVdiop.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：lane1";
            item.Value = 1;
            cbSelectVdiop.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：lane2";
            item.Value = 2;
            cbSelectVdiop.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：lane3";
            item.Value = 3;
            cbSelectVdiop.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：sel";
            item.Value = 0;
            cbModeImon.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：sum";
            item.Value = 1;
            cbModeImon.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：lane0";
            item.Value = 0;
            cbSelectImon.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：lane1";
            item.Value = 1;
            cbSelectImon.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：lane2";
            item.Value = 2;
            cbSelectImon.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：lane3";
            item.Value = 3;
            cbSelectImon.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.45-0.95";
            item.Value = 0;
            cbAutoTuneLockTh.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.50-0.90";
            item.Value = 1;
            cbAutoTuneLockTh.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.55-0.85";
            item.Value = 2;
            cbAutoTuneLockTh.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：0.60-0.80";
            item.Value = 3;
            cbAutoTuneLockTh.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.30-1.10";
            item.Value = 0;
            cbAutoTuneUnlockTh.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.35-1.05";
            item.Value = 1;
            cbAutoTuneUnlockTh.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.40-1.00";
            item.Value = 2;
            cbAutoTuneUnlockTh.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：0.45-0.95";
            item.Value = 3;
            cbAutoTuneUnlockTh.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：18MHz";
            item.Value = 0;
            cbCdrLoopBandWidth.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：15MHz";
            item.Value = 1;
            cbCdrLoopBandWidth.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：12MHz";
            item.Value = 2;
            cbCdrLoopBandWidth.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：6MHz";
            item.Value = 3;
            cbCdrLoopBandWidth.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：32MHz";
            item.Value = 4;
            cbCdrLoopBandWidth.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：28MHz";
            item.Value = 5;
            cbCdrLoopBandWidth.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：24MHz";
            item.Value = 6;
            cbCdrLoopBandWidth.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：20MHz";
            item.Value = 7;
            cbCdrLoopBandWidth.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：off";
            item.Value = 0;
            cbSwitchBistVref.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：on";
            item.Value = 1;
            cbSwitchBistVref.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：off";
            item.Value = 0;
            cbSwitchBistVdet.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：on";
            item.Value = 1;
            cbSwitchBistVdet.Items.Add(item);
            
            for (i = 0, dTmp = 30; i < 6; i++, dTmp += 15)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + "mV";
                item.Value = i;
                cbLosThresholdCh0.Items.Add(item);
            }
            for (i = 6; i < 15; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：Reserved";
                item.Value = i;
                cbLosThresholdCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 30; i < 6; i++, dTmp += 15)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + "mV";
                item.Value = i;
                cbLosThresholdCh1.Items.Add(item);
            }
            for (i = 6; i < 15; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：Reserved";
                item.Value = i;
                cbLosThresholdCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 30; i < 6; i++, dTmp += 15)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + "mV";
                item.Value = i;
                cbLosThresholdCh2.Items.Add(item);
            }
            for (i = 6; i < 15; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：Reserved";
                item.Value = i;
                cbLosThresholdCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 30; i < 6; i++, dTmp += 15)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + "mV";
                item.Value = i;
                cbLosThresholdCh3.Items.Add(item);
            }
            for (i = 6; i < 15; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：Reserved";
                item.Value = i;
                cbLosThresholdCh3.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++ , dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqPeakCh0.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqPeakCh0.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqPeakCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqPeakCh1.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqPeakCh1.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqPeakCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqPeakCh2.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqPeakCh2.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqPeakCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqPeakCh3.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqPeakCh3.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqPeakCh3.Items.Add(item);
            }

            for (i = 0, dTmp = 50; i < 8; i++, dTmp += 1.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " %";
                item.Value = i;
                cbCrossPointCh0.Items.Add(item);
            }
            for (i = 8, dTmp = 50; i < 16; i++, dTmp -= 1.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " %";
                item.Value = i;
                cbCrossPointCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 50; i < 8; i++, dTmp += 1.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " %";
                item.Value = i;
                cbCrossPointCh1.Items.Add(item);
            }
            for (i = 8, dTmp = 50; i < 16; i++, dTmp -= 1.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " %";
                item.Value = i;
                cbCrossPointCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 50; i < 8; i++, dTmp += 1.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " %";
                item.Value = i;
                cbCrossPointCh2.Items.Add(item);
            }
            for (i = 8, dTmp = 50; i < 16; i++, dTmp -= 1.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " %";
                item.Value = i;
                cbCrossPointCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 50; i < 8; i++, dTmp += 1.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " %";
                item.Value = i;
                cbCrossPointCh3.Items.Add(item);
            }
            for (i = 8, dTmp = 50; i < 16; i++, dTmp -= 1.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " %";
                item.Value = i;
                cbCrossPointCh3.Items.Add(item);
            }

            item = new ComboBoxItem();
            item.Text = "0：min.";
            item.Value = 0;
            cbDeEmphasisCh0.Items.Add(item);
            for (i = 1; i < 15; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + i;
                item.Value = i;
                cbDeEmphasisCh0.Items.Add(item);
            }
            item = new ComboBoxItem();
            item.Text = "15：max.";
            item.Value = 15;
            cbDeEmphasisCh0.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：min.";
            item.Value = 0;
            cbDeEmphasisCh1.Items.Add(item);
            for (i = 1; i < 15; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + i;
                item.Value = i;
                cbDeEmphasisCh1.Items.Add(item);
            }
            item = new ComboBoxItem();
            item.Text = "15：max.";
            item.Value = 15;
            cbDeEmphasisCh1.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：min.";
            item.Value = 0;
            cbDeEmphasisCh2.Items.Add(item);
            for (i = 1; i < 15; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + i;
                item.Value = i;
                cbDeEmphasisCh2.Items.Add(item);
            }
            item = new ComboBoxItem();
            item.Text = "15：max.";
            item.Value = 15;
            cbDeEmphasisCh2.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：min.";
            item.Value = 0;
            cbDeEmphasisCh3.Items.Add(item);
            for (i = 1; i < 15; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + i;
                item.Value = i;
                cbDeEmphasisCh3.Items.Add(item);
            }
            item = new ComboBoxItem();
            item.Text = "15：max.";
            item.Value = 15;
            cbDeEmphasisCh3.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：normal";
            item.Value = 0;
            cbAutoBypassResetCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：reset";
            item.Value = 1;
            cbAutoBypassResetCh0.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：+0";
            item.Value = 0;
            cbVcoMsbSelecCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：+8";
            item.Value = 1;
            cbVcoMsbSelecCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：+16";
            item.Value = 2;
            cbVcoMsbSelecCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：+16";
            item.Value = 3;
            cbVcoMsbSelecCh0.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：normal";
            item.Value = 0;
            cbAutoBypassResetCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：reset";
            item.Value = 1;
            cbAutoBypassResetCh1.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：+0";
            item.Value = 0;
            cbVcoMsbSelecCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：+8";
            item.Value = 1;
            cbVcoMsbSelecCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：+16";
            item.Value = 2;
            cbVcoMsbSelecCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：+16";
            item.Value = 3;
            cbVcoMsbSelecCh1.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：normal";
            item.Value = 0;
            cbAutoBypassResetCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：reset";
            item.Value = 1;
            cbAutoBypassResetCh2.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：+0";
            item.Value = 0;
            cbVcoMsbSelecCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：+8";
            item.Value = 1;
            cbVcoMsbSelecCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：+16";
            item.Value = 2;
            cbVcoMsbSelecCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：+16";
            item.Value = 3;
            cbVcoMsbSelecCh2.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：normal";
            item.Value = 0;
            cbAutoBypassResetCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：reset";
            item.Value = 1;
            cbAutoBypassResetCh3.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：+0";
            item.Value = 0;
            cbVcoMsbSelecCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：+8";
            item.Value = 1;
            cbVcoMsbSelecCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：+16";
            item.Value = 2;
            cbVcoMsbSelecCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：+16";
            item.Value = 3;
            cbVcoMsbSelecCh3.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：on";
            item.Value = 0;
            cbDdmiAdcPowerControl.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：off";
            item.Value = 1;
            cbDdmiAdcPowerControl.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：temp";
            item.Value = 0;
            cbDdmiChannelSelect.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：vcc";
            item.Value = 1;
            cbDdmiChannelSelect.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：vf";
            item.Value = 2;
            cbDdmiChannelSelect.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：vref_bist";
            item.Value = 3;
            cbDdmiChannelSelect.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：vpd_bist";
            item.Value = 4;
            cbDdmiChannelSelect.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：rssi";
            item.Value = 5;
            cbDdmiChannelSelect.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：debug";
            item.Value = 6;
            cbDdmiChannelSelect.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：nc";
            item.Value = 7;
            cbDdmiChannelSelect.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：640K";
            item.Value = 0;
            cbRssiAgcClockSpeed.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：320K";
            item.Value = 1;
            cbRssiAgcClockSpeed.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：5K";
            item.Value = 2;
            cbRssiAgcClockSpeed.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：600";
            item.Value = 3;
            cbRssiAgcClockSpeed.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：auto";
            item.Value = 0;
            cbRssiMode.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：manual";
            item.Value = 1;
            cbRssiMode.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：10-30u";
            item.Value = 0;
            cbRssiLevelSelect.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：20-60u";
            item.Value = 1;
            cbRssiLevelSelect.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：40-120u";
            item.Value = 2;
            cbRssiLevelSelect.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：80-240u";
            item.Value = 3;
            cbRssiLevelSelect.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：160-480u";
            item.Value = 4;
            cbRssiLevelSelect.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：320-960u";
            item.Value = 5;
            cbRssiLevelSelect.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：640-1920u";
            item.Value = 6;
            cbRssiLevelSelect.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：1280-3840u";
            item.Value = 7;
            cbRssiLevelSelect.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：vbg";
            item.Value = 0;
            cbDdmiIdoRefSource.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：rssi";
            item.Value = 1;
            cbDdmiIdoRefSource.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：manual";
            item.Value = 0;
            cbIdoModeSelect.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：auto";
            item.Value = 1;
            cbIdoModeSelect.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：-offset";
            item.Value = 0;
            cbTemperatureOffset.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：";
            item.Value = 1;
            cbTemperatureOffset.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：";
            item.Value = 2;
            cbTemperatureOffset.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：";
            item.Value = 3;
            cbTemperatureOffset.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：";
            item.Value = 4;
            cbTemperatureOffset.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：0";
            item.Value = 5;
            cbTemperatureOffset.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：";
            item.Value = 6;
            cbTemperatureOffset.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：+offset";
            item.Value = 7;
            cbTemperatureOffset.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：flat";
            item.Value = 0;
            cbTemperatureSlope.Items.Add(item);
            for (i = 1; i < 7; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + i;
                item.Value = i;
                cbTemperatureSlope.Items.Add(item);
            }
            item = new ComboBoxItem();
            item.Text = "7：steep";
            item.Value = 7;
            cbTemperatureSlope.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：on";
            item.Value = 0;
            cbDdmiPwd.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：off";
            item.Value = 1;
            cbDdmiPwd.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：on";
            item.Value = 0;
            cbTemperaturePwd.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：off";
            item.Value = 1;
            cbTemperaturePwd.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：on";
            item.Value = 0;
            cbSourceIdoPwd.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：off";
            item.Value = 1;
            cbSourceIdoPwd.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：off";
            item.Value = 0;
            cbEnableVf.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：on";
            item.Value = 1;
            cbEnableVf.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：normal";
            item.Value = 0;
            cbDdmiReset.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：reset";
            item.Value = 1;
            cbDdmiReset.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：ignore";
            item.Value = 0;
            cbIgnoreGlobalPwd.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：normal";
            item.Value = 1;
            cbIgnoreGlobalPwd.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：off";
            item.Value = 0;
            cbCh0SahPwd.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：on";
            item.Value = 1;
            cbCh0SahPwd.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：on";
            item.Value = 0;
            cbRingOscPwd.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：off";
            item.Value = 1;
            cbRingOscPwd.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：on";
            item.Value = 0;
            cbClockAutoTuneEnable.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：off";
            item.Value = 1;
            cbClockAutoTuneEnable.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：on";
            item.Value = 0;
            cbClockAdcEnable.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：off";
            item.Value = 1;
            cbClockAdcEnable.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：on";
            item.Value = 0;
            cbClockLosEnable.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：off";
            item.Value = 1;
            cbClockLosEnable.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：/1";
            item.Value = 0;
            cbSampleHoldClockSpeed.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：/2";
            item.Value = 1;
            cbSampleHoldClockSpeed.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：/4";
            item.Value = 2;
            cbSampleHoldClockSpeed.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：/8";
            item.Value = 3;
            cbSampleHoldClockSpeed.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：/1";
            item.Value = 0;
            cbAutoTuneClockSpeed.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：/2";
            item.Value = 1;
            cbAutoTuneClockSpeed.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：/4";
            item.Value = 2;
            cbAutoTuneClockSpeed.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：/8";
            item.Value = 3;
            cbAutoTuneClockSpeed.Items.Add(item);

            for (i = 0, dTmp = 2; i < 128; i++, dTmp +=0.1)
            {
                dTmp2 = Math.Round(dTmp, 1);
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp2 + " mA";
                item.Value = i;
                cbIbiasCurrentCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 2; i < 128; i++, dTmp += 0.1)
            {
                dTmp2 = Math.Round(dTmp, 1);
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp2 + " mA";
                item.Value = i;
                cbIbiasCurrentCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 2; i < 128; i++, dTmp += 0.1)
            {
                dTmp2 = Math.Round(dTmp, 1);
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp2 + " mA";
                item.Value = i;
                cbIbiasCurrentCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 2; i < 128; i++, dTmp += 0.1)
            {
                dTmp2 = Math.Round(dTmp, 1);
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp2 + " mA";
                item.Value = i;
                cbIbiasCurrentCh3.Items.Add(item);
            }

            for (i = 0, dTmp = 0.1; i < 128; i++, dTmp += 0.091)
            {
                dTmp2 = Math.Round(dTmp, 1);
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp2 + " mA";
                item.Value = i;
                cbModulationCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 0.1; i < 128; i++, dTmp += 0.091)
            {
                dTmp2 = Math.Round(dTmp, 1);
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp2 + " mA";
                item.Value = i;
                cbModulationCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 0.1; i < 128; i++, dTmp += 0.091)
            {
                dTmp2 = Math.Round(dTmp, 1);
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp2 + " mA";
                item.Value = i;
                cbModulationCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 0.1; i < 128; i++, dTmp += 0.091)
            {
                dTmp2 = Math.Round(dTmp, 1);
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp2 + " mA";
                item.Value = i;
                cbModulationCh3.Items.Add(item);
            }

            for (i = 0, dTmp = 0.1; i < 128; i++, dTmp += 0.11)
            {
                dTmp2 = Math.Round(dTmp, 1);
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp2 + " mA";
                item.Value = i;
                cbBurninCurrentCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 0.1; i < 128; i++, dTmp += 0.11)
            {
                dTmp2 = Math.Round(dTmp, 1);
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp2 + " mA";
                item.Value = i;
                cbBurninCurrentCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 0.1; i < 128; i++, dTmp += 0.11)
            {
                dTmp2 = Math.Round(dTmp, 1);
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp2 + " mA";
                item.Value = i;
                cbBurninCurrentCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 0.1; i < 128; i++, dTmp += 0.11)
            {
                dTmp2 = Math.Round(dTmp, 1);
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp2 + " mA";
                item.Value = i;
                cbBurninCurrentCh3.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization0dbCh0.Items.Add(item);
            }                        
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization0dbCh0.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization0dbCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization1dbCh0.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization1dbCh0.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization1dbCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization2dbCh0.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization2dbCh0.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization2dbCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization3dbCh0.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization3dbCh0.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization3dbCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization4dbCh0.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization4dbCh0.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization4dbCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization5dbCh0.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization5dbCh0.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization5dbCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization6dbCh0.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization6dbCh0.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization6dbCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization7dbCh0.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization7dbCh0.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization7dbCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization8dbCh0.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization8dbCh0.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization8dbCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization9dbCh0.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization9dbCh0.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization9dbCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization10dbCh0.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization10dbCh0.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization10dbCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR0Ch0.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR0Ch0.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR0Ch0.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR1Ch0.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR1Ch0.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR1Ch0.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization0dbCh1.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization0dbCh1.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization0dbCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization1dbCh1.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization1dbCh1.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization1dbCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization2dbCh1.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization2dbCh1.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization2dbCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization3dbCh1.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization3dbCh1.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization3dbCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization4dbCh1.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization4dbCh1.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization4dbCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization5dbCh1.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization5dbCh1.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization5dbCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization6dbCh1.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization6dbCh1.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization6dbCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization7dbCh1.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization7dbCh1.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization7dbCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization8dbCh1.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization8dbCh1.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization8dbCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization9dbCh1.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization9dbCh1.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization9dbCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization10dbCh1.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization10dbCh1.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization10dbCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR0Ch1.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR0Ch1.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR0Ch1.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR1Ch1.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR1Ch1.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR1Ch1.Items.Add(item);
            }


            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization0dbCh2.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization0dbCh2.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization0dbCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization1dbCh2.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization1dbCh2.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization1dbCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization2dbCh2.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization2dbCh2.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization2dbCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization3dbCh2.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization3dbCh2.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization3dbCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization4dbCh2.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization4dbCh2.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization4dbCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization5dbCh2.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization5dbCh2.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization5dbCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization6dbCh2.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization6dbCh2.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization6dbCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization7dbCh2.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization7dbCh2.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization7dbCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization8dbCh2.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization8dbCh2.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization8dbCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization9dbCh2.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization9dbCh2.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization9dbCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization10dbCh2.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization10dbCh2.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization10dbCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR0Ch2.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR0Ch2.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR0Ch2.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR1Ch2.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR1Ch2.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR1Ch2.Items.Add(item);
            }


            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization0dbCh3.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization0dbCh3.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization0dbCh3.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization1dbCh3.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization1dbCh3.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization1dbCh3.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization2dbCh3.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization2dbCh3.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization2dbCh3.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization3dbCh3.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization3dbCh3.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization3dbCh3.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization4dbCh3.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization4dbCh3.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization4dbCh3.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization5dbCh3.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization5dbCh3.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization5dbCh3.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization6dbCh3.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization6dbCh3.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization6dbCh3.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization7dbCh3.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization7dbCh3.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization7dbCh3.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization8dbCh3.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization8dbCh3.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization8dbCh3.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization9dbCh3.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization9dbCh3.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization9dbCh3.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization10dbCh3.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization10dbCh3.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualization10dbCh3.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR0Ch3.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR0Ch3.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR0Ch3.Items.Add(item);
            }

            for (i = 0, dTmp = 0.5; i < 9; i++, dTmp += 1)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR1Ch3.Items.Add(item);
            }
            for (i = 9, dTmp = 9; i < 13; i++, dTmp += 0.5)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR1Ch3.Items.Add(item);
            }
            for (i = 13, dTmp = 10.5; i < 16; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + " dB";
                item.Value = i;
                cbEqualizationR1Ch3.Items.Add(item);
            }
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

        public string GetChipId()
        {
            if (this.InvokeRequired)
                return (string)this.Invoke(new Action(() => _GetChipId()));
            else
                return _GetChipId();
        }

        private byte _ReverseBit(byte data){
            data = (byte)(((data & 0xF0) >> 4) | (data & 0x0F) << 4);
            data = (byte)(((data & 0xCC) >> 2) | (data & 0x33) << 2);
            data = (byte)(((data & 0xAA) >> 1) | (data & 0x55) << 1);
            return data;
        }

        private void _ParseAddr00(byte data)
        {
            //data = _ReverseBit(data);

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

        private void _ParseAddr01(byte data){
            //data = _ReverseBit(data);

            if ((data & 0x01) == 0)
                tbLOSCh0.BackColor = Color.LightGreen;
            else
                tbLOSCh0.BackColor = Color.Pink;

            if ((data & 0x02) == 0)
                tbLOSCh1.BackColor = Color.LightGreen;
            else
                tbLOSCh1.BackColor = Color.Pink;

            if ((data & 0x04) == 0)
                tbLOSCh2.BackColor = Color.LightGreen;
            else
                tbLOSCh2.BackColor = Color.Pink;

            if ((data & 0x08) == 0)
                tbLOSCh3.BackColor = Color.LightGreen;
            else
                tbLOSCh3.BackColor = Color.Pink;

            if ((data & 0x10) == 0)
                tbLOLCh0.BackColor = Color.LightGreen;
            else
                tbLOLCh0.BackColor = Color.Pink;

            if ((data & 0x20) == 0)
                tbLOLCh1.BackColor = Color.LightGreen;
            else
                tbLOLCh1.BackColor = Color.Pink;

            if ((data & 0x40) == 0)
                tbLOLCh2.BackColor = Color.LightGreen;
            else
                tbLOLCh2.BackColor = Color.Pink;

            if ((data & 0x80) == 0)
                tbLOLCh3.BackColor = Color.LightGreen;
            else
                tbLOLCh3.BackColor = Color.Pink;
        }

        private void _ParseAddr02(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x01) == 0)
                tbLOSorLOLCh0.BackColor = Color.LightGreen;
            else
                tbLOSorLOLCh0.BackColor = Color.Pink;

            if ((data & 0x02) == 0)
                tbLOSorLOLCh1.BackColor = Color.LightGreen;
            else
                tbLOSorLOLCh1.BackColor = Color.Pink;

            if ((data & 0x04) == 0)
                tbLOSorLOLCh2.BackColor = Color.LightGreen;
            else
                tbLOSorLOLCh2.BackColor = Color.Pink;

            if ((data & 0x08) == 0)
                tbLOSorLOLCh3.BackColor = Color.LightGreen;
            else
                tbLOSorLOLCh3.BackColor = Color.Pink;

            if ((data & 0x10) == 0)
                tbFaultCh0.BackColor = Color.LightGreen;
            else
                tbFaultCh0.BackColor = Color.Pink;

            if ((data & 0x20) == 0)
                tbFaultCh1.BackColor = Color.LightGreen;
            else
                tbFaultCh1.BackColor = Color.Pink;

            if ((data & 0x40) == 0)
                tbFaultCh2.BackColor = Color.LightGreen;
            else
                tbFaultCh2.BackColor = Color.Pink;

            if ((data & 0x80) == 0)
                tbFaultCh3.BackColor = Color.LightGreen;
            else
                tbFaultCh3.BackColor = Color.Pink;
        }

        private void _ParseAddr03(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x01) == 0)
                tbNlatLOSCh0.BackColor = Color.LightGreen;
            else
                tbNlatLOSCh0.BackColor = Color.Pink;

            if ((data & 0x02) == 0)
                tbNlatLOSCh1.BackColor = Color.LightGreen;
            else
                tbNlatLOSCh1.BackColor = Color.Pink;

            if ((data & 0x04) == 0)
                tbNlatLOSCh2.BackColor = Color.LightGreen;
            else
                tbNlatLOSCh2.BackColor = Color.Pink;

            if ((data & 0x08) == 0)
                tbNlatLOSCh3.BackColor = Color.LightGreen;
            else
                tbNlatLOSCh3.BackColor = Color.Pink;

            if ((data & 0x10) == 0)
                tbNlatLOLCh0.BackColor = Color.LightGreen;
            else
                tbNlatLOLCh0.BackColor = Color.Pink;

            if ((data & 0x20) == 0)
                tbNlatLOLCh1.BackColor = Color.LightGreen;
            else
                tbNlatLOLCh1.BackColor = Color.Pink;

            if ((data & 0x40) == 0)
                tbNlatLOLCh2.BackColor = Color.LightGreen;
            else
                tbNlatLOLCh2.BackColor = Color.Pink;

            if ((data & 0x80) == 0)
                tbNlatLOLCh3.BackColor = Color.LightGreen;
            else
                tbNlatLOLCh3.BackColor = Color.Pink;
        }

        private void _ParseAddr04_05(byte data0, byte data1)
        {

#if false
            data0 = (byte)(((data0 & 0xF0) >> 4) | (data0 & 0x0F) << 4);
            data0 = (byte)(((data0 & 0xCC) >> 2) | (data0 & 0x33) << 2);
            data0 = (byte)(((data0 & 0xAA) >> 1) | (data0 & 0x55) << 1);
            data1 = (byte)(((data1 & 0xF0) >> 4) | (data1 & 0x0F) << 4);
            data1 = (byte)(((data1 & 0xCC) >> 2) | (data1 & 0x33) << 2);
            data1 = (byte)(((data1 & 0xAA) >> 1) | (data1 & 0x55) << 1);
#endif

            foreach (ComboBoxItem item in cbAdcOut.Items) {
                int iTmp;

                iTmp = (data0 & 0xFF) | ((data1 & 0x01) << 8);

                if (iTmp == item.Value)
                    cbAdcOut.SelectedItem = item;
                
            }

            foreach (ComboBoxItem item in cbAgcRssi.Items) {
                if ((data1 >> 1 & 0x07) == item.Value)
                    cbAgcRssi.SelectedItem = item;
            }

            if ((data1 & 0x10) == 0)
                tbNlatFalutCh0.BackColor = Color.LightGreen;
            else
                tbNlatFalutCh0.BackColor = Color.Pink;

            if ((data1 & 0x20) == 0)
                tbNlatFalutCh1.BackColor = Color.LightGreen;
            else
                tbNlatFalutCh1.BackColor = Color.Pink;

            if ((data1 & 0x40) == 0)
                tbNlatFalutCh2.BackColor = Color.LightGreen;
            else
                tbNlatFalutCh2.BackColor = Color.Pink;

            if ((data1 & 0x80) == 0)
                tbNlatFalutCh3.BackColor = Color.LightGreen;
            else
                tbNlatFalutCh3.BackColor = Color.Pink;
        }

        private void _ParseAddr06(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbNlatFaultSTCh0.Items)
            {
                if (item.Value == (data & 0x03))
                {
                    cbNlatFaultSTCh0.SelectedItem = item;
                }
            }

            foreach (ComboBoxItem item in cbNlatFaultSTCh1.Items)
            {
                if (item.Value == (data >> 2 & 0x03))
                {
                    cbNlatFaultSTCh1.SelectedItem = item;
                }
            }

            foreach (ComboBoxItem item in cbNlatFaultSTCh2.Items)
            {
                if (item.Value == (data & 0x30))
                {
                    cbNlatFaultSTCh2.SelectedItem = item;
                }
            }

            foreach (ComboBoxItem item in cbNlatFaultSTCh3.Items)
            {
                if (item.Value == (data >> 2 & 0x03))
                {
                    cbNlatFaultSTCh3.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr08(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x01) == 0)
                cbIntrptType.SelectedIndex = 0;
            else
                cbIntrptType.SelectedIndex = 1;

            if ((data & 0x02) == 0)
                cbIntrptPolarity.SelectedIndex = 0;
            else
                cbIntrptPolarity.SelectedIndex = 1;
            
            foreach (ComboBoxItem item in cbSelectVdiop.Items)
            {
                if (item.Value == (data >> 3 & 0x03))
                {
                    cbSelectVdiop.SelectedItem = item;
                }
            }

            if ((data & 0x20) == 0)
                cbModeImon.SelectedIndex = 0;
            else
                cbModeImon.SelectedIndex = 1;

            foreach (ComboBoxItem item in cbSelectImon.Items)
            {
                if (item.Value == (data >> 6 & 0x03))
                {
                    cbSelectImon.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr09(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbAutoTuneLockTh.Items)
            {
                if (item.Value == (data & 0x03))
                {
                    cbAutoTuneLockTh.SelectedItem = item;
                }
            }
            foreach (ComboBoxItem item in cbAutoTuneUnlockTh.Items)
            {
                if (item.Value == (data >> 2 & 0x03))
                {
                    cbAutoTuneUnlockTh.SelectedItem = item;
                }
            }
            if ((data & 0x10) == 0)
                cbMonitorClockEnableCh0.Checked = false;
            else
                cbMonitorClockEnableCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbMonitorClockEnableCh1.Checked = false;
            else
                cbMonitorClockEnableCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbMonitorClockEnableCh2.Checked = false;
            else
                cbMonitorClockEnableCh2.Checked = true;

            if ((data & 0x80) == 0)
                cbMonitorClockEnableCh3.Checked = false;
            else
                cbMonitorClockEnableCh3.Checked = true;
        }

        private void _ParseAddr0A(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbCdrLoopBandWidth.Items)
            {
                if (item.Value == (data & 0x7))
                {
                    cbCdrLoopBandWidth.SelectedItem = item;
                }
            }
            if ((data & 0x40) == 0)
                cbSwitchBistVref.SelectedIndex = 0;
            else
                cbSwitchBistVref.SelectedIndex = 1;

            if ((data & 0x80) == 0)
                cbSwitchBistVdet.SelectedIndex = 0;
            else
                cbSwitchBistVdet.SelectedIndex = 1;
        }

        private void _ParseAddr0B(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x01) == 0)
                cbInvertPolarityCH0.Checked = false;
            else
                cbInvertPolarityCH0.Checked = true;

            if ((data & 0x02) == 0)
                cbInvertPolarityCH1.Checked = false;
            else
                cbInvertPolarityCH1.Checked = true;

            if ((data & 0x04) == 0)
                cbInvertPolarityCH2.Checked = false;
            else
                cbInvertPolarityCH2.Checked = true;

            if ((data & 0x08) == 0)
                cbInvertPolarityCH3.Checked = false;
            else
                cbInvertPolarityCH3.Checked = true;
        }

        private void _ParseAddr0C(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x01) == 0)
                cbTxPowerControlCh0.Checked = false;
            else
                cbTxPowerControlCh0.Checked = true;

            if ((data & 0x02) == 0)
                cbTxPowerControlCh1.Checked = false;
            else
                cbTxPowerControlCh1.Checked = true;

            if ((data & 0x04) == 0)
                cbTxPowerControlCh2.Checked = false;
            else
                cbTxPowerControlCh2.Checked = true;

            if ((data & 0x08) == 0)
                cbTxPowerControlCh3.Checked = false;
            else
                cbTxPowerControlCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbTxCdrControlCh0.Checked = false;
            else
                cbTxCdrControlCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbTxCdrControlCh1.Checked = false;
            else
                cbTxCdrControlCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbTxCdrControlCh2.Checked = false;
            else
                cbTxCdrControlCh2.Checked = true;

            if ((data & 0x80) == 0)
                cbTxCdrControlCh3.Checked = false;
            else
                cbTxCdrControlCh3.Checked = true;
        }

        private void _ParseAddr0D(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x01) == 0)
                cbLosMaskCh0.Checked = false;
            else
                cbLosMaskCh0.Checked = true;

            if ((data & 0x02) == 0)
                cbLosMaskCh1.Checked = false;
            else
                cbLosMaskCh1.Checked = true;

            if ((data & 0x04) == 0)
                cbLosMaskCh2.Checked = false;
            else
                cbLosMaskCh2.Checked = true;

            if ((data & 0x08) == 0)
                cbLosMaskCh3.Checked = false;
            else
                cbLosMaskCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbLolMaskCh0.Checked = false;
            else
                cbLolMaskCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbLolMaskCh1.Checked = false;
            else
                cbLolMaskCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbLolMaskCh2.Checked = false;
            else
                cbLolMaskCh2.Checked = true;

            if ((data & 0x80) == 0)
                cbLolMaskCh3.Checked = false;
            else
                cbLolMaskCh3.Checked = true;
        }

        private void _ParseAddr0E(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x01) == 0)
                cbLosClearCh0.Checked = false;
            else
                cbLosClearCh0.Checked = true;

            if ((data & 0x02) == 0)
                cbLosClearCh1.Checked = false;
            else
                cbLosClearCh1.Checked = true;

            if ((data & 0x04) == 0)
                cbLosClearCh2.Checked = false;
            else
                cbLosClearCh2.Checked = true;

            if ((data & 0x08) == 0)
                cbLosClearCh3.Checked = false;
            else
                cbLosClearCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbLolClearCh0.Checked = false;
            else
                cbLolClearCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbLolClearCh1.Checked = false;
            else
                cbLolClearCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbLolClearCh2.Checked = false;
            else
                cbLolClearCh2.Checked = true;

            if ((data & 0x80) == 0)
                cbLolClearCh3.Checked = false;
            else
                cbLolClearCh3.Checked = true;
        }

        private void _ParseAddr0F(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x01) == 0)
                cbLosModeCh0.Checked = false;
            else
                cbLosModeCh0.Checked = true;

            if ((data & 0x02) == 0)
                cbLosModeCh1.Checked = false;
            else
                cbLosModeCh1.Checked = true;

            if ((data & 0x04) == 0)
                cbLosModeCh2.Checked = false;
            else
                cbLosModeCh2.Checked = true;

            if ((data & 0x08) == 0)
                cbLosModeCh3.Checked = false;
            else
                cbLosModeCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbLolModeCh0.Checked = false;
            else
                cbLolModeCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbLolModeCh1.Checked = false;
            else
                cbLolModeCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbLolModeCh2.Checked = false;
            else
                cbLolModeCh2.Checked = true;

            if ((data & 0x80) == 0)
                cbLolModeCh3.Checked = false;
            else
                cbLolModeCh3.Checked = true;
        }

        private void _ParseAddr10(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x01) == 0)
                cbFaultMaskCh0.Checked = false;
            else
                cbFaultMaskCh0.Checked = true;

            if ((data & 0x02) == 0)
                cbFaultMaskCh1.Checked = false;
            else
                cbFaultMaskCh1.Checked = true;

            if ((data & 0x04) == 0)
                cbFaultMaskCh2.Checked = false;
            else
                cbFaultMaskCh2.Checked = true;

            if ((data & 0x08) == 0)
                cbFaultMaskCh3.Checked = false;
            else
                cbFaultMaskCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbFaultClearCh0.Checked = false;
            else
                cbFaultClearCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbFaultClearCh1.Checked = false;
            else
                cbFaultClearCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbFaultClearCh2.Checked = false;
            else
                cbFaultClearCh2.Checked = true;

            if ((data & 0x80) == 0)
                cbFaultClearCh3.Checked = false;
            else
                cbFaultClearCh3.Checked = true;
        }

        private void _ParseAddr11(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbLosThresholdCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                {
                    cbLosThresholdCh0.SelectedItem = item;
                }
            }
            foreach (ComboBoxItem item in cbLosThresholdCh1.Items)
                if ((data >> 4 & 0x0F) == item.Value)
                {
                    cbLosThresholdCh1.SelectedItem = item;
                }
        }

        private void _ParseAddr12(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbLosThresholdCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                {
                    cbLosThresholdCh2.SelectedItem = item;
                }
            }
            foreach (ComboBoxItem item in cbLosThresholdCh3.Items)
                if ((data >> 4 & 0x0F) == item.Value)
                {
                    cbLosThresholdCh3.SelectedItem = item;
                }
        }

        private void _ParseAddr13(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x01) == 0)
                cbLosHysteresisCh0.Checked = false;
            else
                cbLosHysteresisCh0.Checked = true;

            if ((data & 0x02) == 0)
                cbLosHysteresisCh1.Checked = false;
            else
                cbLosHysteresisCh1.Checked = true;

            if ((data & 0x04) == 0)
                cbLosHysteresisCh2.Checked = false;
            else
                cbLosHysteresisCh2.Checked = true;

            if ((data & 0x08) == 0)
                cbLosHysteresisCh3.Checked = false;
            else
                cbLosHysteresisCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbFaultModeCh0.Checked = false;
            else
                cbFaultModeCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbFaultModeCh1.Checked = false;
            else
                cbFaultModeCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbFaultModeCh2.Checked = false;
            else
                cbFaultModeCh2.Checked = true;

            if ((data & 0x80) == 0)
                cbFaultModeCh3.Checked = false;
            else
                cbFaultModeCh3.Checked = true;
        }

        private void _ParseAddr14(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqPeakCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                {
                    cbEqPeakCh0.SelectedItem = item;
                }
            }
            foreach (ComboBoxItem item in cbEqPeakCh1.Items)
            {
                if ((data >> 4 & 0x0F) == item.Value)
                {
                    cbEqPeakCh1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr15(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqPeakCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                {
                    cbEqPeakCh2.SelectedItem = item;
                }
            }
            foreach (ComboBoxItem item in cbEqPeakCh3.Items)
            {
                if ((data >> 4 & 0x0F) == item.Value)
                {
                    cbEqPeakCh3.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr17(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x10) == 0)
                cbMuteModeCh0.Checked = false;
            else
                cbMuteModeCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbMuteModeCh1.Checked = false;
            else
                cbMuteModeCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbMuteModeCh2.Checked = false;
            else
                cbMuteModeCh2.Checked = true;

            if ((data & 0x70) == 0)
                cbMuteModeCh3.Checked = false;
            else
                cbMuteModeCh3.Checked = true;
        }

        private void _ParseAddr18(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbCrossPointCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                {
                    cbCrossPointCh0.SelectedItem = item;
                }
            }
            foreach (ComboBoxItem item in cbCrossPointCh1.Items)
            {
                if ((data >> 4 & 0x0F) == item.Value)
                {
                    cbCrossPointCh1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr19(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbCrossPointCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                {
                    cbCrossPointCh2.SelectedItem = item;
                }
            }
            foreach (ComboBoxItem item in cbCrossPointCh3.Items)
            {
                if ((data >> 4 & 0x0F) == item.Value)
                {
                    cbCrossPointCh3.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr1A(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbDeEmphasisCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                {
                    cbDeEmphasisCh0.SelectedItem = item;
                }
            }
            foreach (ComboBoxItem item in cbDeEmphasisCh1.Items)
            {
                if ((data >> 4 & 0x0F) == item.Value)
                {
                    cbDeEmphasisCh1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr1B(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbDeEmphasisCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                {
                    cbDeEmphasisCh2.SelectedItem = item;
                }
            }
            foreach (ComboBoxItem item in cbDeEmphasisCh3.Items)
            {
                if ((data >> 4 & 0x0F) == item.Value)
                {
                    cbDeEmphasisCh3.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr1C(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x01) == 0)
                cbDdmiAdcPowerControl.SelectedIndex = 0;
            else
                cbDdmiAdcPowerControl.SelectedIndex = 1;

            foreach (ComboBoxItem item in cbDdmiChannelSelect.Items)
            {
                if ((data >> 1 & 0x07) == item.Value)
                {
                    cbDdmiChannelSelect.SelectedItem = item;
                }
            }

            if ((data & 0x10) == 0)
                cbDeemphasisEnCh0.Checked = false;
            else
                cbDeemphasisEnCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbDeemphasisEnCh1.Checked = false;
            else
                cbDeemphasisEnCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbDeemphasisEnCh2.Checked = false;
            else
                cbDeemphasisEnCh2.Checked = true;

            if ((data & 0x80) == 0)
                cbDeemphasisEnCh3.Checked = false;
            else
                cbDeemphasisEnCh3.Checked = true;
        }

        private void _ParseAddr1D_1E_1F(byte data0, byte data1, byte data2)
        {
#if false
            data0 = (byte)(((data0 & 0xF0) >> 4) | (data0 & 0x0F) << 4);
            data0 = (byte)(((data0 & 0xCC) >> 2) | (data0 & 0x33) << 2);
            data0 = (byte)(((data0 & 0xAA) >> 1) | (data0 & 0x55) << 1);
            data1 = (byte)(((data1 & 0xF0) >> 4) | (data1 & 0x0F) << 4);
            data1 = (byte)(((data1 & 0xCC) >> 2) | (data1 & 0x33) << 2);
            data1 = (byte)(((data1 & 0xAA) >> 1) | (data1 & 0x55) << 1);
            data2 = (byte)(((data2 & 0xF0) >> 4) | (data2 & 0x0F) << 4);
            data2 = (byte)(((data2 & 0xCC) >> 2) | (data2 & 0x33) << 2);
            data2 = (byte)(((data2 & 0xAA) >> 1) | (data2 & 0x55) << 1);
#endif
            foreach (ComboBoxItem item in cbRssiAgcClockSpeed.Items)
            {
                if ((data0 >> 3 & 0x03) == item.Value)
                {
                    cbRssiAgcClockSpeed.SelectedItem = item;
                }
            }

            if ((data0 & 0x20) == 0)
                cbRssiMode.SelectedIndex = 0;
            else
                cbRssiMode.SelectedIndex = 1;

            foreach (ComboBoxItem item in cbRssiLevelSelect.Items)
            {
                int iTmp;

                iTmp = (data0 >> 6 & 0x03) | ((data1 & 0x01) << 2);

                if ((iTmp & 0x07) == item.Value)
                {
                    cbRssiLevelSelect.SelectedItem = item;
                }
            }

            if ((data1 & 0x02) == 0)
                cbDdmiIdoRefSource.SelectedIndex = 0;
            else
                cbDdmiIdoRefSource.SelectedIndex = 1;

            if ((data1 & 0x04) == 0)
                cbIdoModeSelect.SelectedIndex = 0;
            else
                cbIdoModeSelect.SelectedIndex = 1;

            foreach (ComboBoxItem item in cbTemperatureOffset.Items)
            {
                if ((data1 >> 3 & 0x07) == item.Value)
                {
                    cbTemperatureOffset.SelectedItem = item;
                }
            }

            foreach (ComboBoxItem item in cbTemperatureSlope.Items)
            {
                int iTmp;

                iTmp = ((data1 >> 6 & 0x03) | (data2 << 2));

                if ((iTmp & 0x07) == item.Value)
                {
                    cbTemperatureSlope.SelectedItem = item;
                }
            }

            if ((data2 & 0x02) == 0)
                cbDdmiPwd.SelectedIndex = 0;
            else
                cbDdmiPwd.SelectedIndex = 1;

            if ((data2 & 0x08) == 0)
                cbTemperaturePwd.SelectedIndex = 0;
            else
                cbTemperaturePwd.SelectedIndex = 1;

            if ((data2 & 0x10) == 0)
                cbSourceIdoPwd.SelectedIndex = 0;
            else
                cbSourceIdoPwd.SelectedIndex = 1;

            if ((data2 & 0x20) == 0)
                cbEnableVf.SelectedIndex = 0;
            else
                cbEnableVf.SelectedIndex = 1;

            if ((data2 & 0x40) == 0)
                cbDdmiReset.SelectedIndex = 0;
            else
                cbDdmiReset.SelectedIndex = 1;
        }

        private void _ParseAddr20(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x1) == 0)
                cbIgnoreGlobalPwd.SelectedIndex = 0;
            else
                cbIgnoreGlobalPwd.SelectedIndex = 1;

            if ((data & 0x04) == 0)
                cbCh0SahPwd.SelectedIndex = 0;
            else
                cbCh0SahPwd.SelectedIndex = 1;

            if ((data & 0x10) == 0)
                cbCdrPwdCh0.Checked = false;
            else
                cbCdrPwdCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbCdrPwdCh1.Checked = false;
            else
                cbCdrPwdCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbCdrPwdCh2.Checked = false;
            else
                cbCdrPwdCh2.Checked = true;
            if ((data & 0x80) == 0)
                cbCdrPwdCh3.Checked = false;
            else
                cbCdrPwdCh3.Checked = true;
        }

        private void _ParseAddr21(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x01) == 0)
                cbAeqPwdCh0.Checked = false;
            else
                cbAeqPwdCh0.Checked = true;

            if ((data & 0x02) == 0)
                cbAeqPwdCh1.Checked = false;
            else
                cbAeqPwdCh1.Checked = true;

            if ((data & 0x04) == 0)
                cbAeqPwdCh2.Checked = false;
            else
                cbAeqPwdCh2.Checked = true;

            if ((data & 0x08) == 0)
                cbAeqPwdCh3.Checked = false;
            else
                cbAeqPwdCh3.Checked = true;                        
        }

        private void _ParseAddr22(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x01) == 0)
                cbRingOscPwd.SelectedIndex = 0;
            else
                cbRingOscPwd.SelectedIndex = 1;

            if ((data & 0x02) == 0)
                cbClockAutoTuneEnable.SelectedIndex = 0;
            else
                cbClockAutoTuneEnable.SelectedIndex = 1;

            if ((data & 0x04) == 0)
                cbClockAdcEnable.SelectedIndex = 0;
            else
                cbClockAdcEnable.SelectedIndex = 1;

            if ((data & 0x08) == 0)
                cbClockLosEnable.SelectedIndex = 0;
            else
                cbClockLosEnable.SelectedIndex = 1;

            foreach (ComboBoxItem item in cbSampleHoldClockSpeed.Items)
            {
                if ((data >> 4 & 0x03) == item.Value)
                    cbSampleHoldClockSpeed.SelectedItem = item;
            }

            foreach (ComboBoxItem item in cbAutoTuneClockSpeed.Items)
            {
                if ((data >> 6 & 0x03) == item.Value)
                    cbAutoTuneClockSpeed.SelectedItem = item;
            }
        }

        private void _ParseAddr23(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbIbiasCurrentCh0.Items)
            {
                if ((data & 0x7F) == item.Value)
                    cbIbiasCurrentCh0.SelectedItem = item;
            }
        }

        private void _ParseAddr24(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbIbiasCurrentCh1.Items)
            {
                if ((data & 0x7F) == item.Value)
                    cbIbiasCurrentCh1.SelectedItem = item;
            }
        }

        private void _ParseAddr25(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbIbiasCurrentCh2.Items)
            {
                if ((data & 0x7F) == item.Value)
                    cbIbiasCurrentCh2.SelectedItem = item;
            }
        }

        private void _ParseAddr26(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbIbiasCurrentCh3.Items)
            {
                if ((data & 0x7F) == item.Value)
                    cbIbiasCurrentCh3.SelectedItem = item;
            }
        }

        private void _ParseAddr27(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbModulationCh0.Items)
            {
                if ((data & 0x7F) == item.Value)
                    cbModulationCh0.SelectedItem = item;
            }
        }

        private void _ParseAddr28(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbModulationCh1.Items)
            {
                if ((data & 0x7F) == item.Value)
                    cbModulationCh1.SelectedItem = item;
            }
        }

        private void _ParseAddr29(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbModulationCh2.Items)
            {
                if ((data & 0x7F) == item.Value)
                    cbModulationCh2.SelectedItem = item;
            }
        }

        private void _ParseAddr2A(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbModulationCh3.Items)
            {
                if ((data & 0x7F) == item.Value)
                    cbModulationCh3.SelectedItem = item;
            }
        }

        private void _ParseAddr2B(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbBurninCurrentCh0.Items)
            {
                if ((data & 0x7F) == item.Value)
                    cbBurninCurrentCh0.SelectedItem = item;
            }

            if ((data & 0x80) == 0)
                cbBurninEnCh0.Checked = false;
            else
                cbBurninEnCh0.Checked = true;
        }

        private void _ParseAddr2C(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbBurninCurrentCh1.Items)
            {
                if ((data & 0x7F) == item.Value)
                    cbBurninCurrentCh1.SelectedItem = item;
            }

            if ((data & 0x80) == 0)
                cbBurninEnCh1.Checked = false;
            else
                cbBurninEnCh1.Checked = true;
        }

        private void _ParseAddr2D(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbBurninCurrentCh2.Items)
            {
                if ((data & 0x7F) == item.Value)
                    cbBurninCurrentCh2.SelectedItem = item;
            }

            if ((data & 0x80) == 0)
                cbBurninEnCh2.Checked = false;
            else
                cbBurninEnCh2.Checked = true;
        }

        private void _ParseAddr2E(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbBurninCurrentCh3.Items)
            {
                if ((data & 0x7F) == item.Value)
                    cbBurninCurrentCh3.SelectedItem = item;
            }

            if ((data & 0x80) == 0)
                cbBurninEnCh3.Checked = false;
            else
                cbBurninEnCh3.Checked = true;
        }

        private void _ParseAddr3F(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x80) == 0)
                cbAutoBypassResetCh0.SelectedIndex = 0;
            else
                cbAutoBypassResetCh0.SelectedIndex = 1;
        }

        private void _ParseAddr45(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbVcoMsbSelecCh0.Items)
            {
                if ((data >> 6 & 0x03) == item.Value)
                    cbVcoMsbSelecCh0.SelectedItem = item;
            }
        }

        private void _ParseAddr47(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x40) == 0)
                cbIbiasPowerDownCh0.Checked = false;
            else
                cbIbiasPowerDownCh0.Checked = true;

            if ((data & 0x80) == 0)
                cbModulationPowerDownCh0.Checked = false;
            else
                cbModulationPowerDownCh0.Checked = true;
        }

        private void _ParseAddr5F(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x80) == 0)
                cbAutoBypassResetCh1.SelectedIndex = 0;
            else
                cbAutoBypassResetCh1.SelectedIndex = 1;
        }

        private void _ParseAddr65(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbVcoMsbSelecCh1.Items)
            {
                if ((data >> 6 & 0x03) == item.Value)
                    cbVcoMsbSelecCh1.SelectedItem = item;
            }
        }

        private void _ParseAddr67(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x40) == 0)
                cbIbiasPowerDownCh1.Checked = false;
            else
                cbIbiasPowerDownCh1.Checked = true;

            if ((data & 0x80) == 0)
                cbModulationPowerDownCh1.Checked = false;
            else
                cbModulationPowerDownCh1.Checked = true;
        }

        private void _ParseAddr7F(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x80) == 0)
                cbAutoBypassResetCh2.SelectedIndex = 0;
            else
                cbAutoBypassResetCh2.SelectedIndex = 1;
        }

        private void _ParseAddr85(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbVcoMsbSelecCh2.Items)
            {
                if ((data >> 6 & 0x03) == item.Value)
                    cbVcoMsbSelecCh2.SelectedItem = item;
            }
        }

        private void _ParseAddr87(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x40) == 0)
                cbIbiasPowerDownCh2.Checked = false;
            else
                cbIbiasPowerDownCh2.Checked = true;

            if ((data & 0x80) == 0)
                cbModulationPowerDownCh2.Checked = false;
            else
                cbModulationPowerDownCh2.Checked = true;
        }

        private void _ParseAddr9F(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x80) == 0)
                cbAutoBypassResetCh3.SelectedIndex = 0;
            else
                cbAutoBypassResetCh3.SelectedIndex = 1;
        }

        private void _ParseAddrA5(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbVcoMsbSelecCh3.Items)
            {
                if ((data >> 6 & 0x03) == item.Value)
                    cbVcoMsbSelecCh3.SelectedItem = item;
            }
        }

        private void _ParseAddrA7(byte data)
        {
            //data = _ReverseBit(data);

            if ((data & 0x40) == 0)
                cbIbiasPowerDownCh3.Checked = false;
            else
                cbIbiasPowerDownCh3.Checked = true;

            if ((data & 0x80) == 0)
                cbModulationPowerDownCh3.Checked = false;
            else
                cbModulationPowerDownCh3.Checked = true;
        }

        private void _ParseAddrB0(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization0dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization0dbCh0.SelectedItem = item;
            }
        }

        private void _ParseAddrB1(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization1dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization1dbCh0.SelectedItem = item;
            }
        }

        private void _ParseAddrB2(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization2dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization2dbCh0.SelectedItem = item;
            }
        }
        private void _ParseAddrB3(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization3dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization3dbCh0.SelectedItem = item;
            }
        }
        private void _ParseAddrB4(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization4dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization4dbCh0.SelectedItem = item;
            }
        }
        private void _ParseAddrB5(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization5dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization5dbCh0.SelectedItem = item;
            }
        }
        private void _ParseAddrB6(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization6dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization6dbCh0.SelectedItem = item;
            }
        }
        private void _ParseAddrB7(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization7dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization7dbCh0.SelectedItem = item;
            }
        }
        private void _ParseAddrB8(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization8dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization8dbCh0.SelectedItem = item;
            }
        }
        private void _ParseAddrB9(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization9dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization9dbCh0.SelectedItem = item;
            }
        }
        private void _ParseAddrBA(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization10dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization10dbCh0.SelectedItem = item;
            }
        }
        private void _ParseAddrBB(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualizationR0Ch0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualizationR0Ch0.SelectedItem = item;
            }
        }
        private void _ParseAddrBC(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualizationR1Ch0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualizationR1Ch0.SelectedItem = item;
            }
        }

        private void _ParseAddrBD(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization0dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization0dbCh1.SelectedItem = item;
            }
        }

        private void _ParseAddrBE(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization1dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization1dbCh1.SelectedItem = item;
            }
        }

        private void _ParseAddrBF(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization2dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization2dbCh1.SelectedItem = item;
            }
        }
        private void _ParseAddrC0(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization3dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization3dbCh1.SelectedItem = item;
            }
        }
        private void _ParseAddrC1(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization4dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization4dbCh1.SelectedItem = item;
            }
        }
        private void _ParseAddrC2(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization5dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization5dbCh1.SelectedItem = item;
            }
        }
        private void _ParseAddrC3(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization6dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization6dbCh1.SelectedItem = item;
            }
        }
        private void _ParseAddrC4(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization7dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization7dbCh1.SelectedItem = item;
            }
        }
        private void _ParseAddrC5(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization8dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization8dbCh1.SelectedItem = item;
            }
        }
        private void _ParseAddrC6(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization9dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization9dbCh1.SelectedItem = item;
            }
        }
        private void _ParseAddrC7(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization10dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization10dbCh1.SelectedItem = item;
            }
        }
        private void _ParseAddrC8(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualizationR0Ch1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualizationR0Ch1.SelectedItem = item;
            }
        }
        private void _ParseAddrC9(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualizationR1Ch1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualizationR1Ch1.SelectedItem = item;
            }
        }

        private void _ParseAddrCA(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization0dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization0dbCh2.SelectedItem = item;
            }
        }

        private void _ParseAddrCB(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization1dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization1dbCh2.SelectedItem = item;
            }
        }

        private void _ParseAddrCC(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization2dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization2dbCh2.SelectedItem = item;
            }
        }
        private void _ParseAddrCD(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization3dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization3dbCh2.SelectedItem = item;
            }
        }
        private void _ParseAddrCE(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization4dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization4dbCh2.SelectedItem = item;
            }
        }
        private void _ParseAddrCF(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization5dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization5dbCh2.SelectedItem = item;
            }
        }
        private void _ParseAddrD0(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization6dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization6dbCh2.SelectedItem = item;
            }
        }
        private void _ParseAddrD1(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization7dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization7dbCh2.SelectedItem = item;
            }
        }
        private void _ParseAddrD2(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization8dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization8dbCh2.SelectedItem = item;
            }
        }
        private void _ParseAddrD3(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization9dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization9dbCh2.SelectedItem = item;
            }
        }
        private void _ParseAddrD4(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization10dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization10dbCh2.SelectedItem = item;
            }
        }
        private void _ParseAddrD5(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualizationR0Ch2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualizationR0Ch2.SelectedItem = item;
            }
        }
        private void _ParseAddrD6(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualizationR1Ch2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualizationR1Ch2.SelectedItem = item;
            }
        }

        private void _ParseAddrD7(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization0dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization0dbCh3.SelectedItem = item;
            }
        }

        private void _ParseAddrD8(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization1dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization1dbCh3.SelectedItem = item;
            }
        }

        private void _ParseAddrD9(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization2dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization2dbCh3.SelectedItem = item;
            }
        }
        private void _ParseAddrDA(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization3dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization3dbCh3.SelectedItem = item;
            }
        }
        private void _ParseAddrDB(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization4dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization4dbCh3.SelectedItem = item;
            }
        }
        private void _ParseAddrDC(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization5dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization5dbCh3.SelectedItem = item;
            }
        }
        private void _ParseAddrDD(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization6dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization6dbCh3.SelectedItem = item;
            }
        }
        private void _ParseAddrDE(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization7dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization7dbCh3.SelectedItem = item;
            }
        }
        private void _ParseAddrDF(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization8dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization8dbCh3.SelectedItem = item;
            }
        }
        private void _ParseAddrE0(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization9dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization9dbCh3.SelectedItem = item;
            }
        }
        private void _ParseAddrE1(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualization10dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualization10dbCh3.SelectedItem = item;
            }
        }
        private void _ParseAddrE2(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualizationR0Ch3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualizationR0Ch3.SelectedItem = item;
            }
        }
        private void _ParseAddrE3(byte data)
        {
            //data = _ReverseBit(data);

            foreach (ComboBoxItem item in cbEqualizationR1Ch3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbEqualizationR1Ch3.SelectedItem = item;
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

        private void bReadAll_Click(object sender, EventArgs e)
        {
            bReadAll.Enabled = false;
            _ReadAll();
            bReadAll.Enabled = true;
        }

        private int _ReadAll()
        {
            byte[] data = new byte[52];
            int rv;

            if (reading == true)
                goto exit;

            reading = true;

            if (i2cReadCB == null)
                goto exit;

            rv = i2cReadCB(devAddr, 0x00, 7, data);
            if (rv != 7)
                goto exit;

            _ParseAddr00(data[0]);
            _ParseAddr01(data[1]);
            _ParseAddr02(data[2]);
            _ParseAddr03(data[3]);
            _ParseAddr04_05(data[4], data[5]);
            _ParseAddr06(data[6]);

            rv = i2cReadCB(devAddr, 0x08, 8, data);
            if (rv != 8)
                goto exit;

            _ParseAddr08(data[0]);
            _ParseAddr09(data[1]);
            _ParseAddr0A(data[2]);
            _ParseAddr0B(data[3]);
            _ParseAddr0C(data[4]);
            _ParseAddr0D(data[5]);
            _ParseAddr0E(data[6]);
            _ParseAddr0F(data[7]);

            rv = i2cReadCB(devAddr, 0x11, 5, data);
            if (rv != 5)
                goto exit;
            _ParseAddr11(data[0]);
            _ParseAddr12(data[1]);
            _ParseAddr13(data[2]);
            _ParseAddr14(data[3]);
            _ParseAddr15(data[4]);

            rv = i2cReadCB(devAddr, 0x17, 24, data);
            if (rv != 24)
                goto exit;

            _ParseAddr17(data[0]);
            _ParseAddr18(data[1]);
            _ParseAddr19(data[2]);
            _ParseAddr1A(data[3]);
            _ParseAddr1B(data[4]);
            _ParseAddr1C(data[5]);
            _ParseAddr1D_1E_1F(data[6], data[7], data[8]);
            _ParseAddr20(data[9]);
            _ParseAddr21(data[10]);
            _ParseAddr22(data[11]);
            _ParseAddr23(data[12]);
            _ParseAddr24(data[13]);
            _ParseAddr25(data[14]);
            _ParseAddr26(data[15]);
            _ParseAddr27(data[16]);
            _ParseAddr28(data[17]);
            _ParseAddr29(data[18]);
            _ParseAddr2A(data[19]);
            _ParseAddr2B(data[20]);
            _ParseAddr2C(data[21]);
            _ParseAddr2D(data[22]);
            _ParseAddr2E(data[23]);

            rv = i2cReadCB(devAddr, 0x3F, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr3F(data[0]);

            rv = i2cReadCB(devAddr, 0x45, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr45(data[0]);

            rv = i2cReadCB(devAddr, 0x47, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr47(data[0]);

            rv = i2cReadCB(devAddr, 0x5F, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr5F(data[0]);

            rv = i2cReadCB(devAddr, 0x65, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr65(data[0]);

            rv = i2cReadCB(devAddr, 0x67, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr67(data[0]);

            rv = i2cReadCB(devAddr, 0x7F, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr7F(data[0]);

            rv = i2cReadCB(devAddr, 0x85, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr85(data[0]);

            rv = i2cReadCB(devAddr, 0x87, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr87(data[0]);

            rv = i2cReadCB(devAddr, 0x9F, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr9F(data[0]);

            rv = i2cReadCB(devAddr, 0xA5, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddrA5(data[0]);

            rv = i2cReadCB(devAddr, 0xA7, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddrA7(data[0]);

            rv = i2cReadCB(devAddr, 0xB0, 52, data);
            if (rv != 52)
                goto exit;

            _ParseAddrB0(data[0]);
            _ParseAddrB1(data[1]);
            _ParseAddrB2(data[2]);
            _ParseAddrB3(data[3]);
            _ParseAddrB4(data[4]);
            _ParseAddrB5(data[5]);
            _ParseAddrB6(data[6]);
            _ParseAddrB7(data[7]);
            _ParseAddrB8(data[8]);
            _ParseAddrB9(data[9]);
            _ParseAddrBA(data[10]);
            _ParseAddrBB(data[11]);
            _ParseAddrBC(data[12]);
            _ParseAddrBD(data[13]);
            _ParseAddrBE(data[14]);
            _ParseAddrBF(data[15]);
            _ParseAddrC0(data[16]);
            _ParseAddrC1(data[17]);
            _ParseAddrC2(data[18]);
            _ParseAddrC3(data[19]);
            _ParseAddrC4(data[20]);
            _ParseAddrC5(data[21]);
            _ParseAddrC6(data[22]);
            _ParseAddrC7(data[23]);
            _ParseAddrC8(data[24]);
            _ParseAddrC9(data[25]);
            _ParseAddrCA(data[26]);
            _ParseAddrCB(data[27]);
            _ParseAddrCC(data[28]);
            _ParseAddrCD(data[29]);
            _ParseAddrCE(data[30]);
            _ParseAddrCF(data[31]);
            _ParseAddrD0(data[32]);
            _ParseAddrD1(data[33]);
            _ParseAddrD2(data[34]);
            _ParseAddrD3(data[35]);
            _ParseAddrD4(data[36]);
            _ParseAddrD5(data[37]);
            _ParseAddrD6(data[38]);
            _ParseAddrD7(data[39]);
            _ParseAddrD8(data[40]);
            _ParseAddrD9(data[41]);
            _ParseAddrDA(data[42]);
            _ParseAddrDB(data[43]);
            _ParseAddrDC(data[44]);
            _ParseAddrDD(data[45]);
            _ParseAddrDE(data[46]);
            _ParseAddrDF(data[47]);
            _ParseAddrE0(data[48]);
            _ParseAddrE1(data[49]);
            _ParseAddrE2(data[50]);
            _ParseAddrE3(data[51]);
            
            reading = false;
            return 0;

        exit:
            reading = false;
            return -1;
        }

        private int _WriteAll()
        {
            if (_WriteAddr08() < 0) return -1;
            if (_WriteAddr09() < 0) return -1;
            if (_WriteAddr0A() < 0) return -1;
            if (_WriteAddr0B() < 0) return -1;
            if (_WriteAddr0C() < 0) return -1;
            if (_WriteAddr0D() < 0) return -1;
            if (_WriteAddr0E() < 0) return -1;
            if (_WriteAddr0F() < 0) return -1;
            if (_WriteAddr10() < 0) return -1;
            if (_WriteAddr11() < 0) return -1;
            if (_WriteAddr12() < 0) return -1;
            if (_WriteAddr13() < 0) return -1;
            if (_WriteAddr14() < 0) return -1;
            if (_WriteAddr15() < 0) return -1;
            if (_WriteAddr17() < 0) return -1;
            if (_WriteAddr18() < 0) return -1;
            if (_WriteAddr19() < 0) return -1;
            if (_WriteAddr1A() < 0) return -1;
            if (_WriteAddr1B() < 0) return -1;
            if (_WriteAddr1C() < 0) return -1;
            if (_WriteAddr1D_1E_1F() < 0) return -1;
            if (_WriteAddr20() < 0) return -1;
            if (_WriteAddr21() < 0) return -1;
            if (_WriteAddr22() < 0) return -1;
            if (_WriteAddr23() < 0) return -1;
            if (_WriteAddr24() < 0) return -1;
            if (_WriteAddr25() < 0) return -1;
            if (_WriteAddr26() < 0) return -1;
            if (_WriteAddr27() < 0) return -1;
            if (_WriteAddr28() < 0) return -1;
            if (_WriteAddr29() < 0) return -1;
            if (_WriteAddr2A() < 0) return -1;
            if (_WriteAddr2B() < 0) return -1;
            if (_WriteAddr2C() < 0) return -1;
            if (_WriteAddr2D() < 0) return -1;
            if (_WriteAddr2E() < 0) return -1;
            if (_WriteAddr3F() < 0) return -1;
            if (_WriteAddr45() < 0) return -1;
            if (_WriteAddr47() < 0) return -1;
            if (_WriteAddr5F() < 0) return -1;
            if (_WriteAddr65() < 0) return -1;
            if (_WriteAddr67() < 0) return -1;
            if (_WriteAddr7F() < 0) return -1;
            if (_WriteAddr85() < 0) return -1;
            if (_WriteAddr87() < 0) return -1;
            if (_WriteAddr9F() < 0) return -1;
            if (_WriteAddrA5() < 0) return -1;
            if (_WriteAddrA7() < 0) return -1;
            if (_WriteAddrB0() < 0) return -1;
            if (_WriteAddrB1() < 0) return -1;
            if (_WriteAddrB2() < 0) return -1;
            if (_WriteAddrB3() < 0) return -1;
            if (_WriteAddrB4() < 0) return -1;
            if (_WriteAddrB5() < 0) return -1;
            if (_WriteAddrB6() < 0) return -1;
            if (_WriteAddrB7() < 0) return -1;
            if (_WriteAddrB8() < 0) return -1;
            if (_WriteAddrB9() < 0) return -1;
            if (_WriteAddrBA() < 0) return -1;
            if (_WriteAddrBB() < 0) return -1;
            if (_WriteAddrBC() < 0) return -1;
            if (_WriteAddrBD() < 0) return -1;
            if (_WriteAddrBE() < 0) return -1;
            if (_WriteAddrBF() < 0) return -1;
            if (_WriteAddrC0() < 0) return -1;
            if (_WriteAddrC1() < 0) return -1;
            if (_WriteAddrC2() < 0) return -1;
            if (_WriteAddrC3() < 0) return -1;
            if (_WriteAddrC4() < 0) return -1;
            if (_WriteAddrC5() < 0) return -1;
            if (_WriteAddrC6() < 0) return -1;
            if (_WriteAddrC7() < 0) return -1;
            if (_WriteAddrC8() < 0) return -1;
            if (_WriteAddrC9() < 0) return -1;
            if (_WriteAddrCA() < 0) return -1;
            if (_WriteAddrCB() < 0) return -1;
            if (_WriteAddrCC() < 0) return -1;
            if (_WriteAddrCD() < 0) return -1;
            if (_WriteAddrCE() < 0) return -1;
            if (_WriteAddrCF() < 0) return -1;
            if (_WriteAddrD0() < 0) return -1;
            if (_WriteAddrD1() < 0) return -1;
            if (_WriteAddrD2() < 0) return -1;
            if (_WriteAddrD3() < 0) return -1;
            if (_WriteAddrD4() < 0) return -1;
            if (_WriteAddrD5() < 0) return -1;
            if (_WriteAddrD6() < 0) return -1;
            if (_WriteAddrD7() < 0) return -1;
            if (_WriteAddrD8() < 0) return -1;
            if (_WriteAddrD9() < 0) return -1;
            if (_WriteAddrDA() < 0) return -1;
            if (_WriteAddrDB() < 0) return -1;
            if (_WriteAddrDC() < 0) return -1;
            if (_WriteAddrDD() < 0) return -1;
            if (_WriteAddrDE() < 0) return -1;
            if (_WriteAddrDF() < 0) return -1;
            if (_WriteAddrE0() < 0) return -1;
            if (_WriteAddrE1() < 0) return -1;
            if (_WriteAddrE2() < 0) return -1;
            if (_WriteAddrE3() < 0) return -1;

            return 0;
        }

        private int _WriteAddr07()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0xAA;

            rv = i2cWriteCB(devAddr, 0x07, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr08()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbIntrptType.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbIntrptPolarity.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;
                       
            bTmp = Convert.ToByte(cbSelectVdiop.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbModeImon.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbSelectImon.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x08, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbAutoTuneLockTh.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbAutoTuneUnlockTh.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            if (cbMonitorClockEnableCh0.Checked == true)
                data[0] |= 0x10;

            if (cbMonitorClockEnableCh1.Checked == true)
                data[0] |= 0x20;

            if (cbMonitorClockEnableCh2.Checked == true)
                data[0] |= 0x40;

            if (cbMonitorClockEnableCh3.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(devAddr, 0x09, 1, data);

            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr0A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbCdrLoopBandWidth.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbSwitchBistVref.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbSwitchBistVdet.SelectedIndex);
            bTmp <<= 7;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x0A, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr0B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            if (cbInvertPolarityCH0.Checked == true)
                data[0] |= 0x01;

            if (cbInvertPolarityCH1.Checked == true)
                data[0] |= 0x02;

            if (cbInvertPolarityCH2.Checked == true)
                data[0] |= 0x04;

            if (cbInvertPolarityCH3.Checked == true)
                data[0] |= 0x08;

            rv = i2cWriteCB(devAddr, 0x0B, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr0C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (cbTxPowerControlCh0.Checked == true)
                data[0] |= 0x01;

            if (cbTxPowerControlCh1.Checked == true)
                data[0] |= 0x02;

            if (cbTxPowerControlCh2.Checked == true)
                data[0] |= 0x04;

            if (cbTxPowerControlCh3.Checked == true)
                data[0] |= 0x08;

            if (cbTxCdrControlCh0.Checked == true)
                data[0] |= 0x10;

            if (cbTxCdrControlCh1.Checked == true)
                data[0] |= 0x20;

            if (cbTxCdrControlCh2.Checked == true)
                data[0] |= 0x40;

            if (cbTxCdrControlCh3.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(devAddr, 0x0C, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr0D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (cbLosMaskCh0.Checked == true)
                data[0] |= 0x01;

            if (cbLosMaskCh1.Checked == true)
                data[0] |= 0x02;

            if (cbLosMaskCh2.Checked == true)
                data[0] |= 0x04;

            if (cbLosMaskCh3.Checked == true)
                data[0] |= 0x08;

            if (cbLolMaskCh0.Checked == true)
                data[0] |= 0x10;

            if (cbLolMaskCh1.Checked == true)
                data[0] |= 0x20;

            if (cbLolMaskCh2.Checked == true)
                data[0] |= 0x40;

            if (cbLolMaskCh3.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(devAddr, 0x0D, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr0E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (cbLosClearCh0.Checked == true)
                data[0] |= 0x01;

            if (cbLosClearCh1.Checked == true)
                data[0] |= 0x02;

            if (cbLosClearCh2.Checked == true)
                data[0] |= 0x04;

            if (cbLosClearCh3.Checked == true)
                data[0] |= 0x08;

            if (cbLolClearCh0.Checked == true)
                data[0] |= 0x10;

            if (cbLolClearCh1.Checked == true)
                data[0] |= 0x20;

            if (cbLolClearCh2.Checked == true)
                data[0] |= 0x40;

            if (cbLolClearCh3.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(devAddr, 0x0E, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr0F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (cbLosModeCh0.Checked == true)
                data[0] |= 0x01;

            if (cbLosModeCh1.Checked == true)
                data[0] |= 0x02;

            if (cbLosModeCh2.Checked == true)
                data[0] |= 0x04;

            if (cbLosModeCh3.Checked == true)
                data[0] |= 0x08;

            if (cbLolModeCh0.Checked == true)
                data[0] |= 0x10;

            if (cbLolModeCh1.Checked == true)
                data[0] |= 0x20;

            if (cbLolModeCh2.Checked == true)
                data[0] |= 0x40;

            if (cbLolModeCh3.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(devAddr, 0x0F, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr10()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (cbFaultMaskCh0.Checked == true)
                data[0] |= 0x01;

            if (cbFaultMaskCh1.Checked == true)
                data[0] |= 0x02;

            if (cbFaultMaskCh2.Checked == true)
                data[0] |= 0x04;

            if (cbFaultMaskCh3.Checked == true)
                data[0] |= 0x08;

            if (cbFaultClearCh0.Checked == true)
                data[0] |= 0x10;

            if (cbFaultClearCh1.Checked == true)
                data[0] |= 0x20;

            if (cbFaultClearCh2.Checked == true)
                data[0] |= 0x40;

            if (cbFaultClearCh3.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(devAddr, 0x10, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr11()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbLosThresholdCh0.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosThresholdCh1.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x11, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr12()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbLosThresholdCh2.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosThresholdCh3.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x12, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr13()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (cbLosHysteresisCh0.Checked == true)
                data[0] |= 0x01;

            if (cbLosHysteresisCh1.Checked == true)
                data[0] |= 0x02;

            if (cbLosHysteresisCh2.Checked == true)
                data[0] |= 0x04;

            if (cbLosHysteresisCh3.Checked == true)
                data[0] |= 0x08;

            if (cbFaultModeCh0.Checked == true)
                data[0] |= 0x10;

            if (cbFaultModeCh1.Checked == true)
                data[0] |= 0x20;

            if (cbFaultModeCh2.Checked == true)
                data[0] |= 0x40;

            if (cbFaultModeCh3.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(devAddr, 0x13, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr14()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbEqPeakCh0.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbEqPeakCh1.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x14, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbEqPeakCh2.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbEqPeakCh3.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x15, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            
            if (cbMuteModeCh0.Checked == true)
                data[0] |= 0x10;

            if (cbMuteModeCh1.Checked == true)
                data[0] |= 0x20;

            if (cbMuteModeCh2.Checked == true)
                data[0] |= 0x40;

            if (cbMuteModeCh3.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(devAddr, 0x17, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbCrossPointCh0.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbCrossPointCh1.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x18, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbCrossPointCh2.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbCrossPointCh3.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x19, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr1A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbDeEmphasisCh0.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDeEmphasisCh1.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x1A, 1, data);
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

            bTmp = Convert.ToByte(cbDeEmphasisCh2.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDeEmphasisCh3.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x1B, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr1C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbDdmiAdcPowerControl.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDdmiChannelSelect.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            if (cbDeemphasisEnCh0.Checked == true)
                data[0] |= 0x10;

            if (cbDeemphasisEnCh1.Checked == true)
                data[0] |= 0x20;

            if (cbDeemphasisEnCh2.Checked == true)
                data[0] |= 0x40;

            if (cbDeemphasisEnCh3.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(devAddr, 0x1C, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr1D_1E_1F()
        {
            byte[] data = new byte[3];
            int rv;
            byte bTmp;

            bTmp = data[0] = data[1] = data[2] = 0;

            bTmp = Convert.ToByte(cbRssiAgcClockSpeed.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbRssiMode.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbRssiLevelSelect.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;
            bTmp = Convert.ToByte(cbRssiLevelSelect.SelectedIndex);
            bTmp >>= 2;
            data[1] |= bTmp;

            bTmp = Convert.ToByte(cbDdmiIdoRefSource.SelectedIndex);
            bTmp <<= 1;
            data[1] |= bTmp;

            bTmp = Convert.ToByte(cbIdoModeSelect.SelectedIndex);
            bTmp <<= 2;
            data[1] |= bTmp;

            bTmp = Convert.ToByte(cbTemperatureOffset.SelectedIndex);
            bTmp <<= 3;
            data[1] |= bTmp;

            bTmp = Convert.ToByte(cbTemperatureSlope.SelectedIndex);
            bTmp <<= 6;
            data[1] |= bTmp;
            bTmp = Convert.ToByte(cbTemperatureSlope.SelectedIndex);
            bTmp >>= 2;
            data[2] |= bTmp;

            bTmp = Convert.ToByte(cbDdmiPwd.SelectedIndex);
            bTmp <<= 1;
            data[2] |= bTmp;

            bTmp = Convert.ToByte(cbTemperaturePwd.SelectedIndex);
            bTmp <<= 3;
            data[2] |= bTmp;

            bTmp = Convert.ToByte(cbSourceIdoPwd.SelectedIndex);
            bTmp <<= 4;
            data[2] |= bTmp;

            bTmp = Convert.ToByte(cbEnableVf.SelectedIndex);
            bTmp <<= 5;
            data[2] |= bTmp;

            bTmp = Convert.ToByte(cbDdmiReset.SelectedIndex);
            bTmp <<= 6;
            data[2] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x1D, 3, data);
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

            bTmp = Convert.ToByte(cbIgnoreGlobalPwd.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbCh0SahPwd.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            if (cbCdrPwdCh0.Checked == true)
                data[0] |= 0x10;

            if (cbCdrPwdCh1.Checked == true)
                data[0] |= 0x20;

            if (cbCdrPwdCh2.Checked == true)
                data[0] |= 0x40;

            if (cbCdrPwdCh3.Checked == true)
                data[0] |= 0x80;

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

            if (cbAeqPwdCh0.Checked == true)
                data[0] |= 0x01;

            if (cbAeqPwdCh1.Checked == true)
                data[0] |= 0x02;

            if (cbAeqPwdCh2.Checked == true)
                data[0] |= 0x04;

            if (cbAeqPwdCh3.Checked == true)
                data[0] |= 0x08;
            
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

            bTmp = Convert.ToByte(cbRingOscPwd.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbClockAutoTuneEnable.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbClockAdcEnable.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbClockLosEnable.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbSampleHoldClockSpeed.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbAutoTuneClockSpeed.SelectedIndex);
            bTmp <<= 6;
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
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbIbiasCurrentCh0.SelectedIndex);
            data[0] |= bTmp;
                        
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

            bTmp = Convert.ToByte(cbIbiasCurrentCh1.SelectedIndex);
            data[0] |= bTmp;

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

            bTmp = Convert.ToByte(cbIbiasCurrentCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbIbiasCurrentCh3.SelectedIndex);
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

            bTmp = Convert.ToByte(cbModulationCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbModulationCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x28, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr29()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbModulationCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x29, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr2A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbModulationCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x2A, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr2B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbBurninCurrentCh0.SelectedIndex);
            data[0] |= bTmp;

            if (cbBurninEnCh0.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(devAddr, 0x2B, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr2C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbBurninCurrentCh1.SelectedIndex);
            data[0] |= bTmp;

            if (cbBurninEnCh1.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(devAddr, 0x2C, 1, data);
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

            bTmp = Convert.ToByte(cbBurninCurrentCh2.SelectedIndex);
            data[0] |= bTmp;

            if (cbBurninEnCh2.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(devAddr, 0x2D, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr2E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbBurninCurrentCh3.SelectedIndex);
            data[0] |= bTmp;

            if (cbBurninEnCh3.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(devAddr, 0x2E, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr3F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbAutoBypassResetCh0.SelectedIndex);
            bTmp <<= 7;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x3F, 1, data);
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

            bTmp = Convert.ToByte(cbVcoMsbSelecCh0.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x45, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr47()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0x0F;

            if (cbIbiasPowerDownCh0.Checked == true)
                data[0] |= 0x4F;                

            if (cbModulationPowerDownCh0.Checked == true)
                data[0] |= 0x8F;

            rv = i2cWriteCB(devAddr, 0x47, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr5F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbAutoBypassResetCh1.SelectedIndex);
            bTmp <<= 7;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x5F, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr65()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbVcoMsbSelecCh1.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x65, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr67()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0x0F;

            if (cbIbiasPowerDownCh1.Checked == true)
                data[0] |= 0x4F;                

            if (cbModulationPowerDownCh1.Checked == true)
                data[0] |= 0x8F;

            rv = i2cWriteCB(devAddr, 0x67, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr7F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbAutoBypassResetCh2.SelectedIndex);
            bTmp <<= 7;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x7F, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr85()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbVcoMsbSelecCh2.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x85, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr87()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0x0F;

            if (cbIbiasPowerDownCh2.Checked == true)
                data[0] |= 0x4F;

            if (cbModulationPowerDownCh2.Checked == true)
                data[0] |= 0x8F;

            rv = i2cWriteCB(devAddr, 0x87, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr9F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbAutoBypassResetCh3.SelectedIndex);
            bTmp <<= 7;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x9F, 1, data);
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

            bTmp = Convert.ToByte(cbVcoMsbSelecCh3.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xA5, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrA7()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0x0F;

            if (cbIbiasPowerDownCh3.Checked == true)
                data[0] |= 0x4F;

            if (cbModulationPowerDownCh3.Checked == true)
                data[0] |= 0x8F;

            rv = i2cWriteCB(devAddr, 0xA7, 1, data);
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

            bTmp = Convert.ToByte(cbEqualization0dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization1dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization2dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization3dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization4dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization5dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization6dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization7dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization8dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization9dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization10dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualizationR0Ch0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualizationR1Ch0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization0dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization1dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization2dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization3dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization4dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization5dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization6dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization7dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization8dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization9dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization10dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualizationR0Ch1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualizationR1Ch1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization0dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization1dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization2dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization3dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization4dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization5dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization6dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization7dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization8dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization9dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization10dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualizationR0Ch2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualizationR1Ch2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization0dbCh3.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization1dbCh3.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization2dbCh3.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization3dbCh3.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization4dbCh3.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization5dbCh3.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization6dbCh3.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization7dbCh3.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization8dbCh3.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization9dbCh3.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualization10dbCh3.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualizationR0Ch3.SelectedIndex);
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

            bTmp = Convert.ToByte(cbEqualizationR1Ch3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xE3, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void bDeviceReset_Click(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr07() < 0)
                return;
        }

        private void bStoreIntoFlash_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1] { 0xA0 };
            int rv;

            bStoreIntoFlash.Enabled = false;
            reading = true;
            rv = i2cWriteCB(devAddr, 0xFA, 1, data);
            System.Threading.Thread.Sleep(1000);
            bStoreIntoFlash.Enabled = true;
            reading = false;
        }

        private void cbIntrptType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr08() < 0)
                return;
        }

        private void cbIntrptPolarity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr08() < 0)
                return;
        }

        private void cbSelectVdiop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr08() < 0)
                return;
        }

        private void cbModeImon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr08() < 0)
                return;
        }

        private void cbSelectImon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr08() < 0)
                return;
        }

        private void cbAutoTuneLockTh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr09() < 0)
                return;
        }

        private void cbAutoTuneUnlockTh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr09() < 0)
                return;
        }

        private void cbMonitorClockEnableCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr09() < 0)
                return;
        }

        private void cbMonitorClockEnableCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr09() < 0)
                return;
        }

        private void cbMonitorClockEnableCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr09() < 0)
                return;
        }

        private void cbMonitorClockEnableCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr09() < 0)
                return;
        }

        private void cbCdrLoopBandWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0A() < 0)
                return;
        }

        private void cbSwitchBistVref_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0A() < 0)
                return;
        }

        private void cbSwitchBistVdet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0A() < 0)
                return;
        }

        private void cbInvertPolarityCH0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0B() < 0)
                return;
        }

        private void cbInvertPolarityCH1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0B() < 0)
                return;
        }

        private void cbInvertPolarityCH2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0B() < 0)
                return;
        }

        private void cbInvertPolarityCH3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0B() < 0)
                return;
        }

        private void cbTxPowerControlCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0C() < 0)
                return;
        }

        private void cbTxPowerControlCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0C() < 0)
                return;
        }

        private void cbTxPowerControlCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0C() < 0)
                return;
        }

        private void cbTxPowerControlCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0C() < 0)
                return;
        }

        private void cbTxCdrControlCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0C() < 0)
                return;
        }

        private void cbTxCdrControlCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0C() < 0)
                return;
        }

        private void cbTxCdrControlCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0C() < 0)
                return;
        }

        private void cbTxCdrControlCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0C() < 0)
                return;
        }

        private void cbLosMaskCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0D() < 0)
                return;
        }

        private void cbLosMaskCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0D() < 0)
                return;
        }

        private void cbLosMaskCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0D() < 0)
                return;
        }

        private void cbLosMaskCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0D() < 0)
                return;
        }

        private void cbLolMaskCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0D() < 0)
                return;
        }

        private void cbLolMaskCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0D() < 0)
                return;
        }

        private void cbLolMaskCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0D() < 0)
                return;
        }

        private void cbLolMaskCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0D() < 0)
                return;
        }

        private void cbLosClearCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0E() < 0)
                return;
        }

        private void cbLosClearCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0E() < 0)
                return;
        }

        private void cbLosClearCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0E() < 0)
                return;
        }

        private void cbLosClearCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0E() < 0)
                return;
        }

        private void cbLolClearCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0E() < 0)
                return;
        }

        private void cbLolClearCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0E() < 0)
                return;
        }

        private void cbLolClearCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0E() < 0)
                return;
        }

        private void cbLolClearCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0E() < 0)
                return;
        }

        private void cbLosModeCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0F() < 0)
                return;
        }

        private void cbLosModeCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0F() < 0)
                return;
        }

        private void cbLosModeCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0F() < 0)
                return;
        }

        private void cbLosModeCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0F() < 0)
                return;
        }

        private void cbLolModeCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0F() < 0)
                return;
        }

        private void cbLolModeCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0F() < 0)
                return;
        }

        private void cbLolModeCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0F() < 0)
                return;
        }

        private void cbLolModeCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0F() < 0)
                return;
        }

        private void cbFaultMaskCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbFaultMaskCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbFaultMaskCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbFaultMaskCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbFaultClearCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbFaultClearCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbFaultClearCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbFaultClearCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbLosThresholdCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr11() < 0)
                return;
        }

        private void cbLosThresholdCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr11() < 0)
                return;
        }

        private void cbLosThresholdCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12() < 0)
                return;
        }

        private void cbLosThresholdCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr12() < 0)
                return;
        }

        private void cbLosHysteresisCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr13() < 0)
                return;
        }

        private void cbLosHysteresisCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr13() < 0)
                return;
        }

        private void cbLosHysteresisCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr13() < 0)
                return;
        }

        private void cbLosHysteresisCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr13() < 0)
                return;
        }

        private void cbFaultModeCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr13() < 0)
                return;
        }

        private void cbFaultModeCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr13() < 0)
                return;
        }

        private void cbFaultModeCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr13() < 0)
                return;
        }

        private void cbFaultModeCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr13() < 0)
                return;
        }

        private void cbEqPeakCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr14() < 0)
                return;
        }

        private void cbEqPeakCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr14() < 0)
                return;
        }

        private void cbEqPeakCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr15() < 0)
                return;
        }

        private void cbEqPeakCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr15() < 0)
                return;
        }

        private void cbMuteModeCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr17() < 0)
                return;
        }

        private void cbMuteModeCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr17() < 0)
                return;
        }

        private void cbMuteModeCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr17() < 0)
                return;
        }

        private void cbMuteModeCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr17() < 0)
                return;
        }

        private void cbCrossPointCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr18() < 0)
                return;
        }

        private void cbCrossPointCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr18() < 0)
                return;
        }

        private void cbCrossPointCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19() < 0)
                return;
        }

        private void cbCrossPointCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19() < 0)
                return;
        }

        private void cbDeEmphasisCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1A() < 0)
                return;
        }

        private void cbDeEmphasisCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1A() < 0)
                return;
        }

        private void cbDeEmphasisCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1B() < 0)
                return;
        }

        private void cbDeEmphasisCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1B() < 0)
                return;
        }

        private void cbDdmiAdcPowerControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1C() < 0)
                return;
        }

        private void cbDdmiChannelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1C() < 0)
                return;
        }

        private void cbDeemphasisEnCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1C() < 0)
                return;
        }

        private void cbDeemphasisEnCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1C() < 0)
                return;
        }

        private void cbDeemphasisEnCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1C() < 0)
                return;
        }

        private void cbDeemphasisEnCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1C() < 0)
                return;
        }

        private void cbRssiAgcClockSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D_1E_1F() < 0)
                return;
        }

        private void cbRssiMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D_1E_1F() < 0)
                return;
        }

        private void cbRssiLevelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D_1E_1F() < 0)
                return;
        }

        private void cbDdmiIdoRefSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D_1E_1F() < 0)
                return;
        }

        private void cbIdoModeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D_1E_1F() < 0)
                return;
        }

        private void cbTemperatureOffset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D_1E_1F() < 0)
                return;
        }

        private void cbTemperatureSlope_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D_1E_1F() < 0)
                return;
        }

        private void cbDdmiPwd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D_1E_1F() < 0)
                return;
        }

        private void cbTemperaturePwd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D_1E_1F() < 0)
                return;
        }

        private void cbSourceIdoPwd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D_1E_1F() < 0)
                return;
        }

        private void cbEnableVf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D_1E_1F() < 0)
                return;
        }

        private void cbDdmiReset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1D_1E_1F() < 0)
                return;
        }

        private void cbIgnoreGlobalPwd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr20() < 0)
                return;
        }

        private void cbCh0SahPwd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr20() < 0)
                return;
        }

        private void cbCdrPwdCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr20() < 0)
                return;
        }

        private void cbCdrPwdCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr20() < 0)
                return;
        }

        private void cbCdrPwdCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr20() < 0)
                return;
        }

        private void cbCdrPwdCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr20() < 0)
                return;
        }

        private void cbAeqPwdCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21() < 0)
                return;
        }

        private void cbAeqPwdCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21() < 0)
                return;
        }

        private void cbAeqPwdCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21() < 0)
                return;
        }

        private void cbAeqPwdCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21() < 0)
                return;
        }

        private void cbRingOscPwd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22() < 0)
                return;
        }

        private void cbClockAutoTuneEnable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22() < 0)
                return;
        }

        private void cbClockAdcEnable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22() < 0)
                return;
        }

        private void cbClockLosEnable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22() < 0)
                return;
        }

        private void cbSampleHoldClockSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22() < 0)
                return;
        }

        private void cbAutoTuneClockSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22() < 0)
                return;
        }

        private void cbIbiasCurrentCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr23() < 0)
                return;
        }

        private void cbIbiasCurrentCh1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (reading == true)
                return;

            if (_WriteAddr24() < 0)
                return;
        }

        private void cbIbiasCurrentCh2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (reading == true)
                return;

            if (_WriteAddr25() < 0)
                return;
        }

        private void cbIbiasCurrentCh3_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (reading == true)
                return;

            if (_WriteAddr26() < 0)
                return;
        }

        private void cbModulationCh0_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (reading == true)
                return;

            if (_WriteAddr27() < 0)
                return;
        }

        private void cbModulationCh1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (reading == true)
                return;

            if (_WriteAddr28() < 0)
                return;
        }

        private void cbModulationCh2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (reading == true)
                return;

            if (_WriteAddr29() < 0)
                return;
        }

        private void cbModulationCh3_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (reading == true)
                return;

            if (_WriteAddr2A() < 0)
                return;
        }

        private void cbBurninCurrentCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2B() < 0)
                return;
        }

        private void cbBurninEnCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2B() < 0)
                return;
        }

        private void cbBurninCurrentCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2C() < 0)
                return;
        }

        private void cbBurninEnCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2C() < 0)
                return;
        }

        private void cbBurninCurrentCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2D() < 0)
                return;
        }

        private void cbBurninEnCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2D() < 0)
                return;
        }

        private void cbBurninCurrentCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2E() < 0)
                return;
        }

        private void cbBurninEnCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2E() < 0)
                return;
        }

        private void cbAutoBypassResetCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr3F() < 0)
                return;
        }

        private void cbVcoMsbSelecCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr45() < 0)
                return;
        }

        private void cbIbiasPowerDownCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr47() < 0)
                return;
        }

        private void cbModulationPowerDownCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr47() < 0)
                return;
        }

        private void cbAutoBypassResetCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr5F() < 0)
                return;
        }

        private void cbVcoMsbSelecCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65() < 0)
                return;
        }

        private void cbIbiasPowerDownCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr67() < 0)
                return;
        }

        private void cbModulationPowerDownCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr67() < 0)
                return;
        }

        private void cbAutoBypassResetCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr7F() < 0)
                return;
        }

        private void cbVcoMsbSelecCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr85() < 0)
                return;
        }

        private void cbIbiasPowerDownCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr87() < 0)
                return;
        }

        private void cbModulationPowerDownCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr87() < 0)
                return;
        }

        private void cbAutoBypassResetCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr9F() < 0)
                return;
        }

        private void cbVcoMsbSelecCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA5() < 0)
                return;
        }

        private void cbIbiasPowerDownCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA7() < 0)
                return;
        }

        private void cbModulationPowerDownCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA7() < 0)
                return;
        }

        private void cbEqualization0dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB0() < 0)
                return;
        }

        private void cbEqualization1dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB1() < 0)
                return;
        }

        private void cbEqualization2dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB2() < 0)
                return;
        }

        private void cbEqualization3dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB3() < 0)
                return;
        }

        private void cbEqualization4dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB4() < 0)
                return;
        }

        private void cbEqualization5dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB5() < 0)
                return;
        }

        private void cbEqualization6dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB6() < 0)
                return;
        }

        private void cbEqualization7dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB7() < 0)
                return;
        }

        private void cbEqualization8dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB8() < 0)
                return;
        }

        private void cbEqualization9dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB9() < 0)
                return;
        }

        private void cbEqualization10dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBA() < 0)
                return;
        }

        private void cbEqualizationR0Ch0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBB() < 0)
                return;
        }

        private void cbEqualizationR1Ch0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBC() < 0)
                return;
        }

        private void cbEqualization0dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBD() < 0)
                return;
        }

        private void cbEqualization1dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBE() < 0)
                return;
        }

        private void cbEqualization2dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBF() < 0)
                return;
        }

        private void cbEqualization3dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC0() < 0)
                return;
        }

        private void cbEqualization4dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC1() < 0)
                return;
        }

        private void cbEqualization5dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC2() < 0)
                return;
        }

        private void cbEqualization6dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC3() < 0)
                return;
        }

        private void cbEqualization7dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC4() < 0)
                return;
        }

        private void cbEqualization8dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC5() < 0)
                return;
        }

        private void cbEqualization9dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC6() < 0)
                return;
        }

        private void cbEqualization10dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC7() < 0)
                return;
        }

        private void cbEqualizationR0Ch1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC8() < 0)
                return;
        }

        private void cbEqualizationR1Ch1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC9() < 0)
                return;
        }

        private void cbEqualization0dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCA() < 0)
                return;
        }

        private void cbEqualization1dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCB() < 0)
                return;
        }

        private void cbEqualization2dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCC() < 0)
                return;
        }

        private void cbEqualization3dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCD() < 0)
                return;
        }

        private void cbEqualization4dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCE() < 0)
                return;
        }

        private void cbEqualization5dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCF() < 0)
                return;
        }

        private void cbEqualization6dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD0() < 0)
                return;
        }

        private void cbEqualization7dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD1() < 0)
                return;
        }

        private void cbEqualization8dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD2() < 0)
                return;
        }

        private void cbEqualization9dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD3() < 0)
                return;
        }

        private void cbEqualization10dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD4() < 0)
                return;
        }

        private void cbEqualizationR0Ch2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD5() < 0)
                return;
        }

        private void cbEqualizationR1Ch2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD6() < 0)
                return;
        }

        private void cbEqualization0dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD7() < 0)
                return;
        }

        private void cbEqualization1dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD8() < 0)
                return;
        }

        private void cbEqualization2dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD9() < 0)
                return;
        }

        private void cbEqualization3dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDA() < 0)
                return;
        }

        private void cbEqualization4dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDB() < 0)
                return;
        }

        private void cbEqualization5dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDC() < 0)
                return;
        }

        private void cbEqualization6dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDD() < 0)
                return;
        }

        private void cbEqualization7dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDE() < 0)
                return;
        }

        private void cbEqualization8dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDF() < 0)
                return;
        }

        private void cbEqualization9dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE0() < 0)
                return;
        }

        private void cbEqualization10dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE1() < 0)
                return;
        }

        private void cbEqualizationR0Ch3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE2() < 0)
                return;
        }

        private void cbEqualizationR1Ch3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE3() < 0)
                return;
        }
    }
}
