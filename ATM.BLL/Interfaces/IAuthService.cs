using System;
using System.Threading.Tasks;

namespace ATM.BLL.Interfaces
{
    public interface IAuthService
    {
        Task Login();

        Task ResetPin();

        Task LogOut();
    }
}
