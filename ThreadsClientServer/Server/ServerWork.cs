using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    class ServerWork
    {
        

        /// <summary>
        /// Start server, set configuration
        /// </summary>
        public void StartServer()
        {

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
            /*
             * Create server socket who listen incomming connections (number set in Listen method)
             * When have some connection one create new socket for working (by using method Accept()),
             * but server socket will continue listening for incomming connections
             */
            Socket socketServer = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

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
                socketServer.Bind(ipEndPoint);
                /*
                 * Listen
                 * only for TCP, not need for UDP
                 */
                socketServer.Listen(5);
                Console.WriteLine("Listening....");

                while (true)
                {
                    /*
                     * Receive incoming connection (incomming connection is socket to)
                     * Socket creating while accepting connection
                     * This socket create for work (send and receive), server socket 
                     * continue to listen incomming connections
                     */
                    Socket socketClientConnection = socketServer.Accept();

                    string data = ReceiveRequestFromClient(socketClientConnection);
                    ParseRequestAndSendFromClient(socketClientConnection, data);
                    
                    
                    /*
                     * Block send and receive data by this socket
                     * and close socket
                     */
                    socketClientConnection.Shutdown(SocketShutdown.Both);
                    socketClientConnection.Close();
                }

            }
            catch (Exception)
            {

            }
            finally
            {

            }


            Console.WriteLine("End of server");
        }

        String ReceiveRequestFromClient(Socket socketClientConnection)
        {
            byte[] databytes = new byte[256];
            String data;
            do
            {
                socketClientConnection.Receive(databytes);
                data = Encoding.Unicode.GetString(databytes);
            } while (socketClientConnection.Available > 0); // while have data to read
            Console.WriteLine("Time: {0}, Message - '{1}'", DateTime.Now.ToShortTimeString(), data);

            return (data);
            

        }
        void ParseRequestAndSendFromClient(Socket socketClientConnection, string request)
        {
            if (request.CompareTo(ServerProgram._REQ_TEMP) == 0) { SendInformationToClient(socketClientConnection, "100C"); }
            else if (request.CompareTo(ServerProgram._REQ_DATE) == 0) { SendInformationToClient(socketClientConnection, DateTime.Now.ToLongDateString()); }
            else { SendInformationToClient(socketClientConnection, "ERROR"); }
        }
        void SendInformationToClient(Socket socketClientConnection, string data)
        {
            byte[] buff = Encoding.Unicode.GetBytes(data);
            socketClientConnection.Send(buff);
        }
    }

}
