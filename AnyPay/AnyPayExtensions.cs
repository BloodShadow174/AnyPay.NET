using AnyPay.Extensions;
using AnyPay.Requests.Account;
using AnyPay.Requests.Payments;
using AnyPay.Types.Account;
using AnyPay.Types.Enums;
using AnyPay.Types.Payments;
using AnyPay.Types.Payouts;

namespace AnyPay;

public static class AnyPayExtensions
{
    /// <summary>
    /// Get account balance
    /// </summary>
    /// <param name="anyPayClient"><see cref="AnyPayClient"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="AccountBalance"/></returns>
    public static async Task<AccountBalance> GetBalanceAsync(
        this AnyPayClient anyPayClient,
        CancellationToken cancellationToken = default
    )
    {
        return await anyPayClient
            .ThrowIfNull()
            .MakeRequestAsync(
                new BalanceRequest(
                    anyPayClient.ApiId,
                    anyPayClient.ApiKey
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Getting current currency conversion rates
    /// </summary>
    /// <param name="anyPayClient"><see cref="AnyPayClient"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Rates"/></returns>
    public static async Task<Rates> GetRatesAsync(
        this AnyPayClient anyPayClient,
        CancellationToken cancellationToken = default
    )
    {
        return await anyPayClient
            .ThrowIfNull()
            .MakeRequestAsync(
                new RatesRequest(
                    anyPayClient.ApiId,
                    anyPayClient.ApiKey
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Get <see cref="PaymentSystem"/> fees
    /// </summary>
    /// <param name="anyPayClient"><see cref="AnyPayClient"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>List of fees <see cref="PaymentSystem"/> as a dictionary</returns>
    public static async Task<IDictionary<PaymentSystem, double>> GetCommissionsAsync(
        this AnyPayClient anyPayClient,
        CancellationToken cancellationToken = default
    )
    {
        return await anyPayClient
            .ThrowIfNull()
            .MakeRequestAsync(
                new CommissionsRequest(
                    anyPayClient.ApiId,
                    anyPayClient.ApiKey,
                    anyPayClient.ProjectId
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Create a new payment
    /// </summary>
    /// <param name="anyPayClient"><see cref="AnyPayClient"/></param>
    /// <param name="payId">Order number in the seller's system (up to 15 characters from the characters "0-9")</param>
    /// <param name="amount">Payment amount (for example, 100.00)</param>
    /// <param name="currency">
    /// Payment currency according to ISO 4217:
    ///   RUB - Russian ruble (default);
    ///   UAH - Ukrainian hryvnia;
    ///   BYN - Belarusian ruble;
    ///   KZT - Kazakhstani tenge;
    ///   USD - US dollar;
    ///   EUR - Euro
    /// </param>
    /// <param name="desc">Short description of the payment (up to 150 symbols)</param>
    /// <param name="email">Payer's email address</param>
    /// <param name="method">Payment method (see <see cref="PaymentSystem"/>)</param>
    /// <param name="methodCurrency">
    /// Currency of payment method:
    /// Webmoney (USD, EUR);
    /// Advcash (RUB, USD, EUR);
    /// Perfect Money (USD, EUR)
    /// </param>
    /// <param name="phone">Payer phone number (for example, 79990000000)</param>
    /// <param name="tail">Last 4 digits of card number (for card <see cref="PaymentSystem">method</see>)</param>
    /// <param name="successUrl">Forwarding address in case of successful payment</param>
    /// <param name="failUrl">Forwarding address in case of unsuccessful payment</param>
    /// <param name="lang">
    /// Payment page interface language:
    ///   ru - Russian (default);
    ///   en - English
    /// </param>
    /// <param name="additionalProperties">Additional seller parameters</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="CreatedPayment"/></returns>
    public static async Task<CreatedPayment> CreatePaymentAsync(
        this AnyPayClient anyPayClient,
        long payId,
        double amount,
        Currency currency,
        string desc,
        string email,
        PaymentSystem method,
        Currency? methodCurrency = default,
        long? phone = default,
        int? tail = default,
        string? successUrl = default,
        string? failUrl = default,
        string? lang = default,
        IDictionary<string, string>? additionalProperties = default,
        CancellationToken cancellationToken = default
    )
    {
        return await anyPayClient
            .ThrowIfNull()
            .MakeRequestAsync(
                new CreatePaymentRequest(
                    anyPayClient.ApiId,
                    anyPayClient.ApiKey,
                    anyPayClient.ProjectId,
                    payId,
                    amount,
                    currency,
                    desc,
                    email,
                    method,
                    methodCurrency,
                    phone,
                    tail,
                    successUrl,
                    failUrl,
                    lang,
                    additionalProperties
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Receiving payment transactions
    /// </summary>
    /// <param name="anyPayClient"><see cref="AnyPayClient"/></param>
    /// <param name="transId">AnyPay payment number</param>
    /// <param name="payId">Order number in the seller's system (up to 15 characters from the characters "0-9")</param>
    /// <param name="offset">
    /// The offset required to select a specific subset of transactions (default is 0).
    /// Important! The response contains 1000 transactions
    ///</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="PaymentTransactions"/></returns>
    public static async Task<PaymentTransactions> GetPaymentsAsync(
        this AnyPayClient anyPayClient,
        long? transId = default,
        long? payId = default,
        int? offset = default,
        CancellationToken cancellationToken = default
    )
    {
        return await anyPayClient
            .ThrowIfNull()
            .MakeRequestAsync(
                new PaymentsRequest(
                    anyPayClient.ApiId,
                    anyPayClient.ApiKey,
                    anyPayClient.ProjectId,
                    transId,
                    payId,
                    offset
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Gets information about the order by order number in the seller's system
    /// </summary>
    /// <param name="anyPayClient"><see cref="AnyPayClient"/></param>
    /// <param name="payId">Order number in the seller's system (up to 15 characters from the characters "0-9")</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>If the information is found, then get information <see cref="Payment">about payment</see></returns>
    public static async Task<Payment?> GetPaymentAsync(
        this AnyPayClient anyPayClient,
        long payId,
        CancellationToken cancellationToken = default
    )
    {
        var payments = await anyPayClient
            .GetPaymentsAsync(payId: payId, cancellationToken: cancellationToken);

        if (payments.Payments is null)
            return null;

        return payments.Payments.Select(p => p.Value).FirstOrDefault();
    }

    /// <summary>
    /// Create a new payout
    /// </summary>
    /// <param name="anyPayClient"><see cref="AnyPayClient"/></param>
    /// <param name="payoutId">Unique payout number in the merchant system</param>
    /// <param name="payoutType">
    /// Payout system:
    ///  <see cref="PaymentSystem.Qiwi"/>;
    ///  <see cref="PaymentSystem.YooMoney"/>;
    ///  <see cref="PaymentSystem.WebMoney"/> (WMZ);
    ///  <see cref="PaymentSystem.MobilePhone"/>;
    ///  <see cref="PaymentSystem.Card"/>
    /// </param>
    /// <param name="amount">Payout amount (for example, 100.00) Minimum: 50.00 RUB</param>
    /// <param name="wallet">Recipient's wallet number (no spaces or separators)</param>
    /// <param name="walletCurrency">
    /// Recipient's wallet currency (for bank cards):
    ///  <see cref="Currency.RUB"/>;
    ///  <see cref="Currency.UAH"/>;
    ///  <see cref="Currency.KZT"/>
    /// </param>
    /// <param name="commissionType">Where is the commission charged from?</param>
    /// <param name="statusUrl">
    /// This is the URL to which the GET request will be sent when the payout reaches final status
    /// </param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Payout"/></returns>
    public static async Task<Payout> CreatePayoutAsync(
        this AnyPayClient anyPayClient,
        long payoutId,
        PaymentSystem payoutType,
        double amount,
        string wallet,
        Currency? walletCurrency = default,
        CommissionType? commissionType = default,
        string? statusUrl = default,
        CancellationToken cancellationToken = default
    )
    {
        return await anyPayClient
            .ThrowIfNull()
            .MakeRequestAsync(
                new CreatePayoutRequest(
                    anyPayClient.ApiId,
                    anyPayClient.ApiKey,
                    anyPayClient.ProjectId,
                    payoutId,
                    payoutType,
                    amount,
                    wallet,
                    walletCurrency,
                    commissionType,
                    statusUrl
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Receiving payout transactions
    /// </summary>
    /// <param name="anyPayClient"><see cref="AnyPayClient"/></param>
    /// <param name="transId">AnyPay payout number</param>
    /// <param name="payoutId">Payout number in merchant system</param>
    /// <param name="offset">
    /// The offset required to select a specific subset of transactions (default is 0).
    /// Important! The response contains 1000 transactions
    /// </param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    public static async Task<PayoutTransactions> GetPayoutsAsync(
        this AnyPayClient anyPayClient,
        long? transId = default,
        long? payoutId = default,
        int? offset = default,
        CancellationToken cancellationToken = default
    )
    {
        return await anyPayClient
            .ThrowIfNull()
            .MakeRequestAsync(
                new PayoutsRequest(
                    anyPayClient.ApiId,
                    anyPayClient.ApiKey,
                    anyPayClient.ProjectId,
                    transId,
                    payoutId,
                    offset
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Gets information about payout by order number in the seller's system
    /// </summary>
    /// <param name="anyPayClient"><see cref="AnyPayClient"/></param>
    /// <param name="payoutId">Payout number in merchant system</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>If the information is found, then get information <see cref="Payout">about payout</see></returns>
    public static async Task<Payout?> GetPayoutAsync(
        this AnyPayClient anyPayClient,
        long payoutId,
        CancellationToken cancellationToken = default
    )
    {
        var payouts = await anyPayClient
            .GetPayoutsAsync(payoutId: payoutId, cancellationToken: cancellationToken);

        if (payouts.Payouts is null)
            return null;

        return payouts.Payouts.Select(p => p.Value).FirstOrDefault();
    }

    /// <summary>
    /// Getting a list of actual IP addresses from which payment notifications are sent
    /// </summary>
    /// <param name="anyPayClient"><see cref="AnyPayClient"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="IpAddresses"/></returns>
    public static async Task<IpAddresses> GetIpNotificationAsync(
        this AnyPayClient anyPayClient,
        CancellationToken cancellationToken = default
    )
    {
        return await anyPayClient
            .ThrowIfNull()
            .MakeRequestAsync(
                new IpNotificationRequest(
                    anyPayClient.ApiId,
                    anyPayClient.ApiKey
                ),
                cancellationToken
            );
    }

    /// <summary>
    /// Generate a special URL to initiate payment.
    /// </summary>
    /// <param name="payId">Order number in the seller's system (up to 15 characters from the characters "0-9")</param>
    /// <param name="amount">Payment amount (for example, 100.00)</param>
    /// <param name="currency">
    /// Payment currency according to ISO 4217:
    ///   RUB - Russian ruble (default);
    ///   UAH - Ukrainian hryvnia;
    ///   BYN - Belarusian ruble;
    ///   KZT - Kazakhstani tenge;
    ///   USD - US dollar;
    ///   EUR - Euro
    /// </param>
    /// <param name="desc">Short description of the payment (up to 150 symbols)</param>
    /// <param name="email">Payer's email address</param>
    /// <param name="phone">Payer phone number (for example, 79990000000)</param>
    /// <param name="method">Payment method (see <see cref="PaymentSystem"/>)</param>
    /// <param name="successUrl">Forwarding address in case of successful payment</param>
    /// <param name="failUrl">Forwarding address in case of unsuccessful payment</param>
    /// <param name="lang">
    /// Payment page interface language:
    ///   ru - Russian (default);
    ///   en - English
    /// </param>
    /// <param name="additionalProperties">Additional seller parameters</param>
    /// <param name="signType">Request signature type</param>
    /// <returns>Special URL to initiate payment</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static string GetMerchantUri(
        this AnyPayClient anyPayClient,
        long payId,
        double amount,
        Currency currency,
        string? desc = default,
        string? email = default,
        long? phone = default,
        PaymentSystem? method = default,
        string? successUrl = default,
        string? failUrl = default,
        string? lang = default,
        IDictionary<string, string>? additionalProperties = default,
        SignType signType = SignType.MD5
    ) => anyPayClient.ThrowIfNull().MakeMerchantUri(
        payId: payId,
        amount: amount,
        currency: currency,
        desc: desc,
        email: email,
        phone: phone,
        method: method,
        successUrl: successUrl,
        failUrl: failUrl,
        lang: lang,
        additionalProperties: additionalProperties,
        signType: signType
    ).AbsoluteUri;
}
