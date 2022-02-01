using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace EOS.Core.Common
{
    public class DataBase
    {

        public string DataBaseName = "";
        public DataTable GetDataTable(string SQLString, string ConnectionString)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlConnection cnn = new SqlConnection(ConnectionString);
                cnn.Open();
                SqlDataAdapter dad = new SqlDataAdapter(SQLString, cnn);
                dad.Fill(dt);
                dad = null;
                cnn.Close();
                cnn = null;
            }
            catch (Exception e)
            {

            }
            return dt;
        }

        public DataTable GetDataTable(SqlCommand cmd)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlDataAdapter dad = new SqlDataAdapter(cmd);
                dad.Fill(dt);
                dad = null;
            }
            catch (Exception e)
            {

            }

            return dt;
        }

        public int ExecuteNonQuery(string SQLString, string ConnectionString)
        {
            int ret = -1;

            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLString;
                cmd.Connection = cnn;

                ret = cmd.ExecuteNonQuery();
                cmd = null;
                cnn = null;
            }
            catch (Exception e)
            {

            }
            return ret;
        }

        public Object ExecuteScalar(string SQLString, string ConnectionString)
        {
            Object ret = new object();

            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLString;
                cmd.Connection = cnn;

                ret = cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
            }

            return ret;
        }

        public bool CheckDatabaseConnection(string ConnectionString)
        {
            bool flag = new bool();
            flag = false;
            try
            {

                SqlConnection sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = ConnectionString;

                DataBaseName = String.Format("{0}\\{1}", sqlConnection.DataSource, sqlConnection.Database);
                sqlConnection.Open();
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlConnection = null;
                flag = true;
            }
            catch (Exception e)
            {
                XtraMessageBox.Show("Error connecting to database! Check configuration and retry.", "CheckDatabaseConnection", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return flag;
        }
    }
}
