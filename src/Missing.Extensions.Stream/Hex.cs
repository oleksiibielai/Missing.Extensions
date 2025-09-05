namespace Missing.Extensions.Stream;

internal readonly ref struct Hex
{
    private readonly ReadOnlySpan<char> _source;

    private Hex(ReadOnlySpan<char> source)
    {
        if (int.DivRem(source.Length, 2) is not (> 0, 0))
        {
            throw new FormatException("The input is not a valid hex string.");
        }

        _source = source;
    }

    public static implicit operator Hex(string? source) => new(source);

    public static implicit operator Hex(ReadOnlySpan<char> source) => new(source);

    public int BytesCount => _source.Length >> 1;
}
