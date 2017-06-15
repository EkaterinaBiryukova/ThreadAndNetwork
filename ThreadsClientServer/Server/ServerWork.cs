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
        public async Task StartServer()
        {
            Console.WriteLine("Start " + Thread.CurrentThread.Name);
            string data = await ReceiveRequestFromClient();
            await ParseRequestAndSendFromClient(data);

            tcpClient.Close();

            Console.WriteLine("End " + Thread.CurrentThread.Name);
        }

        async Task<String>  ReceiveRequestFromClient()
        {
            byte[] buffer = new byte[256]; // MUST BE EXCEPTION
            String data;
            do
            {
                NetworkStream networkStream = tcpClient.GetStream();
                await networkStream.ReadAsync(buffer, 0, buffer.Length);
                data = Encoding.Unicode.GetString(buffer);
                Console.WriteLine("(1): Time: {0}, Message - '{1}'", DateTime.Now.ToShortTimeString(), data);
            } while (tcpClient.Available > 0); // while have data to read
            Console.WriteLine("(2): Time: {0}, Message - '{1}'", DateTime.Now.ToShortTimeString(), data);

            return (data);
            

        }
        async Task ParseRequestAndSendFromClient(string request)
        {
            if (request.CompareTo(ServerProgram._REQ_TEMP) == 0) { await SendInformationToClient("100C"); }
            else if (request.CompareTo(ServerProgram._REQ_DATE) == 0) {await SendInformationToClient(DateTime.Now.ToLongDateString()); }
            else { await SendInformationToClient( "ERROR"); }
        }
        async Task SendInformationToClient(string data)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(data);
            NetworkStream networkStream = tcpClient.GetStream();
            await networkStream.WriteAsync(buffer, 0, buffer.Length);
        }
    }

}
