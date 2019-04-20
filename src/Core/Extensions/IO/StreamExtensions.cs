using System;
using System.IO;
using System.Threading.Tasks;

namespace Pocket.Common
{
    public static class StreamExtensions
    {
        public static Task<string> Text(this Stream self) =>
            self.Using(x => new StreamReader(x).Using(y => y.ReadToEndAsync()));

        public static Task<byte[]> Bytes(this Stream self) => self.Bytes(self.Length, Progress.Fake<int>());
        
        public static async Task<byte[]> Bytes(this Stream self, long length, IProgress<int> progress)
        {
            var bytes = new byte[length];
            var total = 0;

            while (total != length)
            {
                var read = await self.ReadAsync(bytes, total, (int) (length - total));
                if (read == 0)
                    throw new Exception($"Expected to read at least [ {length} ] number of bytes, but got only [ {total} ].");

                total += read;
                progress.Report((int) ((double) total / length * 100));
            }
            
            return bytes;
        }
    }
}