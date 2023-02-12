using ATM.DAL.Enums;

namespace ATM.DAL.Models
{
    public class Customer : User
    {
        public Customer(long id, string password) : base(id, password) { }
        public override Role Role { get; } = Role.Customer;
        public Account Account { get; set; }
    }
}