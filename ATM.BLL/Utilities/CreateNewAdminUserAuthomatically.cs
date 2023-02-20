using ATM.DAL.Database.DbQueries;
using ATM.DAL.Enums;
using System.Threading.Tasks;
using ATM.DAL.Database;
using ATM.DAL.Models;
using System;

namespace ATM.BLL.Utilities
{
    public class CreateNewAdminUserAuthomatically
    {
        public static async Task NewUser()
        {
            DbQuery dbQuery = new DbQuery(new DbContext());
            var UserData = new User
            {
                FullName = "Kelechi Amos Omeh",
                Email = "kellyncodes@gmail.com",
                Password = "123456",
                PhoneNumber = "+234090847389024",
                UserBank = "Gt Bank",
            };
            var AccountData = new Account
            {
                UserId = 1,
                UserName = "KellynCodes",
                AccountNo = "0669976019",
                AccountType = AccountType.Savings,
                Balance = 100_000_000,
                Pin = "12345",
                CreatedDate = DateTime.Now.ToLongDateString()
            };

            await dbQuery.CreateUserAndAccountAsync(AccountData, UserData);
        }
    }
}
