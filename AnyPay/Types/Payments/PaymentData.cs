using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using AnyPay.Types.Enums;
using Newtonsoft.Json.Converters;

namespace AnyPay.Types.Payments;

/// <summary>
/// Output of payment details
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public sealed class PaymentData
{
    /// <summary>
    /// Amount to be paid
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public double? Amount { get; set; }

    /// <summary>
    /// Currency to be paid
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [JsonConverter(typeof(StringEnumConverter))]
    public Currency? Currency { get; set; }

    /// <summary>
    /// Recipient's account number to which the transfer must be made
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Account { get; set; }

    /// <summary>
    /// Recipient's bank
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Bank { get; set; }
}
