using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace EOS.UI
{
    public partial class frmMemo : Form
    {

        public int IDOperazione = 0;

        public frmMemo()
        {
            InitializeComponent();
        }

        private void frmMemo_Load(object sender, EventArgs e)
        {
            string DettaglioOperazione = "";
            string SQLString = "";

            SQLString = "Select DettaglioOperazione from Log_Operazioni WHERE IDOperazione='" + IDOperazione + "' ";

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
                        while (dr.Read())
                        {
                            DettaglioOperazione = dr["DettaglioOperazione"].ToString();
                        }
                    }
                }
            }

            memoDettaglioOperazione.EditValue= DettaglioOperazione;
        }
    }
}
