using ATM.DAL.Enums;
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
                string AccountQuery = $@"USE Atm; 
INSERT INTO Account(UserId, UserName, AccountNo, AccountType, Balance, Pin, CreatedDate) VALUES('1', @UserName, @AccountNo, @AccountType, @Balance, @Pin, @CreatedDate');";
                string UserQuery = $@"USE Atm;
	INSERT INTO Users(FullName, Email, Password, PhoneNumber, UserBank, Role) VALUES(@FullName, @Email, @Password, @PhoneNumber, @UserBank, @Role);
";
                    SqlConnection sqlConnection = _dbContext.OpenConnection();
                    SqlTransaction transaction = sqlConnection.BeginTransaction();

                try
                {
                    SqlCommand AccountSqlCommand = new SqlCommand(AccountQuery, sqlConnection, transaction);
                    SqlCommand UserSqlCommand = new SqlCommand(UserQuery, sqlConnection, transaction);
                    AccountSqlCommand.Parameters.AddWithValue("@UserName", account.UserName);
                    AccountSqlCommand.Parameters.AddWithValue("@AccountNo", account.AccountNo);
                    AccountSqlCommand.Parameters.AddWithValue("@AccountType", account.AccountType);
                    AccountSqlCommand.Parameters.AddWithValue("@Balance", account.Balance);
                    AccountSqlCommand.Parameters.AddWithValue("@Pin", account.Pin);
                    AccountSqlCommand.Parameters.AddWithValue("@CreatedDate", account.CreatedDate);

                    UserSqlCommand.Parameters.AddWithValue("@FullName", user.FullName);
                    UserSqlCommand.Parameters.AddWithValue("@Email", user.Email);
                    UserSqlCommand.Parameters.AddWithValue("@Password", user.Password);
                    UserSqlCommand.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                    UserSqlCommand.Parameters.AddWithValue("@UserBank", user.UserBank);
                    UserSqlCommand.Parameters.AddWithValue("@Role", user.Role);

                    string AccountMessage = await AccountSqlCommand.ExecuteNonQueryAsync() > 1 ? "Account Query executed successfully." : "Account Query was not successfull.";
                    Console.WriteLine(AccountMessage);
                    string UserMessage = await AccountSqlCommand.ExecuteNonQueryAsync() > 1 ? "User Query executed successfully." : "User Query was not successfull.";
                    Console.WriteLine(UserMessage);

                    transaction.Commit();
                }
                catch (Exception ex)
                {  
                   transaction.Rollback();
                    Console.WriteLine(ex.Message);
                }
            }

            public async Task<IEnumerable<Account>> SelectAccountAsync(string accountNo, string Pin, string accountType)
            {

                SqlConnection sqlConn = _dbContext.OpenConnection();
                string getUserQuery = $"USE Atm; SELECT Account.Id, Account.UserId, Account.Pin, Account.UserName, Account.AccountNo, Account.Balance, Account.CreatedDate FROM Account WHERE AccountNo = @AccountNo AND Pin = @Pin AND AccountType = @AccountType";
                IList<Account> account = new List<Account>();
                try
                {
                    using (SqlCommand command = new SqlCommand(getUserQuery, sqlConn))
                    {
                        command.Parameters.AddWithValue("@AccountNo", accountNo);
                        command.Parameters.AddWithValue("@Pin", Pin);
                        command.Parameters.AddWithValue("@AccountType", accountType);
                        using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                        {
                            while (await dataReader.ReadAsync())
                            {
                                account.Add(
                                    new Account()
                                    {
                                        Id = (long)dataReader["Id"],
                                        UserId = (long)dataReader["UserID"],
                                        UserName = dataReader["UserName"].ToString(),
                                        AccountNo = dataReader["AccountNo"].ToString(),
                                        Balance = (decimal)dataReader["Balance"],
                                        Pin = dataReader["Pin"].ToString(),
                                        AccountType = (AccountType)dataReader["AccountType"],
                                        CreatedDate = dataReader["CreatedDate"].ToString(),
                                    }
                                    );
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                return account;
            }

            public async Task<IEnumerable<Account>> SelectAccountAsync(string accountNo, string accountType)
            {

                SqlConnection sqlConn = _dbContext.OpenConnection();
                string getUserQuery = $"USE Atm; SELECT Account.Id, Account.UserId, Account.Pin, Account.UserName, Account.AccountNo, Account.Balance, Account.CreatedDate FROM Account WHERE AccountNo = @AccountNo AND Pin = @Pin AND AccountType = @AccountType";
                IList<Account> account = new List<Account>();
                try
                {
                    using (SqlCommand command = new SqlCommand(getUserQuery, sqlConn))
                    {
                        command.Parameters.AddWithValue("@AccountNo", accountNo);
                        command.Parameters.AddWithValue("@AccountType", accountType);
                        using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                        {
                            while (await dataReader.ReadAsync())
                            {
                                account.Add(
                                    new Account()
                                    {
                                        Id = (long)dataReader["Id"],
                                        UserId = (long)dataReader["UserID"],
                                        UserName = dataReader["UserName"].ToString(),
                                        AccountNo = dataReader["AccountNo"].ToString(),
                                        Balance = (decimal)dataReader["Balance"],
                                        Pin = dataReader["Pin"].ToString(),
                                        AccountType = (AccountType)dataReader["AccountType"],
                                        CreatedDate = dataReader["CreatedDate"].ToString(),
                                    }
                                    );
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                return account;
            }
        }
    }
