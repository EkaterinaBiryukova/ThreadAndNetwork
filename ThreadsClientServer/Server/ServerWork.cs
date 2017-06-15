using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Server
{
    class ServerWork
    {

        TcpClient tcpClient;
        public ServerWork(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
        }
        /// <summary>
        /// Start server for incomming connection (this is thread)
        /// </summary>
        public void StartServer()
        {
            Console.WriteLine("Start " + Thread.CurrentThread.Name);
            string data = ReceiveRequestFromClient();
            ParseRequestAndSendFromClient(data);

            tcpClient.Close();


            Console.WriteLine("End " + Thread.CurrentThread.Name);
        }

        String ReceiveRequestFromClient()
        {
            byte[] buffer = new byte[256]; // MUST BE EXCEPTION
            String data;
            do
            {
                NetworkStream networkStream = tcpClient.GetStream();
                networkStream.Read(buffer, 0, buffer.Length);
                data = Encoding.Unicode.GetString(buffer);
                Console.WriteLine("(1): Time: {0}, Message - '{1}'", DateTime.Now.ToShortTimeString(), data);
            } while (tcpClient.Available > 0); // while have data to read
            Console.WriteLine("(2): Time: {0}, Message - '{1}'", DateTime.Now.ToShortTimeString(), data);

            return (data);
            

        }
        void ParseRequestAndSendFromClient(string request)
        {
            if (request.CompareTo(ServerProgram._REQ_TEMP) == 0) { SendInformationToClient("100C"); }
            else if (request.CompareTo(ServerProgram._REQ_DATE) == 0) { SendInformationToClient(DateTime.Now.ToLongDateString()); }
            else { SendInformationToClient( "ERROR"); }
        }
        void SendInformationToClient(string data)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(data);
            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.Write(buffer, 0, buffer.Length);
        }
    }

}
