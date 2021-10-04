using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private PrinterSettings prnSettings;

        private void button1_Click(object sender, EventArgs e)
        {

            XtraReport1 report = new XtraReport1();
            ReportPrintTool printTool = new ReportPrintTool(report);

            DataSet1 ds1 = new DataSet1(); //데이터 모델
            DataTable dt = new DataTable();
            DataRow dr;

            for (int i = 0; i < 100; i++)
            {
                dr = ds1.Tables[0].NewRow();
                dr["DataColumn1"] = i.ToString();
                dr["DataColumn2"] = "test " + i.ToString();

                ds1.Tables[0].Rows.Add(dr);
            }

            report.DataSource = ds1;

            printTool.PrintingSystem.StartPrint += new PrintDocumentEventHandler(PrintingSystem_StartPrint);
            printTool.PrintingSystem.PrintProgress += new PrintProgressEventHandler(PrintingSystem_PrintProgress);
            printTool.PrintingSystem.EndPrint += new EventHandler(PrintingSystem_EndPrint);

            // Invoke the Print dialog.
            //printTool.PrintDialog(); //인쇄 설정 화면 보이기
            // Send the report to the default printer.
            printTool.Print(); //바로 인쇄
            // Send the report to the specified printer.
            //printTool.Print("myPrinter"); //프린터 지정해서 바로 인쇄
        }
        

        void PrintingSystem_StartPrint(object sender, PrintDocumentEventArgs e)
        {
            prnSettings = e.PrintDocument.PrinterSettings;
            Debug.WriteLine("프린터 시작");
        }

        void PrintingSystem_PrintProgress(object sender, PrintProgressEventArgs e)
        {
            Debug.WriteLine(string.Format("{0} : {1}", "프린터 인쇄 중", e.PageIndex));
        }

        void PrintingSystem_EndPrint(object sender, EventArgs e)
        {
            Debug.WriteLine("프린터 종료");
        }
    }
}
