using ProfileSharp.Attributes;
using ProfileSharp.Store;
using System;

namespace ProfileSharp.AspNetCore
{
    public interface IProfileSharpConfiguration
    {
        /// <summary>
        /// Specifies that ProfileSharp will use Profiling Mode. All <see cref="Type"/>s that implement the <see cref="EnableProfileSharpAttribute"/> will be profiled and the execution scope will be stored in the specified <see cref="IProfilingStore"/>.
        /// </summary>
        void EnableProfiling();

        /// <summary>
        /// Specifies that ProfileSharp will use Mocking Mode. All <see cref="Type"/>s that implement the <see cref="EnableProfileSharpAttribute"/> will attempt to mocked from data contained in the <see cref="IMockDataStore"/>.
        /// </summary>
        void EnableMocking();

        /// <summary>
        /// Specifies that ProfileSharp will disabled, no profiling or mocking will be done.
        /// </summary>
        /// <remarks>All <see cref="Type"/>s with the <see cref="EnableProfileSharpAttribute"/> will still be intercepted.</remarks>
        void Disable();
    }
}
