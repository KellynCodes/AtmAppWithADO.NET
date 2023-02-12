using ATM.DAL.Database.QueryObject;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace ATM.DAL.Database.DbQueries
{
    public class DbQuery
    {
        private static readonly DbContext dbContext = new DbContext();
        public static async Task Query(UserAndAccount queryObject)
        {
            SqlConnection sqlConnection = await dbContext.OpenConnection();

            using(SqlCommand sqlCommand = new SqlCommand(queryObject.ToString(), sqlConnection))
            {
                sqlCommand.ExecuteNonQuery();
                string Message = "Query executed successfully.";
                Console.WriteLine(Message);
            }
        }
    }
}
