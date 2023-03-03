using AnyPay.Converters;
using AnyPay.Types.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AnyPay.Types.Payouts;

/// <summary>
/// Payout
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public sealed class Payout
{
    /// <summary>
    /// Payout number
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public long TransactionId { get; set; }

    /// <summary>
    /// Unique payout number in the merchant system
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public long PayoutId { get; }

    /// <summary>
    /// Payout system (see <see cref="PaymentSystem"/>)
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(StringEnumConverter))]
    public PaymentSystem PayoutType { get; set; }

    /// <summary>
    /// Payout status (see <see cref="PayoutStatus"/>)
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(StringEnumConverter))]
    public PayoutStatus Status { get; set; }

    /// <summary>
    /// Payout amount (for example, 100.00)
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public double Amount { get; set; }

    /// <summary>
    /// Payout commission
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public double Comission { get; set; }

    /// <summary>
    /// Where is the commission charged from?
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(StringEnumConverter))]
    public CommissionType? CommissionType { get; set; }

    /// <summary>
    /// Conversion course
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public double Rate { get; set; }

    /// <summary>
    /// Recipient's wallet number
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Wallet { get; set; } = null!;

    /// <summary>
    /// System balance available for payouts
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public double? Balance { get; set; }

    /// <summary>
    /// Payout creation date
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(DateFormatConverter), "dd.MM.yyyy HH:mm:ss")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Payout processing date
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [JsonConverter(typeof(DateFormatConverter), "dd.MM.yyyy HH:mm:ss")]
    public DateTime? CompleteDate { get; set; }
}
