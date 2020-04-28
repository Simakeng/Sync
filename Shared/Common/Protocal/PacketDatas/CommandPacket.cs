using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Common.Protocal.PacketData
{
    class ConnectionCommand
    {
        public string ClientVersion { get; set; }
        public string ClientSigniture { get; set; }
        public string ClientPublicKey { get; set; }
    }
}