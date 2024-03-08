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

public class AnyPayClient(string apiId, string apiKey, string secretKey, int projectId, HttpClient? httpClient = default)
{
    public const long MaxPayId = 99999999999999;

    private readonly string _apiId = apiId;

    private readonly string _apiKey = apiKey;

    private readonly string _secretKey = secretKey;

    private readonly int _projectId = projectId;

    private readonly string _apiUrl = "https://anypay.io/api";

    private readonly string _merchantUrl = "https://anypay.io/merchant";

    private readonly HttpClient _httpClient = httpClient ?? new HttpClient();

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

    /// <summary>
    /// Generate a special URL to initiate payment.
    /// </summary>
    /// <param name="payId">Order number in the seller's system (up to 15 characters from the characters "0-9")</param>
    /// <param name="amount">Payment amount (for example, 100.00)</param>
    /// <param name="currency">
    /// Payment currency according to ISO 4217:
    ///   RUB - Russian ruble (default);
    ///   UAH - Ukrainian hryvnia;
    ///   BYN - Belarusian ruble;
    ///   KZT - Kazakhstani tenge;
    ///   USD - US dollar;
    ///   EUR - Euro
    /// </param>
    /// <param name="desc">Short description of the payment (up to 150 symbols)</param>
    /// <param name="email">Payer's email address</param>
    /// <param name="phone">Payer phone number (for example, 79990000000)</param>
    /// <param name="method">Payment method (see <see cref="PaymentSystem"/>)</param>
    /// <param name="successUrl">Forwarding address in case of successful payment</param>
    /// <param name="failUrl">Forwarding address in case of unsuccessful payment</param>
    /// <param name="lang">
    /// Payment page interface language:
    ///   ru - Russian (default);
    ///   en - English
    /// </param>
    /// <param name="additionalProperties">Additional seller parameters</param>
    /// <param name="signType">Request signature type</param>
    /// <returns>Special URL to initiate payment</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    internal Uri MakeMerchantUri(
        long payId,
        double amount,
        Currency currency,
        string? desc = default,
        string? email = default,
        long? phone = default,
        PaymentSystem? method = default,
        string? successUrl = default,
        string? failUrl = default,
        string? lang = default,
        IDictionary<string, string>? additionalProperties = default,
        SignType signType = SignType.MD5
    )
    {
        if (payId <= 0 || payId > MaxPayId)
            throw new ArgumentOutOfRangeException(
                paramName: nameof(payId),
                message: $"PayId must be a positive integer not greater than {MaxPayId}."
            );

        if (amount <= 0)
            throw new ArgumentOutOfRangeException(
                paramName: nameof(amount),
                message: "Amount must be greater than zero."
            );

        var urlParameters = new Dictionary<string, string>();

        string amountString = amount.ToString(CultureInfo.InvariantCulture);

        urlParameters.AddOptionalParameter(
            key: "merchant_id",
            value: _projectId.ToString()
        );
        urlParameters.AddOptionalParameter(
            key: "pay_id",
            value: payId.ToString()
        );
        urlParameters.AddOptionalParameter(
            key: "amount",
            value: amountString
        );
        urlParameters.AddOptionalParameter(
            key: "currency",
            value: currency.ToString()
        );
        urlParameters.AddOptionalParameter(
            key: "desc",
            value: desc
        );
        urlParameters.AddOptionalParameter(
            key: "email",
            value: email
        );
        urlParameters.AddOptionalParameter(
            key: "phone",
            value: phone?.ToString()
        );
        urlParameters.AddOptionalParameter(
            key: "method",
            value: method?.ToString()
        );
        urlParameters.AddOptionalParameter(
            key: "success_url",
            value: successUrl
        );
        urlParameters.AddOptionalParameter(
            key: "fail_url",
            value: failUrl
        );
        urlParameters.AddOptionalParameter(
            key: "lang",
            value: lang
        );
        urlParameters.AddOptionalParameter(
            key: "sign",
            value: CalculateSign(
                signType: signType,
                payId: payId,
                amountString: amountString,
                currency: currency,
                desc: desc,
                successUrl: successUrl,
                failUrl: failUrl
            )
        );

        if (additionalProperties != null)
        {
            foreach (var additionalProperty in additionalProperties)
            {
                urlParameters.AddOptionalParameter(
                    additionalProperty.Key,
                    additionalProperty.Value
                );
            }
        }

        return new Uri(_merchantUrl).AddParameters(urlParameters);
    }
    //_projectId = merchant_id
    private string CalculateSign(
        SignType signType,
        long payId,
        string amountString,
        Currency currency,
        string? desc,
        string? successUrl,
        string? failUrl
    )
    {
        if (signType is SignType.MD5)
        {
            return $"{currency}:{amountString}:{_secretKey}:{_projectId}:{payId}".ToMD5();
        }

        if (signType is SignType.SHA256)
        {
            return $"{_projectId}:{payId}:{amountString}:{currency}:{desc}:{successUrl}:{failUrl}".ToSHA256();
        }

        throw new MerchantException($"Sign type {signType} is not supported.");
    }

    internal async Task<TResponse> MakeRequestAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(request);

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
