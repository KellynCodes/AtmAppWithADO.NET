using System.Threading.Tasks;

namespace ATM.BLL.Interfaces
{
    public interface IAtmService
    {
        Task Start();
        Task CheckBalance();
        Task Withdraw();
        void Transfer();
        void Deposit();
        void PayBill();
        Task CreateAccount();
        void ReloadCash(decimal amount);

    }



}