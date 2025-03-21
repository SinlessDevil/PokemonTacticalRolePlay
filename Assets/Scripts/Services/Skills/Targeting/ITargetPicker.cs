using System.Collections.Generic;
using CodeBase.StaticData.Skills;

namespace CodeBase.Gameplay.Skills.Targeting
{
    public interface ITargetPicker
    {
        IEnumerable<string> AvailableTargetsFor(string casterId, TargetType targetType);
    }
}