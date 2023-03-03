using System.Reflection;
using System.Runtime.Serialization;

namespace AnyPay.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Get the value of an enum member, or a value
    /// </summary>
    /// <typeparam name="T"><see cref="Enum"/></typeparam>
    /// <param name="value"><see cref="Enum"/></param>
    /// <returns>Value of an enum member, or a value</returns>
    public static string GetMemberValueOrValue<T>(this T value) where T : Enum
    {
        return value
            .GetType()
            .GetTypeInfo()
            .DeclaredMembers
            .SingleOrDefault(x => x.Name == value.ToString())
            ?.GetCustomAttribute<EnumMemberAttribute>(false)
            ?.Value
            ?? value.ToString();
    }
}
