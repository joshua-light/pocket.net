using System.Net.Sockets;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods for <see cref="Socket"/>.
    /// </summary>
    public static class SocketExtensions
    {
        /// <summary>
        ///     Checks whether <paramref name="self"/> is connected to client. 
        /// </summary>
        /// <remarks>This method uses some polling to do that</remarks>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns><code>true</code> if <paramref name="self"/> is connected, otherwise <code>false</code>.</returns>
        public static bool IsConnected(this Socket self)
        {
            try
            {
                return !(self.Poll(0, SelectMode.SelectRead) && self.Available == 0);
            }
            catch (SocketException)
            {
                return false;
            }
        }
    }
}