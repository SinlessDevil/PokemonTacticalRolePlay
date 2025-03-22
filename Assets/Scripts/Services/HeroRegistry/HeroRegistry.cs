using System.Collections.Generic;
using System.Linq;
using Logic.Heroes;

namespace Services.HeroRegistry
{
    public class HeroRegistry : IHeroRegistry
    {
        public List<string> PlayerTeam { get; } = new();
        public List<string> EnemyTeam { get; } = new();
        public List<string> AllIds { get; private set; } = new();

        public Dictionary<string, HeroBehaviour> All { get; } = new();

        public void RegisterPlayerTeamHero(HeroBehaviour hero)
        {
            if (!PlayerTeam.Contains(hero.Id))
                PlayerTeam.Add(hero.Id);

            All[hero.Id] = hero;

            UpdateCashes();
        }

        public void RegisterEnemyTeamHero(HeroBehaviour hero)
        {
            if (!EnemyTeam.Contains(hero.Id))
                EnemyTeam.Add(hero.Id);

            All[hero.Id] = hero;

            UpdateCashes();
        }

        public void Unregister(string heroId)
        {
            if (PlayerTeam.Contains(heroId))
                PlayerTeam.Remove(heroId);

            if (EnemyTeam.Contains(heroId))
                EnemyTeam.Remove(heroId);

            if (All.ContainsKey(heroId))
                All.Remove(heroId);

            UpdateCashes();
        }

        public bool IsAlive(string id) => All.ContainsKey(id);

        public HeroBehaviour GetHero(string id)
        {
            return All.TryGetValue(id, out HeroBehaviour heroBehaviour)
                ? heroBehaviour
                : null;
        }

        public IEnumerable<HeroBehaviour> AllActiveHeroes() =>
            All.Values;

        public IEnumerable<string> AlliesOf(string heroId)
        {
            if (PlayerTeam.Contains(heroId))
                return PlayerTeam;

            return EnemyTeam;
        }

        public IEnumerable<string> EnemiesOf(string heroId)
        {
            if (PlayerTeam.Contains(heroId))
                return EnemyTeam;

            return PlayerTeam;
        }

        public void CleanUp()
        {
            PlayerTeam.Clear();
            EnemyTeam.Clear();
            All.Clear();

            AllIds.Clear();
        }

        private void UpdateCashes()
        {
            AllIds = All.Keys.ToList();
        }
    }
}