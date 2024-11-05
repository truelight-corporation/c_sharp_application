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
    public partial class UCMemoryDump: UserControl
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

        public int WriteRegisterPageForSas3Api(string targetPage, int delayTime, string registerFilePath)
        {
            if (this.InvokeRequired)
                return (int)this.Invoke(new Action(() => _WriteAllRegisterForSas3(targetPage, delayTime, registerFilePath)));
            else
                return _WriteAllRegisterForSas3(targetPage, delayTime, registerFilePath);
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
                    return 0;

                case "Up 00h":
                    pageData[0] = 0x00;
                    break;

                case "Up 01h":
                    pageData[0] = 0x01;
                    break;

                case "Up 02h":
                    pageData[0] = 0x02;
                    break;

                case "Up 03h":
                    pageData[0] = 0x03;
                    break;

                case "80h":
                    pageData[0] = 0x80;
                    break;

                case "81h":
                    pageData[0] = 0x81;
                    break;
                // For SAS3.0 module
                case "Lower":
                    return 0;

                case "Page 00":
                    pageData[0] = 0x00;
                    break;

                case "Page 01":
                    pageData[0] = 0x01;
                    break;

                case "Page 02":
                    pageData[0] = 0x02;
                    break;

                case "Page 03":
                    pageData[0] = 0x03;
                    break;

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

            if (selectedPage == "Low Page")
                starAddr = 0;
            else {
                if (_ChangePage(selectedPage) < 0)
                    goto exit;
                starAddr = 128;
            }

            if (i2cReadCB(80, starAddr, 128, data) != 128)
                goto exit;

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
                bWrite.Enabled = true;
            else
                bWrite.Enabled = true;
        }

        private int _WriteAll()
        {
            byte[] data = new byte[128];
            int i;
            byte starAddr;
            string selectedPage = cbPageSelect.SelectedItem.ToString();

            if (writePasswordCB == null)
                goto exit;

            if (i2cWriteCB == null)
                goto exit;

            if (writePasswordCB() < 0)
                goto exit;

            if (selectedPage == "Low Page")
                starAddr = 0;
            else {
                if (_ChangePage(selectedPage) < 0)
                    goto exit;
                starAddr = 128;
            }

            for (i = 0; i < 128; i++)
                data[i] = Convert.ToByte(dtMemory.Rows[i / 16].ItemArray[i % 16].ToString(), 16);

            if (i2cWriteCB(80, starAddr, 128, data) < 0)
                goto exit;

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

            //待補，檢查檔案存在與否...
            rowsToRead = bytesToRead / bytesPerRow;
            dataToWrite = ReadCsvData(filePath, targetPage, rowsToRead);

            if (DebugMode) {
                MessageBox.Show("filePath: \n" + filePath +
                            "\n\ndataToWrite: \n" + FormatDataToWrite(dataToWrite));
            }

            if (dataToWrite == null)
                return -1;

            // Prepare data to be written as byte array
            byte[] data = FormatDataForWrite(dataToWrite, bytesToRead);

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

            return PerformWriteOperation(data, targetPage, delayTime);
        }

        private int _WriteAllRegisterForSas3(string targetPage, int delayTime, string registerFilePath)
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
            dataToWrite = ReadCsvData(filePath, targetPage, rowsToRead);

            if (DebugMode) {
                MessageBox.Show("filePath: \n" + filePath +
                            "\n\ndataToWrite: \n" + FormatDataToWrite(dataToWrite));
            }

            if (dataToWrite == null)
                return -1;

            // Prepare data to be written as byte array
            byte[] data = FormatDataForWrite(dataToWrite, bytesToRead);

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

            return PerformWriteOperationForSas3(data, targetPage, delayTime);
        }

        private string FormatDataToWrite(List<string[]> dataToWrite)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var row in dataToWrite) {
                sb.AppendLine(string.Join(", ", row)); // 將每行陣列轉為逗號分隔的字串
            }

            return sb.ToString();
        }

        private List<string[]> ReadCsvData(string filePath, string targetPage, int rowsToRead)
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

        private byte[] FormatDataForWrite(List<string[]> dataToWrite, int totalBytes)
        {
            byte[] data = new byte[totalBytes]; // Dynamic range for byte array

            try {
                int index = 0;

                foreach (var row in dataToWrite) {
                    foreach (var value in row) {
                        data[index++] = Convert.ToByte(value.Trim('"'), 16);
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Error formatting data for write: {ex.Message}");
                return null;
            }

            return data;
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

            if (_ChangePage(targetPage) < 0)
                goto exit;

            starAddr = 128;
            if (i2cWriteCB(80, starAddr, 128, data) < 0)
                goto exit;

            Thread.Sleep(delayTime);

            bWrite.Enabled = true;
            return 0;

        exit:
            bWrite.Enabled = true;
            return -1;
        }

        private int PerformWriteOperationForSas3(byte[] data, string targetPage, int delayTime)
        {
            if (writePasswordCB == null)
                goto exit;

            if (i2cWriteCB == null)
                goto exit;

            if (writePasswordCB() < 0)
                goto exit;

            if (_ChangePage(targetPage) < 0)
                goto exit;

            byte starAddr = 128;
            if (i2cWriteCB(80, starAddr, 128, data) < 0)
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

    }
}
