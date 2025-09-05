namespace Missing.Extensions.Stream.Tests;

public sealed class HexTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("A")]
    [InlineData("ABC")]
    public void ShouldThrowFormatException(string? s) =>
        Assert.Throws<FormatException>(() =>
        {
            Hex _ = s;
        });

    [Theory]
    [InlineData("AB", 1)]
    [InlineData("ABCD", 2)]
    public void ShouldCountBytes(string s, int expected) =>
        Assert.Equal(expected, ((Hex)s).BytesCount);
}
