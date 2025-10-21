using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace IntegratedGuiV2
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new MainForm(true));
                //Application.Run(new LoginForm());
                Application.Run(new MainForm());
            }
            catch ( Exception ex) {
                File.WriteAllText("error_log.txt", ex.ToString());
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
