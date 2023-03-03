namespace AnyPay.Types.Enums;

/// <summary>
/// Payout status
/// </summary>
public enum PayoutStatus
{
    /// <summary>
    /// Successful payout
    /// </summary>
    Paid = 1,
    /// <summary>
    /// Payout sent to the payment system (temporary status)
    /// </summary>
    InProcess = 2,
    /// <summary>
    /// Payout has been canceled by the payment system, funds have been returned to the balance
    /// </summary>
    Canceled = 3,
    /// <summary>
    /// Payout blocked by the monitoring system
    /// </summary>
    Blocked = 4,
}
