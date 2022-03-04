using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.Utils;
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
            // TODO: questa riga di codice carica i dati nella tabella 'lupin.UbicazioniSelectCommand'. È possibile spostarla o rimuoverla se necessario.
            this.ubicazioniSelectCommandTableAdapter.Fill(this.lupin.UbicazioniSelectCommand);
            // TODO: questa riga di codice carica i dati nella tabella 'lupin.UtensiliSelectCommand'. È possibile spostarla o rimuoverla se necessario.
            this.utensiliSelectCommandTableAdapter.Fill(this.lupin.UtensiliSelectCommand);
            // TODO: questa riga di codice carica i dati nella tabella 'lupin.ApparecchiSelectCommand'. È possibile spostarla o rimuoverla se necessario.
            this.apparecchiSelectCommandTableAdapter.Fill(this.lupin.ApparecchiSelectCommand);
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
        }

        private void frmRetta_Shown(object sender, EventArgs e)
        {
            abilitazionecontrolli();
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
                        LoadControl();
                        LoadRetteCbo();
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
                SQL = SQL + "      ,RET.Tipologia ";
                SQL = SQL + "      ,RET.Nome ";
                SQL = SQL + "      ,COM.Nome AS Stato ";
                SQL = SQL + "      ,MAT.DenominazioneProdotto AS Solvente ";
                SQL = SQL + "      ,SOV.Nome AS MiscelaSolventi ";
                SQL = SQL + "      ,RET.DefaultGiorniScadenza ";
                SQL = SQL + "      ,RET.DataScadenza ";
                SQL = SQL + "	  ,RET.Note ";
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
                SQL = SQL + "  ORDER BY RET.CodiceRetta ";

                DataTable dt = new DataTable();
                dt = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                gridRette.DataSource = null;
                //gviewRette.Columns.Clear();
                gridRette.DataSource = dt;
                //gviewRette.PopulateColumns();
                //gridRette.ForceInitialize();
                gviewRette.Columns[0].Visible = false;

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
                    gviewRette.Columns["Tipologia"].Width = 140;
                    gviewRette.Columns["Nome"].Width = 250;
                    gviewRette.Columns["Stato"].Width = 80;
                }

                str = null;
            }
            catch (Exception e)
            {

            }
        }

        private void LoadRetteDetailsGrid()
        {
            try
            {
                int idRettaMaster = 0;

                if (gviewRette.SelectedRowsCount == 1)
                {
                    int[] selectedRows = gviewRette.GetSelectedRows();
                    int selectedRow = 0;
                    foreach (int rowHandle in selectedRows)
                    {
                        selectedRow = rowHandle;
                    }
                    if (selectedRow > -1)
                    {
                        idRettaMaster = Int32.Parse(gviewRette.GetRowCellValue(selectedRow, "IDRetta").ToString());
                    }
                }

                global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();

                string SQLString = "";

                SQLString = SQLString + "SELECT ";
                SQLString = SQLString + "SOL.IDSoluzione, ";
                SQLString = SQLString + "SOL.CodiceSoluzione, ";
                SQLString = SQLString + "SOL.Tipologia, ";
                SQLString = SQLString + "SOL.Nome as NomeSoluzione, ";
                SQLString = SQLString + "SOL.IDStato, ";
                SQLString = SQLString + "SOL.UMVolumeFinale, ";
                SQLString = SQLString + "SOL.VolumeFinale, ";
                SQLString = SQLString + "SOL.IDSolvente, ";
                SQLString = SQLString + "SOL.IDSchedaDocumenti, ";
                SQLString = SQLString + "SOL.IDApparecchio, ";
                SQLString = SQLString + "SOL.IDUtensile, ";
                SQLString = SQLString + "SOL.IDApparecchio2, ";
                SQLString = SQLString + "SOL.IDUtensile2, ";
                SQLString = SQLString + "SOL.DefaultGiorniScadenza, ";
                SQLString = SQLString + "SOL.DataScadenza, ";
                SQLString = SQLString + "SOL.NotePrescrittive, ";
                SQLString = SQLString + "SOL.NoteDescrittive, ";
                SQLString = SQLString + "Sol.DataCreazione, ";
                SQLString = SQLString + "Sol.IDUtente, ";
                SQLString = SQLString + "Sol.IDUbicazione ";
                SQLString = SQLString + "FROM Rette RET ";
                SQLString = SQLString + "INNER JOIN [dbo].[Rette_Soluzioni] RETSOL ";
                SQLString = SQLString + "ON RET.IDRetta=RETSOL.IDRetta ";
                SQLString = SQLString + "INNER JOIN Soluzioni SOL ";
                SQLString = SQLString + "ON RETSOL.IDSoluzione=SOL.IDSoluzione ";
                SQLString = SQLString + "where RET.IDRetta={0} ";
                SQLString = SQLString + "Order By SOL.IDSoluzione ";

                SQLString = string.Format(SQLString, idRettaMaster);

                DataTable dt = new DataTable();
                dt = DB.GetDataTable(SQLString, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                gridRetteDetails.DataSource = null;
                //gviewRetteDetails.Columns.Clear();
                gridRetteDetails.DataSource = dt;

                //gviewRetteDetails.PopulateColumns();
                //gridRetteDetails.ForceInitialize();
                gviewRetteDetails.Columns[0].Visible = false;

                System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmRette", "gridRetteDetails", IDUtente);

                if (str != null)
                {
                    gridRetteDetails.FocusedView.RestoreLayoutFromStream(str);
                    str.Seek(0, System.IO.SeekOrigin.Begin);
                }

                str = null;
            }
            catch (Exception e)
            {

            }
        }

        private void LoadRetteCbo()
        {
            try
            {
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

                EOS.Core.Control.Control_Utenti ctlUtente = new EOS.Core.Control.Control_Utenti();

                if (!ctlUtente.CloneAllowedCheck(IDUtente))
                {
                    string filter = " (NomeTipologia <> 'Retta Taratura Modello') ";

                    cboTipologia.Properties.View.ActiveFilter.Add(cboTipologia.Properties.View.Columns["ID"], new ColumnFilterInfo(filter, ""));
                }

                ctlUtente = null;

                if (gviewRette.GetRowCellValue(gviewRette.FocusedRowHandle, "Stato").ToString() != "Preparazione")
                {
                    string filter = " (ID <> 5) ";

                    cboStato.Properties.View.ActiveFilter.Add(cboStato.Properties.View.Columns["ID"], new ColumnFilterInfo(filter, ""));
                }
            }
            catch (Exception e)
            {

            }
        }

        private void LoadControl()
        {
            if (gviewRette.SelectedRowsCount == 1)
            {
                removecontrolhandler();
                IDRetta = Convert.ToInt32(gviewRette.GetFocusedRowCellValue("IDRetta"));

                Core.Model.Model_Rette ModelRetta = new EOS.Core.Model.Model_Rette();
                Core.Control.Control_Rette ControlRetta = new Core.Control.Control_Rette();

                ControlRetta.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                ModelRetta = ControlRetta.GetRettaByID(IDRetta).First().Value;

                txtCodiceRetta.EditValue = ModelRetta.CodiceRetta;
                cboTipologia.EditValue = ModelRetta.Tipologia;
                txtNome.EditValue = ModelRetta.Nome;
                txtNote.EditValue = ModelRetta.Note;
                cboSingoloSolvente.EditValue = ModelRetta.IDSchedaDocumenti;
                cboMiscelaSolventi.EditValue = ModelRetta.IDSolvente;
                cboStato.EditValue = ModelRetta.IDStato;
                cboUtenteCreazione.EditValue = ModelRetta.IDUtente;
                txtGiorniScadenza.EditValue = ModelRetta.DefaultGiorniScadenza;

                if (ModelRetta.DataCreazione.ToString() == "01/01/0001 00:00:00")
                {
                    txtDataCreazione.EditValue = null;
                }
                else
                {
                    txtDataCreazione.EditValue = ModelRetta.DataCreazione;
                }
                if (ModelRetta.DataScadenza.ToString() == "01/01/0001 00:00:00")
                {
                    txtDataScadenza.EditValue = null;
                }
                else
                {
                    txtDataScadenza.EditValue = ModelRetta.DataScadenza;
                }

                ControlRetta = null;
                ModelRetta = null;

                LoadRetteDetailsGrid();

                addcontrolhandler();

                abilitazionecontrolli();
            }
            else
            {
                removecontrolhandler();
                IDRetta = 0;

                txtCodiceRetta.EditValue = "";
                cboTipologia.EditValue = null;
                txtNome.EditValue = "";
                txtNote.EditValue = "";
                cboSingoloSolvente.EditValue = null;
                cboMiscelaSolventi.EditValue = null;
                cboStato.EditValue = null;
                txtDataCreazione.EditValue = "";
                cboUtenteCreazione.EditValue = null;
                txtGiorniScadenza.EditValue = null;
                txtDataScadenza.EditValue = "";

                LoadRetteDetailsGrid();

                addcontrolhandler();

                abilitazionecontrolli();
            }
        }

        private void abilitazionecontrolli()
        {
            if (!DBNull.Value.Equals(cboStato.EditValue))
            {
                if (Convert.ToInt32(cboStato.EditValue) != 5)
                {
                    cboTipologia.Enabled = false;
                    txtNome.Enabled = false;
                    txtNote.Enabled = false;
                    cboSingoloSolvente.Enabled = false;
                    cboMiscelaSolventi.Enabled = false;
                    cboStato.Enabled = true;
                    txtDataCreazione.Enabled = false;
                    cboUtenteCreazione.Enabled = false;
                    txtGiorniScadenza.Enabled = false;
                    txtDataScadenza.Enabled = false;

                    if ((IDRetta != 0) && (cboTipologia.EditValue.ToString() == "Retta Taratura Modello") && (Convert.ToInt32(cboStato.EditValue) == 6))
                    {
                        tbDuplica.Enabled = true;
                    }
                    else
                    {
                        tbDuplica.Enabled = false;
                    }

                    butScollega.Enabled = false;
                    butScollegaEAnnulla.Enabled = false;
                    butModifica.Enabled = false;
                    butAggiungi.Enabled = false;
                }
                else
                {
                    cboTipologia.Enabled = true;
                    txtNome.Enabled = true;
                    txtNote.Enabled = true;
                    cboSingoloSolvente.Enabled = true;
                    cboMiscelaSolventi.Enabled = true;
                    cboStato.Enabled = true;
                    txtDataCreazione.Enabled = false;
                    cboUtenteCreazione.Enabled = false;
                    txtDataScadenza.Enabled = false;

                    if (gviewRette.RowCount != 0)
                    {
                        if (gviewRetteDetails.RowCount != 0)
                        {
                            butScollega.Enabled = true;
                            butScollegaEAnnulla.Enabled = true;
                            butModifica.Enabled = true;
                        }
                        else
                        {
                            butScollega.Enabled = false;
                            butScollegaEAnnulla.Enabled = false;
                            butModifica.Enabled = false;
                        }

                        if (DataAdd == false)
                        {
                            butAggiungi.Enabled = true;
                        }
                    }
                    else
                    {
                        butScollega.Enabled = false;
                        butScollegaEAnnulla.Enabled = false;
                        butModifica.Enabled = false;
                        butAggiungi.Enabled = false;

                        if (DataAdd == false)
                        {
                            butAggiungi.Enabled = true;
                        }
                    }

                    if ((IDRetta != 0) && (cboTipologia.EditValue.ToString() == "Retta Taratura Modello"))
                    {
                        if (Convert.ToInt32(cboStato.EditValue) == 6)
                        {
                            tbDuplica.Enabled = true;
                        }
                        else
                        {
                            tbDuplica.Enabled = false;
                        }
                        txtGiorniScadenza.Enabled = true;
 
                        removecontrolhandler();
                        addcontrolhandler();
                    }
                    else
                    {
                        tbDuplica.Enabled = false;
                        txtGiorniScadenza.Enabled = true;
                    }
                }
            }
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
            LoadControl();
            LoadRetteCbo();
            LoadRetteDetailsGrid();
        }

        private void LoadGrid()
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
                    IDRettaCalled = 0;
                }
            }
            LoadRetteDetailsGrid();
        }

        private void addcontrolhandler()
        {
            this.cboSingoloSolvente.EditValueChanged += new System.EventHandler(this.SolventeSingolo);
            this.cboMiscelaSolventi.EditValueChanged += new System.EventHandler(this.SolventeMiscela);
            this.cboTipologia.EditValueChanged += new System.EventHandler(this.CambioTipologia);
            this.cboStato.EditValueChanged += new System.EventHandler(this.CambioStato);

            this.cboStato.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboUtenteCreazione.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboSingoloSolvente.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboMiscelaSolventi.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboTipologia.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtNome.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtNote.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtGiorniScadenza.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtDataScadenza.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtDataCreazione.EditValueChanged += new System.EventHandler(this.DataChange);
        }

        private void removecontrolhandler()
        {
            this.cboSingoloSolvente.EditValueChanged -= new System.EventHandler(this.SolventeSingolo);
            this.cboMiscelaSolventi.EditValueChanged -= new System.EventHandler(this.SolventeMiscela);
            this.cboTipologia.EditValueChanged -= new System.EventHandler(this.CambioTipologia);
            this.cboStato.EditValueChanged -= new System.EventHandler(this.CambioStato);

            this.cboStato.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboUtenteCreazione.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboSingoloSolvente.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboMiscelaSolventi.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboTipologia.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtNome.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtNote.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtGiorniScadenza.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtDataScadenza.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtDataCreazione.EditValueChanged -= new System.EventHandler(this.DataChange);
        }

        private void DataChange(object sender, EventArgs e)
        {
            DataChanged = true;
            tbAggiorna.Enabled = true;
            tbNuovo.Enabled = true;
            tbSalva.Enabled = true;
        }

        private void SolventeSingolo(object sender, EventArgs e)
        {
            if (!changingSingoloSolvente)
            {
                removecontrolhandler();
                changingSingoloSolvente = true;
                cboMiscelaSolventi.EditValue = null;
                addcontrolhandler();
                changingSingoloSolvente = false;
            }
        }

        private void SolventeMiscela(object sender, EventArgs e)
        {
            if (!changingMiscelaSolventi)
            {
                removecontrolhandler();
                changingMiscelaSolventi = true;
                cboSingoloSolvente.EditValue = null;
                addcontrolhandler();
                changingMiscelaSolventi = false;
            }
        }

        private void CambioTipologia(object sender, EventArgs e)
        {
            abilitazionecontrolli();
        }
        private void CambioStato(object sender, EventArgs e)
        {
            abilitazionecontrolli();
        }

        private void tbNuovo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if ((DataChanged) || (DataAdd))
            {
                DialogResult response = XtraMessageBox.Show("Attenzione, ci sono dei dati non salvati, se si prosegue verranno persi. Continuare?", "Dati non salvati", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (response == DialogResult.No)
                {
                    return;
                }
            }

            butAggiungi.Enabled = false;
            butModifica.Enabled = false;
            butScollega.Enabled = false;
            butScollegaEAnnulla.Enabled = false;
            
            cboTipologia.Enabled = true;
            txtNome.Enabled = true;
            txtNote.Enabled = true;
            cboSingoloSolvente.Enabled = true;
            cboMiscelaSolventi.Enabled = true;
            txtGiorniScadenza.Enabled = true;
            txtDataScadenza.Enabled = false;
            cboStato.Enabled = true;
            txtDataCreazione.Enabled = false;
            cboUtenteCreazione.Enabled = false;

            removecontrolhandler();

            txtCodiceRetta.EditValue = "";
            cboTipologia.EditValue = null;
            txtNome.EditValue = "";
            txtNote.EditValue = "";
            cboSingoloSolvente.EditValue = null;
            cboMiscelaSolventi.EditValue = null;
            cboStato.EditValue = 5;
            txtDataCreazione.EditValue = DateTime.Now;
            cboUtenteCreazione.EditValue = IDUtente;
            txtGiorniScadenza.EditValue = null;
            txtDataScadenza.EditValue = "";

            addcontrolhandler();

            gridRetteDetails.DataSource = null;
            DataChanged = false;
            DataAdd = true;
            XtraMessageBox.Show("Inserire i dati del nuovo elemento.", "Nuovo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tbSalva_ItemClick(object sender, ItemClickEventArgs e)
        {
            int ProcediACambioStatoDaModello = 1;

            if (Validazione())
            {
                int ret = 0;
                bool CambioStatoOK = false;

                removecontrolhandler();

                Core.Model.Model_Rette ModelRette = new Core.Model.Model_Rette();
                Core.Control.Control_Rette ControlRette = new Core.Control.Control_Rette();

                if (DataAdd)
                {
                    ControlRette.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                    ModelRette.Tipologia = Convert.ToString(cboTipologia.EditValue);
                    ModelRette.Nome = Convert.ToString(txtNome.EditValue);
                    ModelRette.Note = Convert.ToString(txtNote.EditValue);
                    ModelRette.IDSchedaDocumenti = Convert.ToInt32(cboSingoloSolvente.EditValue);
                    ModelRette.IDSolvente = Convert.ToInt32(cboMiscelaSolventi.EditValue);
                    ModelRette.IDStato = Convert.ToInt32(cboStato.EditValue);
                    ModelRette.DataCreazione = DateTime.Now;
                    ModelRette.IDUtente = IDUtente;

                    if ((txtGiorniScadenza.EditValue.ToString() != "") && (txtGiorniScadenza.EditValue != null))
                    {
                        ModelRette.DefaultGiorniScadenza = Convert.ToInt32(txtGiorniScadenza.EditValue);
                    }
                    else
                    {
                        ModelRette.DefaultGiorniScadenza = Convert.ToInt32(null);
                    }

                    ControlRette.IDUtente = IDUtente;

                    ret = ControlRette.AddRette(ModelRette);

                    IDRettaCalled = ret;

                    CambioStatoOK = true;
                }
                else
                {
                    ControlRette.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                    ModelRette = ControlRette.GetRettaByID(IDRetta).First().Value;

                    EOS.Core.Control.Control_RetteSoluzioni ControlRetteSoluzioni = new EOS.Core.Control.Control_RetteSoluzioni();
                    ControlRetteSoluzioni.ConnectionString= System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                    if ((ModelRette.Tipologia == "Retta Taratura Modello") && (cboTipologia.EditValue.ToString() != "Retta Taratura Modello") && (ControlRetteSoluzioni.countModelSolution(ModelRette.IDRetta)>0))
                    {
                        DialogResult dialogResult = MessageBox.Show("Salva", "Si sta salvando la Retta cambiandone la tipologia da modello ad altro tipo, se si prosegue con l'aggiornamento tutte le soluzioni modello che compongono i punti della retta modello verranno scollegate e annullate. Proseguire con il salvataggio?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            ProcediACambioStatoDaModello = 2;
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            ProcediACambioStatoDaModello = 0;
                            XtraMessageBox.Show("Il salvataggio è stato annullato.", "Salva", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    if (ProcediACambioStatoDaModello>0)
                    {
                        if ((ModelRette.IDStato != 5) && (Convert.ToInt32(cboStato.EditValue) == 5) && (ModelRette.IDStato != Convert.ToInt32(cboStato.EditValue)))
                        {
                            CambioStatoOK = false;
                        }
                        else
                        {
                            ModelRette.Tipologia = Convert.ToString(cboTipologia.EditValue);
                            ModelRette.Nome = Convert.ToString(txtNome.EditValue);
                            ModelRette.Note = Convert.ToString(txtNote.EditValue);
                            ModelRette.IDSchedaDocumenti = Convert.ToInt32(cboSingoloSolvente.EditValue);
                            ModelRette.IDSolvente = Convert.ToInt32(cboMiscelaSolventi.EditValue);
                            ModelRette.DataCreazione = Convert.ToDateTime(txtDataCreazione.EditValue);
                            ModelRette.IDUtente = IDUtente;

                            if ((txtGiorniScadenza.EditValue.ToString() != "") && (txtGiorniScadenza.EditValue != null))
                            {
                                ModelRette.DefaultGiorniScadenza = Convert.ToInt32(txtGiorniScadenza.EditValue);
                            }
                            else
                            {
                                ModelRette.DefaultGiorniScadenza = Convert.ToInt32(null);
                            }

                            ControlRette.IDUtente = IDUtente;

                            ret = ControlRette.UpdateRette(ModelRette);

                            //annullamento delle soluzioni modello per cambio tipologia da retta modello a retta di altro tipo
                            if (ProcediACambioStatoDaModello == 2)
                            {
                                ControlRetteSoluzioni.CambiaStatoScollegaSoluzioni(ModelRette.IDRetta, ModelRette.IDStato, true, false, true);
                            }

                            IDRettaCalled = IDRetta;

                            CambioStatoOK = true;
                        }
                    }

                    ControlRetteSoluzioni = null;

                }

                ControlRette = null;
                ModelRette = null;

                addcontrolhandler();

                if ((CambioStatoOK) && (ProcediACambioStatoDaModello == 1))
                {
                    if (ret != 0)
                    {
                        System.IO.MemoryStream str = new System.IO.MemoryStream();
                        gridRette.FocusedView.SaveLayoutToStream(str);
                        str.Seek(0, System.IO.SeekOrigin.Begin);
                        frmLogin.SaveUserPreferences("frmRette", "gridRette", str, IDUtente);

                        //DataRowView current = (DataRowView)soluzioniBindingSource.Current;
                        //soluzioniBindingSource.Filter = "IDSoluzione=" + current["IDSoluzione"];

                        tbAggiorna.Enabled = true;
                        tbNuovo.Enabled = true;
                        tbSalva.Enabled = false;
                        DataAdd = false;
                        DataChanged = false;
                        activeFilterTrafficLight = true;
                        LoadGrid();
                        LoadControl();
                        activeFilterTrafficLight = false;

                        str = frmLogin.LoadUserPreferences("frmRette", "gridRette", IDUtente);

                        if (str != null)
                        {
                            gridRette.FocusedView.RestoreLayoutFromStream(str);
                            str.Seek(0, System.IO.SeekOrigin.Begin);
                        }

                        str = null;

                        abilitazionecontrolli();
                        XtraMessageBox.Show("I dati sono stati salvati.", "Salva", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("Il salvataggio dei dati è terminato con errore.", "Salva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Non è possibile tornare allo stato Preparazione da un altro stato.", "Salva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                XtraMessageBox.Show("Impossibile salvare i dati compilare tutti i campi obbligatori.", "Salva", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbAggiorna_ItemClick(object sender, ItemClickEventArgs e)
        {

            if ((DataChanged) || (DataAdd))
            {
                DialogResult response = XtraMessageBox.Show("Attenzione, ci sono dei dati non salvati, se si prosegue verranno persi. Continuare?", "Dati non salvati", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (response == DialogResult.No)
                {
                    XtraMessageBox.Show("Aggiornamento visualizzazione dati annullato.", "Aggiorna visualizzazione dati", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            System.IO.MemoryStream str = new System.IO.MemoryStream();
            gridRette.FocusedView.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            frmLogin.SaveUserPreferences("frmRette", "gridRette", str, IDUtente);

            DataAdd = false;
            DataChanged = false;
            IDRettaCalled = IDRetta;
            activeFilterTrafficLight = true;
            LoadData();
            activeFilterTrafficLight = false;

            str = frmLogin.LoadUserPreferences("frmRette", "gridRette", IDUtente);

            if (str != null)
            {
                gridRette.FocusedView.RestoreLayoutFromStream(str);
                str.Seek(0, System.IO.SeekOrigin.Begin);
            }

            str = null;

            abilitazionecontrolli();

            tbAggiorna.Enabled = true;
            tbNuovo.Enabled = true;
            tbSalva.Enabled = false;
            XtraMessageBox.Show("la visualizzazione dei dati è stata aggiornata.", "Aggiorna visualizzazione dati", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GridSize(object sender, CancelEventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;
            edit.Properties.PopupFormMinSize = new Size(1000, 400);
        }

        private void GridSizeApparecchi(object sender, CancelEventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;
            edit.Properties.PopupFormMinSize = new Size(1000, 400);
            edit.Properties.PopupView.Columns[0].Visible = false;
            edit.Properties.PopupView.Columns[2].Visible = false;
            edit.Properties.PopupView.Columns[4].Visible = false;
            edit.Properties.PopupView.Columns[5].Visible = false;
            edit.Properties.PopupView.Columns[6].Visible = false;
            edit.Properties.PopupView.Columns[8].Visible = false;
            edit.Properties.PopupView.Columns[9].Visible = false;
            edit.Properties.PopupView.Columns[10].Visible = false;
            edit.Properties.PopupView.Columns[11].Visible = false;
            edit.Properties.PopupView.Columns[12].Visible = false;
            edit.Properties.PopupView.Columns[13].Visible = false;
            edit.Properties.PopupView.Columns[14].Visible = false;
            edit.Properties.PopupView.Columns[15].Visible = false;
            edit.Properties.PopupView.Columns[16].Visible = false;
            edit.Properties.PopupView.Columns[17].Visible = false;
            edit.Properties.PopupView.Columns[18].Visible = false;
            edit.Properties.PopupView.Columns[19].Visible = false;
            edit.Properties.PopupView.Columns[20].Visible = false;
            edit.Properties.PopupView.Columns[21].Visible = false;

            edit.Properties.PopupView.Columns[3].Width = 250;

            string filter = " (Tipologia = 'Bilancia' OR Tipologia = 'Pipetta') AND IDStatoServizio=1 ";

            edit.Properties.View.ActiveFilterString = filter;
        }

        private void gridRette_DoubleClick(object sender, EventArgs e)
        {
            object cellValue = gviewRette.GetRowCellValue(gviewRette.FocusedRowHandle, "IDRetta");
            int ID = Convert.ToInt32(cellValue);

            frmRetta Retta = new frmRetta();
            Retta.IDUtente = IDUtente;

            Retta.IDRettaCalled = ID;

            Retta.MdiParent = this.ParentForm;
            Retta.Show();
        }

        private void gridRetteDetails_DoubleClick(object sender, EventArgs e)
        {

        }

        private void tbDuplica_ItemClick(object sender, ItemClickEventArgs e)
        {
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Da adattare a rette
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //frmDuplica Duplica = new frmDuplica();
            //Duplica.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
            //Duplica.TipoElemento = "Soluzione";
            //Duplica.IDElemento = IDSoluzione;
            //Duplica.IDSchedaDocumenti = Convert.ToInt32(cboSingoloSolvente.EditValue);
            //Duplica.IDSolvente = Convert.ToInt32(cboMiscelaSolventi.EditValue);
            //Duplica.IDApparecchio = Convert.ToInt32(cboApparecchio.EditValue);
            //Duplica.IDUtensile = Convert.ToInt32(cboUtensile.EditValue);
            //Duplica.IDApparecchio2 = Convert.ToInt32(cboApparecchio2.EditValue);
            //Duplica.IDUtensile2 = Convert.ToInt32(cboUtensile2.EditValue);
            //Duplica.IDUtente = IDUtente;
            //Duplica.ShowDialog();

            //if (Duplica.newIDElemento != 0)
            //{
            //    IDSoluzioneCalled = Duplica.newIDElemento;
            //    LoadData();
            //}
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Da adattare a rette
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void tbStampa_ItemClick(object sender, ItemClickEventArgs e)
        {
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Da adattare a rette
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //int Copie = 0;
            //string Report = "";
            //string Stampante = "";

            //frmPrintSelect PrintSelect = new frmPrintSelect();
            //PrintSelect.Form = "frmSoluzioni";
            //PrintSelect.ShowDialog();

            //Copie = PrintSelect.Copie;
            //Stampante = PrintSelect.Stampante;
            //Report = PrintSelect.Report;

            //if (Stampante != "")
            //{
            //    int[] rowHandles = gviewSoluzioni.GetSelectedRows();

            //    for (int c = 0; c < gviewSoluzioni.SelectedRowsCount; c++)
            //    {
            //        XtraReport myReport = new XtraReport();
            //        myReport = XtraReport.FromFile(Report);

            //        myReport.Parameters["parCodiceSoluzione"].Value = gviewSoluzioni.GetRowCellValue(rowHandles[c], "CodiceSoluzione").ToString();
            //        myReport.Parameters["parTipoSoluzione"].Value = "MR";
            //        myReport.Parameters["parConnectionString"].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

            //        myReport.CreateDocument();

            //        for (int i = 0; i < Copie; i++)
            //        {
            //            myReport.Print(Stampante);
            //        }
            //    }
            //}
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Da adattare a rette
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void tbEsportaExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Da adattare a rette
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //string idlist = "";

            //gviewSoluzioni.MoveLast();
            //gviewSoluzioni.MoveFirst();

            //for (int i = 0; i < gviewSoluzioni.DataRowCount; i++)
            //{
            //    if (Convert.ToBoolean(gviewSoluzioni.GetRowCellValue(i, "Seleziona")))
            //    {
            //        idlist = idlist + Convert.ToString(gviewSoluzioni.GetRowCellValue(i, "IDSoluzione")) + ", ";
            //    }
            //}

            //if (idlist != "")
            //{
            //    idlist = idlist.Substring(0, idlist.Length - 2);

            //    global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();
            //    string SQL = "";

            //    SQL = SQL + "SELECT SOL.IDSoluzione ";
            //    SQL = SQL + "      ,SOL.CodiceSoluzione ";
            //    SQL = SQL + "      ,SOL.Tipologia AS TipologiaSoluzione ";
            //    SQL = SQL + "      ,SOL.Nome ";
            //    SQL = SQL + "      ,COM.Nome AS Stato ";
            //    SQL = SQL + "      ,SOL.NotePrescrittive ";
            //    SQL = SQL + "      ,SOL.NoteDescrittive ";
            //    SQL = SQL + "      ,UBI.Ubicazione ";
            //    SQL = SQL + "      ,MAT.DenominazioneProdotto AS Solvente ";
            //    SQL = SQL + "      ,SOV.Nome AS MiscelaSolventi ";
            //    SQL = SQL + "      ,SOL.VolumeFinale ";
            //    SQL = SQL + "      ,SOL.UMVolumeFinale ";
            //    SQL = SQL + "      ,SOL.DefaultGiorniScadenza ";
            //    SQL = SQL + "      ,convert(varchar(20),SOL.DataPreparazione,23) as DataPreparazione ";
            //    SQL = SQL + "      ,convert(varchar(20),SOL.DataScadenza,23) as DataScadenza ";
            //    SQL = SQL + "      ,convert(varchar(20),SOL.DataCreazione,23) as DataCreazione ";
            //    SQL = SQL + "      ,UTE.NomeUtente AS UtenteCreazione ";
            //    SQL = SQL + "  FROM dbo.Soluzioni SOL ";
            //    SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Ubicazioni UBI ";
            //    SQL = SQL + "  ON SOL.IDUbicazione=UBI.IDUbicazione ";
            //    SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.SchedeDocumenti [CER] ";
            //    SQL = SQL + "  ON SOL.IDSchedaDocumenti=[CER].IDSchedaDocumenti ";
            //    SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Materiali MAT ";
            //    SQL = SQL + "  ON [CER].IDMateriale=MAT.IDMateriale ";
            //    SQL = SQL + "  LEFT JOIN Solventi SOV ";
            //    SQL = SQL + "  ON SOL.IDSolvente=SOV.IDSolvente ";
            //    SQL = SQL + "  LEFT JOIN Login_Utenti UTE ";
            //    SQL = SQL + "  ON SOL.IDUtente=UTE.IDUtente ";
            //    SQL = SQL + "  LEFT JOIN Composti_Stati COM ";
            //    SQL = SQL + "  ON SOL.IDStato=COM.ID ";
            //    SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Apparecchi APP ";
            //    SQL = SQL + "  ON SOL.IDApparecchio=APP.IDApparecchio ";
            //    SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Utensili UTI ";
            //    SQL = SQL + "  ON SOL.IDUtensile=UTI.IDUtensile ";
            //    SQL = SQL + "  WHERE SOL.IDSoluzione in (" + idlist + ") ";
            //    SQL = SQL + "  ORDER BY SOL.CodiceSoluzione ";

            //    DataTable dt = new DataTable();
            //    dt = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

            //    string SQLString = "";

            //    SQLString = SQLString + "SELECT ";
            //    SQLString = SQLString + "SOL.[CodiceSoluzione] as CodiceSoluzioneMR, ";
            //    SQLString = SQLString + "SOLDET.[IDSoluzioneMaster], ";
            //    SQLString = SQLString + "SOLDET.[IDSoluzioneDetail], ";
            //    SQLString = SQLString + "SOLDET.[IDSchedaDocumenti], ";
            //    SQLString = SQLString + "MT.[Nome] as Tipologia_MR, ";
            //    SQLString = SQLString + "SOLDET.[CAS], ";
            //    SQLString = SQLString + "MAT.DenominazioneProdotto as MaterialeMR, ";
            //    SQLString = SQLString + "SOLDET.[IDSoluzione], ";
            //    SQLString = SQLString + "Sol.Tipologia as TipoSoluzione, ";
            //    SQLString = SQLString + "SOL.Nome as NomeSoluzione, ";
            //    SQLString = SQLString + "SOLDET.[UM_Prelievo], ";
            //    SQLString = SQLString + "SOLDET.[Quantita_Prelievo], ";
            //    SQLString = SQLString + "APP.NumeroApparecchio as ApparecchioPrelievo, ";
            //    SQLString = SQLString + "UTE.Nome as UtensilePrelievo, ";
            //    SQLString = SQLString + "APP2.NumeroApparecchio as ApparecchioPrelievo2, ";
            //    SQLString = SQLString + "UTE2.Nome as UtensilePrelievo2, ";
            //    SQLString = SQLString + "SOLDET.[Note], ";
            //    SQLString = SQLString + "SOLDET.[Concentrazione], ";
            //    SQLString = SQLString + "convert(varchar(20),SOLDET.[DataScadenza],23) as DataScadenza ";
            //    SQLString = SQLString + "FROM Soluzioni_Details SOLDET ";
            //    SQLString = SQLString + "LEFT JOIN Soluzioni SOL ";
            //    SQLString = SQLString + "ON SOLDET.IDSoluzioneMaster=SOL.IDSoluzione ";
            //    SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
            //    SQLString = SQLString + "ON SOLDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
            //    SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Materiali MAT ";
            //    SQLString = SQLString + "ON SCDOC.IDMateriale=MAT.IDMateriale ";
            //    SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Apparecchi APP ";
            //    SQLString = SQLString + "ON SOLDET.IDApparecchio=APP.IDApparecchio ";
            //    SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Utensili UTE ";
            //    SQLString = SQLString + "ON SOLDET.IDUtensile=UTE.IDUtensile ";
            //    SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Apparecchi APP2 ";
            //    SQLString = SQLString + "ON SOLDET.IDApparecchio2=APP2.IDApparecchio ";
            //    SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Utensili UTE2 ";
            //    SQLString = SQLString + "ON SOLDET.IDUtensile2=UTE2.IDUtensile ";
            //    SQLString = SQLString + "LEFT JOIN Materiale_Tipologia MT ";
            //    SQLString = SQLString + "ON MT.ID=SOLDET.Tipologia_MR ";
            //    SQLString = SQLString + "where SOLDET.idSoluzioneMaster in (" + idlist + ") ";
            //    SQLString = SQLString + "Order By SOLDET.[IDSoluzioneMaster],SOLDET.[IDSoluzioneDetail] ";

            //    DataTable dtsub = new DataTable();
            //    dtsub = DB.GetDataTable(SQLString, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

            //    SQLString = "";

            //    SQLString = SQLString + "SELECT SOL.CodiceSoluzione as CodiceSoluzioneMR, IDSoluzioneDetailConcentration, IDSoluzioneMaster, ";
            //    SQLString = SQLString + "CAS, ";
            //    SQLString = SQLString + "(SELECT CASE WHEN ";
            //    SQLString = SQLString + "	(SELECT count(*) ";
            //    SQLString = SQLString + "	FROM Soluzioni_Details SOLDET ";
            //    SQLString = SQLString + "	LEFT JOIN Soluzioni SOL ";
            //    SQLString = SQLString + "	ON SOLDET.IDSoluzioneMaster=SOL.IDSoluzione ";
            //    SQLString = SQLString + "	LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
            //    SQLString = SQLString + "	ON SOLDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
            //    SQLString = SQLString + "	LEFT JOIN [SERVER026].[LUPIN].dbo.Materiali MAT ";
            //    SQLString = SQLString + "	ON SCDOC.IDMateriale=MAT.IDMateriale ";
            //    SQLString = SQLString + "	where SOLDET.idSoluzioneMaster=SOLDETCONC.IDSoluzioneMaster ";
            //    SQLString = SQLString + "	AND SOLDET.CAS=SOLDETCONC.CAS)>0 ";
            //    SQLString = SQLString + "THEN ";
            //    SQLString = SQLString + "	(SELECT top 1 MAT.DenominazioneProdotto ";
            //    SQLString = SQLString + "	FROM Soluzioni_Details SOLDET ";
            //    SQLString = SQLString + "	LEFT JOIN Soluzioni SOL ";
            //    SQLString = SQLString + "	ON SOLDET.IDSoluzioneMaster=SOL.IDSoluzione ";
            //    SQLString = SQLString + "	LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
            //    SQLString = SQLString + "	ON SOLDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
            //    SQLString = SQLString + "	LEFT JOIN [SERVER026].[LUPIN].dbo.Materiali MAT ";
            //    SQLString = SQLString + "	ON SCDOC.IDMateriale=MAT.IDMateriale ";
            //    SQLString = SQLString + "	where SOLDET.idSoluzioneMaster=SOLDETCONC.IDSoluzioneMaster ";
            //    SQLString = SQLString + "	AND SOLDET.CAS=SOLDETCONC.CAS) ";
            //    SQLString = SQLString + "ELSE ";
            //    SQLString = SQLString + "	CASE WHEN ";
            //    SQLString = SQLString + "		(SELECT count(*) ";
            //    SQLString = SQLString + "		FROM Soluzioni_Details SOLDET ";
            //    SQLString = SQLString + "		INNER JOIN [dbo].[Materiale_Tipologia] MT ";
            //    SQLString = SQLString + "		ON SOLDET.Tipologia_MR=MT.ID ";
            //    SQLString = SQLString + "		AND MT.Nome='Working Solution' ";
            //    SQLString = SQLString + "		LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
            //    SQLString = SQLString + "		ON SOLDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
            //    SQLString = SQLString + "		LEFT JOIN [SERVER026].[LUPIN].dbo.WorkingSolutionsDetails WSD ";
            //    SQLString = SQLString + "		ON SCDOC.IDSchedaDocumenti=WSD.IDSchedaDocumenti ";
            //    SQLString = SQLString + "		where SOLDET.idSoluzioneMaster=SOLDETCONC.IDSoluzioneMaster ";
            //    SQLString = SQLString + "		AND WSD.CasComponente=SOLDETCONC.CAS)>0 ";
            //    SQLString = SQLString + "	THEN ";
            //    SQLString = SQLString + "		(SELECT top 1 WSD.NomeComponente ";
            //    SQLString = SQLString + "		FROM Soluzioni_Details SOLDET ";
            //    SQLString = SQLString + "		INNER JOIN [dbo].[Materiale_Tipologia] MT ";
            //    SQLString = SQLString + "		ON SOLDET.Tipologia_MR=MT.ID ";
            //    SQLString = SQLString + "		AND MT.Nome='Working Solution' ";
            //    SQLString = SQLString + "		LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
            //    SQLString = SQLString + "		ON SOLDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
            //    SQLString = SQLString + "		LEFT JOIN [SERVER026].[LUPIN].dbo.WorkingSolutionsDetails WSD ";
            //    SQLString = SQLString + "		ON SCDOC.IDSchedaDocumenti=WSD.IDSchedaDocumenti ";
            //    SQLString = SQLString + "		where SOLDET.idSoluzioneMaster=SOLDETCONC.IDSoluzioneMaster ";
            //    SQLString = SQLString + "		AND WSD.CasComponente=SOLDETCONC.CAS) ";
            //    SQLString = SQLString + "	ELSE ";
            //    SQLString = SQLString + "	    CASE WHEN ";
            //    SQLString = SQLString + "		    (select count(*) ";
            //    SQLString = SQLString + "		    from [dbo].[Soluzioni_Details] ";
            //    SQLString = SQLString + "		    INNER JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti ";
            //    SQLString = SQLString + "	    	ON Soluzioni_Details.IDSchedaDocumenti=SchedeDocumenti.IDSchedaDocumenti ";
            //    SQLString = SQLString + "		    INNER JOIN [SERVER026].[LUPIN].dbo.Materiali ";
            //    SQLString = SQLString + "		    ON SchedeDocumenti.IDMateriale=Materiali.IDMateriale ";
            //    SQLString = SQLString + "		    where [Soluzioni_Details].cas=SOLDETCONC.CAS)>0 ";
            //    SQLString = SQLString + "	    THEN ";
            //    SQLString = SQLString + "		    (select TOP 1 Materiali.DenominazioneProdotto ";
            //    SQLString = SQLString + "		    from [dbo].[Soluzioni_Details] ";
            //    SQLString = SQLString + "		    INNER JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti ";
            //    SQLString = SQLString + "	    	ON Soluzioni_Details.IDSchedaDocumenti=SchedeDocumenti.IDSchedaDocumenti ";
            //    SQLString = SQLString + "		    INNER JOIN [SERVER026].[LUPIN].dbo.Materiali ";
            //    SQLString = SQLString + "		    ON SchedeDocumenti.IDMateriale=Materiali.IDMateriale ";
            //    SQLString = SQLString + "		    where [Soluzioni_Details].cas=SOLDETCONC.CAS) ";
            //    SQLString = SQLString + "	    ELSE ";
            //    SQLString = SQLString + "		    (SELECT top 1 WSD.NomeComponente ";
            //    SQLString = SQLString + "		    FROM [SERVER026].[LUPIN].dbo.WorkingSolutionsDetails WSD ";
            //    SQLString = SQLString + "		    where WSD.CasComponente=SOLDETCONC.CAS) ";
            //    SQLString = SQLString + "	    END ";
            //    SQLString = SQLString + "	END ";
            //    SQLString = SQLString + "END) AS DenominazioneProdotto, ";
            //    SQLString = SQLString + "ConcentrazioneFinale, ";
            //    SQLString = SQLString + "convert(varchar(20),DataCalcolo,23) as DataCalcolo ";
            //    SQLString = SQLString + "FROM Soluzioni_Details_Concentration SOLDETCONC ";
            //    SQLString = SQLString + "INNER JOIN Soluzioni SOL ";
            //    SQLString = SQLString + "ON SOL.IDSoluzione=SOLDETCONC.idSoluzioneMaster ";
            //    SQLString = SQLString + "where SOLDETCONC.idSoluzioneMaster in (" + idlist + ") ";
            //    SQLString = SQLString + "Order By IDSoluzioneMaster,IDSoluzioneDetailConcentration ";

            //    DataTable dtconc = new DataTable();
            //    dtconc = DB.GetDataTable(SQLString, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

            //    string path = @"c:\temp\" + gviewSoluzioni.GetFocusedRowCellValue("CodiceSoluzione").ToString() + "_" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".xlsx";
            //    FileInfo fl = new FileInfo(path);

            //    using (ExcelPackage pck = new ExcelPackage(fl))
            //    {

            //        int colNumber = 0;

            //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Soluzione");
            //        ws.Cells["A1"].LoadFromDataTable(dt, true, TableStyles.None);

            //        foreach (DataColumn col in dt.Columns)
            //        {
            //            colNumber++;
            //            if (col.DataType == typeof(DateTime))
            //            {
            //                ws.Column(colNumber).Style.Numberformat.Format = "mm/dd/yyyy hh:mm:ss AM/PM";
            //            }
            //        }

            //        ws = pck.Workbook.Worksheets.Add("Soluzione Componenti");
            //        ws.Cells["A1"].LoadFromDataTable(dtsub, true, TableStyles.None);

            //        foreach (DataColumn col in dtsub.Columns)
            //        {
            //            colNumber++;
            //            if (col.DataType == typeof(DateTime))
            //            {
            //                ws.Column(colNumber).Style.Numberformat.Format = "mm/dd/yyyy hh:mm:ss AM/PM";
            //            }
            //        }

            //        ws = pck.Workbook.Worksheets.Add("Soluzioni Concentrazioni");
            //        ws.Cells["A1"].LoadFromDataTable(dtconc, true, TableStyles.None);

            //        foreach (DataColumn col in dtconc.Columns)
            //        {
            //            colNumber++;
            //            if (col.DataType == typeof(DateTime))
            //            {
            //                ws.Column(colNumber).Style.Numberformat.Format = "mm/dd/yyyy hh:mm:ss AM/PM";
            //            }
            //        }

            //        pck.Save();
            //    }

            //    XtraMessageBox.Show(@"Esportazione terminata in c:\temp.", "Esporta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    XtraMessageBox.Show("E' necessario selezionare una Soluzione MR.", "Esporta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Da adattare a rette
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void frmRetta_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.IO.MemoryStream str = new System.IO.MemoryStream();
            gridRette.FocusedView.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            frmLogin.SaveUserPreferences("frmRette", "gridRette", str, IDUtente);

            str = new System.IO.MemoryStream();
            gridRetteDetails.FocusedView.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            frmLogin.SaveUserPreferences("frmRette", "gridRetteDetails", str, IDUtente);

            str = null;
        }

        private void tbVisualizzaLog_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmLog LogView = new frmLog();
            LogView.IDUtente = IDUtente;

            int idRetta = 0;

            if (gviewRette.SelectedRowsCount == 1)
            {
                int[] selectedRows = gviewRette.GetSelectedRows();
                int selectedRow = 0;
                foreach (int rowHandle in selectedRows)
                {
                    selectedRow = rowHandle;
                }
                if (selectedRow > -1)
                {
                    idRetta = Int32.Parse(gviewRette.GetRowCellValue(selectedRow, "IDRetta").ToString());
                }
            }

            EOS.Core.Control.Control_Transcode ctlTranscode = new EOS.Core.Control.Control_Transcode();

            LogView.CodiceSoluzione = ctlTranscode.GetCodiceRettaByID(idRetta);

            ctlTranscode = null;

            LogView.Show();
        }

        private void tbStampaReport_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void butScollega_Click(object sender, EventArgs e)
        {

        }

        private void butScollegaEAnnulla_Click(object sender, EventArgs e)
        {

        }

        private void butModifica_Click(object sender, EventArgs e)
        {

        }

        private void butAggiungi_Click(object sender, EventArgs e)
        {
            

            FrmRettaPuntiSelezioneComponenti Seleziona = new FrmRettaPuntiSelezioneComponenti();
            Seleziona.IDCaller = IDRetta;
            Seleziona.IDRetta = IDRetta;
            Seleziona.IDUtente = IDUtente;

            Core.Control.Control_Rette ControlRette = new Core.Control.Control_Rette();
            ControlRette.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
            Seleziona.IDGruppoPunti = ControlRette.GetNextIDGruppoPuntiComponentiFromIDRetta(IDRetta);
            ControlRette = null;

            Seleziona.ConnectioString= System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
            Seleziona.ShowDialog();
        }

        
    }
}