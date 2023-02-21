using static ATM.DAL.Database.QueryStrings.CreateQueryStrings;
using ATM.BLL.Implementation;
using System.Threading.Tasks;
using ATM.BLL.Interfaces;
using ATM.BLL.Interface;
using ATM.DAL.Database;
using System;
using ATM.DAL.Database.DbQueries;

namespace ATM.BLL.Utilities
{
    public class CreatDb
    {
        private readonly static ICreate create = new Create(new DbContext());
        private readonly static InsertQuery insertQuery = new InsertQuery(new DbContext());
        private readonly static IMessage message = new Message();

        public static async Task<string> Run()
        {
            try
            {

                await create.CreateDB("AtmMachine", AtmDbQueryString);

                await create.CreateTableAsync("AtmInfo", AtmTableQueryString);
                await create.CreateTableAsync("Users", UserTableQueryString);
                await create.CreateTableAsync("Account", AccountTableQueryString);
                await create.CreateTableAsync("Transactions", TransactionTableQueryString);
                await create.CreateTableAsync("Admin", AdminTableQueryString);
                await create.CreateTableAsync("Customer", CustomerTableQueryString);
                await create.CreateTableAsync("ThirdParty", ThirdPartyQueryString);
                await create.CreateTableAsync("Bank", BankTableQueryString);
                await create.CreateTableAsync("Bill", BillTableQueryString);

                await CreateNewAdminUserAuthomatically.NewUser();
                await insertQuery.InsertIntoAtmInfoAsync(name: "Z-Tech Limited", amount: 1_000_000_000, language: "English");
                await insertQuery.InsertIntoAtmInfoAsync(name: "GTech", amount: 1_000_000_000_000, language: "English");
            }
            catch (Exception exception)
            {
                message.AlertInfo(exception.Message);
                return "";
            }
                return "Query was successfull";
        }
    }
}
