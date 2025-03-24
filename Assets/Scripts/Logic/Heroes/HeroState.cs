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
        public float HpPercentage => CurrentHp / MaxHp;
        public float InitiativePercentage => CurrentInitiative / MaxInitiative;

        public void ModifyInitiative(float value)
        {
            if (CurrentInitiative <= MaxInitiative)
                CurrentInitiative += value;
        }
    }
}