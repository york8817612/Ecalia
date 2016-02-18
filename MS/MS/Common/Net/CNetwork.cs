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

namespace MS.Common.Net
{
    public class CNetwork : NetBaseClient<byte[]>
    {
        AesManaged aes = new AesManaged();
        byte[] buffer = new byte[1024];
        ArrayReader reader;
        ArrayWriter writer;
        ushort GV = 62;
        ulong AESKey = 0x130806B41B0F3352;
        NetObjectClient client;
        Cipher cry;
        

        public string IP { get; private set; }
        public int Port { get; private set; }

        public CNetwork(string ip, int port)
        {
            client = new NetObjectClient();
            IP = ip;
            Port = port;
            writer = new ArrayWriter();
            reader = new ArrayReader(buffer);
            cry = new Cipher(GV, AESKey);
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
            // Server send = our recv || server recv = our send
            Console.WriteLine(ByteArrayToString(e.Data));
            reader = new ArrayReader(e.Data, e.Data.Length);
            short header = reader.ReadShort();
            byte[] check;

            switch (header)
            {
                case 0x0D:
                    Console.WriteLine("Hello");
                    writer.WriteShort(0x01);
                    writer.WriteMapleString("admin");
                    writer.WriteMapleString("admin");
                    check = writer.ToArray();
                    Console.WriteLine(BitConverter.ToString(check));
                    cry.Encrypt(ref check, false);
                    Send(check);
                    break;
            }
        }

        private void CNetwork_OnDisconnected(object sender, NetDisconnectedEventArgs e)
        {
            Disconnect(NetStoppedReason.Manually);
        }

        private void CNetwork_OnConnected(object sender, NetConnectedEventArgs e)
        {
            //Send(SendHandShake());
            //NetUtility.Ping(IP, Port, TimeSpan.FromSeconds(10));
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

            cry.Encrypt(ref result, false);
            cry.SetIV(0);

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
