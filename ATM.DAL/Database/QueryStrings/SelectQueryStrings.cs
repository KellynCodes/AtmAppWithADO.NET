namespace ATM.DAL.Database.QueryStrings
{
    public class SelectQueryStrings
    {
        public static string GetUserQuery { get; } = $"USE AtmMachine; SELECT Users.FullName, Users.Email, Users.Password, Users.UserBank, Users.PhoneNumber FROM Users WHERE Email = @Email AND Password = @Password";

        public static string GetAdminQuery { get; } = $"USE AtmMachine; SELECT Users.FullName, Users.Email, Users.Password, Users.UserBank, Users.PhoneNumber FROM Users WHERE Email = @Email AND Password = @Password AND Role = 'Admin'";
    }
}
