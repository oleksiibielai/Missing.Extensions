using Missing.Extensions.Stream.Tests.Abstractions;
using Missing.Extensions.Stream.Tests.Helpers;

namespace Missing.Extensions.Stream.Tests;

public sealed class HexTests : TestsBase
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("A")]
    [InlineData("ABC")]
    public void ShouldThrowFormatException(string? s) =>
        Assert.Throws<FormatException>(() => Using<Hex>(s, _ => { }));

    [Theory]
    [MemberData(
        nameof(TestData.ValidData),
        MemberType = typeof(TestData))]
    public void ShouldMatchValidData(long offset, string s, System.IO.Stream stream) =>
        Using<Hex>(s, hex => Assert.True(hex.IsMatch(offset, stream)));
}
