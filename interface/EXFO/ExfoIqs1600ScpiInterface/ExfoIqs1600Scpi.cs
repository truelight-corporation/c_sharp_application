using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using SCPIcontrol;

namespace ExfoIqs1600ScpiInterface
{
    public class ExfoIqs1600Scpi
    {
        SCPIControl scpiControl = new SCPIControl();

        public int ConnectApi(string ipAddr)
        {
            int rv = 0;

            if (ipAddr.Length < 7) {
                MessageBox.Show("Input ipAddr: " + ipAddr + " Error!!");
                return -1;
            }

            try {
                rv = scpiControl.Scpi_initial(1, "172.16.102.50");
            }
            catch (Exception e1) {
                MessageBox.Show(e1.ToString());
                return -1;
            }

            if (rv < 0) {
                MessageBox.Show("Connect Error!");
                return -1;
            }

            try {
                scpiControl.sendQueryExpectingAStringAnswer("LINS10:READ1:SCAL:POW:DC?");
            }
            catch (ArgumentException ex) {
                MessageBox.Show("Init exception!!\n" + ex.Message.ToString());
                return -1;
            }

            return 0;
        }

        private bool _IsNumber(string value)
        {
            Regex NumberPattern = new Regex("^[0-9]*$");
            return !NumberPattern.IsMatch(value);
        }

        private string _SplitValue(string value)
        {

            string[] sTemp = new string[2];
            double dValue = 0.0;
            double dUnit = 0.0;
            double dGetValue = 0.0;

            sTemp = value.Split('E');
            if (sTemp.Length < 2)
                return "0";
            if (sTemp[1].IndexOf('R') > 0)
                sTemp[1] = sTemp[1].Substring(0, sTemp[1].IndexOf('R'));

            if (!_IsNumber(sTemp[0]))
                return "0";

            Double.TryParse(sTemp[0], out dValue);
            Double.TryParse(sTemp[1], out dUnit);

            //dUnit += 3; //mW
            dUnit += 6; //uW
            dGetValue = dValue * Math.Pow(10.0, dUnit);

            return dGetValue.ToString();
        }

        public int ReadPower(string[] dataBufer)
        {
            string[] value = new string[4];

            if (dataBufer.Length != 4) {
                MessageBox.Show("dataBufer size: " + dataBufer.Length + "!= 4");
                return -1;
            }
            try {
                value[0] = scpiControl.sendQueryExpectingAStringAnswer("LINS10:READ1:SCAL:POW:DC?"); //for channel 1
                value[1] = scpiControl.sendQueryExpectingAStringAnswer("LINS10:READ2:SCAL:POW:DC?"); //for channel 2
                value[2] = scpiControl.sendQueryExpectingAStringAnswer("LINS10:READ3:SCAL:POW:DC?"); //for channel 3
                value[3] = scpiControl.sendQueryExpectingAStringAnswer("LINS10:READ4:SCAL:POW:DC?"); //for channel 4
            }
            catch (Exception e) {
                MessageBox.Show("Read power exception!!\n" + e.ToString());
                return -1;
            }

            dataBufer[0] = _SplitValue(value[0]);
            dataBufer[1] = _SplitValue(value[1]);
            dataBufer[2] = _SplitValue(value[2]);
            dataBufer[3] = _SplitValue(value[3]);

            return 0;
        }
    }
}
