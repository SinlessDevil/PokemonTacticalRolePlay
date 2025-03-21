using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.Heroes;

namespace CodeBase.Gameplay.HeroRegistry
{
    public class HeroRegistry : IHeroRegistry
    {
        public List<string> FirstTeam { get; } = new();
        public List<string> SecondTeam { get; } = new();
        public List<string> AllIds { get; private set; } = new();

        public Dictionary<string, HeroBehaviour> All { get; } = new();

        public void RegisterFirstTeamHero(HeroBehaviour hero)
        {
            if (!FirstTeam.Contains(hero.Id))
                FirstTeam.Add(hero.Id);

            All[hero.Id] = hero;

            UpdateCashes();
        }

        public void RegisterSecondTeamHero(HeroBehaviour hero)
        {
            if (!SecondTeam.Contains(hero.Id))
                SecondTeam.Add(hero.Id);

            All[hero.Id] = hero;

            UpdateCashes();
        }

        public void Unregister(string heroId)
        {
            if (FirstTeam.Contains(heroId))
                FirstTeam.Remove(heroId);

            if (SecondTeam.Contains(heroId))
                SecondTeam.Remove(heroId);

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
            if (FirstTeam.Contains(heroId))
                return FirstTeam;

            return SecondTeam;
        }

        public IEnumerable<string> EnemiesOf(string heroId)
        {
            if (FirstTeam.Contains(heroId))
                return SecondTeam;

            return FirstTeam;
        }

        public void CleanUp()
        {
            FirstTeam.Clear();
            SecondTeam.Clear();
            All.Clear();

            AllIds.Clear();
        }

        private void UpdateCashes()
        {
            AllIds = All.Keys.ToList();
        }
    }
}