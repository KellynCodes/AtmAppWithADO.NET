using ATM.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ATM.DAL.Database.DbQueries
{
    public class DbQuery
    {
        private static readonly DbContext dbContext = new DbContext();
        public static async Task UserInsertAsync(User queryObject)
        {
            string query = $@"USE Atm;
	INSERT INTO Users(FullName, Email, Password, PhoneNumber, UserBank, Role) VALUES(@FullName, @Email, @Password, @PhoneNumber, @UserBank, @Role);
";
            SqlConnection sqlConnection = dbContext.OpenConnection();

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlCommand.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter
                    {
                    ParameterName = "@FullName",
                    Value = queryObject.FullName,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                    },

                    new SqlParameter
                    {
                    ParameterName = "@Email",
                    Value = queryObject.Email,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 100
                    },

                    new SqlParameter
                    {
                        ParameterName = "@Password",
                        Value = queryObject.Password,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 500
                    },

                    new SqlParameter
                    {
                    ParameterName = "@UserBank",
                    Value = "Gt bank",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                    },

                    new SqlParameter
                    {
                    ParameterName = "@PhoneNumber",
                    Value =  queryObject.PhoneNumber,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                    },

                    new SqlParameter
                    {
                    ParameterName = "@Role",
                    Value = queryObject.Role,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                    },
                });

                sqlCommand.ExecuteNonQuery();
                string Message = "Query executed successfully.";
                Console.WriteLine(Message);
            }
        }

        public static async Task AccountInsertAsync(Account queryObject)
        {
            string query = $@"USE Atm; 
INSERT INTO Account(UserId, UserName, AccountNo, AccountType, Balance, Pin, CreatedDate) VALUES('1', @UserName, @AccountNo, @AccountType, @Balance, @Pin, @CreatedDate');";
            SqlConnection sqlConnection = dbContext.OpenConnection();

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlCommand.Parameters.AddRange(new SqlParameter[]
                {
                        new SqlParameter
                    {
                    ParameterName = "@UserName",
                    Value = queryObject.UserName,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 100
                    },

                    new SqlParameter
                    {
                    ParameterName = "@AccountNo",
                    Value = queryObject.AccountNo,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                    },

                    new SqlParameter
                    {
                        ParameterName = "@AccountType",
                        Value = queryObject.AccountType,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 50
                    },

                    new SqlParameter
                    {
                        ParameterName = "@Balance",
                        Value = queryObject.Balance,
                        SqlDbType = SqlDbType.Decimal,
                        Size = 43950335,
                    },

                    new SqlParameter
                    {
                    ParameterName = "@Pin",
                    Value = queryObject.Pin,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size  = int.MaxValue
                    },

                    new SqlParameter
                    {
                    ParameterName = "@CreatedDate",
                    Value =  queryObject.CreatedDate,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 100
                    },
                });

                string Message = await sqlCommand.ExecuteNonQueryAsync() > 1 ? "Query executed successfully." : "Query was not successfull.";
                Console.WriteLine(Message);
            }
        }

        public static Account SelectAccountAsync(string accountNo, string Pin, string accountTpe)
        {

            return new Account();
        }
    }
}
