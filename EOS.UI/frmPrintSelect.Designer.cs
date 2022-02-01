
namespace EOS.UI
{
    partial class frmPrintSelect
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
            this.txtCopie = new DevExpress.XtraEditors.TextEdit();
            this.cboStampante = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.butOK = new DevExpress.XtraEditors.SimpleButton();
            this.butAnnulla = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtCopie.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStampante.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCopie
            // 
            this.txtCopie.Location = new System.Drawing.Point(74, 8);
            this.txtCopie.Name = "txtCopie";
            this.txtCopie.Properties.BeepOnError = false;
            this.txtCopie.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtCopie.Properties.MaskSettings.Set("mask", "d");
            this.txtCopie.Size = new System.Drawing.Size(245, 20);
            this.txtCopie.TabIndex = 0;
            // 
            // cboStampante
            // 
            this.cboStampante.Location = new System.Drawing.Point(74, 34);
            this.cboStampante.Name = "cboStampante";
            this.cboStampante.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStampante.Size = new System.Drawing.Size(245, 20);
            this.cboStampante.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(27, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Copie";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 37);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Stampante";
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(157, 60);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 4;
            this.butOK.Text = "OK";
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // butAnnulla
            // 
            this.butAnnulla.Location = new System.Drawing.Point(244, 60);
            this.butAnnulla.Name = "butAnnulla";
            this.butAnnulla.Size = new System.Drawing.Size(75, 23);
            this.butAnnulla.TabIndex = 5;
            this.butAnnulla.Text = "Annulla";
            this.butAnnulla.Click += new System.EventHandler(this.butAnnulla_Click);
            // 
            // frmPrintSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(332, 99);
            this.Controls.Add(this.butAnnulla);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cboStampante);
            this.Controls.Add(this.txtCopie);
            this.Name = "frmPrintSelect";
            this.Text = "frmPrintSelect";
            this.Load += new System.EventHandler(this.frmPrintSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCopie.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStampante.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtCopie;
        private DevExpress.XtraEditors.ComboBoxEdit cboStampante;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton butOK;
        private DevExpress.XtraEditors.SimpleButton butAnnulla;
    }
}