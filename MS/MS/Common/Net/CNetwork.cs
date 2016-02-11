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

namespace MS.Common.Net
{
    public class CNetwork
    {
        byte[] buffer = new byte[1024];
        //ArrayReader reader;
        //ArrayWriter writer;
        ushort GV = 62;
        ulong AESKey = 0x130806B41B0F3352;
        //CipherHelper OutCrypto;
        NetClient client = new NetClient();

        public string IP { get; private set; }
        public int Port { get; private set; }

        public CNetwork(string ip, int port)
        {
            IP = ip;
            Port = port;
            //writer = new ArrayWriter();
            //reader = new ArrayReader(buffer);
            //OutCrypto = new CipherHelper(GV, AESKey);
        }

        public void Initialize()
        {
            client.OnConnected += CNetwork_OnConnected;
            client.OnDisconnected += CNetwork_OnDisconnected;
            client.OnReceived += CNetwork_OnReceived;

            if (!client.IsConnected)
            {
                client.Connect(IP, Port);
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
            throw new NotImplementedException();
        }

        private void CNetwork_OnDisconnected(object sender, NetDisconnectedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CNetwork_OnConnected(object sender, NetConnectedEventArgs e)
        {
            //writer.WriteShort(0x0d); // Header
            //writer.WriteShort(0x62);
            //writer.WriteBytes(new byte[] { 0, 0 });
            //byte[] arr = writer.ToArray();

            //client.Send(writer.ToArray());
            
        }

    }
}
