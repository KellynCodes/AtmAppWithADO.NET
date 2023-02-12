using ATM.BLL.Implementation;
using ATM.BLL.Interfaces;
using ATM.BLL.Utilities;

namespace ATM.UI
{
    internal class Program
    {
        private static readonly IAtmService atmService = new AtmService();
      static void Main()
        {
            atmService.Start();
            MainMethod.GetUserChoice();
        }   
    }
}