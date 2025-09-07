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

    [Theory]
    [MemberData(nameof(TestData.InvalidData), MemberType = typeof(TestData))]
    public void ShouldNotMatchInvalidData(long offset, string s, System.IO.Stream stream)
    {
        Sign sign = $"{offset},{s}";
        Assert.False(sign.IsMatch(stream));
    }

    [Theory]
    [MemberData(nameof(TestData.ValidData), MemberType = typeof(TestData))]
    public void ShouldMatchValidData(long offset, string s, System.IO.Stream stream)
    {
        Sign sign = $"{offset},{s}";
        Assert.True(sign.IsMatch(stream));
    }
}
