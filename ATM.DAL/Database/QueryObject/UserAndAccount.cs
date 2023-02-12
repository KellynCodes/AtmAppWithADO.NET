using ATM.DAL.Enums;
using System;

namespace ATM.DAL.Database.QueryObject
{
    internal class UserAndAccount
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserBank { get; set; }
        public string PhoneNumber { get; set; }
        public Role Role { get; set; }

        public long UserId { get; set; }
        public string UserName { get; set; }
        public string AccountNo { get; set; }
        public AccountType AccountType { get; set; }
        public string Pin { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
