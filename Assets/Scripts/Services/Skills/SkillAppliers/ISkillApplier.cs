using StaticData.Skills;

namespace Services.Skills.SkillApplier
{
    public interface ISkillApplier
    {
        void WarmUp();
        void ApplySkill(ActiveSkill activeSkill);
        SkillKind SkillKind { get; }
        float CalculateSkillValue(string casterId, SkillTypeId skillTypeId, string targetId);
    }
}