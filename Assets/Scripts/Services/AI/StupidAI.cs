using System.Collections.Generic;
using System.Linq;
using Extensions;
using Logic.Heroes;
using Services.Battle;
using Services.HeroRegistry;
using Services.Skills.Targeting;
using Services.StaticData;
using StaticData.Heroes;
using StaticData.Skills;

namespace Services.AI
{
    public class StupidAI : IArtificialIntelligence
    {
        private readonly ITargetPicker _targetPicker;
        private readonly IStaticDataService _staticDataService;
        private readonly IHeroRegistry _heroRegistry;

        public StupidAI(
            ITargetPicker targetPicker, 
            IStaticDataService staticDataService, 
            IHeroRegistry heroRegistry)
        {
            _staticDataService = staticDataService;
            _heroRegistry = heroRegistry;
            _targetPicker = targetPicker;
        }

        public HeroAction MakeBestDecision(IHero readyHero)
        {
            SkillTypeId chosen =
                readyHero.State.SkillStates
                    .Where(x => x.IsReady)
                    .Select(x => x.TypeId)
                    .PickRandom();

            HeroSkill skill = _staticDataService.HeroSkillFor(chosen, readyHero.TypeId);

            return new HeroAction
            {
                Caster = readyHero,
                Skill = chosen,
                SkillKind = skill.Kind,
                TargetIds = ChoseTargets(readyHero.Id, skill.TargetType,
                    _targetPicker.AvailableTargetsFor(readyHero.Id, skill.TargetType))
            };
        }

        private List<string> ChoseTargets(string casterId, TargetType targetType, IEnumerable<string> availableTargets)
        {
            switch (targetType)
            {
                case TargetType.Ally:
                    return new List<string> { _heroRegistry.AlliesOf(casterId).PickRandom() };
                case TargetType.Enemy:
                    return new List<string> { _heroRegistry.EnemiesOf(casterId).PickRandom() };
                case TargetType.AllAllies:
                    return _heroRegistry.AlliesOf(casterId).ToList();
                case TargetType.AllEnemies:
                    return _heroRegistry.EnemiesOf(casterId).ToList();
                case TargetType.Self:
                    return new List<string> { casterId };
            }

            return new List<string>();
        }
    }
}