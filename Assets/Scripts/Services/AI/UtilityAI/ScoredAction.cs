using System.Collections.Generic;
using System.Linq;
using Logic.Heroes;
using Services.Battle;
using StaticData.Skills;

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

        public override string ToString()
        {
            string skillCategory = "other";
            if (SkillKind is SkillKind.Damage)
                skillCategory = "dmg";
            
            if(SkillKind is SkillKind.Heal)
                skillCategory = "heal";
            
            if(SkillKind is SkillKind.InitiativeBurn)
                skillCategory = "initiative burn";
            
            return $"{skillCategory}: {Skill} targets: {TargetIds.Count} score: {Score:0.00}";
        }
    }
}