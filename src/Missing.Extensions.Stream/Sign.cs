using Missing.Extensions.Stream.Abstractions;

namespace Missing.Extensions.Stream;

internal readonly ref struct Sign : IParsableRef<Sign>
{
    private readonly long _offset;
    private readonly Hex _hex;

    private Sign(ReadOnlySpan<char> source)
    {
        if (source.Count(',') != 1)
        {
            throw new FormatException("The input is not a valid sign string.");
        }

        scoped Span<Range> ranges = stackalloc Range[2];
        source.Split(ranges, ',');

        if (!long.TryParse(source[ranges[0]], out _offset))
        {
            throw new FormatException("The input is not a valid offset string.");
        }

        _hex = source[ranges[1]];
    }

    public static implicit operator Sign(string? s) => new(s);

    public static implicit operator Sign(ReadOnlySpan<char> s) => new(s);

    public bool IsMatch(System.IO.Stream stream) =>
        stream is not { CanRead: true, CanSeek: true }
            ? throw new NotSupportedException("Stream does not support matching.")
            : stream.Length >= _offset + _hex && _hex.IsMatch(_offset, stream);
}
