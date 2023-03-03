namespace AnyPay.Types.Enums;

/// <summary>
/// Payment status
/// </summary>
public enum PaymentStatus
{
    /// <summary>
    /// Successful payment
    /// </summary>
    Paid = 1,
    /// <summary>
    /// Waiting for payment
    /// </summary>
    Waiting = 2,
    /// <summary>
    /// Refund to the buyer
    /// </summary>
    Refund = 3,
    /// <summary>
    /// Payment canceled
    /// </summary>
    Canceled = 4,
    /// <summary>
    /// Invoice expiration date
    /// </summary>
    Expired = 5,
    /// <summary>
    /// Payment error
    /// </summary>
    Error = 6,
}
