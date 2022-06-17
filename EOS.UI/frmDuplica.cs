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
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using DevExpress.Utils;
using EOS.Core;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.IO;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraReports.UI;

namespace EOS.UI
{

    public partial class frmDuplica : Form
    {
        public string TipoElemento;
        public int IDElemento = 0;
        public int newIDElemento = 0;
        public int IDUtente = 0;
        public int IDSchedaDocumenti = 0;
        public int IDSolvente = 0;
        public int IDApparecchio = 0;
        public int IDUtensile = 0;
        public int IDApparecchio2 = 0;
        public int IDUtensile2 = 0;
        private string NotaUtilizzoScaduto = "";
        bool changingMiscelaSolventi = false;
        bool changingSingoloSolvente = false;
        bool changingApparecchio = false;
        bool changingUtensile = false;
        bool changingApparecchio2 = false;
        bool changingUtensile2 = false;
        public string ConnectionString;

        public frmDuplica()
        {
            InitializeComponent();
        }

        private void frmDuplica_Load(object sender, EventArgs e)
        {
            // TODO: questa riga di codice carica i dati nella tabella 'lupin.UtensiliSelectCommand'. È possibile spostarla o rimuoverla se necessario. 
            this.utensiliSelectCommandTableAdapter.Fill(this.lupin.UtensiliSelectCommand);
            // TODO: questa riga di codice carica i dati nella tabella 'lupin.ApparecchiSelectCommand'. È possibile spostarla o rimuoverla se necessario.
            this.apparecchiSelectCommandTableAdapter.Fill(this.lupin.ApparecchiSelectCommand);
            // TODO: questa riga di codice carica i dati nella tabella 'solventi.Solventi_Tipologia'. È possibile spostarla o rimuoverla se necessario.
            this.solventi_TipologiaTableAdapter.Fill(this.solventi.Solventi_Tipologia);
            // TODO: questa riga di codice carica i dati nella tabella 'soluzioni.Soluzioni_Tipologia'. È possibile spostarla o rimuoverla se necessario.
            this.soluzioni_TipologiaTableAdapter.Fill(this.soluzioni.Soluzioni_Tipologia);
            // TODO: questa riga di codice carica i dati nella tabella 'solventi._Solventi'. È possibile spostarla o rimuoverla se necessario.
            this.solventiTableAdapter.Fill(this.solventi._Solventi);
            // TODO: questa riga di codice carica i dati nella tabella 'soluzioni._Soluzioni'. È possibile spostarla o rimuoverla se necessario.
            this.soluzioniTableAdapter.Fill(this.soluzioni._Soluzioni);
            // TODO: questa riga di codice carica i dati nella tabella 'lupin.MaterialiLottiSelectCommand'. È possibile spostarla o rimuoverla se necessario.
            this.materialiLottiSelectCommandTableAdapter.Fill(this.lupin.MaterialiLottiSelectCommand);

            EOS.Core.Control.Control_Utenti ctlUtente = new EOS.Core.Control.Control_Utenti();

            if (!ctlUtente.CloneAllowedCheck(IDUtente))
            {
                string filter = " (NomeTipologia <> 'Soluzione MR Modello') ";

                cboTipologiaSoluzioneMR.Properties.View.ActiveFilter.Add(cboTipologiaSoluzioneMR.Properties.View.Columns["ID"], new ColumnFilterInfo(filter, ""));

                filter = " (NomeTipologia <> 'Soluzione di Lavoro Modello') ";

                cboTipologiaSoluzioniDiLavoro.Properties.View.ActiveFilter.Add(cboTipologiaSoluzioniDiLavoro.Properties.View.Columns["ID"], new ColumnFilterInfo(filter, ""));
            }

            //boTipologiaSoluzioniDiLavoro

            ctlUtente = null;

            try
            {
                global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();
                string SQLString = "";
                string SQLStringMAT = "";

                if (TipoElemento == "Soluzione")
                {
                    lblSoluzioniDiLavoro.Enabled = false;
                    cboTipologiaSoluzioniDiLavoro.Enabled = false;
                    lblTipologiaSoluzioniDiLavoro.Enabled = false;
                    gridDuplicaSolventiMAT.Enabled = false;
                    gridDuplicaSolventiSOV.Enabled = false;
                    lblDuplicaSolventiMAT.Enabled = false;
                    lblDuplicaSolventiSOV.Enabled = false;

                    SQLString = SQLString + "SELECT ";
                    SQLString = SQLString + "SOLDET.[IDSoluzioneDetail] ";
                    SQLString = SQLString + ",SDOC.[IDMateriale] ";
                    SQLString = SQLString + ",SOLDET.[IDSchedaDocumenti] ";
                    SQLString = SQLString + ",SOLDET.[Tipologia_MR] ";
                    SQLString = SQLString + ",SOLDET.[CAS] ";
                    SQLString = SQLString + ",MAT.Identificativo as CodiceProdotto ";
                    SQLString = SQLString + ",MAT.DenominazioneProdotto as DenominazioneProdotto ";
                    SQLString = SQLString + ",SOLDET.[IDSoluzione] ";
                    SQLString = SQLString + ",SOL.CodiceSoluzione ";
                    SQLString = SQLString + ",SOL.Nome as NomeSoluzione ";
                    SQLString = SQLString + ",SOLDET.[UM_Prelievo] ";
                    SQLString = SQLString + ",SOLDET.[Quantita_Prelievo] ";
                    SQLString = SQLString + ",SOLDET.[Note] ";
                    SQLString = SQLString + ",SOLDET.[Concentrazione] ";
                    SQLString = SQLString + ",SOLDET.[DataScadenza] ";
                    SQLString = SQLString + ",0 as [IDSoluzioneSelezionata] ";
                    SQLString = SQLString + ",'' as [NoteScaduto] ";
                    SQLString = SQLString + ",SOLDET.IDApparecchio ";
                    SQLString = SQLString + ",SOLDET.IDUtensile ";
                    SQLString = SQLString + ",SOLDET.IDApparecchio2 ";
                    SQLString = SQLString + ",SOLDET.IDUtensile2 ";
                    SQLString = SQLString + "FROM [EOS].[dbo].[Soluzioni_Details] SOLDET ";
                    SQLString = SQLString + "left join [SERVER026].[LUPIN].dbo.SchedeDocumenti SDOC ";
                    SQLString = SQLString + "ON SOLDET.IDSchedaDocumenti=SDOC.IDSchedaDocumenti ";
                    SQLString = SQLString + "left join [SERVER026].[LUPIN].dbo.Materiali MAT ";
                    SQLString = SQLString + "ON SDOC.IDMateriale=MAT.IDMateriale ";
                    SQLString = SQLString + "left join Soluzioni SOL ";
                    SQLString = SQLString + "ON SOLDET.IDSoluzione=SOL.IDSoluzione ";
                    SQLString = SQLString + "WHERE SOLDET.IDSoluzioneMaster={0} ";
                    SQLString = SQLString + "AND SOLDET.IDSoluzione<>0 ";
                    SQLString = string.Format(SQLString, IDElemento);

                    SQLStringMAT = SQLStringMAT + "SELECT ";
                    SQLStringMAT = SQLStringMAT + "SOLDET.[IDSoluzioneDetail] ";
                    SQLStringMAT = SQLStringMAT + ",SDOC.[IDMateriale] ";
                    SQLStringMAT = SQLStringMAT + ",SOLDET.[IDSchedaDocumenti] ";
                    SQLStringMAT = SQLStringMAT + ",SOLDET.[Tipologia_MR] ";
                    SQLStringMAT = SQLStringMAT + ",SOLDET.[CAS] ";
                    SQLStringMAT = SQLStringMAT + ",MAT.Identificativo as CodiceProdotto ";
                    SQLStringMAT = SQLStringMAT + ",MAT.DenominazioneProdotto as DenominazioneProdotto ";
                    SQLStringMAT = SQLStringMAT + ",SOLDET.[IDSoluzione] ";
                    SQLStringMAT = SQLStringMAT + ",SOL.CodiceSoluzione ";
                    SQLStringMAT = SQLStringMAT + ",SOL.Nome as NomeSoluzione ";
                    SQLStringMAT = SQLStringMAT + ",SOLDET.[UM_Prelievo] ";
                    SQLStringMAT = SQLStringMAT + ",SOLDET.[Quantita_Prelievo] ";
                    SQLStringMAT = SQLStringMAT + ",SOLDET.[Note] ";
                    SQLStringMAT = SQLStringMAT + ",SOLDET.[Concentrazione] ";
                    SQLStringMAT = SQLStringMAT + ",SOLDET.[DataScadenza] ";
                    SQLStringMAT = SQLStringMAT + ",0 as [IDSchedaDocumentiSelezionata] ";
                    SQLStringMAT = SQLStringMAT + ",'' as [NoteScaduto] ";
                    SQLStringMAT = SQLStringMAT + ",SOLDET.IDApparecchio ";
                    SQLStringMAT = SQLStringMAT + ",SOLDET.IDUtensile ";
                    SQLStringMAT = SQLStringMAT + ",SOLDET.IDApparecchio2 ";
                    SQLStringMAT = SQLStringMAT + ",SOLDET.IDUtensile2 ";
                    SQLStringMAT = SQLStringMAT + "FROM [EOS].[dbo].[Soluzioni_Details] SOLDET ";
                    SQLStringMAT = SQLStringMAT + "left join [SERVER026].[LUPIN].dbo.SchedeDocumenti SDOC ";
                    SQLStringMAT = SQLStringMAT + "ON SOLDET.IDSchedaDocumenti=SDOC.IDSchedaDocumenti ";
                    SQLStringMAT = SQLStringMAT + "left join [SERVER026].[LUPIN].dbo.Materiali MAT ";
                    SQLStringMAT = SQLStringMAT + "ON SDOC.IDMateriale=MAT.IDMateriale ";
                    SQLStringMAT = SQLStringMAT + "left join Soluzioni SOL ";
                    SQLStringMAT = SQLStringMAT + "ON SOLDET.IDSoluzione=SOL.IDSoluzione ";
                    SQLStringMAT = SQLStringMAT + "WHERE SOLDET.IDSoluzioneMaster={0} ";
                    SQLStringMAT = SQLStringMAT + "AND SOLDET.IDSchedaDocumenti<>0 ";
                    SQLStringMAT = string.Format(SQLStringMAT, IDElemento);

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //inplace repository
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    string SQL = "";

                    SQL = SQL + "SELECT ";
                    SQL = SQL + "SOL.IDSoluzione, ";
                    SQL = SQL + "SOL.IDStato, ";
                    SQL = SQL + "SOL.CodiceSoluzione as CodiceSoluzioneMR, ";
                    SQL = SQL + "SOL.Tipologia, ";
                    SQL = SQL + "SOL.Nome, ";
                    SQL = SQL + "SOL.NotePrescrittive, ";
                    SQL = SQL + "SOL.NoteDescrittive, ";
                    SQL = SQL + "UBI.Ubicazione, ";
                    SQL = SQL + "LOTT.Lotto as LottoMateriale, ";
                    SQL = SQL + "SOV.CodiceSolvente, ";
                    SQL = SQL + "SOL.VolumeFinale, ";
                    SQL = SQL + "SOL.UMVolumeFinale, ";
                    SQL = SQL + "SOL.DefaultGiorniScadenza, ";
                    SQL = SQL + "SOL.DataPreparazione, ";
                    SQL = SQL + "SOL.DataScadenza, ";
                    SQL = SQL + "SOL.DataCreazione, ";
                    SQL = SQL + "UTENTI.NomeUtente as Utente, ";
                    SQL = SQL + "STAT.Nome as Stato, ";
                    SQL = SQL + "APP.NumeroApparecchio as Apparecchio, ";
                    SQL = SQL + "UTE.Nome as Utensile, ";
                    SQL = SQL + "APP2.NumeroApparecchio as Apparecchio2, ";
                    SQL = SQL + "UTE2.Nome as Utensile2 ";
                    SQL = SQL + "FROM Soluzioni SOL ";
                    SQL = SQL + "LEFT JOIN Composti_Stati STAT ";
                    SQL = SQL + "ON SOL.IDStato=STAT.ID ";
                    SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].dbo.Apparecchi APP ";
                    SQL = SQL + "ON SOL.IDApparecchio=APP.IDApparecchio ";
                    SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].dbo.Utensili UTE ";
                    SQL = SQL + "ON SOL.IDUtensile=UTE.IDUtensile ";
                    SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].dbo.Apparecchi APP2 ";
                    SQL = SQL + "ON SOL.IDApparecchio2=APP2.IDApparecchio ";
                    SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].dbo.Utensili UTE2 ";
                    SQL = SQL + "ON SOL.IDUtensile2=UTE.IDUtensile ";
                    SQL = SQL + "LEFT JOIN Solventi SOV ";
                    SQL = SQL + "ON SOL.IDSolvente=SOV.IDSolvente ";
                    SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].dbo.Ubicazioni UBI ";
                    SQL = SQL + "ON SOL.IDUbicazione=UBI.IDUbicazione ";
                    SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].dbo.[SchedeDocumenti] LOTT ";
                    SQL = SQL + "ON SOL.IDSchedaDocumenti=LOTT.IDSchedaDocumenti ";
                    SQL = SQL + "LEFT JOIN Login_Utenti UTENTI ";
                    SQL = SQL + "ON SOL.IDUtente=UTENTI.IDUtente ";
                    SQL = SQL + "WHERE STAT.Nome<>'Annullata' ";

                    DataTable dtinplaceaSOL = new DataTable();
                    dtinplaceaSOL = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                    cboSoluzioniSelezionaSOL.DataSource = null;
                    cboSoluzioniSelezionaSOL.View.Columns.Clear();
                    cboSoluzioniSelezionaSOL.DataSource = dtinplaceaSOL;
                    cboSoluzioniSelezionaSOL.View.PopulateColumns(cboSoluzioniSelezionaSOL.DataSource);
                    cboSoluzioniSelezionaSOL.View.Columns.ColumnByFieldName("IDSoluzione").Visible = false;
                    cboSoluzioniSelezionaSOL.View.Columns.ColumnByFieldName("IDStato").Visible = false;

                    SQL = "";

                    SQL = SQL + "SELECT ";
                    SQL = SQL + "SC.IDSchedaDocumenti, ";
                    SQL = SQL + "MAT.IDMateriale, ";
                    SQL = SQL + "MAT.DenominazioneProdotto AS DenominazioneMateriale, ";
                    SQL = SQL + "MAT.DataScadenza AS DataScadenzaMateriale, ";
                    SQL = SQL + "MAT.MesiScadenza AS MesiScadenzaMateriale, ";
                    SQL = SQL + "MAT.Cas AS CasMateriale, ";
                    SQL = SQL + "SC.DataScadenza AS DataScadenzaLotto, ";
                    SQL = SQL + "SC.DataInserimento AS DataInserimentoLotto, ";
                    SQL = SQL + "SC.Certificato AS CertificatoLotto,";
                    SQL = SQL + "SC.Lotto AS CodiceLotto, ";
                    SQL = SQL + "SC.Purezza AS PurezzaLotto, ";
                    SQL = SQL + "SC.Acqua AS AcquaLotto, ";
                    SQL = SQL + "SC.CodiceArticolo AS CodiceArticoloLotto, ";
                    SQL = SQL + "SC.Densita AS DensitaLotto, ";
                    SQL = SQL + "SC.Concentrazione AS ConcentrazioneLotto, ";
                    SQL = SQL + "SC.UM_Concentrazione AS UMConcentrazioneLotto ";
                    SQL = SQL + "FROM [SERVER026].[LUPIN].dbo.SchedeDocumenti SC ";
                    SQL = SQL + "INNER JOIN [SERVER026].[LUPIN].dbo.Materiali MAT ";
                    SQL = SQL + "ON SC.IDMateriale = MAT.IDMateriale ";
                    SQL = SQL + "ORDER BY DenominazioneMateriale ";

                    DataTable dtinplaceaMAT = new DataTable();
                    dtinplaceaMAT = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                    cboSoluzioniSelezionaMAT.DataSource = null;
                    cboSoluzioniSelezionaMAT.View.Columns.Clear();
                    cboSoluzioniSelezionaMAT.DataSource = dtinplaceaMAT;
                    cboSoluzioniSelezionaMAT.View.PopulateColumns(cboSoluzioniSelezionaMAT.DataSource);
                    cboSoluzioniSelezionaMAT.View.Columns.ColumnByFieldName("IDSchedaDocumenti").Visible = false;
                    cboSoluzioniSelezionaMAT.View.Columns.ColumnByFieldName("IDMateriale").Visible = false;

                    SQL = "";
                    SQL = SQL + "SELECT ";
                    SQL = SQL + "App.IDApparecchio, ";
                    SQL = SQL + "App.NumeroApparecchio, ";
                    SQL = SQL + "App.NumeroCespite, ";
                    SQL = SQL + "App.Descrizione, ";
                    SQL = SQL + "App.NumeroSerie, ";
                    SQL = SQL + "App.Modello, ";
                    SQL = SQL + "App.Marca, ";
                    SQL = SQL + "Tip.Nome AS Tipologia, ";
                    SQL = SQL + "Ser.Nome as StatoDiServizio ";
                    SQL = SQL + "FROM [SERVER026].[LUPIN].dbo.Apparecchi App ";
                    SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].dbo.TipologieApparecchi Tip ";
                    SQL = SQL + "ON App.IDTipologiaApparecchio=Tip.IDTipologiaApparecchio ";
                    SQL = SQL + "INNER JOIN [SERVER026].[LUPIN].dbo.StatiServizio Ser ";
                    SQL = SQL + "ON App.IDStatoServizio=Ser.IDStatoServizio ";

                    DataTable dtinplace_APP = new DataTable();
                    dtinplace_APP = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                    cboSoluzioniSelezionaMAT_APP.DataSource = null;
                    cboSoluzioniSelezionaMAT_APP.View.Columns.Clear();
                    cboSoluzioniSelezionaMAT_APP.DataSource = dtinplace_APP;
                    cboSoluzioniSelezionaMAT_APP.View.PopulateColumns(cboSoluzioniSelezionaMAT_APP.DataSource);
                    cboSoluzioniSelezionaMAT_APP.View.Columns.ColumnByFieldName("IDApparecchio").Visible = false;

                    cboSoluzioniSelezionaMAT_APP2.DataSource = null;
                    cboSoluzioniSelezionaMAT_APP2.View.Columns.Clear();
                    cboSoluzioniSelezionaMAT_APP2.DataSource = dtinplace_APP;
                    cboSoluzioniSelezionaMAT_APP2.View.PopulateColumns(cboSoluzioniSelezionaMAT_APP2.DataSource);
                    cboSoluzioniSelezionaMAT_APP2.View.Columns.ColumnByFieldName("IDApparecchio").Visible = false;

                    cboSoluzioniSelezionaSOL_APP.DataSource = null;
                    cboSoluzioniSelezionaSOL_APP.View.Columns.Clear();
                    cboSoluzioniSelezionaSOL_APP.DataSource = dtinplace_APP;
                    cboSoluzioniSelezionaSOL_APP.View.PopulateColumns(cboSoluzioniSelezionaSOL_APP.DataSource);
                    cboSoluzioniSelezionaSOL_APP.View.Columns.ColumnByFieldName("IDApparecchio").Visible = false;

                    cboSoluzioniSelezionaSOL_APP2.DataSource = null;
                    cboSoluzioniSelezionaSOL_APP2.View.Columns.Clear();
                    cboSoluzioniSelezionaSOL_APP2.DataSource = dtinplace_APP;
                    cboSoluzioniSelezionaSOL_APP2.View.PopulateColumns(cboSoluzioniSelezionaSOL_APP2.DataSource);
                    cboSoluzioniSelezionaSOL_APP2.View.Columns.ColumnByFieldName("IDApparecchio").Visible = false;

                    SQL = "";
                    SQL = SQL + "SELECT ";
                    SQL = SQL + "IDUtensile, ";
                    SQL = SQL + "Descrizione, ";
                    SQL = SQL + "Nome ";
                    SQL = SQL + "FROM [SERVER026].[LUPIN].dbo.Utensili ";

                    DataTable dtinplace_UTE = new DataTable();
                    dtinplace_UTE = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                    cboSoluzioniSelezionaMAT_UTE.DataSource = null;
                    cboSoluzioniSelezionaMAT_UTE.View.Columns.Clear();
                    cboSoluzioniSelezionaMAT_UTE.DataSource = dtinplace_UTE;
                    cboSoluzioniSelezionaMAT_UTE.View.PopulateColumns(cboSoluzioniSelezionaMAT_UTE.DataSource);
                    cboSoluzioniSelezionaMAT_UTE.View.Columns.ColumnByFieldName("IDUtensile").Visible = false;

                    cboSoluzioniSelezionaMAT_UTE2.DataSource = null;
                    cboSoluzioniSelezionaMAT_UTE2.View.Columns.Clear();
                    cboSoluzioniSelezionaMAT_UTE2.DataSource = dtinplace_UTE;
                    cboSoluzioniSelezionaMAT_UTE2.View.PopulateColumns(cboSoluzioniSelezionaMAT_UTE2.DataSource);
                    cboSoluzioniSelezionaMAT_UTE2.View.Columns.ColumnByFieldName("IDUtensile").Visible = false;

                    cboSoluzioniSelezionaSOL_UTE.DataSource = null;
                    cboSoluzioniSelezionaSOL_UTE.View.Columns.Clear();
                    cboSoluzioniSelezionaSOL_UTE.DataSource = dtinplace_UTE;
                    cboSoluzioniSelezionaSOL_UTE.View.PopulateColumns(cboSoluzioniSelezionaSOL_UTE.DataSource);
                    cboSoluzioniSelezionaSOL_UTE.View.Columns.ColumnByFieldName("IDUtensile").Visible = false;

                    cboSoluzioniSelezionaSOL_UTE2.DataSource = null;
                    cboSoluzioniSelezionaSOL_UTE2.View.Columns.Clear();
                    cboSoluzioniSelezionaSOL_UTE2.DataSource = dtinplace_UTE;
                    cboSoluzioniSelezionaSOL_UTE2.View.PopulateColumns(cboSoluzioniSelezionaSOL_UTE2.DataSource);
                    cboSoluzioniSelezionaSOL_UTE2.View.Columns.ColumnByFieldName("IDUtensile").Visible = false;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //inplace repository
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else
                {
                    lblTipologiaSoluzioneMR.Enabled = false;
                    cboTipologiaSoluzioneMR.Enabled = false;
                    lblSoluzioniMR.Enabled = false;
                    gridDuplicaSoluzioniMAT.Enabled = false;
                    gridDuplicaSoluzioniSOL.Enabled = false;
                    cboSingoloSolvente.Enabled = false;
                    cboSolventeApparecchio.Enabled = false;
                    cboSolventeApparecchio2.Enabled = false;
                    cboSolventeUtensile.Enabled = false;
                    cboSolventeUtensile2.Enabled = false;
                    cboSoluzioneDiLavoro.Enabled = false;
                    lblSolventeApparecchio.Enabled = false;
                    lblSolventeApparecchio2.Enabled = false;
                    lblSolventeUtensile.Enabled = false;
                    lblSolventeUtensile2.Enabled = false;
                    lblDuplicaSoluzioniMAT.Enabled = false;
                    lblDuplicaSoluzioniSOL.Enabled = false;
                    lblSingoloSolvente.Enabled = false;
                    lblSoluzioneDiLavoro.Enabled = false;
                    lblSolvente.Enabled = false;

                    SQLString = SQLString + "SELECT ";
                    SQLString = SQLString + "SOLVDET.[IDSolventeDetail] ";
                    SQLString = SQLString + ",SDOC.[IDMateriale] ";
                    SQLString = SQLString + ",SOLVDET.[IDSchedaDocumenti] ";
                    SQLString = SQLString + ",SOLVDET.[Tipologia_MR] ";
                    SQLString = SQLString + ",SOLVDET.[CAS] ";
                    SQLString = SQLString + ",MAT.Identificativo as CodiceProdotto ";
                    SQLString = SQLString + ",MAT.DenominazioneProdotto as DenominazioneProdotto ";
                    SQLString = SQLString + ",SOLVDET.[IDSolvente] ";
                    SQLString = SQLString + ",'' as CodiceSolvente ";
                    SQLString = SQLString + ",'' as NomeSolvente ";
                    SQLString = SQLString + ",SOLV.CodiceSolvente ";
                    SQLString = SQLString + ",SOLV.Nome as NomeSolvente ";
                    SQLString = SQLString + ",SOLVDET.[UM_Prelievo] ";
                    SQLString = SQLString + ",SOLVDET.[Quantita_Prelievo] ";
                    SQLString = SQLString + ",SOLVDET.[Note] ";
                    SQLString = SQLString + ",SOLVDET.[DataScadenza] ";
                    SQLString = SQLString + ",0 as [IDSolventeSelezionato] ";
                    SQLString = SQLString + ",'' as [NoteScaduto] ";
                    SQLString = SQLString + ",SOLVDET.IDApparecchio ";
                    SQLString = SQLString + ",SOLVDET.IDUtensile ";
                    SQLString = SQLString + ",SOLVDET.IDApparecchio2 ";
                    SQLString = SQLString + ",SOLVDET.IDUtensile2 ";
                    SQLString = SQLString + "FROM [EOS].[dbo].[Solventi_Details] SOLVDET ";
                    SQLString = SQLString + "left join [SERVER026].[LUPIN].dbo.SchedeDocumenti SDOC ";
                    SQLString = SQLString + "ON SOLVDET.IDSchedaDocumenti=SDOC.IDSchedaDocumenti ";
                    SQLString = SQLString + "left join [SERVER026].[LUPIN].dbo.Materiali MAT ";
                    SQLString = SQLString + "ON SDOC.IDMateriale=MAT.IDMateriale ";
                    SQLString = SQLString + "left join Solventi SOLV ";
                    SQLString = SQLString + "ON SOLVDET.IDSolvente=SOLV.IDSolvente ";
                    SQLString = SQLString + "WHERE SOLVDET.IDSolventeMaster={0} ";
                    SQLString = SQLString + "AND SOLVDET.IDSolvente<>0 ";
                    SQLString = string.Format(SQLString, IDElemento);

                    SQLStringMAT = SQLStringMAT + "SELECT ";
                    SQLStringMAT = SQLStringMAT + "SOLVDET.[IDSolventeDetail] ";
                    SQLStringMAT = SQLStringMAT + ",SDOC.[IDMateriale] ";
                    SQLStringMAT = SQLStringMAT + ",SOLVDET.[IDSchedaDocumenti] ";
                    SQLStringMAT = SQLStringMAT + ",SOLVDET.[Tipologia_MR] ";
                    SQLStringMAT = SQLStringMAT + ",SOLVDET.[CAS] ";
                    SQLStringMAT = SQLStringMAT + ",MAT.Identificativo as CodiceProdotto ";
                    SQLStringMAT = SQLStringMAT + ",MAT.DenominazioneProdotto as DenominazioneProdotto ";
                    SQLStringMAT = SQLStringMAT + ",SOLVDET.[IDSolvente] ";
                    SQLStringMAT = SQLStringMAT + ",SOLV.CodiceSolvente ";
                    SQLStringMAT = SQLStringMAT + ",SOLV.Nome as NomeSolvente ";
                    SQLStringMAT = SQLStringMAT + ",SOLVDET.[UM_Prelievo] ";
                    SQLStringMAT = SQLStringMAT + ",SOLVDET.[Quantita_Prelievo] ";
                    SQLStringMAT = SQLStringMAT + ",SOLVDET.[Note] ";
                    SQLStringMAT = SQLStringMAT + ",SOLVDET.[DataScadenza] ";
                    SQLStringMAT = SQLStringMAT + ",0 as [IDSchedaDocumentiSelezionata] ";
                    SQLStringMAT = SQLStringMAT + ",'' as [NoteScaduto] ";
                    SQLStringMAT = SQLStringMAT + ",SOLVDET.IDApparecchio ";
                    SQLStringMAT = SQLStringMAT + ",SOLVDET.IDUtensile ";
                    SQLStringMAT = SQLStringMAT + ",SOLVDET.IDApparecchio2 ";
                    SQLStringMAT = SQLStringMAT + ",SOLVDET.IDUtensile2 ";
                    SQLStringMAT = SQLStringMAT + "FROM [EOS].[dbo].[Solventi_Details] SOLVDET ";
                    SQLStringMAT = SQLStringMAT + "left join [SERVER026].[LUPIN].dbo.SchedeDocumenti SDOC ";
                    SQLStringMAT = SQLStringMAT + "ON SOLVDET.IDSchedaDocumenti=SDOC.IDSchedaDocumenti ";
                    SQLStringMAT = SQLStringMAT + "left join [SERVER026].[LUPIN].dbo.Materiali MAT ";
                    SQLStringMAT = SQLStringMAT + "ON SDOC.IDMateriale=MAT.IDMateriale ";
                    SQLStringMAT = SQLStringMAT + "left join Solventi SOLV ";
                    SQLStringMAT = SQLStringMAT + "ON SOLVDET.IDSolvente=SOLV.IDSolvente ";
                    SQLStringMAT = SQLStringMAT + "WHERE SOLVDET.IDSolventeMaster={0} ";
                    SQLStringMAT = SQLStringMAT + "AND SOLVDET.IDSchedaDocumenti<>0 ";
                    SQLStringMAT = string.Format(SQLStringMAT, IDElemento);

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //inplace repository
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    string SQL = "";

                    SQL = SQL + "SELECT ";
                    SQL = SQL + "SOV.IDSolvente, ";
                    SQL = SQL + "SOV.IDStato, ";
                    SQL = SQL + "SOV.CodiceSolvente as CodiceSoluzioneDiLavoro, ";
                    SQL = SQL + "SOV.Tipologia, ";
                    SQL = SQL + "SOV.Nome, ";
                    SQL = SQL + "SOV.NotePrescrittive, ";
                    SQL = SQL + "SOV.NoteDescrittive, ";
                    SQL = SQL + "UBI.Ubicazione, ";
                    SQL = SQL + "SOV.DefaultGiorniScadenza, ";
                    SQL = SQL + "SOV.DataPreparazione, ";
                    SQL = SQL + "SOV.DataScadenza, ";
                    SQL = SQL + "SOV.DataCreazione, ";
                    SQL = SQL + "UTENTI.NomeUtente as Utente, ";
                    SQL = SQL + "STAT.Nome as Stato ";
                    SQL = SQL + "FROM SOLVENTI SOV ";
                    SQL = SQL + "LEFT JOIN Composti_Stati STAT ";
                    SQL = SQL + "ON SOV.IDStato=STAT.ID ";
                    SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].dbo.Ubicazioni UBI ";
                    SQL = SQL + "ON SOV.IDUbicazione=UBI.IDUbicazione ";
                    SQL = SQL + "LEFT JOIN Login_Utenti UTENTI ";
                    SQL = SQL + "ON SOV.IDUtente=UTENTI.IDUtente ";
                    SQL = SQL + "WHERE STAT.Nome<>'Annullata' ";

                    DataTable dtinplacebSOV = new DataTable();
                    dtinplacebSOV = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                    cboSolventiSelezionaSOV.DataSource = null;
                    cboSolventiSelezionaSOV.View.Columns.Clear();
                    cboSolventiSelezionaSOV.DataSource = dtinplacebSOV;
                    cboSolventiSelezionaSOV.View.PopulateColumns(cboSolventiSelezionaSOV.DataSource);
                    cboSolventiSelezionaSOV.View.Columns.ColumnByFieldName("IDSolvente").Visible = false;
                    cboSolventiSelezionaSOV.View.Columns.ColumnByFieldName("IDStato").Visible = false;

                    SQL = "";

                    SQL = SQL + "SELECT ";
                    SQL = SQL + "SC.IDSchedaDocumenti, ";
                    SQL = SQL + "MAT.IDMateriale, ";
                    SQL = SQL + "MAT.DenominazioneProdotto AS DenominazioneMateriale, ";
                    SQL = SQL + "MAT.DataScadenza AS DataScadenzaMateriale, ";
                    SQL = SQL + "MAT.MesiScadenza AS MesiScadenzaMateriale, ";
                    SQL = SQL + "MAT.Cas AS CasMateriale, ";
                    SQL = SQL + "SC.DataScadenza AS DataScadenzaLotto, ";
                    SQL = SQL + "SC.DataInserimento AS DataInserimentoLotto, ";
                    SQL = SQL + "SC.Certificato AS CertificatoLotto,";
                    SQL = SQL + "SC.Lotto AS CodiceLotto, ";
                    SQL = SQL + "SC.Purezza AS PurezzaLotto, ";
                    SQL = SQL + "SC.Acqua AS AcquaLotto, ";
                    SQL = SQL + "SC.CodiceArticolo AS CodiceArticoloLotto, ";
                    SQL = SQL + "SC.Densita AS DensitaLotto, ";
                    SQL = SQL + "SC.Concentrazione AS ConcentrazioneLotto, ";
                    SQL = SQL + "SC.UM_Concentrazione AS UMConcentrazioneLotto ";
                    SQL = SQL + "FROM [SERVER026].[LUPIN].dbo.SchedeDocumenti SC ";
                    SQL = SQL + "INNER JOIN [SERVER026].[LUPIN].dbo.Materiali MAT ";
                    SQL = SQL + "ON SC.IDMateriale = MAT.IDMateriale ";
                    SQL = SQL + "ORDER BY DenominazioneMateriale ";

                    DataTable dtinplacecMAT = new DataTable();
                    dtinplacecMAT = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                    cboSolventiSelezionaMAT.DataSource = null;
                    cboSolventiSelezionaMAT.View.Columns.Clear();
                    cboSolventiSelezionaMAT.DataSource = dtinplacecMAT;
                    cboSolventiSelezionaMAT.View.PopulateColumns(cboSolventiSelezionaMAT.DataSource);
                    cboSolventiSelezionaMAT.View.Columns.ColumnByFieldName("IDSchedaDocumenti").Visible = false;
                    cboSolventiSelezionaMAT.View.Columns.ColumnByFieldName("IDMateriale").Visible = false;

                    SQL = "";
                    SQL = SQL + "SELECT ";
                    SQL = SQL + "App.IDApparecchio, ";
                    SQL = SQL + "App.NumeroApparecchio, ";
                    SQL = SQL + "App.NumeroCespite, ";
                    SQL = SQL + "App.Descrizione, ";
                    SQL = SQL + "App.NumeroSerie, ";
                    SQL = SQL + "App.Modello, ";
                    SQL = SQL + "App.Marca, ";
                    SQL = SQL + "Tip.Nome AS Tipologia, ";
                    SQL = SQL + "Ser.Nome as StatoDiServizio ";
                    SQL = SQL + "FROM [SERVER026].[LUPIN].dbo.Apparecchi App ";
                    SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].dbo.TipologieApparecchi Tip ";
                    SQL = SQL + "ON App.IDTipologiaApparecchio=Tip.IDTipologiaApparecchio ";
                    SQL = SQL + "INNER JOIN [SERVER026].[LUPIN].dbo.StatiServizio Ser ";
                    SQL = SQL + "ON App.IDStatoServizio=Ser.IDStatoServizio ";

                    DataTable dtinplace_APP = new DataTable();
                    dtinplace_APP = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                    cboSolventiSelezionaMAT_APP.DataSource = null;
                    cboSolventiSelezionaMAT_APP.View.Columns.Clear();
                    cboSolventiSelezionaMAT_APP.DataSource = dtinplace_APP;
                    cboSolventiSelezionaMAT_APP.View.PopulateColumns(cboSolventiSelezionaMAT_APP.DataSource);
                    cboSolventiSelezionaMAT_APP.View.Columns.ColumnByFieldName("IDApparecchio").Visible = false;

                    cboSolventiSelezionaMAT_APP2.DataSource = null;
                    cboSolventiSelezionaMAT_APP2.View.Columns.Clear();
                    cboSolventiSelezionaMAT_APP2.DataSource = dtinplace_APP;
                    cboSolventiSelezionaMAT_APP2.View.PopulateColumns(cboSolventiSelezionaMAT_APP2.DataSource);
                    cboSolventiSelezionaMAT_APP2.View.Columns.ColumnByFieldName("IDApparecchio").Visible = false;

                    cboSolventiSelezionaSOV_APP.DataSource = null;
                    cboSolventiSelezionaSOV_APP.View.Columns.Clear();
                    cboSolventiSelezionaSOV_APP.DataSource = dtinplace_APP;
                    cboSolventiSelezionaSOV_APP.View.PopulateColumns(cboSolventiSelezionaSOV_APP.DataSource);
                    cboSolventiSelezionaSOV_APP.View.Columns.ColumnByFieldName("IDApparecchio").Visible = false;

                    cboSolventiSelezionaSOV_APP2.DataSource = null;
                    cboSolventiSelezionaSOV_APP2.View.Columns.Clear();
                    cboSolventiSelezionaSOV_APP2.DataSource = dtinplace_APP;
                    cboSolventiSelezionaSOV_APP2.View.PopulateColumns(cboSolventiSelezionaSOV_APP2.DataSource);
                    cboSolventiSelezionaSOV_APP2.View.Columns.ColumnByFieldName("IDApparecchio").Visible = false;

                    SQL = "";
                    SQL = SQL + "SELECT ";
                    SQL = SQL + "IDUtensile, ";
                    SQL = SQL + "Descrizione, ";
                    SQL = SQL + "Nome ";
                    SQL = SQL + "FROM [SERVER026].[LUPIN].dbo.Utensili ";

                    DataTable dtinplace_UTE = new DataTable();
                    dtinplace_UTE = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                    cboSolventiSelezionaMAT_UTE.DataSource = null;
                    cboSolventiSelezionaMAT_UTE.View.Columns.Clear();
                    cboSolventiSelezionaMAT_UTE.DataSource = dtinplace_UTE;
                    cboSolventiSelezionaMAT_UTE.View.PopulateColumns(cboSolventiSelezionaMAT_UTE.DataSource);
                    cboSolventiSelezionaMAT_UTE.View.Columns.ColumnByFieldName("IDUtensile").Visible = false;

                    cboSolventiSelezionaMAT_UTE2.DataSource = null;
                    cboSolventiSelezionaMAT_UTE2.View.Columns.Clear();
                    cboSolventiSelezionaMAT_UTE2.DataSource = dtinplace_UTE;
                    cboSolventiSelezionaMAT_UTE2.View.PopulateColumns(cboSolventiSelezionaMAT_UTE2.DataSource);
                    cboSolventiSelezionaMAT_UTE2.View.Columns.ColumnByFieldName("IDUtensile").Visible = false;

                    cboSolventiSelezionaSOV_UTE.DataSource = null;
                    cboSolventiSelezionaSOV_UTE.View.Columns.Clear();
                    cboSolventiSelezionaSOV_UTE.DataSource = dtinplace_UTE;
                    cboSolventiSelezionaSOV_UTE.View.PopulateColumns(cboSolventiSelezionaSOV_UTE.DataSource);
                    cboSolventiSelezionaSOV_UTE.View.Columns.ColumnByFieldName("IDUtensile").Visible = false;

                    cboSolventiSelezionaSOV_UTE2.DataSource = null;
                    cboSolventiSelezionaSOV_UTE2.View.Columns.Clear();
                    cboSolventiSelezionaSOV_UTE2.DataSource = dtinplace_UTE;
                    cboSolventiSelezionaSOV_UTE2.View.PopulateColumns(cboSolventiSelezionaSOV_UTE2.DataSource);
                    cboSolventiSelezionaSOV_UTE2.View.Columns.ColumnByFieldName("IDUtensile").Visible = false;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //inplace repository
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }

                DataTable dt = new DataTable();
                dt = DB.GetDataTable(SQLString, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                DataTable dtMAT = new DataTable();
                dtMAT = DB.GetDataTable(SQLStringMAT, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                //gridDuplicaSoluzioni.DataSource = null;
                //gviewDuplicaSoluzioni.Columns.Clear();
                //gridDuplicaSoluzioni.DataSource = dt;
                //gviewDuplicaSoluzioni.PopulateColumns();
                //gridDuplicaSoluzioni.ForceInitialize();

                if (TipoElemento == "Soluzione")
                {
                    gridDuplicaSoluzioniSOL.DataSource = null;
                    //gviewDuplicaSoluzioniSOL.Columns.Clear();
                    gridDuplicaSoluzioniSOL.DataSource = dt;
                    gridDuplicaSoluzioniSOL.ForceInitialize();

                    //gviewDuplicaSoluzioniSOL.Columns[0].Visible = false;
                    //gviewDuplicaSoluzioniSOL.Columns[1].Visible = false;
                    //gviewDuplicaSoluzioniSOL.Columns[2].Visible = false;
                    //gviewDuplicaSoluzioniSOL.Columns[7].Visible = false;

                    gridDuplicaSoluzioniMAT.DataSource = null;
                    //gviewDuplicaSoluzioniMAT.Columns.Clear();
                    gridDuplicaSoluzioniMAT.DataSource = dtMAT;
                    gridDuplicaSoluzioniMAT.ForceInitialize();

                    //gviewDuplicaSoluzioniMAT.Columns[0].Visible = false;
                    //gviewDuplicaSoluzioniMAT.Columns[1].Visible = false;
                    //gviewDuplicaSoluzioniMAT.Columns[2].Visible = false;
                    //gviewDuplicaSoluzioniMAT.Columns[7].Visible = false;

                    //for (int i = 0; i < gviewDuplicaSoluzioniMAT.RowCount; i++)
                    //{
                    //    DataRowView row = gviewDuplicaSoluzioniMAT.GetRow(i) as DataRowView;
                    //    if (row != null)
                    //    {

                    //    }
                    //}

                    //for (int i = 0; i < gviewDuplicaSoluzioniMAT.DataRowCount; i++)
                    //{
                    //    //gviewDuplicaSoluzioni.rows
                    //    //Object SingleObject = gviewDuplicaSoluzioni.GetRow(i);
                    //    //DataGridViewRow SingleRow = (DataGridViewRow)SingleObject;
                    //    //SingleRow.cel;
                    //    //if (gviewDuplicaSoluzioni.GetRowCellValue(i, "FieldName") == "")
                    //    //{

                    //    //}    
                    //}
                    System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmDuplica", "gridDuplicaSoluzioniMAT", IDUtente);

                    if (str != null)
                    {
                        gridDuplicaSoluzioniMAT.FocusedView.RestoreLayoutFromStream(str);
                        str.Seek(0, System.IO.SeekOrigin.Begin);
                    }

                    str = frmLogin.LoadUserPreferences("frmDuplica", "gridDuplicaSoluzioniSOL", IDUtente);

                    if (str != null)
                    {
                        gridDuplicaSoluzioniSOL.FocusedView.RestoreLayoutFromStream(str);
                        str.Seek(0, System.IO.SeekOrigin.Begin);
                    }

                    str = null;
                }
                else
                {
                    gridDuplicaSolventiSOV.DataSource = null;
                    //gviewDuplicaSolventiSOV.Columns.Clear();
                    gridDuplicaSolventiSOV.DataSource = dt;
                    gridDuplicaSolventiSOV.ForceInitialize();

                    //gviewDuplicaSolventiSOV.Columns[0].Visible = false;
                    //gviewDuplicaSolventiSOV.Columns[1].Visible = false;
                    //gviewDuplicaSolventiSOV.Columns[2].Visible = false;
                    //gviewDuplicaSolventiSOV.Columns[7].Visible = false;

                    gridDuplicaSolventiMAT.DataSource = null;
                    //gviewDuplicaSolventiMAT.Columns.Clear();
                    gridDuplicaSolventiMAT.DataSource = dtMAT;
                    gridDuplicaSolventiMAT.ForceInitialize();

                    //gviewDuplicaSolventiMAT.Columns[0].Visible = false;
                    //gviewDuplicaSolventiMAT.Columns[1].Visible = false;
                    //gviewDuplicaSolventiMAT.Columns[2].Visible = false;
                    //gviewDuplicaSolventiMAT.Columns[7].Visible = false;

                    cboTipologiaSoluzioniDiLavoro.EditValue = "Soluzione di Lavoro Standard";

                    System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmDuplica", "gridDuplicaSolventiMAT", IDUtente);

                    if (str != null)
                    {
                        gridDuplicaSolventiMAT.FocusedView.RestoreLayoutFromStream(str);
                        str.Seek(0, System.IO.SeekOrigin.Begin);
                    }

                    str = frmLogin.LoadUserPreferences("frmDuplica", "gridDuplicaSolventiSOV", IDUtente);

                    if (str != null)
                    {
                        gridDuplicaSolventiSOV.FocusedView.RestoreLayoutFromStream(str);
                        str.Seek(0, System.IO.SeekOrigin.Begin);
                    }

                    str = null;
                }

                cboSingoloSolvente.EditValue = IDSchedaDocumenti;
                cboSoluzioneDiLavoro.EditValue = IDSolvente;
                cboSolventeApparecchio.EditValue = IDApparecchio;
                cboSolventeApparecchio2.EditValue = IDApparecchio2;
                cboSolventeUtensile.EditValue = IDUtensile;
                cboSolventeUtensile2.EditValue = IDUtensile2;

                addcontrolhandler();
                
                addEventHandler();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        private void butChiudi_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void cboSeleziona_Shown(object sender, EventArgs e)
        //{
        //    ColumnView view = (ColumnView)sender;
        //    if (view.FocusedColumn.Name == "colSeleziona" && view.ActiveEditor is GridLookUpEdit)
        //    {
        //        GridLookUpEdit edit = (GridLookUpEdit)view.ActiveEditor;
        //        int IDSchedaDocumenti = (int)view.GetFocusedRowCellValue("IDSchedaDocumenti");

        //        try
        //        {
        //            global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();
        //            string SQL = "";

        //            if(IDSchedaDocumenti!=0)
        //            {
        //                SQL = "";
        //                SQL = SQL + "SELECT IDSchedaDocumenti, ";
        //                SQL = SQL + "ARTICOLO.Identificativo, ";
        //                SQL = SQL + "LOTTO.Lotto, ";
        //                SQL = SQL + "ARTICOLO.DenominazioneProdotto, ";
        //                SQL = SQL + "ARTICOLO.Cas as CAS, ";
        //                SQL = SQL + "LOTTO.DataInserimento, ";
        //                SQL = SQL + "LOTTO.DataScadenza ";
        //                SQL = SQL + "FROM [SERVER026].[LUPIN].[dbo].[Materiali] ARTICOLO ";
        //                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].[dbo].TipoMateriale TIPO ";
        //                SQL = SQL + "ON ARTICOLO.IDTipoMateriale=TIPO.IDTipoMateriale ";
        //                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].[dbo].Ubicazioni UBICAZIONE ";
        //                SQL = SQL + "ON ARTICOLO.IDUbicazione=UBICAZIONE.IDUbicazione ";
        //                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].[dbo].StatoMateriale STATO ";
        //                SQL = SQL + "ON ARTICOLO.IDStatoMateriale=STATO.IDStatoMateriale ";
        //                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].[dbo].ClasseMateriale CLASSE ";
        //                SQL = SQL + "ON ARTICOLO.IDClasseMateriale=CLASSE.IDClasseMateriale ";
        //                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].[dbo].Reparti REPARTO ";
        //                SQL = SQL + "ON ARTICOLO.IDReparto=REPARTO.IDReparto ";
        //                SQL = SQL + "INNER JOIN [SERVER026].[LUPIN].[dbo].[SchedeDocumenti] LOTTO ";
        //                SQL = SQL + "ON ARTICOLO.IDMateriale=LOTTO.IDMateriale ";
        //                SQL = SQL + "WHERE LOTTO.IDSchedaDocumenti IN (Select SDLIST.IDSchedaDocumenti from [SERVER026].[LUPIN].dbo.SchedeDocumenti SD INNER JOIN [SERVER026].[LUPIN].dbo.Materiali MAT ON SD.IDMateriale=MAT.IDMateriale INNER JOIN [SERVER026].[LUPIN].dbo.SchedeDocumenti SDLIST ON MAT.IDMateriale=SDLIST.IDMateriale where SD.IDSchedaDocumenti=" + IDSchedaDocumenti + ") ";
        //            }
        //            else
        //            {
        //                SQL = "";
        //                SQL = SQL + "SELECT SOL.IDSoluzione ";
        //                SQL = SQL + "      ,SOL.Tipologia AS TipologiaSoluzione ";
        //                SQL = SQL + "      ,SOL.Nome ";
        //                SQL = SQL + "      ,SOL.DataPreparazione ";
        //                SQL = SQL + "      ,SOL.DataScadenza ";
        //                SQL = SQL + "  FROM dbo.Soluzioni SOL ";
        //                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Ubicazioni UBI ";
        //                SQL = SQL + "  ON SOL.IDUbicazione=UBI.IDUbicazione ";
        //                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.SchedeDocumenti [CER] ";
        //                SQL = SQL + "  ON SOL.IDSchedaDocumenti=[CER].IDSchedaDocumenti ";
        //                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Materiali MAT ";
        //                SQL = SQL + "  ON [CER].IDMateriale=MAT.IDMateriale ";
        //                SQL = SQL + "  LEFT JOIN Solventi SOV ";
        //                SQL = SQL + "  ON SOL.IDSolvente=SOV.IDSolvente ";
        //                SQL = SQL + "  LEFT JOIN Login_Utenti UTE ";
        //                SQL = SQL + "  ON SOL.IDUtente=UTE.IDUtente ";
        //                SQL = SQL + "  LEFT JOIN Composti_Stati COM ";
        //                SQL = SQL + "  ON SOL.IDStato=COM.ID ";
        //                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Apparecchi APP ";
        //                SQL = SQL + "  ON SOL.IDApparecchio=APP.IDApparecchio ";
        //                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Utensili UTI ";
        //                SQL = SQL + "  ON SOL.IDUtensile=UTI.IDUtensile ";
        //                SQL = SQL + "  WHERE ";
        //                SQL = SQL + "  AND SOL.TIPOLOGIA IN ('Soluzione Intermedia','Soluzione Madre') ";
        //                SQL = SQL + "  AND COM.NOME IN ('In Uso','Scaduta') ";
        //            }    


        //            DataTable dt = new DataTable();
        //            dt = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

        //            edit.Properties.DataSource = dt;

        //            edit.Properties.PopupFormMinSize = new Size(1000, 400);

        //            if (IDSchedaDocumenti != 0)
        //            {
        //                edit.Properties.DisplayMember = "Lotto";
        //                edit.Properties.ValueMember = "IDSchedaDocumenti";
        //            }
        //            else
        //            {
        //                edit.Properties.DisplayMember = "Nome";
        //                edit.Properties.ValueMember = "IDSoluzione";
        //            }
        //        }
        //        catch (Exception a)
        //        {

        //        }
        //    }
        //}

        private void butSalva_Click(object sender, EventArgs e)
        {
            string errori = "";
            if (TipoElemento == "Soluzione")
            {
                if (cboTipologiaSoluzioneMR.EditValue == null)
                {
                    errori = errori + "Indicare la tipologia di Soluzione MR." + System.Environment.NewLine;
                }

                if ((cboSoluzioneDiLavoro.EditValue == null) && (cboSingoloSolvente.EditValue == null))
                {
                    errori = errori + "Scegliere un solvente tra gli articoli o tra le soluzioni di lavoro." + System.Environment.NewLine;
                }

                //int[] rowHandles = gviewDuplicaSoluzioniMAT.GetSelectedRows();

                for (int c = 0; c < gviewDuplicaSoluzioniMAT.RowCount; c++)
                {
                    if (gviewDuplicaSoluzioniMAT.GetRowCellValue(c, "IDSchedaDocumentiSelezionata").ToString() == "0")
                    {
                        errori = errori + "Scegliere un lotto per ogni articolo presente nella ricetta." + System.Environment.NewLine;
                        break; 
                    }
                }

                //rowHandles = gviewDuplicaSoluzioniSOL.GetSelectedRows();

                for (int c = 0; c < gviewDuplicaSoluzioniSOL.RowCount; c++)
                {
                    if (gviewDuplicaSoluzioniSOL.GetRowCellValue(c, "IDSoluzioneSelezionata").ToString() == "0")
                    {
                        errori = errori + "Scegliere una Soluzione MR per ogni Soluzione MR presente nella ricetta." + System.Environment.NewLine;
                        break;
                    }
                }
            }
            else
            {
                if (cboTipologiaSoluzioniDiLavoro.EditValue == null)
                {
                    errori = errori + "Indicare la tipologia di Soluzione di Lavoro." + System.Environment.NewLine;
                }

                for (int c = 0; c < gviewDuplicaSolventiMAT.RowCount; c++)
                {
                    if (gviewDuplicaSolventiMAT.GetRowCellValue(c, "IDSchedaDocumentiSelezionata").ToString() == "0")
                    {
                        errori = errori + "Scegliere un lotto per ogni articolo presente nella ricetta." + System.Environment.NewLine;
                        break; ;
                    }
                }

                //rowHandles = gviewDuplicaSolventiSOV.GetSelectedRows();

                for (int c = 0; c < gviewDuplicaSolventiSOV.RowCount; c++)
                {
                    if (gviewDuplicaSolventiSOV.GetRowCellValue(c, "IDSolventeSelezionato").ToString() == "0")
                    {
                        errori = errori + "Scegliere una Soluzione di Lavoro per ogni Soluzione di Lavoro presente nella ricetta." + System.Environment.NewLine;
                        break;
                    }
                }
            }

            if (errori != "")
            {
                XtraMessageBox.Show(errori, "Crea da ricetta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string nota = "";

                if (TipoElemento == "Soluzione")
                {
                    int newid;
                    object newcode = "";

                    try
                    {
                        System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                        cnn.ConnectionString = ConnectionString;
                        cnn.Open();

                        SqlCommand cmdstore = new SqlCommand();
                        cmdstore.Connection = cnn;
                        cmdstore.CommandType = CommandType.StoredProcedure;
                        cmdstore.CommandText = "dbo.GetProgressivo";

                        cmdstore.Parameters.Add("@TIPOPROGRESSIVO", SqlDbType.VarChar, 50).Value = "SOL";

                        newcode = cmdstore.ExecuteScalar();

                        cmdstore = null;

                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                        string SQLString = "";
                        SQLString = SQLString + "INSERT INTO [Soluzioni] ";
                        SQLString = SQLString + "SELECT ";
                        SQLString = SQLString + "'" + cboTipologiaSoluzioneMR.EditValue.ToString() + "' as [Tipologia] ";
                        SQLString = SQLString + ",[Nome] as Nome ";
                        SQLString = SQLString + ",[NotePrescrittive] ";
                        SQLString = SQLString + ",[NoteDescrittive] ";
                        SQLString = SQLString + ",[IDUbicazione] ";

                        if (cboSingoloSolvente.EditValue == null)
                        {
                            SQLString = SQLString + ",NULL as [IDSchedaDocumenti] ";
                        }
                        else
                        {
                            SQLString = SQLString + "," + cboSingoloSolvente.EditValue.ToString() + " as [IDSchedaDocumenti] ";
                        }

                        if (cboSoluzioneDiLavoro.EditValue == null)
                        {
                            SQLString = SQLString + ",NULL as [IDSolvente] ";
                        }
                        else
                        {
                            SQLString = SQLString + "," + cboSoluzioneDiLavoro.EditValue.ToString() + " as [IDSolvente] ";
                        }

                        SQLString = SQLString + ",[VolumeFinale] ";
                        SQLString = SQLString + ",[UMVolumeFinale] ";
                        SQLString = SQLString + ",[DefaultGiorniScadenza] ";
                        SQLString = SQLString + ",getdate() as DataPreparazione ";
                        SQLString = SQLString + ",NULL as DataScadenza ";
                        SQLString = SQLString + ",getdate() as DataCreazione ";
                        SQLString = SQLString + "," + IDUtente.ToString() + " as [IDUtente] ";
                        SQLString = SQLString + ",5 as IDStato ";

                        //SQLString = SQLString + ",[IDApparecchio] ";
                        //SQLString = SQLString + ",[IDUtensile] ";

                        if (cboSolventeApparecchio.EditValue == null)
                        {
                            SQLString = SQLString + ",NULL as [IDApparecchio] ";
                        }
                        else
                        {
                            SQLString = SQLString + "," + cboSolventeApparecchio.EditValue.ToString() + " as [IDApparecchio] ";
                        }

                        if (cboSolventeUtensile.EditValue == null)
                        {
                            SQLString = SQLString + ",NULL as [IDUtensile] ";
                        }
                        else
                        {
                            SQLString = SQLString + "," + cboSolventeUtensile.EditValue.ToString() + " as [IDUtensile] ";
                        }

                        SQLString = SQLString + ",'" + newcode.ToString() + "' as CodiceSoluzione ";
                        
                        //SQLString = SQLString + ",[IDApparecchio2] ";
                        //SQLString = SQLString + ",[IDUtensile2] ";

                        if (cboSolventeApparecchio2.EditValue == null)
                        {
                            SQLString = SQLString + ",NULL as [IDApparecchio2] ";
                        }
                        else
                        {
                            SQLString = SQLString + "," + cboSolventeApparecchio2.EditValue.ToString() + " as [IDApparecchio2] ";
                        }

                        if (cboSolventeUtensile2.EditValue == null)
                        {
                            SQLString = SQLString + ",NULL as [IDUtensile2] ";
                        }
                        else
                        {
                            SQLString = SQLString + "," + cboSolventeUtensile2.EditValue.ToString() + " as [IDUtensile2] ";
                        }

                        SQLString = SQLString + "FROM[EOS].[dbo].[Soluzioni] ";
                        SQLString = SQLString + "WHERE IDSoluzione = {0}; SELECT SCOPE_IDENTITY() ";
                        SQLString = string.Format(SQLString, IDElemento);

                        cmd.CommandText = SQLString;
                        cmd.Connection = cnn;
                        newid = Convert.ToInt32(cmd.ExecuteScalar());

                        Core.Model.Model_Soluzioni ModelSoluzione = new Core.Model.Model_Soluzioni();
                        Core.Control.Controller_Soluzioni ControlSoluzione = new Core.Control.Controller_Soluzioni();
                        ControlSoluzione.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                        ControlSoluzione.IDUtente = IDUtente;
                        ModelSoluzione = ControlSoluzione.GetSolutionByID(newid).First().Value;
                        ControlSoluzione.AddLogSoluzione("Inserimento", ModelSoluzione, ModelSoluzione.DataPreparazione.ToString(), ModelSoluzione.DataScadenza.ToString(), ModelSoluzione.DataCreazione.ToString());
                        ControlSoluzione = null;
                        ModelSoluzione = null;

                        for (int c = 0; c < gviewDuplicaSoluzioniMAT.RowCount; c++)
                        {
                            EOS.Core.Model.Model_MaterialiMR ModelMaterialeSelezionato = new EOS.Core.Model.Model_MaterialiMR();
                            EOS.Core.Control.Control_MaterialiMR ControlMaterialeSelezionato = new EOS.Core.Control.Control_MaterialiMR();
                            ControlMaterialeSelezionato.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                            ModelMaterialeSelezionato = ControlMaterialeSelezionato.GetByIDSchedaDocumenti(Convert.ToInt32(gviewDuplicaSoluzioniMAT.GetRowCellValue(c, "IDSchedaDocumentiSelezionata"))).First().Value;

                            if (gviewDuplicaSoluzioniMAT.GetRowCellValue(c, "NoteScaduto") != null)
                            {
                                nota = gviewDuplicaSoluzioniMAT.GetRowCellValue(c, "NoteScaduto").ToString();
                            }
                            else
                            {
                                nota = "";
                            }

                            SQLString = "";
                            SQLString = SQLString + "INSERT INTO Soluzioni_Details ";
                            SQLString = SQLString + "SELECT ";
                            SQLString = SQLString + "{0} as IDSoluzioneMaster ";
                            SQLString = SQLString + ",[Tipologia_MR] ";
                            SQLString = SQLString + "," + ModelMaterialeSelezionato.IDSchedaDocumenti + " as IDSchedaDocumenti ";
                            SQLString = SQLString + ",[CAS] ";
                            SQLString = SQLString + ",[IDSoluzione] ";
                            SQLString = SQLString + ",[UM_Prelievo] ";
                            SQLString = SQLString + ",[Quantita_Prelievo] ";
                            //SQLString = SQLString + ",[IDApparecchio] ";
                            //SQLString = SQLString + ",[IDUtensile] ";

                            if (gviewDuplicaSoluzioniMAT.GetRowCellValue(c, "IDApparecchio").Equals(System.DBNull.Value))
                            {
                                SQLString = SQLString + ",0 ";
                            }
                            else
                            {
                                SQLString = SQLString + "," + Convert.ToInt32(gviewDuplicaSoluzioniMAT.GetRowCellValue(c, "IDApparecchio")) + " ";
                            }

                            if (gviewDuplicaSoluzioniMAT.GetRowCellValue(c, "IDUtensile").Equals(System.DBNull.Value))
                            {
                                SQLString = SQLString + ",0 ";
                            }
                            else
                            {
                                SQLString = SQLString + "," + Convert.ToInt32(gviewDuplicaSoluzioniMAT.GetRowCellValue(c, "IDUtensile")) + " ";
                            }

                            SQLString = SQLString + ",Note + '" + Environment.NewLine + nota + "' as [Note] ";
                            SQLString = SQLString + ",[Concentrazione] ";
                            //SQLString = SQLString + ",left(right('" + ModelMaterialeSelezionato.DataScadenza_Lotto.ToString() + "',13),4) + '/' + right(left('" + ModelMaterialeSelezionato.DataScadenza_Lotto.ToString() + "',5),2) + '/' + left('" + ModelMaterialeSelezionato.DataScadenza_Lotto.ToString() + "',2) as DataScadenza ";
                            SQLString = SQLString + ",case when '" + ModelMaterialeSelezionato.DataScadenza_Lotto.ToString() + "' <> '01/01/0001 00:00:00' then '" + ModelMaterialeSelezionato.DataScadenza_Lotto.ToString() + "' else null end as DataScadenza ";
                            //SQLString = SQLString + ",[IDApparecchio2] ";
                            //SQLString = SQLString + ",[IDUtensile2] ";

                            if(gviewDuplicaSoluzioniMAT.GetRowCellValue(c, "IDApparecchio2").Equals(System.DBNull.Value))
                            {
                                SQLString = SQLString + ",0 ";
                            }
                            else
                            {
                                SQLString = SQLString + "," + Convert.ToInt32(gviewDuplicaSoluzioniMAT.GetRowCellValue(c, "IDApparecchio2")) + " ";
                            }

                            if (gviewDuplicaSoluzioniMAT.GetRowCellValue(c, "IDUtensile2").Equals(System.DBNull.Value))
                            {
                                SQLString = SQLString + ",0 ";
                            }
                            else
                            {
                                SQLString = SQLString + "," + Convert.ToInt32(gviewDuplicaSoluzioniMAT.GetRowCellValue(c, "IDUtensile2")) + " ";
                            }

                            SQLString = SQLString + "FROM [EOS].[dbo].[Soluzioni_Details] ";
                            SQLString = SQLString + "WHERE IDSoluzioneDetail={1}; SELECT SCOPE_IDENTITY() ";
                            SQLString = string.Format(SQLString, newid, Convert.ToInt32(gviewDuplicaSoluzioniMAT.GetRowCellValue(c, "IDSoluzioneDetail")));

                            cmd.CommandText = SQLString;
                            cmd.Connection = cnn;
                            int newiddetail = 0;
                            newiddetail = Convert.ToInt32(cmd.ExecuteScalar());

                            ControlMaterialeSelezionato = null;
                            ModelMaterialeSelezionato = null;
                            
                            Core.Model.Model_Soluzioni_Details ModelSoluzioneDetail = new Core.Model.Model_Soluzioni_Details();
                            Core.Control.Control_Soluzioni_Details ControlSoluzioneDetail = new Core.Control.Control_Soluzioni_Details();
                            ControlSoluzioneDetail.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                            ControlSoluzioneDetail.IDUtente = IDUtente;
                            ModelSoluzioneDetail = ControlSoluzioneDetail.GetByIDSolutionDetail(newiddetail).First().Value;
                            ControlSoluzioneDetail.AddLogSoluzioneDettaglio("Inserimento", ModelSoluzioneDetail, ModelSoluzioneDetail.DataScadenza.ToString());
                            ControlSoluzioneDetail = null;
                            ModelSoluzioneDetail = null;

                            ControlMaterialeSelezionato = null;
                            ModelMaterialeSelezionato = null;
                        }

                        for (int c = 0; c < gviewDuplicaSoluzioniSOL.RowCount; c++)
                        {
                            EOS.Core.Model.Model_Soluzioni ModelSoluzioneSelezionata = new EOS.Core.Model.Model_Soluzioni();
                            EOS.Core.Control.Controller_Soluzioni ControlSoluzioneSelezionata = new EOS.Core.Control.Controller_Soluzioni();
                            ControlSoluzioneSelezionata.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                            ModelSoluzioneSelezionata = ControlSoluzioneSelezionata.GetSolutionByID(Convert.ToInt32(gviewDuplicaSoluzioniSOL.GetRowCellValue(c, "IDSoluzioneSelezionata"))).First().Value;

                            if (gviewDuplicaSoluzioniSOL.GetRowCellValue(c, "NoteScaduto") != null)
                            {
                                nota = gviewDuplicaSoluzioniSOL.GetRowCellValue(c, "NoteScaduto").ToString();
                            }
                            else
                            {
                                nota = "";
                            }

                            SQLString = "";
                            SQLString = SQLString + "INSERT INTO Soluzioni_Details ";
                            SQLString = SQLString + "SELECT ";
                            SQLString = SQLString + "{0} as IDSoluzioneMaster ";
                            SQLString = SQLString + ",[Tipologia_MR] ";
                            SQLString = SQLString + ",[IDSchedaDocumenti] ";
                            SQLString = SQLString + ",[CAS] ";
                            SQLString = SQLString + "," + ModelSoluzioneSelezionata.IDSoluzione.ToString() + " as IDSoluzione ";
                            SQLString = SQLString + ",[UM_Prelievo] ";
                            SQLString = SQLString + ",[Quantita_Prelievo] ";
                            //SQLString = SQLString + ",[IDApparecchio] ";
                            //SQLString = SQLString + ",[IDUtensile] ";

                            if (gviewDuplicaSoluzioniSOL.GetRowCellValue(c, "IDApparecchio").Equals(System.DBNull.Value))
                            {
                                SQLString = SQLString + ",0 ";
                            }
                            else
                            {
                                SQLString = SQLString + "," + Convert.ToInt32(gviewDuplicaSoluzioniSOL.GetRowCellValue(c, "IDApparecchio")) + " ";
                            }

                            if (gviewDuplicaSoluzioniSOL.GetRowCellValue(c, "IDUtensile").Equals(System.DBNull.Value))
                            {
                                SQLString = SQLString + ",0 ";
                            }
                            else
                            {
                                SQLString = SQLString + "," + Convert.ToInt32(gviewDuplicaSoluzioniSOL.GetRowCellValue(c, "IDUtensile")) + " ";
                            }

                            SQLString = SQLString + ",Note + '" + Environment.NewLine + nota + "' as [Note] ";
                            SQLString = SQLString + ",[Concentrazione] ";
                            //SQLString = SQLString + ",case when '" + ModelSoluzioneSelezionata.DataScadenza.ToString() + "' <> '01/01/0001 00:00:00' then left(right('" + ModelSoluzioneSelezionata.DataScadenza.ToString() + "',13),4) + '/' + right(left('" + ModelSoluzioneSelezionata.DataScadenza.ToString() + "',5),2) + '/' + left('" + ModelSoluzioneSelezionata.DataScadenza.ToString() + "',2) else null end as DataScadenza ";
                            SQLString = SQLString + ",case when '" + ModelSoluzioneSelezionata.DataScadenza.ToString() + "' <> '01/01/0001 00:00:00' then '" + ModelSoluzioneSelezionata.DataScadenza.ToString() + "' else null end as DataScadenza ";
                            //SQLString = SQLString + ",[IDApparecchio2] ";
                            //SQLString = SQLString + ",[IDUtensile2] ";

                            if (gviewDuplicaSoluzioniSOL.GetRowCellValue(c, "IDApparecchio2").Equals(System.DBNull.Value))
                            {
                                SQLString = SQLString + ",0 ";
                            }
                            else
                            {
                                SQLString = SQLString + "," + Convert.ToInt32(gviewDuplicaSoluzioniSOL.GetRowCellValue(c, "IDApparecchio2")) + " ";
                            }

                            if (gviewDuplicaSoluzioniSOL.GetRowCellValue(c, "IDUtensile2").Equals(System.DBNull.Value))
                            {
                                SQLString = SQLString + ",0 ";
                            }
                            else
                            {
                                SQLString = SQLString + "," + Convert.ToInt32(gviewDuplicaSoluzioniSOL.GetRowCellValue(c, "IDUtensile2")) + " ";
                            }

                            SQLString = SQLString + "FROM [EOS].[dbo].[Soluzioni_Details] ";
                            SQLString = SQLString + "WHERE IDSoluzioneDetail={1}; SELECT SCOPE_IDENTITY() ";
                            SQLString = string.Format(SQLString, newid, Convert.ToInt32(gviewDuplicaSoluzioniSOL.GetRowCellValue(c, "IDSoluzioneDetail")));

                            cmd.CommandText = SQLString;
                            cmd.Connection = cnn;
                            int newiddetail = 0;
                            newiddetail = Convert.ToInt32(cmd.ExecuteScalar());

                            ControlSoluzioneSelezionata = null;
                            ModelSoluzioneSelezionata = null;

                            Core.Model.Model_Soluzioni_Details ModelSoluzioneDetail = new Core.Model.Model_Soluzioni_Details();
                            Core.Control.Control_Soluzioni_Details ControlSoluzioneDetail = new Core.Control.Control_Soluzioni_Details();
                            ControlSoluzioneDetail.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                            ControlSoluzioneDetail.IDUtente = IDUtente;
                            ModelSoluzioneDetail = ControlSoluzioneDetail.GetByIDSolutionDetail(newiddetail).First().Value;
                            ControlSoluzioneDetail.AddLogSoluzioneDettaglio("Inserimento", ModelSoluzioneDetail, ModelSoluzioneDetail.DataScadenza.ToString());
                            ControlSoluzioneDetail = null;
                            ModelSoluzioneDetail = null;

                            ControlSoluzioneSelezionata = null;
                            ModelSoluzioneSelezionata = null;
                        }

                        cmd = null;
                        cnn = null;

                        EOS.Core.Control.Controller_Soluzioni ControllerSoluzioni = new Core.Control.Controller_Soluzioni();
                        ControllerSoluzioni.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                        ControllerSoluzioni.IDUtente = IDUtente;
                        EOS.Core.Model.Model_Soluzioni ModelSoluzioni = new Core.Model.Model_Soluzioni();
                        ModelSoluzioni = ControllerSoluzioni.GetSolutionByID(newid).First().Value;
                        ControllerSoluzioni.UpdateSolution(ModelSoluzioni,0);

                        ModelSoluzioni = null;
                        ControllerSoluzioni = null;

                        newIDElemento = newid;

                        XtraMessageBox.Show("Soluzione MR creata da Ricetta correttamente,", "Crea da ricetta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();

                    }
                    catch (Exception a)
                    {

                    }
                }
                else
                {
                    int newid;
                    object newcode = "";

                    try
                    {
                        System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                        cnn.ConnectionString = ConnectionString;
                        cnn.Open();

                        SqlCommand cmdstore = new SqlCommand();
                        cmdstore.Connection = cnn;
                        cmdstore.CommandType = CommandType.StoredProcedure;
                        cmdstore.CommandText = "dbo.GetProgressivo";

                        cmdstore.Parameters.Add("@TIPOPROGRESSIVO", SqlDbType.VarChar, 50).Value = "MIS";

                        newcode = cmdstore.ExecuteScalar();

                        cmdstore = null;

                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                        string SQLString = "";
                        SQLString = SQLString + "INSERT INTO [Solventi] ";
                        SQLString = SQLString + "SELECT ";
                        SQLString = SQLString + "[Nome] as Nome ";
                        SQLString = SQLString + ",[NotePrescrittive] ";
                        SQLString = SQLString + ",[NoteDescrittive] ";
                        SQLString = SQLString + ",[IDUbicazione] ";
                        SQLString = SQLString + ",[DefaultGiorniScadenza] ";
                        SQLString = SQLString + ",getdate() as DataPreparazione ";
                        SQLString = SQLString + ",NULL as DataScadenza ";
                        SQLString = SQLString + ",getdate() as DataCreazione ";
                        SQLString = SQLString + "," + IDUtente.ToString() + " as [IDUtente] ";
                        SQLString = SQLString + ",5 as IDStato ";
                        SQLString = SQLString + ",'" + cboTipologiaSoluzioniDiLavoro.EditValue.ToString() + "' as [Tipologia] ";
                        SQLString = SQLString + ",'" + newcode.ToString() + "' as CodiceSolvente ";
                        SQLString = SQLString + "FROM [EOS].[dbo].[Solventi] ";
                        SQLString = SQLString + "WHERE IDSolvente = {0}; SELECT SCOPE_IDENTITY() ";
                        SQLString = string.Format(SQLString, IDElemento);

                        cmd.CommandText = SQLString;
                        cmd.Connection = cnn;
                        newid = Convert.ToInt32(cmd.ExecuteScalar());

                        Core.Model.Model_Solventi ModelSolVente = new Core.Model.Model_Solventi();
                        Core.Control.Control_Solventi ControlSolVente = new Core.Control.Control_Solventi();
                        ControlSolVente.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                        ControlSolVente.IDUtente = IDUtente;
                        ModelSolVente = ControlSolVente.GetSolventeByID(newid).First().Value;
                        ControlSolVente.AddLogSolVente("Inserimento", ModelSolVente, ModelSolVente.DataPreparazione.ToString(), ModelSolVente.DataScadenza.ToString(), ModelSolVente.DataCreazione.ToString());
                        ControlSolVente = null;
                        ModelSolVente = null;

                        for (int c = 0; c < gviewDuplicaSolventiMAT.RowCount; c++)
                        {
                            EOS.Core.Model.Model_MaterialiMR ModelMaterialeSelezionato = new EOS.Core.Model.Model_MaterialiMR();
                            EOS.Core.Control.Control_MaterialiMR ControlMaterialeSelezionato = new EOS.Core.Control.Control_MaterialiMR();
                            ControlMaterialeSelezionato.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                            ModelMaterialeSelezionato = ControlMaterialeSelezionato.GetByIDSchedaDocumenti(Convert.ToInt32(gviewDuplicaSolventiMAT.GetRowCellValue(c, "IDSchedaDocumentiSelezionata"))).First().Value;

                            if (gviewDuplicaSolventiMAT.GetRowCellValue(c, "NoteScaduto") != null)
                            {
                                nota = gviewDuplicaSolventiMAT.GetRowCellValue(c, "NoteScaduto").ToString();
                            }
                            else
                            {
                                nota = "";
                            }

                            SQLString = "";
                            SQLString = SQLString + "INSERT INTO Solventi_Details ";
                            SQLString = SQLString + "SELECT ";
                            SQLString = SQLString + "{0} as IDSolventeMaster";
                            SQLString = SQLString + "," + ModelMaterialeSelezionato.IDSchedaDocumenti + " as IDSchedaDocumenti ";
                            SQLString = SQLString + ",[IDSolvente] ";
                            SQLString = SQLString + ",[UM_Prelievo] ";
                            SQLString = SQLString + ",[Quantita_Prelievo] ";
                            //SQLString = SQLString + ",[IDApparecchio] ";
                            //SQLString = SQLString + ",[IDUtensile] ";

                            if (gviewDuplicaSolventiMAT.GetRowCellValue(c, "IDApparecchio").Equals(System.DBNull.Value))
                            {
                                SQLString = SQLString + ",0 ";
                            }
                            else
                            {
                                SQLString = SQLString + "," + Convert.ToInt32(gviewDuplicaSolventiMAT.GetRowCellValue(c, "IDApparecchio")) + " ";
                            }

                            if (gviewDuplicaSolventiMAT.GetRowCellValue(c, "IDUtensile").Equals(System.DBNull.Value))
                            {
                                SQLString = SQLString + ",0 ";
                            }
                            else
                            {
                                SQLString = SQLString + "," + Convert.ToInt32(gviewDuplicaSolventiMAT.GetRowCellValue(c, "IDUtensile")) + " ";
                            }

                            SQLString = SQLString + ",[Tipologia_MR] ";
                            SQLString = SQLString + ",[CAS] ";
                            SQLString = SQLString + ",Note + '" + Environment.NewLine + nota + "' as [Note] ";
                            //SQLString = SQLString + ",left(right('" + ModelMaterialeSelezionato.DataScadenza_Lotto.ToString() + "',13),4) + '/' + right(left('" + ModelMaterialeSelezionato.DataScadenza_Lotto.ToString() + "',5),2) + '/' + left('" + ModelMaterialeSelezionato.DataScadenza_Lotto.ToString() + "',2) as DataScadenza ";
                            SQLString = SQLString + ",case when '" + ModelMaterialeSelezionato.DataScadenza_Lotto.ToString() + "' <> '01/01/0001 00:00:00' then '" + ModelMaterialeSelezionato.DataScadenza_Lotto.ToString() + "' else null end as DataScadenza ";
                            //SQLString = SQLString + ",[IDApparecchio2] ";
                            //SQLString = SQLString + ",[IDUtensile2] ";

                            if (gviewDuplicaSolventiMAT.GetRowCellValue(c, "IDApparecchio2").Equals(System.DBNull.Value))
                            {
                                SQLString = SQLString + ",0 ";
                            }
                            else
                            {
                                SQLString = SQLString + "," + Convert.ToInt32(gviewDuplicaSolventiMAT.GetRowCellValue(c, "IDApparecchio2")) + " ";
                            }

                            if (gviewDuplicaSolventiMAT.GetRowCellValue(c, "IDUtensile2").Equals(System.DBNull.Value))
                            {
                                SQLString = SQLString + ",0 ";
                            }
                            else
                            {

                                SQLString = SQLString + "," + Convert.ToInt32(gviewDuplicaSolventiMAT.GetRowCellValue(c, "IDUtensile2")) + " ";
                            }

                            SQLString = SQLString + "FROM [EOS].[dbo].[Solventi_Details] ";
                            SQLString = SQLString + "WHERE IDSolventeDetail={1}; SELECT SCOPE_IDENTITY() ";
                            SQLString = string.Format(SQLString, newid, Convert.ToInt32(gviewDuplicaSolventiMAT.GetRowCellValue(c, "IDSolventeDetail")));

                            cmd.CommandText = SQLString;
                            cmd.Connection = cnn;
                            int newiddetail = 0;
                            newiddetail = Convert.ToInt32(cmd.ExecuteScalar());

                            Core.Model.Model_Solventi_Details ModelSolventeDetail = new Core.Model.Model_Solventi_Details();
                            Core.Control.Control_Solventi_Details ControlSolventeDetail = new Core.Control.Control_Solventi_Details();
                            ControlSolventeDetail.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                            ControlSolventeDetail.IDUtente = IDUtente;
                            ModelSolventeDetail = ControlSolventeDetail.GetByIDSolventeDetail(newiddetail).First().Value;
                            ControlSolventeDetail.AddLogSolventeDettaglio("Inserimento", ModelSolventeDetail, ModelSolventeDetail.DataScadenza.ToString());
                            ControlSolventeDetail = null;
                            ModelSolventeDetail = null;

                            ControlMaterialeSelezionato = null;
                            ModelMaterialeSelezionato = null;
                        }

                        for (int c = 0; c < gviewDuplicaSolventiSOV.RowCount; c++)
                        {
                            EOS.Core.Model.Model_Solventi ModelSolventeSelezionato = new EOS.Core.Model.Model_Solventi();
                            EOS.Core.Control.Control_Solventi ControlSolventeSelezionato = new EOS.Core.Control.Control_Solventi();
                            ControlSolventeSelezionato.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                            ModelSolventeSelezionato = ControlSolventeSelezionato.GetSolventeByID(Convert.ToInt32(gviewDuplicaSolventiSOV.GetRowCellValue(c, "IDSolventeSelezionato"))).First().Value;

                            if (gviewDuplicaSolventiSOV.GetRowCellValue(c, "NoteScaduto") != null)
                            {
                                nota = gviewDuplicaSolventiSOV.GetRowCellValue(c, "NoteScaduto").ToString();
                            }
                            else
                            {
                                nota = "";
                            }

                            SQLString = "";
                            SQLString = SQLString + "INSERT INTO Solventi_Details ";
                            SQLString = SQLString + "SELECT ";
                            SQLString = SQLString + "{0} as IDSolventeMaster ";
                            SQLString = SQLString + ",[IDSchedaDocumenti] ";
                            SQLString = SQLString + "," + ModelSolventeSelezionato.IDSolvente.ToString() + " as IDSolvente ";
                            SQLString = SQLString + ",[UM_Prelievo] ";
                            SQLString = SQLString + ",[Quantita_Prelievo] ";
                            //SQLString = SQLString + ",[IDApparecchio] ";
                            //SQLString = SQLString + ",[IDUtensile] ";

                            if (gviewDuplicaSolventiSOV.GetRowCellValue(c, "IDApparecchio").Equals(System.DBNull.Value))
                            {
                                SQLString = SQLString + ",0 ";
                            }
                            else
                            {
                                SQLString = SQLString + "," + Convert.ToInt32(gviewDuplicaSolventiSOV.GetRowCellValue(c, "IDApparecchio")) + " ";
                            }

                            if (gviewDuplicaSolventiSOV.GetRowCellValue(c, "IDUtensile").Equals(System.DBNull.Value))
                            {
                                SQLString = SQLString + ",0 ";
                            }
                            else
                            {
                                SQLString = SQLString + "," + Convert.ToInt32(gviewDuplicaSolventiSOV.GetRowCellValue(c, "IDUtensile")) + " ";
                            }

                            SQLString = SQLString + ",[Tipologia_MR] ";
                            SQLString = SQLString + ",[CAS] ";
                            SQLString = SQLString + ",Note + '" + Environment.NewLine + nota + "' as [Note] ";
                            //SQLString = SQLString + ",case when '" + ModelSolventeSelezionato.DataScadenza.ToString() + "' <> '01/01/0001 00:00:00' then left(right('" + ModelSolventeSelezionato.DataScadenza.ToString() + "',13),4) + '/' + right(left('" + ModelSolventeSelezionato.DataScadenza.ToString() + "',5),2) + '/' + left('" + ModelSolventeSelezionato.DataScadenza.ToString() + "',2) else null end as DataScadenza ";
                            SQLString = SQLString + ",case when '" + ModelSolventeSelezionato.DataScadenza.ToString() + "' <> '01/01/0001 00:00:00' then '" + ModelSolventeSelezionato.DataScadenza.ToString() + "' else null end as DataScadenza ";
                            //SQLString = SQLString + ",[IDApparecchio2] ";
                            //SQLString = SQLString + ",[IDUtensile2] ";

                            if (gviewDuplicaSolventiSOV.GetRowCellValue(c, "IDApparecchio2").Equals(System.DBNull.Value))
                            {
                                SQLString = SQLString + ",0 ";
                            }
                            else
                            {
                                SQLString = SQLString + "," + Convert.ToInt32(gviewDuplicaSolventiSOV.GetRowCellValue(c, "IDApparecchio2")) + " ";
                            }

                            if (gviewDuplicaSolventiSOV.GetRowCellValue(c, "IDUtensile2").Equals(System.DBNull.Value))
                            {
                                SQLString = SQLString + ",0 ";
                            }
                            else
                            {
                                SQLString = SQLString + "," + Convert.ToInt32(gviewDuplicaSolventiSOV.GetRowCellValue(c, "IDUtensile2")) + " ";
                            }

                            SQLString = SQLString + "FROM [EOS].[dbo].[Solventi_Details] ";
                            SQLString = SQLString + "WHERE IDSolventeDetail={1}; SELECT SCOPE_IDENTITY() ";
                            SQLString = string.Format(SQLString, newid, Convert.ToInt32(gviewDuplicaSolventiSOV.GetRowCellValue(c, "IDSolventeDetail")));

                            cmd.CommandText = SQLString;
                            cmd.Connection = cnn;
                            int newiddetail = 0;
                            newiddetail = Convert.ToInt32(cmd.ExecuteScalar());

                            Core.Model.Model_Solventi_Details ModelSolventeDetail = new Core.Model.Model_Solventi_Details();
                            Core.Control.Control_Solventi_Details ControlSolventeDetail = new Core.Control.Control_Solventi_Details();
                            ControlSolventeDetail.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                            ControlSolventeDetail.IDUtente = IDUtente;
                            ModelSolventeDetail = ControlSolventeDetail.GetByIDSolventeDetail(newiddetail).First().Value;
                            ControlSolventeDetail.AddLogSolventeDettaglio("Inserimento", ModelSolventeDetail, ModelSolventeDetail.DataScadenza.ToString());
                            ControlSolventeDetail = null;
                            ModelSolventeDetail = null;

                            ControlSolventeSelezionato = null;
                            ModelSolventeSelezionato = null;
                        }

                        cmd = null;
                        cnn = null;

                        EOS.Core.Control.Control_Solventi ControlSolventi = new Core.Control.Control_Solventi();
                        ControlSolventi.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                        ControlSolventi.IDUtente = IDUtente;
                        EOS.Core.Model.Model_Solventi ModelSolventi = new Core.Model.Model_Solventi();
                        ModelSolventi = ControlSolventi.GetSolventeByID(newid).First().Value;
                        ControlSolventi.UpdateSolvente(ModelSolventi,0);

                        ModelSolventi = null;
                        ControlSolventi = null;

                        newIDElemento = newid;

                        XtraMessageBox.Show("Soluzione di Lavoro creata da Ricetta correttamente,", "Crea da ricetta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                    catch (Exception a)
                    {

                    }
                }

            }
        }

        private void SolventeSingolo(object sender, EventArgs e)
        {
            if (!changingSingoloSolvente)
            {
                removecontrolhandler();
                changingSingoloSolvente = true;
                cboSoluzioneDiLavoro.EditValue = null;
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

        private void SolventeApparecchio(object sender, EventArgs e)
        {
            if (!changingApparecchio)
            {
                removecontrolhandler();
                changingApparecchio = true;
                cboSolventeUtensile.EditValue = null;
                addcontrolhandler();
                changingApparecchio = false;
            }
        }

        private void SolventeUtensile(object sender, EventArgs e)
        {
            if (!changingUtensile)
            {
                removecontrolhandler();
                changingUtensile = true;
                cboSolventeApparecchio.EditValue = null;
                addcontrolhandler();
                changingUtensile = false;
            }
        }

        private void SolventeApparecchio2(object sender, EventArgs e)
        {
            if (!changingApparecchio2)
            {
                removecontrolhandler();
                changingApparecchio2 = true;
                cboSolventeUtensile2.EditValue = null;
                addcontrolhandler();
                changingApparecchio2 = false;
            }
        }

        private void SolventeUtensile2(object sender, EventArgs e)
        {
            if (!changingUtensile2)
            {
                removecontrolhandler();
                changingUtensile2 = true;
                cboSolventeApparecchio2.EditValue = null;
                addcontrolhandler();
                changingUtensile2 = false;
            }
        }

        private void addcontrolhandler()
        {
            this.cboSingoloSolvente.EditValueChanged += new System.EventHandler(this.SolventeSingolo);
            this.cboSoluzioneDiLavoro.EditValueChanged += new System.EventHandler(this.SolventeMiscela);
            this.cboSolventeApparecchio.EditValueChanged += new System.EventHandler(this.SolventeApparecchio);
            this.cboSolventeUtensile.EditValueChanged += new System.EventHandler(this.SolventeUtensile);
            this.cboSolventeApparecchio2.EditValueChanged += new System.EventHandler(this.SolventeApparecchio2);
            this.cboSolventeUtensile2.EditValueChanged += new System.EventHandler(this.SolventeUtensile2);
        }

        private void removecontrolhandler()
        {
            this.cboSingoloSolvente.EditValueChanged -= new System.EventHandler(this.SolventeSingolo);
            this.cboSoluzioneDiLavoro.EditValueChanged -= new System.EventHandler(this.SolventeMiscela);
            this.cboSolventeApparecchio.EditValueChanged -= new System.EventHandler(this.SolventeApparecchio);
            this.cboSolventeUtensile.EditValueChanged -= new System.EventHandler(this.SolventeUtensile);
            this.cboSolventeApparecchio2.EditValueChanged -= new System.EventHandler(this.SolventeApparecchio2);
            this.cboSolventeUtensile2.EditValueChanged -= new System.EventHandler(this.SolventeUtensile2);
        }

        private int UtilizzoScaduti(DateTime DataScadenza)
        {
            int Ret = 0;

            if (DataScadenza <= DateTime.Now)
            {
                XtraInputBoxArgs args = new XtraInputBoxArgs();
                //args.Caption = "Autorizzazione utilizzo materiale MR o Soluzione scaduta";
                args.Caption = "Autorizzazione utilizzo materiale MR o Soluzione MR scaduta";
                args.Prompt = "Inserire password di autorizzazione";
                args.DefaultButtonIndex = 0;
                TextEdit editor = new TextEdit();
                editor.Properties.UseSystemPasswordChar = true;
                args.Editor = editor;
                var result = XtraInputBox.Show(args);

                if (result != null)
                {
                    EOS.Core.Control.Control_Configurazione ControlConfigurazione = new EOS.Core.Control.Control_Configurazione();
                    EOS.Core.Model.Model_Configurazione ModelConfigurazione = new EOS.Core.Model.Model_Configurazione();

                    ControlConfigurazione.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                    ModelConfigurazione = ControlConfigurazione.GetActiveConfiguration().First().Value;

                    if (result.ToString() == ModelConfigurazione.AuthorizationPassword)
                    {
                        XtraInputBoxArgs argsnote = new XtraInputBoxArgs();
                        argsnote.Caption = "Nota di autorizzazione utilizzo materiale scaduto";
                        argsnote.Prompt = "Inserire la nota";
                        argsnote.DefaultButtonIndex = 0;
                        MemoEdit editornote = new MemoEdit();
                        args.Editor = editor;
                        var resultnote = XtraInputBox.Show(argsnote);

                        if (resultnote != null)
                        {
                            if (resultnote.ToString() != "")
                            {
                                NotaUtilizzoScaduto = resultnote.ToString();
                                Ret = 2;
                            }
                            else
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("Non è possibile utilizzare il materiale scaduto senza specificare una nota", "Utilizzo Materiale Scaduto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                Ret = 1;
                            }
                        }
                        else
                        {
                            Ret = 1;
                        }
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("Password di autorizzazione non corretta", "Utilizzo Materiale Scaduto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Ret = 1;
                    }

                    ControlConfigurazione = null;
                    ModelConfigurazione = null;
                }
                else
                {
                    Ret = 1;
                }
            }
            else
            {
                Ret = 0;
            }

            return Ret;
        }

        private void cboSceltaMaterialeSOLChanged(object sender, EventArgs e)
        {
            int IDSchedaDocumenti = 0;
            int CheckDataScadenza = 0;

            GridLookUpEdit editor = (GridLookUpEdit)sender;

            IDSchedaDocumenti = Convert.ToInt32(editor.EditValue);

            EOS.Core.Model.Model_MaterialiMR ModelMaterialeSelezionato = new EOS.Core.Model.Model_MaterialiMR();
            EOS.Core.Control.Control_MaterialiMR ControlMaterialeSelezionato = new EOS.Core.Control.Control_MaterialiMR();
            ControlMaterialeSelezionato.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
            ModelMaterialeSelezionato = ControlMaterialeSelezionato.GetByIDSchedaDocumenti(IDSchedaDocumenti).First().Value;

            if (CheckDate(ModelMaterialeSelezionato.DataScadenza_Lotto))
            {
                CheckDataScadenza = UtilizzoScaduti(Convert.ToDateTime(ModelMaterialeSelezionato.DataScadenza_Lotto));
                if (CheckDataScadenza == 0)
                {
                    NotaUtilizzoScaduto = "";
                }
                else
                {
                    if (CheckDataScadenza == 1)
                    {
                        NotaUtilizzoScaduto = "";
                        editor.EditValue = null;
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                NotaUtilizzoScaduto = "";
            }

            if ((CheckDataScadenza == 0) || (CheckDataScadenza == 2))
            {
                int[] rowHandles = gviewDuplicaSoluzioniMAT.GetSelectedRows();

                for (int c = 0; c < gviewDuplicaSoluzioniMAT.SelectedRowsCount; c++)
                {
                    gviewDuplicaSoluzioniMAT.SetRowCellValue(rowHandles[c], "NoteScaduto", NotaUtilizzoScaduto);
                }
            }

            ControlMaterialeSelezionato = null;
            ModelMaterialeSelezionato = null;
        }

        private void cboSceltaSoluzioneSOLChanged(object sender, EventArgs e)
        {
            int IDSoluzione = 0;
            int CheckDataScadenza = 0;

            GridLookUpEdit editor = (GridLookUpEdit)sender;

            IDSoluzione = Convert.ToInt32(editor.EditValue);

            EOS.Core.Model.Model_Soluzioni ModelSoluzioneSelezionata = new EOS.Core.Model.Model_Soluzioni();
            EOS.Core.Control.Controller_Soluzioni ControlSoluzioneSelezionata = new EOS.Core.Control.Controller_Soluzioni();
            ControlSoluzioneSelezionata.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
            ModelSoluzioneSelezionata = ControlSoluzioneSelezionata.GetSolutionByID(IDSoluzione).First().Value;

            if (ModelSoluzioneSelezionata.DataScadenza != null)
            {
                CheckDataScadenza = UtilizzoScaduti(Convert.ToDateTime(ModelSoluzioneSelezionata.DataScadenza));
                if (CheckDataScadenza == 0)
                {
                    NotaUtilizzoScaduto = "";
                }
                else
                {
                    if (CheckDataScadenza == 1)
                    {
                        NotaUtilizzoScaduto = "";
                        editor.EditValue = null;
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                NotaUtilizzoScaduto = "";
            }

            if ((CheckDataScadenza == 0) || (CheckDataScadenza == 2))
            {
                int[] rowHandles = gviewDuplicaSoluzioniSOL.GetSelectedRows();

                for (int c = 0; c < gviewDuplicaSoluzioniSOL.SelectedRowsCount; c++)
                {
                    gviewDuplicaSoluzioniSOL.SetRowCellValue(rowHandles[c], "NoteScaduto", NotaUtilizzoScaduto);
                }
            }

            ControlSoluzioneSelezionata = null;
            ModelSoluzioneSelezionata = null;
        }

        private void cboSceltaMaterialeSOVChanged(object sender, EventArgs e)
        {
            int IDSchedaDocumenti = 0;
            int CheckDataScadenza = 0;

            GridLookUpEdit editor = (GridLookUpEdit)sender;

            IDSchedaDocumenti = Convert.ToInt32(editor.EditValue);

            EOS.Core.Model.Model_MaterialiMR ModelMaterialeSelezionato = new EOS.Core.Model.Model_MaterialiMR();
            EOS.Core.Control.Control_MaterialiMR ControlMaterialeSelezionato = new EOS.Core.Control.Control_MaterialiMR();
            ControlMaterialeSelezionato.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
            ModelMaterialeSelezionato = ControlMaterialeSelezionato.GetByIDSchedaDocumenti(IDSchedaDocumenti).First().Value;

            if (CheckDate(ModelMaterialeSelezionato.DataScadenza_Lotto))
            {
                CheckDataScadenza = UtilizzoScaduti(Convert.ToDateTime(ModelMaterialeSelezionato.DataScadenza_Lotto));
                if (CheckDataScadenza == 0)
                {
                    NotaUtilizzoScaduto = "";
                }
                else
                {
                    if (CheckDataScadenza == 1)
                    {
                        NotaUtilizzoScaduto = "";
                        editor.EditValue = null;
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                NotaUtilizzoScaduto = "";
            }

            if ((CheckDataScadenza == 0) || (CheckDataScadenza == 2))
            {
                int[] rowHandles = gviewDuplicaSolventiMAT.GetSelectedRows();

                for (int c = 0; c < gviewDuplicaSolventiMAT.SelectedRowsCount; c++)
                {
                    gviewDuplicaSolventiMAT.SetRowCellValue(rowHandles[c], "NoteScaduto", NotaUtilizzoScaduto);
                }
            }

            ControlMaterialeSelezionato = null;
            ModelMaterialeSelezionato = null;
        }

        private void cboSceltaSoluzioneSOVChanged(object sender, EventArgs e)
        {
            int IDSolvente = 0;
            int CheckDataScadenza = 0;

            GridLookUpEdit editor = (GridLookUpEdit)sender;

            IDSolvente = Convert.ToInt32(editor.EditValue);

            EOS.Core.Model.Model_Solventi ModelSolventeSelezionato = new EOS.Core.Model.Model_Solventi();
            EOS.Core.Control.Control_Solventi ControlSolventeSelezionato = new EOS.Core.Control.Control_Solventi();
            ControlSolventeSelezionato.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
            ModelSolventeSelezionato = ControlSolventeSelezionato.GetSolventeByID(IDSolvente).First().Value;

            if (ModelSolventeSelezionato.DataScadenza != null)
            {
                CheckDataScadenza = UtilizzoScaduti(Convert.ToDateTime(ModelSolventeSelezionato.DataScadenza));
                if (CheckDataScadenza == 0)
                {
                    NotaUtilizzoScaduto = "";
                }
                else
                {
                    if (CheckDataScadenza == 1)
                    {
                        NotaUtilizzoScaduto = "";
                        editor.EditValue = null;
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                NotaUtilizzoScaduto = "";
            }

            if ((CheckDataScadenza == 0) || (CheckDataScadenza == 2))
            {
                int[] rowHandles = gviewDuplicaSolventiSOV.GetSelectedRows();

                for (int c = 0; c < gviewDuplicaSolventiSOV.SelectedRowsCount; c++)
                {
                    gviewDuplicaSolventiSOV.SetRowCellValue(rowHandles[c], "NoteScaduto", NotaUtilizzoScaduto);
                }
            }

            ControlSolventeSelezionato = null;
            ModelSolventeSelezionato = null;
        }

        protected bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void frmDuplica_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (TipoElemento == "Soluzione")
            {
                System.IO.MemoryStream str = new System.IO.MemoryStream();
                gridDuplicaSoluzioniMAT.FocusedView.SaveLayoutToStream(str);
                str.Seek(0, System.IO.SeekOrigin.Begin);
                frmLogin.SaveUserPreferences("frmDuplica", "gridDuplicaSoluzioniMAT", str, IDUtente);

                str = new System.IO.MemoryStream();
                gridDuplicaSoluzioniSOL.FocusedView.SaveLayoutToStream(str);
                str.Seek(0, System.IO.SeekOrigin.Begin);
                frmLogin.SaveUserPreferences("frmDuplica", "gridDuplicaSoluzioniSOL", str, IDUtente);

                str = null;
            }
            else
            {
                System.IO.MemoryStream str = new System.IO.MemoryStream();
                gridDuplicaSolventiMAT.FocusedView.SaveLayoutToStream(str);
                str.Seek(0, System.IO.SeekOrigin.Begin);
                frmLogin.SaveUserPreferences("frmDuplica", "gridDuplicaSolventiMAT", str, IDUtente);

                str = new System.IO.MemoryStream();
                gridDuplicaSolventiSOV.FocusedView.SaveLayoutToStream(str);
                str.Seek(0, System.IO.SeekOrigin.Begin);
                frmLogin.SaveUserPreferences("frmDuplica", "gridDuplicaSolventiSOV", str, IDUtente);

                str = null;
            }
        }

        private void addEventHandler()
        {
            if (TipoElemento == "Soluzione")
            {
                this.cboSoluzioniSelezionaMAT_APP.EditValueChanged += new System.EventHandler(this.cboSoluzioniSelezionaMAT_APP_EditValueChanged);
                this.cboSoluzioniSelezionaMAT_UTE.EditValueChanged += new System.EventHandler(this.cboSoluzioniSelezionaMAT_UTE_EditValueChanged);
                this.cboSoluzioniSelezionaMAT_APP2.EditValueChanged += new System.EventHandler(this.cboSoluzioniSelezionaMAT_APP2_EditValueChanged);
                this.cboSoluzioniSelezionaMAT_UTE2.EditValueChanged += new System.EventHandler(this.cboSoluzioniSelezionaMAT_UTE2_EditValueChanged);

                this.cboSoluzioniSelezionaSOL_APP.EditValueChanged += new System.EventHandler(this.cboSoluzioniSelezionaSOL_APP_EditValueChanged);
                this.cboSoluzioniSelezionaSOL_UTE.EditValueChanged += new System.EventHandler(this.cboSoluzioniSelezionaSOL_UTE_EditValueChanged);
                this.cboSoluzioniSelezionaSOL_APP2.EditValueChanged += new System.EventHandler(this.cboSoluzioniSelezionaSOL_APP2_EditValueChanged);
                this.cboSoluzioniSelezionaSOL_UTE2.EditValueChanged += new System.EventHandler(this.cboSoluzioniSelezionaSOL_UTE2_EditValueChanged);
            }
            else
            {
                this.cboSolventiSelezionaMAT_APP.EditValueChanged += new System.EventHandler(this.cboSolventiSelezionaMAT_APP_EditValueChanged);
                this.cboSolventiSelezionaMAT_UTE.EditValueChanged += new System.EventHandler(this.cboSolventiSelezionaMAT_UTE_EditValueChanged);
                this.cboSolventiSelezionaMAT_APP2.EditValueChanged += new System.EventHandler(this.cboSolventiSelezionaMAT_APP2_EditValueChanged);
                this.cboSolventiSelezionaMAT_UTE2.EditValueChanged += new System.EventHandler(this.cboSolventiSelezionaMAT_UTE2_EditValueChanged);

                this.cboSolventiSelezionaSOV_APP.EditValueChanged += new System.EventHandler(this.cboSolventiSelezionaSOV_APP_EditValueChanged);
                this.cboSolventiSelezionaSOV_UTE.EditValueChanged += new System.EventHandler(this.cboSolventiSelezionaSOV_UTE_EditValueChanged);
                this.cboSolventiSelezionaSOV_APP2.EditValueChanged += new System.EventHandler(this.cboSolventiSelezionaSOV_APP2_EditValueChanged);
                this.cboSolventiSelezionaSOV_UTE2.EditValueChanged += new System.EventHandler(this.cboSolventiSelezionaSOV_UTE2_EditValueChanged);
            }
        }

        private void removeEventHandler()
        {
            if (TipoElemento == "Soluzione")
            {
                this.cboSoluzioniSelezionaMAT_APP.EditValueChanged -= new System.EventHandler(this.cboSoluzioniSelezionaMAT_APP_EditValueChanged);
                this.cboSoluzioniSelezionaMAT_UTE.EditValueChanged -= new System.EventHandler(this.cboSoluzioniSelezionaMAT_UTE_EditValueChanged);
                this.cboSoluzioniSelezionaMAT_APP2.EditValueChanged -= new System.EventHandler(this.cboSoluzioniSelezionaMAT_APP2_EditValueChanged);
                this.cboSoluzioniSelezionaMAT_UTE2.EditValueChanged -= new System.EventHandler(this.cboSoluzioniSelezionaMAT_UTE2_EditValueChanged);

                this.cboSoluzioniSelezionaSOL_APP.EditValueChanged -= new System.EventHandler(this.cboSoluzioniSelezionaSOL_APP_EditValueChanged);
                this.cboSoluzioniSelezionaSOL_UTE.EditValueChanged -= new System.EventHandler(this.cboSoluzioniSelezionaSOL_UTE_EditValueChanged);
                this.cboSoluzioniSelezionaSOL_APP2.EditValueChanged -= new System.EventHandler(this.cboSoluzioniSelezionaSOL_APP2_EditValueChanged);
                this.cboSoluzioniSelezionaSOL_UTE2.EditValueChanged -= new System.EventHandler(this.cboSoluzioniSelezionaSOL_UTE2_EditValueChanged);
            }
            else
            {
                this.cboSolventiSelezionaMAT_APP.EditValueChanged -= new System.EventHandler(this.cboSolventiSelezionaMAT_APP_EditValueChanged);
                this.cboSolventiSelezionaMAT_UTE.EditValueChanged -= new System.EventHandler(this.cboSolventiSelezionaMAT_UTE_EditValueChanged);
                this.cboSolventiSelezionaMAT_APP2.EditValueChanged -= new System.EventHandler(this.cboSolventiSelezionaMAT_APP2_EditValueChanged);
                this.cboSolventiSelezionaMAT_UTE2.EditValueChanged -= new System.EventHandler(this.cboSolventiSelezionaMAT_UTE2_EditValueChanged);

                this.cboSolventiSelezionaSOV_APP.EditValueChanged -= new System.EventHandler(this.cboSolventiSelezionaSOV_APP_EditValueChanged);
                this.cboSolventiSelezionaSOV_UTE.EditValueChanged -= new System.EventHandler(this.cboSolventiSelezionaSOV_UTE_EditValueChanged);
                this.cboSolventiSelezionaSOV_APP2.EditValueChanged -= new System.EventHandler(this.cboSolventiSelezionaSOV_APP2_EditValueChanged);
                this.cboSolventiSelezionaSOV_UTE2.EditValueChanged -= new System.EventHandler(this.cboSolventiSelezionaSOV_UTE2_EditValueChanged);
            }
        }

        //Soluzioni Materiali Scelta Apparecchio
        private void cboSoluzioniSelezionaMAT_APP_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle=-1;

            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if(editor.EditValue.ToString()!="")
            {
                rowHandle = gviewDuplicaSoluzioniMAT.FocusedRowHandle;
                gviewDuplicaSoluzioniMAT.SetRowCellValue(rowHandle, "IDUtensile", null);
            }
        }

        //Soluzioni Materiali Scelta Utensile
        private void cboSoluzioniSelezionaMAT_UTE_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle = -1;

            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if (editor.EditValue.ToString() != "")
            {
                rowHandle = gviewDuplicaSoluzioniMAT.FocusedRowHandle;
                gviewDuplicaSoluzioniMAT.SetRowCellValue(rowHandle, "IDApparecchio", null);
            }
        }

        //Soluzioni Materiali Scelta Apparecchio2
        private void cboSoluzioniSelezionaMAT_APP2_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle = -1;

            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if (editor.EditValue.ToString() != "")
            {
                rowHandle = gviewDuplicaSoluzioniMAT.FocusedRowHandle;
                gviewDuplicaSoluzioniMAT.SetRowCellValue(rowHandle, "IDUtensile2", null);
            }
        }

        //Soluzioni Materiali Scelta Utensile2
        private void cboSoluzioniSelezionaMAT_UTE2_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle = -1;

            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if (editor.EditValue.ToString() != "")
            {
                rowHandle = gviewDuplicaSoluzioniMAT.FocusedRowHandle;
                gviewDuplicaSoluzioniMAT.SetRowCellValue(rowHandle, "IDApparecchio2", null);
            }
        }

        //Soluzioni Soluzioni Scelta Apparecchio
        private void cboSoluzioniSelezionaSOL_APP_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle = -1;

            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if (editor.EditValue.ToString() != "")
            {
                rowHandle = gviewDuplicaSoluzioniSOL.FocusedRowHandle;
                gviewDuplicaSoluzioniSOL.SetRowCellValue(rowHandle, "IDUtensile", null);
            }
        }

        //Soluzioni Soluzioni Scelta Utensile
        private void cboSoluzioniSelezionaSOL_UTE_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle = -1;

            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if (editor.EditValue.ToString() != "")
            {
                rowHandle = gviewDuplicaSoluzioniSOL.FocusedRowHandle;
                gviewDuplicaSoluzioniSOL.SetRowCellValue(rowHandle, "IDApparecchio", null);
            }
        }

        //Soluzioni Soluzioni Scelta Apparecchio2
        private void cboSoluzioniSelezionaSOL_APP2_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle = -1;

            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if (editor.EditValue.ToString() != "")
            {
                rowHandle = gviewDuplicaSoluzioniSOL.FocusedRowHandle;
                gviewDuplicaSoluzioniSOL.SetRowCellValue(rowHandle, "IDUtensile2", null);
            }
        }

        //Soluzioni Soluzioni Scelta Utensile2
        private void cboSoluzioniSelezionaSOL_UTE2_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle = -1;

            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if (editor.EditValue.ToString() != "")
            {
                rowHandle = gviewDuplicaSoluzioniSOL.FocusedRowHandle;
                gviewDuplicaSoluzioniSOL.SetRowCellValue(rowHandle, "IDApparecchio2", null);
            }
        }

        //Solventi Materiali Scelta Apparecchio
        private void cboSolventiSelezionaMAT_APP_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle = -1;

            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if (editor.EditValue.ToString() != "")
            {
                rowHandle = gviewDuplicaSolventiMAT.FocusedRowHandle;
                gviewDuplicaSolventiMAT.SetRowCellValue(rowHandle, "IDUtensile", null);
            }
        }

        //Solventi Materiali Scelta Utensile
        private void cboSolventiSelezionaMAT_UTE_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle = -1;

            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if (editor.EditValue.ToString() != "")
            {
                rowHandle = gviewDuplicaSolventiMAT.FocusedRowHandle;
                gviewDuplicaSolventiMAT.SetRowCellValue(rowHandle, "IDApparecchio", null);
            }
        }

        //Solventi Materiali Scelta Apparecchio2
        private void cboSolventiSelezionaMAT_APP2_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle = -1;

            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if (editor.EditValue.ToString() != "")
            {
                rowHandle = gviewDuplicaSolventiMAT.FocusedRowHandle;
                gviewDuplicaSolventiMAT.SetRowCellValue(rowHandle, "IDUtensile2", null);
            }
        }

        //Solventi Materiali Scelta Utensile2
        private void cboSolventiSelezionaMAT_UTE2_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle = -1;

            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if (editor.EditValue.ToString() != "")
            {
                rowHandle = gviewDuplicaSolventiMAT.FocusedRowHandle;
                gviewDuplicaSolventiMAT.SetRowCellValue(rowHandle, "IDApparecchio2", null);
            }
        }

        //Solventi Solventi Scelta Apparecchio
        private void cboSolventiSelezionaSOV_APP_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle = -1;

            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if (editor.EditValue.ToString() != "")
            {
                rowHandle = gviewDuplicaSolventiSOV.FocusedRowHandle;
                gviewDuplicaSolventiSOV.SetRowCellValue(rowHandle, "IDUtensile", null);
            }
        }

        //Solventi Solventi Scelta Utensile
        private void cboSolventiSelezionaSOV_UTE_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle = -1;

            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if (editor.EditValue.ToString() != "")
            {
                rowHandle = gviewDuplicaSolventiSOV.FocusedRowHandle;
                gviewDuplicaSolventiSOV.SetRowCellValue(rowHandle, "IDApparecchio", null);
            }
        }

        //Solventi Solventi Scelta Apparecchio2
        private void cboSolventiSelezionaSOV_APP2_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle = -1;

            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if (editor.EditValue.ToString() != "")
            {
                rowHandle = gviewDuplicaSolventiSOV.FocusedRowHandle;
                gviewDuplicaSolventiSOV.SetRowCellValue(rowHandle, "IDUtensile2", null);
            }
        }

        //Solventi Solventi Scelta Utensile2
        private void cboSolventiSelezionaSOV_UTE2_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle = -1;

            GridLookUpEdit editor = (GridLookUpEdit)sender;
            if (editor.EditValue.ToString() != "")
            {
                rowHandle = gviewDuplicaSolventiSOV.FocusedRowHandle;
                gviewDuplicaSolventiSOV.SetRowCellValue(rowHandle, "IDApparecchio2", null);
            }
        }

        private void GridSizeSoluzioniSOL(object sender, EventArgs e)
        {
            string filter = "";
            int selectedRow = -1;

            GridLookUpEdit edit = sender as GridLookUpEdit;
            //edit.Properties.PopupFormMinSize = new Size(1000, 400);

            foreach (int rowHandle in gviewDuplicaSoluzioniSOL.GetSelectedRows())
            {
                selectedRow = rowHandle;
            }

            if (selectedRow >= 0)
            {
                //System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmDuplica", "cboSoluzioniSelezionaSOL", IDUtente);

                //if (str != null)
                //{
                //    cboSoluzioniSelezionaSOL.View.RestoreLayoutFromStream(str);
                //    str.Seek(0, System.IO.SeekOrigin.Begin);
                //}
                //else
                //{
                    if (gviewDuplicaSoluzioniSOL.GetFocusedRowCellValue("IDSoluzione").ToString() != "0")
                    {
                        filter = " (IDStato=3 or IDStato=6) and Tipologia<>'Soluzione MR Modello' ";
                        edit.Properties.View.ActiveFilterString = filter;
                    }
                //}

                //str = null;
            }
        }

        private void GridSizeSoluzioniMAT(object sender, EventArgs e)
        {
            string filter = "";
            int selectedRow = -1;

            GridLookUpEdit edit = sender as GridLookUpEdit;
            //edit.Properties.PopupFormMinSize = new Size(1000, 400);

            foreach (int rowHandle in gviewDuplicaSoluzioniMAT.GetSelectedRows())
            {
                selectedRow = rowHandle;
            }

            if (selectedRow >= 0)
            {
                if (gviewDuplicaSoluzioniMAT.GetFocusedRowCellValue("IDMateriale").ToString() != "0")
                {
                    filter = " IDMateriale= " + gviewDuplicaSoluzioniMAT.GetFocusedRowCellValue("IDMateriale").ToString() + " ";
                    edit.Properties.View.ActiveFilterString = filter;
                }
            }
        }

        private void GridSizeSolventiSOV(object sender, EventArgs e)
        {
            string filter = "";
            int selectedRow = -1;

            GridLookUpEdit edit = sender as GridLookUpEdit;
            //edit.Properties.PopupFormMinSize = new Size(1000, 400);

            foreach (int rowHandle in gviewDuplicaSolventiSOV.GetSelectedRows())
            {
                selectedRow = rowHandle;
            }

            if (selectedRow >= 0)
            {
                if (gviewDuplicaSolventiSOV.GetFocusedRowCellValue("IDSolvente").ToString() != "0")
                {
                    filter = " (IDStato=3 or IDStato=6) and Tipologia<>'Soluzione di Lavoro Modello' ";
                    edit.Properties.View.ActiveFilterString = filter;
                }
            }
        }

        private void GridSizeSolventiMAT(object sender, EventArgs e)
        {
            string filter = "";
            int selectedRow = -1;

            GridLookUpEdit edit = sender as GridLookUpEdit;
            //edit.Properties.PopupFormMinSize = new Size(1000, 400);

            foreach (int rowHandle in gviewDuplicaSolventiMAT.GetSelectedRows())
            {
                selectedRow = rowHandle;
            }

            if (selectedRow >= 0)
            {
                if (gviewDuplicaSolventiMAT.GetFocusedRowCellValue("IDMateriale").ToString() != "0")
                {
                    filter = " IDMateriale= " + gviewDuplicaSolventiMAT.GetFocusedRowCellValue("IDMateriale").ToString() + " ";
                    edit.Properties.View.ActiveFilterString = filter;
                }
            }
        }

        private void GridSizeUtensili(object sender, EventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;
            edit.Properties.PopupFormMinSize = new Size(1000, 400);
            edit.Properties.PopupView.Columns[0].Visible = false;
            edit.Properties.PopupView.Columns[2].Width = 250;
        }

        private void GridSizeApparecchiSolvente(object sender, EventArgs e)
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

        private void GridSizeApparecchi(object sender, EventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;
            edit.Properties.PopupFormMinSize = new Size(1000, 400);
            edit.Properties.PopupView.Columns[0].Visible = false;
            edit.Properties.PopupView.Columns[2].Visible = false;
            edit.Properties.PopupView.Columns[4].Visible = false;
            edit.Properties.PopupView.Columns[5].Visible = false;
            edit.Properties.PopupView.Columns[6].Visible = false;
            edit.Properties.PopupView.Columns[8].Visible = false;

            //edit.Properties.PopupView.Columns[9].Visible = false;
            //edit.Properties.PopupView.Columns[10].Visible = false;
            //edit.Properties.PopupView.Columns[11].Visible = false;
            //edit.Properties.PopupView.Columns[12].Visible = false;
            //edit.Properties.PopupView.Columns[13].Visible = false;
            //edit.Properties.PopupView.Columns[14].Visible = false;
            //edit.Properties.PopupView.Columns[15].Visible = false;
            //edit.Properties.PopupView.Columns[16].Visible = false;
            //edit.Properties.PopupView.Columns[17].Visible = false;
            //edit.Properties.PopupView.Columns[18].Visible = false;
            //edit.Properties.PopupView.Columns[19].Visible = false;
            //edit.Properties.PopupView.Columns[20].Visible = false;

            edit.Properties.PopupView.Columns[3].Width = 250;

            string filter = " (Tipologia = 'Bilancia' OR Tipologia = 'Pipetta') AND IDStatoServizio=1 ";

            edit.Properties.View.ActiveFilterString = filter;
        }

        private void cboSoluzioniSelezionaSOL_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            //System.IO.MemoryStream str = new System.IO.MemoryStream();
            //cboSoluzioniSelezionaSOL.View.SaveLayoutToStream(str);
            //str.Seek(0, System.IO.SeekOrigin.Begin);
            //frmLogin.SaveUserPreferences("frmDuplica", "cboSoluzioniSelezionaSOL", str, IDUtente);
        }
    }
}
