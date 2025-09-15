// ReSharper disable UnusedMember.Global

namespace Missing.Extensions.Stream.Models;

public readonly struct MediaType(
    string name,
    string extension)
{
    public string Name { get; } = name;
    public string Extension { get; } = extension;
}
