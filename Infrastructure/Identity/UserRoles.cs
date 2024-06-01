namespace Infrastructure.Identity
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string User = "User";
        public const string AdminOrUser = Admin + "," + User;
        public const string AdminOrManager = Admin + "," + Manager;
        public const string AdminOrManagerOrUser = Admin + "," + Manager + "," + User;
    }
}
