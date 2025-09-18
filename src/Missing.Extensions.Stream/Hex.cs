using System.Buffers;
using Missing.Extensions.Stream.Abstractions;

namespace Missing.Extensions.Stream;

internal readonly ref struct Hex : IParsableRef<Hex>
{
    private readonly ReadOnlySpan<char> _source;

    private Hex(ReadOnlySpan<char> source)
    {
        if (source.IsEmpty || (source.Length & 1) != 0)
        {
            throw new FormatException("The input is not a valid hex string.");
        }

        _source = source;
    }

    public static implicit operator Hex(string? s) => new(s);

    public static implicit operator Hex(ReadOnlySpan<char> s) => new(s);

    public static implicit operator int(Hex hex) => hex._source.Length >> 1;

    public bool IsMatch(long offset, System.IO.Stream stream)
    {
        scoped Span<byte> bytes = stackalloc byte[this];
        var status = Convert.FromHexString(_source, bytes, out _, out _);

        if (status != OperationStatus.Done)
        {
            return false;
        }

        scoped Span<byte> buffer = stackalloc byte[this];
        lock (stream)
        {
            stream.Seek(offset, SeekOrigin.Begin);
            stream.ReadExactly(buffer);
        }

        return bytes.SequenceEqual(buffer);
    }
}
