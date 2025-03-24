using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Logic.Heroes;
using Services.AI.Reporting;
using Services.Battle;
using Services.HeroRegistry;
using Services.Skills;
using Services.Skills.Targeting;
using Services.StaticData;

namespace Services.AI.UtilityAI
{
    public class UtilityAI : IArtificialIntelligence
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ITargetPicker _targetPicker;
        private readonly IHeroRegistry _heroRegistry;
        private readonly ISkillSolver _skillSolver;
        private readonly IEnumerable<IUtilityFunction> _utilityFunctions;
        private readonly IAIReporter _aiReporter;

        public UtilityAI(
            IStaticDataService staticDataService, 
            ITargetPicker targetPicker, 
            IHeroRegistry heroRegistry, 
            ISkillSolver skillSolver,
            IAIReporter aiReporter)
        {
            _staticDataService = staticDataService;
            _targetPicker = targetPicker;
            _heroRegistry = heroRegistry;
            _skillSolver = skillSolver;
            _aiReporter = aiReporter;

            _utilityFunctions = new Brains().GetUnitilityFunctions();
        }

        public HeroAction MakeBestDecision(IHero readyHero)
        {
            List<ScoredAction> choices = GetScoredActions(readyHero, ReadyBattleSkills(readyHero))
                .ToList();
            
            _aiReporter.ReportDecisionScores(readyHero, choices);
            
            return choices.FindMax(x => x.Score);
        }

        private IEnumerable<BattleSkill> ReadyBattleSkills(IHero readyHero)
        {
            return readyHero.State.SkillStates
                .Where(x => x.IsReady)
                .Select(x => new BattleSkill()
                {
                    CasterId = readyHero.Id,
                    TypeId = x.TypeId,
                    Kind = _staticDataService.HeroSkillFor(x.TypeId, readyHero.TypeId).Kind,
                    TargetType = _staticDataService.HeroSkillFor(x.TypeId, readyHero.TypeId).TargetType,
                    MaxCooldown = x.MaxCooldown 
                });
        }

        private IEnumerable<ScoredAction> GetScoredActions(IHero readyHero, IEnumerable<BattleSkill> readySkills)
        {
            foreach (BattleSkill skill in readySkills)
            {
                foreach (HeroSet heroSet in HeroSetsForSkill(skill, readyHero))
                {
                    float? score = CalculateScore(skill, heroSet);
                    if(!score.HasValue)
                        continue;

                    yield return new ScoredAction(readyHero, skill, heroSet.Targets, score.Value);
                }
            }
        }

        private float? CalculateScore(BattleSkill skill, HeroSet heroSet)
        {
            if (heroSet.Targets.IsNullOrEmpty())
                return null;
            
            return heroSet.Targets
                .Select(hero => CalculateScore(skill, hero))
                .Where(x=> x != null)
                .Sum();
        }

        private float? CalculateScore(BattleSkill skill, IHero hero)
        {
            List<ScoreFactor> scoreFactors = (
                from utilityFunction in _utilityFunctions
                where utilityFunction.AppliesTo(skill, hero)
                let input = utilityFunction.GetInput(skill, hero, _skillSolver)
                let score = utilityFunction.Score(input, hero)
                
                select new ScoreFactor(utilityFunction.Name, score))
                .ToList();
            
            _aiReporter.ReportDecisionDetails(skill, hero, scoreFactors);
            
            return scoreFactors.Select(x => x.Score).SumOrNull();
        }

        private IEnumerable<HeroSet> HeroSetsForSkill(BattleSkill skill, IHero readyHero)
        {
            var availableTargets = _targetPicker.AvailableTargetsFor(readyHero.Id, skill.TargetType);

            if (skill.IsSingleTarget)
            {
                foreach (string targetId in availableTargets)
                    yield return new HeroSet(_heroRegistry.GetHero(targetId));

                yield break;
            }

            yield return new HeroSet(availableTargets.Select(_heroRegistry.GetHero));
        }
    }
}