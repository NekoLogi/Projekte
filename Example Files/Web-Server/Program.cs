using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TCP_Web_Server_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Höre diesen port nach anfragen ab.
            var listener = new TcpListener(new IPEndPoint(IPAddress.IPv6Any, 80));  // Erstelle einen IPv4/IPv6-Listener für port [80].
            listener.Server.DualMode = true;    // Der bool wert gibt an ob beide IPAddress Versionen IPv4 und IPv6 verwendet werden sollen.
                                                // "true" = IPv4/IPv6 | "false" = IPv6 Only.
            listener.Start();   // Starte Listener.

            while (true)
            {
                Console.WriteLine("Waiting for a connection");      // Schreibe "Waiting for a connection" in die Konsole.
                TcpClient client = listener.AcceptTcpClient();      // Aktzeptiere TCP-Client und stelle Verbindung her.

                StreamReader sr = new StreamReader(client.GetStream());     // Erstelle einen "StreamReader" als Objekt "sr" um Empfangene Nachrichten zu lesen.
                StreamWriter sw = new StreamWriter(client.GetStream());     // Erstelle einen "StreamWriter" als Objekt "sw" um Nachrichten zu senden.
                try
                {
                    //Client Anfrage.
                    string request = sr.ReadLine();             // Erstelle String "request" und füge Empfangene Nachricht(Stream) in "request" ein.
                    Console.WriteLine(request);                 // Schreibe String von "request" in Konsole.
                    string[] tokens = request.Split(' ');       // Erstelle einen String-Array als Variable "tokens" und trenne Wörter(Strings) mit Leerzeichen.
                    string page = tokens[1];                    // Erstelle String "page" und füge Array[1] von "tokens" in "page" ein.
                    string methode = tokens[0];                 // Erstelle String "methode" und füge Array[0] von "tokens" in "methode" ein.
                    string name = "";
                    string username = "";
                    string email = "";
                    string age = "";
                    string message = "";

                    // Prüfe ob nach Hauptseite gefragt wurde.
                    if (page == "/")
                    {
                        page = "/Clan.html";   // Füge String (HTML-Dateiname) "Clan.html" in "page" ein.
                    }
                    if (methode == "POST")
                    {
                        FormHandler(name, username, email, age, message);
                        page = "/Clan_Beitreten.html";
                    }
                    // Finde die Datei.
                    StreamReader file = new StreamReader("../../web" + page);   // Erstelle einen "StreamReader" als Objekt "file" und suche nach Datei unter diesem Verzeichnis. 
                    sw.WriteLine("HTTP/1.0 200 OK\n");                           // Gebe aus das die Anfrage Erfolgreich war.

                    // Sende die Datei.
                    string data = file.ReadLine();  // Erstelle String "data", lese "file" (gefundene Datei) aus und füge sie in "data" ein.
                    while (data != null)
                    {
                        sw.WriteLine(data);         // Schicke Client die Angeforderte Datei.
                        sw.Flush();                 // Lösche Stream Puffer.
                        data = file.ReadLine();     // Lese "file"(gefundene Datei) aus und füge sie in "data" ein.
                    }
                }
                catch (Exception e)
                {
                    // Finde die Datei.
                    StreamReader file = new StreamReader("../../web/Error404.html");   // Erstelle einen "StreamReader" als Objekt "file" und suche nach Datei unter diesem Verzeichnis. 
                    sw.WriteLine("HTTP/1.0 404 OK\n");                           // Gebe aus das die Anfrage Erfolgreich war.

                    // Sende die Datei.
                    string data = file.ReadLine();  // Erstelle String "data", lese "file" (gefundene Datei) aus und füge sie in "data" ein.
                    while (data != null)
                    {
                        sw.WriteLine(data);         // Schicke Client die Angeforderte Datei.
                        sw.Flush();                 // Lösche Stream Puffer.
                        data = file.ReadLine();     // Lese "file"(gefundene Datei) aus und füge sie in "data" ein.
                    }
                    sw.Flush();     // Lösche Stream Puffer.

                    Console.WriteLine("Error: " + e);
                    DateTime localDate = DateTime.Now;
                    string time = localDate.ToString();

                    time = time.Replace('.', '-');
                    time = time.Replace(' ', '_');
                    time = time.Replace(':', '-');
                    StreamWriter errorLog = new StreamWriter(@"../../Logs/ErrorLog " + time + ".txt");
                    errorLog.Write(e);
                    errorLog.Close();
                }
                client.Close();     // Schließe Verbindung.

                void FormHandler(string name, string username, string email, string age, string message)
                {
                    string test;

                    for (int i = 0; i < 17; i++) {
                        test = sr.ReadLine();
                        Console.WriteLine(test);

                        if (i == 16) {
                            name = test;
                            for (int e = 0; e < 4; e++) {
                                test = sr.ReadLine();
                                Console.WriteLine(test);
                            }
                            username = test;
                            for (int e = 0; e < 4; e++) {
                                test = sr.ReadLine();
                                Console.WriteLine(test);
                            }
                            email = test;
                            for (int e = 0; e < 4; e++) {
                                test = sr.ReadLine();
                                Console.WriteLine(test);
                            }
                            age = test;
                            for (int e = 0; e < 4; e++) {
                                test = sr.ReadLine();
                                Console.WriteLine(test);
                            }
                            message = test;
                        }
                    }
                    new Thread(() =>
                    {
                        SQLInsert(name, username, email, age, message);
                    }).Start();
                }

                void SQLInsert(string name, string username, string email, string age, string message)
                {
                    // Fordere Methode "GetDBConnection" von Klasse "DBUtils", für Objekt "conn" an.
                    Console.WriteLine("////////////////////////////////");
                    Console.WriteLine("///");
                    Console.WriteLine("Getting Connection");    // Gebe Statusstring "Warte auf Verbindungsdaten.." aus.
                    MySqlConnection conn = DBUtils.GetDBConnection();

                    // Versuche MySQL Datenbank zu verbinden.
                    // Wenn erfolgreich bestätigung ausgeben.
                    // Wenn Fehler, Ausnahme(exception) auffangen und ausgeben.
                    try
                    {
                        Console.WriteLine("///");
                        Console.WriteLine("Openning Connection ...");   // Gebe Statusstring "Stelle Verbindung her.." aus.

                        conn.Open();

                        Console.WriteLine("///");
                        Console.WriteLine("Connection Successful!");    // Gebe Statusstring "Verbindung erfolgreich!" aus.

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("///");
                        Console.WriteLine("Error: " + e.Message);
                        Console.WriteLine("///");
                        Console.WriteLine("////////////////////////////////");

                    }

                    // Der Befehl Insert.
                    string sql = "Insert into basedigitsclan.bewerbungen (Vorname, Username, Email, Age, Nachricht) " + " values ('" + name + "','" + username + "','" + email + "','" + age + "','" + message + "');";

                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("///");
                    Console.WriteLine("Command Successful!");
                    Console.WriteLine("///");
                    Console.WriteLine("////////////////////////////////");

                    conn.Close();
                    return;

                }
            }

        }
    }
}
