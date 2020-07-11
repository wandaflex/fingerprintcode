using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint
{
    public class UDPUser : UDPBase
    {
        public UDPUser()
        {
        }

        public static UDPUser ConnectTo(string hostname, int port)
        {
            var connection = new UDPUser();
            connection.Client.Connect(hostname, port);

            return connection;
        }

        public void Send(string message)
        {
            var datagram = Encoding.ASCII.GetBytes(message);
            Client.Send(datagram, datagram.Length);
        }
    }
}
