using System.Text;
using System;

namespace ATM.BLL.Utilities
{
    public class SecretInput
    {
        private static readonly MessageTwo message = new MessageTwo();
        public static string Hashed(string userInput = null)
        {
            const int exactInputLength = 10;
            if (userInput?.Length <= exactInputLength)
            {
                message.Error($"{userInput} is small. Input must be ten in numbers. Please do try again");
                Hashed();
                return string.Empty;
            }
            else if (userInput?.Length > exactInputLength)
            {
                message.Error($"{userInput} is more than ten. Input must be ten in numbers. Please do try again");
                Hashed();
                return string.Empty;
            }
            else
            {
                string DisplayString(string originalString, int lastDigit)
                {
                    string strResult = new string('#', originalString.Length - lastDigit) +
                            originalString.Substring(originalString.Length - lastDigit);
                    return strResult;
                }
                const int LastFiveDigit = 5;
                return DisplayString(userInput, LastFiveDigit);
            }
        }

        public static string Hash()
        {
            bool IsPrompt = true;
            string asterics = "";
            StringBuilder input = new StringBuilder();

            while (true)
            {
                if (IsPrompt)
                    IsPrompt = false;
                ConsoleKeyInfo inputKey = Console.ReadKey(true);

                if (inputKey.Key == ConsoleKey.Enter)
                {
                    if (input.Length >= 4)
                    {
                        break;
                    }
                    else
                    {
                        message.Error("\nPlease input must be greater Four.");
                        input.Clear();
                        IsPrompt = true;
                        continue;
                    }
                }
                if (inputKey.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove(input.Length - 1, 1);
                }
                else if (inputKey.Key != ConsoleKey.Backspace)
                {
                    input.Append(inputKey.KeyChar);
                    Console.Write(asterics + "*");
                }
            }

            Console.WriteLine();
            return input.ToString();
        }
    }
}

