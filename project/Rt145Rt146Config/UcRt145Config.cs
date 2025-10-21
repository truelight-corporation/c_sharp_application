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
    public partial class UcRt145Config : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private const byte devAddr = 0x1A;
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

        public UcRt145Config()
        {
            InitializeComponent();

            ComboBoxItem item;
            double dTmp;
            int i;            

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
            item.Text = "0：sel";
            item.Value = 0;
            cbModeRssi.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：sum";
            item.Value = 1;
            cbModeRssi.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：lane0";
            item.Value = 0;
            cbSelectRssi.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：lane1";
            item.Value = 1;
            cbSelectRssi.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：lane2";
            item.Value = 2;
            cbSelectRssi.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：lane3";
            item.Value = 3;
            cbSelectRssi.Items.Add(item);

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

            for (i = 0; i < 5; i++){
                item = new ComboBoxItem();
                item.Text = i + "：Reserved" ;
                item.Value = i;
                cbLosThresholdCh0.Items.Add(item);
            }            
            for (i = 5 , dTmp = 6; i < 16; i++, dTmp += 2) {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + "uA";
                item.Value = i;
                cbLosThresholdCh0.Items.Add(item);
            }

            for (i = 0; i < 5; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：Reserved";
                item.Value = i;
                cbLosThresholdCh1.Items.Add(item);
            }
            for (i = 5, dTmp = 6; i < 16; i++, dTmp += 2)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + "uA";
                item.Value = i;
                cbLosThresholdCh1.Items.Add(item);
            }

            for (i = 0; i < 5; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：Reserved";
                item.Value = i;
                cbLosThresholdCh2.Items.Add(item);
            }
            for (i = 5, dTmp = 6; i < 16; i++, dTmp += 2)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + "uA";
                item.Value = i;
                cbLosThresholdCh2.Items.Add(item);
            }

            for (i = 0; i < 5; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：Reserved";
                item.Value = i;
                cbLosThresholdCh3.Items.Add(item);
            }
            for (i = 5, dTmp = 6; i < 16; i++, dTmp += 2)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + "uA";
                item.Value = i;
                cbLosThresholdCh3.Items.Add(item);
            }

            item = new ComboBoxItem();
            item.Text = "0：-20u";
            item.Value = 0;
            cbCommonVoltageCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：-10u";
            item.Value = 1;
            cbCommonVoltageCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0u";
            item.Value = 2;
            cbCommonVoltageCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：10u";
            item.Value = 3;
            cbCommonVoltageCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：10u";
            item.Value = 4;
            cbCommonVoltageCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：18u";
            item.Value = 5;
            cbCommonVoltageCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：25u";
            item.Value = 6;
            cbCommonVoltageCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：33u";
            item.Value = 7;
            cbCommonVoltageCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：40u";
            item.Value = 8;
            cbCommonVoltageCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：48u";
            item.Value = 9;
            cbCommonVoltageCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：55u";
            item.Value = 10;
            cbCommonVoltageCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：63u";
            item.Value = 11;
            cbCommonVoltageCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：40u";
            item.Value = 12;
            cbCommonVoltageCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：48u";
            item.Value = 13;
            cbCommonVoltageCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：45u";
            item.Value = 14;
            cbCommonVoltageCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：63u";
            item.Value = 15;
            cbCommonVoltageCh0.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：-20u";
            item.Value = 0;
            cbCommonVoltageCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：-10u";
            item.Value = 1;
            cbCommonVoltageCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0u";
            item.Value = 2;
            cbCommonVoltageCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：10u";
            item.Value = 3;
            cbCommonVoltageCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：10u";
            item.Value = 4;
            cbCommonVoltageCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：18u";
            item.Value = 5;
            cbCommonVoltageCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：25u";
            item.Value = 6;
            cbCommonVoltageCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：33u";
            item.Value = 7;
            cbCommonVoltageCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：40u";
            item.Value = 8;
            cbCommonVoltageCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：48u";
            item.Value = 9;
            cbCommonVoltageCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：55u";
            item.Value = 10;
            cbCommonVoltageCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：63u";
            item.Value = 11;
            cbCommonVoltageCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：40u";
            item.Value = 12;
            cbCommonVoltageCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：48u";
            item.Value = 13;
            cbCommonVoltageCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：45u";
            item.Value = 14;
            cbCommonVoltageCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：63u";
            item.Value = 15;
            cbCommonVoltageCh1.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：-20u";
            item.Value = 0;
            cbCommonVoltageCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：-10u";
            item.Value = 1;
            cbCommonVoltageCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0u";
            item.Value = 2;
            cbCommonVoltageCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：10u";
            item.Value = 3;
            cbCommonVoltageCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：10u";
            item.Value = 4;
            cbCommonVoltageCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：18u";
            item.Value = 5;
            cbCommonVoltageCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：25u";
            item.Value = 6;
            cbCommonVoltageCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：33u";
            item.Value = 7;
            cbCommonVoltageCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：40u";
            item.Value = 8;
            cbCommonVoltageCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：48u";
            item.Value = 9;
            cbCommonVoltageCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：55u";
            item.Value = 10;
            cbCommonVoltageCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：63u";
            item.Value = 11;
            cbCommonVoltageCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：40u";
            item.Value = 12;
            cbCommonVoltageCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：48u";
            item.Value = 13;
            cbCommonVoltageCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：45u";
            item.Value = 14;
            cbCommonVoltageCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：63u";
            item.Value = 15;
            cbCommonVoltageCh2.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：-20u";
            item.Value = 0;
            cbCommonVoltageCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：-10u";
            item.Value = 1;
            cbCommonVoltageCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0u";
            item.Value = 2;
            cbCommonVoltageCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：10u";
            item.Value = 3;
            cbCommonVoltageCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：10u";
            item.Value = 4;
            cbCommonVoltageCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：18u";
            item.Value = 5;
            cbCommonVoltageCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：25u";
            item.Value = 6;
            cbCommonVoltageCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：33u";
            item.Value = 7;
            cbCommonVoltageCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：40u";
            item.Value = 8;
            cbCommonVoltageCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：48u";
            item.Value = 9;
            cbCommonVoltageCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：55u";
            item.Value = 10;
            cbCommonVoltageCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：63u";
            item.Value = 11;
            cbCommonVoltageCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：40u";
            item.Value = 12;
            cbCommonVoltageCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：48u";
            item.Value = 13;
            cbCommonVoltageCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：45u";
            item.Value = 14;
            cbCommonVoltageCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：63u";
            item.Value = 15;
            cbCommonVoltageCh3.Items.Add(item);

            for (i = 0; i < 4; i++)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + "Reserved";
                item.Value = i;
                cbAgcKick.Items.Add(item);
            }
            for (i = 4, dTmp = 80; i < 16; i++, dTmp += 40) {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp + "uA";
                item.Value = i;
                cbAgcKick.Items.Add(item);
            }
            


            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35) {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp.ToString() + "m";
                item.Value = i;
                cbOutputSwingCh0.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35) {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp.ToString() + "m";
                item.Value = i;
                cbOutputSwingCh1.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp.ToString() + "m";
                item.Value = i;
                cbOutputSwingCh2.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp.ToString() + "m";
                item.Value = i;
                cbOutputSwingCh3.Items.Add(item);
            }

            item = new ComboBoxItem();
            item.Text = "0：0dB";
            item.Value = 0;
            cbDeEmphasisCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4dB";
            item.Value = 1;
            cbDeEmphasisCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9dB";
            item.Value = 2;
            cbDeEmphasisCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3dB";
            item.Value = 3;
            cbDeEmphasisCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8dB";
            item.Value = 4;
            cbDeEmphasisCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3dB";
            item.Value = 5;
            cbDeEmphasisCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9dB";
            item.Value = 6;
            cbDeEmphasisCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5dB";
            item.Value = 7;
            cbDeEmphasisCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1dB";
            item.Value = 8;
            cbDeEmphasisCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8dB";
            item.Value = 9;
            cbDeEmphasisCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5dB";
            item.Value = 10;
            cbDeEmphasisCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3dB";
            item.Value = 11;
            cbDeEmphasisCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2dB";
            item.Value = 12;
            cbDeEmphasisCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1dB";
            item.Value = 13;
            cbDeEmphasisCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2dB";
            item.Value = 14;
            cbDeEmphasisCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5dB";
            item.Value = 15;
            cbDeEmphasisCh0.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0dB";
            item.Value = 0;
            cbDeEmphasisCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4dB";
            item.Value = 1;
            cbDeEmphasisCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9dB";
            item.Value = 2;
            cbDeEmphasisCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3dB";
            item.Value = 3;
            cbDeEmphasisCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8dB";
            item.Value = 4;
            cbDeEmphasisCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3dB";
            item.Value = 5;
            cbDeEmphasisCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9dB";
            item.Value = 6;
            cbDeEmphasisCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5dB";
            item.Value = 7;
            cbDeEmphasisCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1dB";
            item.Value = 8;
            cbDeEmphasisCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8dB";
            item.Value = 9;
            cbDeEmphasisCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5dB";
            item.Value = 10;
            cbDeEmphasisCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3dB";
            item.Value = 11;
            cbDeEmphasisCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2dB";
            item.Value = 12;
            cbDeEmphasisCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1dB";
            item.Value = 13;
            cbDeEmphasisCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2dB";
            item.Value = 14;
            cbDeEmphasisCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5dB";
            item.Value = 15;
            cbDeEmphasisCh1.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0dB";
            item.Value = 0;
            cbDeEmphasisCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4dB";
            item.Value = 1;
            cbDeEmphasisCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9dB";
            item.Value = 2;
            cbDeEmphasisCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3dB";
            item.Value = 3;
            cbDeEmphasisCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8dB";
            item.Value = 4;
            cbDeEmphasisCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3dB";
            item.Value = 5;
            cbDeEmphasisCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9dB";
            item.Value = 6;
            cbDeEmphasisCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5dB";
            item.Value = 7;
            cbDeEmphasisCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1dB";
            item.Value = 8;
            cbDeEmphasisCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8dB";
            item.Value = 9;
            cbDeEmphasisCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5dB";
            item.Value = 10;
            cbDeEmphasisCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3dB";
            item.Value = 11;
            cbDeEmphasisCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2dB";
            item.Value = 12;
            cbDeEmphasisCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1dB";
            item.Value = 13;
            cbDeEmphasisCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2dB";
            item.Value = 14;
            cbDeEmphasisCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5dB";
            item.Value = 15;
            cbDeEmphasisCh2.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0dB";
            item.Value = 0;
            cbDeEmphasisCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4dB";
            item.Value = 1;
            cbDeEmphasisCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9dB";
            item.Value = 2;
            cbDeEmphasisCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3dB";
            item.Value = 3;
            cbDeEmphasisCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8dB";
            item.Value = 4;
            cbDeEmphasisCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3dB";
            item.Value = 5;
            cbDeEmphasisCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9dB";
            item.Value = 6;
            cbDeEmphasisCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5dB";
            item.Value = 7;
            cbDeEmphasisCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1dB";
            item.Value = 8;
            cbDeEmphasisCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8dB";
            item.Value = 9;
            cbDeEmphasisCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5dB";
            item.Value = 10;
            cbDeEmphasisCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3dB";
            item.Value = 11;
            cbDeEmphasisCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2dB";
            item.Value = 12;
            cbDeEmphasisCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1dB";
            item.Value = 13;
            cbDeEmphasisCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2dB";
            item.Value = 14;
            cbDeEmphasisCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5dB";
            item.Value = 15;
            cbDeEmphasisCh3.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：on";
            item.Value = 0;
            cbImonToDdmiPwdCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：off";
            item.Value = 1;
            cbImonToDdmiPwdCh0.Items.Add(item);

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
            item.Text = "0：on";
            item.Value = 0;
            cbImonToDdmiPwdCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：off";
            item.Value = 1;
            cbImonToDdmiPwdCh1.Items.Add(item);

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
            item.Text = "0：on";
            item.Value = 0;
            cbImonToDdmiPwdCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：off";
            item.Value = 1;
            cbImonToDdmiPwdCh2.Items.Add(item);

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
            item.Text = "0：on";
            item.Value = 0;
            cbImonToDdmiPwdCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：off";
            item.Value = 1;
            cbImonToDdmiPwdCh3.Items.Add(item);

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
            cbDdmiAdcPwd.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：off";
            item.Value = 1;
            cbDdmiAdcPwd.Items.Add(item);

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
            item.Text = "0：on";
            item.Value = 0;
            cbEnableVf.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：off";
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

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35) {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmp100400Ch0.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35) {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmp100400Ch1.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35) {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmp100400Ch2.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35) {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmp100400Ch3.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35) {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmp300600Ch0.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmp300600Ch1.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmp300600Ch2.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmp300600Ch3.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmp400800Ch0.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmp400800Ch1.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmp400800Ch2.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmp400800Ch3.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmp6001200Ch0.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmp6001200Ch1.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmp6001200Ch2.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmp6001200Ch3.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved0Ch0.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved0Ch1.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved0Ch2.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved0Ch3.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved1Ch0.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved1Ch1.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved1Ch2.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved1Ch3.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved2Ch0.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved2Ch1.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved2Ch2.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved2Ch3.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved3Ch0.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved3Ch1.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved3Ch2.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved3Ch3.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved4Ch0.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved4Ch1.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved4Ch2.Items.Add(item);
            }

            for (i = 0, dTmp = 400; i < 16; i++, dTmp += 35)
            {
                item = new ComboBoxItem();
                item.Text = i + "：" + dTmp;
                item.Value = i;
                cbOutputAmpReserved4Ch3.Items.Add(item);
            }


            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis0dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis0dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis0dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis0dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis0dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis0dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis0dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis0dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis0dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis0dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis0dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis0dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis0dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis0dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis0dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis0dbCh0.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis0dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis0dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis0dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis0dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis0dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis0dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis0dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis0dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis0dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis0dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis0dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis0dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis0dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis0dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis0dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis0dbCh1.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis0dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis0dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis0dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis0dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis0dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis0dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis0dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis0dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis0dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis0dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis0dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis0dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis0dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis0dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis0dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis0dbCh2.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis0dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis0dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis0dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis0dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis0dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis0dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis0dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis0dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis0dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis0dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis0dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis0dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis0dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis0dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis0dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis0dbCh3.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis1dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis1dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis1dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis1dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis1dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis1dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis1dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis1dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis1dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis1dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis1dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis1dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis1dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis1dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis1dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis1dbCh0.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis1dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis1dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis1dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis1dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis1dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis1dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis1dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis1dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis1dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis1dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis1dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis1dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis1dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis1dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis1dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis1dbCh1.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis1dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis1dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis1dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis1dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis1dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis1dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis1dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis1dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis1dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis1dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis1dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis1dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis1dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis1dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis1dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis1dbCh2.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis1dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis1dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis1dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis1dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis1dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis1dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis1dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis1dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis1dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis1dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis1dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis1dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis1dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis1dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis1dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis1dbCh3.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis2dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis2dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis2dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis2dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis2dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis2dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis2dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis2dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis2dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis2dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis2dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis2dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis2dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis2dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis2dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis2dbCh0.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis2dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis2dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis2dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis2dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis2dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis2dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis2dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis2dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis2dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis2dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis2dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis2dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis2dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis2dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis2dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis2dbCh1.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis2dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis2dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis2dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis2dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis2dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis2dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis2dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis2dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis2dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis2dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis2dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis2dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis2dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis2dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis2dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis2dbCh2.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis2dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis2dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis2dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis2dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis2dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis2dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis2dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis2dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis2dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis2dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis2dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis2dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis2dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis2dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis2dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis2dbCh3.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis3dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis3dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis3dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis3dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis3dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis3dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis3dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis3dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis3dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis3dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis3dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis3dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis3dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis3dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis3dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis3dbCh0.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis3dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis3dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis3dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis3dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis3dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis3dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis3dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis3dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis3dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis3dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis3dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis3dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis3dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis3dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis3dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis3dbCh1.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis3dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis3dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis3dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis3dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis3dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis3dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis3dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis3dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis3dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis3dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis3dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis3dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis3dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis3dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis3dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis3dbCh2.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis3dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis3dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis3dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis3dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis3dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis3dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis3dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis3dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis3dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis3dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis3dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis3dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis3dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis3dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis3dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis3dbCh3.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis4dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis4dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis4dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis4dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis4dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis4dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis4dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis4dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis4dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis4dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis4dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis4dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis4dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis4dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis4dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis4dbCh0.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis4dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis4dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis4dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis4dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis4dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis4dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis4dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis4dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis4dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis4dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis4dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis4dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis4dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis4dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis4dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis4dbCh1.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis4dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis4dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis4dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis4dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis4dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis4dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis4dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis4dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis4dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis4dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis4dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis4dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis4dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis4dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis4dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis4dbCh2.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis4dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis4dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis4dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis4dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis4dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis4dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis4dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis4dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis4dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis4dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis4dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis4dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis4dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis4dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis4dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis4dbCh3.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis5dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis5dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis5dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis5dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis5dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis5dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis5dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis5dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis5dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis5dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis5dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis5dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis5dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis5dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis5dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis5dbCh0.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis5dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis5dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis5dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis5dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis5dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis5dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis5dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis5dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis5dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis5dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis5dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis5dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis5dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis5dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis5dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis5dbCh1.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis5dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis5dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis5dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis5dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis5dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis5dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis5dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis5dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis5dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis5dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis5dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis5dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis5dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis5dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis5dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis5dbCh2.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis5dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis5dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis5dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis5dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis5dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis5dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis5dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis5dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis5dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis5dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis5dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis5dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis5dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis5dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis5dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis5dbCh3.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis6dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis6dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis6dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis6dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis6dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis6dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis6dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis6dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis6dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis6dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis6dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis6dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis6dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis6dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis6dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis6dbCh0.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis6dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis6dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis6dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis6dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis6dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis6dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis6dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis6dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis6dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis6dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis6dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis6dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis6dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis6dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis6dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis6dbCh1.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis6dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis6dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis6dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis6dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis6dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis6dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis6dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis6dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis6dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis6dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis6dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis6dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis6dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis6dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis6dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis6dbCh2.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis6dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis6dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis6dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis6dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis6dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis6dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis6dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis6dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis6dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis6dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis6dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis6dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis6dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis6dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis6dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis6dbCh3.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis7dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis7dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis7dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis7dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis7dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis7dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis7dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis7dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis7dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis7dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis7dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis7dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis7dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis7dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis7dbCh0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis7dbCh0.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis7dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis7dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis7dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis7dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis7dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis7dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis7dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis7dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis7dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis7dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis7dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis7dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis7dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis7dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis7dbCh1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis7dbCh1.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis7dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis7dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis7dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis7dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis7dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis7dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis7dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis7dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis7dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis7dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis7dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis7dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis7dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis7dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis7dbCh2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis7dbCh2.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasis7dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasis7dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasis7dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasis7dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasis7dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasis7dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasis7dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasis7dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasis7dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasis7dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasis7dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasis7dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasis7dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasis7dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasis7dbCh3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasis7dbCh3.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasisR0Ch0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasisR0Ch0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasisR0Ch0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasisR0Ch0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasisR0Ch0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasisR0Ch0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasisR0Ch0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasisR0Ch0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasisR0Ch0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasisR0Ch0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasisR0Ch0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasisR0Ch0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasisR0Ch0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasisR0Ch0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasisR0Ch0.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasisR0Ch0.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasisR0Ch1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasisR0Ch1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasisR0Ch1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasisR0Ch1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasisR0Ch1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasisR0Ch1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasisR0Ch1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasisR0Ch1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasisR0Ch1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasisR0Ch1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasisR0Ch1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasisR0Ch1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasisR0Ch1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasisR0Ch1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasisR0Ch1.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasisR0Ch1.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasisR0Ch2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasisR0Ch2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasisR0Ch2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasisR0Ch2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasisR0Ch2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasisR0Ch2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasisR0Ch2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasisR0Ch2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasisR0Ch2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasisR0Ch2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasisR0Ch2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasisR0Ch2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasisR0Ch2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasisR0Ch2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasisR0Ch2.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasisR0Ch2.Items.Add(item);

            item = new ComboBoxItem();
            item.Text = "0：0.0";
            item.Value = 0;
            cbOutputEmphasisR0Ch3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "1：0.4";
            item.Value = 1;
            cbOutputEmphasisR0Ch3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "2：0.9";
            item.Value = 2;
            cbOutputEmphasisR0Ch3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "3：1.3";
            item.Value = 3;
            cbOutputEmphasisR0Ch3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "4：1.8";
            item.Value = 4;
            cbOutputEmphasisR0Ch3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "5：2.3";
            item.Value = 5;
            cbOutputEmphasisR0Ch3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "6：2.9";
            item.Value = 6;
            cbOutputEmphasisR0Ch3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "7：3.5";
            item.Value = 7;
            cbOutputEmphasisR0Ch3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "8：4.1";
            item.Value = 8;
            cbOutputEmphasisR0Ch3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "9：4.8";
            item.Value = 9;
            cbOutputEmphasisR0Ch3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "10：5.5";
            item.Value = 10;
            cbOutputEmphasisR0Ch3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "11：6.3";
            item.Value = 11;
            cbOutputEmphasisR0Ch3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "12：7.2";
            item.Value = 12;
            cbOutputEmphasisR0Ch3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "13：8.1";
            item.Value = 13;
            cbOutputEmphasisR0Ch3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "14：9.2";
            item.Value = 14;
            cbOutputEmphasisR0Ch3.Items.Add(item);
            item = new ComboBoxItem();
            item.Text = "15：10.5";
            item.Value = 15;
            cbOutputEmphasisR0Ch3.Items.Add(item);
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

        private byte _ReverseBit(byte data)
        {
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

        private void _ParseAddr01(byte data)
        {
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

            foreach (ComboBoxItem item in cbAdcOut.Items)
            {
                int iTmp;

                iTmp = (data0 & 0xFF) | ((data1 & 0x01) << 8);

                if (iTmp == item.Value) {
                    cbAdcOut.SelectedItem = item;
                }
            }
            foreach (ComboBoxItem item in cbAgcRssi.Items)
            {
                if ((data1 >> 1 & 0x07) == item.Value)
                {
                    cbAgcRssi.SelectedItem = item;
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

            if ((data & 0x04) == 0)
                cbModeRssi.SelectedIndex = 0;
            else
                cbModeRssi.SelectedIndex = 1;

            foreach (ComboBoxItem item in cbSelectRssi.Items)
            {
                if (item.Value == (data >> 3 & 0x03))
                {
                    cbSelectRssi.SelectedItem = item;
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
                if ((data & 0x03) == item.Value)
                {
                    cbAutoTuneLockTh.SelectedItem = item;
                }
            }
            foreach (ComboBoxItem item in cbAutoTuneUnlockTh.Items)
            {
                if ((data >> 2 & 0x03) == item.Value)
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
                if (item.Value == (data & 0x07))
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
                cbRxPowerControlCh0.Checked = false;
            else
                cbRxPowerControlCh0.Checked = true;

            if ((data & 0x02) == 0)
                cbRxPowerControlCh1.Checked = false;
            else
                cbRxPowerControlCh1.Checked = true;

            if ((data & 0x04) == 0)
                cbRxPowerControlCh2.Checked = false;
            else
                cbRxPowerControlCh2.Checked = true;

            if ((data & 0x08) == 0)
                cbRxPowerControlCh3.Checked = false;
            else
                cbRxPowerControlCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbRxCdrControlCh0.Checked = false;
            else
                cbRxCdrControlCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbRxCdrControlCh1.Checked = false;
            else
                cbRxCdrControlCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbRxCdrControlCh2.Checked = false;
            else
                cbRxCdrControlCh2.Checked = true;

            if ((data & 0x80) == 0)
                cbRxCdrControlCh3.Checked = false;
            else
                cbRxCdrControlCh3.Checked = true;
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
                cbTiaRateSelectCh0.Checked = false;
            else
                cbTiaRateSelectCh0.Checked = true;

            if ((data & 0x02) == 0)
                cbTiaRateSelectCh1.Checked = false;
            else
                cbTiaRateSelectCh1.Checked = true;

            if ((data & 0x04) == 0)
                cbTiaRateSelectCh2.Checked = false;
            else
                cbTiaRateSelectCh2.Checked = true;

            if ((data & 0x08) == 0)
                cbTiaRateSelectCh3.Checked = false;
            else
                cbTiaRateSelectCh3.Checked = true;
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
        }

        private void _ParseAddr14(byte data)
        {

            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbCommonVoltageCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                {
                    cbCommonVoltageCh0.SelectedItem = item;
                }
            }
            foreach (ComboBoxItem item in cbCommonVoltageCh1.Items)
            {
                if ((data >> 4 & 0x0F) == item.Value)
                {
                    cbCommonVoltageCh1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr15(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbCommonVoltageCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                {
                    cbCommonVoltageCh2.SelectedItem = item;
                }
            }
            foreach (ComboBoxItem item in cbCommonVoltageCh3.Items)
            {
                if ((data >> 4 & 0x0F) == item.Value)
                {
                    cbCommonVoltageCh3.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr16(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbAgcKick.Items)
            {
                if ((data & 0x0F) == item.Value)
                {
                    cbAgcKick.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr17(byte data)
        {
            //data = _ReverseBit(data);            

            if ((data & 0x01) == 0)
                cbMuteCh0.Checked = false;
            else
                cbMuteCh0.Checked = true;

            if ((data & 0x02) == 0)
                cbMuteCh1.Checked = false;
            else
                cbMuteCh1.Checked = true;

            if ((data & 0x04) == 0)
                cbMuteCh2.Checked = false;
            else
                cbMuteCh2.Checked = true;

            if ((data & 0x08) == 0)
                cbMuteCh3.Checked = false;
            else
                cbMuteCh3.Checked = true;

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

            foreach (ComboBoxItem item in cbOutputSwingCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                {
                    cbOutputSwingCh0.SelectedItem = item;
                }
            }
            foreach (ComboBoxItem item in cbOutputSwingCh1.Items)
            {
                if ((data >> 4 & 0x0F) == item.Value)
                {
                    cbOutputSwingCh1.SelectedItem = item;
                }
            }
        }

        private void _ParseAddr19(byte data)
        {

            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputSwingCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                {
                    cbOutputSwingCh2.SelectedItem = item;
                }
            }
            foreach (ComboBoxItem item in cbOutputSwingCh3.Items)
            {
                if ((data >> 4 & 0x0F) == item.Value)
                {
                    cbOutputSwingCh3.SelectedItem = item;
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
                cbDdmiAdcPwd.SelectedIndex = 0;
            else
                cbDdmiAdcPwd.SelectedIndex = 1;

            foreach (ComboBoxItem item in cbDdmiChannelSelect.Items)
            {
                if ((data >> 1 & 0x07) == item.Value)
                {
                    cbDdmiChannelSelect.SelectedItem = item;
                }
            }
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
                cbTiaPwdCh0.Checked = false;
            else
                cbTiaPwdCh0.Checked = true;

            if ((data & 0x02) == 0)
                cbTiaPwdCh1.Checked = false;
            else
                cbTiaPwdCh1.Checked = true;

            if ((data & 0x04) == 0)
                cbTiaPwdCh2.Checked = false;
            else
                cbTiaPwdCh2.Checked = true;

            if ((data & 0x08) == 0)
                cbTiaPwdCh3.Checked = false;
            else
                cbTiaPwdCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbCmlDriverPwdCh0.Checked = false;
            else
                cbCmlDriverPwdCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbCmlDriverPwdCh1.Checked = false;
            else
                cbCmlDriverPwdCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbCmlDriverPwdCh2.Checked = false;
            else
                cbCmlDriverPwdCh2.Checked = true;

            if ((data & 0x80) == 0)
                cbCmlDriverPwdCh3.Checked = false;
            else
                cbCmlDriverPwdCh3.Checked = true;
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

        private void _ParseAddr34(byte data)
        {
            //data = _ReverseBit(data);            

            if ((data & 0x02) == 0)
                cbImonToDdmiPwdCh0.SelectedIndex = 0;
            else
                cbImonToDdmiPwdCh0.SelectedIndex = 1;            
        }

        private void _ParseAddr42(byte data)
        {
            //data = _ReverseBit(data);            

            if ((data & 0x80) == 0)
                cbAutoBypassResetCh0.SelectedIndex = 0;
            else
                cbAutoBypassResetCh0.SelectedIndex = 1;
        }

        private void _ParseAddr48(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbVcoMsbSelecCh0.Items)
            {
                if ((data >> 6 & 0x03) == item.Value)
                    cbVcoMsbSelecCh0.SelectedItem = item;
            }   
        }

        private void _ParseAddr54(byte data)
        {
            //data = _ReverseBit(data);            

            if ((data & 0x02) == 0)
                cbImonToDdmiPwdCh1.SelectedIndex = 0;
            else
                cbImonToDdmiPwdCh1.SelectedIndex = 1;
        }

        private void _ParseAddr62(byte data)
        {
            //data = _ReverseBit(data);            

            if ((data & 0x80) == 0)
                cbAutoBypassResetCh1.SelectedIndex = 0;
            else
                cbAutoBypassResetCh1.SelectedIndex = 1;
        }

        private void _ParseAddr68(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbVcoMsbSelecCh1.Items)
            {
                if ((data >> 6 & 0x3) == item.Value)
                    cbVcoMsbSelecCh1.SelectedItem = item;
            }
        }

        private void _ParseAddr74(byte data)
        {
            //data = _ReverseBit(data);            

            if ((data & 0x02) == 0)
                cbImonToDdmiPwdCh2.SelectedIndex = 0;
            else
                cbImonToDdmiPwdCh2.SelectedIndex = 1;
        }

        private void _ParseAddr82(byte data)
        {
            //data = _ReverseBit(data);            

            if ((data & 0x80) == 0)
                cbAutoBypassResetCh2.SelectedIndex = 0;
            else
                cbAutoBypassResetCh2.SelectedIndex = 1;
        }

        private void _ParseAddr88(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbVcoMsbSelecCh2.Items)
            {
                if ((data >> 6 & 0x03) == item.Value)
                    cbVcoMsbSelecCh2.SelectedItem = item;
            }
        }

        private void _ParseAddr94(byte data)
        {
            //data = _ReverseBit(data);            

            if ((data & 0x02) == 0)
                cbImonToDdmiPwdCh3.SelectedIndex = 0;
            else
                cbImonToDdmiPwdCh3.SelectedIndex = 1;
        }

        private void _ParseAddrA2(byte data)
        {
            //data = _ReverseBit(data);            

            if ((data & 0x80) == 0)
                cbAutoBypassResetCh3.SelectedIndex = 0;
            else
                cbAutoBypassResetCh3.SelectedIndex = 1;
        }

        private void _ParseAddrA8(byte data)
        {
            //data = _ReverseBit(data);                        

            foreach (ComboBoxItem item in cbVcoMsbSelecCh3.Items)
            {
                if ((data >> 6 & 0x03) == item.Value)
                    cbVcoMsbSelecCh3.SelectedItem = item;
            }
        }

        private void _ParseAddrB0(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmp100400Ch0.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmp100400Ch0.SelectedItem = item;
            }
        }

        private void _ParseAddrB1(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmp300600Ch0.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmp300600Ch0.SelectedItem = item;
            }
        }

        private void _ParseAddrB2(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmp400800Ch0.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmp400800Ch0.SelectedItem = item;
            }
        }

        private void _ParseAddrB3(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmp6001200Ch0.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmp6001200Ch0.SelectedItem = item;
            }
        }

        private void _ParseAddrB4(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved0Ch0.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved0Ch0.SelectedItem = item;
            }
        }

        private void _ParseAddrB5(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved1Ch0.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved1Ch0.SelectedItem = item;
            }
        }

        private void _ParseAddrB6(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved2Ch0.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved2Ch0.SelectedItem = item;
            }
        }

        private void _ParseAddrB7(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved3Ch0.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved3Ch0.SelectedItem = item;
            }
        }

        private void _ParseAddrB8(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved4Ch0.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved4Ch0.SelectedItem = item;
            }
        }

        private void _ParseAddrB9(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis0dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis0dbCh0.SelectedItem = item;
            }
        }

        private void _ParseAddrBA(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis1dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis1dbCh0.SelectedItem = item;
            }
        }

        private void _ParseAddrBB(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis2dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis2dbCh0.SelectedItem = item;
            }
        }

        private void _ParseAddrBC(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis3dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis3dbCh0.SelectedItem = item;
            }
        }

        private void _ParseAddrBD(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis4dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis4dbCh0.SelectedItem = item;
            }
        }

        private void _ParseAddrBE(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis5dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis5dbCh0.SelectedItem = item;
            }
        }

        private void _ParseAddrBF(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis6dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis6dbCh0.SelectedItem = item;
            }
        }

        private void _ParseAddrC0(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis7dbCh0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis7dbCh0.SelectedItem = item;
            }
        }

        private void _ParseAddrC1(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasisR0Ch0.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasisR0Ch0.SelectedItem = item;
            }
        }

        private void _ParseAddrC2(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmp100400Ch1.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmp100400Ch1.SelectedItem = item;
            }
        }

        private void _ParseAddrC3(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmp300600Ch1.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmp300600Ch1.SelectedItem = item;
            }
        }

        private void _ParseAddrC4(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmp400800Ch1.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmp400800Ch1.SelectedItem = item;
            }
        }

        private void _ParseAddrC5(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmp6001200Ch1.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmp6001200Ch1.SelectedItem = item;
            }
        }

        private void _ParseAddrC6(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved0Ch1.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved0Ch1.SelectedItem = item;
            }
        }

        private void _ParseAddrC7(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved1Ch1.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved1Ch1.SelectedItem = item;
            }
        }

        private void _ParseAddrC8(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved2Ch1.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved2Ch1.SelectedItem = item;
            }
        }

        private void _ParseAddrC9(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved3Ch1.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved3Ch1.SelectedItem = item;
            }
        }

        private void _ParseAddrCA(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved4Ch1.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved4Ch1.SelectedItem = item;
            }
        }

        private void _ParseAddrCB(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis0dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis0dbCh1.SelectedItem = item;
            }
        }

        private void _ParseAddrCC(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis1dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis1dbCh1.SelectedItem = item;
            }
        }

        private void _ParseAddrCD(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis2dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis2dbCh1.SelectedItem = item;
            }
        }

        private void _ParseAddrCE(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis3dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis3dbCh1.SelectedItem = item;
            }
        }

        private void _ParseAddrCF(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis4dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis4dbCh1.SelectedItem = item;
            }
        }

        private void _ParseAddrD0(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis5dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis5dbCh1.SelectedItem = item;
            }
        }

        private void _ParseAddrD1(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis6dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis6dbCh1.SelectedItem = item;
            }
        }

        private void _ParseAddrD2(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis7dbCh1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis7dbCh1.SelectedItem = item;
            }
        }

        private void _ParseAddrD3(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasisR0Ch1.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasisR0Ch1.SelectedItem = item;
            }
        }

        private void _ParseAddrD4(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmp100400Ch2.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmp100400Ch2.SelectedItem = item;
            }
        }

        private void _ParseAddrD5(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmp300600Ch2.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmp300600Ch2.SelectedItem = item;
            }
        }

        private void _ParseAddrD6(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmp400800Ch2.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmp400800Ch2.SelectedItem = item;
            }
        }

        private void _ParseAddrD7(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmp6001200Ch2.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmp6001200Ch2.SelectedItem = item;
            }
        }

        private void _ParseAddrD8(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved0Ch2.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved0Ch2.SelectedItem = item;
            }
        }

        private void _ParseAddrD9(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved1Ch2.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved1Ch2.SelectedItem = item;
            }
        }

        private void _ParseAddrDA(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved2Ch2.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved2Ch2.SelectedItem = item;
            }
        }

        private void _ParseAddrDB(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved3Ch2.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved3Ch2.SelectedItem = item;
            }
        }

        private void _ParseAddrDC(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved4Ch2.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved4Ch2.SelectedItem = item;
            }
        }

        private void _ParseAddrDD(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis0dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis0dbCh2.SelectedItem = item;
            }
        }

        private void _ParseAddrDE(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis1dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis1dbCh2.SelectedItem = item;
            }
        }

        private void _ParseAddrDF(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis2dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis2dbCh2.SelectedItem = item;
            }
        }

        private void _ParseAddrE0(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis3dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis3dbCh2.SelectedItem = item;
            }
        }

        private void _ParseAddrE1(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis4dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis4dbCh2.SelectedItem = item;
            }
        }

        private void _ParseAddrE2(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis5dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis5dbCh2.SelectedItem = item;
            }
        }

        private void _ParseAddrE3(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis6dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis6dbCh2.SelectedItem = item;
            }
        }

        private void _ParseAddrE4(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis7dbCh2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis7dbCh2.SelectedItem = item;
            }
        }

        private void _ParseAddrE5(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasisR0Ch2.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasisR0Ch2.SelectedItem = item;
            }
        }

        private void _ParseAddrE6(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmp100400Ch3.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmp100400Ch3.SelectedItem = item;
            }
        }

        private void _ParseAddrE7(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmp300600Ch3.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmp300600Ch3.SelectedItem = item;
            }
        }

        private void _ParseAddrE8(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmp400800Ch3.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmp400800Ch3.SelectedItem = item;
            }
        }

        private void _ParseAddrE9(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmp6001200Ch3.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmp6001200Ch3.SelectedItem = item;
            }
        }

        private void _ParseAddrEA(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved0Ch3.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved0Ch3.SelectedItem = item;
            }
        }

        private void _ParseAddrEB(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved1Ch3.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved1Ch3.SelectedItem = item;
            }
        }

        private void _ParseAddrEC(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved2Ch3.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved2Ch3.SelectedItem = item;
            }
        }

        private void _ParseAddrED(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved3Ch3.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved3Ch3.SelectedItem = item;
            }
        }

        private void _ParseAddrEE(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputAmpReserved4Ch3.Items)
            {
                if ((data & 0x3F) == item.Value)
                    cbOutputAmpReserved4Ch3.SelectedItem = item;
            }
        }

        private void _ParseAddrEF(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis0dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis0dbCh3.SelectedItem = item;
            }
        }

        private void _ParseAddrF0(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis1dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis1dbCh3.SelectedItem = item;
            }
        }

        private void _ParseAddrF1(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis2dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis2dbCh3.SelectedItem = item;
            }
        }

        private void _ParseAddrF2(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis3dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis3dbCh3.SelectedItem = item;
            }
        }

        private void _ParseAddrF3(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis4dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis4dbCh3.SelectedItem = item;
            }
        }

        private void _ParseAddrF4(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis5dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis5dbCh3.SelectedItem = item;
            }
        }

        private void _ParseAddrF5(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis6dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis6dbCh3.SelectedItem = item;
            }
        }

        private void _ParseAddrF6(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasis7dbCh3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasis7dbCh3.SelectedItem = item;
            }
        }

        private void _ParseAddrF7(byte data)
        {
            //data = _ReverseBit(data);            

            foreach (ComboBoxItem item in cbOutputEmphasisR0Ch3.Items)
            {
                if ((data & 0x0F) == item.Value)
                    cbOutputEmphasisR0Ch3.SelectedItem = item;
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
                sb.Append($"\"Rx\",\"{i:X2}\"");
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
            byte[] data = new byte[72];
            int rv;

            if (reading == true)
                return 0;

            reading = true;

            if (i2cReadCB == null)
                goto exit;

            rv = i2cReadCB(devAddr, 0x00, 6, data);
            if (rv != 6)
                goto exit;

            _ParseAddr00(data[0]);
            _ParseAddr01(data[1]);
            _ParseAddr02(data[2]);
            _ParseAddr03(data[3]);
            _ParseAddr04_05(data[4], data[5]);

            rv = i2cReadCB(devAddr, 0x08, 27, data);
            if (rv != 27)
                goto exit;

            _ParseAddr08(data[0]);
            _ParseAddr09(data[1]);
            _ParseAddr0A(data[2]);
            _ParseAddr0B(data[3]);
            _ParseAddr0C(data[4]);
            _ParseAddr0D(data[5]);
            _ParseAddr0E(data[6]);
            _ParseAddr0F(data[7]);
            _ParseAddr10(data[8]);
            _ParseAddr11(data[9]);
            _ParseAddr12(data[10]);
            _ParseAddr13(data[11]);
            _ParseAddr14(data[12]);
            _ParseAddr15(data[13]);
            _ParseAddr16(data[14]);
            _ParseAddr17(data[15]);
            _ParseAddr18(data[16]);
            _ParseAddr19(data[17]);
            _ParseAddr1A(data[18]);
            _ParseAddr1B(data[19]);
            _ParseAddr1C(data[20]);
            _ParseAddr1D_1E_1F(data[21], data[22], data[23]);
            _ParseAddr20(data[24]);
            _ParseAddr21(data[25]);
            _ParseAddr22(data[26]);

            rv = i2cReadCB(devAddr, 0x34, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr34(data[0]);

            rv = i2cReadCB(devAddr, 0x42, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr42(data[0]);

            rv = i2cReadCB(devAddr, 0x48, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr48(data[0]);

            rv = i2cReadCB(devAddr, 0x54, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr54(data[0]);

            rv = i2cReadCB(devAddr, 0x62, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr62(data[0]);

            rv = i2cReadCB(devAddr, 0x68, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr68(data[0]);

            rv = i2cReadCB(devAddr, 0x74, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr74(data[0]);

            rv = i2cReadCB(devAddr, 0x82, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr82(data[0]);

            rv = i2cReadCB(devAddr, 0x88, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr88(data[0]);

            rv = i2cReadCB(devAddr, 0x94, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr94(data[0]);

            rv = i2cReadCB(devAddr, 0xA2, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddrA2(data[0]);

            rv = i2cReadCB(devAddr, 0xA8, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddrA8(data[0]);

            rv = i2cReadCB(devAddr, 0xB0, 56, data);
            if (rv != 56)
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
            _ParseAddrE4(data[52]);
            _ParseAddrE5(data[53]);
            _ParseAddrE6(data[54]);
            _ParseAddrE7(data[55]);

            rv = i2cReadCB(devAddr, 0xE8, 16, data);
            if (rv != 16)
                goto exit;

            _ParseAddrE8(data[0]);
            _ParseAddrE9(data[1]);
            _ParseAddrEA(data[2]);
            _ParseAddrEB(data[3]);
            _ParseAddrEC(data[4]);
            _ParseAddrED(data[5]);
            _ParseAddrEE(data[6]);
            _ParseAddrEF(data[7]);
            _ParseAddrF0(data[8]);
            _ParseAddrF1(data[9]);
            _ParseAddrF2(data[10]);
            _ParseAddrF3(data[11]);
            _ParseAddrF4(data[12]);
            _ParseAddrF5(data[13]);
            _ParseAddrF6(data[14]);
            _ParseAddrF7(data[15]);
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
            if (_WriteAddr16() < 0) return -1;
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
            if (_WriteAddr34() < 0) return -1;
            if (_WriteAddr42() < 0) return -1;
            if (_WriteAddr48() < 0) return -1;
            if (_WriteAddr54() < 0) return -1;
            if (_WriteAddr62() < 0) return -1;
            if (_WriteAddr68() < 0) return -1;
            if (_WriteAddr74() < 0) return -1;
            if (_WriteAddr82() < 0) return -1;
            if (_WriteAddr88() < 0) return -1;
            if (_WriteAddr94() < 0) return -1;
            if (_WriteAddrA2() < 0) return -1;
            if (_WriteAddrA8() < 0) return -1;
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
            if (_WriteAddrE4() < 0) return -1;
            if (_WriteAddrE5() < 0) return -1;
            if (_WriteAddrE6() < 0) return -1;
            if (_WriteAddrE7() < 0) return -1;
            if (_WriteAddrE8() < 0) return -1;
            if (_WriteAddrE9() < 0) return -1;
            if (_WriteAddrEA() < 0) return -1;
            if (_WriteAddrEB() < 0) return -1;
            if (_WriteAddrEC() < 0) return -1;
            if (_WriteAddrED() < 0) return -1;
            if (_WriteAddrEE() < 0) return -1;
            if (_WriteAddrEF() < 0) return -1;
            if (_WriteAddrF0() < 0) return -1;
            if (_WriteAddrF1() < 0) return -1;
            if (_WriteAddrF2() < 0) return -1;
            if (_WriteAddrF3() < 0) return -1;
            if (_WriteAddrF4() < 0) return -1;
            if (_WriteAddrF5() < 0) return -1;
            if (_WriteAddrF6() < 0) return -1;
            if (_WriteAddrF7() < 0) return -1;

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

            bTmp = Convert.ToByte(cbModeRssi.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbSelectRssi.SelectedIndex);
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

            if (cbRxPowerControlCh0.Checked == true)
                data[0] |= 0x01;

            if (cbRxPowerControlCh1.Checked == true)
                data[0] |= 0x02;

            if (cbRxPowerControlCh2.Checked == true)
                data[0] |= 0x04;

            if (cbRxPowerControlCh3.Checked == true)
                data[0] |= 0x08;

            if (cbRxCdrControlCh0.Checked == true)
                data[0] |= 0x10;

            if (cbRxCdrControlCh1.Checked == true)
                data[0] |= 0x20;

            if (cbRxCdrControlCh2.Checked == true)
                data[0] |= 0x40;

            if (cbRxCdrControlCh3.Checked == true)
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

            rv = i2cWriteCB(devAddr, 0x0D, 1, data);
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

            if (cbTiaRateSelectCh0.Checked == true)
                data[0] |= 0x01;

            if (cbTiaRateSelectCh1.Checked == true)
                data[0] |= 0x02;

            if (cbTiaRateSelectCh2.Checked == true)
                data[0] |= 0x04;

            if (cbTiaRateSelectCh3.Checked == true)
                data[0] |= 0x08;

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

            bTmp = Convert.ToByte(cbCommonVoltageCh0.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbCommonVoltageCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbCommonVoltageCh2.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbCommonVoltageCh3.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x15, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbAgcKick.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x16, 1, data);
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

            if (cbMuteCh0.Checked == true)
                data[0] |= 0x01;

            if (cbMuteCh1.Checked == true)
                data[0] |= 0x02;

            if (cbMuteCh2.Checked == true)
                data[0] |= 0x04;

            if (cbMuteCh3.Checked == true)
                data[0] |= 0x08;

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

            bTmp = Convert.ToByte(cbOutputSwingCh0.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbOutputSwingCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputSwingCh2.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbOutputSwingCh3.SelectedIndex);
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

            bTmp = Convert.ToByte(cbDdmiAdcPwd.SelectedIndex);
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDdmiChannelSelect.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;           

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

            if (cbTiaPwdCh0.Checked == true)
                data[0] = 0x01;

            if (cbTiaPwdCh1.Checked == true)
                data[0] = 0x02;

            if (cbTiaPwdCh2.Checked == true)
                data[0] = 0x04;

            if (cbTiaPwdCh3.Checked == true)
                data[0] = 0x08;

            if (cbCmlDriverPwdCh0.Checked == true)
                data[0] |= 0x10;

            if (cbCmlDriverPwdCh1.Checked == true)
                data[0] |= 0x20;

            if (cbCmlDriverPwdCh2.Checked == true)
                data[0] |= 0x40;

            if (cbCmlDriverPwdCh3.Checked == true)
                data[0] |= 0x80;

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

        private int _WriteAddr34()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbImonToDdmiPwdCh0.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x34, 1, data);
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

            bTmp = Convert.ToByte(cbAutoBypassResetCh0.SelectedIndex);
            bTmp <<= 7;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x42, 1, data);
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

            bTmp = Convert.ToByte(cbVcoMsbSelecCh0.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x48, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr54()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbImonToDdmiPwdCh1.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x54, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr62()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbAutoBypassResetCh1.SelectedIndex);
            bTmp <<= 7;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x62, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr68()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbVcoMsbSelecCh1.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x68, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr74()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbImonToDdmiPwdCh2.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x74, 1, data);
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

            bTmp = Convert.ToByte(cbAutoBypassResetCh2.SelectedIndex);
            bTmp <<= 7;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x82, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr88()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbVcoMsbSelecCh2.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x88, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddr94()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbImonToDdmiPwdCh3.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0x94, 1, data);
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

            bTmp = Convert.ToByte(cbAutoBypassResetCh3.SelectedIndex);
            bTmp <<= 7;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xA2, 1, data);
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

            bTmp = Convert.ToByte(cbVcoMsbSelecCh3.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xA8, 1, data);
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

            bTmp = Convert.ToByte(cbOutputAmp100400Ch0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmp300600Ch0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmp400800Ch0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmp6001200Ch0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmpReserved0Ch0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmpReserved1Ch0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmpReserved2Ch0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmpReserved3Ch0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmpReserved4Ch0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis0dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis1dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis2dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis3dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis4dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis5dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis6dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis7dbCh0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasisR0Ch0.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmp100400Ch1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmp300600Ch1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmp400800Ch1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmp6001200Ch1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmpReserved0Ch1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmpReserved1Ch1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmpReserved2Ch1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmpReserved3Ch1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmpReserved4Ch1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis0dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis1dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis2dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis3dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis4dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis5dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis6dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis7dbCh1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasisR0Ch1.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmp100400Ch2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmp300600Ch2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmp400800Ch2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmp6001200Ch2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmpReserved0Ch2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmpReserved1Ch2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmpReserved2Ch2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmpReserved3Ch2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmpReserved4Ch2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis0dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis1dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis2dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis3dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis4dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis5dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis6dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasis7dbCh2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputEmphasisR0Ch2.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmp100400Ch3.SelectedIndex);
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

            bTmp = Convert.ToByte(cbOutputAmp300600Ch3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xE7, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrE8()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputAmp400800Ch3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xE8, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrE9()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputAmp6001200Ch3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xE9, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrEA()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputAmpReserved0Ch3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xEA, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrEB()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputAmpReserved1Ch3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xEB, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrEC()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputAmpReserved2Ch3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xEC, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrED()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputAmpReserved3Ch3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xED, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrEE()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputAmpReserved4Ch3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xEE, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrEF()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputEmphasis0dbCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xEF, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrF0()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputEmphasis1dbCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xF0, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrF1()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputEmphasis2dbCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xF1, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrF2()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputEmphasis3dbCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xF2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrF3()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputEmphasis4dbCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xF3, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrF4()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputEmphasis5dbCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xF4, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrF5()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputEmphasis6dbCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xF5, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrF6()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputEmphasis7dbCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xF6, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WriteAddrF7()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            bTmp = Convert.ToByte(cbOutputEmphasisR0Ch3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(devAddr, 0xF7, 1, data);
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

        private void cbModeRssi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr08() < 0)
                return;
        }

        private void cbSelectRssi_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cbRxPowerControlCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0C() < 0)
                return;
        }

        private void cbRxPowerControlCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0C() < 0)
                return;
        }

        private void cbRxPowerControlCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0C() < 0)
                return;
        }

        private void cbRxPowerControlCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0C() < 0)
                return;
        }

        private void cbRxCdrControlCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0C() < 0)
                return;
        }

        private void cbRxCdrControlCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0C() < 0)
                return;
        }

        private void cbRxCdrControlCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0C() < 0)
                return;
        }

        private void cbRxCdrControlCh3_CheckedChanged(object sender, EventArgs e)
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

        private void cbTiaRateSelectCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbTiaRateSelectCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbTiaRateSelectCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void cbTiaRateSelectCh3_CheckedChanged(object sender, EventArgs e)
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

        private void cbCommonVoltageCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr14() < 0)
                return;
        }

        private void cbCommonVoltageCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr14() < 0)
                return;
        }

        private void cbCommonVoltageCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr15() < 0)
                return;
        }

        private void cbCommonVoltageCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr15() < 0)
                return;
        }

        private void cbAgcKick_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr16() < 0)
                return;
        }

        private void cbMuteCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr17() < 0)
                return;
        }

        private void cbMuteCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr17() < 0)
                return;
        }

        private void cbMuteCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr17() < 0)
                return;
        }

        private void cbMuteCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr17() < 0)
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

        private void cbOutputSwingCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr18() < 0)
                return;
        }

        private void cbOutputSwingCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr18() < 0)
                return;
        }

        private void cbOutputSwingCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19() < 0)
                return;
        }

        private void cbOutputSwingCh3_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cbDdmiAdcPwd_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cbTiaPwdCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21() < 0)
                return;
        }

        private void cbTiaPwdCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21() < 0)
                return;
        }

        private void cbTiaPwdCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21() < 0)
                return;
        }

        private void cbTiaPwdCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21() < 0)
                return;
        }

        private void cbCmlDriverPwdCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21() < 0)
                return;
        }

        private void cbCmlDriverPwdCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21() < 0)
                return;
        }

        private void cbCmlDriverPwdCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21() < 0)
                return;
        }

        private void cbCmlDriverPwdCh3_CheckedChanged(object sender, EventArgs e)
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

        private void cbImonToDdmiPwdCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr34() < 0)
                return;
        }

        private void cbAutoBypassResetCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr42() < 0)
                return;
        }

        private void cbVcoMsbSelecCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr48() < 0)
                return;
        }

        private void cbImonToDdmiPwdCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr54() < 0)
                return;
        }

        private void cbAutoBypassResetCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr62() < 0)
                return;
        }

        private void cbVcoMsbSelecCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr68() < 0)
                return;
        }

        private void cbImonToDdmiPwdCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr74() < 0)
                return;
        }

        private void cbAutoBypassResetCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr82() < 0)
                return;
        }

        private void cbVcoMsbSelecCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr88() < 0)
                return;
        }

        private void cbImonToDdmiPwdCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr94() < 0)
                return;
        }

        private void cbAutoBypassResetCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA2() < 0)
                return;
        }

        private void cbVcoMsbSelecCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrA8() < 0)
                return;
        }
        
        private void cbOutputAmp100400Ch0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB0() < 0)
                return;
        }

        private void cbOutputAmp300600Ch0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB1() < 0)
                return;
        }

        private void cbOutputAmp400800Ch0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB2() < 0)
                return;
        }

        private void cbOutputAmp6001200Ch0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB3() < 0)
                return;
        }

        private void cbOutputAmpReserved0Ch0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB4() < 0)
                return;
        }

        private void cbOutputAmpReserved1Ch0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB5() < 0)
                return;
        }

        private void cbOutputAmpReserved2Ch0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB6() < 0)
                return;
        }

        private void cbOutputAmpReserved3Ch0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB7() < 0)
                return;
        }

        private void cbOutputAmpReserved4Ch0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB8() < 0)
                return;
        }

        private void cbOutputEmphasis0dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrB9() < 0)
                return;
        }

        private void cbOutputEmphasis1dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBA() < 0)
                return;
        }

        private void cbOutputEmphasis2dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBB() < 0)
                return;
        }

        private void cbOutputEmphasis3dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBC() < 0)
                return;
        }

        private void cbOutputEmphasis4dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBD() < 0)
                return;
        }

        private void cbOutputEmphasis5dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBE() < 0)
                return;
        }

        private void cbOutputEmphasis6dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrBF() < 0)
                return;
        }

        private void cbOutputEmphasis7dbCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC0() < 0)
                return;
        }

        private void cbOutputEmphasisR0Ch0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC1() < 0)
                return;
        }

        private void cbOutputAmp100400Ch1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC2() < 0)
                return;
        }

        private void cbOutputAmp300600Ch1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC3() < 0)
                return;
        }

        private void cbOutputAmp400800Ch1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC4() < 0)
                return;
        }

        private void cbOutputAmp6001200Ch1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC5() < 0)
                return;
        }

        private void cbOutputAmpReserved0Ch1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC6() < 0)
                return;
        }

        private void cbOutputAmpReserved1Ch1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC7() < 0)
                return;
        }

        private void cbOutputAmpReserved2Ch1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC8() < 0)
                return;
        }

        private void cbOutputAmpReserved3Ch1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrC9() < 0)
                return;
        }

        private void cbOutputAmpReserved4Ch1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCA() < 0)
                return;
        }

        private void cbOutputEmphasis0dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCB() < 0)
                return;
        }

        private void cbOutputEmphasis1dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCC() < 0)
                return;
        }

        private void cbOutputEmphasis2dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCD() < 0)
                return;
        }

        private void cbOutputEmphasis3dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCE() < 0)
                return;
        }

        private void cbOutputEmphasis4dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrCF() < 0)
                return;
        }

        private void cbOutputEmphasis5dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD0() < 0)
                return;
        }

        private void cbOutputEmphasis6dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD1() < 0)
                return;
        }

        private void cbOutputEmphasis7dbCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD2() < 0)
                return;
        }

        private void cbOutputEmphasisR0Ch1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD3() < 0)
                return;
        }

        private void cbOutputAmp100400Ch2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD4() < 0)
                return;
        }

        private void cbOutputAmp300600Ch2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD5() < 0)
                return;
        }

        private void cbOutputAmp400800Ch2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD6() < 0)
                return;
        }

        private void cbOutputAmp6001200Ch2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD7() < 0)
                return;
        }

        private void cbOutputAmpReserved0Ch2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD8() < 0)
                return;
        }

        private void cbOutputAmpReserved1Ch2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrD9() < 0)
                return;
        }

        private void cbOutputAmpReserved2Ch2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDA() < 0)
                return;
        }

        private void cbOutputAmpReserved3Ch2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDB() < 0)
                return;
        }

        private void cbOutputAmpReserved4Ch2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDC() < 0)
                return;
        }

        private void cbOutputEmphasis0dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDD() < 0)
                return;
        }

        private void cbOutputEmphasis1dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDE() < 0)
                return;
        }

        private void cbOutputEmphasis2dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrDF() < 0)
                return;
        }

        private void cbOutputEmphasis3dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE0() < 0)
                return;
        }

        private void cbOutputEmphasis4dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE1() < 0)
                return;
        }

        private void cbOutputEmphasis5dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE2() < 0)
                return;
        }

        private void cbOutputEmphasis6dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE3() < 0)
                return;
        }

        private void cbOutputEmphasis7dbCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE4() < 0)
                return;
        }

        private void cbOutputEmphasisR0Ch2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE5() < 0)
                return;
        }

        private void cbOutputAmp100400Ch3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE6() < 0)
                return;
        }

        private void cbOutputAmp300600Ch3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE7() < 0)
                return;
        }

        private void cbOutputAmp400800Ch3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE8() < 0)
                return;
        }

        private void cbOutputAmp6001200Ch3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrE9() < 0)
                return;
        }

        private void cbOutputAmpReserved0Ch3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrEA() < 0)
                return;
        }

        private void cbOutputAmpReserved1Ch3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrEB() < 0)
                return;
        }

        private void cbOutputAmpReserved2Ch3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrEC() < 0)
                return;
        }

        private void cbOutputAmpReserved3Ch3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrED() < 0)
                return;
        }

        private void cbOutputAmpReserved4Ch3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrEE() < 0)
                return;
        }

        private void cbOutputEmphasis0dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrEF() < 0)
                return;
        }

        private void cbOutputEmphasis1dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrF0() < 0)
                return;
        }

        private void cbOutputEmphasis2dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrF1() < 0)
                return;
        }

        private void cbOutputEmphasis3dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrF2() < 0)
                return;
        }

        private void cbOutputEmphasis4dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrF3() < 0)
                return;
        }

        private void cbOutputEmphasis5dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrF4() < 0)
                return;
        }

        private void cbOutputEmphasis6dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrF5() < 0)
                return;
        }

        private void cbOutputEmphasis7dbCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrF6() < 0)
                return;
        }

        private void cbOutputEmphasisR0Ch3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddrF7() < 0)
                return;
        }
        
    }
}
