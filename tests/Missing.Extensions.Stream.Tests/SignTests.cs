using Missing.Extensions.Stream.Tests.Abstractions;
using Missing.Extensions.Stream.Tests.Helpers;

namespace Missing.Extensions.Stream.Tests;

public sealed class SignTests : TestsBase
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
        Assert.Throws<FormatException>(() => Using<Sign>(s, _ => { }));

    [Theory]
    [MemberData(
        nameof(TestData.UnmatchableData),
        MemberType = typeof(TestData))]
    public void ShouldThrowNotSupportedException(long offset, string s, System.IO.Stream stream) =>
        Assert.Throws<NotSupportedException>(() => Using<Sign>($"{offset},{s}", sign =>
            sign.IsMatch(stream)));

    [Theory]
    [MemberData(
        nameof(TestData.InvalidData),
        MemberType = typeof(TestData))]
    public void ShouldNotMatchInvalidData(long offset, string s, System.IO.Stream stream) =>
        Using<Sign>($"{offset},{s}", sign => Assert.False(sign.IsMatch(stream)));

    [Theory]
    [MemberData(
        nameof(TestData.ValidData),
        MemberType = typeof(TestData))]
    public void ShouldMatchValidData(long offset, string s, System.IO.Stream stream) =>
        Using<Sign>($"{offset},{s}", sign => Assert.True(sign.IsMatch(stream)));
}
