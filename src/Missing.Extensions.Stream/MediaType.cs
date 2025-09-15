namespace Missing.Extensions.Stream;

internal readonly struct MediaType(
    string name,
    string extension,
    string[] signs)
{
    private const string Unknown =
        nameof(Unknown);

    private readonly string? _name = name;
    private readonly string? _extension = extension;

    public static implicit operator (string Name, string Extension)(MediaType x) =>
        (x._name ?? Unknown, x._extension ?? Unknown);

    public bool IsMatch(System.IO.Stream stream) =>
        Array.Exists(signs, sign => IsMatch(sign, stream));

    private static bool IsMatch(
        scoped Sign sign, System.IO.Stream stream) =>
        sign.IsMatch(stream);
}
