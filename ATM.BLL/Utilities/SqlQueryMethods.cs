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
        public static async Task<string> Run()
        {
           // await createDatabase.CreateDB("Atm", AtmDbQueryString);

           // await createDatabase.CreateTableAsync("Users", UserTableQueryString);
          //  await createDatabase.CreateTableAsync("Account", AccountTableQueryString);
          //  await createDatabase.CreateTableAsync("Admin", AdminTableQueryString);
           // await createDatabase.CreateTableAsync("Customer", CustomerTableQueryString);
          //  await createDatabase.CreateTableAsync("ThirdParty", ThirdPartyQueryString);
            //await createDatabase.CreateTableAsync("Bank", BankTableQueryString);
          //  await createDatabase.CreateTableAsync("Bill", BillTableQueryString);
            return "Query was successfull";
        }
    }
}
