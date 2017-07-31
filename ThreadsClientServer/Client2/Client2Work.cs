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

        /// <summary>
        /// Thread method
        /// Create socket to server and send some message
        /// </summary>
        public void ClientThread(object request)
        {
            Console.WriteLine("start of client");
            //SOCKET
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 8005);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            socket.Connect(ipEndPoint); // port of server

            /*
             * Send data to server
             */
            SendRequestToServer(socket, request.ToString());
            ReceiveInfromatoinFromServer(socket);
            /*
             * Block send and receive data by this socket
             * and close socket
             */
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();


            Console.WriteLine("End of client");
        }
        void SendRequestToServer(Socket socket, String request)
        {
            byte[] data = Encoding.Unicode.GetBytes(request);
            socket.Send(data);
        }
        void ReceiveInfromatoinFromServer(Socket socket)
        {
            byte[] buff = new byte[256];
            String data;
            do
            {
                socket.Receive(buff);
                data = Encoding.Unicode.GetString(buff);
            } while (socket.Available > 0);
            Console.WriteLine("For client {0} get {1}", Thread.CurrentThread.Name, data);

        }
    }

}
