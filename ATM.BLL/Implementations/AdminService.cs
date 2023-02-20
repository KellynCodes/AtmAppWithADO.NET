using static ATM.DAL.Database.QueryStrings.SelectQueryStrings;
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
    public class AdminService : IAdminService
    {
        private readonly IMessage message = new Message();
        private readonly IContinueOrEndProcess continueOrEndProcess = new ContinueOrEndProcess();
        private readonly DbQuery dbQuery = new DbQuery( new DbContext());

        private User SessionAdmin { get; set; }
        public decimal CashLimit { get; set; }

        public async Task LoginAdmin()
        {
        Start: Console.WriteLine("Enter your Email");
            string UserEmail = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(UserEmail))
            {
                message.Error("Input was empty of not valid. Please try agian");
                goto Start;
            }  
        Password: Console.WriteLine("Enter your Password");
            string UserPassword = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(UserPassword))
            {
                message.Error("Input was empty of not valid. Please try agian");
                goto Password;
            }

            var GetUser = await dbQuery.SelectUserAsync(UserEmail, UserPassword, GetAdminQuery);
            var UserDetails = GetUser.FirstOrDefault(user => user.Email == UserEmail && user.Password == UserPassword);
            SessionAdmin = UserDetails;
            if (UserDetails != null)
            {
                message.Alert($"Welcome back {UserDetails.FullName}");
            AtmServices: Console.WriteLine("What would like to Do");    
                Console.WriteLine("1.\t Create Database\n2.\t Reload Cash\n3.\t Set Cash Limit\n4.\t View list of Users");
                string userInput = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    message.Error("Input was empty or not valid");
                    goto AtmServices;
                }
                if (int.TryParse(userInput, out int answer))
                {
                    switch (answer)
                    {
                        case (int)SwitchCase.One:
                            await CreatDb.Run(dbName: "Atmm");
                            break;
                            case (int)SwitchCase.Two:
                          await ReloadCash();
                            break;
                        case (int)SwitchCase.Three:
                           await SetCashLimit();
                            break;
                        case (int)SwitchCase.Four:
                        await ViewListOfUsers();
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


        public async Task SetCashLimit()
        {
            EnterCashLimit: message.AlertInfo($"Hi {SessionAdmin.FullName} How much do you want to set as cash limit?.");
            if (decimal.TryParse(Console.ReadLine(), out decimal cashLimit))
            {
                CashLimit = cashLimit;
                message.Success($"{SessionAdmin.FullName} Cash limit set successfully.");
            }
            else
            {
                message.Error("Wrong input. Please enter only numbers.");
                goto EnterCashLimit;
            }
            await continueOrEndProcess.Answer();
        }

        public async Task ReloadCash()
        {
        EnterAmount: Console.WriteLine("Enter amount to reload");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
              var AtmInfo = await dbQuery.SelectAtmDataInfoAsync(ReturnAtmId.Id());
              if(AtmInfo != null)
                {
                    foreach(var atm in AtmInfo)
                    {
                    message.Success($"Reloading {amount}...");
                    atm.AvailableCash += amount;
                    message.Alert($"New Balance :: {atm.AvailableCash}");
                    }
                    await MainMethod.GetUserChoice();
                }
            }
            else
            {
                message.Error("Input was not valid. Please Try again.");
                goto EnterAmount;
            }
        }

        public async Task ViewListOfUsers()
        {
            var Users = await dbQuery.SelectAllUserAsync();
            message.Alert("\n These are the List of your users.");
            foreach (var user in Users)
            {
                Console.WriteLine($"{user.Id} {user.FullName}");
            }
           await MainMethod.Logout();
        }

    }
}