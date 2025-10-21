using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using System.Xml;
using ComponentFactory.Krypton.Toolkit;
using Ionic.Zip;
using NuvotonIcpTool;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IntegratedGuiV2
{
    
    public partial class MainForm: KryptonForm
    {
        private EngineerForm engineerForm; // Assist processing for i2c communication
        private System.Windows.Forms.Timer timer;
        private int SequenceIndexA = 0, SequenceIndexB = 0;
        private int SequenceIndexDirectionA = 0, SequenceIndexDirectionB = 0;
        private int ForceOpenSas4 = 0, ForceOpenPcie4 = 0, ForceOpenQsfp28 = 0;
        private int ForceControl1 = 0, ForceControl2 = 0, ForceControl3 = 0;
        private bool DoubleSideMode = false;
        private int ProcessingChannel = 1;
        private int SerialNumber = 1;
        private int MinSerialNumber = 1;
        private int MaxSerialNumber = 9999;
        private bool DemoMode = false;
        private bool DebugMode = false;
        private bool FirstRound = true;
        private bool RxPowerUpdate = false;
        private bool I2cConnected = false;
        private bool ForceConnectWithoutInvoke = false;
        private bool Sas3Module = false;
        private bool IsListeningForHideKeys = false;
        private bool IsSwitching = false;
        private string CurrentDate = DateTime.Now.ToString("yyMMdd");
        private int Revision = 1;
        private string TempFolderPath = string.Empty;
        private CancellationTokenSource cancellationTokenSource;

        private Thread RxPowerUpdateThread;
        private bool ContinueRxPowerUpdate = true;
        private WaitFormFunc loadingForm = new WaitFormFunc();
        private List<NamingRuleModel> namingRules;
        private Dictionary<string, DomainUpDown> fieldControls;
        private Dictionary<string, Label> lables;

        public class NamingRuleModel
        {
            public string RuleName { get; set; }
            public List<string> Fields { get; set; }
        }

        private void InitializeNamingRules()
        {
            namingRules = new List<NamingRuleModel>
            {
                new NamingRuleModel
                {
                    RuleName = "Rule 1: YYMMDDRRSSSS",
                    Fields = new List<string> { "YY", "MM", "DD", "RR", "SSSS" }
                },
                new NamingRuleModel
                {
                    RuleName = "Rule 2: YYWWDLSSSS",
                    Fields = new List<string> { "YY", "WW", "D", "L", "SSSS" }
                },
                new NamingRuleModel
                {
                    RuleName = "Rule 3: YYMMDDSSSS",
                    Fields = new List<string> { "YY", "MM", "DD", "SSSS" }
                }
            };
        }

        private int GetCurrentWeekOfYear()
        {
            DateTime currentDate = DateTime.Now;

            System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CurrentCulture;

            int weekOfYear = culture.Calendar.GetWeekOfYear(
                currentDate,
                System.Globalization.CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);

            return weekOfYear;
        }

        private int GetCurrentWorkDay()
        {
            DayOfWeek dayOfWeek = DateTime.Now.DayOfWeek;

            if (dayOfWeek >= DayOfWeek.Monday && dayOfWeek <= DayOfWeek.Friday)
                return (int)dayOfWeek;
            else
                return 1;
        }

        private void BindNamingRulesToComboBox()
        {
            foreach (var rule in namingRules) {
                cbSnNamingRule.Items.Add(rule.RuleName);
            }
            cbSnNamingRule.SelectedIndex = 0;
        }

        private void GenerateDynamicComponents(NamingRuleModel rule)
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();

            foreach (var field in rule.Fields) {
                var label = new Label {
                    Name = $"l{field}",
                    Text = field,
                };

                var domainUpDown = new DomainUpDown {
                    Name = $"dud{field}",
                    Tag = field,
                };

                domainUpDown.TextChanged += domainUpDown_TextChanged;
                InitializeDynamicItems(label, domainUpDown, field);
                flowLayoutPanel1.Controls.Add(label);
                flowLayoutPanel2.Controls.Add(domainUpDown);
            }
        }

        
        private void InitializeDynamicItems(Label label, DomainUpDown domainUpDown, string field)
        {
            switch (field) {
                case "YY":
                    label.Text = "YY";
                    label.Width = 40;
                    for (int i = 30; i >= 15; i--)
                        domainUpDown.Items.Add(i.ToString("D2"));
                    domainUpDown.SelectedItem = DateTime.Now.ToString("yy");
                    domainUpDown.Width = 40;
                    break;

                case "MM":
                    label.Text = "MM";
                    label.Width = 40;
                    for (int i = 12; i >= 1; i--)
                        domainUpDown.Items.Add(i.ToString("D2"));
                    domainUpDown.SelectedItem = DateTime.Now.ToString("MM");
                    domainUpDown.Width = 40;
                    break;

                case "DD":
                    label.Text = "DD";
                    label.Width = 40;
                    for (int i = 31; i >= 1; i--)
                        domainUpDown.Items.Add(i.ToString("D2"));
                    domainUpDown.SelectedItem = DateTime.Now.ToString("dd");
                    domainUpDown.Width = 40;
                    break;

                case "RR":
                    label.Text = "RR";
                    label.Width = 40;
                    for (int i = 99; i >= 1; i--)
                        domainUpDown.Items.Add(i.ToString("D2"));
                    domainUpDown.SelectedItem = "01";
                    domainUpDown.Width = 40;
                    break;

                case "SSSS":
                    label.Text = "SSSS";
                    label.Width = 55;
                    for (int i = 9999; i >= 1; i--)
                        domainUpDown.Items.Add(i.ToString("D4"));
                    domainUpDown.SelectedItem = "0001";
                    domainUpDown.Width = 55;
                    break;

                case "WW":
                    label.Text = "WW";
                    label.Width = 40;
                    for (int i = 52; i >= 1; i--)
                        domainUpDown.Items.Add(i.ToString("D2"));
                    int currentWeek = GetCurrentWeekOfYear();
                    if (domainUpDown.Items.Contains(currentWeek.ToString("D2")))
                        domainUpDown.SelectedItem = currentWeek.ToString("D2");
                    else
                        MessageBox.Show($"Current week {currentWeek} not found in DomainUpDown.");

                    domainUpDown.Width = 40;

                    for (int i = 12; i >= 1; i--)
                        dudMm2.Items.Add(i.ToString("D2"));
                    dudMm2.SelectedItem = DateTime.Now.ToString("MM");

                    for (int i = 31; i >= 1; i--)
                        dudDd2.Items.Add(i.ToString("D2"));
                    dudDd2.SelectedItem = DateTime.Now.ToString("dd");

                    break;

                case "D":
                    label.Text = "D";
                    label.Width = 30;
                    for (int i = 5; i >= 1; i--)
                        domainUpDown.Items.Add(i.ToString());
                    int currentDay = GetCurrentWorkDay();
                    domainUpDown.SelectedItem = currentDay.ToString();
                    domainUpDown.Width = 30;
                    break;

                case "L":
                    label.Text = "L";
                    label.Width = 30;
                    for (int i = 4; i >= 1; i--)
                        domainUpDown.Items.Add(i.ToString());
                    domainUpDown.SelectedItem = "1";
                    domainUpDown.Width = 30;
                    break;
            }
        }

        private void cbSnNamingRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            gbCodeEditor.Select();

            if (cbSnNamingRule.SelectedIndex >= 0 && namingRules != null) {
                var selectedRule = namingRules[cbSnNamingRule.SelectedIndex];
                GenerateDynamicComponents(selectedRule);
            }
            else {
                MessageBox.Show("Please select a valid naming convention.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _UpdateSerialNumberTextBox();
        }

        private string GenerateSerialNumber()
        {
            StringBuilder serialNumber = new StringBuilder();

            var domainUpDowns = flowLayoutPanel2.Controls.OfType<DomainUpDown>();

            foreach (var dud in domainUpDowns) {
                serialNumber.Append(dud.Text);
            }

            return serialNumber.ToString();
        }

        private void MainForm_MainMessageUpdated(object sender, MessageEventArgs e)
        {
            if (ProcessingChannel == 1) {
                BeginInvoke(new Action(() =>
                {
                    lCh1Message.Text = e.Message;
                    cProgressBar1.Value = e.ProgressValue;
                    cProgressBar1.Text = cProgressBar1.Value.ToString() + "%";
                }));
            }
            else if (ProcessingChannel == 2) {
                BeginInvoke(new Action(() =>
                {
                    lCh2Message.Text = e.Message;
                    cProgressBar2.Value = e.ProgressValue;
                    cProgressBar2.Text = cProgressBar2.Value.ToString() + "%";
                }));
            }
        }

        private void domainUpDown_TextChanged(object sender, EventArgs e)
        {
            _SerialNumberRule2Control();
            _UpdateSerialNumberTextBox();
        }

        private void _UpdateSerialNumberTextBox()
        {
            string yy = _GetDomainUpDownValue("dudYy");
            string mm = _GetDomainUpDownValue("dudMm");
            string mm2 = !string.IsNullOrEmpty(dudMm2.Text) ? dudMm2.Text : DateTime.Now.ToString("MM");
            string dd = _GetDomainUpDownValue("dudDd");
            string dd2 = !string.IsNullOrEmpty(dudDd2.Text) ? dudDd2.Text : DateTime.Now.ToString("dd");
            string rr = _GetDomainUpDownValue("dudRr");
            string ssss = _GetDomainUpDownValue("dudSsss");
            string ww = _GetDomainUpDownValue("dudWw");
            string d = _GetDomainUpDownValue("dudD");
            string l = _GetDomainUpDownValue("dudL");

            int selectedRuleIndex = cbSnNamingRule.SelectedIndex;

            if (selectedRuleIndex == 0) {
                tbVenderSn.Text = yy + mm + dd + rr + ssss;
                tbDateCode.Text = yy + mm + dd;
            }
            else if (selectedRuleIndex == 1) {
                tbVenderSn.Text = yy + ww + d + l + ssss;
                tbDateCode.Text = yy + mm2 + dd2;
            }
            else if (selectedRuleIndex == 2) {
                tbVenderSn.Text = yy + mm + dd + ssss;
                tbDateCode.Text = yy + mm + dd;
            }
        }

        private void _SerialNumberRule2Control()
        {
            if (cbSnNamingRule.SelectedIndex == 1) {
                lMm2.Visible = true;
                lDd2.Visible = true;
                dudMm2.Visible = true;
                dudDd2.Visible = true;
            }
            else {
                lMm2.Visible = false;
                lDd2.Visible = false;
                dudMm2.Visible = false;
                dudDd2.Visible = false;
            }
        }

        private string _GetDomainUpDownValue(string name)
        {
            var control = flowLayoutPanel2.Controls.Find(name, true).FirstOrDefault()
                 ?? this.Controls.Find(name, true).FirstOrDefault(); // 如果找不到則從主表單尋找

            return control?.Text ?? string.Empty;
        }

        private void _SetDomainUpDownValue(string name, int value)
        {
            var control = flowLayoutPanel2.Controls.Find(name, true).FirstOrDefault()
                         ?? this.Controls.Find(name, true).FirstOrDefault(); // 如果找不到則從主表單尋找

            if (control is DomainUpDown domainUpDown) {
                if (value > 9999) {
                    value %= 9999;
                    MessageBox.Show($"Value exceeded 9999. Adjusted to: {value}",
                                    "Value Adjusted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                domainUpDown.Text = value.ToString("D4");
            }
            else {
                MessageBox.Show($"Control with name '{name}' not found or is not a DomainUpDown.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindEvents()
        {
            //dudYy.TextChanged += new EventHandler(domainUpDown_TextChanged);
            //dudMm.TextChanged += new EventHandler(domainUpDown_TextChanged);
            dudMm2.TextChanged += new EventHandler(domainUpDown_TextChanged);
            //dudDd.TextChanged += new EventHandler(domainUpDown_TextChanged);
            dudDd2.TextChanged += new EventHandler(domainUpDown_TextChanged);
            //dudRr.TextChanged += new EventHandler(domainUpDown_TextChanged);
            //dudSsss.TextChanged += new EventHandler(domainUpDown_TextChanged);
            //dudWw.TextChanged += new EventHandler(domainUpDown_TextChanged);
            //dudD.TextChanged += new EventHandler(domainUpDown_TextChanged);
            //dudL.TextChanged += new EventHandler(domainUpDown_TextChanged);
        }
        
        private void HandlePluginWaiting(bool isWaiting)
        {
            if (isWaiting )
                if (!ForceConnectWithoutInvoke)
                    loadingForm.OnPluginWaiting();
        }
                
        private void HandlePluginDetected(bool isDetected)
        {
            if (isDetected) {
                if (!ForceConnectWithoutInvoke)
                    loadingForm.PluginDetected();
                else
                    MessageBox.Show("Get it!");
            }
        }

        public MainForm()
        {
            InitializeComponent();
            InitializeNamingRules();
            BindNamingRulesToComboBox();
            BindEvents();
            _UpdateSerialNumberTextBox();
            engineerForm = new EngineerForm(true);
            this.FormClosing += new FormClosingEventHandler(_FormClosing);
            _AdjustGuiSizeAndCenter(550, 280);
            this.Text = "OptiSync Manager";
            cProgressBar1.Value = 0;
            cProgressBar2.Value = 0;
            this.Load += MainForm_Load;
            this.KeyPreview = true;
            this.KeyDown += _HideKeys_KeyDown;
            this.tbVenderSn.KeyDown += TbVenderSn_KeyDown;
            this.tbCustomerSn.KeyDown += TbCustomerSn_KeyDown;
            engineerForm.OnPluginWaiting += HandlePluginWaiting;
            engineerForm.OnPluginDetected += HandlePluginDetected;
            tbHideKey.Enter += tbHideKey_MouseEnter;
            tbHideKey.Leave += tbHideKey_MouseLeave;

            if (!(engineerForm.ChannelSetApi(1) < 0)) // (bool setMode, bool setPassword)
                I2cConnected = true;
            else {
                MessageBox.Show("I2c master connection failed.\nPlease check if the hardware configuration or UI is activated.",
                                "I2c master connection failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.FormClosing -= _FormClosing;
                this.Close();
                Application.ExitThread();
                Environment.Exit(0);
            }

            engineerForm.ReadStateUpdated += MainForm_ReadStateUpdated;
            engineerForm.ProgressValue += MainForm_ProgressUpdated;
            engineerForm.MainMessageUpdated += MainForm_MainMessageUpdated;

            //ucMainForm initial...
            if (DebugMode) {
                bool beforeTestMode = engineerForm.GetVarBoolStateFromNuvotonIcpApi("TestMode");
                engineerForm.SetVarBoolStateToNuvotonIcpApi("TestMode", true);
                MessageBox.Show("Icp TestMode state...\n\nBefore: " + beforeTestMode
                                + "\n\nAfter: " + engineerForm.GetVarBoolStateFromNuvotonIcpApi("TestMode")
                                );
                //mainForm?.SelectProduct("SAS4.0"); Force switch
                engineerForm?.SetCheckBoxCheckedByNameApi("cbInfomation", false);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbTxIcConfig", false);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbRxIcConfig", true);

                var checkBoxStates = engineerForm.GetCheckBoxStates();
                var items = String.Join(", ", engineerForm.GetComboBoxItems());
                string selectedProduct = engineerForm.GetSelectedProduct();


                var BeforecbInformation = checkBoxStates["cbInfomation"];
                var BeforecbTxIcConfig = checkBoxStates["cbTxIcConfig"];
                var BeforecbRxIcConfig = checkBoxStates["cbRxIcConfig"];
                var BeforeItems = items;
                var BeforeSelectedProducts = selectedProduct;

                engineerForm?.SetCheckBoxCheckedByNameApi("cbInfomation", true);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbTxIcConfig", true);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbRxIcConfig", true);

                checkBoxStates = engineerForm.GetCheckBoxStates();
                MessageBox.Show("CheckBox state...\n\nBefore: "
                                + "\n   cbInfomation state: " + BeforecbInformation
                                + "\n   cbTxIcConfig state: " + BeforecbTxIcConfig
                                + "\n   cbRxIcConfig state: " + BeforecbRxIcConfig
                                + "\n   cbProductSelect items: " + BeforeItems
                                + "\n   cbProductSelect state: " + BeforeSelectedProducts
                                + "\n\nAfter:\n   cbInfomation state: " + checkBoxStates["cbInfomation"]
                                + "\n   cbTxIcConfig state: " + checkBoxStates["cbTxIcConfig"]
                                + "\n   cbRxIcConfig state: " + checkBoxStates["cbRxIcConfig"]
                                + "\n   cbProductSelect state: " + selectedProduct
                                );
            }
            else {
                engineerForm?.SetCheckBoxCheckedByNameApi("cbInfomation", true);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbDdm", true);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbMemDump", true);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbCorrector", true);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbTxIcConfig", true);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbRxIcConfig", true);
            }

            //ucNuvotonICP initial...
            if (DebugMode) {
                MessageBox.Show("ReLink and Erase APROM...Done");
                bool beforeSecurityLock = engineerForm.GetCheckBoxStateFromNuvotonIcpApi("cbSecurityLock");
                engineerForm.SetCheckBoxStateToNuvotonIcpApi("cbSecurityLock", false);
                engineerForm.UpdateSecurityLockStateFromNuvotonIcpApi();
                MessageBox.Show("SecurityLock state...\n\nBefore: " + beforeSecurityLock
                                + "\n\nAfter: " + engineerForm.GetCheckBoxStateFromNuvotonIcpApi("cbSecurityLock")
                                );

                var BeforecbLDROM = engineerForm.GetCheckBoxStateFromNuvotonIcpApi("cbLDROM");
                var BeforecbAPROM = engineerForm.GetCheckBoxStateFromNuvotonIcpApi("cbAPROM");
                var BeforecbDATAROM = engineerForm.GetCheckBoxStateFromNuvotonIcpApi("cbDataFlash");
                string pathLDROM = engineerForm.GetTextBoxTextFromNuvotonIcpApi("tbLDROM");
                string pathAPROM = engineerForm.GetTextBoxTextFromNuvotonIcpApi("tbAPROM");
                string pathDATAROM = engineerForm.GetTextBoxTextFromNuvotonIcpApi("tbDataFlash");

                engineerForm.SetCheckBoxStateToNuvotonIcpApi("cbLDROM", false);
                engineerForm.SetCheckBoxStateToNuvotonIcpApi("cbAPROM", true);
                engineerForm.SetCheckBoxStateToNuvotonIcpApi("cbDataFlash", false);

                MessageBox.Show("Flashing checkBox state...\n\nBefore: "
                                + "\n   cbLDROM: " + BeforecbLDROM
                                + "\n   cbAPROM: " + BeforecbAPROM
                                + "\n   cbDataFlash: " + BeforecbDATAROM
                                + "\n   tbLDROM: " + pathLDROM
                                + "\n   tbAPROM: " + pathAPROM
                                + "\n   tbcbDataFlash: " + pathDATAROM
                                + "\n\nAfter:\n   cbLDROM: " + engineerForm.GetCheckBoxStateFromNuvotonIcpApi("cbLDROM")
                                + "\n   cbAPROM: " + engineerForm.GetCheckBoxStateFromNuvotonIcpApi("cbAPROM")
                                + "\n   cbDataFlash: " + engineerForm.GetCheckBoxStateFromNuvotonIcpApi("cbDataFlash")
                                );
            }
            else {
                engineerForm.SetCheckBoxStateToNuvotonIcpApi("cbLDROM", false);
                engineerForm.SetCheckBoxStateToNuvotonIcpApi("cbAPROM", true);
                engineerForm.SetCheckBoxStateToNuvotonIcpApi("cbDataFlash", false);
                engineerForm.SetCheckBoxStateToNuvotonIcpApi("cbSecurityLock", true);
            }

            engineerForm.UpdateSecurityLockStateFromNuvotonIcpApi();
        }

        private void TbHideKey_Leave(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Activate();
            this.BringToFront();
            tbHideKey.Select();
        }

        private void _InitialUi()
        {
            lCh1EC.Text = "...";
            lCh2EC.Text = "...";
            lCh1Message.Text = "...";
            lCh2Message.Text = "...";
            cProgressBar1.Text = "A";
            cProgressBar1.ForeColor = Color.FromArgb(85,213,219);
            cProgressBar2.Text = "B";
            cProgressBar2.ForeColor = Color.FromArgb(85, 213, 219);
            cProgressBar1.Value = 0;
            cProgressBar2.Value = 0;
            tbVersionCodeCh1.Text = "";
            tbVersionCodeCh2.Text = "";
            tbVersionCodeReNewCh1.Text = "";
            tbVersionCodeReNewCh2.Text = "";
            tbOrignalSNCh1.Text = "";
            tbOrignalSNCh2.Text = "";
            tbReNewSNCh1.Text = "";
            tbReNewSNCh2.Text = "";

            tbOrignalTLSN.Text = "";
            tbReNewTLSN.Text = "";
            lStatus.TextAlign = ContentAlignment.MiddleCenter;

            tbVersionCodeCh1.BackColor = Color.White;
            tbVersionCodeCh2.BackColor = Color.White;
            tbVersionCodeReNewCh1.BackColor = Color.White;
            tbVersionCodeReNewCh2.BackColor = Color.White;
            tbOrignalSNCh1.BackColor = Color.LightBlue;
            tbOrignalSNCh2.BackColor = Color.LightBlue;
            tbReNewSNCh1.BackColor = Color.LightBlue;
            tbReNewSNCh2.BackColor = Color.LightBlue;
            tbOrignalSNCh1.ForeColor = Color.Black;
            tbOrignalSNCh2.ForeColor = Color.Black;
            tbReNewSNCh1.ForeColor = Color.Black;
            tbReNewSNCh2.ForeColor = Color.Black;
            lCh1Message.ForeColor = Color.White;
            lCh2Message.ForeColor = Color.White;
            tbCustomerSn.BackColor = Color.White;
            tbTruelightSn.BackColor = Color.White;
            tbOrignalTLSN.BackColor = Color.White;
            tbReNewTLSN.BackColor = Color.White;

            bStart.Select();
            _InitialRssiTextBox();
            _InitializeUIForgbPrompt();
            /*
            if (!FirstRound) {
                for (int i = 100; i > -1; i -= 5)// For ProgressBar renew
                {
                    cProgressBar1.Value = i;
                    cProgressBar1.Text = "" + i + "%";
                    cProgressBar2.Value = i;
                    cProgressBar2.Text = "" + i + "%";
                    Thread.Sleep(1);
                }
            }
            */
            Application.DoEvents();
        }

        private void _InitializeUIForgbPrompt()
        {
            //gbPrompt.Visible = true;
            gbPrompt.BackColor = Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(145)))), ((int)(((byte)(168)))));
            lStatus.Visible = true;
            lStatus.Text = "...";
            tbOrignalTLSN.BackColor = Color.White;
            tbOrignalTLSN.Text = "";
            tbReNewTLSN.BackColor = Color.White;
            tbReNewTLSN.Text = "";

            Application.DoEvents();
        }

        private void _HideKeys_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsListeningForHideKeys)
                return;

            Keys[][] expectedKeys = {
                new Keys[] { Keys.D, Keys.NumPad4, Keys.NumPad4, Keys.NumPad6, Keys.NumPad6 },
                new Keys[] { Keys.D, Keys.NumPad8, Keys.NumPad8, Keys.NumPad2, Keys.NumPad2 },
                new Keys[] { Keys.C, Keys.O, Keys.N, Keys.N, Keys.P, Keys.R, Keys.O },
                new Keys[] { Keys.C, Keys.NumPad2, Keys.NumPad3, Keys.NumPad6, Keys.NumPad8 },
                new Keys[] { Keys.D,Keys.NumPad1, Keys.NumPad1, Keys.NumPad1, Keys.Down},
                new Keys[] { Keys.D,Keys.NumPad2, Keys.NumPad2, Keys.NumPad2, Keys.Down},
                new Keys[] { Keys.D,Keys.NumPad3, Keys.NumPad3, Keys.NumPad3, Keys.Down},
                new Keys[] { Keys.T, Keys.I, Keys.M, Keys.E},
                new Keys[] { Keys.D, Keys.NumPad3, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4},
                new Keys[] { Keys.D, Keys.NumPad0, Keys.NumPad0, Keys.NumPad8, Keys.NumPad0}
                };

            Action[] actions = {
                () => _OpenLoginForm(),
                () => _OpenAdminAuthenticationForm(),
                () => _OpenLoginForm(),
                () => _OpenAdminAuthenticationForm(),
                () => _SelectModelBeforeActivateEngineerMode(0), // SAS4
                () => _SelectModelBeforeActivateEngineerMode(1), // PCIe4
                () => _SelectModelBeforeActivateEngineerMode(2), // QSFP28
                () => _DisplayRelinkControlUi(),
                () => _DirectDriveMpMode(),
                () => _EnableHiddenEngineerMode(),
            };

            int[] sequenceIndices = {   SequenceIndexA, SequenceIndexB, 
                                        SequenceIndexDirectionA, SequenceIndexDirectionB,
                                        ForceOpenSas4, ForceOpenPcie4, ForceOpenQsfp28,
                                        ForceControl1, ForceControl2 ,ForceControl3};

            for (int i = 0; i < expectedKeys.Length; i++) {
                if (sequenceIndices[i] < expectedKeys[i].Length &&
                    e.KeyCode == expectedKeys[i][sequenceIndices[i]]) {
                    sequenceIndices[i]++;

                    if (CheckSequenceComplete(sequenceIndices[i], expectedKeys[i])) {
                        I2cConnected = (engineerForm.I2cMasterDisconnectApi() < 0);
                        actions[i]();
                        _ResetSequence();
                    }
                }
                else {
                    _ResetSequence();
                }
            }

            SequenceIndexA = sequenceIndices[0];
            SequenceIndexB = sequenceIndices[1];
            SequenceIndexDirectionA = sequenceIndices[2];
            SequenceIndexDirectionB = sequenceIndices[3];
            ForceOpenSas4 = sequenceIndices[4];
            ForceOpenPcie4 = sequenceIndices[5];
            ForceOpenQsfp28 = sequenceIndices[6];
            ForceControl1 = sequenceIndices[7];
            ForceControl2 = sequenceIndices[8];
            ForceControl3 = sequenceIndices[9];
        }

        private void _ResetSequence()
        {
            SequenceIndexA = 0;
            SequenceIndexB = 0;
            SequenceIndexDirectionA = 0;
            SequenceIndexDirectionB = 0;
            ForceOpenSas4 = 0;
            ForceOpenPcie4 = 0;
            ForceOpenQsfp28 = 0;
            ForceControl1 = 0;
            ForceControl2 = 0;
            ForceControl3 = 0;
        }

        private bool CheckSequenceComplete(int currentIndex, Keys[] expectedKeys)
        {
            return currentIndex == expectedKeys.Length;
        }

        private void _EnableHiddenEngineerMode()
        {
            _DirectDriveMpMode();
            _ExtendFormForEngineerMode();
        }

        private void _ExtendFormForEngineerMode()
        {
            cbBarcodeMode_CheckedChanged(null, null);

            cbBarcodeMode.Checked = true;
            cbRegisterMapView.Checked = true;
            rbFullMode.Visible = false;
            rbOnlySN.Visible = false;
            rbLogFileMode.Visible = false;
            _AdjustGuiSizeAndCenter(700, 550);

            tbVenderSn.Enabled = false;
            cbAutoWirte.Checked = true;
            bWriteTruelightSn.Enabled = false;
            tbCustomerSn.Enabled = true;
            tbTruelightSn.Enabled = false;
            tbCustomerSn.Select();
        }

        private void TbCustomerSn_KeyDown(object sender, KeyEventArgs e)
        {
            int errorCode = 0;
            cbRegisterMapView.Checked = true;

            if (e.KeyCode == Keys.Enter && tbCustomerSn.Text.Length == 12 &&
                   tbCustomerSn.Focused && !cbDeepCheck.Checked) {
                
                _DisableButtons();
                tbVenderSn.Text = "";
                _UIActionForReWriteSerialNumber(true);
                _InitialUi();

                if (cbAutoWirte.Checked)
                    tbCustomerSn.Enabled = false;

                _GetTruelightSn(true, 1);
                tbVenderSn.Text = tbCustomerSn.Text;
                tbDateCode.Text = tbVenderSn.Text.Substring(0, 6);

                if (_SerialNumberSerach() < 0) {
                    _PromptError(false, ProcessingChannel);
                    tbTruelightSn.Text = "Search failed";
                    _InitialReWriteSnForFinish();
                    return;
                }

                if (cbReParameter.Checked) {
                    errorCode = _SetParameterFromPage8081();
                    if (errorCode < 0) {
                        _PromptError(false, ProcessingChannel);
                        MessageBox.Show("ErrorCode: " + errorCode);
                        return;
                    }
                }

                errorCode = _WriteTruelightSn(tbTruelightSn.Text);
                if (errorCode < 0) {
                    _PromptError(false, ProcessingChannel);
                    MessageBox.Show("ErrorCode: " + errorCode);
                    return;
                }
                else
                    _PromptCompleted(ProcessingChannel, false); //ToDo

                if (cbReParameter.Checked && _WriteSnDateCodeFlow() < 0)
                    _PromptError(false, ProcessingChannel);
                else
                    _PromptCompleted(ProcessingChannel, true); //ToDo

                _GetTruelightSn(false, 1);
                _InitialReWriteSnForFinish();
            }
            else if (e.KeyCode == Keys.Enter && tbCustomerSn.Text.Length == 12 &&
                        tbCustomerSn.Focused && cbDeepCheck.Checked) {
                _InformationCheck(true);
            }
            else if (e.KeyCode == Keys.Enter && tbCustomerSn.Text.Length != 12 && tbCustomerSn.Focused) {
                MessageBox.Show("Please enter 12 characters.");
                //tbCustomerSn.Text = "";
            }

        exit:
            _EnableButtons();
            tbCustomerSn.Select();
        }

        private void _InitialReWriteSnForFinish()
        {
            tbCustomerSn.Text = "";
            tbCustomerSn.Enabled = true;
            _UIActionForReWriteSerialNumber(false);
            tbCustomerSn.Select();
        }

        private void TbVenderSn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && cbBarcodeMode.Checked && tbVenderSn.Focused) {
                tbVenderSn.Enabled = false;
                _DisableButtons();
                _InitialUi();

                if (ValidateVenderSn() >= 0) {
                    tbDateCode.Text = tbVenderSn.Text.Substring(0, 6);
                    
                    if (rbFullMode.Checked) { //Handle CfgFile
                        bool isCustomerMode = lMode.Text == "Customer";

                        if ((isCustomerMode || lMode.Text == "MP") && 
                            _Processor(isCustomerMode) < 0) {
                            //MessageBox.Show("There are some problems", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            tbVenderSn.Text = "";
                            tbVenderSn.Enabled = true;
                            tbVenderSn.Select();
                            _CloseLoadingFormAndReturn(-1);
                        }

                        if (cbBarcodeMode.Checked && _WriteSnDateCodeFlow() < 0)
                            _PromptError(false, ProcessingChannel);
                        else
                            _PromptCompleted(ProcessingChannel, true);
                    }

                    else if (rbOnlySN.Checked) {
                        if (cbBarcodeMode.Checked && _WriteSnDateCodeFlow() < 0)
                            _PromptError(false, ProcessingChannel);
                        else
                            _PromptCompleted(ProcessingChannel, true);
                    }

                    else if (rbLogFileMode.Checked) {
                        if (cbBarcodeMode.Checked)
                         _LogFileComparison();
                    }
                }

                if (!cbBarcodeMode.Checked)
                    _EnableButtons();

                tbVenderSn.Text = "";
                tbVenderSn.Enabled = true;
                loadingForm.Close();
                this.BringToFront();
                this.Activate();
                tbVenderSn.Select();
            }
        }

        private void _UpdateButtonState()
        {
            if (I2cConnected)
                cbI2cConnect.Checked = true;
            else if (!I2cConnected)
                cbI2cConnect.Checked = false;
        }

        private void _DirectDriveMpMode()
        {
            if (tbFilePath.Text == "Please click here, to import the Config file...") {
                tbFilePath.Text = "";
                tbFilePath.ForeColor = Color.MidnightBlue;
            }

            _CleanTempFolder(); // Before change zip file, Clean the tempFolder
            _DirectDriveToLoadXmlFile();

            if (lMode.Text == "Customer") {
                _AdjustGuiSizeAndCenter(550, 280);
                // rbSingle.Enabled = true;
                //rbBoth.Enabled = true;
                cbSecurityLock.Visible = false;
                gbOperatorMode.Visible = false;
                bWriteSnDateCone.Visible = false;
                tbOrignalSNCh1.Visible = false;
                tbOrignalSNCh2.Visible = false;
                tbReNewSNCh1.Visible = false;
                tbReNewSNCh2.Visible = false;
                lOriginalSN.Visible = false;
                lReNewSn.Visible = false;
                bCfgFileComparison.Visible = false;
                bLogFileComparison.Visible = false;
                bRenewRssi.Visible = false;
                cbCfgCheckAfterFw.Visible = false;
            }

            else if (lMode.Text == "MP") {
                _AdjustGuiSizeAndCenter(550, 550);
                rbSingle.Select();
                //rbSingle.Enabled = false;
                //rbBoth.Enabled = false;
                cbSecurityLock.Visible = true;
                gbOperatorMode.Visible = true;
                bWriteSnDateCone.Visible = true;
                tbOrignalSNCh1.Visible = true;
                tbOrignalSNCh2.Visible = true;
                tbReNewSNCh1.Visible = true;
                tbReNewSNCh2.Visible = true;
                lOriginalSN.Visible = true;
                lReNewSn.Visible = true;
                bCfgFileComparison.Visible = true;
                bLogFileComparison.Visible = true;
                bRenewRssi.Visible = true;
                cbCfgCheckAfterFw.Visible = true;

                if (rbBoth.Checked) {
                    tbOrignalSNCh2.Visible = true;
                    tbReNewSNCh2.Visible = true;
                }
                else {
                    tbOrignalSNCh2.Visible = false;
                    tbReNewSNCh2.Visible = false;
                }
            }

            I2cConnected = !(engineerForm.ChannelSetApi(1) < 0);
            _ResetSequence();
        }

        private void _SelectModelBeforeActivateEngineerMode(int productType) // 0_SAS4, 1_PCIe4,
        {
            EngineerForm mainForm = new EngineerForm(true);
            loadingForm.Show(this);
            mainForm.SetPermissions("Administrator");
            
            switch (productType) {
                case 0:
                    mainForm.SetProduct("SAS4.0");
                    break;
                case 1:
                    mainForm.SetProduct("PCIe4.0");
                    break;
                case 2:
                    mainForm.SetProduct("QSFP28");
                    break;
            }
            
            mainForm.Show();
            loadingForm.Close();
            this.Hide();
        }

        private void _OpenLoginForm()
        {
            LoginForm formB = new LoginForm();
            formB.Show();
            this.Hide();
        }

        private void _OpenAdminAuthenticationForm()
        {
            AdminAuthentication formC = new AdminAuthentication();
            formC.Show();
            this.Hide();
        }

        private void _DisplayRelinkControlUi()
        {
            gbRelinkTest.Visible = true;
            _ResetSequence();
        }

        private void MainForm_ReadStateUpdated(object sender, string e)
        {
            if (ProcessingChannel == 1) {
                if (lCh1Message.InvokeRequired) {
                    Invoke(new Action(() =>
                    {
                        lCh1Message.Text = e; // MainForm 送出 ReadStateUpdated event，update to Label.text
                    }));
                }
                else
                    lCh1Message.Text = e;
            }
            else if (ProcessingChannel == 2) {
                if (lCh2Message.InvokeRequired) {
                    Invoke(new Action(() =>
                    {
                        lCh2Message.Text = e;
                    }));
                }
                else
                    lCh2Message.Text = e;
            }

        }

        private void MainForm_ProgressUpdated(object sender, int e)
        {

            if (ProcessingChannel == 1) {
                if (cProgressBar1.InvokeRequired) {
                    Invoke(new Action(() =>
                    {
                        cProgressBar1.Value = e;
                        cProgressBar1.Text = cProgressBar1.Value.ToString() + "%";
                    }));
                }
                else {
                    cProgressBar1.Value = e;
                    cProgressBar1.Text = cProgressBar1.Value.ToString() + "%";
                }
            }
            else if (ProcessingChannel == 2) {
                if (cProgressBar2.InvokeRequired) {
                    Invoke(new Action(() =>
                    {
                        cProgressBar2.Value = e;
                        cProgressBar2.Text = cProgressBar2.Value.ToString() + "%";
                    }));
                }
                else {
                    cProgressBar2.Value = e;
                    cProgressBar2.Text = cProgressBar2.Value.ToString() + "%";
                }
            }
        }

        private int _RemoteInitial(bool cutomerMode) // True: Customer Mode, Flase: MP mode
        {
            string apromPath, dataromPath;
            string directoryPath;

            /*
            if (cbBypassW.Checked)
                mainForm.SetVarBoolStateToMainFormApi("BypassWriteIcConfig", true);
            */

            if ((string.IsNullOrEmpty(lProduct.Text))) {
                MessageBox.Show("The configuration file format is incorrect.");
                return -1;
            }

            else {
                string selectedProduct = lProduct.Text;
                engineerForm?.SelectProductApi(selectedProduct);
            }

            if (cutomerMode) // Customer Mode
            {
                engineerForm?.SetCheckBoxCheckedByNameApi("cbInfomation", true);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbDdm", true);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbMemDump", false);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbCorrector", true);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbTxIcConfig", false);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbRxIcConfig", false);
            }
            else // Mp Mode
            {
                engineerForm?.SetCheckBoxCheckedByNameApi("cbInfomation", true);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbDdm", true);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbMemDump", false);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbCorrector", true);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbTxIcConfig", false);
                engineerForm?.SetCheckBoxCheckedByNameApi("cbRxIcConfig", false);
            }

            if (!Sas3Module) { 
                if (!(string.IsNullOrEmpty(lApName.Text) || lApName.Text == "_")) {
                    engineerForm.SetCheckBoxStateToNuvotonIcpApi("cbAPROM", true);
                    directoryPath = TempFolderPath;
                    apromPath = Path.Combine(directoryPath, lApName.Text);
                    engineerForm.SetTextBoxTextToNuvotonIcpApi("tbAPROM", apromPath);
                    if (DebugMode) {
                        MessageBox.Show("TempFolderPath: \n" + TempFolderPath +
                                    "\n\nAPROM path: \n" + engineerForm.GetTextBoxTextFromNuvotonIcpApi("tbAPROM"));
                    }
                }
                else {
                    MessageBox.Show("The configuration file format is incorrect.\nAPROM path not specified.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }

                if (!(string.IsNullOrEmpty(lDataName.Text) || lDataName.Text == "_")) {
                    engineerForm.SetCheckBoxStateToNuvotonIcpApi("cbDataFlash", true);
                    directoryPath = TempFolderPath;
                    dataromPath = Path.Combine(directoryPath, lDataName.Text);
                    engineerForm.SetTextBoxTextToNuvotonIcpApi("tbDataFlash", dataromPath);
                    //MessageBox.Show("DATAROM path: \n" + mainForm.GetTextBoxTextFromNuvotonIcpApi("tbDataFlash"));
                }
                else
                    engineerForm.SetCheckBoxStateToNuvotonIcpApi("cbDataFlash", false);

                engineerForm.SetCheckBoxStateToNuvotonIcpApi("cbLDROM", false);

                if (cbSecurityLock.Checked) {
                    engineerForm.SetCheckBoxStateToNuvotonIcpApi("cbSecurityLock", true); // Dominic
                }
                else {
                    engineerForm.SetCheckBoxStateToNuvotonIcpApi("cbSecurityLock", false);
                }

                engineerForm.SetVarIntStateToNuvotonIcpApi("LinkState", 0);
                engineerForm.UpdateSecurityLockStateFromNuvotonIcpApi();
                Thread.Sleep(10);
            }

            return 0;
        }

        private void _SwitchOrNot()
        {
            if (rbSingle.Checked == true) {
                cProgressBar1.Visible = true;
                cProgressBar2.Visible = false;
                lCh1Message.Visible = true;
                lCh2Message.Visible = false;
                //lCh1EC.Visible = true;
                //lCh2EC.Visible = false;
                DoubleSideMode = false;
                tbVersionCodeCh2.Visible = false;
                tbVersionCodeReNewCh2.Visible = false;
                tbOrignalSNCh2.Visible = false;
                tbReNewSNCh2.Visible = false;
                lRxPowerCh2_0.Visible = false;
                lRxPowerCh2_1.Visible = false;
                lRxPowerCh2_2.Visible = false;
                lRxPowerCh2_3.Visible = false;
                tbRxPowerCh2_0.Visible = false;
                tbRxPowerCh2_1.Visible = false;
                tbRxPowerCh2_2.Visible = false;
                tbRxPowerCh2_3.Visible = false;
            }
            else if (rbBoth.Checked == true) {
                cProgressBar1.Visible = true;
                cProgressBar2.Visible = true;
                lCh1Message.Visible = true;
                lCh2Message.Visible = true;
                //lCh1EC.Visible = true;
                //lCh2EC.Visible = true;
                DoubleSideMode = true;
                tbVersionCodeCh2.Visible = true;
                tbVersionCodeReNewCh2.Visible = true;
                lRxPowerCh2_0.Visible = true;
                lRxPowerCh2_1.Visible = true;
                lRxPowerCh2_2.Visible = true;
                lRxPowerCh2_3.Visible = true;
                tbRxPowerCh2_0.Visible = true;
                tbRxPowerCh2_1.Visible = true;
                tbRxPowerCh2_2.Visible = true;
                tbRxPowerCh2_3.Visible = true;

                if (lMode.Text == "MP") {
                    tbOrignalSNCh2.Visible = true;
                    tbReNewSNCh2.Visible = true;
                }
                else {
                    tbOrignalSNCh2.Visible = false;
                    tbReNewSNCh2.Visible = false;
                }
            }
        }

        private void _StoreConfigurationPaths(string zipPath, string logFilePath, string rssiCriteria)
        {
            string exeFolderPath = Application.StartupPath;
            string combinedPath = Path.Combine(exeFolderPath, "XmlFolder");
            bool cfgFileCheckState = cbCfgCheckAfterFw.Checked;
            bool logFileCheckState = cbLogCheckAfterSn.Checked;
            bool registerMapViewState = cbRegisterMapView.Checked;

            if (!Directory.Exists(combinedPath))
                Directory.CreateDirectory(combinedPath);

            string xmlFilePath = Path.Combine(combinedPath, "MainFormPaths.xml");
            xmlFilePath = xmlFilePath.Replace("\\\\", "\\");

            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists(xmlFilePath)) {
                xmlDoc.Load(xmlFilePath);
            }
            else {
                XmlElement root = xmlDoc.CreateElement("Paths");
                xmlDoc.AppendChild(root);
            }

            XmlNode rootElement = xmlDoc.DocumentElement;

            if (!string.IsNullOrEmpty(zipPath)) {
                XmlElement zipFolderPathElement = rootElement.SelectSingleNode("FormPaths/ZipFolderPath") as XmlElement;
                if (zipFolderPathElement == null) {
                    XmlElement formPaths = rootElement.SelectSingleNode("FormPaths") as XmlElement;
                    if (formPaths == null) {
                        formPaths = xmlDoc.CreateElement("FormPaths");
                        rootElement.AppendChild(formPaths);
                    }

                    zipFolderPathElement = xmlDoc.CreateElement("ZipFolderPath");
                    formPaths.AppendChild(zipFolderPathElement);
                }
                zipFolderPathElement.InnerText = Path.GetDirectoryName(zipPath);
                XmlElement zipFilePathElement = rootElement.SelectSingleNode("FormPaths/ZipFilePath") as XmlElement;
                if (zipFilePathElement == null) {
                    XmlElement formPaths = rootElement.SelectSingleNode("FormPaths") as XmlElement;
                    if (formPaths == null) {
                        formPaths = xmlDoc.CreateElement("FormPaths");
                        rootElement.AppendChild(formPaths);
                    }

                    zipFilePathElement = xmlDoc.CreateElement("ZipFilePath");
                    formPaths.AppendChild(zipFilePathElement);
                }
                zipFilePathElement.InnerText = zipPath;
                XmlElement cfgFileCheckStateElement = rootElement.SelectSingleNode("FormPaths/CfgFileCheckState") as XmlElement;
                if (cfgFileCheckStateElement == null) {
                    XmlElement formPaths = rootElement.SelectSingleNode("FormPaths") as XmlElement;
                    if (formPaths == null) {
                        formPaths = xmlDoc.CreateElement("FormPaths");
                        rootElement.AppendChild(formPaths);
                    }

                    cfgFileCheckStateElement = xmlDoc.CreateElement("CfgFileCheckState");
                    formPaths.AppendChild(cfgFileCheckStateElement);
                }
                cfgFileCheckStateElement.InnerText = cfgFileCheckState.ToString();
            }

            if (!string.IsNullOrEmpty(logFilePath)) {
                XmlElement logFilePathElement = rootElement.SelectSingleNode("FormPaths/LogFilePath") as XmlElement;
                if (logFilePathElement == null) {
                    XmlElement formPaths = rootElement.SelectSingleNode("FormPaths") as XmlElement;
                    if (formPaths == null) {
                        formPaths = xmlDoc.CreateElement("FormPaths");
                        rootElement.AppendChild(formPaths);
                    }

                    logFilePathElement = xmlDoc.CreateElement("LogFilePath");
                    formPaths.AppendChild(logFilePathElement);
                }
                logFilePathElement.InnerText = logFilePath;
                
            }

            if (!string.IsNullOrEmpty(rssiCriteria)) {
                XmlElement rssiRriteriaElement = rootElement.SelectSingleNode("FormPaths/RssiCriteria") as XmlElement;
                if (rssiRriteriaElement == null) {
                    XmlElement formPaths = rootElement.SelectSingleNode("FormPaths") as XmlElement;
                    if (formPaths == null) {
                        formPaths = xmlDoc.CreateElement("FormPaths");
                        rootElement.AppendChild(formPaths);
                    }

                    rssiRriteriaElement = xmlDoc.CreateElement("RssiCriteria");
                    formPaths.AppendChild(rssiRriteriaElement);
                }
                rssiRriteriaElement.InnerText = rssiCriteria;
                XmlElement logFileCheckStateElement = rootElement.SelectSingleNode("FormPaths/LogFileCheckState") as XmlElement;
                if (logFileCheckStateElement == null) {
                    XmlElement formPaths = rootElement.SelectSingleNode("FormPaths") as XmlElement;
                    if (formPaths == null) {
                        formPaths = xmlDoc.CreateElement("FormPaths");
                        rootElement.AppendChild(formPaths);
                    }

                    logFileCheckStateElement = xmlDoc.CreateElement("LogFileCheckState");
                    formPaths.AppendChild(logFileCheckStateElement);
                }
                logFileCheckStateElement.InnerText = logFileCheckState.ToString();
            }
            
            XmlElement registerMapViewStateElement = rootElement.SelectSingleNode("FormPaths/RegisterMapViewState") as XmlElement;
            if (registerMapViewStateElement == null) {
                XmlElement formPaths = rootElement.SelectSingleNode("FormPaths") as XmlElement;
                if (formPaths == null) {
                    formPaths = xmlDoc.CreateElement("FormPaths");
                    rootElement.AppendChild(formPaths);
                }

                registerMapViewStateElement = xmlDoc.CreateElement("RegisterMapViewState");
                formPaths.AppendChild(registerMapViewStateElement);
            }
            registerMapViewStateElement.InnerText = registerMapViewState.ToString();

            xmlDoc.Save(xmlFilePath);
        }

        private MainFormPaths _LoadLastPathsAndSetup()
        {
            string folderPath = Application.StartupPath;
            string combinedPath = Path.Combine(folderPath, "XmlFolder");
            string xmlFilePath = Path.Combine(combinedPath, "MainFormPaths.xml");
            xmlFilePath = xmlFilePath.Replace("\\\\", "\\");

            try {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);
                XmlNode zipFolderPathElement = xmlDoc.SelectSingleNode("//ZipFolderPath");
                XmlNode zipFilePathElement = xmlDoc.SelectSingleNode("//ZipFilePath");
                XmlNode logFilePathElement = xmlDoc.SelectSingleNode("//LogFilePath");
                XmlNode rssiRriteriaElement = xmlDoc.SelectSingleNode("//RssiCriteria");
                XmlNode cfgFileCheckStateElement = xmlDoc.SelectSingleNode("//CfgFileCheckState");
                XmlNode logFileCheckStateElement = xmlDoc.SelectSingleNode("//LogFileCheckState");
                XmlNode registerMapViewStateElement = xmlDoc.SelectSingleNode("//RegisterMapViewState");

                string zipFolderPath = zipFolderPathElement?.InnerText;
                string zipFilePath = zipFilePathElement?.InnerText;
                string logFilePath = logFilePathElement?.InnerText;
                string rssiCriteria = rssiRriteriaElement?.InnerText;
                bool cfgFileCheckState = cfgFileCheckStateElement != null && Convert.ToBoolean(cfgFileCheckStateElement.InnerText);
                bool logFileCheckState = logFileCheckStateElement != null && Convert.ToBoolean(logFileCheckStateElement.InnerText);
                bool registerMapViewState = registerMapViewStateElement != null && Convert.ToBoolean(registerMapViewStateElement.InnerText);

                return new MainFormPaths {
                    ZipFolderPath = zipFolderPath,
                    ZipFilePath = zipFilePath,
                    LogFilePath = logFilePath,
                    RssiCriteria = rssiCriteria,
                    CfgFileCheckState = cfgFileCheckState,
                    LogFileCheckState = logFileCheckState,
                    RegisterMapViewState = registerMapViewState
                };
            }
            catch (Exception) {
                return new MainFormPaths {
                    ZipFolderPath = null,
                    ZipFilePath= null,
                    LogFilePath = null,
                    RssiCriteria = null,
                    CfgFileCheckState = false,
                    LogFileCheckState = false,
                    RegisterMapViewState = false
                };
            }
        }

        private void _LoadXmlFile()
        {
            MainFormPaths lastPath = _LoadLastPathsAndSetup();
            string initialDirectory = lastPath.ZipFolderPath;
            string initialZipFile = lastPath.ZipFilePath;
            string logFilePath = lastPath.LogFilePath;
            string rssiCriteria = lastPath.RssiCriteria;
            //cbCfgCheckAfterFw.Checked = lastPath.CfgFileCheckState;
            //cbLogCheckAfterSn.Checked = lastPath.LogFileCheckState;
            cbRegisterMapView.Checked = lastPath.RegisterMapViewState;

            if (string.IsNullOrEmpty(logFilePath))
                _EnterDefaultLogfilePath();
            else {
                tbLogFilePath.Text = logFilePath;
                tbLogFilePath.SelectionStart = tbLogFilePath.Text.Length;
            }

            if (!string.IsNullOrEmpty(rssiCriteria))
                tbRssiCriteria.Text = rssiCriteria;
            else
                tbRssiCriteria.Text = "200";

            if (string.IsNullOrEmpty(initialDirectory))
                initialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.Title = "Files position";
                openFileDialog.Filter = "Zip Files (*.zip)|*.zip";
                openFileDialog.InitialDirectory = initialDirectory;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    string selectedFilePath = openFileDialog.FileName;
                    string extension = Path.GetExtension(selectedFilePath).ToLower();

                    if (extension == ".zip") {
                        try {
                            TempFolderPath = ExtractZipToTemporaryFolder(selectedFilePath);
                            SetHiddenAttribute(TempFolderPath);
                            string xmlFilePath = Path.Combine(TempFolderPath, "Cfg.xml");

                            if (File.Exists(xmlFilePath)) {
                                _ParserXmlForProjectInformation(xmlFilePath);
                                tbFilePath.Text = selectedFilePath;
                            }
                            else {
                                MessageBox.Show("Cfg.xml not found in the zip file.");
                            }
                        }
                        catch (Exception ex) {
                            MessageBox.Show("Failed to extract zip file: " + ex.Message);
                        }
                    }
                    
                    tbFilePath.SelectionStart = tbFilePath.Text.Length;
                }
            }
        }

        private void _DirectDriveToLoadXmlFile()
        {
            MainFormPaths lastPath = _LoadLastPathsAndSetup();
            tbLogFilePath.Text = lastPath.LogFilePath;
            tbLogFilePath.SelectionStart = tbLogFilePath.Text.Length;
            string inspectionFolder = Path.Combine(Application.StartupPath, "InspectionFiles");
            string inspectionFile = GetUniqueZipFile(inspectionFolder);
            if (inspectionFile != null) 
                //MessageBox.Show($"ZIP file found: {inspectionFile}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (!string.IsNullOrEmpty(lastPath.RssiCriteria))
                tbRssiCriteria.Text = lastPath.RssiCriteria;
            else
                tbRssiCriteria.Text = "200";
            
            try {
                if (inspectionFile != null) {
                    TempFolderPath = ExtractZipToTemporaryFolder(inspectionFile);
                    SetHiddenAttribute(TempFolderPath);
                    string xmlFilePath = Path.Combine(TempFolderPath, "Cfg.xml");

                    if (File.Exists(xmlFilePath)) {
                        _ParserXmlForProjectInformation(xmlFilePath);
                        tbFilePath.Text = inspectionFile;
                    }
                    else {
                        MessageBox.Show("Cfg.xml not found in the zip file.");
                    }
                }
            }
            catch (UnauthorizedAccessException uaEx) {
                MessageBox.Show("Access denied to the specified path: " + uaEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) {
                MessageBox.Show("Failed to extract zip file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            tbFilePath.SelectionStart = tbFilePath.Text.Length;
            _ResetSequence();
        }

        private string GetUniqueZipFile(string folderPath)
        {
            if (!Directory.Exists(folderPath)) {
                MessageBox.Show("The inspectionFiles folder does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            string[] zipFiles = Directory.GetFiles(folderPath, "*.zip");

            if (zipFiles.Length == 0) {
                MessageBox.Show("No ZIP files found in the specified folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            else if (zipFiles.Length > 1) {
                MessageBox.Show("Multiple ZIP files found. Please ensure only one ZIP file exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return zipFiles[0];
        }

        private string ExtractZipToTemporaryFolder(string zipFilePath)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(zipFilePath));

            using (ZipFile zip = ZipFile.Read(zipFilePath)) {
                zip.Password = "2368";
                zip.ExtractAll(tempPath, ExtractExistingFileAction.OverwriteSilently);
            }

            return tempPath;
        }

        private void SetHiddenAttribute(string folderPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
            dirInfo.Attributes |= FileAttributes.Hidden;
        }

        private void _SetLogFilePath()
        {
            string initialDirectory = Application.StartupPath;
            string logFileFolderPath;

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()) {
                folderBrowserDialog.Description = "Select a Folder";
                folderBrowserDialog.SelectedPath = initialDirectory;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                    tbLogFilePath.Text = folderBrowserDialog.SelectedPath;
                    tbLogFilePath.SelectionStart = tbLogFilePath.Text.Length;
                    logFileFolderPath = folderBrowserDialog.SelectedPath;
                    _StoreConfigurationPaths(null, logFileFolderPath, null);
                }
            }
        }

        private void _ParserXmlForProjectInformation_original(string filePath)
        {
            lApName.Text = "_";
            lDataName.Text = "_";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNode permissionsNode = xmlDoc.SelectSingleNode("//Premissions");
            string role = permissionsNode.Attributes["role"].Value;
            XmlNode productNode = xmlDoc.SelectSingleNode("//Product");
            string productName = productNode.Attributes["name"].Value;
            XmlNode APROMNode = xmlDoc.SelectSingleNode("//APROM");
            string APROMName = APROMNode.Attributes["name"].Value;
            XmlNode DATAROMNode = xmlDoc.SelectSingleNode("//DATAROM");
            string DARAROMName = DATAROMNode.Attributes["name"].Value;

            lMode.Text = role;
            lProduct.Text = productName;

            if (!string.IsNullOrEmpty(APROMName))
                lApName.Text = APROMName;
            else
                MessageBox.Show("The configuration file format is incorrect.\nAPROM path not specified.", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            if (!string.IsNullOrEmpty(DARAROMName))
                lDataName.Text = DARAROMName;
        }

        private void _ParserXmlForProjectInformation_method2(XmlDocument xmlDoc)
        {
            lApName.Text = "_";
            lDataName.Text = "_";

            XmlNode permissionsNode = xmlDoc.SelectSingleNode("//Premissions");
            string role = permissionsNode.Attributes["role"].Value;
            XmlNode productNode = xmlDoc.SelectSingleNode("//Product");
            string productName = productNode.Attributes["name"].Value;
            XmlNode APROMNode = xmlDoc.SelectSingleNode("//APROM");
            string APROMName = APROMNode.Attributes["name"].Value;
            XmlNode DATAROMNode = xmlDoc.SelectSingleNode("//DATAROM");
            string DARAROMName = DATAROMNode.Attributes["name"].Value;

            lMode.Text = role;
            lProduct.Text = productName;

            if (!string.IsNullOrWhiteSpace(APROMName))
                lApName.Text = APROMName;
            else
                MessageBox.Show("The configuration file format is incorrect.\nAPROM path not specified.", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (!string.IsNullOrWhiteSpace(DARAROMName))
                lDataName.Text = DARAROMName;
        }

        private void _ParserXmlForProjectInformation(string filePath )
        {
            lApName.Text = "_";
            lDataName.Text = "_";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNode permissionsNode = xmlDoc.SelectSingleNode("//Premissions");
            string role = permissionsNode.Attributes["role"].Value;
            XmlNode productNode = xmlDoc.SelectSingleNode("//Product");
            string productName = productNode.Attributes["name"].Value;

            lMode.Text = role;
            lProduct.Text = productName;
            Sas3Module = (lProduct.Text == "SAS3.0");

            if (Sas3Module)
                engineerForm._SetSas3Password();

            if (Sas3Module) {
                XmlNode ASideNode = xmlDoc.SelectSingleNode("//ASIDE");
                string ASideFileName = ASideNode.Attributes["name"].Value;
                XmlNode BSideNode = xmlDoc.SelectSingleNode("//BSIDE");
                string BSideFileName = BSideNode.Attributes["name"].Value;
                lPathAP.Text = "A side:";
                lPathData.Text = "B side:";

                if (!string.IsNullOrWhiteSpace(ASideFileName)) {

                    lApName.Location = new Point(66, 212);
                    lApName.Text = ASideFileName;
                }
                else {
                    MessageBox.Show("The configuration file format is incorrect.\nAPROM path not specified.", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (!string.IsNullOrWhiteSpace(BSideFileName)) {
                    lDataName.Location = new Point(66, 227);
                    lDataName.Text = BSideFileName;
                }
            }
            else {
                XmlNode APROMNode = xmlDoc.SelectSingleNode("//APROM");
                string APROMName = APROMNode.Attributes["name"].Value;
                XmlNode DATAROMNode = xmlDoc.SelectSingleNode("//DATAROM");
                string DARAROMName = DATAROMNode.Attributes["name"].Value;

                if (!string.IsNullOrWhiteSpace(APROMName)) {
                    lApName.Location = new Point(76, 212);
                    lApName.Text = APROMName;
                }
                else {
                    MessageBox.Show("The configuration file format is incorrect.\nAPROM path not specified.", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (!string.IsNullOrWhiteSpace(DARAROMName)) {
                    lDataName.Location = new Point(91, 227);
                    lDataName.Text = DARAROMName;
                }
            }

            
        }

        private bool _PathCheck(Label lable)
        {
            string directoryPath = TempFolderPath;
            string fileName = lable.Text;
            string filePath = Path.Combine(directoryPath, fileName);

            if (File.Exists(filePath)) {
                if (Path.GetExtension(filePath).Equals(".bin", StringComparison.OrdinalIgnoreCase))
                    return true;
                else {
                    MessageBox.Show("Please verify if there is an error in the format of the Config file.");
                    return false;
                }
            }

            return false;
        }

        private void raSingle_CheckedChanged(object sender, EventArgs e)
        {
            _SwitchOrNot();
        }

        private void rbBoth_CheckedChanged(object sender, EventArgs e)
        {
            _SwitchOrNot();
        }

        private void tbFilePath_MouseClick(object sender, MouseEventArgs e)
        {
            _ConfigFilePathChanges();
        }

        private void tbFilePath_Enter(object sender, EventArgs e)
        {
            _ConfigFilePathChanges();
        }

        private void _ConfigFilePathChanges()
        {
            bool customerMode = lMode.Text == "Customer";
            int channelNumber = 1;

            if (tbFilePath.Text == "Please click here, to import the Config file...") {
                tbFilePath.Text = "";
                tbFilePath.ForeColor = Color.MidnightBlue;
            }

            _CleanTempFolder(); // Before change zip file, Clean the tempFolder
            _LoadXmlFile();

            if (lMode.Text == "Customer") {
                _AdjustGuiSizeAndCenter(550, 280);
                //rbSingle.Enabled = true;
                //rbBoth.Enabled = true;
                cbSecurityLock.Visible = false;
                gbOperatorMode.Visible = false;
                bWriteSnDateCone.Visible = false;
                tbOrignalSNCh1.Visible = false;
                tbOrignalSNCh2.Visible = false;
                tbReNewSNCh1.Visible = false;
                tbReNewSNCh2.Visible = false;
                lOriginalSN.Visible = false;
                lReNewSn.Visible = false;
                bCfgFileComparison.Visible = false;
                bLogFileComparison.Visible = false;
                bRenewRssi.Visible = false;
                cbCfgCheckAfterFw.Visible = false;
            }

            else if (lMode.Text == "MP") {
                _AdjustGuiSizeAndCenter(550, 550);
                rbSingle.Select();
                //rbSingle.Enabled = false;
                //rbBoth.Enabled = false;
                cbSecurityLock.Visible = true;
                gbOperatorMode.Visible = true;
                bWriteSnDateCone.Visible = true;
                tbOrignalSNCh1.Visible = true;
                tbOrignalSNCh2.Visible = true;
                tbReNewSNCh1.Visible = true;
                tbReNewSNCh2.Visible = true;
                lOriginalSN.Visible = true;
                lReNewSn.Visible = true;
                bCfgFileComparison.Visible = true;
                bLogFileComparison.Visible = true;
                bRenewRssi.Visible = true;
                cbCfgCheckAfterFw.Visible = true;

                if (rbBoth.Checked) {
                    tbOrignalSNCh2.Visible = true;
                    tbReNewSNCh2.Visible = true;
                }
                else {
                    tbOrignalSNCh2.Visible = false;
                    tbReNewSNCh2.Visible = false;
                }
            }

            I2cConnected = !(engineerForm.ChannelSetApi(channelNumber) < 0);
        }

        private void _AdjustGuiSizeAndCenter(int width, int height)
        {
            this.Size = new Size(width, height);
            Rectangle screenBounds = Screen.GetWorkingArea(this);
            //int centerX = screenBounds.Left + (screenBounds.Width - this.Width) / 2;
            int centerY = screenBounds.Top + (screenBounds.Height - this.Height) / 2;
            //this.Location = new Point(centerX, centerY);
            this.Location = new Point(this.Location.X, centerY);
        }

        private int _RxPowerUpdateWithoutThread()
        {
            int rssiCriteria;
            int[] rxPowers = new int[4];
            bool isParsed = int.TryParse(tbRssiCriteria.Text, out rssiCriteria);
            if (!isParsed) {
                MessageBox.Show("Please enter a valid integer value for the RSSI criteria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            
            engineerForm.RxPowerReadApiFromDdmApi();
            Thread.Sleep(1000);
            if (engineerForm.RxPowerReadApiFromDdmApi() < 0) {
                MessageBox.Show("Please check the module plugin status");
                return -1;
            }
            Thread.Sleep(100);

            /*
            while (rxPowers[3] == 0) {
                engineerForm.RxPowerReadApiFromDdmApi();
                Thread.Sleep(100);
                rxPowers[0] = _decimalRemove(engineerForm.GetTextBoxTextFromDdmApi("tbRxPower1"));
            }

            */
            rxPowers[0] = _decimalRemove(engineerForm.GetTextBoxTextFromDdmApi("tbRxPower1"));
            rxPowers[1] = _decimalRemove(engineerForm.GetTextBoxTextFromDdmApi("tbRxPower2"));
            rxPowers[2] = _decimalRemove(engineerForm.GetTextBoxTextFromDdmApi("tbRxPower3"));
            rxPowers[3] = _decimalRemove(engineerForm.GetTextBoxTextFromDdmApi("tbRxPower4"));

            if (UpdateRssiDisplay(ProcessingChannel, rxPowers, rssiCriteria) > 0) {
                MessageBox.Show("RSSI value exceeds the criteria ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return -2;
            }

            _StoreConfigurationPaths(null, null, tbRssiCriteria.Text);
            return 0;
        }

        private void _InitialRssiTextBox()
        {
            System.Windows.Forms.TextBox[] textBoxes = new[]
            {
                tbRxPowerCh1_0, tbRxPowerCh1_1, tbRxPowerCh1_2, tbRxPowerCh1_3,
                tbRxPowerCh2_0, tbRxPowerCh2_1, tbRxPowerCh2_2, tbRxPowerCh2_3
            };

            foreach (var textBox in textBoxes) {
                textBox.Text = "";
                textBox.BackColor = SystemColors.Window; // Reset back color
            }

            Application.DoEvents();
        }

        private int UpdateRssiDisplay(int channel, int[] rxPowers, int rssiCriteria)
        {
            int errorCount = 0;

            System.Windows.Forms.TextBox[] textBoxes = (channel == 1) ?
                new[] { tbRxPowerCh1_0, tbRxPowerCh1_1, tbRxPowerCh1_2, tbRxPowerCh1_3 } :
                new[] { tbRxPowerCh2_0, tbRxPowerCh2_1, tbRxPowerCh2_2, tbRxPowerCh2_3 };

            for (int i = 0; i < rxPowers.Length; i++) {
                if (rxPowers[i] < rssiCriteria) {
                    textBoxes[i].BackColor = Color.HotPink;
                    if (cbRxPowerNgInterrupt.Checked) errorCount++;
                }

                textBoxes[i].Text = rxPowers[i].ToString();
            }

            Application.DoEvents();

            return errorCount;
        }

        private int _decimalRemove(string text)
        {
            if (text == "4.4" || text == "4")
                return 0;

            int decimalIndex = text.IndexOf('.');
            if (decimalIndex != -1) {
                text = text.Substring(0, decimalIndex);
            }

            return int.Parse(text);
        }

        private void tbFilePath_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFilePath.Text)) {
                tbFilePath.Text = "Please click here, to import the Config file...";
                tbFilePath.ForeColor = Color.Silver;
            }
        }
       
        private void _FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) {
                I2cConnected = (engineerForm.I2cMasterDisconnectApi() < 0);
                Application.Exit();
            }
            _CleanTempFolder();
        }

        private void _CleanTempFolder()
        {
            if (Directory.Exists(TempFolderPath)) {
                try {
                    Directory.Delete(TempFolderPath, true);
                }
                catch (Exception ex) {
                    MessageBox.Show("Failed to delete temporary folder: " + ex.Message);
                }
            }
        }
        private void _DisableButtonsForBarcodeMode()
        {
            bStart.Enabled = false;
            bWriteSnDateCone.Enabled = false;
            bCfgFileComparison.Enabled = false;
            bLogFileComparison.Enabled = false;
            bRenewRssi.Enabled = false;
            gbCodeEditor.Enabled = false;
            tbLogFilePath.Enabled = false;
            tbVenderSn.Enabled = true;
            tbRssiCriteria.Enabled = false;
            cbRxPowerNgInterrupt.Enabled = false;
            bCheckSerialNumber.Enabled = false;
            bCurrentRegister.Enabled = false;
            bOpenLogFileFolder.Enabled = false;
        }
        private void _EnableButtonsForBarcodeMode()
        {
            bStart.Enabled = true;
            bWriteSnDateCone.Enabled = true;
            bCfgFileComparison.Enabled = true;
            bLogFileComparison.Enabled = true;
            bRenewRssi.Enabled = true;
            gbCodeEditor.Enabled = true;
            tbVenderSn.Enabled = false;
            tbLogFilePath.Enabled = true;
            tbFilePath.Enabled = true;
            rbSingle.Enabled = true;
            rbBoth.Enabled = true;
            cbSecurityLock.Enabled = true;
            cbI2cConnect.Enabled = true;
            cbRegisterMapView.Enabled = true;
            tbRssiCriteria.Enabled = true;
            cbRxPowerNgInterrupt.Enabled = true;
            bCheckSerialNumber.Enabled = true;
            bCurrentRegister.Enabled = true;
            bOpenLogFileFolder.Enabled = true;
        }

        private void _DisableButtons()
        {
            loadingForm.Show(this);
            tbLogFilePath.Enabled = false;
            tbFilePath.Enabled = false;
            bStart.Enabled = false;
            rbSingle.Enabled = false;
            rbBoth.Enabled = false;
            cbSecurityLock.Enabled = false;
            cbI2cConnect.Enabled = false;
            //cbBypassW.Enabled = false;
            cbRegisterMapView.Enabled = false;
            //gbOperatorMode.Enabled = false;
            bWriteFromFile.Enabled = false;
            bWriteSnDateCone.Enabled = false;
            bCfgFileComparison.Enabled = false;
            bLogFileComparison.Enabled = false;
            bRenewRssi.Enabled = false;
            bCheckSerialNumber.Enabled = false;
            gbOptionsControl.Enabled = false;
            cbLogCheckAfterSn.Enabled = false;
            cbCfgCheckAfterFw.Enabled = false;
            bCurrentRegister.Enabled = false;
            bOpenLogFileFolder.Enabled = false;
        }
        
        private void _EnableButtons()
        {
            tbFilePath.Enabled = true;
            bStart.Enabled = true;
            rbSingle.Enabled = true;
            rbBoth.Enabled = true;
            cbSecurityLock.Enabled = true;
            cbI2cConnect.Enabled = true;
            //cbBypassW.Enabled = true;
            cbRegisterMapView.Enabled = true;
            //gbOperatorMode.Enabled = true;
            bWriteFromFile.Enabled = true;
            bWriteSnDateCone.Enabled = true;
            bCfgFileComparison.Enabled = true;
            bLogFileComparison.Enabled = true;
            bRenewRssi.Enabled = true;
            bCheckSerialNumber.Enabled = true;
            gbOptionsControl.Enabled = true;
            //cbLogCheckAfterSn.Enabled = true;
            //cbCfgCheckAfterFw.Enabled = true;
            bCurrentRegister.Enabled = true;
            bOpenLogFileFolder.Enabled = true;
            tbLogFilePath.Enabled = true;
            loadingForm.Close();
            this.BringToFront();
            this.Activate();
        }

        private int _ExportCurrentModuleRegisterToFile()
        {
            string backupFileName = "ReWriteRegister";
            string modelType = lProduct.Text;
            lCh1Message.Text = "";
            Application.DoEvents();

            tbOrignalTLSN.Text = engineerForm.GetSerialNumberApi();
            engineerForm.ExportLogfileApi(modelType, backupFileName, true, false); //目標模組Cfg Backup
            engineerForm.SetToChannle2Api(false);
            tbVersionCodeCh1.Text = engineerForm.GetFirmwareVersionCodeApi();
            lCh1Message.Text = "Exported Register.";
                   
            Application.DoEvents();
            Thread.Sleep(100);

            return 0;
        }

        private int _RemoteControl(bool customerMode)
        {
            string modelType = lProduct.Text;
            string directoryPath = TempFolderPath;
            string registerFileName = "RegisterFile.csv";
            string registerFilePath = Path.Combine(directoryPath, registerFileName); //Generate the CfgFilePath with config folder
            string backupFileName = "ModuleRegisterFile";
            int relinkCount = 0, startTime = 0, intervalTime = 0;

            //Check module status
            engineerForm.InformationReadApi();
            if (string.IsNullOrEmpty(engineerForm.GetVendorSnFromDdmiApi())) {
                MessageBox.Show("Please confirm the plug-in status of the module.", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            tbOrignalTLSN.Text = engineerForm.GetSerialNumberApi();

            if (DebugMode) {
                MessageBox.Show("directoryPath: \n" + directoryPath +
                                "\nRegisterFilePath: \n" + registerFilePath);
            }

            if (!((_PathCheck(lApName)) || (_PathCheck(lDataName)))) {
                MessageBox.Show("No file path specified. Please choose the file again.", "Config file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            else {
                engineerForm.ExportLogfileApi(modelType, backupFileName, true, false); //目標模組Cfg Backup

                if (ProcessingChannel == 1) {
                    engineerForm.SetToChannle2Api(false);
                    tbVersionCodeCh1.Text = engineerForm.GetFirmwareVersionCodeApi();
                }
                else if (ProcessingChannel == 2) {
                    engineerForm.SetToChannle2Api(true);
                    tbVersionCodeCh2.Text = engineerForm.GetFirmwareVersionCodeApi();
                }

                if (DebugMode) {
                    MessageBox.Show("GlobalRead...Done");
                    return -1;
                }

                //Set ICPTool Funciton
                engineerForm.SetAutoReconnectApi(true); // An automatic connection to MCU will be initiated.
                engineerForm.SetBypassEraseAllCheckModeApi(true); // Avoid the intervention of MessgaeBox

                if (DebugMode) {
                    MessageBox.Show("AutoReconnec mode: " + engineerForm.GetAutoReconnectApi()
                                + "\nBypassEraseAll mode " + engineerForm.GetBypassEraseAllCheckModeApi()
                                );
                }

                if (!string.IsNullOrEmpty(tbRelinkCount.Text) && int.TryParse(tbRelinkCount.Text, out int parsedRelinkCount))
                    relinkCount = parsedRelinkCount;

                if (!string.IsNullOrEmpty(tbStartTime.Text) && int.TryParse(tbStartTime.Text, out int parsedStartTime))
                    startTime = parsedStartTime;

                if (!string.IsNullOrEmpty(tbIntervalTime.Text) && int.TryParse(tbIntervalTime.Text, out int parsedIntervalTime))
                    intervalTime = parsedIntervalTime;

                engineerForm.ForceConnectApi(false, relinkCount, startTime, intervalTime); // Link DUT and EraseAPROM
                //mainForm.ForceConnectApi(false,0,0,0); // Link DUT and EraseAPROM
                Thread.Sleep(10);
                engineerForm.StartFlashingApi(); // Firmware update
                Thread.Sleep(10);
                engineerForm._GlobalWriteFromRegisterFile(customerMode, registerFilePath, ProcessingChannel);
                Thread.Sleep(10);
                tbReNewTLSN.Text = engineerForm.GetSerialNumberApi();
                _SnComparison();

                /*
                if (!customerMode)
                    mainForm.ComparisonRegisterApi(RegisterFilePath, false , cbEngineerMode.Checked);
                */

                Thread.Sleep(10);

                if (ProcessingChannel == 1) {
                    lCh1Message.Text = "Update completed.";
                    cProgressBar1.Value = 100;
                    cProgressBar1.Text = "100%";
                    tbVersionCodeReNewCh1.Text = engineerForm.GetFirmwareVersionCodeApi();
                }
                else if (ProcessingChannel == 2) {
                    lCh2Message.Text = "Update completed.";
                    cProgressBar2.Value = 100;
                    cProgressBar2.Text = "100%";
                    tbVersionCodeReNewCh2.Text = engineerForm.GetFirmwareVersionCodeApi();
                }

                Application.DoEvents();
                Thread.Sleep(100);
            }

            return 0;
        }

        private void _SnComparison()
        {
            string a, b;

            a = tbOrignalTLSN.Text;
            b = tbReNewTLSN.Text;

            if ( a != b)
                tbReNewTLSN.BackColor = Color.HotPink;
            else
                tbReNewTLSN.BackColor = Color.White;
        }

        private int _RemoteControlForSas3(bool customerMode)
        {
            string dutCheck;
            string directoryPath = TempFolderPath;
            string registerFileName = "RegisterFile.csv";
            string registerFilePath = Path.Combine(directoryPath, registerFileName); //Generate the CfgFilePath with config folder
            int reWriteCount = 0;
            const int maxRetryCount = 2;

            do {
                engineerForm._WriteFromRegisterFileForSas3(customerMode, registerFilePath, ProcessingChannel);
                Thread.Sleep(10);

                UpdateUIAfterWrite(ProcessingChannel);

                Application.DoEvents();
                Thread.Sleep(100);

                engineerForm.InformationReadApi();
                dutCheck = engineerForm.GetVenderPnApi();

            } while (string.IsNullOrEmpty(dutCheck) && (++reWriteCount <= maxRetryCount));

            return 0;
        }

        private void UpdateUIAfterWrite(int channel)
        {
            if (channel == 1) {
                lCh1Message.Text = "Update completed.";
                cProgressBar1.Value = 100;
                cProgressBar1.Text = "100%";
                tbVersionCodeReNewCh1.Text = engineerForm.GetFirmwareVersionCodeApi();
            }
            else if (channel == 2) {
                lCh2Message.Text = "Update completed.";
                cProgressBar2.Value = 100;
                cProgressBar2.Text = "100%";
                tbVersionCodeReNewCh2.Text = engineerForm.GetFirmwareVersionCodeApi();
            }
        }

        private int _WriteSnDatecode(int ch)
        {
            string modelType = lProduct.Text;
            string venderSn = tbVenderSn.Text;
            string dataCode = tbDateCode.Text;
            string originalVenderSn, originalDateCode;
            string logFileName;
            int channelNumber;

            logFileName = venderSn + (ch == 1 ? "A" : "B");

            channelNumber = (ch == 1
                ? (lMode.Text == "Customer" || string.IsNullOrEmpty(lMode.Text)) ? 1 : 13
                : (lMode.Text == "Customer" || string.IsNullOrEmpty(lMode.Text)) ? 2 : 23);

            I2cConnected = !(engineerForm.ChannelSetApi(channelNumber) < 0);
            Thread.Sleep(200);
            _GetModuleVenderSn(true, ch);
            originalDateCode = engineerForm.GetDateCodeFromDdmiApi();
            originalVenderSn = ch == 1
                ? tbOrignalSNCh1.Text
                : tbOrignalSNCh2.Text;

           

            if (DebugMode)
                ShowDebugInfo(originalVenderSn, originalDateCode);

            if (Sas3Module) {
                _UpdateMessage(ch, "Writing SN, DateCode");

                if (engineerForm.WriteVendorSerialNumberApi(venderSn, dataCode) < 0)
                    return -1;
            }
            else {
                engineerForm.SetVendorSnToDdmiApi(venderSn);
                engineerForm.SetDataCodeToDdmiApi(dataCode);
                _UpdateMessage(ch, "Writing information");

                if (engineerForm.InformationWriteApi() < 0)
                    return -1;

                Thread.Sleep(100);
                _UpdateMessage(ch, "Store into flash");

                if (engineerForm.InformationWriteApi() < 0)
                    return -1;

                Thread.Sleep(100);
            }
            
            _UpdateMessage(ch, "StoreFlash..Done");
            _GetModuleVenderSn(false, ch);
            
            if (_RxPowerUpdateWithoutThread() < 0) return -1;

            if (Sas3Module) {
                if (engineerForm.ExportLogfileForSas3Api(logFileName, true, true, ProcessingChannel) < 0)
                    return -1; //Must be implement
            }
            else {
                if (engineerForm.ExportLogfileApi(modelType, logFileName, true, true) < 0)
                    return -1; //Must be implement
            }
            
            Thread.Sleep(10);
            _UpdateMessage(ch, "LogFile..exported");

            return 0;
        }

        private void ShowDebugInfo(string originalVenderSn, string originalDateCode)
        {
            MessageBox.Show("Information check"
                        + "\nBefore:\n"
                        + "VenderSn: " + originalVenderSn
                        + "\nDateCode: " + originalDateCode
                        + "\n\nAfter:\n"
                        + "VerderSn: " + engineerForm.GetVendorSnFromDdmiApi()
                        + "\nDateCode:" + engineerForm.GetDateCodeFromDdmiApi()
                        );
        }

        private void _UpdateMessage(int channel, string message)
        {
            string msgLabel = channel == 1 ? "lCh1Message" : "lCh2Message";
            switch (msgLabel) {
                case "lCh1Message":
                    lCh1Message.Text = message;
                    break;
                case "lCh2Message":
                    lCh2Message.Text = message;
                    break;
            }
            Application.DoEvents();
        }
        
        private int _Processor(bool customerMode) // True: Customer Mode, Flase: MP mode
        {
            int tmp;
            int channelNumber;
            _StoreConfigurationPaths(tbFilePath.Text, null, null);
            I2cConnected = (engineerForm.I2cMasterDisconnectApi() < 0);
            //I2cConnected = !(mainForm.ChannelSetApi(channelNumber) < 0);

            for (ProcessingChannel = 1; ProcessingChannel <= (DoubleSideMode ? 2 : 1); ProcessingChannel++) {
                switch (ProcessingChannel) {
                    case 1:
                        channelNumber = 1;
                        break;
                    case 2:
                        channelNumber = 2;
                        break;
                    default:
                        channelNumber = 0;
                        return -1;
                }

                I2cConnected = !(engineerForm.ChannelSetApi(channelNumber) < 0);
                Thread.Sleep(200);

                if (_RemoteInitial(customerMode) < 0)
                    return _CloseLoadingFormAndReturn(-1);

                if (Sas3Module) {
                    if (_RemoteControlForSas3(customerMode) < 0)
                        return _CloseLoadingFormAndReturn(-1);
                }
                else {
                    tmp = _RemoteControl(customerMode);
                    if (tmp == -2)
                        return -2;
                    else if (tmp < 0)
                        return _CloseLoadingFormAndReturn(-1);
                }
                
                FirstRound = false;
            }
            
            if (DoubleSideMode) {
                //mainForm.ChannelSwitchApi(customerMode, channelNumber); // return to ch1
                _ReadRssiForBothSide();
            }

            if (cbCfgCheckAfterFw.Checked)
                _CfgFileComparison();
            
            return 0;
        }

        private int ValidateVenderSn()
        {
            string venderSn = tbVenderSn.Text;

            if (venderSn.Length != 12) {
                MessageBox.Show("The serial number must be 12 characters long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            int yy = int.Parse(venderSn.Substring(0, 2));
            if (yy < 23 || yy > 30) {
                MessageBox.Show("The first two digits(YY) must be between 23 and 30.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            int mm = int.Parse(venderSn.Substring(2, 2));
            if (mm < 1 || mm > 12) {
                MessageBox.Show("The 3~4 digits(MM) must be between 01 and 12.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            int dd = int.Parse(venderSn.Substring(4, 2));
            if (dd < 1 || dd > 31) {
                MessageBox.Show("The 5~6 digits(DD) must be between 01 and 31.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            int ssss = int.Parse(venderSn.Substring(8, 4));
            if (ssss < 1 || ssss > 9999) {
                MessageBox.Show("The 9~12 digits(SSSS) must be between 0001 and 9999.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            return 0;
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            int tmp;
            bool isCustomerMode = (lMode.Text == "Customer" || lMode.Text == "");

            if (isCustomerMode || lMode.Text == "MP") {
                _DisableButtons();
                _InitialUi();
                tmp = _Processor(isCustomerMode);

                if (tmp == -2)
                    return;
                else if (tmp < 0){
                    MessageBox.Show("There are some problems", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            _EnableButtons();
            bStart.Select();
        }

        private void I2cMasterConnect_CheckedChanged(object sender, EventArgs e)
        {
            if (IsSwitching)
                return;
            IsSwitching = true;

            int channelNumber;

            if ((lMode.Text == "Customer") || (lMode.Text == "_"))
                channelNumber = 1;
            else
                channelNumber = 13;

            if (cbI2cConnect.Checked) {
                if (!(engineerForm.ChannelSetApi(channelNumber) < 0))
                    I2cConnected = true;
                else {
                    MessageBox.Show("I2c master connection failed.\nPlease check if the hardware configuration or UI is activated.",
                                    "I2c master connection failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.FormClosing -= _FormClosing;
                    this.Close();
                    Application.ExitThread();
                    Environment.Exit(0);
                }
            }
            else
                I2cConnected = (engineerForm.I2cMasterDisconnectApi() < 0);

            Thread.Sleep(100);
            IsSwitching = false;
            cbI2cConnect.Focus();
            cbI2cConnect.Select();
        }

        private void tbLogFilePath_Click(object sender, EventArgs e)
        {
            _SetLogFilePath();
        }
                       
        private void bWriteFromFile_Click(object sender, EventArgs e)
        {
            _DisableButtons();
            string modelType = lProduct.Text;
            string directoryPath = Application.StartupPath;
            string tempRegisterFilePath = Path.Combine(directoryPath, "LogFolder/TempRegister.csv");

            engineerForm.WriteRegisterPageApi("Up 00h", 50, tempRegisterFilePath);
            engineerForm.WriteRegisterPageApi("Up 03h", 50, tempRegisterFilePath);
            engineerForm.WriteRegisterPageApi("80h", 200, tempRegisterFilePath);
            engineerForm.WriteRegisterPageApi("81h", 200, tempRegisterFilePath);
            engineerForm.WriteRegisterPageApi("Rx", 1000, tempRegisterFilePath);
            engineerForm.WriteRegisterPageApi("Tx", 1000, tempRegisterFilePath);
            engineerForm.StoreIntoFlashApi();
            engineerForm._GlobalRead();
            string LogFileName = "AfterFlasing";
            engineerForm.ExportLogfileApi(modelType, LogFileName, true, true);

            /*
            string[] commands = { "Up 00h", "Up 03h", "80h", "81h", "Tx", "Rx" };
            
            foreach(var command in commands) {             
                mainForm.WriteRegisterPageApi(command);
                Thread.Sleep(500);
            }
            */

            _EnableButtons();
        }

        private int _WriteSnDateCodeFlow()
        {
            string RegisterFilePath = Path.Combine(TempFolderPath, "RegisterFile.csv"); //Generate the CfgFilePath with config folder
            //FirstRound = true;  //??
           

            if (_VenderSnInputFormatCheck() < 0)
                return _CloseLoadingFormAndReturn(-1);

            for (ProcessingChannel = 1; ProcessingChannel <= (DoubleSideMode ? 2 : 1); ProcessingChannel++) {
                if (_WriteSnDatecode(ProcessingChannel) < 0) return -1; // Writing SN and DateCode, then export csv file.

                _UpdateMessage(ProcessingChannel, "VenderSN writed");

                if (!Sas3Module) {
                    if (ProcessingChannel == 1)
                        tbVersionCodeCh1.Text = engineerForm.GetFirmwareVersionCodeApi();
                    else if (ProcessingChannel == 2)
                        tbVersionCodeCh2.Text = engineerForm.GetFirmwareVersionCodeApi();
                }

                _MessageManagement(2, true);
                Application.DoEvents();

                Thread.Sleep(100);
                if (!DoubleSideMode) {
                    break;
                }
            }

            if (cbLogCheckAfterSn.Checked)
                _LogFileComparison();
                

            if (DoubleSideMode) {
               if (engineerForm.ChannelSwitchApi() < 0)// return to ch1
                    return _CloseLoadingFormAndReturn(-1);
            }

            SerialNumber = Convert.ToUInt16(_GetDomainUpDownValue("dudSsss"));
            SerialNumber++;
            _SetDomainUpDownValue("dudSsss", SerialNumber);
            _UpdateSerialNumberTextBox();
            return 0;
        }

        private int _CloseLoadingFormAndReturn(int returnValue)
        {
            this.BringToFront();
            this.Activate();

            if (!cbBarcodeMode.Checked) {
                _EnableButtons();
            }

            loadingForm.Close();
            return returnValue;
        }

        private int _VenderSnInputFormatCheck()
        {
            int serialNumber1;
            string newSerialNumber1;
            string venderSerialNumber = tbVenderSn.Text;

            try {
                if (cbSnNamingRule.SelectedIndex == 0 ) {
                    if (venderSerialNumber.Length != 12) {
                        MessageBox.Show("The SN must be exactly 12 characters long." +
                                        "\nPlease enter a valid Vender SN (YYMMDDRRSSSS).");
                        return -1;
                    }
                    CurrentDate = venderSerialNumber.Substring(0, 6).ToString(); // Get YYMMDD
                    Revision = Convert.ToInt16(venderSerialNumber.Substring(6, 2)); // Get RR
                    SerialNumber = Convert.ToInt16(venderSerialNumber.Substring(8, 4)); // Get SSSS
                }
                else if (cbSnNamingRule.SelectedIndex == 1) {
                    if (venderSerialNumber.Length != 10) {
                        MessageBox.Show("The SN must be exactly 10 characters long." +
                                        "\nPlease enter a valid Vender SN (YYWWDLSSSS).");
                        
                        return -1;
                    }
                    CurrentDate = venderSerialNumber.Substring(0, 6).ToString(); // Get YYWWDL
                    SerialNumber = Convert.ToInt16(venderSerialNumber.Substring(6, 4)); // Get SSSS
                }
                else {
                    if (venderSerialNumber.Length != 10) {
                        MessageBox.Show("The SN must be exactly 10 characters long." +
                                        "\nPlease enter a valid Vender SN (YYMMDDSSSS).");

                        return -1;
                    }
                    CurrentDate = venderSerialNumber.Substring(0, 6).ToString(); // Get YYMMDD
                    SerialNumber = Convert.ToInt16(venderSerialNumber.Substring(6, 4)); // Get SSSS
                }

                serialNumber1 = SerialNumber;

                if (serialNumber1 < MinSerialNumber || serialNumber1 > MaxSerialNumber) {
                    MessageBox.Show("Invalid serial number in VenderSN" +
                                    $"Serial number must be between {MinSerialNumber:D4} and {MaxSerialNumber:D4}.");
                    return -1;
                }
              
                newSerialNumber1 = serialNumber1.ToString("0000");

                if (cbSnNamingRule.SelectedIndex == 0)
                    tbVenderSn.Text = $"{CurrentDate}{Revision.ToString("D2")}{newSerialNumber1}";
                else
                    tbVenderSn.Text = $"{CurrentDate}{newSerialNumber1}";
            }
            catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}");
                return -1;
            }

            return 0;
        }

        private void _PromptCorrect(bool detailMessageUpdate, int ch)
        {
            if (cbBarcodeMode.Checked && false) {
                gbPrompt.Visible = true;
                gbPrompt.BackColor = Color.SpringGreen;
                lStatus.Visible = true;
                lStatus.Text = "Correct";
            }

            switch (ch) {
                case 1:
                    cProgressBar1.Text = "PASS";
                    cProgressBar1.ForeColor = Color.SpringGreen;
                    break;
                case 2:
                    cProgressBar2.Text = "PASS";
                    cProgressBar2.ForeColor = Color.SpringGreen;
                    break;
            }

            if (detailMessageUpdate)
                _UpdateMessage(ProcessingChannel, "Verify State:\nComparison match");

        }
        private void _PromptWrong(int ch)
        {
            if (cbBarcodeMode.Checked) {
                gbPrompt.Visible = true;
                gbPrompt.BackColor = Color.Violet;
                lStatus.Visible = true;
                lStatus.Text = "Wrong !!";
            }

            switch (ch) {
                case 1:
                    cProgressBar1.Text = "Wrong";
                    cProgressBar1.ForeColor = Color.HotPink;
                    break;
                case 2:
                    cProgressBar2.Text = "Wrong";
                    cProgressBar2.ForeColor = Color.HotPink;
                    break;
            }

            _UpdateMessage(ProcessingChannel, "Verify State:\nWrong !!");
        }

        private void _PromptError(bool detailMessageUpdate, int ch)
        {
            if (cbBarcodeMode.Checked) {
                gbPrompt.Visible = true;
                gbPrompt.BackColor = Color.HotPink;
                lStatus.Visible = true;
                lStatus.Text = "Error !!";
            }

            switch (ch) {
                case 1:
                    cProgressBar1.Text = "Failed";
                    cProgressBar1.ForeColor = Color.HotPink;
                    break;
                case 2:
                    cProgressBar2.Text = "Failed";
                    cProgressBar2.ForeColor = Color.HotPink;
                    break;
            }

            if (detailMessageUpdate)
                _UpdateMessage(ProcessingChannel, "Verify State:\nLogfile Mismatch!!");
        }

        private void _PromptCompleted(int ch, bool finishState)
        {
            if (cbBarcodeMode.Checked && finishState) {
                gbPrompt.Visible = true;
                gbPrompt.BackColor = Color.SpringGreen;
                lStatus.Visible = true;
                lStatus.Text = "Completed";
            }

            switch (ch) {
                case 1:
                    cProgressBar1.Text = "Done";
                    cProgressBar1.ForeColor = Color.SpringGreen;
                    break;
                case 2:
                    cProgressBar2.Text = "Done";
                    cProgressBar2.ForeColor = Color.SpringGreen;
                    break;
            }

            _UpdateMessage(ProcessingChannel, "Completed");
        }

        private void bWriteSnDateCode_Click(object sender, EventArgs e)
        {
            _DisableButtons();
            _InitialUi();

            _WriteSnDateCodeFlow();

            _EnableButtons();
            bWriteSnDateCone.Select();
        }

        private void bCfgFileComparison_Click(object sender, EventArgs e)
        {
            _DisableButtons();
            _InitialUi();
            
            _CfgFileComparison();

            _EnableButtons();
            bCfgFileComparison.Select();
        }

        private int _CfgFileComparison()
        {
            int comparisonResults = 0;
            string modelType = lProduct.Text;
            string directoryPath = TempFolderPath;
            string registerFilePath = Path.Combine(directoryPath, "RegisterFile.csv"); //Generate the CfgFilePath with config folder
            //FirstRound = true;

            for (ProcessingChannel = 1; ProcessingChannel <= (DoubleSideMode ? 2 : 1); ProcessingChannel++) {
                //engineerForm.I2cMasterDisconnectApi();
                //Thread.Sleep(500);
                I2cConnected = !(engineerForm.ChannelSetApi(ProcessingChannel) < 0);
                Thread.Sleep(200);

                if (Sas3Module) {
                    engineerForm._KeyForSas3();
                    comparisonResults = engineerForm.ComparisonRegisterForSas3Api(registerFilePath, true, "CfgFile", cbRegisterMapView.Checked, ProcessingChannel);
                }
                else {
                    comparisonResults = engineerForm.ComparisonRegisterApi(modelType, registerFilePath, true, "CfgFile", cbRegisterMapView.Checked);
                }
                
                if (comparisonResults < 0) 
                    return _CloseLoadingFormAndReturn(-1);

                Thread.Sleep(100);
                _MessageManagement(comparisonResults, true);
                
                if (!DoubleSideMode)
                    break;
            }

            if (DoubleSideMode)
                if (engineerForm.ChannelSwitchApi() < 0) 
                    return _CloseLoadingFormAndReturn(-1);

            return 0;
        }

        private void _MessageManagement(int controlCode, bool detailMessageUpdate)
        {
            Label messageLabel = (ProcessingChannel == 1) ? lCh1Message : lCh2Message;
            Color newColor;
            if (ProcessingChannel == 1) {
                cProgressBar1.Text = "A";
                cProgressBar1.ForeColor = Color.FromArgb(85, 213, 219);
            }
            else {
                cProgressBar2.Text = "B";
                cProgressBar2.ForeColor = Color.FromArgb(85, 213, 219);
            }

            switch (controlCode) {
                case 0: //PASS
                    newColor = Color.White;
                    _PromptCorrect(detailMessageUpdate, ProcessingChannel);
                    break;
                case 1: //Failed
                    newColor = Color.DeepPink;
                    _PromptError(detailMessageUpdate, ProcessingChannel);
                    break;
                case 2: //Completed
                    newColor = Color.White;
                    _PromptCompleted(ProcessingChannel, false);
                    break;
                case 3: //Wrong
                    newColor = Color.DeepPink;
                    _PromptWrong(ProcessingChannel);
                    break;

                default:
                    newColor = Color.White;
                    break;
            }

            messageLabel.ForeColor = newColor;
            Application.DoEvents();
        }

        private void _EnterDefaultLogfilePath()
        {
            string initialDirectory = Application.StartupPath;
            tbLogFilePath.Text = Path.Combine(initialDirectory, "LogFolder");
            tbLogFilePath.SelectionStart = tbLogFilePath.Text.Length;
        }

        private void bLogFileComparison_Click(object sender, EventArgs e)
        {
            _DisableButtons();
            _InitialUi();

            _LogFileComparison();

            _EnableButtons();
            bLogFileComparison.Select();
        }
        
        private void _LogFileComparison()
        {
            int comparisonResults = 0;
            string modelType = lProduct.Text;
            string directoryPath = tbLogFilePath.Text;
            string objectFileName;
            string registerFilePath;
            string venderSn;

            //mainForm.InformationReadApi();
            //string OriginalVenderSn = mainForm.GetVendorSnFromDdmiA_pi();
            //string OriginalDateCode = mainForm.GetDateCodeFromDdmiApi();
            //FirstRound = true;

            for (ProcessingChannel = 1; ProcessingChannel <= (DoubleSideMode ? 2 : 1); ProcessingChannel++) {
                _GetModuleVenderSn(true, ProcessingChannel);
                venderSn = ProcessingChannel == 1 ? tbOrignalSNCh1.Text : tbOrignalSNCh2.Text;
                objectFileName = venderSn + (ProcessingChannel == 1 ? "A" : "B");
                registerFilePath = Path.Combine(directoryPath, objectFileName + ".csv"); //Generate the CfgFilePath with config folder

                if (!Directory.Exists(directoryPath)) {
                    MessageBox.Show("Please check if the log file path has been correctly specified?");
                    goto exit;
                }

                if (!File.Exists(registerFilePath)) {
                    MessageBox.Show("Please check if the log file exists at the specified path?" +
                                    "\n\nTarget path: " + directoryPath + "\\..." +
                                    "\nModule SN: " + objectFileName + ".csv" + "   <<Missing file");
                    goto exit;
                }

                I2cConnected = !(engineerForm.ChannelSetApi(ProcessingChannel) < 0);
                Thread.Sleep(300);

                if (Sas3Module) {
                    engineerForm._KeyForSas3();
                    comparisonResults = engineerForm.ComparisonRegisterForSas3Api(registerFilePath, true, "LogFile", cbRegisterMapView.Checked, ProcessingChannel);
                }
                else {
                    comparisonResults = engineerForm.ComparisonRegisterApi(modelType, registerFilePath, true, "LogFile", cbRegisterMapView.Checked);
                }

                Thread.Sleep(100);
                _MessageManagement(comparisonResults, true);
                Application.DoEvents();

                if (!DoubleSideMode) 
                    break;
            }

        exit:
            if (DoubleSideMode) 
                if (engineerForm.ChannelSwitchApi() < 0) 
                    _CloseLoadingFormAndReturn(-1);
        }

        private int _GetModuleVenderSn(bool currentVenderSn, int ch)
        {
            string venderSn;

            if (Sas3Module)
                engineerForm._SetSas3Password();

            _UpdateMessage(ch, "CheckVendorSN");
            if (engineerForm.InformationReadApi() < 0)
                return -1;
            venderSn = engineerForm.GetVendorSnFromDdmiApi();
            venderSn = venderSn.Replace(" ", "");

            if (currentVenderSn) {
                if (ch == 1) {
                    tbOrignalSNCh1.Text = venderSn;
                    tbOrignalSNCh1.BackColor = Color.DarkBlue;
                    tbOrignalSNCh1.ForeColor = Color.White;
                }
                else {
                    tbOrignalSNCh2.Text = venderSn;
                    tbOrignalSNCh2.BackColor = Color.DarkBlue;
                    tbOrignalSNCh2.ForeColor = Color.White;
                }
            }
            else {
                if (ch == 1) {
                    tbReNewSNCh1.Text = venderSn;
                    tbReNewSNCh1.BackColor = Color.DarkBlue;
                    tbReNewSNCh1.ForeColor = Color.White;
                }

                else {
                    tbReNewSNCh2.Text = venderSn;
                    tbReNewSNCh2.BackColor = Color.DarkBlue;
                    tbReNewSNCh1.ForeColor = Color.White;
                }
            }

            return 0;
        }

        private int _GetTruelightSn(bool currentVenderSn, int ch)
        {
            string truelightSn;
            _UpdateMessage(ch, "CheckTLSN");
            
            truelightSn = engineerForm.GetSerialNumberApi();
            truelightSn = truelightSn.Replace(" ", "");

            if (currentVenderSn) {
                if (ch == 1) {
                    tbOrignalTLSN.Text = truelightSn;
                    tbOrignalTLSN.BackColor = Color.DarkBlue;
                    tbOrignalTLSN.ForeColor = Color.White;
                }
            }
            else {
                if (ch == 1) {
                    tbReNewTLSN.Text = truelightSn;
                    tbReNewTLSN.BackColor = Color.DarkBlue;
                    tbReNewTLSN.ForeColor = Color.White;
                }
            }

            return 0;
        }

        private void cbBarcodeMode_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBarcodeMode.Checked) {
                _DisableButtonsForBarcodeMode();
                rbSingle.Enabled = false;
                rbBoth.Enabled = false;
                rbSingle.Checked = true;
                rbFullMode.Visible = true;
                rbOnlySN.Visible = true;
                rbLogFileMode.Visible = true;
                tbVenderSn.Text = "";
                tbVenderSn.Select();
                gbPrompt.Visible = true;
                _InitializeUIForgbPrompt();
            }
            else {
                rbSingle.Enabled = true;
                rbBoth.Enabled = true;
                rbFullMode.Visible = false;
                rbOnlySN.Visible = false;
                rbLogFileMode.Visible = false;
                gbPrompt.Visible = false;
                _UpdateSerialNumberTextBox();
                _EnableButtonsForBarcodeMode();
            }
        }

        private void tbHideKey_MouseEnter(object sender, EventArgs e)
        {
            IsListeningForHideKeys = true;
        }

        private void tbHideKey_MouseLeave(object sender, EventArgs e)
        {
            IsListeningForHideKeys = false;
        }

        private void ReRssi_Click(object sender, EventArgs e)
        {
            _DisableButtons();
            _InitialUi();
            //_InformationCheck(false);
            _ReadRssiForBothSide();
            
            _EnableButtons();
            bRenewRssi.Select();
        }

        private void _UIActionForReWriteSerialNumber(bool executing)
        {
            if (executing) {
                bWriteTruelightSn.Enabled = false;
                bCheckSerialNumber.Enabled = false;
                bSearchNumber.Enabled = false;
                cbAutoWirte.Enabled = false;
                bExportRegister.Enabled = false;
            }
            else {
                if (!cbAutoWirte.Checked)
                    bWriteTruelightSn.Enabled = true;

                bCheckSerialNumber.Enabled = true;
                bSearchNumber.Enabled = false;
                cbAutoWirte.Enabled = true;
                bExportRegister.Enabled = true;
            }
            Application.DoEvents();
        }

        private void bWriteTruelightSn_Click(object sender, EventArgs e)
        {
            _InitializeUIForgbPrompt();
            _UIActionForReWriteSerialNumber(true);

            if (string.IsNullOrEmpty(tbTruelightSn.Text)) {
                MessageBox.Show("Plz enter the serial number in the text box");
                return;
            }

            _WriteTruelightSn(tbTruelightSn.Text);
            tbTruelightSn.Text = "";
            _GetSerialNumber();
            _UIActionForReWriteSerialNumber(false);
        }

        private int _WriteTruelightSn(string serialNumber)
        {
            tbReNewTLSN.Text = "";

            return engineerForm.SetSerialNumberApi(serialNumber);
        }

        private void bInfCheck_Click(object sender, EventArgs e)
        {
            _DisableButtons();
            _InitialUi();

            _InformationCheck(false);

            _EnableButtons();
            bCheckSerialNumber.Select();
        }

        private int _InformationCheck(bool deepCheck)
        {
            int errorCode = 0;
            int errorCount = 0;
            int channelNumber;
            string modelType = lProduct.Text;
            string tempRegisterFilePath = Path.Combine(TempFolderPath, "RegisterFile.csv"); //For UpPage00, Page03
            string directoryPath = Application.StartupPath;
            string reWriteRegister = Path.Combine(directoryPath, "RegisterFiles/ReWriteRegister.csv"); //For Page81,PageTx,PageRx

            _UIActionForReWriteSerialNumber(true);

            for (ProcessingChannel = 1; ProcessingChannel <= (DoubleSideMode ? 2 : 1); ProcessingChannel++) {
                switch (ProcessingChannel) {
                    case 1:
                        channelNumber = 1;
                        break;
                    case 2:
                        channelNumber = 2;
                        break;
                    default:
                        channelNumber = 0;
                        return -1;
                }

                I2cConnected = !(engineerForm.ChannelSetApi(channelNumber) < 0);
                Thread.Sleep(100);

                _GetSerialNumber();
                _GetFirmwareVersion(ProcessingChannel);
                _GetCustomerSn(ProcessingChannel);
                _GetPropagationDelay(ProcessingChannel);
            }


            if (DoubleSideMode) {
                ProcessingChannel = 1;
                engineerForm.ChannelSetApi(1);
            }

            _CheckTextBoxAndNotifyForSn();

            if (deepCheck) {
                if (!File.Exists(reWriteRegister)) {
                    MessageBox.Show("Please check if the log file exists at the specified path?" +
                                        "\n\nTarget path: " + reWriteRegister + "<<Missing file");
                    return 0;
                }

                //Part A: 透過撈出的String 跟Database 進行比對...from CSN, check to SN.
                _SerialNumberSerach();
                errorCode = _CheckAllSerialNumber();

                if (errorCode == -2) {
                    _PromptError(false, ProcessingChannel);
                    MessageBox.Show("ErrorCode: " + errorCode +
                                    "\n Input CustomerSn doesn't match the module CSN.");
                    return -1;
                }
                else if (errorCode == -3) {
                    _PromptError(false, ProcessingChannel);
                    MessageBox.Show("ErrorCode: " + errorCode +
                                    "\n Input TruelightSn doesn't match the module TLSN.");
                    return -1;
                }

                if (engineerForm.ComparisonRegisterForFinalCheckApi(modelType, reWriteRegister, "Low Page", true) != 0)
                    errorCount++;

                if (engineerForm.ComparisonRegisterForFinalCheckApi(modelType, reWriteRegister, "Page81", true) != 0)
                    errorCount++;

                if (engineerForm.ComparisonRegisterForFinalCheckApi(modelType, tempRegisterFilePath, "UpPage00", true) != 0)
                    errorCount++;
                
                if (engineerForm.ComparisonRegisterForFinalCheckApi(modelType, tempRegisterFilePath, "Page03", true) !=0)
                    errorCount++;

                //    errorCount++;

                //與ReWriteRegister.csv?? or LogFile 進行Tx參數比對.
                //與ReWriteRegister.csv or LogFile 進行Rx參數比對.

                if (errorCount == 0) {
                    tbCustomerSn.Text = "";
                    _PromptCorrect(false, ProcessingChannel);
                }
                else {
                    lCh1Message.Text = "Error count: " + errorCount;
                    _PromptError(false, ProcessingChannel);
                }
            }

            _UIActionForReWriteSerialNumber(false);

            if (tbCustomerSn.Enabled)
                tbCustomerSn.Select();
            else if (tbTruelightSn.Enabled)
                tbTruelightSn.Select();
            else
                bCheckSerialNumber.Select();

            return 0;
        }

        private int _CheckAllSerialNumber()
        {
            if (tbCustomerSn.Text != tbOrignalSNCh1.Text)
                return -2;

            if (tbTruelightSn.Text != tbOrignalTLSN.Text)
                return -3;

            return 0;
        }

        private void _CompletionNotificationForInfCheck()
        {
            tbOrignalTLSN.BackColor = Color.DarkBlue;
            tbVersionCodeCh1.BackColor = Color.DarkBlue;
            tbOrignalSNCh1.BackColor = Color.DarkBlue;
            Application.DoEvents();
        }

        private void _GetSerialNumber()
        {
            string sericalNumber = engineerForm.GetSerialNumberApi();

            tbOrignalTLSN.Text = "...";
            Application.DoEvents();
            tbOrignalTLSN.Text = sericalNumber;
            tbOrignalTLSN.BackColor = Color.DarkBlue;
            tbOrignalTLSN.ForeColor = Color.White;
            Application.DoEvents();
        }

        private void _GetFirmwareVersion(int ch)
        {
            string firmwareVersion = engineerForm.GetFirmwareVersionCodeApi();

            if (ch == 1) {
                tbVersionCodeCh1.Text = "...";
                Application.DoEvents();
                tbVersionCodeCh1.Text = firmwareVersion;
                tbVersionCodeCh1.BackColor = Color.DarkBlue;
                tbVersionCodeCh1.ForeColor = Color.White;
                Application.DoEvents();
            }
            else if (ch == 2) {
                tbVersionCodeCh2.Text = "...";
                Application.DoEvents();
                tbVersionCodeCh2.Text = firmwareVersion;
                tbVersionCodeCh2.BackColor = Color.DarkBlue;
                tbVersionCodeCh2.ForeColor = Color.White;
                Application.DoEvents();
            }
            else
                MessageBox.Show("The control channel has not been defined yet!!");
        }

        private void _GetCustomerSn(int ch)
        {
            string venderSn;

            if (engineerForm.InformationReadApi() < 0)
                return;

            venderSn = engineerForm.GetVendorSnFromDdmiApi();
            venderSn = venderSn.Replace(" ", "");

            if (ch == 1) {
                tbOrignalSNCh1.Text = "...";
                Application.DoEvents();
                tbOrignalSNCh1.Text = venderSn;
                tbOrignalSNCh1.BackColor = Color.DarkBlue;
                tbOrignalSNCh1.ForeColor = Color.White;
                Application.DoEvents();
            }
            else if (ch == 2) {
                tbOrignalSNCh2.Text = "...";
                Application.DoEvents();
                tbOrignalSNCh2.Text = venderSn;
                tbOrignalSNCh2.BackColor = Color.DarkBlue;
                tbOrignalSNCh2.ForeColor = Color.White;
                Application.DoEvents();
            }
            else
                MessageBox.Show("The control channel has not been defined yet!!");

        }

        private void _GetPropagationDelay(int ch)
        {
            string propagationDelay = engineerForm.GetPropagationDelayApi();

            if (ch == 1) {
                lCh1Message.Text = "...";
                Application.DoEvents();
                lCh1Message.Text = "Propagation Delay:\n" + propagationDelay;
                lCh1Message.ForeColor = Color.DarkBlue;
                Application.DoEvents();
            }
            else if (ch == 2) {
                lCh2Message.Text = "...";
                Application.DoEvents();
                lCh2Message.Text = "Propagation Delay:\n" + propagationDelay;
                lCh2Message.ForeColor = Color.DarkBlue;
                Application.DoEvents();
            }
            else
                MessageBox.Show("The control channel has not been defined yet!!");
        }

        private int _SerialNumberSerach()
        {
            string customerSn = tbCustomerSn.Text;

            if (!string.IsNullOrEmpty(customerSn)) {
                string result = GetTruelightSnFromDatabase(customerSn);
                if (result != null) {
                    tbTruelightSn.Text = result;
                    tbTruelightSn.BackColor = Color.DarkBlue;
                    tbTruelightSn.ForeColor = Color.White;
                }
                else {
                    MessageBox.Show("No matching serial number found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }

            return 0;
        }

        private int _CheckTextBoxAndNotifyForSn()
        {
            var textBoxesToCheck = new List<System.Windows.Forms.TextBox>
            {
                tbOrignalTLSN,
                //tbReNewTLSN,
                tbOrignalSNCh1,
                tbOrignalSNCh2,
                tbVersionCodeCh1,
                tbVersionCodeCh2
            };

            foreach (var textBox in textBoxesToCheck) {
                if (string.IsNullOrEmpty(textBox.Text)) {
                    textBox.BackColor = Color.HotPink;
                }
            }

            return 0;
        }

        private int _SetParameterFromPage8081()
        {
            string directoryPath = Application.StartupPath;
            string tempRegisterFilePath = Path.Combine(directoryPath, "RegisterFiles/ReWriteRegister.csv");

            if (!File.Exists(tempRegisterFilePath)) {
                MessageBox.Show("Loss the .../RegisterFiles/ReWriteRegister.csv");
                return -2775;
            }

            if (engineerForm.WriteRegisterPageApi("80h", 200, tempRegisterFilePath) < 0)
                return -2779;
            if (engineerForm.WriteRegisterPageApi("81h", 200, tempRegisterFilePath) < 0)
                return -2781;
            if (engineerForm.StoreIntoFlashApi() < 0)
                return -2783;

            /*
            LogFileName = "ReWriteRegister_CheckFile";
            if (mainForm.ExportLogfileApi(LogFileName, true, true) < 0)
                return -2762;
            */

            _GetModuleVenderSn(true, 1);
            string SnA = tbCustomerSn.Text;
            string SnB = tbOrignalSNCh1.Text;

            if (SnA != SnB) {
                MessageBox.Show("Wrong! Module SN has been cleared");
                return -9999;
            }

            return 0;
        }

        private string GetTruelightSnFromDatabase(string customerSn)
        {
            string databaseFolderPath = Path.Combine(Application.StartupPath, "SNDatabase");
            string[] datFiles = Directory.GetFiles(databaseFolderPath, "*.dat");

            foreach (var datFile in datFiles) {
                var lines = File.ReadAllLines(datFile);
                foreach (var line in lines) {
                    string[] columns = line.Split(new char[] { ',', '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (columns.Length >= 2 && columns[1].Trim() == customerSn) {
                        string truelightSn = columns[0].Trim();
                        truelightSn = truelightSn.Replace("-", "");

                        return truelightSn;
                    }
                }
            }

            return null;
        }

        private void _OpenLogfileExplorer(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) {
                MessageBox.Show("The file path is empty. Please check your input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(path) && !File.Exists(path)) {
                MessageBox.Show("The specified path does not exist. Please verify it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try {
                // If the path is a file, get its directory
                string folderPath = File.Exists(path) ? Path.GetDirectoryName(path) : path;
                Process.Start("explorer.exe", folderPath);
            }
            catch (Exception ex) {
                MessageBox.Show($"Failed to open File Explorer.\nError message: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbAutoWirte_CheckedChanged(object sender, EventArgs e)
        {
            _ReWriteSnCheckBoxChange();
        }
        private void cbReParameter_CheckedChanged(object sender, EventArgs e)
        {
            _ReWriteSnCheckBoxChange();
        }

        private void cbDeepCheck_Click(object sender, EventArgs e)
        {
            if (cbDeepCheck.Checked) {
                tbCustomerSn.Select();
            }
            else
                bCheckSerialNumber.Select();
        }
        private void _ReWriteSnCheckBoxChange()
        {
            _InitialUi();
            tbTruelightSn.Text = "";

            if (cbAutoWirte.Checked) {
                tbCustomerSn.Enabled = true;
                tbTruelightSn.Enabled = false;
                bWriteTruelightSn.Enabled = false;
                tbCustomerSn.Select();
            }
            else {
                tbCustomerSn.Enabled = false;
                tbTruelightSn.Enabled = true;
                bWriteTruelightSn.Enabled = true;
                tbTruelightSn.Select();
            }
        }

        private void bTest_Click(object sender, EventArgs e)
        {
            bSearchNumber.Enabled = false;
            _SerialNumberSerach();
            bSearchNumber.Enabled = true;
        }

        private void bExportRegister_Click(object sender, EventArgs e)
        {
            bExportRegister.Enabled = false;
            _ExportCurrentModuleRegisterToFile();
            bExportRegister.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int tmp = Convert.ToUInt16( _GetDomainUpDownValue("dudSsss"));
            MessageBox.Show("Step1\ndudSsss.Text_UInt16: " + _GetDomainUpDownValue("dudSsss"));
            
            SerialNumber = tmp;
            SerialNumber++;
            MessageBox.Show("Step2\nSerialNumber: " + SerialNumber.ToString("D4"));
            
            _SetDomainUpDownValue("dudSsss", SerialNumber);
            tmp = Convert.ToUInt16(_GetDomainUpDownValue("dudSsss"));
            MessageBox.Show("Step3\ndudSsss.Text_UInt16: " + tmp.ToString("D4"));

            SerialNumber = 10003;
            SerialNumber++;
            MessageBox.Show("Step4\nSerialNumber: " + SerialNumber.ToString("D4"));

            _SetDomainUpDownValue("dudSsss", SerialNumber);
            tmp = Convert.ToUInt16(_GetDomainUpDownValue("dudSsss"));
            MessageBox.Show("Step5\ndudSsss.Text_UInt16: " + tmp.ToString("D4"));
        }

        private void bHardwareValidation_Click(object sender, EventArgs e)
        {
            string ModelType = lProduct.Text;
            if (engineerForm.GetChipIdApi(ModelType))
                MessageBox.Show("Get it!!");

            engineerForm.HardwareIdentificationValidationApi(ModelType, true);
        }

        private void bCurrentRegister_Click(object sender, EventArgs e)
        {
            string modelType = lProduct.Text;
            _DisableButtons();

            if (modelType == "SAS4.0" || modelType == "PCIe4.0") {
                engineerForm.HardwareIdentificationValidationApi(modelType, false);
                engineerForm.CurrentRegistersApi(modelType);
            }
            else {
                MessageBox.Show("Crruent module is: " + modelType + "\nThe function only supports SAS4.0 or PCIe4.0");
                _EnableButtons();
                return;
            }

            _EnableButtons();
        }

        private void bOpenLogFileFolder_Click(object sender, EventArgs e)
        {
            string path = tbLogFilePath.Text;
            _DisableButtons();
            _OpenLogfileExplorer(path);
            _EnableButtons();
        }

        private void _ReadRssiForBothSide()
        {
            engineerForm.ChannelSetApi(0);
            ProcessingChannel = 1;
            engineerForm.ChannelSetApi(13);
            Thread.Sleep(500);
            _RxPowerUpdateWithoutThread();
            Thread.Sleep(200);

            if (rbBoth.Checked) {
                engineerForm.ChannelSetApi(23);
                ProcessingChannel = 2;
                Thread.Sleep(500);
                _RxPowerUpdateWithoutThread();
                Thread.Sleep(200);
                engineerForm.ChannelSetApi(13);
            }

            ProcessingChannel = 1;
            engineerForm.ChannelSetApi(1);
        }

        private void PerformLongRunningOperation()
        {
            System.Threading.Thread.Sleep(5000); // 模擬5秒
        }

        private void BarcodeMode_CheckedChanged(object sender, EventArgs e)
        {
            tbVenderSn.Select();
        }

        private void bRelinkTest_Click(object sender, EventArgs e)
        {
            int relinkCount = 0, startTime = 0, intervalTime = 0;
            _DisableButtons();
            engineerForm.SetAutoReconnectApi(true);
            ForceConnectWithoutInvoke = true;

            if (!string.IsNullOrEmpty(tbRelinkCount.Text) && int.TryParse(tbRelinkCount.Text, out int parsedRelinkCount))
                relinkCount = parsedRelinkCount;

            if (!string.IsNullOrEmpty(tbStartTime.Text) && int.TryParse(tbStartTime.Text, out int parsedStartTime))
                startTime = parsedStartTime;

            if (!string.IsNullOrEmpty(tbIntervalTime.Text) && int.TryParse(tbIntervalTime.Text, out int parsedIntervalTime))
                intervalTime = parsedIntervalTime;

            lCh1Message.Text = engineerForm.ForceConnectApi(true, relinkCount, startTime, intervalTime).ToString();
            ForceConnectWithoutInvoke = false;
            _EnableButtons();
            bRelinkTest.Select();
        }

        private void cbRelinkCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRelinkCheck.Checked) {
                tbRelinkCount.Visible = true;
                tbStartTime.Visible = true;
                tbIntervalTime.Visible = true;
                bRelinkTest.Visible = true;
            }
            else {
                tbRelinkCount.Visible = false;
                tbStartTime.Visible = false;
                tbIntervalTime.Visible = false;
                bRelinkTest.Visible = false;
            }
        }
    }

    public class MainFormPaths
    {
        public string ZipFolderPath { get; set; }
        public string ZipFilePath { get; set; }
        public string LogFilePath { get; set; }
        public string RssiCriteria { get; set; }
        public bool CfgFileCheckState { get; set; }
        public bool LogFileCheckState { get; set; }
        public bool RegisterMapViewState { get; set; }
    }

}

