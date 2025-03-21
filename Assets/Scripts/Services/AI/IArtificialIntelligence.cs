using CodeBase.Gameplay.Battle;
using CodeBase.Gameplay.Heroes;

namespace CodeBase.Gameplay.AI
{
    public interface IArtificialIntelligence
    {
        HeroAction MakeBestDecision(IHero readyHero);
    }
}