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
            {When.SkillIsDamage, GetInput.HpPercentage, (hpPerc, _) => (1f - hpPerc) * 50, "Focus Damage"},

            {When.SkillIsHeal, GetInput.HealPercentage, Score.CullByTargetHp, "Heal"},
            
            {When.SkillIsInitiativeBurn, GetInput.InitiativeBurn, Score.CullByTargetInitiative(50f,0.25f), "Initiative Burn"},
            {When.SkillIsInitiativeBurn, GetInput.TargetUltimateIsReady, Score.IfTrueThen(+30), "Initiative Burn Ultimate Is Ready"},
        };
        
        public IEnumerable<IUtilityFunction> GetUnitilityFunctions()
        {
            return _convolution;
        }
    }
}