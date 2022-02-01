using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace EOS.UI
{
    public partial class frmPrintSelect : Form
    {
        public int Copie = 1;
        public string Stampante = "";

        public frmPrintSelect()
        {
            InitializeComponent();
        }

        private void frmPrintSelect_Load(object sender, EventArgs e)
        {
            LoadStampanti();
        }
        private void LoadStampanti()
        {
            System.Drawing.Printing.PrinterSettings PS = new System.Drawing.Printing.PrinterSettings();

            foreach (object p in PrinterSettings.InstalledPrinters)
            {
                cboStampante.Properties.Items.Add(p);
            }

            cboStampante.SelectedItem = PS.PrinterName;

            PS = null;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            Copie = Convert.ToInt32(txtCopie.EditValue);
            Stampante = cboStampante.EditValue.ToString();
            this.Close();
        }

        private void butAnnulla_Click(object sender, EventArgs e)
        {
            Stampante = "";
            this.Close();
        }
    }
}
