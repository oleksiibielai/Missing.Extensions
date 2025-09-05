namespace Missing.Extensions.Stream.Tests;

public sealed class SignTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("16ABCD")]
    [InlineData("16,AB,CD")]
    [InlineData("!6,ABCD")]
    [InlineData(",ABCD")]
    [InlineData("16,A")]
    [InlineData("16,")]
    public void ShouldThrowFormatException(string? s) =>
        Assert.Throws<FormatException>(() =>
        {
            Sign _ = s;
        });
}
