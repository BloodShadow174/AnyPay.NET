using AnyPay.Types.Account;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AnyPay.Requests.Account;

/// <summary>
/// Use this class to create <see cref="IpAddresses"/> request
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
internal sealed class IpNotificationRequest : ParameterlessRequest<IpAddresses>
{
    /// <summary>
    /// Initializes a new request to get <see cref="IpAddresses"/>
    /// </summary>
    /// <param name="apiId">Anypay API ID</param>
    /// <param name="apiKey">Anypay API Key</param>
    public IpNotificationRequest(string apiId, string apiKey)
        : base("ip-notification")
    {
        Sign = GenerateSign(MethodName, apiId, apiKey);
    }

    /// <summary>
    /// Request signature. Formed by gluing parameters together and creating a hash
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Sign { get; }
}
