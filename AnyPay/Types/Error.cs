using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AnyPay.Types;

/// <summary>
/// Error from response
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public sealed class Error
{
    /// <summary>
    /// Create instance of <see cref="Error"/>
    /// </summary>
    /// <param name="code">Error code</param>
    /// <param name="message">Error message</param>
    public Error(int code, string message)
    {
        Code = code;
        Message = message;
    }

    [JsonConstructor]
    private Error() { }

    /// <summary>
    /// Error code from response
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Code { get; init; }

    /// <summary>
    /// Error message from response
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Message { get; init; } = null!;
}
