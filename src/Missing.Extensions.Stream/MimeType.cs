namespace Missing.Extensions.Stream;

internal readonly record struct MimeType(
    string Name,
    string Extension,
    string[] Signs
);
