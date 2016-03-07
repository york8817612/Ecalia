using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NetSockets;
using System.Windows.Forms;
using System.IO;
using DarkMapleLib;
using DarkMapleLib.Helpers;
using System.Security.Cryptography;

namespace Ecalia.Common.Networking
{
    public partial class CNetwork : NetBaseClient<byte[]>
    {
        AesManaged aes = new AesManaged();
        byte[] buffer = new byte[1024]; // This really isnt used in the end of the day...just need to initialize the reader
        ArrayReader reader;
        ArrayWriter writer;
        byte[] key = { 0x13, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0xB4, 0x00, 0x00, 0x00, 0x1B, 0x00, 0x00, 0x00, 0x0F, 0x00, 0x00, 0x00, 0x33, 0x00, 0x00, 0x00, 0x52, 0x00, 0x00, 0x00 };
        //ulong AESKey = 0x130806B41B0F3352;
        NetObjectClient client;
        

        public string IP { get; private set; }
        public int Port { get; private set; }

        public CNetwork(string ip, int port)
        {
            client = new NetObjectClient();
            IP = ip;
            Port = port;
            writer = new ArrayWriter();
            reader = new ArrayReader(buffer);
        }

        public void Initialize()
        {
            if (TryConnect(IP, Port))
            {
                OnConnected += CNetwork_OnConnected;
                OnDisconnected += CNetwork_OnDisconnected;
                OnReceived += CNetwork_OnReceived;
            }
            else
            {
                MessageBox.Show("Unable to connect to Login Server. Please check the server website for any information regarding this issue.", "Unable to connect", MessageBoxButtons.OK, MessageBoxIcon.None);
                Environment.Exit(0);
                return;
            }
        }

        private void CNetwork_OnReceived(object sender, NetReceivedEventArgs<byte[]> e)
        {
            reader = new ArrayReader(e.Data, e.Data.Length);
            FROM_SERVER header = (FROM_SERVER)reader.ReadShort();

            switch (header)
            {
                case FROM_SERVER.LOGIN_STATUS:
                    break;
                case FROM_SERVER.CLIENT_HELLO:
                    var version = reader.ReadShort();
                    var patch = reader.ReadMapleString();

                    if (version != (short)GameConstants.MAJOR_VERSION && patch != GameConstants.MINOR_VERSION)
                    {
                        Disconnect(NetStoppedReason.Exception);
                        Environment.Exit(0);
                    }
                    break;
                default:
                    Console.WriteLine("[Unhandled] Packet received: {0}", ByteArrayToString(e.Data));
                    break;
            }
        }

        private void CNetwork_OnDisconnected(object sender, NetDisconnectedEventArgs e)
        {
            Disconnect(NetStoppedReason.Manually);
        }

        private void CNetwork_OnConnected(object sender, NetConnectedEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }

        private byte[] SendHandShake()
        {
            byte[] result;

            writer.WriteShort(14);
            writer.WriteShort((short)GameConstants.MAJOR_VERSION);
            writer.WriteMapleString(GameConstants.MINOR_VERSION);
            writer.WriteByte(0);
            writer.WriteByte(0);

            result = writer.ToArray();

            return result;
        }

        protected override NetBaseStream<byte[]> CreateStream(NetworkStream ns, EndPoint ep)
        {
            return new NetStream(ns, ep);
        }

        public static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", " ");
        }
    }
}
