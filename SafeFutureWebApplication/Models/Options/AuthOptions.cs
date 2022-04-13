
namespace SafeFutureWebApplication.Models.Options
{
    public class AuthOptions
    {
        public const string Key = "AuthorizedRoles";
        public string[] AdminGroup { get; set; }
        public string[] StaffGroup { get; set; }
        public string[] DevGroup { get; set; }
    }
}
