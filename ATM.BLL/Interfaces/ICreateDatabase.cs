using System;
using System.Threading.Tasks;

namespace ATM.BLL.Interface
{
    public interface ICreateDatabase : IDisposable
    {
        Task CreateDB(string dataBase, string SqlQuery);
        Task DeleteDB(string dataBase, string SqlQuery);
        Task CreateTable(string table, string SqlQuery);
        Task DeleteTable(string table, string SqlQuery);
    }
}
