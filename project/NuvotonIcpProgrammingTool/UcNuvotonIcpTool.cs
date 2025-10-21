using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using AardvarkAdapter;
using static System.Windows.Forms.AxHost;
//using System.Windows.Forms;
using I2cMasterInterface;
using System.Reflection;

namespace NuvotonIcpTool
{
    public partial class UcNuvotonIcpTool: UserControl
    {
        //private int iHandler = -1;
        private delegate int AdapterConnectedCB(int handler);
        private string IceId = null;
        private int LinkState = 0; // 0_Disconnted 1_Link ICE 2_Link module
        private bool SetToChannel2 = false;
        private bool FinishState = false;
        private bool EncryptionState = false;
        private string IceResponse = null;
        private bool PluginDetectionInterrupted = false;
        private bool UnplugDetectionInterruptionFlag = false;
        private bool ClickStartDetection = false;
        private bool SessionInterruptionFlag = false;
        //private bool ExternalControl = false;
        private bool FirstRound = true;
        private short TimeoutTime = 5000;
        private short ShortTiggerTime = 800;
        private bool I2cConnected = false;
        private bool RefuseToErase = false;
        private bool BypassEraseAllCheckMode = false;
        private bool AutoReconnectMode = false;
        //private bool PublicVariable = false;
        private bool DebugMode = false;
        //private bool ModuleDetecting = false;
        public int RelinkCount = 3;
        public int RelinkStartTime = 10;
        public int RelinkIntervalTime = 200;
        public int ReturnSleepTime;
        public event EventHandler<MessageEventArgs> MessageUpdated;
        internal I2cMaster i2cMaster = new I2cMaster();
        public delegate void LinkTextUpdatedEventHandler(string text);
        public event LinkTextUpdatedEventHandler LinkTextUpdated;
        public event EventHandler<I2cOperationEventArgs> RequestI2cOperation;
        public event Action<bool> OnPluginWaiting;
        public event Action<bool> OnPluginDetected;

        private void OnRequestI2cOperation(I2cOperationType operationType, int channel)
        {
            RequestI2cOperation?.Invoke(this, new I2cOperationEventArgs(operationType, channel));
        }

        public UcNuvotonIcpTool()
        {
            InitializeComponent();
            _InitializeUI(0, true);
            LastBinPaths lastBinPaths = LoadLastBinPaths();
            tbAPROM.Text = lastBinPaths.APROMPath.Value ?? AppDomain.CurrentDomain.BaseDirectory;
            tbLDROM.Text = lastBinPaths.LDROMPath.Value ?? AppDomain.CurrentDomain.BaseDirectory;
            tbDataFlash.Text = lastBinPaths.DataFlashPath ?? AppDomain.CurrentDomain.BaseDirectory;
            cbSecurityLock.Checked = lastBinPaths.SecurityLockState;
            _UpdateButtonState();
            this.Select();
        }

        protected virtual void OnMessageUpdated(MessageEventArgs e)
        {
            MessageUpdated?.Invoke(this, e);
        }

        private int _I2cMasterConnect()
        {
            if (i2cMaster.ConnectApi(400) < 0)
                return -1;

            return 0;
        }

        public int IcpChannelSetApi(int ch) // Used for NuvotonIcpTool own UI-form
        {
            int result = -1;

            if (this.InvokeRequired)
                this.Invoke(new Action(() => result = _ChannelSet(ch)));
            else
                result = _ChannelSet(ch);

            return result;
        }

        private int _ChannelSet(int Ch)
        {

            if (!I2cConnected) {
                if (_I2cMasterConnect() < 0)
                    return -1;

                I2cConnected = true;
            }

            Thread.Sleep(10);


            i2cMaster.ChannelSetApi(Ch);

            if (Ch == 0) {
                i2cMaster.DisconnectApi();
                I2cConnected = false;
            }

            return 0;
        }

        private void UpdateLinkButtonText(string text)
        {
            if (LinkTextUpdated != null)
                LinkTextUpdated(text);
        }

        public void IcpConnectApi() // Only used for MainForm_Icp button
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => _IcpConnect()));
            else
                _IcpConnect();
        }

        public void ForceConnectApi(bool relinkTestMode) // 
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => _ForceConnect(relinkTestMode)));
            else
                _ForceConnect(relinkTestMode);
        }

        public bool GetVarBoolState(string varName)
        {
            Type type = this.GetType();

            FieldInfo field = type.GetField(varName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null && field.FieldType == typeof(bool)) {
                return (bool)field.GetValue(this);
            }
            else {
                throw new ArgumentException("Invalid Var Name or Var is not a bool type");
            }
        }

        public void SetVarBoolState(string varName, bool value)
        {
            Type type = this.GetType();
            FieldInfo field = type.GetField(varName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null && field.FieldType == typeof(bool)) {
                field.SetValue(this, value);
            }
            else {
                throw new ArgumentException("Invalid Var Name or Var is not a bool type");
            }
        }

        public int GetVarIntState(string varName)
        {
            Type type = this.GetType();
            FieldInfo field = type.GetField(varName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null && field.FieldType == typeof(int)) {
                return (int)field.GetValue(this);
            }
            else {
                throw new ArgumentException("Invalid Var Name or Var is not a int type");
            }
        }

        public void SetVarIntState(string varName, int value)
        {
            Type type = this.GetType();
            FieldInfo field = type.GetField(varName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null && field.FieldType == typeof(int)) {
                field.SetValue(this, value);
            }
            else {
                throw new ArgumentException("Invalid Var Name or Var is not a int type");
            }
        }
        /*
        public void DisableButtonApi()
        {
            bLink.Enabled = false;
        }

        public void EnableButtonApi()
        {
            bLink.Enabled = true;
        }
        */
        public void SetButtonEnable(string buttonId, bool enabled)
        {
            System.Windows.Forms.Button button = this.Controls.Find(buttonId, true).FirstOrDefault() as System.Windows.Forms.Button;

            if (button != null)
                button.Enabled = enabled;
            else
                throw new ArgumentException("Invalid Button ID");
        }

        public string GetTextBoxText(string textBoxId)
        {
            System.Windows.Forms.TextBox textBox = this.Controls.Find(textBoxId, true).FirstOrDefault() as System.Windows.Forms.TextBox;

            if (textBox != null)
                return textBox.Text;
            else
                throw new ArgumentException("Invalid TextBox ID");
        }

        public void SetTextBoxText(string textBoxId, string newText)
        {
            System.Windows.Forms.TextBox textBox = this.Controls.Find(textBoxId, true).FirstOrDefault() as System.Windows.Forms.TextBox;

            if (textBox != null)
                textBox.Text = newText;
            else
                throw new ArgumentException("Invalid TextBox ID");
        }

        public string GetRichTextBoxText(string richTextBoxId)
        {
            System.Windows.Forms.RichTextBox textBox = this.Controls.Find(richTextBoxId, true).FirstOrDefault() as System.Windows.Forms.RichTextBox;

            if (textBox != null)
                return textBox.Text;
            else
                throw new ArgumentException("Invalid TextBox ID");
        }


        public bool GetCheckBoxState(string checkBoxId)
        {
            CheckBox checkBox = this.Controls.Find(checkBoxId, true).FirstOrDefault() as CheckBox;

            if (checkBox != null)
                return checkBox.Checked; // Get CheckBox ... Checked state
            else
                throw new ArgumentException("Invalid CheckBox ID"); // If no found 指定ID
        }

        public void SetCheckBoxState(string checkBoxId, bool state)
        {
            CheckBox checkBox = this.Controls.Find(checkBoxId, true).FirstOrDefault() as CheckBox;

            if (checkBox != null)
                checkBox.Checked = state; // Set CheckBox ... Checked state
            else
                throw new ArgumentException("Invalid CheckBox ID"); // // If no found 指定ID
        }

        public void UpdateSecurityLockStateApi()
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(_UpdateSecurityLockState));
            else
                _UpdateSecurityLockState();
        }

        private void _InitializeUI(int? LinkStateChange, bool ClearPriviusState)
        {
            if (LinkStateChange.HasValue)
                LinkState = LinkStateChange.Value;

            FinishState = false;
            _MessageUpdate();
            lMessage.ForeColor = Color.Black;
            lMcuPnContent.Text = "";
            richTextBox1.Text = "";
            tbCommand.Text = "";
            tbCfg1.Text = "0x00007000";
            bStart.Enabled = false;
            lMessage.ForeColor = Color.Black;
            lMessage.Text = "";
            tbDone.BackColor = Color.White;

            if (ClearPriviusState)
                _ClearPreviusState();

            System.Windows.Forms.Application.DoEvents();
        }

        private void _ClearPreviusState()
        {
            FinishState = false;
            lMessage.Text = "";
            tbDecrypt.BackColor = Color.White;
            tbErase.BackColor = Color.White;
            tbWrite.BackColor = Color.White;
            tbVefiry.BackColor = Color.White;
            tbReset.BackColor = Color.White;
            tbEncryption.BackColor = Color.White;
            tbDone.BackColor = Color.White;
            System.Windows.Forms.Application.DoEvents();
        }

        private void _MessageUpdate()
        {
            lLinkState.ForeColor = Color.Green;

            switch (LinkState) {
                case 0:
                    bLink.Text = "Connect";
                    lLinkState.ForeColor = Color.Red;
                    lLinkState.Text = "Disconnected";
                    break;
                case 1:
                    bLink.Text = "Stop Check";
                    lLinkState.ForeColor = Color.Red;
                    lLinkState.Text = "Connected...<< ICE_ID：" + IceId + " >>";
                    break;
                case 2:
                    bLink.Text = "Disconnect";
                    lLinkState.Text = "Connected...<< ICE_ID：" + IceId + " >>";
                    break;
                default:
                    bLink.Text = "Unknown State";
                    break;
            }

            UpdateLinkButtonText(bLink.Text);
        }

        private void UpdateCheckBoxState(CheckBox checkBox, System.Windows.Forms.Button button, System.Windows.Forms.TextBox textBox)
        {
            button.Enabled = checkBox.Checked;
            //textBox.Enabled = checkBox.Checked;
            textBox.SelectionStart = textBox.Text.Length;
        }

        private void _UpdateButtonState()
        {
            bStart.Enabled = (LinkState != 0);
            gbPathConfig.Enabled = (LinkState != 0);
            //cbLDROM.Enabled = (LinkState != 0);
            //cbAPROM.Enabled = (LinkState != 0);
            //cbDataFlash.Enabled = (LinkState != 0);
            //cbSecurityLock.Enabled = (LinkState != 0);

            UpdateCheckBoxState(cbLDROM, bLDROM, tbLDROM);
            UpdateCheckBoxState(cbAPROM, bAPROM, tbAPROM);
            UpdateCheckBoxState(cbDataFlash, bDataFlash, tbDataFlash);

            if (cbSecurityLock.Checked)
                tbCfg0.Text = "0xFFFFFFFC"; // lock
            else
                tbCfg0.Text = "0xFFFFFFFE"; // unlock

            bLink.Select();
            System.Windows.Forms.Application.DoEvents();
        }

        private void _UpdateSecurityLockState()
        {
            if (cbSecurityLock.Checked)
                tbCfg0.Text = "0xFFFFFFFC";
            else
                tbCfg0.Text = "0xFFFFFFFE";
        }

        private void _SaveLastBinPaths(string apromPath, string ldromPath, string dataFlashPath, bool continuousState)
        {

            string exeFolderPath = System.Windows.Forms.Application.StartupPath;
            string combinedPath = Path.Combine(exeFolderPath, "XmlFolder");

            if (!Directory.Exists(combinedPath))
                Directory.CreateDirectory(combinedPath);

            string xmlFilePath = Path.Combine(combinedPath, "LastBinPaths.xml");
            xmlFilePath = xmlFilePath.Replace("\\\\", "\\");

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("Paths");

            XmlElement apromPathElement = xmlDoc.CreateElement("APROMPath");
            apromPathElement.InnerText = apromPath;
            root.AppendChild(apromPathElement);

            XmlElement ldromPathElement = xmlDoc.CreateElement("LDROMPath");
            ldromPathElement.InnerText = ldromPath;
            root.AppendChild(ldromPathElement);

            XmlElement dataFlashPathElement = xmlDoc.CreateElement("DataFlashPath");
            dataFlashPathElement.InnerText = dataFlashPath;
            root.AppendChild(dataFlashPathElement);

            XmlElement continuousElement = xmlDoc.CreateElement("SecurityLockState");
            continuousElement.InnerText = continuousState.ToString();
            root.AppendChild(continuousElement);

            xmlDoc.AppendChild(root);
            xmlDoc.Save(xmlFilePath);
        }

        private LastBinPaths LoadLastBinPaths()
        {
            string folderPath = System.Windows.Forms.Application.StartupPath;
            string combinedPath = Path.Combine(folderPath, "XmlFolder");
            string xmlFilePath = Path.Combine(combinedPath, "LastBinPaths.xml");
            xmlFilePath = xmlFilePath.Replace("\\\\", "\\");

            try {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);
                XmlNode apromPathNode = xmlDoc.SelectSingleNode("//APROMPath");
                XmlNode ldromPathNode = xmlDoc.SelectSingleNode("//LDROMPath");
                XmlNode dataFlashPathNode = xmlDoc.SelectSingleNode("//DataFlashPath");
                XmlNode continuousNode = xmlDoc.SelectSingleNode("//SecurityLockState");

                string apromPath = apromPathNode?.InnerText;
                string ldromPath = ldromPathNode?.InnerText;
                string dataFlashPath = dataFlashPathNode?.InnerText;
                bool continuousState = continuousNode != null && Convert.ToBoolean(continuousNode.InnerText);

                return new LastBinPaths {
                    DataFlashPath = dataFlashPath,
                    APROMPath = new KeyValuePair<string, string>("APROMPath", apromPath),
                    LDROMPath = new KeyValuePair<string, string>("LDROMPath", ldromPath),
                    SecurityLockState = continuousState
                };
            }
            catch (Exception) {
                return new LastBinPaths {
                    DataFlashPath = null,
                    APROMPath = new KeyValuePair<string, string>("APROMPath", null),
                    LDROMPath = new KeyValuePair<string, string>("LDROMPath", null),
                    SecurityLockState = false
                };
            }
        }

        private string _ActionCommand(string command)
        {
            bool shortTigger = false;
            short timer;
            string apiPosition = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NuLinkTool\\NuLink.exe");

            richTextBox1.Text = "";
            if (command == "-p" || command == "-l")
                shortTigger = true;

            using (System.Diagnostics.Process process = new System.Diagnostics.Process()) {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    FileName = apiPosition,
                    Arguments = !string.IsNullOrEmpty(command) ? command : tbCommand.Text
                };

                process.StartInfo = startInfo;
                process.Start();

                if (shortTigger)
                    timer = ShortTiggerTime;
                else
                    timer = TimeoutTime;

                if (process.WaitForExit(timer)) {
                    IceResponse = process.StandardOutput.ReadToEnd();
                    richTextBox1.Text = IceResponse;

                    if (!string.IsNullOrEmpty(command)) {
                        return ParseResponse(IceResponse, command);
                    }
                }
                else {
                    //process.Kill(); //管理者權限問題.
                    string taskkillCommand = $"/F /PID {process.Id}";
                    Process.Start(new ProcessStartInfo("taskkill", taskkillCommand) { CreateNoWindow = true, UseShellExecute = false });

                    if (!FinishState && !PluginDetectionInterrupted) {
                        lMessage.Text = "TimeOut!! Plz check DUT...";
                        lMessage.ForeColor = Color.Red;
                        return "TimeOut";
                    }
                    return null;
                }

            }

            return null;
        }

        private string ParseResponse(string outputText, string command)
        {
            bool successAck = false;
            bool isLdrom = false;
            bool isAprom = false;
            bool isDatarom = false;
            bool isFileSizeBigger = false;
            string response = "";

            string line = outputText.Replace(Environment.NewLine, " ");
            Regex nuLinkIdRegex = new Regex(@"NuLink ID: 0x([0-9A-Fa-f]{8})", RegexOptions.Compiled);

            while (true) {
                if (DebugMode) //&& !(LinkState == 2)
                {
                    MessageBox.Show("<DeBug mode IcpState...>\n\nCommandLine: \n" + command
                                    + "\n\nTextBox: \n" + richTextBox1.Text
                                    );
                }

                switch (true) {
                    case var _ when line.Contains("NuLink ID: "):
                        successAck = true;
                        LinkState = 1;
                        Match match = nuLinkIdRegex.Match(line);
                        if (match.Success) {
                            response = match.Groups[1].Value;
                            tbDecrypt.BackColor = Color.GreenYellow;
                        }
                        System.Windows.Forms.Application.DoEvents();
                        break;

                    case var _ when line.Contains("Cannot find target IC chip connect with NuLink"):
                        successAck = true;
                        lMessage.Text = "No DUT found";
                        lMessage.ForeColor = Color.Red;
                        response = "NoDutFound";
                        break;

                    case var _ when line.Contains("MINI58ZDE"):
                        successAck = true;
                        LinkState = 2;
                        lMcuPnContent.Text = "MINI58ZDE";
                        System.Windows.Forms.Application.DoEvents();
                        response = "SuccessfullyLinked";
                        break;

                    case var _ when line.Contains("Get wrong CHIP Info"):
                        successAck = true;
                        EncryptionState = true;
                        response = "EncryptionState";
                        if (!FinishState)
                            tbDecrypt.BackColor = Color.Red;
                        break;

                    case var _ when line.Contains("Start erase") && line.Contains("Erase") && line.Contains("finish"):
                        successAck = true;
                        response = "EraseSuccess";
                        break;

                    case var _ when line.Contains("Start write") && line.Contains("Write") && line.Contains("finish"):
                        successAck = true;
                        response = "WriteSuccess";
                        break;

                    case var _ when line.Contains("Start to write CFG") && line.Contains("Finish write to CFG"):
                        successAck = true;
                        response = "WriteCfgSuccess";
                        break;

                    case var _ when line.Contains("Start read") && line.Contains("Verify") && line.Contains("success."):
                        successAck = true;
                        response = "VefirySuccess";
                        break;

                    case var _ when line.Contains("Chip Reset finish"):
                        successAck = true;
                        response = "ResetFinish";
                        break;

                    case var _ when line.Contains("Erase whole chip success"):
                        successAck = true;
                        EncryptionState = false;
                        response = "EraseAllSuccess";
                        break;

                    case var _ when isLdrom && line.Contains("Write") && line.Contains("fail.") && isFileSizeBigger:
                        successAck = true;
                        response = "LdromWriteFailed";
                        MessageBox.Show("Write failed. \nThe BIN file size is bigger than LDROM");
                        break;

                    case var _ when isAprom && line.Contains("Write") && line.Contains("fail.") && isFileSizeBigger:
                        successAck = true;
                        response = "ApromWriteFailed";
                        MessageBox.Show("Write failed. \nThe BIN file size is bigger than APROM");
                        break;

                    case var _ when isDatarom && line.Contains("Write") && line.Contains("fail.") && isFileSizeBigger:
                        successAck = true;
                        response = "DataromWriteFailed";
                        MessageBox.Show("Write failed. \nThe BIN file size is bigger than DataROM");
                        break;

                    case var _ when isLdrom && line.Contains("Verify") && line.Contains("fail.") && isFileSizeBigger:
                        successAck = true;
                        response = "LdromVerifyFailed";
                        MessageBox.Show("Verify failed. \nThe BIN file size is bigger than LDROM");
                        break;

                    case var _ when isAprom && line.Contains("Verify") && line.Contains("fail.") && isFileSizeBigger:
                        successAck = true;
                        response = "ApromVerifyFailed";
                        MessageBox.Show("Verify failed. \nThe BIN file size is bigger than APROM");
                        break;

                    case var _ when isDatarom && line.Contains("Verify") && line.Contains("fail.") && isFileSizeBigger:
                        successAck = true;
                        response = "DataromVerifyFailed";
                        MessageBox.Show("Verify failed. \nThe BIN file size is bigger than DataROM");
                        break;

                    case var _ when (line.Contains("BIN file size is bigger") || line.Contains("data fail. !!!")):
                        isFileSizeBigger = true;

                        if (line.Contains("LDROM"))
                            isLdrom = true;

                        if (line.Contains("APROM"))
                            isAprom = true;

                        if (line.Contains("DATAROM"))
                            isDatarom = true;

                        break;

                    case var _ when line.Contains("Command format Error"):
                        successAck = true;
                        lMessage.Text = "Command format Error";
                        lMessage.ForeColor = Color.Red;
                        response = "Error";
                        break;

                    case var _ when line.Contains("Execute operation ending"):
                        successAck = false;
                        break;


                    default:
                        MessageBox.Show("Unrecognized Line content: \n" + line);
                        break;

                }



                if (successAck) {
                    break;
                }

            }



            _MessageUpdate();
            return successAck ? response : null;
        }

        private void _EraseAll()
        {
            DialogResult result = DialogResult.None;

            if (!BypassEraseAllCheckMode)
                result = MessageBox.Show("Encryption State: Confirm clearing MCU Flash data?", "Preference", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if ((result == DialogResult.Yes) || BypassEraseAllCheckMode) {
                if (_ActionCommand("-e all M483KGCAE") == "EraseAllSuccess") {
                    lMessage.Text = "Erase whole chip success!";
                    lMessage.ForeColor = Color.Black;
                    tbDecrypt.BackColor = Color.GreenYellow;
                    tbErase.BackColor = Color.GreenYellow;
                    LinkState = 2;
                    _MessageUpdate();
                }
            }
            else if (result == DialogResult.No) {
                _InitializeUI(0, true);
                tbDecrypt.BackColor = Color.Red;
                RefuseToErase = true;
            }
        }

        private void _EraseApromOnly()
        {
            if (_ActionCommand("-e APROM") == "EraseAllSuccess") {
                //lMessage.Text = "Erase whole chip success!";
                //lMessage.ForeColor = Color.Black;
                //tbDecrypt.BackColor = Color.GreenYellow;
                //tbErase.BackColor = Color.GreenYellow;
                LinkState = 2;
                OnMessageUpdated(new MessageEventArgs("Firmware State:\nErase...Done", 40));
            }
        }

        private string _PathCheck(System.Windows.Forms.TextBox textBox)
        {
            string filePath = textBox.Text;
            string textBoxName = textBox.Name.Substring(2);
            string tmp = filePath;

            if (filePath.Contains(" ")) {
                System.Text.StringBuilder shortPath = new System.Text.StringBuilder(255);
                // Using the GetShortPathName for longPath change to shortPath
                NativeMethods.GetShortPathName(filePath, shortPath, (uint)shortPath.Capacity);
                filePath = shortPath.ToString();

                if (DebugMode)
                    MessageBox.Show("Original FilePath:\n" + tmp + "Converted Short FilePath:\n" + filePath);
            }

            if (File.Exists(filePath)) {
                if (Path.GetExtension(filePath).Equals(".bin", StringComparison.OrdinalIgnoreCase)) {
                    filePath = filePath.Replace("\\\\", "\\");

                    if (DebugMode)
                        MessageBox.Show("FilePath:\n" + filePath);

                    return (filePath);
                }
                else {
                    lMessage.Text = "For the " + textBoxName + " file, Please select a *.bin file.";
                    lMessage.ForeColor = Color.Red;
                }
            }
            else {
                lMessage.Text = "For the " + textBoxName + " file, the specified file does not exist. Please select the file.";
                lMessage.ForeColor = Color.Red;
            }

            return null;
        }

        private bool _PathCheckLoop()
        {
            bool pathCorrect = true;
            List<CheckBox> checkBoxes = new List<CheckBox>
            {
                cbLDROM,
                cbAPROM,
                cbDataFlash,
            };

            List<System.Windows.Forms.TextBox> textBoxesToCheck = new List<System.Windows.Forms.TextBox>
            {
                tbLDROM,
                tbAPROM,
                tbDataFlash
            };

            for (short i = 0; i < checkBoxes.Count; i++) {
                CheckBox checkBox = checkBoxes[i];
                System.Windows.Forms.TextBox textBox = textBoxesToCheck[i];

                if (checkBox.Checked && _PathCheck(textBox) == null) {
                    pathCorrect = false;
                    break;
                }
            }

            return pathCorrect;
        }

        private void _ConnectSingle()
        {
            string response;
            
            if (!SessionInterruptionFlag) {// 若對話中，
                switch (LinkState) {
                    case 0: // ICP hardware device exist check

                        int retryCount = 0;
                        int sleepTime = 10;

                        while (retryCount < 3) {
                            IceId = _ActionCommand("-l");
                            if (IceId != null && IceId != "TimeOut") {
                                LinkState = 1;
                                bLink.Enabled = true;
                                _MessageUpdate();
                                System.Windows.Forms.Application.DoEvents();
                                goto case 1;
                            }
                            else {
                                Thread.Sleep(sleepTime);
                                sleepTime += 200;
                                retryCount++;
                            }
                        }

                        LinkState = 0;
                        lMessage.Text = "Please...reconnect or reinsert the USB cable!";
                        lMessage.ForeColor = Color.Red;
                        break;

                    case 1: // MCU link check
                        response = _PluginDetection(AutoReconnectMode, false);

                        OnMessageUpdated(new MessageEventArgs("Firmware State:\nErase...", 37));

                        if (PluginDetectionInterrupted) {
                            PluginDetectionInterrupted = false;
                            break;
                        }

                        if (response != "MINI58ZDE")
                            lMessage.Text = "...";

                        if (EncryptionState)
                            _EraseAll();

                        EncryptionState = false;
                        //_EraseAll();

                        if (!RefuseToErase) // This state is bypassed, if the "No" button is clicked in the Erase function
                        {
                            _UnplugDetection();
                            RefuseToErase = false;
                        }

                        if (UnplugDetectionInterruptionFlag)
                            UnplugDetectionInterruptionFlag = false;

                        break;

                    case 2: // Connected with MCU
                        _InitializeUI(0, true);
                        break;

                    default:
                        break;
                }

                System.Windows.Forms.Application.DoEvents();
            }

            else if (SessionInterruptionFlag) {
                _InitializeUI(0, true);
                SessionInterruptionFlag = false;
            }

            _MessageUpdate();
            _UpdateButtonState();
        }

        private void _ForceConnect(bool relinkTestMode)
        {
            string response;

            if (!SessionInterruptionFlag) {
                switch (LinkState) {
                    case 0: // ICP hardware device exist check

                        int retryCount = 0;
                        int sleepTime = 10;

                        while (retryCount < 3) {
                            IceId = _ActionCommand("-l");
                            if (IceId != null && IceId != "TimeOut") {
                                LinkState = 1;
                                OnMessageUpdated(new MessageEventArgs("Firmware State:\nDevice connection...", 32));
                                goto case 1;
                            }
                            else {
                                Thread.Sleep(sleepTime);
                                sleepTime += 200;
                                retryCount++;
                            }
                        }

                        LinkState = 0;
                        OnMessageUpdated(new MessageEventArgs("Please...reconnect or reinsert the USB cable!", 0));
                        MessageBox.Show("Please...reconnect or reinsert the USB cable!");
                        break;

                    case 1: //Linked status
                        response = _PluginDetection(relinkTestMode, true);

                        if (relinkTestMode)
                            break;

                        if (PluginDetectionInterrupted) {
                            PluginDetectionInterrupted = false;
                            break;
                        }

                        if (response != "MINI58ZDE")
                            lMessage.Text = "...";

                        if (EncryptionState)
                            _EraseAll();

                        EncryptionState = false;
                        //_EraseAll();

                        /*
                        if (!RefuseToErase)
                        {
                            _UnplugDetection();
                            RefuseToErase = false;
                        }

                        if (UnplugDetectionInterruptionFlag)
                            UnplugDetectionInterruptionFlag = false;
                        */

                        //LinkState = 2;
                        OnMessageUpdated(new MessageEventArgs("Firmware State:\nDUT connected", 40));
                        break;

                    case 2: // Connected with MCU
                        _InitializeUI(0, true);

                        break;

                    default:
                        break;
                }
            }


            /*
            if (TestMode)
            {
                MessageBox.Show("EncryptionState: " + EncryptionState
                                + "\n(if true then erase)");
            }

            if (EncryptionState)
            _EraseAll();
            */
            _EraseApromOnly();
            EncryptionState = false;

        }

        private void _Write()
        {
            short sleep = 100;
            bLink.Enabled = false;
            gbPathConfig.Enabled = false;
            lMessage.ForeColor = Color.Black;
            bool wdLDROM = true, wdAPROM = true, wdDataFlash = true;
            bool vdLDROM = true, vdAPROM = true, vdDataFlash = true;

            if (_PathCheckLoop()) {
                /*
                if (!(_ActionCommand("-w CFG0 0xFFFFFFFE") == "WriteCfgSuccess") &&
                    (_ActionCommand("-w CFG1 " + tbCfg1.Text) == "WriteCfgSuccess"))
                    MessageBox.Show("-w CFG0 and CFG1 failed");
                */

                if (!(_ActionCommand("-e all") == "EraseSuccess"))
                    MessageBox.Show("Erase failed");

                if (!(_ActionCommand("-w CFG0 0xFFFFFFFE") == "WriteCfgSuccess"))
                    MessageBox.Show("Write cfg0 failed");

                if (!(_ActionCommand("-w CFG1 " + tbCfg1.Text) == "WriteCfgSuccess"))
                    MessageBox.Show("Write cfg1 failed");


                if (cbLDROM.Checked) {
                    wdLDROM = false;
                    if (!DebugMode) {
                        if (_ActionCommand("-w LDROM " + _PathCheck(tbLDROM)) == "WriteSuccess")
                            wdLDROM = true;
                    }
                    else {
                        if (_ActionCommand("-w LDROM " + _PathCheck(tbLDROM)) == "WriteSuccess") {
                            MessageBox.Show("LDROM: WriteSuccess");
                            wdLDROM = true;
                        }
                        else
                            MessageBox.Show("LDROM: WriteFailed");
                    }

                }

                Thread.Sleep(sleep);

                if (cbAPROM.Checked) {
                    wdAPROM = false;
                    if (!DebugMode) {
                        if (_ActionCommand("-w APROM " + _PathCheck(tbAPROM)) == "WriteSuccess")
                            wdAPROM = true;
                    }
                    else {
                        MessageBox.Show("Check Point");

                        if (_ActionCommand("-w APROM " + _PathCheck(tbAPROM)) == "WriteSuccess") {
                            MessageBox.Show("APROM: WriteSuccess");
                            wdAPROM = true;
                        }
                        else {
                            MessageBox.Show("LinkState: " + LinkState
                                            + "\nAPROM: WriteFailed");
                        }
                    }
                }

                Thread.Sleep(sleep);

                if (cbDataFlash.Checked) {
                    wdDataFlash = false;
                    if (!DebugMode) {
                        if (_ActionCommand("-w DATAROM " + _PathCheck(tbDataFlash)) == "WriteSuccess")
                            wdDataFlash = true;
                    }
                    else {
                        if (_ActionCommand("-w DATAROM " + _PathCheck(tbDataFlash)) == "WriteSuccess") {
                            MessageBox.Show("DATAROM: WriteSuccess");
                            wdDataFlash = true;
                        }
                        else
                            MessageBox.Show("DATAROM: WriteFailed");
                    }
                }

                Thread.Sleep(sleep);

                if (wdLDROM && wdAPROM && wdDataFlash) {
                    tbWrite.BackColor = Color.GreenYellow;
                    lMessage.Text = "Write success.";
                    OnMessageUpdated(new MessageEventArgs("Firmware State:\nWrite...Done", 45));
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(sleep);
                }
                else {
                    MessageBox.Show("Link state: " + LinkState
                                    + "\nwdLDROM: " + wdLDROM
                                    + "\nwdAPROM: " + wdAPROM
                                    + "\nwdDataFlash: " + wdDataFlash
                                    );
                }

                if (cbLDROM.Checked) {
                    vdLDROM = false;
                    if (_ActionCommand("-v LDROM " + _PathCheck(tbLDROM)) == "VefirySuccess")
                        vdLDROM = true;
                }

                if (cbAPROM.Checked) {
                    vdAPROM = false;
                    if (_ActionCommand("-v APROM " + _PathCheck(tbAPROM)) == "VefirySuccess")
                        vdAPROM = true;
                }

                if (cbDataFlash.Checked) {
                    vdDataFlash = false;
                    if (_ActionCommand("-v DATAROM " + _PathCheck(tbDataFlash)) == "VefirySuccess")
                        vdDataFlash = true;
                }

                if (vdLDROM && vdAPROM && vdDataFlash) {
                    tbVefiry.BackColor = Color.GreenYellow;
                    lMessage.Text = "Verify success.";
                    OnMessageUpdated(new MessageEventArgs("Firmware State:\nVerify...Done", 48));
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(sleep);
                }
                else {
                    MessageBox.Show("vdLDROM: " + vdLDROM
                                    + "\nvdAPROM: " + vdAPROM
                                    + "\nvdDataFlash: " + vdDataFlash
                                    );
                }

                if (_ActionCommand("-w CFG1 " + tbCfg1.Text) == "WriteCfgSuccess") {
                    tbEncryption.BackColor = Color.GreenYellow;
                    lMessage.Text = "Encryption...";
                    OnMessageUpdated(new MessageEventArgs("Firmware State:\nEncryption...Done", 51));
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(sleep);
                }

                if (_ActionCommand("-w CFG0 " + tbCfg0.Text) == "WriteCfgSuccess") {
                    tbReset.BackColor = Color.GreenYellow;
                    lMessage.Text = "Encrypt success.";
                    OnMessageUpdated(new MessageEventArgs("Firmware State:\nEncrypt...Done", 54));
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(sleep);
                }

                if (_ActionCommand("-reset ") == "ResetFinish") {
                    tbDone.BackColor = Color.GreenYellow;
                    lMessage.Text = "Reset...";
                    OnMessageUpdated(new MessageEventArgs("Firmware State:\nReset...", 57));
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(sleep);
                }
                bLink.Select();
                tbDone.BackColor = Color.GreenYellow;
                lMessage.Text = "Firmware burning has been completed.";
                OnMessageUpdated(new MessageEventArgs("Firmware State:\nBurning...Done", 60));
                lMessage.ForeColor = Color.Blue;
                //_ChannelSet(0);
                SessionInterruptionFlag = false;
                bLink.Enabled = true;
            }
        }

        private string _PluginDetection(bool relinkTestMode, bool externalCall)
        {
            bool dutStateChanged = false;
            bool autoRelinkMode = AutoReconnectMode;
            string stateCheck = null;
            int animationState = 0;
            int baseTime = RelinkStartTime; // Start time
            int intervalTime = RelinkIntervalTime; // Interval time
            int relinkCount = RelinkCount; // Relink count
            int counter = 0;
            bLink.Select();
            tbDone.BackColor = Color.White;
            OnPluginWaiting?.Invoke(true);
            //MessageBox.Show("Relink count:" + relinkCount + "\nStart time: " + startTime + "\nInterval time: " + RelinkIntervalTime);

            while (!dutStateChanged) {
                ReturnSleepTime = 0;
                lMessage.ForeColor = Color.Red;

                switch (animationState) {
                    case 0:
                        lMessage.Text = "Waiting for DUT to be plugin.../";
                        OnMessageUpdated(new MessageEventArgs("Firmware State:\nWaiting for DUT.../", 35));
                        break;
                    case 1:
                        lMessage.Text = "Waiting for DUT to be plugin...-";
                        OnMessageUpdated(new MessageEventArgs("Firmware State:\nWaiting for DUT...-", 35));
                        break;
                    case 2:
                        lMessage.Text = "Waiting for DUT to be plugin...\\";
                        OnMessageUpdated(new MessageEventArgs("Firmware State:\nWaiting for DUT...\\", 35));
                        break;
                    case 3:
                        lMessage.Text = "Waiting for DUT to be plugin...-";
                        OnMessageUpdated(new MessageEventArgs("Firmware State:\nWaiting for DUT...-", 35));
                        break;
                }

                animationState = (animationState + 1) % 4;
                System.Windows.Forms.Application.DoEvents();

                if (PluginDetectionInterrupted) {
                    //dutStateChanged = true;
                    LinkState = 0;
                    OnPluginWaiting?.Invoke(false);
                    return "Interrupted";
                }

                // Reconnection logic with correct counter increment
                if (!autoRelinkMode)
                    counter = relinkCount;


                if (counter < relinkCount) {
                    int currentSleepTime = (intervalTime * counter); // Adjust the sleep time dynamically
                    ReturnSleepTime = currentSleepTime; // Return value
                    if (externalCall)
                        OnRequestI2cOperation(I2cOperationType.SetChannel, 0); // Turn-off channel
                    else
                        _ChannelSet(0);

                    Thread.Sleep(currentSleepTime); // Sleep for the calculated time
                    stateCheck = _ActionCommand("-p");
                    Thread.Sleep(baseTime); // Sleep for the calculated time
                    // Check if it should switch to channel 2
                    if (SetToChannel2) {
                        if (externalCall)
                            OnRequestI2cOperation(I2cOperationType.SetChannel, 2);
                        else
                            _ChannelSet(2);
                    }
                    else {
                        if (externalCall)
                            OnRequestI2cOperation(I2cOperationType.SetChannel, 1);
                        else
                            _ChannelSet(1);
                    }

                    counter++;
                    Thread.Sleep(10);
                    stateCheck = _ActionCommand("-p");

                    if (stateCheck == "NoDutFound" || stateCheck == "TimeOut" || stateCheck == null)
                        dutStateChanged = false;
                    else {
                        dutStateChanged = true;
                        OnPluginDetected?.Invoke(true);
                    }
                }

                if (relinkTestMode && counter == relinkCount)
                    return stateCheck;

                if (counter == relinkCount) {
                    stateCheck = _ActionCommand("-p");
                    Thread.Sleep(10);

                    if (stateCheck == "NoDutFound" || stateCheck == "TimeOut" || stateCheck == null)
                        dutStateChanged = false;
                    else {
                        dutStateChanged = true;
                        OnPluginDetected?.Invoke(true);
                    }
                }
            }

            lMessage.ForeColor = Color.Black;
            bLink.Select();
            System.Windows.Forms.Application.DoEvents();
            OnPluginWaiting?.Invoke(false);
            return stateCheck;
        }

        /*
        private string _PluginDetection()
        {
            bool dutStateChanged = false;
            string stateCheck = null;
            int animationState = 0;
            int initialSleepTime = RelinkIntervalTime;
            int relinkCount = RelinkCount;
            bLink.Select();
            tbDone.BackColor = Color.White;
            OnPluginWaiting?.Invoke(true);
            //MessageBox.Show("Relink count:" + relinkCount + "\nSleep time: " + initialSleepTime);

            while (!dutStateChanged) {
                switch (animationState) {
                    case 0:
                        lMessage.Text = "Waiting for DUT to be plugin.../";
                        OnMessageUpdated(new MessageEventArgs("Firmware State:\nWaiting for DUT.../", 35));
                        break;
                    case 1:
                        lMessage.Text = "Waiting for DUT to be plugin...-";
                        OnMessageUpdated(new MessageEventArgs("Firmware State:\nWaiting for DUT...-", 35));
                        break;
                    case 2:
                        lMessage.Text = "Waiting for DUT to be plugin...\\";
                        OnMessageUpdated(new MessageEventArgs("Firmware State:\nWaiting for DUT...\\", 35));
                        break;
                    case 3:
                        lMessage.Text = "Waiting for DUT to be plugin...-";
                        OnMessageUpdated(new MessageEventArgs("Firmware State:\nWaiting for DUT...-", 35));
                        break;
                }

                lMessage.ForeColor = Color.Red;
                System.Windows.Forms.Application.DoEvents();
                stateCheck = _ActionCommand("-p");

                if (stateCheck == "NoDutFound" || stateCheck == "TimeOut" || stateCheck == null)
                    dutStateChanged = false;
                else {
                    dutStateChanged = true;
                    OnPluginDetected?.Invoke(true);
                }

                if (PluginDetectionInterrupted) {
                    //dutStateChanged = true;
                    LinkState = 0;
                    OnPluginWaiting?.Invoke(false);
                    //OnPluginDetected?.Invoke(true);
                    return "";
                }

                animationState = (animationState + 1) % 4;
                
                if (AutoReconnectMode)
                {
                    if (relinkCount < 5) { 
                        OnRequestI2cOperation(I2cOperationType.SetChannel, 0);
                        sleepTime -= 100;
                        relinkCount++;
                    }
                    else
                        sleepTime = 10;

                    Thread.Sleep(sleepTime);

                    if (SetToChannel2) {
                        OnRequestI2cOperation(I2cOperationType.SetChannel, 2);
                    }
                    else{
                        OnRequestI2cOperation(I2cOperationType.SetChannel, 1);
                    }
                }
            }

            lMessage.ForeColor = Color.Black;
            bLink.Select();
            System.Windows.Forms.Application.DoEvents();
            OnPluginWaiting?.Invoke(false);
            return stateCheck;
        }
        */

        private string _UnplugDetection() // 偵測模組在已連結Icp的期間，突然拔件的情境...；不適用在組合or連續動作功能使用
        {
            bool dutStateChanged = false;
            string stateCheck = null;
            int animationState = 0;
            bLink.Select();
            tbDone.BackColor = Color.White;
            bLink.Enabled = true;
            _UpdateButtonState();

            while (!dutStateChanged) {
                switch (animationState) {
                    case 0:
                        lMessage.Text = "DUT connected.../";
                        break;
                    case 1:
                        lMessage.Text = "DUT connected...-";
                        break;
                    case 2:
                        lMessage.Text = "DUT connected...\\";
                        break;
                    case 3:
                        lMessage.Text = "DUT connected...-";
                        break;
                }

                lMessage.ForeColor = Color.Black;
                System.Windows.Forms.Application.DoEvents();
                stateCheck = _ActionCommand("-p");

                if (stateCheck == "NoDutFound" || stateCheck == "TimeOut" || stateCheck == null) {
                    dutStateChanged = true; // Unplug DUT
                    _InitializeUI(0, true);
                }
                else
                    dutStateChanged = false;

                if (ClickStartDetection) {
                    dutStateChanged = true;
                    ClickStartDetection = false;
                }

                if (UnplugDetectionInterruptionFlag) {
                    LinkState = 0;
                    return "";
                }

                animationState = (animationState + 1) % 4;
            }

            //lMessage.ForeColor = Color.Red;
            bStart.Select();
            System.Windows.Forms.Application.DoEvents();
            return stateCheck;
        }

        private void tbRun_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                lMessage.Text = _ActionCommand(tbCommand.Text);
            }
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _UpdateButtonState();
        }

        private void UcNuvotonIcpTool_Load(object sender, EventArgs e)
        {
            _UpdateButtonState();
        }

        private void cbLDROM_CheckedChanged(object sender, EventArgs e)
        {
            _UpdateButtonState();
        }

        private void cbAPROM_CheckedChanged(object sender, EventArgs e)
        {
            _UpdateButtonState();
        }

        private void cbDataFlash_CheckedChanged(object sender, EventArgs e)
        {
            _UpdateButtonState();
        }

        private void cbConfig_CheckedChanged(object sender, EventArgs e)
        {
            _UpdateButtonState();
        }

        private void cbSecurityLock_CheckedChanged(object sender, EventArgs e)
        {
            _UpdateButtonState();
        }

        private void _LoadFilesPosition(System.Windows.Forms.TextBox textBox)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.Title = "Files position";
                openFileDialog.Filter = "Binary Files (*.bin)|*.bin";

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    string originalPath = openFileDialog.FileName;
                    string directory = Path.GetDirectoryName(originalPath); // Get filepath
                    string originalFileName = Path.GetFileName(originalPath); // Get original filename

                    if (originalFileName.Contains(" ")) {
                        string newFileName = originalFileName.Replace(" ", "_");
                        string newPath = Path.Combine(directory, newFileName);

                        try {
                            File.Move(originalPath, newPath); // 覆蓋原檔
                            textBox.Text = newPath; // 更新 TextBox 的內容為新路徑
                            _SaveLastBinPaths(tbAPROM.Text, tbLDROM.Text, tbDataFlash.Text, cbSecurityLock.Checked);

                            if(DebugMode) 
                                MessageBox.Show($"已重新命名為: {newFileName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (IOException ex) {
                            if (DebugMode)
                                MessageBox.Show($"無法重新命名: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else {
                        textBox.Text = originalPath;
                        _SaveLastBinPaths(tbAPROM.Text, tbLDROM.Text, tbDataFlash.Text, cbSecurityLock.Checked);
                    }

                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void bAPROM_Click(object sender, EventArgs e)
        {
            _LoadFilesPosition(tbAPROM);
        }

        private void bLDROM_Click(object sender, EventArgs e)
        {
            _LoadFilesPosition(tbLDROM);
        }

        private void bDataFlash_Click(object sender, EventArgs e)
        {
            _LoadFilesPosition(tbDataFlash);
        }

        internal void FormClosing()
        {
            //Switching(1);
            _UpdateButtonState();
        }

        private void _IcpConnect()
        {
            if (bLink.Enabled) {
                bLink.Enabled = false;
                Thread.Sleep(10);
            }
            //MessageBox.Show("LinkStatus: " + LinkState);

            if (LinkState != 0) //LinkState 非0 ，程序正進行中
            {
                SessionInterruptionFlag = true;
                UnplugDetectionInterruptionFlag = true;
            }

            if (LinkState == 1) //LinkState 非0 ，程序正進行中
                PluginDetectionInterrupted = true;

            _InitializeUI(null, true);
            _ConnectSingle();

            if (!bLink.Enabled) {
                bLink.Enabled = true;
                Thread.Sleep(10);
            }
        }

        public void StartFlashingApi()
        {
            if (!_PathCheckLoop()) {
                if (!bStart.Enabled)
                    bStart.Enabled = true;
                return;
            }

            bLink.Select();
            SessionInterruptionFlag = true;
            _SaveLastBinPaths(tbAPROM.Text, tbLDROM.Text, tbDataFlash.Text, cbSecurityLock.Checked);
            _Write();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            if (bStart.Enabled)
                bStart.Enabled = false;

            ClickStartDetection = true;

            if (FirstRound) {
                FirstRound = false;
            }

            StartFlashingApi();
            _InitializeUI(0, false);
            _UpdateButtonState();
            bStart.Enabled = false;
        }

        private void bIcpConnect_Click(object sender, EventArgs e)
        {
            _IcpConnect();
        }

        private void cbAutoReconnect_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoReconnect.Checked)
                AutoReconnectMode = true;
            else if (!cbAutoReconnect.Checked)
                AutoReconnectMode = false;
        }

        private void cbBypassEraseAllCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBypassEraseAllCheck.Checked)
                BypassEraseAllCheckMode = true;
            else if (!cbBypassEraseAllCheck.Checked)
                BypassEraseAllCheckMode = false;
        }
    }
}
