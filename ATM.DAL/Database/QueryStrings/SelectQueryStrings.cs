namespace ATM.DAL.Database.QueryStrings
{
    public class SelectQueryStrings
    {
        public static string GetUserQuery {get; set;} = $"USE Atm; SELECT Users.FullName, Users.Email, Users.Password, Users.UserBank, Users.PhoneNumber FROM Users WHERE Email = @Email AND Password = @Password";

        public static string GetAdminQuery { get; set; } = $"USE Atm; SELECT Users.FullName, Users.Email, Users.Password, Users.UserBank, Users.PhoneNumber FROM Users WHERE Email = @Email AND Password = @Password AND Role = 'Admin'";
    }
}
