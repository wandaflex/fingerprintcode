using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace FingerPrint
{
    public class UDPListener : UDPBase
    {
        private IPEndPoint listenOn;

        public UDPListener(IPEndPoint endpoint)
        {
            listenOn = endpoint;
            Client = new UdpClient(listenOn);
        }

        public void Reply(string message, IPEndPoint endpoint)
        {
            var datagram = Encoding.ASCII.GetBytes(message);
            Client.Send(datagram, datagram.Length, endpoint);
        }
    }
}
