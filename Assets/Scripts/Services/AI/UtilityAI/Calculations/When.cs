using Logic.Heroes;
using StaticData.Skills;

namespace Services.AI.UtilityAI.Calculations
{
    public static class When
    {
        public static bool SkillIsDamage(BattleSkill skill, IHero hero) =>
            skill.Kind == SkillKind.Damage;
    }
}