using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using ComponentFactory.Krypton.Toolkit;
using System.Diagnostics;

namespace IntegratedGuiV2
{
    public partial class LoginForm : KryptonForm
    {

        static string decryptionPassword = "c369";  // password for account management
        OleDbConnection dbConnect = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=dbUsers.mdb;Jet OLEDB:Database Password={decryptionPassword}");
        OleDbCommand dbCommand = new OleDbCommand();
        OleDbDataAdapter dbAdapter = new OleDbDataAdapter();
        WaitFormFunc loadingForm = new WaitFormFunc();

        public LoginForm()
        {
            InitializeComponent();
            cbProducts.SelectedIndex = 1;
            this.FormClosing += new FormClosingEventHandler(_FormClosing);
        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            dbConnect.Open();
            string login = "SELECT * FROM ConnproMembers WHERE dbId= @dbId AND dbPassword = @dbPassword";
            dbCommand = new OleDbCommand(login, dbConnect);
            dbCommand.Parameters.AddWithValue("@dbId", tbId.Text);
            dbCommand.Parameters.AddWithValue("@dbPassword", tbPassword.Text);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();
            EngineerForm mainForm = new EngineerForm(true);

            if (dbReader.Read())
            {
                loadingForm.Show(this);
                string permission = dbReader["dbPermissions"].ToString();
                mainForm.SetPermissions(permission);
                mainForm.SetProduct(cbProducts.SelectedItem.ToString());
                mainForm.Show();
                loadingForm.Close();
                this.Hide();
            }
            else
            {
                //loadingForm.Close();
                tbId.Text = "";
                tbPassword.Text = "";
                tbId.Focus();
                MessageBox.Show("Oops, seems like the account or password is not valid, Please try again.", "Login failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dbConnect.Close();
        }

        private void TbLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bLogin_Click(sender, e);
            }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            tbId.Text = "";
            tbPassword.Text = "";
            tbId.Focus();
        }

        private void cbShowPasswrod_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPasswrod.Checked)
            {
                tbPassword.PasswordChar = '\0';
            }
            else
            {
                tbPassword.PasswordChar = '•';
            }
        }

        private void lCreateAccount_Click(object sender, EventArgs e)
        {
            AdminAuthentication adminAuthForm = new AdminAuthentication();
            adminAuthForm.FormClosed += (s, args) => {
                this.Show();
            };
            this.Hide();
            adminAuthForm.ShowDialog();
        }

        private void _FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void lBackToMain_Click(object sender, EventArgs e)
        {
            Application.Restart();
            var process = Process.GetCurrentProcess();
            process.WaitForInputIdle();
            SetForegroundWindow(process.MainWindowHandle);
        }
        
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
