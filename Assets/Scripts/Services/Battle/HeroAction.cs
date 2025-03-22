using System.Collections.Generic;
using Logic.Heroes;
using StaticData.Skills;

namespace Services.Battle
{
    public class HeroAction
    {
        public IHero Caster;
        public List<string> TargetIds;
        public SkillTypeId Skill;
        public SkillKind SkillKind;
    }
}