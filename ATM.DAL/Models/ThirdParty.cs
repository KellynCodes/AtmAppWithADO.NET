using ATM.DAL.Enums;

namespace ATM.DAL.Models
{
    internal class ThirdParty : User
    {
        public ThirdParty(int id, string password) : base(id, password) { }
        public override Role Role { get; } = Role.ThirdParty;
        public Account Account { get; set; }
    }
}