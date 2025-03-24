using System.Collections.Generic;
using Services.AI.UtilityAI.Calculations;

namespace Services.AI.UtilityAI
{
    public class Brains
    {
        private Convolution _convolution = new()
        {
            {When.SkillIsDamage, GetInput.PercentageDamage, Score.AsIs, "Damage Damage"},
        };
        
        public IEnumerable<IUtilityFunction> GetUnitilityFunctions()
        {
            return _convolution;
        }
    }
}