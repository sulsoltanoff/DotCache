using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DotCache.ValueObjects;

/// <summary>
/// A unit of currency.
/// This class represents a unit of currency such as the British Pound, Euro, US Dollar and etc.
/// </summary>
public readonly struct UnitCurrency : IEquatable<UnitCurrency>
{
    private string Code { get; }
    private ushort NumericCode { get; }
    private ushort DecimalPlaces { get; }

    private static readonly Regex CodePatt = new("[A-Z][A-Z][A-Z]");

    private static readonly ConcurrentDictionary<string, UnitCurrency> CurrenciesByCode = new();

    private static readonly ConcurrentDictionary<int, UnitCurrency> CurrenciesByNumericCode = new();

    private static readonly ConcurrentDictionary<string, UnitCurrency> CurrenciesByCountry = new();

    public static readonly UnitCurrency Dollar = Of("USD");
    public static readonly UnitCurrency Euro = Of("EUR");
    public static readonly UnitCurrency BritishPound = Of("GBR");
    public static readonly UnitCurrency Canadian = Of("CAD");
    public static readonly UnitCurrency Krona = Of("SEK");
    public static readonly UnitCurrency Australian = Of("AUD");
    public static readonly UnitCurrency Japanese = Of("JPY");
    public static readonly UnitCurrency Real = Of("BRL");
    public static readonly UnitCurrency SwissFranc = Of("CHF");

    public UnitCurrency(string code, ushort numericCode, ushort decimalPlaces)
    {
        Code = code;
        NumericCode = numericCode;
        DecimalPlaces = decimalPlaces;
    }

    public static UnitCurrency Of([NotNull] string currencyCode)
    {
        UnitCurrency currency = CurrenciesByCode[currencyCode];
        if (currency == null)
        {
            throw new CurrencyException($"Unknown currency '{currencyCode}'");
        }
        return currency;
    }

    public static UnitCurrency Of(CultureInfo locale)
    {
        UnitCurrency currency = CurrenciesByCountry[locale.DisplayName];
        if (currency == null) throw new CurrencyException($"No currency found for locale '{locale}'");
        return currency;
    }

    public static UnitCurrency CurrencyRegister(string currencyCode, int numericCode, int decimalPlace)
    {
        List<String> countryCodes = new();
        return CurrencyRegister(currencyCode, numericCode, decimalPlace, countryCodes);
    }

    public static UnitCurrency CurrencyRegister(
        [NotNull]string currencyCode, 
        int numericCode, 
        int decimalPlace, 
        [NotNull] List<string> countryCodes
        )
    {
        if (currencyCode.Length != 3) throw new CurrencyException("Invalid string code, must be length 3");
        
        if (CodePatt.IsMatch(currencyCode) == false)
            throw new CurrencyException("Invalid string code, must be ASCII upper-case letters");

        if (numericCode > 999) throw new CurrencyException("Invalid numeric code, must be a positive three-digit value");

        if (decimalPlace > 30) throw new CurrencyException("Invalid number of decimal places, must not exceed 30");

        UnitCurrency currency = new(currencyCode, (ushort)numericCode, (ushort)decimalPlace);
        if (CurrenciesByCode.ContainsKey(currencyCode) || CurrenciesByNumericCode.ContainsKey(numericCode))
            throw new CurrencyException($"Currency already registered: '{currencyCode}'");
        
        foreach (var cc in countryCodes.Where(cc => CurrenciesByCode.ContainsKey(cc)))
            throw new CurrencyException($"Currency already registered for country: '{cc}'");

        CurrenciesByCode.GetOrAdd(currencyCode, currency);
        if (numericCode >= 0) CurrenciesByNumericCode.GetOrAdd(numericCode, currency);

        foreach (var code in countryCodes)
        {
            RegisterCountry(code, currency);
        }

        return CurrenciesByCode[currencyCode];
    }

    public static void RegisterCountry(string countryCode, UnitCurrency currency)
    {
        CurrenciesByCountry.GetOrAdd(countryCode, currency);
    }

    public static List<UnitCurrency> GetRegistredCurrencies()
    {
        return new List<UnitCurrency>(CurrenciesByCode.Values);
    }

    public static List<string> GetRegistredCountries()
    {
        return new List<string>(CurrenciesByCountry.Keys);
    }

    public bool Equals(UnitCurrency other) => this.Code == other.Code;
    

    public override bool Equals(object obj)
    {
        return obj is UnitCurrency unitCurrency && Equals(unitCurrency);
    }

    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }

    public static bool operator ==(UnitCurrency left, UnitCurrency right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(UnitCurrency left, UnitCurrency right)
    {
        return !(left == right);
    }

    public override string ToString() => this.Code;
}