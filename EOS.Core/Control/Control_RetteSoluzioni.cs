using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EOS.Core.Control
{
    public class Control_RetteSoluzioni
    {
        public string ConnectionString;
        public int IDUtente = 0;

        public int countModelSolution(int IDRetta)
        {
            int ret = 0;
            string SQLString = "";

            SQLString = "select count(*) as conta from Rette_Soluzioni RETSOL INNER JOIN Soluzioni SOL ON RETSOL.IDSoluzione=SOL.IDSoluzione AND SOL.Tipologia='Soluzione MR Modello' Where RETSOL.IDRetta='" + IDRetta + "' ";
           
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
                            ret = Convert.ToInt32(dr["conta"]);
                        }
                    }
                }
            }

            return ret;
        }

        public int AnnullaScollegaSoluzioni(int IDRetta, int IDStato, bool Annulla, bool Scollega, bool Modello)
        {
            //IDSTATO=8 = Annullato
            int ret=0;

            System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
            cnn.ConnectionString = ConnectionString;
            cnn.Open();

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            string SQLString = "";

            if(Annulla)
            {
                if (Modello)
                {
                    SQLString = SQLString + "Update SOL Set SOL.IDStato=" + IDStato + " FROM Soluzioni SOL Inner Join Rette_Soluzioni RETSOL ON SOL.IDSoluzione=RETSOL.IDSoluzione WHERE RETSOL.IDRetta='" + IDRetta + "' and SOL.IDStato=6 and SOL.Tipologia='Soluzione MR Modello' ";
                }
                else
                {
                    SQLString = SQLString + "Update SOL Set SOL.IDStato=" + IDStato + " FROM Soluzioni SOL Inner Join Rette_Soluzioni RETSOL ON SOL.IDSoluzione=RETSOL.IDSoluzione WHERE RETSOL.IDRetta='" + IDRetta + "' and SOL.IDStato=6 ";
                }

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                ret = cmd.ExecuteNonQuery();
            }

            if(Scollega)
            {
                if (Modello)
                {
                    SQLString = SQLString + "Delete from Rette_Soluzioni where IDRettaSoluzione in (Select RETSOL.IDRettaSoluzione FROM Soluzioni SOL Inner Join Rette_Soluzioni RETSOL ON SOL.IDSoluzione=RETSOL.IDSoluzione WHERE RETSOL.IDRetta='" + IDRetta + "' and SOL.IDStato=6 and SOL.Tipologia='Soluzione MR Modello') ";
                }
                else
                {
                    SQLString = SQLString + "Delete from Rette_Soluzioni where IDRettaSoluzione in (Select RETSOL.IDRettaSoluzione FROM Soluzioni SOL Inner Join Rette_Soluzioni RETSOL ON SOL.IDSoluzione=RETSOL.IDSoluzione WHERE RETSOL.IDRetta='" + IDRetta + "' and SOL.IDStato=6) ";
                }

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                ret = cmd.ExecuteNonQuery();
            }

            cmd = null;
            cnn = null;

            return ret;
        }
    }
}
