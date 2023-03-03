using AnyPay.Types.Account;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AnyPay.Requests.Account;

/// <summary>
/// Use this class to create <see cref="Rates"/> request
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
internal sealed class RatesRequest : ParameterlessRequest<Rates>
{
    /// <summary>
    /// Initializes a new request to get <see cref="Rates"/>
    /// </summary>
    /// <param name="apiId">Anypay API ID</param>
    /// <param name="apiKey">Anypay API Key</param
    public RatesRequest(string apiId, string apiKey)
        : base("rates")
    {
        Sign = GenerateSign(MethodName, apiId, apiKey);
    }

    /// <summary>
    /// Request signature. Formed by gluing parameters together and creating a hash
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Sign { get; }
}
