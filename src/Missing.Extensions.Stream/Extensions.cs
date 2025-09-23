using System.IO.Compression;
using Missing.Extensions.Stream.Providers;
using static System.IO.Compression.CompressionMode;

namespace Missing.Extensions.Stream;

public static class Extensions
{
    private static readonly CachedMediaTypesProvider MediaTypesProvider =
        new(new EmbeddedMediaTypesProvider("MediaTypes.db"));

    public static MediaType GetMediaType(
        this System.IO.Stream stream, bool leaveOpen = false)
    {
        try
        {
            return Array.Find(
                MediaTypesProvider.GetMediaTypes(),
                mediaType => mediaType.IsMatch(stream));
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
