using SafeFutureWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SafeFutureWebApplication
{
    public static class Extensions
    {
        public static bool IsNullOrWhitespace(this string value)
        {
            return (value is null || value.Length <= 0 || value.Trim().Length <= 0);
        }

        public static bool IsNullOrEmpty(this IEnumerable<object> data)
        {
            return data != null && data.Any();
        }

        public static void SetModified<T>(this T x, string requester) where T: IAuditable
        {
            x.CreatedOn = DateTime.UtcNow;
            x.CreatedBy = requester;
        }
    }
}
