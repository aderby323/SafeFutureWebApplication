using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeFutureWebApplication
{
    public static class Extensions
    {
        public static bool IsNullOrWhitespace(string value)
        {
            return !(value.Length <= 0 || value.Trim().Length <= 0) ? true : false;
        }

        public static bool IsNullOrEmpty()
        {
            throw new NotImplementedException();
        }
    }
}
