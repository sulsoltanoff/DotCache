using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace DotCache.ValueObjects;

public struct BiggerMoney : IEquatable<IBiggerMoney>
{
    private IEquatable<IBiggerMoney> _equatableImplementation;
    private UnitCurrency? Currency { get; }
    private decimal? Amount { get; }

    private static readonly Regex CodePatt = new("[+-]?[0-9]*[.]?[0-9]*");

    public BiggerMoney()
    {
        Currency = null;
        Amount = null;
    }

    public BiggerMoney([NotNull] UnitCurrency currency, [NotNull] decimal amount)
    {
        Currency = currency;
        Amount = amount;
    }
    
    public static bool Equals(BiggerMoney other)
    {
        throw new NotImplementedException();
    }

    public bool Equals(IBiggerMoney? other)
    {
        return _equatableImplementation.Equals(other);
    }

    public override bool Equals(object? obj)
    {
        return obj is BiggerMoney other && Equals(other);
    }
    
    public static bool operator ==(BiggerMoney left, BiggerMoney right)
    {
        return Equals(right);
    }

    public static bool operator !=(BiggerMoney left, BiggerMoney right)
    {
        return !(left == right);
    }

    public override int GetHashCode() => Currency.GetHashCode() ^ Amount.GetHashCode();

    public override string ToString() =>
        new StringBuilder().Append(Currency!.Value).Append(' ').Append(Amount.ToString()).ToString();
}