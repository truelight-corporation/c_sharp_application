using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComponentFactory.Krypton.Toolkit;
using System.Threading;
using System.Diagnostics;

namespace IntegratedGuiV2
{
    public partial class AccountsManagement : KryptonForm
    {
        DataTable userDataTable = new DataTable();
        static string decryptionPassword = "c369";  // password for account management database
        OleDbConnection dbConnect = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=dbUsers.mdb;Jet OLEDB:Database Password={decryptionPassword}");
        OleDbCommand dbCommand = new OleDbCommand();
        OleDbDataAdapter dbAdapter = new OleDbDataAdapter();

        public AccountsManagement()
        {
            InitializeComponent();
            LoadData();

            cbPermissions.SelectedIndex = 0;
            this.FormClosing += new FormClosingEventHandler(_FormClosing);

        }

        private void LoadData()
        {
            userDataTable.Clear();

            dbConnect.Open();
            string selectQuery = "SELECT * FROM ConnproMembers";
            dbAdapter = new OleDbDataAdapter(selectQuery, dbConnect);
            dbAdapter.Fill(userDataTable);
            dataGridView1.DataSource = userDataTable;
            dbConnect.Close();
        }


        private void bRegister_Click(object sender, EventArgs e)
        {
            if (tbId.Text == "" && tbPassword.Text == "")
            {
                MessageBox.Show("ID and Password fields are empty", "Registeration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (tbPassword.Text != "")
            {
                using (OleDbConnection con = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=dbUsers.mdb;Jet OLEDB:Database Password={decryptionPassword}"))
                using (OleDbCommand cmd = new OleDbCommand())

                {
                    if (IsIdExists(tbId.Text))
                    {
                        MessageBox.Show("This account already exists. Please choose another account.", "Registration failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bClear_Click(sender, e);
                        return;
                    }

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO ConnproMembers (dbId, dbPassword, dbPermissions) VALUES (?, ?, ?)";

                    cmd.Parameters.Add(new OleDbParameter("@dbId", OleDbType.VarChar)).Value = tbId.Text;
                    cmd.Parameters.Add(new OleDbParameter("@dbPassword", OleDbType.VarChar)).Value = tbPassword.Text;
                    cmd.Parameters.Add(new OleDbParameter("@dbPermissions", OleDbType.VarChar)).Value = cbPermissions.Text;

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Passwords does not match, please Re-enter", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbPassword.Text = "";
                tbPassword.Focus();
            }
        }

        private bool IsIdExists(string id)
        {
            using (OleDbConnection con = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=dbUsers.mdb;Jet OLEDB:Database Password={decryptionPassword}"))
            using (OleDbCommand cmd = new OleDbCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(*) FROM ConnproMembers WHERE dbId = ?";

                cmd.Parameters.Add(new OleDbParameter("@dbId", OleDbType.VarChar)).Value = id;

                con.Open();
                int count = (int)cmd.ExecuteScalar();
                con.Close();

                return count > 0;
            }
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete the selected account？", "Confirm the deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string selectedUserId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    string deleteQuery = "DELETE FROM ConnproMembers WHERE dbId = ?";

                    dbConnect.Open();
                    dbCommand = new OleDbCommand(deleteQuery, dbConnect);
                    dbCommand.Parameters.AddWithValue("@dbId", selectedUserId);
                    dbCommand.ExecuteNonQuery();
                    dbConnect.Close();
                    LoadData();
                }
                
            }
            else
            {
                MessageBox.Show("Please choose the account you want to delete\n " +
                                "Tip: Make sure your selection on a row-by-row basis.", "Oops, couldn't delete.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            tbId.Text = "";
            tbPassword.Text = "";
            tbId.Focus();
        }

        private void lBackToLogin_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void TbRegister_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bRegister_Click(sender, e);
            }
        }

        private void lExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
