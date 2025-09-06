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
}
