using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSSindicato.Hubs
{
    public class MessageItem
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Message { get; set; }
        public int SourceId{ get; set; }
        public int TargetId { get; set; }
    }
}
