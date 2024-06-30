namespace Infrastructure.Identity
{
    [Flags]
    public enum UserRoles
    {
        None     = 0,
        Admin    = 1,
        Employee = 2,
        Customer = 4,

        AdminOrCustomer           = Admin | Customer,
        AdminOrEmployee           = Admin | Employee,
        AdminOrEmployeeOrCustomer = Admin | Employee | Customer
    }
}
