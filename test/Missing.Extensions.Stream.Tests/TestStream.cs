namespace Missing.Extensions.Stream.Tests;

internal sealed class TestStream : MemoryStream
{
    private TestStream(byte[] buffer, bool canRead = true, bool canSeek = true)
        : base(buffer) => (CanRead, CanSeek) = (canRead, canSeek);

    public static TestStream Empty => new([]);

    public static TestStream Unreadable => new([0], false);

    public static TestStream Unseekable => new([0], true, false);

    public static TestStream Closed => new([0], false, false);

    public static TestStream Unknown => new("Unknown"u8.ToArray());

    public static TestStream Mp4 => new("\0\0\0 ftypisom"u8.ToArray());

    public static TestStream Pdf => new("%PDF"u8.ToArray());

    public override bool CanRead { get; }

    public override bool CanSeek { get; }
}
