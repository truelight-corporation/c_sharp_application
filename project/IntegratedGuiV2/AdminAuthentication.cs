using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace IntegratedGuiV2
{
    public partial class AdminAuthentication : KryptonForm
    {
        private const string AdminPassword = "543"; // 帳管密碼
        WaitFormFunc loadingForm = new WaitFormFunc();
        AccountsManagement accountsManagement = new AccountsManagement();
        LoginForm loginForm1 = new LoginForm();

        public AdminAuthentication()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(_FormClosing);
        }

        private void bAuthenticate_Click(object sender, EventArgs e)
        {
            if (tbAdminPassword.Text == AdminPassword)
            {
                loadingForm.Show(this);
                accountsManagement.Show();
                accountsManagement.BringToFront();

                loadingForm.Close();
                loginForm1.Close();
                this.Hide();
            }
            else
            {
                //loadingForm.Close();
                tbAdminPassword.Text = "";
                tbAdminPassword.Focus();
                MessageBox.Show("Incorrect Administrator Password. Please try again.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TbAdminPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bAuthenticate_Click(sender, e);
            }
        }

        private void lBackToLogin_Click(object sender, EventArgs e)
        {
            Application.Restart();
            var process = Process.GetCurrentProcess();
            process.WaitForInputIdle();
            SetForegroundWindow(process.MainWindowHandle);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private void _FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}
