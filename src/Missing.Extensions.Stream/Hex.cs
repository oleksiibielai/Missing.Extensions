using System.Buffers;

namespace Missing.Extensions.Stream;

internal readonly ref struct Hex
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

    public static implicit operator Hex(string? source) => new(source);

    public static implicit operator Hex(ReadOnlySpan<char> source) => new(source);

    public int BytesLength => _source.Length >> 1;

    public bool IsMatch(long offset, System.IO.Stream stream)
    {
        Span<byte> bytes = stackalloc byte[BytesLength];
        var status = Convert.FromHexString(_source, bytes, out _, out _);

        if (status != OperationStatus.Done)
        {
            return false;
        }

        Span<byte> buffer = stackalloc byte[BytesLength];
        stream.Seek(offset, SeekOrigin.Begin);
        stream.ReadExactly(buffer);

        return bytes.SequenceEqual(buffer);
    }
}
