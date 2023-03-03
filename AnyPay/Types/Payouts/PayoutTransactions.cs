using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace AnyPay.Types.Payouts;

/// <summary>
/// Payout transactions
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public sealed class PayoutTransactions
{
    /// <summary>
    /// Number of payouts
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int Total { get; set; }

    /// <summary>
    /// List of <see cref="Payout">transactions</see> in the form of a dictionary 
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [MaybeNull]
    public Dictionary<long, Payout> Payouts { get; set; }
}
