using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System;

namespace ATM.DAL.Database.DbQueries
{
    public class UpdateQuery
    {
        private readonly DbContext _dbContext;

        public UpdateQuery(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UpdateAtmInfoAsync(int atmId, decimal amount)
        {

            string AtmQuery = @"USE AtmMachine; UPDATE AtmInfo SET AvailableCash = @AvailableCash WHERE Id = @Id";
            SqlConnection sqlConnection = await _dbContext.OpenConnection();

            try
            {

                SqlCommand sqlCommand = new SqlCommand(AtmQuery, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Id", atmId);
                sqlCommand.Parameters.AddWithValue("@AvailableCash", amount);

                string UserMessage = await sqlCommand.ExecuteNonQueryAsync() > 0 ? "AtmInfo: Update Query executed successfully." : "AtmInfo: Update Query was not successfull.";
                Console.WriteLine(UserMessage);
                await sqlConnection.CloseAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task UpdateAccountAsync(int userId, decimal balance)
        {

            string UserQuery = @"USE AtmMachine; UPDATE Account SET Balance = @Balance WHERE UserId = @UserId";
            SqlConnection sqlConnection = await _dbContext.OpenConnection();

            try
            {

                SqlCommand UserSqlCommand = new SqlCommand(UserQuery, sqlConnection);
                UserSqlCommand.Parameters.AddWithValue("@UserId", userId);
                UserSqlCommand.Parameters.AddWithValue("@Balance", balance);

                string UserMessage = await UserSqlCommand.ExecuteNonQueryAsync() > 0 ? "Update Query executed successfully." : "Update Query was not successfull.";
                Console.WriteLine(UserMessage);
                await sqlConnection.CloseAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAccountAsync(int userId, string pin)
        {

            string UserQuery = @"USE AtmMachine; UPDATE Account SET Pin = @Pin WHERE UserId = @UserId";
            SqlConnection sqlConnection = await _dbContext.OpenConnection();

            try
            {

                SqlCommand UserSqlCommand = new SqlCommand(UserQuery, sqlConnection);
                UserSqlCommand.Parameters.AddWithValue("@UserId", userId);
                UserSqlCommand.Parameters.AddWithValue("@Pin", pin);

                string UserMessage = await UserSqlCommand.ExecuteNonQueryAsync() > 1 ? "Update Query executed successfully." : "Update Query was not successfull.";
                Console.WriteLine(UserMessage);
                await sqlConnection.CloseAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
