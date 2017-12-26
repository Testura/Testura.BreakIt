using System;
using System.Linq;
using System.Text;

#pragma warning disable 1591

namespace Testura.BreakIt.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsDictionary(this Type type)
        {
            var name = type.Name;
            return name.StartsWith("IDictionary") ||
                   name.StartsWith("Dictionary");
        }

        internal static string ConvertToReadableType(this Type type)
        {
            if (!type.IsGenericType)
            {
                return type.Name;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(type.Name.Substring(0, type.Name.LastIndexOf("`", StringComparison.Ordinal)));
            sb.Append(type.GetGenericArguments().Aggregate("<", (aggregate, genericType) => aggregate + (aggregate == "<" ? string.Empty : ",") + ConvertToReadableType(genericType)));
            sb.Append(">");
            return sb.ToString();
        }
    }
}