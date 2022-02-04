
namespace EOS.UI
{
    partial class frmMemo
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
            this.memoDettaglioOperazione = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDettaglioOperazione.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // memoDettaglioOperazione
            // 
            this.memoDettaglioOperazione.Location = new System.Drawing.Point(12, 12);
            this.memoDettaglioOperazione.Name = "memoDettaglioOperazione";
            this.memoDettaglioOperazione.Size = new System.Drawing.Size(362, 426);
            this.memoDettaglioOperazione.TabIndex = 0;
            // 
            // frmMemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 450);
            this.Controls.Add(this.memoDettaglioOperazione);
            this.Name = "frmMemo";
            this.Text = "Dettaglio Operazione";
            this.Load += new System.EventHandler(this.frmMemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.memoDettaglioOperazione.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit memoDettaglioOperazione;
    }
}