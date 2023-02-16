using System.Threading.Tasks;

namespace ATM.BLL.Interfaces
{
    public interface IAtmService
    {
        Task Start();
        Task CheckBalance();
        Task Withdraw();
        Task Transfer();
        Task Deposit();
        Task PayBill();
        Task CreateAccount();
        Task ReloadCash();

    }



}