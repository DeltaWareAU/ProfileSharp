using ProfileSharp.Enums;
using ProfileSharp.Settings;

namespace ProfileSharp.AspNetCore
{
    internal class ProfileSharpConfiguration : IProfileSharpConfiguration
    {
        private readonly ProfileSharpSettings _settings;

        public ProfileSharpConfiguration(ProfileSharpSettings settings)
        {
            _settings = settings;
        }

        public void EnableProfiling()
            => _settings.Mode = ProfileSharpMode.Profiling;

        public void EnableMocking()
            => _settings.Mode = ProfileSharpMode.Mocking;

        public void Disable()
            => _settings.Mode = ProfileSharpMode.Disabled;
    }
}
