using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Ecalia.Common.Networking
{
    public class Crypto
    {
        protected AesManaged Aes = new AesManaged();
     

        byte[] key = { 0x13, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0xB4, 0x00, 0x00, 0x00, 0x1B, 0x00, 0x00, 0x00, 0x0F, 0x00, 0x00, 0x00, 0x33, 0x00, 0x00, 0x00, 0x52, 0x00, 0x00, 0x00 };

        public Crypto()
        {
        }

        public byte[] Encrypt (byte[] block)
        {
            ICryptoTransform enc = null;
            Aes.Mode = CipherMode.CBC;
            Aes.Key = key;
            Aes.GenerateIV();
            Console.WriteLine("Key: {0} IV: {1}", CNetwork.ByteArrayToString(Aes.Key), CNetwork.ByteArrayToString(Aes.IV));

            CryptoStream cstream = null;
            MemoryStream mem = null;
            byte[] toEncrypt = null;

            try
            {
                cstream = null;
                mem = new MemoryStream();
                toEncrypt = block;
                enc = Aes.CreateEncryptor();
                cstream = new CryptoStream(mem, enc, CryptoStreamMode.Write);
                cstream.Write(toEncrypt, 0, toEncrypt.Length);
            }
            finally
            {
                if (cstream != null)
                    Aes.Clear();
                cstream.Close();
            }

            Console.WriteLine(CNetwork.ByteArrayToString(mem.ToArray()));

            return mem.ToArray();
        }
    }
}
