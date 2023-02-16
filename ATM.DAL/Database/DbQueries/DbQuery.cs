using ATM.DAL.Enums;
using ATM.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATM.DAL.Database.DbQueries
{
    public class DbQuery
    {
        private readonly DbContext _dbContext;

        public DbQuery(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateUserAndAccountAsync(Account account, User user)
        {
            string AccountQuery = @"USE Atm; 
INSERT INTO Account(UserId, UserName, AccountNo, AccountType, Balance, Pin, CreatedDate) VALUES(@UserId, @UserName, @AccountNo, @AccountType, @Balance, @Pin, @CreatedDate)";
            string UserQuery = @"USE Atm;
	INSERT INTO Users(FullName, Email, Password, PhoneNumber, UserBank, Role) VALUES(@FullName, @Email, @Password, @PhoneNumber, @UserBank, @Role)";
            SqlConnection sqlConnection = await _dbContext.OpenConnection();
            SqlTransaction transaction = sqlConnection.BeginTransaction();

            try
            {
                SqlCommand AccountSqlCommand = new SqlCommand(AccountQuery, sqlConnection, transaction);
                AccountSqlCommand.Parameters.AddWithValue("@UserId", account.UserId);
                AccountSqlCommand.Parameters.AddWithValue("@UserName", account.UserName);
                AccountSqlCommand.Parameters.AddWithValue("@AccountNo", account.AccountNo);
                AccountSqlCommand.Parameters.AddWithValue("@AccountType", account.AccountType);
                AccountSqlCommand.Parameters.AddWithValue("@Balance", account.Balance);
                AccountSqlCommand.Parameters.AddWithValue("@Pin", account.Pin);
                AccountSqlCommand.Parameters.AddWithValue("@CreatedDate", account.CreatedDate);

                SqlCommand UserSqlCommand = new SqlCommand(UserQuery, sqlConnection, transaction);
                UserSqlCommand.Parameters.AddWithValue("@FullName", user.FullName);
                UserSqlCommand.Parameters.AddWithValue("@Email", user.Email);
                UserSqlCommand.Parameters.AddWithValue("@Password", user.Password);
                UserSqlCommand.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                UserSqlCommand.Parameters.AddWithValue("@UserBank", user.UserBank);
                UserSqlCommand.Parameters.AddWithValue("@Role", user.Role);

                string UserMessage = await UserSqlCommand.ExecuteNonQueryAsync() > 1 ? "User Query executed successfully." : "User Query was not successfull.";
                Console.WriteLine(UserMessage);
                string AccountMessage = await AccountSqlCommand.ExecuteNonQueryAsync() > 1 ? "Account Query executed successfully." : "Account Query was not successfull.";
                Console.WriteLine(AccountMessage);


                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }


        public async Task<IEnumerable<Account>> SelectAccountAsync(string accountNo, string Pin)
        {

            SqlConnection sqlConn = await _dbContext.OpenConnection();
            string getUserQuery = $"USE Atm; SELECT Account.Id, Account.UserId, Account.Pin, Account.UserName, Account.AccountNo, Account.AccountType, Account.Balance, Account.CreatedDate FROM Account WHERE AccountNo = @AccountNo AND Pin = @Pin";
            IList<Account> account = new List<Account>();
            try
            {
                using SqlCommand command = new SqlCommand(getUserQuery, sqlConn);
                command.Parameters.AddWithValue("@AccountNo", accountNo);
                command.Parameters.AddWithValue("@Pin", Pin);
                using SqlDataReader dataReader = await command.ExecuteReaderAsync();
                while (await dataReader.ReadAsync())
                {
                    account.Add(
                        new Account()
                        {
                            Id = (int)dataReader["Id"],
                            UserId = (int)dataReader["UserID"],
                            UserName = dataReader["UserName"].ToString(),
                            AccountNo = dataReader["AccountNo"].ToString(),
                            Balance = (decimal)dataReader["Balance"],
                            Pin = dataReader["Pin"].ToString(),
                            CreatedDate = dataReader["CreatedDate"].ToString(),
                        }
                        ); ;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return account;
        }
        public async Task<IEnumerable<Account>> SelectAccountAsync(string accountNo)
        {

            SqlConnection sqlConn = await _dbContext.OpenConnection();
            string getUserQuery = $"USE Atm; SELECT Account.Id, Account.UserId, Account.Pin, Account.UserName, Account.AccountNo, Account.AccountType, Account.Balance, Account.CreatedDate FROM Account WHERE AccountNo = @AccountNo";
            IList<Account> account = new List<Account>();
            try
            {
                using SqlCommand command = new SqlCommand(getUserQuery, sqlConn);
                command.Parameters.AddWithValue("@AccountNo", accountNo);
                using SqlDataReader dataReader = await command.ExecuteReaderAsync();
                while (await dataReader.ReadAsync())
                {
                    account.Add(
                        new Account()
                        {
                            Id = (int)dataReader["Id"],
                            UserId = (int)dataReader["UserId"],
                            UserName = dataReader["UserName"].ToString(),
                            AccountNo = dataReader["AccountNo"].ToString(),
                            Balance = (decimal)dataReader["Balance"],
                            Pin = dataReader["Pin"].ToString(),
                            CreatedDate = dataReader["CreatedDate"].ToString(),
                        }
                        ); ;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return account;
        }

        public async Task UpdateAccountAsync(int userId, decimal balance)
        {

            string UserQuery = @"USE ATM; UPDATE Account SET Balance = @Balance WHERE UserId = @UserId";
            SqlConnection sqlConnection = await _dbContext.OpenConnection();

            try
            {

                SqlCommand UserSqlCommand = new SqlCommand(UserQuery, sqlConnection);
                UserSqlCommand.Parameters.AddWithValue("@UserId", userId);
                UserSqlCommand.Parameters.AddWithValue("@Balance", balance);

                string UserMessage = await UserSqlCommand.ExecuteNonQueryAsync() > 1 ? "Update Query executed successfully." : "Update Query was not successfull.";
                Console.WriteLine(UserMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
