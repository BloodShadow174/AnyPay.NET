using System.Web;

namespace AnyPay.Extensions;

internal static class UriExtensions
{
    /// <summary>
    /// Adds the specified parameter to the Query String.
    /// </summary>
    /// <param name="url"></param>
    /// <param name="paramName">Name of the parameter to add.</param>
    /// <param name="paramValue">Value for the parameter to add.</param>
    /// <returns>Url with added parameter.</returns>
    public static Uri AddParameter(this Uri url, string paramName, string? paramValue)
    {
        var uriBuilder = new UriBuilder(url);

        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        query.Add(paramName, paramValue);

        uriBuilder.Query = query.ToString();

        return uriBuilder.Uri;
    }

    public static Uri AddParameters(this Uri url, IDictionary<string, string?> urlParameters)
    {
        var uriBuilder = new UriBuilder(url);

        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (var parameter in urlParameters)
        {
            query.Add(parameter.Key, parameter.Value);
        }

        uriBuilder.Query = query.ToString();

        return uriBuilder.Uri;
    }
}
