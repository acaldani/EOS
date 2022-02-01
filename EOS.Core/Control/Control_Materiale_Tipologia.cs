using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EOS.Core.Control
{
    public class Control_Materiale_Tipologia
    {
        public string ConnectionString;
        public IDictionary<int, EOS.Core.Model.Model_Materiale_Tipologia> GetByID(int ID)
        {
            string SQLString = string.Format("SELECT * FROM Materiale_Tipologia WHERE ID={0}", ID);
            return GetData(SQLString);
        }

        public IDictionary<int, EOS.Core.Model.Model_Materiale_Tipologia> GetData(string SQLString)
        {
            IDictionary<int, EOS.Core.Model.Model_Materiale_Tipologia> ret = new Dictionary<int, EOS.Core.Model.Model_Materiale_Tipologia>();

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
                            EOS.Core.Model.Model_Materiale_Tipologia r = new EOS.Core.Model.Model_Materiale_Tipologia();

                            if (DBNull.Value.Equals(dr["ID"])) { r.ID = 0; } else { r.ID = Convert.ToInt32(dr["ID"]); }
                            if (DBNull.Value.Equals(dr["Nome"])) { r.Nome = ""; } else { r.Nome = Convert.ToString(dr["Nome"]); }
                            if (DBNull.Value.Equals(dr["MultiploSingolo"])) { r.MultiploSingolo = ""; } else { r.MultiploSingolo = Convert.ToString(dr["MultiploSingolo"]); }
                            if (DBNull.Value.Equals(dr["PesoVolumeConcentrazione"])) { r.PesoVolumeConcentrazione = ""; } else { r.PesoVolumeConcentrazione = Convert.ToString(dr["PesoVolumeConcentrazione"]); }
                            if (DBNull.Value.Equals(dr["UM"])) { r.UM = ""; } else { r.UM = Convert.ToString(dr["UM"]); }

                            ret.Add(r.ID, r);
                            r = null;
                        }
                    }
                    else
                    {
                        EOS.Core.Model.Model_Materiale_Tipologia r = new EOS.Core.Model.Model_Materiale_Tipologia();

                        r.ID = 0;
                        r.Nome = "";
                        r.MultiploSingolo = "";
                        r.PesoVolumeConcentrazione = "";
                        r.UM = "";

                        ret.Add(r.ID, r);
                        r = null;
                    }
                }
            }
            return ret;
        }
    }
}
