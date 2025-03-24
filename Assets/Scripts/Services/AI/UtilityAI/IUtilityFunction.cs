using Logic.Heroes;
using Services.Skills;

namespace Services.AI.UtilityAI
{
    public interface IUtilityFunction
    {
        bool AppliesTo(BattleSkill skill, IHero hero);
        float GetInput(BattleSkill skill, IHero hero, ISkillSolver skillSolver);
        float Score(float input, IHero hero);
        string Name { get; set; }
    }
}