using System;
using System.Net;
using System.Net.Sockets;
using Pocket.Extensions;
using Xunit;

namespace Pocket.Tests.Extensions
{
    public class SocketExtensionsTest
    {
        [Fact]
        public void IsConnected_ShouldBeTrue_IfSocketIsConnected()
        {
            using (Start())
            {
                var client = Connect();
            
                Assert.True(client.Socket.IsConnected());
            }
        }
        
        [Fact]
        public void IsConnected_ShouldBeTrue_IfSocketIsConnectedOnServer()
        {
            using (var server = Start())
            {
                Connect();
                var serverClient = server.Accept();
            
                Assert.True(serverClient.IsConnected());
            }
        }
        
        [Fact]
        public void IsConnected_ShouldBeFalse_IfSocketIsConnectedOnServerAndThenClosed()
        {
            using (var server = Start())
            {
                var client = Connect();
                var serverClient = server.Accept();

                client.Socket.Close();
                
                Assert.False(serverClient.IsConnected());
            }
        }
        
        [Fact]
        public void IsConnected_ShouldBeFalse_IfSocketIsConnectedOnServerAndThenDisposed()
        {
            using (var server = Start())
            {
                var client = Connect();
                var serverClient = server.Accept();

                client.Disconnect();
                
                Assert.False(serverClient.IsConnected());
            }
        }
        
        [Fact]
        public void IsConnected_ShouldThrow_IfSocketIsDisposed()
        {
            using (Start())
            {
                var client = Connect();
                client.Socket.Close();
            
                Assert.Throws<ObjectDisposedException>(() => client.Socket.IsConnected());
            }
        }

        #region Helpers

        private class Server : IDisposable
        {
            public const string Ip = "127.0.0.1";
            public const int Port = 4752;

            private readonly TcpListener _listener;

            public Server()
            {
                _listener = new TcpListener(IPAddress.Parse(Ip), Port);
                _listener.Start();
            }

            public Socket Accept() => _listener.AcceptSocket();
            
            public void Dispose()
            {
                _listener.Stop();
            }
        }
        
        private class Client
        {
            private readonly TcpClient _client;

            public Client()
            {
                _client = new TcpClient();
                _client.Connect(IPAddress.Parse(Server.Ip), Server.Port);
            }

            public Socket Socket => _client.Client;

            public void Disconnect() => _client.Dispose();
        }
        
        private static Server Start() => new Server();
        private static Client Connect() => new Client();

        #endregion
    }
}