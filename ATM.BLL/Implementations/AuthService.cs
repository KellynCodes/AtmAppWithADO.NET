using ATM.BLL.Interfaces;
using ATM.BLL.Utilities;
using ATM.DAL.Database;
using ATM.DAL.Models;
using ATM.DAL.Enums;
using ATM.UI;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.BLL.Implementation
{
    public class AuthService : IAuthService
    {
        static readonly IAtmService atmService = new AtmService();
        public static Account SessionUser { get; set; } = new Account(); 
        private readonly static Message message = new Message();
        private readonly static IContinueOrEndProcess continueOrEndProcess = new ContinueOrEndProcess();

        /// <summary>
        /// Login Validation.
        /// </summary>
        public void Login()
        {
        Start: Console.WriteLine("Enter your User ID");
            string userId = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(userId))
            {
                message.Error("Input was empty of not valid. Please try agian");
                goto Start;
            }
        EnterUserID: Console.WriteLine("Enter your account number");
            string AccountNo = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(AccountNo))
            {
                message.Error("Input was empty of not valid. Please try agian");
                goto EnterUserID;
            }

            if (int.TryParse(userId, out int UserID))
            {
                var UserDetails = AtmDB.Account.FirstOrDefault(user => user.UserId == UserID && user.AccountNo == AccountNo);
                SessionUser = UserDetails;
                if (UserDetails != null)    
                {
                   message.Alert($"Welcome back {UserDetails.UserName}");
                AtmServices: Console.WriteLine("What would like to Do");
                    Console.WriteLine("1.\t Check Balance\n2.\t Withdrawal\n3.\t Transfer\n4.\t Deposit");
                    string userInput = Console.ReadLine() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(userInput))
                    {
                        Console.Clear();
                        Console.WriteLine("Input was empty or not valid");
                        goto AtmServices;
                    }
                    if (int.TryParse(userInput, out int answer))
                    {
                        switch (answer)
                        {
                            case (int)SwitchCase.One: atmService.CheckBalance();
                                break;
                            case (int)SwitchCase.Two: atmService.Withdraw();
                                break;
                            case (int)SwitchCase.Three: atmService.Transfer();
                                break;
                            case (int)SwitchCase.Four: atmService.Deposit();
                                break;
                            case (int)SwitchCase.Five: atmService.PayBill();
                                break;
                            default:
                                
                                message.Error("Entered value was not in the list");
                                goto AtmServices;
                        }
                    }
                }
                else
                {
                   message.Error("Opps!. Sorry this users does not exist. Please try again with a valid user information");
                    goto Start;
                }
            }
            else
            {
                message.Error("Input was not valid. Please Try again");
                goto Start;
            }

        }

        /// <summary>
        /// When user wants to recet Pin
        /// <param name="accNo"></param>

        public async Task ResetPin()
        {
            EnterUserID: Console.WriteLine("Enter your user ID");
            if (!long.TryParse(Console.ReadLine(), out long userID))
            {
                message.Error("Invalid input please try again.");
                goto EnterUserID;
            }
            Console.WriteLine("Enter your account number.");
        EnterAccNumber: string accountNumber = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                message.Error("Input was empty.");
                goto EnterAccNumber;
            }
            var userInfo = AtmDB.Account.FirstOrDefault(user => user.AccountNo == accountNumber && user.UserId == userID);
            if(userInfo == null)
            {
                message.Error("Sorry this user does not exist. Please try again with valid user information.");
                goto EnterUserID;
            }
        Question: message.Danger($"Do you want to update your secret pin {userInfo.UserName}");
            Console.WriteLine("1. YES\t 2. NO");
            if(!int.TryParse(Console.ReadLine(), out int answer))
            {
                message.Error("Invalid input. Please try again.");
                goto Question;
            }
            const int TwoHundredMilliseconds = 200;
            switch (answer)
            {
                case (int)SwitchCase.One:
                    goto Next;
                    case (int)SwitchCase.Two:
                    message.Alert("You have canceled this operation.");
                   await Task.Delay(TwoHundredMilliseconds);
                    continueOrEndProcess.Answer();
                    break;
                default: message.Error("Entered input was not in the case. Please try again.");
                    goto Question;
            }
        Next:
            message.AlertInfo("Enter your new pin number");
            string Pin = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(Pin))
            {
                message.Error("Input was empty. Do try again.");
                goto Next;
            }
            const int MaximumPinLength = 4;
            if(Pin.Length > MaximumPinLength || Pin.Length < MaximumPinLength)
            {
                message.Error("Sorry Pin cannot be greater or less than four digits. Do try again.");
                goto Next;
            }
            userInfo.Pin = Pin;
            if(userInfo.Pin == Pin)
            {
                message.Success($"{userInfo.UserName} your pin have been updated successfully");
            }
            Login();
        }
        /// <summary>
        /// Logout Users
        /// </summary>
        public async Task LogOut()        {
            const int ThreeSeconds = 3000;
            message.Error($"Logging out....\nPLease Wait.");
            await Animae.PrintDotAnimation();
            await Task.Delay(ThreeSeconds);
        }
    }
}