using DarkMapleLib.Helpers;
using NetSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSServer
{
    class Program
    {
        private static NetServer server = new NetServer();
        private static ArrayWriter writer = new ArrayWriter();
        private static Array reader;

        static void Main(string[] args)
        {
            server.EchoMode = NetEchoMode.EchoAll;
            server.OnClientAccepted += Server_OnClientAccepted;
            server.OnClientConnected += Server_OnClientConnected;
            server.OnClientDisconnected += Server_OnClientDisconnected;
            server.OnClientRejected += Server_OnClientRejected;
            server.OnError += Server_OnError;
            server.OnReceived += Server_OnReceived;
            server.OnStarted += Server_OnStarted;
            server.OnStopped += Server_OnStopped;
            
            server.Start(new System.Net.IPAddress(new byte[] { 127, 0, 0, 1 }), 8484);

            while (Console.ReadKey().Key == ConsoleKey.Escape) ;
        }

        private static void Server_OnReceived(object sender, NetClientReceivedEventArgs<byte[]> e)
        {
            throw new NotImplementedException();
        }

        private static void Server_OnStopped(object sender, NetStoppedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void Server_OnStarted(object sender, NetStartedEventArgs e)
        {
            write("[Server] has started listening to {0}:{1}", server.Address, server.Port);
        }

        private static void Server_OnError(object sender, NetExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void Server_OnClientRejected(object sender, NetClientRejectedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void Server_OnClientDisconnected(object sender, NetClientDisconnectedEventArgs e)
        {
            write("[Client] Disconnected from server. \nReason: {0}", e.Reason);
        }

        private static void Server_OnClientConnected(object sender, NetClientConnectedEventArgs e)
        {
            write("[Client] has connected \nGuid: {0} ", ByteArrayToString(e.Guid.ToByteArray()));

            writer.WriteShort(0x0D);
            writer.WriteShort(0x63);
            writer.WriteMapleString("2");
            server.DispatchTo(e.Guid, writer.ToArray());
        }

        private static void Server_OnClientAccepted(object sender, NetClientAcceptedEventArgs e)
        {

        }

        private static void write(string msg, params object[]  obj)
        {
            Console.WriteLine(msg, obj);
        }

        private static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", " ");
        }
    }
}
