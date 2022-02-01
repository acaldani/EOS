using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOS.UI
{
    public partial class frmDocumenti : DevExpress.XtraEditors.XtraForm
    {
        public bool AggiungiNuova = true;
        public bool DataChanged = false;
        public string CodiceComposto = "";
        public int IDDocumento = 0;
        string filename="";
        string filepath = "";
        string filepathdestination = "";
        bool newfileload = false;
        
        public frmDocumenti()
        {
            InitializeComponent();
        }

        private void frmDocumenti_Load(object sender, EventArgs e)
        {
            CaricaModelloInControlli();

            abilitazioneControlli();
            addcontrolhandler();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (DataChanged)
            {
                DialogResult response = XtraMessageBox.Show("Attenzione, ci sono dei dati non salvati, se si prosegue verranno persi. Continuare?", "Dati non salvati", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (response == DialogResult.Yes)
                {
                    CaricaModelloInControlli();

                    newfileload = false;
                    DataChanged = false;
                    butSalva.Enabled = false;
                    butAnnulla.Enabled = false;
                }
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (DataChanged)
            {
                DialogResult response = XtraMessageBox.Show("Attenzione, ci sono dei dati non salvati, se si prosegue verranno persi. Continuare?", "Dati non salvati", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (response == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private EOS.Core.Model.Model_Documenti CaricaControlliInModello(EOS.Core.Model.Model_Documenti ModelDocumento)
        {
            ModelDocumento.CodiceComposto = CodiceComposto;
            ModelDocumento.NomeDocumento = Convert.ToString(txtNomeDocumento.EditValue);
            ModelDocumento.DescrizioneDocumento = Convert.ToString(txtDescrizioneDocumento.EditValue);
            ModelDocumento.PathDocumento = Convert.ToString(txtPathDocumento.EditValue);
            ModelDocumento.DataDocumento = Convert.ToDateTime(txtDataDocumento.EditValue.ToString());
            
            return ModelDocumento;

        }

        private void CaricaModelloInControlli()
        {
            if (AggiungiNuova)  //inserimento nuovo
            {
                txtCodiceComposto.EditValue = CodiceComposto;
                txtDataDocumento.EditValue = DateTime.Now.ToString();
            }
            else
            {
                EOS.Core.Control.Control_Documenti ControlDocumento = new EOS.Core.Control.Control_Documenti();
                ControlDocumento.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                EOS.Core.Model.Model_Documenti ModelDocumento = new EOS.Core.Model.Model_Documenti();

                ModelDocumento = ControlDocumento.GetDocumentoByID(IDDocumento).First().Value;

                txtCodiceComposto.EditValue = ModelDocumento.CodiceComposto;
                txtNomeDocumento.EditValue = ModelDocumento.NomeDocumento;
                txtDescrizioneDocumento.EditValue = ModelDocumento.DescrizioneDocumento;
                txtPathDocumento.EditValue = ModelDocumento.PathDocumento;

                if (ModelDocumento.DataDocumento.ToString() == "01/01/0001 00:00:00")
                {
                    txtDataDocumento.EditValue = null;
                }
                else
                {
                    txtDataDocumento.EditValue = ModelDocumento.DataDocumento;
                }

                ModelDocumento = null;
                ControlDocumento = null;
            }
        }

        private Boolean Validazione()
        {

            bool validato = true;
            string errori = "";

            if ((txtNomeDocumento.EditValue == "")||(txtNomeDocumento.EditValue == null))
            {
                validato = false;
                errori = errori + "Deve essere specificato il nome del documento!\r\n";
            }

            if ((txtDescrizioneDocumento.EditValue == "")||(txtDescrizioneDocumento.EditValue == null))
            {
                validato = false;
                errori = errori + "Deve essere specificata la descrizione del documento!\r\n";
            }

            if ((txtPathDocumento.EditValue == "")||(txtPathDocumento.EditValue == null))
            {
                validato = false;
                errori = errori + "Deve essere specificato il percorso del documento!\r\n";
            }

            if (!validato)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(errori, "Validazione", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return validato;
        }

        private void abilitazioneControlli()
        {
            txtNomeDocumento.Enabled = true;
            txtDescrizioneDocumento.Enabled = true;
            butCaricaFile.Enabled = true;

            if(DataChanged)
            {
                butSalva.Enabled = true;
                if (AggiungiNuova)  //inserimento nuovo
                {
                    butAnnulla.Enabled = false;
                }
                else //visualizzazione/modifica esistente
                {
                    butAnnulla.Enabled = true;
                }
            }
            else
            {
                butAnnulla.Enabled = false;
                butSalva.Enabled = false;
            }
        }

        private void DataChange(object sender, EventArgs e)
        {
            DataChanged = true;
            butSalva.Enabled = true;
            if(AggiungiNuova)
            {

            }
            else
            {
                butAnnulla.Enabled = true;
            }
        }

        private void addcontrolhandler()
        {
            this.txtNomeDocumento.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtDescrizioneDocumento.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtPathDocumento.EditValueChanged += new System.EventHandler(this.DataChange);
        }

        private void removecontrolhandler()
        {
            this.txtNomeDocumento.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtDescrizioneDocumento.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtPathDocumento.EditValueChanged -= new System.EventHandler(this.DataChange);
        }

        private void butCaricaFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open File";
            theDialog.Filter = "All files (*.*)|*.*";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    newfileload = true;
                    txtPathDocumento.EditValue = Convert.ToString(theDialog.FileName);
                    filepath = Convert.ToString(theDialog.FileName);
                    filename = Convert.ToString(theDialog.SafeFileName);
                    theDialog = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void butSalva_Click_1(object sender, EventArgs e)
        {
            int ret1 = 0;

            if (AggiungiNuova)  //inserimento nuovo
            {
                if (Validazione())
                {
                    EOS.Core.Model.Model_Configurazione ModelConfigurazione = new EOS.Core.Model.Model_Configurazione();
                    EOS.Core.Control.Control_Configurazione ControlConfigurazione = new EOS.Core.Control.Control_Configurazione();
                    ControlConfigurazione.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                    ModelConfigurazione = ControlConfigurazione.GetActiveConfiguration().First().Value;

                    EOS.Core.Model.Model_Documenti ModelDocumento = new EOS.Core.Model.Model_Documenti();
                    EOS.Core.Control.Control_Documenti ControlDocumento = new EOS.Core.Control.Control_Documenti();
                    ControlDocumento.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                    ModelDocumento.CodiceComposto = Convert.ToString(txtCodiceComposto.EditValue);
                    ModelDocumento.NomeDocumento = Convert.ToString(txtNomeDocumento.EditValue);
                    ModelDocumento.DescrizioneDocumento = Convert.ToString(txtDescrizioneDocumento.EditValue);
                    ModelDocumento.PathDocumento = ModelConfigurazione.DocumentsRoot + CodiceComposto + @"\" + filename;
                    ModelDocumento.DataDocumento = Convert.ToDateTime(txtDataDocumento.EditValue);

                    ret1 = ControlDocumento.AddDocumento(ModelDocumento);

                    if (ret1 != 0)
                    {
                        if(newfileload)
                        {
                            if (!Directory.Exists(ModelConfigurazione.DocumentsRoot + CodiceComposto))
                            {
                                Directory.CreateDirectory(ModelConfigurazione.DocumentsRoot + CodiceComposto);
                            }

                            File.Copy(filepath, ModelConfigurazione.DocumentsRoot + CodiceComposto + @"\" + filename, true);
                        }

                        XtraMessageBox.Show("Dati aggiornati.", "Salva", MessageBoxButtons.OK);
                    }
                    else
                    {
                        XtraMessageBox.Show("L'aggiornamento dei dati è terminata con errore.", "Salva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    ModelDocumento = null;
                    ControlDocumento = null;
                    ModelConfigurazione = null;
                    ControlConfigurazione = null;

                    DataChanged = false;
                    butSalva.Enabled = false;
                    butAnnulla.Enabled = false;

                    DevExpress.XtraEditors.XtraMessageBox.Show("Documento aggiunto al composto selezionato", "Aggiungi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("I dati minimi per l'inserimento non sono correttamente compilati", "Aggiungi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                if (Validazione())
                {
                    EOS.Core.Model.Model_Documenti ModelDocumento = new EOS.Core.Model.Model_Documenti();
                    EOS.Core.Control.Control_Documenti ControlDocumento = new EOS.Core.Control.Control_Documenti();
                    ControlDocumento.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                    ModelDocumento = ControlDocumento.GetDocumentoByID(IDDocumento).First().Value;

                    ModelDocumento.CodiceComposto = Convert.ToString(txtCodiceComposto.EditValue);
                    ModelDocumento.NomeDocumento = Convert.ToString(txtNomeDocumento.EditValue);
                    ModelDocumento.DescrizioneDocumento = Convert.ToString(txtDescrizioneDocumento.EditValue);
                    ModelDocumento.PathDocumento = Convert.ToString(txtPathDocumento.EditValue);
                    ModelDocumento.DataDocumento = Convert.ToDateTime(txtDataDocumento.EditValue);

                    ret1 = ControlDocumento.UpdateDocumento(ModelDocumento);

                    if (ret1 != 0)
                    {
                        XtraMessageBox.Show("Dati aggiornati.", "Salva", MessageBoxButtons.OK);
                    }
                    else
                    {
                        XtraMessageBox.Show("L'aggiornamento dei dati è terminata con errore.", "Salva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    ModelDocumento = null;
                    ControlDocumento = null;

                    DataChanged = false;
                    butSalva.Enabled = false;
                    butAnnulla.Enabled = false;

                    DevExpress.XtraEditors.XtraMessageBox.Show("Dati dettaglio miscela di solventi aggiornati", "Modifica", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("I dati minimi per l'aggiornamento non sono correttamente compilati", "Modifica", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void butApriFile_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtPathDocumento.EditValue.ToString()))
            {
                System.Diagnostics.Process.Start(txtPathDocumento.EditValue.ToString());
            }
        }
    }
}