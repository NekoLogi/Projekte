using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MySQL_Example
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            // Variablen.
            string host = "localhost";
            int port = 3306;
            string database = "basedigitsclan";
            string username = "root";
            string password = "Ixcb3EM^c*Mck75^";

            // Füge alle Strings zusammen in connString.
            String connString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + password;
            // Erstelle Objekte und füge Wert von connString in conn ein.
            MySqlConnection conn = new MySqlConnection(connString);

            // schicke Objekt mit neuen Wert zurück zum Anforderer.
            return conn;
        }
    }
}
