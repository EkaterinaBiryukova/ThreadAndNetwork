using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LibExchange;


namespace Client
{
    class ClientProgram
    {
        static void Main(string[] args)
        {
            Console.Title = "CLIENT";

            for (int i = 0; i< 5; i++)
            {
                string request;
                if (i % 2 == 0) request = ConstForRequest._REQ_DATE;
                else request = ConstForRequest._REQ_TEMP;
                ClientWork client = new ClientWork("127.0.0.1", 8005, request);

                ThreadPool.QueueUserWorkItem(new WaitCallback ( (s) => { client.ClientThread(); }));
            }

            Console.ReadLine();
        }
    }
}
