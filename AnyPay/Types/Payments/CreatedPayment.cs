using AnyPay.Types.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AnyPay.Types.Payments;

/// <summary>
/// Created payment
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public sealed class CreatedPayment
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
    /// Payment Link
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string PaymentUrl { get; set; } = null!;

    /// <summary>
    /// Output of payment details
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public PaymentData? PaymentData { get; set; }
}
