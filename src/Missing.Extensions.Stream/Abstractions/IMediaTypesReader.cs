using Missing.Extensions.Stream.Models;

namespace Missing.Extensions.Stream.Abstractions;

internal interface IMediaTypesReader
{
    MediaTypeInfo[] ReadMediaTypes();
}
