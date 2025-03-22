using System.Collections.Generic;

namespace Logic.Heroes
{
    public class HeroState
    {
        public float MaxHp;
        public float CurrentHp;
        public float Damage;
        public float Armor;
        public float CurrentInitiative;
        public float MaxInitiative;

        public List<SkillState> SkillStates;

        public void ModifyInitiative(float value)
        {
            if (CurrentInitiative <= MaxInitiative)
                CurrentInitiative += value;
        }
    }
}