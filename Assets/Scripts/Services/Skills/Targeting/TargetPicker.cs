using System;
using System.Collections.Generic;
using Services.HeroRegistry;
using StaticData.Skills;

namespace Services.Skills.Targeting
{
    public class TargetPicker : ITargetPicker
    {
        private readonly IHeroRegistry _heroRegistry;

        public TargetPicker(IHeroRegistry heroRegistry)
        {
            _heroRegistry = heroRegistry;
        }

        public IEnumerable<string> AvailableTargetsFor(string casterId, TargetType targetType)
        {
            switch (targetType)
            {
                case TargetType.Ally:
                case TargetType.AllAllies:
                    return _heroRegistry.AlliesOf(casterId);
                case TargetType.Enemy:
                case TargetType.AllEnemies:
                    return _heroRegistry.EnemiesOf(casterId);
                case TargetType.Self:
                    return new[] { casterId };
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetType), targetType, null);
            }
        }
    }
}