using System.Threading.Tasks;

namespace ATM.BLL.Interfaces
{
    public interface IBillPayment
    {
        Task Airtime();
        Task Nepa();
        Task CableTransmission();
    }
}
