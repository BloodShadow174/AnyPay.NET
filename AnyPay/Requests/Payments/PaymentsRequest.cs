using AnyPay.Types.Payments;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AnyPay.Requests.Payments;

/// <summary>
/// Use this class to create <see cref="PaymentTransactions"/> request
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
internal sealed class PaymentsRequest : ParameterlessRequest<PaymentTransactions>
{
    /// <summary>
    /// Initializes a new request to get <see cref="PaymentTransactions"/>
    /// </summary>
    /// <param name="apiId">AnyPay API ID</param>
    /// <param name="apiKey">AnyPay API Key</param
    /// <param name="projectId">AnyPay project ID</param>
    /// <param name="transId">AnyPay payment number</param>
    /// <param name="payId">Order number in the seller's system</param>
    /// <param name="offset">
    /// The offset required to select a specific subset of transactions (default is 0).
    /// Important! The response contains 1000 transactions
    /// </param>
    public PaymentsRequest(
        string apiId,
        string apiKey,
        int projectId,
        long? transId = default,
        long? payId = default,
        int? offset = default
    )
        : base("payments")
    {
        ProjectId = projectId;
        TransId = transId;
        PayId = payId;
        Offset = offset;
        Sign = GenerateSign(
            MethodName,
            apiId,
            projectId.ToString(),
            apiKey
        );
    }

    /// <summary>
    /// AnyPay project ID
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int ProjectId { get; set; }

    /// <summary>
    /// AnyPay payment number
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public long? TransId { get; set; }

    /// <summary>
    /// Order number in the seller's system (up to 15 characters from the characters "0-9")
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public long? PayId { get; set; }

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
