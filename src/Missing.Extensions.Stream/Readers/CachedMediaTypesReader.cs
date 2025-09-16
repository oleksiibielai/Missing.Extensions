using Missing.Extensions.Stream.Abstractions;

namespace Missing.Extensions.Stream.Readers;

internal sealed class CachedMediaTypesReader(
    IMediaTypesReader reader) : IMediaTypesReader
{
    private readonly WeakReference<MediaTypeInfo[]> _cache =
        new(reader.ReadMediaTypes());

    public MediaTypeInfo[] ReadMediaTypes()
    {
        if (_cache.TryGetTarget(out var mediaTypes))
        {
            return mediaTypes;
        }

        mediaTypes = reader.ReadMediaTypes();
        _cache.SetTarget(mediaTypes);
        return mediaTypes;
    }
}
