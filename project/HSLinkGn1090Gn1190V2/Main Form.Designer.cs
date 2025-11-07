using Gn1090Gn1190Config;
using HSLinkGn1090Gn1190;

namespace HSLinkGn1090Gn1190
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Gn1090Gn1190Config.UcGn1090Config ucGn1090Config;
        private Gn1090Gn1190Config.UcGn1190Config ucGn1190Config;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucGn1090Config = new Gn1090Gn1190Config.UcGn1090Config();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucGn1190Config1 = new Gn1090Gn1190Config.UcGn1190Config();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ucGn1090Config1 = new Gn1090Gn1190Config.UcGn1090Config();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.ucGn1190Config2 = new Gn1090Gn1190Config.UcGn1190Config();
            this.cbConnected = new System.Windows.Forms.CheckBox();
            this.ucGn1190Config = new Gn1090Gn1190Config.UcGn1190Config();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rbRX4 = new System.Windows.Forms.RadioButton();
            this.rbRX5 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.rbRX6 = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.rbCmd_T = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.rbCmd_R = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.rbRX1 = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.rbRX2 = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.rbRX3 = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.rbTX3 = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.rbTX2 = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.rbTX1 = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.rbCmt_T = new System.Windows.Forms.RadioButton();
            this.label16 = new System.Windows.Forms.Label();
            this.rbCmt_R = new System.Windows.Forms.RadioButton();
            this.label17 = new System.Windows.Forms.Label();
            this.rbTX6 = new System.Windows.Forms.RadioButton();
            this.label18 = new System.Windows.Forms.Label();
            this.rbTX5 = new System.Windows.Forms.RadioButton();
            this.label19 = new System.Windows.Forms.Label();
            this.rbTX4 = new System.Windows.Forms.RadioButton();
            this.label20 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ledArrayDisplay1 = new OpticalLedManager.LedArrayDisplay();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.ItemSize = new System.Drawing.Size(88, 18);
            this.tabControl1.Location = new System.Drawing.Point(0, 126);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 555);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucGn1090Config);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(752, 529);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "GN1090_I2C_1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ucGn1090Config
            // 
            this.ucGn1090Config.Location = new System.Drawing.Point(0, 6);
            this.ucGn1090Config.Name = "ucGn1090Config";
            this.ucGn1090Config.Size = new System.Drawing.Size(711, 378);
            this.ucGn1090Config.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucGn1190Config1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "GN1190_I2C_1";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ucGn1190Config1
            // 
            this.ucGn1190Config1.Location = new System.Drawing.Point(0, 0);
            this.ucGn1190Config1.Name = "ucGn1190Config1";
            this.ucGn1190Config1.Size = new System.Drawing.Size(643, 523);
            this.ucGn1190Config1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ucGn1090Config1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(192, 74);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "GN1090_I2C_2";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ucGn1090Config1
            // 
            this.ucGn1090Config1.Location = new System.Drawing.Point(0, 0);
            this.ucGn1090Config1.Name = "ucGn1090Config1";
            this.ucGn1090Config1.Size = new System.Drawing.Size(643, 314);
            this.ucGn1090Config1.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.ucGn1190Config2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(192, 74);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "GN1190_I2C_2";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // ucGn1190Config2
            // 
            this.ucGn1190Config2.Location = new System.Drawing.Point(0, 0);
            this.ucGn1190Config2.Name = "ucGn1190Config2";
            this.ucGn1190Config2.Size = new System.Drawing.Size(643, 523);
            this.ucGn1190Config2.TabIndex = 0;
            // 
            // cbConnected
            // 
            this.cbConnected.AutoSize = true;
            this.cbConnected.Location = new System.Drawing.Point(583, 99);
            this.cbConnected.Name = "cbConnected";
            this.cbConnected.Size = new System.Drawing.Size(74, 16);
            this.cbConnected.TabIndex = 2;
            this.cbConnected.Text = "Connected";
            this.cbConnected.UseVisualStyleBackColor = true;
            // 
            // ucGn1190Config
            // 
            this.ucGn1190Config.Location = new System.Drawing.Point(0, 0);
            this.ucGn1190Config.Name = "ucGn1190Config";
            this.ucGn1190Config.Size = new System.Drawing.Size(711, 378);
            this.ucGn1190Config.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Optical CH.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Definition";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Definition";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "Optical CH.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(96, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "16.";
            // 
            // rbRX4
            // 
            this.rbRX4.AutoSize = true;
            this.rbRX4.Location = new System.Drawing.Point(85, 40);
            this.rbRX4.Name = "rbRX4";
            this.rbRX4.Size = new System.Drawing.Size(43, 16);
            this.rbRX4.TabIndex = 7;
            this.rbRX4.TabStop = true;
            this.rbRX4.Text = "Rx4";
            this.rbRX4.UseVisualStyleBackColor = true;
            this.rbRX4.CheckedChanged += new System.EventHandler(this.rbRX4_CheckedChanged);
            // 
            // rbRX5
            // 
            this.rbRX5.AutoSize = true;
            this.rbRX5.Location = new System.Drawing.Point(134, 40);
            this.rbRX5.Name = "rbRX5";
            this.rbRX5.Size = new System.Drawing.Size(43, 16);
            this.rbRX5.TabIndex = 9;
            this.rbRX5.TabStop = true;
            this.rbRX5.Text = "Rx5";
            this.rbRX5.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(145, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "15.";
            // 
            // rbRX6
            // 
            this.rbRX6.AutoSize = true;
            this.rbRX6.Location = new System.Drawing.Point(183, 40);
            this.rbRX6.Name = "rbRX6";
            this.rbRX6.Size = new System.Drawing.Size(43, 16);
            this.rbRX6.TabIndex = 11;
            this.rbRX6.TabStop = true;
            this.rbRX6.Text = "Rx6";
            this.rbRX6.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(194, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "14.";
            // 
            // rbCmd_T
            // 
            this.rbCmd_T.AutoSize = true;
            this.rbCmd_T.Location = new System.Drawing.Point(232, 40);
            this.rbCmd_T.Name = "rbCmd_T";
            this.rbCmd_T.Size = new System.Drawing.Size(56, 16);
            this.rbCmd_T.TabIndex = 13;
            this.rbCmd_T.TabStop = true;
            this.rbCmd_T.Text = "Cmd T";
            this.rbCmd_T.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(249, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "8.";
            // 
            // rbCmd_R
            // 
            this.rbCmd_R.AutoSize = true;
            this.rbCmd_R.Location = new System.Drawing.Point(287, 40);
            this.rbCmd_R.Name = "rbCmd_R";
            this.rbCmd_R.Size = new System.Drawing.Size(57, 16);
            this.rbCmd_R.TabIndex = 15;
            this.rbCmd_R.TabStop = true;
            this.rbCmd_R.Text = "Cmd R";
            this.rbCmd_R.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(298, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "4.";
            // 
            // rbRX1
            // 
            this.rbRX1.AutoSize = true;
            this.rbRX1.Location = new System.Drawing.Point(343, 40);
            this.rbRX1.Name = "rbRX1";
            this.rbRX1.Size = new System.Drawing.Size(43, 16);
            this.rbRX1.TabIndex = 17;
            this.rbRX1.TabStop = true;
            this.rbRX1.Text = "Rx1";
            this.rbRX1.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(354, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 12);
            this.label10.TabIndex = 16;
            this.label10.Text = "3.";
            // 
            // rbRX2
            // 
            this.rbRX2.AutoSize = true;
            this.rbRX2.Location = new System.Drawing.Point(392, 40);
            this.rbRX2.Name = "rbRX2";
            this.rbRX2.Size = new System.Drawing.Size(43, 16);
            this.rbRX2.TabIndex = 19;
            this.rbRX2.TabStop = true;
            this.rbRX2.Text = "Rx2";
            this.rbRX2.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(403, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 12);
            this.label11.TabIndex = 18;
            this.label11.Text = "2.";
            // 
            // rbRX3
            // 
            this.rbRX3.AutoSize = true;
            this.rbRX3.Location = new System.Drawing.Point(441, 40);
            this.rbRX3.Name = "rbRX3";
            this.rbRX3.Size = new System.Drawing.Size(43, 16);
            this.rbRX3.TabIndex = 21;
            this.rbRX3.TabStop = true;
            this.rbRX3.Text = "Rx3";
            this.rbRX3.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(452, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 12);
            this.label12.TabIndex = 20;
            this.label12.Text = "1.";
            // 
            // rbTX3
            // 
            this.rbTX3.AutoSize = true;
            this.rbTX3.Location = new System.Drawing.Point(441, 83);
            this.rbTX3.Name = "rbTX3";
            this.rbTX3.Size = new System.Drawing.Size(42, 16);
            this.rbTX3.TabIndex = 37;
            this.rbTX3.TabStop = true;
            this.rbTX3.Text = "Tx3";
            this.rbTX3.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(453, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 12);
            this.label13.TabIndex = 36;
            this.label13.Text = "16.";
            // 
            // rbTX2
            // 
            this.rbTX2.AutoSize = true;
            this.rbTX2.Location = new System.Drawing.Point(392, 83);
            this.rbTX2.Name = "rbTX2";
            this.rbTX2.Size = new System.Drawing.Size(42, 16);
            this.rbTX2.TabIndex = 35;
            this.rbTX2.TabStop = true;
            this.rbTX2.Text = "Tx2";
            this.rbTX2.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(404, 64);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(20, 12);
            this.label14.TabIndex = 34;
            this.label14.Text = "15.";
            // 
            // rbTX1
            // 
            this.rbTX1.AutoSize = true;
            this.rbTX1.Location = new System.Drawing.Point(343, 83);
            this.rbTX1.Name = "rbTX1";
            this.rbTX1.Size = new System.Drawing.Size(42, 16);
            this.rbTX1.TabIndex = 33;
            this.rbTX1.TabStop = true;
            this.rbTX1.Text = "Tx1";
            this.rbTX1.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(355, 64);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(20, 12);
            this.label15.TabIndex = 32;
            this.label15.Text = "14.";
            // 
            // rbCmt_T
            // 
            this.rbCmt_T.AutoSize = true;
            this.rbCmt_T.Location = new System.Drawing.Point(287, 83);
            this.rbCmt_T.Name = "rbCmt_T";
            this.rbCmt_T.Size = new System.Drawing.Size(53, 16);
            this.rbCmt_T.TabIndex = 31;
            this.rbCmt_T.TabStop = true;
            this.rbCmt_T.Text = "Cmt T";
            this.rbCmt_T.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(298, 64);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(20, 12);
            this.label16.TabIndex = 30;
            this.label16.Text = "13.";
            // 
            // rbCmt_R
            // 
            this.rbCmt_R.AutoSize = true;
            this.rbCmt_R.Location = new System.Drawing.Point(232, 83);
            this.rbCmt_R.Name = "rbCmt_R";
            this.rbCmt_R.Size = new System.Drawing.Size(54, 16);
            this.rbCmt_R.TabIndex = 29;
            this.rbCmt_R.TabStop = true;
            this.rbCmt_R.Text = "Cmt R";
            this.rbCmt_R.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(249, 64);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 12);
            this.label17.TabIndex = 28;
            this.label17.Text = "9.";
            // 
            // rbTX6
            // 
            this.rbTX6.AutoSize = true;
            this.rbTX6.Location = new System.Drawing.Point(183, 83);
            this.rbTX6.Name = "rbTX6";
            this.rbTX6.Size = new System.Drawing.Size(42, 16);
            this.rbTX6.TabIndex = 27;
            this.rbTX6.TabStop = true;
            this.rbTX6.Text = "Tx6";
            this.rbTX6.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(194, 64);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 12);
            this.label18.TabIndex = 26;
            this.label18.Text = "3.";
            // 
            // rbTX5
            // 
            this.rbTX5.AutoSize = true;
            this.rbTX5.Location = new System.Drawing.Point(134, 83);
            this.rbTX5.Name = "rbTX5";
            this.rbTX5.Size = new System.Drawing.Size(42, 16);
            this.rbTX5.TabIndex = 25;
            this.rbTX5.TabStop = true;
            this.rbTX5.Text = "Tx5";
            this.rbTX5.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(145, 64);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(14, 12);
            this.label19.TabIndex = 24;
            this.label19.Text = "2.";
            // 
            // rbTX4
            // 
            this.rbTX4.AutoSize = true;
            this.rbTX4.Location = new System.Drawing.Point(85, 83);
            this.rbTX4.Name = "rbTX4";
            this.rbTX4.Size = new System.Drawing.Size(42, 16);
            this.rbTX4.TabIndex = 23;
            this.rbTX4.TabStop = true;
            this.rbTX4.Text = "Tx4";
            this.rbTX4.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(96, 64);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(14, 12);
            this.label20.TabIndex = 22;
            this.label20.Text = "1.";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(518, 40);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(74, 16);
            this.checkBox1.TabIndex = 38;
            this.checkBox1.Text = "Connected";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ledArrayDisplay1
            // 
            this.ledArrayDisplay1.Location = new System.Drawing.Point(490, 3);
            this.ledArrayDisplay1.Name = "ledArrayDisplay1";
            this.ledArrayDisplay1.Size = new System.Drawing.Size(634, 277);
            this.ledArrayDisplay1.TabIndex = 39;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 851);
            this.Controls.Add(this.ledArrayDisplay1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.rbTX3);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.rbTX2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.rbTX1);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.rbCmt_T);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.rbCmt_R);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.rbTX6);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.rbTX5);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.rbTX4);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.rbRX3);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.rbRX2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.rbRX1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.rbCmd_R);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.rbCmd_T);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.rbRX6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.rbRX5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rbRX4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private UcGn1190Config ucGn1190Config1;
        private UcGn1090Config ucGn1090Config1;
        private UcGn1190Config ucGn1190Config2;
        private System.Windows.Forms.CheckBox cbConnected;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbRX4;
        private System.Windows.Forms.RadioButton rbRX5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rbRX6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbCmd_T;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rbCmd_R;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rbRX1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rbRX2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton rbRX3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton rbTX3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RadioButton rbTX2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RadioButton rbTX1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RadioButton rbCmt_T;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RadioButton rbCmt_R;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.RadioButton rbTX6;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.RadioButton rbTX5;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.RadioButton rbTX4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox checkBox1;
        private OpticalLedManager.LedArrayDisplay ledArrayDisplay1;
    }
}

