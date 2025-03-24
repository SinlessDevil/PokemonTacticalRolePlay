using System.Collections.Generic;
using System.Linq;
using Logic.Heroes;
using Services.Battle;

namespace Services.AI.UtilityAI
{
    public class ScoredAction : HeroAction
    {
        public float Score;

        public ScoredAction(IHero readyHero, BattleSkill skill, IEnumerable<IHero> heroSetTargets, float score)
        {
            Caster = readyHero;
            SkillKind = skill.Kind;
            Skill = skill.TypeId;
            TargetIds = heroSetTargets.Select(x => x.Id).ToList();
            Score = score;
        }
    }
}