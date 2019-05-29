using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FirmwareFlashLead
{
    public partial class UcFirmwareFlashLead : UserControl
    {
        private string firmwarePath;

        public UcFirmwareFlashLead()
        {
            InitializeComponent();
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            tbLotNumber.Text = "";
            tbModelNumber.Text = "";
            tbFirmwareVersion.Text = "";
        }

        private void tbLotNumber_TextChanged(object sender, EventArgs e)
        {
            string[] lines = {};
            string lotNumber;
            int i;

            firmwarePath = "";
            tbModelNumber.Text = "";
            tbFirmwareVersion.Text = "";

            if (tbLotNumber.Text.Length < 8)
                goto Error;

            lotNumber = tbLotNumber.Text.Substring(0, 8);

            try {
                lines = System.IO.File.ReadAllLines(@"H:\COB_PrdData\Firmware_ini\" + lotNumber + ".ini");
            } catch(System.IO.FileNotFoundException e1) { 
                MessageBox.Show("批號" + lotNumber + "韌體版本設定檔不存在!!\n" + e1.ToString());
                tbLotNumber.Text = "";
                goto Error;
            }

            for (i = 0; i < lines.Length; i ++) {
                if (lines[i].Equals("[Burn]")) {
                    firmwarePath = lines[i + 1].Substring(lines[i + 1].IndexOf('=') + 1);
                    tbFirmwareVersion.Text = firmwarePath.Substring(firmwarePath.LastIndexOf('-') + 1, 6);
                }
                if (lines[i].Equals("[Product]"))
                    tbModelNumber.Text = lines[i + 1].Substring(6);
            }

            bConfirm.Enabled = true;
            return;

            Error:
            bConfirm.Enabled = false;
        }

        private void bConfirm_Click(object sender, EventArgs e)
        {
            Shell32.Folder input, output;
            System.IO.DirectoryInfo di;
            Shell32.Shell shell = new Shell32.Shell();

            foreach (var process in System.Diagnostics.Process.GetProcessesByName("NuMicro ICP Programming Tool")) {
                process.Kill();
                System.Threading.Thread.Sleep(1000);
            }

            if (!System.IO.Directory.Exists(@"D:\Firmware\")) {
                System.IO.Directory.CreateDirectory(@"D:\Firmware\");
            }
            else {
                di = new System.IO.DirectoryInfo(@"D:\Firmware\");
                foreach (System.IO.FileInfo file in di.GetFiles()) {
                    file.Delete();
                }
            }

            if (!System.IO.File.Exists(@firmwarePath)) {
                MessageBox.Show("無法讀取韌體檔案：" + firmwarePath);
                return;
            }
            
            input = shell.NameSpace(@firmwarePath);
            output = shell.NameSpace(@"D:\Firmware\");
            output.CopyHere(input.Items(), 4);

            System.Diagnostics.Process.Start(@"D:\Firmware\" + tbFirmwareVersion.Text + ".icp");
        }
    }
}
