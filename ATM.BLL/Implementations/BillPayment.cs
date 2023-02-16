using System.Threading.Tasks;
using ATM.BLL.Interfaces;
using System;

namespace ATM.BLL.Implementation
{
    public class BillPayment : IBillPayment
    {
        public async Task Airtime()
        {
            Console.WriteLine("How much would like to buy.");
        }

        public async Task CableTransmission()
        {
            Console.WriteLine("How much would like to Pay.");
        }

        public async Task Nepa()
        {
            Console.WriteLine("How much would like to subscribe.");
        }
    }
}
