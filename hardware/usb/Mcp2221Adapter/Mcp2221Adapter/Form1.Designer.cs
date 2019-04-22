namespace Mcp2221Adapter
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ucFunctionTest1 = new Mcp2221Adapter.ucFunctionTest();
            this.SuspendLayout();
            // 
            // ucFunctionTest1
            // 
            this.ucFunctionTest1.Location = new System.Drawing.Point(12, 12);
            this.ucFunctionTest1.Name = "ucFunctionTest1";
            this.ucFunctionTest1.Size = new System.Drawing.Size(244, 277);
            this.ucFunctionTest1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 293);
            this.Controls.Add(this.ucFunctionTest1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ucFunctionTest ucFunctionTest1;
    }
}

