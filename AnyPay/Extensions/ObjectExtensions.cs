using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace AnyPay.Extensions;

internal static class ObjectExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ThrowIfNull<T>(
        this T? value,
        [CallerArgumentExpression(nameof(value))] string? parameterName = default
    ) =>
        value ?? throw new ArgumentNullException(parameterName);

    internal static IDictionary<string, string>? ToKeyValue(this object metaToken)
    {
        if (metaToken == null)
            return null;

        if (metaToken is not JToken token)
        {
            return ToKeyValue(JObject.FromObject(metaToken));
        }

        if (token.HasValues)
        {
            var contentData = new Dictionary<string, string>();

            foreach (var child in token.Children().ToList())
            {
                var childContent = child.ToKeyValue();

                if (childContent != null)
                {
                    contentData = contentData.Concat(childContent)
                        .ToDictionary(k => k.Key, v => v.Value);
                }
            }

            return contentData;
        }

        var jValue = token as JValue;

        if (jValue?.Value == null)
        {
            return null;
        }

        var value = jValue?.Type == JTokenType.Date ?
            jValue?.ToString("o", CultureInfo.InvariantCulture) :
            jValue?.ToString(CultureInfo.InvariantCulture);

        return new Dictionary<string, string> { { token.Path, value ?? string.Empty } };
    }
}
