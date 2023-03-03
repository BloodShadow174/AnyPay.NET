using AnyPay.Types.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AnyPay.Types.Account;

/// <summary>
/// Current currency conversion rates
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public sealed class Rates
{
    /// <summary>
    /// Conversion type: to receive
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public Dictionary<Currency, double> In { get; set; } = null!;

    /// <summary>
    /// Conversion type: for withdrawal
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public Dictionary<Currency, double> Out { get; set; } = null!;
}
