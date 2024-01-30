using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace AnyPay.Types.Enums;

/// <summary>
/// Payment systems
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum PaymentSystem
{
    [EnumMember(Value = "qiwi")]
    Qiwi = 1,
    [EnumMember(Value = "ym")]
    YooMoney = 2,
    [EnumMember(Value = "wm")]
    WebMoney = 3,
    [EnumMember(Value = "mp")]
    [Obsolete("No longer supported.")]
    MobilePhone = 4,
    [EnumMember(Value = "card")]
    Card = 5,
    [EnumMember(Value = "advcash")]
    Advcash = 6,
    [EnumMember(Value = "pm")]
    PerfectMoney = 7,
    [EnumMember(Value = "applepay")]
    ApplePay = 8,
    [EnumMember(Value = "googlepay")]
    GooglePay = 9,
    [EnumMember(Value = "samsungpay")]
    SamsungPay = 10,
    [EnumMember(Value = "sbp")]
    SBP = 11,
    [EnumMember(Value = "payeer")]
    Payeer = 12,
    [EnumMember(Value = "btc")]
    Bitcoin = 13,
    [EnumMember(Value = "eth")]
    Ethereum = 14,
    [EnumMember(Value = "bch")]
    BitcoinCash = 15,
    [EnumMember(Value = "ltc")]
    Litecoin = 16,
    [EnumMember(Value = "dash")]
    Dash = 17,
    [EnumMember(Value = "zec")]
    Zcash = 18,
    [EnumMember(Value = "doge")]
    Dogecoin = 19,
    [EnumMember(Value = "usdt")]
    Tether = 20,
    [EnumMember(Value = "mts")]
    Mts = 21,
    [EnumMember(Value = "beeline")]
    Beeline = 22,
    [EnumMember(Value = "megafon")]
    Megafon = 23,
    [EnumMember(Value = "tele2")]
    Tele2 = 24,
    [EnumMember(Value = "exmo")]
    [Obsolete("No longer supported.")]
    Exmo = 25,
    [EnumMember(Value = "term")]
    Terminal = 26,
}
