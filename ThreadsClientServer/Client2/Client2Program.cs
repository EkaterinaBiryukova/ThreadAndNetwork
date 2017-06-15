using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Client2
{
    class ClientProgram
    {
        public static readonly string _REQ_TEMP = "temperature";
        public static readonly string _REQ_DATE = "date";
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
                    clientThread.Start(_REQ_TEMP);
                }
                else clientThread.Start(_REQ_DATE);
                Thread.Sleep(1000);
            }

            Console.ReadLine();
        }
    }
}
