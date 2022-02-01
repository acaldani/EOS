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
    public class Control_Soluzioni_Details_Concentration
    {

        public string ConnectionString;
        public IDictionary<int, EOS.Core.Model.Model_Soluzioni_Details_Concentration> GetByIDSolutionDetailConcentration(int IDSoluzioneDetail)
        {
            string SQLString = string.Format("SELECT * FROM Soluzioni_Details_Concentration WHERE IDSoluzioneDetailConcentration={0}", IDSoluzioneDetail);
            return GetData(SQLString);
        }

        public IDictionary<int, EOS.Core.Model.Model_Soluzioni_Details_Concentration> GetByIDSolution(int IDSoluzione)
        {
            string SQLString = string.Format("SELECT * FROM Soluzioni_Details_Concentration WHERE IDSoluzioneMaster={0}", IDSoluzione);
            return GetData(SQLString);
        }

        public IDictionary<int, EOS.Core.Model.Model_Soluzioni_Details_Concentration> GetByIDSolutionCAS(int IDSoluzione, string CAS)
        {
            string SQLString = string.Format("SELECT * FROM Soluzioni_Details_Concentration WHERE IDSoluzioneMaster={0} and CAS='{1}' ", IDSoluzione, CAS);
            return GetData(SQLString);
        }

        public int AddSolutionDetailConcentration(EOS.Core.Model.Model_Soluzioni_Details_Concentration SoluzioneDetailConcentration)
        {
            int newid = 0;
            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "INSERT INTO [dbo].[Soluzioni_Details_Concentration] ";
                SQLString = SQLString + "           ([IDSoluzioneMaster] ";
                SQLString = SQLString + "           ,[CAS] ";
                SQLString = SQLString + "           ,[ConcentrazioneFinale] ";
                SQLString = SQLString + "           ,[DataCalcolo]) ";
                SQLString = SQLString + "     VALUES ";
                SQLString = SQLString + "           ('{0}' ";
                SQLString = SQLString + "           ,'{1}' ";
                SQLString = SQLString + "           ,{2} ";
                SQLString = SQLString + "           ,'{3}') ";

                SQLString = string.Format(SQLString, SoluzioneDetailConcentration.IDSoluzioneMaster, SoluzioneDetailConcentration.CAS.Replace("'", "''"), SoluzioneDetailConcentration.ConcentrazioneFinale.ToString().Replace(",","."), SoluzioneDetailConcentration.DataCalcolo.ToString());

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                newid = cmd.ExecuteNonQuery();

                cmd = null;
                cnn = null;

                return newid;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int UpdateSolutionDetail(EOS.Core.Model.Model_Soluzioni_Details_Concentration SoluzioneDetailConcentration)
        {
            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "UPDATE [dbo].[Soluzioni_Details_Concentration] ";
                SQLString = SQLString + "   SET [IDSoluzioneMaster] = '{0}' ";
                SQLString = SQLString + "      ,[CAS] = '{1}' ";
                SQLString = SQLString + "      ,[ConcentrazioneFinale] = {2} ";
                SQLString = SQLString + "      ,[DataCalcolo] = '{3}' ";
                SQLString = SQLString + " WHERE  IDSoluzioneDetail={4} ";

                SQLString = string.Format(SQLString, SoluzioneDetailConcentration.IDSoluzioneMaster, SoluzioneDetailConcentration.CAS.Replace("'", "''"), SoluzioneDetailConcentration.ConcentrazioneFinale, SoluzioneDetailConcentration.DataCalcolo, SoluzioneDetailConcentration.IDSoluzioneDetailConcentration);

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

        public int DeleteSolutionDetailsConcentrationByIDSoluzione(int IDSoluzione)
        {
            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "DELETE FROM [dbo].[Soluzioni_Details_Concentration] WHERE IDSoluzioneMaster={0} ";
                SQLString = string.Format(SQLString, IDSoluzione);

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

        public int DeleteSolutionDetailConcentration(EOS.Core.Model.Model_Soluzioni_Details_Concentration SoluzioneDetailConcentration)
        {
            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "DELETE FROM [dbo].[SoluzioneDetailConcentration] WHERE IDSoluzioneDetailConcentration={0} ";
                SQLString = string.Format(SQLString, SoluzioneDetailConcentration.IDSoluzioneDetailConcentration);

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

        public IDictionary<int, EOS.Core.Model.Model_Soluzioni_Details_Concentration> GetData(string SQLString)
        {
            IDictionary<int, EOS.Core.Model.Model_Soluzioni_Details_Concentration> ret = new Dictionary<int, EOS.Core.Model.Model_Soluzioni_Details_Concentration>();

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
                            EOS.Core.Model.Model_Soluzioni_Details_Concentration r = new EOS.Core.Model.Model_Soluzioni_Details_Concentration();

                            if (DBNull.Value.Equals(dr["IDSoluzioneDetailConcentration"])) { r.IDSoluzioneDetailConcentration = 0; } else { r.IDSoluzioneDetailConcentration = Convert.ToInt32(dr["IDSoluzioneDetailConcentration"]); }
                            if (DBNull.Value.Equals(dr["IDSoluzioneMaster"])) { r.IDSoluzioneMaster = 0; } else { r.IDSoluzioneMaster = Convert.ToInt32(dr["IDSoluzioneMaster"]); }
                            if (DBNull.Value.Equals(dr["CAS"])) { r.CAS = ""; } else { r.CAS = Convert.ToString(dr["CAS"]); }
                            if (DBNull.Value.Equals(dr["ConcentrazioneFinale"])) { r.ConcentrazioneFinale = 0; } else { r.ConcentrazioneFinale = Convert.ToDecimal(dr["ConcentrazioneFinale"]); }
                            if (DBNull.Value.Equals(dr["DataCalcolo"])) { r.DataCalcolo = Convert.ToDateTime("01/01/0001"); } else { r.DataCalcolo = Convert.ToDateTime(dr["DataCalcolo"]); }

                            ret.Add(r.IDSoluzioneDetailConcentration, r);
                            r = null;
                        }
                    }
                    else
                    {
                        EOS.Core.Model.Model_Soluzioni_Details_Concentration r = new EOS.Core.Model.Model_Soluzioni_Details_Concentration();

                        r.IDSoluzioneDetailConcentration = 0;
                        r.IDSoluzioneMaster = 0;
                        r.CAS = "";
                        r.ConcentrazioneFinale = Convert.ToDecimal(0);
                        r.DataCalcolo = Convert.ToDateTime(null);

                        ret.Add(r.IDSoluzioneDetailConcentration, r);
                        r = null;
                    }
                }
            }
            return ret;
        }
    }
}
