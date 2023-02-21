using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.DAL.Database.DbQueries
{
    public class DeleteQuery
        {
            private readonly DbContext _dbContext;

            public DeleteQuery(DbContext dbContext)
            {
                _dbContext = dbContext;
            }
        }
}
