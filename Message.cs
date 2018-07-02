using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTotalCommander
{
    [Serializable]
    public class Message
    {
        public string Ip { get; set; }
        public string Control { get; set; }
        public string Type { get; set; }
        public object Argument { get; set; }
        public object Result { get; set; }
        public Message(string ip, string control, string type, object argument, object result)
        {
            Ip = ip;
            Control = control;
            Type = type;
            Argument = argument;
            Result = result;
        }
        public Message()
        { }
    }
}

