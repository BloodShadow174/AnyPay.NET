using AnyPay.Extensions;
using AnyPay.Types.Enums;
using AnyPay.Types.Payouts;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Globalization;

namespace AnyPay.Requests.Payments;

/// <summary>
/// Use this class to create <see cref="Payout"/> request
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
internal sealed class CreatePayoutRequest : ParameterlessRequest<Payout>
{
    /// <summary>
    /// Initializes a new request to get <see cref="Payout"/>
    /// </summary>
    /// <param name="apiId">Anypay API ID</param>
    /// <param name="apiKey">Anypay API Key</param
    /// <param name="projectId">Anypay project ID</param>
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
    public CreatePayoutRequest(
        string apiId,
        string apiKey,
        int projectId,
        long payoutId,
        PaymentSystem payoutType,
        double amount,
        string wallet,
        Currency? walletCurrency = default,
        CommissionType? commissionType = default,
        string? statusUrl = default
    )
        : base("create-payout")
    {
        ProjectId = projectId;
        PayoutId = payoutId;
        PayoutType = payoutType;
        Amount = amount;
        Wallet = wallet;
        WalletCurrency = walletCurrency;
        CommissionType = commissionType;
        StatusUrl = statusUrl;
        Sign = GenerateSign(
            MethodName,
            apiId,
            payoutId.ToString(),
            payoutType.GetMemberValueOrValue(),
            amount.ToString(CultureInfo.InvariantCulture),
            wallet,
            apiKey
        );
    }

    /// <summary>
    /// Anypay project ID
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int ProjectId { get; }

    /// <summary>
    /// Unique payout number in the merchant system
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public long PayoutId { get; }

    /// <summary>
    /// Payout system:
    ///   <see cref="PaymentSystem.Qiwi"/>;
    ///   <see cref="PaymentSystem.YooMoney"/>;
    ///   <see cref="PaymentSystem.WebMoney"/> (WMZ);
    ///   <see cref="PaymentSystem.MobilePhone"/>;
    ///   <see cref="PaymentSystem.Card"/>
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(StringEnumConverter))]
    public PaymentSystem PayoutType { get; }

    /// <summary>
    /// Payout amount (for example, 100.00)
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public double Amount { get; }

    /// <summary>
    /// Recipient's wallet number (no spaces or separators)
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Wallet { get; }

    /// <summary>
    /// Recipient's wallet currency (for bank cards):
    ///  <see cref="Currency.RUB"/>;
    ///  <see cref="Currency.UAH"/>;
    ///  <see cref="Currency.KZT"/>
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [JsonConverter(typeof(StringEnumConverter))]
    public Currency? WalletCurrency { get; }

    /// <summary>
    /// Where is the commission charged from?
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [JsonConverter(typeof(StringEnumConverter))]
    public CommissionType? CommissionType { get; }

    /// <summary>
    /// This is the URL to which the GET request will be sent when the payout reaches final status
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? StatusUrl { get; }

    /// <summary>
    /// Request signature. Formed by gluing parameters together and creating a hash
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Sign { get; }
}
