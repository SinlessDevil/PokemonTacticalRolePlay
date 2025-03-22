using System.Collections.Generic;
using Logic.Heroes;

namespace Services.HeroRegistry
{
    public interface IHeroRegistry
    {
        void RegisterPlayerTeamHero(HeroBehaviour hero);
        void RegisterEnemyTeamHero(HeroBehaviour hero);
        void CleanUp();
        void Unregister(string heroId);
        List<string> PlayerTeam { get; }
        List<string> EnemyTeam { get; }
        Dictionary<string, HeroBehaviour> All { get; }
        List<string> AllIds { get; }
        HeroBehaviour GetHero(string id);
        IEnumerable<HeroBehaviour> AllActiveHeroes();
        IEnumerable<string> AlliesOf(string heroId);
        IEnumerable<string> EnemiesOf(string heroId);
        bool IsAlive(string id);
    }
}