using Logic.Heroes;
using StaticData.Skills;

namespace Services.AI.UtilityAI.Calculations
{
    public static class When
    {
        public static bool SkillIsDamage(BattleSkill skill, IHero hero) =>
            skill.Kind == SkillKind.Damage;

        public static bool SkillIsHeal(BattleSkill skill, IHero hero) =>
            skill.Kind == SkillKind.Heal;

        public static bool SkillIsInitiativeBurn(BattleSkill skill, IHero hero) =>
            skill.Kind == SkillKind.InitiativeBurn;
        
        public static bool SkillIsBasicAttack(BattleSkill skill, IHero hero) =>
            skill.Kind == SkillKind.Damage && skill.MaxCooldown == 0;
    }
}