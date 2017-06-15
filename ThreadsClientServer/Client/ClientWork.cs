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
    class ClientWork : AbstractClient
    {
        String request;
        public ClientWork (String ip, int port, object request) : base (ip, port)
        {
            this.request = request.ToString();
        }

        /// <summary>
        /// Override
        /// </summary>
        /// <param name="tcpClient"></param>
        public override void SendRequestToServer(TcpClient tcpClient)
        {
            byte[] data = Encoding.Unicode.GetBytes(request);
            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// Override
        /// </summary>
        /// <param name="tcpClient"></param>
        public override void ReceiveInfromatoinFromServer(TcpClient tcpClient)
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
