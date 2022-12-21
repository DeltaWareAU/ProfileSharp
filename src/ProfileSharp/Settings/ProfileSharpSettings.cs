using ProfileSharp.Enums;

namespace ProfileSharp.Settings
{
    public sealed class ProfileSharpSettings : IProfileSharpSettings
    {
        public ProfileSharpMode Mode { get; set; } = ProfileSharpMode.Disabled;
    }
}
