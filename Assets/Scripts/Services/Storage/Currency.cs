using System;

namespace Services.Storage
{
    [Serializable]
    public class Currency
    {
        public CurrencyType CurrencyType;
        public float Value;

        public Currency(CurrencyType currencyType)
        {
            CurrencyType = currencyType;
        }
    }
}