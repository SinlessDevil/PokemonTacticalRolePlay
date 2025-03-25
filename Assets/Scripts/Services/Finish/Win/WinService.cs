using System.Collections.Generic;
using Services.HeroRegistry;
using Services.Levels;
using Services.PersistenceProgress;
using Services.SaveLoad;
using Services.Storage;
using Services.Timer;
using Services.Window;
using UnityEditor;
using Window;
using Window.Finish.Win;

namespace Services.Finish.Win
{
    public class WinService : IWinService
    {
        private readonly IWindowService _windowService;
        private readonly ILevelService _levelService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPersistenceProgressService _persistenceProgressService;
        private readonly IHeroRegistry _heroRegistry;
        private readonly IStorageService _storageService;

        public WinService(
            IWindowService windowService, 
            ILevelService levelService,
            ISaveLoadService saveLoadService,
            IPersistenceProgressService persistenceProgressService,
            IHeroRegistry heroRegistry,
            IStorageService storageService)
        {
            _windowService = windowService;
            _levelService = levelService;
            _saveLoadService = saveLoadService;
            _persistenceProgressService = persistenceProgressService;
            _heroRegistry = heroRegistry;
            _storageService = storageService;
        }
        
        public void Win()
        {
            var currentGem = GetCurrencyGemText();
            var currentGold = GetCurrencyGoldText();
            var currentCount = GetCountStart();
            
            ApplyCurrency();
            CompleteLevel();
            CompleteTutor();
            
            SaveProgress();
            
            var window = _windowService.Open(WindowTypeId.Win);
            var winWindow = window.GetComponent<WinWindow>();
            winWindow.SetGems(currentGem);
            winWindow.SetGold(currentGold);
            winWindow.SetCountStars(currentCount);
            winWindow.Initialize();
            winWindow.ResetWindow();
            winWindow.OpenWindow(null);
        }

        public void BonusWin()
        {
            var currentGem = GetCurrencyGemText();
            var currentGold = GetCurrencyGoldText();
            var currentCount = GetCountStart();
            
            ApplyCurrency();
            CompleteLevel();
            CompleteTutor();
            
            SaveProgress();
            
            var window = _windowService.Open(WindowTypeId.Bonus);
            var winWindow = window.GetComponent<BonusWindow>();
            winWindow.SetGems(currentGem);
            winWindow.SetGold(currentGold);
            winWindow.SetCountStars(currentCount);
            winWindow.Initialize();
            winWindow.ResetWindow();
            winWindow.OpenWindow(null);
        }
        
        private void CompleteLevel()
        {
            _levelService.LevelsComplete();
        }

        private void CompleteTutor()
        {
            _persistenceProgressService.PlayerData.PlayerTutorialData.HasFirstCompleteLevel = true;
        }
        
        private void ApplyCurrency()
        {
            List<Currency> rewards = _levelService.GetCurrentLevelStaticData().Rewards;
            foreach (Currency reward in rewards)
            {
                _storageService.AddCurrency(reward);
            }
        }
        
        private string GetCurrencyGemText()
        {
            Currency currency = _levelService.GetCurrentLevelStaticData()
                .Rewards
                .Find(c => c.CurrencyType == CurrencyType.Gem);

            var value = currency != null ? currency.Value : 0; 
            
            return value.ToString();
        }
        
        private string GetCurrencyGoldText()
        {
            Currency currency = _levelService.GetCurrentLevelStaticData()
                .Rewards
                .Find(c => c.CurrencyType == CurrencyType.Gold);

            var value = currency != null ? currency.Value : 0;
            
            return value.ToString();
        }

        private int GetCountStart()
        {
            return _heroRegistry.PlayerTeam.Count;
        }
        
        private void SaveProgress()
        {
            _saveLoadService.SaveProgress();
        }
    }
}