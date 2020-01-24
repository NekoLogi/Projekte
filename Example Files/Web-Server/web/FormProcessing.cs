using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace TCP_Web_Server_Example
{
    class FormProcessing
    {
        public ActionResult BewerbungData(string name, string nickname, string email, int age, string message)
        {
            // Fordere Methode "GetDBConnection" von Klasse "DBUtils", für Objekt "conn" an.
            Console.WriteLine("Getting Connection");    // Gebe Statusstring "Warte auf Verbindungsdaten.." aus.
            MySqlConnection conn = DBUtils.GetDBConnection();

            // Versuche MySQL Datenbank zu verbinden.
            // Wenn erfolgreich bestätigung ausgeben.
            // Wenn Fehler, Ausnahme(exception) auffangen und ausgeben.
            try
            {
                Console.WriteLine("Openning Connection ...");   // Gebe Statusstring "Stelle Verbindung her.." aus.

                conn.Open();

                Console.WriteLine("Connection Successful!");    // Gebe Statusstring "Verbindung erfolgreich!" aus.
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            // Der Befehl Insert.
            string sql = "Insert into basedigitsclan.bewerbungen (Vorname, Username, Email, Age, Nachricht) " + " values ('" + name + "','" + nickname + "','" + email + "','" + age + "','" + message + "');";

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            Console.WriteLine("Command Successful!");


            conn.Close();

            return new EmptyResult();
        }
    }
}
