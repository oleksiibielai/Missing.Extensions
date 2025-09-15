namespace Missing.Extensions.Stream.Models;

internal readonly struct MediaTypeInfo(
    string name,
    string extension,
    string[] signs)
{
    private readonly string? _name = name;
    private readonly string? _extension = extension;

    public static implicit operator MediaType(MediaTypeInfo x) =>
        new(x._name ?? "Unknown", x._extension ?? "Unknown");

    public bool IsMatch(System.IO.Stream stream) =>
        Array.Exists(signs, sign => IsMatch(sign, stream));

    private static bool IsMatch(
        scoped Sign sign, System.IO.Stream stream) =>
        sign.IsMatch(stream);
}
