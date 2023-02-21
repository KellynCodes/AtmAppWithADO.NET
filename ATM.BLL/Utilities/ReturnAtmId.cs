using ATM.BLL.Implementation;
using ATM.BLL.Interfaces;
using System;

namespace ATM.BLL.Utilities
{
    public class ReturnAtmId
    {
    private readonly static IMessage message = new Message();
        public static int Id()
        {
            ChooseAtm: message.AlertInfo("Choose atm to operate.");
            if (!int.TryParse(Console.ReadLine(), out int ID))
            {
                message.Error("Input is not valid. Enter only numbers.");
                goto ChooseAtm;
            }
            return ID;
        }
    }
}
