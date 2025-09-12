namespace Missing.Extensions.Stream.Tests;

public class TestsBase
{
    private protected static void Using<T>(string? s, Action<T> action)
        where T : IParsable<T>, allows ref struct =>
        action(s);
}
