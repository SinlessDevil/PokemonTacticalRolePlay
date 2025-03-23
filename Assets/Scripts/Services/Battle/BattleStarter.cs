using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Heroes;
using Services.Factories.Hero;
using Services.HeroRegistry;
using Services.Levels;
using StaticData.Heroes;
using UnityEngine;

namespace Services.Battle
{
    public class BattleStarter : IBattleStarter
    {
        private const int MaxHeroesCount = 3;
        
        private readonly IHeroFactory _heroFactory;
        private readonly IHeroRegistry _heroRegistry;
        private readonly IBattleConductor _battleConductor;
        private readonly ILevelService _levelService;

        private List<HeroTypeId> _playerHeroTypeIds = new();
        private SlotSetupBehaviour _slotSetup;
        
        public BattleStarter(
            IHeroFactory heroFactory, 
            IHeroRegistry heroRegistry, 
            IBattleConductor battleConductor,
            ILevelService levelService)
        {
            _heroFactory = heroFactory;
            _heroRegistry = heroRegistry;
            _battleConductor = battleConductor;
            _levelService = levelService;
        }

        public int GetMaxHeroesCount => MaxHeroesCount;

        public void SetUpSlotSetup(SlotSetupBehaviour slotSetup)
        {
            _slotSetup = slotSetup;
        }

        public void StartRandomBattle()
        {
            Debug.Log("Start");
            
            SetupPlayerTeam(_slotSetup);
            SetupEnemyTeam(_slotSetup);

            _battleConductor.Start();
        }

        public void CleanUp()
        {
            _playerHeroTypeIds.Clear();
            _slotSetup = null;
        }
        
        public HeroTypeId RandomHeroTypeId()
        {
            List<HeroTypeId> availableTypeIds = Enum.GetValues(typeof(HeroTypeId))
                .Cast<HeroTypeId>()
                .Except(new[] { HeroTypeId.Unknown })
                .Except(_playerHeroTypeIds)
                .ToList();

            if (availableTypeIds.Count == 0)
                throw new InvalidOperationException("All available heroes have already been selected.");

            HeroTypeId selectedHero = availableTypeIds[UnityEngine.Random.Range(0, availableTypeIds.Count)];
            _playerHeroTypeIds.Add(selectedHero);

            return selectedHero;
        }
        
        private void SetupPlayerTeam(SlotSetupBehaviour slotSetup)
        {
            foreach (Slot slot in slotSetup.FirstTeamSlots)
            {
                int index = slotSetup.FirstTeamSlots.IndexOf(slot);
                HeroBehaviour hero = _heroFactory.CreateHeroAt(_playerHeroTypeIds[index], slot, slot.Turned);
                _heroRegistry.RegisterPlayerTeamHero(hero);
            }
        }

        private void SetupEnemyTeam(SlotSetupBehaviour slotSetup)
        {
            var listEnemy = _levelService.GetCurrentLevelStaticData().Enemies;
            
            foreach (Slot slot in slotSetup.SecondTeamSlots)
            {
                int index = slotSetup.SecondTeamSlots.IndexOf(slot);
                HeroBehaviour hero = _heroFactory.CreateHeroAt(listEnemy[index], slot, slot.Turned);
                _heroRegistry.RegisterEnemyTeamHero(hero);
            }
        }
    }
}