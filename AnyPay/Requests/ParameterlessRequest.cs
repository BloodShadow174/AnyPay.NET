using AnyPay.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AnyPay.Requests;

/// <summary>
/// Represents a request that doesn't require any parameters
/// </summary>
/// <typeparam name="TResult"></typeparam>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
internal class ParameterlessRequest<TResult> : RequestBase<TResult>
{
    /// <summary>
    /// Initializes an instance of <see cref="ParameterlessRequest{TResult}" />
    /// </summary>
    /// <param name="methodName">Name of request method</param>
    protected ParameterlessRequest(string methodName)
        : base(methodName) { }

    /// <summary>
    /// Initializes an instance of <see cref="ParameterlessRequest{TResult}" />
    /// </summary>
    /// <param name="methodName">Name of request method</param>
    /// <param name="method">HTTP request method</param>
    protected ParameterlessRequest(string methodName, HttpMethod method)
        : base(methodName, method) { }

    /// <summary>
    /// Create a request signature
    /// </summary>
    /// <param name="strings">Request parameters</param>
    /// <returns>Request signature</returns>
    protected static string GenerateSign(params string[] strings)
        => strings.GenerateSHA256Sign(string.Empty);
}
