namespace Missing.Extensions.Stream;

internal sealed class MediaTypeInfo(
    string nameInfo,
    string extensionInfo,
    string[] signs)
{
    public void Deconstruct(
        out string name, out string extension) =>
        (name, extension) = (nameInfo, extensionInfo);

    public bool IsMatch(System.IO.Stream stream) =>
        Array.Exists(signs, sign => IsMatch(sign, stream));

    private static bool IsMatch(
        Sign sign, System.IO.Stream stream) =>
        sign.IsMatch(stream);
}
