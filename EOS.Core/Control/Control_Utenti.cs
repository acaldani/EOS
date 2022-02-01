using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EOS.Core.Control
{
    public class Control_Utenti
    {
        public int LoginCheck(String Utente, String Password)
        {
            int ret=0;

            string SQLString = "Select * from Login_Utenti WHERE NomeUtente=@User AND Password=@Password ";

            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                cnn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = SQLString;

                    //cmd.Parameters.AddWithValue("@User", SqlDbType.NVarChar).Value = txtUser.EditValue;
                    cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = Utente;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password;
                    var dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ret = Convert.ToInt32(dr["IDUtente"]);
                        }
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("User o password errati", "Errore di autenticazione", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }

            return ret;
        }

        public bool CloneAllowedCheck(int IDUtente)
        {
            bool ret = false;

            string SQLString = "Select CloneAllowed from Login_Utenti WHERE IDUtente=@IDUtente ";

            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                cnn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = SQLString;

                    cmd.Parameters.Add("@IDUtente", SqlDbType.NVarChar).Value = IDUtente;
                    var dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ret = Convert.ToBoolean(dr["CloneAllowed"]);
                        }
                    }
                    else
                    {
                        ret = false;
                    }
                }
            }

            return ret;

        }

    }
}
