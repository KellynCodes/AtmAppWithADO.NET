using System;
using System.Threading.Tasks;

namespace ATM.BLL.Utilities
{
    public class Animae
    {
        public static async Task PrintDotAnimation(int Timer = 10, int seconds = 200)
        {
            for (int i = 0; i < Timer; i++)
            {
                Console.Write(".");
                await Task.Delay(seconds);
            }
            Console.Clear();
        }
    }
}
