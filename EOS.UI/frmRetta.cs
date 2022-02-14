using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EOS.Core;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.IO;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraReports.UI;

namespace EOS.UI
{
    
    public partial class frmRetta : DevExpress.XtraEditors.XtraForm
    {
        int IDRetta = 0;
        int IDRettaCalled = 0;
        public int IDUtente = 0;
        bool DataAdd = false;
        bool DataChanged = false;
        bool changingMiscelaSolventi = false;
        bool changingSingoloSolvente = false;
        bool activeFilterTrafficLight = false;
        int restorefocus = 0;

        public frmRetta()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRetta_Load(object sender, EventArgs e)
        {
            EOS.Core.Control.Control_Configurazione ControlConfigurazione = new EOS.Core.Control.Control_Configurazione();
            EOS.Core.Model.Model_Configurazione ModelConfigurazione = new EOS.Core.Model.Model_Configurazione();

            ControlConfigurazione.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

            ModelConfigurazione = ControlConfigurazione.GetActiveConfiguration().First().Value;

            if (ModelConfigurazione.NewFromTemplate == false)
            {
                tbDuplica.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            ControlConfigurazione = null;
            ModelConfigurazione = null;

            LoadData();

            tbAggiorna.Enabled = true;
            tbNuovo.Enabled = true;
            tbSalva.Enabled = false;

            // TODO: questa riga di codice carica i dati nella tabella 'soluzioni.Rette_Tipologia'. È possibile spostarla o rimuoverla se necessario.
            this.rette_TipologiaTableAdapter.Fill(this.soluzioni.Rette_Tipologia);
            // TODO: questa riga di codice carica i dati nella tabella 'soluzioni.Login_Utenti'. È possibile spostarla o rimuoverla se necessario.
            this.login_UtentiTableAdapter.Fill(this.soluzioni.Login_Utenti);
            // TODO: questa riga di codice carica i dati nella tabella 'soluzioni.Composti_Stati'. È possibile spostarla o rimuoverla se necessario.
            this.composti_StatiTableAdapter.Fill(this.soluzioni.Composti_Stati);
            // TODO: questa riga di codice carica i dati nella tabella 'solventi._Solventi'. È possibile spostarla o rimuoverla se necessario.
            this.solventiTableAdapter.Fill(this.solventi._Solventi);
            // TODO: questa riga di codice carica i dati nella tabella 'lupin.MaterialiLottiSelectCommand'. È possibile spostarla o rimuoverla se necessario.
            this.materialiLottiSelectCommandTableAdapter.Fill(this.lupin.MaterialiLottiSelectCommand);
            // TODO: questa riga di codice carica i dati nella tabella 'soluzioni.Soluzioni_Tipologia'. È possibile spostarla o rimuoverla se necessario.
            this.soluzioni_TipologiaTableAdapter.Fill(this.soluzioni.Soluzioni_Tipologia);

        }

        private void LoadData()
        {
            LoadRetteGrid();
            if (IDRettaCalled != 0)
            {
                int rowHandle = gviewRette.LocateByValue("IDRetta", IDRettaCalled);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    restorefocus = 1;
                    gviewRette.FocusedRowHandle = rowHandle;
                    restorefocus = 0;
                }
            }
            //LoadControl();
            //LoadRetteCbo();
            //LoadRetteDetailsGrid();
        }

        private void LoadGrid()
        {
            LoadRetteGrid();
            if (IDRettaCalled != 0)
            {
                int rowHandle = gviewRette.LocateByValue("IDSoluzione", IDRettaCalled);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    restorefocus = 1;
                    gviewRette.FocusedRowHandle = rowHandle;
                    restorefocus = 0;
                    IDRettaCalled = 0;
                }
            }
            //LoadRetteDetailsGrid();
        }

        private void frmRetta_Shown(object sender, EventArgs e)
        {
            //abilitazionecontrolli();
        }

        private void gviewRette_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (restorefocus == 0)
            {
                if (DataAdd == false)
                {
                    IDRetta = Convert.ToInt32(gviewRette.GetFocusedRowCellValue("IDRetta"));

                    if (IDRetta != 0)
                    {
                        //LoadControl();
                        //LoadSoluzioniCbo();
                    }
                }
            }
        }



        private Boolean Validazione()
        {
            bool validato = true;
            string errori = "";

            if (cboTipologia.EditValue == null)
            {
                validato = false;
                errori = errori + "Deve essere specificata una tipologia di Retta di Taratura!\r\n";
            }
            else
            {
                if (txtNome.EditValue.ToString() == "")
                {
                    validato = false;
                    errori = errori + "Deve essere specificato un nome per la Retta di Taratura!\r\n";
                }

                if ((cboSingoloSolvente.EditValue == null) && (cboMiscelaSolventi.EditValue == null))
                {
                    validato = false;
                    errori = errori + "Deve essere specificata una Preparazione di Lavoro o un solvente scelto da anagrafica!\r\n";
                }

                if (cboStato.EditValue == null)
                {
                    validato = false;
                    errori = errori + "La Retta di Taratura deve avere uno stato specificato!\r\n";
                }

                if (cboTipologia.EditValue == null)
                {
                    validato = false;
                    errori = errori + "Deve essere specificata una tipologia di Retta di Taratura!\r\n";
                }

                if ((txtGiorniScadenza.EditValue == null) && (cboTipologia.EditValue.ToString() == "Soluzione MR Modello"))
                {
                    validato = false;
                    errori = errori + "Devono essere specificati dei giorni di default di scadenza della Retta di Taratura!\r\n";
                }
            }

            if (!validato)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(errori, "Validazione", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return validato;
        }

        private void LoadRetteGrid()
        {
            try
            {
                global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();
                string SQL = "";

                SQL = SQL + "SELECT RET.IDRetta ";
                SQL = SQL + "      ,RET.CodiceRetta ";
                SQL = SQL + "      ,RET.Tipologia AS TipologiaRetta ";
                SQL = SQL + "      ,RET.Nome ";
                SQL = SQL + "      ,COM.Nome AS Stato ";
                SQL = SQL + "      ,UBI.Ubicazione ";
                SQL = SQL + "      ,RET.NotePrescrittive ";
                SQL = SQL + "      ,RET.NoteDescrittive ";
                SQL = SQL + "      ,MAT.DenominazioneProdotto AS Solvente ";
                SQL = SQL + "      ,SOV.Nome AS MiscelaSolventi ";
                SQL = SQL + "      ,RET.DefaultGiorniScadenza ";
                SQL = SQL + "      ,RET.DataScadenza ";
                SQL = SQL + "      ,RET.DataCreazione ";
                SQL = SQL + "      ,UTE.NomeUtente AS UtenteCreazione ";
                SQL = SQL + "      ,cast(0 as bit) AS Seleziona ";
                SQL = SQL + "  FROM dbo.Rette RET ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.SchedeDocumenti [CER] ";
                SQL = SQL + "  ON RET.IDSchedaDocumenti=[CER].IDSchedaDocumenti ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Materiali MAT ";
                SQL = SQL + "  ON [CER].IDMateriale=MAT.IDMateriale ";
                SQL = SQL + "  LEFT JOIN Solventi SOV ";
                SQL = SQL + "  ON RET.IDSolvente=SOV.IDSolvente ";
                SQL = SQL + "  LEFT JOIN Login_Utenti UTE ";
                SQL = SQL + "  ON RET.IDUtente=UTE.IDUtente ";
                SQL = SQL + "  LEFT JOIN Composti_Stati COM ";
                SQL = SQL + "  ON RET.IDStato=COM.ID ";
                SQL = SQL + "  ORDER BY RET.CodiceSoluzione ";

                DataTable dt = new DataTable();
                dt = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                gridRette.DataSource = null;
                gviewRette.Columns.Clear();
                gridRette.DataSource = dt;
                gviewRette.PopulateColumns();
                gridRette.ForceInitialize();
                gviewRette.Columns["IDRette"].Visible = false;

                System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmRette", "gridRette", IDUtente);

                if (str != null)
                {
                    gridRette.FocusedView.RestoreLayoutFromStream(str);
                    str.Seek(0, System.IO.SeekOrigin.Begin);
                }
                else
                {
                    gviewRette.Columns["Solvente"].Visible = false;
                    gviewRette.Columns["MiscelaSolventi"].Visible = false;

                    gviewRette.Columns["CodiceRetta"].Width = 100;
                    gviewRette.Columns["TipologiaRetta"].Width = 140;
                    gviewRette.Columns["Nome"].Width = 250;
                    gviewRette.Columns["Stato"].Width = 80;
                    gviewRette.Columns["Ubicazione"].Width = 250;
                }

                str = null;
            }
            catch (Exception e)
            {

            }
        }
    }
}