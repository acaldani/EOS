using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EOS.Core.Control
{
    class Control_Log
    {

        public string ConnectionString = "";

        public int InsertLog(string TipoOperazione, string Tabella, int IDTabella, string CodiceSoluzione, string DettaglioOperazione, int IDUtente)
        {
            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "INSERT INTO Log_Operazioni (TipoOperazione,Tabella,IDTabella,CodiceSoluzione,DettaglioOperazione,IDUtente,DataOperazione) VALUES ( ";
                SQLString = SQLString + "   '" + TipoOperazione + "', ";
                SQLString = SQLString + "   '" + Tabella + "', ";
                SQLString = SQLString + "   '" + IDTabella + "', ";
                SQLString = SQLString + "   '" + CodiceSoluzione + "', ";
                SQLString = SQLString + "   '" + DettaglioOperazione + "', ";
                SQLString = SQLString + "   '" + IDUtente + "', ";
                SQLString = SQLString + "   getdate() ";
                SQLString = SQLString + " ) ";

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
