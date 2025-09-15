using System.Reflection;
using Missing.Extensions.Stream.Abstractions;
using static System.Text.Json.JsonSerializer;
using static System.Text.Json.JsonSerializerOptions;

namespace Missing.Extensions.Stream.Readers;

internal sealed class ResourceReader(string name) : IMediaTypesReader
{
    public MediaTypeInfo[] ReadMediaTypes()
    {
        using var stream = new MemoryStream();
        GetResourceStream(name).DecompressTo(stream);
        return DeserializeFrom(stream);
    }

    private static System.IO.Stream GetResourceStream(string name)
    {
        var resourceStream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream(typeof(Extensions), $"Resources.{name}");
        return resourceStream ?? throw new InvalidOperationException(
            "The specified resource was not found.");
    }

    private static MediaTypeInfo[] DeserializeFrom(System.IO.Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);
        return Deserialize<Dictionary<string, MimeType>>(stream, Web)?
            .Select(x => new MediaTypeInfo(x.Value.Mime, x.Key, x.Value.Signs))
            .ToArray() ?? throw new InvalidOperationException(
            "The input stream is empty or invalid.");
    }

    private sealed record MimeType(string Mime, string[] Signs);
}
