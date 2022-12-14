using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ProfileSharp.AspNetCore")]
namespace ProfileSharp.Enums
{
    internal enum ProfileSharpMode
    {
        Disabled,
        Profiling,
        Mocking
    }
}
