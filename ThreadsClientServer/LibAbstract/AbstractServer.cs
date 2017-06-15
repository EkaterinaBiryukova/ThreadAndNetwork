using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;


namespace Server
{
    public abstract class AbstractServer
    {
        private TcpClient insidetcpClient;

        public TcpClient tcpClient
        {
            get
            {
                return insidetcpClient;
            }

            set
            {
                insidetcpClient = value;
            }
        }

        public AbstractServer(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
        }
        /// <summary>
        /// Start server for incomming connection (this is thread)
        /// </summary>
        public async Task StartServer()
        {
            Console.WriteLine("Start " + Thread.CurrentThread.Name);

            object data = await ReceiveRequestFromClientAsync();
            ParseRequestAndPrepareFromClient(data);
            SendInformationToClient();

            tcpClient.Close();

            Console.WriteLine("End " + Thread.CurrentThread.Name);
        }

        /// <summary>
        /// Async receive request from client
        /// </summary>
        /// <returns>Task</returns>
        // Task, async????????
        public abstract Task<object> ReceiveRequestFromClientAsync();

        /// <summary>
        /// Parse request, prepare information for sending
        /// </summary>
        /// <param name="request">object</param>
        // SENDING??????
        public abstract void ParseRequestAndPrepareFromClient(object request);


        /// <summary>
        /// Sending data to client
        /// </summary>
        /// <param name="data"></param>
        public abstract void SendInformationToClient();

        

    }
}
