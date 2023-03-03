using System.Security.Cryptography;
using System.Text;

namespace AnyPay.Extensions;

public static class StringExtensions
{
    internal static string ToSHA256(this string source)
    {
        StringBuilder stringBuilder = new();

        byte[] result = SHA256.HashData(
            Encoding.UTF8.GetBytes(source)
        );

        foreach (byte b in result)
            stringBuilder.Append(b.ToString("x2"));

        return stringBuilder.ToString();
    }

    internal static string ToMD5(this string source)
    {
        StringBuilder stringBuilder = new();

        byte[] result = MD5.HashData(
            Encoding.UTF8.GetBytes(source)
        );

        foreach (byte b in result)
            stringBuilder.Append(b.ToString("x2"));

        return stringBuilder.ToString();
    }

    internal static string GenerateSHA256Sign(this string[] strings, string separator)
    {
        StringBuilder stringBuilder = new();

        stringBuilder.AppendJoin(separator, strings);

        return stringBuilder.ToString().ToSHA256();
    }
}
