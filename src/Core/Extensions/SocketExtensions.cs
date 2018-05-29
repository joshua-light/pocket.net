using System.Net.Sockets;

namespace Pocket.Common
{
    public static class SocketExtensions
    {
        public static bool IsConnected(this Socket self)
        {
            try
            {
                return !(self.Poll(1, SelectMode.SelectRead) && self.Available == 0);
            }
            catch (SocketException)
            {
                return false;
            }
        }
    }
}