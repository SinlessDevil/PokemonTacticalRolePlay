using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.Factory;
using CodeBase.Gameplay.Heroes;
using CodeBase.Gameplay.HeroRegistry;
using CodeBase.StaticData.Heroes;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay.Battle
{
    public class BattleStarter : IBattleStarter
    {
        private readonly IHeroFactory _heroFactory;
        private readonly IHeroRegistry _heroRegistry;
        private readonly IBattleConductor _battleConductor;

        public BattleStarter(IHeroFactory heroFactory, IHeroRegistry heroRegistry, IBattleConductor battleConductor)
        {
            _heroFactory = heroFactory;
            _heroRegistry = heroRegistry;
            _battleConductor = battleConductor;
        }

        public void StartRandomBattle(SlotSetupBehaviour slotSetup)
        {
            SetupFirstTeam(slotSetup);
            SetupSecondTeam(slotSetup);

            _battleConductor.Start();
        }

        private void SetupFirstTeam(SlotSetupBehaviour slotSetup)
        {
            foreach (Slot slot in slotSetup.FirstTeamSlots)
            {
                HeroBehaviour hero = _heroFactory.CreateHeroAt(RandomHeroTypeId(), slot, slot.Turned);
                _heroRegistry.RegisterFirstTeamHero(hero);
            }
        }

        private void SetupSecondTeam(SlotSetupBehaviour slotSetup)
        {
            foreach (Slot slot in slotSetup.SecondTeamSlots)
            {
                HeroBehaviour hero = _heroFactory.CreateHeroAt(RandomHeroTypeId(), slot, slot.Turned);
                _heroRegistry.RegisterSecondTeamHero(hero);
            }
        }

        private static HeroTypeId RandomHeroTypeId()
        {
            List<HeroTypeId> typeIds = Enum.GetValues(typeof(HeroTypeId))
                .Cast<HeroTypeId>()
                .Except(new[] { HeroTypeId.Unknown })
                .ToList();

            return typeIds[Random.Range(0, typeIds.Count)];
        }
    }
}