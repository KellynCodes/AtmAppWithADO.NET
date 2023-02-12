using System;

namespace ATM.BLL.Utilities
{
    public class DefaultSwitchCaseMethod
    {
        public static string SwitchCase(int inputCase)
        {
            Console.WriteLine("1. Gt Bank\n2.\t Access Bank\n3.\t Union Bank\n4.\t Fidelity Bank\n5.\t Ecobank\n6.\t First Bank.");

            if (inputCase == 1) return "Gt Bank";
            else
            if (inputCase == 2) return "Access Bank";
            else
            if (inputCase == 3) return "Union Bank";
            else
            if (inputCase == 4) return "Fidelity Bank";
            else
            if (inputCase == 5) return "Eco Bank";
            else
            if (inputCase == 6) return "First Bank";
            return "Empty input";
        }
    }
}
