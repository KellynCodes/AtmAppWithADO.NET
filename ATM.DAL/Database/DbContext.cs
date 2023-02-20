using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ATM.DAL.Database
{
    public class DbContext : IDisposable
    {
        private readonly string _connString;

        private bool _disposed;

        private SqlConnection _dbConnection = null;
        /// <summary>
        /// Do enter you Server here
        /// </summary>
        private static readonly string _connectionString = "Data Source=DESKTOP-N2LHC09";
        public DbContext() : this(@$"{_connectionString};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {

        }

        public DbContext(string connString)
        {
            _connString = connString;
        }

        public async Task<SqlConnection> OpenConnection()
        {
            _dbConnection = new SqlConnection(_connString);
            await _dbConnection.OpenAsync();
            return _dbConnection;
        }

        public void CloseConnection()
        {
            if (_dbConnection?.State != ConnectionState.Closed)
            {
                 _dbConnection.Close();
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dbConnection.Dispose();
            }

            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}