using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mald38045Mata38044Config
{
    public partial class UcMata38044Config : UserControl
    {
        public delegate int I2cReadCB(byte bank, byte page, byte regAddr, int length, byte[] data);
        public delegate int I2cWriteCB(byte bank, byte page, byte regAddr, int length, byte[] data);

        private DataTable dtCmisReg = new DataTable();
        private byte regBank = 0x00;
        private byte regPage = 0xB0;
        private byte regStartAddr = 0x00;
        private I2cReadCB i2cReadCB;
        private I2cWriteCB i2cWriteCB;
        private bool reading = false;

        private int I2cNotImplemented(byte bank, byte page, byte regAddr, int length, byte[] data)
        {
            throw new NotImplementedException();
        }

        private int I2cWrite(byte regAddr, int length, byte[] data)
        {
            int addr;
            byte page;

            page = regPage;
            addr = regStartAddr + regAddr;
            while (addr > 255) {
                page++;
                addr -= 128;
            }

            return i2cWriteCB(regBank, page, (byte)addr, length, data);
        }

        public UcMata38044Config()
        {
            ComboboxItem item;
            double dTmp;
            int i;

            i2cReadCB = new I2cReadCB(I2cNotImplemented);
            i2cWriteCB = new I2cWriteCB(I2cNotImplemented);

            InitializeComponent();

            item = new ComboboxItem();
            item.Text = "0x00:Normal";
            item.Value = 0x00;
            cbReset.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0xAA:Reset w/o OTP";
            item.Value = 0xAA;
            cbReset.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0xAB:Reset w/ OTP";
            item.Value = 0xAB;
            cbReset.Items.Add(item);

            for (i = 0; i < 4; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":Ch" + i.ToString();
                item.Value = i;
                cbRssiChSel.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0x00:Default";
            item.Value = 0x00;
            cbDeviceIdProgCode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Prog mode";
            item.Value = 0x01;
            cbDeviceIdProgCode.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Not programmable";
            item.Value = 0x00;
            cbDeviceIdProgMode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Programmed through DIPC";
            item.Value = 0x01;
            cbDeviceIdProgMode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:programmable";
            item.Value = 0x02;
            cbDeviceIdProgMode.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Overide LSBs";
            item.Value = 0x03;
            cbDeviceIdProgMode.Items.Add(item);

            for (i = 0; i <= 0x3F; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2");
                item.Value = i;
                cbDeviceIdValue.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0x00:Both buf turned off";
            item.Value = 0x00;
            cbPllPdbClkbuf.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Buf1 on (full rate)";
            item.Value = 0x01;
            cbPllPdbClkbuf.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:Buf2 on (half rate)";
            item.Value = 0x02;
            cbPllPdbClkbuf.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Both buf turned on";
            item.Value = 0x03;
            cbPllPdbClkbuf.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:1/2";
            item.Value = 0x00;
            cbPllDivSel.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:1/8";
            item.Value = 0x01;
            cbPllDivSel.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:1/32";
            item.Value = 0x02;
            cbPllDivSel.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:1/40";
            item.Value = 0x03;
            cbPllDivSel.Items.Add(item);

            for (i = 0, dTmp = 1.5; i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "dB";
                item.Value = i;
                cbEqTiaToCdrCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "dB";
                item.Value = i;
                cbEqTiaToCdrCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "dB";
                item.Value = i;
                cbEqTiaToCdrCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "dB";
                item.Value = i;
                cbEqTiaToCdrCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "dB";
                item.Value = i;
                cbEqTiaToCdrChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 126 - 54; i < 16; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (54 + (dTmp / 15 * i)).ToString("F0") + "%";
                item.Value = i;
                cbRefAmpSwingAgcLoopCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (54 + (dTmp / 15 * i)).ToString("F0") + "%";
                item.Value = i;
                cbRefAmpSwingAgcLoopCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (54 + (dTmp / 15 * i)).ToString("F0") + "%";
                item.Value = i;
                cbRefAmpSwingAgcLoopCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (54 + (dTmp / 15 * i)).ToString("F0") + "%";
                item.Value = i;
                cbRefAmpSwingAgcLoopCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (54 + (dTmp / 15 * i)).ToString("F0") + "%";
                item.Value = i;
                cbRefAmpSwingAgcLoopChAll.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0x00:2.078V";
            item.Value = 0x00;
            cbTuneRefCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:2.111V";
            item.Value = 0x01;
            cbTuneRefCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:2.143V";
            item.Value = 0x02;
            cbTuneRefCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:2.176V";
            item.Value = 0x03;
            cbTuneRefCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:2.208V";
            item.Value = 0x04;
            cbTuneRefCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:2.241V";
            item.Value = 0x05;
            cbTuneRefCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:2.273V";
            item.Value = 0x06;
            cbTuneRefCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:2.306V";
            item.Value = 0x07;
            cbTuneRefCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:1.847V";
            item.Value = 0x08;
            cbTuneRefCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:1879V";
            item.Value = 0x09;
            cbTuneRefCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:1.912V";
            item.Value = 0x0A;
            cbTuneRefCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:1.945V";
            item.Value = 0x0B;
            cbTuneRefCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:1.978V";
            item.Value = 0x0C;
            cbTuneRefCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:2.011V";
            item.Value = 0x0D;
            cbTuneRefCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:2.044V";
            item.Value = 0x0E;
            cbTuneRefCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:2.078V";
            item.Value = 0x0F;
            cbTuneRefCh0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:2.078V";
            item.Value = 0x00;
            cbTuneRefCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:2.111V";
            item.Value = 0x01;
            cbTuneRefCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:2.143V";
            item.Value = 0x02;
            cbTuneRefCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:2.176V";
            item.Value = 0x03;
            cbTuneRefCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:2.208V";
            item.Value = 0x04;
            cbTuneRefCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:2.241V";
            item.Value = 0x05;
            cbTuneRefCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:2.273V";
            item.Value = 0x06;
            cbTuneRefCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:2.306V";
            item.Value = 0x07;
            cbTuneRefCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:1.847V";
            item.Value = 0x08;
            cbTuneRefCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:1879V";
            item.Value = 0x09;
            cbTuneRefCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:1.912V";
            item.Value = 0x0A;
            cbTuneRefCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:1.945V";
            item.Value = 0x0B;
            cbTuneRefCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:1.978V";
            item.Value = 0x0C;
            cbTuneRefCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:2.011V";
            item.Value = 0x0D;
            cbTuneRefCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:2.044V";
            item.Value = 0x0E;
            cbTuneRefCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:2.078V";
            item.Value = 0x0F;
            cbTuneRefCh1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:2.078V";
            item.Value = 0x00;
            cbTuneRefCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:2.111V";
            item.Value = 0x01;
            cbTuneRefCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:2.143V";
            item.Value = 0x02;
            cbTuneRefCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:2.176V";
            item.Value = 0x03;
            cbTuneRefCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:2.208V";
            item.Value = 0x04;
            cbTuneRefCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:2.241V";
            item.Value = 0x05;
            cbTuneRefCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:2.273V";
            item.Value = 0x06;
            cbTuneRefCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:2.306V";
            item.Value = 0x07;
            cbTuneRefCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:1.847V";
            item.Value = 0x08;
            cbTuneRefCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:1879V";
            item.Value = 0x09;
            cbTuneRefCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:1.912V";
            item.Value = 0x0A;
            cbTuneRefCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:1.945V";
            item.Value = 0x0B;
            cbTuneRefCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:1.978V";
            item.Value = 0x0C;
            cbTuneRefCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:2.011V";
            item.Value = 0x0D;
            cbTuneRefCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:2.044V";
            item.Value = 0x0E;
            cbTuneRefCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:2.078V";
            item.Value = 0x0F;
            cbTuneRefCh2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:2.078V";
            item.Value = 0x00;
            cbTuneRefCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:2.111V";
            item.Value = 0x01;
            cbTuneRefCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:2.143V";
            item.Value = 0x02;
            cbTuneRefCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:2.176V";
            item.Value = 0x03;
            cbTuneRefCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:2.208V";
            item.Value = 0x04;
            cbTuneRefCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:2.241V";
            item.Value = 0x05;
            cbTuneRefCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:2.273V";
            item.Value = 0x06;
            cbTuneRefCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:2.306V";
            item.Value = 0x07;
            cbTuneRefCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:1.847V";
            item.Value = 0x08;
            cbTuneRefCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:1879V";
            item.Value = 0x09;
            cbTuneRefCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:1.912V";
            item.Value = 0x0A;
            cbTuneRefCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:1.945V";
            item.Value = 0x0B;
            cbTuneRefCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:1.978V";
            item.Value = 0x0C;
            cbTuneRefCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:2.011V";
            item.Value = 0x0D;
            cbTuneRefCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:2.044V";
            item.Value = 0x0E;
            cbTuneRefCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:2.078V";
            item.Value = 0x0F;
            cbTuneRefCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:2.078V";
            item.Value = 0x00;
            cbTuneRefChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:2.111V";
            item.Value = 0x01;
            cbTuneRefChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:2.143V";
            item.Value = 0x02;
            cbTuneRefChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:2.176V";
            item.Value = 0x03;
            cbTuneRefChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:2.208V";
            item.Value = 0x04;
            cbTuneRefChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:2.241V";
            item.Value = 0x05;
            cbTuneRefChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:2.273V";
            item.Value = 0x06;
            cbTuneRefChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:2.306V";
            item.Value = 0x07;
            cbTuneRefChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:1.847V";
            item.Value = 0x08;
            cbTuneRefChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:1879V";
            item.Value = 0x09;
            cbTuneRefChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:1.912V";
            item.Value = 0x0A;
            cbTuneRefChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:1.945V";
            item.Value = 0x0B;
            cbTuneRefChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:1.978V";
            item.Value = 0x0C;
            cbTuneRefChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:2.011V";
            item.Value = 0x0D;
            cbTuneRefChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:2.044V";
            item.Value = 0x0E;
            cbTuneRefChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:2.078V";
            item.Value = 0x0F;
            cbTuneRefChAll.Items.Add(item);

            for (i = 0; i < 4; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (i + 1).ToString() + "dB";
                item.Value = i;
                cbLosHystForRssiCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (i + 1).ToString() + "dB";
                item.Value = i;
                cbLosHystForRssiCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (i + 1).ToString() + "dB";
                item.Value = i;
                cbLosHystForRssiCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (i + 1).ToString() + "dB";
                item.Value = i;
                cbLosHystForRssiCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (i + 1).ToString() + "dB";
                item.Value = i;
                cbLosHystForRssiChAll.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0x00:33uA";
            item.Value = 0x00;
            cbLosThreshCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:36uA";
            item.Value = 0x01;
            cbLosThreshCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:40uA";
            item.Value = 0x02;
            cbLosThreshCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:43uA";
            item.Value = 0x03;
            cbLosThreshCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:47uA";
            item.Value = 0x04;
            cbLosThreshCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:50uA";
            item.Value = 0x05;
            cbLosThreshCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:54uA";
            item.Value = 0x06;
            cbLosThreshCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:57uA";
            item.Value = 0x07;
            cbLosThreshCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:10uA";
            item.Value = 0x08;
            cbLosThreshCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:13uA";
            item.Value = 0x09;
            cbLosThreshCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:17uA";
            item.Value = 0x0A;
            cbLosThreshCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:20uA";
            item.Value = 0x0B;
            cbLosThreshCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:23uA";
            item.Value = 0x0C;
            cbLosThreshCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:27uA";
            item.Value = 0x0D;
            cbLosThreshCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:30uA";
            item.Value = 0x0E;
            cbLosThreshCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:33uA";
            item.Value = 0x0F;
            cbLosThreshCh0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:33uA";
            item.Value = 0x00;
            cbLosThreshCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:36uA";
            item.Value = 0x01;
            cbLosThreshCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:40uA";
            item.Value = 0x02;
            cbLosThreshCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:43uA";
            item.Value = 0x03;
            cbLosThreshCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:47uA";
            item.Value = 0x04;
            cbLosThreshCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:50uA";
            item.Value = 0x05;
            cbLosThreshCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:54uA";
            item.Value = 0x06;
            cbLosThreshCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:57uA";
            item.Value = 0x07;
            cbLosThreshCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:10uA";
            item.Value = 0x08;
            cbLosThreshCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:13uA";
            item.Value = 0x09;
            cbLosThreshCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:17uA";
            item.Value = 0x0A;
            cbLosThreshCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:20uA";
            item.Value = 0x0B;
            cbLosThreshCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:23uA";
            item.Value = 0x0C;
            cbLosThreshCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:27uA";
            item.Value = 0x0D;
            cbLosThreshCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:30uA";
            item.Value = 0x0E;
            cbLosThreshCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:33uA";
            item.Value = 0x0F;
            cbLosThreshCh1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:33uA";
            item.Value = 0x00;
            cbLosThreshCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:36uA";
            item.Value = 0x01;
            cbLosThreshCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:40uA";
            item.Value = 0x02;
            cbLosThreshCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:43uA";
            item.Value = 0x03;
            cbLosThreshCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:47uA";
            item.Value = 0x04;
            cbLosThreshCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:50uA";
            item.Value = 0x05;
            cbLosThreshCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:54uA";
            item.Value = 0x06;
            cbLosThreshCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:57uA";
            item.Value = 0x07;
            cbLosThreshCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:10uA";
            item.Value = 0x08;
            cbLosThreshCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:13uA";
            item.Value = 0x09;
            cbLosThreshCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:17uA";
            item.Value = 0x0A;
            cbLosThreshCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:20uA";
            item.Value = 0x0B;
            cbLosThreshCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:23uA";
            item.Value = 0x0C;
            cbLosThreshCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:27uA";
            item.Value = 0x0D;
            cbLosThreshCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:30uA";
            item.Value = 0x0E;
            cbLosThreshCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:33uA";
            item.Value = 0x0F;
            cbLosThreshCh2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:33uA";
            item.Value = 0x00;
            cbLosThreshCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:36uA";
            item.Value = 0x01;
            cbLosThreshCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:40uA";
            item.Value = 0x02;
            cbLosThreshCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:43uA";
            item.Value = 0x03;
            cbLosThreshCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:47uA";
            item.Value = 0x04;
            cbLosThreshCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:50uA";
            item.Value = 0x05;
            cbLosThreshCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:54uA";
            item.Value = 0x06;
            cbLosThreshCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:57uA";
            item.Value = 0x07;
            cbLosThreshCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:10uA";
            item.Value = 0x08;
            cbLosThreshCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:13uA";
            item.Value = 0x09;
            cbLosThreshCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:17uA";
            item.Value = 0x0A;
            cbLosThreshCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:20uA";
            item.Value = 0x0B;
            cbLosThreshCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:23uA";
            item.Value = 0x0C;
            cbLosThreshCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:27uA";
            item.Value = 0x0D;
            cbLosThreshCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:30uA";
            item.Value = 0x0E;
            cbLosThreshCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:33uA";
            item.Value = 0x0F;
            cbLosThreshCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:33uA";
            item.Value = 0x00;
            cbLosThreshChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:36uA";
            item.Value = 0x01;
            cbLosThreshChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:40uA";
            item.Value = 0x02;
            cbLosThreshChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:43uA";
            item.Value = 0x03;
            cbLosThreshChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:47uA";
            item.Value = 0x04;
            cbLosThreshChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:50uA";
            item.Value = 0x05;
            cbLosThreshChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:54uA";
            item.Value = 0x06;
            cbLosThreshChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:57uA";
            item.Value = 0x07;
            cbLosThreshChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:10uA";
            item.Value = 0x08;
            cbLosThreshChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:13uA";
            item.Value = 0x09;
            cbLosThreshChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:17uA";
            item.Value = 0x0A;
            cbLosThreshChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:20uA";
            item.Value = 0x0B;
            cbLosThreshChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:23uA";
            item.Value = 0x0C;
            cbLosThreshChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:27uA";
            item.Value = 0x0D;
            cbLosThreshChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:30uA";
            item.Value = 0x0E;
            cbLosThreshChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:33uA";
            item.Value = 0x0F;
            cbLosThreshChAll.Items.Add(item);

            for (i = 0; i < 1024; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X3");
                item.Value = i;
                cbAgcValueCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X3");
                item.Value = i;
                cbAgcValueCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X3");
                item.Value = i;
                cbAgcValueCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X3");
                item.Value = i;
                cbAgcValueCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X3");
                item.Value = i;
                cbAgcValueChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X3");
                item.Value = i;
                cbDcdValueCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X3");
                item.Value = i;
                cbDcdValueCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X3");
                item.Value = i;
                cbDcdValueCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X3");
                item.Value = i;
                cbDcdValueCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X3");
                item.Value = i;
                cbDcdValueChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X3");
                item.Value = i;
                cbBiasValueCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X3");
                item.Value = i;
                cbBiasValueCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X3");
                item.Value = i;
                cbBiasValueCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X3");
                item.Value = i;
                cbBiasValueCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X3");
                item.Value = i;
                cbBiasValueChAll.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0x00:2.0dB";
            item.Value = 0x00;
            cbLosHystCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:2.5dB";
            item.Value = 0x01;
            cbLosHystCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:3.5dB";
            item.Value = 0x02;
            cbLosHystCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:4.0dB";
            item.Value = 0x03;
            cbLosHystCh0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:2.0dB";
            item.Value = 0x00;
            cbLosHystCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:2.5dB";
            item.Value = 0x01;
            cbLosHystCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:3.5dB";
            item.Value = 0x02;
            cbLosHystCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:4.0dB";
            item.Value = 0x03;
            cbLosHystCh1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:2.0dB";
            item.Value = 0x00;
            cbLosHystCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:2.5dB";
            item.Value = 0x01;
            cbLosHystCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:3.5dB";
            item.Value = 0x02;
            cbLosHystCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:4.0dB";
            item.Value = 0x03;
            cbLosHystCh2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:2.0dB";
            item.Value = 0x00;
            cbLosHystCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:2.5dB";
            item.Value = 0x01;
            cbLosHystCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:3.5dB";
            item.Value = 0x02;
            cbLosHystCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:4.0dB";
            item.Value = 0x03;
            cbLosHystCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:2.0dB";
            item.Value = 0x00;
            cbLosHystChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:2.5dB";
            item.Value = 0x01;
            cbLosHystChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:3.5dB";
            item.Value = 0x02;
            cbLosHystChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:4.0dB";
            item.Value = 0x03;
            cbLosHystChAll.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:30mVppd";
            item.Value = 0x00;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:40mVppd";
            item.Value = 0x01;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:45mVppd";
            item.Value = 0x02;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:52mVppd";
            item.Value = 0x03;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:60mVppd";
            item.Value = 0x04;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:68mVppd";
            item.Value = 0x05;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:75mVppd";
            item.Value = 0x06;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:85mVppd";
            item.Value = 0x07;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:110mVppd";
            item.Value = 0x08;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:125mVppd";
            item.Value = 0x09;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:140mVppd";
            item.Value = 0x0A;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:155mVppd";
            item.Value = 0x0B;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:170mVppd";
            item.Value = 0x0C;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:210mVppd";
            item.Value = 0x0D;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:230mVppd";
            item.Value = 0x0E;
            cbLosVthCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:250mVppd";
            item.Value = 0x0F;
            cbLosVthCh0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:30mVppd";
            item.Value = 0x00;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:40mVppd";
            item.Value = 0x01;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:45mVppd";
            item.Value = 0x02;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:52mVppd";
            item.Value = 0x03;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:60mVppd";
            item.Value = 0x04;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:68mVppd";
            item.Value = 0x05;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:75mVppd";
            item.Value = 0x06;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:85mVppd";
            item.Value = 0x07;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:110mVppd";
            item.Value = 0x08;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:125mVppd";
            item.Value = 0x09;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:140mVppd";
            item.Value = 0x0A;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:155mVppd";
            item.Value = 0x0B;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:170mVppd";
            item.Value = 0x0C;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:210mVppd";
            item.Value = 0x0D;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:230mVppd";
            item.Value = 0x0E;
            cbLosVthCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:250mVppd";
            item.Value = 0x0F;
            cbLosVthCh1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:30mVppd";
            item.Value = 0x00;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:40mVppd";
            item.Value = 0x01;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:45mVppd";
            item.Value = 0x02;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:52mVppd";
            item.Value = 0x03;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:60mVppd";
            item.Value = 0x04;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:68mVppd";
            item.Value = 0x05;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:75mVppd";
            item.Value = 0x06;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:85mVppd";
            item.Value = 0x07;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:110mVppd";
            item.Value = 0x08;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:125mVppd";
            item.Value = 0x09;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:140mVppd";
            item.Value = 0x0A;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:155mVppd";
            item.Value = 0x0B;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:170mVppd";
            item.Value = 0x0C;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:210mVppd";
            item.Value = 0x0D;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:230mVppd";
            item.Value = 0x0E;
            cbLosVthCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:250mVppd";
            item.Value = 0x0F;
            cbLosVthCh2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:30mVppd";
            item.Value = 0x00;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:40mVppd";
            item.Value = 0x01;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:45mVppd";
            item.Value = 0x02;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:52mVppd";
            item.Value = 0x03;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:60mVppd";
            item.Value = 0x04;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:68mVppd";
            item.Value = 0x05;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:75mVppd";
            item.Value = 0x06;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:85mVppd";
            item.Value = 0x07;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:110mVppd";
            item.Value = 0x08;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:125mVppd";
            item.Value = 0x09;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:140mVppd";
            item.Value = 0x0A;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:155mVppd";
            item.Value = 0x0B;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:170mVppd";
            item.Value = 0x0C;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:210mVppd";
            item.Value = 0x0D;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:230mVppd";
            item.Value = 0x0E;
            cbLosVthCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:250mVppd";
            item.Value = 0x0F;
            cbLosVthCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:30mVppd";
            item.Value = 0x00;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:40mVppd";
            item.Value = 0x01;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:45mVppd";
            item.Value = 0x02;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:52mVppd";
            item.Value = 0x03;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:60mVppd";
            item.Value = 0x04;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:68mVppd";
            item.Value = 0x05;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:75mVppd";
            item.Value = 0x06;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:85mVppd";
            item.Value = 0x07;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x08:110mVppd";
            item.Value = 0x08;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x09:125mVppd";
            item.Value = 0x09;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0A:140mVppd";
            item.Value = 0x0A;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0B:155mVppd";
            item.Value = 0x0B;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0C:170mVppd";
            item.Value = 0x0C;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0D:210mVppd";
            item.Value = 0x0D;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0E:230mVppd";
            item.Value = 0x0E;
            cbLosVthChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x0F:250mVppd";
            item.Value = 0x0F;
            cbLosVthChAll.Items.Add(item);

            for (i = 0, dTmp = 90; i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-90 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac0InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-90 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac0InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-90 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac0InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-90 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac0InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-90 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac0InChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 90; i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac1InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac1InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac1InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac1InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac7baDac1InChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 105; i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac0InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac0InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac0InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac0InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac0InChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac1InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac1InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac1InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac1InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac1InChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (105 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac0InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (105 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac0InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (105 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac0InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (105 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac0InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (105 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac0InChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac1InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac1InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac1InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac1InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac1InChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 209 - 105; i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac2InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac2InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac2InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac2InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (105 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac2InChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-105 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac2InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-105 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac2InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-105 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac2InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-105 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac2InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-105 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac2InChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 314 - 209; i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (209 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac3InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (209 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac3InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (209 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac3InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (209 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac3InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (209 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac3InChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-209 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac3InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-209 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac3InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-209 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac3InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-209 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac3InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-209 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac3InChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 418 - 314; i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (314 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac4InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (314 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac4InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (314 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac4InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (314 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac4InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (314 + (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8baDac4InChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-314 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac4InCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-314 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac4InCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-314 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac4InCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-314 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac4InCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-314 - (dTmp / 63 * i)).ToString("F0") + "mV";
                item.Value = i;
                cbDacEqDac8bbDac4InChAll.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0x00:28MHz";
            item.Value = 0x00;
            cbCdrLbwAdjCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:25MHz";
            item.Value = 0x01;
            cbCdrLbwAdjCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:20MHz";
            item.Value = 0x02;
            cbCdrLbwAdjCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:17MHz";
            item.Value = 0x03;
            cbCdrLbwAdjCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:13MHz";
            item.Value = 0x04;
            cbCdrLbwAdjCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:10MHz";
            item.Value = 0x05;
            cbCdrLbwAdjCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:5MHz";
            item.Value = 0x06;
            cbCdrLbwAdjCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:3MHz";
            item.Value = 0x07;
            cbCdrLbwAdjCh0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:28MHz";
            item.Value = 0x00;
            cbCdrLbwAdjCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:25MHz";
            item.Value = 0x01;
            cbCdrLbwAdjCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:20MHz";
            item.Value = 0x02;
            cbCdrLbwAdjCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:17MHz";
            item.Value = 0x03;
            cbCdrLbwAdjCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:13MHz";
            item.Value = 0x04;
            cbCdrLbwAdjCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:10MHz";
            item.Value = 0x05;
            cbCdrLbwAdjCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:5MHz";
            item.Value = 0x06;
            cbCdrLbwAdjCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:3MHz";
            item.Value = 0x07;
            cbCdrLbwAdjCh1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:28MHz";
            item.Value = 0x00;
            cbCdrLbwAdjCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:25MHz";
            item.Value = 0x01;
            cbCdrLbwAdjCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:20MHz";
            item.Value = 0x02;
            cbCdrLbwAdjCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:17MHz";
            item.Value = 0x03;
            cbCdrLbwAdjCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:13MHz";
            item.Value = 0x04;
            cbCdrLbwAdjCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:10MHz";
            item.Value = 0x05;
            cbCdrLbwAdjCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:5MHz";
            item.Value = 0x06;
            cbCdrLbwAdjCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:3MHz";
            item.Value = 0x07;
            cbCdrLbwAdjCh2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:28MHz";
            item.Value = 0x00;
            cbCdrLbwAdjCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:25MHz";
            item.Value = 0x01;
            cbCdrLbwAdjCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:20MHz";
            item.Value = 0x02;
            cbCdrLbwAdjCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:17MHz";
            item.Value = 0x03;
            cbCdrLbwAdjCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:13MHz";
            item.Value = 0x04;
            cbCdrLbwAdjCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:10MHz";
            item.Value = 0x05;
            cbCdrLbwAdjCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:5MHz";
            item.Value = 0x06;
            cbCdrLbwAdjCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:3MHz";
            item.Value = 0x07;
            cbCdrLbwAdjCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:28MHz";
            item.Value = 0x00;
            cbCdrLbwAdjChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:25MHz";
            item.Value = 0x01;
            cbCdrLbwAdjChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:20MHz";
            item.Value = 0x02;
            cbCdrLbwAdjChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:17MHz";
            item.Value = 0x03;
            cbCdrLbwAdjChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x04:13MHz";
            item.Value = 0x04;
            cbCdrLbwAdjChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x05:10MHz";
            item.Value = 0x05;
            cbCdrLbwAdjChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x06:5MHz";
            item.Value = 0x06;
            cbCdrLbwAdjChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x07:3MHz";
            item.Value = 0x07;
            cbCdrLbwAdjChAll.Items.Add(item);

            for (i = 0, dTmp = 0.1; i < 8; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrFastlockClockDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrFastlockClockDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrFastlockClockDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrFastlockClockDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrFastlockClockDelayChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrTopRetimeDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrTopRetimeDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrTopRetimeDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrTopRetimeDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrTopRetimeDelayChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrBotRetimeDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrBotRetimeDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrBotRetimeDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrBotRetimeDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrBotRetimeDelayChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrMidRetimeDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrMidRetimeDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrMidRetimeDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrMidRetimeDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrMidRetimeDelayChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrPhaseDetectorDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrPhaseDetectorDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrPhaseDetectorDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrPhaseDetectorDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 8 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacCdrPhaseDetectorDelayChAll.Items.Add(item);
            }
            for (i = 0, dTmp = 0.1; i < 8; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrFastlockClockDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrFastlockClockDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrFastlockClockDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrFastlockClockDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrFastlockClockDelayChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrTopRetimeDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrTopRetimeDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrTopRetimeDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrTopRetimeDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrTopRetimeDelayChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrBotRetimeDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrBotRetimeDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrBotRetimeDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrBotRetimeDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrBotRetimeDelayChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrMidRetimeDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrMidRetimeDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrMidRetimeDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrMidRetimeDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrMidRetimeDelayChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrPhaseDetectorDelayCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrPhaseDetectorDelayCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrPhaseDetectorDelayCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrPhaseDetectorDelayCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + (i + 8).ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "UI";
                item.Value = i + 8;
                cbDacCdrPhaseDetectorDelayChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 0.9; i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain6bBotCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain6bBotCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain6bBotCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain6bBotCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain6bBotChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain6bMidCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain6bMidCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain6bMidCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain6bMidCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain6bMidChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain6bTopCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain6bTopCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain6bTopCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain6bTopCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain6bTopChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 0.9; i < 8; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain3bBotCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain3bBotCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain3bBotCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain3bBotCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain3bBotChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain3bMidCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain3bMidCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain3bMidCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain3bMidCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain3bMidChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain3bTopCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain3bTopCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain3bTopCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain3bTopCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 7 * i).ToString("F2") + "Vppd";
                item.Value = i;
                cbDacTxDacMain3bTopChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 20; i < 16; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdBotCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdBotCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdBotCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdBotCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdBotChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdMidCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdMidCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdMidCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdMidCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdMidChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdTopCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdTopCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdTopCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdTopCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 15 * i).ToString("F1") + "%";
                item.Value = i;
                cbDacTxDacDcdTopChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 7.7; i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPreBotCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPreBotCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPreBotCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPreBotCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPreBotChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPreMidCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPreMidCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPreMidCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPreMidCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPreMidChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPreTopCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPreTopCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPreTopCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPreTopCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPreTopChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 6; i < 64; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPostBotCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPostBotCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPostBotCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPostBotCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPostBotChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPostMidCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPostMidCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPostMidCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPostMidCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPostMidChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPostTopCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPostTopCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPostTopCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPostTopCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (dTmp / 63 * i).ToString("F1") + "dB";
                item.Value = i;
                cbDacTxDacPostTopChAll.Items.Add(item);
            }

            for (i = 0, dTmp = 0.2; i < 16; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 15 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacTxDacDatdelBotCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 15 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacTxDacDatdelBotCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 15 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacTxDacDatdelBotCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 15 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacTxDacDatdelBotCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 15 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacTxDacDatdelBotChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 15 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacTxDacDatdelMidCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 15 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacTxDacDatdelMidCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 15 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacTxDacDatdelMidCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 15 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacTxDacDatdelMidCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 15 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacTxDacDatdelMidChAll.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 15 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacTxDacDatdelTopCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 15 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacTxDacDatdelTopCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 15 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacTxDacDatdelTopCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 15 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacTxDacDatdelTopCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2") + ":" + (-0.1 + (dTmp / 15 * i)).ToString("F2") + "UI";
                item.Value = i;
                cbDacTxDacDatdelTopChAll.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0x00:45Ohm";
            item.Value = 0x00;
            cbTxTermOutResCntlCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:50Ohm";
            item.Value = 0x01;
            cbTxTermOutResCntlCh0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:45Ohm";
            item.Value = 0x00;
            cbTxTermOutResCntlCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:50Ohm";
            item.Value = 0x01;
            cbTxTermOutResCntlCh1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:45Ohm";
            item.Value = 0x00;
            cbTxTermOutResCntlCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:50Ohm";
            item.Value = 0x01;
            cbTxTermOutResCntlCh2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:45Ohm";
            item.Value = 0x00;
            cbTxTermOutResCntlCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:50Ohm";
            item.Value = 0x01;
            cbTxTermOutResCntlCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:45Ohm";
            item.Value = 0x00;
            cbTxTermOutResCntlChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:50Ohm";
            item.Value = 0x01;
            cbTxTermOutResCntlChAll.Items.Add(item);

            for (i = 0; i < 8; i++) {
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2");
                item.Value = i;
                cbTxTermOutPeakingCntlCh0.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2");
                item.Value = i;
                cbTxTermOutPeakingCntlCh1.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2");
                item.Value = i;
                cbTxTermOutPeakingCntlCh2.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2");
                item.Value = i;
                cbTxTermOutPeakingCntlCh3.Items.Add(item);
                item = new ComboboxItem();
                item.Text = "0x" + i.ToString("X2");
                item.Value = i;
                cbTxTermOutPeakingCntlChAll.Items.Add(item);
            }

            item = new ComboboxItem();
            item.Text = "0x00:Enable automatic mute cntl";
            item.Value = 0x00;
            cbMuteCntlCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Forece mute on";
            item.Value = 0x01;
            cbMuteCntlCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:Forece driver on";
            item.Value = 0x02;
            cbMuteCntlCh0.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Enable automatic mute control based on analog LOS";
            item.Value = 0x03;
            cbMuteCntlCh0.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Enable automatic mute cntl";
            item.Value = 0x00;
            cbMuteCntlCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Forece mute on";
            item.Value = 0x01;
            cbMuteCntlCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:Forece driver on";
            item.Value = 0x02;
            cbMuteCntlCh1.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Enable automatic mute control based on analog LOS";
            item.Value = 0x03;
            cbMuteCntlCh1.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Enable automatic mute cntl";
            item.Value = 0x00;
            cbMuteCntlCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Forece mute on";
            item.Value = 0x01;
            cbMuteCntlCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:Forece driver on";
            item.Value = 0x02;
            cbMuteCntlCh2.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Enable automatic mute control based on analog LOS";
            item.Value = 0x03;
            cbMuteCntlCh2.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Enable automatic mute cntl";
            item.Value = 0x00;
            cbMuteCntlCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Forece mute on";
            item.Value = 0x01;
            cbMuteCntlCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:Forece driver on";
            item.Value = 0x02;
            cbMuteCntlCh3.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Enable automatic mute control based on analog LOS";
            item.Value = 0x03;
            cbMuteCntlCh3.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "0x00:Enable automatic mute cntl";
            item.Value = 0x00;
            cbMuteCntlChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x01:Forece mute on";
            item.Value = 0x01;
            cbMuteCntlChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x02:Forece driver on";
            item.Value = 0x02;
            cbMuteCntlChAll.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "0x03:Enable automatic mute control based on analog LOS";
            item.Value = 0x03;
            cbMuteCntlChAll.Items.Add(item);

            dtCmisReg.Columns.Add("# (Dec)", typeof(String));
            dtCmisReg.Columns.Add("# (Hex)", typeof(String));
            dtCmisReg.Columns.Add("Data", typeof(String));

            dgvCmisReg.DataSource = dtCmisReg;
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

        public int SetRegBankApi(byte bank)
        {
            regBank = bank;

            return 0;
        }

        public int SetRegPageApi(byte page)
        {
            regPage = page;

            return 0;
        }

        public int SetRegStartAddrApi(byte startAddr)
        {
            regStartAddr = startAddr;

            return 0;
        }

        private void _ParsePage00Addr00(byte data)
        {
            tbChipId.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage00Addr01(byte data)
        {
            tbRevId.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage00Addr02(byte data)
        {
            foreach (ComboboxItem item in cbReset.Items) {
                if (item.Value == data)
                    cbReset.SelectedItem = item;
            }
        }

        private void _ParsePage00Addr03(byte data)
        {
            if ((data & 0x80) == 0)
                cbIntrptPadPolarity.Checked = false;
            else
                cbIntrptPadPolarity.Checked = true;

            if ((data & 0x40) == 0)
                cbIntrptOen.Checked = false;
            else
                cbIntrptOen.Checked = true;

            if ((data & 0x20) == 0)
                cbIntrptOut.Checked = false;
            else
                cbIntrptOut.Checked = true;

            if ((data & 0x10) == 0)
                cbI2cAddressMode.Checked = false;
            else
                cbI2cAddressMode.Checked = true;

            foreach (ComboboxItem item in cbRssiChSel.Items) {
                if (item.Value == ((data & 0x0C) >> 2))
                    cbRssiChSel.SelectedItem = item;
            }
        }

        private void _ParsePage00Addr04(byte data)
        {
            if ((data & 0x08) == 0)
                cbLpmodePadPolarity.Checked = false;
            else
                cbLpmodePadPolarity.Checked = true;

            if ((data & 0x04) == 0)
                cbStatusLpmode.Checked = false;
            else
                cbStatusLpmode.Checked = true;

            if ((data & 0x02) == 0)
                cbForceLpmodePin.Checked = false;
            else
                cbForceLpmodePin.Checked = true;

            if ((data & 0x01) == 0)
                cbSetLpmode.Checked = false;
            else
                cbSetLpmode.Checked = true;
        }

        private void _ParsePage00Addr06(byte data)
        {
            foreach (ComboboxItem item in cbDeviceIdProgCode.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDeviceIdProgCode.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDeviceIdProgMode.Items) {
                if (item.Value == (data & 0x03))
                    cbDeviceIdProgMode.SelectedItem = item;
            }
        }

        private void _ParsePage00Addr07(byte data)
        {
            foreach (ComboboxItem item in cbDeviceIdValue.Items) {
                if (item.Value == ((data & 0xFE) >> 1))
                    cbDeviceIdValue.SelectedItem = item;
            }
        }

        private void _ParsePage00AddrFF(byte data)
        {
            tbRegPage.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage10Addr08(byte data)
        {
            if ((data & 0x01) == 0)
                cbTsDisable.Checked = false;
            else
                cbTsDisable.Checked = true;
        }

        private void _ParsePage10Addr30(byte data)
        {
            foreach (ComboboxItem item in cbPllPdbClkbuf.Items) {
                if (item.Value == ((data & 0x0C) >> 2))
                    cbPllPdbClkbuf.SelectedItem = item;
            }

            if ((data & 0x02) == 0)
                cbPllPdbMonclk.Checked = false;
            else
                cbPllPdbMonclk.Checked = true;

            if ((data & 0x01) == 0)
                cbPllPdb.Checked = false;
            else
                cbPllPdb.Checked = true;
        }

        private void _ParsePage10Addr31(byte data)
        {
            foreach (ComboboxItem item in cbPllDivSel.Items) {
                if (item.Value == (data & 0x03))
                    cbPllDivSel.SelectedItem = item;
            }
        }

        private void _ParsePage10Addr42(byte data)
        {
            if ((data & 0x02) == 0)
                cbPrbsReset.Checked = false;
            else
                cbPrbsReset.Checked = true;

            if ((data & 0x01) == 0)
                cbPrbsEnable.Checked = false;
            else
                cbPrbsEnable.Checked = true;
        }

        private void _ParsePage10Addr44(byte data)
        {
            if ((data & 0x20) == 0)
                cbPrbsInsErrB.Checked = false;
            else
                cbPrbsInsErrB.Checked = true;

            if ((data & 0x10) == 0)
                cbPrbsInsErrA.Checked = false;
            else
                cbPrbsInsErrA.Checked = true;

            if ((data & 0x08) == 0)
                cbPrbsForcePolflipHighB.Checked = false;
            else
                cbPrbsForcePolflipHighB.Checked = true;

            if ((data & 0x04) == 0)
                cbPrbsForcePolflipLowB.Checked = false;
            else
                cbPrbsForcePolflipLowB.Checked = true;

            if ((data & 0x02) == 0)
                cbPrbsForcePolflipHighA.Checked = false;
            else
                cbPrbsForcePolflipHighA.Checked = true;

            if ((data & 0x01) == 0)
                cbPrbsForcePolflipLowA.Checked = false;
            else
                cbPrbsForcePolflipLowA.Checked = true;
        }

        private void _ParsePage10Addr45(byte data)
        {
            if ((data & 0x20) == 0)
                cbPrbsEnNrz.Checked = false;
            else
                cbPrbsEnNrz.Checked = true;
        }

        private void _ParsePage10Addr46(byte data)
        {
            if ((data & 0x01) == 0)
                cbPrbsSelClk.Checked = false;
            else
                cbPrbsSelClk.Checked = true;
        }

        private void _ParsePage10Addr47(byte data)
        {
            if ((data & 0x08) == 0)
                cbChkFreezeCntr.Checked = false;
            else
                cbChkFreezeCntr.Checked = true;

            if ((data & 0x04) == 0)
                cbChkReset.Checked = false;
            else
                cbChkReset.Checked = true;

            if ((data & 0x02) == 0)
                cbChkClosedLoop.Checked = false;
            else
                cbChkClosedLoop.Checked = true;

            if ((data & 0x01) == 0)
                cbChkEnable.Checked = false;
            else
                cbChkEnable.Checked = true;
        }

        private void _ParsePage10Addr48(byte data)
        {
            if ((data & 0x02) == 0)
                cbChkPolflipStrmB.Checked = false;
            else
                cbChkPolflipStrmB.Checked = true;

            if ((data & 0x01) == 0)
                cbChkPolflipStrmA.Checked = false;
            else
                cbChkPolflipStrmA.Checked = true;
        }

        private void _ParsePage10Addr4A(byte data)
        {
            tbChkErrCnt1A.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage10Addr4B(byte data)
        {
            tbChkErrCnt1B.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage10Addr4C(byte data)
        {
            tbChkErrCnt2A.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage10Addr4D(byte data)
        {
            tbChkErrCnt2B.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage11Addr1A(byte data)
        {
            if ((data & 0x80) == 0)
                cbDdmiReadRequest.Checked = false;
            else
                cbDdmiReadRequest.Checked = true;
        }

        private void _ParsePage11Addr40_41(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutV3p3Reg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 3.73;
            tbDdmiOutV3p3Value.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr42_43(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutV1monReg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 3.73;
            tbDdmiOutV1monValue.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr46_47(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutV1p8Reg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 2;
            tbDdmiOutV1p8Value.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr48_49(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutV1p2Reg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 2;
            tbDdmiOutV1p2Value.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr60_61(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutRssi0Reg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 0.4 * Math.Pow(2, 2);
            tbDdmiOutRssi0Value.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr62_63(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutRssi1Reg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 0.4 * Math.Pow(2, 2);
            tbDdmiOutRssi1Value.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr64_65(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutRssi2Reg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 0.4 * Math.Pow(2, 2);
            tbDdmiOutRssi2Value.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr66_67(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutRssi3Reg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = dTmp / 4095 * 0.4 * Math.Pow(2, 2);
            tbDdmiOutRssi3Value.Text = dTmp.ToString("F2");
        }

        private void _ParsePage11Addr68_69(byte data0, byte data1)
        {
            int iTmp;
            double dTmp;

            iTmp = (data1 << 8) | data0;

            tbDdmiOutTempSenseReg.Text = "0x" + iTmp.ToString("X4");

            dTmp = iTmp & 0x1FFF;
            dTmp = 0.128 * (dTmp - 2210) + 22;
            tbDdmiOutTempSenseValue.Text = dTmp.ToString("F2");
        }

        private void _ParsePage20Addr00(byte data)
        {
            foreach (ComboboxItem item in cbEqTiaToCdrCh0.Items) {
                if (item.Value == ((data & 0xFC) >> 2))
                    cbEqTiaToCdrCh0.SelectedItem = item;
            }
        }

        private void _ParsePage20Addr02(byte data)
        {
            foreach (ComboboxItem item in cbRefAmpSwingAgcLoopCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbRefAmpSwingAgcLoopCh0.SelectedItem = item;
            }
        }

        private void _ParsePage20Addr0A(byte data)
        {
            foreach (ComboboxItem item in cbTuneRefCh0.Items) {
                if (item.Value == ((data & 0x1E) >> 1))
                    cbTuneRefCh0.SelectedItem = item;
            }

            if ((data & 0x01) == 0)
                cbSelRefLowCh0.Checked = false;
            else
                cbSelRefLowCh0.Checked = true;
        }

        private void _ParsePage20Addr19(byte data)
        {
            if ((data & 0x80) == 0)
                cbSquelchOnLosCh0.Checked = false;
            else
                cbSquelchOnLosCh0.Checked = true;

            if ((data & 0x40) == 0)
                cbLosChMaskCh0.Checked = false;
            else
                cbLosChMaskCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbLosChStickyCh0.Checked = false;
            else
                cbLosChStickyCh0.Checked = true;

            if ((data & 0x10) == 0)
                cbLosChCh0.Checked = false;
            else
                cbLosChCh0.Checked = true;

            foreach (ComboboxItem item in cbLosHystForRssiCh0.Items) {
                if (item.Value == ((data & 0x0C) >> 2))
                    cbLosHystForRssiCh0.SelectedItem = item;
            }
        }

        private void _ParsePage20Addr1A(byte data)
        {
            foreach (ComboboxItem item in cbLosThreshCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbLosThreshCh0.SelectedItem = item;
            }
        }

        private void _ParsePage20Addr90_91(byte data0, byte data1)
        {
            int iTmp;

            iTmp = ((data1 & 0x03) << 8) | data0;

            foreach (ComboboxItem item in cbAgcValueCh0.Items) {
                if (item.Value == iTmp)
                    cbAgcValueCh0.SelectedItem = item;
            }
        }

        private void _ParsePage20Addr92(byte data)
        {
            if ((data & 0x01) == 0)
                cbAgcLoopEnableCh0.Checked = false;
            else
                cbAgcLoopEnableCh0.Checked = true;
        }

        private void _ParsePage20AddrB0_B1(byte data0, byte data1)
        {
            int iTmp;

            iTmp = ((data1 & 0x03) << 8) | data0;

            foreach (ComboboxItem item in cbDcdValueCh0.Items) {
                if (item.Value == iTmp)
                    cbDcdValueCh0.SelectedItem = item;
            }
        }

        private void _ParsePage20AddrB2(byte data)
        {
            if ((data & 0x01) == 0)
                cbDcdFilterCh0.Checked = false;
            else
                cbDcdFilterCh0.Checked = true;
        }

        private void _ParsePage20AddrD0_D1(byte data0, byte data1)
        {
            int iTmp;

            iTmp = ((data1 & 0x03) << 8) | data0;

            foreach (ComboboxItem item in cbBiasValueCh0.Items) {
                if (item.Value == iTmp)
                    cbBiasValueCh0.SelectedItem = item;
            }
        }

        private void _ParsePage20AddrD2(byte data)
        {
            if ((data & 0x01) == 0)
                cbFilter2FilterEnableCh0.Checked = false;
            else
                cbFilter2FilterEnableCh0.Checked = true;
        }

        private void _ParsePage21Addr00(byte data)
        {
            foreach (ComboboxItem item in cbEqTiaToCdrCh1.Items) {
                if (item.Value == ((data & 0xFC) >> 2))
                    cbEqTiaToCdrCh1.SelectedItem = item;
            }
        }

        private void _ParsePage21Addr02(byte data)
        {
            foreach (ComboboxItem item in cbRefAmpSwingAgcLoopCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbRefAmpSwingAgcLoopCh1.SelectedItem = item;
            }
        }

        private void _ParsePage21Addr0A(byte data)
        {
            foreach (ComboboxItem item in cbTuneRefCh1.Items) {
                if (item.Value == ((data & 0x1E) >> 1))
                    cbTuneRefCh1.SelectedItem = item;
            }

            if ((data & 0x01) == 0)
                cbSelRefLowCh1.Checked = false;
            else
                cbSelRefLowCh1.Checked = true;
        }

        private void _ParsePage21Addr19(byte data)
        {
            if ((data & 0x80) == 0)
                cbSquelchOnLosCh1.Checked = false;
            else
                cbSquelchOnLosCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbLosChMaskCh1.Checked = false;
            else
                cbLosChMaskCh1.Checked = true;

            if ((data & 0x20) == 0)
                cbLosChStickyCh1.Checked = false;
            else
                cbLosChStickyCh1.Checked = true;

            if ((data & 0x10) == 0)
                cbLosChCh1.Checked = false;
            else
                cbLosChCh1.Checked = true;

            foreach (ComboboxItem item in cbLosHystForRssiCh1.Items) {
                if (item.Value == ((data & 0x0C) >> 2))
                    cbLosHystForRssiCh1.SelectedItem = item;
            }
        }

        private void _ParsePage21Addr1A(byte data)
        {
            foreach (ComboboxItem item in cbLosThreshCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbLosThreshCh1.SelectedItem = item;
            }
        }

        private void _ParsePage21Addr90_91(byte data0, byte data1)
        {
            int iTmp;

            iTmp = ((data1 & 0x03) << 8) | data0;

            foreach (ComboboxItem item in cbAgcValueCh1.Items) {
                if (item.Value == iTmp)
                    cbAgcValueCh1.SelectedItem = item;
            }
        }

        private void _ParsePage21Addr92(byte data)
        {
            if ((data & 0x01) == 0)
                cbAgcLoopEnableCh1.Checked = false;
            else
                cbAgcLoopEnableCh1.Checked = true;
        }

        private void _ParsePage21AddrB0_B1(byte data0, byte data1)
        {
            int iTmp;

            iTmp = ((data1 & 0x03) << 8) | data0;

            foreach (ComboboxItem item in cbDcdValueCh1.Items) {
                if (item.Value == iTmp)
                    cbDcdValueCh1.SelectedItem = item;
            }
        }

        private void _ParsePage21AddrB2(byte data)
        {
            if ((data & 0x01) == 0)
                cbDcdFilterCh1.Checked = false;
            else
                cbDcdFilterCh1.Checked = true;
        }

        private void _ParsePage21AddrD0_D1(byte data0, byte data1)
        {
            int iTmp;

            iTmp = ((data1 & 0x03) << 8) | data0;

            foreach (ComboboxItem item in cbBiasValueCh1.Items) {
                if (item.Value == iTmp)
                    cbBiasValueCh1.SelectedItem = item;
            }
        }

        private void _ParsePage21AddrD2(byte data)
        {
            if ((data & 0x01) == 0)
                cbFilter2FilterEnableCh1.Checked = false;
            else
                cbFilter2FilterEnableCh1.Checked = true;
        }

        private void _ParsePage22Addr00(byte data)
        {
            foreach (ComboboxItem item in cbEqTiaToCdrCh2.Items) {
                if (item.Value == ((data & 0xFC) >> 2))
                    cbEqTiaToCdrCh2.SelectedItem = item;
            }
        }

        private void _ParsePage22Addr02(byte data)
        {
            foreach (ComboboxItem item in cbRefAmpSwingAgcLoopCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbRefAmpSwingAgcLoopCh2.SelectedItem = item;
            }
        }

        private void _ParsePage22Addr0A(byte data)
        {
            foreach (ComboboxItem item in cbTuneRefCh2.Items) {
                if (item.Value == ((data & 0x1E) >> 1))
                    cbTuneRefCh2.SelectedItem = item;
            }

            if ((data & 0x01) == 0)
                cbSelRefLowCh2.Checked = false;
            else
                cbSelRefLowCh2.Checked = true;
        }

        private void _ParsePage22Addr19(byte data)
        {
            if ((data & 0x80) == 0)
                cbSquelchOnLosCh2.Checked = false;
            else
                cbSquelchOnLosCh2.Checked = true;

            if ((data & 0x40) == 0)
                cbLosChMaskCh2.Checked = false;
            else
                cbLosChMaskCh2.Checked = true;

            if ((data & 0x20) == 0)
                cbLosChStickyCh2.Checked = false;
            else
                cbLosChStickyCh2.Checked = true;

            if ((data & 0x10) == 0)
                cbLosChCh2.Checked = false;
            else
                cbLosChCh2.Checked = true;

            foreach (ComboboxItem item in cbLosHystForRssiCh2.Items) {
                if (item.Value == ((data & 0x0C) >> 2))
                    cbLosHystForRssiCh2.SelectedItem = item;
            }
        }

        private void _ParsePage22Addr1A(byte data)
        {
            foreach (ComboboxItem item in cbLosThreshCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbLosThreshCh2.SelectedItem = item;
            }
        }

        private void _ParsePage22Addr90_91(byte data0, byte data1)
        {
            int iTmp;

            iTmp = ((data1 & 0x03) << 8) | data0;

            foreach (ComboboxItem item in cbAgcValueCh2.Items) {
                if (item.Value == iTmp)
                    cbAgcValueCh2.SelectedItem = item;
            }
        }

        private void _ParsePage22Addr92(byte data)
        {
            if ((data & 0x01) == 0)
                cbAgcLoopEnableCh2.Checked = false;
            else
                cbAgcLoopEnableCh2.Checked = true;
        }

        private void _ParsePage22AddrB0_B1(byte data0, byte data1)
        {
            int iTmp;

            iTmp = ((data1 & 0x03) << 8) | data0;

            foreach (ComboboxItem item in cbDcdValueCh2.Items) {
                if (item.Value == iTmp)
                    cbDcdValueCh2.SelectedItem = item;
            }
        }

        private void _ParsePage22AddrB2(byte data)
        {
            if ((data & 0x01) == 0)
                cbDcdFilterCh2.Checked = false;
            else
                cbDcdFilterCh2.Checked = true;
        }

        private void _ParsePage22AddrD0_D1(byte data0, byte data1)
        {
            int iTmp;

            iTmp = ((data1 & 0x03) << 8) | data0;

            foreach (ComboboxItem item in cbBiasValueCh2.Items) {
                if (item.Value == iTmp)
                    cbBiasValueCh2.SelectedItem = item;
            }
        }

        private void _ParsePage22AddrD2(byte data)
        {
            if ((data & 0x01) == 0)
                cbFilter2FilterEnableCh2.Checked = false;
            else
                cbFilter2FilterEnableCh2.Checked = true;
        }

        private void _ParsePage23Addr00(byte data)
        {
            foreach (ComboboxItem item in cbEqTiaToCdrCh3.Items) {
                if (item.Value == ((data & 0xFC) >> 2))
                    cbEqTiaToCdrCh3.SelectedItem = item;
            }
        }

        private void _ParsePage23Addr02(byte data)
        {
            foreach (ComboboxItem item in cbRefAmpSwingAgcLoopCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbRefAmpSwingAgcLoopCh3.SelectedItem = item;
            }
        }

        private void _ParsePage23Addr0A(byte data)
        {
            foreach (ComboboxItem item in cbTuneRefCh3.Items) {
                if (item.Value == ((data & 0x1E) >> 1))
                    cbTuneRefCh3.SelectedItem = item;
            }

            if ((data & 0x01) == 0)
                cbSelRefLowCh3.Checked = false;
            else
                cbSelRefLowCh3.Checked = true;
        }

        private void _ParsePage23Addr19(byte data)
        {
            if ((data & 0x80) == 0)
                cbSquelchOnLosCh3.Checked = false;
            else
                cbSquelchOnLosCh3.Checked = true;

            if ((data & 0x40) == 0)
                cbLosChMaskCh3.Checked = false;
            else
                cbLosChMaskCh3.Checked = true;

            if ((data & 0x20) == 0)
                cbLosChStickyCh3.Checked = false;
            else
                cbLosChStickyCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbLosChCh3.Checked = false;
            else
                cbLosChCh3.Checked = true;

            foreach (ComboboxItem item in cbLosHystForRssiCh3.Items) {
                if (item.Value == ((data & 0x0C) >> 2))
                    cbLosHystForRssiCh3.SelectedItem = item;
            }
        }

        private void _ParsePage23Addr1A(byte data)
        {
            foreach (ComboboxItem item in cbLosThreshCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbLosThreshCh3.SelectedItem = item;
            }
        }

        private void _ParsePage23Addr90_91(byte data0, byte data1)
        {
            int iTmp;

            iTmp = ((data1 & 0x03) << 8) | data0;

            foreach (ComboboxItem item in cbAgcValueCh3.Items) {
                if (item.Value == iTmp)
                    cbAgcValueCh3.SelectedItem = item;
            }
        }

        private void _ParsePage23Addr92(byte data)
        {
            if ((data & 0x01) == 0)
                cbAgcLoopEnableCh3.Checked = false;
            else
                cbAgcLoopEnableCh3.Checked = true;
        }

        private void _ParsePage23AddrB0_B1(byte data0, byte data1)
        {
            int iTmp;

            iTmp = ((data1 & 0x03) << 8) | data0;

            foreach (ComboboxItem item in cbDcdValueCh3.Items) {
                if (item.Value == iTmp)
                    cbDcdValueCh3.SelectedItem = item;
            }
        }

        private void _ParsePage23AddrB2(byte data)
        {
            if ((data & 0x01) == 0)
                cbDcdFilterCh3.Checked = false;
            else
                cbDcdFilterCh3.Checked = true;
        }

        private void _ParsePage23AddrD0_D1(byte data0, byte data1)
        {
            int iTmp;

            iTmp = ((data1 & 0x03) << 8) | data0;

            foreach (ComboboxItem item in cbBiasValueCh3.Items) {
                if (item.Value == iTmp)
                    cbBiasValueCh3.SelectedItem = item;
            }
        }

        private void _ParsePage23AddrD2(byte data)
        {
            if ((data & 0x01) == 0)
                cbFilter2FilterEnableCh3.Checked = false;
            else
                cbFilter2FilterEnableCh3.Checked = true;
        }

        private void _ParsePage30Addr08(byte data)
        {
            if ((data & 0x80) == 0)
                cbLosAnaCh0.Checked = false;
            else
                cbLosAnaCh0.Checked = true;

            if ((data & 0x40) == 0)
                cbLolOrLosCh0.Checked = false;
            else
                cbLolOrLosCh0.Checked = true;

            if ((data & 0x20) == 0)
                cbLolCh0.Checked = false;
            else
                cbLolCh0.Checked = true;

            if ((data & 0x10) == 0)
                cbLosAlarmStickyCh0.Checked = false;
            else
                cbLosAlarmStickyCh0.Checked = true;

            if ((data & 0x08) == 0)
                cbLolAlarmStickyCh0.Checked = false;
            else
                cbLolAlarmStickyCh0.Checked = true;

            if ((data & 0x01) == 0)
                cbLosPdCh0.Checked = false;
            else
                cbLosPdCh0.Checked = true;
        }

        private void _ParsePage30Addr09(byte data)
        {
            foreach (ComboboxItem item in cbLosHystCh0.Items) {
                if (item.Value == ((data & 0x30) >> 4))
                    cbLosHystCh0.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbLosVthCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbLosVthCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr0C(byte data)
        {
            if ((data & 0x10) == 0)
                cbLosForceLosvalCh0.Checked = false;
            else
                cbLosForceLosvalCh0.Checked = true;

            if ((data & 0x08) == 0)
                cbLosForceLosCh0.Checked = false;
            else
                cbLosForceLosCh0.Checked = true;

            if ((data & 0x04) == 0)
                cbLolMaskCh0.Checked = false;
            else
                cbLolMaskCh0.Checked = true;

            if ((data & 0x02) == 0)
                cbLosMaskCh0.Checked = false;
            else
                cbLosMaskCh0.Checked = true;
        }

        private void _ParsePage30Addr15(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsMid0ForceCh0.Checked = false;
            else
                cbThrsMid0ForceCh0.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac7baPdbCh0.Checked = false;
            else
                cbDacEqDac7baPdbCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac7baDac0InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac7baDac0InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr16(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsMid1ForceCh0.Checked = false;
            else
                cbThrsMid1ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac7baDac1InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac7baDac1InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr17(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop0ForceCh0.Checked = false;
            else
                cbThrsTop0ForceCh0.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac8baPdbCh0.Checked = false;
            else
                cbDacEqDac8baPdbCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac0InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac0InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr18(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop1ForceCh0.Checked = false;
            else
                cbThrsTop1ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac1InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac1InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr19(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop2ForceCh0.Checked = false;
            else
                cbThrsTop2ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac2InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac2InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr1A(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop3ForceCh0.Checked = false;
            else
                cbThrsTop3ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac3InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac3InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr1B(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop4ForceCh0.Checked = false;
            else
                cbThrsTop4ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac4InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac4InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr1C(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot0ForceCh0.Checked = false;
            else
                cbThrsBot0ForceCh0.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac8bbPdbCh0.Checked = false;
            else
                cbDacEqDac8bbPdbCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac0InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac0InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr1D(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot1ForceCh0.Checked = false;
            else
                cbThrsBot1ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac1InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac1InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr1E(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot2ForceCh0.Checked = false;
            else
                cbThrsBot2ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac2InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac2InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr1F(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot3ForceCh0.Checked = false;
            else
                cbThrsBot3ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac3InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac3InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr20(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot4ForceCh0.Checked = false;
            else
                cbThrsBot4ForceCh0.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac4InCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac4InCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr41(byte data)
        {
            foreach (ComboboxItem item in cbCdrLbwAdjCh0.Items) {
                if (item.Value == (data & 0x07))
                    cbCdrLbwAdjCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr46(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrFastlockClockDelayCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrFastlockClockDelayCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr47(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrTopRetimeDelayCh0.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDacCdrTopRetimeDelayCh0.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDacCdrBotRetimeDelayCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrBotRetimeDelayCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr48(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrMidRetimeDelayCh0.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDacCdrMidRetimeDelayCh0.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDacCdrPhaseDetectorDelayCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrPhaseDetectorDelayCh0.SelectedItem = item;
            }
        }

        private void _ParsePage30Addr6D(byte data)
        {
            tbEmAdcOutCh0.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage31Addr08(byte data)
        {
            if ((data & 0x80) == 0)
                cbLosAnaCh1.Checked = false;
            else
                cbLosAnaCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbLolOrLosCh1.Checked = false;
            else
                cbLolOrLosCh1.Checked = true;

            if ((data & 0x20) == 0)
                cbLolCh1.Checked = false;
            else
                cbLolCh1.Checked = true;

            if ((data & 0x10) == 0)
                cbLosAlarmStickyCh1.Checked = false;
            else
                cbLosAlarmStickyCh1.Checked = true;

            if ((data & 0x08) == 0)
                cbLolAlarmStickyCh1.Checked = false;
            else
                cbLolAlarmStickyCh1.Checked = true;

            if ((data & 0x01) == 0)
                cbLosPdCh1.Checked = false;
            else
                cbLosPdCh1.Checked = true;
        }

        private void _ParsePage31Addr09(byte data)
        {
            foreach (ComboboxItem item in cbLosHystCh1.Items) {
                if (item.Value == ((data & 0x30) >> 4))
                    cbLosHystCh1.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbLosVthCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbLosVthCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr0C(byte data)
        {
            if ((data & 0x10) == 0)
                cbLosForceLosvalCh1.Checked = false;
            else
                cbLosForceLosvalCh1.Checked = true;

            if ((data & 0x08) == 0)
                cbLosForceLosCh1.Checked = false;
            else
                cbLosForceLosCh1.Checked = true;

            if ((data & 0x04) == 0)
                cbLolMaskCh1.Checked = false;
            else
                cbLolMaskCh1.Checked = true;

            if ((data & 0x02) == 0)
                cbLosMaskCh1.Checked = false;
            else
                cbLosMaskCh1.Checked = true;
        }

        private void _ParsePage31Addr15(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsMid0ForceCh1.Checked = false;
            else
                cbThrsMid0ForceCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac7baPdbCh1.Checked = false;
            else
                cbDacEqDac7baPdbCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac7baDac0InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac7baDac0InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr16(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsMid1ForceCh1.Checked = false;
            else
                cbThrsMid1ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac7baDac1InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac7baDac1InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr17(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop0ForceCh1.Checked = false;
            else
                cbThrsTop0ForceCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac8baPdbCh1.Checked = false;
            else
                cbDacEqDac8baPdbCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac0InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac0InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr18(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop1ForceCh1.Checked = false;
            else
                cbThrsTop1ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac1InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac1InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr19(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop2ForceCh1.Checked = false;
            else
                cbThrsTop2ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac2InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac2InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr1A(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop3ForceCh1.Checked = false;
            else
                cbThrsTop3ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac3InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac3InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr1B(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop4ForceCh1.Checked = false;
            else
                cbThrsTop4ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac4InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac4InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr1C(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot0ForceCh1.Checked = false;
            else
                cbThrsBot0ForceCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac8bbPdbCh1.Checked = false;
            else
                cbDacEqDac8bbPdbCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac0InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac0InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr1D(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot1ForceCh1.Checked = false;
            else
                cbThrsBot1ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac1InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac1InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr1E(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot2ForceCh1.Checked = false;
            else
                cbThrsBot2ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac2InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac2InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr1F(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot3ForceCh1.Checked = false;
            else
                cbThrsBot3ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac3InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac3InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr20(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot4ForceCh1.Checked = false;
            else
                cbThrsBot4ForceCh1.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac4InCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac4InCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr41(byte data)
        {
            foreach (ComboboxItem item in cbCdrLbwAdjCh1.Items) {
                if (item.Value == (data & 0x07))
                    cbCdrLbwAdjCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr46(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrFastlockClockDelayCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrFastlockClockDelayCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr47(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrTopRetimeDelayCh1.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDacCdrTopRetimeDelayCh1.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDacCdrBotRetimeDelayCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrBotRetimeDelayCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr48(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrMidRetimeDelayCh1.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDacCdrMidRetimeDelayCh1.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDacCdrPhaseDetectorDelayCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrPhaseDetectorDelayCh1.SelectedItem = item;
            }
        }

        private void _ParsePage31Addr6D(byte data)
        {
            tbEmAdcOutCh1.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage32Addr08(byte data)
        {
            if ((data & 0x80) == 0)
                cbLosAnaCh2.Checked = false;
            else
                cbLosAnaCh2.Checked = true;

            if ((data & 0x40) == 0)
                cbLolOrLosCh2.Checked = false;
            else
                cbLolOrLosCh2.Checked = true;

            if ((data & 0x20) == 0)
                cbLolCh2.Checked = false;
            else
                cbLolCh2.Checked = true;

            if ((data & 0x10) == 0)
                cbLosAlarmStickyCh2.Checked = false;
            else
                cbLosAlarmStickyCh2.Checked = true;

            if ((data & 0x08) == 0)
                cbLolAlarmStickyCh2.Checked = false;
            else
                cbLolAlarmStickyCh2.Checked = true;

            if ((data & 0x01) == 0)
                cbLosPdCh2.Checked = false;
            else
                cbLosPdCh2.Checked = true;
        }

        private void _ParsePage32Addr09(byte data)
        {
            foreach (ComboboxItem item in cbLosHystCh2.Items) {
                if (item.Value == ((data & 0x30) >> 4))
                    cbLosHystCh2.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbLosVthCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbLosVthCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr0C(byte data)
        {
            if ((data & 0x10) == 0)
                cbLosForceLosvalCh2.Checked = false;
            else
                cbLosForceLosvalCh2.Checked = true;

            if ((data & 0x08) == 0)
                cbLosForceLosCh2.Checked = false;
            else
                cbLosForceLosCh2.Checked = true;

            if ((data & 0x04) == 0)
                cbLolMaskCh2.Checked = false;
            else
                cbLolMaskCh2.Checked = true;

            if ((data & 0x02) == 0)
                cbLosMaskCh2.Checked = false;
            else
                cbLosMaskCh2.Checked = true;
        }

        private void _ParsePage32Addr15(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsMid0ForceCh2.Checked = false;
            else
                cbThrsMid0ForceCh2.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac7baPdbCh2.Checked = false;
            else
                cbDacEqDac7baPdbCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac7baDac0InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac7baDac0InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr16(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsMid1ForceCh2.Checked = false;
            else
                cbThrsMid1ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac7baDac1InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac7baDac1InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr17(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop0ForceCh2.Checked = false;
            else
                cbThrsTop0ForceCh2.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac8baPdbCh2.Checked = false;
            else
                cbDacEqDac8baPdbCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac0InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac0InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr18(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop1ForceCh2.Checked = false;
            else
                cbThrsTop1ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac1InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac1InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr19(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop2ForceCh2.Checked = false;
            else
                cbThrsTop2ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac2InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac2InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr1A(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop3ForceCh2.Checked = false;
            else
                cbThrsTop3ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac3InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac3InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr1B(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop4ForceCh2.Checked = false;
            else
                cbThrsTop4ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac4InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac4InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr1C(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot0ForceCh2.Checked = false;
            else
                cbThrsBot0ForceCh2.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac8bbPdbCh2.Checked = false;
            else
                cbDacEqDac8bbPdbCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac0InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac0InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr1D(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot1ForceCh2.Checked = false;
            else
                cbThrsBot1ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac1InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac1InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr1E(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot2ForceCh2.Checked = false;
            else
                cbThrsBot2ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac2InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac2InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr1F(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot3ForceCh2.Checked = false;
            else
                cbThrsBot3ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac3InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac3InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr20(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot4ForceCh2.Checked = false;
            else
                cbThrsBot4ForceCh2.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac4InCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac4InCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr41(byte data)
        {
            foreach (ComboboxItem item in cbCdrLbwAdjCh2.Items) {
                if (item.Value == (data & 0x07))
                    cbCdrLbwAdjCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr46(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrFastlockClockDelayCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrFastlockClockDelayCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr47(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrTopRetimeDelayCh2.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDacCdrTopRetimeDelayCh2.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDacCdrBotRetimeDelayCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrBotRetimeDelayCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr48(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrMidRetimeDelayCh2.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDacCdrMidRetimeDelayCh2.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDacCdrPhaseDetectorDelayCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrPhaseDetectorDelayCh2.SelectedItem = item;
            }
        }

        private void _ParsePage32Addr6D(byte data)
        {
            tbEmAdcOutCh2.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage33Addr08(byte data)
        {
            if ((data & 0x80) == 0)
                cbLosAnaCh3.Checked = false;
            else
                cbLosAnaCh3.Checked = true;

            if ((data & 0x40) == 0)
                cbLolOrLosCh3.Checked = false;
            else
                cbLolOrLosCh3.Checked = true;

            if ((data & 0x20) == 0)
                cbLolCh3.Checked = false;
            else
                cbLolCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbLosAlarmStickyCh3.Checked = false;
            else
                cbLosAlarmStickyCh3.Checked = true;

            if ((data & 0x08) == 0)
                cbLolAlarmStickyCh3.Checked = false;
            else
                cbLolAlarmStickyCh3.Checked = true;

            if ((data & 0x01) == 0)
                cbLosPdCh3.Checked = false;
            else
                cbLosPdCh3.Checked = true;
        }

        private void _ParsePage33Addr09(byte data)
        {
            foreach (ComboboxItem item in cbLosHystCh3.Items) {
                if (item.Value == ((data & 0x30) >> 4))
                    cbLosHystCh3.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbLosVthCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbLosVthCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr0C(byte data)
        {
            if ((data & 0x10) == 0)
                cbLosForceLosvalCh3.Checked = false;
            else
                cbLosForceLosvalCh3.Checked = true;

            if ((data & 0x08) == 0)
                cbLosForceLosCh3.Checked = false;
            else
                cbLosForceLosCh3.Checked = true;

            if ((data & 0x04) == 0)
                cbLolMaskCh3.Checked = false;
            else
                cbLolMaskCh3.Checked = true;

            if ((data & 0x02) == 0)
                cbLosMaskCh3.Checked = false;
            else
                cbLosMaskCh3.Checked = true;
        }

        private void _ParsePage33Addr15(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsMid0ForceCh3.Checked = false;
            else
                cbThrsMid0ForceCh3.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac7baPdbCh3.Checked = false;
            else
                cbDacEqDac7baPdbCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac7baDac0InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac7baDac0InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr16(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsMid1ForceCh3.Checked = false;
            else
                cbThrsMid1ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac7baDac1InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac7baDac1InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr17(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop0ForceCh3.Checked = false;
            else
                cbThrsTop0ForceCh3.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac8baPdbCh3.Checked = false;
            else
                cbDacEqDac8baPdbCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac0InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac0InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr18(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop1ForceCh3.Checked = false;
            else
                cbThrsTop1ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac1InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac1InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr19(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop2ForceCh3.Checked = false;
            else
                cbThrsTop2ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac2InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac2InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr1A(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop3ForceCh3.Checked = false;
            else
                cbThrsTop3ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac3InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac3InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr1B(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsTop4ForceCh3.Checked = false;
            else
                cbThrsTop4ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8baDac4InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8baDac4InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr1C(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot0ForceCh3.Checked = false;
            else
                cbThrsBot0ForceCh3.Checked = true;

            if ((data & 0x40) == 0)
                cbDacEqDac8bbPdbCh3.Checked = false;
            else
                cbDacEqDac8bbPdbCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac0InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac0InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr1D(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot1ForceCh3.Checked = false;
            else
                cbThrsBot1ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac1InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac1InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr1E(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot2ForceCh3.Checked = false;
            else
                cbThrsBot2ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac2InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac2InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr1F(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot3ForceCh3.Checked = false;
            else
                cbThrsBot3ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac3InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac3InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr20(byte data)
        {
            if ((data & 0x80) == 0)
                cbThrsBot4ForceCh3.Checked = false;
            else
                cbThrsBot4ForceCh3.Checked = true;

            foreach (ComboboxItem item in cbDacEqDac8bbDac4InCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacEqDac8bbDac4InCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr41(byte data)
        {
            foreach (ComboboxItem item in cbCdrLbwAdjCh3.Items) {
                if (item.Value == (data & 0x07))
                    cbCdrLbwAdjCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr46(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrFastlockClockDelayCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrFastlockClockDelayCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr47(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrTopRetimeDelayCh3.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDacCdrTopRetimeDelayCh3.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDacCdrBotRetimeDelayCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrBotRetimeDelayCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr48(byte data)
        {
            foreach (ComboboxItem item in cbDacCdrMidRetimeDelayCh3.Items) {
                if (item.Value == ((data & 0xF0) >> 4))
                    cbDacCdrMidRetimeDelayCh3.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbDacCdrPhaseDetectorDelayCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacCdrPhaseDetectorDelayCh3.SelectedItem = item;
            }
        }

        private void _ParsePage33Addr6D(byte data)
        {
            tbEmAdcOutCh3.Text = "0x" + data.ToString("X2");
        }

        private void _ParsePage40Addr00(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPolinvTopCh0.Checked = false;
            else
                cbTxPolinvTopCh0.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacMain6bPdbTopCh0.Checked = false;
            else
                cbDacTxDacMain6bPdbTopCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain6bTopCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacMain6bTopCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr01(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPolinvMidCh0.Checked = false;
            else
                cbTxPolinvMidCh0.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacMain6bPdbMidCh0.Checked = false;
            else
                cbDacTxDacMain6bPdbMidCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain6bMidCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacMain6bMidCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr02(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPolinvBotCh0.Checked = false;
            else
                cbTxPolinvBotCh0.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacMain6bPdbBotCh0.Checked = false;
            else
                cbDacTxDacMain6bPdbBotCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain6bBotCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacMain6bBotCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr03(byte data)
        {
            if ((data & 0x08) == 0)
                cbDacTxDacMain3bPdbTopCh0.Checked = false;
            else
                cbDacTxDacMain3bPdbTopCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain3bTopCh0.Items) {
                if (item.Value == (data & 0x07))
                    cbDacTxDacMain3bTopCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr04(byte data)
        {
            if ((data & 0x08) == 0)
                cbDacTxDacMain3bPdbMidCh0.Checked = false;
            else
                cbDacTxDacMain3bPdbMidCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain3bMidCh0.Items) {
                if (item.Value == (data & 0x07))
                    cbDacTxDacMain3bMidCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr05(byte data)
        {
            if ((data & 0x08) == 0)
                cbDacTxDacMain3bPdbBotCh0.Checked = false;
            else
                cbDacTxDacMain3bPdbBotCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain3bBotCh0.Items) {
                if (item.Value == (data & 0x07))
                    cbDacTxDacMain3bBotCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr06(byte data)
        {
            if ((data & 0x20) == 0)
                cbTxDcdDacPolflipTopCh0.Checked = false;
            else
                cbTxDcdDacPolflipTopCh0.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdPdbTopCh0.Checked = false;
            else
                cbDacTxDacDcdPdbTopCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdTopCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdTopCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr07(byte data)
        {
            if ((data & 0x20) == 0)
                cbTxDcdDacPolflipMidCh0.Checked = false;
            else
                cbTxDcdDacPolflipMidCh0.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdPdbMidCh0.Checked = false;
            else
                cbDacTxDacDcdPdbMidCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdMidCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdMidCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr08(byte data)
        {
            if ((data & 0x20) == 0)
                cbTxDcdDacPolflipBotCh0.Checked = false;
            else
                cbTxDcdDacPolflipBotCh0.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdPdbBotCh0.Checked = false;
            else
                cbDacTxDacDcdPdbBotCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdBotCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdBotCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr09(byte data)
        {
            if ((data & 0x40) == 0)
                cbDacTxDacPrePdbTopCh0.Checked = false;
            else
                cbDacTxDacPrePdbTopCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreTopCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPreTopCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr0A(byte data)
        {
            if ((data & 0x40) == 0)
                cbDacTxDacPrePdbMidCh0.Checked = false;
            else
                cbDacTxDacPrePdbMidCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreMidCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPreMidCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr0B(byte data)
        {
            if ((data & 0x40) == 0)
                cbDacTxDacPrePdbBotCh0.Checked = false;
            else
                cbDacTxDacPrePdbBotCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreBotCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPreBotCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr0C(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPostPolinvTopCh0.Checked = false;
            else
                cbTxPostPolinvTopCh0.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacPostPdbTopCh0.Checked = false;
            else
                cbDacTxDacPostPdbTopCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostTopCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPostTopCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr0D(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPostPolinvMidCh0.Checked = false;
            else
                cbTxPostPolinvMidCh0.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacPostPdbMidCh0.Checked = false;
            else
                cbDacTxDacPostPdbMidCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostMidCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPostMidCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr0E(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPostPolinvBotCh0.Checked = false;
            else
                cbTxPostPolinvBotCh0.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacPostPdbBotCh0.Checked = false;
            else
                cbDacTxDacPostPdbBotCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostBotCh0.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPostBotCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr1B(byte data)
        {
            if ((data & 0x10) == 0)
                cbTxPdbDatdelTopCh0.Checked = false;
            else
                cbTxPdbDatdelTopCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDatdelTopCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDatdelTopCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr1C(byte data)
        {
            if ((data & 0x10) == 0)
                cbTxPdbDatdelMidCh0.Checked = false;
            else
                cbTxPdbDatdelMidCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDatdelMidCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDatdelMidCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr1D(byte data)
        {
            if ((data & 0x10) == 0)
                cbTxPdbDatdelBotCh0.Checked = false;
            else
                cbTxPdbDatdelBotCh0.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDatdelBotCh0.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDatdelBotCh0.SelectedItem = item;
            }
        }

        private void _ParsePage40Addr1E(byte data)
        {
            if ((data & 0x02) == 0)
                cbTxPdbLbkOutCh0.Checked = false;
            else
                cbTxPdbLbkOutCh0.Checked = true;

            if ((data & 0x01) == 0)
                cbTxPdbLbkInCh0.Checked = false;
            else
                cbTxPdbLbkInCh0.Checked = true;
        }

        private void _ParsePage40Addr4B(byte data)
        {
            if ((data & 0x80) == 0)
                cbAdaptationMuteCh0.Checked = false;
            else
                cbAdaptationMuteCh0.Checked = true;

            foreach (ComboboxItem item in cbTxTermOutResCntlCh0.Items) {
                if (item.Value == ((data & 0x20) >> 5))
                    cbTxTermOutResCntlCh0.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbTxTermOutPeakingCntlCh0.Items) {
                if (item.Value == ((data & 0x1C) >> 2))
                    cbTxTermOutPeakingCntlCh0.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbMuteCntlCh0.Items) {
                if (item.Value == (data & 0x03))
                    cbMuteCntlCh0.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr00(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPolinvTopCh1.Checked = false;
            else
                cbTxPolinvTopCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacMain6bPdbTopCh1.Checked = false;
            else
                cbDacTxDacMain6bPdbTopCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain6bTopCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacMain6bTopCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr01(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPolinvMidCh1.Checked = false;
            else
                cbTxPolinvMidCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacMain6bPdbMidCh1.Checked = false;
            else
                cbDacTxDacMain6bPdbMidCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain6bMidCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacMain6bMidCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr02(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPolinvBotCh1.Checked = false;
            else
                cbTxPolinvBotCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacMain6bPdbBotCh1.Checked = false;
            else
                cbDacTxDacMain6bPdbBotCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain6bBotCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacMain6bBotCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr03(byte data)
        {
            if ((data & 0x08) == 0)
                cbDacTxDacMain3bPdbTopCh1.Checked = false;
            else
                cbDacTxDacMain3bPdbTopCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain3bTopCh1.Items) {
                if (item.Value == (data & 0x07))
                    cbDacTxDacMain3bTopCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr04(byte data)
        {
            if ((data & 0x08) == 0)
                cbDacTxDacMain3bPdbMidCh1.Checked = false;
            else
                cbDacTxDacMain3bPdbMidCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain3bMidCh1.Items) {
                if (item.Value == (data & 0x07))
                    cbDacTxDacMain3bMidCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr05(byte data)
        {
            if ((data & 0x08) == 0)
                cbDacTxDacMain3bPdbBotCh1.Checked = false;
            else
                cbDacTxDacMain3bPdbBotCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain3bBotCh1.Items) {
                if (item.Value == (data & 0x07))
                    cbDacTxDacMain3bBotCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr06(byte data)
        {
            if ((data & 0x20) == 0)
                cbTxDcdDacPolflipTopCh1.Checked = false;
            else
                cbTxDcdDacPolflipTopCh1.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdPdbTopCh1.Checked = false;
            else
                cbDacTxDacDcdPdbTopCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdTopCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdTopCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr07(byte data)
        {
            if ((data & 0x20) == 0)
                cbTxDcdDacPolflipMidCh1.Checked = false;
            else
                cbTxDcdDacPolflipMidCh1.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdPdbMidCh1.Checked = false;
            else
                cbDacTxDacDcdPdbMidCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdMidCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdMidCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr08(byte data)
        {
            if ((data & 0x20) == 0)
                cbTxDcdDacPolflipBotCh1.Checked = false;
            else
                cbTxDcdDacPolflipBotCh1.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdPdbBotCh1.Checked = false;
            else
                cbDacTxDacDcdPdbBotCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdBotCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdBotCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr09(byte data)
        {
            if ((data & 0x40) == 0)
                cbDacTxDacPrePdbTopCh1.Checked = false;
            else
                cbDacTxDacPrePdbTopCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreTopCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPreTopCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr0A(byte data)
        {
            if ((data & 0x40) == 0)
                cbDacTxDacPrePdbMidCh1.Checked = false;
            else
                cbDacTxDacPrePdbMidCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreMidCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPreMidCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr0B(byte data)
        {
            if ((data & 0x40) == 0)
                cbDacTxDacPrePdbBotCh1.Checked = false;
            else
                cbDacTxDacPrePdbBotCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreBotCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPreBotCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr0C(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPostPolinvTopCh1.Checked = false;
            else
                cbTxPostPolinvTopCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacPostPdbTopCh1.Checked = false;
            else
                cbDacTxDacPostPdbTopCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostTopCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPostTopCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr0D(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPostPolinvMidCh1.Checked = false;
            else
                cbTxPostPolinvMidCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacPostPdbMidCh1.Checked = false;
            else
                cbDacTxDacPostPdbMidCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostMidCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPostMidCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr0E(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPostPolinvBotCh1.Checked = false;
            else
                cbTxPostPolinvBotCh1.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacPostPdbBotCh1.Checked = false;
            else
                cbDacTxDacPostPdbBotCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostBotCh1.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPostBotCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr1B(byte data)
        {
            if ((data & 0x10) == 0)
                cbTxPdbDatdelTopCh1.Checked = false;
            else
                cbTxPdbDatdelTopCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDatdelTopCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDatdelTopCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr1C(byte data)
        {
            if ((data & 0x10) == 0)
                cbTxPdbDatdelMidCh1.Checked = false;
            else
                cbTxPdbDatdelMidCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDatdelMidCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDatdelMidCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr1D(byte data)
        {
            if ((data & 0x10) == 0)
                cbTxPdbDatdelBotCh1.Checked = false;
            else
                cbTxPdbDatdelBotCh1.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDatdelBotCh1.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDatdelBotCh1.SelectedItem = item;
            }
        }

        private void _ParsePage41Addr1E(byte data)
        {
            if ((data & 0x02) == 0)
                cbTxPdbLbkOutCh1.Checked = false;
            else
                cbTxPdbLbkOutCh1.Checked = true;

            if ((data & 0x01) == 0)
                cbTxPdbLbkInCh1.Checked = false;
            else
                cbTxPdbLbkInCh1.Checked = true;
        }

        private void _ParsePage41Addr4B(byte data)
        {
            if ((data & 0x80) == 0)
                cbAdaptationMuteCh1.Checked = false;
            else
                cbAdaptationMuteCh1.Checked = true;

            foreach (ComboboxItem item in cbTxTermOutResCntlCh1.Items) {
                if (item.Value == ((data & 0x20) >> 5))
                    cbTxTermOutResCntlCh1.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbTxTermOutPeakingCntlCh1.Items) {
                if (item.Value == ((data & 0x1C) >> 2))
                    cbTxTermOutPeakingCntlCh1.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbMuteCntlCh1.Items) {
                if (item.Value == (data & 0x03))
                    cbMuteCntlCh1.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr00(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPolinvTopCh2.Checked = false;
            else
                cbTxPolinvTopCh2.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacMain6bPdbTopCh2.Checked = false;
            else
                cbDacTxDacMain6bPdbTopCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain6bTopCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacMain6bTopCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr01(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPolinvMidCh2.Checked = false;
            else
                cbTxPolinvMidCh2.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacMain6bPdbMidCh2.Checked = false;
            else
                cbDacTxDacMain6bPdbMidCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain6bMidCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacMain6bMidCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr02(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPolinvBotCh2.Checked = false;
            else
                cbTxPolinvBotCh2.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacMain6bPdbBotCh2.Checked = false;
            else
                cbDacTxDacMain6bPdbBotCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain6bBotCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacMain6bBotCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr03(byte data)
        {
            if ((data & 0x08) == 0)
                cbDacTxDacMain3bPdbTopCh2.Checked = false;
            else
                cbDacTxDacMain3bPdbTopCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain3bTopCh2.Items) {
                if (item.Value == (data & 0x07))
                    cbDacTxDacMain3bTopCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr04(byte data)
        {
            if ((data & 0x08) == 0)
                cbDacTxDacMain3bPdbMidCh2.Checked = false;
            else
                cbDacTxDacMain3bPdbMidCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain3bMidCh2.Items) {
                if (item.Value == (data & 0x07))
                    cbDacTxDacMain3bMidCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr05(byte data)
        {
            if ((data & 0x08) == 0)
                cbDacTxDacMain3bPdbBotCh2.Checked = false;
            else
                cbDacTxDacMain3bPdbBotCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain3bBotCh2.Items) {
                if (item.Value == (data & 0x07))
                    cbDacTxDacMain3bBotCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr06(byte data)
        {
            if ((data & 0x20) == 0)
                cbTxDcdDacPolflipTopCh2.Checked = false;
            else
                cbTxDcdDacPolflipTopCh2.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdPdbTopCh2.Checked = false;
            else
                cbDacTxDacDcdPdbTopCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdTopCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdTopCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr07(byte data)
        {
            if ((data & 0x20) == 0)
                cbTxDcdDacPolflipMidCh2.Checked = false;
            else
                cbTxDcdDacPolflipMidCh2.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdPdbMidCh2.Checked = false;
            else
                cbDacTxDacDcdPdbMidCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdMidCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdMidCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr08(byte data)
        {
            if ((data & 0x20) == 0)
                cbTxDcdDacPolflipBotCh2.Checked = false;
            else
                cbTxDcdDacPolflipBotCh2.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdPdbBotCh2.Checked = false;
            else
                cbDacTxDacDcdPdbBotCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdBotCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdBotCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr09(byte data)
        {
            if ((data & 0x40) == 0)
                cbDacTxDacPrePdbTopCh2.Checked = false;
            else
                cbDacTxDacPrePdbTopCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreTopCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPreTopCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr0A(byte data)
        {
            if ((data & 0x40) == 0)
                cbDacTxDacPrePdbMidCh2.Checked = false;
            else
                cbDacTxDacPrePdbMidCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreMidCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPreMidCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr0B(byte data)
        {
            if ((data & 0x40) == 0)
                cbDacTxDacPrePdbBotCh2.Checked = false;
            else
                cbDacTxDacPrePdbBotCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreBotCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPreBotCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr0C(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPostPolinvTopCh2.Checked = false;
            else
                cbTxPostPolinvTopCh2.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacPostPdbTopCh2.Checked = false;
            else
                cbDacTxDacPostPdbTopCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostTopCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPostTopCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr0D(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPostPolinvMidCh2.Checked = false;
            else
                cbTxPostPolinvMidCh2.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacPostPdbMidCh2.Checked = false;
            else
                cbDacTxDacPostPdbMidCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostMidCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPostMidCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr0E(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPostPolinvBotCh2.Checked = false;
            else
                cbTxPostPolinvBotCh2.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacPostPdbBotCh2.Checked = false;
            else
                cbDacTxDacPostPdbBotCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostBotCh2.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPostBotCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr1B(byte data)
        {
            if ((data & 0x10) == 0)
                cbTxPdbDatdelTopCh2.Checked = false;
            else
                cbTxPdbDatdelTopCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDatdelTopCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDatdelTopCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr1C(byte data)
        {
            if ((data & 0x10) == 0)
                cbTxPdbDatdelMidCh2.Checked = false;
            else
                cbTxPdbDatdelMidCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDatdelMidCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDatdelMidCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr1D(byte data)
        {
            if ((data & 0x10) == 0)
                cbTxPdbDatdelBotCh2.Checked = false;
            else
                cbTxPdbDatdelBotCh2.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDatdelBotCh2.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDatdelBotCh2.SelectedItem = item;
            }
        }

        private void _ParsePage42Addr1E(byte data)
        {
            if ((data & 0x02) == 0)
                cbTxPdbLbkOutCh2.Checked = false;
            else
                cbTxPdbLbkOutCh2.Checked = true;

            if ((data & 0x01) == 0)
                cbTxPdbLbkInCh2.Checked = false;
            else
                cbTxPdbLbkInCh2.Checked = true;
        }

        private void _ParsePage42Addr4B(byte data)
        {
            if ((data & 0x80) == 0)
                cbAdaptationMuteCh2.Checked = false;
            else
                cbAdaptationMuteCh2.Checked = true;

            foreach (ComboboxItem item in cbTxTermOutResCntlCh2.Items) {
                if (item.Value == ((data & 0x20) >> 5))
                    cbTxTermOutResCntlCh2.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbTxTermOutPeakingCntlCh2.Items) {
                if (item.Value == ((data & 0x1C) >> 2))
                    cbTxTermOutPeakingCntlCh2.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbMuteCntlCh2.Items) {
                if (item.Value == (data & 0x03))
                    cbMuteCntlCh2.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr00(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPolinvTopCh3.Checked = false;
            else
                cbTxPolinvTopCh3.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacMain6bPdbTopCh3.Checked = false;
            else
                cbDacTxDacMain6bPdbTopCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain6bTopCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacMain6bTopCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr01(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPolinvMidCh3.Checked = false;
            else
                cbTxPolinvMidCh3.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacMain6bPdbMidCh3.Checked = false;
            else
                cbDacTxDacMain6bPdbMidCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain6bMidCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacMain6bMidCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr02(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPolinvBotCh3.Checked = false;
            else
                cbTxPolinvBotCh3.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacMain6bPdbBotCh3.Checked = false;
            else
                cbDacTxDacMain6bPdbBotCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain6bBotCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacMain6bBotCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr03(byte data)
        {
            if ((data & 0x08) == 0)
                cbDacTxDacMain3bPdbTopCh3.Checked = false;
            else
                cbDacTxDacMain3bPdbTopCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain3bTopCh3.Items) {
                if (item.Value == (data & 0x07))
                    cbDacTxDacMain3bTopCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr04(byte data)
        {
            if ((data & 0x08) == 0)
                cbDacTxDacMain3bPdbMidCh3.Checked = false;
            else
                cbDacTxDacMain3bPdbMidCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain3bMidCh3.Items) {
                if (item.Value == (data & 0x07))
                    cbDacTxDacMain3bMidCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr05(byte data)
        {
            if ((data & 0x08) == 0)
                cbDacTxDacMain3bPdbBotCh3.Checked = false;
            else
                cbDacTxDacMain3bPdbBotCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacMain3bBotCh3.Items) {
                if (item.Value == (data & 0x07))
                    cbDacTxDacMain3bBotCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr06(byte data)
        {
            if ((data & 0x20) == 0)
                cbTxDcdDacPolflipTopCh3.Checked = false;
            else
                cbTxDcdDacPolflipTopCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdPdbTopCh3.Checked = false;
            else
                cbDacTxDacDcdPdbTopCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdTopCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdTopCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr07(byte data)
        {
            if ((data & 0x20) == 0)
                cbTxDcdDacPolflipMidCh3.Checked = false;
            else
                cbTxDcdDacPolflipMidCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdPdbMidCh3.Checked = false;
            else
                cbDacTxDacDcdPdbMidCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdMidCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdMidCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr08(byte data)
        {
            if ((data & 0x20) == 0)
                cbTxDcdDacPolflipBotCh3.Checked = false;
            else
                cbTxDcdDacPolflipBotCh3.Checked = true;

            if ((data & 0x10) == 0)
                cbDacTxDacDcdPdbBotCh3.Checked = false;
            else
                cbDacTxDacDcdPdbBotCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDcdBotCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDcdBotCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr09(byte data)
        {
            if ((data & 0x40) == 0)
                cbDacTxDacPrePdbTopCh3.Checked = false;
            else
                cbDacTxDacPrePdbTopCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreTopCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPreTopCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr0A(byte data)
        {
            if ((data & 0x40) == 0)
                cbDacTxDacPrePdbMidCh3.Checked = false;
            else
                cbDacTxDacPrePdbMidCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreMidCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPreMidCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr0B(byte data)
        {
            if ((data & 0x40) == 0)
                cbDacTxDacPrePdbBotCh3.Checked = false;
            else
                cbDacTxDacPrePdbBotCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPreBotCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPreBotCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr0C(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPostPolinvTopCh3.Checked = false;
            else
                cbTxPostPolinvTopCh3.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacPostPdbTopCh3.Checked = false;
            else
                cbDacTxDacPostPdbTopCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostTopCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPostTopCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr0D(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPostPolinvMidCh3.Checked = false;
            else
                cbTxPostPolinvMidCh3.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacPostPdbMidCh3.Checked = false;
            else
                cbDacTxDacPostPdbMidCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostMidCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPostMidCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr0E(byte data)
        {
            if ((data & 0x80) == 0)
                cbTxPostPolinvBotCh3.Checked = false;
            else
                cbTxPostPolinvBotCh3.Checked = true;

            if ((data & 0x40) == 0)
                cbDacTxDacPostPdbBotCh3.Checked = false;
            else
                cbDacTxDacPostPdbBotCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacPostBotCh3.Items) {
                if (item.Value == (data & 0x3F))
                    cbDacTxDacPostBotCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr1B(byte data)
        {
            if ((data & 0x10) == 0)
                cbTxPdbDatdelTopCh3.Checked = false;
            else
                cbTxPdbDatdelTopCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDatdelTopCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDatdelTopCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr1C(byte data)
        {
            if ((data & 0x10) == 0)
                cbTxPdbDatdelMidCh3.Checked = false;
            else
                cbTxPdbDatdelMidCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDatdelMidCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDatdelMidCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr1D(byte data)
        {
            if ((data & 0x10) == 0)
                cbTxPdbDatdelBotCh3.Checked = false;
            else
                cbTxPdbDatdelBotCh3.Checked = true;

            foreach (ComboboxItem item in cbDacTxDacDatdelBotCh3.Items) {
                if (item.Value == (data & 0x0F))
                    cbDacTxDacDatdelBotCh3.SelectedItem = item;
            }
        }

        private void _ParsePage43Addr1E(byte data)
        {
            if ((data & 0x02) == 0)
                cbTxPdbLbkOutCh3.Checked = false;
            else
                cbTxPdbLbkOutCh3.Checked = true;

            if ((data & 0x01) == 0)
                cbTxPdbLbkInCh3.Checked = false;
            else
                cbTxPdbLbkInCh3.Checked = true;
        }

        private void _ParsePage43Addr4B(byte data)
        {
            if ((data & 0x80) == 0)
                cbAdaptationMuteCh3.Checked = false;
            else
                cbAdaptationMuteCh3.Checked = true;

            foreach (ComboboxItem item in cbTxTermOutResCntlCh3.Items) {
                if (item.Value == ((data & 0x20) >> 5))
                    cbTxTermOutResCntlCh3.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbTxTermOutPeakingCntlCh3.Items) {
                if (item.Value == ((data & 0x1C) >> 2))
                    cbTxTermOutPeakingCntlCh3.SelectedItem = item;
            }

            foreach (ComboboxItem item in cbMuteCntlCh3.Items) {
                if (item.Value == (data & 0x03))
                    cbMuteCntlCh3.SelectedItem = item;
            }
        }

        private void bReadAll_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[256];
            int i, rv;

            if (reading == true)
                return;

            if (i2cReadCB == null)
                return;

            bReadAll.Enabled = false;
            bReadAll.Text = "Reading...";
            reading = true;

            rv = i2cReadCB(regBank, regPage, regStartAddr, 256, data);
            if (rv != 256)
                goto exit;

            while (dtCmisReg.Rows.Count > 1)
            {
                dtCmisReg.Rows.RemoveAt(dtCmisReg.Rows.Count - 1);
            }

            for (i = 0; i < rv; i++)
                dtCmisReg.Rows.Add(i.ToString("D3"), "0x" + i.ToString("X2"), "0x" + data[i].ToString("X2"));

            _ParsePage00Addr00(data[0]);
            _ParsePage00Addr01(data[1]);
            _ParsePage00Addr02(data[2]);
            _ParsePage00Addr03(data[3]);
            _ParsePage00Addr04(data[4]);
            _ParsePage00Addr06(data[5]);
            _ParsePage00Addr07(data[6]);
            _ParsePage00AddrFF(data[7]);

            _ParsePage10Addr08(data[8]);
            _ParsePage10Addr30(data[9]);
            _ParsePage10Addr31(data[10]);
            _ParsePage10Addr42(data[11]);
            _ParsePage10Addr44(data[12]);
            _ParsePage10Addr45(data[13]);
            _ParsePage10Addr46(data[14]);
            _ParsePage10Addr47(data[15]);
            _ParsePage10Addr48(data[16]);
            _ParsePage10Addr4A(data[17]);
            _ParsePage10Addr4B(data[18]);
            _ParsePage10Addr4C(data[19]);
            _ParsePage10Addr4D(data[20]);

            _ParsePage11Addr1A(data[21]);
            _ParsePage11Addr40_41(data[22], data[23]);
            _ParsePage11Addr42_43(data[24], data[25]);
            _ParsePage11Addr46_47(data[26], data[27]);
            _ParsePage11Addr48_49(data[28], data[29]);
            _ParsePage11Addr60_61(data[30], data[31]);
            _ParsePage11Addr62_63(data[32], data[33]);
            _ParsePage11Addr64_65(data[34], data[35]);
            _ParsePage11Addr66_67(data[36], data[37]);
            _ParsePage11Addr68_69(data[38], data[39]);

            _ParsePage20Addr00(data[40]);
            _ParsePage20Addr02(data[41]);
            _ParsePage20Addr0A(data[42]);
            _ParsePage20Addr19(data[43]);
            _ParsePage20Addr1A(data[44]);
            _ParsePage20Addr90_91(data[45], data[46]);
            _ParsePage20Addr92(data[47]);
            _ParsePage20AddrB0_B1(data[48], data[49]);
            _ParsePage20AddrB2(data[50]);
            _ParsePage20AddrD0_D1(data[51], data[52]);
            _ParsePage20AddrD2(data[53]);

            _ParsePage21Addr00(data[54]);
            _ParsePage21Addr02(data[55]);
            _ParsePage21Addr0A(data[56]);
            _ParsePage21Addr19(data[57]);
            _ParsePage21Addr1A(data[58]);
            _ParsePage21Addr90_91(data[59], data[60]);
            _ParsePage21Addr92(data[61]);
            _ParsePage21AddrB0_B1(data[62], data[63]);
            _ParsePage21AddrB2(data[64]);
            _ParsePage21AddrD0_D1(data[65], data[66]);
            _ParsePage21AddrD2(data[67]);

            _ParsePage22Addr00(data[68]);
            _ParsePage22Addr02(data[69]);
            _ParsePage22Addr0A(data[70]);
            _ParsePage22Addr19(data[71]);
            _ParsePage22Addr1A(data[72]);
            _ParsePage22Addr90_91(data[73], data[74]);
            _ParsePage22Addr92(data[75]);
            _ParsePage22AddrB0_B1(data[76], data[77]);
            _ParsePage22AddrB2(data[78]);
            _ParsePage22AddrD0_D1(data[79], data[80]);
            _ParsePage22AddrD2(data[81]);

            _ParsePage23Addr00(data[82]);
            _ParsePage23Addr02(data[83]);
            _ParsePage23Addr0A(data[84]);
            _ParsePage23Addr19(data[85]);
            _ParsePage23Addr1A(data[86]);
            _ParsePage23Addr90_91(data[87], data[88]);
            _ParsePage23Addr92(data[89]);
            _ParsePage23AddrB0_B1(data[90], data[91]);
            _ParsePage23AddrB2(data[92]);
            _ParsePage23AddrD0_D1(data[93], data[94]);
            _ParsePage23AddrD2(data[95]);

            _ParsePage30Addr08(data[96]);
            _ParsePage30Addr09(data[97]);
            _ParsePage30Addr0C(data[98]);
            _ParsePage30Addr15(data[99]);
            _ParsePage30Addr16(data[100]);
            _ParsePage30Addr17(data[101]);
            _ParsePage30Addr18(data[102]);
            _ParsePage30Addr19(data[103]);
            _ParsePage30Addr1A(data[104]);
            _ParsePage30Addr1B(data[105]);
            _ParsePage30Addr1C(data[106]);
            _ParsePage30Addr1D(data[107]);
            _ParsePage30Addr1E(data[108]);
            _ParsePage30Addr1F(data[109]);
            _ParsePage30Addr20(data[110]);
            _ParsePage30Addr41(data[111]);
            _ParsePage30Addr46(data[112]);
            _ParsePage30Addr47(data[113]);
            _ParsePage30Addr48(data[114]);
            _ParsePage30Addr6D(data[115]);

            _ParsePage31Addr08(data[116]);
            _ParsePage31Addr09(data[117]);
            _ParsePage31Addr0C(data[118]);
            _ParsePage31Addr15(data[119]);
            _ParsePage31Addr16(data[120]);
            _ParsePage31Addr17(data[121]);
            _ParsePage31Addr18(data[122]);
            _ParsePage31Addr19(data[123]);
            _ParsePage31Addr1A(data[124]);
            _ParsePage31Addr1B(data[125]);
            _ParsePage31Addr1C(data[126]);
            _ParsePage31Addr1D(data[127]);
            _ParsePage31Addr1E(data[128]);
            _ParsePage31Addr1F(data[129]);
            _ParsePage31Addr20(data[130]);
            _ParsePage31Addr41(data[131]);
            _ParsePage31Addr46(data[132]);
            _ParsePage31Addr47(data[133]);
            _ParsePage31Addr48(data[134]);
            _ParsePage31Addr6D(data[135]);

            _ParsePage32Addr08(data[136]);
            _ParsePage32Addr09(data[137]);
            _ParsePage32Addr0C(data[138]);
            _ParsePage32Addr15(data[139]);
            _ParsePage32Addr16(data[140]);
            _ParsePage32Addr17(data[141]);
            _ParsePage32Addr18(data[142]);
            _ParsePage32Addr19(data[143]);
            _ParsePage32Addr1A(data[144]);
            _ParsePage32Addr1B(data[145]);
            _ParsePage32Addr1C(data[146]);
            _ParsePage32Addr1D(data[147]);
            _ParsePage32Addr1E(data[148]);
            _ParsePage32Addr1F(data[149]);
            _ParsePage32Addr20(data[150]);
            _ParsePage32Addr41(data[151]);
            _ParsePage32Addr46(data[152]);
            _ParsePage32Addr47(data[153]);
            _ParsePage32Addr48(data[154]);
            _ParsePage32Addr6D(data[155]);
        
            _ParsePage33Addr08(data[156]);
            _ParsePage33Addr09(data[157]);
            _ParsePage33Addr0C(data[158]);
            _ParsePage33Addr15(data[159]);
            _ParsePage33Addr16(data[160]);
            _ParsePage33Addr17(data[161]);
            _ParsePage33Addr18(data[162]);
            _ParsePage33Addr19(data[163]);
            _ParsePage33Addr1A(data[164]);
            _ParsePage33Addr1B(data[165]);
            _ParsePage33Addr1C(data[166]);
            _ParsePage33Addr1D(data[167]);
            _ParsePage33Addr1E(data[168]);
            _ParsePage33Addr1F(data[169]);
            _ParsePage33Addr20(data[170]);
            _ParsePage33Addr41(data[171]);
            _ParsePage33Addr46(data[172]);
            _ParsePage33Addr47(data[173]);
            _ParsePage33Addr48(data[174]);
            _ParsePage33Addr6D(data[175]);

            _ParsePage40Addr00(data[176]);
            _ParsePage40Addr01(data[177]);
            _ParsePage40Addr02(data[178]);
            _ParsePage40Addr03(data[179]);
            _ParsePage40Addr04(data[180]);
            _ParsePage40Addr05(data[181]);
            _ParsePage40Addr06(data[182]);
            _ParsePage40Addr07(data[183]);
            _ParsePage40Addr08(data[184]);
            _ParsePage40Addr09(data[185]);
            _ParsePage40Addr0A(data[186]);
            _ParsePage40Addr0B(data[187]);
            _ParsePage40Addr0C(data[188]);
            _ParsePage40Addr0D(data[189]);
            _ParsePage40Addr0E(data[190]);
            _ParsePage40Addr1B(data[191]);
            _ParsePage40Addr1C(data[192]);
            _ParsePage40Addr1D(data[193]);
            _ParsePage40Addr1E(data[194]);
            _ParsePage40Addr4B(data[195]);

            _ParsePage41Addr00(data[196]);
            _ParsePage41Addr01(data[197]);
            _ParsePage41Addr02(data[198]);
            _ParsePage41Addr03(data[199]);
            _ParsePage41Addr04(data[200]);
            _ParsePage41Addr05(data[201]);
            _ParsePage41Addr06(data[202]);
            _ParsePage41Addr07(data[203]);
            _ParsePage41Addr08(data[204]);
            _ParsePage41Addr09(data[205]);
            _ParsePage41Addr0A(data[206]);
            _ParsePage41Addr0B(data[207]);
            _ParsePage41Addr0C(data[208]);
            _ParsePage41Addr0D(data[209]);
            _ParsePage41Addr0E(data[210]);
            _ParsePage41Addr1B(data[211]);
            _ParsePage41Addr1C(data[212]);
            _ParsePage41Addr1D(data[213]);
            _ParsePage41Addr1E(data[214]);
            _ParsePage41Addr4B(data[215]);

            _ParsePage42Addr00(data[216]);
            _ParsePage42Addr01(data[217]);
            _ParsePage42Addr02(data[218]);
            _ParsePage42Addr03(data[219]);
            _ParsePage42Addr04(data[220]);
            _ParsePage42Addr05(data[221]);
            _ParsePage42Addr06(data[222]);
            _ParsePage42Addr07(data[223]);
            _ParsePage42Addr08(data[224]);
            _ParsePage42Addr09(data[225]);
            _ParsePage42Addr0A(data[226]);
            _ParsePage42Addr0B(data[227]);
            _ParsePage42Addr0C(data[228]);
            _ParsePage42Addr0D(data[229]);
            _ParsePage42Addr0E(data[230]);
            _ParsePage42Addr1B(data[231]);
            _ParsePage42Addr1C(data[232]);
            _ParsePage42Addr1D(data[233]);
            _ParsePage42Addr1E(data[234]);
            _ParsePage42Addr4B(data[235]);

            _ParsePage43Addr00(data[236]);
            _ParsePage43Addr01(data[237]);
            _ParsePage43Addr02(data[238]);
            _ParsePage43Addr03(data[239]);
            _ParsePage43Addr04(data[240]);
            _ParsePage43Addr05(data[241]);
            _ParsePage43Addr06(data[242]);
            _ParsePage43Addr07(data[243]);
            _ParsePage43Addr08(data[244]);
            _ParsePage43Addr09(data[245]);
            _ParsePage43Addr0A(data[246]);
            _ParsePage43Addr0B(data[247]);
            _ParsePage43Addr0C(data[248]);
            _ParsePage43Addr0D(data[249]);
            _ParsePage43Addr0E(data[250]);
            _ParsePage43Addr1B(data[251]);
            _ParsePage43Addr1C(data[252]);
            _ParsePage43Addr1D(data[253]);
            _ParsePage43Addr1E(data[254]);
            _ParsePage43Addr4B(data[255]);

        exit:
            reading = false;
            bReadAll.Text = "Read All";
            bReadAll.Enabled = true;
        }

        private int _WritePage00Addr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbReset.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(2, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbReset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr02() < 0)
                return;
        }

        private int _WritePage00Addr03()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;

            if (cbIntrptPadPolarity.Checked == true)
                data[0] |= 0x80;

            if (cbIntrptOen.Checked == true)
                data[0] |= 0x40;

            if (cbI2cAddressMode.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbRssiChSel.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            rv = I2cWrite(3, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbIntrptPadPolarity_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr03() < 0)
                return;
        }

        private void cbIntrptOen_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr03() < 0)
                return;
        }

        private void cbI2cAddressMode_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr03() < 0)
                return;
        }

        private void cbRssiChSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr03() < 0)
                return;
        }

        private int _WritePage00Addr04()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0;

            if (cbLpmodePadPolarity.Checked == true)
                data[0] |= 0x08;

            if (cbForceLpmodePin.Checked == true)
                data[0] |= 0x02;

            if (cbSetLpmode.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(4, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLpmodePadPolarity_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr04() < 0)
                return;
        }

        private void cbForceLpmodePin_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr04() < 0)
                return;
        }

        private void cbSetLpmode_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr04() < 0)
                return;
        }

        private int _WritePage00Addr05()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbDeviceIdProgCode.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDeviceIdProgMode.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(5, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDeviceIdProgCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr05() < 0)
                return;
        }

        private void cbDeviceIdProgMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr05() < 0)
                return;
        }

        private int _WritePage00Addr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbDeviceIdValue.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            rv = I2cWrite(6, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDeviceIdValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage00Addr06() < 0)
                return;
        }

        private int _WritePage10Addr08()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x20; //Default

            if (cbTsDisable.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(8, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTsDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr08() < 0)
                return;
        }

        private int _WritePage10Addr30()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = data[0] = 0;
            bTmp = Convert.ToByte(cbPllPdbClkbuf.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            if (cbPllPdbMonclk.Checked == true)
                data[0] |= 0x02;

            if (cbPllPdb.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(9, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbPllPdbClkbuf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr30() < 0)
                return;
        }

        private void cbPllPdbMonclk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr30() < 0)
                return;
        }

        private void cbPllPdb_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr30() < 0)
                return;
        }

        private int _WritePage10Addr31()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x04; //Default
            bTmp = Convert.ToByte(cbPllDivSel.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(10, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbPllDivSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr31() < 0)
                return;
        }

        private int _WritePage10Addr42()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbPrbsReset.Checked == true)
                data[0] |= 0x02;

            if (cbPrbsEnable.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(11, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbPrbsReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr42() < 0)
                return;
        }

        private void cbPrbsEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr42() < 0)
                return;
        }

        private int _WritePage10Addr44()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbPrbsInsErrB.Checked == true)
                data[0] |= 0x20;

            if (cbPrbsInsErrA.Checked == true)
                data[0] |= 0x10;

            if (cbPrbsForcePolflipHighB.Checked == true)
                data[0] |= 0x08;

            if (cbPrbsForcePolflipLowB.Checked == true)
                data[0] |= 0x04;

            if (cbPrbsForcePolflipHighA.Checked == true)
                data[0] |= 0x02;

            if (cbPrbsForcePolflipLowA.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(12, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbPrbsInsErrB_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr44() < 0)
                return;
        }

        private void cbPrbsInsErrA_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr44() < 0)
                return;
        }

        private void cbPrbsForcePolflipHighB_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr44() < 0)
                return;
        }

        private void cbPrbsForcePolflipLowB_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr44() < 0)
                return;
        }

        private void cbPrbsForcePolflipHighA_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr44() < 0)
                return;
        }

        private void cbPrbsForcePolflipLowA_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr44() < 0)
                return;
        }

        private int _WritePage10Addr45()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbPrbsEnNrz.Checked == true)
                data[0] |= 0x20;

            rv = I2cWrite(13, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbPrbsEnNrz_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr45() < 0)
                return;
        }

        private int _WritePage10Addr46()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbPrbsSelClk.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(14, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbPrbsSelClk_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr46() < 0)
                return;
        }

        private int _WritePage10Addr47()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbChkFreezeCntr.Checked == true)
                data[0] |= 0x08;

            if (cbChkReset.Checked == true)
                data[0] |= 0x04;

            if (cbChkClosedLoop.Checked == true)
                data[0] |= 0x02;

            if (cbChkEnable.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(15, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbChkFreezeCntr_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr47() < 0)
                return;
        }

        private void cbChkReset_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr47() < 0)
                return;
        }

        private void cbChkClosedLoop_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr47() < 0)
                return;
        }

        private void cbChkEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr47() < 0)
                return;
        }

        private int _WritePage10Addr48()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00;

            if (cbChkPolflipStrmB.Checked == true)
                data[0] |= 0x02;

            if (cbChkPolflipStrmA.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(16, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbChkPolflipStrmB_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr48() < 0)
                return;
        }

        private void cbChkPolflipStrmA_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage10Addr48() < 0)
                return;
        }

        private int _WritePage20Addr00()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbEqTiaToCdrCh0.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            rv = I2cWrite(40, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage21Addr00()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbEqTiaToCdrCh1.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            rv = I2cWrite(54, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage22Addr00()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbEqTiaToCdrCh2.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            rv = I2cWrite(68, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage23Addr00()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbEqTiaToCdrCh3.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            rv = I2cWrite(82, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage1FAddr00()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbEqTiaToCdrChAll.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            rv = I2cWrite(40, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(54, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(68, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(82, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbEqTiaToCdrCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage20Addr00() < 0)
                return;
        }

        private void cbEqTiaToCdrCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage21Addr00() < 0)
                return;
        }

        private void cbEqTiaToCdrCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage22Addr00() < 0)
                return;
        }

        private void cbEqTiaToCdrCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage23Addr00() < 0)
                return;
        }

        private void cbEqTiaToCdrChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage1FAddr00() < 0)
                return;
        }

        private int _WritePage20Addr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbRefAmpSwingAgcLoopCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(41, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage21Addr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbRefAmpSwingAgcLoopCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(55, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage22Addr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbRefAmpSwingAgcLoopCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(69, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage23Addr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbRefAmpSwingAgcLoopCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(83, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage1FAddr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbRefAmpSwingAgcLoopChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(41, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(55, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(69, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(83, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbRefAmpSwingAgcLoopCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage20Addr02() < 0)
                return;
        }

        private void cbRefAmpSwingAgcLoopCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage21Addr02() < 0)
                return;
        }

        private void cbRefAmpSwingAgcLoopCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage22Addr02() < 0)
                return;
        }

        private void cbRefAmpSwingAgcLoopCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage23Addr02() < 0)
                return;
        }

        private void cbRefAmpSwingAgcLoopChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage1FAddr02() < 0)
                return;
        }

        private int _WritePage20Addr0A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTuneRefCh0.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            if (cbSelRefLowCh0.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(42, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage21Addr0A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTuneRefCh1.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            if (cbSelRefLowCh1.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(56, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage22Addr0A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTuneRefCh2.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            if (cbSelRefLowCh2.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(70, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage23Addr0A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTuneRefCh3.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            if (cbSelRefLowCh3.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(84, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage1FAddr0A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default
            bTmp = Convert.ToByte(cbTuneRefChAll.SelectedIndex);
            bTmp <<= 1;
            data[0] |= bTmp;

            if (cbSelRefLowChAll.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(42, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(56, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(70, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(84, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTuneRefCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage20Addr0A() < 0)
                return;
        }

        private void cbSelRefLowCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage20Addr0A() < 0)
                return;
        }

        private void cbTuneRefCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage21Addr0A() < 0)
                return;
        }

        private void cbSelRefLowCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage21Addr0A() < 0)
                return;
        }

        private void cbTuneRefCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage22Addr0A() < 0)
                return;
        }

        private void cbSelRefLowCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage22Addr0A() < 0)
                return;
        }

        private void cbTuneRefCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage23Addr0A() < 0)
                return;
        }

        private void cbSelRefLowCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage23Addr0A() < 0)
                return;
        }

        private void cbTuneRefChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage1FAddr0A() < 0)
                return;
        }

        private void cbSelRefLowChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage1FAddr0A() < 0)
                return;
        }

        private int _WritePage20Addr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbSquelchOnLosCh0.Checked == true)
                data[0] |= 0x80;

            if (cbLosChMaskCh0.Checked == true)
                data[0] |= 0x40;

            if (cbLosChStickyCh0.Checked == true)
                data[0] |= 0x20;

            if (cbLosChCh0.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbLosHystForRssiCh0.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            rv = I2cWrite(43, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage21Addr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbSquelchOnLosCh1.Checked == true)
                data[0] |= 0x80;

            if (cbLosChMaskCh1.Checked == true)
                data[0] |= 0x40;

            if (cbLosChStickyCh1.Checked == true)
                data[0] |= 0x20;

            if (cbLosChCh1.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbLosHystForRssiCh1.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            rv = I2cWrite(57, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage22Addr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbSquelchOnLosCh2.Checked == true)
                data[0] |= 0x80;

            if (cbLosChMaskCh2.Checked == true)
                data[0] |= 0x40;

            if (cbLosChStickyCh2.Checked == true)
                data[0] |= 0x20;

            if (cbLosChCh2.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbLosHystForRssiCh2.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            rv = I2cWrite(71, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage23Addr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbSquelchOnLosCh3.Checked == true)
                data[0] |= 0x80;

            if (cbLosChMaskCh3.Checked == true)
                data[0] |= 0x40;

            if (cbLosChStickyCh3.Checked == true)
                data[0] |= 0x20;

            if (cbLosChCh3.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbLosHystForRssiCh3.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            rv = I2cWrite(85, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage1FAddr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbSquelchOnLosChAll.Checked == true)
                data[0] |= 0x80;

            if (cbLosChMaskChAll.Checked == true)
                data[0] |= 0x40;

            if (cbLosChStickyChAll.Checked == true)
                data[0] |= 0x20;

            if (cbLosChChAll.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbLosHystForRssiChAll.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            rv = I2cWrite(43, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(57, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(71, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(85, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbSquelchOnLosCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage20Addr19() < 0)
                return;
        }

        private void cbLosChMaskCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage20Addr19() < 0)
                return;
        }

        private void cbLosChStickyCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage20Addr19() < 0)
                return;
        }

        private void cbLosChCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage20Addr19() < 0)
                return;
        }

        private void cbLosHystForRssiCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage20Addr19() < 0)
                return;
        }

        private void cbSquelchOnLosCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage21Addr19() < 0)
                return;
        }

        private void cbLosChMaskCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage21Addr19() < 0)
                return;
        }

        private void cbLosChStickyCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage21Addr19() < 0)
                return;
        }

        private void cbLosChCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage21Addr19() < 0)
                return;
        }

        private void cbLosHystForRssiCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage21Addr19() < 0)
                return;
        }

        private void cbSquelchOnLosCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage22Addr19() < 0)
                return;
        }

        private void cbLosChMaskCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage22Addr19() < 0)
                return;
        }

        private void cbLosChStickyCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage22Addr19() < 0)
                return;
        }

        private void cbLosChCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage22Addr19() < 0)
                return;
        }

        private void cbLosHystForRssiCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage22Addr19() < 0)
                return;
        }

        private void cbSquelchOnLosCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage23Addr19() < 0)
                return;
        }

        private void cbLosChMaskCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage23Addr19() < 0)
                return;
        }

        private void cbLosChStickyCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage23Addr19() < 0)
                return;
        }

        private void cbLosChCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage23Addr19() < 0)
                return;
        }

        private void cbLosHystForRssiCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage23Addr19() < 0)
                return;
        }

        private void cbSquelchOnLosChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage1FAddr19() < 0)
                return;
        }

        private void cbLosChMaskChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage1FAddr19() < 0)
                return;
        }

        private void cbLosChStickyChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage1FAddr19() < 0)
                return;
        }

        private void cbLosChChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage1FAddr19() < 0)
                return;
        }

        private void cbLosHystForRssiChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage1FAddr19() < 0)
                return;
        }

        private int _WritePage20Addr1A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbLosThreshCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(44, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage21Addr1A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbLosThreshCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(58, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage22Addr1A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbLosThreshCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(72, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage23Addr1A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbLosThreshCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(86, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage1FAddr1A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbLosThreshChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(44, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(58, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(72, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(86, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosThreshCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage20Addr1A() < 0)
                return;
        }

        private void cbLosThreshCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage21Addr1A() < 0)
                return;
        }

        private void cbLosThreshCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage22Addr1A() < 0)
                return;
        }

        private void cbLosThreshCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage23Addr1A() < 0)
                return;
        }

        private void cbLosThreshChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage1FAddr1A() < 0)
                return;
        }

        private int _WritePage20Addr90_91()
        {
            byte[] data;
            int rv;

            data = BitConverter.GetBytes(cbAgcValueCh0.SelectedIndex);

            rv = I2cWrite(45, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage21Addr90_91()
        {
            byte[] data;
            int rv;

            data = BitConverter.GetBytes(cbAgcValueCh1.SelectedIndex);

            rv = I2cWrite(59, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage22Addr90_91()
        {
            byte[] data;
            int rv;

            data = BitConverter.GetBytes(cbAgcValueCh2.SelectedIndex);

            rv = I2cWrite(73, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage23Addr90_91()
        {
            byte[] data;
            int rv;

            data = BitConverter.GetBytes(cbAgcValueCh3.SelectedIndex);

            rv = I2cWrite(87, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage1FAddr90_91()
        {
            byte[] data;
            int rv;

            data = BitConverter.GetBytes(cbAgcValueChAll.SelectedIndex);

            rv = I2cWrite(45, 2, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(59, 2, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(73, 2, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(87, 2, data);
            if (rv < 0)
                return -1;


            return 0;
        }

        private void cbAgcValueCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage20Addr90_91() < 0)
                return;
        }

        private void cbAgcValueCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage21Addr90_91() < 0)
                return;
        }

        private void cbAgcValueCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage22Addr90_91() < 0)
                return;
        }

        private void cbAgcValueCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage23Addr90_91() < 0)
                return;
        }

        private void cbAgcValueChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage1FAddr90_91() < 0)
                return;
        }

        private int _WritePage20Addr92()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbAgcLoopEnableCh0.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(47, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage21Addr92()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbAgcLoopEnableCh1.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(61, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage22Addr92()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbAgcLoopEnableCh2.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(75, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage23Addr92()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbAgcLoopEnableCh3.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(89, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage1FAddr92()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbAgcLoopEnableChAll.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(47, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(61, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(75, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(89, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAgcLoopEnableCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage20Addr92() < 0)
                return;
        }

        private void cbAgcLoopEnableCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage21Addr92() < 0)
                return;
        }

        private void cbAgcLoopEnableCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage22Addr92() < 0)
                return;
        }

        private void cbAgcLoopEnableCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage23Addr92() < 0)
                return;
        }

        private void cbAgcLoopEnableChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage1FAddr92() < 0)
                return;
        }

        private int _WritePage20AddrB0_B1()
        {
            byte[] data;
            int rv;

            data = BitConverter.GetBytes(cbDcdValueCh0.SelectedIndex);

            rv = I2cWrite(48, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage21AddrB0_B1()
        {
            byte[] data;
            int rv;

            data = BitConverter.GetBytes(cbDcdValueCh1.SelectedIndex);

            rv = I2cWrite(62, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage22AddrB0_B1()
        {
            byte[] data;
            int rv;

            data = BitConverter.GetBytes(cbDcdValueCh2.SelectedIndex);

            rv = I2cWrite(76, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage23AddrB0_B1()
        {
            byte[] data;
            int rv;

            data = BitConverter.GetBytes(cbDcdValueCh3.SelectedIndex);

            rv = I2cWrite(90, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage1FAddrB0_B1()
        {
            byte[] data;
            int rv;

            data = BitConverter.GetBytes(cbDcdValueChAll.SelectedIndex);

            rv = I2cWrite(48, 2, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(62, 2, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(76, 2, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(90, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDcdValueCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage20AddrB0_B1() < 0)
                return;
        }

        private void cbDcdValueCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage21AddrB0_B1() < 0)
                return;
        }

        private void cbDcdValueCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage22AddrB0_B1() < 0)
                return;
        }

        private void cbDcdValueCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage23AddrB0_B1() < 0)
                return;
        }

        private void cbDcdValueChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage1FAddrB0_B1() < 0)
                return;
        }

        private int _WritePage20AddrB2()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbDcdFilterCh0.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(50, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage21AddrB2()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbDcdFilterCh1.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(64, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage22AddrB2()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbDcdFilterCh2.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(78, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage23AddrB2()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbDcdFilterCh3.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(92, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage1FAddrB2()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbDcdFilterChAll.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(50, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(64, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(78, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(92, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDcdFilterCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage20AddrB2() < 0)
                return;
        }

        private void cbDcdFilterCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage21AddrB2() < 0)
                return;
        }

        private void cbDcdFilterCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage22AddrB2() < 0)
                return;
        }

        private void cbDcdFilterCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage23AddrB2() < 0)
                return;
        }

        private void cbDcdFilterChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage1FAddrB2() < 0)
                return;
        }

        private int _WritePage20AddrD0_D1()
        {
            byte[] data;
            int rv;

            data = BitConverter.GetBytes(cbBiasValueCh0.SelectedIndex);

            rv = I2cWrite(51, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage21AddrD0_D1()
        {
            byte[] data;
            int rv;

            data = BitConverter.GetBytes(cbBiasValueCh1.SelectedIndex);

            rv = I2cWrite(65, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage22AddrD0_D1()
        {
            byte[] data;
            int rv;

            data = BitConverter.GetBytes(cbBiasValueCh2.SelectedIndex);

            rv = I2cWrite(79, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage23AddrD0_D1()
        {
            byte[] data;
            int rv;

            data = BitConverter.GetBytes(cbBiasValueCh3.SelectedIndex);

            rv = I2cWrite(93, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage1FAddrD0_D1()
        {
            byte[] data;
            int rv;

            data = BitConverter.GetBytes(cbBiasValueChAll.SelectedIndex);

            rv = I2cWrite(51, 2, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(65, 2, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(79, 2, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(93, 2, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbBiasValueCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage20AddrD0_D1() < 0)
                return;
        }

        private void cbBiasValueCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage21AddrD0_D1() < 0)
                return;
        }

        private void cbBiasValueCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage22AddrD0_D1() < 0)
                return;
        }

        private void cbBiasValueCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage23AddrD0_D1() < 0)
                return;
        }

        private void cbBiasValueChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage1FAddrD0_D1() < 0)
                return;
        }

        private int _WritePage20AddrD2()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbFilter2FilterEnableCh0.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(53, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage21AddrD2()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbFilter2FilterEnableCh1.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(67, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage22AddrD2()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbFilter2FilterEnableCh2.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(81, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage23AddrD2()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbFilter2FilterEnableCh3.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(95, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage1FAddrD2()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbFilter2FilterEnableChAll.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(53, 1, data);
            if (rv < 0)
                return -1;
            
            rv = I2cWrite(67, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(81, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(95, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbFilter2FilterEnableCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage20AddrD2() < 0)
                return;
        }

        private void cbFilter2FilterEnableCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage21AddrD2() < 0)
                return;
        }

        private void cbFilter2FilterEnableCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage22AddrD2() < 0)
                return;
        }

        private void cbFilter2FilterEnableCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage23AddrD2() < 0)
                return;
        }

        private void cbFilter2FilterEnableChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage1FAddrD2() < 0)
                return;
        }

        private int _WritePage30Addr08()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbLosAlarmStickyCh0.Checked == true)
                data[0] |= 0x10;

            if (cbLolAlarmStickyCh0.Checked == true)
                data[0] |= 0x08;

            if (cbLosPdCh0.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(96, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr08()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbLosAlarmStickyCh1.Checked == true)
                data[0] |= 0x10;

            if (cbLolAlarmStickyCh1.Checked == true)
                data[0] |= 0x08;

            if (cbLosPdCh1.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(116, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr08()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbLosAlarmStickyCh2.Checked == true)
                data[0] |= 0x10;

            if (cbLolAlarmStickyCh2.Checked == true)
                data[0] |= 0x08;

            if (cbLosPdCh2.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(136, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr08()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbLosAlarmStickyCh3.Checked == true)
                data[0] |= 0x10;

            if (cbLolAlarmStickyCh3.Checked == true)
                data[0] |= 0x08;

            if (cbLosPdCh3.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(156, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr08()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbLosAlarmStickyChAll.Checked == true)
                data[0] |= 0x10;

            if (cbLolAlarmStickyChAll.Checked == true)
                data[0] |= 0x08;

            if (cbLosPdChAll.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(96, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(116, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(136, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(156, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosAlarmStickyCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr08() < 0)
                return;
        }

        private void cbLolAlarmStickyCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr08() < 0)
                return;
        }

        private void cbLosPdCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr08() < 0)
                return;
        }

        private void cbLosAlarmStickyCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr08() < 0)
                return;
        }

        private void cbLolAlarmStickyCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr08() < 0)
                return;
        }

        private void cbLosPdCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr08() < 0)
                return;
        }

        private void cbLosAlarmStickyCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr08() < 0)
                return;
        }

        private void cbLolAlarmStickyCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr08() < 0)
                return;
        }

        private void cbLosPdCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr08() < 0)
                return;
        }

        private void cbLosAlarmStickyCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr08() < 0)
                return;
        }

        private void cbLolAlarmStickyCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr08() < 0)
                return;
        }

        private void cbLosPdCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr08() < 0)
                return;
        }

        private void cbLosAlarmStickyChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr08() < 0)
                return;
        }

        private void cbLolAlarmStickyChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr08() < 0)
                return;
        }

        private void cbLosPdChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr08() < 0)
                return;
        }

        private int _WritePage30Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbLosHystCh0.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosVthCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(97, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbLosHystCh1.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosVthCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(117, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbLosHystCh2.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosVthCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(137, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbLosHystCh3.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosVthCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(157, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbLosHystChAll.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbLosVthChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(97, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(117, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(137, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(157, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosHystCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr09() < 0)
                return;
        }

        private void cbLosVthCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr09() < 0)
                return;
        }

        private void cbLosHystCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr09() < 0)
                return;
        }

        private void cbLosVthCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr09() < 0)
                return;
        }

        private void cbLosHystCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr09() < 0)
                return;
        }

        private void cbLosVthCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr09() < 0)
                return;
        }

        private void cbLosHystCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr09() < 0)
                return;
        }

        private void cbLosVthCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr09() < 0)
                return;
        }

        private void cbLosHystChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr09() < 0)
                return;
        }

        private void cbLosVthChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr09() < 0)
                return;
        }

        private int _WritePage30Addr0C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbLosForceLosvalCh0.Checked == true)
                data[0] |= 0x10;

            if (cbLosForceLosCh0.Checked == true)
                data[0] |= 0x08;

            if (cbLolMaskCh0.Checked == true)
                data[0] |= 0x04;

            if (cbLosMaskCh0.Checked == true)
                data[0] |= 0x02;

            rv = I2cWrite(98, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr0C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbLosForceLosvalCh1.Checked == true)
                data[0] |= 0x10;

            if (cbLosForceLosCh1.Checked == true)
                data[0] |= 0x08;

            if (cbLolMaskCh1.Checked == true)
                data[0] |= 0x04;

            if (cbLosMaskCh1.Checked == true)
                data[0] |= 0x02;

            rv = I2cWrite(118, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr0C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbLosForceLosvalCh2.Checked == true)
                data[0] |= 0x10;

            if (cbLosForceLosCh2.Checked == true)
                data[0] |= 0x08;

            if (cbLolMaskCh2.Checked == true)
                data[0] |= 0x04;

            if (cbLosMaskCh2.Checked == true)
                data[0] |= 0x02;

            rv = I2cWrite(138, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr0C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbLosForceLosvalCh3.Checked == true)
                data[0] |= 0x10;

            if (cbLosForceLosCh3.Checked == true)
                data[0] |= 0x08;

            if (cbLolMaskCh3.Checked == true)
                data[0] |= 0x04;

            if (cbLosMaskCh3.Checked == true)
                data[0] |= 0x02;

            rv = I2cWrite(158, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr0C()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbLosForceLosvalChAll.Checked == true)
                data[0] |= 0x10;

            if (cbLosForceLosChAll.Checked == true)
                data[0] |= 0x08;

            if (cbLolMaskChAll.Checked == true)
                data[0] |= 0x04;

            if (cbLosMaskChAll.Checked == true)
                data[0] |= 0x02;

            rv = I2cWrite(98, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(118, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(138, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(158, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbLosForceLosvalCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr0C() < 0)
                return;
        }

        private void cbLosForceLosCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr0C() < 0)
                return;
        }

        private void cbLolMaskCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr0C() < 0)
                return;
        }

        private void cbLosMaskCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr0C() < 0)
                return;
        }

        private void cbLosForceLosvalCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr0C() < 0)
                return;
        }

        private void cbLosForceLosCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr0C() < 0)
                return;
        }

        private void cbLolMaskCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr0C() < 0)
                return;
        }

        private void cbLosMaskCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr0C() < 0)
                return;
        }

        private void cbLosForceLosvalCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr0C() < 0)
                return;
        }

        private void cbLosForceLosCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr0C() < 0)
                return;
        }

        private void cbLolMaskCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr0C() < 0)
                return;
        }

        private void cbLosMaskCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr0C() < 0)
                return;
        }

        private void cbLosForceLosvalCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr0C() < 0)
                return;
        }

        private void cbLosForceLosCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr0C() < 0)
                return;
        }

        private void cbLolMaskCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr0C() < 0)
                return;
        }

        private void cbLosMaskCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr0C() < 0)
                return;
        }

        private void cbLosForceLosvalChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr0C() < 0)
                return;
        }

        private void cbLosForceLosChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr0C() < 0)
                return;
        }

        private void cbLolMaskChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr0C() < 0)
                return;
        }

        private void cbLosMaskChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr0C() < 0)
                return;
        }

        private int _WritePage30Addr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid0ForceCh0.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac7baPdbCh0.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac7baDac0InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(99, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid0ForceCh1.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac7baPdbCh1.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac7baDac0InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(119, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid0ForceCh2.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac7baPdbCh2.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac7baDac0InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(139, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid0ForceCh3.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac7baPdbCh3.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac7baDac0InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(159, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr15()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid0ForceChAll.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac7baPdbChAll.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac7baDac0InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(99, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(119, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(139, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(159, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsMid0ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr15() < 0)
                return;
        }

        private void cbDacEqDac7baPdbCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr15() < 0)
                return;
        }

        private void cbDacEqDac7baDac0InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr15() < 0)
                return;
        }

        private void cbThrsMid0ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr15() < 0)
                return;
        }

        private void cbDacEqDac7baPdbCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr15() < 0)
                return;
        }

        private void cbDacEqDac7baDac0InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr15() < 0)
                return;
        }

        private void cbThrsMid0ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr15() < 0)
                return;
        }

        private void cbDacEqDac7baPdbCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr15() < 0)
                return;
        }

        private void cbDacEqDac7baDac0InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr15() < 0)
                return;
        }

        private void cbThrsMid0ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr15() < 0)
                return;
        }

        private void cbDacEqDac7baPdbCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr15() < 0)
                return;
        }

        private void cbDacEqDac7baDac0InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr15() < 0)
                return;
        }

        private void cbThrsMid0ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr15() < 0)
                return;
        }

        private void cbDacEqDac7baPdbChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr15() < 0)
                return;
        }

        private void cbDacEqDac7baDac0InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr15() < 0)
                return;
        }

        private int _WritePage30Addr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid1ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac7baDac1InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(100, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid1ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac7baDac1InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(120, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid1ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac7baDac1InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(140, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid1ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac7baDac1InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(160, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr16()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsMid1ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac7baDac1InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(100, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(120, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(140, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(160, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsMid1ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr16() < 0)
                return;
        }

        private void cbDacEqDac7baDac1InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr16() < 0)
                return;
        }

        private void cbThrsMid1ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr16() < 0)
                return;
        }

        private void cbDacEqDac7baDac1InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr16() < 0)
                return;
        }

        private void cbThrsMid1ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr16() < 0)
                return;
        }

        private void cbDacEqDac7baDac1InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr16() < 0)
                return;
        }

        private void cbThrsMid1ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr16() < 0)
                return;
        }

        private void cbDacEqDac7baDac1InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr16() < 0)
                return;
        }

        private void cbThrsMid1ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr16() < 0)
                return;
        }

        private void cbDacEqDac7baDac1InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr16() < 0)
                return;
        }

        private int _WritePage30Addr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop0ForceCh0.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8baPdbCh0.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8baDac0InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(101, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop0ForceCh1.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8baPdbCh1.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8baDac0InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(121, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop0ForceCh2.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8baPdbCh2.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8baDac0InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(141, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop0ForceCh3.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8baPdbCh3.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8baDac0InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(161, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr17()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop0ForceChAll.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8baPdbChAll.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8baDac0InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(101, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(121, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(141, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(161, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop0ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr17() < 0)
                return;
        }

        private void cbDacEqDac8baPdbCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr17() < 0)
                return;
        }

        private void cbDacEqDac8baDac0InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr17() < 0)
                return;
        }

        private void cbThrsTop0ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr17() < 0)
                return;
        }

        private void cbDacEqDac8baPdbCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr17() < 0)
                return;
        }

        private void cbDacEqDac8baDac0InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr17() < 0)
                return;
        }

        private void cbThrsTop0ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr17() < 0)
                return;
        }

        private void cbDacEqDac8baPdbCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr17() < 0)
                return;
        }

        private void cbDacEqDac8baDac0InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr17() < 0)
                return;
        }

        private void cbThrsTop0ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr17() < 0)
                return;
        }

        private void cbDacEqDac8baPdbCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr17() < 0)
                return;
        }

        private void cbDacEqDac8baDac0InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr17() < 0)
                return;
        }

        private void cbThrsTop0ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr17() < 0)
                return;
        }

        private void cbDacEqDac8baPdbChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr17() < 0)
                return;
        }

        private void cbDacEqDac8baDac0InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr17() < 0)
                return;
        }

        private int _WritePage30Addr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop1ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac1InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(102, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop1ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac1InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(122, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop1ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac1InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(142, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop1ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac1InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(162, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr18()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop1ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac1InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(102, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(122, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(142, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(162, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop1ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr18() < 0)
                return;
        }

        private void cbDacEqDac8baDac1InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr18() < 0)
                return;
        }

        private void cbThrsTop1ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr18() < 0)
                return;
        }

        private void cbDacEqDac8baDac1InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr18() < 0)
                return;
        }

        private void cbThrsTop1ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr18() < 0)
                return;
        }

        private void cbDacEqDac8baDac1InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr18() < 0)
                return;
        }

        private void cbThrsTop1ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr18() < 0)
                return;
        }

        private void cbDacEqDac8baDac1InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr18() < 0)
                return;
        }

        private void cbThrsTop1ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr18() < 0)
                return;
        }

        private void cbDacEqDac8baDac1InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr18() < 0)
                return;
        }

        private int _WritePage30Addr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop2ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac2InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(103, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop2ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac2InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(123, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop2ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac2InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(143, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop2ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac2InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(163, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr19()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop2ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac2InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(103, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(123, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(143, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(163, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop2ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr19() < 0)
                return;
        }

        private void cbDacEqDac8baDac2InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr19() < 0)
                return;
        }

        private void cbThrsTop2ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr19() < 0)
                return;
        }

        private void cbDacEqDac8baDac2InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr19() < 0)
                return;
        }

        private void cbThrsTop2ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr19() < 0)
                return;
        }

        private void cbDacEqDac8baDac2InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr19() < 0)
                return;
        }

        private void cbThrsTop2ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr19() < 0)
                return;
        }

        private void cbDacEqDac8baDac2InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr19() < 0)
                return;
        }

        private void cbThrsTop2ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr19() < 0)
                return;
        }

        private void cbDacEqDac8baDac2InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr19() < 0)
                return;
        }

        private int _WritePage30Addr1A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop3ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac3InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(104, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr1A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop3ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac3InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(124, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr1A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop3ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac3InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(144, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr1A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop3ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac3InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(164, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr1A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop3ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac3InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(104, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(124, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(144, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(164, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop3ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1A() < 0)
                return;
        }

        private void cbDacEqDac8baDac3InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1A() < 0)
                return;
        }

        private void cbThrsTop3ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1A() < 0)
                return;
        }

        private void cbDacEqDac8baDac3InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1A() < 0)
                return;
        }

        private void cbThrsTop3ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1A() < 0)
                return;
        }

        private void cbDacEqDac8baDac3InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1A() < 0)
                return;
        }

        private void cbThrsTop3ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1A() < 0)
                return;
        }

        private void cbDacEqDac8baDac3InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1A() < 0)
                return;
        }

        private void cbThrsTop3ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1A() < 0)
                return;
        }

        private void cbDacEqDac8baDac3InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1A() < 0)
                return;
        }

        private int _WritePage30Addr1B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop4ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac4InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(105, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr1B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop4ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac4InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(125, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr1B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop4ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac4InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(145, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr1B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop4ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac4InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(165, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr1B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsTop4ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8baDac4InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(105, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(125, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(145, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(165, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsTop4ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1B() < 0)
                return;
        }

        private void cbDacEqDac8baDac4InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1B() < 0)
                return;
        }

        private void cbThrsTop4ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1B() < 0)
                return;
        }

        private void cbDacEqDac8baDac4InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1B() < 0)
                return;
        }

        private void cbThrsTop4ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1B() < 0)
                return;
        }

        private void cbDacEqDac8baDac4InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1B() < 0)
                return;
        }

        private void cbThrsTop4ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1B() < 0)
                return;
        }

        private void cbDacEqDac8baDac4InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1B() < 0)
                return;
        }

        private void cbThrsTop4ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1B() < 0)
                return;
        }

        private void cbDacEqDac8baDac4InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1B() < 0)
                return;
        }

        private int _WritePage30Addr1C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot0ForceCh0.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8bbPdbCh0.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac0InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(106, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr1C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot0ForceCh1.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8bbPdbCh1.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac0InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(126, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr1C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot0ForceCh2.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8bbPdbCh2.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac0InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(146, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr1C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot0ForceCh3.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8bbPdbCh3.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac0InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(166, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr1C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot0ForceChAll.Checked == true)
                data[0] |= 0x80;

            if (cbDacEqDac8bbPdbChAll.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac0InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(106, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(126, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(146, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(166, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot0ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbPdbCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbDac0InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1C() < 0)
                return;
        }

        private void cbThrsBot0ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbPdbCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbDac0InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1C() < 0)
                return;
        }

        private void cbThrsBot0ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbPdbCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbDac0InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1C() < 0)
                return;
        }

        private void cbThrsBot0ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbPdbCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbDac0InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1C() < 0)
                return;
        }

        private void cbThrsBot0ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbPdbChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1C() < 0)
                return;
        }

        private void cbDacEqDac8bbDac0InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1C() < 0)
                return;
        }

        private int _WritePage30Addr1D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot1ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac1InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(107, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr1D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot1ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac1InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(127, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr1D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot1ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac1InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(147, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr1D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot1ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac1InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(167, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr1D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot1ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac1InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(107, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(127, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(147, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(167, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot1ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1D() < 0)
                return;
        }

        private void cbDacEqDac8bbDac1InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1D() < 0)
                return;
        }

        private void cbThrsBot1ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1D() < 0)
                return;
        }

        private void cbDacEqDac8bbDac1InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1D() < 0)
                return;
        }

        private void cbThrsBot1ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1D() < 0)
                return;
        }

        private void cbDacEqDac8bbDac1InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1D() < 0)
                return;
        }

        private void cbThrsBot1ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1D() < 0)
                return;
        }

        private void cbDacEqDac8bbDac1InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1D() < 0)
                return;
        }

        private void cbThrsBot1ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1D() < 0)
                return;
        }

        private void cbDacEqDac8bbDac1InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1D() < 0)
                return;
        }

        private int _WritePage30Addr1E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot2ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac2InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(108, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr1E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot2ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac2InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(128, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr1E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot2ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac2InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(148, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr1E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot2ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac2InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(168, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr1E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot2ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac2InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(108, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(128, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(148, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(168, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot2ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1E() < 0)
                return;
        }

        private void cbDacEqDac8bbDac2InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1E() < 0)
                return;
        }

        private void cbThrsBot2ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1E() < 0)
                return;
        }

        private void cbDacEqDac8bbDac2InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1E() < 0)
                return;
        }

        private void cbThrsBot2ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1E() < 0)
                return;
        }

        private void cbDacEqDac8bbDac2InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1E() < 0)
                return;
        }

        private void cbThrsBot2ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1E() < 0)
                return;
        }

        private void cbDacEqDac8bbDac2InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1E() < 0)
                return;
        }

        private void cbThrsBot2ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1E() < 0)
                return;
        }

        private void cbDacEqDac8bbDac2InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1E() < 0)
                return;
        }

        private int _WritePage30Addr1F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot3ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac3InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(109, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr1F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot3ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac3InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(129, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr1F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot3ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac3InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(149, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr1F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot3ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac3InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(169, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr1F()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot3ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac3InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(109, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(129, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(149, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(169, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot3ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1F() < 0)
                return;
        }

        private void cbDacEqDac8bbDac3InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr1F() < 0)
                return;
        }

        private void cbThrsBot3ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1F() < 0)
                return;
        }

        private void cbDacEqDac8bbDac3InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr1F() < 0)
                return;
        }

        private void cbThrsBot3ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1F() < 0)
                return;
        }

        private void cbDacEqDac8bbDac3InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr1F() < 0)
                return;
        }

        private void cbThrsBot3ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1F() < 0)
                return;
        }

        private void cbDacEqDac8bbDac3InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr1F() < 0)
                return;
        }

        private void cbThrsBot3ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1F() < 0)
                return;
        }

        private void cbDacEqDac8bbDac3InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr1F() < 0)
                return;
        }

        private int _WritePage30Addr20()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot4ForceCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac4InCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(110, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr20()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot4ForceCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac4InCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(130, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr20()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot4ForceCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac4InCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(150, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr20()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot4ForceCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac4InCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(170, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr20()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbThrsBot4ForceChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbDacEqDac8bbDac4InChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(110, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(130, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(150, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(170, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbThrsBot4ForceCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr20() < 0)
                return;
        }

        private void cbDacEqDac8bbDac4InCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr20() < 0)
                return;
        }

        private void cbThrsBot4ForceCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr20() < 0)
                return;
        }

        private void cbDacEqDac8bbDac4InCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr20() < 0)
                return;
        }

        private void cbThrsBot4ForceCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr20() < 0)
                return;
        }

        private void cbDacEqDac8bbDac4InCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr20() < 0)
                return;
        }

        private void cbThrsBot4ForceCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr20() < 0)
                return;
        }

        private void cbDacEqDac8bbDac4InCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr20() < 0)
                return;
        }

        private void cbThrsBot4ForceChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr20() < 0)
                return;
        }

        private void cbDacEqDac8bbDac4InChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr20() < 0)
                return;
        }

        private int _WritePage30Addr41()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbCdrLbwAdjCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(111, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr41()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbCdrLbwAdjCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(131, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr41()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbCdrLbwAdjCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(151, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr41()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbCdrLbwAdjCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(171, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr41()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbCdrLbwAdjChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(111, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(131, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(151, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(171, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbCdrLbwAdjCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr41() < 0)
                return;
        }

        private void cbCdrLbwAdjCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr41() < 0)
                return;
        }

        private void cbCdrLbwAdjCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr41() < 0)
                return;
        }

        private void cbCdrLbwAdjCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr41() < 0)
                return;
        }

        private void cbCdrLbwAdjChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr41() < 0)
                return;
        }

        private int _WritePage30Addr46()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbDacCdrFastlockClockDelayCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(112, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr46()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbDacCdrFastlockClockDelayCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(132, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr46()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbDacCdrFastlockClockDelayCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(152, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr46()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbDacCdrFastlockClockDelayCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(172, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr46()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbDacCdrFastlockClockDelayChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(112, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(132, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(152, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(172, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrFastlockClockDelayCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr46() < 0)
                return;
        }

        private void cbDacCdrFastlockClockDelayCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr46() < 0)
                return;
        }

        private void cbDacCdrFastlockClockDelayCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr46() < 0)
                return;
        }

        private void cbDacCdrFastlockClockDelayCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr46() < 0)
                return;
        }

        private void cbDacCdrFastlockClockDelayChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr46() < 0)
                return;
        }

        private int _WritePage30Addr47()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbDacCdrTopRetimeDelayCh0.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrBotRetimeDelayCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(113, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr47()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbDacCdrTopRetimeDelayCh1.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrBotRetimeDelayCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(133, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr47()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbDacCdrTopRetimeDelayCh2.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrBotRetimeDelayCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(153, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr47()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbDacCdrTopRetimeDelayCh3.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrBotRetimeDelayCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(173, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr47()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbDacCdrTopRetimeDelayChAll.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrBotRetimeDelayChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(113, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(133, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(153, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(173, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrTopRetimeDelayCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr47() < 0)
                return;
        }

        private void cbDacCdrBotRetimeDelayCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr47() < 0)
                return;
        }

        private void cbDacCdrTopRetimeDelayCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr47() < 0)
                return;
        }

        private void cbDacCdrBotRetimeDelayCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr47() < 0)
                return;
        }

        private void cbDacCdrTopRetimeDelayCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr47() < 0)
                return;
        }

        private void cbDacCdrBotRetimeDelayCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr47() < 0)
                return;
        }

        private void cbDacCdrTopRetimeDelayCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr47() < 0)
                return;
        }

        private void cbDacCdrBotRetimeDelayCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr47() < 0)
                return;
        }

        private void cbDacCdrTopRetimeDelayChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr47() < 0)
                return;
        }

        private void cbDacCdrBotRetimeDelayChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr47() < 0)
                return;
        }

        private int _WritePage30Addr48()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbDacCdrMidRetimeDelayCh0.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrPhaseDetectorDelayCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(114, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage31Addr48()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbDacCdrMidRetimeDelayCh1.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrPhaseDetectorDelayCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(134, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage32Addr48()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbDacCdrMidRetimeDelayCh2.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrPhaseDetectorDelayCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(154, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage33Addr48()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbDacCdrMidRetimeDelayCh3.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrPhaseDetectorDelayCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(174, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage2FAddr48()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            bTmp = Convert.ToByte(cbDacCdrMidRetimeDelayChAll.SelectedIndex);
            bTmp <<= 4;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbDacCdrPhaseDetectorDelayChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(114, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(134, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(154, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(174, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacCdrMidRetimeDelayCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr48() < 0)
                return;
        }

        private void cbDacCdrPhaseDetectorDelayCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage30Addr48() < 0)
                return;
        }

        private void cbDacCdrMidRetimeDelayCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr48() < 0)
                return;
        }

        private void cbDacCdrPhaseDetectorDelayCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage31Addr48() < 0)
                return;
        }

        private void cbDacCdrMidRetimeDelayCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr48() < 0)
                return;
        }

        private void cbDacCdrPhaseDetectorDelayCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage32Addr48() < 0)
                return;
        }

        private void cbDacCdrMidRetimeDelayCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr48() < 0)
                return;
        }

        private void cbDacCdrPhaseDetectorDelayCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage33Addr48() < 0)
                return;
        }

        private void cbDacCdrMidRetimeDelayChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr48() < 0)
                return;
        }

        private void cbDacCdrPhaseDetectorDelayChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage2FAddr48() < 0)
                return;
        }

        private int _WritePage40Addr00()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPolinvTopCh0.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacMain6bPdbTopCh0.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacMain6bTopCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(176, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr00()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPolinvTopCh1.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacMain6bPdbTopCh1.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacMain6bTopCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(196, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr00()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPolinvTopCh2.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacMain6bPdbTopCh2.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacMain6bTopCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(216, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr00()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPolinvTopCh3.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacMain6bPdbTopCh3.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacMain6bTopCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(236, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr00()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPolinvTopChAll.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacMain6bPdbTopChAll.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacMain6bTopChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(176, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(196, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(216, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(236, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPolinvTopCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr00() < 0)
                return;
        }

        private void cbDacTxDacMain6bPdbTopCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr00() < 0)
                return;
        }

        private void cbDacTxDacMain6bTopCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr00() < 0)
                return;
        }

        private void cbTxPolinvTopCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr00() < 0)
                return;
        }

        private void cbDacTxDacMain6bPdbTopCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr00() < 0)
                return;
        }

        private void cbDacTxDacMain6bTopCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr00() < 0)
                return;
        }

        private void cbTxPolinvTopCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr00() < 0)
                return;
        }

        private void cbDacTxDacMain6bPdbTopCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr00() < 0)
                return;
        }

        private void cbDacTxDacMain6bTopCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr00() < 0)
                return;
        }

        private void cbTxPolinvTopCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr00() < 0)
                return;
        }

        private void cbDacTxDacMain6bPdbTopCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr00() < 0)
                return;
        }

        private void cbDacTxDacMain6bTopCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr00() < 0)
                return;
        }

        private void cbTxPolinvTopChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr00() < 0)
                return;
        }

        private void cbDacTxDacMain6bPdbTopChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr00() < 0)
                return;
        }

        private void cbDacTxDacMain6bTopChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr00() < 0)
                return;
        }

        private int _WritePage40Addr01()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPolinvMidCh0.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacMain6bPdbMidCh0.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacMain6bMidCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(177, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr01()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPolinvMidCh1.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacMain6bPdbMidCh1.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacMain6bMidCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(197, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr01()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPolinvMidCh2.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacMain6bPdbMidCh2.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacMain6bMidCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(217, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr01()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPolinvMidCh3.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacMain6bPdbMidCh3.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacMain6bMidCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(237, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr01()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPolinvMidChAll.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacMain6bPdbMidChAll.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacMain6bMidChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(177, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(197, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(217, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(237, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPolinvMidCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr01() < 0)
                return;
        }

        private void cbDacTxDacMain6bPdbMidCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr01() < 0)
                return;
        }

        private void cbDacTxDacMain6bMidCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr01() < 0)
                return;
        }

        private void cbTxPolinvMidCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr01() < 0)
                return;
        }

        private void cbDacTxDacMain6bPdbMidCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr01() < 0)
                return;
        }

        private void cbDacTxDacMain6bMidCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr01() < 0)
                return;
        }

        private void cbTxPolinvMidCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr01() < 0)
                return;
        }

        private void cbDacTxDacMain6bPdbMidCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr01() < 0)
                return;
        }

        private void cbDacTxDacMain6bMidCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr01() < 0)
                return;
        }

        private void cbTxPolinvMidCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr01() < 0)
                return;
        }

        private void cbDacTxDacMain6bPdbMidCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr01() < 0)
                return;
        }

        private void cbDacTxDacMain6bMidCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr01() < 0)
                return;
        }

        private void cbTxPolinvMidChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr01() < 0)
                return;
        }

        private void cbDacTxDacMain6bPdbMidChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr01() < 0)
                return;
        }

        private void cbDacTxDacMain6bMidChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr01() < 0)
                return;
        }

        private int _WritePage40Addr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPolinvBotCh0.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacMain6bPdbBotCh0.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacMain6bBotCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(178, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPolinvBotCh1.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacMain6bPdbBotCh1.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacMain6bBotCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(198, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPolinvBotCh2.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacMain6bPdbBotCh2.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacMain6bBotCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(218, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPolinvBotCh3.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacMain6bPdbBotCh3.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacMain6bBotCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(238, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr02()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPolinvBotChAll.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacMain6bPdbBotChAll.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacMain6bBotChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(178, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(198, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(218, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(238, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPolinvBotCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr02() < 0)
                return;
        }

        private void cbDacTxDacMain6bPdbBotCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr02() < 0)
                return;
        }

        private void cbDacTxDacMain6bBotCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr02() < 0)
                return;
        }

        private void cbTxPolinvBotCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr02() < 0)
                return;
        }

        private void cbDacTxDacMain6bPdbBotCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr02() < 0)
                return;
        }

        private void cbDacTxDacMain6bBotCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr02() < 0)
                return;
        }

        private void cbTxPolinvBotCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr02() < 0)
                return;
        }

        private void cbDacTxDacMain6bPdbBotCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr02() < 0)
                return;
        }

        private void cbDacTxDacMain6bBotCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr02() < 0)
                return;
        }

        private void cbTxPolinvBotCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr02() < 0)
                return;
        }

        private void cbDacTxDacMain6bPdbBotCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr02() < 0)
                return;
        }

        private void cbDacTxDacMain6bBotCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr02() < 0)
                return;
        }

        private void cbTxPolinvBotChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr02() < 0)
                return;
        }

        private void cbDacTxDacMain6bPdbBotChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr02() < 0)
                return;
        }

        private void cbDacTxDacMain6bBotChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr02() < 0)
                return;
        }

        private int _WritePage40Addr03()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacMain3bPdbTopCh0.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbDacTxDacMain3bTopCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(179, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr03()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacMain3bPdbTopCh1.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbDacTxDacMain3bTopCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(199, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr03()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacMain3bPdbTopCh2.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbDacTxDacMain3bTopCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(219, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr03()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacMain3bPdbTopCh3.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbDacTxDacMain3bTopCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(239, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr03()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacMain3bPdbTopChAll.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbDacTxDacMain3bTopChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(179, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(199, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(219, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(239, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain3bPdbTopCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr03() < 0)
                return;
        }

        private void cbDacTxDacMain3bTopCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr03() < 0)
                return;
        }

        private void cbDacTxDacMain3bPdbTopCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr03() < 0)
                return;
        }

        private void cbDacTxDacMain3bTopCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr03() < 0)
                return;
        }

        private void cbDacTxDacMain3bPdbTopCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr03() < 0)
                return;
        }

        private void cbDacTxDacMain3bTopCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr03() < 0)
                return;
        }

        private void cbDacTxDacMain3bPdbTopCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr03() < 0)
                return;
        }

        private void cbDacTxDacMain3bTopCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr03() < 0)
                return;
        }

        private void cbDacTxDacMain3bPdbTopChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr03() < 0)
                return;
        }

        private void cbDacTxDacMain3bTopChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr03() < 0)
                
                return;
        }

        private int _WritePage40Addr04()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacMain3bPdbMidCh0.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbDacTxDacMain3bMidCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(180, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr04()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacMain3bPdbMidCh1.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbDacTxDacMain3bMidCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(200, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr04()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacMain3bPdbMidCh2.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbDacTxDacMain3bMidCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(220, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr04()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacMain3bPdbMidCh3.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbDacTxDacMain3bMidCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(240, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr04()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacMain3bPdbMidChAll.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbDacTxDacMain3bMidChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(180, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(200, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(220, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(240, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain3bPdbMidCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr04() < 0)
                return;
        }

        private void cbDacTxDacMain3bMidCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr04() < 0)
                return;
        }

        private void cbDacTxDacMain3bPdbMidCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr04() < 0)
                return;
        }

        private void cbDacTxDacMain3bMidCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr04() < 0)
                return;
        }

        private void cbDacTxDacMain3bPdbMidCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr04() < 0)
                return;
        }

        private void cbDacTxDacMain3bMidCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr04() < 0)
                return;
        }

        private void cbDacTxDacMain3bPdbMidCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr04() < 0)
                return;
        }

        private void cbDacTxDacMain3bMidCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr04() < 0)
                return;
        }

        private void cbDacTxDacMain3bPdbMidChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr04() < 0)
                return;
        }

        private void cbDacTxDacMain3bMidChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr04() < 0)
                return;
        }

        private int _WritePage40Addr05()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacMain3bPdbBotCh0.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbDacTxDacMain3bBotCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(181, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr05()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacMain3bPdbBotCh1.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbDacTxDacMain3bBotCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(201, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr05()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacMain3bPdbBotCh2.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbDacTxDacMain3bBotCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(221, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr05()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacMain3bPdbBotCh3.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbDacTxDacMain3bBotCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(241, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr05()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacMain3bPdbBotChAll.Checked == true)
                data[0] |= 0x08;

            bTmp = Convert.ToByte(cbDacTxDacMain3bBotChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(181, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(201, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(221, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(241, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacMain3bPdbBotCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr05() < 0)
                return;
        }

        private void cbDacTxDacMain3bBotCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr05() < 0)
                return;
        }

        private void cbDacTxDacMain3bPdbBotCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr05() < 0)
                return;
        }

        private void cbDacTxDacMain3bBotCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr05() < 0)
                return;
        }

        private void cbDacTxDacMain3bPdbBotCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr05() < 0)
                return;
        }

        private void cbDacTxDacMain3bBotCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr05() < 0)
                return;
        }

        private void cbDacTxDacMain3bPdbBotCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr05() < 0)
                return;
        }

        private void cbDacTxDacMain3bBotCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr05() < 0)
                return;
        }

        private void cbDacTxDacMain3bPdbBotChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr05() < 0)
                return;
        }

        private void cbDacTxDacMain3bBotChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr05() < 0)
                return;
        }

        private int _WritePage40Addr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxDcdDacPolflipTopCh0.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdPdbTopCh0.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdTopCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(182, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxDcdDacPolflipTopCh1.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdPdbTopCh1.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdTopCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(202, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxDcdDacPolflipTopCh2.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdPdbTopCh2.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdTopCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(222, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxDcdDacPolflipTopCh3.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdPdbTopCh3.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdTopCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(242, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr06()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxDcdDacPolflipTopChAll.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdPdbTopChAll.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdTopChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(182, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(202, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(222, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(242, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxDcdDacPolflipTopCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr06() < 0)
                return;
        }

        private void cbDacTxDacDcdPdbTopCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr06() < 0)
                return;
        }

        private void cbDacTxDacDcdTopCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr06() < 0)
                return;
        }

        private void cbTxDcdDacPolflipTopCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr06() < 0)
                return;
        }

        private void cbDacTxDacDcdPdbTopCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr06() < 0)
                return;
        }

        private void cbDacTxDacDcdTopCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr06() < 0)
                return;
        }

        private void cbTxDcdDacPolflipTopCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr06() < 0)
                return;
        }

        private void cbDacTxDacDcdPdbTopCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr06() < 0)
                return;
        }

        private void cbDacTxDacDcdTopCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr06() < 0)
                return;
        }

        private void cbTxDcdDacPolflipTopCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr06() < 0)
                return;
        }

        private void cbDacTxDacDcdPdbTopCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr06() < 0)
                return;
        }

        private void cbDacTxDacDcdTopCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr06() < 0)
                return;
        }

        private void cbTxDcdDacPolflipTopChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr06() < 0)
                return;
        }

        private void cbDacTxDacDcdPdbTopChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr06() < 0)
                return;
        }

        private void cbDacTxDacDcdTopChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr06() < 0)
                return;
        }

        private int _WritePage40Addr07()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxDcdDacPolflipMidCh0.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdPdbMidCh0.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdMidCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(183, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr07()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxDcdDacPolflipMidCh1.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdPdbMidCh1.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdMidCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(203, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr07()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxDcdDacPolflipMidCh2.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdPdbMidCh2.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdMidCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(223, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr07()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxDcdDacPolflipMidCh3.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdPdbMidCh3.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdMidCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(243, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr07()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxDcdDacPolflipMidChAll.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdPdbMidChAll.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdMidChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(183, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(203, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(223, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(243, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxDcdDacPolflipMidCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr07() < 0)
                return;
        }

        private void cbDacTxDacDcdPdbMidCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr07() < 0)
                return;
        }

        private void cbDacTxDacDcdMidCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr07() < 0)
                return;
        }

        private void cbTxDcdDacPolflipMidCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr07() < 0)
                return;
        }

        private void cbDacTxDacDcdPdbMidCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr07() < 0)
                return;
        }

        private void cbDacTxDacDcdMidCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr07() < 0)
                return;
        }

        private void cbTxDcdDacPolflipMidCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr07() < 0)
                return;
        }

        private void cbDacTxDacDcdPdbMidCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr07() < 0)
                return;
        }

        private void cbDacTxDacDcdMidCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr07() < 0)
                return;
        }

        private void cbTxDcdDacPolflipMidCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr07() < 0)
                return;
        }

        private void cbDacTxDacDcdPdbMidCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr07() < 0)
                return;
        }

        private void cbDacTxDacDcdMidCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr07() < 0)
                return;
        }

        private void cbTxDcdDacPolflipMidChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr07() < 0)
                return;
        }

        private void cbDacTxDacDcdPdbMidChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr07() < 0)
                return;
        }

        private void cbDacTxDacDcdMidChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr07() < 0)
                return;
        }

        private int _WritePage40Addr08()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxDcdDacPolflipBotCh0.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdPdbBotCh0.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdBotCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(184, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr08()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxDcdDacPolflipBotCh1.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdPdbBotCh1.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdBotCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(204, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr08()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxDcdDacPolflipBotCh2.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdPdbBotCh2.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdBotCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(224, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr08()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxDcdDacPolflipBotCh3.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdPdbBotCh3.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdBotCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(244, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr08()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxDcdDacPolflipBotChAll.Checked == true)
                data[0] |= 0x20;

            if (cbDacTxDacDcdPdbBotChAll.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDcdBotChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(184, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(204, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(224, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(244, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxDcdDacPolflipBotCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr08() < 0)
                return;
        }

        private void cbDacTxDacDcdPdbBotCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr08() < 0)
                return;
        }

        private void cbDacTxDacDcdBotCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr08() < 0)
                return;
        }

        private void cbTxDcdDacPolflipBotCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr08() < 0)
                return;
        }

        private void cbDacTxDacDcdPdbBotCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr08() < 0)
                return;
        }

        private void cbDacTxDacDcdBotCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr08() < 0)
                return;
        }

        private void cbTxDcdDacPolflipBotCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr08() < 0)
                return;
        }

        private void cbDacTxDacDcdPdbBotCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr08() < 0)
                return;
        }

        private void cbDacTxDacDcdBotCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr08() < 0)
                return;
        }

        private void cbTxDcdDacPolflipBotCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr08() < 0)
                return;
        }

        private void cbDacTxDacDcdPdbBotCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr08() < 0)
                return;
        }

        private void cbDacTxDacDcdBotCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr08() < 0)
                return;
        }

        private void cbTxDcdDacPolflipBotChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr08() < 0)
                return;
        }

        private void cbDacTxDacDcdPdbBotChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr08() < 0)
                return;
        }

        private void cbDacTxDacDcdBotChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr08() < 0)
                return;
        }

        private int _WritePage40Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPrePdbTopCh0.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPreTopCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(185, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPrePdbTopCh1.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPreTopCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(205, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPrePdbTopCh2.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPreTopCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(225, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPrePdbTopCh3.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPreTopCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(245, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr09()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPrePdbTopChAll.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPreTopChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(185, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(205, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(225, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(245, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPrePdbTopCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr09() < 0)
                return;
        }

        private void cbDacTxDacPreTopCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr09() < 0)
                return;
        }

        private void cbDacTxDacPrePdbTopCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr09() < 0)
                return;
        }

        private void cbDacTxDacPreTopCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr09() < 0)
                return;
        }

        private void cbDacTxDacPrePdbTopCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr09() < 0)
                return;
        }

        private void cbDacTxDacPreTopCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr09() < 0)
                return;
        }

        private void cbDacTxDacPrePdbTopCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr09() < 0)
                return;
        }

        private void cbDacTxDacPreTopCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr09() < 0)
                return;
        }

        private void cbDacTxDacPrePdbTopChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr09() < 0)
                return;
        }

        private void cbDacTxDacPreTopChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr09() < 0)
                return;
        }

        private int _WritePage40Addr0A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPrePdbMidCh0.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPreMidCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(186, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr0A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPrePdbMidCh1.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPreMidCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(206, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr0A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPrePdbMidCh2.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPreMidCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(226, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr0A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPrePdbMidCh3.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPreMidCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(246, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr0A()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPrePdbMidChAll.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPreMidChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(186, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(206, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(226, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(246, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPrePdbMidCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0A() < 0)
                return;
        }

        private void cbDacTxDacPreMidCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0A() < 0)
                return;
        }

        private void cbDacTxDacPrePdbMidCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0A() < 0)
                return;
        }

        private void cbDacTxDacPreMidCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0A() < 0)
                return;
        }

        private void cbDacTxDacPrePdbMidCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0A() < 0)
                return;
        }

        private void cbDacTxDacPreMidCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0A() < 0)
                return;
        }

        private void cbDacTxDacPrePdbMidCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0A() < 0)
                return;
        }

        private void cbDacTxDacPreMidCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0A() < 0)
                return;
        }

        private void cbDacTxDacPrePdbMidChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0A() < 0)
                return;
        }

        private void cbDacTxDacPreMidChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0A() < 0)
                return;
        }

        private int _WritePage40Addr0B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPrePdbBotCh0.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPreBotCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(187, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr0B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPrePdbBotCh1.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPreBotCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(207, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr0B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPrePdbBotCh2.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPreBotCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(227, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr0B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPrePdbBotCh3.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPreBotCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(247, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr0B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbDacTxDacPrePdbBotChAll.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPreBotChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(187, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(207, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(227, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(247, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbDacTxDacPrePdbBotCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0B() < 0)
                return;
        }

        private void cbDacTxDacPreBotCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0B() < 0)
                return;
        }

        private void cbDacTxDacPrePdbBotCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0B() < 0)
                return;
        }

        private void cbDacTxDacPreBotCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0B() < 0)
                return;
        }

        private void cbDacTxDacPrePdbBotCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0B() < 0)
                return;
        }

        private void cbDacTxDacPreBotCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0B() < 0)
                return;
        }

        private void cbDacTxDacPrePdbBotCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0B() < 0)
                return;
        }

        private void cbDacTxDacPreBotCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0B() < 0)
                return;
        }

        private void cbDacTxDacPrePdbBotChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0B() < 0)
                return;
        }

        private void cbDacTxDacPreBotChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0B() < 0)
                return;
        }

        private int _WritePage40Addr0C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPostPolinvTopCh0.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacPostPdbTopCh0.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPostTopCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(188, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr0C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPostPolinvTopCh1.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacPostPdbTopCh1.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPostTopCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(208, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr0C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPostPolinvTopCh2.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacPostPdbTopCh2.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPostTopCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(228, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr0C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPostPolinvTopCh3.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacPostPdbTopCh3.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPostTopCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(248, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr0C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPostPolinvTopChAll.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacPostPdbTopChAll.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPostTopChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(188, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(208, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(228, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(248, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPostPolinvTopCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0C() < 0)
                return;
        }

        private void cbDacTxDacPostPdbTopCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0C() < 0)
                return;
        }

        private void cbDacTxDacPostTopCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0C() < 0)
                return;
        }

        private void cbTxPostPolinvTopCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0C() < 0)
                return;
        }

        private void cbDacTxDacPostPdbTopCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0C() < 0)
                return;
        }

        private void cbDacTxDacPostTopCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0C() < 0)
                return;
        }

        private void cbTxPostPolinvTopCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0C() < 0)
                return;
        }

        private void cbDacTxDacPostPdbTopCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0C() < 0)
                return;
        }

        private void cbDacTxDacPostTopCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0C() < 0)
                return;
        }

        private void cbTxPostPolinvTopCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0C() < 0)
                return;
        }

        private void cbDacTxDacPostPdbTopCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0C() < 0)
                return;
        }

        private void cbDacTxDacPostTopCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0C() < 0)
                return;
        }

        private void cbTxPostPolinvTopChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0C() < 0)
                return;
        }

        private void cbDacTxDacPostPdbTopChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0C() < 0)
                return;
        }

        private void cbDacTxDacPostTopChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0C() < 0)
                return;
        }

        private int _WritePage40Addr0D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPostPolinvMidCh0.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacPostPdbMidCh0.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPostMidCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(189, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr0D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPostPolinvMidCh1.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacPostPdbMidCh1.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPostMidCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(209, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr0D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPostPolinvMidCh2.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacPostPdbMidCh2.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPostMidCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(229, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr0D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPostPolinvMidCh3.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacPostPdbMidCh3.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPostMidCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(249, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr0D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPostPolinvMidChAll.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacPostPdbMidChAll.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPostMidChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(189, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(209, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(229, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(249, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPostPolinvMidCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0D() < 0)
                return;
        }

        private void cbDacTxDacPostPdbMidCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0D() < 0)
                return;
        }

        private void cbDacTxDacPostMidCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0D() < 0)
                return;
        }

        private void cbTxPostPolinvMidCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0D() < 0)
                return;
        }

        private void cbDacTxDacPostPdbMidCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0D() < 0)
                return;
        }

        private void cbDacTxDacPostMidCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0D() < 0)
                return;
        }

        private void cbTxPostPolinvMidCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0D() < 0)
                return;
        }

        private void cbDacTxDacPostPdbMidCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0D() < 0)
                return;
        }

        private void cbDacTxDacPostMidCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0D() < 0)
                return;
        }

        private void cbTxPostPolinvMidCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0D() < 0)
                return;
        }

        private void cbDacTxDacPostPdbMidCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0D() < 0)
                return;
        }

        private void cbDacTxDacPostMidCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0D() < 0)
                return;
        }

        private void cbTxPostPolinvMidChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0D() < 0)
                return;
        }

        private void cbDacTxDacPostPdbMidChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0D() < 0)
                return;
        }

        private void cbDacTxDacPostMidChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0D() < 0)
                return;
        }

        private int _WritePage40Addr0E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPostPolinvBotCh0.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacPostPdbBotCh0.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPostBotCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(190, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr0E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPostPolinvBotCh1.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacPostPdbBotCh1.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPostBotCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(210, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr0E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPostPolinvBotCh2.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacPostPdbBotCh2.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPostBotCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(230, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr0E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPostPolinvBotCh3.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacPostPdbBotCh3.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPostBotCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(250, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr0E()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPostPolinvBotChAll.Checked == true)
                data[0] |= 0x80;

            if (cbDacTxDacPostPdbBotChAll.Checked == true)
                data[0] |= 0x40;

            bTmp = Convert.ToByte(cbDacTxDacPostBotChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(190, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(210, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(230, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(250, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPostPolinvBotCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0E() < 0)
                return;
        }

        private void cbDacTxDacPostPdbBotCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0E() < 0)
                return;
        }

        private void cbDacTxDacPostBotCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr0E() < 0)
                return;
        }

        private void cbTxPostPolinvBotCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0E() < 0)
                return;
        }

        private void cbDacTxDacPostPdbBotCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0E() < 0)
                return;
        }

        private void cbDacTxDacPostBotCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr0E() < 0)
                return;
        }

        private void cbTxPostPolinvBotCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0E() < 0)
                return;
        }

        private void cbDacTxDacPostPdbBotCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0E() < 0)
                return;
        }

        private void cbDacTxDacPostBotCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr0E() < 0)
                return;
        }

        private void cbTxPostPolinvBotCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0E() < 0)
                return;
        }

        private void cbDacTxDacPostPdbBotCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0E() < 0)
                return;
        }

        private void cbDacTxDacPostBotCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr0E() < 0)
                return;
        }

        private void cbTxPostPolinvBotChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0E() < 0)
                return;
        }

        private void cbDacTxDacPostPdbBotChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0E() < 0)
                return;
        }

        private void cbDacTxDacPostBotChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr0E() < 0)
                return;
        }

        private int _WritePage40Addr1B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPdbDatdelTopCh0.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDatdelTopCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(191, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr1B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPdbDatdelTopCh1.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDatdelTopCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(211, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr1B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPdbDatdelTopCh2.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDatdelTopCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(231, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr1B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPdbDatdelTopCh3.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDatdelTopCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(251, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr1B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPdbDatdelTopChAll.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDatdelTopChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(191, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(211, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(231, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(251, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPdbDatdelTopCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr1B() < 0)
                return;
        }

        private void cbDacTxDacDatdelTopCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr1B() < 0)
                return;
        }

        private void cbTxPdbDatdelTopCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr1B() < 0)
                return;
        }

        private void cbDacTxDacDatdelTopCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr1B() < 0)
                return;
        }

        private void cbTxPdbDatdelTopCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr1B() < 0)
                return;
        }

        private void cbDacTxDacDatdelTopCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr1B() < 0)
                return;
        }

        private void cbTxPdbDatdelTopCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr1B() < 0)
                return;
        }

        private void cbDacTxDacDatdelTopCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr1B() < 0)
                return;
        }

        private void cbTxPdbDatdelTopChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr1B() < 0)
                return;
        }

        private void cbDacTxDacDatdelTopChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr1B() < 0)
                return;
        }

        private int _WritePage40Addr1C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPdbDatdelMidCh0.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDatdelMidCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(192, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr1C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPdbDatdelMidCh1.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDatdelMidCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(212, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr1C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPdbDatdelMidCh2.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDatdelMidCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(232, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr1C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPdbDatdelMidCh3.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDatdelMidCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(252, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr1C()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPdbDatdelMidChAll.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDatdelMidChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(192, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(212, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(232, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(252, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPdbDatdelMidCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr1C() < 0)
                return;
        }

        private void cbDacTxDacDatdelMidCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr1C() < 0)
                return;
        }

        private void cbTxPdbDatdelMidCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr1C() < 0)
                return;
        }

        private void cbDacTxDacDatdelMidCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr1C() < 0)
                return;
        }

        private void cbTxPdbDatdelMidCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr1C() < 0)
                return;
        }

        private void cbDacTxDacDatdelMidCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr1C() < 0)
                return;
        }

        private void cbTxPdbDatdelMidCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr1C() < 0)
                return;
        }

        private void cbDacTxDacDatdelMidCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr1C() < 0)
                return;
        }

        private void cbTxPdbDatdelMidChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr1C() < 0)
                return;
        }

        private void cbDacTxDacDatdelMidChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr1C() < 0)
                return;
        }

        private int _WritePage40Addr1D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPdbDatdelBotCh0.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDatdelBotCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(193, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr1D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPdbDatdelBotCh1.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDatdelBotCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(213, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr1D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPdbDatdelBotCh2.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDatdelBotCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(233, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr1D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPdbDatdelBotCh3.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDatdelBotCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(253, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr1D()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbTxPdbDatdelBotChAll.Checked == true)
                data[0] |= 0x10;

            bTmp = Convert.ToByte(cbDacTxDacDatdelBotChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(193, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(213, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(233, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(253, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPdbDatdelBotCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr1D() < 0)
                return;
        }

        private void cbDacTxDacDatdelBotCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr1D() < 0)
                return;
        }

        private void cbTxPdbDatdelBotCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr1D() < 0)
                return;
        }

        private void cbDacTxDacDatdelBotCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr1D() < 0)
                return;
        }

        private void cbTxPdbDatdelBotCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr1D() < 0)
                return;
        }

        private void cbDacTxDacDatdelBotCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr1D() < 0)
                return;
        }

        private void cbTxPdbDatdelBotCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr1D() < 0)
                return;
        }

        private void cbDacTxDacDatdelBotCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr1D() < 0)
                return;
        }

        private void cbTxPdbDatdelBotChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr1D() < 0)
                return;
        }

        private void cbDacTxDacDatdelBotChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr1D() < 0)
                return;
        }

        private int _WritePage40Addr1E()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxPdbLbkOutCh0.Checked == true)
                data[0] |= 0x02;

            if (cbTxPdbLbkInCh0.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(194, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr1E()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxPdbLbkOutCh1.Checked == true)
                data[0] |= 0x02;

            if (cbTxPdbLbkInCh1.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(214, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr1E()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxPdbLbkOutCh2.Checked == true)
                data[0] |= 0x02;

            if (cbTxPdbLbkInCh2.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(234, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr1E()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxPdbLbkOutCh3.Checked == true)
                data[0] |= 0x02;

            if (cbTxPdbLbkInCh3.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(254, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr1E()
        {
            byte[] data = new byte[1];
            int rv;

            data[0] = 0x00; //Default

            if (cbTxPdbLbkOutChAll.Checked == true)
                data[0] |= 0x02;

            if (cbTxPdbLbkInChAll.Checked == true)
                data[0] |= 0x01;

            rv = I2cWrite(194, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(214, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(234, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(254, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbTxPdbLbkOutCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr1E() < 0)
                return;
        }

        private void cbTxPdbLbkInCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr1E() < 0)
                return;
        }

        private void cbTxPdbLbkOutCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr1E() < 0)
                return;
        }

        private void cbTxPdbLbkInCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr1E() < 0)
                return;
        }

        private void cbTxPdbLbkOutCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr1E() < 0)
                return;
        }

        private void cbTxPdbLbkInCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr1E() < 0)
                return;
        }

        private void cbTxPdbLbkOutCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr1E() < 0)
                return;
        }

        private void cbTxPdbLbkInCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr1E() < 0)
                return;
        }

        private void cbTxPdbLbkOutChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr1E() < 0)
                return;
        }

        private void cbTxPdbLbkInChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr1E() < 0)
                return;
        }

        private int _WritePage40Addr4B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbAdaptationMuteCh0.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbTxTermOutResCntlCh0.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbTxTermOutPeakingCntlCh0.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbMuteCntlCh0.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(195, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage41Addr4B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbAdaptationMuteCh1.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbTxTermOutResCntlCh1.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbTxTermOutPeakingCntlCh1.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbMuteCntlCh1.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(215, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage42Addr4B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbAdaptationMuteCh2.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbTxTermOutResCntlCh2.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbTxTermOutPeakingCntlCh2.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbMuteCntlCh2.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(235, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage43Addr4B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbAdaptationMuteCh3.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbTxTermOutResCntlCh3.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbTxTermOutPeakingCntlCh3.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbMuteCntlCh3.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(255, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private int _WritePage3FAddr4B()
        {
            byte[] data = new byte[1];
            int rv;
            byte bTmp;

            bTmp = 0;
            data[0] = 0x00; //Default

            if (cbAdaptationMuteChAll.Checked == true)
                data[0] |= 0x80;

            bTmp = Convert.ToByte(cbTxTermOutResCntlChAll.SelectedIndex);
            bTmp <<= 5;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbTxTermOutPeakingCntlChAll.SelectedIndex);
            bTmp <<= 2;
            data[0] |= bTmp;

            bTmp = Convert.ToByte(cbMuteCntlChAll.SelectedIndex);
            data[0] |= bTmp;

            rv = I2cWrite(195, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(215, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(235, 1, data);
            if (rv < 0)
                return -1;

            rv = I2cWrite(255, 1, data);
            if (rv < 0)
                return -1;

            return 0;
        }

        private void cbAdaptationMuteCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr4B() < 0)
                return;
        }

        private void cbTxTermOutResCntlCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr4B() < 0)
                return;
        }

        private void cbTxTermOutPeakingCntlCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr4B() < 0)
                return;
        }

        private void cbMuteCntlCh0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage40Addr4B() < 0)
                return;
        }

        private void cbAdaptationMuteCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr4B() < 0)
                return;
        }

        private void cbTxTermOutResCntlCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr4B() < 0)
                return;
        }

        private void cbTxTermOutPeakingCntlCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr4B() < 0)
                return;
        }

        private void cbMuteCntlCh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage41Addr4B() < 0)
                return;
        }

        private void cbAdaptationMuteCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr4B() < 0)
                return;
        }

        private void cbTxTermOutResCntlCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr4B() < 0)
                return;
        }

        private void cbTxTermOutPeakingCntlCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr4B() < 0)
                return;
        }

        private void cbMuteCntlCh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage42Addr4B() < 0)
                return;
        }

        private void cbAdaptationMuteCh3_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr4B() < 0)
                return;
        }

        private void cbTxTermOutResCntlCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr4B() < 0)
                return;
        }

        private void cbTxTermOutPeakingCntlCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr4B() < 0)
                return;
        }

        private void cbMuteCntlCh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage43Addr4B() < 0)
                return;
        }

        private void cbAdaptationMuteChAll_CheckedChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr4B() < 0)
                return;
        }

        private void cbTxTermOutResCntlChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr4B() < 0)
                return;
        }

        private void cbTxTermOutPeakingCntlChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr4B() < 0)
                return;
        }

        private void cbMuteCntlChAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reading == true)
                return;

            if (_WritePage3FAddr4B() < 0)
                return;
        }
    }
}
