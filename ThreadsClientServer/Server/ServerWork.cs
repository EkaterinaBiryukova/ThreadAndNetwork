using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LibExchange;

namespace Server
{
    /// <summary>
    /// Работа сервера: прием и выдача данных
    /// </summary>
    class ServerWork : AbstractServer
    {
        InformationFromServer output;
        public ServerWork(TcpClient tcpClient) : base(tcpClient)
        {
            // create new object for sending to this client
            output = new InformationFromServer();
        }
        /// <summary>
        /// async Receiving request information from server
        /// </summary>
        /// <returns>String request</returns>
        public override async Task<object>  ReceiveRequestFromClientAsync()
        {
            byte[] buffer = new byte[256]; // MUST BE EXCEPTION
            String data;
            do
            {
                NetworkStream networkStream = base.tcpClient.GetStream();
                await networkStream.ReadAsync(buffer, 0, buffer.Length);
                data = Encoding.Unicode.GetString(buffer);
            } while (tcpClient.Available > 0); // while have data to read
            Console.WriteLine("Time: {0}, Message - '{1}'", DateTime.Now.ToShortTimeString(), data);

            return (data);
        }
        /// <summary>
        /// Parse request from client and prepare information
        /// </summary>
        /// <param name="request">request from client</param>
        public override void ParseRequestAndPrepareFromClient(object request)
        {
            if (request.ToString().CompareTo(ConstForRequest._REQ_TEMP) == 0)
            {
                PrepareTemperature();
            }
            else if (request.ToString().CompareTo(ConstForRequest._REQ_DATE) == 0)
            {
                PrepareData();
            }
            else
            {
                PrepareError();
            }
        }

        void PrepareTemperature()
        {
            output.Str = "100C";
            output.Value = 1;
        }
        void PrepareData()
        {
            output.Str = DateTime.Now.ToLongDateString();
            output.Value = 2;
        }
        void PrepareError()
        {
            output.Str = "ERROR";
            output.Value = 3;
        }

        /// <summary>
        /// Sending information to client as an serialized object
        /// </summary>
        public override void SendInformationToClient()
        {
            NetworkStream networkStream = tcpClient.GetStream();

            // try to send object
            //InformationFromServer output = new InformationFromServer(data, 1);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(networkStream, output);

        }
    }

}
