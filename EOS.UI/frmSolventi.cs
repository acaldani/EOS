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
    public partial class frmSolventi : DevExpress.XtraEditors.XtraForm
    {

        int IDSolvente=0;
        int IDSolventeCalled = 0;
        public int IDUtente = 0;
        bool DataAdd = false;
        bool DataChanged = false;
        //string activeFilter = "([Stato] <> 'Annullata')";
        bool activeFilterTrafficLight = false;
        int restorefocus = 0;

        public frmSolventi()
        {
            InitializeComponent();
        }

        private void frmSolventi_Load(object sender, EventArgs e)
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

            this.solventi_TipologiaTableAdapter.Fill(this.solventi.Solventi_Tipologia);
            LoadData();

            //System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmSolventi", "gridSolventi", IDUtente);

            //if (str != null)
            //{
            //    gridSolventi.FocusedView.RestoreLayoutFromStream(str);
            //    str.Seek(0, System.IO.SeekOrigin.Begin);
            //}

            //str = frmLogin.LoadUserPreferences("frmSolventi", "gridSolventiDetails", IDUtente);

            //if (str != null)
            //{
            //    gridSolventiDetails.FocusedView.RestoreLayoutFromStream(str);
            //    str.Seek(0, System.IO.SeekOrigin.Begin);
            //}

            //str = frmLogin.LoadUserPreferences("frmSolventi", "gridDocumenti", IDUtente);

            //if (str != null)
            //{
            //    gridDocumenti.FocusedView.RestoreLayoutFromStream(str);
            //    str.Seek(0, System.IO.SeekOrigin.Begin);
            //}

            //str = null;

            tbAggiorna.Enabled = true;
            tbNuovo.Enabled = true;
            tbSalva.Enabled = false;
        }

        private void frmSolventi_Shown(object sender, EventArgs e)
        {
            abilitazionecontrolli();
        }

        private void gviewSolventi_FocusedRowChanged(object sender, EventArgs e)
        {
            int i = gviewSolventi.FocusedRowHandle;

            if (i > 0)
            {
                if (restorefocus == 0)
                {
                    if (DataAdd == false)
                    {
                        IDSolvente = Convert.ToInt32(gviewSolventi.GetFocusedRowCellValue("IDSolvente"));
                        LoadControl();
                        LoadSolventiCbo();
                    }
                }
            }
        }

        private Boolean Validazione()
        {
            int testInt;
            decimal testDecimal;
            bool validato = true;
            string errori = "";

            if (cboTipologia.EditValue == null)
            {
                validato = false;
                errori = errori + "Deve essere specificata una tipologia di Soluzione di Lavoro!\r\n";
            }
            else
            {
                if (txtNome.EditValue.ToString() == "")
                {
                    validato = false;
                    errori = errori + "Deve essere specificato un nome per la Soluzione di Lavoro!\r\n";
                }

                if (cboStato.EditValue == null)
                {
                    validato = false;
                    errori = errori + "La Soluzione di Lavoro deve avere uno stato specificato!\r\n";
                }

                if (cboUbicazione.EditValue == null)
                {
                    validato = false;
                    errori = errori + "Deve essere specificata una ubicazione dove viene conservata la Soluzione di Lavoro!\r\n";
                }

                if (cboTipologia.EditValue == null)
                {
                    validato = false;
                    errori = errori + "Deve essere specificata una tipologia di Soluzione di Lavoro!\r\n";
                }

                if ((txtGiorniScadenza.EditValue == null) && (cboTipologia.EditValue.ToString() == "Soluzione di Lavoro Modello"))
                {
                    validato = false;
                    errori = errori + "Devono essere specificati dei giorni di default di scadenza della Soluzione di Lavoro!\r\n";
                }

                if ((txtDataPreparazione.EditValue == null) && (cboTipologia.EditValue.ToString() != "Soluzione di Lavoro Modello"))
                {
                    validato = false;
                    errori = errori + "Deve essere specificata una data di preparazione della Soluzione di Lavoro!\r\n";
                }
            }

            if (!validato)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(errori, "Validazione", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return validato;
        }

        #region LoadData
        private void LoadSolventiGrid()
        {
            try
            {
                global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();
                string SQL = "";

                SQL = SQL + "SELECT SOLV.IDSolvente ";
                SQL = SQL + "      ,SOLV.CodiceSolvente AS CodiceMiscelaSolventi ";
                SQL = SQL + "      ,SOLV.Tipologia AS TipologiaSolvente ";
                SQL = SQL + "      ,SOLV.Nome ";
                SQL = SQL + "      ,COM.Nome AS Stato ";
                SQL = SQL + "      ,UBI.Ubicazione ";
                SQL = SQL + "      ,SOLV.NotePrescrittive ";
                SQL = SQL + "      ,SOLV.NoteDescrittive ";
                SQL = SQL + "      ,SOLV.DefaultGiorniScadenza ";
                SQL = SQL + "      ,SOLV.DataPreparazione ";
                SQL = SQL + "      ,SOLV.DataScadenza ";
                SQL = SQL + "      ,SOLV.DataCreazione ";
                SQL = SQL + "      ,UTE.NomeUtente AS UtenteCreazione ";
                SQL = SQL + "      ,cast(0 as bit) AS Seleziona ";
                SQL = SQL + "  FROM dbo.Solventi SOLV ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Ubicazioni UBI ";
                SQL = SQL + "  ON SOLV.IDUbicazione=UBI.IDUbicazione ";
                SQL = SQL + "  LEFT JOIN Login_Utenti UTE ";
                SQL = SQL + "  ON SOLV.IDUtente=UTE.IDUtente ";
                SQL = SQL + "  LEFT JOIN Composti_Stati COM ";
                SQL = SQL + "  ON SOLV.IDStato=COM.ID ";
                SQL = SQL + "  ORDER BY SOLV.CodiceSolvente ";

                DataTable dt = new DataTable();
                dt = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                gridSolventi.DataSource = null;
                gviewSolventi.Columns.Clear();
                gridSolventi.DataSource = dt;
                gviewSolventi.PopulateColumns();
                gridSolventi.ForceInitialize();

                //gviewSolventi.Columns[0].Visible = false;
                gviewSolventi.Columns["IDSolvente"].Visible = false;

                System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmSolventi", "gridSolventi", IDUtente);

                if (str != null)
                {
                    gridSolventi.FocusedView.RestoreLayoutFromStream(str);
                    str.Seek(0, System.IO.SeekOrigin.Begin);
                }
                else
                {
                    gviewSolventi.Columns["CodiceMiscelaSolventi"].Width = 100;
                    gviewSolventi.Columns["TipologiaSolvente"].Width = 140;
                    gviewSolventi.Columns["Nome"].Width = 250;
                    gviewSolventi.Columns["Stato"].Width = 80;
                    gviewSolventi.Columns["Ubicazione"].Width = 250;
                }

                str = null;

                //gviewSolventi.ActiveFilterString = activeFilter;

                cboUbicazione.Properties.PopupView.ActiveFilterString = "Annullata=false";
            }
            catch (Exception e)
            {

            }
        }

        private void LoadSolventiDetailsGrid()
        {
            try
            {
                int IDSolventeMaster = 0;

                if (gviewSolventi.SelectedRowsCount == 1)
                {
                    int[] selectedRows = gviewSolventi.GetSelectedRows();
                    int selectedRow = 0;
                    foreach (int rowHandle in selectedRows)
                    {
                        selectedRow = rowHandle;
                    }
                    if(selectedRow > -1)
                    {
                        IDSolventeMaster = Int32.Parse(gviewSolventi.GetRowCellValue(selectedRow, "IDSolvente").ToString());
                    }
                }

                global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();

                string SQLString = "";

                SQLString = SQLString + "SELECT ";
                SQLString = SQLString + "SOLVDET.[IDSolventeMaster], ";
                SQLString = SQLString + "SOLVDET.[IDSolventeDetail], ";
                SQLString = SQLString + "SOLVDET.[IDSchedaDocumenti], ";
                SQLString = SQLString + "MT.[Nome] as Tipologia_MR, ";
                SQLString = SQLString + "SOLVDET.[CAS], ";
                SQLString = SQLString + "SCDOC.Lotto, ";
                SQLString = SQLString + "MAT.DenominazioneProdotto as MaterialeMR, ";
                SQLString = SQLString + "SOLVDET.[IDSolvente], ";
                SQLString = SQLString + "SOLV.Tipologia as TipoSolvente, ";
                SQLString = SQLString + "SOLV.CodiceSolvente as CodiceSoluzioneDiLavoro, ";
                SQLString = SQLString + "SOLV.Nome as NomeSoluzioneDiLavoro, ";
                SQLString = SQLString + "SOLVDET.[UM_Prelievo], ";
                SQLString = SQLString + "SOLVDET.[Quantita_Prelievo], ";
                SQLString = SQLString + "APP.NumeroApparecchio, ";
                SQLString = SQLString + "UTE.Nome, ";
                SQLString = SQLString + "SOLVDET.[Note], ";
                SQLString = SQLString + "SOLVDET.[DataScadenza] ";
                SQLString = SQLString + "FROM Solventi_Details SOLVDET ";
                SQLString = SQLString + "LEFT JOIN Solventi SOLV ";
                SQLString = SQLString + "ON SOLVDET.IDSolvente=SOLV.IDSolvente ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
                SQLString = SQLString + "ON SOLVDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Materiali MAT ";
                SQLString = SQLString + "ON SCDOC.IDMateriale=MAT.IDMateriale ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Apparecchi APP ";
                SQLString = SQLString + "ON SOLVDET.IDApparecchio=APP.IDApparecchio ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Utensili UTE ";
                SQLString = SQLString + "ON SOLVDET.IDUtensile=UTE.IDUtensile ";
                SQLString = SQLString + "LEFT JOIN Materiale_Tipologia MT ";
                SQLString = SQLString + "ON MT.ID=SOLVDET.Tipologia_MR ";
                SQLString = SQLString + "where SOLVDET.IDSolventeMaster='{0}' ";
                SQLString = SQLString + "Order By SOLVDET.[IDSolventeDetail] ";

                SQLString = string.Format(SQLString, IDSolventeMaster);

                DataTable dt = new DataTable();
                dt = DB.GetDataTable(SQLString, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                gridSolventiDetails.DataSource = null;
                gviewSolventiDetails.Columns.Clear();
                gridSolventiDetails.DataSource = dt;
                gviewSolventiDetails.PopulateColumns();
                gridSolventiDetails.ForceInitialize();
                gviewSolventiDetails.Columns[0].Visible = false;
                gviewSolventiDetails.Columns[1].Visible = false;
                gviewSolventiDetails.Columns[2].Visible = false;
                gviewSolventiDetails.Columns[7].Visible = false;

                System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmSolventi", "gridSolventiDetails", IDUtente);

                if (str != null)
                {
                    gridSolventiDetails.FocusedView.RestoreLayoutFromStream(str);
                    str.Seek(0, System.IO.SeekOrigin.Begin);
                }

                str = null;
            }
            catch (Exception e)
            {

            }
        }

        private void LoadDocumentiGrid()
        {
            try
            {
                string CodiceSolvente = "";
                int IDSolventeMaster = 0;

                if (gviewSolventi.SelectedRowsCount == 1)
                {
                    //CodiceSolvente = gviewSolventi.GetFocusedRowCellValue("CodiceMiscelaSolventi").ToString();

                    int[] selectedRows = gviewSolventi.GetSelectedRows();
                    int selectedRow = 0;
                    foreach (int rowHandle in selectedRows)
                    {
                        selectedRow = rowHandle;
                    }
                    if (selectedRow > -1)
                    {
                        IDSolvente = Convert.ToInt32(gviewSolventi.GetRowCellValue(selectedRow, "IDSolvente"));
                    }
                }

                EOS.Core.Control.Control_Solventi ctlSolvente = new EOS.Core.Control.Control_Solventi();
                ctlSolvente.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                CodiceSolvente = ctlSolvente.GetCodiceSolventeFromIDSolvente(IDSolventeMaster);
                ctlSolvente = null;

                global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();

                string SQLString = "";

                SQLString = SQLString + "SELECT [IDDocumento] ";
                SQLString = SQLString + "      ,[CodiceComposto] ";
                SQLString = SQLString + "      ,[NomeDocumento] ";
                SQLString = SQLString + "      ,[DescrizioneDocumento] ";
                SQLString = SQLString + "      ,[PathDocumento] ";
                SQLString = SQLString + "      ,[DataDocumento] ";
                SQLString = SQLString + "  FROM [EOS].[dbo].[Documenti] ";
                SQLString = SQLString + "  WHERE [CodiceComposto]= '{0}'";

                SQLString = string.Format(SQLString, CodiceSolvente);

                DataTable dt = new DataTable();
                dt = DB.GetDataTable(SQLString, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                gridDocumenti.DataSource = null;
                gviewDocumenti.Columns.Clear();
                gridDocumenti.DataSource = dt;
                gviewDocumenti.PopulateColumns();
                gridDocumenti.ForceInitialize();
                gviewDocumenti.Columns[0].Visible = false;

                System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmSolventi", "gridDocumenti", IDUtente);

                if (str != null)
                {
                    gridDocumenti.FocusedView.RestoreLayoutFromStream(str);
                    str.Seek(0, System.IO.SeekOrigin.Begin);
                }

                str = null;
            }
            catch (Exception e)
            {

            }
        }

        private void LoadSolventiCbo()
        {
            try
            {
                // TODO: questa riga di codice carica i dati nella tabella 'lupin1.ApparecchiSelectCommand'. È possibile spostarla o rimuoverla se necessario.
                this.utensiliSelectCommandTableAdapter.Fill(this.lupin1.UtensiliSelectCommand);
                // TODO: questa riga di codice carica i dati nella tabella 'lupin1.ApparecchiSelectCommand'. È possibile spostarla o rimuoverla se necessario.
                this.apparecchiSelectCommandTableAdapter.Fill(this.lupin1.ApparecchiSelectCommand);
                // TODO: questa riga di codice carica i dati nella tabella 'lupin1.UbicazioniSelectCommand'. È possibile spostarla o rimuoverla se necessario.
                this.ubicazioniSelectCommandTableAdapter.Fill(this.lupin1.UbicazioniSelectCommand);
                // TODO: questa riga di codice carica i dati nella tabella 'lupin1.MaterialiLottiSelectCommand'. È possibile spostarla o rimuoverla se necessario.
                this.materialiLottiSelectCommandTableAdapter.Fill(this.lupin1.MaterialiLottiSelectCommand);
                // TODO: questa riga di codice carica i dati nella tabella 'soluzioni.SolventiUbicazioniStatiUtentiSelectCommand'. È possibile spostarla o rimuoverla se necessario.
                this.solventiUbicazioniStatiUtentiSelectCommandTableAdapter.Fill(this.soluzioni.SolventiUbicazioniStatiUtentiSelectCommand);
                // TODO: questa riga di codice carica i dati nella tabella 'soluzioni.Composti_Stati'. È possibile spostarla o rimuoverla se necessario.
                this.composti_StatiTableAdapter.Fill(this.soluzioni.Composti_Stati);
                // TODO: questa riga di codice carica i dati nella tabella 'soluzioni.Login_Utenti'. È possibile spostarla o rimuoverla se necessario.
                this.login_UtentiTableAdapter.Fill(this.soluzioni.Login_Utenti);
                // TODO: questa riga di codice carica i dati nella tabella 'solventi._Solventi'. È possibile spostarla o rimuoverla se necessario.
                this.solventiTableAdapter.Fill(this.solventi._Solventi);
                // TODO: questa riga di codice carica i dati nella tabella 'soluzioni.Soluzioni_Tipologia'. È possibile spostarla o rimuoverla se necessario.
                this.soluzioni_TipologiaTableAdapter.Fill(this.soluzioni.Soluzioni_Tipologia);
                // TODO: questa riga di codice carica i dati nella tabella 'soluzioni._Soluzioni'. È possibile spostarla o rimuoverla se necessario.
                //removecontrolhandler();
                //this.soluzioniTableAdapter.Fill(this.soluzioni._Soluzioni);
                //addcontrolhandler();
                //addgridhandler();

                EOS.Core.Control.Control_Utenti ctlUtente = new EOS.Core.Control.Control_Utenti();

                if (!ctlUtente.CloneAllowedCheck(IDUtente))
                {
                    string filter = " (NomeTipologia <> 'Soluzione di Lavoro Modello') ";

                    cboTipologia.Properties.View.ActiveFilter.Add(cboTipologia.Properties.View.Columns["ID"], new ColumnFilterInfo(filter, ""));
                }

                ctlUtente = null;

                if (gviewSolventi.GetRowCellValue(gviewSolventi.FocusedRowHandle, "Stato").ToString() != "Preparazione")
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
            if (gviewSolventi.SelectedRowsCount == 1)
            {
                removecontrolhandler();
                IDSolvente = Convert.ToInt32(gviewSolventi.GetFocusedRowCellValue("IDSolvente"));

                Core.Model.Model_Solventi ModelSolvente = new Core.Model.Model_Solventi();
                Core.Control.Control_Solventi ControlSolvente = new Core.Control.Control_Solventi();

                ControlSolvente.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                ModelSolvente = ControlSolvente.GetSolventeByID(IDSolvente).First().Value;

                txtCodiceMiscelaSolventi.EditValue = ModelSolvente.CodiceSolvente;
                cboTipologia.EditValue = ModelSolvente.Tipologia;
                txtNome.EditValue = ModelSolvente.Nome;
                txtNotePrescrittive.EditValue = ModelSolvente.NotePrescrittive;
                txtNoteDescrittive.EditValue = ModelSolvente.NoteDescrittive;
                cboStato.EditValue = ModelSolvente.IDStato;
                cboUbicazione.EditValue = ModelSolvente.IDUbicazione;
                cboUtenteCreazione.EditValue = ModelSolvente.IDUtente;
                txtGiorniScadenza.EditValue = ModelSolvente.DefaultGiorniScadenza;
                
                if(ModelSolvente.DataCreazione.ToString()=="01/01/0001 00:00:00")
                {
                    txtDataCreazione.EditValue = null;
                }
                else
                {
                    txtDataCreazione.EditValue = ModelSolvente.DataCreazione;
                }
                if (ModelSolvente.DataPreparazione.ToString() == "01/01/0001 00:00:00")
                {
                    txtDataPreparazione.EditValue = null;
                }
                else
                {
                    txtDataPreparazione.EditValue = ModelSolvente.DataPreparazione;
                }
                if (ModelSolvente.DataScadenza.ToString() == "01/01/0001 00:00:00")
                {
                    txtDataScadenza.EditValue = null;
                }
                else
                {
                    txtDataScadenza.EditValue = ModelSolvente.DataScadenza;
                }

                ControlSolvente = null;
                ModelSolvente = null;

                LoadSolventiDetailsGrid();
                LoadDocumentiGrid();

                addcontrolhandler();

                abilitazionecontrolli();
            }
            else
            {
                removecontrolhandler();
                IDSolvente = 0;

                txtCodiceMiscelaSolventi.EditValue = "";
                cboTipologia.EditValue = null;
                txtNome.EditValue = "";
                txtNotePrescrittive.EditValue = "";
                txtNoteDescrittive.EditValue = "";
                cboStato.EditValue = null;
                cboUbicazione.EditValue = null;
                txtDataCreazione.EditValue = "";
                cboUtenteCreazione.EditValue = null;
                txtGiorniScadenza.EditValue = null;
                txtDataPreparazione.EditValue = null;
                txtDataScadenza.EditValue = "";

                LoadSolventiDetailsGrid();
                LoadDocumentiGrid();

                addcontrolhandler();

                abilitazionecontrolli();
            }
        }

        private void abilitazionecontrolli()
        {
            //if ((IDSolvente != 0) && (cboTipologia.EditValue.ToString() == "Soluzione di Lavoro Modello"))
            //{
            //    tbDuplica.Enabled = true;
            //}
            //else
            //{
            //    tbDuplica.Enabled = false;
            //}

            if (!DBNull.Value.Equals(cboStato.EditValue))
            {
                if (Convert.ToInt32(cboStato.EditValue) != 5)
                {
                    cboTipologia.Enabled = false;
                    txtNome.Enabled = false;

                    //txtNotePrescrittive.Enabled = false;
                    //txtNoteDescrittive.Enabled = false;
                    txtNotePrescrittive.Enabled = true;
                    txtNoteDescrittive.Enabled = true;
                    txtNotePrescrittive.ReadOnly = true;
                    txtNoteDescrittive.ReadOnly = true;

                    cboStato.Enabled = true;
                    cboUbicazione.Enabled = false;
                    txtDataCreazione.Enabled = false;
                    cboUtenteCreazione.Enabled = false;
                    txtGiorniScadenza.Enabled = false;
                    txtDataPreparazione.Enabled = false;
                    txtDataScadenza.Enabled = false;

                    if ((IDSolvente != 0) && (cboTipologia.EditValue.ToString() == "Soluzione di Lavoro Modello") && (Convert.ToInt32(cboStato.EditValue) == 6)) 
                    {
                        tbDuplica.Enabled = true;
                    }
                    else
                    {
                        tbDuplica.Enabled = false;
                    }

                    butAggiungiComponente.Enabled = false;
                    butCancella.Enabled = false;
                    butModifica.Enabled = false;
                    butAggiungiDocumento.Enabled = false;
                    butCancellaDocumento.Enabled = false;
                    butModificaDocumento.Enabled = false;
                }
                else
                {
                    cboTipologia.Enabled = true;
                    txtNome.Enabled = true;

                    txtNotePrescrittive.Enabled = true;
                    txtNoteDescrittive.Enabled = true;
                    txtNotePrescrittive.ReadOnly = false;
                    txtNoteDescrittive.ReadOnly = false;

                    cboStato.Enabled = true;
                    cboUbicazione.Enabled = true;
                    txtDataCreazione.Enabled = false;
                    cboUtenteCreazione.Enabled = false;

                    txtDataScadenza.Enabled = false;

                    if (gviewSolventi.RowCount != 0)
                    {
                        if (gviewSolventiDetails.RowCount != 0)
                        {
                            butCancella.Enabled = true;
                            butModifica.Enabled = true;
                        }
                        else
                        {
                            butCancella.Enabled = false;
                            butModifica.Enabled = false;
                        }

                        if (gviewDocumenti.RowCount != 0)
                        {
                            butCancellaDocumento.Enabled = true;
                            butModificaDocumento.Enabled = true;
                        }
                        else
                        {
                            butCancellaDocumento.Enabled = false;
                            butModificaDocumento.Enabled = false;
                        }

                        if (DataAdd == false)
                        {
                            butAggiungiComponente.Enabled = true;
                            butAggiungiDocumento.Enabled = true;
                        }
                    }
                    else
                    {
                        butCancella.Enabled = false;
                        butModifica.Enabled = false;
                        butAggiungiComponente.Enabled = false;
                        butCancellaDocumento.Enabled = false;
                        butModificaDocumento.Enabled = false;
                        butAggiungiDocumento.Enabled = false;

                        if (DataAdd == false)
                        {
                            butAggiungiComponente.Enabled = true;
                            butAggiungiDocumento.Enabled = true;
                        }
                    }

                    if ((IDSolvente != 0) && (cboTipologia.EditValue.ToString() == "Soluzione di Lavoro Modello"))
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
                        txtDataPreparazione.Enabled = false;

                        //txtNotePrescrittive.Enabled = true;
                        //txtNoteDescrittive.Enabled = false;
                        txtNotePrescrittive.Enabled = true;
                        txtNoteDescrittive.Enabled = true;
                        txtNotePrescrittive.ReadOnly = false;
                        txtNoteDescrittive.ReadOnly = true;

                        //txtDataScadenza.Enabled = false;
                        //removecontrolhandler();
                        //txtDataPreparazione.EditValue = null;
                        //txtDataScadenza.EditValue = null;
                        //addcontrolhandler();

                        removecontrolhandler();
                        txtDataPreparazione.EditValue = null;
                        addcontrolhandler();
                    }
                    else
                    {
                        tbDuplica.Enabled = false;
                        txtDataPreparazione.Enabled = true;
                        //txtDataScadenza.Enabled = true;
                        txtGiorniScadenza.Enabled = true;

                        //txtNotePrescrittive.Enabled = false;
                        //txtNoteDescrittive.Enabled = true;
                        txtNotePrescrittive.Enabled = true;
                        txtNoteDescrittive.Enabled = true;
                        txtNotePrescrittive.ReadOnly = true;
                        txtNoteDescrittive.ReadOnly = false;

                        //removecontrolhandler();
                        //txtGiorniScadenza.EditValue = null;
                        //addcontrolhandler();

                        //removecontrolhandler();
                        //txtGiorniScadenza.EditValue = null;
                        //addcontrolhandler();
                    }
                }
            }
        }

        private void LoadData()
        {
            LoadSolventiGrid();
            if(IDSolventeCalled != 0)
            {
                int rowHandle = gviewSolventi.LocateByValue("IDSolvente", IDSolventeCalled);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    restorefocus = 1;
                    gviewSolventi.FocusedRowHandle = rowHandle;
                    restorefocus = 0;
                }
            }
            LoadControl();
            LoadSolventiCbo();
            LoadSolventiDetailsGrid();
            LoadDocumentiGrid();
        }

        private void LoadGrid()
        {
            LoadSolventiGrid();
            if (IDSolventeCalled != 0)
            {
                int rowHandle = gviewSolventi.LocateByValue("IDSolvente", IDSolventeCalled);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    restorefocus = 1;
                    gviewSolventi.FocusedRowHandle = rowHandle;
                    restorefocus = 0;
                    IDSolventeCalled = 0;
                }
            }
            LoadSolventiDetailsGrid();
            LoadDocumentiGrid();
        }
        #endregion

        #region ManageHandler

        //private void addgridhandler()
        //{
        //    this.gviewSolventi.DoubleClick += new System.EventHandler(this.gviewSolventi_DoubleClick);
        //    this.gviewSolventiDetails.DoubleClick += new System.EventHandler(this.gviewSolventiDetails_DoubleClick);
        //}

        private void addcontrolhandler()
        {
            this.cboTipologia.EditValueChanged += new System.EventHandler(this.CambioTipologia);
            this.cboStato.EditValueChanged += new System.EventHandler(this.CambioStato);

            this.cboStato.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboUtenteCreazione.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboUbicazione.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboTipologia.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtNome.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtNotePrescrittive.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtNoteDescrittive.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtGiorniScadenza.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtDataPreparazione.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtDataScadenza.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtDataCreazione.EditValueChanged += new System.EventHandler(this.DataChange);
        }

        //private void removegridhandler()
        //{
        //    this.gviewSolventi.DoubleClick -= new System.EventHandler(this.gviewSolventi_DoubleClick);
        //    this.gviewSolventiDetails.DoubleClick -= new System.EventHandler(this.gviewSolventiDetails_DoubleClick);
        //}


        private void removecontrolhandler()
        {
            this.cboTipologia.EditValueChanged -= new System.EventHandler(this.CambioTipologia);
            this.cboStato.EditValueChanged -= new System.EventHandler(this.CambioStato);

            this.cboStato.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboUtenteCreazione.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboUbicazione.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboTipologia.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtNome.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtNotePrescrittive.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtNoteDescrittive.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtGiorniScadenza.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtDataPreparazione.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtDataScadenza.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtDataCreazione.EditValueChanged -= new System.EventHandler(this.DataChange);
        }
        #endregion

        #region FunctionHandledToEvent

        private void DataChange(object sender, EventArgs e)
        {
            DataChanged = true;
            tbAggiorna.Enabled = true;
            tbNuovo.Enabled = true;
            tbSalva.Enabled = true;
        }

        private void CambioTipologia(object sender, EventArgs e)
        {
            //if (cboTipologia.EditValue.ToString() == "Soluzione di Lavoro Modello")
            //{
            //    txtGiorniScadenza.Enabled = true;
            //    txtDataPreparazione.Enabled = false;
            //    //txtDataScadenza.Enabled = false;
            //    //txtDataPreparazione.EditValue = "";
            //    //txtDataScadenza.EditValue = "";
            //}
            //else
            //{
            //    txtDataPreparazione.Enabled = true;
            //    //txtDataScadenza.Enabled = true;
            //    txtGiorniScadenza.Enabled = false;
            //    //txtGiorniScadenza.EditValue = null;
            //}
            abilitazionecontrolli();
        }
        private void CambioStato(object sender, EventArgs e)
        {
            abilitazionecontrolli();
        }
        #endregion

        #region ButtonGridSolventeDettaglio
        private void butAggiungiComponente_Click(object sender, EventArgs e)
        {
            frmSeleziona Seleziona = new frmSeleziona();
            Seleziona.TipoElenco = "Solventi";
            Seleziona.IDCaller = IDSolvente;
            Seleziona.IDUtente = IDUtente;
            Seleziona.ShowDialog();

            if(Seleziona.SceltaEffettuata)
            {
                if ((Seleziona.Conferma == "MaterialeMR") || (Seleziona.Conferma == "Solvente"))
                {
                    frmSolventiDetails SolventeDettaglio = new frmSolventiDetails();
                    SolventeDettaglio.IDUtente = IDUtente;

                    switch (Seleziona.Conferma)
                    {
                        case "MaterialeMR":
                            SolventeDettaglio.IDSolventeMaster = IDSolvente;
                            SolventeDettaglio.StatoSolvente = cboStato.EditValue.ToString();
                            SolventeDettaglio.AggiungiNuova = true;
                            SolventeDettaglio.DataChanged = true;
                            SolventeDettaglio.IDSchedaDocumenti = Seleziona.ID;
                            SolventeDettaglio.IDSolvente = Convert.ToInt32(null);
                            SolventeDettaglio.TipoMaterialeMR = Seleziona.TipoMaterialeMR;
                            SolventeDettaglio.CAS = Seleziona.CAS;
                            SolventeDettaglio.DataScadenza = Convert.ToDateTime(Seleziona.DataScadenza);
                            SolventeDettaglio.Note = Convert.ToString(Seleziona.NotaUtilizzoScaduto);

                            break;
                        
                        case "Solvente":
                            SolventeDettaglio.IDSolventeMaster = IDSolvente;
                            SolventeDettaglio.StatoSolvente = cboStato.EditValue.ToString();
                            SolventeDettaglio.AggiungiNuova = true;
                            SolventeDettaglio.DataChanged = true;
                            SolventeDettaglio.IDSchedaDocumenti = Convert.ToInt32(null);
                            SolventeDettaglio.IDSolvente = Seleziona.ID;
                            SolventeDettaglio.TipoMaterialeMR = Convert.ToInt32(null);
                            SolventeDettaglio.CAS = Convert.ToString(null);
                            SolventeDettaglio.DataScadenza = Convert.ToDateTime(Seleziona.DataScadenza);
                            SolventeDettaglio.Note = Convert.ToString(Seleziona.NotaUtilizzoScaduto);

                            break;
                        default:

                            break;
                    }

                    SolventeDettaglio.ShowDialog();

                    SolventeDettaglio = null;

                    IDSolventeCalled = IDSolvente;
                    LoadData();
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Selezione nuovo componente annullata", "Aggiungi Componente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void butModifica_Click(object sender, EventArgs e)
        {
            if (gviewSolventiDetails.SelectedRowsCount == 1)
            {
                frmSolventiDetails SolventeDettaglio = new frmSolventiDetails();
                SolventeDettaglio.IDUtente = IDUtente;

                SolventeDettaglio.IDSolventeDetail = Convert.ToInt32(gviewSolventiDetails.GetFocusedRowCellValue("IDSolventeDetail"));
                SolventeDettaglio.IDSolventeMaster = IDSolvente;
                SolventeDettaglio.StatoSolvente = cboStato.EditValue.ToString();
                SolventeDettaglio.AggiungiNuova = false;
                SolventeDettaglio.DataChanged = false;
                SolventeDettaglio.ShowDialog();

                SolventeDettaglio = null;

                IDSolventeCalled = IDSolvente;
                LoadData();
            }
            else
            {
                //XtraMessageBox.Show("E' possibile modificare solo un dettaglio della miscela di solventi alla volta.", "Modifica", MessageBoxButtons.OK);
                XtraMessageBox.Show("E' possibile modificare solo un dettaglio della Soluzione di Lavoro alla volta.", "Modifica", MessageBoxButtons.OK);
            }
        }

        private void butCancella_Click(object sender, EventArgs e)
        {
            int ret1=0;

            if (XtraMessageBox.Show("Sei sicuro di cancellare gli elementi selezionati?", "Cancella", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                EOS.Core.Control.Control_Solventi_Details ControlSolventiDetail = new EOS.Core.Control.Control_Solventi_Details();
                ControlSolventiDetail.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                ControlSolventiDetail.IDUtente = IDUtente;

                //for (int i = 0; i < gridView.SelectedRowsCount; i++)
                //{
                //    int rowHandle = gridView.GetSelectedRows()[i];
                //    GridView detail = (GridView)gridView.GetDetailView(rowHandle, 0);
                //}

                for (int i = 0; i < gviewSolventiDetails.SelectedRowsCount; i++)
                {
                    int rowHandle = gviewSolventiDetails.GetSelectedRows()[i];
                    ret1 = ControlSolventiDetail.DeleteSolventeDetail(Convert.ToInt32(gviewSolventiDetails.GetRowCellValue(rowHandle, "IDSolventeDetail")));
                }

                if (ret1 == 1)
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///Commentato, era in uso ma non era utile in quanto queste operazioni si svolgono già prima nell'deletesolventedetail
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //EOS.Core.Control.Control_Solventi ControlSolventi = new Core.Control.Control_Solventi();
                    //ControlSolventi.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                    //EOS.Core.Model.Model_Solventi ModelSolventi = new Core.Model.Model_Solventi();
                    //ModelSolventi = ControlSolventi.GetSolventeByID(IDSolvente).First().Value;
                    //ModelSolventi.DataScadenza = Convert.ToDateTime(null);
                    //ControlSolventi.IDUtente = IDUtente;
                    //ControlSolventi.UpdateSolvente(ModelSolventi,0);

                    //ModelSolventi = null;
                    //ControlSolventi = null;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///Commentato, era in uso ma non era utile in quanto queste operazioni si svolgono già prima nell'deletesolventedetail
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    XtraMessageBox.Show("Dati cancellati.", "Cancella", MessageBoxButtons.OK);
                }
                else
                {
                    XtraMessageBox.Show("La cancellazione dei dati è terminata con errore.", "Cancella", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                ControlSolventiDetail = null;

                IDSolventeCalled = IDSolvente;
                LoadData();
            }
            else
            {
                XtraMessageBox.Show("Cancellazione annullata.", "Cancella", MessageBoxButtons.OK);
            }
        }
        #endregion

        #region ButtonForm
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

            butAggiungiComponente.Enabled = false;
            butModifica.Enabled = false;
            butCancella.Enabled = false;
            butAggiungiDocumento.Enabled = false;
            butCancellaDocumento.Enabled = false;
            butModificaDocumento.Enabled = false;

            cboTipologia.Enabled = true;
            txtNome.Enabled = true;

            //txtNotePrescrittive.Enabled = true;
            //txtNoteDescrittive.Enabled = true;
            txtNotePrescrittive.Enabled = true;
            txtNoteDescrittive.Enabled = true;
            txtNotePrescrittive.ReadOnly = false;
            txtNoteDescrittive.ReadOnly = false;

            txtGiorniScadenza.Enabled = true;
            txtDataPreparazione.Enabled = true;
            txtDataScadenza.Enabled = false;
            cboStato.Enabled = true;
            cboUbicazione.Enabled = true;
            txtDataCreazione.Enabled = false;
            cboUtenteCreazione.Enabled = false;

            removecontrolhandler();

            txtCodiceMiscelaSolventi.EditValue = "";
            cboTipologia.EditValue = null;
            txtNome.EditValue = "";
            txtNotePrescrittive.EditValue = "";
            txtNoteDescrittive.EditValue = "";
            cboStato.EditValue = 5;
            cboUbicazione.EditValue = null;
            txtDataCreazione.EditValue = DateTime.Now;
            cboUtenteCreazione.EditValue = IDUtente;
            txtGiorniScadenza.EditValue = null;
            txtDataPreparazione.EditValue = null;
            txtDataScadenza.EditValue = "";
            cboUbicazione.EditValue = null;

            addcontrolhandler();

            gridSolventiDetails.DataSource = null;
            gviewSolventiDetails.Columns.Clear();
            DataChanged = false;
            DataAdd = true;
            XtraMessageBox.Show("Inserire i dati del nuovo elemento.", "Nuovo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tbSalva_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Validazione())
            {
                int ret=0;
                bool CambioStatoOK = false;

                removecontrolhandler();

                Core.Model.Model_Solventi ModelSolVenti = new Core.Model.Model_Solventi();
                Core.Control.Control_Solventi ControlSolvente = new Core.Control.Control_Solventi();
                
                if (DataAdd)
                {
                    ControlSolvente.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                    ModelSolVenti.Tipologia = Convert.ToString(cboTipologia.EditValue);
                    ModelSolVenti.Nome = Convert.ToString(txtNome.EditValue);
                    ModelSolVenti.NotePrescrittive = Convert.ToString(txtNotePrescrittive.EditValue);
                    ModelSolVenti.NoteDescrittive = Convert.ToString(txtNoteDescrittive.EditValue);
                    ModelSolVenti.IDStato = Convert.ToInt32(cboStato.EditValue);
                    ModelSolVenti.IDUbicazione = Convert.ToInt32(cboUbicazione.EditValue);
                    ModelSolVenti.DataCreazione = DateTime.Now;
                    ModelSolVenti.IDUtente = IDUtente;
                    //ModelSolVenti.DefaultGiorniScadenza = Convert.ToInt32(txtGiorniScadenza.EditValue);
                    if ((txtGiorniScadenza.EditValue != "") && (txtGiorniScadenza.EditValue != null))
                    {
                        ModelSolVenti.DefaultGiorniScadenza = Convert.ToInt32(txtGiorniScadenza.EditValue);
                    }
                    else
                    {
                        ModelSolVenti.DefaultGiorniScadenza = Convert.ToInt32(null);
                    }
                    if ((txtDataPreparazione.EditValue != "")&&(txtDataPreparazione.EditValue != null))
                    {
                        ModelSolVenti.DataPreparazione = Convert.ToDateTime(txtDataPreparazione.EditValue);
                    }
                    else
                    {
                        ModelSolVenti.DataPreparazione = Convert.ToDateTime(null);
                    }

                    ControlSolvente.IDUtente = IDUtente;

                    ret = ControlSolvente.AddSolvente(ModelSolVenti);

                    IDSolventeCalled = ret;

                    CambioStatoOK = true;
                }
                else
                {
                    ControlSolvente.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                    ModelSolVenti = ControlSolvente.GetSolventeByID(IDSolvente).First().Value;

                    if ((ModelSolVenti.IDStato!=5) && (Convert.ToInt32(cboStato.EditValue)==5) && (ModelSolVenti.IDStato!= Convert.ToInt32(cboStato.EditValue)))
                    {
                        CambioStatoOK = false;
                    }
                    else
                    {
                        ModelSolVenti.Tipologia = Convert.ToString(cboTipologia.EditValue);
                        ModelSolVenti.Nome = Convert.ToString(txtNome.EditValue);
                        ModelSolVenti.NotePrescrittive = Convert.ToString(txtNotePrescrittive.EditValue);
                        ModelSolVenti.NoteDescrittive = Convert.ToString(txtNoteDescrittive.EditValue);
                        ModelSolVenti.IDStato = Convert.ToInt32(cboStato.EditValue);
                        ModelSolVenti.IDUbicazione = Convert.ToInt32(cboUbicazione.EditValue);
                        ModelSolVenti.DataCreazione = Convert.ToDateTime(txtDataCreazione.EditValue);
                        ModelSolVenti.IDUtente = IDUtente;
                        //ModelSolVenti.DefaultGiorniScadenza = Convert.ToInt32(txtGiorniScadenza.EditValue);
                        if ((txtGiorniScadenza.EditValue != "") && (txtGiorniScadenza.EditValue != null))
                        {
                            ModelSolVenti.DefaultGiorniScadenza = Convert.ToInt32(txtGiorniScadenza.EditValue);
                        }
                        else
                        {
                            ModelSolVenti.DefaultGiorniScadenza = Convert.ToInt32(null);
                        }
                        //ModelSolVenti.DataPreparazione = Convert.ToDateTime(txtDataPreparazione.EditValue);
                        if ((txtDataPreparazione.EditValue != "") && (txtDataPreparazione.EditValue != null))
                        {
                            ModelSolVenti.DataPreparazione = Convert.ToDateTime(txtDataPreparazione.EditValue);
                        }
                        else
                        {
                            ModelSolVenti.DataPreparazione = Convert.ToDateTime(null);
                        }

                        ControlSolvente.IDUtente = IDUtente;

                        ret = ControlSolvente.UpdateSolvente(ModelSolVenti);

                        IDSolventeCalled = IDSolvente;

                        CambioStatoOK = true;
                    }
                }

                ControlSolvente = null;
                ModelSolVenti = null;

                addcontrolhandler();

                if(CambioStatoOK)
                {
                    if (ret != 0)
                    {
                        System.IO.MemoryStream str = new System.IO.MemoryStream();
                        gridSolventi.FocusedView.SaveLayoutToStream(str);
                        str.Seek(0, System.IO.SeekOrigin.Begin);
                        frmLogin.SaveUserPreferences("frmSolventi", "gridSolventi", str, IDUtente);

                        tbAggiorna.Enabled = true;
                        tbNuovo.Enabled = true;
                        tbSalva.Enabled = false;
                        DataAdd = false;
                        DataChanged = false;
                        activeFilterTrafficLight = true;
                        LoadGrid();
                        activeFilterTrafficLight = false;

                        str = frmLogin.LoadUserPreferences("frmSolventi", "gridSolventi", IDUtente);

                        if (str != null)
                        {
                            gridSolventi.FocusedView.RestoreLayoutFromStream(str);
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
            gridSolventi.FocusedView.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            frmLogin.SaveUserPreferences("frmSolventi", "gridSolventi", str, IDUtente);

            DataAdd = false;
            DataChanged = false;
            IDSolventeCalled = IDSolvente;
            activeFilterTrafficLight = true;
            LoadData();
            activeFilterTrafficLight = false;

            abilitazionecontrolli();

            str = frmLogin.LoadUserPreferences("frmSolventi", "gridSolventi", IDUtente);

            if (str != null)
            {
                gridSolventi.FocusedView.RestoreLayoutFromStream(str);
                str.Seek(0, System.IO.SeekOrigin.Begin);
            }

            str = null;

            tbAggiorna.Enabled = true;
            tbNuovo.Enabled = true;
            tbSalva.Enabled = false;
            XtraMessageBox.Show("la visualizzazione dei dati è stata aggiornata.", "Aggiorna visualizzazione dati", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void butChiudi_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region cboColumnsSizer
        private void GridSize(object sender, CancelEventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;
            edit.Properties.PopupFormMinSize = new Size(1000, 400);
        }
        #endregion

        private void gridSolventi_DoubleClick(object sender, EventArgs e)
        {
                object cellValue = gviewSolventi.GetRowCellValue(gviewSolventi.FocusedRowHandle, "IDSolvente");
                int ID = Convert.ToInt32(cellValue);

                frmSolventi Sol = new frmSolventi();
                Sol.IDUtente = IDUtente;

                Sol.IDSolventeCalled = ID;

                Sol.MdiParent = this.ParentForm;
                Sol.Show();
        }

        private void gridDocumenti_DoubleClick(object sender, EventArgs e)
        {
            object cellValue = gviewDocumenti.GetRowCellValue(gviewDocumenti.FocusedRowHandle, "PathDocumento");
            string PathDocumento = Convert.ToString(cellValue);

            System.Diagnostics.Process.Start(PathDocumento);
        }

        private void gridSolventiDetails_DoubleClick(object sender, EventArgs e)
        {
                object cellValue = gviewSolventiDetails.GetRowCellValue(gviewSolventiDetails.FocusedRowHandle, "IDSolvente");
                int ID = Convert.ToInt32(cellValue);

                if(ID!=0)
                {
                    frmSolventi Sol = new frmSolventi();
                    Sol.IDUtente = IDUtente;

                    Sol.IDSolventeCalled = ID;

                    Sol.MdiParent = this.ParentForm;
                    Sol.Show();
                }
                else
                {
                    cellValue = gviewSolventiDetails.GetRowCellValue(gviewSolventiDetails.FocusedRowHandle, "IDSchedaDocumenti");
                    ID = Convert.ToInt32(cellValue);

                    if (ID != 0)
                    {
                        frmMaterialiMR MatMR = new frmMaterialiMR();

                        MatMR.IDSchedaDocumentiCalled = ID;

                        MatMR.MdiParent = this.ParentForm;
                        MatMR.Show();
                    }
                }
        }

        private void butDuplica_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmDuplica Duplica = new frmDuplica();
            Duplica.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
            Duplica.TipoElemento = "Solvente";
            Duplica.IDElemento = IDSolvente;
            Duplica.IDUtente = IDUtente;
            Duplica.ShowDialog();

            if (Duplica.newIDElemento != 0)
            {
                IDSolventeCalled = Duplica.newIDElemento;
                LoadData();
            }
        }

        private void tbStampa_ItemClick(object sender, ItemClickEventArgs e)
        {
            int Copie = 0;
            string Stampante = "";
            string Report = "";

            //int intref = 0;
            //int NumeroCopie = 1;
            //EOS.Core.Common.PrintLabel StampaEtichetta = new EOS.Core.Common.PrintLabel();
            //List<String> Codici = new List<String>();

            //if (gviewSolventi.SelectedRowsCount > 0)
            //{
            //    XtraInputBoxArgs args = new XtraInputBoxArgs();
            //    args.Caption = "Numero etichette da stampare";
            //    args.Prompt = "Inserire la quantità di etichette desiderate";
            //    args.DefaultButtonIndex = 0;
            //    TextEdit editor = new TextEdit();
            //    args.Editor = editor;
            //    var result = XtraInputBox.Show(args);

            //    if ((result != null) && (int.TryParse(result.ToString(), out intref)))
            //    {
            //        NumeroCopie = Convert.ToInt32(result);

            //        int[] rowHandles = gviewSolventi.GetSelectedRows();

            //        for (int c = 0; c < gviewSolventi.SelectedRowsCount; c++)
            //        {
            //            Codici.Add(gviewSolventi.GetRowCellValue(rowHandles[c], "CodiceMiscelaSolventi").ToString());
            //        }

            //        StampaEtichetta.Codici = Codici;
            //        StampaEtichetta.NumeroCopie = NumeroCopie;
            //        StampaEtichetta.StampaEtichette();
            //    }
            //    else
            //    {
            //        XtraMessageBox.Show("Inserire la quantità di etichette che si desidera stampare.", "Stampa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //}
            //else
            //{
            //    XtraMessageBox.Show("Nessuna Soluzione di Lavoro selezionata.", "Stampa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            frmPrintSelect PrintSelect = new frmPrintSelect();
            PrintSelect.Form = "frmSolventi";
            PrintSelect.ShowDialog();

            Copie = PrintSelect.Copie;
            Stampante = PrintSelect.Stampante;
            Report = PrintSelect.Report;

            if (Stampante != "")
            {
                int[] rowHandles = gviewSolventi.GetSelectedRows();

                for (int c = 0; c < gviewSolventi.SelectedRowsCount; c++)
                {
                    XtraReport myReport = new XtraReport();
                    myReport = XtraReport.FromFile(Report);

                    myReport.Parameters["parCodiceSoluzione"].Value = gviewSolventi.GetRowCellValue(rowHandles[c], "CodiceMiscelaSolventi").ToString();
                    myReport.Parameters["parTipoSoluzione"].Value = "Lavoro";
                    myReport.Parameters["parConnectionString"].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                    myReport.CreateDocument();

                    for (int i = 0; i < Copie; i++)
                    {
                        myReport.Print(Stampante);
                    }
                }
            }
        }

        private void tbEsportaExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            string idlist = "";
            
            gviewSolventi.MoveLast();
            gviewSolventi.MoveFirst();

            for (int i = 0; i < gviewSolventi.DataRowCount; i++)
            {
                if (Convert.ToBoolean(gviewSolventi.GetRowCellValue(i, "Seleziona")))
                {
                    idlist = idlist + Convert.ToString(gviewSolventi.GetRowCellValue(i, "IDSolvente")) + ", ";
                }
            }

            if (idlist!="")
            {
                idlist = idlist.Substring(0, idlist.Length - 2);

                global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();
                string SQL = "";

                SQL = SQL + "SELECT SOLV.IDSolvente ";
                SQL = SQL + "      ,SOLV.CodiceSolvente AS CodiceMiscelaSolventi ";
                SQL = SQL + "      ,SOLV.Tipologia AS TipologiaSolvente ";
                SQL = SQL + "      ,SOLV.Nome ";
                SQL = SQL + "      ,SOLV.NotePrescrittive ";
                SQL = SQL + "      ,SOLV.NoteDescrittive ";
                SQL = SQL + "      ,UBI.Ubicazione ";
                SQL = SQL + "      ,SOLV.DefaultGiorniScadenza ";
                SQL = SQL + "      ,convert(varchar(20),SOLV.DataPreparazione,23) AS DataPreparazione ";
                SQL = SQL + "      ,convert(varchar(20),SOLV.DataScadenza,23) AS DataScadenza ";
                SQL = SQL + "      ,convert(varchar(20),SOLV.DataCreazione,23) AS DataCreazione ";
                SQL = SQL + "      ,UTE.NomeUtente AS UtenteCreazione ";
                SQL = SQL + "      ,COM.Nome AS Stato ";
                SQL = SQL + "  FROM dbo.Solventi SOLV ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Ubicazioni UBI ";
                SQL = SQL + "  ON SOLV.IDUbicazione=UBI.IDUbicazione ";
                SQL = SQL + "  LEFT JOIN Login_Utenti UTE ";
                SQL = SQL + "  ON SOLV.IDUtente=UTE.IDUtente ";
                SQL = SQL + "  LEFT JOIN Composti_Stati COM ";
                SQL = SQL + "  ON SOLV.IDStato=COM.ID ";
                SQL = SQL + "  WHERE SOLV.IDSolvente in (" + idlist + ") ";
                SQL = SQL + "  ORDER BY SOLV.CodiceSolvente ";

                DataTable dt = new DataTable();
                dt = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                string SQLString = "";

                SQLString = SQLString + "SELECT ";
                SQLString = SQLString + "SOLV.[CodiceSolvente] as CodiceSoluzioneDiLavoro, ";
                SQLString = SQLString + "SOLVDET.[IDSolventeMaster], ";
                SQLString = SQLString + "SOLVDET.[IDSolventeDetail], ";
                SQLString = SQLString + "SOLVDET.[IDSchedaDocumenti], ";
                SQLString = SQLString + "MT.[Nome] as Tipologia_MR, ";
                SQLString = SQLString + "SOLVDET.[CAS], ";
                SQLString = SQLString + "MAT.DenominazioneProdotto as MaterialeMR, ";
                SQLString = SQLString + "SOLVDET.[IDSolvente], ";
                SQLString = SQLString + "SOLV.Tipologia as TipoSolvente, ";
                SQLString = SQLString + "SOLV.Nome as NomeSolvente, ";
                SQLString = SQLString + "SOLVDET.[UM_Prelievo], ";
                SQLString = SQLString + "SOLVDET.[Quantita_Prelievo], ";
                SQLString = SQLString + "APP.NumeroApparecchio, ";
                SQLString = SQLString + "UTE.Nome, ";
                SQLString = SQLString + "SOLVDET.[Note], ";
                SQLString = SQLString + "convert(varchar(20),SOLVDET.[DataScadenza],23) AS DataScadenza ";
                SQLString = SQLString + "FROM Solventi_Details SOLVDET ";
                SQLString = SQLString + "LEFT JOIN Solventi SOLV ";
                SQLString = SQLString + "ON SOLVDET.IDSolventeMaster=SOLV.IDSolvente ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
                SQLString = SQLString + "ON SOLVDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Materiali MAT ";
                SQLString = SQLString + "ON SCDOC.IDMateriale=MAT.IDMateriale ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Apparecchi APP ";
                SQLString = SQLString + "ON SOLVDET.IDApparecchio=APP.IDApparecchio ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Utensili UTE ";
                SQLString = SQLString + "ON SOLVDET.IDUtensile=UTE.IDUtensile ";
                SQLString = SQLString + "LEFT JOIN Materiale_Tipologia MT ";
                SQLString = SQLString + "ON MT.ID=SOLVDET.Tipologia_MR ";
                SQLString = SQLString + "where SOLVDET.IDSolventeMaster in (" + idlist + ") ";
                SQLString = SQLString + "Order By SOLV.[CodiceSolvente], SOLVDET.[IDSolventeDetail] ";

                DataTable dtsub = new DataTable();
                dtsub = DB.GetDataTable(SQLString, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                string path = @"c:\temp\" + gviewSolventi.GetFocusedRowCellValue("CodiceMiscelaSolventi").ToString() + "_" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".xlsx";
                FileInfo fl = new FileInfo(path);

                using (ExcelPackage pck = new ExcelPackage(fl))
                {

                    int colNumber = 0;

                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Miscela di Solventi");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, TableStyles.None);

                    foreach (DataColumn col in dt.Columns)
                    {
                        colNumber++;
                        if (col.DataType == typeof(DateTime))
                        {
                            ws.Column(colNumber).Style.Numberformat.Format = "mm/dd/yyyy hh:mm:ss AM/PM";
                        }
                    }

                    ws = pck.Workbook.Worksheets.Add("Miscela di Solventi Componenti");
                    ws.Cells["A1"].LoadFromDataTable(dtsub, true, TableStyles.None);

                    foreach (DataColumn col in dtsub.Columns)
                    {
                        colNumber++;
                        if (col.DataType == typeof(DateTime))
                        {
                            ws.Column(colNumber).Style.Numberformat.Format = "mm/dd/yyyy hh:mm:ss AM/PM";
                        }
                    }

                    pck.Save();
                }

                XtraMessageBox.Show(@"Esportazione terminata in c:\temp.", "Esporta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("E' necessario selezionare una Soluzione di Lavoro.", "Esporta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void butAggiungiDocumento_Click(object sender, EventArgs e)
        {
            frmDocumenti frmDocumento = new frmDocumenti();
            frmDocumento.CodiceComposto = gviewSolventi.GetFocusedRowCellValue("CodiceMiscelaSolventi").ToString();
            frmDocumento.IDDocumento = 0;
            frmDocumento.AggiungiNuova = true;
            frmDocumento.ShowDialog();

            IDSolventeCalled = IDSolvente;
            LoadData();
        }

        private void butModificaDocumento_Click(object sender, EventArgs e)
        {
            if (gviewDocumenti.SelectedRowsCount == 1)
            {
                frmDocumenti frmDocumento = new frmDocumenti();
                frmDocumento.CodiceComposto = "";
                frmDocumento.IDDocumento = Convert.ToInt32(gviewDocumenti.GetFocusedRowCellValue("IDDocumento"));
                frmDocumento.AggiungiNuova = false;
                frmDocumento.ShowDialog();

                IDSolventeCalled = IDSolvente;
                LoadData();
            }
            else
            {
                XtraMessageBox.Show("E' possibile modificare solo un documento della Soluzione di Lavoro alla volta.", "Modifica", MessageBoxButtons.OK);
            }
        }

        private void butCancellaDocumento_Click(object sender, EventArgs e)
        {
            int ret1 = 0;

            if (XtraMessageBox.Show("Sei sicuro di cancellare gli elementi selezionati?", "Cancella", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                EOS.Core.Control.Control_Documenti ControlDocumento = new EOS.Core.Control.Control_Documenti();
                ControlDocumento.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                EOS.Core.Model.Model_Documenti ModelDocumento = new EOS.Core.Model.Model_Documenti();

                for (int i = 0; i < gviewDocumenti.SelectedRowsCount; i++)
                {
                    int rowHandle = gviewDocumenti.GetSelectedRows()[i];
                    ModelDocumento = ControlDocumento.GetDocumentoByID(Convert.ToInt32(gviewDocumenti.GetRowCellValue(rowHandle, "IDDocumento"))).First().Value;
                    ret1 = ControlDocumento.DeleteDocumento(ModelDocumento);
                }

                if (ret1 == 1)
                {
                    XtraMessageBox.Show("Dati cancellati.", "Cancella", MessageBoxButtons.OK);
                }
                else
                {
                    XtraMessageBox.Show("La cancellazione dei dati è terminata con errore.", "Cancella", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                ModelDocumento = null;
                ControlDocumento = null;

                IDSolventeCalled = IDSolvente;
                LoadData();
            }
            else
            {
                XtraMessageBox.Show("Cancellazione annullata.", "Cancella", MessageBoxButtons.OK);
            }
        }

        private void frmSolventi_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.IO.MemoryStream str = new System.IO.MemoryStream();
            gridSolventi.FocusedView.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            frmLogin.SaveUserPreferences("frmSolventi", "gridSolventi", str, IDUtente);

            str = new System.IO.MemoryStream();
            gridSolventiDetails.FocusedView.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            frmLogin.SaveUserPreferences("frmSolventi", "gridSolventiDetails", str, IDUtente);

            str = new System.IO.MemoryStream();
            gridDocumenti.FocusedView.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            frmLogin.SaveUserPreferences("frmSolventi", "gridDocumenti", str, IDUtente);

            str = null;
        }

        private void tbLog_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmLog LogView = new frmLog();
            LogView.IDUtente = IDUtente;

            int idSolvente = 0;

            if (gviewSolventi.SelectedRowsCount == 1)
            {
                int[] selectedRows = gviewSolventi.GetSelectedRows();
                int selectedRow = 0;
                foreach (int rowHandle in selectedRows)
                {
                    selectedRow = rowHandle;
                }
                if (selectedRow > -1)
                {
                    idSolvente = Int32.Parse(gviewSolventi.GetRowCellValue(selectedRow, "IDSolvente").ToString());
                }
            }

            EOS.Core.Control.Control_Transcode ctlTranscode = new EOS.Core.Control.Control_Transcode();

            LogView.CodiceSoluzione = ctlTranscode.GetCodiceSolventeByID(idSolvente);

            ctlTranscode = null;

            LogView.Show();
        }
    }
}