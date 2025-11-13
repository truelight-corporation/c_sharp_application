using Gn1090Gn1190Config;
using I2cMasterInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HSLinkGn1090Gn1190
{
    public partial class MainForm : Form
    {
        private I2cMaster i2cMaster = new I2cMaster();

        private UcGn1090Config ucGn1090_IC1;
        private UcGn1090Config ucGn1090_IC2;
        private UcGn1190Config ucGn1190_IC1;
        private UcGn1190Config ucGn1190_IC2;

        // Optical CH LED 映射
        private Dictionary<string, RadioButton> opticalLEDs;

        // 內容顯示 Panel
        private Panel contentPanel;

        // 當前選中的按鈕
        private Button currentButton;

        // ========== I2C 方法（保持不變）==========
        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[] { 32 };
            if (_I2cWrite(80, 127, 1, data) < 0) return -1;
            data[0] = mode;
            if (_I2cWrite(80, 164, 1, data) < 0) return -1;
            return 0;
        }

        private int _I2cMasterConnect()
        {
            if (i2cMaster.ConnectApi(100) < 0) return -1;
            cbConnected.Checked = true;
            if (_SetQsfpMode(0x4D) < 0) return -1;
            return 0;
        }

        private int _I2cMasterDisconnect()
        {
            if (i2cMaster.DisconnectApi() < 0) return -1;
            cbConnected.Checked = false;
            if (_SetQsfpMode(0x00) < 0) return -1;
            return 0;
        }

        private int _I2cRead(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;
            if (i2cMaster.connected == false)
            {
                if (_I2cMasterConnect() < 0) return -1;
            }
            rv = i2cMaster.ReadApi(devAddr, regAddr, length, data);
            if (rv < 0)
            {
                MessageBox.Show("QSFP+ module no response!!");
                _I2cMasterDisconnect();
            }
            else if (rv != length)
                MessageBox.Show("Only read " + rv + " not " + length + " byte Error!!");
            return rv;
        }

        private int _I2cWrite(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int rv;
            if (i2cMaster.connected == false)
            {
                if (_I2cMasterConnect() < 0) return -1;
            }
            rv = i2cMaster.WriteApi(devAddr, regAddr, length, data);
            if (rv < 0)
            {
                MessageBox.Show("QSFP+ module no response!!");
                _I2cMasterDisconnect();
            }
            return rv;
        }

        // ========== 建構函數 ==========
        public MainForm()
        {
            InitializeComponent();

            // 建立內容顯示區域
            CreateContentPanel();

            // 建立 IC 切換按鈕
            CreateICButtons();

            // 建立 4 個 IC UserControl
            CreateICControls();

            // 預設顯示 GN1090 IC_1
            ShowICControl(ucGn1090_IC1);

            // Form 載入事件
            this.Load += MainForm_Load;
        }

        // ========== 建立內容顯示 Panel ==========
        private void CreateContentPanel()
        {
            contentPanel = new Panel();
            contentPanel.BorderStyle = BorderStyle.FixedSingle;
            contentPanel.Location = new Point(25, 165);  // 在按鈕下方
            contentPanel.Size = new Size(745, 480);      // 根據您的截圖調整
            contentPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                                 AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(contentPanel);
        }

        // ========== 建立 IC 切換按鈕 ==========
        private void CreateICButtons()
        {
            int btnY = 128;        // Y 座標（在 Optical CH 下方）
            int btnWidth = 100;    // 按鈕寬度
            int btnHeight = 25;    // 按鈕高度
            int spacing = 5;       // 按鈕間距
            int startX = 25;       // 起始 X 座標

            // GN1090 IC_1 按鈕
            Button btnIC1 = new Button();
            btnIC1.Text = "GN1090 IC_1";
            btnIC1.Location = new Point(startX, btnY);
            btnIC1.Size = new Size(btnWidth, btnHeight);
            btnIC1.Tag = "IC1";
            btnIC1.Click += BtnIC1_Click;
            this.Controls.Add(btnIC1);

            // GN1090 IC_2 按鈕
            Button btnIC2 = new Button();
            btnIC2.Text = "GN1090 IC_2";
            btnIC2.Location = new Point(startX + (btnWidth + spacing), btnY);
            btnIC2.Size = new Size(btnWidth, btnHeight);
            btnIC2.Tag = "IC2";
            btnIC2.Click += BtnIC2_Click;
            this.Controls.Add(btnIC2);

            // GN1190 IC_1 按鈕
            Button btnIC3 = new Button();
            btnIC3.Text = "GN1190 IC_1";
            btnIC3.Location = new Point(startX + (btnWidth + spacing) * 2, btnY);
            btnIC3.Size = new Size(btnWidth, btnHeight);
            btnIC3.Tag = "IC3";
            btnIC3.Click += BtnIC3_Click;
            this.Controls.Add(btnIC3);

            // GN1190 IC_2 按鈕
            Button btnIC4 = new Button();
            btnIC4.Text = "GN1190 IC_2";
            btnIC4.Location = new Point(startX + (btnWidth + spacing) * 3, btnY);
            btnIC4.Size = new Size(btnWidth, btnHeight);
            btnIC4.Tag = "IC4";
            btnIC4.Click += BtnIC4_Click;
            this.Controls.Add(btnIC4);

            // 設定第一個按鈕為選中狀態
            currentButton = btnIC1;
            currentButton.BackColor = Color.LightBlue;
        }

        // ========== 按鈕事件處理 ==========
        private void BtnIC1_Click(object sender, EventArgs e)
        {
            UpdateButtonState((Button)sender);
            ShowICControl(ucGn1090_IC1);
        }

        private void BtnIC2_Click(object sender, EventArgs e)
        {
            UpdateButtonState((Button)sender);
            ShowICControl(ucGn1090_IC2);
        }

        private void BtnIC3_Click(object sender, EventArgs e)
        {
            UpdateButtonState((Button)sender);
            ShowICControl(ucGn1190_IC1);
        }

        private void BtnIC4_Click(object sender, EventArgs e)
        {
            UpdateButtonState((Button)sender);
            ShowICControl(ucGn1190_IC2);
        }

        // ========== 更新按鈕選中狀態 ==========
        private void UpdateButtonState(Button clickedButton)
        {
            if (currentButton != null)
            {
                currentButton.BackColor = SystemColors.Control;
            }
            currentButton = clickedButton;
            currentButton.BackColor = Color.LightBlue;
        }

        // ========== 建立 4 個 IC UserControl ==========
        private void CreateICControls()
        {
            // GN1090 IC_1 (Rx1-4)
            ucGn1090_IC1 = new UcGn1090Config();
            ucGn1090_IC1.SetI2cReadCBApi(_I2cRead);
            ucGn1090_IC1.SetI2cWriteCBApi(_I2cWrite);
            ucGn1090_IC1.Dock = DockStyle.Fill;

            // GN1090 IC_2 (Rx5-7)
            ucGn1090_IC2 = new UcGn1090Config();
            ucGn1090_IC2.SetI2cReadCBApi(_I2cRead);
            ucGn1090_IC2.SetI2cWriteCBApi(_I2cWrite);
            ucGn1090_IC2.Dock = DockStyle.Fill;

            // GN1190 IC_1 (Tx1-4)
            ucGn1190_IC1 = new UcGn1190Config();
            ucGn1190_IC1.SetI2cReadCBApi(_I2cRead);
            ucGn1190_IC1.SetI2cWriteCBApi(_I2cWrite);
            ucGn1190_IC1.Dock = DockStyle.Fill;

            // GN1190 IC_2 (Tx5-7)
            ucGn1190_IC2 = new UcGn1190Config();
            ucGn1190_IC2.SetI2cReadCBApi(_I2cRead);
            ucGn1190_IC2.SetI2cWriteCBApi(_I2cWrite);
            ucGn1190_IC2.Dock = DockStyle.Fill;
        }

        // ========== 顯示指定的 IC UserControl ==========
        private void ShowICControl(UserControl control)
        {
            contentPanel.Controls.Clear();
            contentPanel.Controls.Add(control);
        }

        // ========== Form 載入 ==========
        private void MainForm_Load(object sender, EventArgs e)
        {
            // 初始化 Optical CH LEDs（從設計檔案中的 RadioButton）
            InitializeOpticalLEDs();

            // 攔截所有 UserControl 的事件
            HookControlEvents(ucGn1090_IC1, 0, "RX");  // Rx1-4
            HookControlEvents(ucGn1090_IC2, 4, "RX");  // Rx5-7 (offset +4)
            HookControlEvents(ucGn1190_IC1, 0, "TX");  // Tx1-4
            HookControlEvents(ucGn1190_IC2, 4, "TX");  // Tx5-7 (offset +4)
        }

        // ========== 初始化 Optical CH LEDs ==========
        private void InitializeOpticalLEDs()
        {
            opticalLEDs = new Dictionary<string, RadioButton>();

            // ========== 根據您截圖中的 RadioButton 名稱 ==========
            // 請根據您實際的控制項名稱修改

            // RX 上排（從您的設計檔案中）
            opticalLEDs.Add("Rx4", GetRadioButton("rbRx4"));  // Optical CH 16
            opticalLEDs.Add("Rx5", GetRadioButton("rbRx5"));  // Optical CH 15
            opticalLEDs.Add("Rx6", GetRadioButton("rbRx6"));  // Optical CH 14
            opticalLEDs.Add("Rx1", GetRadioButton("rbRx1"));  // Optical CH 3
            opticalLEDs.Add("Rx2", GetRadioButton("rbRx2"));  // Optical CH 2
            opticalLEDs.Add("Rx3", GetRadioButton("rbRx3"));  // Optical CH 1

            // TX 下排
            opticalLEDs.Add("Tx4", GetRadioButton("rbTx4"));  // Optical CH 1
            opticalLEDs.Add("Tx5", GetRadioButton("rbTx5"));  // Optical CH 2
            opticalLEDs.Add("Tx6", GetRadioButton("rbTx6"));  // Optical CH 3
            opticalLEDs.Add("Tx1", GetRadioButton("rbTx1"));  // Optical CH 14
            opticalLEDs.Add("Tx2", GetRadioButton("rbTx2"));  // Optical CH 15
            opticalLEDs.Add("Tx3", GetRadioButton("rbTx3"));  // Optical CH 16

            // 初始化所有燈為灰色
            foreach (KeyValuePair<string, RadioButton> kvp in opticalLEDs)
            {
                if (kvp.Value != null)
                {
                    kvp.Value.BackColor = Color.Gray;
                    kvp.Value.Enabled = false;  // 禁用點擊（僅作為指示燈）
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(
                        string.Format("⚠ LED not found: {0}", kvp.Key));
                }
            }
        }

        // ========== 取得 RadioButton ==========
        private RadioButton GetRadioButton(string name)
        {
            // 直接從 this 的 Controls 中尋找
            // 因為您已經把 RadioButton 建在 MainForm 中
            Control ctrl = FindControlRecursive(this, name);
            return ctrl as RadioButton;
        }

        // ========== 攔截 UserControl 內的 CheckBox 事件 ==========
        private void HookControlEvents(Control parent, int channelOffset, string chipType)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is CheckBox)
                {
                    CheckBox chk = (CheckBox)ctrl;
                    string name = chk.Name.ToLower();

                    if (name.Contains("rx") || name.Contains("tx") ||
                        name.Contains("all") || name.Contains("ch") ||
                        name.Contains("power"))
                    {
                        int localChannel = ExtractChannelNumber(chk.Name);
                        int globalChannel = channelOffset + localChannel;

                        string type = chipType;
                        int channel = globalChannel;
                        string ctrlName = chk.Name;

                        chk.CheckedChanged += delegate (object s, EventArgs e)
                        {
                            CheckBox checkbox = (CheckBox)s;
                            OnChannelChanged(type, channel, checkbox.Checked, ctrlName);
                        };

                        System.Diagnostics.Debug.WriteLine(
                            string.Format("✓ Hooked: {0} Global#{1} ← {2}",
                                chipType, globalChannel, chk.Name));
                    }
                }

                if (ctrl.HasChildren)
                {
                    HookControlEvents(ctrl, channelOffset, chipType);
                }
            }
        }

        // ========== 從控制項名稱解析通道號 ==========
        private int ExtractChannelNumber(string controlName)
        {
            string digits = "";
            foreach (char c in controlName)
            {
                if (char.IsDigit(c))
                {
                    digits += c;
                }
            }

            int num;
            if (int.TryParse(digits, out num))
            {
                return num;
            }

            return 0;
        }

        // ========== 通道狀態改變處理 ==========
        private void OnChannelChanged(string chipType, int channelIndex, bool isEnabled, string controlName)
        {
            string channelName = (chipType == "RX") ?
                string.Format("Rx{0}", channelIndex) :
                string.Format("Tx{0}", channelIndex);

            UpdateOpticalLED(channelName, isEnabled);

            System.Diagnostics.Debug.WriteLine(
                string.Format("[{0}] {1} ({2}) → {3}",
                    DateTime.Now.ToString("HH:mm:ss"),
                    channelName,
                    controlName,
                    isEnabled ? "ON" : "OFF"));
        }

        // ========== 更新 Optical CH 燈號 ==========
        private void UpdateOpticalLED(string channelName, bool isEnabled)
        {
            if (opticalLEDs == null || !opticalLEDs.ContainsKey(channelName))
                return;

            RadioButton led = opticalLEDs[channelName];
            if (led == null) return;

            Color color = isEnabled ? Color.LimeGreen : Color.Gray;

            if (led.InvokeRequired)
            {
                led.Invoke(new Action(delegate () { led.BackColor = color; }));
            }
            else
            {
                led.BackColor = color;
            }
        }

        // ========== 遞迴尋找控制項 ==========
        private Control FindControlRecursive(Control parent, string name)
        {
            if (parent.Name == name)
                return parent;

            foreach (Control child in parent.Controls)
            {
                Control found = FindControlRecursive(child, name);
                if (found != null)
                    return found;
            }

            return null;
        }

        // ========== cbConnected 事件 ==========
        private void cbConnected_CheckedChanged_1(object sender, EventArgs e)
        {
            if (cbConnected.Checked == true)
                _I2cMasterConnect();
            else
                _I2cMasterDisconnect();
        }

        private void rbRX4_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
