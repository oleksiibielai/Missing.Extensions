using System.IO.Compression;
using Missing.Extensions.Stream.Readers;
using static System.IO.Compression.CompressionMode;

namespace Missing.Extensions.Stream;

public static class Extensions
{
    private static readonly ResourceReader Reader = new("MediaTypes.db");

    public static (string Name, string Extension) GetMediaType(
        this System.IO.Stream stream, bool leaveOpen = false)
    {
        lock (stream)
        {
            try
            {
                var mediaTypes = Reader.ReadMediaTypes();
                return Array.Find(mediaTypes, x => x.IsMatch(stream));
            }
            finally
            {
                if (!leaveOpen)
                {
                    stream.Dispose();
                }
            }
        }
    }

    internal static void DecompressTo(
        this System.IO.Stream stream, System.IO.Stream output)
    {
        using var decompressor = new GZipStream(stream, Decompress);
        decompressor.CopyTo(output);
    }
}
