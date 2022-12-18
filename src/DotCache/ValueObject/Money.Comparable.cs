﻿namespace DotCache.ValueObject;

/// <summary>Represents Money, an amount defined in a specific Currency.</summary>
public partial struct Money : IComparable, IComparable<Money>
{
    /// <summary>Returns a value indicating whether a specified <see cref="ValueObject.Money"/> is less than another specified <see cref="ValueObject.Money"/>.</summary>
    /// <param name="left">A <see cref="ValueObject.Money"/> object on the left side.</param>
    /// <param name="right">A <see cref="ValueObject.Money"/> object on the right side.</param>
    /// <returns>true if left is less than right; otherwise, false.</returns>
    public static bool operator <(Money left, Money right) => Compare(left, right) < 0;

    /// <summary>Returns a value indicating whether a specified <see cref="ValueObject.Money"/> is greater than another specified <see cref="ValueObject.Money"/>.</summary>
    /// <param name="left">A <see cref="ValueObject.Money"/> object on the left side.</param>
    /// <param name="right">A <see cref="ValueObject.Money"/> object on the right side.</param>
    /// <returns>true if left is greater than right; otherwise, false.</returns>
    public static bool operator >(Money left, Money right) => Compare(left, right) > 0;

    /// <summary>Returns a value indicating whether a specified <see cref="ValueObject.Money"/> is less than or equal to another specified <see cref="ValueObject.Money"/>.</summary>
    /// <param name="left">A <see cref="ValueObject.Money"/> object on the left side.</param>
    /// <param name="right">A <see cref="ValueObject.Money"/> object on the right side.</param>
    /// <returns>true if left is less than or equal to right; otherwise, false.</returns>
    public static bool operator <=(Money left, Money right) => Compare(left, right) <= 0;

    /// <summary>Returns a value indicating whether a specified <see cref="ValueObject.Money"/> is greater than or equal to another specified <see cref="ValueObject.Money"/>.</summary>
    /// <param name="left">A <see cref="ValueObject.Money"/> object on the left side.</param>
    /// <param name="right">A <see cref="ValueObject.Money"/> object on the right side.</param>
    /// <returns>true if left is greater than or equal to right; otherwise, false.</returns>
    public static bool operator >=(Money left, Money right) => Compare(left, right) >= 0;

    /// <summary>Compares two specified <see cref="ValueObject.Money"/> values.</summary>
    /// <param name="left">The first <see cref="ValueObject.Money"/> object.</param>
    /// <param name="right">The second <see cref="ValueObject.Money"/> object.</param>
    /// <returns>
    /// A signed number indicating the relative values of this instance and value.
    /// <list type="table">
    /// <listheader>
    ///   <term>Return Value</term>
    ///   <description>Meaning</description>
    /// </listheader>
    /// <item>
    ///   <term>Less than zero</term>
    ///   <description>This instance is less than value.</description>
    /// </item>
    /// <item>
    ///   <term>Zero</term>
    ///   <description>This instance is equal to value.</description>
    /// </item>
    /// <item>
    ///   <term>Greater than zero </term>
    ///   <description>This instance is greater than value.</description>
    /// </item>
    /// </list>
    /// </returns>
    public static int Compare(Money left, Money right) => left.CompareTo(right);

    /// <summary>Compares this instance to a specified <see cref="ValueObject.Money"/> object.</summary>
    /// <param name="obj">A <see cref="ValueObject.Money"/> object.</param>
    /// <returns>
    /// A signed number indicating the relative values of this instance and value.
    /// <list type="table">
    /// <listheader>
    ///   <term>Return Value</term>
    ///   <description>Meaning</description>
    /// </listheader>
    /// <item>
    ///   <term>Less than zero</term>
    ///   <description>This instance is less than value.</description>
    /// </item>
    /// <item>
    ///   <term>Zero</term>
    ///   <description>This instance is equal to value.</description>
    /// </item>
    /// <item>
    ///   <term>Greater than zero </term>
    ///   <description>This instance is greater than value.</description>
    /// </item>
    /// </list>
    /// </returns>
    /// <exception cref="ArgumentException">object is not the same type as this instance.</exception>
    public int CompareTo(object obj)
    {
        if (obj == null)
            return 1;

        if (!(obj is Money))
            throw new ArgumentException("obj is not the same type as this instance", nameof(obj));

        return CompareTo((Money)obj);
    }

    /// <summary>Compares this instance to a specified <see cref="object"/>.</summary>
    /// <param name="other">An <see cref="object"/> or null.</param>
    /// <returns>
    /// A signed number indicating the relative values of this instance and value.
    /// <list type="table">
    /// <listheader>
    ///   <term>Return Value</term>
    ///   <description>Meaning</description>
    /// </listheader>
    /// <item>
    ///   <term>Less than zero</term>
    ///   <description>This instance is less than value.</description>
    /// </item>
    /// <item>
    ///   <term>Zero</term>
    ///   <description>This instance is equal to value.</description>
    /// </item>
    /// <item>
    ///   <term>Greater than zero </term>
    ///   <description>This instance is greater than value.</description>
    /// </item>
    /// </list>
    /// </returns>
    public int CompareTo(Money other)
    {
        AssertIsSameCurrency(this, other);
        return Amount.CompareTo(other.Amount);
    }
}