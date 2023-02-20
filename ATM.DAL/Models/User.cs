using ATM.DAL.Enums;

namespace ATM.DAL.Models
{
    public class User
    {
        public User() { }

        protected User(int id, string password)
        {
            Password = password;
            Id = id;
        }


        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserBank { get; set; }
        public string PhoneNumber { get; set; }
        public virtual Role Role { get; }

    }
}
