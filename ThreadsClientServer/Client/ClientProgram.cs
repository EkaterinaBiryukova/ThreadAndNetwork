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
            ClientWork clientWork = new ClientWork(10, 10.1, new DateTime());
            new Thread(clientWork.ClientWork__1).Start();


            //SOCKET
            IPAddress ipAddr = IPAddress.Parse("120.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(ipEndPoint); // port of server

            // Send-Receive data
            String str = "Hi, Im client";
            byte[] data = Encoding.Unicode.GetBytes(str);

            socket.Send(data);
            /*
             * Block send and receive data by this socket
             * and close socket
             */
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            Console.ReadLine();
        }
    }
}
