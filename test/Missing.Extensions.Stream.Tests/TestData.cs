using static Missing.Extensions.Stream.Tests.TestStream;

namespace Missing.Extensions.Stream.Tests;

public static class TestData
{
    private const string HexPdf = "25504446";
    private const string HexMp4 = "6674797069736F6D";

    public static MatrixTheoryData<long, string, System.IO.Stream> InvalidData => new(
        [0], [HexPdf], [Mp4, Empty, NotReadable, NotSeekable, Closed]
    );

    public static TheoryData<long, string, System.IO.Stream> ValidData => new(
        (4, HexMp4, Mp4), (0, HexPdf, Pdf)
    );
}
