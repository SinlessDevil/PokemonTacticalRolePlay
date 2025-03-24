using StaticData.Skills;

namespace Services.AI.UtilityAI
{
    public class BattleSkill
    {
        public string CasterId;
        public SkillTypeId TypeId;
        public SkillKind Kind;
        public TargetType TargetType;
        public bool IsSingleTarget => TargetType is TargetType.Ally or TargetType.Enemy or TargetType.Self;
    }
}