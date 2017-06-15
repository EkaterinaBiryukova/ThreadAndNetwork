using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LibExchange;

namespace Client2
{
    class ClientProgram
    {
        static void Main(string[] args)
        {
            Console.Title = "CLIENT2";

            Client2Work client = new Client2Work();

            for (int i = 1; i < 10; i++)
            {
                Thread clientThread = new Thread(client.ClientThread);
                clientThread.Name = i.ToString();
                if (i % 2 == 0)
                {
                    clientThread.Start(ConstForRequest._REQ_TEMP);
                }
                else clientThread.Start(ConstForRequest._REQ_DATE);
                Thread.Sleep(1000);
            }

            Console.ReadLine();
        }
    }
}
