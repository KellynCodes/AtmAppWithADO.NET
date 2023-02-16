namespace ATM.DAL.Database.QueryStrings
{
    public class SelectQueryStrings
    {
        public static string SelectFromUsersTable { get; } = @"USE Atm; SELECT Users.Id from Users where Users.Password = 12345;";
    }
}
