using System.Collections.Generic;
using StaticData.Skills;

namespace Services.Skills.Targeting
{
    public interface ITargetPicker
    {
        IEnumerable<string> AvailableTargetsFor(string casterId, TargetType targetType);
    }
}