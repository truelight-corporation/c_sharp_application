namespace LensAlignment
{
    partial class fLensAlignment
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
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbLightSourceConnected = new System.Windows.Forms.CheckBox();
            this.cbBeAlignmentConnected = new System.Windows.Forms.CheckBox();
            this.cbStartMonitor = new System.Windows.Forms.CheckBox();
            this.lBeAlignmentPassword = new System.Windows.Forms.Label();
            this.tbBeAlignmentPassword123 = new System.Windows.Forms.TextBox();
            this.bLog = new System.Windows.Forms.Button();
            this.bClearLog = new System.Windows.Forms.Button();
            this.lLogLable = new System.Windows.Forms.Label();
            this.tbLogLable = new System.Windows.Forms.TextBox();
            this.bSave = new System.Windows.Forms.Button();
            this.dgvRecord = new System.Windows.Forms.DataGridView();
            this.tbBeAlignmentPassword124 = new System.Windows.Forms.TextBox();
            this.tbBeAlignmentPassword125 = new System.Windows.Forms.TextBox();
            this.tbBeAlignmentPassword126 = new System.Windows.Forms.TextBox();
            this.tcFunction = new System.Windows.Forms.TabControl();
            this.tpLensAlignment = new System.Windows.Forms.TabPage();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.ucLensAlignment = new LensAlignment.UcLensAlignment();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).BeginInit();
            this.tcFunction.SuspendLayout();
            this.tpLensAlignment.SuspendLayout();
            this.tpLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbLightSourceConnected
            // 
            this.cbLightSourceConnected.AutoSize = true;
            this.cbLightSourceConnected.Location = new System.Drawing.Point(12, 12);
            this.cbLightSourceConnected.Name = "cbLightSourceConnected";
            this.cbLightSourceConnected.Size = new System.Drawing.Size(137, 16);
            this.cbLightSourceConnected.TabIndex = 0;
            this.cbLightSourceConnected.Text = "Light Source Connected";
            this.cbLightSourceConnected.UseVisualStyleBackColor = true;
            this.cbLightSourceConnected.CheckedChanged += new System.EventHandler(this.cbLightSourceConnected_CheckedChanged);
            // 
            // cbBeAlignmentConnected
            // 
            this.cbBeAlignmentConnected.AutoSize = true;
            this.cbBeAlignmentConnected.Location = new System.Drawing.Point(155, 12);
            this.cbBeAlignmentConnected.Name = "cbBeAlignmentConnected";
            this.cbBeAlignmentConnected.Size = new System.Drawing.Size(142, 16);
            this.cbBeAlignmentConnected.TabIndex = 1;
            this.cbBeAlignmentConnected.Text = "Be Alignment Connected";
            this.cbBeAlignmentConnected.UseVisualStyleBackColor = true;
            this.cbBeAlignmentConnected.CheckedChanged += new System.EventHandler(this.cbBeAlignmentConnected_CheckedChanged);
            // 
            // cbStartMonitor
            // 
            this.cbStartMonitor.AutoSize = true;
            this.cbStartMonitor.Enabled = false;
            this.cbStartMonitor.Location = new System.Drawing.Point(822, 12);
            this.cbStartMonitor.Name = "cbStartMonitor";
            this.cbStartMonitor.Size = new System.Drawing.Size(86, 16);
            this.cbStartMonitor.TabIndex = 3;
            this.cbStartMonitor.Text = "Start Monitor";
            this.cbStartMonitor.UseVisualStyleBackColor = true;
            this.cbStartMonitor.CheckedChanged += new System.EventHandler(this.cbStartMonitor_CheckedChanged);
            // 
            // lBeAlignmentPassword
            // 
            this.lBeAlignmentPassword.AutoSize = true;
            this.lBeAlignmentPassword.Location = new System.Drawing.Point(303, 13);
            this.lBeAlignmentPassword.Name = "lBeAlignmentPassword";
            this.lBeAlignmentPassword.Size = new System.Drawing.Size(122, 12);
            this.lBeAlignmentPassword.TabIndex = 4;
            this.lBeAlignmentPassword.Text = "Be Alignment Password :";
            // 
            // tbBeAlignmentPassword123
            // 
            this.tbBeAlignmentPassword123.Location = new System.Drawing.Point(431, 10);
            this.tbBeAlignmentPassword123.Name = "tbBeAlignmentPassword123";
            this.tbBeAlignmentPassword123.Size = new System.Drawing.Size(25, 22);
            this.tbBeAlignmentPassword123.TabIndex = 5;
            this.tbBeAlignmentPassword123.Text = "0";
            // 
            // bLog
            // 
            this.bLog.BackColor = System.Drawing.SystemColors.Control;
            this.bLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bLog.Location = new System.Drawing.Point(762, 8);
            this.bLog.Name = "bLog";
            this.bLog.Size = new System.Drawing.Size(50, 23);
            this.bLog.TabIndex = 6;
            this.bLog.Text = "Log";
            this.bLog.UseVisualStyleBackColor = false;
            this.bLog.Click += new System.EventHandler(this.bLog_Click);
            // 
            // bClearLog
            // 
            this.bClearLog.BackColor = System.Drawing.SystemColors.Control;
            this.bClearLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bClearLog.Location = new System.Drawing.Point(555, 8);
            this.bClearLog.Name = "bClearLog";
            this.bClearLog.Size = new System.Drawing.Size(60, 23);
            this.bClearLog.TabIndex = 7;
            this.bClearLog.Text = "Clear Log";
            this.bClearLog.UseVisualStyleBackColor = false;
            this.bClearLog.Click += new System.EventHandler(this.bClearLog_Click);
            // 
            // lLogLable
            // 
            this.lLogLable.AutoSize = true;
            this.lLogLable.Location = new System.Drawing.Point(621, 13);
            this.lLogLable.Name = "lLogLable";
            this.lLogLable.Size = new System.Drawing.Size(59, 12);
            this.lLogLable.TabIndex = 8;
            this.lLogLable.Text = "Log Lable :";
            // 
            // tbLogLable
            // 
            this.tbLogLable.Location = new System.Drawing.Point(686, 10);
            this.tbLogLable.Name = "tbLogLable";
            this.tbLogLable.Size = new System.Drawing.Size(70, 22);
            this.tbLogLable.TabIndex = 9;
            // 
            // bSave
            // 
            this.bSave.BackColor = System.Drawing.SystemColors.Control;
            this.bSave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bSave.Location = new System.Drawing.Point(914, 8);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(45, 23);
            this.bSave.TabIndex = 10;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = false;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // dgvRecord
            // 
            this.dgvRecord.AllowUserToAddRows = false;
            this.dgvRecord.AllowUserToResizeRows = false;
            this.dgvRecord.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvRecord.BackgroundColor = System.Drawing.Color.DimGray;
            this.dgvRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecord.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecord.GridColor = System.Drawing.Color.Silver;
            this.dgvRecord.Location = new System.Drawing.Point(0, 0);
            this.dgvRecord.Name = "dgvRecord";
            this.dgvRecord.RowTemplate.Height = 24;
            this.dgvRecord.Size = new System.Drawing.Size(992, 520);
            this.dgvRecord.TabIndex = 11;
            // 
            // tbBeAlignmentPassword124
            // 
            this.tbBeAlignmentPassword124.Location = new System.Drawing.Point(462, 10);
            this.tbBeAlignmentPassword124.Name = "tbBeAlignmentPassword124";
            this.tbBeAlignmentPassword124.Size = new System.Drawing.Size(25, 22);
            this.tbBeAlignmentPassword124.TabIndex = 12;
            this.tbBeAlignmentPassword124.Text = "0";
            // 
            // tbBeAlignmentPassword125
            // 
            this.tbBeAlignmentPassword125.Location = new System.Drawing.Point(493, 10);
            this.tbBeAlignmentPassword125.Name = "tbBeAlignmentPassword125";
            this.tbBeAlignmentPassword125.Size = new System.Drawing.Size(25, 22);
            this.tbBeAlignmentPassword125.TabIndex = 13;
            this.tbBeAlignmentPassword125.Text = "0";
            // 
            // tbBeAlignmentPassword126
            // 
            this.tbBeAlignmentPassword126.Location = new System.Drawing.Point(524, 10);
            this.tbBeAlignmentPassword126.Name = "tbBeAlignmentPassword126";
            this.tbBeAlignmentPassword126.Size = new System.Drawing.Size(25, 22);
            this.tbBeAlignmentPassword126.TabIndex = 14;
            this.tbBeAlignmentPassword126.Text = "0";
            // 
            // tcFunction
            // 
            this.tcFunction.Controls.Add(this.tpLensAlignment);
            this.tcFunction.Controls.Add(this.tpLog);
            this.tcFunction.Location = new System.Drawing.Point(8, 38);
            this.tcFunction.Name = "tcFunction";
            this.tcFunction.SelectedIndex = 0;
            this.tcFunction.Size = new System.Drawing.Size(1000, 546);
            this.tcFunction.TabIndex = 15;
            // 
            // tpLensAlignment
            // 
            this.tpLensAlignment.BackColor = System.Drawing.Color.Black;
            this.tpLensAlignment.Controls.Add(this.ucLensAlignment);
            this.tpLensAlignment.Location = new System.Drawing.Point(4, 22);
            this.tpLensAlignment.Name = "tpLensAlignment";
            this.tpLensAlignment.Padding = new System.Windows.Forms.Padding(3);
            this.tpLensAlignment.Size = new System.Drawing.Size(992, 520);
            this.tpLensAlignment.TabIndex = 0;
            this.tpLensAlignment.Text = "Lens Alignment";
            // 
            // tpLog
            // 
            this.tpLog.BackColor = System.Drawing.Color.Black;
            this.tpLog.Controls.Add(this.dgvRecord);
            this.tpLog.Location = new System.Drawing.Point(4, 22);
            this.tpLog.Name = "tpLog";
            this.tpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpLog.Size = new System.Drawing.Size(992, 520);
            this.tpLog.TabIndex = 1;
            this.tpLog.Text = "Log";
            // 
            // ucLensAlignment
            // 
            this.ucLensAlignment.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ucLensAlignment.ForeColor = System.Drawing.SystemColors.Info;
            this.ucLensAlignment.Location = new System.Drawing.Point(0, 0);
            this.ucLensAlignment.Name = "ucLensAlignment";
            this.ucLensAlignment.Size = new System.Drawing.Size(992, 521);
            this.ucLensAlignment.TabIndex = 2;
            // 
            // fLensAlignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1016, 590);
            this.Controls.Add(this.tcFunction);
            this.Controls.Add(this.tbBeAlignmentPassword126);
            this.Controls.Add(this.tbBeAlignmentPassword125);
            this.Controls.Add(this.tbBeAlignmentPassword124);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.tbLogLable);
            this.Controls.Add(this.lLogLable);
            this.Controls.Add(this.bClearLog);
            this.Controls.Add(this.bLog);
            this.Controls.Add(this.tbBeAlignmentPassword123);
            this.Controls.Add(this.lBeAlignmentPassword);
            this.Controls.Add(this.cbStartMonitor);
            this.Controls.Add(this.cbBeAlignmentConnected);
            this.Controls.Add(this.cbLightSourceConnected);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "fLensAlignment";
            this.Text = "Lens Alignment";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).EndInit();
            this.tcFunction.ResumeLayout(false);
            this.tpLensAlignment.ResumeLayout(false);
            this.tpLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbLightSourceConnected;
        private System.Windows.Forms.CheckBox cbBeAlignmentConnected;
        private UcLensAlignment ucLensAlignment;
        private System.Windows.Forms.CheckBox cbStartMonitor;
        private System.Windows.Forms.Label lBeAlignmentPassword;
        private System.Windows.Forms.TextBox tbBeAlignmentPassword123;
        private System.Windows.Forms.Button bLog;
        private System.Windows.Forms.Button bClearLog;
        private System.Windows.Forms.Label lLogLable;
        private System.Windows.Forms.TextBox tbLogLable;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.DataGridView dgvRecord;
        private System.Windows.Forms.TextBox tbBeAlignmentPassword124;
        private System.Windows.Forms.TextBox tbBeAlignmentPassword125;
        private System.Windows.Forms.TextBox tbBeAlignmentPassword126;
        private System.Windows.Forms.TabControl tcFunction;
        private System.Windows.Forms.TabPage tpLensAlignment;
        private System.Windows.Forms.TabPage tpLog;
    }
}

