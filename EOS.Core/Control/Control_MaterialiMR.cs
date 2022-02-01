using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace EOS.Core.Control
{
    public class Control_MaterialiMR
    {
        public string ConnectionString="";
        public IDictionary<int, EOS.Core.Model.Model_MaterialiMR> GetByIDSchedaDocumenti(int IDSchedaDocumenti)
        {
            string SQLString = "";

            SQLString = SQLString + "select  ";
            SQLString = SQLString + "SD.IDSchedaDocumenti, ";
            SQLString = SQLString + "M.CodiceProdotto as Codice_Materiale, ";
            SQLString = SQLString + "M.Cas as CAS_Materiale, ";
            SQLString = SQLString + "M.DenominazioneProdotto as Denominazione_Materiale, ";
            SQLString = SQLString + "TM.Nome as Tipo_Materiale, ";
            SQLString = SQLString + "SM.Nome as Stato_Materiale, ";
            SQLString = SQLString + "M.PMMisurando as PMMisurando_Materiale, ";
            SQLString = SQLString + "M.PMSostanza as PMSostanza_Materiale, ";
            SQLString = SQLString + "SD.Lotto as Numero_Lotto, ";
            SQLString = SQLString + "SD.DataScadenza as DataScadenza_Lotto, ";
            SQLString = SQLString + "SD.Concentrazione as Concentrazione_Lotto, ";
            SQLString = SQLString + "SD.Densita as Densita_Lotto, ";
            SQLString = SQLString + "SD.Purezza as Purezza_Lotto, ";
            SQLString = SQLString + "SD.Acqua as Acqua_Lotto ";
            SQLString = SQLString + "from [SERVER026].[LUPIN].dbo.SchedeDocumenti SD ";
            SQLString = SQLString + "left join [SERVER026].[LUPIN].dbo.Materiali M ";
            SQLString = SQLString + "ON M.IDMateriale=SD.IDMateriale ";
            SQLString = SQLString + "left join [SERVER026].[LUPIN].dbo.StatoMateriale SM ";
            SQLString = SQLString + "on M.IDStatoMateriale=SM.IDStatoMateriale ";
            SQLString = SQLString + "left join [SERVER026].[LUPIN].dbo.TipoMateriale TM ";
            SQLString = SQLString + "ON M.IDTipoMateriale=TM.IDTipoMateriale ";
            SQLString = SQLString + "where SD.IDSchedaDocumenti={0} ";

            SQLString = string.Format(SQLString, IDSchedaDocumenti);
            return GetData(SQLString);
        }

        public IDictionary<int, EOS.Core.Model.Model_MaterialiMR> GetData(string SQLString)
        {
            IDictionary<int, EOS.Core.Model.Model_MaterialiMR> ret = new Dictionary<int, EOS.Core.Model.Model_MaterialiMR>();

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
                            EOS.Core.Model.Model_MaterialiMR r = new EOS.Core.Model.Model_MaterialiMR();

                            if (DBNull.Value.Equals(dr["IDSchedaDocumenti"])) { r.IDSchedaDocumenti = 0; } else { r.IDSchedaDocumenti = Convert.ToInt32(dr["IDSchedaDocumenti"]); }
                            if (DBNull.Value.Equals(dr["Codice_Materiale"])) { r.Codice_Materiale = ""; } else { r.Codice_Materiale = Convert.ToString(dr["Codice_Materiale"]); }
                            if (DBNull.Value.Equals(dr["CAS_Materiale"])) { r.CAS_Materiale = ""; } else { r.CAS_Materiale = Convert.ToString(dr["CAS_Materiale"]); }
                            if (DBNull.Value.Equals(dr["Denominazione_Materiale"])) { r.Denominazione_Materiale = ""; } else { r.Denominazione_Materiale = Convert.ToString(dr["Denominazione_Materiale"]); }
                            if (DBNull.Value.Equals(dr["Tipo_Materiale"])) { r.Tipo_Materiale = ""; } else { r.Tipo_Materiale = Convert.ToString(dr["Tipo_Materiale"]); }
                            if (DBNull.Value.Equals(dr["Stato_Materiale"])) { r.Stato_Materiale = ""; } else { r.Stato_Materiale = Convert.ToString(dr["Stato_Materiale"]); }
                            if (DBNull.Value.Equals(dr["PMMisurando_Materiale"])) { r.PMMisurando_Materiale = ""; } else { r.PMMisurando_Materiale = Convert.ToString(dr["PMMisurando_Materiale"]); }
                            if (DBNull.Value.Equals(dr["PMSostanza_Materiale"])) { r.PMSostanza_Materiale = ""; } else { r.PMSostanza_Materiale = Convert.ToString(dr["PMSostanza_Materiale"]); }
                            if (DBNull.Value.Equals(dr["Numero_Lotto"])) { r.Numero_Lotto = ""; } else { r.Numero_Lotto = Convert.ToString(dr["Numero_Lotto"]); }
                            if (DBNull.Value.Equals(dr["DataScadenza_Lotto"])) { r.DataScadenza_Lotto = ""; } else { r.DataScadenza_Lotto = Convert.ToString(dr["DataScadenza_Lotto"]); }
                            if (DBNull.Value.Equals(dr["Concentrazione_Lotto"])) { r.Concentrazione_Lotto = ""; } else { r.Concentrazione_Lotto = Convert.ToString(dr["Concentrazione_Lotto"]); }
                            if (DBNull.Value.Equals(dr["Densita_Lotto"])) { r.Densita_Lotto = ""; } else { r.Densita_Lotto = Convert.ToString(dr["Densita_Lotto"]); }
                            if (DBNull.Value.Equals(dr["Purezza_Lotto"])) { r.Purezza_Lotto = ""; } else { r.Purezza_Lotto = Convert.ToString(dr["Purezza_Lotto"]); }
                            if (DBNull.Value.Equals(dr["Acqua_Lotto"])) { r.Acqua_Lotto = ""; } else { r.Acqua_Lotto = Convert.ToString(dr["Acqua_Lotto"]); }

                            ret.Add(r.IDSchedaDocumenti, r);
                            r = null;
                        }
                    }
                    else
                    {
                        EOS.Core.Model.Model_MaterialiMR r = new EOS.Core.Model.Model_MaterialiMR();

                        r.IDSchedaDocumenti = 0;
                        r.Codice_Materiale = "";
                        r.CAS_Materiale = "";
                        r.Denominazione_Materiale = "";
                        r.Tipo_Materiale = "";
                        r.Stato_Materiale = "";
                        r.PMMisurando_Materiale = "";
                        r.PMSostanza_Materiale = "";
                        r.Numero_Lotto = "";
                        r.DataScadenza_Lotto = "";
                        r.Concentrazione_Lotto = "";
                        r.Densita_Lotto = "";
                        r.Purezza_Lotto = "";
                        r.Acqua_Lotto = "";

                        ret.Add(r.IDSchedaDocumenti, r);
                        r = null;
                    }
                }
            }
            return ret;
        }

    }
}
