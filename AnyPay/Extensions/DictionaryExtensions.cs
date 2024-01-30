namespace AnyPay.Extensions;

public static class DictionaryExtensions
{
    public static void AddOptionalParameter(
        this IDictionary<string, string> parameters,
        string key,
        string? value
    )
    {
        if (string.IsNullOrWhiteSpace(value)) return;

        if (parameters.ContainsKey(key)) return;

        parameters[key] = value;
    }
}
