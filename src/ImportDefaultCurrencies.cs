using System.Text.RegularExpressions;

namespace DotCache;

public class ImportDefaultCurrencies : IUnitCurrencyDataProvider
{
    private static readonly Regex CurrencyRegex = new("([A-Z]{3}),(-1|[0-9]{1,3}),(-1|[0-9]|[1-2][0-9]|30) *(#.*)?"); 
    private static readonly Regex CountryRegex = new("([A-Z]{2}),([A-Z]{3}) *(#.*)?"); 
    
    public void RegisterCurrencies()
    {
        throw new NotImplementedException();
    }
}