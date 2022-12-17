using DotCache.ValueObjects;

namespace DotCache;

public interface IUnitCurrencyDataProvider
{
    protected void RegisterCurrencies();

    protected void RegisterCurrency(string currencyCode, int numericCurrencyCode, int decimalPlaces)
    {
        UnitCurrency.CurrencyRegister(currencyCode, numericCurrencyCode, decimalPlaces);
    }

    protected void RegisterCountry(string countryCode, string currencyCode)
    {
        UnitCurrency.RegisterCountry(countryCode, UnitCurrency.Of(currencyCode));
    }
}