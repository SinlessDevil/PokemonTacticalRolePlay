using Logic.Heroes;
using Services.HeroRegistry;

namespace Services.Initiative
{
    public class InitiativeService : IInitiativeService
    {
        private const int InitiativeTickValue = 3;

        private readonly IHeroRegistry _heroRegistry;

        public InitiativeService(IHeroRegistry heroRegistry)
        {
            _heroRegistry = heroRegistry;
        }

        public void ReplenishInitiativeTick()
        {
            foreach (HeroBehaviour hero in _heroRegistry.AllActiveHeroes())
            {
                hero.SwitchNextTurnPointer(false);
                hero.State.ModifyInitiative(InitiativeTickValue);
            }
        }

        public bool HeroIsReadyOnNextTick()
        {
            foreach (HeroBehaviour hero in _heroRegistry.AllActiveHeroes())
            {
                if (hero.State.CurrentInitiative + InitiativeTickValue >= hero.State.MaxInitiative)
                {
                    hero.SwitchNextTurnPointer(true);
                    return true;
                }
            }

            return false;
        }
    }
}