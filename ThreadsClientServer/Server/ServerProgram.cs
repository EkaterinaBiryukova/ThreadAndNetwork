using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;


namespace Server
{
    class ServerProgram
    {
        public static readonly string _REQ_TEMP = "temperature";
        public static readonly string _REQ_DATE = "date";
        static void Main(string[] args)
        {
            Console.Title = "SERVER";

            //Sockets
            //ServerWork serverWork = new ServerWork();
            //serverWork.StartServer();

            //MultiThreadServer

            IPHostEntry ipHostEntry = Dns.GetHostEntry("127.0.0.1"); // take IP of localhost
            IPAddress ipAddr = ipHostEntry.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 8005); // port of server

            TcpListener tcpListener = new TcpListener(ipEndPoint);
            int i = 1;
            while (true)
            {
                try
                {
                    tcpListener.Start(10);
                    Console.WriteLine("Listening....");

                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    //get incoming connection, start server thread for this connection
                    ServerWork serverWork = new ServerWork(tcpClient);
                    Thread serverThread = new Thread(serverWork.StartServer);
                    serverThread.Name = "Server N " + i.ToString();
                    serverThread.Start();
                    i++;
                }
                catch (SocketException)
                {
                    Console.WriteLine("Get SocketException");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Get ArgumentOutOfRangeException");
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Get InvalidOperationException");
                }
                finally
                {
                    Console.WriteLine("Do smth finally");
                }

            }

        }
    }
}
