using System.Collections.Generic;
using CodeBase.Gameplay.Heroes;

namespace CodeBase.Gameplay.HeroRegistry
{
    public interface IHeroRegistry
    {
        void RegisterFirstTeamHero(HeroBehaviour hero);
        void RegisterSecondTeamHero(HeroBehaviour hero);
        void CleanUp();
        void Unregister(string heroId);
        List<string> FirstTeam { get; }
        List<string> SecondTeam { get; }
        Dictionary<string, HeroBehaviour> All { get; }
        List<string> AllIds { get; }
        HeroBehaviour GetHero(string id);
        IEnumerable<HeroBehaviour> AllActiveHeroes();
        IEnumerable<string> AlliesOf(string heroId);
        IEnumerable<string> EnemiesOf(string heroId);
        bool IsAlive(string id);
    }
}