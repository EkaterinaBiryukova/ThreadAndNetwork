using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using LibExchange;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client
{
    class ClientWork
    {
        
        /// <summary>
        /// Thread method
        /// Create socket to server and send some message
        /// </summary>
        public void ClientThread(object request)
        {
            Console.WriteLine("start of client");
            
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 8005);
            TcpClient tcpClient = new TcpClient();
            
            tcpClient.Connect(ipEndPoint); // port of server

            /*
             * Send data to server
             */
            SendRequestToServer(tcpClient, request.ToString());
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
        /// <param name="request">request</param>
        void SendRequestToServer(TcpClient tcpClient, String request)
        {
            byte[] data = Encoding.Unicode.GetBytes(request);
            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// Receive and deserialize information from server
        /// </summary>
        /// <param name="tcpClient">connection to server</param>
        void ReceiveInfromatoinFromServer(TcpClient tcpClient)
        {
            InformationFromServer input;
            do
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                input = (InformationFromServer)binaryFormatter.Deserialize(tcpClient.GetStream());
            } while (tcpClient.Available > 0);

            input.Print();
        }
    }

}
