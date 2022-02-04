using EOS.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using System.Linq;
using DevExpress.XtraBars;
using System.Dynamic;
using System.Runtime.Remoting;
using System.Reflection;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace EOS.Core.Control
{
    public class Controller_Soluzioni
    {

        public string ConnectionString;
        public int IDUtente=0;

        public IDictionary<int, Model_Soluzioni> GetSolutionByID(int IDSoluzione)
        {
            string SQLString = string.Format("SELECT * FROM Soluzioni WHERE IDSoluzione={0}", IDSoluzione);
            return GetData(SQLString);
        }

        public string GetCodiceSoluzioneFromIDSoluzione(int IDSoluzione)
        {
            string ret = "";

            string SQLString = "Select CodiceSoluzione from Soluzioni WHERE IDSoluzione='" + IDSoluzione + "' ";

            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                cnn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = SQLString;

                    var dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ret = dr["CodiceSoluzione"].ToString();
                        }
                    }
                }
            }

            return ret;
        }

        public decimal GetVolumeFinaleFromIDSoluzione(int IDSoluzione)
        {
            decimal ret = 0;

            string SQLString = "Select VolumeFinale from Soluzioni WHERE IDSoluzione='" + IDSoluzione + "' ";

            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                cnn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = SQLString;

                    var dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ret = Convert.ToDecimal(dr["VolumeFinale"]);
                        }
                    }
                }
            }

            return ret;
        }

        public int AddSolution(Model_Soluzioni Soluzione)
        {
            int newid=0;
            object newcode = "";
            string DataPreparazione;
            string DataCreazione;
            string DataScadenza;

            try
            {

                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                SqlCommand cmdstore = new SqlCommand();
                cmdstore.Connection = cnn;
                cmdstore.CommandType = CommandType.StoredProcedure;
                cmdstore.CommandText = "dbo.GetProgressivo";

                cmdstore.Parameters.Add("@TIPOPROGRESSIVO", SqlDbType.VarChar, 50).Value = "SOL";

                newcode = cmdstore.ExecuteScalar();

                cmdstore = null;

                string SQLString = "";
                SQLString = SQLString + "INSERT INTO [dbo].[Soluzioni] ";
                SQLString = SQLString + "           ([CodiceSoluzione] ";
                SQLString = SQLString + "           ,[Tipologia] ";
                SQLString = SQLString + "           ,[Nome] ";
                SQLString = SQLString + "           ,[NotePrescrittive] ";
                SQLString = SQLString + "           ,[NoteDescrittive] ";
                SQLString = SQLString + "           ,[IDUbicazione] ";
                SQLString = SQLString + "           ,[IDSchedaDocumenti] ";
                SQLString = SQLString + "           ,[IDSolvente] ";
                SQLString = SQLString + "           ,[VolumeFinale] ";
                SQLString = SQLString + "           ,[UMVolumeFinale] ";
                SQLString = SQLString + "           ,[DefaultGiorniScadenza] ";
                SQLString = SQLString + "           ,[DataPreparazione] ";
                SQLString = SQLString + "           ,[DataScadenza] ";
                SQLString = SQLString + "           ,[DataCreazione] ";
                SQLString = SQLString + "           ,[IDUtente] ";
                SQLString = SQLString + "           ,[IDStato] ";
                SQLString = SQLString + "           ,[IDApparecchio] ";
                SQLString = SQLString + "           ,[IDUtensile] ";
                SQLString = SQLString + "           ,[IDApparecchio2] ";
                SQLString = SQLString + "           ,[IDUtensile2]) ";
                SQLString = SQLString + "     VALUES ";
                SQLString = SQLString + "           ('{0}' ";
                SQLString = SQLString + "           ,'{1}' ";
                SQLString = SQLString + "           ,'{2}' ";
                SQLString = SQLString + "           ,'{3}' ";
                SQLString = SQLString + "           ,'{4}' ";
                SQLString = SQLString + "           ,{5} ";
                SQLString = SQLString + "           ,{6} ";
                SQLString = SQLString + "           ,{7} ";
                SQLString = SQLString + "           ,{8} ";
                SQLString = SQLString + "           ,'{9}' ";
                SQLString = SQLString + "           ,{10} ";
                SQLString = SQLString + "           ,Case When '{11}'<>'01/01/0001 00:00:00' then '{11}' else null end ";
                SQLString = SQLString + "           ,Case When '{12}'<>'01/01/0001 00:00:00' then '{12}' else null end ";
                SQLString = SQLString + "           ,Case When '{13}'<>'01/01/0001 00:00:00' then '{13}' else null end ";
                SQLString = SQLString + "           ,{14} ";
                SQLString = SQLString + "           ,{15} ";
                SQLString = SQLString + "           ,{16} ";
                SQLString = SQLString + "           ,{17} ";
                SQLString = SQLString + "           ,{18} ";
                SQLString = SQLString + "           ,{19}); SELECT SCOPE_IDENTITY() ";

                if((Soluzione.DataCreazione!=null) && (Soluzione.DataCreazione != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataCreazione = Soluzione.DataCreazione.ToString();
                }
                else
                {
                    DataCreazione = "01/01/0001 00:00:00";
                }

                if ((Soluzione.DataPreparazione != null) && (Soluzione.DataPreparazione != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataPreparazione = Soluzione.DataPreparazione.ToString();
                }
                else
                {
                    DataPreparazione = "01/01/0001 00:00:00";
                }

                if ((Soluzione.DataScadenza != null) && (Soluzione.DataScadenza !=Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataScadenza = Soluzione.DataScadenza.ToString();
                }
                else
                {
                    DataScadenza = "01/01/0001 00:00:00";
                }

                SQLString = string.Format(SQLString, newcode.ToString(), Soluzione.Tipologia, Soluzione.Nome.Replace("'", "''"), Soluzione.NotePrescrittive.Replace("'", "''"), Soluzione.NoteDescrittive.Replace("'", "''"), Soluzione.IDUbicazione, Soluzione.IDSchedaDocumenti, Soluzione.IDSolvente, Soluzione.VolumeFinale.ToString().Replace(",", ".").Replace("'", "''"), Soluzione.UMVolumeFinale.Replace("'", "''"), Soluzione.DefaultGiorniScadenza, DataPreparazione, DataScadenza, DataCreazione, Soluzione.IDUtente, Soluzione.IDStato, Soluzione.IDApparecchio, Soluzione.IDUtensile, Soluzione.IDApparecchio2, Soluzione.IDUtensile2);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                newid = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = null;
                cnn = null;

                AddLogSoluzione("Inserimento", Soluzione, DataPreparazione, DataScadenza, DataCreazione, newid);

                return newid;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int UpdateSolution(Model_Soluzioni Soluzione, int addLog=1)
        {
            string DataPreparazione;
            string DataCreazione;
            string DataScadenza;

            try
            {
                Model_Soluzioni SoluzioneOriginale = this.GetSolutionByID(Soluzione.IDSoluzione).First().Value;

                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "UPDATE [dbo].[Soluzioni] ";
                SQLString = SQLString + "   SET [Tipologia] = '{0}' ";
                SQLString = SQLString + "      ,[Nome] = '{1}' ";
                SQLString = SQLString + "      ,[NotePrescrittive] = '{2}' ";
                SQLString = SQLString + "      ,[NoteDescrittive] = '{3}' ";
                SQLString = SQLString + "      ,[IDUbicazione] = {4} ";
                SQLString = SQLString + "      ,[IDSchedaDocumenti] = {5} ";
                SQLString = SQLString + "      ,[IDSolvente] = {6} ";
                SQLString = SQLString + "      ,[VolumeFinale] = {7} ";
                SQLString = SQLString + "      ,[UMVolumeFinale] = '{8}' ";
                SQLString = SQLString + "      ,[DefaultGiorniScadenza] = {9} ";
                SQLString = SQLString + "      ,[DataPreparazione] = Case When '{10}'<>'01/01/0001 00:00:00' then '{10}' else null end ";
                SQLString = SQLString + "      ,[DataScadenza] = Case When '{11}'<>'01/01/0001 00:00:00' then '{11}' else null end ";
                SQLString = SQLString + "      ,[DataCreazione] = Case When '{12}'<>'01/01/0001 00:00:00' then '{12}' else null end ";
                SQLString = SQLString + "      ,[IDUtente] = {13} ";
                SQLString = SQLString + "      ,[IDStato] = {14} ";
                SQLString = SQLString + "      ,[IDApparecchio] = {15} ";
                SQLString = SQLString + "      ,[IDUtensile] = {16} ";
                SQLString = SQLString + "      ,[IDApparecchio2] = {17} ";
                SQLString = SQLString + "      ,[IDUtensile2] = {18} ";
                SQLString = SQLString + " WHERE  IDSoluzione={19} ";

                if ((Soluzione.DataCreazione != null) && (Soluzione.DataCreazione != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataCreazione = Soluzione.DataCreazione.ToString();
                }
                else
                {
                    DataCreazione = "01/01/0001 00:00:00";
                }

                if ((Soluzione.DataPreparazione != null) && (Soluzione.DataPreparazione != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataPreparazione = Soluzione.DataPreparazione.ToString();
                }
                else
                {
                    DataPreparazione = "01/01/0001 00:00:00";
                }

                if ((Soluzione.DataScadenza != null) && (Soluzione.DataScadenza != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataScadenza = Soluzione.DataScadenza.ToString();
                }
                else
                {
                    DataScadenza = "01/01/0001 00:00:00";
                }

                SQLString = string.Format(SQLString, Soluzione.Tipologia, Soluzione.Nome.Replace("'", "''"), Soluzione.NotePrescrittive.Replace("'", "''"), Soluzione.NoteDescrittive.Replace("'", "''"), Soluzione.IDUbicazione, Soluzione.IDSchedaDocumenti, Soluzione.IDSolvente, Soluzione.VolumeFinale.ToString().Replace(",", ".").Replace("'", "''"), Soluzione.UMVolumeFinale.Replace("'", "''"), Soluzione.DefaultGiorniScadenza, DataPreparazione, DataScadenza, DataCreazione, Soluzione.IDUtente, Soluzione.IDStato, Soluzione.IDApparecchio, Soluzione.IDUtensile, Soluzione.IDApparecchio2, Soluzione.IDUtensile2, Soluzione.IDSoluzione);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

                cmd = null;
                cnn = null;

                if(addLog==1)
                {
                    AddLogSoluzione("Aggiornamento", Soluzione, DataPreparazione, DataScadenza, DataCreazione, 0, SoluzioneOriginale);
                }
                
                SoluzioneOriginale = null;

                EOS.Core.Control.Control_Calcolo ControlCalcolo = new EOS.Core.Control.Control_Calcolo();
                ControlCalcolo.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                if (ControlCalcolo.CountComponent(Soluzione.IDSoluzione, "Soluzione") > 0)
                {
                    EOS.Core.Control.Controller_Soluzioni ControlSoluzioni = new Core.Control.Controller_Soluzioni();
                    ControlSoluzioni.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                    EOS.Core.Model.Model_Soluzioni ModelSoluzioni = new Core.Model.Model_Soluzioni();
                    ModelSoluzioni = ControlSoluzioni.GetSolutionByID(Soluzione.IDSoluzione).First().Value;

                    ControlCalcolo.CalcolaConcentrazione(ModelSoluzioni.IDSoluzione);

                    ModelSoluzioni = ControlSoluzioni.GetSolutionByID(ModelSoluzioni.IDSoluzione).First().Value;
                    ControlCalcolo.SetDataScadenza(ModelSoluzioni.IDSoluzione, ModelSoluzioni.DefaultGiorniScadenza, ModelSoluzioni.DataPreparazione, "Soluzione");

                    ModelSoluzioni = null;
                    ControlSoluzioni = null;
                }

                ControlCalcolo = null;

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int AddLogSoluzione(string TipoOperazione, Model_Soluzioni Soluzione, string DataPreparazione, string DataScadenza, string DataCreazione, int newid = 0, Model_Soluzioni SoluzioneOriginale=null)
        {
            try
            {
                Control_Transcode ctlTranscode = new Control_Transcode();
                Control_Log ctlLog = new Control_Log();

                if(newid!=0)
                {
                    Soluzione.IDSoluzione = newid;
                }

                string SQLStringLog = "";

                if(TipoOperazione=="Inserimento")
                {
                    SQLStringLog = SQLStringLog + "CodiceSoluzioneMR = " + ctlTranscode.GetCodiceSoluzioneByID(Soluzione.IDSoluzione) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "Tipologia = " + Soluzione.Tipologia + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "Nome = " + Soluzione.Nome.Replace("'", "''") + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "NotePrescrittive = " + Soluzione.NotePrescrittive.Replace("'", "''") + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "NoteDescrittive= " + Soluzione.NoteDescrittive.Replace("'", "''") + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "CodiceSoluzioneDiLavoro (Solvente) = " + ctlTranscode.GetCodiceSolventeByID(Soluzione.IDSolvente) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "Identificativo - LottoMateriale (Solvente) = " + ctlTranscode.GetIdentificativoELottoByID(Soluzione.IDSchedaDocumenti) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "NumeroApparecchio = " + ctlTranscode.GetCodiceApparecchioByID(Soluzione.IDApparecchio) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "NomeUtensile = " + ctlTranscode.GetNomeUtensileByID(Soluzione.IDUtensile) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "NumeroApparecchio2 = " + ctlTranscode.GetCodiceApparecchioByID(Soluzione.IDApparecchio2) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "NomeUtensile2 = " + ctlTranscode.GetNomeUtensileByID(Soluzione.IDUtensile2) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "UMVolumeFinale = " + Soluzione.UMVolumeFinale.Replace("'", "''") + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "VolumeFinale = " + Soluzione.VolumeFinale.ToString().Replace(",", ".").Replace("'", "''") + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "DefaultGiorniScadenza = " + Soluzione.DefaultGiorniScadenza + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "DataScadenza = " + DataScadenza + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "DataPreparazione = " + DataPreparazione + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "Ubicazione = " + ctlTranscode.GetUbicazioneByID(Soluzione.IDUbicazione) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "Stato = " + ctlTranscode.GetStatoByID(Soluzione.IDStato) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "DataCreazione=" + DataCreazione + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "NomeUtente = " + ctlTranscode.GetNomeUtenteByID(Soluzione.IDUtente) + System.Environment.NewLine;
                }
                else
                {
                    SQLStringLog = SQLStringLog + "CodiceSoluzioneMR = " + ctlTranscode.GetCodiceSoluzioneByID(Soluzione.IDSoluzione) + System.Environment.NewLine;

                    if (Soluzione.Tipologia!= SoluzioneOriginale.Tipologia)
                    {
                        SQLStringLog = SQLStringLog + "Tipologia Precedente = " + SoluzioneOriginale.Tipologia + " - Attuale = " + Soluzione.Tipologia + System.Environment.NewLine;
                    }

                    if (Soluzione.Nome != SoluzioneOriginale.Nome)
                    {
                        SQLStringLog = SQLStringLog + "Nome Precedente = " + SoluzioneOriginale.Nome.Replace("'", "''") + " - Attuale = " + Soluzione.Nome.Replace("'", "''") + System.Environment.NewLine;
                    }

                    if (Soluzione.NotePrescrittive != SoluzioneOriginale.NotePrescrittive)
                    {
                        SQLStringLog = SQLStringLog + "NotePrescrittive Precedenti = " + SoluzioneOriginale.NotePrescrittive.Replace("'", "''") + " - Attuali = " + Soluzione.NotePrescrittive.Replace("'", "''") + System.Environment.NewLine;
                    }

                    if (Soluzione.NoteDescrittive != SoluzioneOriginale.NoteDescrittive)
                    {
                        SQLStringLog = SQLStringLog + "NoteDescrittive Precedenti = " + SoluzioneOriginale.NoteDescrittive.Replace("'", "''") + " - Attuali = " + Soluzione.NoteDescrittive.Replace("'", "''") + System.Environment.NewLine;
                    }

                    if (Soluzione.IDSolvente != SoluzioneOriginale.IDSolvente)
                    {
                        SQLStringLog = SQLStringLog + "CodiceSoluzioneDiLavoro Precedente (Solvente) = " + ctlTranscode.GetCodiceSolventeByID(SoluzioneOriginale.IDSolvente) + " - Attuale = " + ctlTranscode.GetCodiceSolventeByID(Soluzione.IDSolvente) + System.Environment.NewLine;
                    }

                    if (Soluzione.IDSchedaDocumenti != SoluzioneOriginale.IDSchedaDocumenti)
                    {
                        SQLStringLog = SQLStringLog + "Identificativo - LottoMateriale  Precedente (Solvente) = " + ctlTranscode.GetIdentificativoELottoByID(SoluzioneOriginale.IDSchedaDocumenti) + " - Attuale (Solvente) = " + ctlTranscode.GetIdentificativoELottoByID(Soluzione.IDSchedaDocumenti) + System.Environment.NewLine;
                    }

                    if (Soluzione.IDApparecchio != SoluzioneOriginale.IDApparecchio)
                    {
                        SQLStringLog = SQLStringLog + "NumeroApparecchio  Precedente = " + ctlTranscode.GetCodiceApparecchioByID(SoluzioneOriginale.IDApparecchio) + " - Attuale = " + ctlTranscode.GetCodiceApparecchioByID(Soluzione.IDApparecchio) + System.Environment.NewLine;
                    }

                    if (Soluzione.IDUtensile != SoluzioneOriginale.IDUtensile)
                    {
                        SQLStringLog = SQLStringLog + "NomeUtensile  Precedente = " + ctlTranscode.GetNomeUtensileByID(SoluzioneOriginale.IDUtensile) + " - Attuale = " + ctlTranscode.GetNomeUtensileByID(Soluzione.IDUtensile) + System.Environment.NewLine;
                    }

                    if (Soluzione.IDApparecchio2 != SoluzioneOriginale.IDApparecchio2)
                    {
                        SQLStringLog = SQLStringLog + "NumeroApparecchio2  Precedente = " + ctlTranscode.GetCodiceApparecchioByID(SoluzioneOriginale.IDApparecchio2) + " - Attuale = " + ctlTranscode.GetCodiceApparecchioByID(Soluzione.IDApparecchio2) + System.Environment.NewLine;
                    }

                    if (Soluzione.IDUtensile2 != SoluzioneOriginale.IDUtensile2)
                    {
                        SQLStringLog = SQLStringLog + "NomeUtensile2  Precedente = " + ctlTranscode.GetNomeUtensileByID(SoluzioneOriginale.IDUtensile2) + " - Attuale = " + ctlTranscode.GetNomeUtensileByID(Soluzione.IDUtensile2) + System.Environment.NewLine;
                    }

                    if (Soluzione.UMVolumeFinale != SoluzioneOriginale.UMVolumeFinale)
                    {
                        SQLStringLog = SQLStringLog + "UMVolumeFinale  Precedente = " + SoluzioneOriginale.UMVolumeFinale.Replace("'", "''") + " - Attuale = " + Soluzione.UMVolumeFinale.Replace("'", "''") + System.Environment.NewLine;
                    }

                    if (Soluzione.VolumeFinale != SoluzioneOriginale.VolumeFinale)
                    {
                        SQLStringLog = SQLStringLog + "VolumeFinale  Precedente = " + SoluzioneOriginale.VolumeFinale.ToString().Replace(",", ".").Replace("'", "''") + " - Attuale = " + Soluzione.VolumeFinale.ToString().Replace(",", ".").Replace("'", "''") + System.Environment.NewLine;
                    }

                    if (Soluzione.DefaultGiorniScadenza != SoluzioneOriginale.DefaultGiorniScadenza)
                    {
                        SQLStringLog = SQLStringLog + "DefaultGiorniScadenza  Precedente = " + SoluzioneOriginale.DefaultGiorniScadenza + " - Attuale = " + Soluzione.DefaultGiorniScadenza + System.Environment.NewLine;
                    }

                    if (Soluzione.DataScadenza != SoluzioneOriginale.DataScadenza)
                    {
                        SQLStringLog = SQLStringLog + "DataScadenza  Precedente = " + SoluzioneOriginale.DataScadenza + " - Attuale = " + DataScadenza + System.Environment.NewLine;
                    }

                    if (Soluzione.DataPreparazione != SoluzioneOriginale.DataPreparazione)
                    {
                        SQLStringLog = SQLStringLog + "DataPreparazione  Precedente = " + SoluzioneOriginale.DataPreparazione + " - Attuale = " + DataPreparazione + System.Environment.NewLine;
                    }

                    if (Soluzione.IDUbicazione != SoluzioneOriginale.IDUbicazione)
                    {
                        SQLStringLog = SQLStringLog + "Ubicazione  Precedente = " + ctlTranscode.GetUbicazioneByID(SoluzioneOriginale.IDUbicazione) + " - Attuale = " + ctlTranscode.GetUbicazioneByID(Soluzione.IDUbicazione) + System.Environment.NewLine;
                    }

                    if (Soluzione.IDStato != SoluzioneOriginale.IDStato)
                    {
                        SQLStringLog = SQLStringLog + "Stato  Precedente = " + ctlTranscode.GetStatoByID(SoluzioneOriginale.IDStato) + " - Attuale = " + ctlTranscode.GetStatoByID(Soluzione.IDStato) + System.Environment.NewLine;
                    }
                }

                ctlLog.ConnectionString = ConnectionString;

                ctlLog.InsertLog(TipoOperazione, "Soluzioni MR", Soluzione.IDSoluzione, ctlTranscode.GetCodiceSoluzioneByID(Soluzione.IDSoluzione), SQLStringLog, IDUtente);
                
                ctlTranscode = null;
                ctlLog = null;

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int DeleteSolution(Model_Soluzioni Soluzione)
        {
            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "DELETE FROM [dbo].[Soluzioni] WHERE IDSoluzione={0} ";
                SQLString = string.Format(SQLString, Soluzione.IDSoluzione);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

                SQLString = "";
                SQLString = SQLString + "DELETE FROM [dbo].[SoluzioniDetails] WHERE IDSoluzioneMaster={0} ";
                SQLString = string.Format(SQLString, Soluzione.IDSoluzione);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

                cmd = null;
                cnn = null;

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int CloneSolution(int IDSoluzione)
        {
            int newid;
            object newcode = "";

            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                SqlCommand cmdstore = new SqlCommand();
                cmdstore.Connection = cnn;
                cmdstore.CommandType = CommandType.StoredProcedure;
                cmdstore.CommandText = "dbo.GetProgressivo";

                cmdstore.Parameters.Add("@TIPOPROGRESSIVO", SqlDbType.VarChar, 50).Value = "SOL";

                newcode = cmdstore.ExecuteScalar();

                cmdstore = null;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "INSERT INTO [Soluzioni] ";
                SQLString = SQLString + "SELECT ";
                SQLString = SQLString + "[Tipologia] ";
                SQLString = SQLString + ",[Nome] as Nome ";
                SQLString = SQLString + ",[NotePrescrittive] ";
                SQLString = SQLString + ",[NoteDescrittive] ";
                SQLString = SQLString + ",[IDUbicazione] ";
                SQLString = SQLString + ",[IDSchedaDocumenti] ";
                SQLString = SQLString + ",[IDSolvente] ";
                SQLString = SQLString + ",[VolumeFinale] ";
                SQLString = SQLString + ",[UMVolumeFinale] ";
                SQLString = SQLString + ",[DefaultGiorniScadenza] ";
                SQLString = SQLString + ",getdate() as DataPreparazione ";
                SQLString = SQLString + ",NULL as DataScadenza ";
                SQLString = SQLString + ",getdate() as DataCreazione ";
                SQLString = SQLString + ",[IDUtente] ";
                SQLString = SQLString + ",5 as IDStato ";
                SQLString = SQLString + ",[IDApparecchio] ";
                SQLString = SQLString + ",[IDUtensile] ";
                SQLString = SQLString + ",'" + newcode.ToString() + "' as CodiceSoluzione ";
                SQLString = SQLString + ",[IDApparecchio2] ";
                SQLString = SQLString + ",[IDUtensile2] ";
                SQLString = SQLString + "FROM[EOS].[dbo].[Soluzioni] ";
                SQLString = SQLString + "WHERE IDSoluzione = {0}; SELECT SCOPE_IDENTITY() ";
                SQLString = string.Format(SQLString, IDSoluzione);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                newid = Convert.ToInt32(cmd.ExecuteScalar());

                SQLString = "";
                SQLString = SQLString + "INSERT INTO Soluzioni_Details ";
                SQLString = SQLString + "SELECT ";
                SQLString = SQLString + "{0} ";
                SQLString = SQLString + ",[Tipologia_MR] ";
                SQLString = SQLString + ",[IDSchedaDocumenti] ";
                SQLString = SQLString + ",[CAS] ";
                SQLString = SQLString + ",[IDSoluzione] ";
                SQLString = SQLString + ",[UM_Prelievo] ";
                SQLString = SQLString + ",[Quantita_Prelievo] ";
                SQLString = SQLString + ",[IDApparecchio] ";
                SQLString = SQLString + ",[IDUtensile] ";
                SQLString = SQLString + ",[Note] ";
                SQLString = SQLString + ",[Concentrazione] ";
                SQLString = SQLString + ",[DataScadenza] ";
                SQLString = SQLString + ",[IDApparecchio2] ";
                SQLString = SQLString + ",[IDUtensile2] ";
                SQLString = SQLString + "FROM [EOS].[dbo].[Soluzioni_Details] ";
                SQLString = SQLString + "WHERE IDSoluzioneMaster={1} ";
                SQLString = string.Format(SQLString, newid, IDSoluzione);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

                cmd = null;
                cnn = null;

                return newid;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public IDictionary<int, Model_Soluzioni> GetData(string SQLString)
        {
            IDictionary<int, Model_Soluzioni> ret =new Dictionary<int, Model_Soluzioni>();

            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = ConnectionString;
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
                            Model_Soluzioni r = new Model_Soluzioni();

                            if (DBNull.Value.Equals(dr["IDSoluzione"])) { r.IDSoluzione = 0; } else { r.IDSoluzione = Convert.ToInt32(dr["IDSoluzione"]); }
                            if (DBNull.Value.Equals(dr["CodiceSoluzione"])) { r.CodiceSoluzione = ""; } else { r.CodiceSoluzione = Convert.ToString(dr["CodiceSoluzione"]); }
                            if (DBNull.Value.Equals(dr["Tipologia"])) { r.Tipologia = ""; } else { r.Tipologia = Convert.ToString(dr["Tipologia"]); }
                            if (DBNull.Value.Equals(dr["Nome"])) { r.Nome = ""; } else { r.Nome = Convert.ToString(dr["Nome"]); }
                            if (DBNull.Value.Equals(dr["NotePrescrittive"])) { r.NotePrescrittive = ""; } else { r.NotePrescrittive = Convert.ToString(dr["NotePrescrittive"]); }
                            if (DBNull.Value.Equals(dr["NoteDescrittive"])) { r.NoteDescrittive = ""; } else { r.NoteDescrittive = Convert.ToString(dr["NoteDescrittive"]); }
                            if (DBNull.Value.Equals(dr["IDUbicazione"])) { r.IDUbicazione = 0; } else { r.IDUbicazione = Convert.ToInt32(dr["IDUbicazione"]); }
                            if (DBNull.Value.Equals(dr["IDSchedaDocumenti"])) { r.IDSchedaDocumenti = 0; } else { r.IDSchedaDocumenti = Convert.ToInt32(dr["IDSchedaDocumenti"]); }
                            if (DBNull.Value.Equals(dr["IDSolvente"])) { r.IDSolvente = 0; } else { r.IDSolvente = Convert.ToInt32(dr["IDSolvente"]); }
                            if (DBNull.Value.Equals(dr["VolumeFinale"])) { r.VolumeFinale = 0; } else { r.VolumeFinale = Convert.ToDecimal(dr["VolumeFinale"]); }
                            if (DBNull.Value.Equals(dr["UMVolumeFinale"])) { r.UMVolumeFinale = ""; } else { r.UMVolumeFinale = Convert.ToString(dr["UMVolumeFinale"]); }
                            if (DBNull.Value.Equals(dr["DefaultGiorniScadenza"])) { r.DefaultGiorniScadenza = 0; } else { r.DefaultGiorniScadenza = Convert.ToInt32(dr["DefaultGiorniScadenza"]); }
                            if (DBNull.Value.Equals(dr["DataPreparazione"])) { r.DataPreparazione = Convert.ToDateTime("01/01/0001"); } else { r.DataPreparazione = Convert.ToDateTime(dr["DataPreparazione"]); }
                            if (DBNull.Value.Equals(dr["DataScadenza"])) { r.DataScadenza = Convert.ToDateTime("01/01/0001"); } else { r.DataScadenza = Convert.ToDateTime(dr["DataScadenza"]); }
                            if (DBNull.Value.Equals(dr["DataCreazione"])) { r.DataCreazione = Convert.ToDateTime("01/01/0001"); } else { r.DataCreazione = Convert.ToDateTime(dr["DataCreazione"]); }
                            if (DBNull.Value.Equals(dr["IDStato"])) { r.IDStato = 0; } else { r.IDStato = Convert.ToInt32(dr["IDStato"]); }
                            if (DBNull.Value.Equals(dr["IDUtente"])) { r.IDUtente = 0; } else { r.IDUtente = Convert.ToInt32(dr["IDUtente"]); }
                            if (DBNull.Value.Equals(dr["IDApparecchio"])) { r.IDApparecchio = 0; } else { r.IDApparecchio = Convert.ToInt32(dr["IDApparecchio"]); }
                            if (DBNull.Value.Equals(dr["IDUtensile"])) { r.IDUtensile = 0; } else { r.IDUtensile = Convert.ToInt32(dr["IDUtensile"]); }
                            if (DBNull.Value.Equals(dr["IDApparecchio2"])) { r.IDApparecchio2 = 0; } else { r.IDApparecchio2 = Convert.ToInt32(dr["IDApparecchio2"]); }
                            if (DBNull.Value.Equals(dr["IDUtensile2"])) { r.IDUtensile2 = 0; } else { r.IDUtensile2 = Convert.ToInt32(dr["IDUtensile2"]); }

                            ret.Add(r.IDSoluzione, r);
                            r = null;
                        }
                    }
                    else
                    {
                        Model_Soluzioni r = new Model_Soluzioni();

                        r.IDSoluzione = 0;
                        r.CodiceSoluzione = "";
                        r.Tipologia = "";
                        r.Nome = "";
                        r.NotePrescrittive = "";
                        r.NoteDescrittive = "";
                        r.IDUbicazione = 0;
                        r.IDSchedaDocumenti = 0;
                        r.IDSolvente = 0;
                        r.VolumeFinale = Convert.ToDecimal(null);
                        r.UMVolumeFinale = "";
                        r.Nome = "";
                        r.DefaultGiorniScadenza = Convert.ToInt32(null);
                        r.DataPreparazione = Convert.ToDateTime(null);
                        r.DataScadenza = Convert.ToDateTime(null);
                        r.IDStato = 0;
                        r.DataCreazione = Convert.ToDateTime(null);
                        r.IDUtente = 0;
                        r.IDApparecchio = 0;
                        r.IDUtensile = 0;
                        r.IDApparecchio2 = 0;
                        r.IDUtensile2 = 0;

                        ret.Add(r.IDSoluzione, r);
                        r = null;
                    }
                }
            }
            return ret;
        }
    }
}

