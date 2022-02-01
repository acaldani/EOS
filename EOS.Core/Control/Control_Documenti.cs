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
    public class Control_Documenti
    {
        public string ConnectionString;
        public IDictionary<int, Model_Documenti> GetDocumentoByID(int IDDocumento)
        {
            string SQLString = string.Format("SELECT * FROM Documenti WHERE IDDocumento={0}", IDDocumento);
            return GetData(SQLString);
        }

        public IDictionary<int, Model_Documenti> GetDocumentoByCodiceCompostoNomeDocumento(string CodiceComposto, string NomeDocumento)
        {
            string SQLString = string.Format("SELECT * FROM Documenti WHERE CodiceComposto='{0}' and NomeDocumento='{1}'", CodiceComposto, NomeDocumento);
            return GetData(SQLString);
        }

        public IDictionary<int, Model_Documenti> GetDocumentiByCodiceComposto(string CodiceComposto)
        {
            string SQLString = string.Format("SELECT * FROM Documenti WHERE CodiceComposto='{0}'", CodiceComposto);
            return GetData(SQLString);
        }

        public IDictionary<int, Model_Documenti> GetData(string SQLString)
        {
            IDictionary<int, Model_Documenti> ret = new Dictionary<int, Model_Documenti>();

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
                            Model_Documenti r = new Model_Documenti();

                            if (DBNull.Value.Equals(dr["IDDocumento"])) { r.IDDocumento = 0; } else { r.IDDocumento = Convert.ToInt32(dr["IDDocumento"]); }
                            if (DBNull.Value.Equals(dr["CodiceComposto"])) { r.CodiceComposto = ""; } else { r.CodiceComposto = Convert.ToString(dr["CodiceComposto"]); }
                            if (DBNull.Value.Equals(dr["NomeDocumento"])) { r.NomeDocumento = ""; } else { r.NomeDocumento = Convert.ToString(dr["NomeDocumento"]); }
                            if (DBNull.Value.Equals(dr["DescrizioneDocumento"])) { r.DescrizioneDocumento = ""; } else { r.DescrizioneDocumento = Convert.ToString(dr["DescrizioneDocumento"]); }
                            if (DBNull.Value.Equals(dr["PathDocumento"])) { r.PathDocumento = ""; } else { r.PathDocumento = Convert.ToString(dr["PathDocumento"]); }
                            if (DBNull.Value.Equals(dr["DataDocumento"])) { r.DataDocumento = Convert.ToDateTime("01/01/0001"); } else { r.DataDocumento = Convert.ToDateTime(dr["DataDocumento"]); }

                            ret.Add(r.IDDocumento, r);
                            r = null;
                        }
                    }
                    else
                    {
                        Model_Documenti r = new Model_Documenti();

                        r.IDDocumento = 0;
                        r.CodiceComposto = "";
                        r.NomeDocumento = "";
                        r.DescrizioneDocumento = "";
                        r.PathDocumento = "";
                        r.DataDocumento = Convert.ToDateTime(null);

                        ret.Add(r.IDDocumento, r);
                        r = null;
                    }
                }
            }
            return ret;
        }

        public int AddDocumento(Model_Documenti Documento)
        {
            string DataDocumento;
            int newid = 0;

            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "INSERT INTO [dbo].[Documenti] ";
                SQLString = SQLString + "           ([CodiceComposto] ";
                SQLString = SQLString + "           ,[NomeDocumento] ";
                SQLString = SQLString + "           ,[DescrizioneDocumento] ";
                SQLString = SQLString + "           ,[PathDocumento] ";
                SQLString = SQLString + "           ,[DataDocumento]) ";
                SQLString = SQLString + "     VALUES ";
                SQLString = SQLString + "           ('{0}' ";
                SQLString = SQLString + "           ,'{1}' ";
                SQLString = SQLString + "           ,'{2}' ";
                SQLString = SQLString + "           ,'{3}' ";
                SQLString = SQLString + "           ,Case When '{4}'<>'01/01/0001 00:00:00' then '{4}' else null end); SELECT SCOPE_IDENTITY() ";

                if ((Documento.DataDocumento != null) && (Documento.DataDocumento != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataDocumento = Documento.DataDocumento.ToString();
                }
                else
                {
                    DataDocumento = "01/01/0001 00:00:00";
                }

                SQLString = string.Format(SQLString, Documento.CodiceComposto.Replace("'","''"), Documento.NomeDocumento.Replace("'", "''"), Documento.DescrizioneDocumento.Replace("'", "''"), Documento.PathDocumento.Replace("'", "''"), DataDocumento);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                newid = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = null;
                cnn = null;

                return newid;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int UpdateDocumento(Model_Documenti Documento)
        {
            string DataDocumento;

            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "UPDATE [dbo].[Documenti] ";
                SQLString = SQLString + "   SET [CodiceComposto] = '{0}' ";
                SQLString = SQLString + "      ,[NomeDocumento] = '{1}' ";
                SQLString = SQLString + "      ,[DescrizioneDocumento] = '{2}' ";
                SQLString = SQLString + "      ,[PathDocumento] = '{3}' ";
                SQLString = SQLString + "      ,[DataDocumento] = Case When '{4}'<>'01/01/0001 00:00:00' then '{4}' else null end ";
                SQLString = SQLString + " WHERE  IDDocumento={5} ";

                if ((Documento.DataDocumento != null) && (Documento.DataDocumento != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataDocumento = Documento.DataDocumento.ToString();
                }
                else
                {
                    DataDocumento = "01/01/0001 00:00:00";
                }

                SQLString = string.Format(SQLString, Documento.CodiceComposto.Replace("'", "''"), Documento.NomeDocumento.Replace("'", "''"), Documento.DescrizioneDocumento.Replace("'", "''"), Documento.PathDocumento.Replace("'", "''"), DataDocumento,Documento.IDDocumento);

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

        public int DeleteDocumento(Model_Documenti Documento)
        {
            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "DELETE FROM [dbo].[Documenti] WHERE IDDocumento={0} ";
                SQLString = string.Format(SQLString, Documento.IDDocumento);

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
