using System;
using System.Collections.Generic;
using CodeBase.Gameplay.HeroRegistry;
using CodeBase.StaticData.Skills;

namespace CodeBase.Gameplay.Skills.Targeting
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
                    return _heroRegistry.EnemiesOf(casterId);
                case TargetType.Enemy:
                case TargetType.AllEnemies:
                    return _heroRegistry.AlliesOf(casterId);
                case TargetType.Self:
                    return new[] { casterId };
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetType), targetType, null);
            }
        }
    }
}