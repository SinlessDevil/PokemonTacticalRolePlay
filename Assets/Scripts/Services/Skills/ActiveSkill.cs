using System.Collections.Generic;
using StaticData.Skills;

namespace Services.Skills
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