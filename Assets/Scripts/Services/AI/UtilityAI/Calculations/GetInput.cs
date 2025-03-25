using System.Linq;
using Logic.Heroes;
using Services.Skills;

namespace Services.AI.UtilityAI.Calculations
{
    public static class GetInput
    {
        private const float True = 1f;
        private const float False = 0f;
        
        public static float PercentageDamage(BattleSkill skill, IHero target, ISkillSolver skillSolver)
        {
            var damage = PotentialDamage(skill, target, skillSolver);
            return damage / target.State.MaxHp;
        }
        
        public static float KillingBlow(BattleSkill skill, IHero target, ISkillSolver skillSolver)
        {
            var damage = PotentialDamage(skill, target, skillSolver);
            return damage >= target.State.CurrentHp ? True : False;
        }

        public static float HpPercentage(BattleSkill skill, IHero target, ISkillSolver skillSolver) => 
            target.State.CurrentHp / target.State.MaxHp;
        
        public static float HealPercentage(BattleSkill skill, IHero target, ISkillSolver skillSolver) =>
            skillSolver.CalculateSkillValue(skill.CasterId,skill.TypeId, target.Id);

        public static float InitiativeBurn(BattleSkill skill, IHero target, ISkillSolver skillSolver)
        {
            var initiativeBurn = skillSolver.CalculateSkillValue(skill.CasterId,skill.TypeId, target.Id);
            return initiativeBurn / target.State.MaxInitiative;
        }
        
        private static float PotentialDamage(BattleSkill skill, IHero target, ISkillSolver skillSolver) =>
            skillSolver.CalculateSkillValue(skill.CasterId,skill.TypeId, target.Id);

        public static float TargetUltimateIsReady(BattleSkill skill, IHero target, ISkillSolver skillSolver) => 
            target.State.SkillStates.Last().IsReady
            ? True
            : False;
        
        public static float WillDieNextTurn(BattleSkill skill, IHero target, ISkillSolver skillSolver) => 
            target.State.Damage >= target.State.CurrentHp ? True : False;

        public static float TargetInitiativePercentage(BattleSkill skill, IHero target, ISkillSolver skillSolver) =>
            target.State.CurrentInitiative / target.State.MaxInitiative;
    }
}