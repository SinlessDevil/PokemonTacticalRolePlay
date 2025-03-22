using Services.Battle;

namespace Services.Skills
{
    public interface ISkillSolver
    {
        void ProcessHeroAction(HeroAction heroAction);
        void SkillDelaysTick();
    }
}