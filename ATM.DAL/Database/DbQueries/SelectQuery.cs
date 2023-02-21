using ATM.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATM.DAL.Database.DbQueries
{
    public class SelectQuery
    {
        private readonly DbContext _dbContext;

        public SelectQuery(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Atm>> SelectAtmDataInfoAsync(int id)
        {
            IList<Atm> StoreAtm = new List<Atm>();

            string queryString = @"USE AtmMachine; SELECT AtmInfo.Id, AtmInfo.Name, AtmInfo.AvailableCash, AtmInfo.Language from AtmInfo WHERE AtmInfo.Id = @Id";
            try
            {
                SqlConnection sqlConnection = await _dbContext.OpenConnection();
                using SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Id", id);
                using SqlDataReader ReadData = await sqlCommand.ExecuteReaderAsync();

                while (await ReadData.ReadAsync())
                {
                    StoreAtm.Add(new Atm()
                    {
                        Id = (int)ReadData["Id"],
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

            string queryString = @"USE AtmMachine; SELECT AtmInfo.Id, AtmInfo.Name, AtmInfo.AvailableCash, AtmInfo.Language from AtmInfo";
            try
            {
                SqlConnection sqlConnection = await _dbContext.OpenConnection();
                using SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                using SqlDataReader ReadData = await sqlCommand.ExecuteReaderAsync();

                while (await ReadData.ReadAsync())
                {
                    StoreAtm.Add(new Atm()
                    {
                        Id = (int)ReadData["Id"],
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

        public async Task<IEnumerable<Account>> SelectAccountAsync(string accountNo, string Pin)
        {

            string getUserQuery = $"USE AtmMachine; SELECT Account.Id, Account.UserId, Account.Pin, Account.UserName, Account.AccountNo, Account.AccountType, Account.Balance, Account.CreatedDate FROM Account WHERE AccountNo = @AccountNo AND Pin = @Pin";
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
            string getUserQuery = $"USE AtmMachine; SELECT Account.Id, Account.UserId, Account.Pin, Account.UserName, Account.AccountNo, Account.AccountType, Account.Balance, Account.CreatedDate FROM Account WHERE AccountNo = @AccountNo";
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
            string getUserQuery = $"USE AtmMachine; SELECT Account.Id, Account.UserId, Account.Pin, Account.UserName, Account.AccountNo, Account.AccountType, Account.Balance, Account.CreatedDate FROM Account";
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

        public async Task<IEnumerable<User>> SelectAllUserAsync()
        {
            string queryString = @"USE AtmMachine; SELECT Users.Id, Users.FullName, Users.Email, Users.Password, Users.UserBank, Users.PhoneNumber FROM Users;";
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
    }
}
