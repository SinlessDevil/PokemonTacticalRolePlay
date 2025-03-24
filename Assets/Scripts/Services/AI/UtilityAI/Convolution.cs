using System;
using System.Collections.Generic;
using Logic.Heroes;
using Services.Skills;

namespace Services.AI.UtilityAI
{
    public class Convolution : List<UtilityFunction>
    {
        public void Add(
            Func<BattleSkill, IHero, bool> appliesTo, 
            Func<BattleSkill, IHero, ISkillSolver, float> getInput, 
            Func<float, IHero, float> score, 
            string name)
        {
            Add(new UtilityFunction(appliesTo, getInput, score, name));
        }
    }
}