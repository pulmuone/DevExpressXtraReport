using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp5.Printer
{
    public sealed class PrinterSingleton
    {
        private static readonly PrinterSingleton instance = new PrinterSingleton();

        private string _printerName = string.Empty;

        private PrinterSingleton() { }

        public static PrinterSingleton Instance
        {
            get
            {
                return instance;
            }
        }

        public string PrinterName
        {
            get
            {
                return this._printerName;
            }

            set
            {
                this._printerName = value;
            }
        }
    }
}
