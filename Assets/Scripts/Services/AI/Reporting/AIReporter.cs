using System;
using System.Collections.Generic;
using System.Linq;
// using CodeBase.Gameplay.AI.Utility;
using CodeBase.Gameplay.Heroes;
using CodeBase.Gameplay.HeroRegistry;

namespace CodeBase.Gameplay.AI.Reporting
{
    public class AIReporter : IAIReporter
    {
        // private readonly IHeroRegistry _registry;
        public event Action<DecisionDetails> DecisionDetailsReported;

        public event Action<DecisionScores> DecisionScoresReported;
        //
        // public AIReporter(IHeroRegistry registry) => 
        //   _registry = registry;
        //
        // public void ReportDecisionDetails(BattleSkill skill, IHero target, List<ScoreFactor> scoreFactors)
        // {
        //   HeroBehaviour caster = _registry.GetHero(skill.CasterId);
        //   HeroBehaviour targetHero = _registry.GetHero(target.Id);
        //   DecisionDetails details = new DecisionDetails
        //   {
        //     CasterName = $"{caster.TypeId} [{caster.SlotNumber}]",
        //     TargetName = $"{targetHero.TypeId} [{targetHero.SlotNumber}]",
        //     SkillName = $"{skill.TypeId}",
        //     Scores = scoreFactors,
        //     FormattedLine = string.Join(Environment.NewLine, 
        //       scoreFactors.OrderByDescending(x => x.Score)
        //         .Select(x => x.ToString())
        //         .ToArray())
        //   };
        //   
        //   DecisionDetailsReported?.Invoke(details);
        // }
        //
        // public void ReportDecisionScores(IHero readyHero, List<ScoredAction> choices)
        // {
        //   HeroBehaviour caster = _registry.GetHero(readyHero.Id);
        //   DecisionScores scores = new DecisionScores
        //   {
        //     HeroName = $"{caster.TypeId} [{caster.SlotNumber}]",
        //     Choices = choices,
        //     FormattedLine = string.Join(Environment.NewLine, 
        //       choices.OrderByDescending(x => x.Score)
        //         .Select(x => x.ToString())
        //         .ToArray())
        //   };
        //
        //   DecisionScoresReported?.Invoke(scores);
        // }
    }
}