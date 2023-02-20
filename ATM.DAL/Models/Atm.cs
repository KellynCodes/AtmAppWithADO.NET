using ATM.DAL.Enums;
using System.Collections.Generic;

namespace ATM.DAL.Models
{
    public class Atm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AvailableCash { get; set; }
        public string CurrentLanguage { get; set; }
    }

}