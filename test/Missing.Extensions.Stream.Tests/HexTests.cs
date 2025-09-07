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
    public void ShouldCalculateBytesLength(string s, int expected)
    {
        Hex hex = s;
        Assert.Equal(expected, hex.BytesLength);
    }

    [Theory]
    [MemberData(nameof(TestData.ValidData), MemberType = typeof(TestData))]
    public void ShouldMatchValidData(long offset, string s, System.IO.Stream stream)
    {
        Hex hex = s;
        Assert.True(hex.IsMatch(offset, stream));
    }
}
