using ATM.BLL.Implementation;
using ATM.BLL.Interface;
using System.Threading.Tasks;
using ATM.DAL.Database;
using static ATM.DAL.Database.QueryStrings.CreateQueryStrings;

namespace ATM.BLL.Utilities
{
    public class SqlQueryMethods
    {
        private readonly static ICreateDatabase createDatabase = new CreateDatabase(new DbContext());
        public static async Task Run()
        {
            await createDatabase.CreateDB("Atm", AtmDbQueryString);

            await createDatabase.CreateTable("Users", UserTableQueryString);
            await createDatabase.CreateTable("Account", AccountTableQueryString);
            await createDatabase.CreateTable("Admin", AdminTableQueryString);
            await createDatabase.CreateTable("Customer", CustomerTableQueryString);
            await createDatabase.CreateTable("ThirdParty", ThirdPartyQueryString);
            await createDatabase.CreateTable("Bank", BankTableQueryString);
            await createDatabase.CreateTable("Bill", BillTableQueryString);
        }
    }
}
