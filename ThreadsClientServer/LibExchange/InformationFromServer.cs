﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibExchange
{
    /// <summary>
    /// Serializable
    /// Information geting from server
    /// </summary>
    [Serializable]
    public class InformationFromServer
    {
        private string str;
        private int value;
        public InformationFromServer(string str, int value)
        {
            Str = str;
            Value = value;
        }

        public string Str
        {
            get
            {
                return str;
            }

            set
            {
                str = value;
            }
        }

        public int Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public void Print()
        {
            Console.WriteLine("str - {0}, value - {1}", str, value);
        }
    }
}