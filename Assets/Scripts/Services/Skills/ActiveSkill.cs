using System.Collections.Generic;
using CodeBase.StaticData.Skills;

namespace CodeBase.Gameplay.Skills
{
    public class ActiveSkill
    {
        public string CasterId;
        public List<string> TargetIds;

        public SkillTypeId Skill;
        public SkillKind Kind;
        public float DelayLeft;
    }
}