using ATM.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
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
            string AccountQuery = @"USE Atmm; 
INSERT INTO Account(UserId, UserName, AccountNo, AccountType, Balance, Pin, CreatedDate) VALUES(@UserId, @UserName, @AccountNo, @AccountType, @Balance, @Pin, @CreatedDate)";
            string UserQuery = @"USE Atmm;
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
               await sqlConnection.CloseAsync();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public async Task InsertIntoAtmInfoAsync(string name, decimal amount, string language)
        {
            string QueryString = @"USE Atm; INSERT INTO AtmInfo(Name, AvailableCash, Language) VALUES(@Name, @Amount, @Language)";

            SqlConnection sqlConnection = await _dbContext.OpenConnection();
            SqlTransaction Transaction = sqlConnection.BeginTransaction();

            try
            {
                SqlCommand SqlCommand = new SqlCommand(QueryString, sqlConnection, Transaction);
                SqlCommand.Parameters.AddWithValue("@Name", name);
                SqlCommand.Parameters.AddWithValue("@Amount", amount);
                SqlCommand.Parameters.AddWithValue("@Language", language);

                string Message = await SqlCommand.ExecuteNonQueryAsync() > 1 ? "Atm table updated successfully." : "Query was not successfull.";
                Console.WriteLine(Message);

                Transaction.Commit();
                await sqlConnection.CloseAsync();
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                throw ex;
            }

        }

        public async Task<IEnumerable<Atm>> SelectAtmDataInfoAsync(int id)
        {
            IList<Atm> StoreAtm = new List<Atm>();

            string queryString = @"USE Atm; SELECT AtmInfo.Name, AtmInfo.AvailableCash, AtmInfo.Language from AtmInfo WHERE AtmInfo.Id = @Id";
            try
            {
                SqlConnection sqlConnection = await _dbContext.OpenConnection();
                using SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Id", id);
                using SqlDataReader ReadData = await sqlCommand.ExecuteReaderAsync();

                while(await ReadData.ReadAsync())
                {
                    StoreAtm.Add(new Atm()
                    {
                        Name = ReadData["Name"].ToString(),
                        AvailableCash = (decimal)ReadData["AvailableCash"],
                        CurrentLanguage = ReadData["Language"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
                return StoreAtm;
        }
        
        public async Task<IEnumerable<Atm>> SelectAtmDataInfoAsync()
        {
            IList<Atm> StoreAtm = new List<Atm>();

            string queryString = @"USE Atm; SELECT AtmInfo.Name, AtmInfo.AvailableCash, AtmInfo.Language from AtmInfo";
            try
            {
                SqlConnection sqlConnection = await _dbContext.OpenConnection();
                using SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                using SqlDataReader ReadData = await sqlCommand.ExecuteReaderAsync();

                while(await ReadData.ReadAsync())
                {
                    StoreAtm.Add(new Atm()
                    {
                        Name = ReadData["Name"].ToString(),
                        AvailableCash = (decimal)ReadData["AvailableCash"],
                        CurrentLanguage = ReadData["Language"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
                return StoreAtm;
        }

        public async Task UpdateAtmInfoAsync(int atmId, decimal amount)
        {

            string AtmQuery = @"USE ATM; UPDATE AtmInfo SET AvailableCash = @AvailableCash WHERE Id = @Id";
            SqlConnection sqlConnection = await _dbContext.OpenConnection();

            try
            {

                SqlCommand UserSqlCommand = new SqlCommand(AtmQuery, sqlConnection);
                UserSqlCommand.Parameters.AddWithValue("@Id", atmId);
                UserSqlCommand.Parameters.AddWithValue("@AvailableCash", amount);

                string UserMessage = await UserSqlCommand.ExecuteNonQueryAsync() > 1 ? "Atm: Update Query executed successfully." : "Atm: Update Query was not successfull.";
                Console.WriteLine(UserMessage);
                await sqlConnection.CloseAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IEnumerable<Account>> SelectAccountAsync(string accountNo, string Pin)
        {

            string getUserQuery = $"USE Atm; SELECT Account.Id, Account.UserId, Account.Pin, Account.UserName, Account.AccountNo, Account.AccountType, Account.Balance, Account.CreatedDate FROM Account WHERE AccountNo = @AccountNo AND Pin = @Pin";
            IList<Account> account = new List<Account>();
            try
            {
            SqlConnection sqlConn = await _dbContext.OpenConnection();
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
                await sqlConn.CloseAsync();
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
                await sqlConn.CloseAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return account;
        } 
        
        public async Task<IEnumerable<Account>> SelectAllAccountAsync()
        {

            SqlConnection sqlConn = await _dbContext.OpenConnection();
            string getUserQuery = $"USE Atm; SELECT Account.Id, Account.UserId, Account.Pin, Account.UserName, Account.AccountNo, Account.AccountType, Account.Balance, Account.CreatedDate FROM Account";
            IList<Account> account = new List<Account>();
            try
            {
                using SqlCommand command = new SqlCommand(getUserQuery, sqlConn);
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
                await sqlConn.CloseAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return account;
        } 
        
        public async Task<IEnumerable<User>> SelectUserAsync(string email, string password, string queryString)
        {

            SqlConnection sqlConn = await _dbContext.OpenConnection();
            
            IList<User> user = new List<User>();
            try
            {
                using SqlCommand command = new SqlCommand(queryString, sqlConn);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                using SqlDataReader dataReader = await command.ExecuteReaderAsync();
                while (await dataReader.ReadAsync())
                {
                    user.Add(
                        new User()
                        {
                            FullName = dataReader["FullName"].ToString(),
                            Email = dataReader["Email"].ToString(),
                            Password = dataReader["Password"].ToString(),
                            UserBank = dataReader["UserBank"].ToString(),
                            PhoneNumber = dataReader["PhoneNumber"].ToString(),
                        }
                        ); ;
                }
                await sqlConn.CloseAsync();
            }
            catch (Exception ex)   

            {
                throw ex;
            }
            return user;
        }
        
        public async Task<IEnumerable<User>> SelectAllUserAsync()
        {
            string queryString = @"USE Atm; SELECT Users.Id, Users.FullName, Users.Email, Users.Password, Users.UserBank, Users.PhoneNumber FROM Users;";
            SqlConnection sqlConn = await _dbContext.OpenConnection();
            
            IList<User> user = new List<User>();
            try
            {
                using SqlCommand command = new SqlCommand(queryString, sqlConn);
                using SqlDataReader dataReader = await command.ExecuteReaderAsync();
                while (await dataReader.ReadAsync())
                {
                    user.Add(
                        new User()
                        {
                            Id = (int)dataReader["Id"],
                            FullName = dataReader["FullName"].ToString(),
                            Email = dataReader["Email"].ToString(),
                            Password = dataReader["Password"].ToString(),
                            UserBank = dataReader["UserBank"].ToString(),
                            PhoneNumber = dataReader["PhoneNumber"].ToString(),
                        }
                        );
                }
                await sqlConn.CloseAsync();
            }
            catch (Exception ex)   

            {
                throw ex;
            }
            return user;
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
                await sqlConnection.CloseAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task UpdateAccountAsync(int userId, string pin)
        {

            string UserQuery = @"USE ATM; UPDATE Account SET Pin = @Pin WHERE UserId = @UserId";
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
