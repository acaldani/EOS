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

namespace EOS.UI
{
    public partial class frmSeleziona : Form
    {
        public bool SceltaEffettuata = false;
        public string Conferma="no";
        public int TipoMaterialeMR;
        public string CAS;
        public DateTime DataScadenza;
        public string TipoElenco;
        public int ID;
        public int IDCaller;
        public int IDUtente;
        public string NotaUtilizzoScaduto="";

        public frmSeleziona()
        {
            InitializeComponent();
        }

        private void SelezionaRecord_Shown(object sender, EventArgs e)
        {
            if (TipoElenco == "Soluzioni")
            {
                xtraTabControl1.TabPages[1].PageVisible = false;
            }
            else
            {
                xtraTabControl1.TabPages[0].PageVisible = false;
            }
        }

        private void SelezionaRecord_Load(object sender, EventArgs e)
        {
            this.materiale_TipologiaTableAdapter.Fill(this.soluzioni.Materiale_Tipologia);
            string SQL ="";
            string ConnectionString = "";

            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

            global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();

            if (TipoElenco=="Soluzioni")
            {
                DataTable dtMaterialiMR = new DataTable();

                SQL = "";
                SQL = SQL + "SELECT IDSchedaDocumenti, ";
                SQL = SQL + "ARTICOLO.Identificativo, ";
                SQL = SQL + "LOTTO.Lotto, ";
                SQL = SQL + "ARTICOLO.DenominazioneProdotto, ";
                SQL = SQL + "ARTICOLO.Cas as CAS, ";
                SQL = SQL + "LOTTO.DataInserimento, ";
                SQL = SQL + "LOTTO.DataScadenza ";
                SQL = SQL + "FROM [SERVER026].[LUPIN].[dbo].[Materiali] ARTICOLO ";
                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].[dbo].TipoMateriale TIPO ";
                SQL = SQL + "ON ARTICOLO.IDTipoMateriale=TIPO.IDTipoMateriale ";
                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].[dbo].Ubicazioni UBICAZIONE ";
                SQL = SQL + "ON ARTICOLO.IDUbicazione=UBICAZIONE.IDUbicazione ";
                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].[dbo].StatoMateriale STATO ";
                SQL = SQL + "ON ARTICOLO.IDStatoMateriale=STATO.IDStatoMateriale ";
                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].[dbo].ClasseMateriale CLASSE ";
                SQL = SQL + "ON ARTICOLO.IDClasseMateriale=CLASSE.IDClasseMateriale ";
                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].[dbo].Reparti REPARTO ";
                SQL = SQL + "ON ARTICOLO.IDReparto=REPARTO.IDReparto ";
                SQL = SQL + "INNER JOIN [SERVER026].[LUPIN].[dbo].[SchedeDocumenti] LOTTO ";
                SQL = SQL + "ON ARTICOLO.IDMateriale=LOTTO.IDMateriale ";
                SQL = SQL + "WHERE LOTTO.IDSchedaDocumenti NOT IN (SELECT IDSchedaDocumenti FROM Soluzioni_Details WHERE IDSoluzioneMaster=" + IDCaller + ") ";

                dtMaterialiMR.Clear();

                dtMaterialiMR = DB.GetDataTable(SQL, ConnectionString);

                gridMaterialiMR.DataSource = null;
                gviewMaterialiMR.Columns.Clear();
                gridMaterialiMR.DataSource = dtMaterialiMR;
                //gviewMaterialiMR.PopulateColumns();
                gridMaterialiMR.ForceInitialize();
                gviewMaterialiMR.Columns[0].Visible = false;
                gviewMaterialiMR.Columns[3].Width = 320;
                gviewMaterialiMR.Columns[4].Width = 70;

                dtMaterialiMR = null;

                DataTable dtSoluzioni = new DataTable();

                SQL = "";
                SQL = SQL + "SELECT SOL.IDSoluzione ";
                SQL = SQL + "      ,SOL.Tipologia AS TipologiaSoluzione ";
                SQL = SQL + "      ,SOL.CodiceSoluzione ";
                SQL = SQL + "      ,SOL.Nome ";
                SQL = SQL + "      ,SOL.DataPreparazione ";
                SQL = SQL + "      ,SOL.DataScadenza ";
                SQL = SQL + "  FROM dbo.Soluzioni SOL ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Ubicazioni UBI ";
                SQL = SQL + "  ON SOL.IDUbicazione=UBI.IDUbicazione ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.SchedeDocumenti [CER] ";
                SQL = SQL + "  ON SOL.IDSchedaDocumenti=[CER].IDSchedaDocumenti ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Materiali MAT ";
                SQL = SQL + "  ON [CER].IDMateriale=MAT.IDMateriale ";
                SQL = SQL + "  LEFT JOIN Solventi SOV ";
                SQL = SQL + "  ON SOL.IDSolvente=SOV.IDSolvente ";
                SQL = SQL + "  LEFT JOIN Login_Utenti UTE ";
                SQL = SQL + "  ON SOL.IDUtente=UTE.IDUtente ";
                SQL = SQL + "  LEFT JOIN Composti_Stati COM ";
                SQL = SQL + "  ON SOL.IDStato=COM.ID ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Apparecchi APP ";
                SQL = SQL + "  ON SOL.IDApparecchio=APP.IDApparecchio ";
                SQL = SQL + "  LEFT JOIN [SERVER026].[LUPIN].DBO.Utensili UTI ";
                SQL = SQL + "  ON SOL.IDUtensile=UTI.IDUtensile ";
                SQL = SQL + "  WHERE SOL.IDSoluzione<>" + IDCaller + " AND SOL.IDSoluzione NOT IN (SELECT IDSoluzione FROM Soluzioni_Details WHERE IDSoluzioneMaster=" + IDCaller + ") ";
                SQL = SQL + "  AND SOL.TIPOLOGIA <> 'Soluzione MR Modello' ";
                SQL = SQL + "  AND COM.NOME IN ('In Uso','Scaduta') ";

                dtSoluzioni.Clear();

                dtSoluzioni = DB.GetDataTable(SQL, ConnectionString);

                gridSoluzioni.DataSource = null;
                gviewSoluzioni.Columns.Clear();
                gridSoluzioni.DataSource = dtSoluzioni;
                //gviewSoluzioni.PopulateColumns();
                gridSoluzioni.ForceInitialize();
                gviewSoluzioni.Columns[0].Visible = false;
                gviewSoluzioni.Columns[1].Width = 140;
                gviewSoluzioni.Columns[2].Width = 90;
                gviewSoluzioni.Columns[3].Width = 240;
                gviewSoluzioni.Columns[4].Width = 70;
                gviewSoluzioni.Columns[5].Width = 70;

                dtSoluzioni = null;

                cboTipoMaterialeMR.Properties.View.ActiveFilterString = " SoluzioneSolvente='Soluzione' ";

                System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmSeleziona", "gridSoluzioni", IDUtente);

                if (str != null)
                {
                    gridSoluzioni.FocusedView.RestoreLayoutFromStream(str);
                    str.Seek(0, System.IO.SeekOrigin.Begin);
                }

                str = frmLogin.LoadUserPreferences("frmSeleziona", "gridMaterialiMR", IDUtente);

                if (str != null)
                {
                    gridMaterialiMR.FocusedView.RestoreLayoutFromStream(str);
                    str.Seek(0, System.IO.SeekOrigin.Begin);
                }

                str = null;
            }

            if (TipoElenco == "Solventi")
            {
                DataTable dtMaterialiMR = new DataTable();

                SQL = "";
                SQL = SQL + "SELECT IDSchedaDocumenti, ";
                SQL = SQL + "ARTICOLO.Identificativo, ";
                SQL = SQL + "LOTTO.Lotto, ";
                SQL = SQL + "ARTICOLO.DenominazioneProdotto, ";
                SQL = SQL + "ARTICOLO.Cas as CAS, ";
                SQL = SQL + "LOTTO.DataInserimento, ";
                SQL = SQL + "LOTTO.DataScadenza ";
                SQL = SQL + "FROM [SERVER026].[LUPIN].[dbo].[Materiali] ARTICOLO ";
                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].[dbo].TipoMateriale TIPO ";
                SQL = SQL + "ON ARTICOLO.IDTipoMateriale=TIPO.IDTipoMateriale ";
                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].[dbo].Ubicazioni UBICAZIONE ";
                SQL = SQL + "ON ARTICOLO.IDUbicazione=UBICAZIONE.IDUbicazione ";
                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].[dbo].StatoMateriale STATO ";
                SQL = SQL + "ON ARTICOLO.IDStatoMateriale=STATO.IDStatoMateriale ";
                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].[dbo].ClasseMateriale CLASSE ";
                SQL = SQL + "ON ARTICOLO.IDClasseMateriale=CLASSE.IDClasseMateriale ";
                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].[dbo].Reparti REPARTO ";
                SQL = SQL + "ON ARTICOLO.IDReparto=REPARTO.IDReparto ";
                SQL = SQL + "INNER JOIN [SERVER026].[LUPIN].[dbo].[SchedeDocumenti] LOTTO ";
                SQL = SQL + "ON ARTICOLO.IDMateriale=LOTTO.IDMateriale ";
                SQL = SQL + "WHERE LOTTO.IDSchedaDocumenti NOT IN (SELECT IDSchedaDocumenti FROM Solventi_Details WHERE IDSolventeMaster=" + IDCaller + ") ";

                dtMaterialiMR.Clear();

                dtMaterialiMR = DB.GetDataTable(SQL, ConnectionString);

                gridMaterialiMR.DataSource = null;
                gviewMaterialiMR.Columns.Clear();
                gridMaterialiMR.DataSource = dtMaterialiMR;
                //gviewMaterialiMR.PopulateColumns();
                gridMaterialiMR.ForceInitialize();
                gviewMaterialiMR.Columns[0].Visible = false;
                gviewMaterialiMR.Columns[3].Width = 320;
                gviewMaterialiMR.Columns[4].Width = 70;

                dtMaterialiMR = null;

                DataTable dtSolventi = new DataTable();

                SQL = "";
                SQL = SQL + "SELECT SOV.IDSolvente ";
                SQL = SQL + "    ,SOV.Tipologia AS TipologiaSolvente ";
                SQL = SQL + "    ,SOV.CodiceSolvente ";
                SQL = SQL + "    ,SOV.Nome ";
                SQL = SQL + "    ,SOV.DataPreparazione ";
                SQL = SQL + "    ,SOV.DataScadenza ";
                SQL = SQL + "FROM [dbo].[Solventi] SOV ";
                SQL = SQL + "LEFT JOIN [SERVER026].[LUPIN].DBO.Ubicazioni UBI ";
                SQL = SQL + "ON SOV.IDUbicazione=UBI.IDUbicazione ";
                SQL = SQL + "LEFT JOIN Login_Utenti UTE ";
                SQL = SQL + "ON SOV.IDUtente=UTE.IDUtente ";
                SQL = SQL + "LEFT JOIN Composti_Stati COM ";
                SQL = SQL + "ON SOV.IDStato=COM.ID ";
                SQL = SQL + "WHERE SOV.IDSolvente<>" + IDCaller + " AND SOV.IDSolvente NOT IN (SELECT IDSolvente FROM Solventi_Details WHERE IDSolventeMaster=" + IDCaller + ") ";
                SQL = SQL + "AND SOV.TIPOLOGIA IN ('Soluzione di Lavoro Standard') ";
                SQL = SQL + "AND COM.NOME IN ('In Uso','Scaduta') ";

                dtSolventi.Clear();

                dtSolventi = DB.GetDataTable(SQL, ConnectionString);

                gridMisceleSolventi.DataSource = null;
                gviewMisceleSolventi.Columns.Clear();
                gridMisceleSolventi.DataSource = dtSolventi;
                //gviewMisceleSolventi.PopulateColumns();
                gridMisceleSolventi.ForceInitialize();
                gviewMisceleSolventi.Columns[0].Visible = false;
                gviewMisceleSolventi.Columns[1].Width = 140;
                gviewMisceleSolventi.Columns[2].Width = 90;
                gviewMisceleSolventi.Columns[3].Width = 240;
                gviewMisceleSolventi.Columns[4].Width = 70;
                gviewMisceleSolventi.Columns[5].Width = 70;

                dtSolventi = null;

                cboTipoMaterialeMR.Properties.View.ActiveFilterString = " SoluzioneSolvente='Solvente' ";

                System.IO.MemoryStream str = frmLogin.LoadUserPreferences("frmSeleziona", "gridMisceleSolventi", IDUtente);

                if (str != null)
                {
                    gridMisceleSolventi.FocusedView.RestoreLayoutFromStream(str);
                    str.Seek(0, System.IO.SeekOrigin.Begin);
                }

                str = frmLogin.LoadUserPreferences("frmSeleziona", "gridMaterialiMR", IDUtente);

                if (str != null)
                {
                    gridMaterialiMR.FocusedView.RestoreLayoutFromStream(str);
                    str.Seek(0, System.IO.SeekOrigin.Begin);
                }

                str = null;
            }
        }

        private void butAnnulla_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool UtilizzoScaduti(DateTime DataScadenza)
        {
            bool Ret=true;

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

                if(result!= null)
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
                                Ret = true;
                            }
                            else
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("Non è possibile utilizzare il materiale scaduto senza specificare una nota", "Utilizzo Materiale Scaduto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                Ret = false;
                            }
                        }
                        else
                        {
                            Ret = false;
                        }
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("Password di autorizzazione non corretta", "Utilizzo Materiale Scaduto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Ret = false;
                    }

                    ControlConfigurazione = null;
                    ModelConfigurazione = null;
                }
                else
                {
                    Ret = false;
                }
            }
            else
            {
                Ret = true;
            }

            return Ret;
        }

        private void burOKSoluzioni_Click(object sender, EventArgs e)
        {
            if (gviewSoluzioni.SelectedRowsCount != 1)
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show("Selezionare una Soluzione", "Soluzione", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DevExpress.XtraEditors.XtraMessageBox.Show("Selezionare una Soluzione MR", "Soluzione MR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ID = Int32.Parse(gviewSoluzioni.GetFocusedRowCellValue("IDSoluzione").ToString());
                TipoMaterialeMR = Convert.ToInt32(null);
                CAS = null;
                if ((gviewSoluzioni.GetFocusedRowCellValue("DataScadenza").Equals(System.DBNull.Value)))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("La data di scadenza dell'elemento selezionato non è valida", "DataScadenza", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DataScadenza = Convert.ToDateTime(gviewSoluzioni.GetFocusedRowCellValue("DataScadenza").ToString());
                    Conferma = "Soluzione";

                    bool retaut = UtilizzoScaduti(DataScadenza);

                    if (retaut == true)
                    {
                        SceltaEffettuata = true;
                        this.Close();
                    }
                }
                
            }
        }

        private void butOKMisceleSolventi_Click(object sender, EventArgs e)
        {
            if (gviewMisceleSolventi.SelectedRowsCount != 1)
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show("Selezionare una Miscela di Solventi", "Miscela di Solventi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DevExpress.XtraEditors.XtraMessageBox.Show("Selezionare una Soluzione di Lavoro", "Soluzione di Lavoro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ID = Int32.Parse(gviewMisceleSolventi.GetFocusedRowCellValue("IDSolvente").ToString());
                TipoMaterialeMR = Convert.ToInt32(null);
                CAS = null;
                if ((gviewMisceleSolventi.GetFocusedRowCellValue("DataScadenza").Equals(System.DBNull.Value)))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("La data di scadenza dell'elemento selezionato non è valida", "DataScadenza", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DataScadenza = Convert.ToDateTime(gviewMisceleSolventi.GetFocusedRowCellValue("DataScadenza").ToString());
                    Conferma = "Solvente";

                    bool retaut = UtilizzoScaduti(DataScadenza);

                    if (retaut == true)
                    {
                        this.Close();
                        SceltaEffettuata = true;
                    }
                }
            }
        }

        private void butOK_Click(object sender, EventArgs e) //Materiali MR
        {
            bool retCheckCas = false;

            if ((gviewMaterialiMR.SelectedRowsCount != 1) || (cboTipoMaterialeMR.EditValue == null))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Selezionare un Materiale MR e la sua tipologia", "Materiale MR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if(Convert.ToInt32(cboTipoMaterialeMR.EditValue)!=4)
                {
                    retCheckCas = CheckCAS(gviewMaterialiMR.GetFocusedRowCellValue("CAS").ToString(),false);
                }
                else
                {
                    retCheckCas = CheckCASWorkingSolution(Convert.ToInt32(gviewMaterialiMR.GetFocusedRowCellValue("IDSchedaDocumenti")));
                }

                if (retCheckCas)
                {
                    ID = Int32.Parse(gviewMaterialiMR.GetFocusedRowCellValue("IDSchedaDocumenti").ToString());
                    //DataRowView dataRow = cboTipoMaterialeMR.GetSelectedDataRow() as DataRowView;
                    //TipoMaterialeMR = Convert.ToInt32(dataRow[""]);
                    TipoMaterialeMR = Convert.ToInt32(cboTipoMaterialeMR.EditValue);
                    CAS = gviewMaterialiMR.GetFocusedRowCellValue("CAS").ToString();
                    if ((gviewMaterialiMR.GetFocusedRowCellValue("DataScadenza").Equals(System.DBNull.Value)))
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("La data di scadenza dell'elemento selezionato non è valida", "DataScadenza", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        DataScadenza = Convert.ToDateTime(gviewMaterialiMR.GetFocusedRowCellValue("DataScadenza").ToString());
                        Conferma = "MaterialeMR";

                        bool retaut = UtilizzoScaduti(DataScadenza);

                        if (retaut == true)
                        {
                            this.Close();
                            SceltaEffettuata = true;
                        }
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Il materiale MR selezionato non presenta uno o più codici identificativi (CAS o Codici Interni)", "Materiale MR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        public bool CheckCAS(string CAS, bool ControlloCongruenzaCodice)
        {
            string[] CASSplitted;
            bool Ritorno=false;
            string Cifre;
            int Ref;
            int Somma=0;
            int Contatore = 1;
            int Modulo;
            int i=0;

            if(ControlloCongruenzaCodice)
            {
                CASSplitted = CAS.Trim().Replace(" ", "").Replace("/", "-").Replace(@"\", " - ").Replace(".", "").Replace(", ", "").Split('-');

                if (CASSplitted.Length == 3)
                {
                    if ((CASSplitted[0].Length <= 7) && (int.TryParse(CASSplitted[0], out Ref)))
                    {
                        if ((CASSplitted[1].Length == 2) && (int.TryParse(CASSplitted[1], out Ref)))
                        {
                            if ((CASSplitted[2].Length == 1) && (int.TryParse(CASSplitted[2], out Ref)))
                            {
                                Cifre = CASSplitted[0] + CASSplitted[1];

                                for (i = Cifre.Length - 1; i >= 0; i--)
                                {
                                    Somma = Somma + (Convert.ToInt32(Cifre[i].ToString()) * Contatore);
                                    Contatore = Contatore + 1;
                                }

                                Modulo = Somma % 10;

                                if (Modulo == Convert.ToInt32(CASSplitted[2]))
                                {
                                    Ritorno = true;
                                }
                                else
                                {
                                    Ritorno = false;
                                }
                            }
                            else
                            {
                                Ritorno = false;
                            }
                        }
                        else
                        {
                            Ritorno = false;
                        }
                    }
                    else
                    {
                        Ritorno = false;
                    }
                }
                else
                {
                    Ritorno = false;
                }
            }
            else
            {
                if(CAS!=null)
                {
                    if(CAS!="")
                    {
                        Ritorno = true;
                    }
                    else
                    {
                        Ritorno = false;
                    }
                }
                else
                {
                    Ritorno = false;
                }
            }

            return Ritorno;
        }

        public bool CheckCASWorkingSolution(int IDSchedaDocumenti)
        {
            bool ret=true;
            string SQLString = "";
            string CheckError = "";

            SQLString = SQLString + "select WSD.NomeComponente, WSD.CasComponente ";
            SQLString = SQLString + "from [SERVER026].[LUPIN].dbo.WorkingSolutionsDetails WSD ";
            SQLString = SQLString + "where WSD.IDSchedaDocumenti={0} ";
            SQLString = SQLString + "GROUP BY WSD.NomeComponente, WSD.CasComponente ";
            SQLString = SQLString + "ORDER BY WSD.NomeComponente, WSD.CasComponente ";

            SQLString = string.Format(SQLString, IDSchedaDocumenti);

            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString; ;
                cnn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = SQLString;

                    System.Data.SqlClient.SqlDataReader dr;
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if(CheckCAS(Convert.ToString(dr["CasComponente"]),false))
                            {
                                
                            }
                            else
                            {
                                ret = false;
                                CheckError = CheckError + "CAS non valido per l'elemento " + Convert.ToString(dr["CasComponente"]) + " " + Convert.ToString(dr["NomeComponente"]) + System.Environment.NewLine;
                            }
                        }
                    }
                    else
                    {
                        ret = false;
                        CheckError = CheckError + "Non sono presenti dati relativi ai componenti della working solution." + System.Environment.NewLine;
                    }

                    if(CheckError!="")
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(CheckError, "Materiale MR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    dr = null;
                }
            }

            return ret;
        }

        private void frmSeleziona_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(TipoElenco=="Soluzioni")
            {
                System.IO.MemoryStream str = new System.IO.MemoryStream();
                gridSoluzioni.FocusedView.SaveLayoutToStream(str);
                str.Seek(0, System.IO.SeekOrigin.Begin);
                frmLogin.SaveUserPreferences("frmSeleziona", "gridSoluzioni", str, IDUtente);

                str = new System.IO.MemoryStream();
                gridMaterialiMR.FocusedView.SaveLayoutToStream(str);
                str.Seek(0, System.IO.SeekOrigin.Begin);
                frmLogin.SaveUserPreferences("frmSeleziona", "gridMaterialiMR", str, IDUtente);

                str = null;
            }
            else
            {
                System.IO.MemoryStream str = new System.IO.MemoryStream();
                gridMisceleSolventi.FocusedView.SaveLayoutToStream(str);
                str.Seek(0, System.IO.SeekOrigin.Begin);
                frmLogin.SaveUserPreferences("frmSeleziona", "gridMisceleSolventi", str, IDUtente);

                str = new System.IO.MemoryStream();
                gridMaterialiMR.FocusedView.SaveLayoutToStream(str);
                str.Seek(0, System.IO.SeekOrigin.Begin);
                frmLogin.SaveUserPreferences("frmSeleziona", "gridMaterialiMR", str, IDUtente);

                str = null;
            }
        }
    }
}
