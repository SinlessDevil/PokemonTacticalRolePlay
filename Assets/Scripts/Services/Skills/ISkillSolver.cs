using CodeBase.Gameplay.Battle;

namespace CodeBase.Gameplay.Skills
{
    public interface ISkillSolver
    {
        void ProcessHeroAction(HeroAction heroAction);
        void SkillDelaysTick();
    }
}