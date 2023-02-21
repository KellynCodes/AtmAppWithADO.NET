using ATM.DAL.Database.DbQueries;
using ATM.BLL.Implementation;
using ATM.BLL.Interfaces;
using ATM.DAL.Database;
using System;
using System.Threading.Tasks;

namespace ATM.BLL.Utilities
{
    public class StartAtm
    {
        public static readonly IMessage message = new Message();

        public static async Task Start()
        {
         SelectQuery selectQuery = new SelectQuery(new DbContext());
            var AtmInfo = await selectQuery.SelectAtmDataInfoAsync();
         if (AtmInfo != null)
            {
                foreach (var Info in AtmInfo)
                {
                    Console.WriteLine($"{Info.Id}. {Info.Name}");
                }
            }

            var AtmData = await GetAtmData.Data();
            if (AtmData != null && AtmData.Id > 0)
            {
                Console.WriteLine($"{AtmData.Name} has booted!");
                Console.WriteLine("Insert Card!");
                return;
            }
                message.Error("Atm does not exist.");
                await Start();
            return;
        }
    }
}
