using DevExpress.XtraBars;
using DevExpress.XtraEditors;
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
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;

namespace EOS.UI
{
    public partial class frmSoluzioniDetails : Form
    {
        public bool AggiungiNuova = true;
        public bool DataChanged = false;
        public string StatoSoluzione = "";
        public int idSoluzioneMaster = 0;
        public int idSoluzioneDetail = 0;
        public int idSoluzione = 0;
        public int idSchedaDocumenti = 0;
        public string CAS = null;
        public string Note = null;
        public DateTime DataScadenza;
        public int TipoMaterialeMR = Convert.ToInt32(null);
        public decimal VolumeFinale = Convert.ToDecimal(null);
        public string UMVolumeFinale = Convert.ToString(null);
        bool changingSoluzione = false;
        bool changingMaterialeMR = false;
        bool changingApparecchio = false;
        bool changingUtensile = false;
        bool changingApparecchio2 = false;
        bool changingUtensile2 = false;
        public int IDUtente=0;

        public frmSoluzioniDetails()
        {
            InitializeComponent();
        }

        private void butChiudi_Click(object sender, EventArgs e)
        {
            if(DataChanged)
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

        private void butAnnulla_Click(object sender, EventArgs e)
        {
            if (DataChanged)
            {
                DialogResult response = XtraMessageBox.Show("Attenzione, ci sono dei dati non salvati, se si prosegue verranno persi. Continuare?", "Dati non salvati", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (response == DialogResult.Yes)
                {
                    CaricaModelloInControlli();

                    DataChanged = false;
                    butSalva.Enabled = false;
                    butAnnulla.Enabled = false;
                }
            }
        }

        private EOS.Core.Model.Model_Soluzioni_Details CaricaControlliInModello(EOS.Core.Model.Model_Soluzioni_Details ModelSoluzioneDetail)
        {
            ModelSoluzioneDetail.IDSoluzioneMaster = idSoluzioneMaster;
            ModelSoluzioneDetail.IDSchedaDocumenti = Convert.ToInt32(cboMaterialeMR.EditValue);
            ModelSoluzioneDetail.IDSoluzione = Convert.ToInt32(cboSoluzione.EditValue);
            ModelSoluzioneDetail.UM_Prelievo = Convert.ToString(txtUMPrelievo.EditValue);
            ModelSoluzioneDetail.Quantita_Prelievo = Convert.ToDecimal(txtQuantitaPrelievo.EditValue.ToString());
            //ModelSoluzioneDetail.Concentrazione = Convert.ToDecimal(txtConcentrazione.EditValue);
            ModelSoluzioneDetail.Note = Convert.ToString(txtNote.EditValue);
            ModelSoluzioneDetail.IDApparecchio = Convert.ToInt32(cboApparecchioPrelievo.EditValue);
            ModelSoluzioneDetail.IDUtensile = Convert.ToInt32(cboUtensilePrelievo.EditValue);
            ModelSoluzioneDetail.IDApparecchio2 = Convert.ToInt32(cboApparecchioPrelievo2.EditValue);
            ModelSoluzioneDetail.IDUtensile2 = Convert.ToInt32(cboUtensilePrelievo2.EditValue);
            ModelSoluzioneDetail.Tipologia_MR = Convert.ToInt32(cboTipoMaterialeMR.EditValue);
            ModelSoluzioneDetail.CAS = Convert.ToString(txtCASMateriale.EditValue);
            ModelSoluzioneDetail.DataScadenza = Convert.ToDateTime(txtDataScadenza.EditValue);

            return ModelSoluzioneDetail;

        }

        private void CaricaModelloInControlli()
        {
            EOS.Core.Control.Control_Soluzioni_Details ControlSolutionDetail = new EOS.Core.Control.Control_Soluzioni_Details();
            ControlSolutionDetail.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
            EOS.Core.Model.Model_Soluzioni_Details ModelSolutionDetail = new EOS.Core.Model.Model_Soluzioni_Details();

            ModelSolutionDetail = ControlSolutionDetail.GetByIDSolutionDetail(idSoluzioneDetail).First().Value;

            cboMaterialeMR.EditValue = ModelSolutionDetail.IDSchedaDocumenti;
            cboTipoMaterialeMR.EditValue = ModelSolutionDetail.Tipologia_MR;
            txtCASMateriale.EditValue = ModelSolutionDetail.CAS;
            cboSoluzione.EditValue = ModelSolutionDetail.IDSoluzione;
            txtUMPrelievo.EditValue = ModelSolutionDetail.UM_Prelievo;
            txtQuantitaPrelievo.EditValue = ModelSolutionDetail.Quantita_Prelievo;
            cboApparecchioPrelievo.EditValue = ModelSolutionDetail.IDApparecchio;
            cboUtensilePrelievo.EditValue = ModelSolutionDetail.IDUtensile;
            cboApparecchioPrelievo2.EditValue = ModelSolutionDetail.IDApparecchio2;
            cboUtensilePrelievo2.EditValue = ModelSolutionDetail.IDUtensile2;
            txtDataScadenza.EditValue = ModelSolutionDetail.DataScadenza;

            if (ModelSolutionDetail.DataScadenza.ToString() == "01/01/0001 00:00:00")
            {
                txtDataScadenza.EditValue = null;
            }
            else
            {
                txtDataScadenza.EditValue = ModelSolutionDetail.DataScadenza;
            }

            //txtConcentrazione.EditValue = ModelSolutionDetail.Concentrazione;
            txtNote.EditValue = ModelSolutionDetail.Note;

            ModelSolutionDetail = null;
            ControlSolutionDetail = null;
        }

        private void butSalva_Click(object sender, EventArgs e)
        {
            int ret1=0;
            int ret2=0;

            if (AggiungiNuova)  //inserimento nuovo
            {
                if (Validazione())
                {
                    EOS.Core.Model.Model_Soluzioni_Details ModelSoluzioniDetail = new EOS.Core.Model.Model_Soluzioni_Details();
                    EOS.Core.Control.Control_Soluzioni_Details ControlSoluzioniDetail = new EOS.Core.Control.Control_Soluzioni_Details();
                    ControlSoluzioniDetail.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                    ModelSoluzioniDetail.IDSoluzioneMaster = Convert.ToInt32(idSoluzioneMaster);
                    ModelSoluzioniDetail.IDSchedaDocumenti = Convert.ToInt32(cboMaterialeMR.EditValue);
                    ModelSoluzioniDetail.Tipologia_MR = Convert.ToInt32(cboTipoMaterialeMR.EditValue);
                    ModelSoluzioniDetail.CAS = Convert.ToString(txtCASMateriale.EditValue);
                    ModelSoluzioniDetail.IDSoluzione = Convert.ToInt32(cboSoluzione.EditValue);
                    ModelSoluzioniDetail.UM_Prelievo = Convert.ToString(txtUMPrelievo.EditValue);
                    ModelSoluzioniDetail.Quantita_Prelievo = Convert.ToDecimal(txtQuantitaPrelievo.EditValue.ToString());
                    ModelSoluzioniDetail.IDApparecchio = Convert.ToInt32(cboApparecchioPrelievo.EditValue);
                    ModelSoluzioniDetail.IDUtensile = Convert.ToInt32(cboUtensilePrelievo.EditValue);
                    ModelSoluzioniDetail.IDApparecchio2 = Convert.ToInt32(cboApparecchioPrelievo2.EditValue);
                    ModelSoluzioniDetail.IDUtensile2 = Convert.ToInt32(cboUtensilePrelievo2.EditValue);
                    ModelSoluzioniDetail.DataScadenza = Convert.ToDateTime(txtDataScadenza.EditValue);
                    //ModelSoluzioniDetail.Concentrazione = Convert.ToDecimal(txtConcentrazione.EditValue);
                    ModelSoluzioniDetail.Concentrazione = 0;
                    ModelSoluzioniDetail.Note = Convert.ToString(txtNote.EditValue);

                    ControlSoluzioniDetail.IDUtente = IDUtente;
                    ret1 = ControlSoluzioniDetail.AddSolutionDetail(ModelSoluzioniDetail);


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///Commentato, era in uso ma non era utile in quanto queste operazioni si svolgono già prima nell'addsolutiondetail
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //if ((ret1 == 1) && (ret2 == 1))
                    //{
                    //    Core.Control.Control_Soluzioni_Details_Concentration ControlSoluzioniDetailsConcentration = new Core.Control.Control_Soluzioni_Details_Concentration();
                    //    ControlSoluzioniDetailsConcentration.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                    //    ret2 = ControlSoluzioniDetailsConcentration.DeleteSolutionDetailsConcentrationByIDSoluzione(idSoluzioneMaster);
                    //    ControlSoluzioniDetailsConcentration = null;
                    //
                    //    EOS.Core.Control.Controller_Soluzioni ControllerSoluzioni = new Core.Control.Controller_Soluzioni();
                    //    ControllerSoluzioni.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                    //    EOS.Core.Model.Model_Soluzioni ModelSoluzioni = new Core.Model.Model_Soluzioni();
                    //    ModelSoluzioni = ControllerSoluzioni.GetSolutionByID(idSoluzioneMaster).First().Value;
                    //    ModelSoluzioni.DataScadenza = Convert.ToDateTime(null);
                    //    ControllerSoluzioni.UpdateSolution(ModelSoluzioni);
                    //
                    //    ModelSoluzioni = null;
                    //    ControllerSoluzioni = null;
                    //
                    //    XtraMessageBox.Show("Dati aggiornati.", "Salva", MessageBoxButtons.OK);
                    //}
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///Commentato, era in uso ma non era utile in quanto queste operazioni si svolgono già prima nell'addsolutiondetail
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (ret1 > 0)
                    {
                        //XtraMessageBox.Show("Dati aggiornati.", "Salva", MessageBoxButtons.OK);
                    }
                    else
                    {
                        XtraMessageBox.Show("L'aggiornamento dei dati è terminata con errore.", "Salva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    ModelSoluzioniDetail = null;
                    ControlSoluzioniDetail = null;

                    DataChanged = false;
                    butSalva.Enabled = false;
                    butAnnulla.Enabled = false;

                    //DevExpress.XtraEditors.XtraMessageBox.Show("Componente aggiunto alla soluzione", "Aggiungi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DevExpress.XtraEditors.XtraMessageBox.Show("Componente aggiunto alla Soluzione MR", "Aggiungi", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    EOS.Core.Model.Model_Soluzioni_Details ModelSoluzioniDetail = new EOS.Core.Model.Model_Soluzioni_Details();
                    EOS.Core.Control.Control_Soluzioni_Details ControlSoluzioniDetail = new EOS.Core.Control.Control_Soluzioni_Details();
                    ControlSoluzioniDetail.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                    ModelSoluzioniDetail = ControlSoluzioniDetail.GetByIDSolutionDetail(idSoluzioneDetail).First().Value;

                    ModelSoluzioniDetail.IDSchedaDocumenti = Convert.ToInt32(cboMaterialeMR.EditValue);
                    ModelSoluzioniDetail.Tipologia_MR = Convert.ToInt32(cboTipoMaterialeMR.EditValue);
                    ModelSoluzioniDetail.CAS = Convert.ToString(txtCASMateriale.EditValue);
                    ModelSoluzioniDetail.IDSoluzione = Convert.ToInt32(cboSoluzione.EditValue);
                    ModelSoluzioniDetail.UM_Prelievo = Convert.ToString(txtUMPrelievo.EditValue);
                    ModelSoluzioniDetail.Quantita_Prelievo = Convert.ToDecimal(txtQuantitaPrelievo.EditValue.ToString());
                    ModelSoluzioniDetail.IDApparecchio = Convert.ToInt32(cboApparecchioPrelievo.EditValue);
                    ModelSoluzioniDetail.IDUtensile = Convert.ToInt32(cboUtensilePrelievo.EditValue);
                    ModelSoluzioniDetail.IDApparecchio2 = Convert.ToInt32(cboApparecchioPrelievo2.EditValue);
                    ModelSoluzioniDetail.IDUtensile2 = Convert.ToInt32(cboUtensilePrelievo2.EditValue);
                    ModelSoluzioniDetail.DataScadenza = Convert.ToDateTime(txtDataScadenza.EditValue);
                    //ModelSoluzioniDetail.Concentrazione = Convert.ToDecimal(txtConcentrazione.EditValue);
                    ModelSoluzioniDetail.Concentrazione = 0;
                    ModelSoluzioniDetail.Note = Convert.ToString(txtNote.EditValue);

                    ControlSoluzioniDetail.IDUtente = IDUtente;
                    ret1 = ControlSoluzioniDetail.UpdateSolutionDetail(ModelSoluzioniDetail);

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///Commentato, era in uso ma non era utile in quanto queste operazioni si svolgono già prima nell'addsolutiondetail
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //Core.Control.Control_Soluzioni_Details_Concentration ControlSoluzioniDetailsConcentration = new Core.Control.Control_Soluzioni_Details_Concentration();
                    //ControlSoluzioniDetailsConcentration.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                    //ret2 = ControlSoluzioniDetailsConcentration.DeleteSolutionDetailsConcentrationByIDSoluzione(idSoluzioneMaster);
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///Commentato, era in uso ma non era utile in quanto queste operazioni si svolgono già prima nell'addsolutiondetail
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    //if ((ret1 == 1) && (ret2 == 1))
                    if (ret1 == 1)
                    {
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ///Commentato, era in uso ma non era utile in quanto queste operazioni si svolgono già prima nell'addsolutiondetail
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //EOS.Core.Control.Controller_Soluzioni ControllerSoluzioni = new Core.Control.Controller_Soluzioni();
                        //ControllerSoluzioni.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                        //EOS.Core.Model.Model_Soluzioni ModelSoluzioni = new Core.Model.Model_Soluzioni();
                        //ModelSoluzioni = ControllerSoluzioni.GetSolutionByID(idSoluzioneMaster).First().Value;
                        //ModelSoluzioni.DataScadenza = Convert.ToDateTime(null);
                        //ControllerSoluzioni.UpdateSolution(ModelSoluzioni);

                        //ModelSoluzioni = null;
                        //ControllerSoluzioni = null;
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ///Commentato, era in uso ma non era utile in quanto queste operazioni si svolgono già prima nell'addsolutiondetail
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        //XtraMessageBox.Show("Dati aggiornati.", "Salva", MessageBoxButtons.OK);
                    }
                    else
                    {
                        XtraMessageBox.Show("L'aggiornamento dei dati è terminata con errore.", "Salva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    ModelSoluzioniDetail = null;
                    ControlSoluzioniDetail = null;
                    //ControlSoluzioniDetailsConcentration = null;

                    DataChanged = false;
                    butSalva.Enabled = false;
                    butAnnulla.Enabled = false;

                    //DevExpress.XtraEditors.XtraMessageBox.Show("Dati dettaglio soluzione aggiornati", "Modifica", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DevExpress.XtraEditors.XtraMessageBox.Show("Dati dettaglio Soluzione MR aggiornati", "Modifica", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("I dati minimi per l'aggiornamento non sono correttamente compilati", "Modifica", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private Boolean Validazione()
        {

            bool validato = true;
            string errori = "";

            if(Convert.ToString(txtUMPrelievo.EditValue)=="")
            {
                validato = false;
                errori = errori + "Deve essere specificata un unità di misura di prelievo (mg per solidi ed ml per liquidi)!\r\n";
            }

            if ((Convert.ToString(txtQuantitaPrelievo.EditValue) == "") || (Convert.ToDecimal(txtQuantitaPrelievo.EditValue) == 0))
            {
                validato = false;
                errori = errori + "Deve essere specificata una quantità prelevata!\r\n";
            }

            if ((cboApparecchioPrelievo.EditValue == null)&&(cboUtensilePrelievo.EditValue == null))
            {
                validato = false;
                errori = errori + "Deve essere specificato un apparecchio o un utensile di prelievo!\r\n";
            }

            if(! validato)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(errori, "Validazione", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
            return validato;
        }

        private void frmSoluzioniDetails_Load(object sender, EventArgs e)
        {
            // TODO: questa riga di codice carica i dati nella tabella 'lupin.UtensiliSelectCommand'. È possibile spostarla o rimuoverla se necessario.
            this.utensiliSelectCommandTableAdapter.Fill(this.lupin.UtensiliSelectCommand);
            // TODO: questa riga di codice carica i dati nella tabella 'lupin.ApparecchiSelectCommand'. È possibile spostarla o rimuoverla se necessario.
            this.apparecchiSelectCommandTableAdapter.Fill(this.lupin.ApparecchiSelectCommand);
            // TODO: questa riga di codice carica i dati nella tabella 'soluzioni._Soluzioni'. È possibile spostarla o rimuoverla se necessario.
            this.soluzioniTableAdapter.Fill(this.soluzioni._Soluzioni);
            // TODO: questa riga di codice carica i dati nella tabella 'lupin.MaterialiLottiSelectCommand'. È possibile spostarla o rimuoverla se necessario.
            this.materialiLottiSelectCommandTableAdapter.Fill(this.lupin.MaterialiLottiSelectCommand);
            // TODO: questa riga di codice carica i dati nella tabella 'soluzioni.Soluzioni_Details'. È possibile spostarla o rimuoverla se necessario.
            this.soluzioni_DetailsTableAdapter.Fill(this.soluzioni.Soluzioni_Details);
            // TODO: questa riga di codice carica i dati nella tabella 'soluzioni.Materiale_Tipologia'. È possibile spostarla o rimuoverla se necessario.
            this.materiale_TipologiaTableAdapter.Fill(this.soluzioni.Materiale_Tipologia);

            if (AggiungiNuova)  //inserimento nuovo
            {
                //lookupEdit1.EditValue = this.cboTipoMaterialeMR.GetDataSourceValue("IDSchedaDocumenti", 1); // Select Person from second row by its ID'
                this.cboTipoMaterialeMR.EditValue = this.TipoMaterialeMR;

                

                if(idSchedaDocumenti!=0)
                {
                    EOS.Core.Model.Model_Materiale_Tipologia ModelMaterialeTipologia = new EOS.Core.Model.Model_Materiale_Tipologia();
                    EOS.Core.Control.Control_Materiale_Tipologia ControlMaterialeTipologia = new EOS.Core.Control.Control_Materiale_Tipologia();
                    ControlMaterialeTipologia.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                    ModelMaterialeTipologia = ControlMaterialeTipologia.GetByID(Convert.ToInt32(this.TipoMaterialeMR)).First().Value;

                    this.txtUMPrelievo.EditValue = ModelMaterialeTipologia.UM;

                    ControlMaterialeTipologia = null;
                    ModelMaterialeTipologia = null;
                }
                else
                {
                    this.txtUMPrelievo.EditValue = "ml";
                }

                this.cboMaterialeMR.EditValue = this.idSchedaDocumenti;
                this.cboSoluzione.EditValue = this.idSoluzione;
                this.txtCASMateriale.EditValue = this.CAS;
                this.txtDataScadenza.EditValue = this.DataScadenza;
                this.txtNote.EditValue = this.Note;
            }
            else
            {
                CaricaModelloInControlli();
            }

            abilitazioneControlli();
            addcontrolhandler();

        }

        private void frmSoluzioniDetails_Shown(object sender, EventArgs e)
        {
            
        }

        private void abilitazioneControlli()
        {
            if (AggiungiNuova)  //inserimento nuovo
            {
                cboMaterialeMR.Enabled = false;
                if(cboSoluzione.EditValue.ToString()!="0")
                {
                    cboTipoMaterialeMR.Enabled = false;
                }
                else
                {
                    cboTipoMaterialeMR.Enabled = true;
                }
                txtCASMateriale.Enabled = false;
                cboSoluzione.Enabled = false;
                //txtConcentrazione.Enabled = false;
                txtDataScadenza.Enabled = false;
                txtQuantitaPrelievo.Enabled = true;
                txtUMPrelievo.Enabled = false;
                cboApparecchioPrelievo.Enabled = true;
                cboUtensilePrelievo.Enabled = true;
                cboApparecchioPrelievo2.Enabled = true;
                cboUtensilePrelievo2.Enabled = true;
                txtNote.Enabled = true;
                butSalva.Enabled = true;
                butAnnulla.Enabled = false;
            }
            else //visualizzazione/modifica esistente
            {
                if (StatoSoluzione == "5")
                {
                    cboMaterialeMR.Enabled = false;
                    cboMaterialeMR.Enabled = false;
                    if (cboSoluzione.EditValue.ToString() != "0")
                    {
                        cboTipoMaterialeMR.Enabled = false;
                    }
                    else
                    {
                        cboTipoMaterialeMR.Enabled = true;
                    }
                    txtCASMateriale.Enabled = false;
                    cboSoluzione.Enabled = false;
                    txtQuantitaPrelievo.Enabled = true;
                    txtUMPrelievo.Enabled = false;
                    //txtConcentrazione.Enabled = false;
                    txtDataScadenza.Enabled = false;
                    cboApparecchioPrelievo.Enabled = true;
                    cboUtensilePrelievo.Enabled = true;
                    cboApparecchioPrelievo2.Enabled = true;
                    cboUtensilePrelievo2.Enabled = true;
                    txtNote.Enabled = true;
                    if (DataChanged)
                    {
                        butSalva.Enabled = true;
                        butAnnulla.Enabled = true;
                    }
                    else
                    {
                        butSalva.Enabled = false;
                        butAnnulla.Enabled = false;
                    }
                }
                else
                {
                    cboMaterialeMR.Enabled = false;
                    cboTipoMaterialeMR.Enabled = false;
                    txtCASMateriale.Enabled = false;
                    cboSoluzione.Enabled = false;
                    txtQuantitaPrelievo.Enabled = false;
                    txtUMPrelievo.Enabled = false;
                    cboApparecchioPrelievo.Enabled = false;
                    cboUtensilePrelievo.Enabled = false;
                    cboApparecchioPrelievo2.Enabled = false;
                    cboUtensilePrelievo2.Enabled = false;
                    txtNote.Enabled = false;
                    butSalva.Enabled = false;
                    butAnnulla.Enabled = false;
                    //txtConcentrazione.Enabled = false;
                    txtDataScadenza.Enabled = false;
                }
            }
        }

        private void DataChange(object sender, EventArgs e)
        {
            DataChanged = true;
            butSalva.Enabled = true;
            butAnnulla.Enabled = true;
        }

        private void addcontrolhandler()
        {
            this.cboMaterialeMR.EditValueChanged += new System.EventHandler(this.MaterialeMR);
            this.cboSoluzione.EditValueChanged += new System.EventHandler(this.Soluzione);
            this.cboApparecchioPrelievo.EditValueChanged += new System.EventHandler(this.Apparecchio);
            this.cboUtensilePrelievo.EditValueChanged += new System.EventHandler(this.Utensile);
            this.cboApparecchioPrelievo2.EditValueChanged += new System.EventHandler(this.Apparecchio2);
            this.cboUtensilePrelievo2.EditValueChanged += new System.EventHandler(this.Utensile2);

            this.txtCASMateriale.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboTipoMaterialeMR.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboSoluzione.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboMaterialeMR.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtNote.EditValueChanged += new System.EventHandler(this.DataChange);
            //this.txtConcentrazione.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtUMPrelievo.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtQuantitaPrelievo.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboUtensilePrelievo.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboApparecchioPrelievo.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboUtensilePrelievo2.EditValueChanged += new System.EventHandler(this.DataChange);
            this.cboApparecchioPrelievo2.EditValueChanged += new System.EventHandler(this.DataChange);
            this.txtDataScadenza.EditValueChanged += new System.EventHandler(this.DataChange);

            //this.cboApparecchioPrelievo.Popup += new System.EventHandler(this.FiltraApparecchi);
        }

        private void removecontrolhandler()
        {
            this.cboMaterialeMR.EditValueChanged -= new System.EventHandler(this.MaterialeMR);
            this.cboSoluzione.EditValueChanged -= new System.EventHandler(this.Soluzione);
            this.cboApparecchioPrelievo.EditValueChanged -= new System.EventHandler(this.Apparecchio);
            this.cboUtensilePrelievo.EditValueChanged -= new System.EventHandler(this.Utensile);
            this.cboApparecchioPrelievo2.EditValueChanged -= new System.EventHandler(this.Apparecchio2);
            this.cboUtensilePrelievo2.EditValueChanged -= new System.EventHandler(this.Utensile2);

            this.txtCASMateriale.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboTipoMaterialeMR.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboSoluzione.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboMaterialeMR.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtNote.EditValueChanged -= new System.EventHandler(this.DataChange);
            //this.txtConcentrazione.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtUMPrelievo.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.txtQuantitaPrelievo.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboUtensilePrelievo.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboApparecchioPrelievo.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboUtensilePrelievo2.EditValueChanged -= new System.EventHandler(this.DataChange);
            this.cboApparecchioPrelievo2.EditValueChanged -= new System.EventHandler(this.DataChange); 
            this.txtDataScadenza.EditValueChanged -= new System.EventHandler(this.DataChange);
        }

        private void MaterialeMR(object sender, EventArgs e)
        {
            if (!changingMaterialeMR)
            {
                removecontrolhandler();
                changingMaterialeMR = true;
                cboSoluzione.EditValue = null;
                addcontrolhandler();
                changingMaterialeMR = false;
            }
        }

        private void Soluzione(object sender, EventArgs e)
        {
            if (!changingSoluzione)
            {
                removecontrolhandler();
                changingSoluzione = true;
                cboMaterialeMR.EditValue = null;
                addcontrolhandler();
                changingSoluzione = false;
            }
        }

        private void Apparecchio(object sender, EventArgs e)
        {
            if (!changingUtensile)
            {
                removecontrolhandler();
                changingApparecchio = true;
                cboUtensilePrelievo.EditValue = null;
                addcontrolhandler();
                changingApparecchio = false;
            }
        }

        private void Utensile(object sender, EventArgs e)
        {
            if (!changingApparecchio)
            {
                removecontrolhandler();
                changingUtensile = true;
                cboApparecchioPrelievo.EditValue = null;
                addcontrolhandler();
                changingUtensile = false;
            }
        }

        private void Apparecchio2(object sender, EventArgs e)
        {
            if (!changingUtensile2)
            {
                removecontrolhandler();
                changingApparecchio2 = true;
                cboUtensilePrelievo2.EditValue = null;
                addcontrolhandler();
                changingApparecchio2 = false;
            }
        }

        private void Utensile2(object sender, EventArgs e)
        {
            if (!changingApparecchio2)
            {
                removecontrolhandler();
                changingUtensile2 = true;
                cboApparecchioPrelievo2.EditValue = null;
                addcontrolhandler();
                changingUtensile2 = false;
            }
        }

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

            edit.Properties.View.ActiveFilterString = filter;
        }
        #endregion
    }
}
