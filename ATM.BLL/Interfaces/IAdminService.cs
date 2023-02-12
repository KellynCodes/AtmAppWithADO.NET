using ATM.DAL.Models;
using System.Threading.Tasks;

namespace ATM.BLL.Interfaces
{
    public interface IAdminService
    {
       Task LoginAdmin();
       Task ReloadCash();
       void SetCashLimit();
    }
}
