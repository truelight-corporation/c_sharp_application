using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using I2cMasterInterface;
using System.Xml;

using ComponentFactory.Krypton.Toolkit;
using QsfpDigitalDiagnosticMonitoring;
using System.Threading;
using System.Runtime.Remoting.Channels;
using System.Web.UI.Design;
using System.Security.Policy;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Threading.Tasks;
using NuvotonIcpTool;
using System.Reflection;
using Ionic.Zip;
using System.Runtime.Remoting.Messaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using System.Globalization;
using System.Diagnostics;
using System.Web.Configuration;
using System.Diagnostics.Eventing.Reader;

namespace IntegratedGuiV2
{
    public partial class EngineerForm : KryptonForm
    {
        private I2cMaster i2cMaster = new I2cMaster();
        private DataTable dtWriteConfig = new DataTable();
        private WaitFormFunc loadingForm = new WaitFormFunc();

        private short iBitrate = 400; //kbps
        private short TriggerDelay = 100; //ms
        private int ProcessingChannel = 1;
        private bool FirstRead = false;
        private bool AutoSelectIcConfig = false;
        private string sAcConfig;
        private string APROMPath, DATAROMPath;
        private string ASidePath, BSidePath;
        private bool writeToFile = false;
        private string fileName = "3234.cfg";
        private bool DebugMode = false;
        private bool I2cConnected = false;
        private bool BypassWriteIcConfig = false;
        private bool FirstRound = true;
        private bool FlagFwUpdated = false;
        private bool _isQCMode = false;
        private string userRole = "";
        private string lastUsedDirectory;
        private bool SetQsfpMode = false;
        private bool TwoByteMode = false;
        internal bool BothSupplyMode = false;

        public bool InformationReadState { get; private set; }
        public bool DdmReadState { get; private set; }
        public bool MemDumpReadState { get; private set; }
        public bool CorrectorReadState { get; private set; }
        public string FwVersionCode, FwVersionCodeCheck;

        public event EventHandler<string> ReadStateUpdated;
        public event EventHandler<int> ProgressValue;
        public event EventHandler<MessageEventArgs> MainMessageUpdated;
        public event EventHandler<TextBoxTextEventArgs> TextBoxTextChanged;
        public event Action<bool> OnPluginWaiting;
        public event Action<bool> OnPluginDetected;
        private DataTable dtMemory = new DataTable();


        protected virtual void StateUpdated(string message, int? value)
        {
            ReadStateUpdated?.Invoke(this, message);

            if (value != null)
                ProgressValue?.Invoke(this, value.Value);

            Application.DoEvents();
        }

        private void SetupControlEvents()
        {
            ucNuvotonIcpTool.LinkTextUpdated += new UcNuvotonIcpTool.LinkTextUpdatedEventHandler(ucNuvotonIcp_LinkTextUpdated);
        }

        private void ucNuvotonIcp_LinkTextUpdated(string text)
        {
            
            if (this.bIcpConnect.InvokeRequired)// 檢查是否需要跨執行緒調用
                this.bIcpConnect.Invoke(new Action(() => this.bIcpConnect.Text = text));
            else
                this.bIcpConnect.Text = text;
        }

        public string GetValueFromUcRt146Config(string comboBoxId)
        {
            return ucRt146Config.GetComboBoxSelectedValue(comboBoxId);
        }

        public string GetValueFromUcRt145Config(string comboBoxId)
        {
            return ucRt145Config.GetComboBoxSelectedValue(comboBoxId);
        }

        public string GetFirmwareVersionCodeApi()
        {
            return _GetFirmwareVersionCode();
        }

        public List<string> GetComboBoxItems()
        {
            List<string> items = new List<string>();

            foreach (var item in cbProductSelect.Items)
            {
                items.Add(item.ToString());
            }

            return items;
        }

        public string GetSelectedProduct()
        {
            if (cbProductSelect.InvokeRequired)
                return (string)cbProductSelect.Invoke(new Func<string>(() => (string)cbProductSelect.SelectedItem));
            else
                return (string)cbProductSelect.SelectedItem;
        }

        public bool HardwareIdentificationValidationApi(string modelType, bool messageMode)
        {
            if (this.InvokeRequired)
                return (bool)this.Invoke(new Action(() => HardwareIdentificationValidation(modelType, messageMode)));
            else
                return (HardwareIdentificationValidation(modelType, messageMode));
        }

        public Dictionary<string, bool> GetCheckBoxStates()
        {
            return new Dictionary<string, bool>
            {
                { "cbInfomation", cbInfomation.Checked },
                { "cbDdm", cbDdm.Checked },
                { "cbMemDump", cbMemDump.Checked },
                { "cbCorrector", cbCorrector.Checked },
                { "cbTxIcConfig", cbTxIcConfig.Checked },
                { "cbRxIcConfig", cbRxIcConfig.Checked }
            };
        }

        public void SelectProductApi(string selectedProduct)
        {
            if (cbProductSelect.InvokeRequired)
                cbProductSelect.Invoke(new Action(() => CheckAndSelectProduct(selectedProduct)));
            else
                CheckAndSelectProduct(selectedProduct);
        }

        private void CheckAndSelectProduct(string selectedProduct)
        {
            if (cbProductSelect.Items.Contains(selectedProduct))
                cbProductSelect.SelectedItem = selectedProduct;
            else
                MessageBox.Show("Product not found in the ComboBox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void SetCheckboxCheckedApi(CheckBox checkbox, bool value)
        {
            if (checkbox.InvokeRequired)
                checkbox.Invoke(new Action(() => checkbox.Checked = value));
            else
                checkbox.Checked = value;
        }

        public void SetCheckBoxCheckedByNameApi(string checkBoxName, bool value)
        {
            CheckBox checkBox = GetCheckBoxByName(checkBoxName);
            if (checkBox != null)
                SetCheckboxCheckedApi(checkBox, value);
        }

        public bool GetVarBoolStateApi(string varName)
        {
            Type type = this.GetType();

            FieldInfo field = type.GetField(varName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null && field.FieldType == typeof(bool))
            {
                return (bool)field.GetValue(this);
            }
            else
            {
                throw new ArgumentException("Invalid Var Name or Var is not a bool type");
            }
        }

        
        public bool GetChipIdApi(string modelType)
        {
            if (this.InvokeRequired)
                return (bool)this.Invoke(new Action(() => _GetChipId(modelType)));
            else
                return (_GetChipId(modelType));
        }

        public string GetSerialNumberApi()
        {
            if (this.InvokeRequired)
                return (string)this.Invoke(new Action(() => ucMemoryDump.GetSerialNumberApi()));
            else
                return (ucMemoryDump.GetSerialNumberApi());
        }

        public string GetHiddenPasswordApi()
        {
            if (this.InvokeRequired)
                return (string)this.Invoke(new Action(() => ucMemoryDump.GetHiddenPasswordApi()));
            else
                return (ucMemoryDump.GetHiddenPasswordApi());
        }

        public int SetSerialNumberApi(string serialNumber)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _SetSerialNumber(serialNumber)));
            else
                return _SetSerialNumber(serialNumber);
        }

        public void SetVarBoolStateToMainFormApi(string varName, bool value)
        {
            Type type = this.GetType();
            FieldInfo field = type.GetField(varName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null && field.FieldType == typeof(bool))
            {
                field.SetValue(this, value);
            }
            else
            {
                throw new ArgumentException("Invalid Var Name or Var is not a bool type");
            }
        }
        public int ComparisonRegisterApi(string modelType, string filePath, bool onlyVerifyMode, string comparisonObject, bool engineerMode)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _ComparisonRegister(modelType, filePath, onlyVerifyMode, comparisonObject, engineerMode)));
            else
                return _ComparisonRegister(modelType, filePath, onlyVerifyMode, comparisonObject, engineerMode);
        }

        public int ComparisonRegisterForSas3Api(string filePath, bool onlyVerifyMode, string comparisonObject, bool engineerMode, int ch)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _ComparisonRegisterForSas3(filePath, onlyVerifyMode, comparisonObject, engineerMode, ch)));
            else
                return _ComparisonRegisterForSas3(filePath, onlyVerifyMode, comparisonObject, engineerMode, ch);
        }

        public int ComparisonRegisterForFinalCheckApi(string modelType, string filePath, string comparisonObject, bool engineerMode)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _ComparisonRegisterForFinalCheck(modelType, filePath, comparisonObject, engineerMode)));
            else
                return _ComparisonRegisterForFinalCheck(modelType, filePath, comparisonObject, engineerMode);
        }

        public int ExportLogfileApi(string modelType, string fileName, bool logFileMode, bool writeSnMode)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _ExportLogfile(modelType, fileName, logFileMode, writeSnMode)));
            else
                return _ExportLogfile(modelType, fileName, logFileMode, writeSnMode);
        }

        public int ExportLogfileForSas3Api(string fileName, bool logFileMode, bool writeSnMode, int processingChannel)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _ExportLogfileForSas3(fileName, logFileMode, writeSnMode, processingChannel)));
            else
                return _ExportLogfileForSas3(fileName, logFileMode, writeSnMode, processingChannel);
        }

        public int ForceConnectApi(bool relinkTestMode, int relinkCount, int startTime, int intervalTime) // Used for MpForm
        {
            if (relinkCount != 0)
                ucNuvotonIcpTool.RelinkCount = relinkCount;

            if (intervalTime != 0)
                ucNuvotonIcpTool.RelinkStartTime = startTime;

            if (intervalTime != 0)
                ucNuvotonIcpTool.RelinkIntervalTime = intervalTime;

            if (this.InvokeRequired)
                this.Invoke(new Action(() => ucNuvotonIcpTool.ForceConnectApi(relinkTestMode)));
            else
                ucNuvotonIcpTool.ForceConnectApi(relinkTestMode);

            return ucNuvotonIcpTool.ReturnSleepTime;
        }

        public void CurrentRegistersApi(string modelType)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => _CurrentRegisters(modelType)));
            else
                _CurrentRegisters(modelType);
        }

        public void StartFlashingApi() // Used for MpForm
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => ucNuvotonIcpTool.StartFlashingApi()));
            else
                ucNuvotonIcpTool.StartFlashingApi();
        }

        public int StoreIntoFlashApi()
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => ucInformation.StoreIntoFlashApi()));
            else
                return ucInformation.StoreIntoFlashApi();
        }

        public int InformationWriteApi() // Used for MpForm
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => ucInformation.WriteAllApi()));
            else
                return ucInformation.WriteAllApi();
        }
        
        public int WriteVendorSerialNumberApi(string vendorSn, string dataCode)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => ucInformation.WriteVendorSerialNumberApi(vendorSn, dataCode)));
            else
                return ucInformation.WriteVendorSerialNumberApi(vendorSn, dataCode);
        }
        
        public int InformationReadApi() // Used for MpForm
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => ucInformation.ReadAllApi()));
            else
                return ucInformation.ReadAllApi();
        }

        public string GetVenderPnApi() // Used for MpForm
        {
            if (this.InvokeRequired)
                return (string)this.Invoke(new Action(() => ucInformation.GetVenderPnApi()));
            else
                return ucInformation.GetVenderPnApi();
        }

        public void SetToChannle2Api(bool mode)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => _SetToChannel2(mode)));
            else
                _SetToChannel2(mode);
        }

        public void SetAutoReconnectApi(bool Mode)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => _SetAutoReconnectControl(Mode)));
            else
                _SetAutoReconnectControl(Mode);
        }

        public void SetBypassEraseAllCheckModeApi(bool Mode)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => _SetBypassEraseAllControl(Mode)));
            else
                _SetBypassEraseAllControl(Mode);
        }

        public bool GetAutoReconnectApi()
        {
            if (this.InvokeRequired)
                return (bool)this.Invoke(new Action(()  => _GetAutoReconnectControl()));
            else
                return _GetAutoReconnectControl();
        }

        public bool GetBypassEraseAllCheckModeApi()
        {
            if (this.InvokeRequired)
                return (bool)this.Invoke(new Action(() => _GetBypassEraseAllControl()));
            else
                return _GetBypassEraseAllControl();
        }

        public int WriteRegisterPageApi(string targetPage, int delayTime, string registerFilePath)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _WriteRegisterPage(targetPage, delayTime, registerFilePath)));
            else
                return _WriteRegisterPage(targetPage, delayTime, registerFilePath);
        }

        public int WriteRegisterPageForSas3Api(string targetPage, int delayTime, byte startAddr, int numberOfBytes, string registerFilePath, int processingChannel)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _WriteRegisterPageForSas3(targetPage, delayTime, startAddr, numberOfBytes, registerFilePath, processingChannel)));
            else
                return _WriteRegisterPageForSas3(targetPage, delayTime, startAddr, numberOfBytes, registerFilePath, processingChannel);
        }

        public void SetVarBoolStateToNuvotonIcpApi(string varName, bool value)
        {
            ucNuvotonIcpTool.SetVarBoolState(varName, value);
        }
        
        public void SetVarIntStateToNuvotonIcpApi(string varName, int value)
        {
            ucNuvotonIcpTool.SetVarIntState(varName, value);
        }

        public void SetCheckBoxStateToNuvotonIcpApi(string checkBoxId, bool state)
        {
            ucNuvotonIcpTool.SetCheckBoxState(checkBoxId, state);
        }

        public bool GetVarBoolStateFromNuvotonIcpApi(string varName)
        {
            return ucNuvotonIcpTool.GetVarBoolState(varName);
        }

        public int GetVarIntStateFromNuvotonIcpApi(string varName)
        {
            return ucNuvotonIcpTool.GetVarIntState(varName);
        }

        public bool GetCheckBoxStateFromNuvotonIcpApi(string checkBoxId)
        {
            return ucNuvotonIcpTool.GetCheckBoxState(checkBoxId);
        }

        public void SetTextBoxTextToNuvotonIcpApi(string textBoxId, string newText)
        {
            ucNuvotonIcpTool.SetTextBoxText(textBoxId, newText);
        }

        public string GetTextBoxTextFromNuvotonIcpApi(string checkBoxId)
        {
            return ucNuvotonIcpTool.GetTextBoxText(checkBoxId);
        }

        public string GetTextBoxTextFromDdmiApi(string checkBoxId)
        {
            return ucInformation.GetTextBoxText(checkBoxId);
        }

        public string GetTextBoxTextFromCorrectorApi(string checkBoxId)
        {
            return (checkBoxId);
        }

        public void SetVendorSnToDdmiApi(string text)
        {
            ucInformation.SetVendorSnApi(text);
        }

        public void SetDataCodeToDdmiApi(string text)
        {
            ucInformation.SetDateCodeApi(text);
        }

        public string GetVendorSnFromDdmiApi()
        {
            return ucInformation.GetVendorSnApi();
        }

        public string GetDateCodeFromDdmiApi()
        {
            return ucInformation.GetDateCodeApi();
        }

        public string GetPropagationDelayApi()
        {
            return ucInformation.GetPropagationDelayApi();
        }

        public void SetVarBoolStateToDdmApi(string varName, bool value)
        {
            ucDigitalDiagnosticsMonitoring.SetVarBoolState(varName, value);
        }

        public bool GetVarBoolStateFromDdmApi(string varName)
        {
            return ucDigitalDiagnosticsMonitoring.GetVarBoolState(varName);
        }

        public string GetTextBoxTextFromDdmApi(string textBoxId)
        {
            return ucDigitalDiagnosticsMonitoring.GetTextBoxText(textBoxId);
        }

        public int RxPowerReadApiFromDdmApi()
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => ucDigitalDiagnosticsMonitoring.RxPowerReadApi()));
            else
                return ucDigitalDiagnosticsMonitoring.RxPowerReadApi();
        }

        public void UpdateSecurityLockStateFromNuvotonIcpApi()
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(ucNuvotonIcpTool.UpdateSecurityLockStateApi));
            else
                ucNuvotonIcpTool.UpdateSecurityLockStateApi();
        }

        private CheckBox GetCheckBoxByName(string name)
        {
            switch (name)
            {
                case "cbInfomation":
                    return cbInfomation;
                case "cbDdm":
                    return cbDdm;
                case "cbMemDump":
                    return cbMemDump;
                case "cbCorrector":
                    return cbCorrector;
                case "cbTxIcConfig":
                    return cbTxIcConfig;
                case "cbRxIcConfig":
                    return cbRxIcConfig;

                default:
                    return null;
            }
        }

        private int _AppendWriteToFile(string sAction)
        {
            if (!File.Exists(fileName)||
                string.IsNullOrEmpty(File.ReadAllText(fileName)))
            { 
                sAcConfig = "//Write,DevAddr,RegAddr,Value\n" + "//Read,DevAddr,RegAddr,Value\n";
            }
            else
            {
                sAcConfig = "";
            }

            sAcConfig += sAction + "\n";

            File.AppendAllText(fileName, sAcConfig);
            return 0;
        }

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[] { 0xaa };

            if (writeToFile == false)
            {

                if (i2cMaster.WriteApi(80, 127, 1, data) < 0)
                    return -1;

                data[0] = mode;

                if (i2cMaster.WriteApi(80, 164, 1, data) < 0)
                    return -1;
            }
            else
            {
                _AppendWriteToFile("Write,0x50,0x7F,0xAA");
                _AppendWriteToFile($"Write,0x50,0x7F,0x{mode:X2}");
            }

            //MessageBox.Show("SetQsfpMode!!");

            return 0;
        }

        private int _I2cMasterConnect(int ch)
        {
            if (i2cMaster.ConnectApi(400) < 0)
                return -1;

            if (ch == 1)
                ProcessingChannel = 1;
            else
                ProcessingChannel = 2;

            cbConnect.Checked = true;
            I2cConnected = true;
            if (_WriteModulePassword() < 0)
                return -1;

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            return 0;
        }

        private int _I2cMasterDisconnect()
        {
            if (i2cMaster.DisconnectApi() < 0)
                return -1;

            cbConnect.Checked = false;
            I2cConnected = false;

            return 0;
        }

        private int _ChannelSwitch()
        {
            _ChannelSet(GetChannelControl(ProcessingChannel == 1 ? 2 : 1));
            ProcessingChannel = ProcessingChannel == 1 ? 2 : 1;
            _UpdateButtonState();

            return ProcessingChannel;
        }

        private int GetChannelControl(int processingChannel)
        {
            if (BothSupplyMode) {
                if (processingChannel == 1)
                    return 13;
                else if (processingChannel == 2)
                    return 23;
                else
                    return 0;
            }
            else {
                return processingChannel;
            }
        }

        private int _ChannelSet(int ch)
        {
            if (!I2cConnected)
            {
                if (_I2cMasterConnect(ch) < 0) //不可指定為ProcessChannel.
                    return -1;

                I2cConnected = true;
            }

            Thread.Sleep(10);
            if (i2cMaster.ChannelSetApi(ch) < 0)
                return -1; 

            if (ch == 0){
                if (i2cMaster.DisconnectApi() < 0)
                    return -1;

                cbConnect.Checked = false;
                I2cConnected = false;
            }

            return 0;
        }

        public int I2cMasterDisconnectApi()
        {
            int result = -1;

            if (this.InvokeRequired)
                this.Invoke(new Action(() => result = _I2cMasterDisconnect()));
            else
                result = _I2cMasterDisconnect();

            return result;
        }

        public int ChannelSwitchApi()
        {
            int result = -1;

            if (this.InvokeRequired)
                this.Invoke(new Action(() => result = _ChannelSwitch()));
            else
                result = _ChannelSwitch();

            return result;
        }

        public int ChannelSetApi(int ch)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _ChannelSet(ch)));
            else
                return _ChannelSet(ch);
        }
        /*
        private int _I2cReadForAll(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int i, rv;
            if (i2cMaster.connected == false) {
                if (_I2cMasterConnect(ProcessingChannel) < 0)
                    return -1;
            }

            if (SetQsfpMode && _SetQsfpMode(0x4D) < 0)
                return -1;

            if (writeToFile == false) {
                rv = i2cMaster.ReadApi(devAddr, regAddr, length, data);

                if (rv < 0) {
                    MessageBox.Show("TRx module no response!!");
                    _I2cMasterDisconnect();
                }
                else if (rv != length) {
                    //MessageBox.Show("Only read " + rv + " not " + length + " byte Error!!");
                    MessageBox.Show("Please confirm the module plug-in status");
                }

                return rv;
            }
            else {
                for (i = 0; i < length; i++) {
                    if (_AppendWriteToFile($"Write,0x{devAddr:X2},0x{regAddr:X2},0x{data[i]:X2}") < 0)
                        MessageBox.Show("_AppendWriteToFile() Error!!");
                }

                return 0;
            }
        }
        */
        private int _I2cReadIcConfig(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int i, rv;
            if (i2cMaster.connected == false) {
                if (_I2cMasterConnect(ProcessingChannel) < 0)
                    return -1;
            }
           
            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (writeToFile == false) {
                rv = i2cMaster.ReadApi(devAddr, regAddr, length, data);
                if (rv < 0) {
                    MessageBox.Show("TRx module no response!!");
                    _I2cMasterDisconnect();
                }
                else if (rv != length) {
                    //MessageBox.Show("Only read " + rv + " not " + length + " byte Error!!");
                    MessageBox.Show("Please confirm the module plug-in status\n_I2cReadIcConfig");
                    return -1;
                }

                return rv;
            }
            else {
                for (i = 0; i < length; i++) {
                    if (_AppendWriteToFile($"Write,0x{devAddr:X2},0x{regAddr:X2},0x{data[i]:X2}") < 0)
                        MessageBox.Show("_AppendWriteToFile() Error!!");
                }

                return 0;
            }
        }

        private int _I2cRead(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int i, rv;
            if (i2cMaster.connected == false) {
                if (_I2cMasterConnect(ProcessingChannel) < 0)
                    return -1;
            }

            if (writeToFile == false) {
                rv = i2cMaster.ReadApi(devAddr, regAddr, length, data);
                if (rv < 0) {
                    MessageBox.Show("TRx module no response!!");
                    _I2cMasterDisconnect();
                }
                else if (rv != length) {
                    MessageBox.Show("Please confirm the module plug-in status\n_I2cRead");
                    return -1;
                }

                return rv;
            }
            else {
                for (i = 0; i < length; i++) {
                    if (_AppendWriteToFile($"Write,0x{devAddr:X2},0x{regAddr:X2},0x{data[i]:X2}") < 0)
                        MessageBox.Show("_AppendWriteToFile() Error!!");
                }

                return 0;
            }
        }
        
        private int _I2cRead16(byte devAddr, byte[] regAddr, byte length, byte[] data)
        {
            int i, rv;
            if (i2cMaster.connected == false) {
                if (_I2cMasterConnect(ProcessingChannel) < 0)
                    return -1;
            }

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (writeToFile == false) {
                rv = i2cMaster.Read16Api(devAddr, regAddr, length, data);
                if (rv < 0) {
                    MessageBox.Show("TRx module no response!!");
                    _I2cMasterDisconnect();
                }
                else if (rv != length) {
                    //MessageBox.Show("Only read " + rv + " not " + length + " byte Error!!");
                    MessageBox.Show("Please confirm the module plug-in status\n_I2cRead16");
                    return -1;
                }

                return rv;
            }
            else {
                for (i = 0; i<length; i++) {
                    if (_AppendWriteToFile($"Write,0x{devAddr:X2},0x{regAddr:X2},0x{data[i]:X2}") < 0)
                        MessageBox.Show("_AppendWriteToFile() Error!!");
                }

                return 0;
            }
        }
        /*
        private int _I2cWriteForAll(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int i, rv;

            if (i2cMaster.connected == false) {
                if (_I2cMasterConnect(ProcessingChannel) < 0)
                    return -1;
            }

            if (SetQsfpMode && _SetQsfpMode(0x4D) < 0)
                return -1;

            if (writeToFile == false) {
                rv = i2cMaster.WriteApi(devAddr, regAddr, length, data);
                
                if (rv < 0) {
                    MessageBox.Show("TRx module no response!!");
                    _I2cMasterDisconnect();
                }

                return rv;
            }
            else {
                for (i = 0; i < length; i++) {
                    if (_AppendWriteToFile($"Write,0x{devAddr:X2},0x{regAddr:X2},0x{data[i]:X2}") < 0)
                        MessageBox.Show("_AppendWriteToFile() Error!!");
                }

                return 0;
            }
        }
        */
        private int _I2cWriteIcConfig(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int i, rv;

            if (i2cMaster.connected == false)
            {
                if (_I2cMasterConnect(ProcessingChannel) < 0)
                    return -1;
            }

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (writeToFile == false)
            {
                rv = i2cMaster.WriteApi(devAddr, regAddr, length, data);
                if (rv < 0)
                {
                    MessageBox.Show("TRx module no response!!");
                    _I2cMasterDisconnect();
                }

                return rv;
            }
            else
            {
                for (i = 0; i < length; i++)
                {
                    if (_AppendWriteToFile($"Write,0x{devAddr:X2},0x{regAddr:X2},0x{data[i]:X2}") < 0)
                        MessageBox.Show("_AppendWriteToFile() Error!!");
                }

                return 0;
            }
        }

        private int _I2cWrite(byte devAddr, byte regAddr, byte length, byte[] data)
        {
            int i, rv;

            if (i2cMaster.connected == false)
            {
                if (_I2cMasterConnect(ProcessingChannel) < 0)
                    return -1;
            }

            if (writeToFile == false)
            {
                rv = i2cMaster.WriteApi(devAddr, regAddr, length, data);
                if (rv < 0)
                {
                    MessageBox.Show("TRx module no response!!");
                    _I2cMasterDisconnect();
                }

                return rv;
            }
            else
            {
                for (i = 0; i < length; i++)
                {
                    if (_AppendWriteToFile($"Write,0x{devAddr:X2},0x{regAddr:X2},0x{data[i]:X2}") < 0)
                        MessageBox.Show("_AppendWriteToFile() Error!!");
                }

                return 0;
            }
        }
        
        private int _I2cWrite16(byte devAddr, byte[] regAddr, byte length, byte[] data)
        {
            int i, rv;

            if (i2cMaster.connected == false)
            {
                if (_I2cMasterConnect(ProcessingChannel) < 0)
                    return -1;
            }

            if (_SetQsfpMode(0x4D) < 0)
                return -1;

            if (writeToFile == false)
            {
                rv = i2cMaster.Write16Api(devAddr, regAddr, length, data);
                if (rv < 0)
                {
                    MessageBox.Show("TRx module no response!!");
                    _I2cMasterDisconnect();
                }

                return rv;
            }
            else
            {
                for (i = 0; i < length; i++)
                {
                    if (_AppendWriteToFile($"Write,0x{devAddr:X2},0x{regAddr:X2},0x{data[i]:X2}") < 0)
                        MessageBox.Show("_AppendWriteToFile() Error!!");
                }

                return 0;
            }
        } 
        private int _WriteModulePassword()
        {
            byte[] data = new byte[4];

            if (tbPasswordB0 == null || tbPasswordB1 == null ||
         tbPasswordB2 == null || tbPasswordB3 == null)
            {
                MessageBox.Show("Password input boxes not found!");
                return -1;
            }
            try
            {
                data[0] = Convert.ToByte(tbPasswordB0.Text, 16);
                data[1] = Convert.ToByte(tbPasswordB1.Text, 16);
                data[2] = Convert.ToByte(tbPasswordB2.Text, 16);
                data[3] = Convert.ToByte(tbPasswordB3.Text, 16);
            }
            catch
            {
                MessageBox.Show("Invalid Password Format! Please enter Hex values (e.g., 1A).");
                return -1;
            }

            if (i2cMaster.WriteApi(80, 123, 4, data) < 0)
                return -1;

            return 0;
        }
        
        private int _SetSerialNumber(string serialNumber)
        {
            int tmp;

            ucInformation.SetPasswordApi();
            _SetQsfpMode(0x4D);
            tmp = ucMemoryDump.SetSerialNumberApi(serialNumber);
            StoreIntoFlashApi();

            return tmp;
        }

        private int _GetPassword(int length, byte[] data)
        {
            if (length < 4 || data == null) return -1;

            if (string.IsNullOrEmpty(tbPasswordB0.Text) || string.IsNullOrEmpty(tbPasswordB1.Text) ||
                string.IsNullOrEmpty(tbPasswordB2.Text) || string.IsNullOrEmpty(tbPasswordB3.Text))
            {
                MessageBox.Show("Please input password (4 bytes Hex) before operate!!");
                return -1;
            }
            try
            {
                data[0] = Convert.ToByte(tbPasswordB0.Text, 16);
                data[1] = Convert.ToByte(tbPasswordB1.Text, 16);
                data[2] = Convert.ToByte(tbPasswordB2.Text, 16);
                data[3] = Convert.ToByte(tbPasswordB3.Text, 16);
                // string dataS = Encoding.Default.GetString(data); 
            }
            catch
            {
                MessageBox.Show("Invalid Password Format! Please enter valid Hex values (e.g., 1A).",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            return 4;
        }

        private void AppendRxRegisterContents(string modelType, string exportFilePath)
        {
            string IcRegisterContent;

            switch (modelType) {
                case "SAS4.0":
                    IcRegisterContent = ucMata37044cConfig.ReadAllRegisterApi();
                    File.AppendAllText(exportFilePath, IcRegisterContent);
                    break;

                case "PCIe4.0":
                    IcRegisterContent = ucRt145Config.ReadAllRegisterApi();
                    File.AppendAllText(exportFilePath, IcRegisterContent);
                    break;
            }
               
            return;
        }

        private void AppendTxRegisterContents(string modelType, string exportFilePath)
        {
            string vendorInfo;

            switch (modelType) {
                case "SAS4.0":
                    vendorInfo = ucMald37045cConfig.ReadAllRegisterApi();
                    File.AppendAllText(exportFilePath, vendorInfo);
                    break;

                case "PCIe4.0":
                    vendorInfo = ucRt146Config.ReadAllRegisterApi();
                    File.AppendAllText(exportFilePath, vendorInfo);
                    break;
            }

            return;
        }
        
        private int _WriteRegisterPage (string targetPage, int delayTime, string registerFilePath)
        {
            switch (targetPage) {
                case "Up 00h":
                case "Up 03h":
                case "80h":
                case "81h":
               
                    if (ucMemoryDump.WriteRegisterPageApi(targetPage, delayTime, registerFilePath) < 0)
                        return -1;
                    
                    break;

                case "Low Page":
                    if (ucMemoryDump.WriteLowPagePartRegisterApi(delayTime, registerFilePath) < 0)
                        return -1;
                    break;

                case "Tx":
                    if (ucMald37045cConfig.WriteAllRegisterApi("Tx", delayTime, registerFilePath) < 0)
                        return -1;
                    
                    break;

                case "Rx":
                    if (ucMata37044cConfig.WriteAllRegisterApi("Rx", delayTime, registerFilePath) < 0)
                        return -1;
                    
                    break;

                default:
                    break;
            }
            return 0;
        }

        private int _WriteRegisterPageForSas3(string targetPage, int delayTime, byte startAddr, int numberOfBytes, string registerFilePath, int processingChannel)
        {
            if (numberOfBytes <= 0 || numberOfBytes > 128) {
                MessageBox.Show("Invalid number of bytes. Must be between 1 and 128.");
                return -1;
            }

            switch (targetPage) {
                
                case "Page 00":
                case "Page 03":
                case "Page 3A":
                case "Page 5D":
                case "Page 6C":
                case "Page 70":
                case "Page 73":
                    if (processingChannel == 2)
                        targetPage = "B_" + targetPage;
                    else
                        targetPage = "A_" + targetPage;

                    if (ucMemoryDump.WriteRegisterPageForSas3Api(targetPage, delayTime, startAddr, numberOfBytes, registerFilePath) < 0)
                        return -1;

                    break;

                default:
                    MessageBox.Show("Exceeds page range!!");
                    break;
            }
            return 0;
        }

        private int _ExportModuleCfg(string modelType, string fileName, string comparisonObject)
        {
            string executableFileFolderPath = Application.StartupPath;
            string exportFilePath;
            string folderPath;
            string tempUcMemoryFile;

            fileName = fileName.Replace(" ", "") + ".csv";
            folderPath = Path.Combine(executableFileFolderPath, "RegisterFiles");
            exportFilePath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (File.Exists(exportFilePath))
                File.Delete(exportFilePath);

            tempUcMemoryFile = Path.Combine(folderPath, "temp_" + fileName);

            if (ucMemoryDump.ExportAllPagesDataApi(tempUcMemoryFile) < 0)
                return -1;

            AppendFileContentToAnother(tempUcMemoryFile, exportFilePath);
            
            if (comparisonObject == "LogFile") {
                AppendRxRegisterContents(modelType, exportFilePath);
                AppendTxRegisterContents(modelType, exportFilePath);
            }

            if (File.Exists(tempUcMemoryFile))
                File.Delete(tempUcMemoryFile);

            return 0;
        }

        private int _ExportModuleCfgForSas3(string fileName, string comparisonObject, int ch)
        {
            string executableFileFolderPath = Application.StartupPath;
            string exportFilePath;
            string folderPath;
            string tempUcMemoryFile;

            fileName = fileName.Replace(" ", "") + ".csv";
            folderPath = Path.Combine(executableFileFolderPath, "RegisterFiles");
            exportFilePath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (File.Exists(exportFilePath))
                File.Delete(exportFilePath);

            tempUcMemoryFile = Path.Combine(folderPath, "temp_" + fileName);

            if (ucMemoryDump.ExportAllPagesDataForSas3Api(tempUcMemoryFile, ch) < 0)
                return -1;

            AppendFileContentToAnother(tempUcMemoryFile, exportFilePath);

            if (File.Exists(tempUcMemoryFile))
                File.Delete(tempUcMemoryFile);

            return 0;
        }

        private int _ExportLogfile(string modelType, string fileName, bool logFileMode, bool writeSnMode)
        {
            string folderPath;
            string exportFilePath;
            string tempExportFilePath;

            if (!writeSnMode)
                StateUpdated("Read State:\nPreparing resources...", 3);

            if (logFileMode) {
                if (fileName == "ModuleRegisterFile") {
                    folderPath = Path.Combine(Application.StartupPath, "LogFolder");
                }
                else if (fileName == "ReWriteRegister") {
                    folderPath = Path.Combine(Application.StartupPath, "RegisterFiles");
                }
                else {
                    MainFormPaths lastPath = _LoadLastPaths();
                    folderPath = lastPath.LogFilePath;

                    if (string.IsNullOrEmpty(folderPath))
                        folderPath = Path.Combine(Application.StartupPath, "LogFolder");
                }
            }
            else {
                folderPath = lastUsedDirectory;
            }

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            fileName = fileName.Replace(" ", "") + ".csv";
            exportFilePath = Path.Combine(folderPath, fileName);
            tempExportFilePath = Path.Combine(folderPath, "temp_" + fileName);

            if (File.Exists(exportFilePath)) {
                if (fileName == "ModuleRegisterFile.csv") {
                    File.Delete(exportFilePath);
                }
                else {
                    DialogResult result = MessageBox.Show($"File {fileName} already exists." +
                                                      $"\nDo you want to overwrite it?","File Exists", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Yes) {
                        File.Delete(exportFilePath);
                    }
                    else {
                        MessageBox.Show("Operation cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }
                }
            }

            //MessageBox.Show("1: \n" + ucInformation.GetVendorInfo() +
            //                "exportFilePath: \n" + exportFilePath);

            AppendVendorInfo(exportFilePath);
            AppendMoreInfo(exportFilePath);

            if (!writeSnMode)
                StateUpdated("Read State:\nInformation...Done", 5);

            if (ucMemoryDump.ExportAllPagesDataApi(tempExportFilePath) < 0)
                return -1;

            AppendFileContentToAnother(tempExportFilePath, exportFilePath);

            if (!writeSnMode)
                StateUpdated("Read State:\nUpPage 00h/03h...Done", 10);
           
            AppendRxRegisterContents(modelType, exportFilePath);

            if (!writeSnMode)
                StateUpdated("Read State:\nRxIcConfig...Done", 20);
            
            AppendTxRegisterContents(modelType, exportFilePath);

            if (!writeSnMode)
                StateUpdated("Read State:\nTxIcConfig...Done", 30);
           
            if (File.Exists(tempExportFilePath))
                File.Delete(tempExportFilePath);

            //MessageBox.Show("9: \n" + ucInformation.GetVendorInfo() + 
            //                "exportFilePath: \n" + exportFilePath);

            return 0;
        }

        private int _ExportLogfileForSas3(string fileName, bool logFileMode, bool writeSnMode, int processingChannel)
        {
            string folderPath;
            string exportFilePath;
            string tempExportFilePath;

            if (!writeSnMode)
                StateUpdated("Read State:\nPreparing resources...", 3);

            if (logFileMode) {
                MainFormPaths lastPath = _LoadLastPaths();
                folderPath = lastPath.LogFilePath;

                if (string.IsNullOrEmpty(folderPath))
                    folderPath = Path.Combine(Application.StartupPath, "LogFolder");
            }
            else {
                
                folderPath = lastUsedDirectory;
            }

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            fileName = fileName.Replace(" ", "") + ".csv";
            exportFilePath = Path.Combine(folderPath, fileName);
            tempExportFilePath = Path.Combine(folderPath, "temp_" + fileName);

            if (File.Exists(exportFilePath)) {
                if (fileName == "ModuleRegisterFile.csv") {
                    File.Delete(exportFilePath);
                }
                else {
                    DialogResult result = MessageBox.Show($"File {fileName} already exists." +
                                                      $"\nDo you want to overwrite it?","File Exists",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Yes) {
                        File.Delete(exportFilePath);
                    }
                    else {
                        MessageBox.Show("Operation cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }
                }
            }

            //MessageBox.Show("1: \n" + ucInformation.GetVendorInfo() +
            //                "exportFilePath: \n" + exportFilePath);

            AppendVendorInfo(exportFilePath);
            AppendMoreInfo(exportFilePath);

            if (!writeSnMode)
                StateUpdated("Read State:\nInformation...Done", 5);

            //輸出 DUT Register map from MCU all page
            if (ucMemoryDump.ExportAllPagesDataForSas3Api(tempExportFilePath, processingChannel) < 0)
                return -1;

            AppendFileContentToAnother(tempExportFilePath, exportFilePath);

            if (!writeSnMode)
                StateUpdated("Read State:\nAllPage 00h,03h~7F...Done", 10);
                        

            if (File.Exists(tempExportFilePath))
                File.Delete(tempExportFilePath);

            //MessageBox.Show("9: \n" + ucInformation.GetVendorInfo() + 
            //                "exportFilePath: \n" + exportFilePath);

            return 0;
        }

        //_FormatChangeFromTextToCsv
        private int _FormatChangeFromTextToCsv(string exportFilePath, string ASideFilePath, string BSideFilePath)
        {
            DataTable dtAllPages = new DataTable();
            string[] pages = new string[]
            {
                "Lower", "Page 00", "Page 03", "Page 3A", "Page 5D", "Page 6C",
                "Page 70", "Page 73", "Page 7B", "Page 7E", "Page 7F"
            };

            // 初始化 DataTable 欄位（Page, Row 和 16個byte對應的欄位）
            dtAllPages.Columns.Add("Page", typeof(string));
            dtAllPages.Columns.Add("Row", typeof(string));
            for (int col = 0; col < 16; col++) {
                dtAllPages.Columns.Add(col.ToString("X2"), typeof(string));
            }

            if (!string.IsNullOrEmpty(ASideFilePath) && _ReadAndProcessFile(ASideFilePath, pages, dtAllPages, "A") < 0)
                return -1;

            if (!string.IsNullOrEmpty(BSideFilePath) && _ReadAndProcessFile(BSideFilePath, pages, dtAllPages, "B") < 0)
                return -1;

            ExportDataTableToCsv(dtAllPages, exportFilePath);
            return 0;
        }

        // Read and process the data, then add the data to dtAllPages.
        private int _ReadAndProcessFile(string filePath, string[] pages, DataTable dtAllPages, string sideLabel)
        {
            foreach (string page in pages) {
                if (_ReadFromTextFile(page, filePath) < 0)
                    return -1;

                DataTable dtCurrentPage = dtMemory.Copy(); 

                for (int rowIdx = 0; rowIdx < 8; rowIdx++) // 8 rows by page
                {
                    DataRow newRow = dtAllPages.NewRow();
                    newRow["Page"] = $"{sideLabel}_{page}"; // Marker A or B side
                    newRow["Row"] = (rowIdx * 16).ToString("X2"); // Row number

                    for (int colIdx = 0; colIdx < 16; colIdx++) {
                        newRow[colIdx.ToString("X2")] = dtCurrentPage.Rows[rowIdx][colIdx].ToString(); // Fill in the corresponding byte data
                    }
                    dtAllPages.Rows.Add(newRow);
                }
            }
            return 0;
        }

        // 假設 _ReadFromTextFile() 會讀取一個頁面的資料並將其儲存到 dtMemory
        private int _ReadFromTextFile(string page, string filePath)
        {
            try {
                using (StreamReader sr = new StreamReader(filePath)) {
                    string line;
                    bool isPageFound = false;
                    dtMemory = new DataTable();

                    // 初始化 dtMemory 的欄位
                    for (int col = 0; col < 16; col++) {
                        dtMemory.Columns.Add(col.ToString("X2"), typeof(string));
                    }

                    while ((line = sr.ReadLine()) != null) {
                        // 偵測到頁面標籤
                        if (line.StartsWith(page)) {
                            isPageFound = true;
                            continue;
                        }

                        // 當找到頁面後，開始讀取資料行
                        if (isPageFound && !string.IsNullOrWhiteSpace(line)) {
                            string[] data = line.Split(' ');

                            // 檢查資料是否有16個byte
                            if (data.Length != 16) {
                                throw new Exception($"Expected 16 bytes, but found {data.Length} bytes in the line.");
                            }

                            // 新增資料行至 dtMemory
                            DataRow newRow = dtMemory.NewRow();
                            for (int i = 0; i < 16; i++) {
                                newRow[i.ToString("X2")] = data[i];
                            }
                            dtMemory.Rows.Add(newRow);

                            // 讀滿8行資料後結束
                            if (dtMemory.Rows.Count == 8) {
                                break;
                            }
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex) {
                MessageBox.Show($"Error reading file: {ex.Message}");
                return -1;
            }
        }

        private void ExportDataTableToCsv(DataTable dt, string filePath)
        {
            StringBuilder sb = new StringBuilder();
            string directoryPath = Path.GetDirectoryName(filePath);

            // 欄位名稱加入CSV
            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames.ToArray()));

            // 每一行資料加入CSV
            foreach (DataRow row in dt.Rows) {
                IEnumerable<string> fields = row.ItemArray.Select(field => "\"" + field.ToString().Replace("\"", "\"\"") + "\"");
                sb.AppendLine(string.Join(",", fields.ToArray()));
            }

            File.WriteAllText(filePath, sb.ToString());
        }


        private void AppendVendorInfo(string exportFilePath)
        {
            string vendorInfo = ucInformation.GetVendorInfo();
            File.AppendAllText(exportFilePath, vendorInfo);
        }

        private void AppendMoreInfo(string exportFilePath)
        {
            string additionalVendorInfo = ucDigitalDiagnosticsMonitoring.GetMoreInfo();
            File.AppendAllText(exportFilePath, additionalVendorInfo + Environment.NewLine);
        }

        private void AppendFileContentToAnother(string sourceFilePath, string destinationFilePath)
        {
            var lines = File.ReadAllLines(sourceFilePath);
            File.AppendAllLines(destinationFilePath, lines);
        }

        private void _SetToChannel2(bool mode) 
        {
            ucNuvotonIcpTool.SetVarBoolState("SetToChannel2", mode);
        }

        private void _SetAutoReconnectControl(bool ControlState)
        {
            ucNuvotonIcpTool.SetVarBoolState("AutoReconnectMode", ControlState);
            cbAutoReconnect.Checked = ControlState;
        }

        private bool _GetAutoReconnectControl()
        {
            return ucNuvotonIcpTool.GetVarBoolState("AutoReconnectMode");
        }

        private void _SetBypassEraseAllControl(bool ControlState)
        {
            ucNuvotonIcpTool.SetVarBoolState("BypassEraseAllCheckMode", ControlState);
            cbBypassEraseAllCheck.Checked = ControlState;
        }

        private bool _GetBypassEraseAllControl()
        {
            return ucNuvotonIcpTool.GetVarBoolState("BypassEraseAllCheckMode");
        }

        private void UcNuvotonIcpControl_RequestI2cOperation(object sender, I2cOperationEventArgs e)
        {
            switch (e.OperationType)
            {
                case I2cOperationType.Connect:
                    _I2cMasterConnect(ProcessingChannel);
                    break;
                case I2cOperationType.Disconnect:
                    _I2cMasterDisconnect();
                    break;
                case I2cOperationType.SetChannel:
                    _ChannelSet(e.Channel);
                    _SetAutoReconnectControl(false);
                    break;
            }
        }

        private void UcNuvotonIcpTool_MessageUpdated(object sender, MessageEventArgs e)
        {
            // UC-B data updated，觸發UC-A的MainMessageUpdated event，將Message傳遞給MainForm-UI
            MainMessageUpdated?.Invoke(this, e);
        }
        private void ucDigitalDiagnosticsMonitoring_TextBoxTextChanged(object sender, TextBoxTextEventArgs e)
        {
            TextBoxTextChanged?.Invoke(this, e);
        }

        private void HandlePluginWaiting(bool isWaiting)
        {
            OnPluginWaiting?.Invoke(isWaiting);
        }

        private void HandlePluginDetected(bool isDetected) // 當DUT插入檢測成功時的處理程序
        {
            OnPluginDetected?.Invoke(isDetected);
        }

        public EngineerForm(bool visible)
        {
            InitializeComponent();
            SetupControlEvents(); // For IcpTool Button.text update

            if (!visible)
            {
                this.ShowInTaskbar = false;
            }

            this.FormClosing += new FormClosingEventHandler(_MainForm_FormClosing);
            ucNuvotonIcpTool.MessageUpdated += UcNuvotonIcpTool_MessageUpdated;
            ucNuvotonIcpTool.RequestI2cOperation += UcNuvotonIcpControl_RequestI2cOperation;
            ucNuvotonIcpTool.OnPluginWaiting += HandlePluginWaiting;
            ucNuvotonIcpTool.OnPluginDetected += HandlePluginDetected;
            ucDigitalDiagnosticsMonitoring.TextBoxTextChanged += ucDigitalDiagnosticsMonitoring_TextBoxTextChanged;
            this.Size = new System.Drawing.Size(1170, 870);
            _InitialStateBar();
            //_EnableIcConfig();
            //_UpdateTabPageVisibility();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            cbProductSelect.SelectedIndex = 0;

            dtWriteConfig.Columns.Add("Command", typeof(String));
            dtWriteConfig.Columns.Add("DevAddr", typeof(String));
            dtWriteConfig.Columns.Add("RegAddr", typeof(String));
            dtWriteConfig.Columns.Add("Value", typeof(String));


            /*
            bool tmp = ucNuvotonIcpTool.GetVarBoolState("PublicVariable");
            ucNuvotonIcpTool.SetVarBoolState("PublicVariable", true);
            MessageBox.Show("ucNuvotonIcp_PublicVariable bool: "
                            + "\nBefore: " + tmp
                            + "\nAfter: " + ucNuvotonIcpTool.GetVarBoolState("PublicVariable")
                            );
            */

            if (ucInformation.SetI2cReadCBApi(_I2cRead) < 0)
            {
                MessageBox.Show("ucInformation.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucInformation.SetI2cWriteCBApi(_I2cWrite) < 0)
            {
                MessageBox.Show("ucInformation.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucInformation.SetGetPasswordCBApi(_GetPassword) < 0)
            {
                MessageBox.Show("ucInformation.SetGetPasswordCBApi() faile Error!!");
                return;
            }
            if (ucDigitalDiagnosticsMonitoring.SetI2cReadCBApi(_I2cRead) < 0)
            {
                MessageBox.Show("ucDigitalDiagnosticsMonitoring.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucDigitalDiagnosticsMonitoring.SetI2cWriteCBApi(_I2cWrite) < 0)
            {
                MessageBox.Show("ucDigitalDiagnosticsMonitoring.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucDigitalDiagnosticsMonitoring.SetWritePasswordCBApi(ucInformation.SetPasswordApi) < 0)
            {
                MessageBox.Show("ucDigitalDiagnosticsMonitoring.SetWritePasswordCBApi() faile Error!!");
                return;
            }
            if (ucMemoryDump.SetI2cReadCBApi(_I2cRead) < 0)
            {
                MessageBox.Show("ucMemoryDump.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucMemoryDump.SetI2cWriteCBApi(_I2cWrite) < 0)
            {
                MessageBox.Show("ucMemoryDump.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucMemoryDump.SetWritePasswordCBApi(ucInformation.SetPasswordApi) < 0)
            {
                MessageBox.Show("ucMemoryDump.SetWritePasswordCBApi() faile Error!!");
                return;
            }

            if (ucGn1190Corrector.SetQsfpI2cReadCBApi(_I2cRead) < 0)
            {
                MessageBox.Show("ucQsfpCorrector.SetQsfpI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucGn1190Corrector.SetQsfpI2cWriteCBApi(_I2cWrite) < 0)
            {
                MessageBox.Show("ucQsfpCorrector.SetQsfpI2cWriteCBApi() faile Error!!");
                return;
            }

            if (ucMald37045cConfig.SetI2cReadCBApi(_I2cReadIcConfig) < 0)
            {
                MessageBox.Show("ucMald37045cConfig.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucMald37045cConfig.SetI2cWriteCBApi(_I2cWriteIcConfig) < 0)
            {
                MessageBox.Show("ucMald37045cConfig.SetI2cWriteCBApi() faile Error!!");
                return;
            }
            if (ucMata37044cConfig.SetI2cReadCBApi(_I2cReadIcConfig) < 0)
            {
                MessageBox.Show("ucMata37044cConfig.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucMata37044cConfig.SetI2cWriteCBApi(_I2cWriteIcConfig) < 0)
            {
                MessageBox.Show("ucMata37044cConfig.SetI2cWriteCBApi() faile Error!!");
                return;
            }
            if (ucRt145Config.SetI2cReadCBApi(_I2cReadIcConfig) < 0)
            {
                MessageBox.Show("ucRt145Config.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucRt145Config.SetI2cWriteCBApi(_I2cWriteIcConfig) < 0)
            {
                MessageBox.Show("ucRt145Config.SetI2cWriteCBApi() faile Error!!");
                return;
            }
            if (ucRt146Config.SetI2cReadCBApi(_I2cReadIcConfig) < 0)
            {
                MessageBox.Show("ucRt145Config.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucRt146Config.SetI2cWriteCBApi(_I2cWriteIcConfig) < 0)
            {
                MessageBox.Show("ucRt145Config.SetI2cWriteCBApi() faile Error!!");
                return;
            }

            if (ucGn2108Config.SetI2cReadCBApi(_I2cReadIcConfig) < 0)
            {
                MessageBox.Show("ucGn1190Config.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucGn2108Config.SetI2cWriteCBApi(_I2cWriteIcConfig) < 0)
            {
                MessageBox.Show("ucGn1190Config.SetI2cWriteCBApi() faile Error!!");
                return;
            }
            if (ucGn2109Config.SetI2cReadCBApi(_I2cReadIcConfig) < 0) {
                MessageBox.Show("ucGn1190Config.SetI2cReadCBApi() faile Error!!");
                return;
            }
            if (ucGn2109Config.SetI2cWriteCBApi(_I2cWriteIcConfig) < 0) {
                MessageBox.Show("ucGn1190Config.SetI2cWriteCBApi() faile Error!!");
                return;
            }


            if (ucGn2108Config.SetI2cRead16CBApi(_I2cRead16) < 0)
            {
                MessageBox.Show("ucGn1190Config.SetI2cRead16CBApi() faile Error!!");
                return;
            }
            if (ucGn2108Config.SetI2cWrite16CBApi(_I2cWrite16) < 0)
            {
                MessageBox.Show("ucGn1190Config.SetI2cWrite16CBApi() faile Error!!");
                return;
            }

            if (ucGn2109Config.SetI2cRead16CBApi(_I2cRead16) < 0)
            {
                MessageBox.Show("ucGn1190Config.SetI2cRead16CBApi() faile Error!!");
                return;
            }
            if (ucGn2109Config.SetI2cWrite16CBApi(_I2cWrite16) < 0)
            {
                MessageBox.Show("ucGn1190Config.SetI2cWrite16CBApi() faile Error!!");
                return;
            }
        }

        public void SetPermissions(string permissions)
        {
            
            cbPermission.SelectedItem = permissions;

            if (permissions == "QC")
            {
                _EnableQCMode(); // Enable QC mode for QC users
            }
            else
            {
                    //_DisableQCMode(); // Disable QC mode for non-QC users
            }
        }

        public void SetProduct(string product)
        {
            cbProductSelect.SelectedItem = product;
        }

        private int _SetWriteConfig()
        {
            byte[] data = new byte[1];
            byte devAddr, regAddr;
            int currentRow = 0;

            progressBar1.Value = 0;

            // 1. Count lines
            int totalLines = File.ReadLines(fileName).Count();
            int lastPercent = 0;

            using (StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    currentRow++;
                    string line = sr.ReadLine();

                    // UI Update (Optimized)
                    int currentPercent = (int)((double)currentRow / totalLines * 100);
                    if (currentPercent > lastPercent)
                    {
                        lastPercent = currentPercent;
                        progressBar1.Value = currentPercent;
                        Application.DoEvents();
                    }

                    if (line.StartsWith("//") || string.IsNullOrWhiteSpace(line)) continue;

                    string[] tokens = line.Split(',');
                    if (tokens.Length < 4) { MessageBox.Show("Format Error: " + line); return -1; }

                    try
                    {
                        // Parse Hex values (e.g. 0xA0)
                        devAddr = byte.Parse(tokens[1].Substring(2), System.Globalization.NumberStyles.HexNumber);
                        regAddr = byte.Parse(tokens[2].Substring(2), System.Globalization.NumberStyles.HexNumber);
                        data[0] = byte.Parse(tokens[3].Substring(2), System.Globalization.NumberStyles.HexNumber);
                    }
                    catch { MessageBox.Show("Hex Format Error: " + line); return -1; }

                    string command = tokens[0];

                    switch (command)
                    {
                        case "Write":
                            // [FIXED] Use your local helper method!
                            // It requires 4 arguments: (Addr, Reg, Length, Data)
                            int writeResult = _I2cWriteIcConfig(devAddr, regAddr, 1, data);
                            if (writeResult < 0) return -1;
                            break;

                        case "Read":
                            int retry = 0;
                            // [FIXED] Use i2cMaster directly for reading
                            // Assuming 'i2cMaster' is available since _I2cWriteIcConfig uses it.
                            // If 'ReadApi' is incorrect, try 'I2cReadApi' or 'Read'.
                            while (i2cMaster.ReadApi(devAddr, regAddr, 1, data) != 1)
                            {
                                if (retry++ > 10)
                                {
                                    MessageBox.Show("Read Timeout");
                                    return -1;
                                }
                                Thread.Sleep(10);
                            }

                            byte expected = byte.Parse(tokens[4].Substring(2), System.Globalization.NumberStyles.HexNumber);
                            if (data[0] != expected)
                            {
                                MessageBox.Show($"Verify Fail! Reg:0x{regAddr:X2} Val:0x{data[0]:X2} Expected:{tokens[4]}");
                                return -1;
                            }
                            break;

                        default:
                            // Delay is handled in CSV sometimes, or just ignore unknown commands
                            if (command.StartsWith("Delay")) Thread.Sleep(10);
                            break;
                    }
                }
            }
            progressBar1.Value = 100;
            return 0;
        }

        private void _StoreGlobalWriteCommandtoFile()
        {
            SaveFileDialog sfdSelectFile = new SaveFileDialog();

            sAcConfig = "//Write,DevAddr,RegAddr,Value\n" + "//Read,DevAddr,RegAddr,Value\n" +
                "//Delay10mSec,Time\n";

            ucInformation.WriteAllApi();
            ucDigitalDiagnosticsMonitoring.WriteAllApi();
            ucMemoryDump.WriteAllApi();

            sfdSelectFile.Filter = "cfg files (*.cfg)|*.cfg";
            if (sfdSelectFile.ShowDialog() != DialogResult.OK)
                return;

            System.IO.File.WriteAllText(sfdSelectFile.FileName, sAcConfig);
        }

        private void _InitialStateBar()
        {
            tbInformationReadState.BackColor= Color.White;
            tbDdmReadState.BackColor = Color.White;
            tbMemDumpReadState.BackColor = Color.White;
            tbCorrectorReadState.BackColor = Color.White;
            tbTxConfigReadState.BackColor = Color.White;
            tbRxConfigReadState.BackColor = Color.White;
        }

        private void _InitialForSas3()
        {

        }

        private void _DisableButtons()
        {
            cbConnect.Enabled = false;
            bGlobalRead.Enabled = false;
            bInnerSwitch.Enabled = false;
            bOutterSwitch.Enabled = false;
            bGlobalWrite.Enabled = false;
            bStoreIntoFlash.Enabled = false;
            bIcpConnect.Enabled = false;
            ucNuvotonIcpTool.SetButtonEnable("bLink" , false);
            /*
            tcDdmi.Enabled = false;
            tcIcConfig.Enabled = false;
            ucGn1190Corrector.DisableButtonApi();
            */
            bLoadAllFromCfgFile.Enabled = false;
            bGenerateCfg.Enabled = false;
            //bFunctionTest2.Enabled = false;
            bSaveAllToCfgFile.Enabled = false;
            cbInfomation.Enabled = false;
            cbDdm.Enabled = false;
            cbMemDump.Enabled = false;
            cbCorrector.Enabled = false;
            cbTxIcConfig.Enabled = false;
            cbRxIcConfig.Enabled = false;
            cbAPPath.Enabled = false;
            cbDAPath.Enabled = false;
            rbCustomerMode.Enabled = false;
            rbMpMode.Enabled = false;
            bBackToMainForm.Enabled = false;
        }

        private void _EnableButtons()
        {
            cbConnect.Enabled = true;
            bGlobalRead.Enabled = true;
            bInnerSwitch.Enabled = true;
            bOutterSwitch.Enabled = true;
            /*
            tcDdmi.Enabled = true;
            tcIcConfig.Enabled = true;
            ucGn1190Corrector.EnableButtonApi();
            */
            bIcpConnect.Enabled = true;
            ucNuvotonIcpTool.SetButtonEnable("bLink", true);
            bLoadAllFromCfgFile.Enabled = true;
            //bFunctionTest2.Enabled = true;
            bSaveAllToCfgFile.Enabled = true;
            cbInfomation.Enabled = true;
            cbDdm.Enabled = true;
            cbMemDump.Enabled = true;
            cbCorrector.Enabled = true;
            cbTxIcConfig.Enabled = true;
            cbRxIcConfig.Enabled = true;
            cbAPPath.Enabled = true;
            cbDAPath.Enabled = true;
            rbCustomerMode.Enabled = true;
            rbMpMode.Enabled = true;
            bBackToMainForm.Enabled = true;

            //_GenerateCfgButtonState(1);
            //_GenerateCfgButtonState(2);
            if (_isQCMode)
            {
                _EnableQCMode();
            }
            else if (FirstRead)
            {
                bGlobalWrite.Enabled = true;
                bStoreIntoFlash.Enabled = true;
            }
            else
            {
                bGlobalWrite.Enabled = false;
                bStoreIntoFlash.Enabled = false;
            }
        }

        private void _GenerateXmlFileFromUcComponents()
        {
            string folderPath = Application.StartupPath;
            string combinedPath = Path.Combine(folderPath, "XmlFolder");
            string xmlFilePath = Path.Combine(combinedPath, "Permission settings file.xml");
            xmlFilePath = xmlFilePath.Replace("\\\\", "\\");

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("Settings");
            xmlDoc.AppendChild(root);
            XmlElement permissionsNode = xmlDoc.CreateElement("Permissions");
            root.AppendChild(permissionsNode);
            string[] roles = { "Administrator", "Engineer", "MP Manager" };

            foreach (string role in roles)
            {
                XmlElement permissionNode = xmlDoc.CreateElement("Permission");
                permissionNode.SetAttribute("role", role);
                permissionsNode.AppendChild(permissionNode);
                                
                List<UserControl> userControls = GetAllUserControls(this); // get MainForm all UserControl

                foreach (UserControl userControl in userControls)
                {
                    XmlElement userControlNode = xmlDoc.CreateElement("UserControl");
                    userControlNode.SetAttribute("name", userControl.Name);

                    /*
                    // 指定 UserControl ...所有 Components enabled = false
                    if (userControl.Name == "ucMald37045cConfig" ||
                        userControl.Name == "ucMata37044cConfig" ||
                        userControl.Name == "ucRt146Config" ||
                        userControl.Name == "ucRt145Config" ||
                        userControl.Name == "ucGn2108Config" ||
                        userControl.Name == "ucGn2109Config")
                    {
                        SetAllComponentsEnabled(userControl, false);
                    }
                    */

                    permissionNode.AppendChild(userControlNode);

                    // Add MainForm components
                    XmlElement mainFormNode = xmlDoc.CreateElement("MainForm");
                    permissionNode.AppendChild(mainFormNode);

                    List<Control> mainFormComponents = GetAllControls(this); // 搜尋 MainForm 所有元件
                    mainFormComponents.Sort(new ControlComparer());

                    XmlElement mainFormComponentsNode = xmlDoc.CreateElement("Components");

                    foreach (Control control in mainFormComponents) {
                        XmlElement componentNode = xmlDoc.CreateElement("Component");

                        componentNode.SetAttribute("name", control.Name);
                        componentNode.SetAttribute("object", control.GetType().Name);
                        componentNode.SetAttribute("visible", "True");

                        if (control.Name.Contains("Write") || control.Name.Contains("Flash")) {
                            componentNode.SetAttribute("enabled", "False");
                        }
                        else if (control.Name.Contains("tp")) {
                            componentNode.SetAttribute("enabled", "True");
                        }
                        else {
                            componentNode.SetAttribute("enabled", control.Enabled.ToString());
                        }

                        if (control is TextBox textBox) {
                            componentNode.SetAttribute("ReadOnly", textBox.ReadOnly.ToString());
                        }

                        mainFormComponentsNode.AppendChild(componentNode);
                    }

                    mainFormNode.AppendChild(mainFormComponentsNode);

                    // Add UserControl components
                    List<Control> userControlComponents = GetAllControls(userControl); // search all components from UserControl and set xml node
                    userControlComponents.Sort(new ControlComparer());

                    XmlElement componentsNode = xmlDoc.CreateElement("Components");

                    foreach (Control control in userControlComponents)
                    {
                        XmlElement componentNode = xmlDoc.CreateElement("Component");
                        componentNode.SetAttribute("name", control.Name);
                        componentNode.SetAttribute("object", control.GetType().Name);
                        componentNode.SetAttribute("visible", "True");
                        //componentNode.SetAttribute("enabled", control.Enabled.ToString());

                        if (control.Name.Contains("Write") || control.Name.Contains("Flash"))
                        {
                            componentNode.SetAttribute("enabled", "False");
                        }
                        // 新增條件：如果 Component name 含有 "tp" 字串，則 enabled 設為 true
                        else if (control.Name.Contains("tp"))
                        {
                            componentNode.SetAttribute("enabled", "True");
                        }
                        else
                        {
                            componentNode.SetAttribute("enabled", control.Enabled.ToString());
                        }


                        if (control is TextBox textBox)
                        {
                            componentNode.SetAttribute("ReadOnly", textBox.ReadOnly.ToString());
                        }

                        componentsNode.AppendChild(componentNode);
                    }

                    userControlNode.AppendChild(componentsNode);
                }
            }

            xmlDoc.Save(xmlFilePath);
        }
        
        private void _GenerateXmlFileForProject()
        {
            string modelType = cbProductSelect.Text;
            string logFileName = "RegisterFile";
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("Project");
            xmlDoc.AppendChild(root);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Zip Files|*.zip";
            saveFileDialog.Title = "Save Zip File";

            string targetApromPath = APROMPath;
            string targetDataromPath = DATAROMPath;

            XmlElement permissionsNode = xmlDoc.CreateElement("Premissions");

            if (rbCustomerMode.Checked) {
                permissionsNode.SetAttribute("role", "Customer");
            }

            else if (rbCustomerCheckMode.Checked)
            {
                permissionsNode.SetAttribute("role", "Customer_Check");
            }

            else if (rbMpMode.Checked) {
                permissionsNode.SetAttribute("role", "MP");
            }

            root.AppendChild(permissionsNode);

            // Check product selected
            if (cbProductSelect.SelectedItem != null) {
                XmlElement productNode = xmlDoc.CreateElement("Product");
                productNode.SetAttribute("name", cbProductSelect.SelectedItem.ToString());
                permissionsNode.AppendChild(productNode);
            }
            else {
                MessageBox.Show("Please select a product.");
                return;
            }

            // Check APROM, DATAROM filepath
            if (!string.IsNullOrWhiteSpace(APROMPath) || !string.IsNullOrWhiteSpace(DATAROMPath)) {
                XmlElement APROMNode = xmlDoc.CreateElement("APROM");
                APROMNode.SetAttribute("name", Path.GetFileName(APROMPath));
                permissionsNode.AppendChild(APROMNode);

                XmlElement DATAROMNode = xmlDoc.CreateElement("DATAROM");
                DATAROMNode.SetAttribute("name", Path.GetFileName(DATAROMPath));
                permissionsNode.AppendChild(DATAROMNode);
            }
            else {
                MessageBox.Show("APROM or DATAROM path is not set.");
                return;
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                loadingForm.Show(this);
                Application.DoEvents();

                string selectedFileName = saveFileDialog.FileName;
                string folderName = Path.GetFileNameWithoutExtension(selectedFileName);
                string folderPath = Path.Combine(Path.GetDirectoryName(selectedFileName), folderName);

                Directory.CreateDirectory(folderPath);

                // Save the XML file as Cfg.xml
                xmlDoc.Save(Path.Combine(folderPath, "Cfg.xml"));

                lastUsedDirectory = folderPath;
                _GlobalWriteFromUi(false);
                _InitialStateBar();
                _ExportLogfile(modelType, logFileName, false, false); // Export CfgFile to config folder

                if (!string.IsNullOrWhiteSpace(targetApromPath)) {
                    string destinationFilePath = Path.Combine(folderPath, Path.GetFileName(APROMPath));
                    File.Copy(targetApromPath, destinationFilePath, true);
                }

                if (!string.IsNullOrWhiteSpace(targetDataromPath)) {
                    string destinationFilePath = Path.Combine(folderPath, Path.GetFileName(DATAROMPath));
                    File.Copy(targetDataromPath, destinationFilePath, true);
                }

                CompressAndDeleteFolder(folderPath);
            }
            loadingForm.Close();
        }
        
        private void _GenerateXmlFileForSas3()
        {
            string fileName = "RegisterFile.csv";
            string folderPath = Path.Combine(Application.StartupPath, "LogFolder");
            string exportFilePath = Path.Combine(folderPath, fileName);
            string targetASidePath, targetBSidePath;

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("Project");
            xmlDoc.AppendChild(root);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Zip Files|*.zip";
            saveFileDialog.Title = "Save Zip File";

            if (cbASidePath.Checked)
                targetASidePath = ASidePath;
            else {
                MessageBox.Show("Please confirm the parameter file path");
                return;
            }

            if (cbBSidePath.Checked)
                targetBSidePath = BSidePath;
            else
                targetBSidePath = ASidePath;

            XmlElement permissionsNode = xmlDoc.CreateElement("Premissions");

            if (rbSas3CustomerMode.Checked) {
                permissionsNode.SetAttribute("role", "Customer");
            }
            else if (rbSas3MpMode.Checked) {
                permissionsNode.SetAttribute("role", "MP");
            }
            else {
                permissionsNode.SetAttribute("role", "Customer Check");
            }

            root.AppendChild(permissionsNode);
            XmlElement productNode = xmlDoc.CreateElement("Product");
            productNode.SetAttribute("name", "SAS3.0");
            permissionsNode.AppendChild(productNode);

            // Check and create filepath
            if (!string.IsNullOrWhiteSpace(ASidePath) || !string.IsNullOrWhiteSpace(BSidePath)) {
                XmlElement ASideFileNode = xmlDoc.CreateElement("ASIDE");
                ASideFileNode.SetAttribute("name", Path.GetFileName(ASidePath));
                permissionsNode.AppendChild(ASideFileNode);

                if (cbBSidePath.Checked) {
                    XmlElement BSideFileNode = xmlDoc.CreateElement("BSIDE");
                    BSideFileNode.SetAttribute("name", Path.GetFileName(BSidePath));
                    permissionsNode.AppendChild(BSideFileNode);
                }
                else {
                    XmlElement BSideFileNode = xmlDoc.CreateElement("BSIDE");
                    BSideFileNode.SetAttribute("name", Path.GetFileName(ASidePath));
                    permissionsNode.AppendChild(BSideFileNode);
                }
            }
            else {
                MessageBox.Show("ASide or BSide file path is not set.");
                return;
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                loadingForm.Show(this);
                Application.DoEvents();

                string selectedFileName = saveFileDialog.FileName;
                string folderName = Path.GetFileNameWithoutExtension(selectedFileName);
                string tempFolderPath = Path.Combine(Path.GetDirectoryName(selectedFileName), folderName);

                Directory.CreateDirectory(tempFolderPath);
                // Save the XML file as Cfg.xml
                xmlDoc.Save(Path.Combine(tempFolderPath, "Cfg.xml"));                
                lastUsedDirectory = tempFolderPath;
                _FormatChangeFromTextToCsv(exportFilePath, targetASidePath, targetBSidePath);

                if (!string.IsNullOrWhiteSpace(exportFilePath)) {
                    string destinationFilePath = Path.Combine(tempFolderPath, Path.GetFileName(exportFilePath));
                    File.Copy(exportFilePath, destinationFilePath, true);
                }

                CompressAndDeleteFolder(tempFolderPath);
            }

            loadingForm.Close();
        }
        /*
        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = "RegisterFile.csv";
            string folderPath = Path.Combine(Application.StartupPath, "LogFolder");
            string exportFilePath = Path.Combine(folderPath, fileName);
            string targetASidePath, targetBSidePath;


            if (cbASidePath.Checked)
                targetASidePath = ASidePath;
            else {
                MessageBox.Show("Please confirm the parameter file path");
                return;
            }

            if (cbBSidePath.Checked)
                targetBSidePath = BSidePath;
            else
                targetBSidePath = null;

            //MessageBox.Show("ASidePath: " + ASidePath + "\nBSidePath: " + BSidePath);
            _FormatChangeFromTextToCsv(exportFilePath, targetASidePath, targetBSidePath);
        }
        */

        private bool HardwareIdentificationValidation(string modelType, bool messageMode)
        {
            bool isVerified = false;

            switch (modelType) {
                case "SAS3.0":
                    isVerified = VerifySAS30Module(messageMode);
                    break;

                case "SAS4.0":
                case "PCIe4.0":
                case "QSFP28":

                    isVerified = VerifyMini58Module(messageMode);
                    break;

                default:
                    MessageBox.Show("Unknown module type. Please check!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }

            if (!isVerified) {
                //LogError
            }

            return isVerified;
        }

        private bool _GetChipId(string modelType)
        {
            string LddChipId = "";
            string TiaChipId = "";

            if (modelType == "SAS4.0") {
                TiaChipId = ucMata37044cConfig.GetChipId();
                LddChipId = ucMald37045cConfig.GetChipId();
                if ((TiaChipId == "0x77") && (LddChipId == "0x79"))
                    return true;
                
            }
            else if (modelType == "PCIe4.0") {
                TiaChipId = ucRt145Config.GetChipId();
                //LddChipId = ucRt146Config.GetChipId();
                //if ((TiaChipId == "0x58") && (LddChipId == "0xFF"))
                if (TiaChipId == "0x58")
                    return true;
            }
            /*
            else if (modelType == "QSFP28") {
                TiaChipId = ucGn2108Config.GetChipId();
                LddChipId = ucGn2109Config.GetChipId();
                if ((TiaChipId == "") && (LddChipId == ""))
                    return true;
            }*/
            else {
                MessageBox.Show("This product is not in the list\nModel: " + modelType );
            }

            MessageBox.Show("LDD Chip ID: " + LddChipId +
                            "\nTIA Chip ID: " + TiaChipId);

            return false;
        }

        private bool VerifySAS30Module(bool messageMode)
        {
            string venderPn = GetVenderPnApi();

            if (messageMode) {
                if (venderPn.Contains("AP3AD5D5")) {
                    MessageBox.Show("Verification PASS!!\nVendorPn: " + venderPn, "Pass",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else {
                    MessageBox.Show("Please confirm whether the plug-in module is SAS3.0", "Error!!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else 
                return venderPn.Contains("AP3AD5D5") ? true : false;
        }

        private bool VerifyMini58Module(bool messageMode)
        {
            string hiddenPassword = GetHiddenPasswordApi();
            string hexString = "1A, 58, 1A, 58";
            string[] hexValues = hexString.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] bytes = new byte[hexValues.Length];
            for (int i = 0; i < hexValues.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexValues[i], 16);
            }
            String targetPassword = Encoding.Default.GetString(bytes);

            if (messageMode)
            {
                if (hiddenPassword == targetPassword)
                {
                    MessageBox.Show("Verification PASS!!\nHiddenPassword (Hex): " + hexString, "Hidden password check",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("Please confirm whether the plug-in module is Mini58 MCU", "Error!!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
                return (hiddenPassword == targetPassword) ? true : false;
        }

        private void CompressAndDeleteFolder(string folderPath)
        {
            string zipFilePath = folderPath + ".zip";
            string password = "2368";

            using (ZipFile zip = new ZipFile()) {
                zip.Password = password;
                zip.AddDirectory(folderPath);
                zip.Save(zipFilePath);
            }

            Directory.Delete(folderPath, true);
        }

        private List<UserControl> GetAllUserControls(Control control) // 遞迴取得 MainForm 中的所有 User Control
        {
            List<UserControl> userControls = new List<UserControl>();

            foreach (Control childControl in control.Controls)
            {
                if (childControl is UserControl userControl)
                {
                    userControls.Add(userControl);
                }

                userControls.AddRange(GetAllUserControls(childControl));
            }

            return userControls;
        }

        private List<Control> GetAllControls(Control control) // 遞迴取得 User Control 中的所有 Control
        {
            List<Control> controls = new List<Control>();

            foreach (Control childControl in control.Controls)
            {
                controls.Add(childControl);

                if (childControl is UserControl userControl) // If UserControl，遞迴取得其內部的所有元件
                {
                    controls.AddRange(GetAllControls(userControl));
                }

                controls.AddRange(GetAllControls(childControl));
            }

            return controls;
        }

        private void SetAllComponentsEnabled(Control control, bool enabled)
        {
            List<Control> controls = GetAllControls(control);

            foreach (Control c in controls)
            {
                c.Enabled = enabled;
            }
        }

        private class ControlComparer : IComparer<Control> //比較器，用於排序
        {
            public int Compare(Control x, Control y)
            {
                return string.Compare(x.Name, y.Name, StringComparison.Ordinal); // 依components name進行排序
            }
        }

        public int ConfigUiByXmlApi(String configXml)
        {
            OpenFileDialog ofdSelectFile = new OpenFileDialog();
            XmlReader xrConfig;

            string folderPath = Application.StartupPath;
            string combinedPath = Path.Combine(folderPath, "XmlFolder");
            string xmlFilePath = Path.Combine(combinedPath, configXml);
            xmlFilePath = xmlFilePath.Replace("\\\\", "\\");


            if (xmlFilePath.Length == 0)
            {
                ofdSelectFile.Title = "Select config file";
                ofdSelectFile.Filter = "xml files (*.xml)|*.xml";
                if (ofdSelectFile.ShowDialog() != DialogResult.OK)
                    return -1;
                xrConfig = XmlReader.Create(ofdSelectFile.FileName);
            }
            else
            {
                xrConfig = XmlReader.Create(xmlFilePath);
            }

            while (xrConfig.Read())
            {
                if (xrConfig.IsStartElement())
                {
                    switch (xrConfig.Name)
                    {
                        case "Settings":
                            xrConfig.Read();
                            _ParseSettingsXml(xrConfig);
                            break;

                        default:
                            break;
                    }
                }
            }

            return 0;
        }

        private void _ParseSettingsXml(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.IsStartElement() && reader.Name == "Permission")
                {
                    string role = reader.GetAttribute("role");

                    if (role == cbPermission.SelectedItem.ToString())
                    {
                        // The selected permission level matches the current Permission element

                        reader.Read(); // Move to the first UserControl element

                        while (reader.IsStartElement() && reader.Name == "UserControl")
                        {
                            string userControlName = reader.GetAttribute("name");

                            reader.Read(); // Move to the Components element within the UserControl
                            
                            while (reader.IsStartElement() && reader.Name == "Components")
                            {
                                reader.Read(); // Move to the first Component element

                                while (reader.IsStartElement() && reader.Name == "Component")
                                {
                                    _ParseComponentXml(reader, userControlName);
                                }

                                reader.ReadEndElement(); // Move out of the Components element
                            }

                            reader.ReadEndElement(); // Move out of the UserControl element
                        }
                    }
                    else
                    {
                        reader.Skip(); // Skip the elements for other permission levels
                    }
                }
            }
        }

        private MainFormPaths _LoadLastPaths()
        {
            string folderPath = Application.StartupPath;
            string combinedPath = Path.Combine(folderPath, "XmlFolder");
            string xmlFilePath = Path.Combine(combinedPath, "MainFormPaths.xml");
            xmlFilePath = xmlFilePath.Replace("\\\\", "\\");

            try {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);
                XmlNode zipPathNode = xmlDoc.SelectSingleNode("//ZipFolderPath");
                XmlNode logFilePathNode = xmlDoc.SelectSingleNode("//LogFilePath");

                string zipPath = zipPathNode?.InnerText;
                string logFilePath = logFilePathNode?.InnerText;

                return new MainFormPaths {
                    ZipFolderPath = zipPath,
                    LogFilePath = logFilePath
                };
            }
            catch (Exception) {
                return new MainFormPaths {
                    ZipFolderPath = null,
                    LogFilePath = null
                };
            }
        }

        private void _ParseComponentXml(XmlReader reader, string userControlName)
        {
            string componentName = reader.GetAttribute("name");
            string objectType = reader.GetAttribute("object");
            string visible = reader.GetAttribute("visible");
            string enabled = reader.GetAttribute("enabled");
            string readOnly = reader.GetAttribute("ReadOnly");

            UserControl targetUserControl = (UserControl)Controls.Find(userControlName, true).FirstOrDefault();

            if (targetUserControl != null)
            {
                switch (objectType)
                {
                    case "TextBox":
                        TextBox tbTmp = (TextBox)targetUserControl.Controls.Find(componentName, true).FirstOrDefault();
                        
                        if (tbTmp != null)
                        {
                            if (visible != null) tbTmp.Visible = visible.Equals("True");
                            if (enabled != null) tbTmp.Enabled = enabled.Equals("True");
                            if (readOnly != null) tbTmp.ReadOnly = readOnly.Equals("True");
                        }
                        break;

                    case "Label":
                        Label lTmp = (Label)targetUserControl.Controls.Find(componentName, true).FirstOrDefault();

                        if (lTmp != null)
                        {
                            if (visible != null) lTmp.Visible = visible.Equals("True");
                            if (enabled != null) lTmp.Enabled = enabled.Equals("True");
                        }
                        break;

                    case "Button":
                        Button bTmp = (Button)targetUserControl.Controls.Find(componentName, true).FirstOrDefault();

                        if (bTmp != null)
                        {
                            if (visible != null) bTmp.Visible = visible.Equals("True", StringComparison.OrdinalIgnoreCase);
                            if (enabled != null) bTmp.Enabled = enabled.Equals("True", StringComparison.OrdinalIgnoreCase);
                        }
                        break;

                    case "GroupBox":
                        GroupBox gbTmp = (GroupBox)targetUserControl.Controls.Find(componentName, true).FirstOrDefault();

                        if (gbTmp != null)
                        {
                            if (visible != null) gbTmp.Visible = visible.Equals("True");
                            if (enabled != null) gbTmp.Enabled = enabled.Equals("True");
                        }
                        break;

                    case "CheckBox":
                        CheckBox cbTmp = (CheckBox)targetUserControl.Controls.Find(componentName, true).FirstOrDefault();

                        if (cbTmp != null)
                        {
                            if (visible != null) cbTmp.Visible = visible.Equals("True");
                            if (enabled != null) cbTmp.Enabled = enabled.Equals("True");
                        }
                        break;

                    case "ComboBox":
                        ComboBox cobTmp = (ComboBox)targetUserControl.Controls.Find(componentName, true).FirstOrDefault();

                        if (cobTmp != null)
                        {
                            if (visible != null) cobTmp.Visible = visible.Equals("True");
                            if (enabled != null) cobTmp.Enabled = enabled.Equals("True");
                        }
                        break;

                    default:
                        break;
                }
            }

            reader.Read(); // Move to the next Component element
        }

        private void bOutterSwitch_Click(object sender, EventArgs e)
        {
            if (bOutterSwitch.Enabled == true)
                bOutterSwitch.Enabled = false;

            ChannelSwitchApi();
            bGlobalRead.Select();

            if (bOutterSwitch.Enabled == false)
                bOutterSwitch.Enabled = true;
        }

        private void bInnerSwitch_Click(object sender, EventArgs e)
        {
            if (bInnerSwitch.Enabled == true)
                bInnerSwitch.Enabled = false;

            ChannelSwitchApi();

            if (bInnerSwitch.Enabled == false)
                bInnerSwitch.Enabled = true;
                
            bInnerSwitch.Select();
        }

        private void _UpdateButtonState()
        {
           
            if ((ProcessingChannel == 1) || (ProcessingChannel == 13))
            {
                rbCh1.Checked = true;
                rbCh2.Checked = false;
                tbInnerStateCh1.BackColor = Color.YellowGreen;
                tbInnerStateCh2.BackColor = Color.White;
            }

            if ((ProcessingChannel == 2) || (ProcessingChannel == 23))
            {
                rbCh2.Checked = true;
                rbCh1.Checked = false;
                tbInnerStateCh1.BackColor = Color.White;
                tbInnerStateCh2.BackColor = Color.YellowGreen;
            }

            Application.DoEvents();
        }
       
        private void _EnableIcConfig()
        {
            if (cbProductSelect.SelectedIndex != 0)
            {
                cbTxIcConfig.Enabled = true;
                cbTxIcConfig.Checked = false;
                cbRxIcConfig.Enabled = true;
                cbRxIcConfig.Checked = false;
                tbTxConfigReadState.Enabled = true;
                tbRxConfigReadState.Enabled = true;
                AutoSelectIcConfig = true;
            }
            else
            {
                cbTxIcConfig.Enabled = false;
                cbTxIcConfig.Checked = false;
                cbRxIcConfig.Enabled = false;
                cbRxIcConfig.Checked = false;
                tbTxConfigReadState.Enabled = false;
                tbRxConfigReadState.Enabled = false;
                AutoSelectIcConfig = false;
            }
        }

        private void _UpdateTabPageVisibility()
        {
            int variable;

            variable = cbProductSelect.SelectedIndex;

            if (variable == 1)
            {
                tcIcConfig.TabPages.Remove(tabPage32);
                tcIcConfig.TabPages.Remove(tabPage33);

                if (!tcIcConfig.TabPages.Contains(tabPage31))
                {
                    tcIcConfig.TabPages.Add(tabPage31);
                }
            }

            else if (variable == 2)
            {
                tcIcConfig.TabPages.Remove(tabPage31);
                tcIcConfig.TabPages.Remove(tabPage33);

                if (!tcIcConfig.TabPages.Contains(tabPage32))
                {
                    tcIcConfig.TabPages.Add(tabPage32);
                }
            }
            else if (variable == 3)
            {
                tcIcConfig.TabPages.Remove(tabPage31);
                tcIcConfig.TabPages.Remove(tabPage32);

                if (!tcIcConfig.TabPages.Contains(tabPage33))
                {
                    tcIcConfig.TabPages.Add(tabPage33);
                }
            }

            else
            {
                if (!tcIcConfig.TabPages.Contains(tabPage31))
                {
                    tcIcConfig.TabPages.Add(tabPage31);
                }
                if (!tcIcConfig.TabPages.Contains(tabPage32))
                {
                    tcIcConfig.TabPages.Add(tabPage32);
                }
                if (!tcIcConfig.TabPages.Contains(tabPage33))
                {
                    tcIcConfig.TabPages.Add(tabPage33);
                }
            }
        }

        private string _GetFirmwareVersionCode()
        {
            byte[] data = new byte[10];
            byte[] bATmp = new byte[2];

            data[0] = 0xAA;
            if (_I2cWrite(80, 127, 1, data) < 0)
                return "-1";

            if (_I2cRead(80, 165, 10, data) < 0)
                return "-1";

            if ((data[0] == 0) && (data[1] == 0) && (data[2] == 0) && (data[3] == 0) &&
                (data[4] == 0) && (data[5] == 0) && (data[6] == 0) && (data[7] == 0) &&
                (data[8] == 0) && (data[9] == 0)) {
                data[0] = 32;
                if (_I2cWrite(80, 127, 1, data) < 0)
                    return "-1";

                if (_I2cRead(80, 165, 10, data) < 0)
                    return "-1";
            }
            
            bATmp = new byte[8];
            System.Buffer.BlockCopy(data, 2, bATmp, 0, 8);
            
            return Encoding.Default.GetString(bATmp);
        }

        private void bGlobalRead_Click(object sender, EventArgs e)
        {
            loadingForm.Show(this);
            _DisableButtons();
            _InitialStateBar();
            _GlobalRead();
            FirstRead = true;
            _EnableButtons();
            loadingForm.Close();
        }

        internal int _GlobalRead()
        {
            bool readFail = false;
            int errorCount = 0;
            int delay = 10;
            StateUpdated("Read State:\nPreparing resources...", 1);

            if (cbInfomation.Checked)
            {
                Thread.Sleep(delay);
                tbInformationReadState.BackColor = Color.SteelBlue;
                Application.DoEvents();

                if (ucInformation.ReadAllApi() < 0)
                {
                    tbInformationReadState.BackColor = Color.Red;
                    StateUpdated("Read State:\nInformation...Failed", 3);
                    errorCount++;
                }
                else
                {
                    tbInformationReadState.BackColor = Color.YellowGreen;
                    StateUpdated("Read State:\nInformation...Done", 3);
                }

                Application.DoEvents();
            }

            if (cbDdm.Checked)
            {
                Thread.Sleep(delay);
                tbDdmReadState.BackColor = Color.SteelBlue;
                Application.DoEvents();

                if (ucDigitalDiagnosticsMonitoring.ReadAllApi() < 0)
                {
                    tbDdmReadState.BackColor = Color.Red;
                    StateUpdated("Read State:\nDdm...Failed", 7);
                    errorCount++;
                }
                else
                {
                    tbDdmReadState.BackColor = Color.YellowGreen;
                    StateUpdated("Read State:\nDdm...Done", 7);
                }

                Application.DoEvents();
            }

            if (cbMemDump.Checked)
            {
                Thread.Sleep(delay);
                tbMemDumpReadState.BackColor = Color.SteelBlue;
                Application.DoEvents();

                if (ucMemoryDump.ReadAllApi(null) < 0)
                {
                    tbMemDumpReadState.BackColor = Color.Red;
                    StateUpdated("Read State:\nMemDump...Failed", 10);
                    errorCount++;
                }
                else
                {
                    tbMemDumpReadState.BackColor = Color.YellowGreen;
                    StateUpdated("Read State:\nMemDump...Done", 10);
                }

                Application.DoEvents();
            }

            if (cbCorrector.Checked)
            {
                Thread.Sleep(delay);
                tbCorrectorReadState.BackColor = Color.SteelBlue;
                Application.DoEvents();

                if (ucGn1190Corrector.ReadAllApi() < 0)
                {
                    tbCorrectorReadState.BackColor = Color.Red;
                    StateUpdated("Read State:\nCorrector...Failed", 15);
                    errorCount++;
                }
                else
                {
                    tbCorrectorReadState.BackColor = Color.YellowGreen;
                    StateUpdated("Read State:\nCorrector...Done", 15);
                }

                Application.DoEvents();
            }

            if (cbProductSelect.SelectedIndex != 0)
            {
                if (cbTxIcConfig.Checked)
                {
                    Thread.Sleep(delay);
                    tbTxConfigReadState.BackColor = Color.SteelBlue;
                    Application.DoEvents();

                    switch (cbProductSelect.SelectedIndex)
                    {
                        case 1: // SAS4.0
                            if (ucMald37045cConfig.ReadAllApi() < 0)
                                readFail = true;

                            break;
                        case 2: // PCIe4
                            if (ucRt146Config.ReadAllApi() < 0)
                                readFail = true;

                            break;
                        case 3: // QSFP28
                            if (ucGn2108Config.ReadAllApi() < 0)
                                readFail = true;

                            break;
                    }

                    if (readFail)
                    {
                        tbTxConfigReadState.BackColor = Color.Red;
                        StateUpdated("Read State:\nTxIcConfig...Failed", 23);
                        errorCount++;
                    }
                    else
                    {
                        tbTxConfigReadState.BackColor = Color.YellowGreen;
                        StateUpdated("Read State:\nTxIcConfig...Done", 23);
                    }

                    Application.DoEvents();
                    readFail = false;
                }

                if (cbRxIcConfig.Checked)
                {
                    Thread.Sleep(delay);
                    tbRxConfigReadState.BackColor = Color.SteelBlue;
                    Application.DoEvents();

                    switch (cbProductSelect.SelectedIndex)
                    {
                        case 1: // SAS4.0
                            if (ucMata37044cConfig.ReadAllApi() < 0)
                                readFail = true;

                            break;
                        case 2: // PCIe4
                            if (ucRt145Config.ReadAllApi() < 0)
                                readFail = true;

                            break;
                        case 3: // QSFP28
                            if (ucGn2109Config.ReadAllApi() < 0)
                                readFail = true;

                            break;
                    }

                    if (readFail)
                    {
                        tbRxConfigReadState.BackColor = Color.Red;
                        StateUpdated("Read State:\nRxIcConfig...Failed", 30);
                        errorCount++;
                    }
                    else
                    {
                        tbRxConfigReadState.BackColor = Color.YellowGreen;
                        StateUpdated("Read State:\nRxIcConfig...Done", 30);
                    }

                    Application.DoEvents();
                }
            }
            return errorCount;
        }

        private bool _LoadFilesPathForBin(string fileType)
        {
            string sourceFileName, sourceFilePath;
            //string initialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.Title = "Files path";
                openFileDialog.Filter = "Binary Files (*.bin)|*.bin";
                //openFileDialog.InitialDirectory = initialDirectory;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    string originalPath = openFileDialog.FileName;
                    string originalFileName = Path.GetFileName(originalPath); // Get original filename
                    lastUsedDirectory = Path.GetDirectoryName(openFileDialog.FileName);
                    //MessageBox.Show("lastUsedDirectory: \n" + lastUsedDirectory );

                    if (originalFileName.Contains(" ")) {
                        string newFileName = originalFileName.Replace(" ", "_");
                        string newPath = Path.Combine(lastUsedDirectory, newFileName);
                        sourceFileName = newFileName;

                        try {
                            File.Move(originalPath, newPath); // 覆蓋原檔
                            //sourceFilePath = newPath;

                            if (DebugMode)
                                MessageBox.Show($"已重新命名為: {newFileName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (IOException ex) {
                            if (DebugMode)
                                MessageBox.Show($"無法重新命名: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else {
                        //sourceFilePath = originalPath;
                        sourceFileName = originalFileName;
                    }

                    if (fileType == "APROM") {
                        APROMPath = Path.Combine(lastUsedDirectory, sourceFileName);
                        cbAPPath.Checked = true;
                        MessageBox.Show("APROMPath: \n" + APROMPath);
                    }

                    if (fileType == "DATAROM") {
                        DATAROMPath = Path.Combine(lastUsedDirectory, sourceFileName);
                        cbDAPath.Checked = true;
                        MessageBox.Show("DATAROMPath: \n" + DATAROMPath);
                    }

                    return true;
                }
                else {
                    if (fileType == "APROM")
                        cbAPPath.Checked = false;

                    if (fileType == "DATAROM")
                        cbDAPath.Checked = false;

                    return false;
                }
            }
        }
        
        private bool _LoadFilesPathForText(string fileType)
        {
            //string initialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.Title = "Files path";
                openFileDialog.Filter = "Text Files (*.txt)|*.txt";
                //openFileDialog.InitialDirectory = initialDirectory;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    string sourceFileName = Path.GetFileName(openFileDialog.FileName);
                    lastUsedDirectory = Path.GetDirectoryName(openFileDialog.FileName);
                    //MessageBox.Show("lastUsedDirectory: \n" + lastUsedDirectory );

                    if (fileType == "ASide") {
                        ASidePath = lastUsedDirectory + "\\" + sourceFileName;
                        cbASidePath.Checked = true;
                    }

                    if (fileType == "BSide") {
                        BSidePath = lastUsedDirectory + "\\" + sourceFileName;
                        cbBSidePath.Checked = true;
                    }

                    return true;
                }
                else {
                    if (fileType == "ASide")
                        cbAPPath.Checked = false;

                    if (fileType == "BSide")
                        cbDAPath.Checked = false;

                    return false;
                }
            }
        }

        private void _GenerateCfgButtonState(int mcuType, bool bSide)
        {
            if (mcuType == 1) {
                if (cbAPPath.Checked)
                    bGenerateCfg.Enabled = true;
                else
                    bGenerateCfg.Enabled = false;
            }
            else if (mcuType == 2 && bSide == false) {
                if ((cbASidePath.Checked) ) {
                    cbBSidePath.Enabled = true;
                    bSas3GenerateCfg.Enabled = true;
                }
                else {
                    cbBSidePath.Enabled = false;
                    bSas3GenerateCfg.Enabled = false;
                }
            }
            else if (mcuType == 2 && bSide == true) {
                if (cbBSidePath.Checked && (rbSas3CustomerMode.Checked || rbSas3MpMode.Checked)) {
                    bSas3GenerateCfg.Enabled = true;
                }
                else {
                    bSas3GenerateCfg.Enabled = false;
                }
            }
        }

        internal int _GlobalWriteFromRegisterFile(bool CustomerMode, string CfgFilePath, int processingChannel)
        {
            MainFormPaths lastPath = _LoadLastPaths();
            string DirectoryPath = Application.StartupPath;
            string BackupRegisterFilePath = Path.Combine(DirectoryPath, "LogFolder\\ModuleRegisterFile.csv");
            
            /*
            string BackupRegisterFilePath = lastPath.LogFilePath;

            if (string.IsNullOrEmpty(BackupRegisterFilePath))
                BackupRegisterFilePath = Path.Combine(DirectoryPath, "LogFolder\\ModuleRegisterFile.csv");
            else
                BackupRegisterFilePath = Path.Combine(BackupRegisterFilePath, "ModuleRegisterFile.csv");
            */

            StateUpdated("Write State:\nPreparing resources...", 61);

            if (WriteRegisterPageApi("80h", 200, BackupRegisterFilePath) < 0)
                return -1; //Write from LogFile/TempRegister
            StateUpdated("Write State:\nPage 0x80h...Done", 67);
            if (WriteRegisterPageApi("81h", 200, BackupRegisterFilePath) < 0)
                return -1;
            StateUpdated("Write State:\nPage 0x81h...Done", 70);
            if (WriteRegisterPageApi("Rx", 1000, BackupRegisterFilePath) < 0)
                return -1;
            StateUpdated("Write State:\nRxIcConfig...Done", 80);
            if (WriteRegisterPageApi("Tx", 1000, BackupRegisterFilePath) < 0)
                return -1;
            StateUpdated("Write State:\nTxIcConfig...Done", 90);

            if (CustomerMode)
            {
                if (WriteRegisterPageApi("Up 00h", 200, BackupRegisterFilePath) < 0) return -1; //Write from LogFile/TempRegister
                StateUpdated("Write State:\nUpPage 00h...Done", 64);
                if (WriteRegisterPageApi("Up 03h", 200, BackupRegisterFilePath) < 0) return -1;
                StateUpdated("Write State:\nUpPage 03h...Done", 65);
            }
            else
            {
                if (WriteRegisterPageApi("Low Page", 200, CfgFilePath) < 0) return -1;
                StateUpdated("Write State:\nLow Page...Done", 63);
                if (WriteRegisterPageApi("Up 00h", 200, CfgFilePath) < 0) return -1; //Write from Cfg.RegisterFile
                StateUpdated("Write State:\nUpPage 00h...Done", 64);
                if (WriteRegisterPageApi("Up 03h", 200, CfgFilePath) < 0) return -1;
                StateUpdated("Write State:\nUpPage 03h...Done", 65);
            }

            StoreIntoFlashApi();
            StateUpdated("Write State:\nStore into flash...Done", 95);

            return 0;
        }

        internal int _WriteFromRegisterFileForSas3(bool CustomerMode, string CfgFilePath, int processingChannel)
        {
            StateUpdated("Write State:\nPreparing resources...", 10);

            if (CustomerMode) {
                //StateUpdated("Write State:\nUpPage 03h...Done", 65);
            }
            else {
                if (WriteRegisterPageForSas3Api("Page 00", 1000, 0x10, 80, CfgFilePath, processingChannel) < 0)
                    return -1; //Write from Cfg.RegisterFile
                StateUpdated("Write State:\nPage 00...Done", 30);
                
                if (WriteRegisterPageForSas3Api("Page 70", 100, 0x6C, 6, CfgFilePath, processingChannel) < 0)
                    return -1; //Write from Cfg.RegisterFile
                StateUpdated("Write State:\nPage 70...Done", 50);

                if (WriteRegisterPageForSas3Api("Page 73", 100, 0x6E, 2, CfgFilePath, processingChannel) < 0)
                    return -1; //Write from Cfg.RegisterFile
                StateUpdated("Write State:\nPage 73...Done", 70);

                if (WriteRegisterPageForSas3Api("Page 3A", 100, 0x00, 9, CfgFilePath, processingChannel) < 0)
                    return -1; //Write from Cfg.RegisterFile
                if (WriteRegisterPageForSas3Api("Page 3A", 100, 0x4F, 11, CfgFilePath, processingChannel) < 0)
                    return -1; //Write from Cfg.RegisterFile
                StateUpdated("Write State:\nPage 3A...Done", 90);

                /*
                if (WriteRegisterPageForSas3Api("Page 00", 1000, CfgFilePath, processingChannel, 0x90, 80) < 0)
                    return -1; //Write from Cfg.RegisterFile
                StateUpdated("Write State:\nPage 00...Done", 30);

                
                if (WriteRegisterPageForSas3Api("Page 00", 100, CfgFilePath, processingChannel, 0xED , 5) < 0)
                    return -1; //Write from Cfg.RegisterFile
                StateUpdated("Write State:\nPage 70...Done", 40);
                */
            }

            StoreIntoFlashApi();
            StateUpdated("Write State:\nStore into flash...Done", 95);

            return 0;
        }

        internal int _KeyForSas3()
        {
            if (_WriteModulePassword() < 0)
                return -1;

            return 0;
        }

        internal int _GlobalWriteFromUi(bool ExternalMode)
        {
            bool writeFail = false;
            int returnValue = 0;
            int errorCount = 0;
            int delay = 10;

            StateUpdated("Write State:\nPreparing resources...", 62);
            Application.DoEvents();

            if (DebugMode)
                MessageBox.Show("cbInfomation Check state: " + cbInfomation.CheckState);

            
            if (cbInfomation.Checked)
            {
                Thread.Sleep(delay);
                tbInformationReadState.BackColor = Color.SteelBlue;
                Application.DoEvents();

                if (ucInformation.WriteAllApi() < 0)
                {
                    tbInformationReadState.BackColor = Color.Red;
                    StateUpdated("Write State:\nInformation...Failed", 65);
                    errorCount++;
                }
                else
                {
                    tbInformationReadState.BackColor = Color.YellowGreen;
                    StateUpdated("Write State:\nInformation...Done", 65);
                    if (DebugMode)
                        MessageBox.Show("Write State: Information...Done");
                }
                Application.DoEvents();
            }

            if (DebugMode)
                MessageBox.Show("cbDdm Check state: " + cbDdm.CheckState);

            if (cbDdm.Checked)
            {
                Thread.Sleep(delay);
                tbDdmReadState.BackColor = Color.SteelBlue;
                Application.DoEvents();

                if (ucDigitalDiagnosticsMonitoring.WriteAllApi() < 0)
                {
                    returnValue = ucDigitalDiagnosticsMonitoring.WriteAllApi();
                    MessageBox.Show("rv : " + returnValue);
                    tbDdmReadState.BackColor = Color.Red;
                    StateUpdated("Write State:\nDdm...Failed", 68);
                    errorCount++;
                }

                else
                {
                    tbDdmReadState.BackColor = Color.YellowGreen;
                    StateUpdated("Write State:\nDdm...Done", 68);
                    if (DebugMode)
                        MessageBox.Show("Write State: Ddm...Done");
                }
                Application.DoEvents();
            }

            if (DebugMode)
                MessageBox.Show("cbMemDump Check state: " + cbMemDump.CheckState);

            if (cbMemDump.Checked)
            {
                Thread.Sleep(delay);
                tbMemDumpReadState.BackColor = Color.SteelBlue;
                Application.DoEvents();

                if (ucMemoryDump.WriteAllApi() < 0)
                {
                    tbMemDumpReadState.BackColor = Color.Red;
                    StateUpdated("Write State:\nMemoryDump...Failed", 70);
                    errorCount++;
                }
                else
                {
                    tbMemDumpReadState.BackColor = Color.YellowGreen;
                    StateUpdated("Write State:\nMemoryDump...Done", 70);
                    if (DebugMode)
                        MessageBox.Show("Write State: MemoryDump...Done");
                }
                Application.DoEvents();
            }

            if (DebugMode)
                MessageBox.Show("cbCorrector Check state: " + cbCorrector.CheckState);

            if (cbCorrector.Checked)
            {
                Thread.Sleep(delay);
                tbCorrectorReadState.BackColor = Color.SteelBlue;
                Application.DoEvents();

                if (ucGn1190Corrector.WriteAllApi() < 0)
                {
                    tbCorrectorReadState.BackColor = Color.Red;
                    StateUpdated("Write State:\nCorrector...Failed", 75);
                    errorCount++;
                }
                else
                {
                    tbCorrectorReadState.BackColor = Color.YellowGreen;
                    StateUpdated("Write State:\nCorrector...Done", 75);
                    if (DebugMode)
                        MessageBox.Show("Write State: Corrector...Done");
                }
                Application.DoEvents();
            }

            if (DebugMode)
            {
                MessageBox.Show("cbTxIcConfig Check state: " + cbTxIcConfig.CheckState
                                + "\ncbProductSelect state: " + cbProductSelect.Text
                                );
            }

            if (cbProductSelect.SelectedIndex != 0)
            {
                if (cbTxIcConfig.Checked)
                {
                    Thread.Sleep(delay);
                    tbTxConfigReadState.BackColor = Color.SteelBlue;
                    Application.DoEvents();

                    if (!BypassWriteIcConfig)
                    {
                        switch (cbProductSelect.SelectedIndex)
                        {
                            case 1: // SAS4.0
                                if (ucMald37045cConfig.WriteAllApi() < 0)
                                    writeFail = true;

                                break;
                            case 2: // PCIe4
                                if (ucRt146Config.WriteAllApi() < 0)
                                    writeFail = true;

                                break;
                            case 3: // QSFP28
                                if (ucGn2108Config.WriteAllApi() < 0)
                                    writeFail = true;

                                break;
                        }
                    }

                    if (writeFail)
                    {
                        tbTxConfigReadState.BackColor = Color.Red;
                        StateUpdated("Write State:\nTxIcConfig...Failed", 80);
                        errorCount++;
                    }
                    else
                    {
                        tbTxConfigReadState.BackColor = Color.YellowGreen;
                        StateUpdated("Write State:\nTxIcConfig...Done", 80);
                        if (DebugMode)
                            MessageBox.Show("Write State: TxIcConfig...Done");
                    }
                    Application.DoEvents();
                    writeFail = false;
                }

                if (DebugMode)
                    MessageBox.Show("cbRxIcConfig Check state: " + cbRxIcConfig.CheckState);

                if (cbRxIcConfig.Checked)
                {
                    Thread.Sleep(delay);
                    tbRxConfigReadState.BackColor = Color.SteelBlue;
                    Application.DoEvents();

                    if (!BypassWriteIcConfig)
                    {
                        switch (cbProductSelect.SelectedIndex)
                        {
                            case 1: // SAS4.0
                                if (ucMata37044cConfig.WriteAllApi() < 0)
                                    writeFail = true;

                                break;
                            case 2: // PCIe4
                                if (ucRt145Config.WriteAllApi() < 0)
                                    writeFail = true;

                                break;
                            case 3: // QSFP28
                                if (ucGn2109Config.WriteAllApi() < 0)
                                    writeFail = true;

                                break;
                        }
                    }

                    if (writeFail)
                    {
                        tbRxConfigReadState.BackColor = Color.Red;
                        StateUpdated("Write State:\nRxIcConfig...Failed", 90);
                        errorCount++;
                    }
                    else
                    {
                        tbRxConfigReadState.BackColor = Color.YellowGreen;
                        StateUpdated("Write State:\nRxIcConfig...Done", 90);
                        if (DebugMode)
                            MessageBox.Show("Write State: RxIcConfig...Done");
                    }

                    Application.DoEvents();
                }
            }
            return errorCount;
        }

        internal int _CurrentRegisters(string modelType)
        {
            string fileName1 = "UpdatedModuleRegisterFile"; // Module cfg file
            string filePath1;
            string executableFileFolderPath = Path.Combine(Application.StartupPath, "RegisterFiles");
            var masks = _GetMasks(modelType, null);

            StateUpdated("Current module:\nPreparing register contents...", null);

            // Export current module register file
            if (_ExportModuleCfg(modelType, fileName1, "LogFile") < 0)
                return -1;

            filePath1 = Path.Combine(executableFileFolderPath, fileName1 + ".csv");
            _ReformatedCsvFile(filePath1, 1, executableFileFolderPath, null);
            filePath1 = Path.Combine(executableFileFolderPath, "temp1_" + fileName1 + ".csv");
            DataTable dt1 = _ReadCsvToDataTable(filePath1);
            _RemoveDoubleQuotes(dt1);//Module
            _ApplyMask(new List<DataTable> { dt1 }, masks);
            _DisplayCurrentRegister(dt1, masks);
            
            if (File.Exists(filePath1))
                File.Delete(filePath1);

            StateUpdated("Current module:\nThe output is ready", null);

            return 0;
        }

        internal int _ComparisonRegister(string modelType, string RegisterFilePath, bool onlyVerifyMode,string comparisonObject, bool engineerMode)
        {
            string fileName1 = "UpdatedModuleRegisterFile"; // Module cfg file
            string fileName2 = Path.GetFileName(RegisterFilePath); // Reference cfg file
            string filePath1;
            string filePath2 = RegisterFilePath; // Reference cfg file
            string executableFileFolderPath = Path.Combine(Application.StartupPath, "RegisterFiles");
            var masks = _GetMasks(modelType, comparisonObject);

            if (!onlyVerifyMode) {
                if (comparisonObject == "CfgFile")
                    StateUpdated("Verify State:\nCfgFile check...", 93);
                else if (comparisonObject == "LogFile")
                    StateUpdated("Verify State:\nLogFIle check...", 93);
            }
            else {
                if (comparisonObject == "CfgFile")
                    StateUpdated("Verify State:\nCfgFile check...", null);
                else if (comparisonObject == "LogFile")
                    StateUpdated("Verify State:\nLogFIle check...", null);
            }

            // Export current module register file
            if (_ExportModuleCfg(modelType, fileName1, comparisonObject) < 0)
                    return -1;

            filePath1 = Path.Combine(executableFileFolderPath, fileName1 + ".csv");
            _ReformatedCsvFile(filePath1, 1, executableFileFolderPath, comparisonObject);
            _ReformatedCsvFile(filePath2, 2, executableFileFolderPath, comparisonObject);
            filePath1 = Path.Combine(executableFileFolderPath, "temp1_" + fileName1 + ".csv");
            filePath2 = Path.Combine(executableFileFolderPath, "temp2_" + fileName2);
            DataTable dt1 = _ReadCsvToDataTable(filePath1);
            DataTable dt2 = _ReadCsvToDataTable(filePath2);



            _RemoveDoubleQuotes(dt1);//Module
            _RemoveDoubleQuotes(dt2);//Cfg
            //ApplyMask(dt1, dt2, masks);
            _ApplyMask(new List<DataTable> { dt1, dt2 }, masks);


            // Error alarm, if there are differences
            if (!_CompareDataTables(dt1, dt2)) {
                //MessageBox.Show("Verify Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (engineerMode|| onlyVerifyMode)
                    _DisplayDifferencesInGrid(dt1, dt2, masks); // EngineerCheck from DataGridView
                else
                MessageBox.Show("There are differences between the module CfgFile and the target CfgFile.",
                                "Error alarm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                StateUpdated("Verify State:\nVerify failed", null);
                return 1;
            }

            // Delete the temp file, if there are no errors
            if (File.Exists(filePath1))
                File.Delete(filePath1);

            if (File.Exists(filePath2))
                File.Delete(filePath2);

            if (!onlyVerifyMode) {
                if (comparisonObject == "CfgFile")
                    StateUpdated("Verify State:\nCfgFile are matching", 97);
                else if (comparisonObject == "LogFile")
                    StateUpdated("Verify State:\nLogFIle are matching", 97);
            }
            else {
                if (comparisonObject == "CfgFile")
                    StateUpdated("Verify State:\nCfgFile are matching", null);
                else if (comparisonObject == "LogFile")
                    StateUpdated("Verify State:\nLogFIle are matching", null);
            }

            return 0;
        }

        internal int _ComparisonRegisterForSas3(string RegisterFilePath, bool onlyVerifyMode, string comparisonObject, bool engineerMode, int ch)
        {
            string fileName1 = "UpdatedModuleRegisterFile"; // Module cfg file
            string fileName2 = Path.GetFileName(RegisterFilePath); // Reference cfg file
            string filePath1;
            string filePath2 = RegisterFilePath; // Reference cfg file
            string executableFileFolderPath = Path.Combine(Application.StartupPath, "RegisterFiles");
            var masks = _GetMasks("SAS3", comparisonObject);

            if (!onlyVerifyMode) {
                if (comparisonObject == "CfgFile")
                    StateUpdated("Verify State:\nCfgFile check...", 93);
                else if (comparisonObject == "LogFile")
                    StateUpdated("Verify State:\nLogFIle check...", 93);
            }
            else {
                if (comparisonObject == "CfgFile")
                    StateUpdated("Verify State:\nCfgFile check...", null);
                else if (comparisonObject == "LogFile")
                    StateUpdated("Verify State:\nLogFIle check...", null);
            }

            // Export current module register file
            if (_ExportModuleCfgForSas3(fileName1, comparisonObject, ch) < 0)
                return -1;

            filePath1 = Path.Combine(executableFileFolderPath, fileName1 + ".csv");
            _ReformatedCsvFileForSas3(filePath1, 1, executableFileFolderPath, comparisonObject, ch); //Get part of data from the module for comparison
            _ReformatedCsvFileForSas3(filePath2, 2, executableFileFolderPath, comparisonObject, ch); //Get part of data from CfgFile for comparison
            filePath1 = Path.Combine(executableFileFolderPath, "temp1_" + fileName1 + ".csv");
            filePath2 = Path.Combine(executableFileFolderPath, "temp2_" + fileName2);
            DataTable dt1 = _ReadCsvToDataTable(filePath1);
            DataTable dt2 = _ReadCsvToDataTable(filePath2);


            _RemoveDoubleQuotes(dt1);
            _RemoveDoubleQuotes(dt2);
            _ApplyMask(new List<DataTable> { dt1, dt2 }, masks);


            // Error alarm, if there are differences
            if (!_CompareDataTables(dt1, dt2))
            {
                //MessageBox.Show("Verify Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (engineerMode || onlyVerifyMode)
                    _DisplayDifferencesInGrid(dt1, dt2, masks); // EngineerCheck from DataGridView
                else
                    MessageBox.Show("There are differences between the module CfgFile and the target CfgFile.",
                                    "Error alarm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                StateUpdated("Verify State:\nVerify failed", null);
                return 1;
            }
            if (!_CompareDataTables(dt1, dt2))
            {
                if (engineerMode || onlyVerifyMode)
                    _DisplayDifferencesInGrid(dt1, dt2, masks);
                else
                    MessageBox.Show("There are differences...");
                return 1;
            }

            if (onlyVerifyMode)
            {
                _DisplayDifferencesInGrid(dt1, dt2, masks);
            }
            //MessageBox.Show("Verify Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (File.Exists(filePath1))
                File.Delete(filePath1);

            if (File.Exists(filePath2))
                File.Delete(filePath2);

            if (!onlyVerifyMode) {
                if (comparisonObject == "CfgFile")
                    StateUpdated("Verify State:\nCfgFile are matching", 97);
                else if (comparisonObject == "LogFile")
                    StateUpdated("Verify State:\nLogFIle are matching", 97);
            }
            else {
                if (comparisonObject == "CfgFile")
                    StateUpdated("Verify State:\nCfgFile are matching", null);
                else if (comparisonObject == "LogFile")
                    StateUpdated("Verify State:\nLogFIle are matching", null);
            }

            return 0;
        }

        internal int _ComparisonRegisterForFinalCheck(string modelType, string RegisterFilePath, string comparisonObject, bool engineerMode)
        {
            string fileName1 = "UpdatedModuleRegisterFile"; // Module cfg file
            string fileName2 = Path.GetFileName(RegisterFilePath); // Reference cfg file
            string filePath1;
            string filePath2 = RegisterFilePath; // Reference cfg file
            string executableFileFolderPath = Path.Combine(Application.StartupPath, "RegisterFiles");
            var masks = _GetMasks("FQC", comparisonObject);

            StateUpdated("Verify State:\n " + comparisonObject + "check...", null);

            // Export current module register file
            if (_ExportModuleCfg(modelType, fileName1, comparisonObject) < 0)
                return -1;

            filePath1 = Path.Combine(executableFileFolderPath, fileName1 + ".csv");
            _ReformatedCsvFileForFinalCheck(filePath1, 1, executableFileFolderPath, comparisonObject);
            _ReformatedCsvFileForFinalCheck(filePath2, 2, executableFileFolderPath, comparisonObject);
            filePath1 = Path.Combine(executableFileFolderPath, "temp1_" + fileName1 + ".csv");
            filePath2 = Path.Combine(executableFileFolderPath, "temp2_" + fileName2);
            DataTable dt1 = _ReadCsvToDataTable(filePath1);
            DataTable dt2 = _ReadCsvToDataTable(filePath2);


            _RemoveDoubleQuotes(dt1);//Module
            _RemoveDoubleQuotes(dt2);//Cfg
            _ApplyMask(new List<DataTable> { dt1, dt2 }, masks);

            // Error alarm, if there are differences
            if (!_CompareDataTables(dt1, dt2)) {
                //MessageBox.Show("Verify Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (engineerMode)
                    _DisplayDifferencesInGrid(dt1, dt2, masks); // EngineerCheck from DataGridView
                else
                MessageBox.Show("There are differences between the module CfgFile and the target CfgFile.",
                                "Error alarm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                StateUpdated("Verify State:\nVerify failed", null);
                return 1;
            }

            // Delete the temp file, if there are no errors
            if (File.Exists(filePath1))
                File.Delete(filePath1);

            if (File.Exists(filePath2))
                File.Delete(filePath2);

            
            StateUpdated("Verify State:\n" + comparisonObject + "are matching", null);

            return 0;
        }

        private List<(string page, int row, int[] columns)> _GetMasks (string products, string comparisonObject)
        {
            if (string.IsNullOrEmpty(comparisonObject)) comparisonObject = "CfgFile";
            if ((products == "SAS4" || products == "SAS4.0") && comparisonObject == "CfgFile") {
                return new List <(string page, int row, int[] columns)> {
                    ("Low Page", 00, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Low Page", 10, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Low Page", 20, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Low Page", 30, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }),
                    ("Low Page", 40, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Low Page", 50, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Low Page", 60, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 15}),
                    ("Low Page", 70, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Up 00h", 40, new[] {4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Up 00h", 50, new[] {0, 1, 2, 3, 4, 5, 6, 7 ,8 ,9 ,10 ,11, 15})
                };
            }
            else if ((products == "SAS4" || products == "SAS4.0") && comparisonObject == "LogFile") {
                return new List<(string page, int row, int[] columns)> {
                    ("Low Page", 00, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Low Page", 10, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Low Page", 20, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Low Page", 30, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }),
                    ("Low Page", 40, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Low Page", 50, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Low Page", 60, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 15}),
                    ("Low Page", 70, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Up 00h", 40, new[] {4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Up 00h", 50, new[] {0, 1, 2, 3, 4, 5, 6, 7 ,8 ,9 ,10 ,11, 15}),
                    ("80h", 70, new[] {12, 13, 14, 15}),
                    ("81h", 70, new[] {15})
                };
            }
            if ((products == "SAS3" || products == "SAS3.0") && comparisonObject == "CfgFile")
            {
                return new List<(string page, int row, int[] columns)> {
                    ("Page 00", 30, new[] {15}), //Checksum (Address 63)
                    // Vendor SN & Name
                    ("Page 00", 40, new[] {4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Page 00", 50, new[] {0, 1, 2, 3, 4, 5, 6, 7 ,8 ,9 ,10 ,11, 15}),
                    ("Page 00", 70, new[] {15}), //Extended Checksum (Address 127)
                    ("Page 6C", 00, new[] {0, 1, 2, 3, 4, 5, 6, 7 ,8 ,9 ,10 ,11, 12, 13, 14, 15}) // Eye Mask / Calibration
                };
            }
            else if ((products == "SAS3" || products == "SAS3.0") && comparisonObject == "LogFile") {
                return new List<(string page, int row, int[] columns)> {
                    ("Page 00", 70, new[] {15})
                };
            }
            if (products == "FQC") {
                if (comparisonObject == "Low Page") {
                    return new List<(string page, int row, int[] columns)> {
                    ("Low Page", 00, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Low Page", 10, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Low Page", 20, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Low Page", 30, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Low Page", 40, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Low Page", 50, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Low Page", 60, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11}),
                    ("Low Page", 70, new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                };
                }

                else if (comparisonObject == "UpPage00") {
                    return new List<(string page, int row, int[] columns)>
                    {
                    ("Up 00h", 40, new[] {4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("Up 00h", 50, new[] {0, 1, 2, 3, 4, 5, 6, 7 ,8 ,9 ,10 ,11, 15})
                    };
                }
                else if (comparisonObject == "Page03") {
                    return new List<(string page, int row, int[] columns)>
                    {
                    ("Up 03h", 70, new[] {14, 15}),
                    };
                }
                else if (comparisonObject == "Page81") {
                    return new List<(string page, int row, int[] columns)>
                    {
                    ("81h", 50, new[] {12, 13, 14, 15}),
                    ("81h", 60, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    ("81h", 70, new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}),
                    };
                }
                else if (comparisonObject == "PageTx") {
                    return new List<(string page, int row, int[] columns)>
                    {
                    ("Tx", 70, new[] {15}),
                    };
                }
                else if (comparisonObject == "PageRx") {
                    return new List<(string page, int row, int[] columns)>
                    {
                    ("Rx", 70, new[] {15}),
                    };
                }
                else {
                    return new List<(string page, int row, int[] columns)>
                    {
                    ("Up 00h", 70, new[] {15}),
                    };
                }
            }

            return new List<(string page, int row, int[] columns)> {
                ("80h", 70, new[] {12, 13, 14, 15})
            };
        }

        private void _ReformatedCsvFile(string FilePath, int fineNumber, string tempFilePath, string comparisonObject)
        {
            int NumberOfSearchRows;

            switch (comparisonObject) {
                case "CfgFile":
                    NumberOfSearchRows = 24; // Low Page, Up00h, 03h 8*3 rows
                    break;

                case "LogFile":
                    NumberOfSearchRows = 40; // Low Page, Up00h, 03h, 80h, 81h, 8x5=40 rows.
                    break;

                default:
                    NumberOfSearchRows = 72; // Low Page, Up00h, 03h, 80h, 81h, 8x5=40 rows. with Rx/TxCfg (16x2)=40+32=72 rows.
                    break;
            }

            if (fineNumber == 1)
                tempFilePath = Path.Combine(tempFilePath, "temp1_" + Path.GetFileName(FilePath));
            else if (fineNumber == 2)
                tempFilePath = Path.Combine(tempFilePath, "temp2_" + Path.GetFileName(FilePath));
            else
                return;

            if (File.Exists(tempFilePath))
                File.Delete(tempFilePath);

            using (StreamReader reader = new StreamReader(FilePath))
            using (StreamWriter writer = new StreamWriter(tempFilePath)) {
                string line;
                bool isHeaderFound = false;
                int rowCount = 0;

                while ((line = reader.ReadLine()) != null) {
                    if (!isHeaderFound && line.StartsWith("Page,Row")) {
                        isHeaderFound = true;
                        writer.WriteLine(line);
                        break;
                    }
                }

                while ((line = reader.ReadLine()) != null) {
                    if (comparisonObject == "CfgFile") {
                        if (line.Contains("\"Low Page\"") ||
                            line.Contains("\"Up 00h\"") || 
                            line.Contains("\"Up 03h\"")) {
                            writer.WriteLine(line);
                            rowCount++;
                        }
                    }
                    else {
                        if (line.Contains("\"Low Page\"") ||
                            line.Contains("\"Up 00h\"") ||
                            line.Contains("\"Up 03h\"") ||
                            line.Contains("\"80h\"") ||
                            line.Contains("\"81h\"") ||
                            line.Contains("\"Rx\"") ||
                            line.Contains("\"Tx\"")) {
                            writer.WriteLine(line);
                            rowCount++;
                        }
                    }

                    if (rowCount >= NumberOfSearchRows) {
                        break;
                    }
                }
            }
        }

        private void _ReformatedCsvFileForSas3(string FilePath, int fineNumber, string tempFilePath, string comparisonObject, int ch)
        {
            int NumberOfSearchRows;

            if (comparisonObject == "LogFile")
                NumberOfSearchRows = 64; // Page 00, 03, 3A, 5D, 6C, 70, 73, 7B, -7E, -7F 8x10=80 rows.
            else
                NumberOfSearchRows = 48; // Page 00, 03, 3A, 6C, 70, 73 8x6 rows

            if (fineNumber == 1) // for module file
                tempFilePath = Path.Combine(tempFilePath, "temp1_" + Path.GetFileName(FilePath));
            else if (fineNumber == 2) // for Cfg file
                tempFilePath = Path.Combine(tempFilePath, "temp2_" + Path.GetFileName(FilePath));
            else
                return;

            if (File.Exists(tempFilePath))
                File.Delete(tempFilePath);

            using (StreamReader reader = new StreamReader(FilePath))
            using (StreamWriter writer = new StreamWriter(tempFilePath)) {
                string line;
                bool isHeaderFound = false;
                int rowCount = 0;

                while ((line = reader.ReadLine()) != null) {
                    if (!isHeaderFound && line.StartsWith("Page,Row")) {
                        isHeaderFound = true;
                        writer.WriteLine(line);
                        break;
                    }
                }

                while ((line = reader.ReadLine()) != null) {
                    string[] columns = line.Split(',');

                    if (ch == 1) {
                        if (columns.Length > 0) {
                            columns[0] = columns[0].Replace("A_", "");
                            line = string.Join(",", columns);
                        }
                    }
                    if (ch == 2) {
                        if (columns.Length > 0) {
                            columns[0] = columns[0].Replace("B_", "");
                            line = string.Join(",", columns);
                        }
                    }

                    if (comparisonObject == "CfgFile") {
                        if (line.Contains("\"Page 00\"") || line.Contains("\"Page 03\"") ||
                            line.Contains("\"Page 3A\"") || line.Contains("\"Page 6C\"") ||
                            line.Contains("\"Page 70\"") || line.Contains("\"Page 73\"")) {
                            writer.WriteLine(line);
                            rowCount++;
                        }
                    }
                    else {
                        if (line.Contains("\"Page 00\"") || line.Contains("\"Page 03\"") ||
                            line.Contains("\"Page 3A\"") || line.Contains("\"Page 5D\"") ||
                            line.Contains("\"Page 6C\"") || line.Contains("\"Page 70\"") ||
                            line.Contains("\"Page 73\"") || line.Contains("\"Page 7B\"")) {
                            writer.WriteLine(line);
                            rowCount++;
                        }
                    }

                    if (rowCount >= NumberOfSearchRows) {
                        break;
                    }
                }
                if (DebugMode) {
                    MessageBox.Show("Get data from: \n" + tempFilePath +
                                "\n\n Task type: " + comparisonObject + 
                                "\n Getdata rowCount: " + rowCount);
                }
            }
        }

        private void _ReformatedCsvFileForFinalCheck(string FilePath, int fineNumber, string tempFilePath, string comparisonObject)
        {
            int NumberOfSearchRows;

            if (comparisonObject == "UpPage00" || comparisonObject == "Page03" ||
                comparisonObject == "Page80" || comparisonObject == "Page81")
                NumberOfSearchRows = 8; // Up00h, 03h, 80h, 81h
            else
                NumberOfSearchRows = 16; // Tx, Rx rows

            if (fineNumber == 1)
                tempFilePath = Path.Combine(tempFilePath, "temp1_" + Path.GetFileName(FilePath));
            else if (fineNumber == 2)
                tempFilePath = Path.Combine(tempFilePath, "temp2_" + Path.GetFileName(FilePath));
            else
                return;

            if (File.Exists(tempFilePath))
                File.Delete(tempFilePath);

            using (StreamReader reader = new StreamReader(FilePath))
            using (StreamWriter writer = new StreamWriter(tempFilePath)) {
                string line;
                bool isHeaderFound = false;
                int rowCount = 0;

                while ((line = reader.ReadLine()) != null) {
                    if (!isHeaderFound && line.StartsWith("Page,Row")) {
                        isHeaderFound = true;
                        writer.WriteLine(line);
                        break;
                    }
                }

                while ((line = reader.ReadLine()) != null) {
                    if (comparisonObject == "UpPage00") {
                        if (line.Contains("\"Up 00h\"")) {
                            writer.WriteLine(line);
                            rowCount++;
                        }
                    }
                    else if (comparisonObject == "Page03") {
                        if (line.Contains("\"Up 03h\"")) {
                            writer.WriteLine(line);
                            rowCount++;
                        }
                    }
                    else if (comparisonObject == "Page80") {
                        if (line.Contains("\"80h\"")) {
                            writer.WriteLine(line);
                            rowCount++;
                        }
                    }
                    else if (comparisonObject == "Page81") {
                        if (line.Contains("\"81h\"")) {
                            writer.WriteLine(line);
                            rowCount++;
                        }
                    }
                    else if (comparisonObject == "PageTx") {
                        if (line.Contains("\"Tx\"")) {
                            writer.WriteLine(line);
                            rowCount++;
                        }
                    }
                    else if (comparisonObject == "PageRx") {
                        if (line.Contains("\"Rx\"")) {
                            writer.WriteLine(line);
                            rowCount++;
                        }
                    }
                    else {
                        MessageBox.Show("This page has not been defined yet\n" + "ComparisonObject: " + comparisonObject);
                    }

                    if (rowCount >= NumberOfSearchRows) {
                        break;
                    }
                }
            }
        }

        private void _DisplayDifferencesInGrid(DataTable dt1, DataTable dt2, List<(string page, int row, int[] columns)> masks)
        {
            Form form = new Form {
                Text = "Differences",
                Width = 1300,
                Height = 600,
                Font = new Font("Times New Roman", 8)
            };

            DataGridView dgv = new DataGridView {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                Font = new Font("Times New Roman", 8),
                EnableHeadersVisualStyles = false
            };

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 8, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Silver;

            foreach (DataColumn col in dt1.Columns) {
                DataGridViewColumn newCol;
                if (col.ColumnName != "Page" && col.ColumnName != "Row") {
                    newCol = dgv.Columns[dgv.Columns.Add(col.ColumnName + "_dt1", "M" + col.ColumnName)];
                    newCol.Width = 35;
                    newCol = dgv.Columns[dgv.Columns.Add(col.ColumnName + "_dt2", "F" + col.ColumnName)];
                    newCol.Width = 35;
                }
                else if (col.ColumnName == "Row") {
                    newCol = dgv.Columns[dgv.Columns.Add(col.ColumnName, col.ColumnName)];
                    newCol.Width = 35;
                    newCol.DefaultCellStyle.Font = new Font("Times New Roman", 8, FontStyle.Bold);
                    newCol.DefaultCellStyle.BackColor = Color.Silver;
                }
                else if (col.ColumnName == "Page") {
                    newCol = dgv.Columns[dgv.Columns.Add(col.ColumnName, col.ColumnName)];
                    newCol.Width = 50;
                    newCol.DefaultCellStyle.BackColor = Color.Gray;
                }
            }

            for (int row = 0; row < dt1.Rows.Count; row++) {
                dgv.Rows.Add();
                for (int col = 0; col < dt1.Columns.Count; col++) {
                    if (col == 0 || col == 1) {
                        dgv.Rows[row].Cells[col].Value = dt1.Rows[row][col];
                    }
                    else {
                        string dt1Value = dt1.Rows[row][col].ToString();
                        string dt2Value = dt2.Rows[row][col].ToString();
                        dgv.Rows[row].Cells[col * 2 - 2].Value = dt1Value;

                        if (dt1Value == dt2Value) {
                            dgv.Rows[row].Cells[col * 2 - 1].Value = "";
                        }
                        else {
                            dgv.Rows[row].Cells[col * 2 - 1].Value = dt2Value;
                            dgv.Rows[row].Cells[col * 2 - 2].Style.BackColor = Color.Red;
                            dgv.Rows[row].Cells[col * 2 - 1].Style.BackColor = Color.Yellow;
                        }

                        // 對應Mask address著色...
                        //if (IsMasked(dgv.Rows[row].Cells[0].Value.ToString(), Convert.ToInt32(dgv.Rows[row].Cells[1].Value.ToString(), 16), col - 2, masks)) {
                        if (IsMasked(dgv.Rows[row].Cells[0].Value.ToString(), Convert.ToInt32(dgv.Rows[row].Cells[1].Value), col - 2, masks)) {
                            dgv.Rows[row].Cells[col * 2 - 2].Style.BackColor = Color.Black;
                            dgv.Rows[row].Cells[col * 2 - 1].Style.BackColor = Color.Black;
                        }
                    }
                }
            }

            form.Controls.Add(dgv);
            form.ShowDialog();
        }

        private void _DisplayCurrentRegister(DataTable dt1, List<(string page, int row, int[] columns)> masks)
        {
            Form form = new Form {
                Text = "Differences",
                Width = 1000,
                Height = 600,
                Font = new Font("Times New Roman", 8)
            };

            DataGridView dgv = new DataGridView {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                Font = new Font("Times New Roman", 8),
                EnableHeadersVisualStyles = false
            };

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 8, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Silver;

            foreach (DataColumn col in dt1.Columns) {
                DataGridViewColumn newCol;
                if (col.ColumnName != "Page" && col.ColumnName != "Row") {
                    newCol = dgv.Columns[dgv.Columns.Add(col.ColumnName, col.ColumnName)];
                    newCol.Width = 50;
                }
                else if (col.ColumnName == "Row") {
                    newCol = dgv.Columns[dgv.Columns.Add(col.ColumnName, col.ColumnName)];
                    newCol.Width = 35;
                    newCol.DefaultCellStyle.Font = new Font("Times New Roman", 8, FontStyle.Bold);
                    newCol.DefaultCellStyle.BackColor = Color.Silver;
                }
                else if (col.ColumnName == "Page") {
                    newCol = dgv.Columns[dgv.Columns.Add(col.ColumnName, col.ColumnName)];
                    newCol.Width = 50;
                    newCol.DefaultCellStyle.BackColor = Color.Gray;
                }
            }

            for (int row = 0; row < dt1.Rows.Count; row++) {
                dgv.Rows.Add();
                for (int col = 0; col < dt1.Columns.Count; col++) {
                    dgv.Rows[row].Cells[col].Value = dt1.Rows[row][col];

                    // Mask邏輯著色
                    if (col > 1) // 忽略 "Page" 和 "Row" 列
                    {
                        string pageValue = dgv.Rows[row].Cells[0].Value?.ToString();
                        string rowValue = dgv.Rows[row].Cells[1].Value?.ToString();
                        if (int.TryParse(rowValue, out int rowNumber)) {
                            if (IsMasked(pageValue, rowNumber, col - 2, masks)) {
                                dgv.Rows[row].Cells[col].Style.BackColor = Color.Black;
                            }
                        }
                        else {
                            Debug.WriteLine($"無效的 Row 值: {rowValue}");
                        }
                    }
                }
            }

            form.Controls.Add(dgv);
            form.ShowDialog();
        }

        private bool IsMasked(string page, int row, int col, List<(string page, int row, int[] columns)> masks)
        {
            foreach (var mask in masks) {
                if (mask.page == page && mask.row == row && mask.columns.Contains(col)) {
                    return true;
                }
            }
            return false;
        }

        private void _ApplyMask(List<DataTable> tables, List<(string page, int row, int[] columns)> masks)
        {
            foreach (var mask in masks) {
                foreach (var table in tables) {
                    foreach (DataRow row in table.Rows) {
                        if (row["Page"].ToString() == mask.page && Convert.ToInt32(row["Row"]) == mask.row) {
                            foreach (int columnIndex in mask.columns) {
                                row[columnIndex + 2] = "FF";
                            }
                        }
                    }
                }
            }
        }

        private void _RemoveDoubleQuotes(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows) {
                foreach (DataColumn column in dataTable.Columns) {
                    if (row[column] is string) {
                        row[column] = ((string)row[column]).Replace("\"", ""); // 替換雙引號
                    }
                }
            }
        }

        private string DataTableToString(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return "Empty DataTable";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // Add column names
            foreach (DataColumn column in dt.Columns) {
                sb.Append(column.ColumnName).Append("\t");
            }
            sb.AppendLine();

            // Add rows
            foreach (DataRow row in dt.Rows) {
                foreach (var item in row.ItemArray) {
                    sb.Append(item.ToString()).Append("\t");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private DataTable _ReadCsvToDataTable(string filePath)
        {
            DataTable dt = new DataTable();
            if (!File.Exists(filePath)) return dt;
            using (StreamReader sr = new StreamReader(filePath)) {
                string headerLine = sr.ReadLine();
                if (string.IsNullOrEmpty(headerLine)) return dt;
                string[] headers = headerLine.Split(',');
                var columnCounts = new Dictionary<string, int>();

                foreach (string header in headers)
                {
                    string columnName = header.Trim();
                    if (columnCounts.ContainsKey(columnName))
                    {
                        columnCounts[columnName]++;
                        columnName = columnName + "_" + columnCounts[columnName];
                    }
                    else
                    {
                        columnCounts[columnName] = 1;
                    }
                    dt.Columns.Add(columnName);
                }
                while (!sr.EndOfStream) {
                    string line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                    string[] rows = line.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length && i < rows.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        private bool _CompareDataTables(DataTable dt1, DataTable dt2)
        {
            if (dt1.Rows.Count != dt2.Rows.Count || dt1.Columns.Count != dt2.Columns.Count) {
                return false;
            }

            for (int i = 0; i < dt1.Rows.Count; i++) {
                for (int j = 0; j < dt1.Columns.Count; j++) {
                    if (!dt1.Rows[i][j].Equals(dt2.Rows[i][j])) {
                        return false;
                    }
                }
            }
            return true;
        }

        private void bGlobalWrite_Click(object sender, EventArgs e)
        {
            loadingForm.Show(this);
            _DisableButtons();
            _InitialStateBar();

            _GlobalWriteFromUi(false);
            StoreIntoFlashApi();
            _EnableButtons();
            loadingForm.Close();
        }

        private void bStoreIntoFlash_Click(object sender, EventArgs e)
        {
            _DisableButtons();
            StoreIntoFlashApi();
            _EnableButtons();
        }

        private void bSaveCfgFile_Click(object sender, EventArgs e)
        {
            loadingForm.Show(this);
            string modelType = cbProductSelect.Text;
            string LogFileName = "EngRegisterFile";
            lastUsedDirectory = Path.Combine(Application.StartupPath, "RegisterFiles");
            _DisableButtons();
            // Save the file 
            _GlobalWriteFromUi(false);
            _InitialStateBar();

            _ExportLogfile(modelType, LogFileName, false, false);
            _EnableButtons();
            loadingForm.Close();
        }

        private void bLoadAllFromCfgFile_Click(object sender, EventArgs e)
        {
            loadingForm.Show(this);
            string DirectoryPath = Path.Combine(Application.StartupPath, "RegisterFiles");
            string RegisterFileName = Path.Combine(DirectoryPath, "EngRegisterFile.csv");

            _DisableButtons();

            progressBar1.Value = 0;
            _ConnectI2c();
            Thread.Sleep(500);
            _ConnectI2c();

            //Write data from RegisterFile
            progressBar1.Value = 5;
            WriteRegisterPageApi("Up 00h", 10, RegisterFileName);
            progressBar1.Value = 10;
            WriteRegisterPageApi("Up 03h", 10, RegisterFileName);
            progressBar1.Value = 20;
            WriteRegisterPageApi("80h", 200, RegisterFileName);
            progressBar1.Value = 30;
            WriteRegisterPageApi("81h", 200, RegisterFileName);
            progressBar1.Value = 40;
            WriteRegisterPageApi("Rx", 1000, RegisterFileName);
            progressBar1.Value = 50;
            WriteRegisterPageApi("Tx", 1000, RegisterFileName);
            progressBar1.Value = 60;

            StoreIntoFlashApi();
            progressBar1.Value = 80;

            _ConnectI2c();
            Thread.Sleep(500);
            _ConnectI2c();

            progressBar1.Value = 90;
            _InitialStateBar();
            _GlobalRead();

            progressBar1.Value = 100;

            _EnableButtons();
            loadingForm.Close();
        }

        private void bReNew_Click(object sender, EventArgs e)
        {
            _DisableButtons();

            dtWriteConfig.Clear();
            
            _EnableButtons();
        }

        private void bScanComponents_Click(object sender, EventArgs e)
        {
            if (bScanComponents.Enabled)
                bScanComponents.Enabled = false;

            _GenerateXmlFileFromUcComponents();
            //_GenerateXmlFileForProject();
            bScanComponents.Enabled = true;
        }

        private void cbProductSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            _EnableIcConfig();
            _UpdateTabPageVisibility();
        }

        private void cbPermission_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigUiByXmlApi("settings.xml");
        }

        private void _MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _I2cMasterDisconnect();

            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void rbCustomerMode_CheckedChanged(object sender, EventArgs e)
        {
            _GenerateCfgButtonState(1, false);
        }

        private void rbMpMode_CheckedChanged(object sender, EventArgs e)
        {
            _GenerateCfgButtonState(1, false);
        }

        private void bBackToMainForm_Click(object sender, EventArgs e)
        {
            Application.Restart();
            var process = Process.GetCurrentProcess();
            process.WaitForInputIdle();
            SetForegroundWindow(process.MainWindowHandle);
        }
        
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        
        private void cbContinuousMode_CheckedChanged(Object sender, EventArgs e)
        {
            MessageBox.Show("Function to be confirmed");
        }

        private void bGenerateCfg_Click(object sender, EventArgs e)
        {
            _DisableButtons();
            _GenerateXmlFileForProject();
            _EnableButtons();
        }

        private void bSas3GenerateCfg_Click(object sender, EventArgs e)
        {
            _DisableButtons();
            _GenerateXmlFileForSas3();
            _EnableButtons();
        }

        private void cbConnected_CheckedChanged(object sender, EventArgs e)
        {
            _DisableButtons();

            if (FirstRound)
            {
                ProcessingChannel = 1;
                FirstRound = false;
            }

            _ConnectI2c();
            _EnableButtons();
        }

        private void _ConnectI2c()
        {
            if (cbConnect.Checked == true) {
                _I2cMasterConnect(ProcessingChannel);
                _WriteModulePassword();
                _ChannelSet(ProcessingChannel);
                _UpdateButtonState();
                gbChannelSwitcher.Enabled = true;
            }
            else {
                _I2cMasterDisconnect();
                gbChannelSwitcher.Enabled = false;
            }
        }

        private void bIcpConnect_Click(object sender, EventArgs e)
        {
            //bIcpConnect.Enabled = false;

            ucNuvotonIcpTool.IcpConnectApi();

            //if (!bIcpConnect.Enabled)
            bIcpConnect.Enabled = true;
        }

        private void cbAutoReconnect_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoReconnect.Checked)
                _SetAutoReconnectControl(true);
            else if (!cbAutoReconnect.Checked)
                _SetAutoReconnectControl(false);
        }

        private void bAutoReconnectStateCheck_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ucNuvotonIcp\nAutoReconnectMode: " + _GetAutoReconnectControl());
        }
        
        private void cbBypassEraseAllCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBypassEraseAllCheck.Checked)
                _SetBypassEraseAllControl(true);
            else if (!cbBypassEraseAllCheck.Checked)
                _SetBypassEraseAllControl(false);
        }

        private void bBypassEraseAllStateCheck_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ucNuvotonIcp\nBypassEraseAllMode: " + _GetBypassEraseAllControl());
        }

        private void cbAPPath_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAPPath.Checked) {
                _LoadFilesPathForBin("APROM");
                _GenerateCfgButtonState(1, false);
            }
            else if (!cbAPPath.Checked) {
                APROMPath = "";
                _GenerateCfgButtonState(1, false);
            }
        }

        private void cbDAPath_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDAPath.Checked) {
                _LoadFilesPathForBin("DATAROM");
                _GenerateCfgButtonState(1, false);
            }
            else if (!cbDAPath.Checked) {
                DATAROMPath = "";
                _GenerateCfgButtonState(1, false);
            }
        }

        private void cbASidePath_CheckedChanged(object sender, EventArgs e)
        {
            if (cbASidePath.Checked) {
                _LoadFilesPathForText("ASide");
                _GenerateCfgButtonState(2, false);
            }
            else if (!cbASidePath.Checked) {
                ASidePath = "";
                _GenerateCfgButtonState(2, false);
            }
        }

        private void cbBSidePath_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBSidePath.Checked) {
                _LoadFilesPathForText("BSide");
                _GenerateCfgButtonState(2, true);
            }
            else if (!cbBSidePath.Checked) {
                BSidePath = "";
                _GenerateCfgButtonState(2, false);
            }
        }

        private void bSas3Password_Click(object sender, EventArgs e)
        {
            _SetSas3Password();
        }

        public void _SetSas3Password()
        {
            if (tbPasswordB0 != null) tbPasswordB0.Text = "1A";
            if (tbPasswordB1 != null) tbPasswordB1.Text = "58";
            if (tbPasswordB2 != null) tbPasswordB2.Text = "1A";
            if (tbPasswordB3 != null) tbPasswordB3.Text = "58";
        }

        private void bMini58Password_Click(object sender, EventArgs e)
        {
            _SetSas3Password();
        }

        private void cbBothSupplyMode_CheckedChanged(object sender, EventArgs e)
        {
            BothSupplyMode = cbBothSupplyMode.Checked;
            _ChannelSet(GetChannelControl(ProcessingChannel));
        }

        private void rbSas3CustomerCheckMode_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCh1.Checked)
                ChannelSetApi(1);
            else
                ChannelSetApi(0);
        }

        private void cbCh2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCh2.Checked)
                ChannelSetApi(2);
            else
                ChannelSetApi(0);
        }

        private void cbAllCh_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllCh.Checked)
                ChannelSetApi(13);
            else
                ChannelSetApi(0);
        }
        private void _EnableQCMode()
        {
            if (!this.Text.Contains("[QC MODE]"))
            {
                this.Text += " [QC MODE]";
            }
            _isQCMode = true;

            SetInnerControlStatus("bWrite", false);
            SetInnerControlStatus("bStoreIntoFlash", false);
            SetInnerControlStatus("bPasswordReset", false);
            SetInnerControlStatus("gbControlBytes", false);
            SetInnerControlStatus("gbUpPage3", false);

            if (bGlobalWrite != null)
            {
                bGlobalWrite.Enabled = false;
                bGlobalWrite.BackColor = Color.LightGray;
            }
            if (cbInfomation != null) cbInfomation.Checked = true;
            if (cbMemDump != null) cbMemDump.Checked = true;
            if (cbTxIcConfig != null) cbTxIcConfig.Checked = true;
            if (cbRxIcConfig != null) cbRxIcConfig.Checked = true;

            if (cbDdm != null)
            {
                cbDdm.Checked = false;
                cbDdm.Enabled = false;
                cbDdm.BackColor = Color.LightGray;
            }
            if (cbCorrector != null)
            {
                cbCorrector.Checked = false;
                cbCorrector.Enabled = false;
                cbCorrector.BackColor = Color.LightGray;
            }

            if (tcMain.TabPages.Contains(tabPage2))
            {
                tcMain.TabPages.Remove(tabPage2);
            }
            if (tcMain.TabPages.Contains(tabPage4))
            {
                tcMain.TabPages.Remove(tabPage4);
            }
            if (tcMain.TabPages.Contains(tabPage5))
            {
                tcMain.TabPages.Remove(tabPage5);
            }      
        }
        private void SetInnerControlStatus(string controlName, bool isEnabled)
        {
            // Find controls even if they are inside DLLs or Panels
            Control[] foundControls = this.Controls.Find(controlName, true);

            if (foundControls.Length > 0)
            {
                foreach (Control c in foundControls)
                {
                    c.Enabled = isEnabled;
                    // Change color for visual feedback
                    if (c is Button)
                    {
                        c.BackColor = isEnabled ? Color.White : Color.LightGray;
                    }
                }
            }
        }
    }

    public class ComboBoxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public override string ToString()
        {
            return Text;
        }

    }
}
