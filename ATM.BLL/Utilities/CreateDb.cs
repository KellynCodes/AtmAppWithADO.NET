using ATM.BLL.Implementation;
using System.Threading.Tasks;
using ATM.BLL.Interface;
using ATM.DAL.Database;
using System;

using static ATM.DAL.Database.QueryStrings.CreateQueryStrings;

namespace ATM.BLL.Utilities
{
    public class CreatDb
    {
        private readonly static ICreate create = new Create(new DbContext());
        public static async Task<string> Run()
        {
            try
            {

                await create.CreateDB("Atm", AtmDbQueryString);

                await create.CreateTableAsync("AtmInfo", AtmTableQueryString);
                await create.CreateTableAsync("Users", UserTableQueryString);
                await create.CreateTableAsync("Account", AccountTableQueryString);
                await create.CreateTableAsync("Admin", AdminTableQueryString);
                await create.CreateTableAsync("Customer", CustomerTableQueryString);
                await create.CreateTableAsync("ThirdParty", ThirdPartyQueryString);
                await create.CreateTableAsync("Bank", BankTableQueryString);
                await create.CreateTableAsync("Bill", BillTableQueryString);
                return "Query was successfull";
            }
            catch (Exception exceptionMessage)
            {

                Console.WriteLine(exceptionMessage);
                throw exceptionMessage;
            }
        }
    }
}
