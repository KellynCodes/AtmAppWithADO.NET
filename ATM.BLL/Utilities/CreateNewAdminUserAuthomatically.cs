using ATM.DAL.Database.DbQueries;
using System.Threading.Tasks;
using ATM.DAL.Database;
using ATM.DAL.Models;
using ATM.DAL.Enums;
using System;

namespace ATM.BLL.Utilities
{
    public class CreateNewAdminUserAuthomatically
    {
        public static async Task NewUser()
        {
       InsertQuery insertQuery = new InsertQuery(new DbContext());
        var UserData = new User
            {
                Id = 1,
                FullName = "Kelechi Amos Omeh",
                Email = "kellyncodes@gmail.com",
                Password = "123456",
                PhoneNumber = "+234090847389024",
                UserBank = "Gt Bank",
                Role = "Admin"
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
            
            var SecondUserData = new User
            {
                Id = 2,
                FullName = "Kennedy Chisom Okoye",
                Email = "kelly@gmail.com",
                Password = "654321",
                PhoneNumber = "+234090847389024",
                UserBank = "Access Bank",
                Role = "Customer"
            };
            var SecondAccountData = new Account
            {
                UserId = 2,
                UserName = "Kelly",
                AccountNo = "1427103773",
                AccountType = AccountType.Savings,
                Balance = 100_000.15m,
                Pin = "54321",
                CreatedDate = DateTime.Now.ToLongDateString()
            };

            await insertQuery.CreateUserAndAccountAsync(AccountData, UserData);
            await insertQuery.CreateUserAndAccountAsync(SecondAccountData, SecondUserData);
        }
    }
}
