using Sync.Common.Cryptography;
using Sync.Common.Protocal;
using System;
using System.IO;

namespace Sync.Common.Network
{
    /*
     * ------------------------------------
     * | SYNC | TYPE | LEN | DATA | CRC32 |
     * ------------------------------------
     *
     * |------------------------------------------------------------------------------|
     * | name  | desc                               | length  | note                  |
     * |-------|------------------------------------|---------|-----------------------|
     * | SYNC  | sync header                        | 4byte   | value fixed to "SYNC" |
     * | PCID  | id of this packet                  | 4byte   |                       |
     * | TYPE  | type of this packet                | 1byte   |                       |
     * | LEN   | length of DATA segement            | 1-4byte | see below             |
     * | DATA  | packet data                        | ....... |                       |
     * | CRC32 | crc32 cheksum of the entire packet | 4byte   |                       |
     * |------------------------------------------------------------------------------|
     *
     * When data length not greater than 127, the LEN is 1byte.
     * When data length greater than 127, the LEN is 2byte.
     * First bit of LEN is 1,the rest is the actual length.
     */

    class Packet
    {
        public Packet() { }

        private int _type = -1;
        protected PacketType packetType
        {
            get
            {
                return (PacketType)_type;
            }
            set
            {
                _type = (int)value;
            }
        }

        static Random rand = new Random();
        static uint NewPacketID()
        {
            byte[] buf = new byte[4];
            rand.NextBytes(buf);
            return BitConverter.ToUInt32(buf, 0);
        }
        public uint packetID { get; } = NewPacketID();

        MemoryStream datas = new MemoryStream();

        public void AppendData(byte[] data)
        {
            datas.Write(data, 0, data.Length);
        }

        public byte[] ToBytes()
        {
            CRC32 crc32 = new CRC32();
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            const string magic = "SYNC"; // magic!

            sw.Write(magic);
            crc32.Compute(magic);

            sw.Write(packetID);
            crc32.Compute(packetID);

            sw.Write(_type);
            crc32.Compute(_type);

            var len = datas.Length;
            if (len <= 127)
            {
                sw.Write((char)len);
                crc32.Compute(new byte[] { (byte)len });
            }
            else
            {
                sw.Write((ushort)len & 0x8000);
                crc32.Compute((ushort)len & 0x8000);
            }

            datas.Seek(0, SeekOrigin.Begin);
            datas.CopyTo(ms);
            crc32.Compute(datas.ToArray());

            sw.Write(crc32.hash);
            return ms.ToArray();
        }
    }

    class InfoPacket : Packet
    {
        InfoPacket() 
        { 
            packetType = PacketType.Info; 
        }
    }
}