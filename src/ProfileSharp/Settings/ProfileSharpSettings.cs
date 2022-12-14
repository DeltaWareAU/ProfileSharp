using ProfileSharp.Enums;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ProfileSharp.AspNetCore")]
namespace ProfileSharp.Settings
{
    internal sealed class ProfileSharpSettings
    {
        public ProfileSharpMode Mode { get; set; } = ProfileSharpMode.Disabled;
    }
}
