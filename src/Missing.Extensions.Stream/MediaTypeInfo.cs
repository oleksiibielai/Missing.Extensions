namespace Missing.Extensions.Stream;

internal sealed class MediaTypeInfo(
    string name,
    string extension,
    string[] signs)
{
    public string Name { get; } = name;
    public string Extension { get; } = extension;
    public string[] Signs { get; } = signs;
}
