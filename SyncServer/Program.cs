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
            SyncHost host = new SyncHost("0.0.0.0", 8686, 10, 10);

            host.Deamon();
        }
    }
}
