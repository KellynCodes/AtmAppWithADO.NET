namespace ATM.DAL.Database.QueryStrings
{
    public class InsertQuery
    {
        public static string AtmInfoQuery { get; } = @"USE Atm; INSERT INTO AtmInfo(Name, AvailableCash, Language) VALUES('Gt Tech', '600000.00', 'English')";
    }
}
