
namespace EOS.UI
{
    partial class FrmRettaPuntiSelezioneComponenti
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
            this.gridComponenti = new DevExpress.XtraGrid.GridControl();
            this.gviewComponenti = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.butCancellaComponente = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.butAggiungiComponente = new DevExpress.XtraEditors.SimpleButton();
            this.butCreaSoluzioni = new DevExpress.XtraEditors.SimpleButton();
            this.cboNumeroPunti = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridComponenti)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewComponenti)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNumeroPunti.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridComponenti
            // 
            this.gridComponenti.Location = new System.Drawing.Point(12, 36);
            this.gridComponenti.MainView = this.gviewComponenti;
            this.gridComponenti.Name = "gridComponenti";
            this.gridComponenti.Size = new System.Drawing.Size(1074, 272);
            this.gridComponenti.TabIndex = 0;
            this.gridComponenti.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gviewComponenti});
            // 
            // gviewComponenti
            // 
            this.gviewComponenti.GridControl = this.gridComponenti;
            this.gviewComponenti.Name = "gviewComponenti";
            this.gviewComponenti.OptionsView.ShowGroupPanel = false;
            // 
            // butCancellaComponente
            // 
            this.butCancellaComponente.Location = new System.Drawing.Point(132, 314);
            this.butCancellaComponente.Name = "butCancellaComponente";
            this.butCancellaComponente.Size = new System.Drawing.Size(114, 23);
            this.butCancellaComponente.TabIndex = 2;
            this.butCancellaComponente.Text = "Cancella Componente";
            this.butCancellaComponente.Click += new System.EventHandler(this.butCancellaComponente_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(972, 314);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(114, 23);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "Chiudi";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // butAggiungiComponente
            // 
            this.butAggiungiComponente.Location = new System.Drawing.Point(12, 314);
            this.butAggiungiComponente.Name = "butAggiungiComponente";
            this.butAggiungiComponente.Size = new System.Drawing.Size(114, 23);
            this.butAggiungiComponente.TabIndex = 5;
            this.butAggiungiComponente.Text = "Aggiungi Componente";
            this.butAggiungiComponente.Click += new System.EventHandler(this.butAggiungiComponente_Click);
            // 
            // butCreaSoluzioni
            // 
            this.butCreaSoluzioni.Location = new System.Drawing.Point(372, 315);
            this.butCreaSoluzioni.Name = "butCreaSoluzioni";
            this.butCreaSoluzioni.Size = new System.Drawing.Size(114, 23);
            this.butCreaSoluzioni.TabIndex = 6;
            this.butCreaSoluzioni.Text = "Crea Soluzioni";
            this.butCreaSoluzioni.Click += new System.EventHandler(this.butCreaSoluzioni_Click);
            // 
            // cboNumeroPunti
            // 
            this.cboNumeroPunti.Location = new System.Drawing.Point(252, 316);
            this.cboNumeroPunti.Name = "cboNumeroPunti";
            this.cboNumeroPunti.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboNumeroPunti.Properties.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cboNumeroPunti.Size = new System.Drawing.Size(114, 20);
            this.cboNumeroPunti.TabIndex = 7;
            // 
            // FrmRettaPuntiSelezioneComponenti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 350);
            this.Controls.Add(this.cboNumeroPunti);
            this.Controls.Add(this.butCreaSoluzioni);
            this.Controls.Add(this.butAggiungiComponente);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.butCancellaComponente);
            this.Controls.Add(this.gridComponenti);
            this.IconOptions.Image = global::EOS.UI.Properties.Resources.download;
            this.Name = "FrmRettaPuntiSelezioneComponenti";
            this.Text = "Seleziona Componenti Gruppo Punti";
            this.Load += new System.EventHandler(this.FrmRettaPuntiSelezioneComponenti_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridComponenti)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewComponenti)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNumeroPunti.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridComponenti;
        private DevExpress.XtraGrid.Views.Grid.GridView gviewComponenti;
        private DevExpress.XtraEditors.SimpleButton butCancellaComponente;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton butAggiungiComponente;
        private DevExpress.XtraEditors.SimpleButton butCreaSoluzioni;
        private DevExpress.XtraEditors.ComboBoxEdit cboNumeroPunti;
    }
}