using AnyPay.Converters;
using AnyPay.Types.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AnyPay.Types.Payments;

/// <summary>
/// Payment
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public sealed class Payment
{
    /// <summary>
    /// Payment number
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public long TransactionId { get; set; }

    /// <summary>
    /// Order number in the seller's system
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public long PayId { get; set; }

    /// <summary>
    /// Payment status (see <see cref="PaymentStatus"/>)
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(StringEnumConverter))]
    public PaymentStatus Status { get; set; }

    /// <summary>
    /// Payment method (see <see cref="PaymentSystem"/>)
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [JsonConverter(typeof(StringEnumConverter))]
    public PaymentSystem? Method { get; set; }

    /// <summary>
    /// Amount of payment
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public long Amount { get; set; }

    /// <summary>
    /// Payment currency (see <see cref="Enums.Currency"/>)
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [JsonConverter(typeof(StringEnumConverter))]
    public Currency? Currency { get; set; }

    /// <summary>
    /// Amount to be credited (in rubles)
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public double? Profit { get; set; }

    /// <summary>
    /// Buyer mailbox
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Email { get; set; }

    /// <summary>
    /// Payment Description
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Desc { get; set; } = null!;

    /// <summary>
    /// Date and time the invoice was created in the format
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(DateFormatConverter), "dd.MM.yyyy HH:mm:ss")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Date and time the invoice was processed in the format
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [JsonConverter(typeof(DateFormatConverter), "dd.MM.yyyy HH:mm:ss")]
    public DateTime? PayDate { get; set; }
}
