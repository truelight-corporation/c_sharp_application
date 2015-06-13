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
    public partial class UcHxr6104aConfig : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);

        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private bool reading = false;

        public UcHxr6104aConfig()
        {
            int i;

            InitializeComponent();

            for (i = 0; i < 4; i++) {
                cbPp.Items.Add(i);
                cbSerRes.Items.Add(i);
                cbGain.Items.Add(i);
                cbDegOut.Items.Add(i);
                cbEfOutDiff.Items.Add(i);
            }

            for (i = 0; i < 8; i++) {
                cbSdth.Items.Add(i);
                cbPedur.Items.Add(i);
                cbAgcth.Items.Add(i);
                cbEfChan.Items.Add(i);
                cbSwingSize.Items.Add(i);
                cbTiaBw.Items.Add(i);
            }

            for (i = 0; i < 16; i++) {
                cbPEAmp1.Items.Add(i);
                cbPEAmp2.Items.Add(i);
                cbPEAmp3.Items.Add(i);
                cbPEAmp4.Items.Add(i);
                cbPEAmpAll.Items.Add(i);
            }

            for (i = 0; i < 64; i++) {
                cbOff1.Items.Add(i);
                cbOff2.Items.Add(i);
                cbOff3.Items.Add(i);
                cbOff4.Items.Add(i);
                cbOffAll.Items.Add(i);
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

        private void _ParseAddr0(byte data)
        {
            int iTmp;

            iTmp = (data & 0xE0) >> 5;
            cbSdth.SelectedIndex = iTmp;

            if ((data & 0x10) == 0)
                cbAmp2LowOp.Checked = false;
            else
                cbAmp2LowOp.Checked = true;

            if ((data & 0x08) == 0)
                cbAmp4LowOp.Checked = false;
            else
                cbAmp4LowOp.Checked = true;

            if ((data & 0x04) == 0)
                cbPushPullEn.Checked = false;
            else
                cbPushPullEn.Checked = true;

            if ((data & 0x02) == 0)
                cbEfSwing.Checked = false;
            else
                cbEfSwing.Checked = true;
        }

        private void _ParseAddr1(byte data)
        {
            int iTmp;

            iTmp = (data & 0xE0) >> 5;
            cbPedur.SelectedIndex = iTmp;

            if ((data & 0x10) == 0)
                cbPeen.Checked = false;
            else
                cbPeen.Checked = true;

            iTmp = (data & 0x0C) >> 2;
            cbPp.SelectedIndex = iTmp;

            if ((data & 0x01) == 0)
                cbIthermRssi.Checked = false;
            else
                cbIthermRssi.Checked = true;
        }

        private void _ParseAddr2(byte data)
        {
            int iTmp;

            iTmp = (data & 0xE0) >> 5;
            cbAgcth.SelectedIndex = iTmp;

            if ((data & 0x10) == 0)
                cbAgcEn.Checked = false;
            else
                cbAgcEn.Checked = true;

            if ((data & 0x08) == 0)
                cbGdPd.Checked = false;
            else
                cbGdPd.Checked = true;

            if ((data & 0x04) == 0)
                cbRegLoop.Checked = false;
            else
                cbRegLoop.Checked = true;

            if ((data & 0x02) == 0)
                cbSdRectEn.Checked = false;
            else
                cbSdRectEn.Checked = true;

            if ((data & 0x01) == 0)
                cbSmartI2c.Checked = false;
            else
                cbSmartI2c.Checked = true;
        }

        private void _ParseAddr3(byte data)
        {
            int iTmp;

            if ((data & 0x80) == 0)
                cbIntInv.Checked = false;
            else
                cbIntInv.Checked = true;

            iTmp = (data & 0x60) >> 5;
            cbSerRes.SelectedIndex = iTmp;

            if ((data & 0x10) == 0)
                cbHighLC.Checked = false;
            else
                cbHighLC.Checked = true;
            
            if ((data & 0x08) == 0)
                cbAutoIncrement.Checked = false;
            else
                cbAutoIncrement.Checked = true;
            
            if ((data & 0x04) == 0)
                cbNotTC.Checked = false;
            else
                cbNotTC.Checked = true;
            
            if ((data & 0x02) == 0)
                cbTestD.Checked = false;
            else
                cbTestD.Checked = true;
            
            if ((data & 0x01) == 0)
                cbDisTiaOver.Checked = false;
            else
                cbDisTiaOver.Checked = true;
        }

        private void _ParseAddr4(byte data)
        {
            int iTmp;
            
            iTmp = (data & 0xC0) >> 6;
            cbGain.SelectedIndex = iTmp;

            iTmp = (data & 0x38) >> 3;
            cbEfChan.SelectedIndex = iTmp;

            iTmp = data & 0x07;
            cbSwingSize.SelectedIndex = iTmp;
        }

        private void _ParseAddr5(byte data)
        {
            int iTmp;
            
            if ((data & 0x80) == 0)
                cbGain4A.Checked = false;
            else
                cbGain4A.Checked = true;
            
            iTmp = (data & 0x60) >> 5;
            cbDegOut.SelectedIndex = iTmp;

            iTmp = (data & 0x1C) >> 2;
            cbTiaBw.SelectedIndex = iTmp;

            iTmp = data & 0x03;
            cbEfOutDiff.SelectedIndex = iTmp;
        }

        private void _ParseAddr7(byte data)
        {            
            if ((data & 0x04) == 0)
                cbConf2.Checked = false;
            else
                cbConf2.Checked = true;

            if ((data & 0x02) == 0)
                cbConf1.Checked = false;
            else
                cbConf1.Checked = true;
            
            if ((data & 0x01) == 0)
                cbGNotInt.Checked = false;
            else
                cbGNotInt.Checked = true;
        }

        private void _ParseAddr63(byte data)
        {
            int iTmp;
            
            iTmp = (data & 0xF0) >> 4;
            cbPEAmpAll.SelectedIndex = iTmp;

            if ((data & 0x08) == 0)
                cbOutputENAll.Checked = false;
            else
                cbOutputENAll.Checked = true;
            
            if ((data & 0x04) == 0)
                cbRssiAll.Checked = false;
            else
                cbRssiAll.Checked = true;
            
            if ((data & 0x02) == 0)
                cbDPDAll.Checked = false;
            else
                cbDPDAll.Checked = true;
            
            if ((data & 0x01) == 0)
                cbInvAll.Checked = false;
            else
                cbInvAll.Checked = true;
        }

        private void _ParseAddr64(byte data)
        {
            int iTmp;

            iTmp = (data & 0xF0) >> 4;
            cbPEAmp1.SelectedIndex = iTmp;

            if ((data & 0x08) == 0)
                cbOutputEN1.Checked = false;
            else
                cbOutputEN1.Checked = true;

            if ((data & 0x04) == 0)
                cbRssi1.Checked = false;
            else
                cbRssi1.Checked = true;

            if ((data & 0x02) == 0)
                cbDPD1.Checked = false;
            else
                cbDPD1.Checked = true;

            if ((data & 0x01) == 0)
                cbInv1.Checked = false;
            else
                cbInv1.Checked = true;
        }

        private void _ParseAddr65(byte data)
        {
            int iTmp;

            iTmp = (data & 0xF0) >> 4;
            cbPEAmp2.SelectedIndex = iTmp;

            if ((data & 0x08) == 0)
                cbOutputEN2.Checked = false;
            else
                cbOutputEN2.Checked = true;

            if ((data & 0x04) == 0)
                cbRssi2.Checked = false;
            else
                cbRssi2.Checked = true;

            if ((data & 0x02) == 0)
                cbDPD2.Checked = false;
            else
                cbDPD2.Checked = true;

            if ((data & 0x01) == 0)
                cbInv2.Checked = false;
            else
                cbInv2.Checked = true;
        }

        private void _ParseAddr66(byte data)
        {
            int iTmp;

            iTmp = (data & 0xF0) >> 4;
            cbPEAmp3.SelectedIndex = iTmp;

            if ((data & 0x08) == 0)
                cbOutputEN3.Checked = false;
            else
                cbOutputEN3.Checked = true;

            if ((data & 0x04) == 0)
                cbRssi3.Checked = false;
            else
                cbRssi3.Checked = true;

            if ((data & 0x02) == 0)
                cbDPD3.Checked = false;
            else
                cbDPD3.Checked = true;

            if ((data & 0x01) == 0)
                cbInv3.Checked = false;
            else
                cbInv3.Checked = true;
        }

        private void _ParseAddr67(byte data)
        {
            int iTmp;

            iTmp = (data & 0xF0) >> 4;
            cbPEAmp4.SelectedIndex = iTmp;

            if ((data & 0x08) == 0)
                cbOutputEN4.Checked = false;
            else
                cbOutputEN4.Checked = true;

            if ((data & 0x04) == 0)
                cbRssi4.Checked = false;
            else
                cbRssi4.Checked = true;

            if ((data & 0x02) == 0)
                cbDPD4.Checked = false;
            else
                cbDPD4.Checked = true;

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
            
            if ((data & 0x08) == 0)
                cbLSdAll.Checked = false;
            else
                cbLSdAll.Checked = true;
            
            if ((data & 0x04) == 0)
                cbLLosAll.Checked = false;
            else
                cbLLosAll.Checked = true;
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

            if ((data & 0x08) == 0)
                cbLSd1.Checked = false;
            else
                cbLSd1.Checked = true;

            if ((data & 0x04) == 0)
                cbLLos1.Checked = false;
            else
                cbLLos1.Checked = true;
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

            if ((data & 0x08) == 0)
                cbLSd2.Checked = false;
            else
                cbLSd2.Checked = true;

            if ((data & 0x04) == 0)
                cbLLos2.Checked = false;
            else
                cbLLos2.Checked = true;
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

            if ((data & 0x08) == 0)
                cbLSd3.Checked = false;
            else
                cbLSd3.Checked = true;

            if ((data & 0x04) == 0)
                cbLLos3.Checked = false;
            else
                cbLLos3.Checked = true;
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

            if ((data & 0x08) == 0)
                cbLSd4.Checked = false;
            else
                cbLSd4.Checked = true;

            if ((data & 0x04) == 0)
                cbLLos4.Checked = false;
            else
                cbLLos4.Checked = true;
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
            
            if ((data & 0x08) == 0)
                cbLSdMAll.Checked = false;
            else
                cbLSdMAll.Checked = true;
            
            if ((data & 0x04) == 0)
                cbLLosMAll.Checked = false;
            else
                cbLLosMAll.Checked = true;
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

            if ((data & 0x08) == 0)
                cbLSdM1.Checked = false;
            else
                cbLSdM1.Checked = true;

            if ((data & 0x04) == 0)
                cbLLosM1.Checked = false;
            else
                cbLLosM1.Checked = true;
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

            if ((data & 0x08) == 0)
                cbLSdM2.Checked = false;
            else
                cbLSdM2.Checked = true;

            if ((data & 0x04) == 0)
                cbLLosM2.Checked = false;
            else
                cbLLosM2.Checked = true;
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

            if ((data & 0x08) == 0)
                cbLSdM3.Checked = false;
            else
                cbLSdM3.Checked = true;

            if ((data & 0x04) == 0)
                cbLLosM3.Checked = false;
            else
                cbLLosM3.Checked = true;
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

            if ((data & 0x08) == 0)
                cbLSdM4.Checked = false;
            else
                cbLSdM4.Checked = true;

            if ((data & 0x04) == 0)
                cbLLosM4.Checked = false;
            else
                cbLLosM4.Checked = true;
        }

        private void _ParseAddr143(byte data)
        {
            int iTmp;

            if ((data & 0x40) == 0)
                cbSignumAll.Checked = false;
            else
                cbSignumAll.Checked = true;

            iTmp = data & 0x3F;
            cbOffAll.SelectedIndex = iTmp;
        }

        private void _ParseAddr144(byte data)
        {
            int iTmp;

            if ((data & 0x40) == 0)
                cbSignum1.Checked = false;
            else
                cbSignum1.Checked = true;

            iTmp = data & 0x3F;
            cbOff1.SelectedIndex = iTmp;
        }

        private void _ParseAddr145(byte data)
        {
            int iTmp;

            if ((data & 0x40) == 0)
                cbSignum2.Checked = false;
            else
                cbSignum2.Checked = true;

            iTmp = data & 0x3F;
            cbOff2.SelectedIndex = iTmp;
        }

        private void _ParseAddr146(byte data)
        {
            int iTmp;

            if ((data & 0x40) == 0)
                cbSignum3.Checked = false;
            else
                cbSignum3.Checked = true;

            iTmp = data & 0x3F;
            cbOff3.SelectedIndex = iTmp;
        }

        private void _ParseAddr147(byte data)
        {
            int iTmp;

            if ((data & 0x40) == 0)
                cbSignum4.Checked = false;
            else
                cbSignum4.Checked = true;

            iTmp = data & 0x3F;
            cbOff4.SelectedIndex = iTmp;
        }

        private void _ParseAddr159(byte data)
        {
            if ((data & 0x01) == 0)
                cbSQENAll.Checked = false;
            else
                cbSQENAll.Checked = true;
        }

        private void _ParseAddr160(byte data)
        {
            if ((data & 0x01) == 0)
                cbSQEN1.Checked = false;
            else
                cbSQEN1.Checked = true;
        }

        private void _ParseAddr161(byte data)
        {
            if ((data & 0x01) == 0)
                cbSQEN2.Checked = false;
            else
                cbSQEN2.Checked = true;
        }

        private void _ParseAddr162(byte data)
        {
            if ((data & 0x01) == 0)
                cbSQEN3.Checked = false;
            else
                cbSQEN3.Checked = true;
        }

        private void _ParseAddr163(byte data)
        {
            if ((data & 0x01) == 0)
                cbSQEN4.Checked = false;
            else
                cbSQEN4.Checked = true;
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
                cbNotGSd.Checked = false;
            else
                cbNotGSd.Checked = true;
            
            if ((data & 0x04) == 0)
                cbNotGLos.Checked = false;
            else
                cbNotGLos.Checked = true;
            
            if ((data & 0x02) == 0)
                cbNotGLSd.Checked = false;
            else
                cbNotGLSd.Checked = true;
            
            if ((data & 0x01) == 0)
                cbNotGLLos.Checked = false;
            else
                cbNotGLLos.Checked = true;
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

        private void _bRead_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[10];
            int rv;

            reading = true;

            if (i2cReadCB == null)
                goto exit;

            if (_SetQsfpMode(0x4C) < 0)
                goto exit;

            rv = i2cReadCB(76, 0, 6, data);
            if (rv != 6)
                goto exit;

            _ParseAddr0(data[0]);
            _ParseAddr1(data[1]);
            _ParseAddr2(data[2]);
            _ParseAddr3(data[3]);
            _ParseAddr4(data[4]);
            _ParseAddr5(data[5]);

            rv = i2cReadCB(76, 7, 1, data);
            if (rv != 1)
                goto exit;

            _ParseAddr7(data[0]);

            rv = i2cReadCB(76, 63, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr63(data[0]);
            _ParseAddr64(data[1]);
            _ParseAddr65(data[2]);
            _ParseAddr66(data[3]);
            _ParseAddr67(data[4]);

            rv = i2cReadCB(76, 79, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr79(data[0]);
            _ParseAddr80(data[1]);
            _ParseAddr81(data[2]);
            _ParseAddr82(data[3]);
            _ParseAddr83(data[4]);

            rv = i2cReadCB(76, 95, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr95(data[0]);
            _ParseAddr96(data[1]);
            _ParseAddr97(data[2]);
            _ParseAddr98(data[3]);
            _ParseAddr99(data[4]);

            rv = i2cReadCB(76, 143, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr143(data[0]);
            _ParseAddr144(data[1]);
            _ParseAddr145(data[2]);
            _ParseAddr146(data[3]);
            _ParseAddr147(data[4]);


            rv = i2cReadCB(76, 159, 5, data);
            if (rv != 5)
                goto exit;

            _ParseAddr159(data[0]);
            _ParseAddr160(data[1]);
            _ParseAddr161(data[2]);
            _ParseAddr162(data[3]);
            _ParseAddr163(data[4]);

            rv = i2cReadCB(76, 224, 2, data);
            if (rv != 2)
                goto exit;

            tbDeviceId.Text = data[0].ToString();
            tbRevisionId.Text = data[1].ToString();

            rv = i2cReadCB(76, 241, 2, data);
            if (rv != 2)
                goto exit;

            _ParseAddr241(data[0]);
            _ParseAddr242(data[1]);

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
            bTmp |= Convert.ToByte(cbSdth.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            if (cbAmp2LowOp.Checked == true)
                data[0] |= 0x10;

            if (cbAmp4LowOp.Checked == true)
                data[0] |= 0x08;

            if (cbPushPullEn.Checked == true)
                data[0] |= 0x04;

            if (cbEfSwing.Checked == true)
                data[0] |= 0x02;

            rv = i2cWriteCB(76, 0, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbSdth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0() < 0)
                return;
        }

        private void _cbAmp2LowOp_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0() < 0)
                return;
        }

        private void _cbAmp4LowOp_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0() < 0)
                return;
        }

        private void _cbPushPullEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr0() < 0)
                return;
        }

        private void _cbEfSwing_CheckedChanged(object sender, EventArgs e)
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
            bTmp |= Convert.ToByte(cbPedur.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            if (cbPeen.Checked == true)
                data[0] |= 0x10;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbPp.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            if (cbIthermRssi.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(76, 1, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPedur_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1() < 0)
                return;
        }

        private void _cbPeen_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1() < 0)
                return;
        }

        private void _cbPp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr1() < 0)
                return;
        }

        private void _cbIthermRssi_CheckedChanged(object sender, EventArgs e)
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
            bTmp |= Convert.ToByte(cbAgcth.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            if (cbAgcEn.Checked == true)
                data[0] |= 0x10;

            if (cbGdPd.Checked == true)
                data[0] |= 0x08;

            if (cbRegLoop.Checked == true)
                data[0] |= 0x04;

            if (cbSdRectEn.Checked == true)
                data[0] |= 0x02;

            if (cbSmartI2c.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(76, 2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbAgcth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2() < 0)
                return;
        }

        private void _cbAgcEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2() < 0)
                return;
        }

        private void _cbGdPd_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2() < 0)
                return;
        }

        private void _cbRegLoop_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2() < 0)
                return;
        }

        private void _cbSdRectEn_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr2() < 0)
                return;
        }

        private void _cbSmartI2c_CheckedChanged(object sender, EventArgs e)
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

            data[0] = 0;
            if (cbIntInv.Checked == true)
                data[0] |= 0x80;
            
            bTmp = 0;
            bTmp |= Convert.ToByte(cbSerRes.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            if (cbHighLC.Checked == true)
                data[0] |= 0x10;

            if (cbAutoIncrement.Checked == true)
                data[0] |= 0x08;

            if (cbNotTC.Checked == true)
                data[0] |= 0x04;

            if (cbTestD.Checked == true)
                data[0] |= 0x02;

            if (cbDisTiaOver.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(76, 3, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbIntInv_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr3() < 0)
                return;
        }

        private void _cbSerRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr3() < 0)
                return;
        }

        private void _cbHighLC_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr3() < 0)
                return;
        }

        private void _cbAutoIncrement_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr3() < 0)
                return;
        }

        private void _cbNotTC_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr3() < 0)
                return;
        }

        private void _cbTestD_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr3() < 0)
                return;
        }

        private void _cbDisTiaOver_CheckedChanged(object sender, EventArgs e)
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
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;
            
            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbGain.SelectedIndex);
            bTmp <<= 6;
            data[0] |= bTmp;
            
            bTmp = 0;
            bTmp |= Convert.ToByte(cbEfChan.SelectedIndex);
            bTmp <<= 3;
            data[0] |= bTmp;
            
            bTmp = 0;
            bTmp |= Convert.ToByte(cbSwingSize.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(76, 4, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4() < 0)
                return;
        }

        private void _cbEfChan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4() < 0)
                return;
        }

        private void _cbSwingSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr4() < 0)
                return;
        }

        private int _WriteAddr5()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;
            
            data[0] = 0;
            if (cbGain4A.Checked == true)
                data[0] |= 0x80;
            
            bTmp = 0;
            bTmp |= Convert.ToByte(cbDegOut.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;
            
            bTmp = 0;
            bTmp |= Convert.ToByte(cbTiaBw.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;
            
            bTmp = 0;
            bTmp |= Convert.ToByte(cbEfOutDiff.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(76, 5, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbGain4A_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr5() < 0)
                return;
        }

        private void _cbDegOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr5() < 0)
                return;
        }

        private void _cbTiaBw_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr5() < 0)
                return;
        }

        private void _cbEfOutDiff_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr5() < 0)
                return;
        }

        private int _WriteAddr63()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbPEAmpAll.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            if (cbOutputENAll.Checked == true)
                data[0] |= 0x08;

            if (cbRssiAll.Checked == true)
                data[0] |= 0x04;

            if (cbDPDAll.Checked == true)
                data[0] |= 0x02;

            if (cbInvAll.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(76, 63, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPEAmpAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr63() < 0)
                return;
        }

        private void _cbOutputENAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr63() < 0)
                return;
        }

        private void _cbRssiAll_CheckedChanged(object sender, EventArgs e)
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

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbPEAmp1.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            if (cbOutputEN1.Checked == true)
                data[0] |= 0x08;

            if (cbRssi1.Checked == true)
                data[0] |= 0x04;

            if (cbDPD1.Checked == true)
                data[0] |= 0x02;

            if (cbInv1.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(76, 64, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPEAmp1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void _cbOutputEN1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr64() < 0)
                return;
        }

        private void _cbRssi1_CheckedChanged(object sender, EventArgs e)
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

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbPEAmp2.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            if (cbOutputEN2.Checked == true)
                data[0] |= 0x08;

            if (cbRssi2.Checked == true)
                data[0] |= 0x04;

            if (cbDPD2.Checked == true)
                data[0] |= 0x02;

            if (cbInv2.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(76, 65, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPEAmp2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65() < 0)
                return;
        }

        private void _cbOutputEN2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr65() < 0)
                return;
        }

        private void _cbRssi2_CheckedChanged(object sender, EventArgs e)
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

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbPEAmp3.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            if (cbOutputEN3.Checked == true)
                data[0] |= 0x08;

            if (cbRssi3.Checked == true)
                data[0] |= 0x04;

            if (cbDPD3.Checked == true)
                data[0] |= 0x02;

            if (cbInv3.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(76, 66, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPEAmp3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr66() < 0)
                return;
        }

        private void _cbOutputEN3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr66() < 0)
                return;
        }

        private void _cbRssi3_CheckedChanged(object sender, EventArgs e)
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

            bTmp = data[0] = 0;
            bTmp |= Convert.ToByte(cbPEAmp4.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            if (cbOutputEN4.Checked == true)
                data[0] |= 0x08;

            if (cbRssi4.Checked == true)
                data[0] |= 0x04;

            if (cbDPD4.Checked == true)
                data[0] |= 0x02;

            if (cbInv4.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(76, 67, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbPEAmp4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr67() < 0)
                return;
        }

        private void _cbOutputEN4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr67() < 0)
                return;
        }

        private void _cbRssi4_CheckedChanged(object sender, EventArgs e)
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

            rv = i2cWriteCB(76, 79, 1, data);
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

            rv = i2cWriteCB(76, 80, 1, data);
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

            rv = i2cWriteCB(76, 81, 1, data);
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

            rv = i2cWriteCB(76, 82, 1, data);
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

            rv = i2cWriteCB(76, 83, 1, data);
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

            if (cbLSdMAll.Checked == true)
                data[0] |= 0x08;

            if (cbLLosMAll.Checked == true)
                data[0] |= 0x04;

            rv = i2cWriteCB(76, 95, 1, data);
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

            if (cbLSdM1.Checked == true)
                data[0] |= 0x08;

            if (cbLLosM1.Checked == true)
                data[0] |= 0x04;

            rv = i2cWriteCB(76, 96, 1, data);
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

            if (cbLSdM2.Checked == true)
                data[0] |= 0x08;

            if (cbLLosM2.Checked == true)
                data[0] |= 0x04;

            rv = i2cWriteCB(76, 97, 1, data);
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

            if (cbLSdM3.Checked == true)
                data[0] |= 0x08;

            if (cbLLosM3.Checked == true)
                data[0] |= 0x04;

            rv = i2cWriteCB(76, 98, 1, data);
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

            if (cbLSdM4.Checked == true)
                data[0] |= 0x08;

            if (cbLLosM4.Checked == true)
                data[0] |= 0x04;

            rv = i2cWriteCB(76, 99, 1, data);
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

        private int _WriteAddr143()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;
            
            data[0] = 0;
            if (cbSignumAll.Checked == true)
                data[0] |= 0x40;
            
            bTmp = 0;
            bTmp |= Convert.ToByte(cbOffAll.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(76, 143, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbSignumAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr143() < 0)
                return;
        }

        private void _cbOffAll_SelectedIndexChanged(object sender, EventArgs e)
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
            if (cbSignum1.Checked == true)
                data[0] |= 0x40;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbOff1.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(76, 144, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbSignum1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr144() < 0)
                return;
        }

        private void _cbOff1_SelectedIndexChanged(object sender, EventArgs e)
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
            if (cbSignum2.Checked == true)
                data[0] |= 0x40;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbOff2.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(76, 145, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbSignum2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr145() < 0)
                return;
        }

        private void _cbOff2_SelectedIndexChanged(object sender, EventArgs e)
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
            if (cbSignum3.Checked == true)
                data[0] |= 0x40;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbOff3.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(76, 146, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbSignum3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr146() < 0)
                return;
        }

        private void _cbOff3_SelectedIndexChanged(object sender, EventArgs e)
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
            if (cbSignum4.Checked == true)
                data[0] |= 0x40;

            bTmp = 0;
            bTmp |= Convert.ToByte(cbOff4.SelectedIndex);
            data[0] |= bTmp;

            rv = i2cWriteCB(76, 147, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbSignum4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr147() < 0)
                return;
        }

        private void _cbOff4_SelectedIndexChanged(object sender, EventArgs e)
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

            if (_SetQsfpMode(0x4D) < 0)
                return -1;
            
            data[0] = 0;
            if (cbSQENAll.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(76, 159, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbSQENAll_CheckedChanged(object sender, EventArgs e)
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

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;
            if (cbSQEN1.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(76, 160, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbSQEN1_CheckedChanged(object sender, EventArgs e)
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

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;
            if (cbSQEN2.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(76, 161, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbSQEN2_CheckedChanged(object sender, EventArgs e)
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

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;
            if (cbSQEN3.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(76, 162, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void _cbSQEN3_CheckedChanged(object sender, EventArgs e)
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

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            data[0] = 0;
            if (cbSQEN4.Checked == true)
                data[0] |= 0x01;

            rv = i2cWriteCB(76, 163, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbSQEN4_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WriteAddr163() < 0)
                return;
        }
    }
}
