using System;
using System.Threading.Tasks;

namespace ATM.BLL.Interfaces
{
    public interface IAuthService
    {
        void Login();

        Task ResetPin();

        Task LogOut();
    }
}
