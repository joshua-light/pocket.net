using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Pocket
{
    public static class TcpListenerExtensions
    {
        public static async Task<TcpClient> AcceptClientAsync(this TcpListener self, CancellationToken token) 
        { 
            using (token.Register(self.Stop)) 
            { 
                try 
                { 
                    return await self.AcceptTcpClientAsync(); 
                } 
                catch (SocketException e) when (e.SocketErrorCode == SocketError.Interrupted) 
                { 
                    throw new OperationCanceledException(); 
                } 
                catch (ObjectDisposedException) when (token.IsCancellationRequested) 
                { 
                    throw new OperationCanceledException(); 
                } 
            } 
        } 
    }
}