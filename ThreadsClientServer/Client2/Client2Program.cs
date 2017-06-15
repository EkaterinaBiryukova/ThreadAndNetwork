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
        static void Main(string[] args)
        {
            Console.Title = "CLIENT2";

            Client2Work client = new Client2Work();

            Thread clientThread = new Thread(client.ClientThread);
            clientThread.Name = "Client2";
            clientThread.Start();


            Console.ReadLine();
        }
    }
}
