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

namespace EOS.UI
{
    public partial class FrmRettaPuntiSelezioneComponenti : DevExpress.XtraEditors.XtraForm
    {
        public int IDRetta = 0;
        public int IDCaller = 0;
        public int IDUtente = 0;
        public string ConnectioString = "";
        public int IDGruppoPunti = 0;

        public FrmRettaPuntiSelezioneComponenti()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butAggiungiComponente_Click(object sender, EventArgs e)
        {
            frmSeleziona Seleziona = new frmSeleziona();
            Seleziona.TipoElenco = "Soluzioni";
            Seleziona.IDCaller = IDRetta;
            Seleziona.IDUtente = IDUtente;
            Seleziona.ShowDialog();

            if (Seleziona.SceltaEffettuata)
            {
                if ((Seleziona.Conferma == "MaterialeMR") || (Seleziona.Conferma == "Soluzione"))
                {
                    EOS.Core.Control.Control_Retta_GruppiPunti_Componenti ControlRettaGruppiPuntiComponenti = new EOS.Core.Control.Control_Retta_GruppiPunti_Componenti();
                    EOS.Core.Model.Model_Retta_GruppiPunti_Componenti ModelRettaGruppiPuntiComponenti = new EOS.Core.Model.Model_Retta_GruppiPunti_Componenti();
                    ControlRettaGruppiPuntiComponenti.IDUtente = IDUtente;
                    ControlRettaGruppiPuntiComponenti.ConnectionString = ConnectioString;

                    switch (Seleziona.Conferma)
                    {
                        case "MaterialeMR":
                            ModelRettaGruppiPuntiComponenti.IDRetta = IDRetta;
                            ModelRettaGruppiPuntiComponenti.IDGruppoPunti = IDGruppoPunti;
                            ModelRettaGruppiPuntiComponenti.IDSoluzione = 0;
                            ModelRettaGruppiPuntiComponenti.IDSchedaDocumenti = Seleziona.ID;
                            ModelRettaGruppiPuntiComponenti.Tipologia_MR = Seleziona.TipoMaterialeMR;
                            ModelRettaGruppiPuntiComponenti.Note = Seleziona.NotaUtilizzoScaduto;

                            break;

                        case "Soluzione":
                            ModelRettaGruppiPuntiComponenti.IDRetta = IDRetta;
                            ModelRettaGruppiPuntiComponenti.IDGruppoPunti = IDGruppoPunti;
                            ModelRettaGruppiPuntiComponenti.IDSoluzione = Seleziona.ID;
                            ModelRettaGruppiPuntiComponenti.IDSchedaDocumenti = 0;
                            ModelRettaGruppiPuntiComponenti.Tipologia_MR = 0;
                            ModelRettaGruppiPuntiComponenti.Note = Seleziona.NotaUtilizzoScaduto;

                            break;

                        default:

                            break;
                    }

                    ControlRettaGruppiPuntiComponenti.AddRettaGruppiPuntiComponenti(ModelRettaGruppiPuntiComponenti);

                    LoadGrid();
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Selezione nuovo componente annullata", "Aggiungi Componente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void LoadGrid()
        {
            try
            {
                global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();
                string SQL = "";

                SQL = SQL + "SELECT RETGPC.IDRetta_GruppoPunti_Componenti, ";
                SQL = SQL + "RETGPC.IDSchedaDocumenti, ";
                SQL = SQL + "RETGPC.IDSoluzione, ";
                SQL = SQL + "RETGPC.Tipologia_MR, ";
                SQL = SQL + "MATTIP.Nome as Tipo_Materiale, ";
                SQL = SQL + "MAT.Identificativo + ' - ' + MAT.DenominazioneProdotto + ' - ' + SD.Lotto as Materiale, ";
                SQL = SQL + "SOL.CodiceSoluzione + ' - ' + SOL.Nome as Soluzione, ";
                SQL = SQL + "RETGPC.Note ";
                SQL = SQL + "FROM Rette RET ";
                SQL = SQL + "INNER JOIN Rette_GruppoPunti_Componenti RETGPC ";
                SQL = SQL + "ON RET.IDRetta=RETGPC.IDRetta ";
                SQL = SQL + "LEFT JOIN [SERVER026].Lupin.dbo.SchedeDocumenti SD ";
                SQL = SQL + "ON RETGPC.[IDSchedaDocumenti]=SD.[IDSchedaDocumenti] ";
                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].[dbo].Materiali  MAT ";
                SQL = SQL + "ON SD.IDMateriale=MAT.IDMateriale ";
                SQL = SQL + "LEFT JOIN Soluzioni SOL ";
                SQL = SQL + "ON RETGPC.IDSoluzione=SOL.IDSoluzione ";
                SQL = SQL + "LEFT JOIN Materiale_Tipologia MATTIP ";
                SQL = SQL + "ON RETGPC.Tipologia_MR=MATTIP.ID ";
                SQL = SQL + "WHERE RET.IDRETTA=" + IDRetta + " ";

                DataTable dt = new DataTable();
                dt = DB.GetDataTable(SQL, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                gridComponenti.DataSource = null;
                //gviewRette.Columns.Clear();
                gridComponenti.DataSource = dt;
                //gviewRette.PopulateColumns();
                //gridRette.ForceInitialize();
                gviewComponenti.Columns[0].Visible = false;

                System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmRettaPuntiSelezioneComponenti", "gridComponenti", IDUtente);

                if (str != null)
                {
                    gridComponenti.FocusedView.RestoreLayoutFromStream(str);
                    str.Seek(0, System.IO.SeekOrigin.Begin);
                }
                else
                {
                    gviewComponenti.Columns["IDRetta_GruppoPunti_Componenti"].Visible = false;
                    gviewComponenti.Columns["IDSchedaDocumenti"].Visible = false;
                    gviewComponenti.Columns["IDSoluzione"].Visible = false;
                    gviewComponenti.Columns["Tipologia_MR"].Visible = false;

                    gviewComponenti.Columns["Tipo_Materiale"].Width = 200;
                    gviewComponenti.Columns["Materiale"].Width = 200;
                    gviewComponenti.Columns["Soluzione"].Width = 200;
                    gviewComponenti.Columns["Note"].Width = 200;
                }

                str = null;
            }
            catch (Exception e)
            {

            }
        }

        private void FrmRettaPuntiSelezioneComponenti_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void butCancellaComponente_Click(object sender, EventArgs e)
        {
            int ret = 0;

            if (XtraMessageBox.Show("Sei sicuro di cancellare gli elementi selezionati?", "Cancella", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                EOS.Core.Control.Control_Retta_GruppiPunti_Componenti ControlRettaGruppiPuntiComponenti = new EOS.Core.Control.Control_Retta_GruppiPunti_Componenti();
                ControlRettaGruppiPuntiComponenti.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                ControlRettaGruppiPuntiComponenti.IDUtente = IDUtente;

                for (int i = 0; i < gviewComponenti.SelectedRowsCount; i++)
                {
                    int rowHandle = gviewComponenti.GetSelectedRows()[i];
                    ret = ControlRettaGruppiPuntiComponenti.DeleteRettaGruppiPuntiComponenti(Convert.ToInt32(gviewComponenti.GetRowCellValue(rowHandle, "IDRetta_GruppoPunti_Componenti")));
                }

                if (ret == 1)
                {
                    XtraMessageBox.Show("Dati cancellati.", "Cancella", MessageBoxButtons.OK);
                }
                else
                {
                    XtraMessageBox.Show("La cancellazione dei dati è terminata con errore.", "Cancella", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                ControlRettaGruppiPuntiComponenti = null;

                LoadGrid();
            }
            else
            {
                XtraMessageBox.Show("Cancellazione annullata.", "Cancella", MessageBoxButtons.OK);
            }
        }

        private void butCreaSoluzioni_Click(object sender, EventArgs e)
        {
            int ret = 0;
            int ret1 = 0;

            if ((cboNumeroPunti.EditValue!="") && (cboNumeroPunti.EditValue != null))
            {
                for (int i = 0; i < Convert.ToInt32(cboNumeroPunti.EditValue); i++)
                {
                    Core.Model.Model_Rette ModelRetta = new Core.Model.Model_Rette();
                    Core.Control.Control_Rette ControlRetta = new Core.Control.Control_Rette();
                    ControlRetta.ConnectionString = ConnectioString;
                    ModelRetta = ControlRetta.GetRettaByID(IDRetta).First().Value;

                    Core.Model.Model_Soluzioni ModelSoluzione = new Core.Model.Model_Soluzioni();
                    Core.Control.Controller_Soluzioni ControlSoluzione = new Core.Control.Controller_Soluzioni();
                    ControlSoluzione.ConnectionString = ConnectioString;

                    Core.Control.Control_Transcode ControlTranscode = new Core.Control.Control_Transcode();

                    ControlSoluzione.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                    if(ModelRetta.Tipologia == "Retta Taratura Modello")
                    {
                        ModelSoluzione.Tipologia = "Soluzione MR Modello";
                    }
                    else
                    {
                        ModelSoluzione.Tipologia = "Soluzione MR per Taratura";
                    }

                    ModelSoluzione.Nome = "L" + Convert.ToString(ControlRetta.GetNextPointNumberInRettaFromIDRetta(IDRetta)) + " - " + ModelRetta.Nome;
                    ModelSoluzione.NotePrescrittive = "";
                    ModelSoluzione.NoteDescrittive = "";
                    ModelSoluzione.IDSchedaDocumenti = ModelRetta.IDSchedaDocumenti;
                    ModelSoluzione.IDSolvente = ModelRetta.IDSolvente;
                    ModelSoluzione.IDApparecchio = Convert.ToInt32(null);
                    //ModelSoluzione.IDUtensile = 49;
                    ModelSoluzione.IDUtensile = Convert.ToInt32(null);
                    ModelSoluzione.IDApparecchio2 = Convert.ToInt32(null);
                    ModelSoluzione.IDUtensile2 = Convert.ToInt32(null);
                    ModelSoluzione.VolumeFinale = 0;
                    ModelSoluzione.UMVolumeFinale = "ml";
                    ModelSoluzione.IDStato = 5;
                    ModelSoluzione.IDUbicazione = 30;
                    ModelSoluzione.DataCreazione = DateTime.Now;
                    ModelSoluzione.IDUtente = IDUtente;
                    ModelSoluzione.DefaultGiorniScadenza = Convert.ToInt32(null);
                    ModelSoluzione.DataPreparazione = DateTime.Now;

                    ControlSoluzione.IDUtente = IDUtente;

                    ret = ControlSoluzione.AddSolution(ModelSoluzione);

                    EOS.Core.Model.Model_Rette_Soluzioni ModelRetteSoluzioni = new EOS.Core.Model.Model_Rette_Soluzioni();
                    EOS.Core.Control.Control_RetteSoluzioni ControlRetteSoluzioni = new EOS.Core.Control.Control_RetteSoluzioni();
                    ControlRetteSoluzioni.ConnectionString = ConnectioString;
                    ModelRetteSoluzioni.IDSoluzione = ret;
                    ModelRetteSoluzioni.IDRetta = IDRetta;
                    ModelRetteSoluzioni.DataCorrelazione = DateTime.Now;
                    ControlRetteSoluzioni.AddRetteSoluzioni(ModelRetteSoluzioni);
                    ModelRetteSoluzioni = null;
                    ControlRetteSoluzioni = null;

                    gviewComponenti.MoveLast();
                    gviewComponenti.MoveFirst();

                    for (int x = 0; x < gviewComponenti.DataRowCount; x++)
                    {
                        EOS.Core.Model.Model_Soluzioni_Details ModelSoluzioniDetail = new EOS.Core.Model.Model_Soluzioni_Details();
                        EOS.Core.Control.Control_Soluzioni_Details ControlSoluzioniDetail = new EOS.Core.Control.Control_Soluzioni_Details();
                        ControlSoluzioniDetail.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                        ModelSoluzioniDetail.IDSoluzioneMaster = Convert.ToInt32(ret);
                        ModelSoluzioniDetail.Tipologia_MR = Convert.ToInt32(gviewComponenti.GetRowCellValue(x, "Tipologia_MR"));
                        ModelSoluzioniDetail.IDSchedaDocumenti = Convert.ToInt32(gviewComponenti.GetRowCellValue(x, "IDSchedaDocumenti"));
                        ModelSoluzioniDetail.CAS = Convert.ToString(ControlTranscode.GetCASByIDSchedaDocumenti(Convert.ToInt32(gviewComponenti.GetRowCellValue(x, "IDSchedaDocumenti"))));
                        ModelSoluzioniDetail.IDSoluzione = Convert.ToInt32(gviewComponenti.GetRowCellValue(x, "IDSoluzione"));

                        if(ModelSoluzioniDetail.IDSoluzione!=0)
                        {
                            ModelSoluzioniDetail.UM_Prelievo = "ml";
                        }
                        else
                        {
                            ModelSoluzioniDetail.UM_Prelievo = ControlTranscode.GetUMByIDTipoMateriale(Convert.ToInt32(gviewComponenti.GetRowCellValue(x, "Tipologia_MR")));
                        }

                        ModelSoluzioniDetail.Quantita_Prelievo = 0;
                        ModelSoluzioniDetail.IDApparecchio = Convert.ToInt32(null);
                        //ModelSoluzioniDetail.IDUtensile = 49;
                        ModelSoluzioniDetail.IDUtensile = Convert.ToInt32(null);
                        ModelSoluzioniDetail.IDApparecchio2 = Convert.ToInt32(null);
                        ModelSoluzioniDetail.IDUtensile2 = Convert.ToInt32(null);
                        ModelSoluzioniDetail.DataScadenza = Convert.ToDateTime(null);
                        ModelSoluzioniDetail.Concentrazione = 0;
                        ModelSoluzioniDetail.Note = Convert.ToString(gviewComponenti.GetRowCellValue(x, "Note"));

                        ControlSoluzioniDetail.IDUtente = IDUtente;
                        ret1 = ControlSoluzioniDetail.AddSolutionDetail(ModelSoluzioniDetail);

                        if (ret1 == 0)
                        {
                            XtraMessageBox.Show("Inserimento componenti terminato con errore nella soluzione di livello "+ Convert.ToString(ControlRetta.GetNextPointNumberInRettaFromIDRetta(IDRetta)) + ".", "Inserisci", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
  
                        ModelSoluzioniDetail = null;
                        ControlSoluzioniDetail = null;

                        this.Close();
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Per creare le soluzioni è necessario specificare il numero di punti!", "Crea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}