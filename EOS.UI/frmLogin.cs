using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;


using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace EOS.UI
{
    public partial class frmLogin : Form
    {

        public bool login = false;
        public int IDUtente = 0;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void butLogin_Click(object sender, EventArgs e)
        {
            if((Convert.ToString(txtUser.EditValue)!="") && (Convert.ToString(txtUser.EditValue) != null) && (Convert.ToString(txtPassword.EditValue)!="") && (txtPassword.EditValue != null))
            {
                EOS.Core.Control.Control_Utenti ControlUtenti = new EOS.Core.Control.Control_Utenti();

                IDUtente = ControlUtenti.LoginCheck(txtUser.EditValue.ToString(), txtPassword.EditValue.ToString());

                if(IDUtente!=0)
                {
                    EOS.Core.Common.LoginData.UserID = IDUtente;

                    login = true;

                    this.Close();
                }
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Inserisci una User e una password", "Errore di autenticazione", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Version version = assembly.GetName().Version;
            this.Text = "EOS Login " + version;
        }

        private void butAnnulla_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static void SaveUserPreferences(string FormName, string ControlName, MemoryStream PreferenceString, int IDUser)
        {
            try
            {
                PreferenceString.Position = 0;
                StreamReader Reader = new StreamReader(PreferenceString);
                string text = Reader.ReadToEnd();

                text = text.ToString().Replace("'", "''");
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                cnn.Open();
                cmd.Connection = cnn;

                ResetUserPreferences(FormName, ControlName, IDUser);

                cmd.CommandText = string.Format("INSERT INTO Login_UtentiPersonalizzazioni (IDUtente, FormName, ControlName, Customizations) VALUES ({0},'{1}','{2}','{3}')", IDUser, FormName, ControlName, text);
                cmd.ExecuteScalar();

                cmd.Dispose();
                cnn.Close();
                cnn.Dispose();
                cnn = null;
            }
            catch
            {

            }
        }

        public static MemoryStream LoadUserPreferences(string FormName, string ControlName, int IDUser)
        {
            string text = "";
            MemoryStream ret = new MemoryStream();

            try
            {
                using (System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection())
                {
                    cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                    cnn.Open();

                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                    {
                        cmd.Connection = cnn;

                        System.Data.SqlClient.SqlDataReader dr;

                        cmd.CommandText = String.Format("SELECT Customizations FROM Login_UtentiPersonalizzazioni WHERE IDUtente=@IDUtente AND FormName=@FormName AND ControlName=@ControlName");
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IDUtente", IDUser);
                        cmd.Parameters.AddWithValue("@FormName", FormName);
                        cmd.Parameters.AddWithValue("@ControlName", ControlName);

                        dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            dr.Read();

                            text = dr["Customizations"].ToString().Replace("''", "'");
                            if(text != null)
                            {
                                byte[] byteArray = Encoding.ASCII.GetBytes(text);
                                ret = new MemoryStream(byteArray);
                            }
                            else
                            {
                                ret = null;
                            }
                        }
                        else
                        {
                            ret = null;
                        }

                        dr.Close();
                    }    
                }
            }
            catch(Exception ex)
            {
                //ShowErrorGenericException(ex);
                ret = null;
            }

            return ret;
        }
        public static void ResetUserPreferences(string FormName, string ControlName, int IDUser)
        {
            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                cnn.Open();
                cmd.Connection = cnn;

                cmd.CommandText = String.Format("DELETE FROM Login_UtentiPersonalizzazioni WHERE IDUtente={0} AND FormName='{1}' AND ControlName='{2}'", IDUser, FormName, ControlName);
                    cmd.ExecuteScalar();

                cmd.Dispose();
                cnn.Close();
                cnn.Dispose();
                cnn = null;
            }
            catch (Exception ex)
            {
                //ShowErrorGenericException(ex)
            }
        }
    }
}