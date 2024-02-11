using AnyPay.Types.Payouts;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AnyPay.Requests.Payments;

/// <summary>
/// Use this class to create <see cref="PayoutTransactions"/> request
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
internal sealed class PayoutsRequest : ParameterlessRequest<PayoutTransactions>
{
    /// <summary>
    /// Initializes a new request to get <see cref="PayoutTransactions"/>
    /// </summary>
    /// <param name="apiId">AnyPay API ID</param>
    /// <param name="apiKey">AnyPay API Key</param
    /// <param name="projectId">AnyPay project ID</param>
    /// <param name="transId">AnyPay payout number</param>
    /// <param name="payoutId">Payout number in merchant system</param>
    /// <param name="offset">
    /// The offset required to select a specific subset of transactions (default is 0).
    /// Important! The response contains 1000 transactions
    /// </param>
    public PayoutsRequest(
        string apiId,
        string apiKey,
        int projectId,
        long? transId = default,
        long? payoutId = default,
        int? offset = default
    )
        : base("payouts")
    {
        ProjectId = projectId;
        TransId = transId;
        PayoutId = payoutId;
        Offset = offset;
        Sign = GenerateSign(
            MethodName,
            apiId,
            apiKey
        );
    }

    /// <summary>
    /// AnyPay project ID
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int ProjectId { get; set; }

    /// <summary>
    /// AnyPay payout number
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public long? TransId { get; set; }

    /// <summary>
    /// Payout number in merchant system
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public long? PayoutId { get; set; }

    /// <summary>
    /// The offset required to select a specific subset of transactions (default is 0).
    /// Important! The response contains 1000 transactions
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Offset { get; set; }

    /// <summary>
    /// Request signature. Formed by gluing parameters together and creating a hash
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Sign { get; }
}
