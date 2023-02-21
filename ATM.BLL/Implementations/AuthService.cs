using ATM.DAL.Database.DbQueries;
using System.Threading.Tasks;
using ATM.BLL.Interfaces;
using ATM.BLL.Utilities;
using ATM.DAL.Database;
using ATM.DAL.Models;
using ATM.DAL.Enums;
using System.Linq;
using ATM.UI;
using System;

namespace ATM.BLL.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly static IContinueOrEndProcess continueOrEndProcess = new ContinueOrEndProcess();
        public static Account SessionUser { get; set; } = new Account();
        static readonly IAtmService atmService = new AtmService();
        private readonly static Message message = new Message();
        private readonly SelectQuery selectQuery = new SelectQuery(new DbContext());
        private readonly UpdateQuery updateQuery = new UpdateQuery(new DbContext());

        /// <summary>
        /// Login Validation.
        /// </summary>
        public async Task Login()
        {
        Start: Console.WriteLine("Enter your account number.");
            string AccountNo = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(AccountNo))
            {
                message.Error("Input was empty or not valid. Please try agian");
                goto Start;
            }
        EnterPin: Console.WriteLine("Enter your account pin.");
            string Pin = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(Pin))
            {
                message.Error("Input was empty of not valid. Please try agian");
                goto EnterPin;
            }

     
            var UserDetails = await selectQuery.SelectAccountAsync(AccountNo, Pin);
            if (UserDetails != null)
            {
                foreach (var user in UserDetails)
                {
                    SessionUser = user;
                    message.Alert($"Welcome back {user.UserName}");
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
                            case (int)SwitchCase.One:
                                await atmService.CheckBalance();
                                break;
                            case (int)SwitchCase.Two:
                               await atmService.Withdraw();
                                break;
                            case (int)SwitchCase.Three:
                              await atmService.Transfer();
                                break;
                            case (int)SwitchCase.Four:
                               await atmService.Deposit();
                                break;
                            case (int)SwitchCase.Five:
                               await atmService.PayBill();
                                break;
                            default:

                                message.Error("Entered value was not in the list");
                                goto AtmServices;
                        }
                    }
                }
            }
            else
            {
                message.Error("Opps!. Sorry this users does not exist. Please try again with a valid user information");
                goto Start;
            }
        }


        /// <summary>
        /// When user wants to recet Pin

        public async Task ResetPin()
        {
        EnterUserID: Console.WriteLine("Enter your user Pin");
            string Pin = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(Pin))
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
            var UserDetails = await selectQuery.SelectAccountAsync(accountNumber, Pin);
            var userInfo = UserDetails.FirstOrDefault(user => user.AccountNo == accountNumber && user.Pin == Pin);

            if (userInfo == null)
            {
                message.Error("Sorry this user does not exist. Please try again with valid user information.");
                goto EnterUserID;
            }
        Question: message.Danger($"Do you want to update your secret pin {userInfo.UserName}");
            Console.WriteLine("1. YES\t 2. NO");
            if (!int.TryParse(Console.ReadLine(), out int answer))
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
                   await continueOrEndProcess.Answer();
                    break;
                default:
                    message.Error("Entered input was not in the case. Please try again.");
                    goto Question;
            }
        Next:
            message.AlertInfo("Enter your new pin number");
            string newPin = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(newPin))
            {
                message.Error("Input was empty. Do try again.");
                goto Next;
            }
            const int MaximumPinLength = 4;
            if (newPin.Length > MaximumPinLength || newPin.Length < MaximumPinLength)
            {
                message.Error("Sorry Pin cannot be greater or less than four digits. Do try again.");
                goto Next;
            }
            await updateQuery.UpdateAccountAsync(AuthService.SessionUser.UserId, newPin);
            if (userInfo.Pin == newPin)
            {
                message.Success($"{userInfo.UserName} your pin have been updated successfully");
            }
            await Login();
        }

        /// <summary>
        /// Logout Users
        /// </summary>
        public async Task LogOut()
        {
            const int ThreeSeconds = 3000;
            message.Error($"Logging out....\nPLease Wait.");
            await Animae.PrintDotAnimation();
            await Task.Delay(ThreeSeconds);
        }
    }
}