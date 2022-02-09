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
    class Control_Rette
    {
        public string ConnectionString;
        public int IDUtente = 0;

        public IDictionary<int, Model_Rette> GetData(string SQLString)
        {
            IDictionary<int, Model_Rette> ret = new Dictionary<int, Model_Rette>();

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
                            Model_Rette r = new Model_Rette();

                            //[IDRetta]
                            //,[CodiceRetta]
                            //,[Nome]
                            //,[DefaultGiorniScadenza]
                            //,[DataScadenza]
                            //,[IDStato]
                            //,[Tipologia]
                            //,[IDSchedaDocumenti]
                            //,[IDSolvente]
                            //,[Note]
                            //,[DataCreazione]
                            //,[IDUtente]

                            if (DBNull.Value.Equals(dr["IDRetta"])) { r.IDRetta = 0; } else { r.IDRetta = Convert.ToInt32(dr["IDRetta"]); }
                            if (DBNull.Value.Equals(dr["CodiceRetta"])) { r.CodiceRetta = ""; } else { r.CodiceRetta = Convert.ToString(dr["CodiceRetta"]); }
                            if (DBNull.Value.Equals(dr["Nome"])) { r.Nome = ""; } else { r.Nome = Convert.ToString(dr["Nome"]); }
                            if (DBNull.Value.Equals(dr["DefaultGiorniScadenza"])) { r.DefaultGiorniScadenza = 0; } else { r.DefaultGiorniScadenza = Convert.ToInt32(dr["DefaultGiorniScadenza"]); }
                            if (DBNull.Value.Equals(dr["DataScadenza"])) { r.DataScadenza = Convert.ToDateTime("01/01/0001"); } else { r.DataScadenza = Convert.ToDateTime(dr["DataScadenza"]); }
                            if (DBNull.Value.Equals(dr["IDStato"])) { r.IDStato = 0; } else { r.IDStato = Convert.ToInt32(dr["IDStato"]); }
                            if (DBNull.Value.Equals(dr["Tipologia"])) { r.Tipologia = ""; } else { r.Tipologia = Convert.ToString(dr["Tipologia"]); }
                            if (DBNull.Value.Equals(dr["IDSchedaDocumenti"])) { r.IDSchedaDocumenti = 0; } else { r.IDSchedaDocumenti = Convert.ToInt32(dr["IDSchedaDocumenti"]); }
                            if (DBNull.Value.Equals(dr["IDSolvente"])) { r.IDSolvente = 0; } else { r.IDSolvente = Convert.ToInt32(dr["IDSolvente"]); }
                            if (DBNull.Value.Equals(dr["Note"])) { r.Note = ""; } else { r.Note = Convert.ToString(dr["Note"]); }
                            if (DBNull.Value.Equals(dr["DataCreazione"])) { r.DataCreazione = Convert.ToDateTime("01/01/0001"); } else { r.DataCreazione = Convert.ToDateTime(dr["DataCreazione"]); }
                            if (DBNull.Value.Equals(dr["IDUtente"])) { r.IDUtente = 0; } else { r.IDUtente = Convert.ToInt32(dr["IDUtente"]); }

                             ret.Add(r.IDRetta, r);
                            r = null;
                        }
                    }
                    else
                    {
                        Model_Rette r = new Model_Rette();

                        r.IDRetta = 0;
                        r.CodiceRetta = "";
                        r.Nome = "";
                        r.DefaultGiorniScadenza = 0;
                        r.DataScadenza = Convert.ToDateTime("01/01/0001");
                        r.IDStato = 0;
                        r.Tipologia = "";
                        r.IDSchedaDocumenti = 0;
                        r.IDSolvente = 0;
                        r.Note = "";
                        r.DataCreazione = Convert.ToDateTime("01/01/0001");
                        r.IDUtente = 0;

                        ret.Add(r.IDSolvente, r);
                        r = null;
                    }
                }
            }
            return ret;
        }

        public IDictionary<int, Model_Rette> GetRettaByID(int IDRetta)
        {
            string SQLString = string.Format("SELECT * FROM Rette WHERE IDRetta={0}", IDRetta);
            return GetData(SQLString);
        }

        public string GetCodiceRettaFromIDRetta(int IDRetta)
        {
            string ret = "";

            string SQLString = "Select CodiceRetta from Rette WHERE IDRetta='" + IDRetta + "' ";

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
                            ret = dr["CodiceRetta"].ToString();
                        }
                    }
                }
            }

            return ret;
        }

        public int AddSolution(Model_Rette Retta)
        {
            int newid = 0;
            object newcode = "";
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

                cmdstore.Parameters.Add("@TIPOPROGRESSIVO", SqlDbType.VarChar, 50).Value = "RET";

                newcode = cmdstore.ExecuteScalar();

                cmdstore = null;

                string SQLString = "";
                SQLString = SQLString + "INSERT INTO [dbo].[Rette] ";
                SQLString = SQLString + "           ([CodiceRetta] ";
                SQLString = SQLString + "           ,[Nome] ";
                SQLString = SQLString + "           ,[DefaultGiorniScadenza] ";
                SQLString = SQLString + "           ,[DataScadenza] ";
                SQLString = SQLString + "           ,[IDStato] ";
                SQLString = SQLString + "           ,[Tipologia] ";
                SQLString = SQLString + "           ,[IDSchedaDocumenti] ";
                SQLString = SQLString + "           ,[IDSolvente] ";
                SQLString = SQLString + "           ,[Note] ";
                SQLString = SQLString + "           ,[DataCreazione] ";
                SQLString = SQLString + "           ,[IDUtente]) ";
                SQLString = SQLString + "     VALUES ";
                SQLString = SQLString + "           ('{0}' ";
                SQLString = SQLString + "           ,'{1}' ";
                SQLString = SQLString + "           ,'{2}' ";
                SQLString = SQLString + "           ,Case When '{3}'<>'01/01/0001 00:00:00' then '{3}' else null end ";
                SQLString = SQLString + "           ,'{4}' ";
                SQLString = SQLString + "           ,'{5}' ";
                SQLString = SQLString + "           ,{6} ";
                SQLString = SQLString + "           ,{7} ";
                SQLString = SQLString + "           ,{8} ";
                SQLString = SQLString + "           ,Case When '{9}'<>'01/01/0001 00:00:00' then '{9}' else null end ";
                SQLString = SQLString + "           ,{10}); SELECT SCOPE_IDENTITY() ";

                if ((Retta.DataCreazione != null) && (Retta.DataCreazione != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataCreazione = Retta.DataCreazione.ToString();
                }
                else
                {
                    DataCreazione = "01/01/0001 00:00:00";
                }

                if ((Retta.DataScadenza != null) && (Retta.DataScadenza != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataScadenza = Retta.DataScadenza.ToString();
                }
                else
                {
                    DataScadenza = "01/01/0001 00:00:00";
                }

                SQLString = string.Format(SQLString, newcode.ToString(), Retta.CodiceRetta, Retta.Nome.Replace("'", "''"), Retta.DefaultGiorniScadenza, DataScadenza, Retta.IDStato, Retta.Tipologia, Retta.IDSchedaDocumenti, Retta.IDSolvente, Retta.Note.Replace("'", "''"), DataCreazione,Retta.IDUtente);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                newid = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = null;
                cnn = null;

                AddLogRetta("Inserimento", Retta, DataScadenza, DataCreazione, newid);

                return newid;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int UpdateRette(Model_Rette Retta, int addLog = 1)
        {
            string DataCreazione;
            string DataScadenza;

            try
            {
                Model_Rette RettaOriginale = this.GetRettaByID(Retta.IDRetta).First().Value;

                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "UPDATE [dbo].[Soluzioni] ";
                SQLString = SQLString + "   SET [Nome] = '{0}' ";
                SQLString = SQLString + "      ,[DefaultGiorniScadenza] = {1} ";
                SQLString = SQLString + "      ,[DataScadenza] = Case When '{2}'<>'01/01/0001 00:00:00' then '{2}' else null end ";
                SQLString = SQLString + "      ,[IDStato] = {3} ";
                SQLString = SQLString + "      ,[Tipologia] = '{4}' ";
                SQLString = SQLString + "      ,[IDSchedaDocumenti] = {5} ";
                SQLString = SQLString + "      ,[IDSolvente] = {6} ";
                SQLString = SQLString + "      ,[Note] = '{7}' ";
                SQLString = SQLString + "      ,[DataCreazione] = Case When '{8}'<>'01/01/0001 00:00:00' then '{8}' else null end ";
                SQLString = SQLString + "      ,[IDUtente] = {9} ";
                SQLString = SQLString + " WHERE  IDRetta={10} ";

                if ((Retta.DataCreazione != null) && (Retta.DataCreazione != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataCreazione = Retta.DataCreazione.ToString();
                }
                else
                {
                    DataCreazione = "01/01/0001 00:00:00";
                }

                if ((Retta.DataScadenza != null) && (Retta.DataScadenza != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataScadenza = Retta.DataScadenza.ToString();
                }
                else
                {
                    DataScadenza = "01/01/0001 00:00:00";
                }

                SQLString = string.Format(SQLString, Retta.Nome.Replace("'", "''"), Retta.DefaultGiorniScadenza, DataScadenza, Retta.IDStato, Retta.Tipologia,Retta.IDSchedaDocumenti, Retta.IDSolvente, Retta.Note.Replace("'", "''"), Retta.DataCreazione, Retta.IDUtente, Retta.IDRetta);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //Ho deciso di far seguire una modifica dello stato della retta ad un conseguente adeguamento dello stato delle soluzioni relative ai punti della retta che si trovano nello
                //stato della retta prima dell'update. Quindi tutte le soluzioni il cui stato non è cambiato per un operazione specifica su quella soluzione seguiranno sempre lo stato della
                //retta. Tutte le altre manterranno il loro stato imputato manualmente proprio perchè l'operatore lo avrà modificat solo su quella soluzione per un motivo valido (esempio
                //una soluzione annullata)
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (RettaOriginale.IDStato != Retta.IDStato)
                {
                    SQLString = "";
                    SQLString = SQLString + "Update SOL set SOL.IDStato={0} from Rette_Soluzioni RETSOL inner join Soluzioni SOL ON RETSOL.IDSoluzione=SOL.IDSoluzione WHERE RETSOL.IDRetta={1} and SOL.IDStato={2} ";
                    SQLString = string.Format(SQLString, Retta.IDStato, Retta.IDRetta, RettaOriginale.IDStato);

                    cmd.CommandText = SQLString;
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //Ho deciso di far seguire una modifica dello stato della retta ad un conseguente adeguamento dello stato delle soluzioni relative ai punti della retta che si trovano nello
                //stato della retta prima dell'update. Quindi tutte le soluzioni il cui stato non è cambiato per un operazione specifica su quella soluzione seguiranno sempre lo stato della
                //retta. Tutte le altre manterranno il loro stato imputato manualmente proprio perchè l'operatore lo avrà modificat solo su quella soluzione per un motivo valido (esempio
                //una soluzione annullata)
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                cmd = null;
                cnn = null;

                if (addLog == 1)
                {
                    AddLogRetta("Aggiornamento", Retta, DataScadenza, DataCreazione, 0, RettaOriginale);
                }

                RettaOriginale = null;

                EOS.Core.Control.Control_Calcolo ControlCalcolo = new EOS.Core.Control.Control_Calcolo();
                ControlCalcolo.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                ControlCalcolo.IDUtente = IDUtente;

                //adattare count component
                if (ControlCalcolo.CountComponent(Retta.IDRetta, "Retta") > 0)
                {
                    Control_Rette ControlRetta = new Control_Rette();
                    ControlRetta.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                    Model_Rette ModelRetta = new Model_Rette();
                    ModelRetta = ControlRetta.GetRettaByID(Retta.IDRetta).First().Value;

                    //adattare setdatascadenza con il discorso gestione della retta
                    ControlCalcolo.SetDataScadenza(ModelRetta.IDRetta, ModelRetta.DefaultGiorniScadenza, DateTime.Today, "Retta");

                    ModelRetta = null;
                    ControlRetta = null;
                }

                ControlCalcolo = null;

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int AddLogRetta(string TipoOperazione, Model_Rette Retta, string DataScadenza, string DataCreazione, int newid = 0, Model_Rette RettaOriginale = null)
        {
            try
            {
                Control_Transcode ctlTranscode = new Control_Transcode();
                Control_Log ctlLog = new Control_Log();

                if (newid != 0)
                {
                    Retta.IDRetta = newid;
                }

                string SQLStringLog = "";

                if (TipoOperazione == "Inserimento")
                {
                    SQLStringLog = SQLStringLog + "CodiceRetta = " + ctlTranscode.GetCodiceRettaByID(Retta.IDRetta) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "Nome = " + Retta.Nome.Replace("'", "''") + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "DefaultGiorniScadenza = " + Retta.DefaultGiorniScadenza + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "DataScadenza = " + DataScadenza + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "Stato = " + ctlTranscode.GetStatoByID(Retta.IDStato) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "Tipologia = " + Retta.Tipologia + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "CodiceSoluzioneDiLavoro (Solvente) = " + ctlTranscode.GetCodiceSolventeByID(Retta.IDSolvente) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "Identificativo - LottoMateriale (Solvente) = " + ctlTranscode.GetIdentificativoELottoByID(Retta.IDSchedaDocumenti) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "Note = " + Retta.Note.Replace("'", "''") + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "DataCreazione=" + DataCreazione + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "NomeUtente = " + ctlTranscode.GetNomeUtenteByID(Retta.IDUtente) + System.Environment.NewLine;
                }
                else
                {
                    SQLStringLog = SQLStringLog + "CodiceRetta = " + ctlTranscode.GetCodiceSoluzioneByID(Retta.IDRetta) + System.Environment.NewLine;

                    if (Retta.Nome != RettaOriginale.Nome)
                    {
                        SQLStringLog = SQLStringLog + "Nome Precedente = " + RettaOriginale.Nome.Replace("'", "''") + " - Attuale = " + Retta.Nome.Replace("'", "''") + System.Environment.NewLine;
                    }

                    if (Retta.DefaultGiorniScadenza != RettaOriginale.DefaultGiorniScadenza)
                    {
                        SQLStringLog = SQLStringLog + "DefaultGiorniScadenza  Precedente = " + RettaOriginale.DefaultGiorniScadenza + " - Attuale = " + Retta.DefaultGiorniScadenza + System.Environment.NewLine;
                    }

                    if (Retta.DataScadenza != RettaOriginale.DataScadenza)
                    {
                        SQLStringLog = SQLStringLog + "DataScadenza  Precedente = " + RettaOriginale.DataScadenza + " - Attuale = " + DataScadenza + System.Environment.NewLine;
                    }

                    if (Retta.IDStato != RettaOriginale.IDStato)
                    {
                        SQLStringLog = SQLStringLog + "Stato  Precedente = " + ctlTranscode.GetStatoByID(RettaOriginale.IDStato) + " - Attuale = " + ctlTranscode.GetStatoByID(Retta.IDStato) + System.Environment.NewLine;
                    }

                    if (Retta.Tipologia != RettaOriginale.Tipologia)
                    {
                        SQLStringLog = SQLStringLog + "Tipologia Precedente = " + RettaOriginale.Tipologia + " - Attuale = " + Retta.Tipologia + System.Environment.NewLine;
                    }

                    if (Retta.IDSolvente != RettaOriginale.IDSolvente)
                    {
                        SQLStringLog = SQLStringLog + "CodiceSoluzioneDiLavoro Precedente (Solvente) = " + ctlTranscode.GetCodiceSolventeByID(RettaOriginale.IDSolvente) + " - Attuale = " + ctlTranscode.GetCodiceSolventeByID(Retta.IDSolvente) + System.Environment.NewLine;
                    }

                    if (Retta.IDSchedaDocumenti != RettaOriginale.IDSchedaDocumenti)
                    {
                        SQLStringLog = SQLStringLog + "Identificativo - LottoMateriale  Precedente (Solvente) = " + ctlTranscode.GetIdentificativoELottoByID(RettaOriginale.IDSchedaDocumenti) + " - Attuale (Solvente) = " + ctlTranscode.GetIdentificativoELottoByID(Retta.IDSchedaDocumenti) + System.Environment.NewLine;
                    }

                    if (Retta.Note != RettaOriginale.Note)
                    {
                        SQLStringLog = SQLStringLog + "Note Precedenti = " + RettaOriginale.Note.Replace("'", "''") + " - Attuali = " + Retta.Note.Replace("'", "''") + System.Environment.NewLine;
                    }
                }

                ctlLog.ConnectionString = ConnectionString;

                ctlLog.InsertLog(TipoOperazione, "Rette", Retta.IDRetta, ctlTranscode.GetCodiceSoluzioneByID(Retta.IDRetta), SQLStringLog, IDUtente);

                ctlTranscode = null;
                ctlLog = null;

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int DeleteRetta(Model_Rette Retta)
        {
            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "DELETE FROM [dbo].[Rette] WHERE IDRetta={0} ";
                SQLString = string.Format(SQLString, Retta.IDRetta);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //Anche se questa funzione non dovrebbe essere utilizzata perchè non dovremmo permettere la cancellazione se utilizzata per cancellare una Retta in automatico provvederà 
                //a porre in stato annullata tutte le soluzioni relative ai vari punti della retta cancellata
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                SQLString = "";
                SQLString = SQLString + "Update SOL set SOL.IDStato=8 from Rette_Soluzioni RETSOL inner join Soluzioni SOL ON RETSOL.IDSoluzione=SOL.IDSoluzione WHERE RETSOL.IDRetta={0} ";
                SQLString = string.Format(SQLString, Retta.IDRetta);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //Anche se questa funzione non dovrebbe essere utilizzata perchè non dovremmo permettere la cancellazione se utilizzata per cancellare una Retta in automatico provvederà 
                //a porre in stato annullata tutte le soluzioni relative ai vari punti della retta cancellata
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                SQLString = "";
                SQLString = SQLString + "DELETE FROM [dbo].[Rette_Soluzioni] WHERE IDRetta={0} ";
                SQLString = string.Format(SQLString, Retta.IDRetta);

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
    }
}
