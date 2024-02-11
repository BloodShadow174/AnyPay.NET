namespace AnyPay.Requests.Abstractions;

/// <summary>
/// Represents a request to AnyPay API
/// </summary>
internal interface IRequest
{
    /// <summary>
    /// HTTP method of request
    /// </summary>
    HttpMethod HttpMethod { get; }

    /// <summary>
    /// API method name
    /// </summary>
    string MethodName { get; }

    /// <summary>
    /// Generate content of HTTP message
    /// </summary>
    /// <param name="formUrlEncoded">If True returned FormUrlEncoded, else StringContent</param>
    /// <returns>Content of HTTP request</returns>
    HttpContent ToHttpContent(bool formUrlEncoded);
}
