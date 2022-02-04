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

namespace EOS.Core.Control
{
    public class Control_Solventi
    {

        public string ConnectionString;
        public int IDUtente=0;

        public IDictionary<int, Model_Solventi> GetSolventeByID(int IDSolvente)
        {
            string SQLString = string.Format("SELECT * FROM Solventi WHERE IDSolvente={0}", IDSolvente);
            return GetData(SQLString);
        }

        public string GetCodiceSolventeFromIDSolvente(int IDSolvente)
        {
            string ret = "";

            string SQLString = "Select CodiceSolVente from Solventi WHERE IDSolvente='" + IDSolvente + "' ";

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
                            ret = dr["CodiceSolvente"].ToString();
                        }
                    }
                }
            }

            return ret;
        }

        public int AddSolvente(Model_Solventi Solvente)
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

                SqlCommand cmdstore = new SqlCommand();
                cmdstore.Connection = cnn;
                cmdstore.CommandType = CommandType.StoredProcedure;
                cmdstore.CommandText = "dbo.GetProgressivo";

                cmdstore.Parameters.Add("@TIPOPROGRESSIVO", SqlDbType.VarChar, 50).Value = "MIS";

                newcode = cmdstore.ExecuteScalar();

                cmdstore = null;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "INSERT INTO [dbo].[Solventi] ";
                SQLString = SQLString + "           ([CodiceSolvente] ";
                SQLString = SQLString + "           ,[Tipologia] ";
                SQLString = SQLString + "           ,[Nome] ";
                SQLString = SQLString + "           ,[NotePrescrittive] ";
                SQLString = SQLString + "           ,[NoteDescrittive] ";
                SQLString = SQLString + "           ,[IDUbicazione] ";
                SQLString = SQLString + "           ,[DefaultGiorniScadenza] ";
                SQLString = SQLString + "           ,[DataPreparazione] ";
                SQLString = SQLString + "           ,[DataScadenza] ";
                SQLString = SQLString + "           ,[DataCreazione] ";
                SQLString = SQLString + "           ,[IDUtente] ";
                SQLString = SQLString + "           ,[IDStato]) ";
                SQLString = SQLString + "     VALUES ";
                SQLString = SQLString + "           ('{0}' ";
                SQLString = SQLString + "           ,'{1}' ";
                SQLString = SQLString + "           ,'{2}' ";
                SQLString = SQLString + "           ,'{3}' ";
                SQLString = SQLString + "           ,'{4}' ";
                SQLString = SQLString + "           ,{5} ";
                SQLString = SQLString + "           ,{6} ";
                SQLString = SQLString + "           ,Case When '{7}'<>'01/01/0001 00:00:00' then '{7}' else null end ";
                SQLString = SQLString + "           ,Case When '{8}'<>'01/01/0001 00:00:00' then '{8}' else null end ";
                SQLString = SQLString + "           ,Case When '{9}'<>'01/01/0001 00:00:00' then '{9}' else null end ";
                SQLString = SQLString + "           ,{10} ";
                SQLString = SQLString + "           ,{11}); SELECT SCOPE_IDENTITY() ";

                if((Solvente.DataCreazione!=null) && (Solvente.DataCreazione != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataCreazione = Solvente.DataCreazione.ToString();
                }
                else
                {
                    DataCreazione = "01/01/0001 00:00:00";
                }

                if ((Solvente.DataPreparazione != null) && (Solvente.DataPreparazione != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataPreparazione = Solvente.DataPreparazione.ToString();
                }
                else
                {
                    DataPreparazione = "01/01/0001 00:00:00";
                }

                if ((Solvente.DataScadenza != null) && (Solvente.DataScadenza !=Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataScadenza = Solvente.DataScadenza.ToString();
                }
                else
                {
                    DataScadenza = "01/01/0001 00:00:00";
                }

                SQLString = string.Format(SQLString, newcode.ToString(), Solvente.Tipologia, Solvente.Nome.Replace("'", "''"), Solvente.NotePrescrittive.Replace("'", "''"), Solvente.NoteDescrittive.Replace("'", "''"), Solvente.IDUbicazione, Solvente.DefaultGiorniScadenza, DataPreparazione, DataScadenza, DataCreazione, Solvente.IDUtente, Solvente.IDStato);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                newid = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = null;
                cnn = null;

                AddLogSolVente("Inserimento", Solvente, DataPreparazione, DataScadenza, DataCreazione, newid);

                return newid;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int UpdateSolvente(Model_Solventi Solvente, int addLog = 1)
        {
            string DataPreparazione;
            string DataCreazione;
            string DataScadenza;

            try
            {
                Model_Solventi SolventeOriginale = this.GetSolventeByID(Solvente.IDSolvente).First().Value;

                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "UPDATE [dbo].[Solventi] ";
                SQLString = SQLString + "   SET [Tipologia] = '{0}' ";
                SQLString = SQLString + "      ,[Nome] = '{1}' ";
                SQLString = SQLString + "      ,[NotePrescrittive] = '{2}' ";
                SQLString = SQLString + "      ,[NoteDescrittive] = '{3}' ";
                SQLString = SQLString + "      ,[IDUbicazione] = {4} ";
                SQLString = SQLString + "      ,[DefaultGiorniScadenza] = {5} ";
                SQLString = SQLString + "      ,[DataPreparazione] = Case When '{6}'<>'01/01/0001 00:00:00' then '{6}' else null end ";
                SQLString = SQLString + "      ,[DataScadenza] = Case When '{7}'<>'01/01/0001 00:00:00' then '{7}' else null end ";
                SQLString = SQLString + "      ,[DataCreazione] = Case When '{8}'<>'01/01/0001 00:00:00' then '{8}' else null end ";
                SQLString = SQLString + "      ,[IDUtente] = {9} ";
                SQLString = SQLString + "      ,[IDStato] = {10} ";
                SQLString = SQLString + " WHERE  IDSolvente={11} ";

                if ((Solvente.DataCreazione != null) && (Solvente.DataCreazione != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataCreazione = Solvente.DataCreazione.ToString();
                }
                else
                {
                    DataCreazione = "01/01/0001 00:00:00";
                }

                if ((Solvente.DataPreparazione != null) && (Solvente.DataPreparazione != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataPreparazione = Solvente.DataPreparazione.ToString();
                }
                else
                {
                    DataPreparazione = "01/01/0001 00:00:00";
                }

                if ((Solvente.DataScadenza != null) && (Solvente.DataScadenza != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataScadenza = Solvente.DataScadenza.ToString();
                }
                else
                {
                    DataScadenza = "01/01/0001 00:00:00";
                }

                SQLString = string.Format(SQLString, Solvente.Tipologia, Solvente.Nome.Replace("'", "''"), Solvente.NotePrescrittive.Replace("'", "''"), Solvente.NoteDescrittive.Replace("'", "''"), Solvente.IDUbicazione, Solvente.DefaultGiorniScadenza, DataPreparazione, DataScadenza, DataCreazione, Solvente.IDUtente, Solvente.IDStato, Solvente.IDSolvente);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

                cmd = null;
                cnn = null;

                if(addLog==1)
                {
                    AddLogSolVente("Aggiornamento", Solvente, DataPreparazione, DataScadenza, DataCreazione, 0, SolventeOriginale);
                }

                SolventeOriginale = null;

                EOS.Core.Control.Control_Calcolo ControlCalcolo = new EOS.Core.Control.Control_Calcolo();
                ControlCalcolo.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                EOS.Core.Control.Control_Solventi ControlSolventi = new Core.Control.Control_Solventi();
                ControlSolventi.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                EOS.Core.Model.Model_Solventi ModelSolventi = new Core.Model.Model_Solventi();
                ModelSolventi = ControlSolventi.GetSolventeByID(Solvente.IDSolvente).First().Value;

                ControlCalcolo.SetDataScadenza(ModelSolventi.IDSolvente, ModelSolventi.DefaultGiorniScadenza, ModelSolventi.DataPreparazione, "Solvente");
                
                ModelSolventi = null;
                ControlSolventi = null;
                ControlCalcolo = null;

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int AddLogSolVente(string TipoOperazione, Model_Solventi Solvente, string DataPreparazione, string DataScadenza, string DataCreazione, int newid = 0, Model_Solventi SolventeOriginale=null)
        {
            try
            {
                Control_Transcode ctlTranscode = new Control_Transcode();
                Control_Log ctlLog = new Control_Log();

                if (newid != 0)
                {
                    Solvente.IDSolvente = newid;
                }

                string SQLStringLog = "";

                if(TipoOperazione=="Inserimento")
                {
                    SQLStringLog = SQLStringLog + "CodiceSoluzioneDiLavoro = " + ctlTranscode.GetCodiceSolventeByID(Solvente.IDSolvente) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "Tipologia = " + Solvente.Tipologia + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "Nome = " + Solvente.Nome.Replace("'", "''") + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "NotePrescrittive = " + Solvente.NotePrescrittive.Replace("'", "''") + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "NoteDescrittive= " + Solvente.NoteDescrittive.Replace("'", "''") + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "DefaultGiorniScadenza = " + Solvente.DefaultGiorniScadenza + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "DataScadenza = " + DataScadenza + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "DataPreparazione = " + DataPreparazione + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "Ubicazione = " + ctlTranscode.GetUbicazioneByID(Solvente.IDUbicazione) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "Stato = " + ctlTranscode.GetStatoByID(Solvente.IDStato) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "DataCreazione=" + DataCreazione + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "NomeUtente = " + ctlTranscode.GetNomeUtenteByID(Solvente.IDUtente) + System.Environment.NewLine;
                }
                else
                {
                    SQLStringLog = SQLStringLog + "CodiceSoluzioneDiLavoro = " + ctlTranscode.GetCodiceSolventeByID(Solvente.IDSolvente) + System.Environment.NewLine;

                    if (Solvente.Tipologia != SolventeOriginale.Tipologia)
                    {
                        SQLStringLog = SQLStringLog + "Tipologia Precedente = " + SolventeOriginale.Tipologia + " - Attuale = " + Solvente.Tipologia + System.Environment.NewLine;
                    }

                    if (Solvente.Nome != SolventeOriginale.Nome)
                    {
                        SQLStringLog = SQLStringLog + "Nome Precedente = " + SolventeOriginale.Nome.Replace("'", "''") + " - Attuale = " + Solvente.Nome.Replace("'", "''") + System.Environment.NewLine;
                    }

                    if (Solvente.NotePrescrittive != SolventeOriginale.NotePrescrittive)
                    {
                        SQLStringLog = SQLStringLog + "NotePrescrittive Precedente = " + SolventeOriginale.NotePrescrittive.Replace("'", "''") + " - Attuale = " + Solvente.NotePrescrittive.Replace("'", "''") + System.Environment.NewLine;
                    }

                    if (Solvente.NoteDescrittive != SolventeOriginale.NoteDescrittive)
                    {
                        SQLStringLog = SQLStringLog + "NoteDescrittive Precedente = " + SolventeOriginale.NoteDescrittive.Replace("'", "''") + " - Attuale = " + Solvente.NoteDescrittive.Replace("'", "''") + System.Environment.NewLine;
                    }

                    if (Solvente.DefaultGiorniScadenza != SolventeOriginale.DefaultGiorniScadenza)
                    {
                        SQLStringLog = SQLStringLog + "DefaultGiorniScadenza Precedente = " + SolventeOriginale.DefaultGiorniScadenza + " - Attuale = " + Solvente.DefaultGiorniScadenza + System.Environment.NewLine;
                    }

                    if (Solvente.DataScadenza != SolventeOriginale.DataScadenza)
                    {
                        SQLStringLog = SQLStringLog + "DataScadenza Precedente = " + SolventeOriginale.DataScadenza + " - Attuale = " + DataScadenza + System.Environment.NewLine;
                    }

                    if (Solvente.DataPreparazione != SolventeOriginale.DataPreparazione)
                    {
                        SQLStringLog = SQLStringLog + "DataPreparazione Precedente = " + SolventeOriginale.DataPreparazione + " - Attuale = " + DataPreparazione + System.Environment.NewLine;
                    }

                    if (Solvente.IDUbicazione != SolventeOriginale.IDUbicazione)
                    {
                        SQLStringLog = SQLStringLog + "Ubicazione Precedente = " + ctlTranscode.GetUbicazioneByID(SolventeOriginale.IDUbicazione) + " - Attuale = " + ctlTranscode.GetUbicazioneByID(Solvente.IDUbicazione) + System.Environment.NewLine;
                    }

                    if (Solvente.IDStato != SolventeOriginale.IDStato)
                    {
                        SQLStringLog = SQLStringLog + "Stato Precedente = " + ctlTranscode.GetStatoByID(SolventeOriginale.IDStato) + " - Attuale = " + ctlTranscode.GetStatoByID(Solvente.IDStato) + System.Environment.NewLine;
                    }
                }

                ctlLog.ConnectionString = ConnectionString;

                ctlLog.InsertLog(TipoOperazione, "Soluzioni di Lavoro", Solvente.IDSolvente, ctlTranscode.GetCodiceSolventeByID(Solvente.IDSolvente), SQLStringLog, IDUtente);

                ctlTranscode = null;
                ctlLog = null;

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int DeleteSolvente(Model_Solventi Solvente)
        {
            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "DELETE FROM [dbo].[Solventi] WHERE IDSolvente={0} ";
                SQLString = string.Format(SQLString, Solvente.IDSolvente);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

                SQLString = "";
                SQLString = SQLString + "DELETE FROM [dbo].[Solventi_Details] WHERE IDSolventeMaster={0} ";
                SQLString = string.Format(SQLString, Solvente.IDSolvente);

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

        //public int CloneSolution(int IDSolvente)
        //{
        //    int newid;
        //    object newcode = "";

        //    try
        //    {
        //        System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
        //        cnn.ConnectionString = ConnectionString;
        //        cnn.Open();

        //        SqlCommand cmdstore = new SqlCommand();
        //        cmdstore.Connection = cnn;
        //        cmdstore.CommandType = CommandType.StoredProcedure;
        //        cmdstore.CommandText = "dbo.GetProgressivo";

        //        cmdstore.Parameters.Add("@TIPOPROGRESSIVO", SqlDbType.VarChar, 50).Value = "MIS";

        //        newcode = cmdstore.ExecuteScalar();

        //        cmdstore = null;

        //        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

        //        string SQLString = "";
        //        SQLString = SQLString + "INSERT INTO [Solventi] ";
        //        SQLString = SQLString + "SELECT ";
        //        SQLString = SQLString + "[Nome] + ' - Copia' As Nome ";
        //        SQLString = SQLString + ",[NotePrescrittive] ";
        //        SQLString = SQLString + ",[NoteDescrittive] ";
        //        SQLString = SQLString + ",[IDUbicazione] ";
        //        SQLString = SQLString + ",[DefaultGiorniScadenza] ";
        //        SQLString = SQLString + ",getdate() as DataPreparazione ";
        //        SQLString = SQLString + ",NULL as DataScadenza ";
        //        SQLString = SQLString + ",getdate() as DataCreazione ";
        //        SQLString = SQLString + ",[IDUtente] ";
        //        SQLString = SQLString + ",5 as IDStato ";
        //        SQLString = SQLString + ",Tipologia ";
        //        SQLString = SQLString + ",'" + newcode.ToString() + "' As CodiceSolvente ";
        //        SQLString = SQLString + "FROM[EOS].[dbo].[Solventi] ";
        //        SQLString = SQLString + "WHERE IDSolvente = {0}; SELECT SCOPE_IDENTITY() ";
        //        SQLString = string.Format(SQLString, IDSolvente);

        //        cmd.CommandText = SQLString;
        //        cmd.Connection = cnn;
        //        newid = Convert.ToInt32(cmd.ExecuteScalar());

        //        SQLString = "";
        //        SQLString = SQLString + "INSERT INTO Solventi_Details ";
        //        SQLString = SQLString + "SELECT ";
        //        SQLString = SQLString + "{0} as IDSolventeMaster ";
        //        SQLString = SQLString + ",[IDSchedaDocumenti] ";
        //        SQLString = SQLString + ",[IDSolvente] ";
        //        SQLString = SQLString + ",[UM_Prelievo] ";
        //        SQLString = SQLString + ",[Quantita_Prelievo] ";
        //        SQLString = SQLString + ",[IDApparecchio] ";
        //        SQLString = SQLString + ",[IDUtensile] ";
        //        SQLString = SQLString + ",[Tipologia_MR] ";
        //        SQLString = SQLString + ",[CAS] ";
        //        SQLString = SQLString + ",[Note] ";
        //        SQLString = SQLString + ",[DataScadenza] ";
        //        SQLString = SQLString + "FROM [EOS].[dbo].[Solventi_Details] ";
        //        SQLString = SQLString + "WHERE IDSolventeMaster={1} ";
        //        SQLString = string.Format(SQLString, newid, IDSolvente);

        //        cmd.CommandText = SQLString;
        //        cmd.Connection = cnn;
        //        cmd.ExecuteNonQuery();

        //        cmd = null;
        //        cnn = null;

        //        return newid;
        //    }
        //    catch (Exception e)
        //    {
        //        return 0;
        //    }
        //}

        public IDictionary<int, Model_Solventi> GetData(string SQLString)
        {
            IDictionary<int, Model_Solventi> ret =new Dictionary<int, Model_Solventi>();

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
                            Model_Solventi r = new Model_Solventi();

                            if (DBNull.Value.Equals(dr["IDSolvente"])) { r.IDSolvente = 0; } else { r.IDSolvente = Convert.ToInt32(dr["IDSolvente"]); }
                            if (DBNull.Value.Equals(dr["CodiceSolvente"])) { r.CodiceSolvente = ""; } else { r.CodiceSolvente = Convert.ToString(dr["CodiceSolvente"]); }
                            if (DBNull.Value.Equals(dr["Tipologia"])) { r.Tipologia = ""; } else { r.Tipologia = Convert.ToString(dr["Tipologia"]); }
                            if (DBNull.Value.Equals(dr["Nome"])) { r.Nome = ""; } else { r.Nome = Convert.ToString(dr["Nome"]); }
                            if (DBNull.Value.Equals(dr["NotePrescrittive"])) { r.NotePrescrittive = ""; } else { r.NotePrescrittive = Convert.ToString(dr["NotePrescrittive"]); }
                            if (DBNull.Value.Equals(dr["NoteDescrittive"])) { r.NoteDescrittive = ""; } else { r.NoteDescrittive = Convert.ToString(dr["NoteDescrittive"]); }
                            if (DBNull.Value.Equals(dr["IDUbicazione"])) { r.IDUbicazione = 0; } else { r.IDUbicazione = Convert.ToInt32(dr["IDUbicazione"]); }
                            if (DBNull.Value.Equals(dr["DefaultGiorniScadenza"])) { r.DefaultGiorniScadenza = 0; } else { r.DefaultGiorniScadenza = Convert.ToInt32(dr["DefaultGiorniScadenza"]); }
                            if (DBNull.Value.Equals(dr["DataPreparazione"])) { r.DataPreparazione = Convert.ToDateTime("01/01/0001"); } else { r.DataPreparazione = Convert.ToDateTime(dr["DataPreparazione"]); }
                            if (DBNull.Value.Equals(dr["DataScadenza"])) { r.DataScadenza = Convert.ToDateTime("01/01/0001"); } else { r.DataScadenza = Convert.ToDateTime(dr["DataScadenza"]); }
                            if (DBNull.Value.Equals(dr["DataCreazione"])) { r.DataCreazione = Convert.ToDateTime("01/01/0001"); } else { r.DataCreazione = Convert.ToDateTime(dr["DataCreazione"]); }
                            if (DBNull.Value.Equals(dr["IDStato"])) { r.IDStato = 0; } else { r.IDStato = Convert.ToInt32(dr["IDStato"]); }
                            if (DBNull.Value.Equals(dr["IDUtente"])) { r.IDUtente = 0; } else { r.IDUtente = Convert.ToInt32(dr["IDUtente"]); }

                            ret.Add(r.IDSolvente, r);
                            r = null;
                        }
                    }
                    else
                    {
                        Model_Solventi r = new Model_Solventi();

                        r.IDSolvente = 0;
                        r.CodiceSolvente = "";
                        r.Tipologia = "";
                        r.Nome = "";
                        r.NotePrescrittive = "";
                        r.NoteDescrittive = "";
                        r.IDUbicazione = 0;
                        r.Nome = "";
                        r.DefaultGiorniScadenza = Convert.ToInt32(null);
                        r.DataPreparazione = Convert.ToDateTime(null);
                        r.DataScadenza = Convert.ToDateTime(null);
                        r.IDStato = 0;
                        r.DataCreazione = Convert.ToDateTime(null);
                        r.IDUtente = 0;

                        ret.Add(r.IDSolvente, r);
                        r = null;
                    }
                }
            }
            return ret;
        }
    }
}

