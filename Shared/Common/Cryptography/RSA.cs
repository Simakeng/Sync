using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
namespace Shared.Common.Cryptography
{
    class RSA
    {
        RSACryptoServiceProvider rsp;
        public RSA() 
        {
            rsp = new RSACryptoServiceProvider();
        }
        public RSA(string xmlstring) : this()
        {
            rsp.FromXmlString(xmlstring);
        }

        private static string ConvertOpenSSLPublicKeyToXMLString(string pkey) 
        {
            throw new NotImplementedException();
        }

        private static string ConvertOpenSSLPrivateKeyToXMLString(string pkey)
        {
            throw new NotImplementedException();
        }

        static RSA FromOpenSSLPublicKeyFile(string path)
        {
            var content = File.ReadAllText(path);
            var xml = ConvertOpenSSLPublicKeyToXMLString(content);
            return new RSA(xml);
        }

        static RSA FromOpenSSLPrivateKeyFile(string path) 
        {
            var content = File.ReadAllText(path);
            var xml = ConvertOpenSSLPrivateKeyToXMLString(content);
            return new RSA(xml);
        }

        public byte[] Encrypt(byte[] rawdata) { return null; }
        public byte[] Decrypt(byte[] rawdata) { return null; }


    }
}
