using Services.Battle;
using StaticData.Skills;

namespace Services.Skills
{
    public interface ISkillSolver
    {
        void ProcessHeroAction(HeroAction heroAction);
        void SkillDelaysTick();
        float CalculateSkillValue(string skillCasterId, SkillTypeId skillTypeId, string targetId);
    }
}