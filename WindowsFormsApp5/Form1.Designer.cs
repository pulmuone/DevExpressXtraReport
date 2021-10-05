
namespace WindowsFormsApp5
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
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrinterSelect = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnPrintStatus = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(22, 59);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(201, 99);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "출력하기";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnPrinterSelect
            // 
            this.btnPrinterSelect.Location = new System.Drawing.Point(255, 59);
            this.btnPrinterSelect.Name = "btnPrinterSelect";
            this.btnPrinterSelect.Size = new System.Drawing.Size(130, 99);
            this.btnPrinterSelect.TabIndex = 1;
            this.btnPrinterSelect.Text = "프린터 선택";
            this.btnPrinterSelect.UseVisualStyleBackColor = true;
            this.btnPrinterSelect.Click += new System.EventHandler(this.btnPrinterSelect_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(22, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(522, 21);
            this.textBox1.TabIndex = 2;
            // 
            // btnPrintStatus
            // 
            this.btnPrintStatus.Location = new System.Drawing.Point(423, 59);
            this.btnPrintStatus.Name = "btnPrintStatus";
            this.btnPrintStatus.Size = new System.Drawing.Size(121, 99);
            this.btnPrintStatus.TabIndex = 3;
            this.btnPrintStatus.Text = "프린터 상태 체크";
            this.btnPrintStatus.UseVisualStyleBackColor = true;
            this.btnPrintStatus.Click += new System.EventHandler(this.btnPrintStatus_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnPrintStatus);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnPrinterSelect);
            this.Controls.Add(this.btnPrint);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrinterSelect;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnPrintStatus;
    }
}

