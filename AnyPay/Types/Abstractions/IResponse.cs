namespace AnyPay.Types.Abstractions;

/// <summary>
/// Response from Anypay service
/// </summary>
public interface IResponse
{
    /// <summary>
    /// Response error
    /// </summary>
    Error? Error { get; init; }
}
