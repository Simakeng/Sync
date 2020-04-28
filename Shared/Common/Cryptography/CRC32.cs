using System;
using System.Collections.Generic;
using System.Text;

namespace Sync.Common.Cryptography
{
    class CRC32
    {
        static uint[] table = InitializeTable();
        static uint[] InitializeTable()
        {
            if (!BitConverter.IsLittleEndian)
                throw new PlatformNotSupportedException("Not supported on Big Endian processors");

            var table = new UInt32[256];
            for (var i = 0; i < 256; i++)
            {
                var entry = (UInt32)i;
                for (var j = 0; j < 8; j++)
                    if ((entry & 1) == 1)
                        entry = (entry >> 1) ^ 0xedb88320u;
                    else
                        entry >>= 1;
                table[i] = entry;
            }
            return table;
        }

        uint _hash = 0;
        public uint hash
        {
            get { return _hash; }
        }

        public CRC32()
        {

        }

        public void Compute(IList<byte> buffer, int start, int size)
        {
            for (var i = start; i < start + size; i++)
                _hash = (hash >> 8) ^ table[buffer[i] ^ hash & 0xff];
        }
        public void Compute(byte[] arr)
        {
            Compute(arr, 0, arr.Length);
        }
        public void Compute(string str)
        {
            Compute(Encoding.UTF8.GetBytes(str));
        }

        public void Compute<T>(T value) 
        {
            foreach (var GetBytes in typeof(BitConverter).GetMethods())
            {
                if (GetBytes.Name != "GetBytes")
                    continue;
                if (GetBytes.GetParameters()[0].ParameterType != typeof(T))
                    continue;
                var bytes = GetBytes.Invoke(null, new object[] { value }) as byte[];
                Compute(bytes);
                return;
            }
            throw new Exception("BitConverter Don't have GetBytes Instance that recive parameter with type " + typeof(T).FullName);
            
        }

        public override string ToString()
        {
            return _hash.ToString("X08");
        }
    }
}