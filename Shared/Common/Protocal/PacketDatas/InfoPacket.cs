using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Shared.Common.Protocal.PacketData
{
    public class ClientInfo
    {
        public string Name { get; set; }
        public string MachineName { get; set; }
        public string MachineMacAddress { get; set; }
        public string TimeZone { get; set; }
        public string ClientSigniture { get; set; }
        public string ClientPublicKey { get; set; }

        public byte[] Pack()
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(Types.String.Pack(Name));
            ms.Write(Types.String.Pack(MachineName));
            ms.Write(Types.String.Pack(MachineMacAddress));
            ms.Write(Types.String.Pack(TimeZone));
            return ms.ToArray();
        }
        public static ClientInfo Unpack(Stream s)
        {
            var info = new ClientInfo();
            info.Name = Types.String.Unpack(s);
            info.MachineName = Types.String.Unpack(s);
            info.MachineMacAddress = Types.String.Unpack(s);
            info.TimeZone = Types.String.Unpack(s);
            return info;
        }
    }

}
