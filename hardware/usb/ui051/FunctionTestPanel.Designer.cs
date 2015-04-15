namespace ui051
{
    partial class FunctionTestPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lDevAddr = new System.Windows.Forms.Label();
            this.tbDevAddr = new System.Windows.Forms.TextBox();
            this.lRegAddr = new System.Windows.Forms.Label();
            this.tbRegAddr = new System.Windows.Forms.TextBox();
            this.lValue = new System.Windows.Forms.Label();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.bSignalRead = new System.Windows.Forms.Button();
            this.bSignalWrite = new System.Windows.Forms.Button();
            this.tcFunction = new System.Windows.Forms.TabControl();
            this.tpSignalRegister = new System.Windows.Forms.TabPage();
            this.tpMultiRegister = new System.Windows.Forms.TabPage();
            this.tcFunction.SuspendLayout();
            this.tpSignalRegister.SuspendLayout();
            this.SuspendLayout();
            // 
            // lDevAddr
            // 
            this.lDevAddr.AutoSize = true;
            this.lDevAddr.Location = new System.Drawing.Point(5, 10);
            this.lDevAddr.Name = "lDevAddr";
            this.lDevAddr.Size = new System.Drawing.Size(54, 12);
            this.lDevAddr.TabIndex = 0;
            this.lDevAddr.Text = "DevAddr :";
            // 
            // tbDevAddr
            // 
            this.tbDevAddr.Location = new System.Drawing.Point(65, 7);
            this.tbDevAddr.Name = "tbDevAddr";
            this.tbDevAddr.Size = new System.Drawing.Size(40, 22);
            this.tbDevAddr.TabIndex = 1;
            // 
            // lRegAddr
            // 
            this.lRegAddr.AutoSize = true;
            this.lRegAddr.Location = new System.Drawing.Point(111, 10);
            this.lRegAddr.Name = "lRegAddr";
            this.lRegAddr.Size = new System.Drawing.Size(54, 12);
            this.lRegAddr.TabIndex = 2;
            this.lRegAddr.Text = "RegAddr :";
            // 
            // tbRegAddr
            // 
            this.tbRegAddr.Location = new System.Drawing.Point(171, 7);
            this.tbRegAddr.Name = "tbRegAddr";
            this.tbRegAddr.Size = new System.Drawing.Size(40, 22);
            this.tbRegAddr.TabIndex = 3;
            // 
            // lValue
            // 
            this.lValue.AutoSize = true;
            this.lValue.Location = new System.Drawing.Point(6, 11);
            this.lValue.Name = "lValue";
            this.lValue.Size = new System.Drawing.Size(38, 12);
            this.lValue.TabIndex = 4;
            this.lValue.Text = "Value :";
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(50, 6);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(40, 22);
            this.tbValue.TabIndex = 5;
            // 
            // bSignalRead
            // 
            this.bSignalRead.Location = new System.Drawing.Point(98, 6);
            this.bSignalRead.Name = "bSignalRead";
            this.bSignalRead.Size = new System.Drawing.Size(60, 23);
            this.bSignalRead.TabIndex = 6;
            this.bSignalRead.Text = "Read";
            this.bSignalRead.UseVisualStyleBackColor = true;
            this.bSignalRead.Click += new System.EventHandler(this.bRead_Click);
            // 
            // bSignalWrite
            // 
            this.bSignalWrite.Location = new System.Drawing.Point(164, 6);
            this.bSignalWrite.Name = "bSignalWrite";
            this.bSignalWrite.Size = new System.Drawing.Size(60, 23);
            this.bSignalWrite.TabIndex = 7;
            this.bSignalWrite.Text = "Write";
            this.bSignalWrite.UseVisualStyleBackColor = true;
            // 
            // tcFunction
            // 
            this.tcFunction.Controls.Add(this.tpSignalRegister);
            this.tcFunction.Controls.Add(this.tpMultiRegister);
            this.tcFunction.Location = new System.Drawing.Point(3, 35);
            this.tcFunction.Name = "tcFunction";
            this.tcFunction.SelectedIndex = 0;
            this.tcFunction.Size = new System.Drawing.Size(628, 417);
            this.tcFunction.TabIndex = 8;
            // 
            // tpSignalRegister
            // 
            this.tpSignalRegister.Controls.Add(this.bSignalWrite);
            this.tpSignalRegister.Controls.Add(this.bSignalRead);
            this.tpSignalRegister.Controls.Add(this.lValue);
            this.tpSignalRegister.Controls.Add(this.tbValue);
            this.tpSignalRegister.Location = new System.Drawing.Point(4, 22);
            this.tpSignalRegister.Name = "tpSignalRegister";
            this.tpSignalRegister.Padding = new System.Windows.Forms.Padding(3);
            this.tpSignalRegister.Size = new System.Drawing.Size(620, 391);
            this.tpSignalRegister.TabIndex = 0;
            this.tpSignalRegister.Text = "Signal Register";
            this.tpSignalRegister.UseVisualStyleBackColor = true;
            // 
            // tpMultiRegister
            // 
            this.tpMultiRegister.Location = new System.Drawing.Point(4, 22);
            this.tpMultiRegister.Name = "tpMultiRegister";
            this.tpMultiRegister.Padding = new System.Windows.Forms.Padding(3);
            this.tpMultiRegister.Size = new System.Drawing.Size(620, 391);
            this.tpMultiRegister.TabIndex = 1;
            this.tpMultiRegister.Text = "Multi Register";
            this.tpMultiRegister.UseVisualStyleBackColor = true;
            // 
            // FunctionTestPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lDevAddr);
            this.Controls.Add(this.tcFunction);
            this.Controls.Add(this.tbDevAddr);
            this.Controls.Add(this.tbRegAddr);
            this.Controls.Add(this.lRegAddr);
            this.MaximumSize = new System.Drawing.Size(634, 455);
            this.MinimumSize = new System.Drawing.Size(634, 455);
            this.Name = "FunctionTestPanel";
            this.Size = new System.Drawing.Size(634, 455);
            this.tcFunction.ResumeLayout(false);
            this.tpSignalRegister.ResumeLayout(false);
            this.tpSignalRegister.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lDevAddr;
        private System.Windows.Forms.TextBox tbDevAddr;
        private System.Windows.Forms.Label lRegAddr;
        private System.Windows.Forms.TextBox tbRegAddr;
        private System.Windows.Forms.Label lValue;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Button bSignalRead;
        private System.Windows.Forms.Button bSignalWrite;
        private System.Windows.Forms.TabControl tcFunction;
        private System.Windows.Forms.TabPage tpSignalRegister;
        private System.Windows.Forms.TabPage tpMultiRegister;
    }
}
