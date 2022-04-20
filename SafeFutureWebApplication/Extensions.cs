using SafeFutureWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SafeFutureWebApplication
{
    public static class Extensions
    {
        public static bool IsNullOrWhitespace(this string value) => (value is null ||  value.Trim().Length <= 0);

        public static bool IsNullOrEmpty(this IEnumerable<object> data) => data == null || !data.Any();

        public static void SetModified<T>(this T x, string requester) where T: IAuditable
        {
            x.CreatedOn = DateTime.Now;
            x.CreatedBy = requester;
        }
    }
}
