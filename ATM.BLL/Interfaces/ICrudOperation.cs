using ATM.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATM.DAL.Database.Interface
{
    public interface ICrudOperation : IDisposable
    {
            Task CreateUser(User user);

            Task<long> UpdateUser(int userId, User user);

            Task DeleteUser(int UserId);

            Task<User> GetUser(int id);

            Task<IEnumerable<User>> GetUsers();
    }
}
