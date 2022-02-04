using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOS.UI
{
    public partial class frmLog : Form
    {

        public string CodiceSoluzione = "";
        public int IDUtente = 0;

        public frmLog()
        {
            InitializeComponent();
        }

        private void frmLog_Load(object sender, EventArgs e)
        {
            global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();
            string SQL = "";

            SQL = SQL + "SELECT ";
            SQL = SQL + "LOGOP.IDOperazione ";
            SQL = SQL + ",LOGOP.TipoOperazione ";
            SQL = SQL + ",LOGOP.Tabella ";
            SQL = SQL + ",LOGOP.CodiceSoluzione ";
            SQL = SQL + ",LOGOP.DettaglioOperazione ";
            SQL = SQL + ",LOGUTE.NomeUtente ";
            SQL = SQL + ",LOGOP.DataOperazione ";
            SQL = SQL + "FROM [EOS].[dbo].[Log_Operazioni] LOGOP ";
            SQL = SQL + "LEFT JOIN Login_Utenti LOGUTE ";
            SQL = SQL + "ON LOGOP.IDUtente=LOGUTE.IDUtente ";
            SQL = SQL + "WHERE LOGOP.CodiceSoluzione='" + CodiceSoluzione + "' ";

            DataTable dt = new DataTable();
            dt = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

            gridLog.DataSource = null;
            gviewLog.Columns.Clear();
            gridLog.DataSource = dt;
            gviewLog.PopulateColumns();
            gridLog.ForceInitialize();

            System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmLog", "gridLog", IDUtente);

            if (str != null)
            {
                gridLog.FocusedView.RestoreLayoutFromStream(str);
                str.Seek(0, System.IO.SeekOrigin.Begin);
            }
            else
            {
                gviewLog.Columns["IDOperazione"].Visible = false;
                //gviewLog.Columns["CodiceMiscelaSolventi"].Width = 100;
                //gviewLog.Columns["TipologiaSolvente"].Width = 140;
                //gviewLog.Columns["Nome"].Width = 250;
                //gviewLog.Columns["Stato"].Width = 80;
                //gviewLog.Columns["Ubicazione"].Width = 250;
            }

            str = null;
        }

        private void repMemo_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void gviewLog_DoubleClick(object sender, EventArgs e)
        {
            int IDOperazione = 0;

            if (gviewLog.SelectedRowsCount == 1)
            {
                int[] selectedRows = gviewLog.GetSelectedRows();
                int selectedRow = 0;
                foreach (int rowHandle in selectedRows)
                {
                    selectedRow = rowHandle;
                }
                if (selectedRow > -1)
                {
                    IDOperazione = Int32.Parse(gviewLog.GetRowCellValue(selectedRow, "IDOperazione").ToString());

                    frmMemo MemoView = new frmMemo();
                    MemoView.IDOperazione = IDOperazione;
                    MemoView.Show();
                }
            }
        }
    }
}
