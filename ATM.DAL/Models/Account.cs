using ATM.DAL.Enums;

namespace ATM.DAL.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string AccountNo { get; set; }
        public AccountType AccountType { get; set; }
        public string Pin { get; set; }
        public decimal Balance { get; set; }
        public string CreatedDate { get; set; }

    }
}