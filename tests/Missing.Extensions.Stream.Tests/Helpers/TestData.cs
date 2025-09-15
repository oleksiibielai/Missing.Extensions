using static Missing.Extensions.Stream.Tests.Helpers.TestStream;

namespace Missing.Extensions.Stream.Tests.Helpers;

public static class TestData
{
    private const string HexPdf = "25504446";
    private const string HexMp4 = "6674797069736F6D";

    public static MatrixTheoryData<long, string, System.IO.Stream> UnmatchableData => new(
        [0], [HexPdf], [Unreadable, Unseekable, Closed]
    );

    public static MatrixTheoryData<long, string, System.IO.Stream> InvalidData => new(
        [0], [HexPdf], [Mp4, Empty, Unknown]
    );

    public static TheoryData<long, string, System.IO.Stream> ValidData => new(
        (4, HexMp4, Mp4), (0, HexPdf, Pdf)
    );

    public static TheoryDataRow<string, string, System.IO.Stream>[] Matchable =>
    [
        new("Unknown", "Unknown", Empty),
        new("Unknown", "Unknown", Unknown),
        new("application/pdf", "pdf", Pdf),
        new("video/mp4", "mp4", Mp4)
    ];

    public static TheoryDataRow<System.IO.Stream>[] Unmatchable =>
    [
        new(Unreadable),
        new(Unseekable),
        new(Closed)
    ];
}
