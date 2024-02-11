using AnyPay.Types.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AnyPay.Requests.Account;

/// <summary>
/// Use this class to create <see cref="IDictionary{PaymentSystem, double}"/> request
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
internal sealed class CommissionsRequest : ParameterlessRequest<IDictionary<PaymentSystem, double>>
{
    /// <summary>
    /// Initializes a new request to get <see cref="IDictionary{PaymentSystem, double}"/>
    /// </summary>
    /// <param name="apiId">AnyPay API ID</param>
    /// <param name="apiKey">AnyPay API Key</param
    /// <param name="projectId">AnyPay project ID</param>
    public CommissionsRequest(string apiId, string apiKey, int projectId)
        : base("commissions")
    {
        ProjectId = projectId;
        Sign = GenerateSign(MethodName, apiId, projectId.ToString(), apiKey);
    }

    /// <summary>
    /// AnyPay project ID
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int ProjectId { get; }

    /// <summary>
    /// Request signature. Formed by gluing parameters together and creating a hash
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Sign { get; }
}
