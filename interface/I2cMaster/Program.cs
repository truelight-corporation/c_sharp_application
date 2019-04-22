using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace I2cMasterInterface
{
    static class Program
    {
       

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            I2cMaster i2cMaster;
            byte[] data = new byte[256];
            
            i2cMaster = new I2cMaster();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new fAdapterSelect());

            i2cMaster.SetBitRateApi(100);
            i2cMaster.ConnectApi();

            Application.Run(i2cMaster.GetAdapterSelectFormApi());
        }
    }
}
