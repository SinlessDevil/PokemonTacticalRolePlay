using System.Collections.Generic;
using Services.AI.UtilityAI.Calculations;

namespace Services.AI.UtilityAI
{
    public class Brains
    {
        private Convolution _convolution = new()
        {
            {When.SkillIsDamage, GetInput.PercentageDamage, Score.ScaleBy(100), "Basic Damage"},
            {When.SkillIsDamage, GetInput.KillingBlow, Score.IfTrueThen(+150f), "Killing Blow"},
            {When.SkillIsBasicAttack, GetInput.KillingBlow, Score.IfTrueThen(+30f), "Killing Blow with Basic Attack"},
            {When.SkillIsDamage, GetInput.HpPercentage, Score.ScaleByHpPercentage(50), "Focus Damage"},
            {When.SkillIsDamage, GetInput.InitiativeBurn, Score.ScaleBy(30), "Attack Low Initiative Target"},
            
            {When.SkillIsHeal, GetInput.HealPercentage, Score.CullByTargetHp, "Heal"},
            {When.SkillIsHeal, GetInput.HpPercentage, Score.ScaleByHpPercentageHigh(150), "Heal Low HP Ally"},
            {When.SkillIsHeal, GetInput.WillDieNextTurn, Score.ScaleBy(100), "Restoring health if I get killed"},
            
            {When.SkillIsInitiativeBurn, GetInput.InitiativeBurn, Score.CullByTargetInitiative(50f,0.25f), "Initiative Burn"},
            {When.SkillIsInitiativeBurn, GetInput.TargetUltimateIsReady, Score.IfTrueThen(+30), "Initiative Burn Ultimate Is Ready"},
            {When.SkillIsInitiativeBurn, GetInput.TargetInitiativePercentage, Score.ScaleBy(50), "Burn Fast Enemy Initiative"},
        };
        
        public IEnumerable<IUtilityFunction> GetUnitilityFunctions()
        {
            return _convolution;
        }
    }
}