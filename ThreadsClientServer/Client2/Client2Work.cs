using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Client2
{
    class Client2Work
    {
        const string _REQ_TEMP = "temperature";
        const string _REQ_DATE = "date";
        /// <summary>
        /// Thread method
        /// Create socket to server and send some message
        /// </summary>
        public void ClientThread()
        {
            //SOCKET
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 8005);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            socket.Connect(ipEndPoint); // port of server

            /*
             * Send data to server
             */
            String str = "Hi, Im client_" + Thread.CurrentThread.Name;
            byte[] data = Encoding.Unicode.GetBytes(str);
            socket.Send(data);
            /*
             * Block send and receive data by this socket
             * and close socket
             */
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();


            Console.WriteLine("End of client");
        }
    }

}
