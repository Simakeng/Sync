using System;
using System.IO;
using System.Text;

namespace Shared.Common.Types
{
    internal class Length
    {
        public static byte[] Pack(int length)
        {
            byte[] res = null;
            if (length < 0x7F)
            {
                res = new byte[] { (byte)length };
            }
            else if (length < 0x3FFF)
            {
                res = BitConverter.GetBytes((ushort)(length | 0x8000));
            }
            else
            {
                res = BitConverter.GetBytes((uint)(length | 0xC0000000));
            }
            return res;
        }

        public static int Unpack(Stream s)
        {
            var buffer = new byte[4];
            s.Read(buffer, 0, 1);
            if ((buffer[0] & 0x80) == 0)
                return buffer[0];
            else if ((buffer[0] & 0x40) == 0)
            {
                s.Read(buffer, 1, 1);
                return BitConverter.ToInt16(buffer) & 0x3FFF;
            }
            else
            {
                s.Read(buffer, 1, 3);
                return BitConverter.ToInt32(buffer) & 0x3FFFFFFF;
            }
        }
    }

    internal class String
    {
        public static byte[] Pack(string s)
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms, Encoding.UTF8);
            sw.Write(Length.Pack(s.Length));
            sw.Write(s);
            return ms.ToArray();
        }

        public static string Unpack(Stream s)
        {
            var len = Length.Unpack(s);
            var buffer = new byte[len];
            s.Read(buffer, 0, len);
            return Encoding.UTF8.GetString(buffer);
        }
    }

    internal class Object
    {
        public static byte[] Pack(object o)
        {
            // if o is string
            if (o.GetType() == typeof(string))
                return String.Pack(o as string);

            // if o has a Pack Function
            var funcPack = o.GetType().GetMethod("Pack");
            if (funcPack != null) 
                return funcPack.Invoke(null, null) as byte[];

            // if o can be convert to byte array by BitConverter
            foreach (var func in typeof(BitConverter).GetMethods()) 
            {
                if (func.Name != "GetBytes")
                    continue;
                var parms = func.GetParameters();
                if (parms.Length != 0 && parms[0].GetType() == o.GetType())
                    return func.Invoke(null, new object[1] { o }) as byte[];
            }

            throw new TypeAccessException("Type <" + o.GetType().FullName + "> Is not Pack able!");
         
        }

        public static string Unpack(Stream s)
        {
            if (s.GetType() == typeof(string))
                return String.Unpack(s);
        }
    }
}