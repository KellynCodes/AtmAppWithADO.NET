using ATM.DAL.Models;
using ATM.DAL.Enums;
using System.Collections.Generic;

namespace ATM.DAL.Database
{
    public static class AtmDB
    {
        public static IList<User> Users { get; }
            = new List<User>
            {
                new Admin(id: 1, password: "rachel1")
                {
                    FullName = "Rachel Okorie",
                    Email = "rachel@atm.com",
                    PhoneNumber = "09157060998",
                    UserBank = "Access Bank",
                },

                new Admin(id: 2, password: "david1")
                {
                    FullName = "David Obi",
                    Email = "david@atm.com",
                    PhoneNumber = "090099883",
                    UserBank = " Fidelity Bank"
                },

                new Customer(id: 3, password: "kelechi1")
                {
                    FullName = "Amos Kelechi",
                    Email = "amos@atm.com",
                    PhoneNumber = "090192383",
                    UserBank = "Access Bank"
                },


                new Customer(id: 4, password: "john1")
                {
                    FullName = "Johnson Pink",
                    Email = "pink@atm.com",
                    PhoneNumber = "090091883",
                    UserBank = "Ecobank"
                },

                new ThirdParty(id: 5,password: "goody1")
                {
                FullName = "Goodness Kalu",
                Email = "goody@atm.com",
                PhoneNumber = "090092300",
                UserBank = "First Bank"
            },

            new ThirdParty(id: 6,password: "caleb1")
            {
                FullName = "Caleb Okechukwu",
                Email = "caleb@atm.com",
                PhoneNumber = "090099983",
                UserBank = " Fidelity Bank"
            }
            };

        public static IList<Account> Account { get; }
            = new List<Account>
            {
                new Account
                {
                    Id = 1,
                    UserId = 1,
                    UserName = "KellynCodes",
                    AccountNo = "0669976019",
                    AccountType = AccountType.Current,
                    Balance = 2_000_000.01m

                },
                new Account
                {
                    Id = 2,
                    UserId = 2,
                    UserName = "Kelechi Amos",
                    AccountNo = "0011223456",
                    AccountType = AccountType.Savings,
                    Balance = 2_000

                },

                new Account
                {
                    Id = 3,
                    UserId = 3,
                    AccountNo = "0022223456",
                    AccountType = AccountType.Savings,
                    Balance = 2_000

                },

                new Account
                {
                    Id = 4,
                    UserId = 4,
                    AccountNo = "0033223456",
                    AccountType = AccountType.Current,
                    Balance = 2000_000_000_000.02m

                }

            };
    }
}
