using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace AnyPay.Types.Enums;

/// <summary>
/// Payment status
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum PaymentStatus
{
    /// <summary>
    /// Partially paid
    /// </summary>
    [EnumMember(Value = "partially-paid")]
    PartiallyPaid = 0,
    /// <summary>
    /// Successful payment
    /// </summary>
    [EnumMember(Value = "paid")]
    Paid = 1,
    /// <summary>
    /// Waiting for payment
    /// </summary>
    [EnumMember(Value = "waiting")]
    Waiting = 2,
    /// <summary>
    /// Refund to the buyer
    /// </summary>
    [EnumMember(Value = "refund")]
    Refund = 3,
    /// <summary>
    /// Payment canceled
    /// </summary>
    [EnumMember(Value = "canceled")]
    Canceled = 4,
    /// <summary>
    /// Invoice expiration date
    /// </summary>
    [EnumMember(Value = "expired")]
    Expired = 5,
    /// <summary>
    /// Payment error
    /// </summary>
    [EnumMember(Value = "error")]
    Error = 6,
}
