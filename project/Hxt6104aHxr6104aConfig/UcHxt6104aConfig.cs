using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hxt6104aHxr6104aConfig
{
    public partial class UcHxt6104aConfig : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private bool reading = false;

        public UcHxt6104aConfig()
        {
            int i;

            InitializeComponent();

            for (i = 0; i < 4; i++) {
                cbGainCtrl.Items.Add(i);
                cbCurScale.Items.Add(i);
            }

            for (i = 0; i < 8; i++) {
                cbBWAdjust.Items.Add(i);
                cbSdth.Items.Add(i);
                cbDrvtnMod.Items.Add(i);
                cbDrvtnCst.Items.Add(i);
                cbEfMod.Items.Add(i);
                cbEfCst.Items.Add(i);
                cbResAdjust.Items.Add(i);
                cbPwa1.Items.Add(i);
                cbPwa2.Items.Add(i);
                cbPwa3.Items.Add(i);
                cbPwa4.Items.Add(i);
                cbPwaAll.Items.Add(i);
            }

            for (i = 0; i < 16; i++) {
                cbVvth.Items.Add(i);
                cbVvtl.Items.Add(i);
            }

            for (i = 0; i < 32; i++) {
                cbPd.Items.Add(i);
                cbEd.Items.Add(i);
                cbPk1.Items.Add(i);
                cbPk2.Items.Add(i);
                cbPk3.Items.Add(i);
                cbPk4.Items.Add(i);
                cbPkAll.Items.Add(i);
                cbEq1A1.Items.Add(i);
                cbEq1A2.Items.Add(i);
                cbEq1A3.Items.Add(i);
                cbEq1A4.Items.Add(i);
                cbEq1AAll.Items.Add(i);
                cbEq2A1.Items.Add(i);
                cbEq2A2.Items.Add(i);
                cbEq2A3.Items.Add(i);
                cbEq2A4.Items.Add(i);
                cbEq2AAll.Items.Add(i);
                cbEq3A1.Items.Add(i);
                cbEq3A2.Items.Add(i);
                cbEq3A3.Items.Add(i);
                cbEq3A4.Items.Add(i);
                cbEq3AAll.Items.Add(i);
            }
            for (i = 0; i < 64; i++) {
                cbBa1.Items.Add(i);
                cbBa2.Items.Add(i);
                cbBa3.Items.Add(i);
                cbBa4.Items.Add(i);
                cbBaAll.Items.Add(i);
                cbBm1.Items.Add(i);
                cbBm2.Items.Add(i);
                cbBm3.Items.Add(i);
                cbBm4.Items.Add(i);
                cbBmAll.Items.Add(i);
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

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[1];

            if (i2cWriteCB == null)
                return -1;

            data[0] = 32;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;

            data[0] = mode;
            if (i2cWriteCB(80, 164, 1, data) < 0)
                return -1;

            return 0;
        }

        private void _ParseAddr0(byte data)
        {
            int iTmp;

            iTmp = (data & 0xE0) >> 5;
            cbBWAdjust.SelectedIndex = iTmp;

            iTmp = (data & 0x18) >> 3;
            cbGainCtrl.SelectedIndex = iTmp;

            iTmp = data & 0x07;
            cbSdth.SelectedIndex = iTmp;
        }

        private void _ParseAddr1(byte data)
        {
            int iTmp;
            
            iTmp = (data & 0xE0) >> 5;
            cbDrvtnMod.SelectedIndex = iTmp;

            iTmp = (data & 0x1C) >> 2;
            cbDrvtnCst.SelectedIndex = iTmp;
            
            if ((data & 0x02) == 0)
                cbAutoLdis.Checked = false;
            else
                cbAutoLdis.Checked = true;
            
            if ((data & 0x01) == 0)
                cbGDPD.Checked = false;
            else
                cbGDPD.Checked = true;
        }

        private void _ParseAddr2(byte data)
        {
            int iTmp;
            
            iTmp = (data & 0xE0) >> 5;
            cbEfMod.SelectedIndex = iTmp;
            
            iTmp = (data & 0x1C) >> 2;
            cbEfCst.SelectedIndex = iTmp;

            iTmp = data & 0x03;
            cbCurScale.SelectedIndex = iTmp;
        }

        private void _ParseAddr3(byte data)
        {
            int iTmp;

            iTmp = (data & 0x38) >> 3;
            cbResAdjust.SelectedIndex = iTmp;
            
            if ((data & 0x04) == 0)
                cbCstTempRef.Checked = false;
            else
                cbCstTempRef.Checked = true;
            
            if ((data & 0x02) == 0)
                cbRegLoopId.Checked = false;
            else
                cbRegLoopId.Checked = true;
            
            if ((data & 0x01) == 0)
                cbRegLoopFe.Checked = false;
            else
                cbRegLoopFe.Checked = true;
        }

        private void _ParseAddr4(byte data)
        {
            if ((data & 0x80) == 0)
                cbIAvgSel4.Checked = false;
            else
                cbIAvgSel4.Checked = true;

            if ((data & 0x40) == 0)
                cbIAvgSel3.Checked = false;
            else
                cbIAvgSel3.Checked = true;

            if ((data & 0x20) == 0)
                cbIAvgSel2.Checked = false;
            else
                cbIAvgSel2.Checked = true;

            if ((data & 0x10) == 0)
                cbIAvgSel1.Checked = false;
            else
                cbIAvgSel1.Checked = true;
            
            if ((data & 0x08) == 0)
                cbIThermSel.Checked = false;
            else
                cbIThermSel.Checked = true;
            
            if ((data & 0x04) == 0)
                cbIUnitSel.Checked = false;
            else
                cbIUnitSel.Checked = true;
        }

        private void _ParseAddr6(byte data)
        {
            if ((data & 0x80) == 0)
                cbULdis.Checked = false;
            else
                cbULdis.Checked = true;
            
            if ((data & 0x40) == 0)
                cbConf2.Checked = false;
            else
                cbConf2.Checked = true;
            
            if ((data & 0x20) == 0)
                cbConf1.Checked = false;
            else
                cbConf1.Checked = true;
            
            if ((data & 0x04) == 0)
                cbBurnIn.Checked = false;
            else
                cbBurnIn.Checked = true;
            
            if ((data & 0x02) == 0)
                cbLdis.Checked = false;
            else
                cbLdis.Checked = true;
            
            if ((data & 0x01) == 0)
                cbNotInt.Checked = false;
            else
                cbNotInt.Checked = true;
        }

        private void _ParseAddr7(byte data)
        {
            if ((data & 0x80) == 0)
                cbIntInv.Checked = false;
            else
                cbIntInv.Checked = true;
            
            if ((data & 0x40) == 0)
                cbTestWin.Checked = false;
            else
                cbTestWin.Checked = true;
            
            if ((data & 0x20) == 0)
                cbInCmlh.Checked = false;
            else
                cbInCmlh.Checked = true;
            
            if ((data & 0x10) == 0)
                cbAutoIncrement.Checked = false;
            else
                cbAutoIncrement.Checked = true;
            
            if ((data & 0x08) == 0)
                cbSmartI2c.Checked = false;
            else
                cbSmartI2c.Checked = true;
        }

        private void _ParseAddr8(byte data)
        {
            int iTmp;
            
            iTmp = (data & 0xF0) >> 4;
            cbVvth.SelectedIndex = iTmp;

            iTmp = data & 0x0F;
            cbVvtl.SelectedIndex = iTmp;
        }

        private void _ParseAddr9(byte data)
        {
            int iTmp;

            if ((data & 0x20) == 0)
                cbPkEn.Checked = false;
            else
                cbPkEn.Checked = true;

            iTmp = data & 0x1F;
            cbPd.SelectedIndex = iTmp;
        }

        private void _ParseAddr10(byte data)
        {
            int iTmp;
            
            if ((data & 0x20) == 0)
                cbEqEn.Checked = false;
            else
                cbEqEn.Checked = true;
            
            iTmp = data & 0x1F;
            cbEd.SelectedIndex = iTmp;
        }

        private void _ParseAddr13(byte data)
        {
            int iTmp;
            
            iTmp = data & 0x3F;
            cbBaAll.SelectedIndex = iTmp;
        }

        private void _ParseAddr14(byte data)
        {
            int iTmp;
            
            iTmp = data & 0x3F;
            cbBmAll.SelectedIndex = iTmp;
        }

        private void _ParseAddr15(byte data)
        {
            int iTmp;
            
            iTmp = data & 0x1F;
            cbPkAll.SelectedIndex = iTmp;
        }

        private void _ParseAddr16(byte data)
        {
            int iTmp;

            iTmp = data & 0x3F;
            cbBa1.SelectedIndex = iTmp;
        }

        private void _ParseAddr17(byte data)
        {
            int iTmp;

            iTmp = data & 0x3F;
            cbBm1.SelectedIndex = iTmp;
        }

        private void _ParseAddr18(byte data)
        {
            int iTmp;

            iTmp = data & 0x1F;
            cbPk1.SelectedIndex = iTmp;
        }

        private void _ParseAddr19(byte data)
        {
            int iTmp;

            iTmp = data & 0x3F;
            cbBa2.SelectedIndex = iTmp;
        }

        private void _ParseAddr20(byte data)
        {
            int iTmp;

            iTmp = data & 0x3F;
            cbBm2.SelectedIndex = iTmp;
        }

        private void _ParseAddr21(byte data)
        {
            int iTmp;

            iTmp = data & 0x1F;
            cbPk2.SelectedIndex = iTmp;
        }

        private void _ParseAddr22(byte data)
        {
            int iTmp;

            iTmp = data & 0x3F;
            cbBa3.SelectedIndex = iTmp;
        }

        private void _ParseAddr23(byte data)
        {
            int iTmp;

            iTmp = data & 0x3F;
            cbBm3.SelectedIndex = iTmp;
        }

        private void _ParseAddr24(byte data)
        {
            int iTmp;

            iTmp = data & 0x1F;
            cbPk3.SelectedIndex = iTmp;
        }

        private void _ParseAddr25(byte data)
        {
            int iTmp;

            iTmp = data & 0x3F;
            cbBa4.SelectedIndex = iTmp;
        }

        private void _ParseAddr26(byte data)
        {
            int iTmp;

            iTmp = data & 0x3F;
            cbBm4.SelectedIndex = iTmp;
        }

        private void _ParseAddr27(byte data)
        {
            int iTmp;

            iTmp = data & 0x1F;
            cbPk4.SelectedIndex = iTmp;
        }

        private void _ParseAddr63(byte data)
        {
            int iTmp;

            if ((data & 0x80) == 0)
                cbPwasAll.Checked = false;
            else
                cbPwasAll.Checked = true;
            
            iTmp = (data & 0x70) >> 4;
            cbPwaAll.SelectedIndex = iTmp;
            
            if ((data & 0x08) == 0)
                cbDPDAll.Checked = false;
            else
                cbDPDAll.Checked = true;
            
            if ((data & 0x04) == 0)
                cbSdEnAll.Checked = false;
            else
                cbSdEnAll.Checked = true;
            
            if ((data & 0x02) == 0)
                cbSqEnAll.Checked = false;
            else
                cbSqEnAll.Checked = true;
            
            if ((data & 0x01) == 0)
                cbInvAll.Checked = false;
            else
                cbInvAll.Checked = true;
        }

        private void _ParseAddr64(byte data)
        {
            int iTmp;

            if ((data & 0x80) == 0)
                cbPwas1.Checked = false;
            else
                cbPwas1.Checked = true;

            iTmp = (data & 0x70) >> 4;
            cbPwa1.SelectedIndex = iTmp;

            if ((data & 0x08) == 0)
                cbDPD1.Checked = false;
            else
                cbDPD1.Checked = true;

            if ((data & 0x04) == 0)
                cbSdEn1.Checked = false;
            else
                cbSdEn1.Checked = true;

            if ((data & 0x02) == 0)
                cbSqEn1.Checked = false;
            else
                cbSqEn1.Checked = true;

            if ((data & 0x01) == 0)
                cbInv1.Checked = false;
            else
                cbInv1.Checked = true;
        }

        private void _ParseAddr65(byte data)
        {
            int iTmp;

            if ((data & 0x80) == 0)
                cbPwas2.Checked = false;
            else
                cbPwas2.Checked = true;

            iTmp = (data & 0x70) >> 4;
            cbPwa2.SelectedIndex = iTmp;

            if ((data & 0x08) == 0)
                cbDPD2.Checked = false;
            else
                cbDPD2.Checked = true;

            if ((data & 0x04) == 0)
                cbSdEn2.Checked = false;
            else
                cbSdEn2.Checked = true;

            if ((data & 0x02) == 0)
                cbSqEn2.Checked = false;
            else
                cbSqEn2.Checked = true;

            if ((data & 0x01) == 0)
                cbInv2.Checked = false;
            else
                cbInv2.Checked = true;
        }

        private void _ParseAddr66(byte data)
        {
            int iTmp;

            if ((data & 0x80) == 0)
                cbPwas3.Checked = false;
            else
                cbPwas3.Checked = true;

            iTmp = (data & 0x70) >> 4;
            cbPwa3.SelectedIndex = iTmp;

            if ((data & 0x08) == 0)
                cbDPD3.Checked = false;
            else
                cbDPD3.Checked = true;

            if ((data & 0x04) == 0)
                cbSdEn3.Checked = false;
            else
                cbSdEn3.Checked = true;

            if ((data & 0x02) == 0)
                cbSqEn3.Checked = false;
            else
                cbSqEn3.Checked = true;

            if ((data & 0x01) == 0)
                cbInv3.Checked = false;
            else
                cbInv3.Checked = true;
        }

        private void _ParseAddr67(byte data)
        {
            int iTmp;

            if ((data & 0x80) == 0)
                cbPwas4.Checked = false;
            else
                cbPwas4.Checked = true;

            iTmp = (data & 0x70) >> 4;
            cbPwa4.SelectedIndex = iTmp;

            if ((data & 0x08) == 0)
                cbDPD4.Checked = false;
            else
                cbDPD4.Checked = true;

            if ((data & 0x04) == 0)
                cbSdEn4.Checked = false;
            else
                cbSdEn4.Checked = true;

            if ((data & 0x02) == 0)
                cbSqEn4.Checked = false;
            else
                cbSqEn4.Checked = true;

            if ((data & 0x01) == 0)
                cbInv4.Checked = false;
            else
                cbInv4.Checked = true;
        }

        private void _ParseAddr79(byte data)
        {
            if ((data & 0x80) == 0)
                cbSdAll.Checked = false;
            else
                cbSdAll.Checked = true;
            
            if ((data & 0x40) == 0)
                cbLosAll.Checked = false;
            else
                cbLosAll.Checked = true;
            
            if ((data & 0x20) == 0)
                cbVvhAll.Checked = false;
            else
                cbVvhAll.Checked = true;

            if ((data & 0x10) == 0)
                cbVvlAll.Checked = false;
            else
                cbVvlAll.Checked = true;

            if ((data & 0x08) == 0)
                cbLSdAll.Checked = false;
            else
                cbLSdAll.Checked = true;

            if ((data & 0x04) == 0)
                cbLLosAll.Checked = false;
            else
                cbLLosAll.Checked = true;

            if ((data & 0x02) == 0)
                cbLVvhAll.Checked = false;
            else
                cbLVvhAll.Checked = true;

            if ((data & 0x01) == 0)
                cbLVvlAll.Checked = false;
            else
                cbLVvlAll.Checked = true;
        }

        private void _ParseAddr80(byte data)
        {
            if ((data & 0x80) == 0)
                cbSd1.Checked = false;
            else
                cbSd1.Checked = true;

            if ((data & 0x40) == 0)
                cbLos1.Checked = false;
            else
                cbLos1.Checked = true;

            if ((data & 0x20) == 0)
                cbVvh1.Checked = false;
            else
                cbVvh1.Checked = true;

            if ((data & 0x10) == 0)
                cbVvl1.Checked = false;
            else
                cbVvl1.Checked = true;

            if ((data & 0x08) == 0)
                cbLSd1.Checked = false;
            else
                cbLSd1.Checked = true;

            if ((data & 0x04) == 0)
                cbLLos1.Checked = false;
            else
                cbLLos1.Checked = true;

            if ((data & 0x02) == 0)
                cbLVvh1.Checked = false;
            else
                cbLVvh1.Checked = true;

            if ((data & 0x01) == 0)
                cbLVvl1.Checked = false;
            else
                cbLVvl1.Checked = true;
        }

        private void _ParseAddr81(byte data)
        {
            if ((data & 0x80) == 0)
                cbSd2.Checked = false;
            else
                cbSd2.Checked = true;

            if ((data & 0x40) == 0)
                cbLos2.Checked = false;
            else
                cbLos2.Checked = true;

            if ((data & 0x20) == 0)
                cbVvh2.Checked = false;
            else
                cbVvh2.Checked = true;

            if ((data & 0x10) == 0)
                cbVvl2.Checked = false;
            else
                cbVvl2.Checked = true;

            if ((data & 0x08) == 0)
                cbLSd2.Checked = false;
            else
                cbLSd2.Checked = true;

            if ((data & 0x04) == 0)
                cbLLos2.Checked = false;
            else
                cbLLos2.Checked = true;

            if ((data & 0x02) == 0)
                cbLVvh2.Checked = false;
            else
                cbLVvh2.Checked = true;

            if ((data & 0x01) == 0)
                cbLVvl2.Checked = false;
            else
                cbLVvl2.Checked = true;
        }

        private void _ParseAddr82(byte data)
        {
            if ((data & 0x80) == 0)
                cbSd3.Checked = false;
            else
                cbSd3.Checked = true;

            if ((data & 0x40) == 0)
                cbLos3.Checked = false;
            else
                cbLos3.Checked = true;

            if ((data & 0x20) == 0)
                cbVvh3.Checked = false;
            else
                cbVvh3.Checked = true;

            if ((data & 0x10) == 0)
                cbVvl3.Checked = false;
            else
                cbVvl3.Checked = true;

            if ((data & 0x08) == 0)
                cbLSd3.Checked = false;
            else
                cbLSd3.Checked = true;

            if ((data & 0x04) == 0)
                cbLLos3.Checked = false;
            else
                cbLLos3.Checked = true;

            if ((data & 0x02) == 0)
                cbLVvh3.Checked = false;
            else
                cbLVvh3.Checked = true;

            if ((data & 0x01) == 0)
                cbLVvl3.Checked = false;
            else
                cbLVvl3.Checked = true;
        }

        private void _ParseAddr83(byte data)
        {
            if ((data & 0x80) == 0)
                cbSd4.Checked = false;
            else
                cbSd4.Checked = true;

            if ((data & 0x40) == 0)
                cbLos4.Checked = false;
            else
                cbLos4.Checked = true;

            if ((data & 0x20) == 0)
                cbVvh4.Checked = false;
            else
                cbVvh4.Checked = true;

            if ((data & 0x10) == 0)
                cbVvl4.Checked = false;
            else
                cbVvl4.Checked = true;

            if ((data & 0x08) == 0)
                cbLSd4.Checked = false;
            else
                cbLSd4.Checked = true;

            if ((data & 0x04) == 0)
                cbLLos4.Checked = false;
            else
                cbLLos4.Checked = true;

            if ((data & 0x02) == 0)
                cbLVvh4.Checked = false;
            else
                cbLVvh4.Checked = true;

            if ((data & 0x01) == 0)
                cbLVvl4.Checked = false;
            else
                cbLVvl4.Checked = true;
        }

        private void _ParseAddr95(byte data)
        {
            if ((data & 0x80) == 0)
                cbSdMAll.Checked = false;
            else
                cbSdMAll.Checked = true;
            
            if ((data & 0x40) == 0)
                cbLosMAll.Checked = false;
            else
                cbLosMAll.Checked = true;
            
            if ((data & 0x20) == 0)
                cbVvhMAll.Checked = false;
            else
                cbVvhMAll.Checked = true;

            if ((data & 0x10) == 0)
                cbVvlMAll.Checked = false;
            else
                cbVvlMAll.Checked = true;

            if ((data & 0x08) == 0)
                cbLSdMAll.Checked = false;
            else
                cbLSdMAll.Checked = true;

            if ((data & 0x04) == 0)
                cbLLosMAll.Checked = false;
            else
                cbLLosMAll.Checked = true;

            if ((data & 0x02) == 0)
                cbLVvhMAll.Checked = false;
            else
                cbLVvhMAll.Checked = true;

            if ((data & 0x01) == 0)
                cbLVvlMAll.Checked = false;
            else
                cbLVvlMAll.Checked = true;
        }

        private void _ParseAddr96(byte data)
        {
            if ((data & 0x80) == 0)
                cbSdM1.Checked = false;
            else
                cbSdM1.Checked = true;

            if ((data & 0x40) == 0)
                cbLosM1.Checked = false;
            else
                cbLosM1.Checked = true;

            if ((data & 0x20) == 0)
                cbVvhM1.Checked = false;
            else
                cbVvhM1.Checked = true;

            if ((data & 0x10) == 0)
                cbVvlM1.Checked = false;
            else
                cbVvlM1.Checked = true;

            if ((data & 0x08) == 0)
                cbLSdM1.Checked = false;
            else
                cbLSdM1.Checked = true;

            if ((data & 0x04) == 0)
                cbLLosM1.Checked = false;
            else
                cbLLosM1.Checked = true;

            if ((data & 0x02) == 0)
                cbLVvhM1.Checked = false;
            else
                cbLVvhM1.Checked = true;

            if ((data & 0x01) == 0)
                cbLVvlM1.Checked = false;
            else
                cbLVvlM1.Checked = true;
        }

        private void _ParseAddr97(byte data)
        {
            if ((data & 0x80) == 0)
                cbSdM2.Checked = false;
            else
                cbSdM2.Checked = true;

            if ((data & 0x40) == 0)
                cbLosM2.Checked = false;
            else
                cbLosM2.Checked = true;

            if ((data & 0x20) == 0)
                cbVvhM2.Checked = false;
            else
                cbVvhM2.Checked = true;

            if ((data & 0x10) == 0)
                cbVvlM2.Checked = false;
            else
                cbVvlM2.Checked = true;

            if ((data & 0x08) == 0)
                cbLSdM2.Checked = false;
            else
                cbLSdM2.Checked = true;

            if ((data & 0x04) == 0)
                cbLLosM2.Checked = false;
            else
                cbLLosM2.Checked = true;

            if ((data & 0x02) == 0)
                cbLVvhM2.Checked = false;
            else
                cbLVvhM2.Checked = true;

            if ((data & 0x01) == 0)
                cbLVvlM2.Checked = false;
            else
                cbLVvlM2.Checked = true;
        }

        private void _ParseAddr98(byte data)
        {
            if ((data & 0x80) == 0)
                cbSdM3.Checked = false;
            else
                cbSdM3.Checked = true;

            if ((data & 0x40) == 0)
                cbLosM3.Checked = false;
            else
                cbLosM3.Checked = true;

            if ((data & 0x20) == 0)
                cbVvhM3.Checked = false;
            else
                cbVvhM3.Checked = true;

            if ((data & 0x10) == 0)
                cbVvlM3.Checked = false;
            else
                cbVvlM3.Checked = true;

            if ((data & 0x08) == 0)
                cbLSdM3.Checked = false;
            else
                cbLSdM3.Checked = true;

            if ((data & 0x04) == 0)
                cbLLosM3.Checked = false;
            else
                cbLLosM3.Checked = true;

            if ((data & 0x02) == 0)
                cbLVvhM3.Checked = false;
            else
                cbLVvhM3.Checked = true;

            if ((data & 0x01) == 0)
                cbLVvlM3.Checked = false;
            else
                cbLVvlM3.Checked = true;
        }

        private void _ParseAddr99(byte data)
        {
            if ((data & 0x80) == 0)
                cbSdM4.Checked = false;
            else
                cbSdM4.Checked = true;

            if ((data & 0x40) == 0)
                cbLosM4.Checked = false;
            else
                cbLosM4.Checked = true;

            if ((data & 0x20) == 0)
                cbVvhM4.Checked = false;
            else
                cbVvhM4.Checked = true;

            if ((data & 0x10) == 0)
                cbVvlM4.Checked = false;
            else
                cbVvlM4.Checked = true;

            if ((data & 0x08) == 0)
                cbLSdM4.Checked = false;
            else
                cbLSdM4.Checked = true;

            if ((data & 0x04) == 0)
                cbLLosM4.Checked = false;
            else
                cbLLosM4.Checked = true;

            if ((data & 0x02) == 0)
                cbLVvhM4.Checked = false;
            else
                cbLVvhM4.Checked = true;

            if ((data & 0x01) == 0)
                cbLVvlM4.Checked = false;
            else
                cbLVvlM4.Checked = true;
        }

        private void _ParseAddr111(byte data)
        {
            if ((data & 0x08) == 0)
                cbFlthAll.Checked = false;
            else
                cbFlthAll.Checked = true;

            if ((data & 0x04) == 0)
                cbFltlAll.Checked = false;
            else
                cbFltlAll.Checked = true;
            
            if ((data & 0x02) == 0)
                cbLFlthAll.Checked = false;
            else
                cbLFlthAll.Checked = true;

            if ((data & 0x01) == 0)
                cbLFltlAll.Checked = false;
            else
                cbLFltlAll.Checked = true;
        }

        private void _ParseAddr112(byte data)
        {
            if ((data & 0x08) == 0)
                cbFlth1.Checked = false;
            else
                cbFlth1.Checked = true;

            if ((data & 0x04) == 0)
                cbFltl1.Checked = false;
            else
                cbFltl1.Checked = true;

            if ((data & 0x02) == 0)
                cbLFlth1.Checked = false;
            else
                cbLFlth1.Checked = true;

            if ((data & 0x01) == 0)
                cbLFltl1.Checked = false;
            else
                cbLFltl1.Checked = true;
        }

        private void _ParseAddr113(byte data)
        {
            if ((data & 0x08) == 0)
                cbFlth2.Checked = false;
            else
                cbFlth2.Checked = true;

            if ((data & 0x04) == 0)
                cbFltl2.Checked = false;
            else
                cbFltl2.Checked = true;

            if ((data & 0x02) == 0)
                cbLFlth2.Checked = false;
            else
                cbLFlth2.Checked = true;

            if ((data & 0x01) == 0)
                cbLFltl2.Checked = false;
            else
                cbLFltl2.Checked = true;
        }

        private void _ParseAddr114(byte data)
        {
            if ((data & 0x08) == 0)
                cbFlth3.Checked = false;
            else
                cbFlth3.Checked = true;

            if ((data & 0x04) == 0)
                cbFltl3.Checked = false;
            else
                cbFltl3.Checked = true;

            if ((data & 0x02) == 0)
                cbLFlth3.Checked = false;
            else
                cbLFlth3.Checked = true;

            if ((data & 0x01) == 0)
                cbLFltl3.Checked = false;
            else
                cbLFltl3.Checked = true;
        }

        private void _ParseAddr115(byte data)
        {
            if ((data & 0x08) == 0)
                cbFlth4.Checked = false;
            else
                cbFlth4.Checked = true;

            if ((data & 0x04) == 0)
                cbFltl4.Checked = false;
            else
                cbFltl4.Checked = true;

            if ((data & 0x02) == 0)
                cbLFlth4.Checked = false;
            else
                cbLFlth4.Checked = true;

            if ((data & 0x01) == 0)
                cbLFltl4.Checked = false;
            else
                cbLFltl4.Checked = true;
        }

        private void _ParseAddr127(byte data)
        {
            if ((data & 0x08) == 0)
                cbFlthMAll.Checked = false;
            else
                cbFlthMAll.Checked = true;

            if ((data & 0x04) == 0)
                cbFltlMAll.Checked = false;
            else
                cbFltlMAll.Checked = true;

            if ((data & 0x02) == 0)
                cbLFlthMAll.Checked = false;
            else
                cbLFlthMAll.Checked = true;

            if ((data & 0x01) == 0)
                cbLFltlMAll.Checked = false;
            else
                cbLFltlMAll.Checked = true;
        }

        private void _ParseAddr128(byte data)
        {
            if ((data & 0x08) == 0)
                cbFlthM1.Checked = false;
            else
                cbFlthM1.Checked = true;

            if ((data & 0x04) == 0)
                cbFltlM1.Checked = false;
            else
                cbFltlM1.Checked = true;

            if ((data & 0x02) == 0)
                cbLFlthM1.Checked = false;
            else
                cbLFlthM1.Checked = true;

            if ((data & 0x01) == 0)
                cbLFltlM1.Checked = false;
            else
                cbLFltlM1.Checked = true;
        }

        private void _ParseAddr129(byte data)
        {
            if ((data & 0x08) == 0)
                cbFlthM2.Checked = false;
            else
                cbFlthM2.Checked = true;

            if ((data & 0x04) == 0)
                cbFltlM2.Checked = false;
            else
                cbFltlM2.Checked = true;

            if ((data & 0x02) == 0)
                cbLFlthM2.Checked = false;
            else
                cbLFlthM2.Checked = true;

            if ((data & 0x01) == 0)
                cbLFltlM2.Checked = false;
            else
                cbLFltlM2.Checked = true;
        }

        private void _ParseAddr130(byte data)
        {
            if ((data & 0x08) == 0)
                cbFlthM3.Checked = false;
            else
                cbFlthM3.Checked = true;

            if ((data & 0x04) == 0)
                cbFltlM3.Checked = false;
            else
                cbFltlM3.Checked = true;

            if ((data & 0x02) == 0)
                cbLFlthM3.Checked = false;
            else
                cbLFlthM3.Checked = true;

            if ((data & 0x01) == 0)
                cbLFltlM3.Checked = false;
            else
                cbLFltlM3.Checked = true;
        }

        private void _ParseAddr131(byte data)
        {
            if ((data & 0x08) == 0)
                cbFlthM4.Checked = false;
            else
                cbFlthM4.Checked = true;

            if ((data & 0x04) == 0)
                cbFltlM4.Checked = false;
            else
                cbFltlM4.Checked = true;

            if ((data & 0x02) == 0)
                cbLFlthM4.Checked = false;
            else
                cbLFlthM4.Checked = true;

            if ((data & 0x01) == 0)
                cbLFltlM4.Checked = false;
            else
                cbLFltlM4.Checked = true;
        }

        private void _ParseAddr143(byte data)
        {
            int iTmp;
            
            if ((data & 0x20) == 0)
                cbEqDly1EnAll.Checked = false;
            else
                cbEqDly1EnAll.Checked = true;
            
            iTmp = (data & 0x1F) >> 4;
            cbEq1AAll.SelectedIndex = iTmp;
        }

        private void _ParseAddr144(byte data)
        {
            int iTmp;

            if ((data & 0x20) == 0)
                cbEqDly1En1.Checked = false;
            else
                cbEqDly1En1.Checked = true;

            iTmp = (data & 0x1F) >> 4;
            cbEq1A1.SelectedIndex = iTmp;
        }

        private void _ParseAddr145(byte data)
        {
            int iTmp;

            if ((data & 0x20) == 0)
                cbEqDly1En2.Checked = false;
            else
                cbEqDly1En2.Checked = true;

            iTmp = (data & 0x1F) >> 4;
            cbEq1A2.SelectedIndex = iTmp;
        }

        private void _ParseAddr146(byte data)
        {
            int iTmp;

            if ((data & 0x20) == 0)
                cbEqDly1En3.Checked = false;
            else
                cbEqDly1En3.Checked = true;

            iTmp = (data & 0x1F) >> 4;
            cbEq1A3.SelectedIndex = iTmp;
        }

        private void _ParseAddr147(byte data)
        {
            int iTmp;

            if ((data & 0x20) == 0)
                cbEqDly1En4.Checked = false;
            else
                cbEqDly1En4.Checked = true;

            iTmp = (data & 0x1F) >> 4;
            cbEq1A4.SelectedIndex = iTmp;
        }

        private void _ParseAddr159(byte data)
        {
            int iTmp;

            if ((data & 0x20) == 0)
                cbEqDly2EnAll.Checked = false;
            else
                cbEqDly2EnAll.Checked = true;

            iTmp = (data & 0x1F) >> 4;
            cbEq2AAll.SelectedIndex = iTmp;
        }

        private void _ParseAddr160(byte data)
        {
            int iTmp;

            if ((data & 0x20) == 0)
                cbEqDly2En1.Checked = false;
            else
                cbEqDly2En1.Checked = true;

            iTmp = (data & 0x1F) >> 4;
            cbEq2A1.SelectedIndex = iTmp;
        }

        private void _ParseAddr161(byte data)
        {
            int iTmp;

            if ((data & 0x20) == 0)
                cbEqDly2En2.Checked = false;
            else
                cbEqDly2En2.Checked = true;

            iTmp = (data & 0x1F) >> 4;
            cbEq2A2.SelectedIndex = iTmp;
        }

        private void _ParseAddr162(byte data)
        {
            int iTmp;

            if ((data & 0x20) == 0)
                cbEqDly2En3.Checked = false;
            else
                cbEqDly2En3.Checked = true;

            iTmp = (data & 0x1F) >> 4;
            cbEq2A3.SelectedIndex = iTmp;
        }

        private void _ParseAddr163(byte data)
        {
            int iTmp;

            if ((data & 0x20) == 0)
                cbEqDly2En4.Checked = false;
            else
                cbEqDly2En4.Checked = true;

            iTmp = (data & 0x1F) >> 4;
            cbEq2A4.SelectedIndex = iTmp;
        }

        private void _ParseAddr175(byte data)
        {
            int iTmp;

            if ((data & 0x20) == 0)
                cbEqDly3EnAll.Checked = false;
            else
                cbEqDly3EnAll.Checked = true;

            iTmp = (data & 0x1F) >> 4;
            cbEq3AAll.SelectedIndex = iTmp;
        }

        private void _ParseAddr176(byte data)
        {
            int iTmp;

            if ((data & 0x20) == 0)
                cbEqDly3En1.Checked = false;
            else
                cbEqDly3En1.Checked = true;

            iTmp = (data & 0x1F) >> 4;
            cbEq3A1.SelectedIndex = iTmp;
        }

        private void _ParseAddr177(byte data)
        {
            int iTmp;

            if ((data & 0x20) == 0)
                cbEqDly3En2.Checked = false;
            else
                cbEqDly3En2.Checked = true;

            iTmp = (data & 0x1F) >> 4;
            cbEq3A2.SelectedIndex = iTmp;
        }

        private void _ParseAddr178(byte data)
        {
            int iTmp;

            if ((data & 0x20) == 0)
                cbEqDly3En3.Checked = false;
            else
                cbEqDly3En3.Checked = true;

            iTmp = (data & 0x1F) >> 4;
            cbEq3A3.SelectedIndex = iTmp;
        }

        private void _ParseAddr179(byte data)
        {
            int iTmp;

            if ((data & 0x20) == 0)
                cbEqDly3En4.Checked = false;
            else
                cbEqDly3En4.Checked = true;

            iTmp = (data & 0x1F) >> 4;
            cbEq3A4.SelectedIndex = iTmp;
        }

        private void _ParseAddr240(byte data)
        {
            if ((data & 0x80) == 0)
                cbGNotInt.Checked = false;
            else
                cbGNotInt.Checked = true;
        }

        private void _ParseAddr241(byte data)
        {
            if ((data & 0x08) == 0)
                cbNotInt4.Checked = false;
            else
                cbNotInt4.Checked = true;

            if ((data & 0x04) == 0)
                cbNotInt3.Checked = false;
            else
                cbNotInt3.Checked = true;

            if ((data & 0x02) == 0)
                cbNotInt2.Checked = false;
            else
                cbNotInt2.Checked = true;

            if ((data & 0x01) == 0)
                cbNotInt1.Checked = false;
            else
                cbNotInt1.Checked = true;
        }

        private void _ParseAddr242(byte data)
        {
            if ((data & 0x80) == 0)
                cbNotGSD.Checked = false;
            else
                cbNotGSD.Checked = true;
            
            if ((data & 0x40) == 0)
                cbNotGLos.Checked = false;
            else
                cbNotGLos.Checked = true;
            
            if ((data & 0x20) == 0)
                cbNotGVvh.Checked = false;
            else
                cbNotGVvh.Checked = true;

            if ((data & 0x10) == 0)
                cbNotGVvl.Checked = false;
            else
                cbNotGVvl.Checked = true;

            if ((data & 0x08) == 0)
                cbNotGLSd.Checked = false;
            else
                cbNotGLSd.Checked = true;

            if ((data & 0x04) == 0)
                cbNotGLLos.Checked = false;
            else
                cbNotGLLos.Checked = true;

            if ((data & 0x02) == 0)
                cbNotGLVvh.Checked = false;
            else
                cbNotGLVvh.Checked = true;

            if ((data & 0x01) == 0)
                cbNotGLVvl.Checked = false;
            else
                cbNotGLVvl.Checked = true;
        }

        private void _ParseAddr243(byte data)
        {
            if ((data & 0x08) == 0)
                cbNotGFlth.Checked = false;
            else
                cbNotGFlth.Checked = true;

            if ((data & 0x04) == 0)
                cbNotGFltl.Checked = false;
            else
                cbNotGFltl.Checked = true;

            if ((data & 0x02) == 0)
                cbNotGLFlth.Checked = false;
            else
                cbNotGLFlth.Checked = true;

            if ((data & 0x01) == 0)
                cbNotGLFltl.Checked = false;
            else
                cbNotGLFltl.Checked = true;
        }

        private void _bRead_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[16];
            int rv;

            reading = true;

            if (i2cReadCB == null)
                goto exit;

            if (_SetQsfpMode(0x4C) < 0)
                goto exit;

            rv = i2cReadCB(107, 0, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr0(data[0]);
            _ParseAddr1(data[1]);
            _ParseAddr2(data[2]);
            _ParseAddr3(data[3]);
            _ParseAddr4(data[4]);

            rv = i2cReadCB(107, 6, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr6(data[0]);
            _ParseAddr7(data[1]);
            _ParseAddr8(data[2]);
            _ParseAddr9(data[3]);
            _ParseAddr10(data[4]);

            rv = i2cReadCB(107, 13, 15, data);
            if (rv != 15)
                goto exit;

            _ParseAddr13(data[0]);
            _ParseAddr14(data[1]);
            _ParseAddr15(data[2]);
            _ParseAddr16(data[3]);
            _ParseAddr17(data[4]);
            _ParseAddr18(data[5]);
            _ParseAddr19(data[6]);
            _ParseAddr20(data[7]);
            _ParseAddr21(data[8]);
            _ParseAddr22(data[9]);
            _ParseAddr23(data[10]);
            _ParseAddr24(data[11]);
            _ParseAddr25(data[12]);
            _ParseAddr26(data[13]);
            _ParseAddr27(data[14]);

            rv = i2cReadCB(107, 63, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr63(data[0]);
            _ParseAddr64(data[1]);
            _ParseAddr65(data[2]);
            _ParseAddr66(data[3]);
            _ParseAddr67(data[4]);

            rv = i2cReadCB(107, 79, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr79(data[0]);
            _ParseAddr80(data[1]);
            _ParseAddr81(data[2]);
            _ParseAddr82(data[3]);
            _ParseAddr83(data[4]);

            rv = i2cReadCB(107, 95, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr95(data[0]);
            _ParseAddr96(data[1]);
            _ParseAddr97(data[2]);
            _ParseAddr98(data[3]);
            _ParseAddr99(data[4]);

            rv = i2cReadCB(107, 111, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr111(data[0]);
            _ParseAddr112(data[1]);
            _ParseAddr113(data[2]);
            _ParseAddr114(data[3]);
            _ParseAddr115(data[4]);

            rv = i2cReadCB(107, 127, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr127(data[0]);
            _ParseAddr128(data[1]);
            _ParseAddr129(data[2]);
            _ParseAddr130(data[3]);
            _ParseAddr131(data[4]);

            rv = i2cReadCB(107, 143, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr143(data[0]);
            _ParseAddr144(data[1]);
            _ParseAddr145(data[2]);
            _ParseAddr146(data[3]);
            _ParseAddr147(data[4]);

            rv = i2cReadCB(107, 159, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr159(data[0]);
            _ParseAddr160(data[1]);
            _ParseAddr161(data[2]);
            _ParseAddr162(data[3]);
            _ParseAddr163(data[4]);

            rv = i2cReadCB(107, 175, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr175(data[0]);
            _ParseAddr176(data[1]);
            _ParseAddr177(data[2]);
            _ParseAddr178(data[3]);
            _ParseAddr179(data[4]);

            rv = i2cReadCB(107, 224, 2, data);
            if (rv != 2)
                goto exit;

            tbDeviceId.Text = data[0].ToString();
            tbRevisionId.Text = data[1].ToString();

            rv = i2cReadCB(107, 240, 4, data);
            if (rv != 4)
                goto exit;

            _ParseAddr240(data[0]);
            _ParseAddr241(data[1]);
            _ParseAddr242(data[2]);
            _ParseAddr243(data[3]);

        exit:
            reading = false;
        }

        private int _WriteAddr0()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;
            
            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbBWAdjust.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;
            
            bTmp = 0;
            bTmp |= Convert.ToByte(cbGainCtrl.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbSdth.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 0, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbBWAdjust_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0() < 0)
                return;
        }

        private void _cbGainCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0() < 0)
                return;
        }

        private void _cbSdth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0() < 0)
                return;
        }

        private int _WriteAddr1()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;
            
            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbDrvtnMod.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;
            
            bTmp = 0;
            bTmp |= Convert.ToByte(cbDrvtnCst.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            if (cbAutoLdis.Checked == true)
                data[0] |= 0x02;

            if (cbGDPD.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 1, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbDrvtnMod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1() < 0)
                return;
        }

        private void _cbDrvtnCst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1() < 0)
                return;
        }

        private void _cbAutoLdis_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1() < 0)
                return;
        }

        private void _cbGDPD_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1() < 0)
                return;
        }

        private int _WriteAddr2()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;
            
            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbEfMod.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;
            
            bTmp = 0;
            bTmp |= Convert.ToByte(cbEfCst.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;
            
            bTmp = 0;
            bTmp |= Convert.ToByte(cbCurScale.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbEfMod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2() < 0)
                return;
        }

        private void _cbEfCst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2() < 0)
                return;
        }

        private void _cbCurScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2() < 0)
                return;
        }

        private int _WriteAddr3()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;
            
            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbResAdjust.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;

            if (cbCstTempRef.Checked == true)
                data[0] |= 0x04;

            if (cbRegLoopId.Checked == true)
                data[0] |= 0x02;

            if (cbRegLoopFe.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 3, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbResAdjust_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr3() < 0)
                return;
        }

        private void _cbCstTempRef_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr3() < 0)
                return;
        }

        private void _cbRegLoopId_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr3() < 0)
                return;
        }

        private void _cbRegLoopFe_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr3() < 0)
                return;
        }

        private int _WriteAddr4()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;
            if (cbIAvgSel4.Checked == true)
                data[0] |= 0x80;

            if (cbIAvgSel3.Checked == true)
                data[0] |= 0x40;

            if (cbIAvgSel2.Checked == true)
                data[0] |= 0x20;

            if (cbIAvgSel1.Checked == true)
                data[0] |= 0x10;

            if (cbIThermSel.Checked == true)
                data[0] |= 0x08;

            if (cbIUnitSel.Checked == true)
                data[0] |= 0x04;

            rv = i2cWriteCB(107, 4, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbIAvgSel4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4() < 0)
                return;
        }

        private void _cbIAvgSel3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4() < 0)
                return;
        }

        private void _cbIAvgSel2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4() < 0)
                return;
        }

        private void _cbIAvgSel1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4() < 0)
                return;
        }

        private void _cbIThermSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4() < 0)
                return;
        }

        private void _cbIUnitSel_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4() < 0)
                return;
        }

        private int _WriteAddr6()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;
            if (cbULdis.Checked == true)
                data[0] |= 0x80;

            rv = i2cWriteCB(107, 6, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbULdis_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr6() < 0)
                return;
        }

        private int _WriteAddr7()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;
            
            data[0] = 0;
            if (cbIntInv.Checked == true)
                data[0] |= 0x80;

            if (cbTestWin.Checked == true)
                data[0] |= 0x40;

            if (cbInCmlh.Checked == true)
                data[0] |= 0x20;

            if (cbAutoIncrement.Checked == true)
                data[0] |= 0x10;

            if (cbSmartI2c.Checked == true)
                data[0] |= 0x08;

            rv = i2cWriteCB(107, 7, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbIntInv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr7() < 0)
                return;
        }

        private void _cbTestWin_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr7() < 0)
                return;
        }

        private void _cbInCmlh_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr7() < 0)
                return;
        }

        private void _cbAutoIncrement_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr7() < 0)
                return;
        }

        private void _cbSmartI2c_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr7() < 0)
                return;
        }

        private int _WriteAddr8()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;
            
            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbVvth.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbVvtl.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 8, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbVvth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr8() < 0)
                return;
        }

        private void _cbVvtl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr8() < 0)
                return;
        }

        private int _WriteAddr9()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbPkEn.Checked == true)
                data[0] |= 0x20;
            
            bTmp = 0;
            bTmp |= Convert.ToByte(cbPd.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 9, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPkEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr9() < 0)
                return;
        }

        private void _cbPd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr9() < 0)
                return;
        }

        private int _WriteAddr10()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbEqEn.Checked == true)
                data[0] |= 0x20;
            
            bTmp = 0;
            bTmp |= Convert.ToByte(cbEd.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 10, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbEqEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private void _cbEd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr10() < 0)
                return;
        }

        private int _WriteAddr13()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;
            
            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbBaAll.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 13, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbBaAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr13() < 0)
                return;
        }

        private int _WriteAddr14()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;
            
            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbBmAll.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 14, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbBmAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr14() < 0)
                return;
        }

        private int _WriteAddr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;
            
            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbPkAll.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 15, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPkAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr15() < 0)
                return;
        }

        private int _WriteAddr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbBa1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 16, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbBa1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr16() < 0)
                return;
        }

        private int _WriteAddr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbBm1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 17, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbBm1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr17() < 0)
                return;
        }

        private int _WriteAddr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbPk1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 18, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPk1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr18() < 0)
                return;
        }

        private int _WriteAddr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbBa2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 19, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbBa2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr19() < 0)
                return;
        }

        private int _WriteAddr20()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbBm2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 20, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbBm2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr20() < 0)
                return;
        }

        private int _WriteAddr21()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbPk2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 21, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPk2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr21() < 0)
                return;
        }

        private int _WriteAddr22()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbBa3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 22, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbBa3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr22() < 0)
                return;
        }

        private int _WriteAddr23()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbBm3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 23, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbBm3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr23() < 0)
                return;
        }

        private int _WriteAddr24()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbPk3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 24, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPk3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr24() < 0)
                return;
        }

        private int _WriteAddr25()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbBa4.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 25, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbBa4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr25() < 0)
                return;
        }

        private int _WriteAddr26()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbBm4.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 26, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbBm4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr26() < 0)
                return;
        }

        private int _WriteAddr27()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbPk4.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 27, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPk4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr27() < 0)
                return;
        }

        private int _WriteAddr63()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbPwasAll.Checked == true)
                data[0] |= 0x80;
            
            bTmp = 0;
            bTmp |= Convert.ToByte(cbPwaAll.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            if (cbDPDAll.Checked == true)
                data[0] |= 0x08;

            if (cbSdEnAll.Checked == true)
                data[0] |= 0x04;

            if (cbSqEnAll.Checked == true)
                data[0] |= 0x02;

            if (cbInvAll.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 63, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPwasAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr63() < 0)
                return;
        }

        private void _cbPwaAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr63() < 0)
                return;
        }

        private void _cbDPDAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr63() < 0)
                return;
        }

        private void _cbSdEnAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr63() < 0)
                return;
        }

        private void _cbSqEnAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr63() < 0)
                return;
        }

        private void _cbInvAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr63() < 0)
                return;
        }

        private int _WriteAddr64()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbPwas1.Checked == true)
                data[0] |= 0x80;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbPwa1.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            if (cbDPD1.Checked == true)
                data[0] |= 0x08;

            if (cbSdEn1.Checked == true)
                data[0] |= 0x04;

            if (cbSqEn1.Checked == true)
                data[0] |= 0x02;

            if (cbInv1.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 64, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPwas1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void _cbPwa1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void _cbDPD1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void _cbSdEn1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void _cbSqEn1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void _cbInv1_CheckedChanged(object sender, EventArgs e)
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

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbPwas2.Checked == true)
                data[0] |= 0x80;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbPwa2.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            if (cbDPD2.Checked == true)
                data[0] |= 0x08;

            if (cbSdEn2.Checked == true)
                data[0] |= 0x04;

            if (cbSqEn2.Checked == true)
                data[0] |= 0x02;

            if (cbInv2.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 65, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPwas2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65() < 0)
                return;
        }

        private void _cbPwa2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65() < 0)
                return;
        }

        private void _cbDPD2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65() < 0)
                return;
        }

        private void _cbSdEn2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65() < 0)
                return;
        }

        private void _cbSqEn2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65() < 0)
                return;
        }

        private void _cbInv2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65() < 0)
                return;
        }

        private int _WriteAddr66()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbPwas3.Checked == true)
                data[0] |= 0x80;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbPwa3.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            if (cbDPD3.Checked == true)
                data[0] |= 0x08;

            if (cbSdEn3.Checked == true)
                data[0] |= 0x04;

            if (cbSqEn3.Checked == true)
                data[0] |= 0x02;

            if (cbInv3.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 66, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPwas3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr66() < 0)
                return;
        }

        private void _cbPwa3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr66() < 0)
                return;
        }

        private void _cbDPD3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr66() < 0)
                return;
        }

        private void _cbSdEn3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr66() < 0)
                return;
        }

        private void _cbSqEn3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr66() < 0)
                return;
        }

        private void _cbInv3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr66() < 0)
                return;
        }

        private int _WriteAddr67()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbPwas4.Checked == true)
                data[0] |= 0x80;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbPwa4.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            if (cbDPD4.Checked == true)
                data[0] |= 0x08;

            if (cbSdEn4.Checked == true)
                data[0] |= 0x04;

            if (cbSqEn4.Checked == true)
                data[0] |= 0x02;

            if (cbInv4.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 67, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPwas4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr67() < 0)
                return;
        }

        private void _cbPwa4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr67() < 0)
                return;
        }

        private void _cbDPD4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr67() < 0)
                return;
        }

        private void _cbSdEn4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr67() < 0)
                return;
        }

        private void _cbSqEn4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr67() < 0)
                return;
        }

        private void _cbInv4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr67() < 0)
                return;
        }

        private int _WriteAddr79()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbLSdAll.Checked == true)
                data[0] |= 0x08;

            if (cbLLosAll.Checked == true)
                data[0] |= 0x04;

            if (cbLVvhAll.Checked == true)
                data[0] |= 0x02;

            if (cbLVvlAll.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 79, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbLSdAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr79() < 0)
                return;
        }

        private void _cbLLosAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr79() < 0)
                return;
        }

        private void _cbLVvhAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr79() < 0)
                return;
        }

        private void _cbLVvlAll_CheckedChanged(object sender, EventArgs e)
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

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbLSd1.Checked == true)
                data[0] |= 0x08;

            if (cbLLos1.Checked == true)
                data[0] |= 0x04;

            if (cbLVvh1.Checked == true)
                data[0] |= 0x02;

            if (cbLVvl1.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 80, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbLSd1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr80() < 0)
                return;
        }

        private void _cbLLos1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr80() < 0)
                return;
        }

        private void _cbLVvh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr80() < 0)
                return;
        }

        private void _cbLVvl1_CheckedChanged(object sender, EventArgs e)
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

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbLSd2.Checked == true)
                data[0] |= 0x08;

            if (cbLLos2.Checked == true)
                data[0] |= 0x04;

            if (cbLVvh2.Checked == true)
                data[0] |= 0x02;

            if (cbLVvl2.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 81, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbLSd2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr81() < 0)
                return;
        }

        private void _cbLLos2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr81() < 0)
                return;
        }

        private void _cbLVvh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr81() < 0)
                return;
        }

        private void _cbLVvl2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr81() < 0)
                return;
        }

        private int _WriteAddr82()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbLSd3.Checked == true)
                data[0] |= 0x08;

            if (cbLLos3.Checked == true)
                data[0] |= 0x04;

            if (cbLVvh3.Checked == true)
                data[0] |= 0x02;

            if (cbLVvl3.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 82, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbLSd3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr82() < 0)
                return;
        }

        private void _cbLLos3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr82() < 0)
                return;
        }

        private void _cbLVvh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr82() < 0)
                return;
        }

        private void _cbLVvl3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr82() < 0)
                return;
        }

        private int _WriteAddr83()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbLSd4.Checked == true)
                data[0] |= 0x08;

            if (cbLLos4.Checked == true)
                data[0] |= 0x04;

            if (cbLVvh4.Checked == true)
                data[0] |= 0x02;

            if (cbLVvl4.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 83, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbLSd4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr83() < 0)
                return;
        }

        private void _cbLLos4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr83() < 0)
                return;
        }

        private void _cbLVvh4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr83() < 0)
                return;
        }

        private void _cbLVvl4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr83() < 0)
                return;
        }

        private int _WriteAddr95()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbSdMAll.Checked == true)
                data[0] |= 0x80;

            if (cbLosMAll.Checked == true)
                data[0] |= 0x40;

            if (cbVvhMAll.Checked == true)
                data[0] |= 0x20;

            if (cbVvlMAll.Checked == true)
                data[0] |= 0x10;

            if (cbLSdMAll.Checked == true)
                data[0] |= 0x08;

            if (cbLLosMAll.Checked == true)
                data[0] |= 0x04;

            if (cbLVvhMAll.Checked == true)
                data[0] |= 0x02;

            if (cbLVvlMAll.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 95, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbSdMAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr95() < 0)
                return;
        }

        private void _cbLosMAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr95() < 0)
                return;
        }

        private void _cbVvhMAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr95() < 0)
                return;
        }

        private void _cbVvlMAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr95() < 0)
                return;
        }

        private void _cbLSdMAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr95() < 0)
                return;
        }

        private void _cbLLosMAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr95() < 0)
                return;
        }

        private void _cbLVvhMAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr95() < 0)
                return;
        }

        private void _cbLVvlMAll_CheckedChanged(object sender, EventArgs e)
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

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbSdM1.Checked == true)
                data[0] |= 0x80;

            if (cbLosM1.Checked == true)
                data[0] |= 0x40;

            if (cbVvhM1.Checked == true)
                data[0] |= 0x20;

            if (cbVvlM1.Checked == true)
                data[0] |= 0x10;

            if (cbLSdM1.Checked == true)
                data[0] |= 0x08;

            if (cbLLosM1.Checked == true)
                data[0] |= 0x04;

            if (cbLVvhM1.Checked == true)
                data[0] |= 0x02;

            if (cbLVvlM1.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 96, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbSdM1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void _cbLosM1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void _cbVvhM1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void _cbVvlM1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void _cbLSdM1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void _cbLLosM1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void _cbLVvhM1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr96() < 0)
                return;
        }

        private void _cbLVvlM1_CheckedChanged(object sender, EventArgs e)
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

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbSdM2.Checked == true)
                data[0] |= 0x80;

            if (cbLosM2.Checked == true)
                data[0] |= 0x40;

            if (cbVvhM2.Checked == true)
                data[0] |= 0x20;

            if (cbVvlM2.Checked == true)
                data[0] |= 0x10;

            if (cbLSdM2.Checked == true)
                data[0] |= 0x08;

            if (cbLLosM2.Checked == true)
                data[0] |= 0x04;

            if (cbLVvhM2.Checked == true)
                data[0] |= 0x02;

            if (cbLVvlM2.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 97, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbSdM2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr97() < 0)
                return;
        }

        private void _cbLosM2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr97() < 0)
                return;
        }

        private void _cbVvhM2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr97() < 0)
                return;
        }

        private void _cbVvlM2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr97() < 0)
                return;
        }

        private void _cbLSdM2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr97() < 0)
                return;
        }

        private void _cbLLosM2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr97() < 0)
                return;
        }

        private void _cbLVvhM2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr97() < 0)
                return;
        }

        private void _cbLVvlM2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr97() < 0)
                return;
        }

        private int _WriteAddr98()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbSdM3.Checked == true)
                data[0] |= 0x80;

            if (cbLosM3.Checked == true)
                data[0] |= 0x40;

            if (cbVvhM3.Checked == true)
                data[0] |= 0x20;

            if (cbVvlM3.Checked == true)
                data[0] |= 0x10;

            if (cbLSdM3.Checked == true)
                data[0] |= 0x08;

            if (cbLLosM3.Checked == true)
                data[0] |= 0x04;

            if (cbLVvhM3.Checked == true)
                data[0] |= 0x02;

            if (cbLVvlM3.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 98, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbSdM3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr98() < 0)
                return;
        }

        private void _cbLosM3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr98() < 0)
                return;
        }

        private void _cbVvhM3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr98() < 0)
                return;
        }

        private void _cbVvlM3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr98() < 0)
                return;
        }

        private void _cbLSdM3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr98() < 0)
                return;
        }

        private void _cbLLosM3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr98() < 0)
                return;
        }

        private void _cbLVvhM3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr98() < 0)
                return;
        }

        private void _cbLVvlM3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr98() < 0)
                return;
        }

        private int _WriteAddr99()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbSdM4.Checked == true)
                data[0] |= 0x80;

            if (cbLosM4.Checked == true)
                data[0] |= 0x40;

            if (cbVvhM4.Checked == true)
                data[0] |= 0x20;

            if (cbVvlM4.Checked == true)
                data[0] |= 0x10;

            if (cbLSdM4.Checked == true)
                data[0] |= 0x08;

            if (cbLLosM4.Checked == true)
                data[0] |= 0x04;

            if (cbLVvhM4.Checked == true)
                data[0] |= 0x02;

            if (cbLVvlM4.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 99, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbSdM4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr99() < 0)
                return;
        }

        private void _cbLosM4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr99() < 0)
                return;
        }

        private void _cbVvhM4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr99() < 0)
                return;
        }

        private void _cbVvlM4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr99() < 0)
                return;
        }

        private void _cbLSdM4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr99() < 0)
                return;
        }

        private void _cbLLosM4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr99() < 0)
                return;
        }

        private void _cbLVvhM4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr99() < 0)
                return;
        }

        private void _cbLVvlM4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr99() < 0)
                return;
        }

        private int _WriteAddr111()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbFlthAll.Checked == true)
                data[0] |= 0x08;

            if (cbFltlAll.Checked == true)
                data[0] |= 0x04;

            if (cbLFlthAll.Checked == true)
                data[0] |= 0x02;

            if (cbLFltlAll.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 111, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbFlthAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr111() < 0)
                return;
        }

        private void _cbFltlAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr111() < 0)
                return;
        }

        private void _cbLFlthAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr111() < 0)
                return;
        }

        private void _cbLFltlAll_CheckedChanged(object sender, EventArgs e)
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

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbFlth1.Checked == true)
                data[0] |= 0x08;

            if (cbFltl1.Checked == true)
                data[0] |= 0x04;

            if (cbLFlth1.Checked == true)
                data[0] |= 0x02;

            if (cbLFltl1.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 112, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbFlth1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr112() < 0)
                return;
        }

        private void _cbFltl1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr112() < 0)
                return;
        }

        private void _cbLFlth1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr112() < 0)
                return;
        }

        private void _cbLFltl1_CheckedChanged(object sender, EventArgs e)
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

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbFlth2.Checked == true)
                data[0] |= 0x08;

            if (cbFltl2.Checked == true)
                data[0] |= 0x04;

            if (cbLFlth2.Checked == true)
                data[0] |= 0x02;

            if (cbLFltl2.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 113, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbFlth2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr113() < 0)
                return;
        }

        private void _cbFltl2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr113() < 0)
                return;
        }

        private void _cbLFlth2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr113() < 0)
                return;
        }

        private void _cbLFltl2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr113() < 0)
                return;
        }

        private int _WriteAddr114()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbFlth3.Checked == true)
                data[0] |= 0x08;

            if (cbFltl3.Checked == true)
                data[0] |= 0x04;

            if (cbLFlth3.Checked == true)
                data[0] |= 0x02;

            if (cbLFltl3.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 114, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbFlth3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr114() < 0)
                return;
        }

        private void _cbFltl3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr114() < 0)
                return;
        }

        private void _cbLFlth3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr114() < 0)
                return;
        }

        private void _cbLFltl3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr114() < 0)
                return;
        }

        private int _WriteAddr115()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbFlth4.Checked == true)
                data[0] |= 0x08;

            if (cbFltl4.Checked == true)
                data[0] |= 0x04;

            if (cbLFlth4.Checked == true)
                data[0] |= 0x02;

            if (cbLFltl4.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 115, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbFlth4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr115() < 0)
                return;
        }

        private void _cbFltl4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr115() < 0)
                return;
        }

        private void _cbLFlth4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr115() < 0)
                return;
        }

        private void _cbLFltl4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr115() < 0)
                return;
        }

        private int _WriteAddr127()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbFlthMAll.Checked == true)
                data[0] |= 0x08;

            if (cbFltlMAll.Checked == true)
                data[0] |= 0x04;

            if (cbLFlthMAll.Checked == true)
                data[0] |= 0x02;

            if (cbLFltlMAll.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 127, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbFlthMAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr127() < 0)
                return;
        }

        private void _cbFltlMAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr127() < 0)
                return;
        }

        private void _cbLFlthMAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr127() < 0)
                return;
        }

        private void _cbLFltlMAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr127() < 0)
                return;
        }

        private int _WriteAddr128()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbFlthM1.Checked == true)
                data[0] |= 0x08;

            if (cbFltlM1.Checked == true)
                data[0] |= 0x04;

            if (cbLFlthM1.Checked == true)
                data[0] |= 0x02;

            if (cbLFltlM1.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 128, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbFlthM1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr128() < 0)
                return;
        }

        private void _cbFltlM1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr128() < 0)
                return;
        }

        private void _cbLFlthM1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr128() < 0)
                return;
        }

        private void _cbLFltlM1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr128() < 0)
                return;
        }

        private int _WriteAddr129()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbFlthM2.Checked == true)
                data[0] |= 0x08;

            if (cbFltlM2.Checked == true)
                data[0] |= 0x04;

            if (cbLFlthM2.Checked == true)
                data[0] |= 0x02;

            if (cbLFltlM2.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 129, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbFlthM2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr129() < 0)
                return;
        }

        private void _cbFltlM2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr129() < 0)
                return;
        }

        private void _cbLFlthM2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr129() < 0)
                return;
        }

        private void _cbLFltlM2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr129() < 0)
                return;
        }

        private int _WriteAddr130()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbFlthM3.Checked == true)
                data[0] |= 0x08;

            if (cbFltlM3.Checked == true)
                data[0] |= 0x04;

            if (cbLFlthM3.Checked == true)
                data[0] |= 0x02;

            if (cbLFltlM3.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 130, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbFlthM3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr130() < 0)
                return;
        }

        private void _cbFltlM3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr130() < 0)
                return;
        }

        private void _cbLFlthM3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr130() < 0)
                return;
        }

        private void _cbLFltlM3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr130() < 0)
                return;
        }

        private int _WriteAddr131()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbFlthM4.Checked == true)
                data[0] |= 0x08;

            if (cbFltlM4.Checked == true)
                data[0] |= 0x04;

            if (cbLFlthM4.Checked == true)
                data[0] |= 0x02;

            if (cbLFltlM4.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 131, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbFlthM4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr131() < 0)
                return;
        }

        private void _cbFltlM4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr131() < 0)
                return;
        }

        private void _cbLFlthM4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr131() < 0)
                return;
        }

        private void _cbLFltlM4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr131() < 0)
                return;
        }

        private int _WriteAddr143()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbEqDly1EnAll.Checked == true)
                data[0] |= 0x20;
            
            bTmp = 0;
            bTmp |= Convert.ToByte(cbEq1AAll.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 143, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbEqDly1EnAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr143() < 0)
                return;
        }

        private void _cbEq1AAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr143() < 0)
                return;
        }

        private int _WriteAddr144()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbEqDly1En1.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbEq1A1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 144, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbEqDly1En1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr144() < 0)
                return;
        }

        private void _cbEq1A1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr144() < 0)
                return;
        }

        private int _WriteAddr145()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbEqDly1En2.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbEq1A2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 145, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbEqDly1En2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr145() < 0)
                return;
        }

        private void cbEq1A2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr145() < 0)
                return;
        }

        private int _WriteAddr146()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbEqDly1En3.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbEq1A3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 146, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbEqDly1En3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr146() < 0)
                return;
        }

        private void _cbEq1A3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr146() < 0)
                return;
        }

        private int _WriteAddr147()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbEqDly1En4.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbEq1A4.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 147, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbEqDly1En4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr147() < 0)
                return;
        }

        private void _cbEq1A4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr147() < 0)
                return;
        }

        private int _WriteAddr159()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbEqDly2EnAll.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbEq2AAll.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 159, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbEqDly2EnAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr159() < 0)
                return;
        }

        private void _cbEq2AAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr159() < 0)
                return;
        }

        private int _WriteAddr160()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbEqDly2En1.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbEq2A1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 160, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbEqDly2En1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr160() < 0)
                return;
        }

        private void _cbEq2A1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr160() < 0)
                return;
        }

        private int _WriteAddr161()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbEqDly2En2.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbEq2A2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 161, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbEqDly2En2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr161() < 0)
                return;
        }

        private void _cbEq2A2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr161() < 0)
                return;
        }

        private int _WriteAddr162()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbEqDly2En3.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbEq2A3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 162, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbEqDly2En3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr162() < 0)
                return;
        }

        private void _cbEq2A3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr162() < 0)
                return;
        }

        private int _WriteAddr163()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbEqDly2En4.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbEq2A4.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 163, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbEqDly2En4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr163() < 0)
                return;
        }

        private void _cbEq2A4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr163() < 0)
                return;
        }

        private int _WriteAddr175()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbEqDly3EnAll.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbEq3AAll.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 175, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbEqDly3EnAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr175() < 0)
                return;
        }

        private void _cbEq3AAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr175() < 0)
                return;
        }

        private int _WriteAddr176()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbEqDly3En1.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbEq3A1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 176, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbEqDly3En1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr176() < 0)
                return;
        }

        private void _cbEq3A1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr176() < 0)
                return;
        }

        private int _WriteAddr177()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbEqDly3En2.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbEq3A2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 177, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbEqDly3En2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr177() < 0)
                return;
        }

        private void _cbEq3A2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr177() < 0)
                return;
        }

        private int _WriteAddr178()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbEqDly3En3.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbEq3A3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 178, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbEqDly3En3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr178() < 0)
                return;
        }

        private void _cbEq3A3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr178() < 0)
                return;
        }

        private int _WriteAddr179()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbEqDly3En4.Checked == true)
                data[0] |= 0x20;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbEq3A4.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(107, 179, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbEqDly3En4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr179() < 0)
                return;
        }

        private void _cbEq3A4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr179() < 0)
                return;
        }

        private int _WriteAddr241()
        {
            byte[] data = new byte[1];
            int rv;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;

            if (cbNotInt4.Checked == true)
                data[0] |= 0x08;

            if (cbNotInt3.Checked == true)
                data[0] |= 0x04;

            if (cbNotInt2.Checked == true)
                data[0] |= 0x02;

            if (cbNotInt1.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(107, 241, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbNotInt4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr241() < 0)
                return;
        }

        private void _cbNotInt3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr241() < 0)
                return;
        }

        private void _cbNotInt2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr241() < 0)
                return;
        }

        private void _cbNotInt1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr241() < 0)
                return;
        }

        private void bWrite_Click(object sender, EventArgs e)
        {
            if (_WriteAddr0() < 0)
                return;
            if (_WriteAddr1() < 0)
                return;
            if (_WriteAddr2() < 0)
                return;
            if (_WriteAddr3() < 0)
                return;
            if (_WriteAddr4() < 0)
                return;
            if (_WriteAddr6() < 0)
                return;
            if (_WriteAddr7() < 0)
                return;
            if (_WriteAddr8() < 0)
                return;
            if (_WriteAddr9() < 0)
                return;
            if (_WriteAddr10() < 0)
                return;
            if (_WriteAddr13() < 0)
                return;
            if (_WriteAddr14() < 0)
                return;
            if (_WriteAddr15() < 0)
                return;
            if (_WriteAddr16() < 0)
                return;
            if (_WriteAddr17() < 0)
                return;
            if (_WriteAddr18() < 0)
                return;
            if (_WriteAddr19() < 0)
                return;
            if (_WriteAddr20() < 0)
                return;
            if (_WriteAddr21() < 0)
                return;
            if (_WriteAddr22() < 0)
                return;
            if (_WriteAddr23() < 0)
                return;
            if (_WriteAddr24() < 0)
                return;
            if (_WriteAddr25() < 0)
                return;
            if (_WriteAddr26() < 0)
                return;
            if (_WriteAddr27() < 0)
                return;
            if (_WriteAddr63() < 0)
                return;
            if (_WriteAddr64() < 0)
                return;
            if (_WriteAddr65() < 0)
                return;
            if (_WriteAddr66() < 0)
                return;
            if (_WriteAddr67() < 0)
                return;
            if (_WriteAddr79() < 0)
                return;
            if (_WriteAddr80() < 0)
                return;
            if (_WriteAddr81() < 0)
                return;
            if (_WriteAddr82() < 0)
                return;
            if (_WriteAddr83() < 0)
                return;
            if (_WriteAddr95() < 0)
                return;
            if (_WriteAddr96() < 0)
                return;
            if (_WriteAddr97() < 0)
                return;
            if (_WriteAddr98() < 0)
                return;
            if (_WriteAddr99() < 0)
                return;
            if (_WriteAddr111() < 0)
                return;
            if (_WriteAddr112() < 0)
                return;
            if (_WriteAddr113() < 0)
                return;
            if (_WriteAddr114() < 0)
                return;
            if (_WriteAddr115() < 0)
                return;
            if (_WriteAddr127() < 0)
                return;
            if (_WriteAddr128() < 0)
                return;
            if (_WriteAddr129() < 0)
                return;
            if (_WriteAddr130() < 0)
                return;
            if (_WriteAddr131() < 0)
                return;
            if (_WriteAddr143() < 0)
                return;
            if (_WriteAddr144() < 0)
                return;
            if (_WriteAddr145() < 0)
                return;
            if (_WriteAddr146() < 0)
                return;
            if (_WriteAddr147() < 0)
                return;
            if (_WriteAddr159() < 0)
                return;
            if (_WriteAddr160() < 0)
                return;
            if (_WriteAddr161() < 0)
                return;
            if (_WriteAddr162() < 0)
                return;
            if (_WriteAddr163() < 0)
                return;
            if (_WriteAddr175() < 0)
                return;
            if (_WriteAddr176() < 0)
                return;
            if (_WriteAddr177() < 0)
                return;
            if (_WriteAddr178() < 0)
                return;
            if (_WriteAddr179() < 0)
                return;
            if (_WriteAddr241() < 0)
                return;
        }
    }
}
