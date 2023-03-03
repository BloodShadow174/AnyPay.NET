using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AnyPay.Types.Account;

/// <summary>
/// Account balance
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public sealed class AccountBalance
{
    /// <summary>
    /// Account balance in rubles
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public double Balance { get; set; }
}
