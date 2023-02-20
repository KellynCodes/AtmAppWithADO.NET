using System;
using System.Threading.Tasks;

namespace ATM.BLL.Interface
{
    public interface ICreate : IDisposable
    {
        Task CreateDB(string dataBase, string SqlQuery);
        Task DeleteDbAsync(string dataBase, string SqlQuery);
        Task CreateTableAsync(string table, string SqlQuery);
        Task DeleteTableAsync(string table, string SqlQuery);
    }
}
