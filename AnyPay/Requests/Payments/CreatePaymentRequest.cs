using AnyPay.Extensions;
using AnyPay.Types.Enums;
using AnyPay.Types.Payments;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Globalization;

namespace AnyPay.Requests.Payments;

/// <summary>
/// Use this class to create <see cref="CreatedPayment"/> request
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
internal sealed class CreatePaymentRequest : ParameterlessRequest<CreatedPayment>
{
    /// <summary>
    /// Initializes a new request to get <see cref="CreatedPayment"/>
    /// </summary>
    /// <param name="apiId">Anypay API ID</param>
    /// <param name="apiKey">Anypay API Key</param
    /// <param name="projectId">Anypay project ID</param>
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
    public CreatePaymentRequest(
        string apiId,
        string apiKey,
        int projectId,
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
        string? lang = default
    )
        : base("create-payment")
    {
        ProjectId = projectId;
        PayId = payId;
        Amount = amount;
        Currency = currency;
        Desc = desc;
        Email = email;
        Method = method;
        MethodCurrency = methodCurrency;
        Phone = phone;
        Tail = tail;
        SuccessUrl = successUrl;
        FailUrl = failUrl;
        Lang = lang;
        Sign = GenerateSign(
            MethodName,
            apiId,
            projectId.ToString(),
            payId.ToString(),
            amount.ToString(CultureInfo.InvariantCulture),
            currency.ToString(),
            desc,
            method.GetMemberValueOrValue(),
            apiKey
        );
    }

    /// <summary>
    /// Anypay project ID
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int ProjectId { get; }

    /// <summary>
    /// Order number in the seller's system (up to 15 characters from the characters "0-9")
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public long PayId { get; }

    /// <summary>
    /// Payment amount (for example, 100.00)
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public double Amount { get; }

    /// <summary>
    /// Payment currency according to ISO 4217:
    ///   RUB - Russian ruble (default);
    ///   UAH - Ukrainian hryvnia;
    ///   BYN - Belarusian ruble;
    ///   KZT - Kazakhstani tenge;
    ///   USD - US dollar;
    ///   EUR - Euro
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(StringEnumConverter))]
    public Currency Currency { get; }

    /// <summary>
    /// Short description of the payment (up to 150 symbols)
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Desc { get; }

    /// <summary>
    /// Payment method (see <see cref="PaymentSystem"/>)
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public PaymentSystem Method { get; }

    /// <summary>
    /// Currency of payment method:
    /// Webmoney (USD, EUR);
    /// Advcash (RUB, USD, EUR);
    /// Perfect Money (USD, EUR)
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [JsonConverter(typeof(StringEnumConverter))]
    public Currency? MethodCurrency { get; }

    /// <summary>
    /// Payer's email address
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Email { get; }

    /// <summary>
    /// Payer phone number (for example, 79990000000)
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public long? Phone { get; }

    /// <summary>
    /// Last 4 digits of card number (for card <see cref="PaymentSystem">method</see>)
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Tail { get; }

    /// <summary>
    /// Forwarding address in case of successful payment
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? SuccessUrl { get; }

    /// <summary>
    /// Forwarding address in case of unsuccessful payment
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? FailUrl { get; }

    /// <summary>
    /// Payment page interface language:
    ///   ru - Russian (default);
    ///   en - English
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Lang { get; }

    /// <summary>
    /// Request signature. Formed by gluing parameters together and creating a hash
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Sign { get; }
}
