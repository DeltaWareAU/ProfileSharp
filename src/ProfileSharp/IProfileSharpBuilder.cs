﻿using ProfileSharp.Mocking.Configuration;
using ProfileSharp.Profiling.Configuration;
using System;

namespace ProfileSharp
{
    public interface IProfileSharpBuilder
    {
        IProfileSharpBuilder AddMocking(Action<IMockingConfigurationBuilder> optionsAction);

        IProfileSharpBuilder AddProfiling(Action<IProfilingConfigurationBuilder> optionsAction);
    }
}
