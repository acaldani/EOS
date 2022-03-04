using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EOS.Core.Control
{
    public class Control_RetteSoluzioni
    {
        public string ConnectionString;
        public int IDUtente = 0;

        public IDictionary<int, EOS.Core.Model.Model_Rette_Soluzioni> GetByIDRettaSoluzione(int IDRettaSoluzione)
        {
            string SQLString = string.Format("SELECT * FROM [dbo].[Rette_Soluzioni] WHERE IDRettaSoluzione={0}", IDRettaSoluzione);
            return GetData(SQLString);
        }

        public IDictionary<int, EOS.Core.Model.Model_Rette_Soluzioni> GetByIDRetta(int IDRetta)
        {
            string SQLString = string.Format("SELECT * FROM [dbo].[Rette_Soluzioni] WHERE IDRetta={0}", IDRetta);
            return GetData(SQLString);
        }

        public IDictionary<int, EOS.Core.Model.Model_Rette_Soluzioni> GetData(string SQLString)
        {
            IDictionary<int, EOS.Core.Model.Model_Rette_Soluzioni> ret = new Dictionary<int, EOS.Core.Model.Model_Rette_Soluzioni>();

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
                            EOS.Core.Model.Model_Rette_Soluzioni r = new EOS.Core.Model.Model_Rette_Soluzioni();

                            if (DBNull.Value.Equals(dr["IDRettaSoluzione"])) { r.IDRettaSoluzione = 0; } else { r.IDRettaSoluzione = Convert.ToInt32(dr["IDRettaSoluzione"]); }
                            if (DBNull.Value.Equals(dr["IDRetta"])) { r.IDRetta = 0; } else { r.IDRetta = Convert.ToInt32(dr["IDRetta"]); }
                            if (DBNull.Value.Equals(dr["IDSoluzione"])) { r.IDSoluzione = 0; } else { r.IDSoluzione = Convert.ToInt32(dr["IDSoluzione"]); }
                            if (DBNull.Value.Equals(dr["DataCorrelazione"])) { r.DataCorrelazione = Convert.ToDateTime("01/01/0001"); } else { r.DataCorrelazione = Convert.ToDateTime(dr["DataCorrelazione"]); }

                            ret.Add(r.IDRettaSoluzione, r);
                            r = null;
                        }
                    }
                    else
                    {
                        EOS.Core.Model.Model_Rette_Soluzioni r = new EOS.Core.Model.Model_Rette_Soluzioni();

                        r.IDRettaSoluzione = 0;
                        r.IDRetta = 0;
                        r.IDSoluzione = 0;
                        r.DataCorrelazione = Convert.ToDateTime(null);

                        ret.Add(r.IDRettaSoluzione, r);
                        r = null;
                    }
                }
            }
            return ret;
        }

        public int countModelSolution(int IDRetta)
        {
            int ret = 0;
            string SQLString = "";

            SQLString = "select count(*) as conta from Rette_Soluzioni RETSOL INNER JOIN Soluzioni SOL ON RETSOL.IDSoluzione=SOL.IDSoluzione AND SOL.Tipologia='Soluzione MR Modello' Where RETSOL.IDRetta='" + IDRetta + "' ";
           
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
                            ret = Convert.ToInt32(dr["conta"]);
                        }
                    }
                }
            }

            return ret;
        }

        public int CambiaStatoScollegaSoluzioni(int IDRetta, int IDStato, bool Annulla, bool Scollega, bool Modello)
        {
            //IDSTATO=8 = Annullato
            int ret=0;

            EOS.Core.Model.Model_Rette_Soluzioni ModelRetteSoluzioni = new EOS.Core.Model.Model_Rette_Soluzioni();
            EOS.Core.Control.Control_RetteSoluzioni ControlRetteSoluzioni = new EOS.Core.Control.Control_RetteSoluzioni();
            ControlRetteSoluzioni.ConnectionString = ConnectionString;

            //System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
            //cnn.ConnectionString = ConnectionString;
            //cnn.Open();

            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            //string SQLString = "";

            if (Annulla)
            {
                //if (Modello)
                //{
                //    SQLString = SQLString + "Update SOL Set SOL.IDStato=" + IDStato + " FROM Soluzioni SOL Inner Join Rette_Soluzioni RETSOL ON SOL.IDSoluzione=RETSOL.IDSoluzione WHERE RETSOL.IDRetta='" + IDRetta + "' and SOL.IDStato=6 and SOL.Tipologia='Soluzione MR Modello' ";
                //}
                //else
                //{
                //    SQLString = SQLString + "Update SOL Set SOL.IDStato=" + IDStato + " FROM Soluzioni SOL Inner Join Rette_Soluzioni RETSOL ON SOL.IDSoluzione=RETSOL.IDSoluzione WHERE RETSOL.IDRetta='" + IDRetta + "' and SOL.IDStato=6 ";
                //}

                //cmd.CommandText = SQLString;
                //cmd.Connection = cnn;
                //ret = cmd.ExecuteNonQuery();

                EOS.Core.Model.Model_Soluzioni ModelSoluzioni = new EOS.Core.Model.Model_Soluzioni();
                EOS.Core.Control.Controller_Soluzioni ControllerSoluzioni = new EOS.Core.Control.Controller_Soluzioni();
                ControllerSoluzioni.ConnectionString = ConnectionString;

                foreach (KeyValuePair<int, EOS.Core.Model.Model_Rette_Soluzioni> element in ControlRetteSoluzioni.GetByIDRetta(IDRetta))
                {
                    ModelRetteSoluzioni = element.Value;
                    ModelSoluzioni = ControllerSoluzioni.GetSolutionByID(ModelRetteSoluzioni.IDSoluzione).First().Value;

                    //if(ModelSoluzioni.)
                    //{

                    //}

                    ModelSoluzioni.IDStato = IDStato;
                    ControllerSoluzioni.UpdateSolution(ModelSoluzioni);
                }

                ModelSoluzioni = null;
                ControllerSoluzioni = null;
            }

            if(Scollega)
            {
                //if (Modello)
                //{
                //    SQLString = SQLString + "Delete from Rette_Soluzioni where IDRettaSoluzione in (Select RETSOL.IDRettaSoluzione FROM Soluzioni SOL Inner Join Rette_Soluzioni RETSOL ON SOL.IDSoluzione=RETSOL.IDSoluzione WHERE RETSOL.IDRetta='" + IDRetta + "' and SOL.IDStato=6 and SOL.Tipologia='Soluzione MR Modello') ";
                //}
                //else
                //{
                //    SQLString = SQLString + "Delete from Rette_Soluzioni where IDRettaSoluzione in (Select RETSOL.IDRettaSoluzione FROM Soluzioni SOL Inner Join Rette_Soluzioni RETSOL ON SOL.IDSoluzione=RETSOL.IDSoluzione WHERE RETSOL.IDRetta='" + IDRetta + "' and SOL.IDStato=6) ";
                //}

                //cmd.CommandText = SQLString;
                //cmd.Connection = cnn;
                //ret = cmd.ExecuteNonQuery();

                foreach (KeyValuePair<int, EOS.Core.Model.Model_Rette_Soluzioni> element in ControlRetteSoluzioni.GetByIDRetta(IDRetta))
                {
                    ModelRetteSoluzioni = element.Value;
                    ControlRetteSoluzioni.DeleteRetteSoluzioni(ModelRetteSoluzioni.IDRettaSoluzione);
                }
            }

            //cmd = null;
            //cnn = null;

            ModelRetteSoluzioni = null;
            ControlRetteSoluzioni = null;

            return ret;
        }

        public int AddRetteSoluzioni(EOS.Core.Model.Model_Rette_Soluzioni ModelRetteSoluzioni)
        {
            int newid = 0;

            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "INSERT INTO [dbo].[Rette_Soluzioni] ";
                SQLString = SQLString + "           ([IDRetta] ";
                SQLString = SQLString + "           ,[IDSoluzione] ";
                SQLString = SQLString + "           ,[DataCorrelazione]) ";
                SQLString = SQLString + "     VALUES ";
                SQLString = SQLString + "           ('{0}' ";
                SQLString = SQLString + "           ,'{1}' ";
                SQLString = SQLString + "           ,'{2}); SELECT SCOPE_IDENTITY() ";

                SQLString = string.Format(SQLString, ModelRetteSoluzioni.IDRetta, ModelRetteSoluzioni.IDSoluzione, ModelRetteSoluzioni.DataCorrelazione);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                newid = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = null;
                cnn = null;

                AddLogRettaSoluzione("Inserimento", ModelRetteSoluzioni, newid);

                return newid;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int DeleteRetteSoluzioni(int IDRettaSoluzione)
        {
            try
            {
                EOS.Core.Control.Control_RetteSoluzioni ControlRetteSoluzioni = new EOS.Core.Control.Control_RetteSoluzioni();
                ControlRetteSoluzioni.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                EOS.Core.Model.Model_Rette_Soluzioni ModelRetteSoluzioni = new EOS.Core.Model.Model_Rette_Soluzioni();
                ModelRetteSoluzioni = ControlRetteSoluzioni.GetByIDRettaSoluzione(IDRettaSoluzione).First().Value;

                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "DELETE FROM [dbo].[Rette_Soluzioni] WHERE IDRettaSoluzione={0} ";
                SQLString = string.Format(SQLString, IDRettaSoluzione);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

                cmd = null;
                cnn = null;

                AddLogRettaSoluzione("Cancellazione", ModelRetteSoluzioni);

                ModelRetteSoluzioni = null;
                ControlRetteSoluzioni = null;

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int AddLogRettaSoluzione(string TipoOperazione, EOS.Core.Model.Model_Rette_Soluzioni ModelRetteSoluzioni, int newid = 0)
        {
            try
            {
                Control_Transcode ctlTranscode = new Control_Transcode();
                Control_Log ctlLog = new Control_Log();

                string SQLStringLog = "";

                SQLStringLog = SQLStringLog + "Retta = " + ctlTranscode.GetCodiceRettaByID(ModelRetteSoluzioni.IDRetta) + System.Environment.NewLine;
                SQLStringLog = SQLStringLog + "Soluzione MR = " + ctlTranscode.GetCodiceSoluzioneByID(ModelRetteSoluzioni.IDSoluzione) + System.Environment.NewLine;
 
                ctlLog.ConnectionString = ConnectionString;

                if (newid != 0)
                {
                    ctlLog.InsertLog(TipoOperazione, "Relazione Rette Soluzioni", newid, ctlTranscode.GetCodiceRettaByID(ModelRetteSoluzioni.IDRetta), SQLStringLog, IDUtente);
                }
                else
                {
                    ctlLog.InsertLog(TipoOperazione, "Relazione Rette Soluzioni", ModelRetteSoluzioni.IDRettaSoluzione, ctlTranscode.GetCodiceRettaByID(ModelRetteSoluzioni.IDRetta), SQLStringLog, IDUtente);
                }

                ctlTranscode = null;
                ctlLog = null;

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
