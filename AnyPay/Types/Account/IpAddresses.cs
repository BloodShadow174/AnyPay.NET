using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AnyPay.Types.Account;

/// <summary>
/// AnyPay IP address list
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public sealed class IpAddresses
{
    /// <summary>
    /// IP address strings
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public List<string> Ip { get; set; } = null!;
}
