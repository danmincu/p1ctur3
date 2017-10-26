using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telekad.Utils
{
    public static class StreamExtensions
    {
        public static void CopyTo(this Stream stream, Stream destination, IProgress<long> progress)
        {
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (!stream.CanRead && !stream.CanWrite)
            {
                throw new ObjectDisposedException(null, "ObjectDisposed_StreamClosed");
            }
            if (!destination.CanRead && !destination.CanWrite)
            {
                throw new ObjectDisposedException("destination", "ObjectDisposed_StreamClosed");
            }
            if (!stream.CanRead)
            {
                throw new NotSupportedException("NotSupported_UnreadableStream");
            }
            if (!destination.CanWrite)
            {
                throw new NotSupportedException("NotSupported_UnwritableStream");
            }
            InternalCopyTo(stream, destination, 0x14000, progress);
        }

        private static void InternalCopyTo(Stream stream, Stream destination, int bufferSize, IProgress<long> progress)
        {
            long cumulative = 0;
            int num;
            byte[] buffer = new byte[bufferSize];
            while ((num = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                destination.Write(buffer, 0, num);
                if (progress != null)
                {
                    cumulative += num;
                    progress.Report(cumulative);
                }

            }
        }

    }
}
