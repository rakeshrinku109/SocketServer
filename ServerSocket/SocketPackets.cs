using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerSocket
{
    public class SocketPackets
    {
        public Socket CurrentSocket { get; set; }
        private byte[] dataBuffer = new byte[1024];

        public byte[] DataBuffer
        {
            get { return dataBuffer; }
        }


    }
}
