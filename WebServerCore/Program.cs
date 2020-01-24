using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace WebServerCore
{
    class Program
    {
        static void Main(string[] args)
        {

            var listener = new TcpListener(new IPEndPoint(IPAddress.IPv6Any, 80));
            listener.Server.DualMode = true;

            listener.Start();

            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                TcpClient client = listener.AcceptTcpClient();

                Task.Run( () => 
                {
                    StreamReader sr = new StreamReader(client.GetStream());
                    StreamWriter sw = new StreamWriter(client.GetStream());

                    try
                    {
                        string request = sr.ReadLine();
                        Console.WriteLine(request);
                        string[] tokens = request.Split(' ');
                        string methode = tokens[0];
                        string page = tokens[1];

                        if (page == "/")
                        {
                            page = "/Clan.html";
                        }
                        if (page == "")
                        {

                        }
                        if (methode == "POST")
                        {
                            DateTime localDate = DateTime.Now;
                            string time = localDate.ToString();
                            time = time.Replace('.', '-');
                            time = time.Replace(' ', '_');
                            time = time.Replace(':', '-');
                            StreamWriter clientStream = new StreamWriter(@"../../../Logs/Bewerbung " + time + ".txt");

                            string test = time;

                            //while (test != null)
                            //{
                            //    clientStream.WriteLine(test);
                            //    clientStream.Flush();
                            //    test = sr.ReadLine();
                            //}

                            for (int i = 0; i < 60; i++)
                            {
                                if (test != "")
                                {
                                    clientStream.WriteLine(test);
                                    clientStream.Flush();
                                    test = sr.ReadLine();
                                }
                            }
                            clientStream.Flush();
                            clientStream.Close();

                            page = "/Clan.html";
                        }
                        StreamReader file = new StreamReader("../../../web" + page);
                        sw.WriteLine("HTTP/1.0 200 OK\n");

                        string data = file.ReadLine();
                        while (data != null)
                        {
                            sw.WriteLine(data);
                            sw.Flush();
                            data = file.ReadLine();
                        }

                    }
                    catch (Exception e)
                    {
                        //StreamReader error = new StreamReader("../../../web/Error404.html");
                        //sw.WriteLine("HTTP/1.0 404 OK\n");
                        //string data = error.ReadLine();
                        //while (data != null)
                        //{
                        //    sw.WriteLine(data);
                        //    sw.Flush();
                        //    data = error.ReadLine();
                        //}
                        //sw.Flush();

                        Console.WriteLine("Error: " + e);

                    }
                    client.Close();
                });
            }
        }
    }
}
