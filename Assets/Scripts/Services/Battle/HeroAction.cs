using System.Collections.Generic;
using CodeBase.Gameplay.Heroes;
using CodeBase.StaticData.Skills;

namespace CodeBase.Gameplay.Battle
{
    public class HeroAction
    {
        public IHero Caster;
        public List<string> TargetIds;
        public SkillTypeId Skill;
        public SkillKind SkillKind;
    }
}