using Missing.Extensions.Stream.Abstractions;

namespace Missing.Extensions.Stream.Tests.Abstractions;

public abstract class TestsBase
{
    private protected static void Using<T>(string? s, Action<T> action)
        where T : IRefParsable<T>, allows ref struct =>
        action(s);
}
