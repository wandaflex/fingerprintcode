using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint
{

    public struct Received
    {
        public IPEndPoint Sender;
        public string Message;
    }

    public abstract class UDPBase
    {
        protected UdpClient Client;

        public UDPBase()
        {
            Client = new UdpClient();
        }

        public async Task<Received> Receive()
        {
            var result = await Client.ReceiveAsync();
            return new Received()
            {
                Message = Encoding.ASCII.GetString(result.Buffer, 0, result.Buffer.Length),
                Sender = result.RemoteEndPoint
            };
        }

    }
}
