using System;

using System.Net;
using System.Net.Sockets;

using System.Threading;

namespace Sync.Common.Network
{
    internal class SocketHost
    {
        private Socket server;

        private bool _stop = false;
        private Thread hostThread;

        public SocketHost(string host, int port, int maxcon)
        {
            server = new Socket(SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(IPAddress.Parse(host), port));
            server.Listen(maxcon);

            hostThread = new Thread(() =>
            {
                try
                {
                    while (!_stop)
                    {
                        if (ConnectionEnstablished != null)
                        {
                            var client = server.Accept();
                            Thread clientThread = new Thread(() =>
                            {
                                ConnectionEnstablished.Invoke(client);
                                if (client.Connected)
                                    client.Close();
                            });
                            clientThread.Start();
                        }
                        Thread.Sleep(0);
                    }
                }
                catch
                {
                    this._stop = true;
                    throw;
                }
            });
            hostThread.Start();
        }

        public void Stop()
        {
            _stop = true;
            if (hostThread.IsAlive)
                hostThread.Join();
        }

        public bool Stoped { get { return _stop; } }

        ~SocketHost()
        {
            Stop();
        }

        public Action<Socket> ConnectionEnstablished { get; set; } = null;
    }
}