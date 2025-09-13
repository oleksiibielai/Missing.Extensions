using Missing.Extensions.Stream.Services;

namespace Missing.Extensions.Stream.Tests.Services;

public sealed class MediaTypesReaderTests
{
    private static readonly MediaTypesReader Sut = new();

    [Fact]
    public void ShouldReadValidMediaTypes() =>
        Assert.All(Sut.ReadMediaTypes(), mediaType =>
        {
            Assert.False(string.IsNullOrWhiteSpace(mediaType.Name));
            Assert.False(string.IsNullOrWhiteSpace(mediaType.Extension));
            Assert.All(mediaType.Signs, sign => Assert.Equal(1, sign.Count(c => c == ',')));
        });
}
