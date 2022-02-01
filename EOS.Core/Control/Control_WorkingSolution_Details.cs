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
    public class Control_WorkingSolution_Details
    {
        public string ConnectionString;
        public IDictionary<int, Model_WorkingSolution_Details> GetByIDSchedaDocumenti(int IDSchedaDocumenti)
        {
            string SQLString = string.Format("SELECT * FROM [SERVER026].[LUPIN].dbo.WorkingSolutionsDetails WHERE IDSchedaDocumenti={0}", IDSchedaDocumenti);
            return GetData(SQLString);
        }

        public IDictionary<int, Model_WorkingSolution_Details> GetByIDSchedaDocumentiCAS(int IDSchedaDocumenti, string CAS)
        {
            string SQLString = string.Format("SELECT * FROM [SERVER026].[LUPIN].dbo.WorkingSolutionsDetails WHERE IDSchedaDocumenti={0} and CAS='{1}'", IDSchedaDocumenti,CAS);
            return GetData(SQLString);
        }

        public IDictionary<int, Model_WorkingSolution_Details> GetData(string SQLString)
        {
            IDictionary<int, Model_WorkingSolution_Details> ret = new Dictionary<int, Model_WorkingSolution_Details>();

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
                            Model_WorkingSolution_Details r = new Model_WorkingSolution_Details();

                            if (DBNull.Value.Equals(dr["IDWorkingSolutionDetail"])) { r.IDWorkingSolutionDetail = 0; } else { r.IDWorkingSolutionDetail = Convert.ToInt32(dr["IDWorkingSolutionDetail"]); }
                            if (DBNull.Value.Equals(dr["IDSchedaDocumenti"])) { r.IDSchedaDocumenti = 0; } else { r.IDSchedaDocumenti = Convert.ToInt32(dr["IDSchedaDocumenti"]); }
                            if (DBNull.Value.Equals(dr["NomeComponente"])) { r.NomeComponente = ""; } else { r.NomeComponente = Convert.ToString(dr["NomeComponente"]); }
                            if (DBNull.Value.Equals(dr["CASComponente"])) { r.CASComponente = ""; } else { r.CASComponente = Convert.ToString(dr["CASComponente"]); }
                            if (DBNull.Value.Equals(dr["UMConcentrazione"])) { r.UMConcentrazione = ""; } else { r.UMConcentrazione = Convert.ToString(dr["UMConcentrazione"]); }
                            if (DBNull.Value.Equals(dr["Concentrazione"])) { r.Concentrazione = Convert.ToDecimal(null); } else { r.Concentrazione = Convert.ToDecimal(dr["Concentrazione"]); }
                            if (DBNull.Value.Equals(dr["UMIncertezza"])) { r.UMIncertezza = ""; } else { r.UMIncertezza = Convert.ToString(dr["UMIncertezza"]); }
                            if (DBNull.Value.Equals(dr["Incertezza"])) { r.Incertezza = Convert.ToDecimal(null); } else { r.Incertezza = Convert.ToDecimal(dr["Incertezza"]); }
                            
                            ret.Add(r.IDWorkingSolutionDetail, r);
                            r = null;
                        }
                    }
                    else
                    {
                        Model_WorkingSolution_Details r = new Model_WorkingSolution_Details();

                        r.IDWorkingSolutionDetail = 0;
                        r.IDSchedaDocumenti = 0;
                        r.NomeComponente = "";
                        r.CASComponente = "";
                        r.UMConcentrazione = "";
                        r.Concentrazione = Convert.ToDecimal(null);
                        r.UMIncertezza = "";
                        r.Incertezza = Convert.ToDecimal(null);

                        ret.Add(r.IDWorkingSolutionDetail, r);
                        r = null;
                    }
                }
            }
            return ret;
        }
    }
}
