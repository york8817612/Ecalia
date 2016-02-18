using DarkMapleLib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Common.Net
{
    public class Message 
    {
        static CNetwork net = new CNetwork(GameConstants.IP, GameConstants.LOGIN_PORT);
        static ArrayWriter writer;

        public Message()
        {
            writer = new ArrayWriter();
        }

        public static void OnMessageReceived(short header)
        {
            Recv msg = (Recv)header;
            writer = new ArrayWriter();

            switch (msg)
            {
                case Recv.CLIENT_HELLO:
                    Console.WriteLine("Hello");
                    writer.WriteShort(0x01);
                    writer.WriteBytes(new byte[] { 0, 0, 0, 0, 0, 0, 0xFF, 0x6A, 1, 0, 0, 0, 0x4E });
                    net.Send(writer.ToArray());
                    break;
                default:
                    Console.WriteLine("[Unhandled Header] {0}", header);
                    break;
            }
        }

        public static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", " ");
        }
    }

    public enum Recv
    {
        CLIENT_HELLO = 0x0D
    };

    public enum Send
    {

    };
}
