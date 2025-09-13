using System.Reflection;
using Missing.Extensions.Stream.Abstractions;
using static System.Text.Json.JsonSerializer;
using static System.Text.Json.JsonSerializerOptions;

namespace Missing.Extensions.Stream.Services;

internal sealed class MediaTypesReader : IMediaTypesReader
{
    public MediaTypeInfo[] ReadMediaTypes()
    {
        using var stream = new MemoryStream();
        GetResourceStream("MediaTypes.db").DecompressTo(stream);
        return DeserializeFrom(stream);
    }

    private static System.IO.Stream GetResourceStream(string name)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceStream = assembly.GetManifestResourceStream(
            typeof(StreamExtensions), $"Resources.{name}");
        return resourceStream ?? throw new InvalidOperationException(
            "The specified resource was not found.");
    }

    private static MediaTypeInfo[] DeserializeFrom(System.IO.Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);
        return Deserialize<Dictionary<string, MimeTypeInfo>>(stream, Web)?
            .Select(x => new MediaTypeInfo(x.Value.Mime, x.Key, x.Value.Signs))
            .ToArray() ?? throw new InvalidOperationException(
            "The input stream is empty or invalid.");
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private sealed record MimeTypeInfo(string Mime, string[] Signs);
}
