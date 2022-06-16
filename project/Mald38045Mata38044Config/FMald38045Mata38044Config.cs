using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using I2cMasterInterface;

namespace Mald38045Mata38044Config
{
    public partial class FMald38045Mata38044Config : Form
    {
        private I2cMaster i2cMaster = new I2cMaster();
        private const byte devAddr = 0x00;

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[] { 0x00 };

            data[0] = 0x00; //bank
            if (i2cMaster.WriteApi(devAddr, 0x7E, 1, data) < 0)
                return -1;

            data[0] = 0xB0; //page
            if (i2cMaster.WriteApi(devAddr, 0x7F, 1, data) < 0)
                return -1;

            data[0] = mode;
            if (i2cMaster.WriteApi(devAddr, 164, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _I2cMasterConnect()
        {
            if (i2cMaster.ConnectApi(100) < 0)
                return -1;

            cbConnected.Checked = true;

            if (_SetQsfpMode(0x4D) < 0)
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
            int readLength, regAddr, rv;

            if (i2cMaster.connected == false) {
                if (_I2cMasterConnect() < 0)
                    return -1;
            }

            if (_SetQsfpMode(0x4D) < 0)
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
                    rv = i2cMaster.ReadApi(devAddr, (byte)regAddr, (byte)(length - readLength), bTmp);
                    if (rv < 0)
                        goto error;

                    Buffer.BlockCopy(bTmp, 0, data, readLength, (length - readLength));
                    readLength += (length - readLength);
                }
                else {
                    rv = i2cMaster.ReadApi(devAddr, (byte)regAddr, 128, bTmp);
                    if (rv < 0)
                        goto error;

                    Buffer.BlockCopy(bTmp, 0, data, readLength, 128);

                    readLength += 128;
                    regAddr = 128;
                    bankAndPage[1]++;
                }
            }
         
        error:
            MessageBox.Show("QSFP+ module no response!!");
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

            if (_SetQsfpMode(0x4D) < 0)
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

            if (i2cMaster.WriteApi(80, 123, 4, data) < 0)
                return -1;

            return 0;
        }

        public FMald38045Mata38044Config()
        {
            InitializeComponent();

            ucMald38045Ch1_4.SetRegBankApi(0);
            ucMald38045Ch1_4.SetRegStartAddrApi(0x80);
            ucMald38045Ch1_4.SetI2cReadCBApi(_I2cRead);
            ucMald38045Ch1_4.SetI2cWriteCBApi(_I2cWrite);
        }

        private void cbConnected_CheckedChanged(object sender, EventArgs e)
        {
            if (cbConnected.Checked == true) {
                if (_I2cMasterConnect() < 0)
                    return;
                _WriteModulePassword();
            }
            else
                _I2cMasterDisconnect();
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