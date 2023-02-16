using ATM.DAL.Database.Interface;
using ATM.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ATM.DAL.Database.Implementation
{
    public class CrudOperation 
    {
        private readonly DbContext _dbContext;
        private bool _disposed;

        public CrudOperation() { }
        public CrudOperation(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateUser(User user)
        {

            SqlConnection sqlConn = await _dbContext.OpenConnection();


            string insertQuery =
                $"INSERT INTO USERS (UserID, FirstName, LastName, UserName, PhoneNumber, Country, ProfileImage)" +
                $" VALUES (@UserID, @FirstName, @LastName, @UserName, @Phone, @Image); SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";


            using (SqlCommand command = new SqlCommand(insertQuery, sqlConn))
            {
                command.Parameters.AddRange(new SqlParameter[]
                {
                new SqlParameter
                {
                    ParameterName = "@UserID",
                    Value = user.Id,
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },

                new SqlParameter
                {
                    ParameterName = "@FullName",
                    Value = user.FullName,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },

                new SqlParameter
                {
                    ParameterName = "@Email",
                    Value = user.Email,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },

                new SqlParameter
                {
                    ParameterName = "@PhoneNumber",
                    Value = user.PhoneNumber,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },

                });

                long userId = (long)await command.ExecuteScalarAsync();
                Console.WriteLine(userId > 0 ? $"User Created Successfully" : $"User not created");
            }

        }

        public async Task<long> UpdateUser(int userId, User user)
        {

            SqlConnection sqlConn = await _dbContext.OpenConnection();



            string insertQuery =
                $"UPDATE Users SET FirstName = @FirstName, LastName = @LastName, UserName = UserName, Country = @Country, PhoneNumber = @Phone WHERE UserID = @UserID";

            using (SqlCommand command = new SqlCommand(insertQuery, sqlConn))
            {

                command.Parameters.AddRange(new SqlParameter[]
                     {
                new SqlParameter
                {
                    ParameterName = "@UserID",
                    Value = user.Id,
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },

                new SqlParameter
                {
                    ParameterName = "@FullName",
                    Value = user.FullName,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },

                new SqlParameter
                {
                    ParameterName = "@Email",
                    Value = user.Email,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },

                new SqlParameter
                {
                    ParameterName = "@PhoneNumber",
                    Value = user.PhoneNumber,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },

                  });

                var result = command.ExecuteNonQuery();
                return result;
            }
        }

        public async Task DeleteUser(int UserId)
        {
            SqlConnection sqlConn = await _dbContext.OpenConnection();

            string deleteQuery = $"DELETE FROM Users WHERE UserId = @UserId ";
            using (SqlCommand command = new SqlCommand(deleteQuery, sqlConn))
            {

                command.Parameters.AddRange(new SqlParameter[]
                {
                new SqlParameter
                {
                    ParameterName = "@UserId",
                    Value = UserId,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                }
                });

                var result = command.ExecuteNonQuery();
                Console.WriteLine(result > 0 ? $"Successfully Deleted" : $"Not Successfully Deleted");
            }
        }

        public async Task<User> GetUser(int id)
        {
            SqlConnection sqlConn = await _dbContext.OpenConnection();
            string getUserQuery = $"SELECT Users.FirstName, Users.LastName, Users.UserName, Users.PhoneNumber, Users.ProfilePicure, Users.Country FROM Users WHERE UserId = @UserId ";
            using (SqlCommand command = new SqlCommand(getUserQuery, sqlConn))
            {
                command.Parameters.AddRange(new SqlParameter[]
                {
                new SqlParameter
                {
                    ParameterName = "@UserId",
                    Value = id,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                }
                });
                User user = new User();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        user.FullName = dataReader["FullName"].ToString();
                        user.Email = dataReader["Email"].ToString();
                        user.PhoneNumber = dataReader["PhoneNumber"].ToString();
                    }
                }

                return user;
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {

            SqlConnection sqlConn = await _dbContext.OpenConnection();
            string getAllUsersQuery = $"SELECT Users.FirstName, Users.LastName, Users.UserName, Users.PhoneNumber, Users.ProfilePicure, Users.Country FROM Users WHERE UserId = @UserId ";
            using (SqlCommand command = new SqlCommand(getAllUsersQuery, sqlConn))
            {
                List<User> users = new List<User>();
                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    while (dataReader.Read())
                    {
                        users.Add(
                            new User()
                            {
                                FullName = dataReader["FullName"].ToString(),
                                Email = dataReader["Email"].ToString(),
                                PhoneNumber = dataReader["PhoneNumber"].ToString(),
                            }
                            );
                    }

                }
                return users;
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
                _dbContext.Dispose();
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