namespace AnyPay.Types.Abstractions;

/// <inheritdoc />
public interface IResponse<TResult> : IResponse
{
    /// <summary>
    /// Response result
    /// </summary>
    public TResult? Result { get; init; }
}
