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
        static void Main(string[] args)
        {
            Console.Title = "SERVER";

            //Sockets

            /*
             * IPHostEntry - info about computer-host, AddressList for many IP of one computer
             * Dns.GetHostEntry - get information about host by name or address
             * GetHostEntry - get IP(IPs) by host name and but this IP(IPs) in ArrayList
             */
            IPHostEntry ipHostEntry = Dns.GetHostEntry("127.0.0.1"); // take IP of localhost
            IPAddress ipAddr = ipHostEntry.AddressList[0];
            // OR
            /* IPAddress - class ip-address
             * If know IP can use IPAddress without IPHostEntry
             */
            ///<example>
            /// IPAddress ipAddr = IPAddress.Parse("127.0.0.1"); // set ip-addr by parsing string
            /// ipAddr = IPAddress.Loopback; // the same as 127.0.0.1
            ///</example>

            /*
             * Build end_point (ip+port of the remote host, here - client)
             * IPEndPoint - subclass of abstract class EndPoint
             * For IP-working use IPEndPoint
             * Сокет будет прослушивать подключения по 8005 порту 
             * на локальном адресе 127.0.0.1. 
             * То есть клиент должен будет 
             * подключаться к локальному адресу и порту 8005.
             */
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 8005); // port of server

            // NUZEN LI PERVIJ PARAM!!!
            Socket socket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                /*
                 * Set up point to listen
                 *  by signature of Bind parametr is of type EndPoint, 
                 *  but EndPoint is abstract, so one should use
                 *  some subclass of EndPoint
                 *  look at msdn site
                 *  EndPoint has 2 subclasses - IPEndPoint and DnsEndPoint
                 */
                socket.Bind(ipEndPoint); 
                /*
                 * Listen
                 * only for TCP, not need for UDP
                 */
                socket.Listen(5);
                Console.WriteLine("Сервер запущен. Ожидание подключений...");
                Console.WriteLine("Listening....");

                while (true)
                {
                    /*
                     * Receive incoming connection (incomming connection is socket to)
                     */
                    Socket tmp = socket.Accept();

                    
                    //Send-Receive data
                    byte[] databytes = new byte[256];
                    String data;

                    do
                    {
                        tmp.Receive(databytes);
                        data = Encoding.Unicode.GetString(databytes);
                    } while (tmp.Available > 0); // while have data to read
                    
                    Console.WriteLine("data - '{0}'", data);
                    /*
                     * Block send and receive data by this socket
                     * and close socket
                     */
                    tmp.Shutdown(SocketShutdown.Both);
                    tmp.Close();
                }
                
            }
            catch (Exception)
            {

            }
            finally
            {

            }


            Console.WriteLine("End of server");
            Console.ReadLine();
        }
    }
}
