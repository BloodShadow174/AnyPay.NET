using AnyPay.Exceptions;
using AnyPay.Types.Abstractions;
using System.Net;

namespace AnyPay.Helpers;

internal class ExceptionParser
{
    internal static ApiRequestException Parse(IResponse apiResponse)
    {
        if (apiResponse is null)
            throw new ArgumentNullException(nameof(apiResponse));

        if(apiResponse.Error is null)
            throw new NullReferenceException(nameof(apiResponse.Error));

        return new(
            message: apiResponse.Error.Message,
            errorCode: apiResponse.Error.Code
        );
    }

    internal static ApiRequestException Parse(HttpStatusCode statusCode)
    {
        return new(
            message: $"Response error: {statusCode}",
            errorCode: Convert.ToInt32(statusCode)
        );
    }
}
