using ATM.DAL.Models;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System;
using ATM.DAL.Enums;

namespace ATM.DAL.Database.DbQueries
{
    public class InsertQuery
    {
        private readonly DbContext _dbContext;

        public InsertQuery(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateUserAndAccountAsync(Account account, User user)
        {

            string AccountQuery = @"USE AtmMachine; 
INSERT INTO Account(UserId, UserName, AccountNo, AccountType, Balance, Pin, CreatedDate) VALUES(@UserId, @UserName, @AccountNo, @AccountType, @Balance, @Pin, @CreatedDate)";
            string UserQuery = @"USE AtmMachine;
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

                string UserMessage = await UserSqlCommand.ExecuteNonQueryAsync() > 0 ? "User Query executed successfully." : "User Query was not successfull.";
                Console.WriteLine(UserMessage);
                string AccountMessage = await AccountSqlCommand.ExecuteNonQueryAsync() > 0 ? "Account Query executed successfully." : "Account Query was not successfull.";
                Console.WriteLine(AccountMessage);


                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public async Task InsertIntoAtmInfoAsync(string name, decimal amount, string language)
        {
            string QueryString = @"USE AtmMachine; INSERT INTO AtmInfo(Name, AvailableCash, Language) VALUES(@Name, @Amount, @Language)";

            SqlConnection sqlConnection = await _dbContext.OpenConnection();
            SqlTransaction Transaction = sqlConnection.BeginTransaction();

            try
            {
                SqlCommand SqlCommand = new SqlCommand(QueryString, sqlConnection, Transaction);
                SqlCommand.Parameters.AddWithValue("@Name", name);
                SqlCommand.Parameters.AddWithValue("@Amount", amount);
                SqlCommand.Parameters.AddWithValue("@Language", language);

                string Message = await SqlCommand.ExecuteNonQueryAsync() > 0 ? "AtmInfo table Created successfully." : "Query was not successfull.";
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

        public async Task InsertIntoTransactionsTable(int senderSessionUser, int reciever, decimal transactionAmount, string transactionType, string transactionDate)
        {
            string QueryString = @"USE AtmMachine; INSERT INTO Transactions(Sender_UserId, Receiver, TransactionAmount, TransactionType, TransactionDate) VALUES(@Sender, @Reciever, @TransactionAmount, @TransactionType, @TransactionDate);";

            SqlConnection sqlConnection = await _dbContext.OpenConnection();
            SqlTransaction Transaction = sqlConnection.BeginTransaction();

            try
            {
                SqlCommand SqlCommand = new SqlCommand(QueryString, sqlConnection, Transaction);
                SqlCommand.Parameters.AddWithValue("@Sender", senderSessionUser);
                SqlCommand.Parameters.AddWithValue("@Reciever", reciever);
                SqlCommand.Parameters.AddWithValue("@TransactionAmount", transactionAmount);
                SqlCommand.Parameters.AddWithValue("@TransactionType", transactionType);
                SqlCommand.Parameters.AddWithValue("@TransactionDate", transactionDate);

                string Message = await SqlCommand.ExecuteNonQueryAsync() > 0 ? "Transaction table Inserted successfully." : "Transaction Query was not successfull.";
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

    }
}
