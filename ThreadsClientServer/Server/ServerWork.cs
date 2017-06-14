using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ServerWork
    {
        int intParam;
        double doublParam;
        DateTime datetineParam;
        public ServerWork(int val1, double val2, DateTime val3)
        {
            intParam = val1;
            doublParam = val2;
            datetineParam = val3;
        }

        /// <summary>
        /// Thread method
        /// Print date and double value while int value (set in cunstructor) < 100 
        /// </summary>
        public void ServerWork__1()
        {

            while (intParam < 100)
            {
                intParam++;
                Console.WriteLine("date - {0}, double val - {1}", datetineParam.DayOfWeek.ToString(), doublParam);
            }
        }
    }

}
