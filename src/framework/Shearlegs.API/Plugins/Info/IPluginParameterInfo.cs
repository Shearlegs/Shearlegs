using System;

namespace Shearlegs.API.Plugins.Info
{
    public interface IPluginParameterInfo
    {   
        string Name { get; }
        string Description { get; }
        Type Type { get; }
        object Value { get; }
        bool IsRequired { get; }
        bool IsSecret { get; }
    }
}
