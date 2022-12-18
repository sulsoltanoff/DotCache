using System.ComponentModel;
using System.Globalization;

namespace DotCache;

/// <summary>Provides a way of converting the type <see cref="string"/> to and from the type <see cref="ValueObject.Money"/>.</summary>
public class MoneyTypeConverter : TypeConverter
{
    /// <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
    /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
    /// <param name="context">An <see cref="ITypeDescriptorContext" /> that provides a format context. </param>
    /// <param name="sourceType">A <see cref="Type" /> that represents the type you want to convert from. </param>
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    /// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
    /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
    /// <param name="context">An <see cref="ITypeDescriptorContext" /> that provides a format context. </param>
    /// <param name="destinationType">A <see cref="Type" /> that represents the type you want to convert to. </param>
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        return destinationType == typeof(ValueObject.Money) || base.CanConvertTo(context, destinationType);
    }

    /// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
    /// <returns>An <see cref="object" /> that represents the converted value.</returns>
    /// <param name="context">An <see cref="ITypeDescriptorContext" /> that provides a format context. </param>
    /// <param name="culture">The <see cref="CultureInfo" /> to use as the current culture. </param>
    /// <param name="value">The <see cref="object" /> to convert. </param>
    /// <exception cref="NotSupportedException">The conversion cannot be performed. </exception>
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is not string valueAsString)
        {
            return base.ConvertFrom(context, culture, value);
        }

        string[] v = valueAsString.Split(new[] { ' ' });
        return new ValueObject.Money(decimal.Parse(v[0], culture), v[1]);

    }

    /// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
    /// <returns>An <see cref="object" /> that represents the converted value.</returns>
    /// <param name="context">An <see cref="ITypeDescriptorContext" /> that provides a format context. </param>
    /// <param name="culture">A <see cref="CultureInfo" />. If null is passed, the current culture is assumed. </param>
    /// <param name="value">The <see cref="object" /> to convert. </param>
    /// <param name="destinationType">The <see cref="Type" /> to convert the <paramref name="value" /> parameter to. </param>
    /// <exception cref="ArgumentNullException">The <paramref name="destinationType" /> parameter is null. </exception>
    /// <exception cref="NotSupportedException">The conversion cannot be performed. </exception>
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object value, Type destinationType)
    {
        if (destinationType == typeof(string))
        {
            return ((ValueObject.Money)value).Amount + " " + ((ValueObject.Money)value).Currency.Code;
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }
}