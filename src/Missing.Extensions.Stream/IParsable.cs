namespace Missing.Extensions.Stream;

internal interface IParsable<out TSelf>
    where TSelf : IParsable<TSelf>, allows ref struct
{
    static abstract implicit operator TSelf(string? s);

    static abstract implicit operator TSelf(ReadOnlySpan<char> s);
}
