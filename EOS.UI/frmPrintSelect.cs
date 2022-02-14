using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        public string Report = "";
        public string Form = "";

        public frmPrintSelect()
        {
            InitializeComponent();
        }

        private void frmPrintSelect_Load(object sender, EventArgs e)
        {
            LoadStampanti();
            LoadReport();
            txtCopie.EditValue = 1;
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

        private void LoadReport()
        {
            try
            {
                string SQLString = "";
                SQLString = SQLString + "SELECT PathReport ";
                SQLString = SQLString + "From Report REP ";
                SQLString = SQLString + "Inner Join Report_Form REPFOR  ";
                SQLString = SQLString + "ON REP.IDReport=REPFOR.IDReport ";
                SQLString = SQLString + "Inner join Form FORM ";
                SQLString = SQLString + "ON REPFOR.IDForm=FORM.IDForm ";
                SQLString = SQLString + "Where REP.Attivo=1 ";
                SQLString = SQLString + "And FORM.NomeForm='" + Form + "' ";

                using (var cnn = new SqlConnection())
                {
                    cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                    cnn.Open();

                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = cnn;
                        cmd.CommandText = SQLString;

                        var dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read() != false)
                            {
                                cboReport.Properties.Items.Add(dr["PathReport"].ToString());
                                cboReport.SelectedIndex = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            Copie = Convert.ToInt32(txtCopie.EditValue);
            Stampante = cboStampante.EditValue.ToString();
            Report = cboReport.EditValue.ToString();
            this.Close();
        }

        private void butAnnulla_Click(object sender, EventArgs e)
        {
            Stampante = "";
            this.Close();
        }
    }
}
