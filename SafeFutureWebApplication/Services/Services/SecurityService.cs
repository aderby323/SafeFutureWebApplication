using System.Collections.Generic;
using System.Linq;

namespace SafeFutureWebApplication.Services
{
    public static class SecurityService
    {
        public static bool IsAdmin(IEnumerable<string> membership)
        {
            return membership.Any(x => x.Contains("Admin"));
        }

        public static bool IsStaff(IEnumerable<string> membership)
        {
            return membership.Any(x => x.Contains("Staff"));
        }
    }
}
