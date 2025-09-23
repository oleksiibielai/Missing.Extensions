# Missing.Extensions

[![Build and publish](https://github.com/oleksiibielai/Missing.Extensions/actions/workflows/build.yml/badge.svg)](https://github.com/oleksiibielai/Missing.Extensions/actions/workflows/build.yml)

## Missing.Extensions.Stream

[![NuGet Version](https://img.shields.io/nuget/v/Missing.Extensions.Stream)](https://www.nuget.org/packages/Missing.Extensions.Stream)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Missing.Extensions.Stream)](https://www.nuget.org/packages/Missing.Extensions.Stream)

Detects media type (MIME type) for `System.IO.Stream` by checking its signature.

### Why is it useful?

- **Security:** Prevents spoofing by validating files based on content, not just extension.
- **Performance optimized:** Minimal allocations, thread safety, and high efficiency.
- **No external dependencies:** Pure .NET, no third-party libraries required.

### Example usage

```csharp
using Missing.Extensions.Stream;

var (name, ext) = File
    .OpenRead("open.me")
    .GetMediaType();

Console.WriteLine($"{name}, {ext}");
// Outputs: application/pdf, pdf
```
