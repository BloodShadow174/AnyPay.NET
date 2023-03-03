namespace AnyPay.Exceptions;

public class ApiRequestException : RequestException
{
    public virtual int ErrorCode { get; }

    public ApiRequestException(string message)
        : base(message) { }

    public ApiRequestException(string message, int errorCode)
        : base(message) => ErrorCode = errorCode;

    public ApiRequestException(string message, Exception innerException)
        : base(message, innerException) { }

    public ApiRequestException(string message, int errorCode, Exception innerException)
        : base(message, innerException) => ErrorCode = errorCode;
}
