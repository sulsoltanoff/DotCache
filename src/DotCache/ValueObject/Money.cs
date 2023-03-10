using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace DotCache.ValueObject;

/// <summary>Represents Money, an amount defined in a specific Currency.</summary>
/// <remarks>
/// The <see cref="Money"/> structure allows development of applications that handle
/// various types of Currency. Money will hold the <see cref="Currency"/> and Amount of money,
/// and ensure that two different currencies cannot be added or subtracted to each other.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public partial struct Money : IEquatable<Money>
{
    /// <summary>Initializes a new instance of the <see cref="Money"/> struct, based on the current culture.</summary>
    /// <param name="amount">The Amount of money as <see langword="decimal"/>.</param>
    /// <remarks>The amount will be rounded to the number of decimal digits of the specified currency
    /// (<see cref="ValueObject.Currency.DecimalDigits"/>). As rounding mode, MidpointRounding.ToEven is used
    /// (<see cref="System.MidpointRounding"/>). The behavior of this method follows IEEE Standard 754, section 4. This
    /// kind of rounding is sometimes called rounding to nearest, or banker's rounding. It minimizes rounding errors that
    /// result from consistently rounding a midpoint value in a single direction.</remarks>
    public Money(decimal amount)
        : this(amount, Currency.CurrentCurrency)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Money"/> struct, based on a ISO 4217 Currency code.</summary>
    /// <param name="amount">The Amount of money as <see langword="decimal"/>.</param>
    /// <param name="code">A ISO 4217 Currency code, like EUR or USD.</param>
    /// <remarks>The amount will be rounded to the number of decimal digits of the specified currency
    /// (<see cref="ValueObject.Currency.DecimalDigits"/>). As rounding mode, MidpointRounding.ToEven is used
    /// (<see cref="System.MidpointRounding"/>). The behavior of this method follows IEEE Standard 754, section 4. This
    /// kind of rounding is sometimes called rounding to nearest, or banker's rounding. It minimizes rounding errors that
    /// result from consistently rounding a midpoint value in a single direction.</remarks>
    public Money(decimal amount, string code)
        : this(amount, Currency.FromCode(code))
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Money"/> struct, based on the current culture.</summary>
    /// <param name="amount">The Amount of money as <see langword="decimal"/>.</param>
    /// <param name="rounding">The rounding mode.</param>
    /// <remarks>The amount will be rounded to the number of decimal digits of the specified currency
    /// (<see cref="ValueObject.Currency.DecimalDigits"/>).</remarks>
    public Money(decimal amount, MidpointRounding rounding)
        : this(amount, Currency.CurrentCurrency, rounding)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Money"/> struct.</summary>
    /// <param name="amount">The Amount of money as <see langword="decimal"/>.</param>
    /// <param name="currency">The Currency of the money.</param>
    /// <remarks>The amount will be rounded to the number of decimal digits of the specified currency
    /// (<see cref="ValueObject.Currency.DecimalDigits"/>). As rounding mode, MidpointRounding.ToEven is used
    /// (<see cref="System.MidpointRounding"/>). The behavior of this method follows IEEE Standard 754, section 4. This
    /// kind of rounding is sometimes called rounding to nearest, or banker's rounding. It minimizes rounding errors that
    /// result from consistently rounding a midpoint value in a single direction.</remarks>
    public Money(decimal amount, Currency currency)
        : this(amount, currency, MidpointRounding.ToEven)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Money"/> struct, based on a ISO 4217 Currency code.</summary>
    /// <param name="amount">The Amount of money as <see langword="decimal"/>.</param>
    /// <param name="code">A ISO 4217 Currency code, like EUR or USD.</param>
    /// <param name="rounding">The rounding mode.</param>
    /// <remarks>The amount will be rounded to the number of decimal digits of the specified currency
    /// (<see cref="ValueObject.Currency.DecimalDigits"/>).</remarks>
    public Money(decimal amount, string code, MidpointRounding rounding)
        : this(amount, Currency.FromCode(code), rounding)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Money"/> struct.</summary>
    /// <param name="amount">The Amount of money as <see langword="decimal"/>.</param>
    /// <param name="currency">The Currency of the money.</param>
    /// <param name="rounding">The rounding mode.</param>
    /// <remarks>The amount will be rounded to the number of decimal digits of the specified currency
    /// (<see cref="ValueObject.Currency.DecimalDigits"/>).</remarks>
    public Money(decimal amount, Currency currency, MidpointRounding rounding)
        : this()
    {
        Currency = currency;
        Amount = Round(amount, currency, rounding);
    }

    // int, uint ([CLSCompliant(false)]) // auto-casting to decimal so not needed

    /// <summary>Initializes a new instance of the <see cref="Money"/> struct, based on the current culture.</summary>
    /// <param name="amount">The Amount of money as <see langword="double"/> or <see langword="float"/> (float is implicitly
    /// casted to double).</param>
    /// <remarks>This constructor will first convert to decimal by rounding the value to 15 significant digits using rounding
    /// to nearest. This is done even if the number has more than 15 digits and the less significant digits are zero.
    /// <para>The amount will be rounded to the number of decimal digits of the specified currency
    /// (<see cref="ValueObject.Currency.DecimalDigits"/>). As rounding mode, MidpointRounding.ToEven is used
    /// (<see cref="System.MidpointRounding"/>). The behavior of this method follows IEEE Standard 754, section 4. This
    /// kind of rounding is sometimes called rounding to nearest, or banker's rounding. It minimizes rounding errors that
    /// result from consistently rounding a midpoint value in a single direction.</para></remarks>
    public Money(double amount)
        : this((decimal)amount)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Money"/> struct, based on a ISO 4217 Currency code.</summary>
    /// <param name="amount">The Amount of money as <see langword="double"/> or <see langword="float"/> (float is implicitly
    /// casted to double).</param>
    /// <param name="code">A ISO 4217 Currency code, like EUR or USD.</param>
    /// <remarks>This constructor will first convert to decimal by rounding the value to 15 significant digits using rounding
    /// to nearest. This is done even if the number has more than 15 digits and the less significant digits are zero.
    /// <para>The amount will be rounded to the number of decimal digits of the specified currency
    /// (<see cref="ValueObject.Currency.DecimalDigits"/>). As rounding mode, MidpointRounding.ToEven is used
    /// (<see cref="System.MidpointRounding"/>). The behavior of this method follows IEEE Standard 754, section 4. This
    /// kind of rounding is sometimes called rounding to nearest, or banker's rounding. It minimizes rounding errors that
    /// result from consistently rounding a midpoint value in a single direction.</para></remarks>
    public Money(double amount, string code)
        : this((decimal)amount, Currency.FromCode(code))
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Money"/> struct.</summary>
    /// <param name="amount">The Amount of money as <see langword="double"/> or <see langword="float"/> (float is implicitly
    /// casted to double).</param>
    /// <param name="currency">The Currency of the money.</param>
    /// <remarks>This constructor will first convert to decimal by rounding the value to 15 significant digits using rounding
    /// to nearest. This is done even if the number has more than 15 digits and the less significant digits are zero.
    /// <para>The amount will be rounded to the number of decimal digits of the specified currency
    /// (<see cref="ValueObject.Currency.DecimalDigits"/>). As rounding mode, MidpointRounding.ToEven is used
    /// (<see cref="System.MidpointRounding"/>). The behavior of this method follows IEEE Standard 754, section 4. This
    /// kind of rounding is sometimes called rounding to nearest, or banker's rounding. It minimizes rounding errors that
    /// result from consistently rounding a midpoint value in a single direction.</para></remarks>
    public Money(double amount, Currency currency)
        : this((decimal)amount, currency)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Money"/> struct.</summary>
    /// <param name="amount">The Amount of money as <see langword="double"/> or <see langword="float"/> (float is implicitly
    /// casted to double).</param>
    /// <param name="currency">The Currency of the money.</param>
    /// <param name="rounding">The rounding mode.</param>
    /// <remarks>This constructor will first convert to decimal by rounding the value to 15 significant digits using rounding
    /// to nearest. This is done even if the number has more than 15 digits and the less significant digits are zero.
    /// <para>The amount will be rounded to the number of decimal digits of the specified currency
    /// (<see cref="ValueObject.Currency.DecimalDigits"/>).</para></remarks>
    public Money(double amount, Currency currency, MidpointRounding rounding)
        : this((decimal)amount, currency, rounding)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Money"/> struct, based on the current culture.</summary>
    /// <param name="amount">The Amount of money as <see langword="long"/>, <see langword="int"/>, <see langword="short"/> or<see cref="byte"/>.</param>
    /// <remarks>The integral types are implicitly converted to long and the result evaluates to decimal. Therefore you can
    /// initialize a Money object using an integer literal, without the suffix, as follows:
    /// <code>Money money = new Money(10, "EUR");</code></remarks>
    public Money(long amount)
        : this((decimal)amount)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Money"/> struct, based on a ISO 4217 Currency code.</summary>
    /// <param name="amount">The Amount of money as <see langword="long"/>, <see langword="int"/>, <see langword="short"/> or<see cref="byte"/>.</param>
    /// <param name="code">A ISO 4217 Currency code, like EUR or USD.</param>
    /// <remarks>The integral types are implicitly converted to long and the result evaluates to decimal. Therefore you can
    /// initialize a Money object using an integer literal, without the suffix, as follows:
    /// <code>Money money = new Money(10, "EUR");</code></remarks>
    public Money(long amount, string code)
        : this((decimal)amount, Currency.FromCode(code))
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Money"/> struct.</summary>
    /// <param name="amount">The Amount of money as <see langword="long"/>, <see langword="int"/>, <see langword="short"/> or<see cref="byte"/>.</param>
    /// <param name="currency">The Currency of the money.</param>
    /// <remarks>The integral types are implicitly converted to long and the result evaluates to decimal. Therefore you can
    /// initialize a Money object using an integer literal, without the suffix, as follows:
    /// <code>Money money = new Money(10, "EUR");</code></remarks>
    public Money(long amount, Currency currency)
        : this((decimal)amount, currency)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Money"/> struct, based on the current culture.</summary>
    /// <param name="amount">The Amount of money as <see langword="ulong"/>, <see langword="uint"/>, <see langword="ushort"/>
    /// or <see cref="byte"/>.</param>
    /// <remarks>The integral types are implicitly converted to long and the result evaluates to decimal. Therefore you can
    /// initialize a Money object using an integer literal, without the suffix, as follows:
    /// <code>Money money = new Money(10, "EUR");</code></remarks>
    [CLSCompliant(false)]
    public Money(ulong amount)
        : this((decimal)amount)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Money"/> struct, based on a ISO 4217 Currency code.</summary>
    /// <param name="amount">The Amount of money as <see langword="ulong"/>, <see langword="uint"/>, <see langword="ushort"/>
    /// or <see cref="byte"/>.</param>
    /// <param name="code">A ISO 4217 Currency code, like EUR or USD.</param>
    /// <remarks>The integral types are implicitly converted to long and the result evaluates to decimal. Therefore you can
    /// initialize a Money object using an integer literal, without the suffix, as follows:
    /// <code>Money money = new Money(10, "EUR");</code></remarks>
    [CLSCompliant(false)]
    public Money(ulong amount, string code)
        : this((decimal)amount, Currency.FromCode(code))
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Money"/> struct.</summary>
    /// <param name="amount">The Amount of money as <see langword="ulong"/>, <see langword="uint"/>, <see langword="ushort"/>
    /// or <see cref="byte"/>.</param>
    /// <param name="currency">The Currency of the money.</param>
    /// <remarks>The integral types are implicitly converted to long and the result evaluates to decimal. Therefore you can
    /// initialize a Money object using an integer literal, without the suffix, as follows:
    /// <code>Money money = new Money(10, "EUR");</code></remarks>
    [CLSCompliant(false)]
    public Money(ulong amount, Currency currency)
        : this((decimal)amount, currency)
    {
    }

    /// <summary>Gets the amount of money.</summary>
    public decimal Amount { get; private set; }

    /// <summary>Gets the <see cref="Currency"/> of the money.</summary>
    public Currency Currency { get; private set; }

    /// <summary>Returns a value indicating whether two instances of <see cref="Money"/> are equal.</summary>
    /// <param name="left">A <see cref="Money"/> object on the left side.</param>
    /// <param name="right">A <see cref="Money"/> object on the right side.</param>
    /// <returns>true if left and right are equal; otherwise, false.</returns>
    public static bool operator ==(Money left, Money right) => left.Equals(right);

    /// <summary>Returns a value indicating whether two instances of <see cref="Money"/> are not equal.</summary>
    /// <param name="left">A <see cref="Money"/> object on the left side.</param>
    /// <param name="right">A <see cref="Money"/> object on the right side.</param>
    /// <returns>true if left and right are not equal; otherwise, false.</returns>
    public static bool operator !=(Money left, Money right) => !(left == right);

    /// <summary>Returns a value indicating whether this instance and a specified <see cref="Money"/> object represent the same
    /// value.</summary>
    /// <param name="other">A <see cref="Money"/> object.</param>
    /// <returns>true if value is equal to this instance; otherwise, false.</returns>
    public bool Equals(Money other) => Amount == other.Amount && Currency == other.Currency;

    /// <summary>Returns a value indicating whether this instance and a specified <see cref="object"/> represent the same type
    /// and value.</summary>
    /// <param name="obj">An <see cref="object"/>.</param>
    /// <returns>true if value is equal to this instance; otherwise, false.</returns>
    public override bool Equals(object obj) => obj is Money money && this.Equals(money);

    /// <summary>Returns the hash code for this instance.</summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = (hash * 23) + Amount.GetHashCode();
            return (hash * 23) + Currency.GetHashCode();
        }
    }

    /// <summary>Deconstructs the current instance into its components.</summary>
    /// <param name="amount">The Amount of money as <see langword="decimal"/>.</param>
    /// <param name="currency">The Currency of the money.</param>
    public void Deconstruct(out decimal amount, out Currency currency)
    {
        amount = Amount;
        currency = Currency;
    }

    private static decimal Round(decimal amount, Currency currency, MidpointRounding rounding)
    {
        switch (currency.DecimalDigits)
        {
            case CurrencyRegistry.NotApplicable:
                return Math.Round(amount);

            case CurrencyRegistry.Z07:
                // divided into five subunits rather than by a power of ten. 5 is 10 to the power of log(5) = 0.69897...
                return Math.Round(amount / 0.2m, 0, rounding) * 0.2m;

            default:
                return Math.Round(amount, (int)currency.DecimalDigits, rounding);
        }
    }

    [SuppressMessage(
        "Microsoft.Globalization",
        "CA1305:SpecifyIFormatProvider",
        MessageId = "System.String.Format(System.String,System.Object[])",
        Justification = "Test fail when Invariant is used. Inline JIT bug? When cloning CultureInfo it works.")]
    private static void AssertIsSameCurrency(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidCurrencyException(left.Currency, right.Currency);
    }
}