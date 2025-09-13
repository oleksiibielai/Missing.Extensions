namespace Missing.Extensions.Stream.Abstractions;

internal interface IParsableRef<out TSelf>
    where TSelf : IParsableRef<TSelf>, allows ref struct
{
    static abstract implicit operator TSelf(string? s);

    static abstract implicit operator TSelf(ReadOnlySpan<char> s);
}
