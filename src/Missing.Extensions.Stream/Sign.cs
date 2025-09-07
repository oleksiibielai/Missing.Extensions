namespace Missing.Extensions.Stream;

internal readonly ref struct Sign
{
    private readonly long _offset;
    private readonly Hex _hex;

    private Sign(ReadOnlySpan<char> source)
    {
        if (source.Count(',') != 1)
        {
            throw new FormatException("The input is not a valid sign string.");
        }

        Span<Range> ranges = stackalloc Range[2];
        source.Split(ranges, ',');

        if (!long.TryParse(source[ranges[0]], out _offset))
        {
            throw new FormatException("The input is not a valid offset string.");
        }

        _hex = source[ranges[1]];
    }

    public static implicit operator Sign(string? source) => new(source);

    public static implicit operator Sign(ReadOnlySpan<char> source) => new(source);

    public bool IsMatch(System.IO.Stream stream) => CanMatch(stream) && _hex.IsMatch(_offset, stream);

    private bool CanMatch(System.IO.Stream stream) =>
        stream is { CanRead: true, CanSeek: true } && stream.Length >= _offset + _hex.BytesLength;
}
