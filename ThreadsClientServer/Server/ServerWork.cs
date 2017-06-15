﻿using System;
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
            ParseRequestAndSendFromClient(data);

            tcpClient.Close();

            Console.WriteLine("End " + Thread.CurrentThread.Name);
        }
        /// <summary>
        /// async Receiving request information from server
        /// </summary>
        /// <returns>String request</returns>
        async Task<String>  ReceiveRequestFromClient()
        {
            byte[] buffer = new byte[256]; // MUST BE EXCEPTION
            String data;
            do
            {
                NetworkStream networkStream = tcpClient.GetStream();
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
        void ParseRequestAndSendFromClient(string request)
        {
            if (request.CompareTo(ConstForRequest._REQ_TEMP) == 0) {  SendInformationToClient("100C"); }
            else if (request.CompareTo(ConstForRequest._REQ_DATE) == 0) {SendInformationToClient(DateTime.Now.ToLongDateString()); }
            else { SendInformationToClient( "ERROR"); }
        }
        /// <summary>
        /// Sending information to client as an serialized object
        /// </summary>
        /// <param name="data">information for sending</param>
        void SendInformationToClient(string data)
        {
            NetworkStream networkStream = tcpClient.GetStream();

            // try to send object
            InformationFromServer output = new InformationFromServer(data, 1);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(networkStream, output);

        }
    }

}
