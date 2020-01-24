using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MySQL_Example
{
    class Program
    {
        static void Main(string[] args)
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
            string sql = "Insert into parts.ram (Manufacturer, Model, RAM_Speed, Memory_Size, Memory_Type, Buffer, ECC, Model_Number) " + " values ('" + "Test" + "','" + "Test" + "','" + "1600" + "','" + "16" + "','" + "DDR3" + "','" + "255" + "','" + "ECC" + "','" + "Test" + "');";

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            Console.WriteLine("Command Successful!");


            conn.Close();

            //Beende Programm mit jeder Taste.
            Console.ReadKey();
        }
    }
}
