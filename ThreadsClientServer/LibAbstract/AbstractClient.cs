using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Client
{
    abstract public class AbstractClient
    {
        String ip;
        int port;

        public AbstractClient(String ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public void ClientThread()
        {
            Console.WriteLine("start of client");

            IPAddress ipAddr = IPAddress.Parse(ip);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);
            TcpClient tcpClient = new TcpClient();

            tcpClient.Connect(ipEndPoint); // port of server

            /*
             * Send data to server
             */
            SendRequestToServer(tcpClient);
            ReceiveInfromatoinFromServer(tcpClient);
            /*
             * Block send and receive data by this socket
             * and close socket
             */
            tcpClient.Close();


            Console.WriteLine("End of client");
        }

        /// <summary>
        /// Send request for information to server
        /// </summary>
        /// <param name="tcpClient">connecton to server</param>
        public abstract void SendRequestToServer(TcpClient tcpClient);


        /// <summary>
        /// Receive and deserialize information from server
        /// </summary>
        /// <param name="tcpClient">connection to server</param>
        public abstract void ReceiveInfromatoinFromServer(TcpClient tcpClient);
        
    }
}
