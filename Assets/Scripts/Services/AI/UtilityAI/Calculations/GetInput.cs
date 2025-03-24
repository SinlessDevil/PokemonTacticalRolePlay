using Logic.Heroes;

namespace Services.AI.UtilityAI.Calculations
{
    public static class GetInput
    {
        public static float PercentageDamage(BattleSkill skill, IHero target) =>
            target.State.MaxHp;
    }
}