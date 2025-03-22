using Logic.Heroes;
using Services.Battle;

namespace Services.AI
{
    public interface IArtificialIntelligence
    {
        HeroAction MakeBestDecision(IHero readyHero);
    }
}