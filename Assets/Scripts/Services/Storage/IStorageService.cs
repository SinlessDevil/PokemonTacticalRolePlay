using System;

namespace Services.Storage
{
    public interface IStorageService
    {
        event Action<Currency> ChangedCurrencyEvent;
        void AddCurrency(CurrencyType currencyType, float value);
        void SubstractCurrency(CurrencyType currencyType, float value);
        Currency GetCurrency(CurrencyType currencyType);
    }
}