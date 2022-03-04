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
    public partial class frmSoluzioni : DevExpress.XtraEditors.XtraForm
    {

        int IDSoluzione=0;
        int IDSoluzioneCalled = 0;
        public int IDUtente = 0;
        bool DataAdd = false;
        bool DataChanged = false;
        bool changingMiscelaSolventi = false;
        bool changingSingoloSolvente = false;
        bool changingApparecchio = false;
        bool changingUtensile = false;
        bool changingApparecchio2 = false;
        bool changingUtensile2 = false;
        //string activeFilter = "([Stato] <> 'Annullata')";
        bool activeFilterTrafficLight = false;
        int restorefocus = 0;

        public frmSoluzioni()
        {
            InitializeComponent();
        }

        private void frmSoluzioni_Load(object sender, EventArgs e)
        {
            EOS.Core.Control.Control_Configurazione ControlConfigurazione = new EOS.Core.Control.Control_Configurazione();
            EOS.Core.Model.Model_Configurazione ModelConfigurazione = new EOS.Core.Model.Model_Configurazione();

            ControlConfigurazione.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

            ModelConfigurazione = ControlConfigurazione.GetActiveConfiguration().First().Value;

            if(ModelConfigurazione.NewFromTemplate==false)
            {
                tbDuplica.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            ControlConfigurazione = null;
            ModelConfigurazione = null;

            LoadData();

            //With gviewCampioni
            //    .OptionsLayout.StoreDataSettings = False
            //    .OptionsLayout.StoreAppearance = False
            //    .OptionsLayout.StoreAllOptions = False
            //    .OptionsLayout.StoreVisualOptions = True
            //    .SaveDefaultLayout()

            //    .RestoreLayoutFromString(Lupin.Login.User.LoadUserPreferences(Me.Name, "gviewCampioni"))
            //End With

            //System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmSoluzioni", "gridSoluzioni", IDUtente);

            //if (str != null)
            //{
            //    gridSoluzioni.FocusedView.RestoreLayoutFromStream(str);
            //    str.Seek(0, System.IO.SeekOrigin.Begin);
            //}

            //str = frmLogin.LoadUserPreferences("frmSoluzioni", "gridSoluzioniDetails", IDUtente);

            //if (str != null)
            //{
            //    gridSoluzioniDetails.FocusedView.RestoreLayoutFromStream(str);
            //    str.Seek(0, System.IO.SeekOrigin.Begin);
            //}

            //str = frmLogin.LoadUserPreferences("frmSoluzioni", "gridSoluzioniDetailsConcentration", IDUtente);

            //if (str != null)
            //{
            //    gridSoluzioniDetailsConcentration.FocusedView.RestoreLayoutFromStream(str);
            //    str.Seek(0, System.IO.SeekOrigin.Begin);
            //}

            //str = frmLogin.LoadUserPreferences("frmSoluzioni", "gridDocumenti", IDUtente);

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

        private void frmSoluzioni_Shown(object sender, EventArgs e)
        {
            abilitazionecontrolli();
        }
        //private void gridSoluzioni_DoubleClick(object sender, EventArgs e)
        //{
        //    frmSoluzioni Sol = new frmSoluzioni();
        //    Sol.IDUtente = IDUtente;
        //    Sol.IDSoluzioneCalled = Convert.ToInt32(gviewSoluzioni.GetFocusedRowCellValue("IDSoluzione"));
        //    Sol.MdiParent = this.ParentForm;
        //    Sol.Show();
        //}

        private void gviewSoluzioni_FocusedRowChanged(object sender, EventArgs e)
        {
            if(restorefocus==0)
            {
                if (DataAdd == false)
                {
                    IDSoluzione = Convert.ToInt32(gviewSoluzioni.GetFocusedRowCellValue("IDSoluzione"));

                    if (IDSoluzione != 0)
                    {
                        LoadControl();
                        LoadSoluzioniCbo();
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
                //errori = errori + "Deve essere specificata una tipologia di soluzione!\r\n";
                errori = errori + "Deve essere specificata una tipologia di Soluzione MR!\r\n";
            }
            else
            {
                if (txtNome.EditValue.ToString() == "")
                {
                    validato = false;
                    //errori = errori + "Deve essere specificato un nome per la soluzione!\r\n";
                    errori = errori + "Deve essere specificato un nome per la Soluzione MR!\r\n";
                }

                if ((cboSingoloSolvente.EditValue == null) && (cboMiscelaSolventi.EditValue == null))
                {
                    validato = false;
                    //errori = errori + "Deve essere specificata una miscela di solventi o un solvente scelto da anagrafica!\r\n";
                    errori = errori + "Deve essere specificata una Preparazione di Lavoro o un solvente scelto da anagrafica!\r\n";
                }

                if ((cboApparecchio.EditValue == null) && (cboUtensile.EditValue == null))
                {
                    validato = false;
                    errori = errori + "Deve essere specificato uno strumento o un utensile utilizzato per il prelievo del materiale!\r\n";
                }

                //||(! decimal.TryParse(txtVolumeFinale.EditValue.ToString(), out testDecimal))
                if ((Convert.ToString(txtVolumeFinale.EditValue) == "") || (Convert.ToDecimal(txtVolumeFinale.EditValue) == 0))
                {
                    validato = false;
                    errori = errori + "Deve essere specificato un valore numerico per il volume finale espresso in ml!\r\n";
                }

                if (cboStato.EditValue == null)
                {
                    validato = false;
                    //errori = errori + "La soluzione deve avere uno stato specificato!\r\n";
                    errori = errori + "La Soluzione MR deve avere uno stato specificato!\r\n";
                }

                if (cboUbicazione.EditValue == null)
                {
                    validato = false;
                    //errori = errori + "Deve essere specificata una ubicazione dove viene conservata la soluzione!\r\n";
                    errori = errori + "Deve essere specificata una ubicazione dove viene conservata la Soluzione MR!\r\n";
                }

                //if (txtDataCreazione.EditValue == "")
                //{
                //    validato = false;
                //    errori = errori + "Deve essere specificata una data di creazione della soluzione!\r\n";
                //}

                //if (cboUtenteCreazione.EditValue == null)
                //{
                //    validato = false;
                //    errori = errori + "Deve essere specificato un utente di creazione della soluzione!\r\n";
                //}

                if (cboTipologia.EditValue == null)
                {
                    validato = false;
                    //errori = errori + "Deve essere specificata una tipologia di soluzione!\r\n";
                    errori = errori + "Deve essere specificata una tipologia di Soluzione MR!\r\n";
                }

                if ((txtGiorniScadenza.EditValue == null) && (cboTipologia.EditValue.ToString() == "Soluzione MR Modello"))
                {
                    validato = false;
                    //errori = errori + "Devono essere specificati dei giorni di default di scadenza della soluzione!\r\n";
                    errori = errori + "Devono essere specificati dei giorni di default di scadenza della Soluzione MR!\r\n";
                }

                if ((txtDataPreparazione.EditValue == null) && (cboTipologia.EditValue.ToString() != "Soluzione MR Modello"))
                {
                    validato = false;
                    //errori = errori + "Deve essere specificata una data di preparazione della soluzione!\r\n";
                    errori = errori + "Deve essere specificata una data di preparazione della Soluzione MR!\r\n";
                }

                //if (txtDataScadenza.EditValue == "")
                //{
                //    validato = false;
                //    errori = errori + "Deve essere specificata una data di scadenza della soluzione!\r\n";
                //}
            }

            if (!validato)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(errori, "Validazione", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return validato;
        }

        #region LoadData
        private void LoadSoluzioniGrid()
        {
            try
            {
                global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();
                string SQL = "";

                SQL = SQL + "SELECT SOL.IDSoluzione ";
                SQL = SQL + "      ,SOL.CodiceSoluzione ";
                SQL = SQL + "      ,SOL.Tipologia AS TipologiaSoluzione ";
                SQL = SQL + "      ,SOL.Nome ";
                SQL = SQL + "      ,COM.Nome AS Stato ";
                SQL = SQL + "      ,UBI.Ubicazione ";
                SQL = SQL + "      ,SOL.NotePrescrittive ";
                SQL = SQL + "      ,SOL.NoteDescrittive ";
                SQL = SQL + "      ,MAT.DenominazioneProdotto AS Solvente ";
                SQL = SQL + "      ,SOV.Nome AS MiscelaSolventi ";
                SQL = SQL + "      ,APP.Descrizione AS ApparecchioPrelievo ";
                SQL = SQL + "      ,UTI.Nome AS UtensilePrelievo ";
                SQL = SQL + "      ,SOL.VolumeFinale ";
                SQL = SQL + "      ,SOL.UMVolumeFinale ";
                SQL = SQL + "      ,SOL.DefaultGiorniScadenza ";
                SQL = SQL + "      ,SOL.DataPreparazione ";
                SQL = SQL + "      ,SOL.DataScadenza ";
                SQL = SQL + "      ,SOL.DataCreazione ";
                SQL = SQL + "      ,UTE.NomeUtente AS UtenteCreazione ";
                SQL = SQL + "      ,cast(0 as bit) AS Seleziona ";
                SQL = SQL + "  FROM dbo.Soluzioni SOL ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Ubicazioni UBI ";
                SQL = SQL + "  ON SOL.IDUbicazione=UBI.IDUbicazione ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.SchedeDocumenti [CER] ";
                SQL = SQL + "  ON SOL.IDSchedaDocumenti=[CER].IDSchedaDocumenti ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Materiali MAT ";
                SQL = SQL + "  ON [CER].IDMateriale=MAT.IDMateriale ";
                SQL = SQL + "  LEFT JOIN Solventi SOV ";
                SQL = SQL + "  ON SOL.IDSolvente=SOV.IDSolvente ";
                SQL = SQL + "  LEFT JOIN Login_Utenti UTE ";
                SQL = SQL + "  ON SOL.IDUtente=UTE.IDUtente ";
                SQL = SQL + "  LEFT JOIN Composti_Stati COM ";
                SQL = SQL + "  ON SOL.IDStato=COM.ID ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Apparecchi APP ";
                SQL = SQL + "  ON SOL.IDApparecchio=APP.IDApparecchio ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Utensili UTI ";
                SQL = SQL + "  ON SOL.IDUtensile=UTI.IDUtensile ";
                SQL = SQL + "  ORDER BY SOL.CodiceSoluzione ";

                DataTable dt = new DataTable();
                dt = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                gridSoluzioni.DataSource = null;
                gviewSoluzioni.Columns.Clear();
                gridSoluzioni.DataSource = dt;
                gviewSoluzioni.PopulateColumns();
                gridSoluzioni.ForceInitialize();
                //gviewSoluzioni.Columns[0].Visible = false;
                gviewSoluzioni.Columns["IDSoluzione"].Visible = false;

                System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmSoluzioni", "gridSoluzioni", IDUtente);

                if (str != null)
                {
                    gridSoluzioni.FocusedView.RestoreLayoutFromStream(str);
                    str.Seek(0, System.IO.SeekOrigin.Begin);
                }
                else
                {
                    gviewSoluzioni.Columns["Solvente"].Visible = false;
                    gviewSoluzioni.Columns["MiscelaSolventi"].Visible = false;
                    gviewSoluzioni.Columns["ApparecchioPrelievo"].Visible = false;
                    gviewSoluzioni.Columns["UtensilePrelievo"].Visible = false;

                    gviewSoluzioni.Columns["CodiceSoluzione"].Width = 100;
                    gviewSoluzioni.Columns["TipologiaSoluzione"].Width = 140;
                    gviewSoluzioni.Columns["Nome"].Width = 250;
                    gviewSoluzioni.Columns["Stato"].Width = 80;
                    gviewSoluzioni.Columns["Ubicazione"].Width = 250;
                }

                str = null;

                //gviewSoluzioni.ActiveFilterString = activeFilter;

                cboUbicazione.Properties.PopupView.ActiveFilterString = "Annullata=false";
            }
            catch (Exception e)
            {

            }
        }

        private void LoadSoluzioniDetailsConcentrationGrid()
        {
            try
            {
                int idSoluzioneMaster = 0;

                if (gviewSoluzioni.SelectedRowsCount == 1)
                {
                    int[] selectedRows = gviewSoluzioni.GetSelectedRows();
                    int selectedRow = 0;
                    foreach (int rowHandle in selectedRows)
                    {
                        selectedRow = rowHandle;
                    }
                    if (selectedRow > -1)
                    {
                        idSoluzioneMaster = Int32.Parse(gviewSoluzioni.GetRowCellValue(selectedRow, "IDSoluzione").ToString());
                    }
                }

                global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();

                string SQLString = "";

                SQLString = SQLString + "SELECT IDSoluzioneDetailConcentration, IDSoluzioneMaster, ";
                SQLString = SQLString + "CAS, ";
                SQLString = SQLString + "( ";
                SQLString = SQLString + "   CASE WHEN ";
                SQLString = SQLString + "   	UPPER(SOLDETCONC.CAS)='N.A.' ";
                SQLString = SQLString + "   THEN ";
                SQLString = SQLString + "	    'CODICE CAS NON VALIDO, ASSEGNARE UN CAS VALIDO O UN CODICE INTERNO AI MATERIALI USATI  COME COMPONENTI E RICREARE LA SOLUZIONE' ";
                SQLString = SQLString + "   ELSE ";
                SQLString = SQLString + "   ( ";
                SQLString = SQLString + "       SELECT CASE WHEN ";
                SQLString = SQLString + "	        (SELECT count(*) ";
                SQLString = SQLString + "	        FROM Soluzioni_Details SOLDET ";
                SQLString = SQLString + "	        LEFT JOIN Soluzioni SOL ";
                SQLString = SQLString + "	        ON SOLDET.IDSoluzioneMaster=SOL.IDSoluzione ";
                SQLString = SQLString + "	        LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
                SQLString = SQLString + "	        ON SOLDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
                SQLString = SQLString + "	        LEFT JOIN [SERVER026].[LUPIN].dbo.Materiali MAT ";
                SQLString = SQLString + "	        ON SCDOC.IDMateriale=MAT.IDMateriale ";
                SQLString = SQLString + "	        where SOLDET.idSoluzioneMaster=SOLDETCONC.IDSoluzioneMaster ";
                SQLString = SQLString + "	        AND SOLDET.CAS=SOLDETCONC.CAS)>0 ";
                SQLString = SQLString + "       THEN ";
                SQLString = SQLString + "	        (SELECT top 1 MAT.DenominazioneProdotto ";
                SQLString = SQLString + "	        FROM Soluzioni_Details SOLDET ";
                SQLString = SQLString + "	        LEFT JOIN Soluzioni SOL ";
                SQLString = SQLString + "	        ON SOLDET.IDSoluzioneMaster=SOL.IDSoluzione ";
                SQLString = SQLString + "	        LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
                SQLString = SQLString + "	        ON SOLDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
                SQLString = SQLString + "	        LEFT JOIN [SERVER026].[LUPIN].dbo.Materiali MAT ";
                SQLString = SQLString + "	        ON SCDOC.IDMateriale=MAT.IDMateriale ";
                SQLString = SQLString + "	        where SOLDET.idSoluzioneMaster=SOLDETCONC.IDSoluzioneMaster ";
                SQLString = SQLString + "	        AND SOLDET.CAS=SOLDETCONC.CAS) ";
                SQLString = SQLString + "       ELSE ";
                SQLString = SQLString + "	        CASE WHEN ";
                SQLString = SQLString + "		        (SELECT count(*) ";
                SQLString = SQLString + "		        FROM Soluzioni_Details SOLDET ";
                SQLString = SQLString + "		        INNER JOIN [dbo].[Materiale_Tipologia] MT ";
                SQLString = SQLString + "		        ON SOLDET.Tipologia_MR=MT.ID ";
                SQLString = SQLString + "		        AND MT.Nome='Working Solution' ";
                SQLString = SQLString + "		        LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
                SQLString = SQLString + "		        ON SOLDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
                SQLString = SQLString + "		        LEFT JOIN [SERVER026].[LUPIN].dbo.WorkingSolutionsDetails WSD ";
                SQLString = SQLString + "		        ON SCDOC.IDSchedaDocumenti=WSD.IDSchedaDocumenti ";
                SQLString = SQLString + "		        where SOLDET.idSoluzioneMaster=SOLDETCONC.IDSoluzioneMaster ";
                SQLString = SQLString + "		        AND WSD.CasComponente=SOLDETCONC.CAS)>0 ";
                SQLString = SQLString + "	        THEN ";
                SQLString = SQLString + "		        (SELECT top 1 WSD.NomeComponente ";
                SQLString = SQLString + "		        FROM Soluzioni_Details SOLDET ";
                SQLString = SQLString + "		        INNER JOIN [dbo].[Materiale_Tipologia] MT ";
                SQLString = SQLString + "		        ON SOLDET.Tipologia_MR=MT.ID ";
                SQLString = SQLString + "		        AND MT.Nome='Working Solution' ";
                SQLString = SQLString + "		        LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
                SQLString = SQLString + "		        ON SOLDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
                SQLString = SQLString + "		        LEFT JOIN [SERVER026].[LUPIN].dbo.WorkingSolutionsDetails WSD ";
                SQLString = SQLString + "		        ON SCDOC.IDSchedaDocumenti=WSD.IDSchedaDocumenti ";
                SQLString = SQLString + "		        where SOLDET.idSoluzioneMaster=SOLDETCONC.IDSoluzioneMaster ";
                SQLString = SQLString + "		        AND WSD.CasComponente=SOLDETCONC.CAS) ";
                SQLString = SQLString + "	        ELSE ";
                SQLString = SQLString + "	            CASE WHEN ";
                SQLString = SQLString + "		            (select count(*) ";
                SQLString = SQLString + "		            from [dbo].[Soluzioni_Details] ";
                SQLString = SQLString + "		            INNER JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti ";
                SQLString = SQLString + "	    	        ON Soluzioni_Details.IDSchedaDocumenti=SchedeDocumenti.IDSchedaDocumenti ";
                SQLString = SQLString + "		            INNER JOIN [SERVER026].[LUPIN].dbo.Materiali ";
                SQLString = SQLString + "		            ON SchedeDocumenti.IDMateriale=Materiali.IDMateriale ";
                SQLString = SQLString + "		            where [Soluzioni_Details].cas=SOLDETCONC.CAS)>0 ";
                SQLString = SQLString + "	            THEN ";
                SQLString = SQLString + "		            (select TOP 1 Materiali.DenominazioneProdotto ";
                SQLString = SQLString + "		            from [dbo].[Soluzioni_Details] ";
                SQLString = SQLString + "		            INNER JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti ";
                SQLString = SQLString + "	    	        ON Soluzioni_Details.IDSchedaDocumenti=SchedeDocumenti.IDSchedaDocumenti ";
                SQLString = SQLString + "		            INNER JOIN [SERVER026].[LUPIN].dbo.Materiali ";
                SQLString = SQLString + "		            ON SchedeDocumenti.IDMateriale=Materiali.IDMateriale ";
                SQLString = SQLString + "		            where [Soluzioni_Details].cas=SOLDETCONC.CAS) ";
                SQLString = SQLString + "	            ELSE ";
                SQLString = SQLString + "		            (SELECT top 1 WSD.NomeComponente ";
                SQLString = SQLString + "		            FROM [SERVER026].[LUPIN].dbo.WorkingSolutionsDetails WSD ";
                SQLString = SQLString + "		            where WSD.CasComponente=SOLDETCONC.CAS) ";
                SQLString = SQLString + "	            END ";
                SQLString = SQLString + "	        END ";
                SQLString = SQLString + "	    END ";
                SQLString = SQLString + "	) ";
                SQLString = SQLString + "   END ";
                SQLString = SQLString + ") AS DenominazioneProdotto, ";
                SQLString = SQLString + "ConcentrazioneFinale, ";
                SQLString = SQLString + "DataCalcolo ";
                SQLString = SQLString + "FROM Soluzioni_Details_Concentration SOLDETCONC ";
                SQLString = SQLString + "where SOLDETCONC.idSoluzioneMaster='{0}' ";
                SQLString = SQLString + "Order By IDSoluzioneDetailConcentration ";

                SQLString = string.Format(SQLString, idSoluzioneMaster);

                DataTable dt = new DataTable();
                dt = DB.GetDataTable(SQLString, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);
                
                gridSoluzioniDetailsConcentration.DataSource = null;
                gviewSoluzioniDetailsConcentration.Columns.Clear();
                gridSoluzioniDetailsConcentration.DataSource = dt;
                gviewSoluzioniDetailsConcentration.PopulateColumns();
                gridSoluzioniDetailsConcentration.ForceInitialize();
                gviewSoluzioniDetailsConcentration.Columns[0].Visible = false;
                gviewSoluzioniDetailsConcentration.Columns[1].Visible = false;

                System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmSoluzioni", "gridSoluzioniDetailsConcentration", IDUtente);

                if (str != null)
                {
                    gridSoluzioniDetailsConcentration.FocusedView.RestoreLayoutFromStream(str);
                    str.Seek(0, System.IO.SeekOrigin.Begin);
                }

                str = null;
            }
            catch (Exception e)
            {

            }
        }

        private void LoadSoluzioniDetailsGrid()
        {
            try
            {
                int idSoluzioneMaster = 0;

                if (gviewSoluzioni.SelectedRowsCount == 1)
                {
                    int[] selectedRows = gviewSoluzioni.GetSelectedRows();
                    int selectedRow = 0;
                    foreach (int rowHandle in selectedRows)
                    {
                        selectedRow = rowHandle;
                    }
                    if (selectedRow > -1)
                    {
                        idSoluzioneMaster = Int32.Parse(gviewSoluzioni.GetRowCellValue(selectedRow, "IDSoluzione").ToString());
                    }
                }

                global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();

                string SQLString = "";

                SQLString = SQLString + "SELECT ";
                SQLString = SQLString + "SOLDET.[IDSoluzioneMaster], ";
                SQLString = SQLString + "SOLDET.[IDSoluzioneDetail], ";
                SQLString = SQLString + "SOLDET.[IDSchedaDocumenti], ";
                SQLString = SQLString + "MT.[Nome] as Tipologia_MR, ";
                SQLString = SQLString + "SOLDET.[CAS], ";
                SQLString = SQLString + "MAT.DenominazioneProdotto as MaterialeMR, ";
                SQLString = SQLString + "SCDOC.Lotto, ";
                SQLString = SQLString + "SOLDET.[IDSoluzione], ";
                SQLString = SQLString + "Sol.Tipologia as TipoSoluzione, ";
                SQLString = SQLString + "Sol.CodiceSoluzione as CodiceSoluzioneMR, ";
                SQLString = SQLString + "SOL.Nome as NomeSoluzioneMR, ";
                SQLString = SQLString + "SOLDET.[UM_Prelievo], ";
                SQLString = SQLString + "SOLDET.[Quantita_Prelievo], ";
                SQLString = SQLString + "APP.NumeroApparecchio, ";
                SQLString = SQLString + "UTE.Nome, ";
                SQLString = SQLString + "SOLDET.[Note], ";
                //SQLString = SQLString + "SCDOC.[Concentrazione], ";
                SQLString = SQLString + "SOLDET.[DataScadenza] ";
                SQLString = SQLString + "FROM Soluzioni_Details SOLDET ";
                SQLString = SQLString + "LEFT JOIN Soluzioni SOL ";
                SQLString = SQLString + "ON SOLDET.IDSoluzione=SOL.IDSoluzione ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
                SQLString = SQLString + "ON SOLDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Materiali MAT ";
                SQLString = SQLString + "ON SCDOC.IDMateriale=MAT.IDMateriale ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Apparecchi APP ";
                SQLString = SQLString + "ON SOLDET.IDApparecchio=APP.IDApparecchio ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Utensili UTE ";
                SQLString = SQLString + "ON SOLDET.IDUtensile=UTE.IDUtensile ";
                SQLString = SQLString + "LEFT JOIN Materiale_Tipologia MT ";
                SQLString = SQLString + "ON MT.ID=SOLDET.Tipologia_MR ";
                SQLString = SQLString + "where SOLDET.idSoluzioneMaster='{0}' ";
                SQLString = SQLString + "Order By SOLDET.[IDSoluzioneDetail] ";

                SQLString = string.Format(SQLString, idSoluzioneMaster);

                DataTable dt = new DataTable();
                dt = DB.GetDataTable(SQLString, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                gridSoluzioniDetails.DataSource = null;
                gviewSoluzioniDetails.Columns.Clear();
                gridSoluzioniDetails.DataSource = dt;

                gviewSoluzioniDetails.PopulateColumns();
                gridSoluzioniDetails.ForceInitialize();
                gviewSoluzioniDetails.Columns[0].Visible = false;
                gviewSoluzioniDetails.Columns[1].Visible = false;
                gviewSoluzioniDetails.Columns[2].Visible = false;
                gviewSoluzioniDetails.Columns[7].Visible = false;

                System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmSoluzioni", "gridSoluzioniDetails", IDUtente);

                if (str != null)
                {
                    gridSoluzioniDetails.FocusedView.RestoreLayoutFromStream(str);
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
                string CodiceSoluzione = "";
                int idSoluzioneMaster = 0;

                if (gviewSoluzioni.SelectedRowsCount == 1)
                {
                    int[] selectedRows = gviewSoluzioni.GetSelectedRows();
                    int selectedRow = 0;
                    foreach (int rowHandle in selectedRows)
                    {
                        selectedRow = rowHandle;
                    }
                    if (selectedRow > -1)
                    {
                        idSoluzioneMaster = Int32.Parse(gviewSoluzioni.GetRowCellValue(selectedRow, "IDSoluzione").ToString());
                    }
                }

                EOS.Core.Control.Controller_Soluzioni ctlSoluzione = new EOS.Core.Control.Controller_Soluzioni();
                ctlSoluzione.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                CodiceSoluzione = ctlSoluzione.GetCodiceSoluzioneFromIDSoluzione(idSoluzioneMaster);
                ctlSoluzione = null;

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

                SQLString = string.Format(SQLString, CodiceSoluzione);

                DataTable dt = new DataTable();
                dt = DB.GetDataTable(SQLString, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                gridDocumenti.DataSource = null;
                gviewDocumenti.Columns.Clear();
                gridDocumenti.DataSource = dt;
                gviewDocumenti.PopulateColumns();
                gridDocumenti.ForceInitialize();
                gviewDocumenti.Columns[0].Visible = false;

                System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmSoluzioni", "gridDocumenti", IDUtente);

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

        private void LoadSoluzioniCbo()
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

                if(!ctlUtente.CloneAllowedCheck(IDUtente))
                {
                    string filter = " (NomeTipologia <> 'Soluzione MR Modello') ";

                    cboTipologia.Properties.View.ActiveFilter.Add(cboTipologia.Properties.View.Columns["ID"], new ColumnFilterInfo(filter, ""));
                }

                ctlUtente = null;

                if(gviewSoluzioni.GetRowCellValue(gviewSoluzioni.FocusedRowHandle, "Stato").ToString()!="Preparazione")
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
            if (gviewSoluzioni.SelectedRowsCount == 1)
            {
                removecontrolhandler();
                IDSoluzione = Convert.ToInt32(gviewSoluzioni.GetFocusedRowCellValue("IDSoluzione"));

                Core.Model.Model_Soluzioni ModelSoluzione = new Core.Model.Model_Soluzioni();
                Core.Control.Controller_Soluzioni ControlSoluzione = new Core.Control.Controller_Soluzioni();
                
                ControlSoluzione.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                ModelSoluzione = ControlSoluzione.GetSolutionByID(IDSoluzione).First().Value;

                txtCodiceSoluzione.EditValue = ModelSoluzione.CodiceSoluzione;
                cboTipologia.EditValue = ModelSoluzione.Tipologia;
                txtNome.EditValue = ModelSoluzione.Nome;
                txtNotePrescrittive.EditValue = ModelSoluzione.NotePrescrittive;
                txtNoteDescrittive.EditValue = ModelSoluzione.NoteDescrittive;
                cboSingoloSolvente.EditValue = ModelSoluzione.IDSchedaDocumenti;
                cboMiscelaSolventi.EditValue = ModelSoluzione.IDSolvente;
                cboApparecchio.EditValue = ModelSoluzione.IDApparecchio;
                cboUtensile.EditValue = ModelSoluzione.IDUtensile;
                cboApparecchio2.EditValue = ModelSoluzione.IDApparecchio2;
                cboUtensile2.EditValue = ModelSoluzione.IDUtensile2;
                txtVolumeFinale.EditValue = ModelSoluzione.VolumeFinale;
                txtUMVolumeFinale.EditValue = ModelSoluzione.UMVolumeFinale;
                cboStato.EditValue = ModelSoluzione.IDStato;
                cboUbicazione.EditValue = ModelSoluzione.IDUbicazione;
                cboUtenteCreazione.EditValue = ModelSoluzione.IDUtente;
                txtGiorniScadenza.EditValue = ModelSoluzione.DefaultGiorniScadenza;
                
                if(ModelSoluzione.DataCreazione.ToString()=="01/01/0001 00:00:00")
                {
                    txtDataCreazione.EditValue = null;
                }
                else
                {
                    txtDataCreazione.EditValue = ModelSoluzione.DataCreazione;
                }
                if (ModelSoluzione.DataPreparazione.ToString() == "01/01/0001 00:00:00")
                {
                    txtDataPreparazione.EditValue = null;
                }
                else
                {
                    txtDataPreparazione.EditValue = ModelSoluzione.DataPreparazione;
                }
                if (ModelSoluzione.DataScadenza.ToString() == "01/01/0001 00:00:00")
                {
                    txtDataScadenza.EditValue = null;
                }
                else
                {
                    txtDataScadenza.EditValue = ModelSoluzione.DataScadenza;
                }

                ControlSoluzione = null;
                ModelSoluzione = null;

                //soluzioniBindingSource.Filter = "IDSoluzione=" + IDSoluzione;

                LoadSoluzioniDetailsGrid();
                LoadSoluzioniDetailsConcentrationGrid();
                LoadDocumentiGrid();

                addcontrolhandler();

                abilitazionecontrolli();
            }
            else
            {
                removecontrolhandler();
                IDSoluzione = 0;

                txtCodiceSoluzione.EditValue = "";
                cboTipologia.EditValue = null;
                txtNome.EditValue = "";
                txtNotePrescrittive.EditValue = "";
                txtNoteDescrittive.EditValue = "";
                cboSingoloSolvente.EditValue = null;
                cboMiscelaSolventi.EditValue = null;
                cboApparecchio.EditValue = null;
                cboUtensile.EditValue = null;
                cboApparecchio2.EditValue = null;
                cboUtensile2.EditValue = null;
                txtVolumeFinale.EditValue = "";
                txtUMVolumeFinale.EditValue = "";
                cboStato.EditValue = null;
                cboUbicazione.EditValue = null;
                txtDataCreazione.EditValue = "";
                cboUtenteCreazione.EditValue = null;
                txtGiorniScadenza.EditValue = null;
                txtDataPreparazione.EditValue = null;
                txtDataScadenza.EditValue = "";

                LoadSoluzioniDetailsGrid();
                LoadSoluzioniDetailsConcentrationGrid();
                LoadDocumentiGrid();

                addcontrolhandler();

                abilitazionecontrolli();
            }
        }

        private void abilitazionecontrolli()
        {
            //if ((IDSoluzione != 0) && (cboTipologia.EditValue.ToString() == "Soluzione MR Modello"))
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

                    cboSingoloSolvente.Enabled = false;
                    cboMiscelaSolventi.Enabled = false;
                    cboApparecchio.Enabled = false;
                    cboUtensile.Enabled = false;
                    cboApparecchio2.Enabled = false;
                    cboUtensile2.Enabled = false;
                    txtVolumeFinale.Enabled = false;
                    txtUMVolumeFinale.Enabled = false;
                    cboStato.Enabled = true;
                    cboUbicazione.Enabled = false;
                    txtDataCreazione.Enabled = false;
                    cboUtenteCreazione.Enabled = false;
                    txtGiorniScadenza.Enabled = false;
                    txtDataPreparazione.Enabled = false;
                    txtDataScadenza.Enabled = false;

                    if ((IDSoluzione != 0) && (cboTipologia.EditValue.ToString() == "Soluzione MR Modello") && (Convert.ToInt32(cboStato.EditValue) == 6)) 
                    {
                        tbDuplica.Enabled = true;
                    }
                    else
                    {
                        tbDuplica.Enabled = false;
                    }

                    butCancella.Enabled = false;
                    butModifica.Enabled = false;
                    butAggiungiComponente.Enabled = false;
                    butAggiungiDocumento.Enabled = false;
                    butCancellaDocumento.Enabled = false;
                    butModificaDocumento.Enabled = false;

                    //if (cboTipologia.EditValue == "Soluzione MR Modello")
                    //{
                    //    removecontrolhandler();
                    //    txtDataPreparazione.EditValue = null;
                    //    txtDataScadenza.EditValue = null;
                    //    addcontrolhandler();
                    //}
                    //else
                    //{
                    //    removecontrolhandler();
                    //    txtGiorniScadenza.EditValue = null;
                    //    addcontrolhandler();
                    //}
                }
                else
                {
                    cboTipologia.Enabled = true;
                    txtNome.Enabled = true;

                    txtNotePrescrittive.Enabled = true;
                    txtNoteDescrittive.Enabled = true;
                    txtNotePrescrittive.ReadOnly = false;
                    txtNoteDescrittive.ReadOnly = false;

                    cboSingoloSolvente.Enabled = true;
                    cboMiscelaSolventi.Enabled = true;
                    cboApparecchio.Enabled = true;
                    cboUtensile.Enabled = true;
                    cboApparecchio2.Enabled = true;
                    cboUtensile2.Enabled = true;
                    txtVolumeFinale.Enabled = true;
                    txtUMVolumeFinale.Enabled = false;
                    cboStato.Enabled = true;
                    cboUbicazione.Enabled = true;
                    txtDataCreazione.Enabled = false;
                    cboUtenteCreazione.Enabled = false;

                    txtDataScadenza.Enabled = false;

                    if (gviewSoluzioni.RowCount != 0)
                    {
                        if (gviewSoluzioniDetails.RowCount != 0)
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

                        if (DataAdd==false)
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

                    if ((IDSoluzione != 0) && (cboTipologia.EditValue.ToString() == "Soluzione MR Modello"))
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
            LoadSoluzioniGrid();
            if(IDSoluzioneCalled != 0)
            {
                int rowHandle = gviewSoluzioni.LocateByValue("IDSoluzione", IDSoluzioneCalled);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    restorefocus = 1;
                    gviewSoluzioni.FocusedRowHandle = rowHandle;
                    restorefocus = 0;
                }
            }
            LoadControl();
            LoadSoluzioniCbo();
            LoadSoluzioniDetailsGrid();
            LoadSoluzioniDetailsConcentrationGrid();
            LoadDocumentiGrid();
        }

        private void LoadGrid()
        {
            LoadSoluzioniGrid();
            if (IDSoluzioneCalled != 0)
            {
                int rowHandle = gviewSoluzioni.LocateByValue("IDSoluzione", IDSoluzioneCalled);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    restorefocus = 1;
                    gviewSoluzioni.FocusedRowHandle = rowHandle;
                    restorefocus = 0;
                    IDSoluzioneCalled = 0;
                }
            }
            LoadSoluzioniDetailsGrid();
            LoadSoluzioniDetailsConcentrationGrid();
            LoadDocumentiGrid();
        }
        #endregion

        #region ManageHandler

        //private void addgridhandler()
        //{
        //    this.gviewSoluzioni.DoubleClick += new System.EventHandler(this.gviewSoluzioni_DoubleClick);
        //    this.gviewSoluzioniDetails.DoubleClick += new System.EventHandler(this.gviewSoluzioniDetails_DoubleClick);
        //}

        private void addcontrolhandler()
        {
            this.cboSingoloSolvente.EditValueChanged += new System.EventHandler(this.SolventeSingolo);
            this.cboMiscelaSolventi.EditValueChanged += new System.EventHandler(this.SolventeMiscela);
            this.cboApparecchio.EditValueChanged += new System.EventHandler(this.Apparecchio);
            this.cboApparecchio2.EditValueChanged += new System.EventHandler(this.Apparecchio2);
            this.cboUtensile.EditValueChanged += new System.EventHandler(this.Utensile);
            this.cboUtensile2.EditValueChanged += new System.EventHandler(this.Utensile2);
            this.cboTipologia.EditValueChanged += new System.EventHandler(this.CambioTipologia);
            this.cboStato.EditValueChanged += new System.EventHandler(this.CambioStato);

            this.cboStato.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboUtenteCreazione.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboUbicazione.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboSingoloSolvente.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboMiscelaSolventi.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboApparecchio.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboUtensile.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboApparecchio2.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboUtensile2.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboTipologia.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtNome.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtNotePrescrittive.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtNoteDescrittive.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtVolumeFinale.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtUMVolumeFinale.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtGiorniScadenza.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtDataPreparazione.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtDataScadenza.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtDataCreazione.EditValueChanged += new System.EventHandler(this.DataChange);

            //this.cboApparecchio.Popup += new System.EventHandler(this.FiltraApparecchi);
        }

        //private void removegridhandler()
        //{
        //    this.gviewSoluzioni.DoubleClick -= new System.EventHandler(this.gviewSoluzioni_DoubleClick);
        //    this.gviewSoluzioniDetails.DoubleClick -= new System.EventHandler(this.gviewSoluzioniDetails_DoubleClick);
        //}


        private void removecontrolhandler()
        {
            this.cboSingoloSolvente.EditValueChanged -= new System.EventHandler(this.SolventeSingolo);
            this.cboMiscelaSolventi.EditValueChanged -= new System.EventHandler(this.SolventeMiscela);
            this.cboApparecchio.EditValueChanged -= new System.EventHandler(this.Apparecchio);
            this.cboUtensile.EditValueChanged -= new System.EventHandler(this.Utensile);
            this.cboApparecchio2.EditValueChanged -= new System.EventHandler(this.Apparecchio2);
            this.cboUtensile2.EditValueChanged -= new System.EventHandler(this.Utensile2);
            this.cboTipologia.EditValueChanged -= new System.EventHandler(this.CambioTipologia);
            this.cboStato.EditValueChanged -= new System.EventHandler(this.CambioStato);

            this.cboStato.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboUtenteCreazione.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboUbicazione.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboSingoloSolvente.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboMiscelaSolventi.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboApparecchio.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboUtensile.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboApparecchio2.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboUtensile2.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboTipologia.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtNome.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtNotePrescrittive.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtNoteDescrittive.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtVolumeFinale.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtUMVolumeFinale.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtGiorniScadenza.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtDataPreparazione.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtDataScadenza.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtDataCreazione.EditValueChanged -= new System.EventHandler(this.DataChange);
        }
        #endregion

        #region FunctionHandledToEvent

        //private void FiltraApparecchi(object sender, EventArgs e)
        //{
        //    string filter = " (Tipologia = 'Bilancia' OR Tipologia = 'Pipetta') ";

        //    cboApparecchio.Properties.View.ActiveFilter.Add(cboApparecchio.Properties.View.Columns["IDTipologiaApparecchio"], new ColumnFilterInfo(filter, ""));
        //}

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

        private void Apparecchio(object sender, EventArgs e)
        {
            if (!changingUtensile)
            {
                removecontrolhandler();
                changingApparecchio = true;
                cboUtensile.EditValue = null;
                addcontrolhandler();
                changingApparecchio = false;
            }
        }

        private void Apparecchio2(object sender, EventArgs e)
        {
            if (!changingUtensile2)
            {
                removecontrolhandler();
                changingApparecchio2 = true;
                cboUtensile2.EditValue = null;
                addcontrolhandler();
                changingApparecchio2 = false;
            }
        }

        private void Utensile(object sender, EventArgs e)
        {
            if (!changingApparecchio)
            {
                removecontrolhandler();
                changingUtensile = true;
                cboApparecchio.EditValue = null;
                addcontrolhandler();
                changingUtensile = false;
            }
        }

        private void Utensile2(object sender, EventArgs e)
        {
            if (!changingApparecchio2)
            {
                removecontrolhandler();
                changingUtensile2 = true;
                cboApparecchio2.EditValue = null;
                addcontrolhandler();
                changingUtensile2 = false;
            }
        }

        private void CambioTipologia(object sender, EventArgs e)
        {
            //if (cboTipologia.EditValue.ToString() == "Soluzione MR Modello")
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

        #region ButtonGridSoluzioneDettaglio
        private void butAggiungiComponente_Click(object sender, EventArgs e)
        {

            if (((cboSingoloSolvente.EditValue != null) || (cboMiscelaSolventi.EditValue != null)) && ((cboApparecchio.EditValue != null) || (cboUtensile.EditValue != null)) && (txtUMVolumeFinale.EditValue != null) && (txtVolumeFinale.EditValue != null))
            {
                frmSeleziona Seleziona = new frmSeleziona();
                Seleziona.TipoElenco = "Soluzioni";
                Seleziona.IDCaller = IDSoluzione;
                Seleziona.IDUtente = IDUtente;
                Seleziona.ShowDialog();

                if(Seleziona.SceltaEffettuata)
                {
                    if ((Seleziona.Conferma == "MaterialeMR") || (Seleziona.Conferma == "Soluzione"))
                    {
                        frmSoluzioniDetails SoluzioneDettaglio = new frmSoluzioniDetails();
                        SoluzioneDettaglio.IDUtente = IDUtente;

                        switch (Seleziona.Conferma)
                        {
                            case "MaterialeMR":
                                SoluzioneDettaglio.idSoluzioneMaster = IDSoluzione;
                                SoluzioneDettaglio.StatoSoluzione = cboStato.EditValue.ToString();
                                SoluzioneDettaglio.AggiungiNuova = true;
                                SoluzioneDettaglio.DataChanged = true;
                                SoluzioneDettaglio.idSchedaDocumenti = Seleziona.ID;
                                SoluzioneDettaglio.idSoluzione = Convert.ToInt32(null);
                                SoluzioneDettaglio.TipoMaterialeMR = Seleziona.TipoMaterialeMR;
                                SoluzioneDettaglio.CAS = Seleziona.CAS;
                                SoluzioneDettaglio.DataScadenza = Convert.ToDateTime(Seleziona.DataScadenza);
                                SoluzioneDettaglio.VolumeFinale = Convert.ToDecimal(txtVolumeFinale.EditValue);
                                SoluzioneDettaglio.UMVolumeFinale = Convert.ToString(txtUMVolumeFinale.EditValue);
                                SoluzioneDettaglio.Note = Convert.ToString(Seleziona.NotaUtilizzoScaduto);

                                break;

                            case "Soluzione":
                                SoluzioneDettaglio.idSoluzioneMaster = IDSoluzione;
                                SoluzioneDettaglio.StatoSoluzione = cboStato.EditValue.ToString();
                                SoluzioneDettaglio.AggiungiNuova = true;
                                SoluzioneDettaglio.DataChanged = true;
                                SoluzioneDettaglio.idSchedaDocumenti = Convert.ToInt32(null);
                                SoluzioneDettaglio.idSoluzione = Seleziona.ID;
                                SoluzioneDettaglio.TipoMaterialeMR = Convert.ToInt32(null);
                                SoluzioneDettaglio.CAS = Convert.ToString(null);
                                SoluzioneDettaglio.DataScadenza = Convert.ToDateTime(Seleziona.DataScadenza);
                                SoluzioneDettaglio.VolumeFinale = Convert.ToDecimal(txtVolumeFinale.EditValue);
                                SoluzioneDettaglio.UMVolumeFinale = Convert.ToString(txtUMVolumeFinale.EditValue);
                                SoluzioneDettaglio.Note = Convert.ToString(Seleziona.NotaUtilizzoScaduto);

                                break;
                            default:

                                break;
                        }

                        SoluzioneDettaglio.ShowDialog();

                        SoluzioneDettaglio = null;

                        IDSoluzioneCalled = IDSoluzione;
                        LoadData();
                        //LoadSoluzioniGrid();
                        //LoadSoluzioniDetailsGrid();
                        //LoadSoluzioniDetailsConcentrationGrid();
                        
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("Selezione nuovo componente annullata", "Aggiungi Componente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show("Selezionare un solvente singolo o una miscela di solventi, un apparecchio o un utensile di prelievo e inserire un Volume Finale e l'unità di misura Volume Finale (ml) prima di aggiungere un componente", "Aggiungi Componente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DevExpress.XtraEditors.XtraMessageBox.Show("Selezionare un solvente singolo o una Soluzione di Lavoro, un apparecchio o un utensile di prelievo e inserire un Volume Finale e l'unità di misura Volume Finale (ml) prima di aggiungere un componente", "Aggiungi Componente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void butModifica_Click(object sender, EventArgs e)
        {
            if (gviewSoluzioni.SelectedRowsCount == 1)
            {
                frmSoluzioniDetails SoluzioneDettaglio = new frmSoluzioniDetails();
                SoluzioneDettaglio.IDUtente = IDUtente;

                SoluzioneDettaglio.idSoluzioneDetail = Convert.ToInt32(gviewSoluzioniDetails.GetFocusedRowCellValue("IDSoluzioneDetail"));
                SoluzioneDettaglio.idSoluzioneMaster = IDSoluzione;
                SoluzioneDettaglio.StatoSoluzione = cboStato.EditValue.ToString();
                SoluzioneDettaglio.AggiungiNuova = false;
                SoluzioneDettaglio.DataChanged = false;
                SoluzioneDettaglio.VolumeFinale = Convert.ToDecimal(txtVolumeFinale.EditValue);
                SoluzioneDettaglio.UMVolumeFinale = Convert.ToString(txtUMVolumeFinale.EditValue);
                SoluzioneDettaglio.ShowDialog();

                SoluzioneDettaglio = null;

                IDSoluzioneCalled = IDSoluzione;
                LoadData();

                abilitazionecontrolli();
            }
            else
            {
                //XtraMessageBox.Show("E' possibile modificare solo un dettaglio della soluzione alla volta.", "Modifica", MessageBoxButtons.OK);
                XtraMessageBox.Show("E' possibile modificare solo un dettaglio della Soluzione MR alla volta.", "Modifica", MessageBoxButtons.OK);
            }
        }

        private void butCancella_Click(object sender, EventArgs e)
        {
            int ret1=0;
            int ret2=0;

            if (XtraMessageBox.Show("Sei sicuro di cancellare gli elementi selezionati?", "Cancella", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                EOS.Core.Control.Control_Soluzioni_Details ControlSolutionDetail = new EOS.Core.Control.Control_Soluzioni_Details();
                ControlSolutionDetail.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                ControlSolutionDetail.IDUtente = IDUtente;

                for (int i = 0; i < gviewSoluzioniDetails.SelectedRowsCount; i++)
                {
                    int rowHandle = gviewSoluzioniDetails.GetSelectedRows()[i];
                    ret1 = ControlSolutionDetail.DeleteSolutionDetail(Convert.ToInt32(gviewSoluzioniDetails.GetRowCellValue(rowHandle, "IDSoluzioneDetail")));
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///Commentato, era in uso ma non era utile in quanto queste operazioni si svolgono già prima nell'deletesolutiondetail
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //Core.Control.Control_Soluzioni_Details_Concentration ControlSoluzioniDetailsConcentration = new Core.Control.Control_Soluzioni_Details_Concentration();
                //ControlSoluzioniDetailsConcentration.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                //ControlSoluzioniDetailsConcentration.IDUtente = IDUtente;
                //ret2 = ControlSoluzioniDetailsConcentration.DeleteSolutionDetailsConcentrationByIDSoluzione(IDSoluzione);

                //if ((ret1 == 1) && (ret2 == 1))
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///Commentato, era in uso ma non era utile in quanto queste operazioni si svolgono già prima nell'deletesolutiondetail
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                if (ret1 == 1)
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///Commentato, era in uso ma non era utile in quanto queste operazioni si svolgono già prima nell'deletesolutiondetail
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //EOS.Core.Control.Controller_Soluzioni ControllerSoluzioni = new Core.Control.Controller_Soluzioni();
                    //ControllerSoluzioni.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                    //EOS.Core.Model.Model_Soluzioni ModelSoluzioni = new Core.Model.Model_Soluzioni();
                    //ModelSoluzioni = ControllerSoluzioni.GetSolutionByID(IDSoluzione).First().Value;
                    //ModelSoluzioni.DataScadenza = Convert.ToDateTime(null);
                    //ControllerSoluzioni.IDUtente = IDUtente;
                    //ControllerSoluzioni.UpdateSolution(ModelSoluzioni,0);

                    //ModelSoluzioni = null;
                    //ControllerSoluzioni = null;
                    //ControlSoluzioniDetailsConcentration = null;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///Commentato, era in uso ma non era utile in quanto queste operazioni si svolgono già prima nell'deletesolutiondetail
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    XtraMessageBox.Show("Dati cancellati.", "Cancella", MessageBoxButtons.OK);
                }
                else
                {
                    XtraMessageBox.Show("La cancellazione dei dati è terminata con errore.", "Cancella", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                ControlSolutionDetail = null;
                
                IDSoluzioneCalled = IDSoluzione;

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

                //soluzioniBindingSource.CancelEdit();
            }

            butAggiungiComponente.Enabled = false;
            butModifica.Enabled = false;
            butCancella.Enabled = false;
            butAggiungiDocumento.Enabled = false;
            butCancellaDocumento.Enabled = false;
            butModificaDocumento.Enabled = false;

            cboTipologia.Enabled = true;
            txtNome.Enabled = true;

            txtNotePrescrittive.Enabled = true;
            txtNoteDescrittive.Enabled = true;
            txtNotePrescrittive.ReadOnly = false;
            txtNoteDescrittive.ReadOnly = false;

            cboSingoloSolvente.Enabled = true;
            cboMiscelaSolventi.Enabled = true;
            cboApparecchio.Enabled = true;
            cboUtensile.Enabled = true;
            cboApparecchio2.Enabled = true;
            cboUtensile2.Enabled = true;
            txtVolumeFinale.Enabled = true;
            txtUMVolumeFinale.Enabled = false;
            txtGiorniScadenza.Enabled = true;
            txtDataPreparazione.Enabled = true;
            txtDataScadenza.Enabled = false;
            cboStato.Enabled = true;
            cboUbicazione.Enabled = true;
            txtDataCreazione.Enabled = false;
            cboUtenteCreazione.Enabled = false;

            //soluzioniBindingSource.AddNew();

            removecontrolhandler();

            txtCodiceSoluzione.EditValue = "";
            cboTipologia.EditValue = null;
            txtNome.EditValue = "";
            txtNotePrescrittive.EditValue = "";
            txtNoteDescrittive.EditValue = "";
            cboSingoloSolvente.EditValue = null;
            cboMiscelaSolventi.EditValue = null;
            cboApparecchio.EditValue = null;
            cboUtensile.EditValue = null;
            cboApparecchio2.EditValue = null;
            cboUtensile2.EditValue = null;
            txtVolumeFinale.EditValue = "";
            txtUMVolumeFinale.EditValue = "ml";
            cboStato.EditValue = 5;
            cboUbicazione.EditValue = null;
            txtDataCreazione.EditValue = DateTime.Now;
            cboUtenteCreazione.EditValue = IDUtente;
            txtGiorniScadenza.EditValue = null;
            txtDataPreparazione.EditValue = null;
            txtDataScadenza.EditValue = "";
            cboUbicazione.EditValue = null;

            addcontrolhandler();

            gridSoluzioniDetails.DataSource = null;
            gviewSoluzioniDetails.Columns.Clear();
            gridSoluzioniDetailsConcentration.DataSource = null;
            gviewSoluzioniDetailsConcentration.Columns.Clear();
            DataChanged = false;
            DataAdd = true;
            XtraMessageBox.Show("Inserire i dati del nuovo elemento.", "Nuovo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //predisponi nuovo record
            //soluzioniBindingSource.AddNew;
        }

        private void tbSalva_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Validazione())
            {
                int ret=0;
                bool CambioStatoOK = false;

                removecontrolhandler();
                //soluzioniBindingSource.EndEdit();
                //soluzioni.GetChanges();
                //soluzioni.AcceptChanges();
                //soluzioniTableAdapter.Update(soluzioni);

                Core.Model.Model_Soluzioni ModelSoluzione = new Core.Model.Model_Soluzioni();
                Core.Control.Controller_Soluzioni ControlSoluzione = new Core.Control.Controller_Soluzioni();
                
                if (DataAdd)
                {
                    ControlSoluzione.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                    ModelSoluzione.Tipologia = Convert.ToString(cboTipologia.EditValue);
                    ModelSoluzione.Nome = Convert.ToString(txtNome.EditValue);
                    ModelSoluzione.NotePrescrittive = Convert.ToString(txtNotePrescrittive.EditValue);
                    ModelSoluzione.NoteDescrittive = Convert.ToString(txtNoteDescrittive.EditValue);
                    ModelSoluzione.IDSchedaDocumenti = Convert.ToInt32(cboSingoloSolvente.EditValue);
                    ModelSoluzione.IDSolvente = Convert.ToInt32(cboMiscelaSolventi.EditValue);
                    ModelSoluzione.IDApparecchio = Convert.ToInt32(cboApparecchio.EditValue);
                    ModelSoluzione.IDUtensile = Convert.ToInt32(cboUtensile.EditValue);
                    ModelSoluzione.IDApparecchio2 = Convert.ToInt32(cboApparecchio2.EditValue);
                    ModelSoluzione.IDUtensile2 = Convert.ToInt32(cboUtensile2.EditValue);
                    ModelSoluzione.VolumeFinale = Convert.ToDecimal(txtVolumeFinale.EditValue.ToString());
                    ModelSoluzione.UMVolumeFinale = Convert.ToString(txtUMVolumeFinale.EditValue);
                    ModelSoluzione.IDStato = Convert.ToInt32(cboStato.EditValue);
                    ModelSoluzione.IDUbicazione = Convert.ToInt32(cboUbicazione.EditValue);
                    ModelSoluzione.DataCreazione = DateTime.Now;
                    ModelSoluzione.IDUtente = IDUtente;
                    //ModelSoluzione.DefaultGiorniScadenza = Convert.ToInt32(txtGiorniScadenza.EditValue);
                    if ((txtGiorniScadenza.EditValue != "") && (txtGiorniScadenza.EditValue != null))
                    {
                        ModelSoluzione.DefaultGiorniScadenza = Convert.ToInt32(txtGiorniScadenza.EditValue);
                    }
                    else
                    {
                        ModelSoluzione.DefaultGiorniScadenza = Convert.ToInt32(null);
                    }
                    if ((txtDataPreparazione.EditValue != "")&&(txtDataPreparazione.EditValue != null))
                    {
                        ModelSoluzione.DataPreparazione = Convert.ToDateTime(txtDataPreparazione.EditValue);
                    }
                    else
                    {
                        ModelSoluzione.DataPreparazione = Convert.ToDateTime(null);
                    }

                    //ModelSoluzione.DataScadenza = Convert.ToDateTime(txtDataScadenza.EditValue);

                    ControlSoluzione.IDUtente = IDUtente;

                    ret = ControlSoluzione.AddSolution(ModelSoluzione);

                    IDSoluzioneCalled = ret;

                    CambioStatoOK = true;
                }
                else
                {
                    ControlSoluzione.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                    ModelSoluzione = ControlSoluzione.GetSolutionByID(IDSoluzione).First().Value;

                    if ((ModelSoluzione.IDStato!=5) && (Convert.ToInt32(cboStato.EditValue)==5) && (ModelSoluzione.IDStato!= Convert.ToInt32(cboStato.EditValue)))
                    {
                        CambioStatoOK = false;
                    }
                    else
                    {
                        ModelSoluzione.Tipologia = Convert.ToString(cboTipologia.EditValue);
                        ModelSoluzione.Nome = Convert.ToString(txtNome.EditValue);
                        ModelSoluzione.NotePrescrittive = Convert.ToString(txtNotePrescrittive.EditValue);
                        ModelSoluzione.NoteDescrittive = Convert.ToString(txtNoteDescrittive.EditValue);
                        ModelSoluzione.IDSchedaDocumenti = Convert.ToInt32(cboSingoloSolvente.EditValue);
                        ModelSoluzione.IDSolvente = Convert.ToInt32(cboMiscelaSolventi.EditValue);
                        ModelSoluzione.IDApparecchio = Convert.ToInt32(cboApparecchio.EditValue);
                        ModelSoluzione.IDUtensile = Convert.ToInt32(cboUtensile.EditValue);
                        ModelSoluzione.IDApparecchio2 = Convert.ToInt32(cboApparecchio2.EditValue);
                        ModelSoluzione.IDUtensile2 = Convert.ToInt32(cboUtensile2.EditValue);
                        //ModelSoluzione.VolumeFinale = Convert.ToDecimal(txtVolumeFinale.EditValue.ToString().Replace(".", ","));
                        ModelSoluzione.VolumeFinale = Convert.ToDecimal(txtVolumeFinale.EditValue.ToString());
                        ModelSoluzione.UMVolumeFinale = Convert.ToString(txtUMVolumeFinale.EditValue);
                        ModelSoluzione.IDStato = Convert.ToInt32(cboStato.EditValue);
                        ModelSoluzione.IDUbicazione = Convert.ToInt32(cboUbicazione.EditValue);
                        ModelSoluzione.DataCreazione = Convert.ToDateTime(txtDataCreazione.EditValue);
                        ModelSoluzione.IDUtente = IDUtente;
                        //ModelSoluzione.DefaultGiorniScadenza = Convert.ToInt32(txtGiorniScadenza.EditValue);
                        if ((txtGiorniScadenza.EditValue != "") && (txtGiorniScadenza.EditValue != null))
                        {
                            ModelSoluzione.DefaultGiorniScadenza = Convert.ToInt32(txtGiorniScadenza.EditValue);
                        }
                        else
                        {
                            ModelSoluzione.DefaultGiorniScadenza = Convert.ToInt32(null);
                        }
                        //ModelSoluzione.DataPreparazione = Convert.ToDateTime(txtDataPreparazione.EditValue);
                        if ((txtDataPreparazione.EditValue != "") && (txtDataPreparazione.EditValue != null))
                        {
                            ModelSoluzione.DataPreparazione = Convert.ToDateTime(txtDataPreparazione.EditValue);
                        }
                        else
                        {
                            ModelSoluzione.DataPreparazione = Convert.ToDateTime(null);
                        }
                        //ModelSoluzione.DataScadenza = Convert.ToDateTime(txtDataScadenza.EditValue);

                        ControlSoluzione.IDUtente = IDUtente;

                        ret = ControlSoluzione.UpdateSolution(ModelSoluzione);

                        IDSoluzioneCalled = IDSoluzione;

                        CambioStatoOK = true;
                    }
                }

                ControlSoluzione = null;
                ModelSoluzione = null;

                addcontrolhandler();

                if(CambioStatoOK)
                {
                    if (ret != 0)
                    {
                        System.IO.MemoryStream str = new System.IO.MemoryStream();
                        gridSoluzioni.FocusedView.SaveLayoutToStream(str);
                        str.Seek(0, System.IO.SeekOrigin.Begin);
                        frmLogin.SaveUserPreferences("frmSoluzioni", "gridSoluzioni", str, IDUtente);

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

                        str = frmLogin.LoadUserPreferences("frmSoluzioni", "gridSoluzioni", IDUtente);

                        if (str != null)
                        {
                            gridSoluzioni.FocusedView.RestoreLayoutFromStream(str);
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

                //soluzioniBindingSource.CancelEdit();
            }

            System.IO.MemoryStream str = new System.IO.MemoryStream();
            gridSoluzioni.FocusedView.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            frmLogin.SaveUserPreferences("frmSoluzioni", "gridSoluzioni", str, IDUtente);

            DataAdd = false;
            DataChanged = false;
            IDSoluzioneCalled = IDSoluzione;
            activeFilterTrafficLight = true;
            LoadData();
            activeFilterTrafficLight = false;

            str = frmLogin.LoadUserPreferences("frmSoluzioni", "gridSoluzioni", IDUtente);

            if (str != null)
            {
                gridSoluzioni.FocusedView.RestoreLayoutFromStream(str);
                str.Seek(0, System.IO.SeekOrigin.Begin);
            }

            str = null;

            abilitazionecontrolli();

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

            edit.Properties.View.ActiveFilterString=filter;
        }
        #endregion

        private void gridSoluzioni_DoubleClick(object sender, EventArgs e)
        {
            //removegridhandler();

            //DXMouseEventArgs ea = e as DXMouseEventArgs;
            //DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = view.CalcHitInfo(ea.Location);
            //if (info.InRow && info.InRowCell)
            //{
            //    object val = view.GetRowCellValue(info.RowHandle, info.Column);

            //    DevExpress.XtraGrid.Columns.GridColumn col = gviewSoluzioni.Columns.ColumnByFieldName("IDSoluzione");
                object cellValue = gviewSoluzioni.GetRowCellValue(gviewSoluzioni.FocusedRowHandle, "IDSoluzione");
                int ID = Convert.ToInt32(cellValue);

                frmSoluzioni Sol = new frmSoluzioni();
                Sol.IDUtente = IDUtente;

                Sol.IDSoluzioneCalled = ID;

                Sol.MdiParent = this.ParentForm;
                Sol.Show();
            //}

            //addgridhandler();
        }

        private void gridDocumenti_DoubleClick(object sender, EventArgs e)
        {
            object cellValue = gviewDocumenti.GetRowCellValue(gviewDocumenti.FocusedRowHandle, "PathDocumento");
            string PathDocumento = Convert.ToString(cellValue);

            System.Diagnostics.Process.Start(PathDocumento);
        }

        private void gridSoluzioniDetails_DoubleClick(object sender, EventArgs e)
        {
            //removegridhandler();

            //DXMouseEventArgs ea = e as DXMouseEventArgs;
            //DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = view.CalcHitInfo(ea.Location);
            //if (info.InRow && info.InRowCell)
            //{
            //    object val = view.GetRowCellValue(info.RowHandle, info.Column);

            //    DevExpress.XtraGrid.Columns.GridColumn col = gviewSoluzioniDetails.Columns.ColumnByFieldName("IDSoluzione");
                object cellValue = gviewSoluzioniDetails.GetRowCellValue(gviewSoluzioniDetails.FocusedRowHandle, "IDSoluzione");
                int ID = Convert.ToInt32(cellValue);

                if(ID!=0)
                {
                    frmSoluzioni Sol = new frmSoluzioni();
                    Sol.IDUtente = IDUtente;

                    Sol.IDSoluzioneCalled = ID;

                    Sol.MdiParent = this.ParentForm;
                    Sol.Show();
                }
                else
                {
                    //col = gviewSoluzioniDetails.Columns.ColumnByFieldName("IDSchedaDocumenti");
                    cellValue = gviewSoluzioniDetails.GetRowCellValue(gviewSoluzioniDetails.FocusedRowHandle, "IDSchedaDocumenti");
                    ID = Convert.ToInt32(cellValue);

                    if (ID != 0)
                    {
                        frmMaterialiMR MatMR = new frmMaterialiMR();

                        MatMR.IDSchedaDocumentiCalled = ID;

                        MatMR.MdiParent = this.ParentForm;
                        MatMR.Show();
                    }
                }
            //}

            //addgridhandler();
        }

        private void tbDuplica_ItemClick(object sender, ItemClickEventArgs e)
        {
            //int newid=0;

            //if (gviewSoluzioni.SelectedRowsCount == 1)
            //{
            //    //if (XtraMessageBox.Show("Sei sicuro di duplicare la soluzione selezionata?", "Duplica", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    if (XtraMessageBox.Show("Sei sicuro di duplicare la Soluzione MR selezionata?", "Duplica", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        EOS.Core.Control.Controller_Soluzioni ControlSoluzioni = new EOS.Core.Control.Controller_Soluzioni();
            //        ControlSoluzioni.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
            //        newid = ControlSoluzioni.CloneSolution(IDSoluzione);
            //        if (newid != 0)
            //        {
            //            IDSoluzioneCalled = newid;
            //            LoadData();

            //            abilitazionecontrolli();
            //        }
            //        else
            //        {
            //            XtraMessageBox.Show("L'operazione di duplicazione è terminata con errore.", "Duplica", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //    }
            //    else
            //    {
            //        XtraMessageBox.Show("L'operazione di duplicazione è stata annullata.", "Duplica", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            //else
            //{
            //    //XtraMessageBox.Show("E' possibile duplicare solo una soluzione alla volta.", "Duplica", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    XtraMessageBox.Show("E' possibile duplicare solo una Soluzione MR alla volta.", "Duplica", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            frmDuplica Duplica = new frmDuplica();
            Duplica.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
            Duplica.TipoElemento = "Soluzione";
            Duplica.IDElemento = IDSoluzione;
            Duplica.IDSchedaDocumenti = Convert.ToInt32(cboSingoloSolvente.EditValue);
            Duplica.IDSolvente = Convert.ToInt32(cboMiscelaSolventi.EditValue);
            Duplica.IDApparecchio = Convert.ToInt32(cboApparecchio.EditValue);
            Duplica.IDUtensile = Convert.ToInt32(cboUtensile.EditValue);
            Duplica.IDApparecchio2 = Convert.ToInt32(cboApparecchio2.EditValue);
            Duplica.IDUtensile2 = Convert.ToInt32(cboUtensile2.EditValue);
            Duplica.IDUtente = IDUtente;
            Duplica.ShowDialog();

            if (Duplica.newIDElemento != 0)
            {
                IDSoluzioneCalled = Duplica.newIDElemento;
                LoadData();
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            int Copie = 0;
            string Report = "";
            string Stampante = "";

            //int intref = 0;
            //int NumeroCopie = 1;
            //EOS.Core.Common.PrintLabel StampaEtichetta = new EOS.Core.Common.PrintLabel();
            //List<String> Codici = new List<String>();

            //if (gviewSoluzioni.SelectedRowsCount > 0)
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

            //        int[] rowHandles = gviewSoluzioni.GetSelectedRows();

            //        for (int c = 0; c < gviewSoluzioni.SelectedRowsCount; c++)
            //        {
            //            Codici.Add(gviewSoluzioni.GetRowCellValue(rowHandles[c], "CodiceSoluzione").ToString());
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
            //    //XtraMessageBox.Show("Nessuna soluzione selezionata.", "Stampa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    XtraMessageBox.Show("Nessuna Soluzione MR selezionata.", "Stampa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            frmPrintSelect PrintSelect = new frmPrintSelect();
            PrintSelect.Form = "frmSoluzioni";
            PrintSelect.ShowDialog();

            Copie = PrintSelect.Copie;
            Stampante = PrintSelect.Stampante;
            Report = PrintSelect.Report;

            if(Stampante!="")
            {
                int[] rowHandles = gviewSoluzioni.GetSelectedRows();

                for (int c = 0; c < gviewSoluzioni.SelectedRowsCount; c++)
                {
                    XtraReport myReport = new XtraReport();
                    myReport = XtraReport.FromFile(Report);

                    myReport.Parameters["parCodiceSoluzione"].Value = gviewSoluzioni.GetRowCellValue(rowHandles[c], "CodiceSoluzione").ToString();
                    myReport.Parameters["parTipoSoluzione"].Value = "MR";
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

            gviewSoluzioni.MoveLast();
            gviewSoluzioni.MoveFirst();

            for (int i = 0; i < gviewSoluzioni.DataRowCount; i++)
            {
                if (Convert.ToBoolean(gviewSoluzioni.GetRowCellValue(i, "Seleziona")))
                {
                    idlist = idlist + Convert.ToString(gviewSoluzioni.GetRowCellValue(i, "IDSoluzione")) + ", ";
                }
            }

            if (idlist != "")
            {
                idlist = idlist.Substring(0, idlist.Length - 2);

                global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();
                string SQL = "";

                SQL = SQL + "SELECT SOL.IDSoluzione ";
                SQL = SQL + "      ,SOL.CodiceSoluzione ";
                SQL = SQL + "      ,SOL.Tipologia AS TipologiaSoluzione ";
                SQL = SQL + "      ,SOL.Nome ";
                SQL = SQL + "      ,COM.Nome AS Stato ";
                SQL = SQL + "      ,SOL.NotePrescrittive ";
                SQL = SQL + "      ,SOL.NoteDescrittive ";
                SQL = SQL + "      ,UBI.Ubicazione ";
                SQL = SQL + "      ,MAT.DenominazioneProdotto AS Solvente ";
                SQL = SQL + "      ,SOV.Nome AS MiscelaSolventi ";
                SQL = SQL + "      ,SOL.VolumeFinale ";
                SQL = SQL + "      ,SOL.UMVolumeFinale ";
                SQL = SQL + "      ,SOL.DefaultGiorniScadenza ";
                SQL = SQL + "      ,convert(varchar(20),SOL.DataPreparazione,23) as DataPreparazione ";
                SQL = SQL + "      ,convert(varchar(20),SOL.DataScadenza,23) as DataScadenza ";
                SQL = SQL + "      ,convert(varchar(20),SOL.DataCreazione,23) as DataCreazione ";
                SQL = SQL + "      ,UTE.NomeUtente AS UtenteCreazione ";
                //SQL = SQL + "      ,APP.Descrizione AS ApparecchioPrelievo ";
                //SQL = SQL + "      ,UTI.Nome AS UtensilePrelievo ";
                SQL = SQL + "  FROM dbo.Soluzioni SOL ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Ubicazioni UBI ";
                SQL = SQL + "  ON SOL.IDUbicazione=UBI.IDUbicazione ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.SchedeDocumenti [CER] ";
                SQL = SQL + "  ON SOL.IDSchedaDocumenti=[CER].IDSchedaDocumenti ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Materiali MAT ";
                SQL = SQL + "  ON [CER].IDMateriale=MAT.IDMateriale ";
                SQL = SQL + "  LEFT JOIN Solventi SOV ";
                SQL = SQL + "  ON SOL.IDSolvente=SOV.IDSolvente ";
                SQL = SQL + "  LEFT JOIN Login_Utenti UTE ";
                SQL = SQL + "  ON SOL.IDUtente=UTE.IDUtente ";
                SQL = SQL + "  LEFT JOIN Composti_Stati COM ";
                SQL = SQL + "  ON SOL.IDStato=COM.ID ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Apparecchi APP ";
                SQL = SQL + "  ON SOL.IDApparecchio=APP.IDApparecchio ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Utensili UTI ";
                SQL = SQL + "  ON SOL.IDUtensile=UTI.IDUtensile ";
                SQL = SQL + "  WHERE SOL.IDSoluzione in (" + idlist + ") ";
                SQL = SQL + "  ORDER BY SOL.CodiceSoluzione ";

                DataTable dt = new DataTable();
                dt = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                string SQLString = "";

                SQLString = SQLString + "SELECT ";
                SQLString = SQLString + "SOL.[CodiceSoluzione] as CodiceSoluzioneMR, ";
                SQLString = SQLString + "SOLDET.[IDSoluzioneMaster], ";
                SQLString = SQLString + "SOLDET.[IDSoluzioneDetail], ";
                SQLString = SQLString + "SOLDET.[IDSchedaDocumenti], ";
                SQLString = SQLString + "MT.[Nome] as Tipologia_MR, ";
                SQLString = SQLString + "SOLDET.[CAS], ";
                SQLString = SQLString + "MAT.DenominazioneProdotto as MaterialeMR, ";
                SQLString = SQLString + "SOLDET.[IDSoluzione], ";
                SQLString = SQLString + "Sol.Tipologia as TipoSoluzione, ";
                SQLString = SQLString + "SOL.Nome as NomeSoluzione, ";
                SQLString = SQLString + "SOLDET.[UM_Prelievo], ";
                SQLString = SQLString + "SOLDET.[Quantita_Prelievo], ";
                SQLString = SQLString + "APP.NumeroApparecchio as ApparecchioPrelievo, ";
                SQLString = SQLString + "UTE.Nome as UtensilePrelievo, ";
                SQLString = SQLString + "APP2.NumeroApparecchio as ApparecchioPrelievo2, ";
                SQLString = SQLString + "UTE2.Nome as UtensilePrelievo2, ";
                SQLString = SQLString + "SOLDET.[Note], ";
                SQLString = SQLString + "SOLDET.[Concentrazione], ";
                SQLString = SQLString + "convert(varchar(20),SOLDET.[DataScadenza],23) as DataScadenza ";
                SQLString = SQLString + "FROM Soluzioni_Details SOLDET ";
                SQLString = SQLString + "LEFT JOIN Soluzioni SOL ";
                SQLString = SQLString + "ON SOLDET.IDSoluzioneMaster=SOL.IDSoluzione ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
                SQLString = SQLString + "ON SOLDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Materiali MAT ";
                SQLString = SQLString + "ON SCDOC.IDMateriale=MAT.IDMateriale ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Apparecchi APP ";
                SQLString = SQLString + "ON SOLDET.IDApparecchio=APP.IDApparecchio ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Utensili UTE ";
                SQLString = SQLString + "ON SOLDET.IDUtensile=UTE.IDUtensile ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Apparecchi APP2 ";
                SQLString = SQLString + "ON SOLDET.IDApparecchio2=APP2.IDApparecchio ";
                SQLString = SQLString + "LEFT JOIN [SERVER026].[LUPIN].dbo.Utensili UTE2 ";
                SQLString = SQLString + "ON SOLDET.IDUtensile2=UTE2.IDUtensile ";
                SQLString = SQLString + "LEFT JOIN Materiale_Tipologia MT ";
                SQLString = SQLString + "ON MT.ID=SOLDET.Tipologia_MR ";
                SQLString = SQLString + "where SOLDET.idSoluzioneMaster in (" + idlist + ") ";
                SQLString = SQLString + "Order By SOLDET.[IDSoluzioneMaster],SOLDET.[IDSoluzioneDetail] ";

                DataTable dtsub = new DataTable();
                dtsub = DB.GetDataTable(SQLString, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                SQLString = "";

                SQLString = SQLString + "SELECT SOL.CodiceSoluzione as CodiceSoluzioneMR, IDSoluzioneDetailConcentration, IDSoluzioneMaster, ";
                SQLString = SQLString + "CAS, ";
                //SQLString = SQLString + "(SELECT TOP 1 MAT.DenominazioneProdotto FROM Soluzioni_Details SOLDET LEFT JOIN Soluzioni SOL ON SOLDET.IDSoluzioneMaster=SOL.IDSoluzione LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ON SOLDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti LEFT JOIN [SERVER026].[LUPIN].dbo.Materiali MAT ON SCDOC.IDMateriale=MAT.IDMateriale where SOLDET.idSoluzioneMaster=SOLDETCONC.IDSoluzioneMaster AND SOLDET.CAS=SOLDETCONC.CAS ) AS DenominazioneProdotto, ";
                SQLString = SQLString + "(SELECT CASE WHEN ";
                SQLString = SQLString + "	(SELECT count(*) ";
                SQLString = SQLString + "	FROM Soluzioni_Details SOLDET ";
                SQLString = SQLString + "	LEFT JOIN Soluzioni SOL ";
                SQLString = SQLString + "	ON SOLDET.IDSoluzioneMaster=SOL.IDSoluzione ";
                SQLString = SQLString + "	LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
                SQLString = SQLString + "	ON SOLDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
                SQLString = SQLString + "	LEFT JOIN [SERVER026].[LUPIN].dbo.Materiali MAT ";
                SQLString = SQLString + "	ON SCDOC.IDMateriale=MAT.IDMateriale ";
                SQLString = SQLString + "	where SOLDET.idSoluzioneMaster=SOLDETCONC.IDSoluzioneMaster ";
                SQLString = SQLString + "	AND SOLDET.CAS=SOLDETCONC.CAS)>0 ";
                SQLString = SQLString + "THEN ";
                SQLString = SQLString + "	(SELECT top 1 MAT.DenominazioneProdotto ";
                SQLString = SQLString + "	FROM Soluzioni_Details SOLDET ";
                SQLString = SQLString + "	LEFT JOIN Soluzioni SOL ";
                SQLString = SQLString + "	ON SOLDET.IDSoluzioneMaster=SOL.IDSoluzione ";
                SQLString = SQLString + "	LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
                SQLString = SQLString + "	ON SOLDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
                SQLString = SQLString + "	LEFT JOIN [SERVER026].[LUPIN].dbo.Materiali MAT ";
                SQLString = SQLString + "	ON SCDOC.IDMateriale=MAT.IDMateriale ";
                SQLString = SQLString + "	where SOLDET.idSoluzioneMaster=SOLDETCONC.IDSoluzioneMaster ";
                SQLString = SQLString + "	AND SOLDET.CAS=SOLDETCONC.CAS) ";
                SQLString = SQLString + "ELSE ";
                SQLString = SQLString + "	CASE WHEN ";
                SQLString = SQLString + "		(SELECT count(*) ";
                SQLString = SQLString + "		FROM Soluzioni_Details SOLDET ";
                SQLString = SQLString + "		INNER JOIN [dbo].[Materiale_Tipologia] MT ";
                SQLString = SQLString + "		ON SOLDET.Tipologia_MR=MT.ID ";
                SQLString = SQLString + "		AND MT.Nome='Working Solution' ";
                SQLString = SQLString + "		LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
                SQLString = SQLString + "		ON SOLDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
                SQLString = SQLString + "		LEFT JOIN [SERVER026].[LUPIN].dbo.WorkingSolutionsDetails WSD ";
                SQLString = SQLString + "		ON SCDOC.IDSchedaDocumenti=WSD.IDSchedaDocumenti ";
                SQLString = SQLString + "		where SOLDET.idSoluzioneMaster=SOLDETCONC.IDSoluzioneMaster ";
                SQLString = SQLString + "		AND WSD.CasComponente=SOLDETCONC.CAS)>0 ";
                SQLString = SQLString + "	THEN ";
                SQLString = SQLString + "		(SELECT top 1 WSD.NomeComponente ";
                SQLString = SQLString + "		FROM Soluzioni_Details SOLDET ";
                SQLString = SQLString + "		INNER JOIN [dbo].[Materiale_Tipologia] MT ";
                SQLString = SQLString + "		ON SOLDET.Tipologia_MR=MT.ID ";
                SQLString = SQLString + "		AND MT.Nome='Working Solution' ";
                SQLString = SQLString + "		LEFT JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SCDOC ";
                SQLString = SQLString + "		ON SOLDET.IDSchedaDocumenti=SCDOC.IDSchedaDocumenti ";
                SQLString = SQLString + "		LEFT JOIN [SERVER026].[LUPIN].dbo.WorkingSolutionsDetails WSD ";
                SQLString = SQLString + "		ON SCDOC.IDSchedaDocumenti=WSD.IDSchedaDocumenti ";
                SQLString = SQLString + "		where SOLDET.idSoluzioneMaster=SOLDETCONC.IDSoluzioneMaster ";
                SQLString = SQLString + "		AND WSD.CasComponente=SOLDETCONC.CAS) ";
                SQLString = SQLString + "	ELSE ";
                //SQLString = SQLString + "		(select TOP 1 Materiali.DenominazioneProdotto ";
                //SQLString = SQLString + "		from [dbo].[Soluzioni_Details] ";
                //SQLString = SQLString + "		INNER JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti ";
                //SQLString = SQLString + "		ON Soluzioni_Details.IDSchedaDocumenti=SchedeDocumenti.IDSchedaDocumenti ";
                //SQLString = SQLString + "		INNER JOIN [SERVER026].[LUPIN].dbo.Materiali ";
                //SQLString = SQLString + "		ON SchedeDocumenti.IDMateriale=Materiali.IDMateriale ";
                //SQLString = SQLString + "		where [Soluzioni_Details].cas=SOLDETCONC.CAS) ";
                SQLString = SQLString + "	    CASE WHEN ";
                SQLString = SQLString + "		    (select count(*) ";
                SQLString = SQLString + "		    from [dbo].[Soluzioni_Details] ";
                SQLString = SQLString + "		    INNER JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti ";
                SQLString = SQLString + "	    	ON Soluzioni_Details.IDSchedaDocumenti=SchedeDocumenti.IDSchedaDocumenti ";
                SQLString = SQLString + "		    INNER JOIN [SERVER026].[LUPIN].dbo.Materiali ";
                SQLString = SQLString + "		    ON SchedeDocumenti.IDMateriale=Materiali.IDMateriale ";
                SQLString = SQLString + "		    where [Soluzioni_Details].cas=SOLDETCONC.CAS)>0 ";
                SQLString = SQLString + "	    THEN ";
                SQLString = SQLString + "		    (select TOP 1 Materiali.DenominazioneProdotto ";
                SQLString = SQLString + "		    from [dbo].[Soluzioni_Details] ";
                SQLString = SQLString + "		    INNER JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti ";
                SQLString = SQLString + "	    	ON Soluzioni_Details.IDSchedaDocumenti=SchedeDocumenti.IDSchedaDocumenti ";
                SQLString = SQLString + "		    INNER JOIN [SERVER026].[LUPIN].dbo.Materiali ";
                SQLString = SQLString + "		    ON SchedeDocumenti.IDMateriale=Materiali.IDMateriale ";
                SQLString = SQLString + "		    where [Soluzioni_Details].cas=SOLDETCONC.CAS) ";
                SQLString = SQLString + "	    ELSE ";
                SQLString = SQLString + "		    (SELECT top 1 WSD.NomeComponente ";
                SQLString = SQLString + "		    FROM [SERVER026].[LUPIN].dbo.WorkingSolutionsDetails WSD ";
                SQLString = SQLString + "		    where WSD.CasComponente=SOLDETCONC.CAS) ";
                SQLString = SQLString + "	    END ";
                SQLString = SQLString + "	END ";
                SQLString = SQLString + "END) AS DenominazioneProdotto, ";
                SQLString = SQLString + "ConcentrazioneFinale, ";
                SQLString = SQLString + "convert(varchar(20),DataCalcolo,23) as DataCalcolo ";
                SQLString = SQLString + "FROM Soluzioni_Details_Concentration SOLDETCONC ";
                SQLString = SQLString + "INNER JOIN Soluzioni SOL ";
                SQLString = SQLString + "ON SOL.IDSoluzione=SOLDETCONC.idSoluzioneMaster ";
                SQLString = SQLString + "where SOLDETCONC.idSoluzioneMaster in (" + idlist + ") ";
                SQLString = SQLString + "Order By IDSoluzioneMaster,IDSoluzioneDetailConcentration ";

                DataTable dtconc = new DataTable();
                dtconc = DB.GetDataTable(SQLString, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                string path = @"c:\temp\" + gviewSoluzioni.GetFocusedRowCellValue("CodiceSoluzione").ToString() + "_" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".xlsx";
                FileInfo fl = new FileInfo(path);

                using (ExcelPackage pck = new ExcelPackage(fl))
                {

                    int colNumber = 0;

                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Soluzione");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, TableStyles.None);

                    foreach (DataColumn col in dt.Columns)
                    {
                        colNumber++;
                        if (col.DataType == typeof(DateTime))
                        {
                            ws.Column(colNumber).Style.Numberformat.Format = "mm/dd/yyyy hh:mm:ss AM/PM";
                        }
                    }

                    ws = pck.Workbook.Worksheets.Add("Soluzione Componenti");
                    ws.Cells["A1"].LoadFromDataTable(dtsub, true, TableStyles.None);

                    foreach (DataColumn col in dtsub.Columns)
                    {
                        colNumber++;
                        if (col.DataType == typeof(DateTime))
                        {
                            ws.Column(colNumber).Style.Numberformat.Format = "mm/dd/yyyy hh:mm:ss AM/PM";
                        }
                    }

                    ws = pck.Workbook.Worksheets.Add("Soluzioni Concentrazioni");
                    ws.Cells["A1"].LoadFromDataTable(dtconc, true, TableStyles.None);
                    
                    foreach (DataColumn col in dtconc.Columns)
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
                //XtraMessageBox.Show("E' necessario selezionare una soluzione.", "Esporta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                XtraMessageBox.Show("E' necessario selezionare una Soluzione MR.", "Esporta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }    
        }

        private void tbStampaReport_ItemClick(object sender, ItemClickEventArgs e)
        {

            //int intref = 0;
            //int NumeroCopie = 1;
            //EOS.Core.Common.PrintLabel StampaEtichetta = new EOS.Core.Common.PrintLabel();
            //List<String> Codici = new List<String>();

            //if (gviewSoluzioni.SelectedRowsCount > 0)
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

            //        int[] rowHandles = gviewSoluzioni.GetSelectedRows();

            //        for (int c = 0; c < gviewSoluzioni.SelectedRowsCount; c++)
            //        {
            //            Codici.Add(gviewSoluzioni.GetRowCellValue(rowHandles[c], "CodiceMiscelaSoluzioni").ToString());
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
            //    XtraMessageBox.Show("Nessuna Soluzione MR selezionata.", "Stampa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            ////XtraReport myReport = new XtraReport();
            ////myReport = XtraReport.FromFile(@"\\server024\apps\EOS\Documenti\Reports\EtichettaSoluzione50x12.repx");
            ////myReport.Parameters["parCodiceSoluzione"].Value = "ASDSADAS";
            ////myReport.Parameters["parTipoSoluzione"].Value = "MR";
            ////myReport.Parameters["parConnectionString"].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

            ////// Show the report's Print Preview.

            //////ReportPrintTool printTool = new ReportPrintTool(myReport);
            //////printTool.ShowPreviewDialog();

            ////myReport.CreateDocument();
            ////myReport.Print(@"\\printersrv\cab04");

            //////NorthwindContext dbContext = new NorthwindContext();
            //////var fetchedRows = dbContext.Categories.ToList<Category>();
            //////(myReport.DataSource as ObjectDataSource).DataSource = fetchedRows;
            //////return View(myReport); //binding a report to the viewer  
        }

        private void butAggiungiDocumento_Click(object sender, EventArgs e)
        {
            frmDocumenti frmDocumento = new frmDocumenti();
            frmDocumento.CodiceComposto = gviewSoluzioni.GetFocusedRowCellValue("CodiceSoluzione").ToString();
            frmDocumento.IDDocumento = 0;
            frmDocumento.AggiungiNuova = true;
            frmDocumento.ShowDialog();

            IDSoluzioneCalled = IDSoluzione;
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

                IDSoluzioneCalled = IDSoluzione;
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

                IDSoluzioneCalled = IDSoluzione;
                LoadData();
            }
            else
            {
                XtraMessageBox.Show("Cancellazione annullata.", "Cancella", MessageBoxButtons.OK);
            }
        }

        private void frmSoluzioni_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.IO.MemoryStream str = new System.IO.MemoryStream();
            gridSoluzioni.FocusedView.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            frmLogin.SaveUserPreferences("frmSoluzioni", "gridSoluzioni", str, IDUtente);

            str = new System.IO.MemoryStream();
            gridSoluzioniDetails.FocusedView.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            frmLogin.SaveUserPreferences("frmSoluzioni", "gridSoluzioniDetails", str, IDUtente);

            str = new System.IO.MemoryStream();
            gridSoluzioniDetailsConcentration.FocusedView.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            frmLogin.SaveUserPreferences("frmSoluzioni", "gridSoluzioniDetailsConcentration", str, IDUtente);

            str = new System.IO.MemoryStream();
            gridDocumenti.FocusedView.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            frmLogin.SaveUserPreferences("frmSoluzioni", "gridDocumenti", str, IDUtente);

            str = null;
        }

        private void tbVisualizzaLog_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmLog LogView = new frmLog();
            LogView.IDUtente = IDUtente;

            int idSoluzione = 0;

            if (gviewSoluzioni.SelectedRowsCount == 1)
            {
                int[] selectedRows = gviewSoluzioni.GetSelectedRows();
                int selectedRow = 0;
                foreach (int rowHandle in selectedRows)
                {
                    selectedRow = rowHandle;
                }
                if (selectedRow > -1)
                {
                    idSoluzione = Int32.Parse(gviewSoluzioni.GetRowCellValue(selectedRow, "IDSoluzione").ToString());
                }
            }

            EOS.Core.Control.Control_Transcode ctlTranscode = new EOS.Core.Control.Control_Transcode();

            LogView.CodiceSoluzione = ctlTranscode.GetCodiceSoluzioneByID(idSoluzione);

            ctlTranscode = null;

            LogView.Show();
        }
    }
}