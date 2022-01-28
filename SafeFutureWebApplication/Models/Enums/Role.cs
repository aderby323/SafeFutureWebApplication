namespace SafeFutureWebApplication.Models
{
    public enum Role
    {
        Admin,
        Staff,
        Dev = Admin | Staff
    }
}
