using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;

namespace EOS.Core.Common
{
    public class PrintLabel
    {

        public string Codice = "";
        public List<String> Codici = new List<String>();
        public int NumeroCopie = 0;

        public void StampaEtichette()
        {
            PrintDocument doc  = new PrintDocument();
            PrintDialog printer = new PrintDialog();

            doc.PrintPage += PrintPageHandler;

            printer.Document = doc;
            printer.PrinterSettings.PrinterName = @"\\Server022\CAB04";

            //Sul report il font del codice campione è arial 18
            //68X25mm
            //268X98 centesimi di pollice
            //25X10mm
            //98X39 centesimi di pollice
            //però da nastro
            //39X10mm
            //154X39 centesimi di pollice

            printer.Document.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Custom_Label", 197, 59);

            foreach (string CodiciOccorrenza in Codici)
            {
                Codice = CodiciOccorrenza;
                for (int i = 0; i < NumeroCopie; i++)
                {
                    doc.Print();
                }
            }

            doc = null;
            printer = null;
        }
        private void PrintPageHandler(Object sendert, PrintPageEventArgs e)
        {
            var canvas = e.Graphics;
            var _font = new Font("Arial", 15, FontStyle.Bold);
            var _brush = Brushes.Black;
            string PrintString;

            PrintString = Codice;

            canvas.DrawString(PrintString, _font, _brush, 12, 27);
        }
    }
}
