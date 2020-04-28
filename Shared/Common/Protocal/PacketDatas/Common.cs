using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Shared.Common.Protocal.PacketDatas
{
    public class PacketData
    {
        public byte[] Pack<T>(T data) 
        {
            MemoryStream ms = new MemoryStream();
            foreach (var prop in typeof(T).GetProperties()) 
            {
                if (prop.PropertyType == typeof(string)) 
                {
                    string s = prop.GetValue(data) as string;
                    ms.Write(Types.Length.Pack(s.Length));
                    ms.Write(Types.String.Pack(s));
                }
            }
            return ms.ToArray();
        }
        public T UnPack<T>(Stream s) 
        { 

        }
    }
}
