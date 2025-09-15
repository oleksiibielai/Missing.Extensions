using System.Reflection;
using Missing.Extensions.Stream.Tests.Helpers;

namespace Missing.Extensions.Stream.Tests;

public sealed class MediaTypeTests
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
        await Task.WhenAll(Enumerable.Range(1, 10).Select(_ => Task.Factory.StartNew(() =>
            Assert.Equal(new MediaType(name, extension), stream.GetMediaType(true)))));

    [Fact]
    public void ShouldVerifyResourceStream() => Assert.Equal(
        new MediaType("application/gzip", "gz"), Assembly.GetAssembly(typeof(Extensions))!
            .GetManifestResourceStream(typeof(Extensions), "Resources.MediaTypes.db")!
            .GetMediaType());
}
