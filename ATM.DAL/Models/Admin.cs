using ATM.DAL.Enums;

namespace ATM.DAL.Models
{
    internal class Admin : User
    {

        public Admin(int id, string password) : base(id, password)
        {
        }

        public override Role Role { get; } = Role.Admin;

    }
}