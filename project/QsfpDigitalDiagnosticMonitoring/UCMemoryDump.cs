using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace QsfpDigitalDiagnosticMonitoring
{
    public partial class UCMemoryDump : UserControl
    {
        public delegate int I2cReadCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int I2cWriteCB(byte devAddr, byte regAddr, byte length, byte[] data);
        public delegate int WritePasswordCB();
        private I2cReadCB i2cReadCB = null;
        private I2cWriteCB i2cWriteCB = null;
        private WritePasswordCB writePasswordCB = null;
        private DataTable dtMemory = new DataTable();
        private bool DebugMode = false;

        public UCMemoryDump()
        {
            int i;

            InitializeComponent();

            for (i = 0; i < 16; i++)
                dtMemory.Columns.Add(i.ToString("X2"), typeof(String));

            for (i = 0; i < 128; i++) {
                if (i % 16 == 0) {
                    dtMemory.Rows.Add("NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA");
                }
            }
            dgvMemory.DataSource = dtMemory;
            for (i = 0; i < dgvMemory.Rows.Count; i++)
                dgvMemory.Rows[i].HeaderCell.Value = (i * 16).ToString("X2");

            cbPageSelect.Items.Add("Low Page");
            cbPageSelect.Items.Add("Up 00h");
            cbPageSelect.Items.Add("Up 01h");
            cbPageSelect.Items.Add("Up 02h");
            cbPageSelect.Items.Add("Up 03h");
            cbPageSelect.Items.Add("80h");
            cbPageSelect.Items.Add("81h");
            // For SAS3.0
            cbPageSelect.Items.Add("Page 00");
            cbPageSelect.Items.Add("Page 03");
            cbPageSelect.Items.Add("Page 3A");
            cbPageSelect.Items.Add("Page 6C");
            cbPageSelect.Items.Add("Page 70");
            cbPageSelect.Items.Add("Page 73");

            cbPageSelect.Text = "Up 00h";
        }

        public int SetI2cReadCBApi(I2cReadCB cb)
        {
            if (cb == null)
                return -1;

            i2cReadCB = new I2cReadCB(cb);

            return 0;
        }

        public int SetI2cWriteCBApi(I2cWriteCB cb)
        {
            if (cb == null)
                return -1;

            i2cWriteCB = new I2cWriteCB(cb);

            return 0;
        }

        public int SetWritePasswordCBApi(WritePasswordCB cb)
        {
            if (cb == null)
                return -1;

            writePasswordCB = new WritePasswordCB(cb);

            return 0;
        }

        public int WriteAllApi()
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _WriteAll()));
            else
                return _WriteAll();
        }

        public int ReadAllApi(string currentPage)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _ReadAll(currentPage)));
            else
                return _ReadAll(currentPage);
        }

        public string GetSerialNumberApi()
        {
            if (this.InvokeRequired)
                return (string)this.Invoke(new Action(() => _GetSerialNumber()));
            else
                return _GetSerialNumber();
        }
        
        public string GetHiddenPasswordApi()
        {
            if (this.InvokeRequired)
                return (string)this.Invoke(new Action(() => _GetHiddenPassword()));
            else
                return _GetHiddenPassword();
        }

        public int SetSerialNumberApi(string serialNumber)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _SetSerialNumber(serialNumber)));
            else
                return _SetSerialNumber(serialNumber);
        }

        public int ExportAllPagesDataApi(string exportFilePath)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _ExportAllPagesData(exportFilePath)));
            else
                return _ExportAllPagesData(exportFilePath);
        }

        public int ExportAllPagesDataForSas3Api(string exportFilePath, int processingChannel)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _ExportAllPagesDataForSas3(exportFilePath, processingChannel)));
            else
                return _ExportAllPagesDataForSas3(exportFilePath, processingChannel);
        }

        public int WriteRegisterPageApi(string targetPage, int delayTime, string registerFilePath)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _WriteAllRegister(targetPage, delayTime, registerFilePath)));
            else
                return _WriteAllRegister(targetPage, delayTime, registerFilePath);
        }
        
        public int WriteLowPagePartRegisterApi(int delayTime, string registerFilePath)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _WriteLowPagePartRegister(delayTime, registerFilePath)));
            else
                return _WriteLowPagePartRegister(delayTime, registerFilePath);
        }

        public int WriteRegisterPageForSas3Api(string targetPage, int delayTime, byte startAddr, int numberOfBytes, string registerFilePath)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _WriteAllRegisterForSas3(targetPage, delayTime, startAddr, numberOfBytes, registerFilePath)));
            else
                return _WriteAllRegisterForSas3(targetPage, delayTime, startAddr, numberOfBytes, registerFilePath);
        }

        private int _SetQsfpMode(byte mode)
        {
            byte[] data = new byte[] { 32 };

            if (i2cWriteCB == null)
                return -1;

            data[0] = 0xAA;

            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;
			
			/* sync_dominic@wood_251021: why need write 2 time? 
            if (i2cWriteCB(80, 127, 1, data) < 0)
                return -1;
			*/
			
            data[0] = mode;

            if (i2cWriteCB(80, 164, 1, data) < 0)
                return -1;

            return 0;
        }

        private int _GetQsfpMode()
        {
            byte[] data = new byte[] { 32 };
            string dataS;

            if (i2cReadCB == null)
                return -1;

            if (i2cReadCB(80, 164, 1, data) < 0)
                return -1;

            dataS = Encoding.Default.GetString(data);
            MessageBox.Show("QsfpMode: " + dataS);

            return 0;
        }

        private int _ChangePage(string selectedPage)
        {
            byte[] pageData = new byte[1];

            if (selectedPage.StartsWith("A_")) {
                selectedPage = selectedPage.Substring(2);
            }
            else if (selectedPage.StartsWith("B_")) {
                selectedPage = selectedPage.Substring(2);
            }

            if (i2cWriteCB == null)
                return -1;

            switch (selectedPage) {
                case "Low Page":
                case "Lower":
                    return 0;

                case "Up 00h":
                case "Page 00":
                    pageData[0] = 0x00;
                    break;

                case "Up 01h":
                case "Page 01":
                    pageData[0] = 0x01;
                    break;

                case "Up 02h":
                case "Page 02":
                    pageData[0] = 0x02;
                    break;

                case "Up 03h":
                case "Page 03":
                    pageData[0] = 0x03;
                    break;

                case "80h":
                    pageData[0] = 0x80;
                    break;

                case "81h":
                    pageData[0] = 0x81;
                    break;

                // For SAS3.0 module
                case "Page 3A":
                    pageData[0] = 0x3A;
                    break;

                case "Page 5D":
                    pageData[0] = 0x5D;
                    break;

                case "Page 6C":
                    pageData[0] = 0x6C;
                    break;

                case "Page 70":
                    pageData[0] = 0x70;
                    break;

                case "Page 73":
                    pageData[0] = 0x73;
                    break;

                case "Page 7B":
                    pageData[0] = 0x7B;
                    break;

                case "Page 7E":
                    pageData[0] = 0x7E;
                    break;

                case "Page 7F":
                    pageData[0] = 0x7F;
                    break;

                default:
                    MessageBox.Show("selectedPage does not match the specified switching item");
                    return -1;
            }

            if (i2cWriteCB(80, 127, 1, pageData) < 0)
                return -1;

            Thread.Sleep(200);

            if (selectedPage == "81h") {
                if (DebugMode)
                    MessageBox.Show("pageData: \n" + pageData[0].ToString());
            }

            return 0;
        }

        private string _GetHiddenPassword()
        {
            byte[] data = new byte[4];
            byte[] pageData = new byte[1];
            byte starAddr;

            if (i2cWriteCB == null)
                return null;

            if (i2cReadCB == null)
                return null;

            pageData[0] = 0x80;

            if (i2cWriteCB(80, 127, 1, pageData) < 0)
                return null;

            starAddr = 252;

            if (i2cReadCB(80, starAddr, 4, data) != 4)
                return null;

            string result = Encoding.ASCII.GetString(data);

            if (DebugMode)
                MessageBox.Show("results: " + result);

            return result;
        }

        private string _GetSerialNumber()
        {
            byte[] data = new byte[16];
            byte[] pageData = new byte[1];
            byte starAddr;

            if (i2cWriteCB == null)
                return null;

            if (i2cReadCB == null)
                return null;

            pageData[0] = 0x81;

            if (i2cWriteCB(80, 127, 1, pageData) < 0)
                return null;

            starAddr = 220;

            if (i2cReadCB(80, starAddr, 16, data) != 16)
                return null;

            string result = System.Text.Encoding.ASCII.GetString(data);

            if (DebugMode)
                MessageBox.Show("results: " + result);

            return result;
        }

        private int _SetSerialNumber(string serialNumber)
        {
            byte[] data = new byte[16];
            byte[] pageData = new byte[1];
            byte starAddr = 220;

            if (i2cWriteCB == null) {
                MessageBox.Show("i2cWriteCB failed");
                return -9;
            }

            byte[] serialBytes = System.Text.Encoding.ASCII.GetBytes(serialNumber);

            if (serialBytes.Length > 16)
                return -2;

            Array.Clear(data, 0, 16);
            Array.Copy(serialBytes, data, serialBytes.Length);
            pageData[0] = 0x81;


            if (i2cWriteCB(80, 127, 1, pageData) < 0)
                return -3;

            Thread.Sleep(100);

            if (i2cWriteCB(80, starAddr, 16, data) < 0)
                return -4;

            Thread.Sleep(100);

            return 0;
        }

        private void bRead_Click(object sender, EventArgs e)
        {
            bRead.Enabled = false;

            if (_ReadAll(null) < 0)
                bRead.Enabled = true;
            else
                bRead.Enabled = true;
        }

        private int _ReadAll(string currentPage)
        {
            byte[] data = new byte[128];
            int i;
            byte starAddr;
            string selectedPage;

            selectedPage = currentPage ?? cbPageSelect.SelectedItem.ToString();

            if (i2cReadCB == null)
                goto exit;

            if (writePasswordCB == null)
                goto exit;

            writePasswordCB();

            if (selectedPage == "Low Page")
                starAddr = 0;
            else {
                if (_ChangePage(selectedPage) < 0)
                    goto exit;
                starAddr = 128;
            }

            if (i2cReadCB(80, starAddr, 128, data) != 128)
                goto exit;

            if (selectedPage == "80h" || selectedPage == "81h")
                Thread.Sleep(200);
            else
                Thread.Sleep(100);


            for (i = 0; i < 128; i++)
                dtMemory.Rows[i / 16].SetField(i % 16, data[i].ToString("X2"));

            if (starAddr == 0)
                bWriteLowPage.Enabled = true;
            else
                bWriteLowPage.Enabled = false;

            return 0;
        exit:
            return -1;
        }

        private void bWrite_Click(object sender, EventArgs e)
        {
            bWrite.Enabled = false;

            if (_WriteAll() < 0)
                MessageBox.Show("_WriteAll < 0");

            bWrite.Enabled = true;
        }

        private int _WriteAll()
        {
            byte[] data = new byte[128];
            byte starAddr;
            string selectedPage = cbPageSelect.SelectedItem.ToString();

            if (writePasswordCB == null)
                goto exit;

            if (i2cWriteCB == null)
                goto exit;

            if (writePasswordCB() < 0)
                goto exit;

            _SetQsfpMode(0x4D);
            //_GetQsfpMode();

            if (selectedPage == "Low Page")
                starAddr = 0;
            else {
                if (_ChangePage(selectedPage) < 0)
                    goto exit;
                starAddr = 128;
            }

            for (int j = 0; j < 128; j++)
                data[j] = Convert.ToByte(dtMemory.Rows[j / 16].ItemArray[j % 16].ToString(), 16);

            if (DebugMode) {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Data:");
                for (int i = 0; i < data.Length; i++) {
                    sb.Append(data[i].ToString("X2")); // Convert byte to hex string representation
                    sb.Append(" ");
                    if ((i + 1) % 16 == 0)
                        sb.AppendLine(); // New line every 16 bytes
                }

                MessageBox.Show("81h_data: \n" + sb.ToString());
            }

            if (i2cWriteCB(80, starAddr, 128, data) < 0)
                goto exit;

            Thread.Sleep(1000);

            //ucInformation.StoreIntoFlashApi();

            return 0;

        exit:
            return -1;
        }

        private int _WriteAllRegister(string targetPage, int delayTime, string registerFilePath)
        {
            string filePath;
            int bytesToRead = 128; // 128 or 256 bytes
            int bytesPerRow = 16; // 16 bytes per row
            int rowsToRead;
            List<string[]> dataToWrite;

            if (!string.IsNullOrEmpty(registerFilePath))
                filePath = registerFilePath;
            else {
                MessageBox.Show("The file path for the write operation is incorrect or empty.");
                return -1;
            }

            rowsToRead = bytesToRead / bytesPerRow;
            dataToWrite = ExtractCsvDataByPage(filePath, targetPage, rowsToRead);

            if (DebugMode) {
                MessageBox.Show("filePath: \n" + filePath +
                            "\n\ndataToWrite: \n" + GenerateDisplayMessageFromData(dataToWrite));
            }

            if (dataToWrite == null)
                return -1;

            // Prepare data to be written as byte array
            byte[] data = FormatHexValuesForOutput(dataToWrite, bytesToRead);

            if (targetPage == "81h" || targetPage == "80h") {
                if (DebugMode) {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(targetPage + "_Data:");
                    for (int i = 0; i < data.Length; i++) {
                        sb.Append(data[i].ToString("X2")); // Convert byte to hex string representation
                        sb.Append(" ");
                        if ((i + 1) % 16 == 0)
                            sb.AppendLine(); // New line every 16 bytes
                    }

                    MessageBox.Show("filePath: \n" + filePath +
                                    "\n\ndata: \n" + sb.ToString());

                }
            }

            return PerformWriteOperation(data, targetPage, delayTime);
        }

        private int _WriteLowPagePartRegister(int delayTime, string registerFilePath)
        {
            string targetPage = "Low Page";
            int startByte = 108;
            int endByte = 112;

            if (string.IsNullOrEmpty(registerFilePath)) {
                MessageBox.Show("The file path for the write operation is incorrect or empty.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            string filePath = registerFilePath;

            if (!File.Exists(filePath)) {
                MessageBox.Show($"The specified file does not exist: {filePath}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            List<string> dataToWrite = ExtractCsvDataByPagePart(filePath, targetPage, startByte, endByte);

            if (dataToWrite == null)
                return -1;

            // Prepare data to be written as byte array
            byte[] data = FormatHexValuesForOutput(dataToWrite);

                if (DebugMode) {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(targetPage + "_Data:");
                    for (int i = 0; i < data.Length; i++) {
                        sb.Append(data[i].ToString("X2")); // Convert byte to hex string representation
                        sb.Append(" ");
                        if ((i + 1) % 16 == 0)
                            sb.AppendLine(); // New line every 16 bytes
                    }

                    MessageBox.Show("filePath: \n" + filePath +
                                    "\n\ndata: \n" + sb.ToString());

                }

            return PerformWriteOperation(data, targetPage, delayTime);
        }

        private int _WriteAllRegisterForSas3(string targetPage, int delayTime, byte startAddr, int numberOfBytes, string registerFilePath)
        {
            List<string[]> dataToWrite;
            string filePath;
            byte[] data;
            int rowsToRead;

            if (!string.IsNullOrEmpty(registerFilePath))
                filePath = registerFilePath;
            else {
                MessageBox.Show("The file path for the write operation is incorrect or empty.");
                return -1;
            }

            rowsToRead = (int)Math.Ceiling(numberOfBytes / 16.0);
            dataToWrite = ExtractCsvDataByPageForSas3(filePath, targetPage, startAddr, numberOfBytes);

            if (DebugMode) {
                MessageBox.Show("filePath: \n" + filePath +
                            "\n\ndataToWrite: \n" + GenerateDisplayMessageFromData(dataToWrite));
            }

            if (dataToWrite == null)
                return -1;

            // Prepare data to be written as byte array
            data = FormatHexValuesForOutput(dataToWrite, numberOfBytes);

            if (DebugMode) {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Data:");
                for (int i = 0; i < data.Length; i++) {
                    sb.Append(data[i].ToString("X2")); // Convert byte to hex string representation
                    sb.Append(" ");
                    if ((i + 1) % 16 == 0)
                        sb.AppendLine(); // New line every 16 bytes
                }

                MessageBox.Show(sb.ToString());
            }

            return PerformWriteOperationForSas3(data, targetPage, delayTime, startAddr);
        }

        private string GenerateDisplayMessageFromDataPart(string[] dataToWrite)
        {
            return string.Join(Environment.NewLine, dataToWrite);
        }


        // Original：List<string[]>
        private string GenerateDisplayMessageFromData(List<string[]> dataToWrite)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var row in dataToWrite) {
                sb.AppendLine(string.Join(", ", row));
            }

            return sb.ToString();
        }

        private byte[] FormatHexValuesForOutput(List<string> dataToWrite)
        {
            byte[] data = new byte[dataToWrite.Count];

            try {
                for (int i = 0; i < dataToWrite.Count; i++) {
                    if (byte.TryParse(dataToWrite[i].Trim('"'), System.Globalization.NumberStyles.HexNumber, null, out byte result)) {
                        data[i] = result;
                    }
                    else {
                        MessageBox.Show($"Invalid hex value: {dataToWrite[i]}. Skipping.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Error formatting data for write: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return data;
        }

        private byte[] FormatHexValuesForOutput(List<string[]> dataToWrite, int totalBytes)
        {
            byte[] data = new byte[totalBytes];

            try {
                int index = 0;

                foreach (var row in dataToWrite) {
                    foreach (var value in row) {
                        if (index >= totalBytes) {
                            // 已經填滿所需的位元組數，提前結束
                            break;
                        }

                        if (byte.TryParse(value.Trim('"'), System.Globalization.NumberStyles.HexNumber, null, out byte result)) {
                            data[index++] = result;
                        }
                        else {
                            MessageBox.Show($"Invalid hex value: {value}. Skipping.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    if (index >= totalBytes) {
                        // 確保外層循環也能提前結束
                        break;
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Error formatting data for write: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return data;
        }


        private List<string[]> ExtractCsvDataByPage(string filePath, string targetPage, int rowsToRead)
        {
            List<string[]> dataToWrite = new List<string[]>();

            try {
                using (var reader = new StreamReader(filePath)) {
                    bool foundHeader = false;
                    bool foundTargetPage = false;
                    int rowsRead = 0;
                    string line;

                    while ((line = reader.ReadLine()) != null) {
                        if (!foundHeader) {
                            if (line.StartsWith("Page,Row")) {
                                foundHeader = true;
                            }
                            continue;
                        }

                        string[] parts = line.Split(',');
                        if (parts.Length >= 18) {
                            if (foundTargetPage) {
                                if (rowsRead < rowsToRead) {
                                    string[] rowData = new string[16];
                                    Array.Copy(parts, 2, rowData, 0, 16); // Copy only the data columns
                                    dataToWrite.Add(rowData);
                                    rowsRead++;
                                }
                                else {
                                    break; // Read enough rows
                                }
                            }
                            else if (parts[0].Trim('"') == targetPage) {
                                foundTargetPage = true;
                                string[] rowData = new string[16];
                                Array.Copy(parts, 2, rowData, 0, 16); // Copy only the data columns
                                dataToWrite.Add(rowData);
                                rowsRead++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Error reading CSV file: {ex.Message}");
                return null;
            }

            return dataToWrite;
        }

        private List<string[]> ExtractCsvDataByPageForSas3(string filePath, string targetPage, byte startAddr, int numberOfBytes)
        {
            List<string[]> dataToWrite = new List<string[]>();

            try {
                using (var reader = new StreamReader(filePath)) {
                    bool foundHeader = false;
                    bool foundTargetPage = false;
                    int totalBytesRead = 0;
                    int rowsSkipped = (startAddr / 16) - 1; // 正確計算需要跳過的完整行數
                    int byteOffset = startAddr % 16; // 行內的偏移量
                    string line;

                    while ((line = reader.ReadLine()) != null) {
                        if (!foundHeader) {
                            if (line.StartsWith("Page,Row")) {
                                foundHeader = true;
                            }
                            continue;
                        }

                        string[] parts = line.Split(',');
                        if (parts.Length >= 18) {
                            if (foundTargetPage) {
                                if (rowsSkipped > 0) {
                                    rowsSkipped--; // 跳過不需要的行
                                    continue;
                                }

                                // 提取當前行的資料，考慮行內偏移和剩餘的需要提取位元組數
                                int availableBytes = Math.Min(16 - byteOffset, numberOfBytes - totalBytesRead);
                                if (availableBytes > 0) {
                                    string[] rowData = new string[availableBytes];
                                    Array.Copy(parts, 2 + byteOffset, rowData, 0, availableBytes);

                                    dataToWrite.Add(rowData);
                                    totalBytesRead += rowData.Length;
                                    byteOffset = 0; // 之後的行無需偏移

                                    if (totalBytesRead >= numberOfBytes) {
                                        break; // 已提取足夠資料
                                    }
                                }
                            }
                            else if (parts[0].Trim('"') == targetPage) {
                                foundTargetPage = true; // 找到目標頁
                                if (rowsSkipped == -1 && byteOffset == 0) {
                                    // 如果偏移量不為0，處理當前行資料
                                    int availableBytes = Math.Min(16 - byteOffset, numberOfBytes - totalBytesRead);
                                    string[] rowData = new string[availableBytes];
                                    Array.Copy(parts, 2 + byteOffset, rowData, 0, availableBytes);

                                    dataToWrite.Add(rowData);
                                    totalBytesRead += rowData.Length;
                                    byteOffset = 0;

                                    if (totalBytesRead >= numberOfBytes) {
                                        break; // 已提取足夠資料
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Error reading CSV file: {ex.Message}");
                return null;
            }

            return dataToWrite;
        }

        private List<string> ExtractCsvDataByPagePart(string filePath, string targetPage, int startByte, int endByte)
        {
            List<string> extractedData = new List<string>();

            try {
                using (var reader = new StreamReader(filePath)) {
                    bool foundHeader = false;
                    bool foundTargetPage = false;
                    string line;
                    int currentByteIndex = 0;

                    while ((line = reader.ReadLine()) != null) {
                        if (!foundHeader) {
                            if (line.StartsWith("Page,Row")) {
                                foundHeader = true;
                            }
                            continue;
                        }

                        string[] parts = line.Split(',');
                        if (parts.Length >= 18 && parts[0].Trim('"') == targetPage) {
                            foundTargetPage = true;
                            int rowStartByte = currentByteIndex;
                            int rowEndByte = currentByteIndex + 16 - 1;

                            // 如果行範圍和目標範圍有交集
                            if (startByte <= rowEndByte && endByte >= rowStartByte) {
                                int startColumn = Math.Max(2, startByte - rowStartByte + 2);
                                int endColumn = Math.Min(17, endByte - rowStartByte + 2);

                                for (int col = startColumn; col <= endColumn; col++) {
                                    extractedData.Add(parts[col]);
                                }
                            }

                            currentByteIndex += 16;

                            if (currentByteIndex > endByte) {
                                break;
                            }
                        }
                        else if (foundTargetPage && currentByteIndex > endByte) {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Error reading CSV file: {ex.Message}");
                return null;
            }

            return extractedData;
        }

        private byte[] FormatDataForSas3Write(List<string[]> dataToWrite, int totalBytes, byte starAddr, int numberOfBytes)
        {
            byte[] data = new byte[totalBytes];

            try {
                int index = 0;

                foreach (var row in dataToWrite) {
                    foreach (var value in row) {
                        data[index++] = Convert.ToByte(value.Trim('"'), 16);
                    }
                }

                int starAddress = (int)starAddr;
                int startIndex = starAddress - 128;

                if (startIndex < 0 || startIndex + numberOfBytes > totalBytes) {
                    MessageBox.Show("The specified starting address or number of bytes is out of range.");
                    return null;
                }

                byte[] outputData = new byte[numberOfBytes];
                Array.Copy(data, startIndex, outputData, 0, numberOfBytes);

                return outputData;
            }
            catch (Exception ex) {
                MessageBox.Show($"An error occurred during initialization of writing data.: {ex.Message}");
                return null;
            }
        }

        private int PerformWriteOperation(byte[] data, string targetPage, int delayTime)
        {
            byte starAddr;

            if (writePasswordCB == null)
                goto exit;

            if (i2cWriteCB == null)
                goto exit;

            bWrite.Enabled = false;

            if (writePasswordCB() < 0)
                goto exit;

            _SetQsfpMode(0x4D);
            //_GetQsfpMode();

            if (_ChangePage(targetPage) < 0)
                goto exit;

            if (targetPage == "81h" || targetPage == "Low Page") {
                if (DebugMode) {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Data:");
                    for (int i = 0; i < data.Length; i++) {
                        sb.Append(data[i].ToString("X2")); // Convert byte to hex string representation
                        sb.Append(" ");
                        if ((i + 1) % 16 == 0)
                            sb.AppendLine(); // New line every 16 bytes
                    }

                    MessageBox.Show(targetPage + "_data: \n" + sb.ToString());
                }
            }

            //_GetQsfpMode();

            if (targetPage == "Low Page") {
                starAddr = 108;
                if (i2cWriteCB(80, starAddr, 5, data) < 0)
                    goto exit;
            }
            else {
                starAddr = 128;
                if (i2cWriteCB(80, starAddr, 128, data) < 0)
                    goto exit;
            }

            Thread.Sleep(delayTime);

            bWrite.Enabled = true;
            return 0;

        exit:
            bWrite.Enabled = true;
            return -1;
        }

        private int MultiByteWriteOperation(string targetPage, byte regAddr, byte length, byte[] data, int delayTime)
        {
            if (writePasswordCB == null)
                goto exit;

            if (i2cWriteCB == null)
                goto exit;

            bWrite.Enabled = false;

            if (writePasswordCB() < 0)
                goto exit;

            _SetQsfpMode(0x4D);

            if (_ChangePage(targetPage) < 0)
                goto exit;

            if (DebugMode) {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Data:");
                for (int i = 0; i < data.Length; i++) {
                    sb.Append(data[i].ToString("X2")); // Convert byte to hex string representation
                    sb.Append(" ");
                    if ((i + 1) % 16 == 0)
                        sb.AppendLine(); // New line every 16 bytes
                }

                MessageBox.Show(targetPage + "_data: \n" + sb.ToString());
            }

            if (i2cWriteCB(80, regAddr, length, data) < 0)
                goto exit;

            Thread.Sleep(delayTime);

            bWrite.Enabled = true;
            return 0;

        exit:
            bWrite.Enabled = true;
            return -1;
        }

        private int PerformWriteOperationForSas3(byte[] data, string targetPage, int delayTime, byte startAddr)
        {
            byte dataLength;

            if (data.Length > 128) {
                MessageBox.Show("Data length exceeds byte limit (128).");
                return -1;
            }
            else
                dataLength = (byte)data.Length;

            if (writePasswordCB == null)
                goto exit;

            if (i2cWriteCB == null)
                goto exit;

            if (writePasswordCB() < 0)
                goto exit;

            if (_ChangePage(targetPage) < 0)
                goto exit;

            if (DebugMode) {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Data:");
                for (int i = 0; i < data.Length; i++) {
                    sb.Append(data[i].ToString("X2")); // Convert byte to hex string representation
                    sb.Append(" ");
                    if ((i + 1) % 16 == 0)
                        sb.AppendLine(); // New line every 16 bytes
                }

                MessageBox.Show(targetPage + "_data: \n" + sb.ToString());
            }
            startAddr = (byte)(startAddr + 128);

            if (i2cWriteCB(80, startAddr, dataLength, data) < 0)
                goto exit;

            Thread.Sleep(delayTime);

            bWrite.Enabled = true;
            return 0;

        exit:
            bWrite.Enabled = true;
            return -1;
        }

        private void bWriteLowPage_Click(object sender, EventArgs e)
        {
            byte[] data = { 0xA9, 0x46, 0x50, 0x54 };
            int i;

            if (i2cWriteCB == null)
                return;

            bWriteLowPage.Enabled = false;

            if (i2cWriteCB(80, 0x7B, 4, data) < 0)
                goto exit;

            data = new byte[123];

            for (i = 0; i < 123; i++)
                data[i] = Convert.ToByte(dtMemory.Rows[i / 16].ItemArray[i % 16].ToString(), 16);

            Thread.Sleep(1000);

            if (i2cWriteCB(80, 0, 123, data) < 0)
                goto exit;

            data[0] = 0x06;
            if (i2cWriteCB(80, 127, 1, data) < 0)
                goto exit;

            data[0] = 0x80;
            if (i2cWriteCB(80, 0x82, 1, data) < 0)
                goto exit;

            Thread.Sleep(100);

            MessageBox.Show("Please re-plug QSFP28 module!!");

        exit:
            bWriteLowPage.Enabled = false;
        }

        private void ExportDataTableToCsv(DataTable dt, string filePath)
        {
            StringBuilder sb = new StringBuilder();
            string directoryPath = Path.GetDirectoryName(filePath);
            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames.ToArray()));

            foreach (DataRow row in dt.Rows) {
                IEnumerable<string> fields = row.ItemArray.Select(field => "\"" + field.ToString().Replace("\"", "\"\"") + "\"");
                sb.AppendLine(string.Join(",", fields.ToArray()));
            }

            File.WriteAllText(filePath, sb.ToString());
        }

        private int _ExportAllPagesData(string exportFilePath)
        {
            DataTable dtAllPages = new DataTable();
            string[] pages = new string[] { "Low Page", "Up 00h", "Up 01h", "Up 02h", "Up 03h", "80h", "81h" };
            dtAllPages.Columns.Add("Page", typeof(String));
            dtAllPages.Columns.Add("Row", typeof(String));

            for (int col = 0; col < 16; col++)
                dtAllPages.Columns.Add(col.ToString("X2"), typeof(String));

            foreach (string page in pages) {
                string currentPage = page;

                if (_ReadAll(currentPage) < 0)
                    return -1;

                DataTable dtCurrentPage = dtMemory.Copy();

                for (int i = 0; i < 8; i++) {
                    DataRow row = dtAllPages.NewRow();
                    row["Page"] = currentPage;
                    row["Row"] = (i * 16).ToString("X2");
                    for (int j = 0; j < 16; j++) {
                        row[j.ToString("X2")] = dtCurrentPage.Rows[i][j].ToString();
                    }
                    dtAllPages.Rows.Add(row);
                }
            }

            ExportDataTableToCsv(dtAllPages, exportFilePath);
            return 0;
        }

        private int _ExportAllPagesDataForSas3(string exportFilePath, int processingChannel)
        {
            DataTable dtAllPages = new DataTable();
            string[] pages = new string[]
            {   "Low Page", "Page 00", "Page 03", "Page 3A", "Page 5D",
                "Page 6C", "Page 70", "Page 73", "Page 7B", "Page 7E", "Page 7F" };
            dtAllPages.Columns.Add("Page", typeof(String));
            dtAllPages.Columns.Add("Row", typeof(String));

            for (int col = 0; col < 16; col++)
                dtAllPages.Columns.Add(col.ToString("X2"), typeof(String));

            foreach (string page in pages) {
                string currentPage = page;
                string currentTab;

                if (processingChannel == 2)
                    currentTab = "B_" + page;
                else
                    currentTab = "A_" + page;

                if (_ReadAll(currentPage) < 0)
                    return -1;

                DataTable dtCurrentPage = dtMemory.Copy();

                for (int i = 0; i < 8; i++) {
                    DataRow row = dtAllPages.NewRow();
                    row["Page"] = currentTab;
                    row["Row"] = (i * 16).ToString("X2");
                    for (int j = 0; j < 16; j++) {
                        row[j.ToString("X2")] = dtCurrentPage.Rows[i][j].ToString();
                    }
                    dtAllPages.Rows.Add(row);
                }
            }

            ExportDataTableToCsv(dtAllPages, exportFilePath);
            return 0;
        }

        private string DataTableToString(DataTable dataTable)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // Add column headers
            foreach (DataColumn column in dataTable.Columns) {
                sb.Append(column.ColumnName + "\t");
            }
            sb.AppendLine();

            // Add row data
            foreach (DataRow row in dataTable.Rows) {
                foreach (var item in row.ItemArray) {
                    sb.Append(item.ToString() + "\t");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _GetSerialNumber();
        }
    }
}
