using ATM.DAL.Enums;
using System.Collections.Generic;

namespace ATM.DAL.Models
{
    public class Atm
    {
        public string Name { get; set; }
        public decimal AvailableCash { get; set; }
        public Language CurrentLanguage { get; set; }

        public static IList<Atm> Data { get; set; } = new List<Atm>()
        {
            new Atm{Name = "KellynCodes Atm Machine", AvailableCash = 0.00m, CurrentLanguage = Language.English}
        };
    }

}