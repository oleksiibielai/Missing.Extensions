using System.IO.Compression;

namespace Missing.Extensions.Stream;

public static class StreamExtensions
{
    internal static void DecompressTo(
        this System.IO.Stream source, System.IO.Stream output)
    {
        using var decompressor = new GZipStream(
            source, CompressionMode.Decompress);
        decompressor.CopyTo(output);
    }
}
