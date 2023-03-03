using AnyPay.Exceptions;
using AnyPay.Extensions;
using AnyPay.Helpers;
using AnyPay.Requests.Abstractions;
using AnyPay.Types;
using AnyPay.Types.Enums;
using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;

namespace AnyPay;

public class AnyPayClient
{
    private readonly string _apiId;

    private readonly string _apiKey;

    private readonly string _secretKey;

    private readonly int _projectId;

    private readonly string _apiUrl;

    private readonly string _merchantUrl;

    private readonly HttpClient _httpClient;

    internal string ApiId
    {
        get => _apiId;
    }

    internal string ApiKey
    {
        get => _apiKey;
    }

    internal int ProjectId
    {
        get => _projectId;
    }

    public AnyPayClient(
        string apiId,
        string apiKey,
        string secretKey,
        int projectId,
        HttpClient? httpClient = null
    )
    {
        _apiId = apiId;
        _apiKey = apiKey;
        _secretKey = secretKey;
        _projectId = projectId;
        _apiUrl = "https://anypay.io/api";
        _merchantUrl = "https://anypay.io/merchant";
        _httpClient = httpClient ?? new HttpClient();
    }

    internal Uri MakeMerchantUri(long payId, double amount, Currency currency)
    {
        if (payId <= 0)
            throw new MerchantException($"Argument {nameof(payId)} cannot be 0 or less than zero.");

        if (payId > 99999999999999)
            throw new MerchantException($"Argument {nameof(payId)} can't be greater than 99999999999999.");

        if (amount <= 0)
            throw new MerchantException($"Argument {nameof(amount)} cannot be 0 or less than zero.");

        string amountString = amount.ToString(CultureInfo.InvariantCulture);

        var urlParameters = new Dictionary<string, string>
        {
            { "merchant_id", _projectId.ToString() },
            { "pay_id", payId.ToString() },
            { "amount", amountString },
            { "currency", currency.ToString() },
            { "sign", $"{currency}:{amountString}:{_secretKey}:{_projectId}:{payId}".ToMD5() },
        };

        return new Uri(_merchantUrl).AddParameters(urlParameters);
    }

    internal async Task<TResponse> MakeRequestAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default
    )
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        var uri = $"{_apiUrl}/{request.MethodName}/{_apiId}";

        using var httpRequest = new HttpRequestMessage(request.HttpMethod, uri)
        {
            Content = request.ToHttpContent(true)
        };

        using var httpResponse = await SendRequestAsync(
            httpClient: _httpClient,
            httpRequest: httpRequest,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        if (httpResponse.StatusCode != HttpStatusCode.OK)
            throw ExceptionParser.Parse(httpResponse.StatusCode);

        var apiResponse = await httpResponse
            .DeserializeContentAsync<ApiResponse<TResponse>>(
                guard: response => response.Result == null && response == null
            )
            .ConfigureAwait(false);

        if (apiResponse.Error != null)
            throw ExceptionParser.Parse(apiResponse);

        return apiResponse.Result!;

        [MethodImpl(methodImplOptions: MethodImplOptions.AggressiveInlining)]
        static async Task<HttpResponseMessage> SendRequestAsync(
            HttpClient httpClient,
            HttpRequestMessage httpRequest,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage? httpResponse;
            try
            {
                httpResponse = await httpClient
                    .SendAsync(request: httpRequest, cancellationToken: cancellationToken)
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (TaskCanceledException exception)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }

                throw new RequestException(message: "Request timed out", innerException: exception);
            }
            catch (Exception exception)
            {
                throw new RequestException(
                    message: "Exception during making request",
                    innerException: exception
                );
            }

            return httpResponse;
        }
    }
}
