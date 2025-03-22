using Logic.Heroes;
using Services.HeroRegistry;

namespace Services.Cooldown
{
    public class CooldownService : ICooldownService
    {
        private readonly IHeroRegistry _heroRegistry;

        public CooldownService(IHeroRegistry heroRegistry)
        {
            _heroRegistry = heroRegistry;
        }

        public void CooldownTick(float deltaTime)
        {
            foreach (HeroBehaviour hero in _heroRegistry.AllActiveHeroes())
            foreach (SkillState skillState in hero.State.SkillStates)
                skillState.TickCooldown(deltaTime);
        }
    }
}