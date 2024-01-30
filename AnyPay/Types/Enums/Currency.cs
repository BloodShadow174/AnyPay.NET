using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AnyPay.Types.Enums;

/// <summary>
/// Payment currency according to ISO 4217 (cryptocurrencies have sequence numbers from 10000)
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum Currency
{
    /// <summary>
    /// Russian Ruble
    /// </summary>
    RUB = 643,
    /// <summary>
    /// Ukrainian hryvnia
    /// </summary>
    UAH = 980,
    /// <summary>
    /// Belarussian Ruble
    /// </summary>
    BYN = 933,
    /// <summary>
    /// Kazakhstani tenge
    /// </summary>
    KZT = 398,
    /// <summary>
    /// United States dollar
    /// </summary>
    USD = 840,
    /// <summary>
    /// Euro
    /// </summary>
    EUR = 978,

    /// <summary>
    /// Bitcoin
    /// </summary>
    BTC = 10000,
    /// <summary>
    /// Ethereum
    /// </summary>
    ETH = 10001,
    /// <summary>
    /// Bitcoin Cash
    /// </summary>
    BCH = 10002,
    /// <summary>
    /// Litecoin
    /// </summary>
    LTC = 10003,
    /// <summary>
    /// Dash
    /// </summary>
    DASH = 10004,
    /// <summary>
    /// Zcash
    /// </summary>
    ZEC = 10005,
    /// <summary>
    /// Dogecoin
    /// </summary>
    DOGE = 10006,
    /// <summary>
    /// Tether
    /// </summary>
    USDT = 10007,
}
