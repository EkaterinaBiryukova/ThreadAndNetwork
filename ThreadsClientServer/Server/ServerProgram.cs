using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Server
{
    class ServerProgram
    {
        public static readonly string _REQ_TEMP = "temperature";
        public static readonly string _REQ_DATE = "date";
        static void Main(string[] args)
        {
            Console.Title = "SERVER";

            //Sockets
            ServerWork serverWork = new ServerWork();
            serverWork.StartServer();
            
            Console.ReadLine();
        }
    }
}
