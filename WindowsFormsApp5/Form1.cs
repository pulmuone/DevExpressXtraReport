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
using System.Printing;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using WindowsFormsApp5.Printer;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        //LocalPrintServer ps = new LocalPrintServer();
        //PrintServer ps = new PrintServer(@"\\192.168.0.23");
        PrintServer ps = new PrintServer();

        String statusReport = string.Empty;

        public Form1()
        {
            InitializeComponent();
            //var server = new PrintServer();
            //PrintQueueCollection queues = server.GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });
            //var arr = queues.Select(q => q.QueuePort.Name + "  " + q.FullName).ToArray();
            //Console.WriteLine(String.Join("\n", arr));
        }

        private PrinterSettings prnSettings;

        private void button1_Click(object sender, EventArgs e)
        {
            XtraReport1 report = new XtraReport1();
            ReportPrintTool printTool = new ReportPrintTool(report);

            DataSet1 ds1 = new DataSet1(); //데이터 모델
            DataTable dt = new DataTable();
            DataRow dr;

            for (int i = 0; i <30; i++)
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
            //printTool.Print(); //바로 인쇄
            // Send the report to the specified printer.
            printTool.Print(this.textBox1.Text); //프린터 지정해서 바로 인쇄
        }

        void PrintStatusCheck()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000; //타이머 1초 단위로 실행
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed); //타이머 이벤트 핸들러 (delegatge 선언, ElapsedEventHandler 열어 보면 Delegate로 되어 있음 심부름 시키는애..)
            timer.Start(); //타이머 시작

            backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
            //backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }

        /// <summary>
        /// https://docs.microsoft.com/ko-kr/dotnet/api/system.printing.printqueuestatus?view=net-5.0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                //foreach (PrintQueue printQueue in ps.GetPrintQueues())
                //{
                //    Debug.WriteLine($"{printQueue.FullName}  [{printQueue.QueueStatus}]");
                //    //if (printQueue.FullName == this.textBox1.Text)
                //    //{
                //    //    Debug.WriteLine($"{printQueue.FullName}  [{printQueue.QueueStatus}]");
                //    //}
                //}

                var printQueue1 = ps.GetPrintQueue(this.textBox1.Text);
                statusReport = string.Empty;
                Debug.WriteLine($"{printQueue1.FullName}  [{printQueue1.QueueStatus}]");
                SpotTroubleUsingQueueAttributes(ref statusReport, printQueue1);
                Debug.WriteLine($"{printQueue1.FullName}  [{statusReport}]");


                ServiceController service = new ServiceController("Spooler");
                Debug.WriteLine(service.Status.ToString());
                if ((!service.Status.Equals(ServiceControllerStatus.Stopped)) &&
                    (!service.Status.Equals(ServiceControllerStatus.StopPending)))
                {
                    //lblStatus.Text = "Stopping spooler...";
                    //lblStatus.Refresh();

                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                }

                //service.Start();
                //service.WaitForStatus(ServiceControllerStatus.Running);

            }));
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Console.WriteLine("work number  End : {0} ", e.Result);

            //5번 실행 이 끝나면 콘솔 프로그램 종료
            //if (_count == 5)
            //{
            //    System.Environment.Exit(0);
            //}
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Debug.WriteLine("Timer Working {0} ", e.SignalTime.ToString());

            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        void PrintingSystem_StartPrint(object sender, PrintDocumentEventArgs e)
        {
            prnSettings = e.PrintDocument.PrinterSettings;
            Debug.WriteLine("프린터 시작");
        }

        void PrintingSystem_PrintProgress(object sender, PrintProgressEventArgs e)
        {
            Debug.WriteLine(string.Format("{0} : {1}", "프린터 인쇄 중", e.PageIndex));
            Console.WriteLine(e.PrintAction);
        }

        void PrintingSystem_EndPrint(object sender, EventArgs e)
        {
            Debug.WriteLine("프린터 종료");
        }

        private void btnPrinterSelect_Click(object sender, EventArgs e)
        {
            PrinterSelect printerSelect = new PrinterSelect();
            printerSelect.PrinaterSelectEvent += PrinterName;

            printerSelect.ShowDialog();
        }

        private void PrinterName(string printerName)
        {
            this.textBox1.Text = printerName;
        }


        private void SpotTroubleUsingQueueAttributes(ref String statusReport, PrintQueue pq)
        {
            if ((pq.QueueStatus & PrintQueueStatus.PaperProblem) == PrintQueueStatus.PaperProblem)
            {
                statusReport = statusReport + "Has a paper problem. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.NoToner) == PrintQueueStatus.NoToner)
            {
                statusReport = statusReport + "Is out of toner. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.DoorOpen) == PrintQueueStatus.DoorOpen)
            {
                statusReport = statusReport + "Has an open door. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.Error) == PrintQueueStatus.Error)
            {
                statusReport = statusReport + "Is in an error state. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.NotAvailable) == PrintQueueStatus.NotAvailable)
            {
                statusReport = statusReport + "Is not available. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.Offline) == PrintQueueStatus.Offline)
            {
                statusReport = statusReport + "Is off line. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.OutOfMemory) == PrintQueueStatus.OutOfMemory)
            {
                statusReport = statusReport + "Is out of memory. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.PaperOut) == PrintQueueStatus.PaperOut)
            {
                statusReport = statusReport + "Is out of paper. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.OutputBinFull) == PrintQueueStatus.OutputBinFull)
            {
                statusReport = statusReport + "Has a full output bin. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.PaperJam) == PrintQueueStatus.PaperJam)
            {
                statusReport = statusReport + "Has a paper jam. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.Paused) == PrintQueueStatus.Paused)
            {
                statusReport = statusReport + "Is paused. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.TonerLow) == PrintQueueStatus.TonerLow)
            {
                statusReport = statusReport + "Is low on toner. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.UserIntervention) == PrintQueueStatus.UserIntervention)
            {
                statusReport = statusReport + "Needs user intervention. ";
            }

            // Check if queue is even available at this time of day
            // The method below is defined in the complete example.
            //ReportAvailabilityAtThisTime(ref statusReport, pq);
        }

        private void btnPrintStatus_Click(object sender, EventArgs e)
        {
            PrintStatusCheck();
        }
    }
}
