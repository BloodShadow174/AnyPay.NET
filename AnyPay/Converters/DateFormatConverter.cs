using Newtonsoft.Json.Converters;

namespace AnyPay.Converters;

/// <summary>
/// Converts a <see cref="DateTime"/> to specific format
/// </summary>
public sealed class DateFormatConverter : IsoDateTimeConverter
{
    /// <summary>
    /// Conversion initialization
    /// </summary>
    /// <param name="format">Format</param>
    public DateFormatConverter(string format)
    {
        DateTimeFormat = format;
    }
}
