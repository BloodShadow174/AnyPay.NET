using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace AnyPay.Types.Payments;

/// <summary>
/// Payment transactions
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public sealed class PaymentTransactions
{
    /// <summary>
    /// Number of transactions
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int Total { get; set; }

    /// <summary>
    /// List of <see cref="Payment">transactions</see> in the form of a dictionary 
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [MaybeNull]
    public Dictionary<long, Payment> Payments { get; set; }
}
