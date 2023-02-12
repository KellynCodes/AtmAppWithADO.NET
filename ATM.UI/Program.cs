using ATM.BLL.Implementation;
using ATM.BLL.Interface;
using ATM.BLL.Interfaces;
using ATM.BLL.Utilities;

namespace ATM.UI
{
    internal class Program
    {
        private static readonly IAtmService atmService = new AtmService();
        static readonly ICreateDatabase createDatabase = new CreateDatabase(new DAL.Database.DbContext());
      static async Task Main()
        {
            atmService.Start();
            // MainMethod.GetUserChoice();
            // await SqlQueryMethods.Run();
           await createDatabase.DeleteDB("Atm", "DROP DATABASE Atm");
        }   
    }
}