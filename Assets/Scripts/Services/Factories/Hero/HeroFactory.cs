using System;
using System.Linq;
using Services.Battle;
using Services.StaticData;
using StaticData.Heroes;
using UnityEngine;
using Extensions;
using Logic.Heroes;
using Zenject;

namespace Services.Factories.Hero
{
    public class HeroFactory : IHeroFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IInstantiator _instantiator;

        public HeroFactory(IStaticDataService staticDataService, IInstantiator instantiator)
        {
            _instantiator = instantiator;
            _staticDataService = staticDataService;
        }

        public HeroBehaviour CreateHeroAt(HeroTypeId heroTypeId, Slot slot, bool turned)
        {
            HeroConfig config = _staticDataService.HeroConfigFor(heroTypeId);
            HeroBehaviour hero = _instantiator
                .InstantiatePrefabForComponent<HeroBehaviour>(config.Prefab, slot.transform)
                .With(x => x.TypeId = heroTypeId)
                .With(x => x.Id = Guid.NewGuid().ToString());

            hero.InitializeWithState(
                new HeroState()
                    .With(x => x.MaxHp = config.Hp)
                    .With(x => x.CurrentHp = config.Hp)
                    .With(x => x.MaxInitiative = config.Initiative)
                    .With(x => x.CurrentInitiative = UnityEngine.Random.Range(0, config.Initiative))
                    .With(x => x.Armor = config.Armor)
                    .With(x => x.SkillStates = config.Skills.Select(SkillState.FromHeroSkill).ToList()),
                turned,
                slot.SlotNumber
            );

            hero.transform.rotation = Quaternion.Euler(slot.transform.rotation.eulerAngles.x, 
                slot.transform.rotation.eulerAngles.y, slot.transform.rotation.eulerAngles.z);
            
            return hero;
        }
    }
}