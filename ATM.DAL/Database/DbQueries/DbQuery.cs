using ATM.DAL.Database.QueryObject;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ATM.DAL.Database.DbQueries
{
    public class DbQuery
    {
        private static readonly DbContext dbContext = new DbContext();
        public static async Task UserInsert(IList<UserAndAccount> queryObjects)
        {
            string query = $@"USE Atm;
	insert into Users(FullName, Email, Password, PhoneNumber, UserBank, Role) values('@FullName', '@Email', '@Password', '@PhoneNumber', '@UserBank', '@Role');
";
            SqlConnection sqlConnection = await dbContext.OpenConnection();

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                foreach(var queryObject in queryObjects) { 
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
                    }

                sqlCommand.ExecuteNonQuery();
                string Message = "Query executed successfully.";
                Console.WriteLine(Message);
            }
        }

        public static async Task AccountInsert(IList<UserAndAccount> queryObjects)
        {
            string query = $@"USE Atm;
	INSERT INTO Account(UserName, AccountNo, AccountType, Balance, Pin, CreateDate) values('@UserName', '@AccountNo', '@AccountType', @Balance, '@Pin', '@CreatedDate');
";
            SqlConnection sqlConnection = await dbContext.OpenConnection();

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                foreach (var queryObject in queryObjects)
                {
                    sqlCommand.Parameters.AddRange(new SqlParameter[]
                    {
                    new SqlParameter
                    {
                    ParameterName = "@UserName",
                    Value = queryObject.UserName,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                    },

                    new SqlParameter
                    {
                    ParameterName = "@AccountNo",
                    Value = queryObject.AccountNo,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 100
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
                    },

                    new SqlParameter
                    {
                    ParameterName = "@Pin",
                    Value = queryObject.Pin,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                    },

                    new SqlParameter
                    {
                    ParameterName = "@CreatedDate",
                    Value =  queryObject.CreatedDate,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                    },
                    });
                }

                sqlCommand.ExecuteNonQuery();
                string Message = "Query executed successfully.";
                Console.WriteLine(Message);
            }
        }
    }
}
