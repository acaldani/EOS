
namespace EOS.UI
{
    partial class frmRetta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.tbNuovo = new DevExpress.XtraBars.BarButtonItem();
            this.tbDuplica = new DevExpress.XtraBars.BarButtonItem();
            this.tbSalva = new DevExpress.XtraBars.BarButtonItem();
            this.tbAggiorna = new DevExpress.XtraBars.BarButtonItem();
            this.tbStampa = new DevExpress.XtraBars.BarButtonItem();
            this.tbStampaReport = new DevExpress.XtraBars.BarButtonItem();
            this.tbEsportaExcel = new DevExpress.XtraBars.BarButtonItem();
            this.tbVisualizzaLog = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridRette = new DevExpress.XtraGrid.GridControl();
            this.gviewRette = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDRetta = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCodiceRetta = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTipologia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNome = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStato = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSolvente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMiscelaSolventi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDefaultGiorniScadenza = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataScadenza = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataCreazione = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUtenteCreazione = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtCodiceRetta = new DevExpress.XtraEditors.TextEdit();
            this.cboTipologia = new DevExpress.XtraEditors.GridLookUpEdit();
            this.retteTipologiaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.soluzioni = new EOS.UI.Soluzioni();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtNome = new DevExpress.XtraEditors.TextEdit();
            this.txtGiorniScadenza = new DevExpress.XtraEditors.TextEdit();
            this.txtDataScadenza = new DevExpress.XtraEditors.TextEdit();
            this.cboSingoloSolvente = new DevExpress.XtraEditors.GridLookUpEdit();
            this.materialiLottiSelectCommandBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lupin = new EOS.UI.Lupin();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboMiscelaSolventi = new DevExpress.XtraEditors.GridLookUpEdit();
            this.solventiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.solventi = new EOS.UI.Solventi();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtNote = new DevExpress.XtraEditors.TextEdit();
            this.txtDataCreazione = new DevExpress.XtraEditors.DateEdit();
            this.cboUtenteCreazione = new DevExpress.XtraEditors.LookUpEdit();
            this.loginUtentiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cboStato = new DevExpress.XtraEditors.GridLookUpEdit();
            this.compostiStatiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.soluzioniTipologiaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridRetteDetails = new DevExpress.XtraGrid.GridControl();
            this.gviewRetteDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSubIDSoluzione = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubCodiceSoluzione = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubTipologia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboSubTipologia = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.soluzioniTipologiaBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSubNome = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubIDStato = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboSubStato = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.compostiStatiBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSubIDUbicazione = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboSubUbicazione = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.ubicazioniSelectCommandBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView12 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSubUMVolumeFinale = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubVolumeFinale = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubIDSolvente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboSubSolvente = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.solventiBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSubIDSchedaDocumenti = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboSubSchedaDocumenti = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSubIDApparecchio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboSubApparecchio = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.apparecchiSelectCommandBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView7 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSubIDUtensile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboSubUtensile = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.utensiliSelectCommandBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView8 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSubIDApparecchio2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboSubApparecchio2 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView9 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSubIDUtensile2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboSubUtensile2 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView10 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSubDefaultGiorniScadenza = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubDataScadenza = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubNotePrescrittive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubNoteDescrittive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubDataCreazione = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubIDUtente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboSubUtente = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.loginUtentiBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView11 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.butAggiungi = new DevExpress.XtraEditors.SimpleButton();
            this.butScollega = new DevExpress.XtraEditors.SimpleButton();
            this.butModifica = new DevExpress.XtraEditors.SimpleButton();
            this.butScollegaEAnnulla = new DevExpress.XtraEditors.SimpleButton();
            this.butClose = new DevExpress.XtraEditors.SimpleButton();
            this.soluzioni_TipologiaTableAdapter = new EOS.UI.SoluzioniTableAdapters.Soluzioni_TipologiaTableAdapter();
            this.materialiLottiSelectCommandTableAdapter = new EOS.UI.LupinTableAdapters.MaterialiLottiSelectCommandTableAdapter();
            this.solventiTableAdapter = new EOS.UI.SolventiTableAdapters.SolventiTableAdapter();
            this.composti_StatiTableAdapter = new EOS.UI.SoluzioniTableAdapters.Composti_StatiTableAdapter();
            this.login_UtentiTableAdapter = new EOS.UI.SoluzioniTableAdapters.Login_UtentiTableAdapter();
            this.rette_TipologiaTableAdapter = new EOS.UI.SoluzioniTableAdapters.Rette_TipologiaTableAdapter();
            this.apparecchiSelectCommandTableAdapter = new EOS.UI.LupinTableAdapters.ApparecchiSelectCommandTableAdapter();
            this.utensiliSelectCommandTableAdapter = new EOS.UI.LupinTableAdapters.UtensiliSelectCommandTableAdapter();
            this.ubicazioniSelectCommandTableAdapter = new EOS.UI.LupinTableAdapters.UbicazioniSelectCommandTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewRette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodiceRetta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipologia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retteTipologiaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.soluzioni)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNome.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGiorniScadenza.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataScadenza.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSingoloSolvente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialiLottiSelectCommandBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMiscelaSolventi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.solventiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.solventi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCreazione.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCreazione.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUtenteCreazione.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginUtentiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStato.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.compostiStatiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.soluzioniTipologiaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRetteDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewRetteDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubTipologia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.soluzioniTipologiaBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubStato)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.compostiStatiBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubUbicazione)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubicazioniSelectCommandBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubSolvente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.solventiBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubSchedaDocumenti)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubApparecchio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.apparecchiSelectCommandBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubUtensile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.utensiliSelectCommandBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubApparecchio2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubUtensile2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubUtente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginUtentiBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView11)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.tbNuovo,
            this.tbDuplica,
            this.tbSalva,
            this.tbAggiorna,
            this.tbStampa,
            this.tbStampaReport,
            this.tbEsportaExcel,
            this.tbVisualizzaLog});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 8;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Text = "Tools";
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.tbNuovo),
            new DevExpress.XtraBars.LinkPersistInfo(this.tbDuplica),
            new DevExpress.XtraBars.LinkPersistInfo(this.tbSalva),
            new DevExpress.XtraBars.LinkPersistInfo(this.tbAggiorna),
            new DevExpress.XtraBars.LinkPersistInfo(this.tbStampa),
            new DevExpress.XtraBars.LinkPersistInfo(this.tbStampaReport),
            new DevExpress.XtraBars.LinkPersistInfo(this.tbEsportaExcel),
            new DevExpress.XtraBars.LinkPersistInfo(this.tbVisualizzaLog)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // tbNuovo
            // 
            this.tbNuovo.Caption = "Nuovo";
            this.tbNuovo.Id = 0;
            this.tbNuovo.Name = "tbNuovo";
            this.tbNuovo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tbNuovo_ItemClick);
            // 
            // tbDuplica
            // 
            this.tbDuplica.Caption = "Nuovo da Modello";
            this.tbDuplica.Id = 1;
            this.tbDuplica.Name = "tbDuplica";
            this.tbDuplica.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tbDuplica_ItemClick);
            // 
            // tbSalva
            // 
            this.tbSalva.Caption = "Salva";
            this.tbSalva.Id = 2;
            this.tbSalva.Name = "tbSalva";
            this.tbSalva.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tbSalva_ItemClick);
            // 
            // tbAggiorna
            // 
            this.tbAggiorna.Caption = "Aggiorna";
            this.tbAggiorna.Id = 3;
            this.tbAggiorna.Name = "tbAggiorna";
            this.tbAggiorna.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tbAggiorna_ItemClick);
            // 
            // tbStampa
            // 
            this.tbStampa.Caption = "Stampa Etichette";
            this.tbStampa.Id = 4;
            this.tbStampa.Name = "tbStampa";
            this.tbStampa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tbStampa_ItemClick);
            // 
            // tbStampaReport
            // 
            this.tbStampaReport.Caption = "Stampa Report";
            this.tbStampaReport.Id = 5;
            this.tbStampaReport.Name = "tbStampaReport";
            this.tbStampaReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tbStampaReport_ItemClick);
            // 
            // tbEsportaExcel
            // 
            this.tbEsportaExcel.Caption = "Esporta su Excel";
            this.tbEsportaExcel.Id = 6;
            this.tbEsportaExcel.Name = "tbEsportaExcel";
            this.tbEsportaExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tbEsportaExcel_ItemClick);
            // 
            // tbVisualizzaLog
            // 
            this.tbVisualizzaLog.Caption = "Visualizza Log";
            this.tbVisualizzaLog.Id = 7;
            this.tbVisualizzaLog.Name = "tbVisualizzaLog";
            this.tbVisualizzaLog.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tbVisualizzaLog_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1302, 47);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 913);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1302, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 47);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 866);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1302, 47);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 866);
            // 
            // gridRette
            // 
            this.gridRette.Location = new System.Drawing.Point(12, 53);
            this.gridRette.MainView = this.gviewRette;
            this.gridRette.MenuManager = this.barManager1;
            this.gridRette.Name = "gridRette";
            this.gridRette.Size = new System.Drawing.Size(1277, 300);
            this.gridRette.TabIndex = 4;
            this.gridRette.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gviewRette});
            this.gridRette.DoubleClick += new System.EventHandler(this.gridRette_DoubleClick);
            // 
            // gviewRette
            // 
            this.gviewRette.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDRetta,
            this.colCodiceRetta,
            this.colTipologia,
            this.colNome,
            this.colStato,
            this.colSolvente,
            this.colMiscelaSolventi,
            this.colDefaultGiorniScadenza,
            this.colDataScadenza,
            this.colNote,
            this.colDataCreazione,
            this.colUtenteCreazione});
            this.gviewRette.GridControl = this.gridRette;
            this.gviewRette.Name = "gviewRette";
            this.gviewRette.OptionsView.ShowGroupPanel = false;
            this.gviewRette.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gviewRette_FocusedRowChanged);
            // 
            // colIDRetta
            // 
            this.colIDRetta.Caption = "ID Retta";
            this.colIDRetta.FieldName = "IDRetta";
            this.colIDRetta.Name = "colIDRetta";
            this.colIDRetta.Visible = true;
            this.colIDRetta.VisibleIndex = 0;
            // 
            // colCodiceRetta
            // 
            this.colCodiceRetta.Caption = "Codice Retta";
            this.colCodiceRetta.FieldName = "CodiceRetta";
            this.colCodiceRetta.Name = "colCodiceRetta";
            this.colCodiceRetta.Visible = true;
            this.colCodiceRetta.VisibleIndex = 1;
            // 
            // colTipologia
            // 
            this.colTipologia.Caption = "Tipologia";
            this.colTipologia.FieldName = "Tipologia";
            this.colTipologia.Name = "colTipologia";
            this.colTipologia.Visible = true;
            this.colTipologia.VisibleIndex = 2;
            // 
            // colNome
            // 
            this.colNome.Caption = "Nome";
            this.colNome.FieldName = "Nome";
            this.colNome.Name = "colNome";
            this.colNome.Visible = true;
            this.colNome.VisibleIndex = 3;
            // 
            // colStato
            // 
            this.colStato.Caption = "Stato";
            this.colStato.FieldName = "Stato";
            this.colStato.Name = "colStato";
            this.colStato.Visible = true;
            this.colStato.VisibleIndex = 4;
            // 
            // colSolvente
            // 
            this.colSolvente.Caption = "Solvente";
            this.colSolvente.FieldName = "Solvente";
            this.colSolvente.Name = "colSolvente";
            this.colSolvente.Visible = true;
            this.colSolvente.VisibleIndex = 5;
            // 
            // colMiscelaSolventi
            // 
            this.colMiscelaSolventi.Caption = "Miscela di Solventi";
            this.colMiscelaSolventi.FieldName = "MiscelaSolventi";
            this.colMiscelaSolventi.Name = "colMiscelaSolventi";
            this.colMiscelaSolventi.Visible = true;
            this.colMiscelaSolventi.VisibleIndex = 6;
            // 
            // colDefaultGiorniScadenza
            // 
            this.colDefaultGiorniScadenza.Caption = "Default Giorni Scadenza";
            this.colDefaultGiorniScadenza.FieldName = "DefaultGiorniScadenza";
            this.colDefaultGiorniScadenza.Name = "colDefaultGiorniScadenza";
            this.colDefaultGiorniScadenza.Visible = true;
            this.colDefaultGiorniScadenza.VisibleIndex = 7;
            // 
            // colDataScadenza
            // 
            this.colDataScadenza.Caption = "Data Scadenza";
            this.colDataScadenza.FieldName = "DataScadenza";
            this.colDataScadenza.Name = "colDataScadenza";
            this.colDataScadenza.Visible = true;
            this.colDataScadenza.VisibleIndex = 8;
            // 
            // colNote
            // 
            this.colNote.Caption = "Note";
            this.colNote.FieldName = "Note";
            this.colNote.Name = "colNote";
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 9;
            // 
            // colDataCreazione
            // 
            this.colDataCreazione.Caption = "Data Creazione";
            this.colDataCreazione.FieldName = "DataCreazione";
            this.colDataCreazione.Name = "colDataCreazione";
            this.colDataCreazione.Visible = true;
            this.colDataCreazione.VisibleIndex = 10;
            // 
            // colUtenteCreazione
            // 
            this.colUtenteCreazione.Caption = "Utente Creazione";
            this.colUtenteCreazione.FieldName = "UtenteCreazione";
            this.colUtenteCreazione.Name = "colUtenteCreazione";
            this.colUtenteCreazione.Visible = true;
            this.colUtenteCreazione.VisibleIndex = 11;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtCodiceRetta);
            this.layoutControl1.Controls.Add(this.cboTipologia);
            this.layoutControl1.Controls.Add(this.txtNome);
            this.layoutControl1.Controls.Add(this.txtGiorniScadenza);
            this.layoutControl1.Controls.Add(this.txtDataScadenza);
            this.layoutControl1.Controls.Add(this.cboSingoloSolvente);
            this.layoutControl1.Controls.Add(this.cboMiscelaSolventi);
            this.layoutControl1.Controls.Add(this.txtNote);
            this.layoutControl1.Controls.Add(this.txtDataCreazione);
            this.layoutControl1.Controls.Add(this.cboUtenteCreazione);
            this.layoutControl1.Controls.Add(this.cboStato);
            this.layoutControl1.Location = new System.Drawing.Point(12, 359);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1277, 166);
            this.layoutControl1.TabIndex = 5;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtCodiceRetta
            // 
            this.txtCodiceRetta.Enabled = false;
            this.txtCodiceRetta.Location = new System.Drawing.Point(138, 12);
            this.txtCodiceRetta.MenuManager = this.barManager1;
            this.txtCodiceRetta.Name = "txtCodiceRetta";
            this.txtCodiceRetta.Size = new System.Drawing.Size(497, 20);
            this.txtCodiceRetta.StyleController = this.layoutControl1;
            this.txtCodiceRetta.TabIndex = 4;
            // 
            // cboTipologia
            // 
            this.cboTipologia.Location = new System.Drawing.Point(138, 36);
            this.cboTipologia.MenuManager = this.barManager1;
            this.cboTipologia.Name = "cboTipologia";
            this.cboTipologia.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTipologia.Properties.DataSource = this.retteTipologiaBindingSource;
            this.cboTipologia.Properties.DisplayMember = "NomeTipologia";
            this.cboTipologia.Properties.NullText = "";
            this.cboTipologia.Properties.PopupSizeable = false;
            this.cboTipologia.Properties.PopupView = this.gridLookUpEdit1View;
            this.cboTipologia.Properties.ValueMember = "NomeTipologia";
            this.cboTipologia.Size = new System.Drawing.Size(497, 20);
            this.cboTipologia.StyleController = this.layoutControl1;
            this.cboTipologia.TabIndex = 5;
            // 
            // retteTipologiaBindingSource
            // 
            this.retteTipologiaBindingSource.DataMember = "Rette_Tipologia";
            this.retteTipologiaBindingSource.DataSource = this.soluzioni;
            // 
            // soluzioni
            // 
            this.soluzioni.DataSetName = "Soluzioni";
            this.soluzioni.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(765, 36);
            this.txtNome.MenuManager = this.barManager1;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(500, 20);
            this.txtNome.StyleController = this.layoutControl1;
            this.txtNome.TabIndex = 6;
            // 
            // txtGiorniScadenza
            // 
            this.txtGiorniScadenza.Location = new System.Drawing.Point(138, 60);
            this.txtGiorniScadenza.MenuManager = this.barManager1;
            this.txtGiorniScadenza.Name = "txtGiorniScadenza";
            this.txtGiorniScadenza.Size = new System.Drawing.Size(497, 20);
            this.txtGiorniScadenza.StyleController = this.layoutControl1;
            this.txtGiorniScadenza.TabIndex = 7;
            // 
            // txtDataScadenza
            // 
            this.txtDataScadenza.Location = new System.Drawing.Point(765, 60);
            this.txtDataScadenza.MenuManager = this.barManager1;
            this.txtDataScadenza.Name = "txtDataScadenza";
            this.txtDataScadenza.Size = new System.Drawing.Size(500, 20);
            this.txtDataScadenza.StyleController = this.layoutControl1;
            this.txtDataScadenza.TabIndex = 8;
            // 
            // cboSingoloSolvente
            // 
            this.cboSingoloSolvente.Location = new System.Drawing.Point(138, 84);
            this.cboSingoloSolvente.MenuManager = this.barManager1;
            this.cboSingoloSolvente.Name = "cboSingoloSolvente";
            this.cboSingoloSolvente.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSingoloSolvente.Properties.DataSource = this.materialiLottiSelectCommandBindingSource;
            this.cboSingoloSolvente.Properties.DisplayMember = "DenominazioneMateriale";
            this.cboSingoloSolvente.Properties.NullText = "";
            this.cboSingoloSolvente.Properties.PopupSizeable = false;
            this.cboSingoloSolvente.Properties.PopupView = this.gridView1;
            this.cboSingoloSolvente.Properties.ValueMember = "IDSchedaDocumenti";
            this.cboSingoloSolvente.Size = new System.Drawing.Size(498, 20);
            this.cboSingoloSolvente.StyleController = this.layoutControl1;
            this.cboSingoloSolvente.TabIndex = 9;
            this.cboSingoloSolvente.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.GridSize);
            // 
            // materialiLottiSelectCommandBindingSource
            // 
            this.materialiLottiSelectCommandBindingSource.DataMember = "MaterialiLottiSelectCommand";
            this.materialiLottiSelectCommandBindingSource.DataSource = this.lupin;
            // 
            // lupin
            // 
            this.lupin.DataSetName = "Lupin";
            this.lupin.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // cboMiscelaSolventi
            // 
            this.cboMiscelaSolventi.Location = new System.Drawing.Point(766, 84);
            this.cboMiscelaSolventi.MenuManager = this.barManager1;
            this.cboMiscelaSolventi.Name = "cboMiscelaSolventi";
            this.cboMiscelaSolventi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMiscelaSolventi.Properties.DataSource = this.solventiBindingSource;
            this.cboMiscelaSolventi.Properties.DisplayMember = "Nome";
            this.cboMiscelaSolventi.Properties.NullText = "";
            this.cboMiscelaSolventi.Properties.PopupSizeable = false;
            this.cboMiscelaSolventi.Properties.PopupView = this.gridView2;
            this.cboMiscelaSolventi.Properties.ValueMember = "IDSolvente";
            this.cboMiscelaSolventi.Size = new System.Drawing.Size(499, 20);
            this.cboMiscelaSolventi.StyleController = this.layoutControl1;
            this.cboMiscelaSolventi.TabIndex = 10;
            this.cboMiscelaSolventi.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.GridSize);
            // 
            // solventiBindingSource
            // 
            this.solventiBindingSource.DataMember = "Solventi";
            this.solventiBindingSource.DataSource = this.solventi;
            // 
            // solventi
            // 
            this.solventi.DataSetName = "Solventi";
            this.solventi.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(766, 108);
            this.txtNote.MenuManager = this.barManager1;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(499, 20);
            this.txtNote.StyleController = this.layoutControl1;
            this.txtNote.TabIndex = 12;
            // 
            // txtDataCreazione
            // 
            this.txtDataCreazione.EditValue = null;
            this.txtDataCreazione.Location = new System.Drawing.Point(138, 132);
            this.txtDataCreazione.MenuManager = this.barManager1;
            this.txtDataCreazione.Name = "txtDataCreazione";
            this.txtDataCreazione.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataCreazione.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataCreazione.Size = new System.Drawing.Size(498, 20);
            this.txtDataCreazione.StyleController = this.layoutControl1;
            this.txtDataCreazione.TabIndex = 13;
            // 
            // cboUtenteCreazione
            // 
            this.cboUtenteCreazione.Location = new System.Drawing.Point(766, 132);
            this.cboUtenteCreazione.MenuManager = this.barManager1;
            this.cboUtenteCreazione.Name = "cboUtenteCreazione";
            this.cboUtenteCreazione.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUtenteCreazione.Properties.DataSource = this.loginUtentiBindingSource;
            this.cboUtenteCreazione.Properties.DisplayMember = "NomeUtente";
            this.cboUtenteCreazione.Properties.NullText = "";
            this.cboUtenteCreazione.Properties.PopupSizeable = false;
            this.cboUtenteCreazione.Properties.ValueMember = "IDUtente";
            this.cboUtenteCreazione.Size = new System.Drawing.Size(499, 20);
            this.cboUtenteCreazione.StyleController = this.layoutControl1;
            this.cboUtenteCreazione.TabIndex = 14;
            // 
            // loginUtentiBindingSource
            // 
            this.loginUtentiBindingSource.DataMember = "Login_Utenti";
            this.loginUtentiBindingSource.DataSource = this.soluzioni;
            // 
            // cboStato
            // 
            this.cboStato.Location = new System.Drawing.Point(138, 108);
            this.cboStato.MenuManager = this.barManager1;
            this.cboStato.Name = "cboStato";
            this.cboStato.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStato.Properties.DataSource = this.compostiStatiBindingSource;
            this.cboStato.Properties.DisplayMember = "Nome";
            this.cboStato.Properties.NullText = "";
            this.cboStato.Properties.PopupSizeable = false;
            this.cboStato.Properties.PopupView = this.gridView3;
            this.cboStato.Properties.ValueMember = "ID";
            this.cboStato.Size = new System.Drawing.Size(498, 20);
            this.cboStato.StyleController = this.layoutControl1;
            this.cboStato.TabIndex = 11;
            // 
            // compostiStatiBindingSource
            // 
            this.compostiStatiBindingSource.DataMember = "Composti_Stati";
            this.compostiStatiBindingSource.DataSource = this.soluzioni;
            // 
            // gridView3
            // 
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1277, 166);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtCodiceRetta;
            this.layoutControlItem1.CustomizationFormText = "Codice Retta";
            this.layoutControlItem1.Enabled = false;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(627, 24);
            this.layoutControlItem1.Text = "Codice Retta";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(122, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(627, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(630, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cboTipologia;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(627, 24);
            this.layoutControlItem2.Text = "* Tipologia";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(122, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtNome;
            this.layoutControlItem3.Location = new System.Drawing.Point(627, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(630, 24);
            this.layoutControlItem3.Text = "* Nome";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(122, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtGiorniScadenza;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(627, 24);
            this.layoutControlItem4.Text = "Giorni Scadenza";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(122, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtDataScadenza;
            this.layoutControlItem5.Location = new System.Drawing.Point(627, 48);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(630, 24);
            this.layoutControlItem5.Text = "Data Scadenza";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(122, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.cboSingoloSolvente;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(628, 24);
            this.layoutControlItem6.Text = "(A1)* Singolo Solvente";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(122, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.cboMiscelaSolventi;
            this.layoutControlItem7.Location = new System.Drawing.Point(628, 72);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(629, 24);
            this.layoutControlItem7.Text = "(A2)* Soluzione di Lavoro";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(122, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.cboStato;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(628, 24);
            this.layoutControlItem8.Text = "* Stato";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(122, 13);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.txtNote;
            this.layoutControlItem9.Location = new System.Drawing.Point(628, 96);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(629, 24);
            this.layoutControlItem9.Text = "Note";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(122, 13);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.txtDataCreazione;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(628, 26);
            this.layoutControlItem10.Text = "Data Creazione";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(122, 13);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.cboUtenteCreazione;
            this.layoutControlItem11.Location = new System.Drawing.Point(628, 120);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(629, 26);
            this.layoutControlItem11.Text = "Utente Creazione";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(122, 13);
            // 
            // soluzioniTipologiaBindingSource
            // 
            this.soluzioniTipologiaBindingSource.DataMember = "Soluzioni_Tipologia";
            this.soluzioniTipologiaBindingSource.DataSource = this.soluzioni;
            // 
            // gridRetteDetails
            // 
            this.gridRetteDetails.Location = new System.Drawing.Point(12, 531);
            this.gridRetteDetails.MainView = this.gviewRetteDetails;
            this.gridRetteDetails.MenuManager = this.barManager1;
            this.gridRetteDetails.Name = "gridRetteDetails";
            this.gridRetteDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cboSubTipologia,
            this.cboSubStato,
            this.cboSubSolvente,
            this.cboSubSchedaDocumenti,
            this.cboSubApparecchio,
            this.cboSubUtensile,
            this.cboSubApparecchio2,
            this.cboSubUtensile2,
            this.cboSubUtente,
            this.cboSubUbicazione});
            this.gridRetteDetails.Size = new System.Drawing.Size(1277, 300);
            this.gridRetteDetails.TabIndex = 10;
            this.gridRetteDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gviewRetteDetails});
            this.gridRetteDetails.DoubleClick += new System.EventHandler(this.gridRetteDetails_DoubleClick);
            // 
            // gviewRetteDetails
            // 
            this.gviewRetteDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSubIDSoluzione,
            this.colSubCodiceSoluzione,
            this.colSubTipologia,
            this.colSubNome,
            this.colSubIDStato,
            this.colSubIDUbicazione,
            this.colSubUMVolumeFinale,
            this.colSubVolumeFinale,
            this.colSubIDSolvente,
            this.colSubIDSchedaDocumenti,
            this.colSubIDApparecchio,
            this.colSubIDUtensile,
            this.colSubIDApparecchio2,
            this.colSubIDUtensile2,
            this.colSubDefaultGiorniScadenza,
            this.colSubDataScadenza,
            this.colSubNotePrescrittive,
            this.colSubNoteDescrittive,
            this.colSubDataCreazione,
            this.colSubIDUtente});
            this.gviewRetteDetails.GridControl = this.gridRetteDetails;
            this.gviewRetteDetails.Name = "gviewRetteDetails";
            this.gviewRetteDetails.OptionsView.ShowGroupPanel = false;
            // 
            // colSubIDSoluzione
            // 
            this.colSubIDSoluzione.Caption = "ID Soluzione";
            this.colSubIDSoluzione.FieldName = "IDSoluzione";
            this.colSubIDSoluzione.Name = "colSubIDSoluzione";
            this.colSubIDSoluzione.Visible = true;
            this.colSubIDSoluzione.VisibleIndex = 0;
            // 
            // colSubCodiceSoluzione
            // 
            this.colSubCodiceSoluzione.Caption = "Codice Soluzione";
            this.colSubCodiceSoluzione.FieldName = "CodiceSoluzione";
            this.colSubCodiceSoluzione.Name = "colSubCodiceSoluzione";
            this.colSubCodiceSoluzione.Visible = true;
            this.colSubCodiceSoluzione.VisibleIndex = 1;
            // 
            // colSubTipologia
            // 
            this.colSubTipologia.Caption = "Tipologia";
            this.colSubTipologia.ColumnEdit = this.cboSubTipologia;
            this.colSubTipologia.FieldName = "Tipologia";
            this.colSubTipologia.Name = "colSubTipologia";
            this.colSubTipologia.Visible = true;
            this.colSubTipologia.VisibleIndex = 19;
            // 
            // cboSubTipologia
            // 
            this.cboSubTipologia.AutoHeight = false;
            this.cboSubTipologia.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSubTipologia.DataSource = this.soluzioniTipologiaBindingSource1;
            this.cboSubTipologia.DisplayMember = "NomeTipologia";
            this.cboSubTipologia.Name = "cboSubTipologia";
            this.cboSubTipologia.PopupView = this.repositoryItemGridLookUpEdit1View;
            this.cboSubTipologia.ValueMember = "NomeTipologia";
            // 
            // soluzioniTipologiaBindingSource1
            // 
            this.soluzioniTipologiaBindingSource1.DataMember = "Soluzioni_Tipologia";
            this.soluzioniTipologiaBindingSource1.DataSource = this.soluzioni;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colSubNome
            // 
            this.colSubNome.Caption = "Nome";
            this.colSubNome.FieldName = "Nome";
            this.colSubNome.Name = "colSubNome";
            this.colSubNome.Visible = true;
            this.colSubNome.VisibleIndex = 2;
            // 
            // colSubIDStato
            // 
            this.colSubIDStato.Caption = "Stato";
            this.colSubIDStato.ColumnEdit = this.cboSubStato;
            this.colSubIDStato.FieldName = "IDStato";
            this.colSubIDStato.Name = "colSubIDStato";
            this.colSubIDStato.Visible = true;
            this.colSubIDStato.VisibleIndex = 3;
            // 
            // cboSubStato
            // 
            this.cboSubStato.AutoHeight = false;
            this.cboSubStato.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSubStato.DataSource = this.compostiStatiBindingSource1;
            this.cboSubStato.DisplayMember = "Nome";
            this.cboSubStato.Name = "cboSubStato";
            this.cboSubStato.PopupView = this.gridView4;
            this.cboSubStato.ValueMember = "ID";
            // 
            // compostiStatiBindingSource1
            // 
            this.compostiStatiBindingSource1.DataMember = "Composti_Stati";
            this.compostiStatiBindingSource1.DataSource = this.soluzioni;
            // 
            // gridView4
            // 
            this.gridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            // 
            // colSubIDUbicazione
            // 
            this.colSubIDUbicazione.Caption = "Ubicazione";
            this.colSubIDUbicazione.ColumnEdit = this.cboSubUbicazione;
            this.colSubIDUbicazione.FieldName = "IDUbicazione";
            this.colSubIDUbicazione.Name = "colSubIDUbicazione";
            this.colSubIDUbicazione.Visible = true;
            this.colSubIDUbicazione.VisibleIndex = 18;
            // 
            // cboSubUbicazione
            // 
            this.cboSubUbicazione.AutoHeight = false;
            this.cboSubUbicazione.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSubUbicazione.DataSource = this.ubicazioniSelectCommandBindingSource;
            this.cboSubUbicazione.DisplayMember = "Ubicazione";
            this.cboSubUbicazione.Name = "cboSubUbicazione";
            this.cboSubUbicazione.PopupView = this.gridView12;
            this.cboSubUbicazione.ValueMember = "IDUbicazione";
            // 
            // ubicazioniSelectCommandBindingSource
            // 
            this.ubicazioniSelectCommandBindingSource.DataMember = "UbicazioniSelectCommand";
            this.ubicazioniSelectCommandBindingSource.DataSource = this.lupin;
            // 
            // gridView12
            // 
            this.gridView12.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView12.Name = "gridView12";
            this.gridView12.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView12.OptionsView.ShowGroupPanel = false;
            // 
            // colSubUMVolumeFinale
            // 
            this.colSubUMVolumeFinale.Caption = "UM Volume Finale";
            this.colSubUMVolumeFinale.FieldName = "UMVolumeFinale";
            this.colSubUMVolumeFinale.Name = "colSubUMVolumeFinale";
            this.colSubUMVolumeFinale.Visible = true;
            this.colSubUMVolumeFinale.VisibleIndex = 4;
            // 
            // colSubVolumeFinale
            // 
            this.colSubVolumeFinale.Caption = "Volume Finale";
            this.colSubVolumeFinale.FieldName = "VolumeFinale";
            this.colSubVolumeFinale.Name = "colSubVolumeFinale";
            this.colSubVolumeFinale.Visible = true;
            this.colSubVolumeFinale.VisibleIndex = 5;
            // 
            // colSubIDSolvente
            // 
            this.colSubIDSolvente.Caption = "Miscela di Solventi";
            this.colSubIDSolvente.ColumnEdit = this.cboSubSolvente;
            this.colSubIDSolvente.FieldName = "IDSolvente";
            this.colSubIDSolvente.Name = "colSubIDSolvente";
            this.colSubIDSolvente.Visible = true;
            this.colSubIDSolvente.VisibleIndex = 6;
            // 
            // cboSubSolvente
            // 
            this.cboSubSolvente.AutoHeight = false;
            this.cboSubSolvente.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSubSolvente.DataSource = this.solventiBindingSource1;
            this.cboSubSolvente.DisplayMember = "Nome";
            this.cboSubSolvente.Name = "cboSubSolvente";
            this.cboSubSolvente.PopupView = this.gridView5;
            this.cboSubSolvente.ValueMember = "IDSolvente";
            this.cboSubSolvente.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.GridSize);
            // 
            // solventiBindingSource1
            // 
            this.solventiBindingSource1.DataMember = "Solventi";
            this.solventiBindingSource1.DataSource = this.solventi;
            // 
            // gridView5
            // 
            this.gridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            // 
            // colSubIDSchedaDocumenti
            // 
            this.colSubIDSchedaDocumenti.Caption = "Solvente Singolo";
            this.colSubIDSchedaDocumenti.ColumnEdit = this.cboSubSchedaDocumenti;
            this.colSubIDSchedaDocumenti.FieldName = "IDSchedaDocumenti";
            this.colSubIDSchedaDocumenti.Name = "colSubIDSchedaDocumenti";
            this.colSubIDSchedaDocumenti.Visible = true;
            this.colSubIDSchedaDocumenti.VisibleIndex = 7;
            // 
            // cboSubSchedaDocumenti
            // 
            this.cboSubSchedaDocumenti.AutoHeight = false;
            this.cboSubSchedaDocumenti.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSubSchedaDocumenti.DataSource = this.materialiLottiSelectCommandBindingSource;
            this.cboSubSchedaDocumenti.DisplayMember = "DenominazioneMateriale";
            this.cboSubSchedaDocumenti.Name = "cboSubSchedaDocumenti";
            this.cboSubSchedaDocumenti.PopupView = this.gridView6;
            this.cboSubSchedaDocumenti.ValueMember = "IDSchedaDocumenti";
            this.cboSubSchedaDocumenti.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.GridSize);
            // 
            // gridView6
            // 
            this.gridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView6.Name = "gridView6";
            this.gridView6.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView6.OptionsView.ShowGroupPanel = false;
            // 
            // colSubIDApparecchio
            // 
            this.colSubIDApparecchio.Caption = "Apparecchio";
            this.colSubIDApparecchio.ColumnEdit = this.cboSubApparecchio;
            this.colSubIDApparecchio.FieldName = "IDApparecchio";
            this.colSubIDApparecchio.Name = "colSubIDApparecchio";
            this.colSubIDApparecchio.Visible = true;
            this.colSubIDApparecchio.VisibleIndex = 8;
            // 
            // cboSubApparecchio
            // 
            this.cboSubApparecchio.AutoHeight = false;
            this.cboSubApparecchio.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSubApparecchio.DataSource = this.apparecchiSelectCommandBindingSource;
            this.cboSubApparecchio.DisplayMember = "NumeroApparecchio";
            this.cboSubApparecchio.Name = "cboSubApparecchio";
            this.cboSubApparecchio.PopupView = this.gridView7;
            this.cboSubApparecchio.ValueMember = "IDApparecchio";
            this.cboSubApparecchio.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.GridSizeApparecchi);
            // 
            // apparecchiSelectCommandBindingSource
            // 
            this.apparecchiSelectCommandBindingSource.DataMember = "ApparecchiSelectCommand";
            this.apparecchiSelectCommandBindingSource.DataSource = this.lupin;
            // 
            // gridView7
            // 
            this.gridView7.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView7.Name = "gridView7";
            this.gridView7.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView7.OptionsView.ShowGroupPanel = false;
            // 
            // colSubIDUtensile
            // 
            this.colSubIDUtensile.Caption = "Utensile";
            this.colSubIDUtensile.ColumnEdit = this.cboSubUtensile;
            this.colSubIDUtensile.FieldName = "IDUtensile";
            this.colSubIDUtensile.Name = "colSubIDUtensile";
            this.colSubIDUtensile.Visible = true;
            this.colSubIDUtensile.VisibleIndex = 9;
            // 
            // cboSubUtensile
            // 
            this.cboSubUtensile.AutoHeight = false;
            this.cboSubUtensile.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSubUtensile.DataSource = this.utensiliSelectCommandBindingSource;
            this.cboSubUtensile.DisplayMember = "Descrizione";
            this.cboSubUtensile.Name = "cboSubUtensile";
            this.cboSubUtensile.PopupView = this.gridView8;
            this.cboSubUtensile.ValueMember = "IDUtensile";
            this.cboSubUtensile.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.GridSize);
            // 
            // utensiliSelectCommandBindingSource
            // 
            this.utensiliSelectCommandBindingSource.DataMember = "UtensiliSelectCommand";
            this.utensiliSelectCommandBindingSource.DataSource = this.lupin;
            // 
            // gridView8
            // 
            this.gridView8.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView8.Name = "gridView8";
            this.gridView8.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView8.OptionsView.ShowGroupPanel = false;
            // 
            // colSubIDApparecchio2
            // 
            this.colSubIDApparecchio2.Caption = "Apparecchio 2";
            this.colSubIDApparecchio2.ColumnEdit = this.cboSubApparecchio2;
            this.colSubIDApparecchio2.FieldName = "IDApparecchio2";
            this.colSubIDApparecchio2.Name = "colSubIDApparecchio2";
            this.colSubIDApparecchio2.Visible = true;
            this.colSubIDApparecchio2.VisibleIndex = 10;
            // 
            // cboSubApparecchio2
            // 
            this.cboSubApparecchio2.AutoHeight = false;
            this.cboSubApparecchio2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSubApparecchio2.DataSource = this.apparecchiSelectCommandBindingSource;
            this.cboSubApparecchio2.DisplayMember = "NumeroApparecchio";
            this.cboSubApparecchio2.Name = "cboSubApparecchio2";
            this.cboSubApparecchio2.PopupView = this.gridView9;
            this.cboSubApparecchio2.ValueMember = "IDApparecchio";
            this.cboSubApparecchio2.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.GridSizeApparecchi);
            // 
            // gridView9
            // 
            this.gridView9.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView9.Name = "gridView9";
            this.gridView9.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView9.OptionsView.ShowGroupPanel = false;
            // 
            // colSubIDUtensile2
            // 
            this.colSubIDUtensile2.Caption = "Utensile 2";
            this.colSubIDUtensile2.ColumnEdit = this.cboSubUtensile2;
            this.colSubIDUtensile2.FieldName = "IDUtensile2";
            this.colSubIDUtensile2.Name = "colSubIDUtensile2";
            this.colSubIDUtensile2.Visible = true;
            this.colSubIDUtensile2.VisibleIndex = 11;
            // 
            // cboSubUtensile2
            // 
            this.cboSubUtensile2.AutoHeight = false;
            this.cboSubUtensile2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSubUtensile2.DataSource = this.utensiliSelectCommandBindingSource;
            this.cboSubUtensile2.DisplayMember = "Descrizione";
            this.cboSubUtensile2.Name = "cboSubUtensile2";
            this.cboSubUtensile2.PopupView = this.gridView10;
            this.cboSubUtensile2.ValueMember = "IDUtensile";
            this.cboSubUtensile2.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.GridSize);
            // 
            // gridView10
            // 
            this.gridView10.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView10.Name = "gridView10";
            this.gridView10.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView10.OptionsView.ShowGroupPanel = false;
            // 
            // colSubDefaultGiorniScadenza
            // 
            this.colSubDefaultGiorniScadenza.Caption = "Default Giorni Scadenza";
            this.colSubDefaultGiorniScadenza.FieldName = "DefaultGiorniScadenza";
            this.colSubDefaultGiorniScadenza.Name = "colSubDefaultGiorniScadenza";
            this.colSubDefaultGiorniScadenza.Visible = true;
            this.colSubDefaultGiorniScadenza.VisibleIndex = 12;
            // 
            // colSubDataScadenza
            // 
            this.colSubDataScadenza.Caption = "Data Scadenza";
            this.colSubDataScadenza.FieldName = "DataScadenza";
            this.colSubDataScadenza.Name = "colSubDataScadenza";
            this.colSubDataScadenza.Visible = true;
            this.colSubDataScadenza.VisibleIndex = 13;
            // 
            // colSubNotePrescrittive
            // 
            this.colSubNotePrescrittive.Caption = "Note Prescrittive";
            this.colSubNotePrescrittive.FieldName = "NotePrescrittive";
            this.colSubNotePrescrittive.Name = "colSubNotePrescrittive";
            this.colSubNotePrescrittive.Visible = true;
            this.colSubNotePrescrittive.VisibleIndex = 14;
            // 
            // colSubNoteDescrittive
            // 
            this.colSubNoteDescrittive.Caption = "Note Descrittive";
            this.colSubNoteDescrittive.FieldName = "NoteDescrittive";
            this.colSubNoteDescrittive.Name = "colSubNoteDescrittive";
            this.colSubNoteDescrittive.Visible = true;
            this.colSubNoteDescrittive.VisibleIndex = 15;
            // 
            // colSubDataCreazione
            // 
            this.colSubDataCreazione.Caption = "Data Creazione";
            this.colSubDataCreazione.FieldName = "DataCreazione";
            this.colSubDataCreazione.Name = "colSubDataCreazione";
            this.colSubDataCreazione.Visible = true;
            this.colSubDataCreazione.VisibleIndex = 16;
            // 
            // colSubIDUtente
            // 
            this.colSubIDUtente.Caption = "Utente Creazione";
            this.colSubIDUtente.ColumnEdit = this.cboSubUtente;
            this.colSubIDUtente.FieldName = "IDUtente";
            this.colSubIDUtente.Name = "colSubIDUtente";
            this.colSubIDUtente.Visible = true;
            this.colSubIDUtente.VisibleIndex = 17;
            // 
            // cboSubUtente
            // 
            this.cboSubUtente.AutoHeight = false;
            this.cboSubUtente.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSubUtente.DataSource = this.loginUtentiBindingSource1;
            this.cboSubUtente.DisplayMember = "NomeUtente";
            this.cboSubUtente.Name = "cboSubUtente";
            this.cboSubUtente.PopupView = this.gridView11;
            this.cboSubUtente.ValueMember = "IDUtente";
            // 
            // loginUtentiBindingSource1
            // 
            this.loginUtentiBindingSource1.DataMember = "Login_Utenti";
            this.loginUtentiBindingSource1.DataSource = this.soluzioni;
            // 
            // gridView11
            // 
            this.gridView11.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView11.Name = "gridView11";
            this.gridView11.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView11.OptionsView.ShowGroupPanel = false;
            // 
            // butAggiungi
            // 
            this.butAggiungi.Location = new System.Drawing.Point(1094, 837);
            this.butAggiungi.Name = "butAggiungi";
            this.butAggiungi.Size = new System.Drawing.Size(195, 22);
            this.butAggiungi.TabIndex = 33;
            this.butAggiungi.Text = "Aggiungi Gruppo Punti/Soluzioni";
            this.butAggiungi.Click += new System.EventHandler(this.butAggiungi_Click);
            // 
            // butScollega
            // 
            this.butScollega.Location = new System.Drawing.Point(491, 837);
            this.butScollega.Name = "butScollega";
            this.butScollega.Size = new System.Drawing.Size(195, 22);
            this.butScollega.TabIndex = 35;
            this.butScollega.Text = "Scollega Punto/Soluzione";
            this.butScollega.Click += new System.EventHandler(this.butScollega_Click);
            // 
            // butModifica
            // 
            this.butModifica.Location = new System.Drawing.Point(893, 837);
            this.butModifica.Name = "butModifica";
            this.butModifica.Size = new System.Drawing.Size(195, 22);
            this.butModifica.TabIndex = 34;
            this.butModifica.Text = "Modifica Punto/Soluzione";
            this.butModifica.Click += new System.EventHandler(this.butModifica_Click);
            // 
            // butScollegaEAnnulla
            // 
            this.butScollegaEAnnulla.Location = new System.Drawing.Point(692, 837);
            this.butScollegaEAnnulla.Name = "butScollegaEAnnulla";
            this.butScollegaEAnnulla.Size = new System.Drawing.Size(195, 22);
            this.butScollegaEAnnulla.TabIndex = 36;
            this.butScollegaEAnnulla.Text = "Scollega e Annulla Punto/Soluzione";
            this.butScollegaEAnnulla.Click += new System.EventHandler(this.butScollegaEAnnulla_Click);
            // 
            // butClose
            // 
            this.butClose.Location = new System.Drawing.Point(1095, 879);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(195, 22);
            this.butClose.TabIndex = 37;
            this.butClose.Text = "Chiudi";
            this.butClose.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // soluzioni_TipologiaTableAdapter
            // 
            this.soluzioni_TipologiaTableAdapter.ClearBeforeFill = true;
            // 
            // materialiLottiSelectCommandTableAdapter
            // 
            this.materialiLottiSelectCommandTableAdapter.ClearBeforeFill = true;
            // 
            // solventiTableAdapter
            // 
            this.solventiTableAdapter.ClearBeforeFill = true;
            // 
            // composti_StatiTableAdapter
            // 
            this.composti_StatiTableAdapter.ClearBeforeFill = true;
            // 
            // login_UtentiTableAdapter
            // 
            this.login_UtentiTableAdapter.ClearBeforeFill = true;
            // 
            // rette_TipologiaTableAdapter
            // 
            this.rette_TipologiaTableAdapter.ClearBeforeFill = true;
            // 
            // apparecchiSelectCommandTableAdapter
            // 
            this.apparecchiSelectCommandTableAdapter.ClearBeforeFill = true;
            // 
            // utensiliSelectCommandTableAdapter
            // 
            this.utensiliSelectCommandTableAdapter.ClearBeforeFill = true;
            // 
            // ubicazioniSelectCommandTableAdapter
            // 
            this.ubicazioniSelectCommandTableAdapter.ClearBeforeFill = true;
            // 
            // frmRetta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 913);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.butScollegaEAnnulla);
            this.Controls.Add(this.butAggiungi);
            this.Controls.Add(this.butScollega);
            this.Controls.Add(this.butModifica);
            this.Controls.Add(this.gridRetteDetails);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.gridRette);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmRetta";
            this.Text = "Rette di Taratura";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRetta_FormClosing);
            this.Load += new System.EventHandler(this.frmRetta_Load);
            this.Shown += new System.EventHandler(this.frmRetta_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewRette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCodiceRetta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipologia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retteTipologiaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.soluzioni)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNome.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGiorniScadenza.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataScadenza.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSingoloSolvente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialiLottiSelectCommandBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMiscelaSolventi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.solventiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.solventi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCreazione.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCreazione.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUtenteCreazione.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginUtentiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStato.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.compostiStatiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.soluzioniTipologiaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRetteDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewRetteDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubTipologia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.soluzioniTipologiaBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubStato)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.compostiStatiBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubUbicazione)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubicazioniSelectCommandBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubSolvente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.solventiBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubSchedaDocumenti)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubApparecchio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.apparecchiSelectCommandBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubUtensile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.utensiliSelectCommandBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubApparecchio2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubUtensile2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubUtente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginUtentiBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView11)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem tbNuovo;
        private DevExpress.XtraBars.BarButtonItem tbDuplica;
        private DevExpress.XtraBars.BarButtonItem tbSalva;
        private DevExpress.XtraBars.BarButtonItem tbAggiorna;
        private DevExpress.XtraBars.BarButtonItem tbStampa;
        private DevExpress.XtraBars.BarButtonItem tbStampaReport;
        private DevExpress.XtraBars.BarButtonItem tbEsportaExcel;
        private DevExpress.XtraBars.BarButtonItem tbVisualizzaLog;
        private DevExpress.XtraGrid.GridControl gridRette;
        private DevExpress.XtraGrid.Views.Grid.GridView gviewRette;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit txtCodiceRetta;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.GridLookUpEdit cboTipologia;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.TextEdit txtNome;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TextEdit txtGiorniScadenza;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit txtDataScadenza;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.GridLookUpEdit cboSingoloSolvente;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GridLookUpEdit cboMiscelaSolventi;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.TextEdit txtNote;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.DateEdit txtDataCreazione;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.LookUpEdit cboUtenteCreazione;
        private DevExpress.XtraEditors.GridLookUpEdit cboStato;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.GridControl gridRetteDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gviewRetteDetails;
        private DevExpress.XtraEditors.SimpleButton butScollegaEAnnulla;
        private DevExpress.XtraEditors.SimpleButton butAggiungi;
        private DevExpress.XtraEditors.SimpleButton butScollega;
        private DevExpress.XtraEditors.SimpleButton butModifica;
        private DevExpress.XtraEditors.SimpleButton butClose;
        private Soluzioni soluzioni;
        private System.Windows.Forms.BindingSource soluzioniTipologiaBindingSource;
        private SoluzioniTableAdapters.Soluzioni_TipologiaTableAdapter soluzioni_TipologiaTableAdapter;
        private Lupin lupin;
        private System.Windows.Forms.BindingSource materialiLottiSelectCommandBindingSource;
        private LupinTableAdapters.MaterialiLottiSelectCommandTableAdapter materialiLottiSelectCommandTableAdapter;
        private Solventi solventi;
        private System.Windows.Forms.BindingSource solventiBindingSource;
        private SolventiTableAdapters.SolventiTableAdapter solventiTableAdapter;
        private System.Windows.Forms.BindingSource compostiStatiBindingSource;
        private SoluzioniTableAdapters.Composti_StatiTableAdapter composti_StatiTableAdapter;
        private System.Windows.Forms.BindingSource loginUtentiBindingSource;
        private SoluzioniTableAdapters.Login_UtentiTableAdapter login_UtentiTableAdapter;
        private System.Windows.Forms.BindingSource retteTipologiaBindingSource;
        private SoluzioniTableAdapters.Rette_TipologiaTableAdapter rette_TipologiaTableAdapter;
        private DevExpress.XtraGrid.Columns.GridColumn colIDRetta;
        private DevExpress.XtraGrid.Columns.GridColumn colCodiceRetta;
        private DevExpress.XtraGrid.Columns.GridColumn colTipologia;
        private DevExpress.XtraGrid.Columns.GridColumn colNome;
        private DevExpress.XtraGrid.Columns.GridColumn colStato;
        private DevExpress.XtraGrid.Columns.GridColumn colSolvente;
        private DevExpress.XtraGrid.Columns.GridColumn colMiscelaSolventi;
        private DevExpress.XtraGrid.Columns.GridColumn colDefaultGiorniScadenza;
        private DevExpress.XtraGrid.Columns.GridColumn colDataScadenza;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraGrid.Columns.GridColumn colDataCreazione;
        private DevExpress.XtraGrid.Columns.GridColumn colSubIDSoluzione;
        private DevExpress.XtraGrid.Columns.GridColumn colSubCodiceSoluzione;
        private DevExpress.XtraGrid.Columns.GridColumn colUtenteCreazione;
        private DevExpress.XtraGrid.Columns.GridColumn colSubNome;
        private DevExpress.XtraGrid.Columns.GridColumn colSubIDStato;
        private DevExpress.XtraGrid.Columns.GridColumn colSubUMVolumeFinale;
        private DevExpress.XtraGrid.Columns.GridColumn colSubVolumeFinale;
        private DevExpress.XtraGrid.Columns.GridColumn colSubIDSolvente;
        private DevExpress.XtraGrid.Columns.GridColumn colSubIDSchedaDocumenti;
        private DevExpress.XtraGrid.Columns.GridColumn colSubIDApparecchio;
        private DevExpress.XtraGrid.Columns.GridColumn colSubIDUtensile;
        private DevExpress.XtraGrid.Columns.GridColumn colSubIDApparecchio2;
        private DevExpress.XtraGrid.Columns.GridColumn colSubIDUtensile2;
        private DevExpress.XtraGrid.Columns.GridColumn colSubDefaultGiorniScadenza;
        private DevExpress.XtraGrid.Columns.GridColumn colSubDataScadenza;
        private DevExpress.XtraGrid.Columns.GridColumn colSubNotePrescrittive;
        private DevExpress.XtraGrid.Columns.GridColumn colSubNoteDescrittive;
        private DevExpress.XtraGrid.Columns.GridColumn colSubDataCreazione;
        private DevExpress.XtraGrid.Columns.GridColumn colSubIDUtente;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit cboSubTipologia;
        private System.Windows.Forms.BindingSource soluzioniTipologiaBindingSource1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit cboSubStato;
        private System.Windows.Forms.BindingSource compostiStatiBindingSource1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit cboSubSolvente;
        private System.Windows.Forms.BindingSource solventiBindingSource1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit cboSubSchedaDocumenti;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit cboSubApparecchio;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView7;
        private System.Windows.Forms.BindingSource apparecchiSelectCommandBindingSource;
        private LupinTableAdapters.ApparecchiSelectCommandTableAdapter apparecchiSelectCommandTableAdapter;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit cboSubUtensile;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView8;
        private System.Windows.Forms.BindingSource utensiliSelectCommandBindingSource;
        private LupinTableAdapters.UtensiliSelectCommandTableAdapter utensiliSelectCommandTableAdapter;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit cboSubApparecchio2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView9;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit cboSubUtensile2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView10;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit cboSubUtente;
        private System.Windows.Forms.BindingSource loginUtentiBindingSource1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView11;
        private DevExpress.XtraGrid.Columns.GridColumn colSubIDUbicazione;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit cboSubUbicazione;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView12;
        private System.Windows.Forms.BindingSource ubicazioniSelectCommandBindingSource;
        private LupinTableAdapters.UbicazioniSelectCommandTableAdapter ubicazioniSelectCommandTableAdapter;
        private DevExpress.XtraGrid.Columns.GridColumn colSubTipologia;
    }
}