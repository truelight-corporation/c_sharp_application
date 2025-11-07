using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Gn1090Gn1190Config
{
    public partial class UcGn1190Config : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private bool reading = false;
        //private byte icI2cAddr = 84;

        public UcGn1190Config()
        {
            int i;

            InitializeComponent();

            reading = true;
            for (i = 0; i < 4; i++) {
                cbTx1TestOscEnable.Items.Add(i);
                cbTx1HfcRsel.Items.Add(i);
                cbTx2TestOscEnable.Items.Add(i);
                cbTx2HfcRsel.Items.Add(i);
                cbTx3TestOscEnable.Items.Add(i);
                cbTx3HfcRsel.Items.Add(i);
                cbTx4TestOscEnable.Items.Add(i);
                cbTx4HfcRsel.Items.Add(i);
                cbAllTestOscEnable.Items.Add(i);
                cbAllHfcRsel.Items.Add(i);
            }
            cbTx1TestOscEnable.SelectedIndex = cbTx2TestOscEnable.SelectedIndex =
                cbTx3TestOscEnable.SelectedIndex = cbTx4TestOscEnable.SelectedIndex =
                cbAllTestOscEnable.SelectedIndex = 0;
            cbTx1HfcRsel.SelectedIndex = cbTx2HfcRsel.SelectedIndex =
                cbTx3HfcRsel.SelectedIndex = cbTx4HfcRsel.SelectedIndex =
                cbAllHfcRsel.SelectedIndex = 0;

            for (i = 0; i < 16; i++) {
                cbTx1CpaLevel.Items.Add(i);
                cbTx1Eq.Items.Add(i);
                cbTx1PeakEn.Items.Add(i);
                cbTx1PeakLenCtrl.Items.Add(i);
                cbTx2CpaLevel.Items.Add(i);
                cbTx2Eq.Items.Add(i);
                cbTx2PeakEn.Items.Add(i);
                cbTx2PeakLenCtrl.Items.Add(i);
                cbTx3CpaLevel.Items.Add(i);
                cbTx3Eq.Items.Add(i);
                cbTx3PeakEn.Items.Add(i);
                cbTx3PeakLenCtrl.Items.Add(i);
                cbTx4CpaLevel.Items.Add(i);
                cbTx4Eq.Items.Add(i);
                cbTx4PeakEn.Items.Add(i);
                cbTx4PeakLenCtrl.Items.Add(i);
                cbAllCpaLevel.Items.Add(i);
                cbAllEq.Items.Add(i);
                cbAllPeakEn.Items.Add(i);
                cbAllPeakLenCtrl.Items.Add(i);
            }
            cbTx1CpaLevel.SelectedIndex = cbTx2CpaLevel.SelectedIndex = 
                cbTx3CpaLevel.SelectedIndex = cbTx4CpaLevel.SelectedIndex = 
                cbAllCpaLevel.SelectedIndex = 0;
            cbTx1Eq.SelectedIndex = cbTx2Eq.SelectedIndex = 
                cbTx3Eq.SelectedIndex = cbTx4Eq.SelectedIndex = 
                cbAllEq.SelectedIndex = 0;
            cbTx1PeakEn.SelectedIndex = cbTx2PeakEn.SelectedIndex = 
                cbTx3PeakEn.SelectedIndex = cbTx4PeakEn.SelectedIndex = 
                cbAllPeakEn.SelectedIndex = 0;
            cbTx1PeakLenCtrl.SelectedIndex = cbTx2PeakLenCtrl.SelectedIndex = 
                cbTx3PeakLenCtrl.SelectedIndex = cbTx4PeakLenCtrl.SelectedIndex = 
                cbAllPeakLenCtrl.SelectedIndex = 0;

            for (i = 0; i < 32; i++) {
                cbTx1VhfCompProp.Items.Add(i);
                cbTx2VhfCompProp.Items.Add(i);
                cbTx3VhfCompProp.Items.Add(i);
                cbTx4VhfCompProp.Items.Add(i);
                cbAllVhfCompProp.Items.Add(i);
            }
            cbTx1VhfCompProp.SelectedIndex = cbTx2VhfCompProp.SelectedIndex =
                cbTx3VhfCompProp.SelectedIndex = cbTx4VhfCompProp.SelectedIndex = 
                cbAllVhfCompProp.SelectedIndex = 0;

            for (i = 0; i < 64; i++) {
                cbTx1VhfCompConst.Items.Add(i);
                cbTx2VhfCompConst.Items.Add(i);
                cbTx3VhfCompConst.Items.Add(i);
                cbTx4VhfCompConst.Items.Add(i);
                cbAllVhfCompConst.Items.Add(i);
            }
            cbTx1VhfCompConst.SelectedIndex = cbTx2VhfCompConst.SelectedIndex = 
                cbTx3VhfCompConst.SelectedIndex = cbTx4VhfCompConst.SelectedIndex = 
                cbAllVhfCompConst.SelectedIndex = 0;

            for (i = 0; i < 128; i++) {
                cbTx1BiasCoefT1.Items.Add(i);
                cbTx1ModCoefT1.Items.Add(i);
                cbTx2BiasCoefT1.Items.Add(i);
                cbTx2ModCoefT1.Items.Add(i);
                cbTx3BiasCoefT1.Items.Add(i);
                cbTx3ModCoefT1.Items.Add(i);
                cbTx4BiasCoefT1.Items.Add(i);
                cbTx4ModCoefT1.Items.Add(i);
                cbAllBiasCoefT1.Items.Add(i);
                cbAllModCoefT1.Items.Add(i);
            }
            cbTx1BiasCoefT1.SelectedIndex = cbTx2BiasCoefT1.SelectedIndex =
                cbTx3BiasCoefT1.SelectedIndex = cbTx4BiasCoefT1.SelectedIndex =
                cbAllBiasCoefT1.SelectedIndex = 0;
            cbTx1ModCoefT1.SelectedIndex = cbTx2ModCoefT1.SelectedIndex = 
                cbTx3ModCoefT1.SelectedIndex = cbTx4ModCoefT1.SelectedIndex = 
                cbAllModCoefT1.SelectedIndex = 0;

            for (i = 0; i < 256; i++) {
                cbBiasThreshold.Items.Add(i);
                cbVfThreshold.Items.Add(i);
                cbTx1BiasCoefT2.Items.Add(i);
                cbTx1ModCoefT2.Items.Add(i);
                cbTx2BiasCoefT2.Items.Add(i);
                cbTx2ModCoefT2.Items.Add(i);
                cbTx3BiasCoefT2.Items.Add(i);
                cbTx3ModCoefT2.Items.Add(i);
                cbTx4BiasCoefT2.Items.Add(i);
                cbTx4ModCoefT2.Items.Add(i);
                cbAllBiasCoefT2.Items.Add(i);
                cbAllModCoefT2.Items.Add(i);
            }
            cbBiasThreshold.SelectedIndex = 0;
            cbVfThreshold.SelectedIndex = 0;
            cbTx1BiasCoefT2.SelectedIndex = cbTx2BiasCoefT2.SelectedIndex = 
                cbTx3BiasCoefT2.SelectedIndex = cbTx4BiasCoefT2.SelectedIndex = 
                cbAllBiasCoefT2.SelectedIndex = 0;
            cbTx1ModCoefT2.SelectedIndex = cbTx2ModCoefT2.SelectedIndex = 
                cbTx3ModCoefT2.SelectedIndex = cbTx4ModCoefT2.SelectedIndex = 
                cbAllModCoefT2.SelectedIndex = 0;

            for (i = 0; i < 512; i++) {
                cbTx1BiasCoefT0.Items.Add(i);
                cbTx1ModCoefT0.Items.Add(i);
                cbTx2BiasCoefT0.Items.Add(i);
                cbTx2ModCoefT0.Items.Add(i);
                cbTx3BiasCoefT0.Items.Add(i);
                cbTx3ModCoefT0.Items.Add(i);
                cbTx4BiasCoefT0.Items.Add(i);
                cbTx4ModCoefT0.Items.Add(i);
                cbAllBiasCoefT0.Items.Add(i);
                cbAllModCoefT0.Items.Add(i);
            }
            cbTx1BiasCoefT0.SelectedIndex = cbTx2BiasCoefT0.SelectedIndex = 
                cbTx3BiasCoefT0.SelectedIndex = cbTx4BiasCoefT0.SelectedIndex = 
                cbAllBiasCoefT0.SelectedIndex = 0;
            cbTx1ModCoefT0.SelectedIndex = cbTx2ModCoefT0.SelectedIndex =
                cbTx3ModCoefT0.SelectedIndex = cbTx4ModCoefT0.SelectedIndex = 
                cbAllModCoefT0.SelectedIndex = 0;

            for (i = 0; i < 2048; i++) {
                cbTx1Bias.Items.Add(i);
                cbTx1Mod.Items.Add(i);
                cbTx2Bias.Items.Add(i);
                cbTx2Mod.Items.Add(i);
                cbTx3Bias.Items.Add(i);
                cbTx3Mod.Items.Add(i);
                cbTx4Bias.Items.Add(i);
                cbTx4Mod.Items.Add(i);
                cbAllBias.Items.Add(i);
                cbAllMod.Items.Add(i);
            }
            cbTx1Bias.SelectedIndex = cbTx2Bias.SelectedIndex = 
                cbTx3Bias.SelectedIndex = cbTx4Bias.SelectedIndex = 
                cbAllBias.SelectedIndex = 0;
            cbTx1Mod.SelectedIndex = cbTx2Mod.SelectedIndex = 
                cbTx3Mod.SelectedIndex = cbTx4Mod.SelectedIndex = 
                cbAllMod.SelectedIndex = 0;

            reading = false;
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
        /*public int SetIcI2cAddrApi(byte addr)  //add by Wood
        {
            icI2cAddr = addr;
            return 0;
        }*/
        private void _ParseAddr0(byte data)
        {
            if ((data & 0x01) == 0)
                cbGlobalTxFault.Checked = false;
            else
                cbGlobalTxFault.Checked = true;

            if ((data & 0x02) == 0)
                cbGlobalTxLos.Checked = false;
            else
                cbGlobalTxLos.Checked = true;

            if ((data & 0x04) == 0)
                cbGlobalBiasHighAlarm.Checked = false;
            else
                cbGlobalBiasHighAlarm.Checked = true;

            if ((data & 0x08) == 0)
                cbTxDisablePinStatus.Checked = false;
            else
                cbTxDisablePinStatus.Checked = true;

            if ((data & 0x10) == 0)
                cbGlobalVcselVfHighAlarm.Checked = false;
            else
                cbGlobalVcselVfHighAlarm.Checked = true;

            if ((data & 0x20) == 0)
                cbGlobalVcselVfLowAlarm.Checked = false;
            else
                cbGlobalVcselVfLowAlarm.Checked = true;

            if ((data & 0x40) == 0)
                cbVddHighAlarm.Checked = false;
            else
                cbVddHighAlarm.Checked = true;

            if ((data & 0x80) == 0)
                cbVddLowAlarm.Checked = false;
            else
                cbVddLowAlarm.Checked = true;
        }

        private void _ParseAddr1(byte data)
        {
            if ((data & 0x01) == 0)
                cbTx1Fault.Checked = false;
            else
                cbTx1Fault.Checked = true;

            if ((data & 0x02) == 0)
                cbTx2Fault.Checked = false;
            else
                cbTx2Fault.Checked = true;

            if ((data & 0x04) == 0)
                cbTx3Fault.Checked = false;
            else
                cbTx3Fault.Checked = true;

            if ((data & 0x08) == 0)
                cbTx4Fault.Checked = false;
            else
                cbTx4Fault.Checked = true;
        }

        private void _ParseAddr3(byte data)
        {
            if ((data & 0x01) == 0)
                cbTx1Los.Checked = false;
            else
                cbTx1Los.Checked = true;

            if ((data & 0x02) == 0)
                cbTx2Los.Checked = false;
            else
                cbTx2Los.Checked = true;

            if ((data & 0x04) == 0)
                cbTx3Los.Checked = false;
            else
                cbTx3Los.Checked = true;

            if ((data & 0x08) == 0)
                cbTx4Los.Checked = false;
            else
                cbTx4Los.Checked = true;
        }

        private void _ParseAddr6_7(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 1;
            iTmp |= ((data1 & 0x80) >> 7);

            tbTemperature.Text = iTmp.ToString();
        }

        private void _ParseAddr8_9(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 8;
            iTmp |= data1;

            tbTx1Mpd.Text = iTmp.ToString();
        }

        private void _ParseAddr10_11(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 8;
            iTmp |= data1;

            tbTx2Mpd.Text = iTmp.ToString();
        }

        private void _ParseAddr12_13(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 8;
            iTmp |= data1;

            tbTx3Mpd.Text = iTmp.ToString();
        }

        private void _ParseAddr14_15(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 8;
            iTmp |= data1;

            tbTx4Mpd.Text = iTmp.ToString();
        }

        private void _ParseAddr18(byte data)
        {
            if ((data & 0x01) == 0)
                cbTx1IBiasHigh.Checked = false;
            else
                cbTx1IBiasHigh.Checked = true;

            if ((data & 0x02) == 0)
                cbTx1VfHigh.Checked = false;
            else
                cbTx1VfHigh.Checked = true;

            if ((data & 0x04) == 0)
                cbTx1VfLow.Checked = false;
            else
                cbTx1VfLow.Checked = true;

            if ((data & 0x08) == 0)
                cbTx1Los.Checked = false;
            else
                cbTx1Los.Checked = true;
        }

        private void _ParseAddr21(byte data)
        {
            if ((data & 0x01) == 0)
                cbTx2IBiasHigh.Checked = false;
            else
                cbTx2IBiasHigh.Checked = true;

            if ((data & 0x02) == 0)
                cbTx2VfHigh.Checked = false;
            else
                cbTx2VfHigh.Checked = true;

            if ((data & 0x04) == 0)
                cbTx2VfLow.Checked = false;
            else
                cbTx2VfLow.Checked = true;

            if ((data & 0x08) == 0)
                cbTx2Los.Checked = false;
            else
                cbTx2Los.Checked = true;
        }

        private void _ParseAddr24(byte data)
        {
            if ((data & 0x01) == 0)
                cbTx3IBiasHigh.Checked = false;
            else
                cbTx3IBiasHigh.Checked = true;

            if ((data & 0x02) == 0)
                cbTx3VfHigh.Checked = false;
            else
                cbTx3VfHigh.Checked = true;

            if ((data & 0x04) == 0)
                cbTx3VfLow.Checked = false;
            else
                cbTx3VfLow.Checked = true;

            if ((data & 0x08) == 0)
                cbTx3Los.Checked = false;
            else
                cbTx3Los.Checked = true;
        }

        private void _ParseAddr27(byte data)
        {
            if ((data & 0x01) == 0)
                cbTx4IBiasHigh.Checked = false;
            else
                cbTx4IBiasHigh.Checked = true;

            if ((data & 0x02) == 0)
                cbTx4VfHigh.Checked = false;
            else
                cbTx4VfHigh.Checked = true;

            if ((data & 0x04) == 0)
                cbTx4VfLow.Checked = false;
            else
                cbTx4VfLow.Checked = true;

            if ((data & 0x08) == 0)
                cbTx4Los.Checked = false;
            else
                cbTx4Los.Checked = true;
        }

        private void _ParseAddr52(byte data)
        {
            if ((data & 0x01) == 0)
                cbGlobalTxFaultDis.Checked = false;
            else
                cbGlobalTxFaultDis.Checked = true;

            if ((data & 0x02) == 0)
                cbGlobalTxLosDis.Checked = false;
            else
                cbGlobalTxLosDis.Checked = true;

            if ((data & 0x04) == 0)
                cbGlobalBiasHighDis.Checked = false;
            else
                cbGlobalBiasHighDis.Checked = true;

            if ((data & 0x10) == 0)
                cbGlobalVcselVfHighDis.Checked = false;
            else
                cbGlobalVcselVfHighDis.Checked = true;

            if ((data & 0x20) == 0)
                cbGlobalVcselVfLowDis.Checked = false;
            else
                cbGlobalVcselVfLowDis.Checked = true;

            if ((data & 0x40) == 0)
                cbVddHighDis.Checked = false;
            else
                cbVddHighDis.Checked = true;

            if ((data & 0x80) == 0)
                cbVddLowDis.Checked = false;
            else
                cbVddLowDis.Checked = true;
        }

        private void _ParseAddr53(byte data)
        {
            int iTmp;

            iTmp = data;
            cbBiasThreshold.SelectedIndex = iTmp;
        }

        private void _ParseAddr54(byte data)
        {
            if ((data & 0x01) == 0)
                cbTxEnable.Checked = false;
            else
                cbTxEnable.Checked = true;

            if ((data & 0x02) == 0)
                cbTemperatureCompensationDisable.Checked = false;
            else
                cbTemperatureCompensationDisable.Checked = true;

            if ((data & 0x04) == 0)
                cbLosPinPolarity.Checked = false;
            else
                cbLosPinPolarity.Checked = true;

            if ((data & 0x08) == 0)
                cbFaultDetectPolarity.Checked = false;
            else
                cbFaultDetectPolarity.Checked = true;

            if ((data & 0x10) == 0)
                cbVcselTestModeEnable.Checked = false;
            else
                cbVcselTestModeEnable.Checked = true;

            if ((data & 0x40) == 0)
                cbCsReset.Checked = false;
            else
                cbCsReset.Checked = true;
        }

        private void _ParseAddr55(byte data)
        {
            if ((data & 0x01) == 0)
                cbI2cTestSpeedup.Checked = false;
            else
                cbI2cTestSpeedup.Checked = true;

            if ((data & 0x08) == 0)
                cbImonSrcMode.Checked = false;
            else
                cbImonSrcMode.Checked = true;

            if ((data & 0x10) == 0)
                cbTempcoModEn.Checked = false;
            else
                cbTempcoModEn.Checked = true;

            if ((data & 0x20) == 0)
                cbTempcoBiasEn.Checked = false;
            else
                cbTempcoBiasEn.Checked = true;

            if ((data & 0x40) == 0)
                cbI2cTimeoutEn.Checked = false;
            else
                cbI2cTimeoutEn.Checked = true;

            if ((data & 0x80) == 0)
                cbI2cAddrPadEn.Checked = false;
            else
                cbI2cAddrPadEn.Checked = true;
        }

        private void _ParseAddr56(byte data)
        {
            int iTmp;

            iTmp = data;
            cbVfThreshold.SelectedIndex = iTmp;
        }

        private void _ParseAddr58(byte data)
        {
            if ((data & 0x01) == 0)
                cbDdmiReady.Checked = false;
            else
                cbDdmiReady.Checked = true;
        }

        private void _ParseAddr59(byte data)
        {
            if ((data & 0x01) == 0)
                cbSoftResetPartial.Checked = false;
            else
                cbSoftResetPartial.Checked = true;

            if ((data & 0x02) == 0)
                cbSoftResetFull.Checked = false;
            else
                cbSoftResetFull.Checked = true;
        }

        private void _ParseAddr60_61(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 8;
            iTmp |= data1;

            tbPassword0.Text = String.Format("{0:x4}", iTmp);
        }

        private void _ParseAddr62_63(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 8;
            iTmp |= data1;

            tbPassword1.Text = String.Format("{0:x4}", iTmp);
        }

        private void _ParseAddr64(byte data)
        {
            if ((data & 0x01) == 0)
                cbTx1Disable.Checked = false;
            else
                cbTx1Disable.Checked = true;

            if ((data & 0x02) == 0)
                cbTx1RateSelect.Checked = false;
            else
                cbTx1RateSelect.Checked = true;

            if ((data & 0x04) == 0)
                cbTx1CpaRange.Checked = false;
            else
                cbTx1CpaRange.Checked = true;

            if ((data & 0x08) == 0)
                cbTx1PolarityInvert.Checked = false;
            else
                cbTx1PolarityInvert.Checked = true;

            if ((data & 0x10) == 0)
                cbTx1OutputSquelchSel.Checked = false;
            else
                cbTx1OutputSquelchSel.Checked = true;

            if ((data & 0x20) == 0)
                cbTx1BiasFaultDisable.Checked = false;
            else
                cbTx1BiasFaultDisable.Checked = true;

            if ((data & 0x40) == 0)
                cbTx1VfHighDisable.Checked = false;
            else
                cbTx1VfHighDisable.Checked = true;

            if ((data & 0x80) == 0)
                cbTx1VfLowDisable.Checked = false;
            else
                cbTx1VfLowDisable.Checked = true;
        }

        private void _ParseAddr65(byte data)
        {
            int iTmp;

            iTmp = data & 0x1F;
            if (iTmp != 0x1F)
                cbTx1CpaLevel.SelectedIndex = iTmp;

            if ((data & 0x20) == 0)
                cbTx1CpaDirection.Checked = false;
            else
                cbTx1CpaDirection.Checked = true;

            iTmp = (data & 0xC0) >> 6;
            cbTx1TestOscEnable.SelectedIndex = iTmp;
        }

        private void _ParseAddr66_67(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 3;
            iTmp |= data1 >> 5;

            cbTx1Bias.SelectedIndex = iTmp;
        }

        private void _ParseAddr68_69(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 3;
            iTmp |= data1 >> 5;

            cbTx1Mod.SelectedIndex = iTmp;
        }

        private void _ParseAddr70(byte data)
        {
            int iTmp;

            iTmp = data & 0x0F;
            cbTx1Eq.SelectedIndex = iTmp;
        }

        private void _ParseAddr71_72(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 1;
            iTmp |= (data1 & 0x80) >> 7;

            cbTx1BiasCoefT0.SelectedIndex = iTmp;

            iTmp = data1 & 0x7F;
            cbTx1BiasCoefT1.SelectedIndex = iTmp;
        }

        private void _ParseAddr73(byte data)
        {
            int iTmp;

            iTmp = data;
            cbTx1BiasCoefT2.SelectedIndex = iTmp;
        }

        private void _ParseAddr74_75(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 1;
            iTmp |= (data1 & 0x80) >> 7;

            cbTx1ModCoefT0.SelectedIndex = iTmp;

            iTmp = data1 & 0x7F;
            cbTx1ModCoefT1.SelectedIndex = iTmp;
        }

        private void _ParseAddr76(byte data)
        {
            int iTmp;

            iTmp = data;
            cbTx1ModCoefT2.SelectedIndex = iTmp;
        }

        private void _ParseAddr77(byte data)
        {
            int iTmp;

            if ((data & 0x01) == 0)
                cbTx1TempcoEnable.Checked = false;
            else
                cbTx1TempcoEnable.Checked = true;

            if ((data & 0x02) == 0)
                cbTx1HfcEnable.Checked = false;
            else
                cbTx1HfcEnable.Checked = true;

            if ((data & 0x04) == 0)
                cbTx1FepeakEnable.Checked = false;
            else
                cbTx1FepeakEnable.Checked = true;

            iTmp = (data & 0xF8) >> 3;
            cbTx1VhfCompProp.SelectedIndex = iTmp;
        }

        private void _ParseAddr78(byte data)
        {
            int iTmp;

            iTmp = data & 0x3F;
            cbTx1VhfCompConst.SelectedIndex = iTmp;

            iTmp = (data & 0xC0) >> 6;
            cbTx1HfcRsel.SelectedIndex = iTmp;
        }

        private void _ParseAddr79(byte data)
        {
            int iTmp;

            iTmp = data & 0x0F;
            cbTx1PeakEn.SelectedIndex = iTmp;

            iTmp = (data & 0xF0) >> 4;
            cbTx1PeakLenCtrl.SelectedIndex = iTmp;
        }

        private void _ParseAddr80(byte data)
        {
            if ((data & 0x01) == 0)
                cbTx2Disable.Checked = false;
            else
                cbTx2Disable.Checked = true;

            if ((data & 0x02) == 0)
                cbTx2RateSelect.Checked = false;
            else
                cbTx2RateSelect.Checked = true;

            if ((data & 0x04) == 0)
                cbTx2CpaRange.Checked = false;
            else
                cbTx2CpaRange.Checked = true;

            if ((data & 0x08) == 0)
                cbTx2PolarityInvert.Checked = false;
            else
                cbTx2PolarityInvert.Checked = true;

            if ((data & 0x10) == 0)
                cbTx2OutputSquelchSel.Checked = false;
            else
                cbTx2OutputSquelchSel.Checked = true;

            if ((data & 0x20) == 0)
                cbTx2BiasFaultDisable.Checked = false;
            else
                cbTx2BiasFaultDisable.Checked = true;

            if ((data & 0x40) == 0)
                cbTx2VfHighDisable.Checked = false;
            else
                cbTx2VfHighDisable.Checked = true;

            if ((data & 0x80) == 0)
                cbTx2VfLowDisable.Checked = false;
            else
                cbTx2VfLowDisable.Checked = true;
        }

        private void _ParseAddr81(byte data)
        {
            int iTmp;

            iTmp = data & 0x1F;
            if (iTmp != 0x1F)
                cbTx2CpaLevel.SelectedIndex = iTmp;

            if ((data & 0x20) == 0)
                cbTx2CpaDirection.Checked = false;
            else
                cbTx2CpaDirection.Checked = true;

            iTmp = (data & 0xC0) >> 6;
            cbTx2TestOscEnable.SelectedIndex = iTmp;
        }

        private void _ParseAddr82_83(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 3;
            iTmp |= data1 >> 5;

            cbTx2Bias.SelectedIndex = iTmp;
        }

        private void _ParseAddr84_85(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 3;
            iTmp |= data1 >> 5;

            cbTx2Mod.SelectedIndex = iTmp;
        }

        private void _ParseAddr86(byte data)
        {
            int iTmp;

            iTmp = data & 0x0F;
            cbTx2Eq.SelectedIndex = iTmp;
        }

        private void _ParseAddr87_88(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 1;
            iTmp |= (data1 & 0x80) >> 7;

            cbTx2BiasCoefT0.SelectedIndex = iTmp;

            iTmp = data1 & 0x7F;
            cbTx2BiasCoefT1.SelectedIndex = iTmp;
        }

        private void _ParseAddr89(byte data)
        {
            int iTmp;

            iTmp = data;
            cbTx2BiasCoefT2.SelectedIndex = iTmp;
        }

        private void _ParseAddr90_91(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 1;
            iTmp |= (data1 & 0x80) >> 7;

            cbTx2ModCoefT0.SelectedIndex = iTmp;

            iTmp = data1 & 0x7F;
            cbTx2ModCoefT1.SelectedIndex = iTmp;
        }

        private void _ParseAddr92(byte data)
        {
            int iTmp;

            iTmp = data;
            cbTx2ModCoefT2.SelectedIndex = iTmp;
        }

        private void _ParseAddr93(byte data)
        {
            int iTmp;

            if ((data & 0x01) == 0)
                cbTx2TempcoEnable.Checked = false;
            else
                cbTx2TempcoEnable.Checked = true;

            if ((data & 0x02) == 0)
                cbTx2HfcEnable.Checked = false;
            else
                cbTx2HfcEnable.Checked = true;

            if ((data & 0x04) == 0)
                cbTx2FepeakEnable.Checked = false;
            else
                cbTx2FepeakEnable.Checked = true;

            iTmp = (data & 0xF8) >> 3;
            cbTx2VhfCompProp.SelectedIndex = iTmp;
        }

        private void _ParseAddr94(byte data)
        {
            int iTmp;

            iTmp = data & 0x3F;
            cbTx2VhfCompConst.SelectedIndex = iTmp;

            iTmp = (data & 0xC0) >> 6;
            cbTx2HfcRsel.SelectedIndex = iTmp;
        }

        private void _ParseAddr95(byte data)
        {
            int iTmp;

            iTmp = data & 0x0F;
            cbTx2PeakEn.SelectedIndex = iTmp;

            iTmp = (data & 0xF0) >> 4;
            cbTx2PeakLenCtrl.SelectedIndex = iTmp;
        }

        private void _ParseAddr96(byte data)
        {
            if ((data & 0x01) == 0)
                cbTx3Disable.Checked = false;
            else
                cbTx3Disable.Checked = true;

            if ((data & 0x02) == 0)
                cbTx3RateSelect.Checked = false;
            else
                cbTx3RateSelect.Checked = true;

            if ((data & 0x04) == 0)
                cbTx3CpaRange.Checked = false;
            else
                cbTx3CpaRange.Checked = true;

            if ((data & 0x08) == 0)
                cbTx3PolarityInvert.Checked = false;
            else
                cbTx3PolarityInvert.Checked = true;

            if ((data & 0x10) == 0)
                cbTx3OutputSquelchSel.Checked = false;
            else
                cbTx3OutputSquelchSel.Checked = true;

            if ((data & 0x20) == 0)
                cbTx3BiasFaultDisable.Checked = false;
            else
                cbTx3BiasFaultDisable.Checked = true;

            if ((data & 0x40) == 0)
                cbTx3VfHighDisable.Checked = false;
            else
                cbTx3VfHighDisable.Checked = true;

            if ((data & 0x80) == 0)
                cbTx3VfLowDisable.Checked = false;
            else
                cbTx3VfLowDisable.Checked = true;
        }

        private void _ParseAddr97(byte data)
        {
            int iTmp;

            iTmp = data & 0x1F;
            if (iTmp != 0x1F)
                cbTx3CpaLevel.SelectedIndex = iTmp;

            if ((data & 0x20) == 0)
                cbTx3CpaDirection.Checked = false;
            else
                cbTx3CpaDirection.Checked = true;

            iTmp = (data & 0xC0) >> 6;
            cbTx3TestOscEnable.SelectedIndex = iTmp;
        }

        private void _ParseAddr98_99(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 3;
            iTmp |= data1 >> 5;

            cbTx3Bias.SelectedIndex = iTmp;
        }

        private void _ParseAddr100_101(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 3;
            iTmp |= data1 >> 5;

            cbTx3Mod.SelectedIndex = iTmp;
        }

        private void _ParseAddr102(byte data)
        {
            int iTmp;

            iTmp = data & 0x0F;
            cbTx3Eq.SelectedIndex = iTmp;
        }

        private void _ParseAddr103_104(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 1;
            iTmp |= (data1 & 0x80) >> 7;

            cbTx3BiasCoefT0.SelectedIndex = iTmp;

            iTmp = data1 & 0x7F;
            cbTx3BiasCoefT1.SelectedIndex = iTmp;
        }

        private void _ParseAddr105(byte data)
        {
            int iTmp;

            iTmp = data;
            cbTx3BiasCoefT2.SelectedIndex = iTmp;
        }

        private void _ParseAddr106_107(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 1;
            iTmp |= (data1 & 0x80) >> 7;

            cbTx3ModCoefT0.SelectedIndex = iTmp;

            iTmp = data1 & 0x7F;
            cbTx3ModCoefT1.SelectedIndex = iTmp;
        }

        private void _ParseAddr108(byte data)
        {
            int iTmp;

            iTmp = data;
            cbTx3ModCoefT2.SelectedIndex = iTmp;
        }

        private void _ParseAddr109(byte data)
        {
            int iTmp;

            if ((data & 0x01) == 0)
                cbTx3TempcoEnable.Checked = false;
            else
                cbTx3TempcoEnable.Checked = true;

            if ((data & 0x02) == 0)
                cbTx3HfcEnable.Checked = false;
            else
                cbTx3HfcEnable.Checked = true;

            if ((data & 0x04) == 0)
                cbTx3FepeakEnable.Checked = false;
            else
                cbTx3FepeakEnable.Checked = true;

            iTmp = (data & 0xF8) >> 3;
            cbTx3VhfCompProp.SelectedIndex = iTmp;
        }

        private void _ParseAddr110(byte data)
        {
            int iTmp;

            iTmp = data & 0x3F;
            cbTx3VhfCompConst.SelectedIndex = iTmp;

            iTmp = (data & 0xC0) >> 6;
            cbTx3HfcRsel.SelectedIndex = iTmp;
        }

        private void _ParseAddr111(byte data)
        {
            int iTmp;

            iTmp = data & 0x0F;
            cbTx3PeakEn.SelectedIndex = iTmp;

            iTmp = (data & 0xF0) >> 4;
            cbTx3PeakLenCtrl.SelectedIndex = iTmp;
        }

        private void _ParseAddr112(byte data)
        {
            if ((data & 0x01) == 0)
                cbTx4Disable.Checked = false;
            else
                cbTx4Disable.Checked = true;

            if ((data & 0x02) == 0)
                cbTx4RateSelect.Checked = false;
            else
                cbTx4RateSelect.Checked = true;

            if ((data & 0x04) == 0)
                cbTx4CpaRange.Checked = false;
            else
                cbTx4CpaRange.Checked = true;

            if ((data & 0x08) == 0)
                cbTx4PolarityInvert.Checked = false;
            else
                cbTx4PolarityInvert.Checked = true;

            if ((data & 0x10) == 0)
                cbTx4OutputSquelchSel.Checked = false;
            else
                cbTx4OutputSquelchSel.Checked = true;

            if ((data & 0x20) == 0)
                cbTx4BiasFaultDisable.Checked = false;
            else
                cbTx4BiasFaultDisable.Checked = true;

            if ((data & 0x40) == 0)
                cbTx4VfHighDisable.Checked = false;
            else
                cbTx4VfHighDisable.Checked = true;

            if ((data & 0x80) == 0)
                cbTx4VfLowDisable.Checked = false;
            else
                cbTx4VfLowDisable.Checked = true;
        }

        private void _ParseAddr113(byte data)
        {
            int iTmp;

            iTmp = data & 0x1F;
            if (iTmp != 0x1F)
                cbTx4CpaLevel.SelectedIndex = iTmp;

            if ((data & 0x20) == 0)
                cbTx4CpaDirection.Checked = false;
            else
                cbTx4CpaDirection.Checked = true;

            iTmp = (data & 0xC0) >> 6;
            cbTx4TestOscEnable.SelectedIndex = iTmp;
        }

        private void _ParseAddr114_115(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 3;
            iTmp |= data1 >> 5;

            cbTx4Bias.SelectedIndex = iTmp;
        }

        private void _ParseAddr116_117(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 3;
            iTmp |= data1 >> 5;

            cbTx4Mod.SelectedIndex = iTmp;
        }

        private void _ParseAddr118(byte data)
        {
            int iTmp;

            iTmp = data & 0x0F;
            cbTx4Eq.SelectedIndex = iTmp;
        }

        private void _ParseAddr119_120(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 1;
            iTmp |= (data1 & 0x80) >> 7;

            cbTx4BiasCoefT0.SelectedIndex = iTmp;

            iTmp = data1 & 0x7F;
            cbTx4BiasCoefT1.SelectedIndex = iTmp;
        }

        private void _ParseAddr121(byte data)
        {
            int iTmp;

            iTmp = data;
            cbTx4BiasCoefT2.SelectedIndex = iTmp;
        }

        private void _ParseAddr122_123(byte data0, byte data1)
        {
            int iTmp;

            iTmp = data0;
            iTmp <<= 1;
            iTmp |= (data1 & 0x80) >> 7;

            cbTx4ModCoefT0.SelectedIndex = iTmp;

            iTmp = data1 & 0x7F;
            cbTx4ModCoefT1.SelectedIndex = iTmp;
        }

        private void _ParseAddr124(byte data)
        {
            int iTmp;

            iTmp = data;
            cbTx4ModCoefT2.SelectedIndex = iTmp;
        }

        private void _ParseAddr125(byte data)
        {
            int iTmp;

            if ((data & 0x01) == 0)
                cbTx4TempcoEnable.Checked = false;
            else
                cbTx4TempcoEnable.Checked = true;

            if ((data & 0x02) == 0)
                cbTx4HfcEnable.Checked = false;
            else
                cbTx4HfcEnable.Checked = true;

            if ((data & 0x04) == 0)
                cbTx4FepeakEnable.Checked = false;
            else
                cbTx4FepeakEnable.Checked = true;

            iTmp = (data & 0xF8) >> 3;
            cbTx4VhfCompProp.SelectedIndex = iTmp;
        }

        private void _ParseAddr126(byte data)
        {
            int iTmp;

            iTmp = data & 0x3F;
            cbTx4VhfCompConst.SelectedIndex = iTmp;

            iTmp = (data & 0xC0) >> 6;
            cbTx4HfcRsel.SelectedIndex = iTmp;
        }

        private void _ParseAddr127(byte data)
        {
            int iTmp;

            iTmp = data & 0x0F;
            cbTx4PeakEn.SelectedIndex = iTmp;

            iTmp = (data & 0xF0) >> 4;
            cbTx4PeakLenCtrl.SelectedIndex = iTmp;
        }

        private void _bRead_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[23];
            int rv;

            reading = true;
            bRead.Enabled = false;

            if (i2cReadCB == null)
                goto exit;

            rv = i2cReadCB(84, 0, 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr0(data[0]);
            _ParseAddr1(data[1]);

            rv = i2cReadCB(84, 3, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr3(data[0]);

            rv = i2cReadCB(84, 5, 23, data);
            if (rv != 23)
                goto exit;

            tbSupplyVoltage.Text = data[0].ToString();

            _ParseAddr6_7(data[1], data[2]);

            _ParseAddr8_9(data[3], data[4]);
            _ParseAddr10_11(data[5], data[6]);
            _ParseAddr12_13(data[7], data[8]);
            _ParseAddr14_15(data[9], data[10]);

            tbTx1Bias.Text = data[11].ToString();
            tbTx1Vf.Text = data[12].ToString();
            _ParseAddr18(data[13]);

            tbTx2Bias.Text = data[14].ToString();
            tbTx2Vf.Text = data[15].ToString();
            _ParseAddr21(data[16]);

            tbTx3Bias.Text = data[17].ToString();
            tbTx3Vf.Text = data[18].ToString();
            _ParseAddr24(data[19]);

            tbTx4Bias.Text = data[20].ToString();
            tbTx4Vf.Text = data[21].ToString();
            _ParseAddr27(data[22]);

            rv = i2cReadCB(84, 52, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr52(data[0]);
            _ParseAddr53(data[1]);
            _ParseAddr54(data[2]);
            _ParseAddr55(data[3]);
            _ParseAddr56(data[4]);

            rv = i2cReadCB(84, 58, 6, data);
            if (rv != 6)
                goto exit;

            _ParseAddr58(data[0]);
            _ParseAddr59(data[1]);
            _ParseAddr60_61(data[2], data[3]);
            _ParseAddr62_63(data[4], data[5]);
            

            rv = i2cReadCB(84, 64, 16, data);
            if (rv != 16)
                goto exit;

            _ParseAddr64(data[0]);
            _ParseAddr65(data[1]);
            _ParseAddr66_67(data[2], data[3]);
            _ParseAddr68_69(data[4], data[5]);
            _ParseAddr70(data[6]);
            _ParseAddr71_72(data[7], data[8]);
            _ParseAddr73(data[9]);
            _ParseAddr74_75(data[10], data[11]);
            _ParseAddr76(data[12]);
            _ParseAddr77(data[13]);
            _ParseAddr78(data[14]);
            _ParseAddr79(data[15]);

            rv = i2cReadCB(84, 80, 16, data);
            if (rv != 16)
                goto exit;

            _ParseAddr80(data[0]);
            _ParseAddr81(data[1]);
            _ParseAddr82_83(data[2], data[3]);
            _ParseAddr84_85(data[4], data[5]);
            _ParseAddr86(data[6]);
            _ParseAddr87_88(data[7], data[8]);
            _ParseAddr89(data[9]);
            _ParseAddr90_91(data[10], data[11]);
            _ParseAddr92(data[12]);
            _ParseAddr93(data[13]);
            _ParseAddr94(data[14]);
            _ParseAddr95(data[15]);

            rv = i2cReadCB(84, 96, 16, data);
            if (rv != 16)
                goto exit;

            _ParseAddr96(data[0]);
            _ParseAddr97(data[1]);
            _ParseAddr98_99(data[2], data[3]);
            _ParseAddr100_101(data[4], data[5]);
            _ParseAddr102(data[6]);
            _ParseAddr103_104(data[7], data[8]);
            _ParseAddr105(data[9]);
            _ParseAddr106_107(data[10], data[11]);
            _ParseAddr108(data[12]);
            _ParseAddr109(data[13]);
            _ParseAddr110(data[14]);
            _ParseAddr111(data[15]);

            rv = i2cReadCB(84, 112, 16, data);
            if (rv != 16)
                goto exit;

            _ParseAddr112(data[0]);
            _ParseAddr113(data[1]);
            _ParseAddr114_115(data[2], data[3]);
            _ParseAddr116_117(data[4], data[5]);
            _ParseAddr118(data[6]);
            _ParseAddr119_120(data[7], data[8]);
            _ParseAddr121(data[9]);
            _ParseAddr122_123(data[10], data[11]);
            _ParseAddr124(data[12]);
            _ParseAddr125(data[13]);
            _ParseAddr126(data[14]);
            _ParseAddr127(data[15]);

        exit:
            reading = false;
            bRead.Enabled = true;
        }

        private int _WriteAddr52()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbGlobalTxFaultDis.Checked == true)
                data[0] |= 0x01;

            if (cbGlobalTxLosDis.Checked == true)
                data[0] |= 0x02;

            if (cbGlobalBiasHighDis.Checked == true)
                data[0] |= 0x04;

            if (cbGlobalVcselVfHighDis.Checked == true)
                data[0] |= 0x10;

            if (cbGlobalVcselVfLowDis.Checked == true)
                data[0] |= 0x20;

            if (cbVddHighDis.Checked == true)
                data[0] |= 0x40;

            if (cbVddLowDis.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(84, 52, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbGlobalTxFaultDis_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr52() < 0)
                return;
        }

        private void cbGlobalTxLosDis_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr52() < 0)
                return;
        }

        private void cbGlobalBiasHighDis_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr52() < 0)
                return;
        }

        private void cbGlobalVcselVfHighDis_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr52() < 0)
                return;
        }

        private void cbGlobalVcselVfLowDis_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr52() < 0)
                return;
        }

        private void cbVddHighDis_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr52() < 0)
                return;
        }

        private void cbVddLowDis_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr52() < 0)
                return;
        }

        private int _WriteAddr53()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbBiasThreshold.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 53, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbBiasThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr53() < 0)
                return;
        }

        private int _WriteAddr54()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbTxEnable.Checked == true)
                data[0] |= 0x01;

            if (cbTemperatureCompensationDisable.Checked == true)
                data[0] |= 0x02;

            if (cbLosPinPolarity.Checked == true)
                data[0] |= 0x04;

            if (cbFaultDetectPolarity.Checked == true)
                data[0] |= 0x08;

            if (cbVcselTestModeEnable.Checked == true)
                data[0] |= 0x10;

            if (cbCsReset.Checked == true)
                data[0] |= 0x40;

            rv = i2cWriteCB(84, 54, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr54() < 0)
                return;
        }

        private void cbTemperatureCompensationDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr54() < 0)
                return;
        }

        private void cbLosPinPolarity_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr54() < 0)
                return;
        }

        private void cbFaultDetectPolarity_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr54() < 0)
                return;
        }

        private void cbVcselTestModeEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr54() < 0)
                return;
        }

        private void cbCsReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr54() < 0)
                return;
        }

        private int _WriteAddr55()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbI2cTestSpeedup.Checked == true)
                data[0] |= 0x01;

            if (cbImonSrcMode.Checked == true)
                data[0] |= 0x08;

            if (cbTempcoModEn.Checked == true)
                data[0] |= 0x10;

            if (cbTempcoBiasEn.Checked == true)
                data[0] |= 0x20;

            if (cbI2cTimeoutEn.Checked == true)
                data[0] |= 0x40;

            if (cbI2cAddrPadEn.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(84, 55, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbI2cTestSpeedup_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr55() < 0)
                return;
        }

        private void cbImonSrcMode_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr55() < 0)
                return;
        }

        private void cbTempcoModEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr55() < 0)
                return;
        }

        private void cbTempcoBiasEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr55() < 0)
                return;
        }

        private void cbI2cTimeoutEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr55() < 0)
                return;
        }

        private void cbI2cAddrPadEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr55() < 0)
                return;
        }

        private int _WriteAddr56()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbVfThreshold.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 56, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbVfThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr56() < 0)
                return;
        }

        private int _WriteAddr59()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbSoftResetPartial.Checked == true)
                data[0] |= 0x01;

            if (cbSoftResetFull.Checked == true)
                data[0] |= 0x02;

            rv = i2cWriteCB(84, 59, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbSoftResetPartial_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr59() < 0)
                return;
        }

        private void cbSoftResetFull_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr59() < 0)
                return;
        }

        private int _WriteAddr60_61()
        {
            byte[] data = new byte[2];
            int i, rv;

            data[0] = 0;

            if (tbPassword0.Text.Length != 4) {
                MessageBox.Show("Password0 length wrong: " + tbPassword0.Text.Length);
                tbPassword0.Text = "0000";
                return -1;
            }

            for (i = 0; i < 4; i += 2)
                data[i / 2] = Convert.ToByte(tbPassword0.Text.Substring(i, 2), 16);

            rv = i2cWriteCB(84, 60, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void tbPassword0_TextChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr60_61() < 0)
                return;
        }

        private int _WriteAddr62_63()
        {
            byte[] data = new byte[2];
            int i, rv;

            data[0] = 0;

            if (tbPassword1.Text.Length != 4) {
                MessageBox.Show("Password1 length wrong: " + tbPassword1.Text.Length);
                tbPassword1.Text = "0000";
                return -1;
            }

            for (i = 0; i < 4; i += 2)
                data[i / 2] = Convert.ToByte(tbPassword1.Text.Substring(i, 2), 16);

            rv = i2cWriteCB(84, 62, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void tbPassword1_TextChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr62_63() < 0)
                return;
        }

        private int _WriteAddr64()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbTx1Disable.Checked == true)
                data[0] |= 0x01;

            if (cbTx1RateSelect.Checked == true)
                data[0] |= 0x02;

            if (cbTx1CpaRange.Checked == true)
                data[0] |= 0x04;

            if (cbTx1PolarityInvert.Checked == true)
                data[0] |= 0x08;

            if (cbTx1OutputSquelchSel.Checked == true)
                data[0] |= 0x10;

            if (cbTx1BiasFaultDisable.Checked == true)
                data[0] |= 0x20;

            if (cbTx1VfHighDisable.Checked == true)
                data[0] |= 0x40;

            if (cbTx1VfLowDisable.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(84, 64, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx1Disable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void cbTx1RateSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void cbTx1CpaRange_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void cbTx1PolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void cbTx1OutputSquelchSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void cbTx1BiasFaultDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void cbTx1VfHighDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void cbTx1VfLowDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private int _WriteAddr65()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx1CpaLevel.SelectedIndex);
            data[0] |= bTmp;

            if (cbTx1CpaDirection.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx1TestOscEnable.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 65, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx1CpaLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65() < 0)
                return;
        }

        private void cbTx1CpaDirection_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65() < 0)
                return;
        }

        private void cbTx1TestOscEnable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65() < 0)
                return;
        }

        private int _WriteAddr66_67()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;

            i16Tmp = Convert.ToInt16(cbTx1Bias.SelectedIndex);
            i16Tmp <<= 5;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            rv = i2cWriteCB(84, 66, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx1Bias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr66_67() < 0)
                return;
        }

        private int _WriteAddr68_69()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;

            i16Tmp = Convert.ToInt16(cbTx1Mod.SelectedIndex);
            i16Tmp <<= 5;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            rv = i2cWriteCB(84, 68, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx1Mod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr68_69() < 0)
                return;
        }

        private int _WriteAddr70()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx1Eq.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 70, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx1Eq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr70() < 0)
                return;
        }

        private int _WriteAddr71_72()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;
            byte bTmp;

            i16Tmp = Convert.ToInt16(cbTx1BiasCoefT0.SelectedIndex);
            i16Tmp <<= 7;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx1BiasCoefT1.SelectedIndex);
            data[1] |= bTmp;

            rv = i2cWriteCB(84, 71, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx1BiasCoefT0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr71_72() < 0)
                return;
        }

        private void cbTx1BiasCoefT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr71_72() < 0)
                return;
        }

        private int _WriteAddr73()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx1BiasCoefT2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 73, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx1BiasCoefT2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr73() < 0)
                return;
        }

        private int _WriteAddr74_75()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;
            byte bTmp;

            i16Tmp = Convert.ToInt16(cbTx1ModCoefT0.SelectedIndex);
            i16Tmp <<= 7;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx1ModCoefT1.SelectedIndex);
            data[1] |= bTmp;

            rv = i2cWriteCB(84, 74, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx1ModCoefT0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr74_75() < 0)
                return;
        }

        private void cbTx1ModCoefT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr74_75() < 0)
                return;
        }

        private int _WriteAddr76()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx1ModCoefT2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 76, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx1ModCoefT2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr76() < 0)
                return;
        }

        private int _WriteAddr77()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;
            if (cbTx1TempcoEnable.Checked == true)
                data[0] |= 0x01;

            if (cbTx1HfcEnable.Checked == true)
                data[0] |= 0x02;

            if (cbTx1FepeakEnable.Checked == true)
                data[0] |= 0x04;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx1VhfCompProp.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 77, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx1TempcoEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr77() < 0)
                return;
        }

        private void cbTx1HfcEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr77() < 0)
                return;
        }

        private void cbTx1FepeakEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr77() < 0)
                return;
        }

        private void cbTx1VhfCompProp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr77() < 0)
                return;
        }

        private int _WriteAddr78()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx1VhfCompConst.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx1HfcRsel.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 78, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx1VhfCompConst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr78() < 0)
                return;
        }

        private void cbTx1HfcRsel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr78() < 0)
                return;
        }

        private int _WriteAddr79()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx1PeakEn.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx1PeakLenCtrl.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 79, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx1PeakEn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr79() < 0)
                return;
        }

        private void cbTx1PeakLenCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr79() < 0)
                return;
        }

        private int _WriteAddr80()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbTx2Disable.Checked == true)
                data[0] |= 0x01;

            if (cbTx2RateSelect.Checked == true)
                data[0] |= 0x02;

            if (cbTx2CpaRange.Checked == true)
                data[0] |= 0x04;

            if (cbTx2PolarityInvert.Checked == true)
                data[0] |= 0x08;

            if (cbTx2OutputSquelchSel.Checked == true)
                data[0] |= 0x10;

            if (cbTx2BiasFaultDisable.Checked == true)
                data[0] |= 0x20;

            if (cbTx2VfHighDisable.Checked == true)
                data[0] |= 0x40;

            if (cbTx2VfLowDisable.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(84, 80, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx2Disable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr80() < 0)
                return;
        }

        private void cbTx2RateSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr80() < 0)
                return;
        }

        private void cbTx2CpaRange_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr80() < 0)
                return;
        }

        private void cbTx2PolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr80() < 0)
                return;
        }

        private void cbTx2OutputSquelchSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr80() < 0)
                return;
        }

        private void cbTx2BiasFaultDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr80() < 0)
                return;
        }

        private void cbTx2VfHighDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr80() < 0)
                return;
        }

        private void cbTx2VfLowDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr80() < 0)
                return;
        }

        private int _WriteAddr81()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx2CpaLevel.SelectedIndex);
            data[0] |= bTmp;

            if (cbTx2CpaDirection.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx2TestOscEnable.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 81, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx2CpaLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr81() < 0)
                return;
        }

        private void cbTx2CpaDirection_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr81() < 0)
                return;
        }

        private void cbTx2TestOscEnable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr81() < 0)
                return;
        }

        private int _WriteAddr82_83()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;

            i16Tmp = Convert.ToInt16(cbTx2Bias.SelectedIndex);
            i16Tmp <<= 5;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            rv = i2cWriteCB(84, 82, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx2Bias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr82_83() < 0)
                return;
        }

        private int _WriteAddr84_85()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;

            i16Tmp = Convert.ToInt16(cbTx2Mod.SelectedIndex);
            i16Tmp <<= 5;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            rv = i2cWriteCB(84, 84, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx2Mod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr84_85() < 0)
                return;
        }

        private int _WriteAddr86()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx2Eq.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 86, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx2Eq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr86() < 0)
                return;
        }

        private int _WriteAddr87_88()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;
            byte bTmp;

            i16Tmp = Convert.ToInt16(cbTx2BiasCoefT0.SelectedIndex);
            i16Tmp <<= 7;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx2BiasCoefT1.SelectedIndex);
            data[1] |= bTmp;

            rv = i2cWriteCB(84, 87, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx2BiasCoefT0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr87_88() < 0)
                return;
        }

        private void cbTx2BiasCoefT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr87_88() < 0)
                return;
        }

        private int _WriteAddr89()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx2BiasCoefT2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 89, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx2BiasCoefT2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr89() < 0)
                return;
        }

        private int _WriteAddr90_91()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;
            byte bTmp;

            i16Tmp = Convert.ToInt16(cbTx2ModCoefT0.SelectedIndex);
            i16Tmp <<= 7;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx2ModCoefT1.SelectedIndex);
            data[1] |= bTmp;

            rv = i2cWriteCB(84, 90, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx2ModCoefT0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr90_91() < 0)
                return;
        }

        private void cbTx2ModCoefT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr90_91() < 0)
                return;
        }

        private int _WriteAddr92()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx2ModCoefT2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 92, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx2ModCoefT2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr92() < 0)
                return;
        }

        private int _WriteAddr93()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;
            if (cbTx2TempcoEnable.Checked == true)
                data[0] |= 0x01;

            if (cbTx2HfcEnable.Checked == true)
                data[0] |= 0x02;

            if (cbTx2FepeakEnable.Checked == true)
                data[0] |= 0x04;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx2VhfCompProp.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 93, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx2TempcoEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr93() < 0)
                return;
        }

        private void cbTx2HfcEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr93() < 0)
                return;
        }

        private void cbTx2FepeakEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr93() < 0)
                return;
        }

        private void cbTx2VhfCompProp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr93() < 0)
                return;
        }

        private int _WriteAddr94()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx2VhfCompConst.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx2HfcRsel.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 94, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx2VhfCompConst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr94() < 0)
                return;
        }

        private void cbTx2HfcRsel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr94() < 0)
                return;
        }

        private int _WriteAddr95()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx2PeakEn.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx2PeakLenCtrl.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 95, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx2PeakEn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr95() < 0)
                return;
        }

        private void cbTx2PeakLenCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr95() < 0)
                return;
        }

        private int _WriteAddr96()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbTx3Disable.Checked == true)
                data[0] |= 0x01;

            if (cbTx3RateSelect.Checked == true)
                data[0] |= 0x02;

            if (cbTx3CpaRange.Checked == true)
                data[0] |= 0x04;

            if (cbTx3PolarityInvert.Checked == true)
                data[0] |= 0x08;

            if (cbTx3OutputSquelchSel.Checked == true)
                data[0] |= 0x10;

            if (cbTx3BiasFaultDisable.Checked == true)
                data[0] |= 0x20;

            if (cbTx3VfHighDisable.Checked == true)
                data[0] |= 0x40;

            if (cbTx3VfLowDisable.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(84, 96, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx3Disable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void cbTx3RateSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void cbTx3CpaRange_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void cbTx3PolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void cbTx3OutputSquelchSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void cbTx3BiasFaultDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void cbTx3VfHighDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void cbTx3VfLowDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private int _WriteAddr97()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx3CpaLevel.SelectedIndex);
            data[0] |= bTmp;

            if (cbTx3CpaDirection.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx3TestOscEnable.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 97, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx3CpaLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr97() < 0)
                return;
        }

        private void cbTx3CpaDirection_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr97() < 0)
                return;
        }

        private void cbTx3TestOscEnable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr97() < 0)
                return;
        }

        private int _WriteAddr98_99()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;

            i16Tmp = Convert.ToInt16(cbTx3Bias.SelectedIndex);
            i16Tmp <<= 5;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            rv = i2cWriteCB(84, 98, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx3Bias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr98_99() < 0)
                return;
        }

        private int _WriteAddr100_101()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;

            i16Tmp = Convert.ToInt16(cbTx3Mod.SelectedIndex);
            i16Tmp <<= 5;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            rv = i2cWriteCB(84, 100, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx3Mod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr100_101() < 0)
                return;
        }

        private int _WriteAddr102()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx3Eq.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 102, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx3Eq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr102() < 0)
                return;
        }

        private int _WriteAddr103_104()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;
            byte bTmp;

            i16Tmp = Convert.ToInt16(cbTx3BiasCoefT0.SelectedIndex);
            i16Tmp <<= 7;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx3BiasCoefT1.SelectedIndex);
            data[1] |= bTmp;

            rv = i2cWriteCB(84, 103, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx3BiasCoefT0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr103_104() < 0)
                return;
        }

        private void cbTx3BiasCoefT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr103_104() < 0)
                return;
        }

        private int _WriteAddr105()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx3BiasCoefT2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 105, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx3BiasCoefT2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr105() < 0)
                return;
        }

        private int _WriteAddr106_107()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;
            byte bTmp;

            i16Tmp = Convert.ToInt16(cbTx3ModCoefT0.SelectedIndex);
            i16Tmp <<= 7;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx3ModCoefT1.SelectedIndex);
            data[1] |= bTmp;

            rv = i2cWriteCB(84, 106, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx3ModCoefT0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr106_107() < 0)
                return;
        }

        private void cbTx3ModCoefT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr106_107() < 0)
                return;
        }

        private int _WriteAddr108()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx3ModCoefT2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 108, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx3ModCoefT2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr108() < 0)
                return;
        }

        private int _WriteAddr109()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;
            if (cbTx3TempcoEnable.Checked == true)
                data[0] |= 0x01;

            if (cbTx3HfcEnable.Checked == true)
                data[0] |= 0x02;

            if (cbTx3FepeakEnable.Checked == true)
                data[0] |= 0x04;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx3VhfCompProp.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 109, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx3TempcoEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr109() < 0)
                return;
        }

        private void cbTx3HfcEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr109() < 0)
                return;
        }

        private void cbTx3FepeakEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr109() < 0)
                return;
        }

        private void cbTx3VhfCompProp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr109() < 0)
                return;
        }

        private int _WriteAddr110()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx3VhfCompConst.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx3HfcRsel.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 110, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx3VhfCompConst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr110() < 0)
                return;
        }

        private void cbTx3HfcRsel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr110() < 0)
                return;
        }

        private int _WriteAddr111()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx3PeakEn.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx3PeakLenCtrl.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 111, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx3PeakEn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr111() < 0)
                return;
        }

        private void cbTx3PeakLenCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr111() < 0)
                return;
        }

        private int _WriteAddr112()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbTx4Disable.Checked == true)
                data[0] |= 0x01;

            if (cbTx4RateSelect.Checked == true)
                data[0] |= 0x02;

            if (cbTx4CpaRange.Checked == true)
                data[0] |= 0x04;

            if (cbTx4PolarityInvert.Checked == true)
                data[0] |= 0x08;

            if (cbTx4OutputSquelchSel.Checked == true)
                data[0] |= 0x10;

            if (cbTx4BiasFaultDisable.Checked == true)
                data[0] |= 0x20;

            if (cbTx4VfHighDisable.Checked == true)
                data[0] |= 0x40;

            if (cbTx4VfLowDisable.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(84, 112, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx4Disable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr112() < 0)
                return;
        }

        private void cbTx4RateSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr112() < 0)
                return;
        }

        private void cbTx4CpaRange_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr112() < 0)
                return;
        }

        private void cbTx4PolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr112() < 0)
                return;
        }

        private void cbTx4OutputSquelchSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr112() < 0)
                return;
        }

        private void cbTx4BiasFaultDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr112() < 0)
                return;
        }

        private void cbTx4VfHighDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr112() < 0)
                return;
        }

        private void cbTx4VfLowDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr112() < 0)
                return;
        }

        private int _WriteAddr113()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx4CpaLevel.SelectedIndex);
            data[0] |= bTmp;

            if (cbTx4CpaDirection.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx4TestOscEnable.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 113, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx4CpaLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr113() < 0)
                return;
        }

        private void cbTx4CpaDirection_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr113() < 0)
                return;
        }

        private void cbTx4TestOscEnable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr113() < 0)
                return;
        }

        private int _WriteAddr114_115()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;

            i16Tmp = Convert.ToInt16(cbTx4Bias.SelectedIndex);
            i16Tmp <<= 5;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            rv = i2cWriteCB(84, 114, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx4Bias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr114_115() < 0)
                return;
        }

        private int _WriteAddr116_117()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;

            i16Tmp = Convert.ToInt16(cbTx4Mod.SelectedIndex);
            i16Tmp <<= 5;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            rv = i2cWriteCB(84, 116, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx4Mod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr116_117() < 0)
                return;
        }

        private int _WriteAddr118()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx4Eq.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 118, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx4Eq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr118() < 0)
                return;
        }

        private int _WriteAddr119_120()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;
            byte bTmp;

            i16Tmp = Convert.ToInt16(cbTx4BiasCoefT0.SelectedIndex);
            i16Tmp <<= 7;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx4BiasCoefT1.SelectedIndex);
            data[1] |= bTmp;

            rv = i2cWriteCB(84, 119, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx4BiasCoefT0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr119_120() < 0)
                return;
        }

        private void cbTx4BiasCoefT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr119_120() < 0)
                return;
        }

        private int _WriteAddr121()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx4BiasCoefT2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 121, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx4BiasCoefT2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr121() < 0)
                return;
        }

        private int _WriteAddr122_123()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;
            byte bTmp;

            i16Tmp = Convert.ToInt16(cbTx4ModCoefT0.SelectedIndex);
            i16Tmp <<= 7;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx4ModCoefT1.SelectedIndex);
            data[1] |= bTmp;

            rv = i2cWriteCB(84, 122, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx4ModCoefT0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr122_123() < 0)
                return;
        }

        private void cbTx4ModCoefT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr122_123() < 0)
                return;
        }

        private int _WriteAddr124()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx4ModCoefT2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 124, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx4ModCoefT2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr124() < 0)
                return;
        }

        private int _WriteAddr125()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;
            if (cbTx4TempcoEnable.Checked == true)
                data[0] |= 0x01;

            if (cbTx4HfcEnable.Checked == true)
                data[0] |= 0x02;

            if (cbTx4FepeakEnable.Checked == true)
                data[0] |= 0x04;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx4VhfCompProp.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 125, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx4TempcoEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr125() < 0)
                return;
        }

        private void cbTx4HfcEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr125() < 0)
                return;
        }

        private void cbTx4FepeakEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr125() < 0)
                return;
        }

        private void cbTx4VhfCompProp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr125() < 0)
                return;
        }

        private int _WriteAddr126()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx4VhfCompConst.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx4HfcRsel.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 126, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx4VhfCompConst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr126() < 0)
                return;
        }

        private void cbTx4HfcRsel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr126() < 0)
                return;
        }

        private int _WriteAddr127()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbTx4PeakEn.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbTx4PeakLenCtrl.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 127, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTx4PeakEn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr127() < 0)
                return;
        }

        private void cbTx4PeakLenCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr127() < 0)
                return;
        }

        private void _bLensAlign_Click(object sender, EventArgs e)
        {
            reading = true;

            cbGlobalTxFaultDis.Checked = true;
            cbGlobalBiasHighDis.Checked = true;
            cbGlobalVcselVfHighDis.Checked = true;
            cbGlobalVcselVfLowDis.Checked = true;
            if (_WriteAddr52() < 0)
                goto exit;

            cbTxEnable.Checked = true;
            cbVcselTestModeEnable.Checked = true;
            if (_WriteAddr54() < 0)
                goto exit;

            cbTx1CpaRange.Checked = true;
            cbTx1BiasFaultDisable.Checked = true;
            cbTx1VfHighDisable.Checked = true;
            cbTx1VfLowDisable.Checked = true;
            if (_WriteAddr64() < 0)
                goto exit;

            cbTx1Bias.SelectedIndex = 750;
            if (_WriteAddr66_67() < 0)
                goto exit;

            cbTx2CpaRange.Checked = true;
            cbTx2BiasFaultDisable.Checked = true;
            cbTx2VfHighDisable.Checked = true;
            cbTx2VfLowDisable.Checked = true;
            if (_WriteAddr80() < 0)
                goto exit;

            cbTx2Bias.SelectedIndex = 750;
            if (_WriteAddr82_83() < 0)
                goto exit;

            cbTx3CpaRange.Checked = true;
            cbTx3BiasFaultDisable.Checked = true;
            cbTx3VfHighDisable.Checked = true;
            cbTx3VfLowDisable.Checked = true;
            if (_WriteAddr96() < 0)
                goto exit;

            cbTx3Bias.SelectedIndex = 750;
            if (_WriteAddr98_99() < 0)
                goto exit;

            cbTx4CpaRange.Checked = true;
            cbTx4BiasFaultDisable.Checked = true;
            cbTx4VfHighDisable.Checked = true;
            cbTx4VfLowDisable.Checked = true;
            if (_WriteAddr112() < 0)
                goto exit;

            cbTx4Bias.SelectedIndex = 750;
            if (_WriteAddr114_115() < 0)
                goto exit;

        exit:
            reading = false;
        }

        private int _WriteAddr64_80_96_112()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;
            if (cbAllDisable.Checked == true)
                data[0] |= 0x01;

            if (cbAllRateSelect.Checked == true)
                data[0] |= 0x02;

            if (cbAllCpaRange.Checked == true)
                data[0] |= 0x04;

            if (cbAllPolarityInvert.Checked == true)
                data[0] |= 0x08;

            if (cbAllOutputSquelchSel.Checked == true)
                data[0] |= 0x10;

            if (cbAllBiasFaultDisable.Checked == true)
                data[0] |= 0x20;

            if (cbAllVfHighDisable.Checked == true)
                data[0] |= 0x40;

            if (cbAllVfLowDisable.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(84, 64, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 80, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 96, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 112, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64_80_96_112() < 0)
                return;
        }

        private void cbAllRateSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64_80_96_112() < 0)
                return;
        }

        private void cbAllCpaRange_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64_80_96_112() < 0)
                return;
        }

        private void cbAllPolarityInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64_80_96_112() < 0)
                return;
        }

        private void cbAllOutputSquelchSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64_80_96_112() < 0)
                return;
        }

        private void cbAllBiasFaultDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64_80_96_112() < 0)
                return;
        }

        private void cbAllVfHighDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64_80_96_112() < 0)
                return;
        }

        private void cbAllVfLowDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64_80_96_112() < 0)
                return;
        }

        private int _WriteAddr65_81_97_113()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbAllCpaLevel.SelectedIndex);
            data[0] |= bTmp;

            if (cbAllCpaDirection.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbAllTestOscEnable.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 65, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 81, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 97, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 113, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllCpaLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65_81_97_113() < 0)
                return;
        }

        private void cbAllCpaDirection_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65_81_97_113() < 0)
                return;
        }

        private void cbAllTestOscEnable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65_81_97_113() < 0)
                return;
        }

        private int _WriteAddr66_67_82_83_98_99_114_115()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;

            i16Tmp = Convert.ToInt16(cbAllBias.SelectedIndex);
            i16Tmp <<= 5;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            rv = i2cWriteCB(84, 66, 2, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 82, 2, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 98, 2, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 114, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllBias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr66_67_82_83_98_99_114_115() < 0)
                return;
        }

        private int _WriteAddr68_69_84_85_100_101_116_117()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;

            i16Tmp = Convert.ToInt16(cbAllMod.SelectedIndex);
            i16Tmp <<= 5;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            rv = i2cWriteCB(84, 68, 2, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 84, 2, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 100, 2, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 116, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllMod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr68_69_84_85_100_101_116_117() < 0)
                return;
        }

        private int _WriteAddr70_86_102_118()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbAllEq.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 70, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 86, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 102, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 118, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllEq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr70_86_102_118() < 0)
                return;
        }

        private int _WriteAddr71_72_87_88_103_104_119_120()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;
            byte bTmp;

            i16Tmp = Convert.ToInt16(cbAllBiasCoefT0.SelectedIndex);
            i16Tmp <<= 7;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            bTmp = 0;
            bTmp |= Convert.ToByte(cbAllBiasCoefT1.SelectedIndex);
            data[1] |= bTmp;

            rv = i2cWriteCB(84, 71, 2, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 87, 2, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 103, 2, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 119, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllBiasCoefT0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr71_72_87_88_103_104_119_120() < 0)
                return;
        }

        private void cbAllBiasCoefT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr71_72_87_88_103_104_119_120() < 0)
                return;
        }

        private int _WriteAddr73_89_105_121()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbAllBiasCoefT2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 73, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 89, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 105, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 121, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllBiasCoefT2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr73_89_105_121() < 0)
                return;
        }

        private int _WriteAddr74_75_90_91_106_107_122_123()
        {
            byte[] data;
            Int16 i16Tmp;
            int rv;
            byte bTmp;

            i16Tmp = Convert.ToInt16(cbAllModCoefT0.SelectedIndex);
            i16Tmp <<= 7;
            data = BitConverter.GetBytes(i16Tmp);
            Array.Reverse(data);

            bTmp = 0;
            bTmp |= Convert.ToByte(cbAllModCoefT1.SelectedIndex);
            data[1] |= bTmp;

            rv = i2cWriteCB(84, 74, 2, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 90, 2, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 106, 2, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 122, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllModCoefT0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr74_75_90_91_106_107_122_123() < 0)
                return;
        }

        private void cbAllModCoefT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr74_75_90_91_106_107_122_123() < 0)
                return;
        }

        private int _WriteAddr76_92_108_124()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbAllModCoefT2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 76, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 92, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 108, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 124, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllModCoefT2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr76_92_108_124() < 0)
                return;
        }

        private int _WriteAddr77_93_109_125()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            data[0] = 0;
            if (cbAllTempcoEnable.Checked == true)
                data[0] |= 0x01;

            if (cbAllHfcEnable.Checked == true)
                data[0] |= 0x02;

            if (cbAllFepeakEnable.Checked == true)
                data[0] |= 0x04;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbAllVhfCompProp.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 77, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 93, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 109, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 125, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllTempcoEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr77_93_109_125() < 0)
                return;
        }

        private void cbAllHfcEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr77_93_109_125() < 0)
                return;
        }

        private void cbAllFepeakEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr77_93_109_125() < 0)
                return;
        }

        private void cbAllVhfCompProp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr77_93_109_125() < 0)
                return;
        }

        private int _WriteAddr78_94_110_126()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbAllVhfCompConst.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbAllHfcRsel.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 78, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 94, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 110, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 126, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllVhfCompConst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr78_94_110_126() < 0)
                return;
        }

        private void cbAllHfcRsel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr78_94_110_126() < 0)
                return;
        }

        private int _WriteAddr79_95_111_127()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbAllPeakEn.SelectedIndex);
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbAllPeakLenCtrl.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            rv = i2cWriteCB(84, 79, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 95, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 111, 1, data);
            if (rv < 0)
                return -1;

            rv = i2cWriteCB(84, 127, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAllPeakEn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr79_95_111_127() < 0)
                return;
        }

        private void cbAllPeakLenCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr79_95_111_127() < 0)
                return;
        }

        private void bUnlock_Click(object sender, EventArgs e)
        {
            tbPassword0.Text = "9B6D";
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            byte[] data = { 0x55 };
            int rv;

            if (reading == true)
                return;

            bStoreIntoFlash.Enabled = false;
            rv = i2cWriteCB(84, 170, 1, data);

            Thread.Sleep(1000);
            bStoreIntoFlash.Enabled = true;
            return;
        }
    }
}
