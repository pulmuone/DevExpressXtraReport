using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Windows.Forms;
using WindowsFormsApp5.Properties;

namespace WindowsFormsApp5.Printer
{
    //public delegate void PrinaterSelectEventHandler(string PrinterName);

    public partial class PrinterSelect : Form
    {
        public event Action<string> PrinaterSelectEvent;
        public PrinterSelect()
        {
            InitializeComponent();
        }

        private void PrinterSelect_Load(object sender, EventArgs e)
        {
            List<string> printers = new List<string>();

            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                printers.Add(printer);
            }

            comboBox1.DataSource = printers;

            string printerName = Settings.Default.PrinterName; //프로그램에서의 기본 프린터

            if (string.IsNullOrEmpty(printerName))
            {
                comboBox1.SelectedItem = new PrinterSettings().PrinterName; //작업 컴퓨터의 기본 프린터
            }
            else
            {
                comboBox1.SelectedItem = printerName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string printerName = comboBox1.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(printerName))
            {
                Settings.Default.PrinterName = printerName;
                Settings.Default.Save();
                PrinterSingleton.Instance.PrinterName = printerName;

                PrinaterSelectEvent?.Invoke(PrinterSingleton.Instance.PrinterName);

                //if (PrinaterSelectEvent != null)
                //{
                //    PrinaterSelectEvent(PrinterSingleton.Instance.PrinterName);
                //}
            }
            MessageBox.Show(PrinterSingleton.Instance.PrinterName, "Saved Complete");
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            MessageBox.Show(PrinterSingleton.Instance.PrinterName);
        }     
    }
}
