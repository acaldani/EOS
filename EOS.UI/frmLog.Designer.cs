
namespace EOS.UI
{
    partial class frmLog
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
            this.gridLog = new DevExpress.XtraGrid.GridControl();
            this.gviewLog = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTipoOperazione = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repMemo = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colTabella = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCodiceSoluzione = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDettaglioOperazione = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNomeUtente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataOperazione = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repMemo)).BeginInit();
            this.SuspendLayout();
            // 
            // gridLog
            // 
            this.gridLog.Location = new System.Drawing.Point(12, 12);
            this.gridLog.MainView = this.gviewLog;
            this.gridLog.Name = "gridLog";
            this.gridLog.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repMemo});
            this.gridLog.Size = new System.Drawing.Size(870, 551);
            this.gridLog.TabIndex = 0;
            this.gridLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gviewLog});
            // 
            // gviewLog
            // 
            this.gviewLog.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTipoOperazione,
            this.colTabella,
            this.colCodiceSoluzione,
            this.colDettaglioOperazione,
            this.colNomeUtente,
            this.colDataOperazione});
            this.gviewLog.GridControl = this.gridLog;
            this.gviewLog.Name = "gviewLog";
            this.gviewLog.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gviewLog.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gviewLog.OptionsBehavior.Editable = false;
            this.gviewLog.OptionsView.RowAutoHeight = true;
            this.gviewLog.OptionsView.ShowGroupPanel = false;
            this.gviewLog.DoubleClick += new System.EventHandler(this.gviewLog_DoubleClick);
            // 
            // colTipoOperazione
            // 
            this.colTipoOperazione.Caption = "Tipo Operazione";
            this.colTipoOperazione.ColumnEdit = this.repMemo;
            this.colTipoOperazione.FieldName = "TipoOperazione";
            this.colTipoOperazione.Name = "colTipoOperazione";
            this.colTipoOperazione.Visible = true;
            this.colTipoOperazione.VisibleIndex = 0;
            // 
            // repMemo
            // 
            this.repMemo.Name = "repMemo";
            // 
            // colTabella
            // 
            this.colTabella.Caption = "Tabella";
            this.colTabella.FieldName = "Tabella";
            this.colTabella.Name = "colTabella";
            this.colTabella.Visible = true;
            this.colTabella.VisibleIndex = 1;
            // 
            // colCodiceSoluzione
            // 
            this.colCodiceSoluzione.Caption = "Codice Soluzione";
            this.colCodiceSoluzione.FieldName = "CodiceSoluzione";
            this.colCodiceSoluzione.Name = "colCodiceSoluzione";
            this.colCodiceSoluzione.Visible = true;
            this.colCodiceSoluzione.VisibleIndex = 2;
            // 
            // colDettaglioOperazione
            // 
            this.colDettaglioOperazione.Caption = "Dettaglio Operazione";
            this.colDettaglioOperazione.FieldName = "Dettaglio Operazione";
            this.colDettaglioOperazione.Name = "colDettaglioOperazione";
            this.colDettaglioOperazione.Visible = true;
            this.colDettaglioOperazione.VisibleIndex = 3;
            // 
            // colNomeUtente
            // 
            this.colNomeUtente.Caption = "Nome Utente";
            this.colNomeUtente.FieldName = "NomeUtente";
            this.colNomeUtente.Name = "colNomeUtente";
            this.colNomeUtente.Visible = true;
            this.colNomeUtente.VisibleIndex = 4;
            // 
            // colDataOperazione
            // 
            this.colDataOperazione.Caption = "Data Operazione";
            this.colDataOperazione.FieldName = "DataOperazione";
            this.colDataOperazione.Name = "colDataOperazione";
            this.colDataOperazione.Visible = true;
            this.colDataOperazione.VisibleIndex = 5;
            // 
            // frmLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 575);
            this.Controls.Add(this.gridLog);
            this.Name = "frmLog";
            this.Text = "Visualizza Log";
            this.Load += new System.EventHandler(this.frmLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repMemo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridLog;
        private DevExpress.XtraGrid.Views.Grid.GridView gviewLog;
        private DevExpress.XtraGrid.Columns.GridColumn colTipoOperazione;
        private DevExpress.XtraGrid.Columns.GridColumn colTabella;
        private DevExpress.XtraGrid.Columns.GridColumn colCodiceSoluzione;
        private DevExpress.XtraGrid.Columns.GridColumn colDettaglioOperazione;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repMemo;
        private DevExpress.XtraGrid.Columns.GridColumn colNomeUtente;
        private DevExpress.XtraGrid.Columns.GridColumn colDataOperazione;
    }
}