using ATM.DAL.Database.DbQueries;
using ATM.DAL.Database;
using ATM.DAL.Models;
using System;
using System.Threading.Tasks;

namespace ATM.BLL.Utilities
{
    public class GetAtmData
    {
        public static Atm GetData { get; private set; }
        public static async Task<Atm> Data()
        {
        Atm _atm = new Atm();
            DbQuery dbQuery = new DbQuery(new DbContext());
            var AtmInfo = await dbQuery.SelectAtmDataInfoAsync(ReturnAtmId.Id());
            if (AtmInfo != null)
            {
                foreach (var Info in AtmInfo)
                {
                    _atm = Info;
                    GetData = Info;
                }
            }
            return _atm;
        }
    }
}
