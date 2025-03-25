using System;
using Services.PersistenceProgress;
using Services.SaveLoad;
using UnityEngine;

namespace Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IPersistenceProgressService _persistenceProgressService;
        private readonly ISaveLoadService _saveLoadService;

        public StorageService(
            IPersistenceProgressService persistenceProgressService, 
            ISaveLoadService saveLoadService)
        {
            _persistenceProgressService = persistenceProgressService;
            _saveLoadService = saveLoadService;
        }
        
        public event Action<Currency> ChangedCurrencyEvent;
        
        public void AddCurrency(CurrencyType currencyType, float value)
        {
            Currency currency = GetCurrency(currencyType);
            currency.Value += value;
            _saveLoadService.SaveProgress();
            ChangedCurrencyEvent?.Invoke(currency);
        }

        public void SubstractCurrency(CurrencyType currencyType, float value)
        {
            Currency currency = GetCurrency(currencyType);
            currency.Value = Mathf.Min(currency.Value - value, 0);
            _saveLoadService.SaveProgress();
            ChangedCurrencyEvent?.Invoke(currency);
        }
        
        public Currency GetCurrency(CurrencyType currencyType)
        {
            return _persistenceProgressService.PlayerData.PlayerResources.GetOrCreateCurrencySave(currencyType);
        }
    }
}