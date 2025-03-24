using System;
using Logic.Heroes;

namespace Services.AI.UtilityAI
{
    public class UtilityFunction : IUtilityFunction
    {
        private readonly Func<BattleSkill, IHero, bool> _appliesTo;
        private readonly Func<BattleSkill, IHero, float> _getInput;
        private readonly Func<float, IHero, float> _score;
        private readonly string _name;

        public UtilityFunction(
            Func<BattleSkill, IHero, bool> appliesTo,
            Func<BattleSkill, IHero, float> getInput,
            Func<float, IHero, float> score,
            string name)
        {
            _appliesTo = appliesTo;
            _getInput = getInput;
            _score = score;
            _name = name;
        }
        
        public string Name { get; set; }

        public bool AppliesTo(BattleSkill skill, IHero hero) => _appliesTo(skill, hero);

        public float GetInput(BattleSkill skill, IHero hero) => _getInput(skill, hero);

        public float Score(float input, IHero hero) => _score(input, hero);
    }
}