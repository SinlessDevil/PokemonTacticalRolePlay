using Extensions;
using StaticData.Heroes;
using StaticData.Skills;
using UnityEngine;

namespace Logic.Heroes
{
    public class SkillState
    {
        public SkillTypeId TypeId;
        public float Cooldown;
        public float MaxCooldown;
        public Color Color;

        public bool IsReady => Cooldown <= 0;

        public void PutOnCooldown()
        {
            Cooldown = MaxCooldown;
        }

        public void TickCooldown(float delta)
        {
            if (Cooldown > 0)
                Cooldown -= delta;
        }

        public static SkillState FromHeroSkill(HeroSkill heroSkill) =>
            new SkillState()
                .With(x => x.TypeId = heroSkill.TypeId)
                .With(x => x.Color = heroSkill.Color)
                .With(x => x.MaxCooldown = heroSkill.Cooldown);
    }
}