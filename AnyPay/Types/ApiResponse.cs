using AnyPay.Types.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace AnyPay.Types;

/// <summary>
/// Represents Anypay API response
/// </summary>
/// <typeparam name="TResult">Expected type of operation result</typeparam>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public sealed class ApiResponse<TResult> : IResponse<TResult>
{
    /// <summary>
    /// Initializes an instance of <see cref="ApiResponse{TResult}" />
    /// </summary>
    /// <param name="result">Result object</param>
    public ApiResponse(TResult result)
    {
        Result = result;
    }

    /// <summary>
    /// Initializes an instance of <see cref="ApiResponse.Error" />
    /// </summary>
    /// <param name="error">Instanse of <see cref="Error"/></param>
    public ApiResponse(Error error)
    {
        Error = error;
    }

    [JsonConstructor]
    private ApiResponse() { }

    /// <summary>
    /// Gets the result object
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [MaybeNull]
    [AllowNull]
    public TResult? Result { get; init; }

    /// <summary>
    /// Instance of <see cref="Error"/>
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [MaybeNull]
    [AllowNull]
    public Error? Error { get; init; }
}
