using AnyPay.Extensions;
using AnyPay.Requests.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace AnyPay.Requests;

/// <inheritdoc/>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
internal class RequestBase<TResponse> : IRequest<TResponse>
{
    /// <inheritdoc />
    [JsonIgnore]
    public HttpMethod HttpMethod { get; }

    /// <inheritdoc />
    [JsonIgnore]
    public string MethodName { get; }

    /// <summary>
    /// Initializes an instance of request
    /// </summary>
    /// <param name="methodName">Anypay API method</param>
    protected RequestBase(string methodName)
        : this(methodName, HttpMethod.Post) { }

    /// <summary>
    /// Initializes an instance of request
    /// </summary>
    /// <param name="methodName">Anypay API method</param>
    /// <param name="httpMethod">HTTP method to use</param>
    protected RequestBase(string methodName, HttpMethod httpMethod)
    {
        MethodName = methodName;
        HttpMethod = httpMethod;
    }

    /// <inheritdoc />
    public virtual HttpContent ToHttpContent(bool formUrlEncoded)
    {
        if (formUrlEncoded)
        {
            var keyValues = this.ToKeyValue();

            if (keyValues == null)
                throw new NullReferenceException(nameof(keyValues));

            return new FormUrlEncodedContent(keyValues);
        }

        var payload = JsonConvert.SerializeObject(this);

        return new StringContent(payload, Encoding.UTF8, "application/json");
    }
}
