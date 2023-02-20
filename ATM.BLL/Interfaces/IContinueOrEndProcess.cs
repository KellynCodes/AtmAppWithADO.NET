using System.Threading.Tasks;

namespace ATM.BLL.Interfaces
{
    public interface IContinueOrEndProcess
    {
        void EndProcess();
        Task ContinueProcess();
        Task Answer();
    }
}
