using System;
using Sync.Common.Network;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;
namespace Sync
{
    class SyncHost : SocketHost
    {
        List<Socket> clients = new List<Socket>();
        public SyncHost(string host, int port, int maxclient, int maxcon) : base(host, port, maxcon)
        {
            ConnectionEnstablished += (Socket s) =>
            {
                if (clients.Count > maxclient)
                {
                    // TODO : tell client we are full.
                    s.Close();
                }
                else
                    NewClientConnected(this, s);
            };
        }
        private static void CheckClientVersion(Socket s) 
        {
            
        };
        private static void NewClientConnected(SyncHost host, Socket sclient) 
        {
            host.clients.Add(sclient);
            try
            {
                CheckClientVersion(sclient);
            }
            catch (Exception)
            {
                host.clients.Remove(sclient);
                sclient.Close();
                throw;
            }
            


        }
        public void Deamon() 
        {
            while (!Stoped)
                Thread.Sleep(1);
        }
    }
}