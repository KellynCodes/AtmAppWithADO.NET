using ATM.DAL.Models;
using System.Threading.Tasks;

namespace ATM.BLL.Interfaces
{
    public interface IAtmService
    {
        Task Start();
        void CheckBalance();
        void Withdraw();
        void Transfer();
        void Deposit();
        void PayBill();
        Task CreateAccount();
        void ReloadCash(decimal amount);

    }



}