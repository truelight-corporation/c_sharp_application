using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using I2cMasterInterface;

namespace Mald38045Mata38044Config
{
    public partial class FMald38045Mata38044Config : Form
    {
        private I2cMaster i2cMaster = new I2cMaster();
        private const byte devAddr = 0x50;

        private int _SetModuleMode(byte mode)
        {
            byte[] data = new byte[] { 0x00 };

            data[0] = 0x00; //bank
            if (i2cMaster.WriteApi(devAddr, 0x7E, 1, data) < 0)
                return -1;

            data[0] = 0xBB; //page
            if (i2cMaster.WriteApi(devAddr, 0x7F, 1, data) < 0)
                return -1;

            data[0] = mode;
            if (i2cMaster.WriteApi(devAddr, 0xA4, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _I2cMasterConnect()
        {
            if (i2cMaster.ConnectApi(100) < 0)
                return -1;

            cbConnected.Checked = true;

            if (_WriteModulePassword() < 0)
                return -1;

            if (_SetModuleMode(0x4D) < 0)
                return -1;

            return 0;
        }

        private int _I2cMasterDisconnect()
        {
            if (i2cMaster.DisconnectApi() < 0)
                return -1;

            cbConnected.Checked = false;

            return 0;
        }

        private int _I2cRead(byte bank, byte page, byte startAddr, int length, byte[] data)
        {
            byte[] bTmp = new byte[128];
            byte[] bankAndPage = new byte[] { bank, page };
            int bufLength, readLength, regAddr, rv;

            if (i2cMaster.connected == false) {
                if (_I2cMasterConnect() < 0)
                    return -1;
            }

            if (_SetModuleMode(0x4D) < 0)
                goto error;

            if (startAddr < 128) {
                rv = i2cMaster.WriteApi(devAddr, 0x7E, 2, bankAndPage);
                if (rv < 0)
                    goto error;

                rv = i2cMaster.ReadApi(devAddr, (byte)startAddr, (byte)length, data);
                if (rv != length)
                    MessageBox.Show("Only read " + rv + " not " + length + " byte Error!!");

                return rv;
            }

            //startAddr > 127
            readLength = 0;
            regAddr = startAddr;

            while (readLength < length) {
                rv = i2cMaster.WriteApi(devAddr, 0x7E, 2, bankAndPage);
                if (rv < 0)
                    goto error;

                if (regAddr + length - readLength <= 256) {
                    bufLength = length - readLength;
                    rv = i2cMaster.ReadApi(devAddr, (byte)regAddr, (byte)bufLength, bTmp);
                    if (rv < 0)
                        goto error;

                    Buffer.BlockCopy(bTmp, 0, data, readLength, bufLength);
                    readLength += bufLength;
                }
                else {
                    bufLength = 256 - regAddr;
                    rv = i2cMaster.ReadApi(devAddr, (byte)regAddr, (byte)bufLength, bTmp);
                    if (rv < 0)
                        goto error;

                    Buffer.BlockCopy(bTmp, 0, data, readLength, bufLength);

                    readLength += bufLength;
                    regAddr = 128;
                    bankAndPage[1]++;
                }
            }

            return readLength;
         
        error:
            MessageBox.Show("Module no response!!");
            _I2cMasterDisconnect();
            return -1;
        }

        private int _I2cWrite(byte bank, byte page, byte startAddr, int length, byte[] data)
        {
            byte[] bTmp = new byte[128];
            byte[] bankAndPage = new byte[] { bank, page };
            int regAddr, writeLength, rv;

            if (i2cMaster.connected == false) {
                if (_I2cMasterConnect() < 0)
                    return -1;
            }
            
            if (_WriteModulePassword() < 0)
                goto error;

            if (_SetModuleMode(0x4D) < 0)
                goto error;

            if (startAddr < 128) {
                rv = i2cMaster.WriteApi(devAddr, (byte)startAddr, (byte)length, data);
                if (rv < 0)
                    goto error;

                return rv;
            }

            //startAddr > 127
            writeLength = 0;
            regAddr = startAddr;

            while (writeLength < length) {
                rv = i2cMaster.WriteApi(devAddr, 0x7E, 2, bankAndPage);
                if (rv < 0)
                    goto error;

                if (regAddr + length - writeLength <= 256) {
                    Buffer.BlockCopy(data, writeLength, bTmp, 0, (length - writeLength));

                    rv = i2cMaster.WriteApi(devAddr, (byte)regAddr, (byte)(length - writeLength), bTmp);
                    if (rv < 0)
                        goto error;

                    writeLength += (length - writeLength);
                }
                else {
                    Buffer.BlockCopy(data, writeLength, bTmp, 0, 128);

                    rv = i2cMaster.WriteApi(devAddr, (byte)regAddr, 128, bTmp);
                    if (rv < 0)
                        goto error;

                    writeLength += 128;
                    regAddr = 128;
                    bankAndPage[1]++;
                }
            }

            return writeLength;

        error:
            MessageBox.Show("QSFP+ module no response!!");
            _I2cMasterDisconnect();
            return -1;
        }

        private int _WriteModulePassword()
        {
            byte[] data;

            data = Encoding.Default.GetBytes(tbPassword.Text);

            if (i2cMaster.WriteApi(80, 122, 4, data) < 0)
                return -1;

            return 0;
        }

        public FMald38045Mata38044Config()
        {
            InitializeComponent();

            ucMald38045ConfigCh1_4.SetRegBankApi(1);
            ucMald38045ConfigCh1_4.SetRegPageApi(0xB0);
            ucMald38045ConfigCh1_4.SetRegStartAddrApi(0x80); //Reg length:238 (Page:0xB0 Addr:0x80 ~ Page:0xB1 Addr:0xED)
            ucMald38045ConfigCh1_4.SetI2cReadCBApi(_I2cRead);
            ucMald38045ConfigCh1_4.SetI2cWriteCBApi(_I2cWrite);

            ucMald38045ConfigCh5_8.SetRegBankApi(1);
            ucMald38045ConfigCh5_8.SetRegPageApi(0xB1);
            ucMald38045ConfigCh5_8.SetRegStartAddrApi(0xEE);
            ucMald38045ConfigCh5_8.SetI2cReadCBApi(_I2cRead);
            ucMald38045ConfigCh5_8.SetI2cWriteCBApi(_I2cWrite);

            ucMata38044ConfigCh4_1.SetRegBankApi(2);
            ucMata38044ConfigCh4_1.SetRegPageApi(0xB0);
            ucMata38044ConfigCh4_1.SetRegStartAddrApi(0x80); //Reg length:256 (Page:0xB0 Addr:0x80 ~ Page:0xB1 Addr:0xFF)
            ucMata38044ConfigCh4_1.SetI2cReadCBApi(_I2cRead);
            ucMata38044ConfigCh4_1.SetI2cWriteCBApi(_I2cWrite);

            ucMata38044ConfigCh8_5.SetRegBankApi(2);
            ucMata38044ConfigCh8_5.SetRegPageApi(0xB2);
            ucMata38044ConfigCh8_5.SetRegStartAddrApi(0x80);
            ucMata38044ConfigCh8_5.SetI2cReadCBApi(_I2cRead);
            ucMata38044ConfigCh8_5.SetI2cWriteCBApi(_I2cWrite);
        }

        private void cbConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbConnected.Checked == true) {
                if (_I2cMasterConnect() < 0)
                    return;
            }
            else
                _I2cMasterDisconnect();
        }

        private void bStoreIntoFlash_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[] { 0x00};

            if (i2cMaster.connected == false) {
                if (_I2cMasterConnect() < 0)
                    return;
            }

            bStoreIntoFlash.Text = "Storing...";
            bStoreIntoFlash.Enabled = false;

            data[0] = 0x00; //bank
            if (i2cMaster.WriteApi(devAddr, 0x7E, 1, data) < 0)
                goto exit;

            data[0] = 0xBB; //page
            if (i2cMaster.WriteApi(devAddr, 0x7F, 1, data) < 0)
                goto exit;

            data[0] = 0xBB;
            if (i2cMaster.WriteApi(devAddr, 0xA2, 1, data) < 0)
                goto exit;

            data[0] = 0xCC;
            if (i2cMaster.WriteApi(devAddr, 0xA2, 1, data) < 0)
                goto exit;

            Thread.Sleep(1000);

        exit:
            bStoreIntoFlash.Text = "Store into flash";
            bStoreIntoFlash.Enabled = true;
            return;
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; } = "";
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}