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
            /*
             * HowTo make multiparam thread:
             * If need to make thread with param use new Thread (new ParameterizedThreadStart(<nameMethod>))
             * signature of <nameMethod>: void <nameMethod> (object param)
             * <nameMethod> can take only one param type object
             * so we can use some class as param and make modification of types in <nameMethod>
             * BAD WAY - types unsafty
             * GOOD WAY:
             * Make class with field - params
             * Constructor of this class is metod to set fields
             * While making new object of the class one set the values of params
             * <nameMethod> uses fields of its class like simple method, without modification of types
             * Using new object make thread from <nameMethod> in main thread
             */
            ServerWork serverWork = new ServerWork(95, 10.1, new DateTime());
            new Thread(serverWork.ServerWork__1).Start();

            //Sockets

            /*
             * IPHostEntry - info about computer-host, AddressList for many IP of one computer
             * Dns.GetHostEntry - get information about host by name or address
             * GetHostEntry - get IP(IPs) by host name and but this IP(IPs) in ArrayList
             */
            IPHostEntry ipHostEntry = Dns.GetHostEntry("localhost"); // take IP of localhost
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
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000); // port of server

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

                while (true)
                {
                    /*
                     * Receive incoming connection (incomming connection is socket to)
                     */
                    Socket tmp = socket.Accept();

                    //Send-Receive data
                    byte[] databytes = new byte[256];
                    String data;
                    tmp.Receive(databytes);
                    data = Encoding.Unicode.GetString(databytes);
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
            


            Console.ReadLine();
        }
    }
}
