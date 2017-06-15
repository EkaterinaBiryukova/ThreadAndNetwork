﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Client
{
    class ClientProgram
    {
        public static readonly string _REQ_TEMP = "temperature";
        public static readonly string _REQ_DATE = "date";
        static void Main(string[] args)
        {
            Console.Title = "CLIENT";

            ClientWork client = new ClientWork();

            for (int i =1; i<5;i++)
            {
                Thread clientThread = new Thread(client.ClientThread);
                clientThread.Name = i.ToString();
                if (i%2 == 0)
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
