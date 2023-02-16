using System.Threading.Tasks;

namespace ATM.BLL.Interfaces
{
    public interface IContinueOrEndProcess
    {
        Task EndProcess();
        Task ContinueProcess();
        Task Answer();
    }
}
