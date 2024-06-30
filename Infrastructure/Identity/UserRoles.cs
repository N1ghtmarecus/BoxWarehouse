namespace Infrastructure.Identity
{
    [Flags]
    public enum UserRoles
    {
        None     = 0,
        Admin    = 1,
        Employee = 2,
        Customer = 4
    }
}
