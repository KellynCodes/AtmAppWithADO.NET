namespace ATM.DAL.Models
{
    internal class ThirdParty : User
    {
        public ThirdParty(int id, string password) : base(id, password) { }
        public override string Role { get; set; }
        public int Account { get; set; }
    }
}