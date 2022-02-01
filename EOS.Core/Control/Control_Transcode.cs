using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EOS.Core.Control
{
    class Control_Transcode
    {
        //TipologiaMR
        public string GetTipologiaMRByID(int ID)
        {
            string ret = "";
            string SQLString = "";

            SQLString = "Select Nome from Materiale_Tipologia WHERE ID='" + ID + "' ";

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
                            ret = dr["Nome"].ToString();
                        }
                    }
                }
            }

            return ret;
        }

        //IDUbicazione
        public string GetUbicazioneByID(int ID)
        {
            string ret = "";
            string SQLString = "";

            SQLString = "Select Ubicazione from [SERVER026].[LUPIN].[dbo].Ubicazioni WHERE IDUbicazione='" + ID + "' ";

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
                            ret = dr["Ubicazione"].ToString();
                        }
                    }
                }
            }

            return ret;
        }

        //IDSchedaDocumenti
        public string GetIdentificativoELottoByID(int ID)
        {
            string ret = "";
            string SQLString = "";

            SQLString = "Select MAT.Identificativo + ' - ' + SD.Lotto as IdentificativoLotto from [SERVER026].[LUPIN].[dbo].SchedeDocumenti  SD INNER JOIN [SERVER026].[LUPIN].[dbo].Materiali  MAT ON SD.IDMateriale=MAT.IDMateriale WHERE SD.IDSchedaDocumenti='" + ID + "' ";

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
                            ret = dr["IdentificativoLotto"].ToString();
                        }
                    }
                }
            }

            return ret;
        }

        //IDSolvente
        public string GetCodiceSolventeByID(int ID)
        {
            string ret = "";
            string SQLString = "";

            SQLString = "Select CodiceSolvente from Solventi WHERE IDSolvente='" + ID + "' ";

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

        //IDSoluzione
        public string GetCodiceSoluzioneByID(int ID)
        {
            string ret = "";
            string SQLString = "";

            SQLString = "Select CodiceSoluzione from Soluzioni WHERE IDSoluzione='" + ID + "' ";

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

        public string GetIDUtenteByIDSOLSOV(string SOLSOV, int ID)
        {
            string ret = "";
            string SQLString = "";

            if(SOLSOV=="SOL")
            {
                SQLString = "Select IDUtente from Soluzioni WHERE IDSoluzione='" + ID + "' ";
            }
            else
            {
                SQLString = "Select IDUtente from Solventi WHERE IDSolvente='" + ID + "' ";
            }

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
                            ret = dr["IDUtente"].ToString();
                        }
                    }
                }
            }

            return ret;
        }

        //IDUtente
        public string GetNomeUtenteByID(int ID)
        {
            string ret = "";
            string SQLString = "";

            SQLString = "Select NomeUtente from Login_Utenti WHERE IDUtente='" + ID + "' ";

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
                            ret = dr["NomeUtente"].ToString();
                        }
                    }
                }
            }

            return ret;
        }

        //IDStato
        public string GetStatoByID(int ID)
        {
            string ret = "";
            string SQLString = "";

            SQLString = "Select Nome from Composti_Stati WHERE ID='" + ID + "' ";

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
                            ret = dr["Nome"].ToString();
                        }
                    }
                }
            }

            return ret;
        }

        //IDApparecchio
        public string GetCodiceApparecchioByID(int ID)
        {
            string ret = "";
            string SQLString = "";

            SQLString = "Select NumeroApparecchio from [SERVER026].[LUPIN].[dbo].Apparecchi WHERE IDApparecchio='" + ID + "' ";

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
                            ret = dr["NumeroApparecchio"].ToString();
                        }
                    }
                }
            }

            return ret;
        }

        //IDUtensile
        public string GetNomeUtensileByID(int ID)
        {
            string ret = "";
            string SQLString = "";

            SQLString = "Select Nome from [SERVER026].[LUPIN].[dbo].Utensili WHERE IDUtensile='" + ID + "' ";

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
                            ret = dr["Nome"].ToString();
                        }
                    }
                }
            }

            return ret;
        }
    }
}
