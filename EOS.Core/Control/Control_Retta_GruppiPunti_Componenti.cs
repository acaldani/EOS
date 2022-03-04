using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EOS.Core.Model;
using System.Data;
using System.Data.SqlClient;

namespace EOS.Core.Control
{
    public class Control_Retta_GruppiPunti_Componenti
    {
        public string ConnectionString;
        public int IDUtente = 0;

        public IDictionary<int, Model_Retta_GruppiPunti_Componenti> GetData(string SQLString)
        {
            IDictionary<int, Model_Retta_GruppiPunti_Componenti> ret = new Dictionary<int, Model_Retta_GruppiPunti_Componenti>();

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
                            Model_Retta_GruppiPunti_Componenti r = new Model_Retta_GruppiPunti_Componenti();

                            if (DBNull.Value.Equals(dr["IDRetta_GruppoPunti_Componenti"])) { r.IDRetta_GruppoPunti_Componenti = 0; } else { r.IDRetta_GruppoPunti_Componenti = Convert.ToInt32(dr["IDRetta_GruppoPunti_Componenti"]); }
                            if (DBNull.Value.Equals(dr["IDRetta"])) { r.IDRetta = 0; } else { r.IDRetta = Convert.ToInt32(dr["IDRetta"]); }
                            if (DBNull.Value.Equals(dr["IDGruppoPunti"])) { r.IDGruppoPunti = 0; } else { r.IDGruppoPunti = Convert.ToInt32(dr["IDGruppoPunti"]); }
                            if (DBNull.Value.Equals(dr["IDSchedaDocumenti"])) { r.IDSchedaDocumenti = 0; } else { r.IDSchedaDocumenti = Convert.ToInt32(dr["IDSchedaDocumenti"]); }
                            if (DBNull.Value.Equals(dr["IDSoluzione"])) { r.IDSoluzione = 0; } else { r.IDSoluzione = Convert.ToInt32(dr["IDSoluzione"]); }
                            if (DBNull.Value.Equals(dr["Tipologia_MR"])) { r.Tipologia_MR = 0; } else { r.Tipologia_MR = Convert.ToInt32(dr["Tipologia_MR"]); }
                            if (DBNull.Value.Equals(dr["Note"])) { r.Note = ""; } else { r.Note = Convert.ToString(dr["Note"]); }
                            if (DBNull.Value.Equals(dr["DataSelezione"])) { r.DataSelezione = Convert.ToDateTime("01/01/0001"); } else { r.DataSelezione = Convert.ToDateTime(dr["DataSelezione"]); }
                            if (DBNull.Value.Equals(dr["DataInserimento"])) { r.DataInserimento = Convert.ToDateTime("01/01/0001"); } else { r.DataInserimento = Convert.ToDateTime(dr["DataInserimento"]); }

                            ret.Add(r.IDRetta_GruppoPunti_Componenti, r);
                            r = null;
                        }
                    }
                    else
                    {
                        Model_Retta_GruppiPunti_Componenti r = new Model_Retta_GruppiPunti_Componenti();

                        r.IDRetta_GruppoPunti_Componenti = 0;
                        r.IDRetta = 0;
                        r.IDGruppoPunti = 0;
                        r.IDSchedaDocumenti = 0;
                        r.IDSoluzione = 0;
                        r.Tipologia_MR = 0;
                        r.Note = "";
                        r.DataSelezione = Convert.ToDateTime(null);
                        r.DataInserimento = Convert.ToDateTime(null);

                        ret.Add(r.IDRetta_GruppoPunti_Componenti, r);
                        r = null;
                    }
                }
            }
            return ret;
        }

        public IDictionary<int, Model_Retta_GruppiPunti_Componenti> GetRettaGruppiPuntiComponentiByID(int IDRetta_GruppoPunti_Componenti)
        {
            string SQLString = string.Format("SELECT * FROM Rette_GruppoPunti_Componenti WHERE IDRetta_GruppoPunti_Componenti={0}", IDRetta_GruppoPunti_Componenti);
            return GetData(SQLString);
        }

        public IDictionary<int, Model_Retta_GruppiPunti_Componenti> GetRettaGruppiPuntiComponentiByIDRetta(int IDRetta)
        {
            string SQLString = string.Format("SELECT * FROM Rette_GruppoPunti_Componenti WHERE IDRetta={0}", IDRetta);
            return GetData(SQLString);
        }

        public IDictionary<int, Model_Retta_GruppiPunti_Componenti> GetRettaGruppiPuntiComponentiByIDRettaIDGruppoPunti(int IDRetta, int IDGruppoPunti)
        {
            string SQLString = string.Format("SELECT * FROM Rette_GruppoPunti_Componenti WHERE IDRetta={0} and IDGruppoPunti={1}", IDRetta, IDGruppoPunti);
            return GetData(SQLString);
        }

        public string GetCodiceRettaFromIDRetta_GruppoPunti_Componenti(int IDRetta_GruppoPunti_Componenti)
        {
            string ret = "";

            string SQLString = "SELECT RET.* FROM [dbo].[Rette] RET inner join [dbo].[Rette_GruppoPunti_Componenti] RETGC ON RET.IDRetta=RETGC.IDRetta WHERE IDRetta_GruppoPunti_Componenti='" + IDRetta_GruppoPunti_Componenti + "' ";

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

        public int AddRettaGruppiPuntiComponenti(Model_Retta_GruppiPunti_Componenti ModelRettaGruppiPuntiComponente)
        {
            int newid = 0;

            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "INSERT INTO [dbo].[Rette_GruppoPunti_Componenti] ";
                SQLString = SQLString + "           ([IDRetta] ";
                SQLString = SQLString + "           ,[IDGruppoPunti] ";
                SQLString = SQLString + "           ,[IDSchedaDocumenti] ";
                SQLString = SQLString + "           ,[IDSoluzione] ";
                SQLString = SQLString + "           ,[Tipologia_MR] ";
                SQLString = SQLString + "           ,[Note] ";
                SQLString = SQLString + "           ,[DataSelezione] ";
                SQLString = SQLString + "           ,[DataInserimento]) ";
                SQLString = SQLString + "     VALUES ";
                SQLString = SQLString + "           ('{0}' ";
                SQLString = SQLString + "           ,'{1}' ";
                SQLString = SQLString + "           ,'{2}' ";
                SQLString = SQLString + "           ,'{3}' ";
                SQLString = SQLString + "           ,'{4}' ";
                SQLString = SQLString + "           ,'{5}' ";
                SQLString = SQLString + "           ,GETDATE() ";
                SQLString = SQLString + "           ,NULL); SELECT SCOPE_IDENTITY() ";

                SQLString = string.Format(SQLString, ModelRettaGruppiPuntiComponente.IDRetta, ModelRettaGruppiPuntiComponente.IDGruppoPunti, ModelRettaGruppiPuntiComponente.IDSchedaDocumenti, ModelRettaGruppiPuntiComponente.IDSoluzione, ModelRettaGruppiPuntiComponente.Tipologia_MR, ModelRettaGruppiPuntiComponente.Note);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                newid = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = null;
                cnn = null;

                AddLogRettaGruppiPuntiComponente("Inserimento", ModelRettaGruppiPuntiComponente, newid);

                return newid;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int DeleteRettaGruppiPuntiComponenti(int IDRetta_GruppoPunti_Componenti)
        {
            try
            {
                EOS.Core.Control.Control_Retta_GruppiPunti_Componenti ControlRettaGruppiPuntiComponenti = new EOS.Core.Control.Control_Retta_GruppiPunti_Componenti();
                ControlRettaGruppiPuntiComponenti.ConnectionString = ConnectionString;
                EOS.Core.Model.Model_Retta_GruppiPunti_Componenti ModelRettaGruppiPuntiComponenti = new EOS.Core.Model.Model_Retta_GruppiPunti_Componenti();
                ModelRettaGruppiPuntiComponenti = ControlRettaGruppiPuntiComponenti.GetRettaGruppiPuntiComponentiByID(IDRetta_GruppoPunti_Componenti).First().Value;

                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "DELETE FROM [dbo].[Rette_GruppoPunti_Componenti] WHERE IDRetta_GruppoPunti_Componenti={0} ";
                SQLString = string.Format(SQLString, IDRetta_GruppoPunti_Componenti);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();
                cmd = null;
                cnn = null;

                AddLogRettaGruppiPuntiComponente("Cancellazione", ModelRettaGruppiPuntiComponenti);

                ModelRettaGruppiPuntiComponenti = null;
                ControlRettaGruppiPuntiComponenti = null;

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int SetDataInserimentoRettaGruppiPuntiComponenti(Model_Retta_GruppiPunti_Componenti ModelRettaGruppiPuntiComponente)
        {
            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "UPDATE [dbo].[Rette_GruppoPunti_Componenti] SET DATAINSERIMENTO=GETDATE() WHERE IDRetta_GruppoPunti_Componenti={0} ";
                SQLString = string.Format(SQLString, ModelRettaGruppiPuntiComponente.IDRetta_GruppoPunti_Componenti);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();
                cmd = null;
                cnn = null;

                AddLogRettaGruppiPuntiComponente("Aggiornamento", ModelRettaGruppiPuntiComponente);

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int AddLogRettaGruppiPuntiComponente(string TipoOperazione, Model_Retta_GruppiPunti_Componenti ModelRettaGruppiPuntiComponente, int newid = 0, Model_Retta_GruppiPunti_Componenti ModelRettaGruppiPuntiComponenteOriginale = null)
        {
            try
            {
                Control_Transcode ctlTranscode = new Control_Transcode();
                Control_Log ctlLog = new Control_Log();

                if (newid != 0)
                {
                    ModelRettaGruppiPuntiComponente.IDRetta_GruppoPunti_Componenti = newid;
                }

                string SQLStringLog = "";

                SQLStringLog = SQLStringLog + "CodiceRetta = " + ctlTranscode.GetCodiceRettaByID(ModelRettaGruppiPuntiComponente.IDRetta) + System.Environment.NewLine;
                SQLStringLog = SQLStringLog + "IDGruppoPunti = " + ModelRettaGruppiPuntiComponente.IDGruppoPunti + System.Environment.NewLine;
                SQLStringLog = SQLStringLog + "CodiceSoluzioneMR (Componente) = " + ctlTranscode.GetCodiceSoluzioneByID(ModelRettaGruppiPuntiComponente.IDSoluzione) + System.Environment.NewLine;
                SQLStringLog = SQLStringLog + "Identificativo - LottoMateriale (Componente) = " + ctlTranscode.GetIdentificativoELottoByID(ModelRettaGruppiPuntiComponente.IDSchedaDocumenti) + System.Environment.NewLine;
                SQLStringLog = SQLStringLog + "TipologiaMR = " + ctlTranscode.GetTipologiaMRByID(ModelRettaGruppiPuntiComponente.Tipologia_MR) + System.Environment.NewLine;
                SQLStringLog = SQLStringLog + "Note = " + ModelRettaGruppiPuntiComponente.Note + System.Environment.NewLine;

                ctlLog.ConnectionString = ConnectionString;

                ctlLog.InsertLog(TipoOperazione, "Rette Componenti Gruppi Punti", ModelRettaGruppiPuntiComponente.IDRetta_GruppoPunti_Componenti, ctlTranscode.GetCodiceRettaByID(ModelRettaGruppiPuntiComponente.IDRetta), SQLStringLog, IDUtente);

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
