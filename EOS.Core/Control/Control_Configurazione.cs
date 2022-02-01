using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace EOS.Core.Control
{
    public class Control_Configurazione
    {
        public string ConnectionString;

        public IDictionary<int, EOS.Core.Model.Model_Configurazione> GetActiveConfiguration()
        {
            string SQLString = string.Format("SELECT top 1 * FROM Configurazione WHERE attiva=1 ");
            return GetData(SQLString);
        }

        public IDictionary<int, EOS.Core.Model.Model_Configurazione> GetData(string SQLString)
        {
            IDictionary<int, EOS.Core.Model.Model_Configurazione> ret = new Dictionary<int, EOS.Core.Model.Model_Configurazione>();

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
                            EOS.Core.Model.Model_Configurazione r = new EOS.Core.Model.Model_Configurazione();

                            if (DBNull.Value.Equals(dr["IDConfigurazione"])) { r.IDConfigurazione = 0; } else { r.IDConfigurazione = Convert.ToInt32(dr["IDConfigurazione"]); }
                            if (DBNull.Value.Equals(dr["Email"])) { r.Email = ""; } else { r.Email = Convert.ToString(dr["Email"]); }
                            if (DBNull.Value.Equals(dr["LoginAbilitazioni"])) { r.LoginAbilitazioni = 0; } else { r.LoginAbilitazioni = Convert.ToInt32(dr["LoginAbilitazioni"]); }
                            if (DBNull.Value.Equals(dr["LogTempiEsecuzione"])) { r.LogTempiEsecuzione = false; } else { r.LogTempiEsecuzione = Convert.ToBoolean(dr["LogTempiEsecuzione"]); }
                            if (DBNull.Value.Equals(dr["Attiva"])) { r.Attiva = false; } else { r.Attiva = Convert.ToBoolean(dr["Attiva"]); }
                            if (DBNull.Value.Equals(dr["AuthorizationPassword"])) { r.AuthorizationPassword = ""; } else { r.AuthorizationPassword = Convert.ToString(dr["AuthorizationPassword"]); }
                            if (DBNull.Value.Equals(dr["NewFromTemplate"])) { r.NewFromTemplate = false; } else { r.NewFromTemplate = Convert.ToBoolean(dr["NewFromTemplate"]); }
                            if (DBNull.Value.Equals(dr["DocumentsRoot"])) { r.DocumentsRoot = ""; } else { r.DocumentsRoot = Convert.ToString(dr["DocumentsRoot"]); }

                            ret.Add(r.IDConfigurazione, r);
                            r = null;
                        }
                    }
                    else
                    {
                        EOS.Core.Model.Model_Configurazione r = new EOS.Core.Model.Model_Configurazione();

                        r.IDConfigurazione = 0;
                        r.Email = "";
                        r.LoginAbilitazioni = 0;
                        r.LogTempiEsecuzione = false;
                        r.Attiva = false;
                        r.AuthorizationPassword = "";
                        r.NewFromTemplate = false;
                        r.DocumentsRoot = "";

                        ret.Add(r.IDConfigurazione, r);
                        r = null;
                    }
                }
            }
            return ret;
        }

    }
}
