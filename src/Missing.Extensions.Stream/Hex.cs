using System.Buffers;

namespace Missing.Extensions.Stream;

internal readonly ref struct Hex
{
    private readonly int _length;
    private readonly ReadOnlySpan<char> _source;

    private Hex(ReadOnlySpan<char> source)
    {
        if (int.DivRem(source.Length, 2) is not (> 0, 0))
        {
            throw new FormatException("The input is not a valid hex string.");
        }

        _length = source.Length >> 1;
        _source = source;
    }

    public static implicit operator Hex(string? source) => new(source);

    public static implicit operator Hex(ReadOnlySpan<char> source) => new(source);

    public static long operator +(Hex left, long right) => left._length + right;

    public static long operator +(long left, Hex right) => right + left;

    internal bool IsMatch(System.IO.Stream stream)
    {
        Span<byte> bytes = stackalloc byte[_length];
        var status = Convert.FromHexString(_source, bytes, out _, out _);

        if (status != OperationStatus.Done)
        {
            return false;
        }

        Span<byte> buffer = stackalloc byte[_length];
        stream.ReadExactly(buffer);

        return bytes.SequenceEqual(buffer);
    }
}
