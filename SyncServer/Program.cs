using System;
using Sync.Common.Network;
using System.Threading;
using System.Net.Sockets;
using Sync.Common.Cryptography;
namespace Sync
{
    class Program
    {
        static void Main(string[] args)
        {
            CRC32 crc = new CRC32();
            crc.Compute("nmsl");

            Console.WriteLine(crc.ToString());
            SocketHost host = new SocketHost("0.0.0.0", 8686, 10);

            host.ConnectionEnstablished += (Socket client) =>
            {
                Console.WriteLine("From : " + client.RemoteEndPoint.ToString());
            };

            while (!host.Stoped)
            {
                Thread.Sleep(1);
            }
            
        }
    }
}
