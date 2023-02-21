using ATM.BLL.Implementation;
using ATM.BLL.Interfaces;
using ATM.DAL.Enums;
using System;
using System.Threading.Tasks;

namespace ATM.BLL.Utilities
{
    public class MainMethod
    {
        private static readonly IAtmService atmService = new AtmService(new AdminService());
        private static readonly IAuthService authService = new AuthService();
        private static readonly IAdminService adminService = new AdminService();
        private static readonly IMessage message = new Message();

        public static async Task GetUserChoice()
        {
        Option:
            Console.WriteLine("Choose from the Option");
            Console.WriteLine("1.\t Login As Admin. \n2.\t Login as User\n3.\t Recet Pin\n4.\t Create new Account\n5`\t Quit App");
            string Answer = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(Answer, out int Choice))
            {
                switch (Choice)
                {
                    case (int)SwitchCase.One:
                      await  adminService.LoginAdmin();
                        break;
                    case (int)SwitchCase.Two:
                        Console.WriteLine("Please provide your details");
                       await authService.Login();
                        break;
                    case (int)SwitchCase.Three:
                       await authService.ResetPin();
                        break;
                    case (int)SwitchCase.Four:
                       await atmService.CreateAccount();
                        break;
                    case (int)SwitchCase.Five:
                      await Logout();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Input is not in the option");
                       await GetUserChoice();
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Please Enter a valid input");
                goto Option;
            }
        }
        public static async Task Logout()
        {
        Mbido: Console.WriteLine("Are you sure you want to Logout [NO/YES]");
            string Answer = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(Answer))
            {
                message.Error("Input was empty or not valid. Please try again");
                goto Mbido;
            }
            if (Answer.Trim().ToUpper() == "YES")
            {
               await authService.LogOut();
            }
            else if (Answer.Trim().ToUpper() == "NO")
            {
                Console.Clear();
                await GetUserChoice();
            }
            else
            {
                message.Error("Please enter [NO/YES] for us to be certain you want to logout");
                goto Mbido;
            }
        }
    }
}
