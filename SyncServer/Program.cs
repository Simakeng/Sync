using System;
using Sync.Common.Network;
using System.Threading;
using System.Net.Sockets;

namespace Sync
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketHost host = new SocketHost("0.0.0.0", 8686, 10);

            host.ConnectionEnstablished += (Socket client) =>
            {
                Console.WriteLine("From : " + client.RemoteEndPoint.ToString());
            };

            while (!host.Stoped)
                Thread.Sleep(0);
            
        }
    }
}
