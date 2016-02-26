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
        private static NetObjectServer server = new NetObjectServer();
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

        private static void Server_OnStopped(object sender, NetStoppedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void Server_OnStarted(object sender, NetStartedEventArgs e)
        {
            write("[Server] has started listening to {0}:{1}", server.Address, server.Port);
        }

        private static void Server_OnReceived(object sender, NetClientReceivedEventArgs<NetObject> e)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private static void Server_OnClientConnected(object sender, NetClientConnectedEventArgs e)
        {
            write("[Client] has connected");

            writer.WriteShort(14);
            writer.WriteShort(62);
            writer.WriteMapleString("1");
            NetObject obj = new NetObject("GetHello", writer.ToArray());
            server.DispatchTo(e.Guid, obj);
        }

        private static void Server_OnClientAccepted(object sender, NetClientAcceptedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void write(string msg, params object[]  obj)
        {
            Console.WriteLine(msg, obj);
        }
    }
}
