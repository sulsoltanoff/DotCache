namespace DotCache.ValueObject;

/// <summary>Represents Money, an amount defined in a specific Currency.</summary>
public partial struct Money
{
    /// <summary>Adds two specified <see cref="ValueObject.Money"/> values.</summary>
    /// <param name="left">A <see cref="ValueObject.Money"/> object on the left side.</param>
    /// <param name="right">A <see cref="ValueObject.Money"/> object on the right side.</param>
    /// <returns>The <see cref="ValueObject.Money"/> result of adding left and right.</returns>
    public static Money operator +(Money left, Money right) => Add(left, right);

    /// <summary>Add the <see cref="ValueObject.Money"/> value with the given value.</summary>
    /// <param name="left">A <see cref="ValueObject.Money"/> object on the left side.</param>
    /// <param name="right">A <see cref="decimal"/> object on the right side.</param>
    /// <returns>The <see cref="ValueObject.Money"/> result of adding left and right.</returns>
    public static Money operator +(Money left, decimal right) => Add(left, right);

    /// <summary>Add the <see cref="ValueObject.Money"/> value with the given value.</summary>
    /// <param name="left">A <see cref="decimal"/> object on the left side.</param>
    /// <param name="right">A <see cref="ValueObject.Money"/> object on the right side.</param>
    /// <returns>The <see cref="ValueObject.Money"/> result of adding left and right.</returns>
    public static Money operator +(decimal left, Money right) => Add(right, left);

    /// <summary>Subtracts two specified <see cref="ValueObject.Money"/> values.</summary>
    /// <param name="left">A <see cref="ValueObject.Money"/> object on the left side.</param>
    /// <param name="right">A <see cref="ValueObject.Money"/> object on the right side.</param>
    /// <returns>The <see cref="ValueObject.Money"/> result of subtracting right from left.</returns>
    public static Money operator -(Money left, Money right) => Subtract(left, right);

    /// <summary>Subtracts <see cref="ValueObject.Money"/> value with the given value.</summary>
    /// <param name="left">A <see cref="ValueObject.Money"/> object on the left side.</param>
    /// <param name="right">A <see cref="decimal"/> object on the right side.</param>
    /// <returns>The <see cref="ValueObject.Money"/> result of subtracting right from left.</returns>
    public static Money operator -(Money left, decimal right) => Subtract(left, right);

    /// <summary>Subtracts <see cref="ValueObject.Money"/> value with the given value.</summary>
    /// <param name="left">A <see cref="decimal"/> object on the left side.</param>
    /// <param name="right">A <see cref="ValueObject.Money"/> object on the right side.</param>
    /// <returns>The <see cref="ValueObject.Money"/> result of subtracting right from left.</returns>
    public static Money operator -(decimal left, Money right) => Subtract(right, left);

    /// <summary>Multiplies the <see cref="ValueObject.Money"/> value by the given value.</summary>
    /// <param name="left">A <see cref="ValueObject.Money"/> object on the left side.</param>
    /// <param name="right">A <see cref="decimal"/> object on the right side.</param>
    /// <returns>The <see cref="ValueObject.Money"/> result of multiplying right with left.</returns>
    public static Money operator *(Money left, decimal right) => Multiply(left, right);

    /// <summary>Multiplies the <see cref="ValueObject.Money"/> value by the given value.</summary>
    /// <param name="left">A <see cref="decimal"/> object on the left side.</param>
    /// <param name="right">A <see cref="ValueObject.Money"/> object on the right side.</param>
    /// <returns>The <see cref="ValueObject.Money"/> result of multiplying left with right.</returns>
    public static Money operator *(decimal left, Money right) => Multiply(right, left);

    /// <summary>Divides the <see cref="ValueObject.Money"/> value by the given value.</summary>
    /// <param name="left">A <see cref="ValueObject.Money"/> object on the left side.</param>
    /// <param name="right">A <see cref="decimal"/> object on the right side.</param>
    /// <returns>The <see cref="ValueObject.Money"/> result of dividing left with right.</returns>
    /// <remarks>This division can lose money! Use <see cref="Extensions.MoneyExtensions.SafeDivide(ValueObject.Money, int)"/> to do a safe division.</remarks>
    public static Money operator /(Money left, decimal right) => Divide(left, right);

    /// <summary>Divides the <see cref="ValueObject.Money"/> value by the given value.</summary>
    /// <param name="left">A <see cref="ValueObject.Money"/> object on the left side.</param>
    /// <param name="right">A <see cref="ValueObject.Money"/> object on the right side.</param>
    /// <returns>The <see cref="decimal"/> result of dividing left with right.</returns>
    /// <remarks>Division of Money by Money, means the unit is lost, so the result will be a ratio <see cref="decimal"/>.</remarks>
    public static decimal operator /(Money left, Money right) => Divide(left, right);

    /// <summary>Adds two specified <see cref="ValueObject.Money"/> values.</summary>
    /// <param name="money1">The first <see cref="ValueObject.Money"/> object.</param>
    /// <param name="money2">The second <see cref="ValueObject.Money"/> object.</param>
    /// <returns>A <see cref="ValueObject.Money"/> object with the values of both <see cref="ValueObject.Money"/> objects added.</returns>
    public static Money Add(Money money1, Money money2)
    {
        AssertIsSameCurrency(money1, money2);
        return new Money(decimal.Add(money1.Amount, money2.Amount), money1.Currency);
    }

    /// <summary>Adds two specified <see cref="ValueObject.Money"/> values.</summary>
    /// <param name="money1">The first <see cref="ValueObject.Money"/> object.</param>
    /// <param name="money2">The second <see cref="decimal"/> object.</param>
    /// <returns>A <see cref="ValueObject.Money"/> object with the values of both <see cref="decimal"/> objects added.</returns>
    public static Money Add(Money money1, decimal money2) => new(decimal.Add(money1.Amount, money2), money1.Currency);

    /// <summary>Subtracts one specified <see cref="ValueObject.Money"/> value from another.</summary>
    /// <param name="money1">The first <see cref="ValueObject.Money"/> object.</param>
    /// <param name="money2">The second <see cref="ValueObject.Money"/> object.</param>
    /// <returns>A <see cref="ValueObject.Money"/> object where the second <see cref="ValueObject.Money"/> object is subtracted from the first.</returns>
    public static Money Subtract(Money money1, Money money2)
    {
        AssertIsSameCurrency(money1, money2);
        return new Money(decimal.Subtract(money1.Amount, money2.Amount), money1.Currency);
    }

    /// <summary>Subtracts one specified <see cref="ValueObject.Money"/> value from another.</summary>
    /// <param name="money1">The first <see cref="ValueObject.Money"/> object.</param>
    /// <param name="money2">The second <see cref="decimal"/> object.</param>
    /// <returns>A <see cref="ValueObject.Money"/> object where the second <see cref="decimal"/> object is subtracted from the first.</returns>
    public static Money Subtract(Money money1, decimal money2) => new(decimal.Subtract(money1.Amount, money2), money1.Currency);

    /// <summary>Multiplies the specified money.</summary>
    /// <param name="money">The money.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns>The result as <see cref="ValueObject.Money"/> after multiplying.</returns>
    public static Money Multiply(Money money, decimal multiplier) => new(decimal.Multiply(money.Amount, multiplier), money.Currency);

    /// <summary>Divides the specified money.</summary>
    /// <param name="money">The money.</param>
    /// <param name="divisor">The divider.</param>
    /// <returns>The division as <see cref="ValueObject.Money"/>.</returns>
    /// <remarks>This division can lose money! Use <see cref="Extensions.MoneyExtensions.SafeDivide(ValueObject.Money, int)"/> to do a safe division.</remarks>
    public static Money Divide(Money money, decimal divisor) => new(decimal.Divide(money.Amount, divisor), money.Currency);

    /// <summary>Divides the specified money.</summary>
    /// <param name="money1">The money.</param>
    /// <param name="money2">The divider.</param>
    /// <returns>The <see cref="decimal"/> result of dividing left with right.</returns>
    /// <remarks>Division of Money by Money, means the unit is lost, so the result will be Decimal.</remarks>
    public static decimal Divide(Money money1, Money money2)
    {
        AssertIsSameCurrency(money1, money2);
        return decimal.Divide(money1.Amount, money2.Amount);
    }
}