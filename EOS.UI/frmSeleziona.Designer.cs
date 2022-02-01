
namespace EOS.UI
{
    partial class frmSeleziona
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSeleziona));
            this.butAnnulla = new DevExpress.XtraEditors.SimpleButton();
            this.butOKMAterialiMR = new DevExpress.XtraEditors.SimpleButton();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.burOKSoluzioni = new DevExpress.XtraEditors.SimpleButton();
            this.gridSoluzioni = new DevExpress.XtraGrid.GridControl();
            this.gviewSoluzioni = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colsolIDSoluzione = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsolCodiceSoluzione = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsolTipologiaSoluzione = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsolNome = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsolDataPreparazione = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsolDataScadenza = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.butOKMisceleSolventi = new DevExpress.XtraEditors.SimpleButton();
            this.gridMisceleSolventi = new DevExpress.XtraGrid.GridControl();
            this.gviewMisceleSolventi = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colsovIDSolvente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsovCodiceSolvente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsovTipologiaSolvente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsovNome = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsovDataPreparazione = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsovDataScadenza = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridMaterialiMR = new DevExpress.XtraGrid.GridControl();
            this.gviewMaterialiMR = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDSchedaDocumenti = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIdentificativo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLotto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDenominazioneProdotto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCAS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataInserimento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataScadenza = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboTipoMaterialeMR = new DevExpress.XtraEditors.GridLookUpEdit();
            this.materialeTipologiaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.soluzioni = new EOS.UI.Soluzioni();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.materiale_TipologiaTableAdapter = new EOS.UI.SoluzioniTableAdapters.Materiale_TipologiaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSoluzioni)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewSoluzioni)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMisceleSolventi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewMisceleSolventi)).BeginInit();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMaterialiMR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewMaterialiMR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoMaterialeMR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialeTipologiaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.soluzioni)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // butAnnulla
            // 
            this.butAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butAnnulla.Location = new System.Drawing.Point(663, 455);
            this.butAnnulla.Name = "butAnnulla";
            this.butAnnulla.Size = new System.Drawing.Size(100, 22);
            this.butAnnulla.TabIndex = 6;
            this.butAnnulla.Text = "Chiudi";
            this.butAnnulla.Click += new System.EventHandler(this.butAnnulla_Click);
            // 
            // butOKMAterialiMR
            // 
            this.butOKMAterialiMR.Location = new System.Drawing.Point(638, 367);
            this.butOKMAterialiMR.Name = "butOKMAterialiMR";
            this.butOKMAterialiMR.Size = new System.Drawing.Size(100, 22);
            this.butOKMAterialiMR.TabIndex = 5;
            this.butOKMAterialiMR.Text = "OK";
            this.butOKMAterialiMR.Click += new System.EventHandler(this.butOK_Click);
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 12);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(743, 416);
            this.xtraTabControl1.TabIndex = 8;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.burOKSoluzioni);
            this.xtraTabPage1.Controls.Add(this.gridSoluzioni);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(737, 388);
            this.xtraTabPage1.Text = "Soluzioni MR";
            // 
            // burOKSoluzioni
            // 
            this.burOKSoluzioni.Location = new System.Drawing.Point(638, 366);
            this.burOKSoluzioni.Name = "burOKSoluzioni";
            this.burOKSoluzioni.Size = new System.Drawing.Size(100, 22);
            this.burOKSoluzioni.TabIndex = 7;
            this.burOKSoluzioni.Text = "OK";
            this.burOKSoluzioni.Click += new System.EventHandler(this.burOKSoluzioni_Click);
            // 
            // gridSoluzioni
            // 
            this.gridSoluzioni.Location = new System.Drawing.Point(3, 3);
            this.gridSoluzioni.MainView = this.gviewSoluzioni;
            this.gridSoluzioni.Name = "gridSoluzioni";
            this.gridSoluzioni.Size = new System.Drawing.Size(735, 357);
            this.gridSoluzioni.TabIndex = 0;
            this.gridSoluzioni.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gviewSoluzioni});
            // 
            // gviewSoluzioni
            // 
            this.gviewSoluzioni.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colsolIDSoluzione,
            this.colsolCodiceSoluzione,
            this.colsolTipologiaSoluzione,
            this.colsolNome,
            this.colsolDataPreparazione,
            this.colsolDataScadenza});
            this.gviewSoluzioni.GridControl = this.gridSoluzioni;
            this.gviewSoluzioni.Name = "gviewSoluzioni";
            this.gviewSoluzioni.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gviewSoluzioni.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gviewSoluzioni.OptionsBehavior.ReadOnly = true;
            this.gviewSoluzioni.OptionsView.ShowAutoFilterRow = true;
            // 
            // colsolIDSoluzione
            // 
            this.colsolIDSoluzione.Caption = "ID Soluzione";
            this.colsolIDSoluzione.FieldName = "IDSoluzione";
            this.colsolIDSoluzione.Name = "colsolIDSoluzione";
            this.colsolIDSoluzione.Visible = true;
            this.colsolIDSoluzione.VisibleIndex = 0;
            // 
            // colsolCodiceSoluzione
            // 
            this.colsolCodiceSoluzione.Caption = "Codice Soluzione MR";
            this.colsolCodiceSoluzione.FieldName = "CodiceSoluzione";
            this.colsolCodiceSoluzione.Name = "colsolCodiceSoluzione";
            this.colsolCodiceSoluzione.Visible = true;
            this.colsolCodiceSoluzione.VisibleIndex = 1;
            // 
            // colsolTipologiaSoluzione
            // 
            this.colsolTipologiaSoluzione.Caption = "Tipologia Soluzione MR";
            this.colsolTipologiaSoluzione.FieldName = "TipologiaSoluzione";
            this.colsolTipologiaSoluzione.Name = "colsolTipologiaSoluzione";
            this.colsolTipologiaSoluzione.Visible = true;
            this.colsolTipologiaSoluzione.VisibleIndex = 2;
            // 
            // colsolNome
            // 
            this.colsolNome.Caption = "Nome";
            this.colsolNome.FieldName = "Nome";
            this.colsolNome.Name = "colsolNome";
            this.colsolNome.Visible = true;
            this.colsolNome.VisibleIndex = 3;
            // 
            // colsolDataPreparazione
            // 
            this.colsolDataPreparazione.Caption = "Data Preparazione";
            this.colsolDataPreparazione.FieldName = "DataPreparazione";
            this.colsolDataPreparazione.Name = "colsolDataPreparazione";
            this.colsolDataPreparazione.Visible = true;
            this.colsolDataPreparazione.VisibleIndex = 4;
            // 
            // colsolDataScadenza
            // 
            this.colsolDataScadenza.Caption = "Data Scadenza";
            this.colsolDataScadenza.FieldName = "DataScadenza";
            this.colsolDataScadenza.Name = "colsolDataScadenza";
            this.colsolDataScadenza.Visible = true;
            this.colsolDataScadenza.VisibleIndex = 5;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.butOKMisceleSolventi);
            this.xtraTabPage2.Controls.Add(this.gridMisceleSolventi);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(737, 388);
            this.xtraTabPage2.Text = "Soluzioni di Lavoro";
            // 
            // butOKMisceleSolventi
            // 
            this.butOKMisceleSolventi.Location = new System.Drawing.Point(638, 366);
            this.butOKMisceleSolventi.Name = "butOKMisceleSolventi";
            this.butOKMisceleSolventi.Size = new System.Drawing.Size(100, 22);
            this.butOKMisceleSolventi.TabIndex = 6;
            this.butOKMisceleSolventi.Text = "OK";
            this.butOKMisceleSolventi.Click += new System.EventHandler(this.butOKMisceleSolventi_Click);
            // 
            // gridMisceleSolventi
            // 
            this.gridMisceleSolventi.Location = new System.Drawing.Point(3, 3);
            this.gridMisceleSolventi.MainView = this.gviewMisceleSolventi;
            this.gridMisceleSolventi.Name = "gridMisceleSolventi";
            this.gridMisceleSolventi.Size = new System.Drawing.Size(735, 357);
            this.gridMisceleSolventi.TabIndex = 0;
            this.gridMisceleSolventi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gviewMisceleSolventi});
            // 
            // gviewMisceleSolventi
            // 
            this.gviewMisceleSolventi.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colsovIDSolvente,
            this.colsovCodiceSolvente,
            this.colsovTipologiaSolvente,
            this.colsovNome,
            this.colsovDataPreparazione,
            this.colsovDataScadenza});
            this.gviewMisceleSolventi.GridControl = this.gridMisceleSolventi;
            this.gviewMisceleSolventi.Name = "gviewMisceleSolventi";
            this.gviewMisceleSolventi.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gviewMisceleSolventi.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gviewMisceleSolventi.OptionsBehavior.ReadOnly = true;
            this.gviewMisceleSolventi.OptionsView.ShowAutoFilterRow = true;
            // 
            // colsovIDSolvente
            // 
            this.colsovIDSolvente.Caption = "ID Soluzione di Lavoro";
            this.colsovIDSolvente.FieldName = "IDSolvente";
            this.colsovIDSolvente.Name = "colsovIDSolvente";
            this.colsovIDSolvente.Visible = true;
            this.colsovIDSolvente.VisibleIndex = 0;
            // 
            // colsovCodiceSolvente
            // 
            this.colsovCodiceSolvente.Caption = "Codice Soluzione di Lavoro";
            this.colsovCodiceSolvente.FieldName = "CodiceSolvente";
            this.colsovCodiceSolvente.Name = "colsovCodiceSolvente";
            this.colsovCodiceSolvente.Visible = true;
            this.colsovCodiceSolvente.VisibleIndex = 1;
            // 
            // colsovTipologiaSolvente
            // 
            this.colsovTipologiaSolvente.Caption = "Tipologia Soluzione di Lavoro";
            this.colsovTipologiaSolvente.FieldName = "TipologiaSolvente";
            this.colsovTipologiaSolvente.Name = "colsovTipologiaSolvente";
            this.colsovTipologiaSolvente.Visible = true;
            this.colsovTipologiaSolvente.VisibleIndex = 5;
            // 
            // colsovNome
            // 
            this.colsovNome.Caption = "Nome";
            this.colsovNome.FieldName = "Nome";
            this.colsovNome.Name = "colsovNome";
            this.colsovNome.Visible = true;
            this.colsovNome.VisibleIndex = 2;
            // 
            // colsovDataPreparazione
            // 
            this.colsovDataPreparazione.Caption = "Data Preparazione";
            this.colsovDataPreparazione.FieldName = "DataPreparazione";
            this.colsovDataPreparazione.Name = "colsovDataPreparazione";
            this.colsovDataPreparazione.Visible = true;
            this.colsovDataPreparazione.VisibleIndex = 3;
            // 
            // colsovDataScadenza
            // 
            this.colsovDataScadenza.Caption = "Data Scadenza";
            this.colsovDataScadenza.FieldName = "DataScadenza";
            this.colsovDataScadenza.Name = "colsovDataScadenza";
            this.colsovDataScadenza.Visible = true;
            this.colsovDataScadenza.VisibleIndex = 4;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.labelControl1);
            this.xtraTabPage3.Controls.Add(this.butOKMAterialiMR);
            this.xtraTabPage3.Controls.Add(this.gridMaterialiMR);
            this.xtraTabPage3.Controls.Add(this.cboTipoMaterialeMR);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(737, 388);
            this.xtraTabPage3.Text = "Articoli da Anagrafica";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(204, 371);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(129, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Tipo Articolo da Anagrafica";
            // 
            // gridMaterialiMR
            // 
            this.gridMaterialiMR.Location = new System.Drawing.Point(3, 3);
            this.gridMaterialiMR.MainView = this.gviewMaterialiMR;
            this.gridMaterialiMR.Name = "gridMaterialiMR";
            this.gridMaterialiMR.Size = new System.Drawing.Size(735, 358);
            this.gridMaterialiMR.TabIndex = 0;
            this.gridMaterialiMR.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gviewMaterialiMR});
            // 
            // gviewMaterialiMR
            // 
            this.gviewMaterialiMR.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDSchedaDocumenti,
            this.colIdentificativo,
            this.colLotto,
            this.colDenominazioneProdotto,
            this.colCAS,
            this.colDataInserimento,
            this.colDataScadenza});
            this.gviewMaterialiMR.GridControl = this.gridMaterialiMR;
            this.gviewMaterialiMR.Name = "gviewMaterialiMR";
            this.gviewMaterialiMR.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gviewMaterialiMR.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gviewMaterialiMR.OptionsBehavior.ReadOnly = true;
            this.gviewMaterialiMR.OptionsView.ShowAutoFilterRow = true;
            // 
            // colIDSchedaDocumenti
            // 
            this.colIDSchedaDocumenti.Caption = "ID Scheda Documenti";
            this.colIDSchedaDocumenti.FieldName = "IDSchedaDocumenti";
            this.colIDSchedaDocumenti.Name = "colIDSchedaDocumenti";
            this.colIDSchedaDocumenti.Visible = true;
            this.colIDSchedaDocumenti.VisibleIndex = 0;
            // 
            // colIdentificativo
            // 
            this.colIdentificativo.Caption = "Identificativo";
            this.colIdentificativo.FieldName = "Identificativo";
            this.colIdentificativo.Name = "colIdentificativo";
            this.colIdentificativo.Visible = true;
            this.colIdentificativo.VisibleIndex = 1;
            // 
            // colLotto
            // 
            this.colLotto.Caption = "Lotto";
            this.colLotto.FieldName = "Lotto";
            this.colLotto.Name = "colLotto";
            this.colLotto.Visible = true;
            this.colLotto.VisibleIndex = 2;
            // 
            // colDenominazioneProdotto
            // 
            this.colDenominazioneProdotto.Caption = "DenominazioneProdotto";
            this.colDenominazioneProdotto.FieldName = "DenominazioneProdotto";
            this.colDenominazioneProdotto.Name = "colDenominazioneProdotto";
            this.colDenominazioneProdotto.Visible = true;
            this.colDenominazioneProdotto.VisibleIndex = 3;
            // 
            // colCAS
            // 
            this.colCAS.Caption = "CAS";
            this.colCAS.FieldName = "CAS";
            this.colCAS.Name = "colCAS";
            this.colCAS.Visible = true;
            this.colCAS.VisibleIndex = 4;
            // 
            // colDataInserimento
            // 
            this.colDataInserimento.Caption = "Data Inserimento";
            this.colDataInserimento.FieldName = "DataInserimento";
            this.colDataInserimento.Name = "colDataInserimento";
            this.colDataInserimento.Visible = true;
            this.colDataInserimento.VisibleIndex = 5;
            // 
            // colDataScadenza
            // 
            this.colDataScadenza.Caption = "Data Scadenza";
            this.colDataScadenza.FieldName = "DataScadenza";
            this.colDataScadenza.Name = "colDataScadenza";
            this.colDataScadenza.Visible = true;
            this.colDataScadenza.VisibleIndex = 6;
            // 
            // cboTipoMaterialeMR
            // 
            this.cboTipoMaterialeMR.Location = new System.Drawing.Point(339, 368);
            this.cboTipoMaterialeMR.Name = "cboTipoMaterialeMR";
            this.cboTipoMaterialeMR.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTipoMaterialeMR.Properties.DataSource = this.materialeTipologiaBindingSource;
            this.cboTipoMaterialeMR.Properties.DisplayMember = "Nome";
            this.cboTipoMaterialeMR.Properties.NullText = "";
            this.cboTipoMaterialeMR.Properties.PopupView = this.gridLookUpEdit1View;
            this.cboTipoMaterialeMR.Properties.ValueMember = "ID";
            this.cboTipoMaterialeMR.Size = new System.Drawing.Size(293, 20);
            this.cboTipoMaterialeMR.TabIndex = 1;
            // 
            // materialeTipologiaBindingSource
            // 
            this.materialeTipologiaBindingSource.DataMember = "Materiale_Tipologia";
            this.materialeTipologiaBindingSource.DataSource = this.soluzioni;
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
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(767, 440);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.xtraTabControl1;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(747, 420);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.xtraTabControl1);
            this.layoutControl1.Location = new System.Drawing.Point(12, 12);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(837, 178, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(767, 440);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // materiale_TipologiaTableAdapter
            // 
            this.materiale_TipologiaTableAdapter.ClearBeforeFill = true;
            // 
            // frmSeleziona
            // 
            this.AcceptButton = this.butOKMAterialiMR;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CancelButton = this.butAnnulla;
            this.ClientSize = new System.Drawing.Size(791, 489);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.butAnnulla);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSeleziona";
            this.Text = "Selezione Record";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSeleziona_FormClosing);
            this.Load += new System.EventHandler(this.SelezionaRecord_Load);
            this.Shown += new System.EventHandler(this.SelezionaRecord_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSoluzioni)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewSoluzioni)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMisceleSolventi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewMisceleSolventi)).EndInit();
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMaterialiMR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewMaterialiMR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoMaterialeMR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialeTipologiaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.soluzioni)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.SimpleButton butOKMAterialiMR;
        private DevExpress.XtraEditors.SimpleButton butAnnulla;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl gridSoluzioni;
        private DevExpress.XtraGrid.Views.Grid.GridView gviewSoluzioni;
        private DevExpress.XtraGrid.GridControl gridMisceleSolventi;
        private DevExpress.XtraGrid.Views.Grid.GridView gviewMisceleSolventi;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gridMaterialiMR;
        private DevExpress.XtraGrid.Views.Grid.GridView gviewMaterialiMR;
        private Soluzioni soluzioni;
        private System.Windows.Forms.BindingSource materialeTipologiaBindingSource;
        private SoluzioniTableAdapters.Materiale_TipologiaTableAdapter materiale_TipologiaTableAdapter;
        private DevExpress.XtraEditors.SimpleButton burOKSoluzioni;
        private DevExpress.XtraEditors.SimpleButton butOKMisceleSolventi;
        private DevExpress.XtraEditors.GridLookUpEdit cboTipoMaterialeMR;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colsovIDSolvente;
        private DevExpress.XtraGrid.Columns.GridColumn colsovCodiceSolvente;
        private DevExpress.XtraGrid.Columns.GridColumn colsovNome;
        private DevExpress.XtraGrid.Columns.GridColumn colsovDataPreparazione;
        private DevExpress.XtraGrid.Columns.GridColumn colsovDataScadenza;
        private DevExpress.XtraGrid.Columns.GridColumn colIDSchedaDocumenti;
        private DevExpress.XtraGrid.Columns.GridColumn colIdentificativo;
        private DevExpress.XtraGrid.Columns.GridColumn colLotto;
        private DevExpress.XtraGrid.Columns.GridColumn colDenominazioneProdotto;
        private DevExpress.XtraGrid.Columns.GridColumn colCAS;
        private DevExpress.XtraGrid.Columns.GridColumn colDataInserimento;
        private DevExpress.XtraGrid.Columns.GridColumn colDataScadenza;
        private DevExpress.XtraGrid.Columns.GridColumn colsolIDSoluzione;
        private DevExpress.XtraGrid.Columns.GridColumn colsolCodiceSoluzione;
        private DevExpress.XtraGrid.Columns.GridColumn colsolTipologiaSoluzione;
        private DevExpress.XtraGrid.Columns.GridColumn colsolNome;
        private DevExpress.XtraGrid.Columns.GridColumn colsolDataPreparazione;
        private DevExpress.XtraGrid.Columns.GridColumn colsolDataScadenza;
        private DevExpress.XtraGrid.Columns.GridColumn colsovTipologiaSolvente;
    }
}