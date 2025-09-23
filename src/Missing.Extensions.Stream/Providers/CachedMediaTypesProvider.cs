using Missing.Extensions.Stream.Abstractions;

namespace Missing.Extensions.Stream.Providers;

internal sealed class CachedMediaTypesProvider(
    IMediaTypesProvider provider) : IMediaTypesProvider
{
    private readonly WeakReference<MediaTypeInfo[]> _cache =
        new(provider.GetMediaTypes());

    public MediaTypeInfo[] GetMediaTypes()
    {
        if (_cache.TryGetTarget(out var mediaTypes))
        {
            return mediaTypes;
        }

        mediaTypes = provider.GetMediaTypes();
        _cache.SetTarget(mediaTypes);
        return mediaTypes;
    }
}
