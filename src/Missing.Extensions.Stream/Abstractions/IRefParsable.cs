namespace Missing.Extensions.Stream.Abstractions;

internal interface IRefParsable<out TSelf>
    where TSelf : IRefParsable<TSelf>, allows ref struct
{
    static abstract implicit operator TSelf(string? s);

    static abstract implicit operator TSelf(ReadOnlySpan<char> s);
}
