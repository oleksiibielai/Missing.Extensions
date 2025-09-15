namespace Missing.Extensions.Stream.Tests;

public sealed class ExtensionsTests
{
    [Theory]
    [MemberData(
        nameof(TestData.Unmatchable),
        MemberType = typeof(TestData))]
    public void ShouldThrowNotSupportedException(System.IO.Stream stream) =>
        Assert.Throws<NotSupportedException>(() => stream.GetMediaType());

    [Theory]
    [MemberData(
        nameof(TestData.Matchable),
        MemberType = typeof(TestData))]
    public async Task ShouldBeThreadSafe(
        string name, string extension, System.IO.Stream stream) =>
        Assert.Null(await Record.ExceptionAsync(() => Task.WhenAll(
            Enumerable.Range(1, 10).Select(_ => Task.Factory.StartNew(() =>
                Assert.Equal((name, extension), stream.GetMediaType(true)))))));
}
