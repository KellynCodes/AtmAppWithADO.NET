using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System;
using ATM.BLL.Utilities;
using ATM.DAL.Database;
using ATM.BLL.Interface;

namespace ATM.BLL.Implementation
{
    public class CreateDatabase : ICreateDatabase
    {
        private bool _disposed;
        private readonly DbContext _dbContext;

        public CreateDatabase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

       public async Task CreateDB(string dataBase, string SqlQuery)
        {
            SqlConnection DbConnection = await _dbContext.OpenConnection();

            using (SqlCommand command = new SqlCommand(SqlQuery, DbConnection))
            {
              int Result = command.ExecuteNonQuery();
                string Message = $"{dataBase} created successfully.";
                QueryResult.Result(Message);
            }

        }

       public async Task CreateTable(string table, string SqlQuery)
        {
            SqlConnection DbConnection = await _dbContext.OpenConnection();

            using (SqlCommand command = new SqlCommand(SqlQuery, DbConnection))
            {
                int Result = command.ExecuteNonQuery();
                string Message = $"{table} created successfully.";
                QueryResult.Result(Message);
            }

        }

        public async Task DeleteDB(string dataBase, string SqlQuery)
        {
            SqlConnection DbConnection = await _dbContext.OpenConnection();

            using (SqlCommand command = new SqlCommand(SqlQuery, DbConnection))
            {
                int Result = command.ExecuteNonQuery();
                string Message = $"{dataBase} created successfully.";
                QueryResult.Result(Message);
            }
        }

       public async Task DeleteTable(string table, string SqlQuery)
        {
            SqlConnection DbConnection = await _dbContext.OpenConnection();

            using (SqlCommand command = new SqlCommand(SqlQuery, DbConnection))
            {
                int Result = command.ExecuteNonQuery();
                string Message = $"{table} deleted successfully.";
                QueryResult.Result(Message);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();   
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposed = true;
            }
            else
            {
                return;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CreateDatabase()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
