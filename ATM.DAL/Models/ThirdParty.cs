using ATM.DAL.Enums;

namespace ATM.DAL.Models
{
    internal class ThirdParty : User
    {
        public ThirdParty(long id, string password) : base(id, password) { }
        public override Role Role { get; } = Role.ThirdParty;
        public Account Account { get; set; }
    }
}