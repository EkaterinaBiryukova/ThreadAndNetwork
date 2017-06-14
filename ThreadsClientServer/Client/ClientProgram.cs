using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace Client
{
    class ClientProgram
    {
        static void Main(string[] args)
        {
            Console.Title = "CLIENT";
            //SOCKET
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 8005);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

  
            socket.Connect(ipEndPoint); // port of server

            // Send-Receive data
            String str = "Hi, Im client";
            byte[] data = Encoding.Unicode.GetBytes(str);

            Console.WriteLine("Send - {0}", socket.Send(data));
            /*
             * Block send and receive data by this socket
             * and close socket
             */
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();


            Console.WriteLine("End of client");
            Console.ReadLine();
        }
    }
}
