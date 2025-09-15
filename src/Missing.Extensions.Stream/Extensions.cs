using System.IO.Compression;
using Missing.Extensions.Stream.Abstractions;
using Missing.Extensions.Stream.Readers;
using static System.IO.Compression.CompressionMode;

namespace Missing.Extensions.Stream;

public static class Extensions
{
    private static readonly IMediaTypesReader Reader =
        new ResourceReader("MediaTypes.db");

    public static MediaType GetMediaType(
        this System.IO.Stream stream, bool leaveOpen = false)
    {
        try
        {
            return Array.Find(
                Reader.ReadMediaTypes(),
                type => type.IsMatch(stream));
        }
        finally
        {
            if (!leaveOpen)
            {
                stream.Dispose();
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
