using ATM.BLL.Interfaces;
using ATM.BLL.Utilities;
using ATM.DAL.Database;
using ATM.DAL.Database.DbQueries;
using ATM.DAL.Enums;
using ATM.DAL.Models;
using ATM.UI;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.BLL.Implementation
{
    public class AtmService : IAtmService
    {
        private readonly IAdminService _adminService;
        private static AccountType _accountType;
        private static int _cashDenomination;
        private static decimal ChoiceAmount;

        private readonly IAuthService authService = new AuthService();
        private readonly IContinueOrEndProcess continueOrEndProcess = new ContinueOrEndProcess();
        private readonly ICreateAccount createAccount = new CreateAccount();
        public static readonly IMessage message = new Message();
        private readonly DbQuery dbQuery = new DbQuery(new DbContext());

        public static int _days = 0;
        private const int Aday = 1;
        private const int OneWeek = 7;

        private static decimal EnteredAmount { get; set; }
        private static decimal Amount { get; set; }

        public AtmService(IAdminService adminService) => _adminService = adminService;
        public AtmService() { }
        public async Task Start()
        {
            GetAtmData.GetData().AvailableCash = 6_000.90m;
            Console.WriteLine($"{GetAtmData.GetData().Name} has booted!");
            Console.WriteLine("Insert Card!");
            string DbQueryResult = await SqlQueryMethods.Run();
            Console.WriteLine(DbQueryResult);
        }

        public async Task CheckBalance()
        {
        CheckBalance: Console.WriteLine("1.\t Current\n2.\t Savings");
            string userInput = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrEmpty(userInput))
            {
                if (int.TryParse(userInput, out int answer))
                {
                    switch (answer)
                    {
                        case (int)SwitchCase.One:
                            _accountType = AccountType.Current;
                            break;
                        case (int)SwitchCase.Two:
                            _accountType = AccountType.Savings;
                            break;
                        default:
                            message.Error("Entered value was not in the list. Please try again");
                            goto CheckBalance;
                    }
                   EnterPin: Console.WriteLine("Enter you account pin.");
                    string Pin = Console.ReadLine() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(Pin))
                    {
                        message.Error("Input cannot be Empty. Do try again.");
                        goto EnterPin;
                    }
                  var CheckAccountType = await dbQuery.SelectAccountAsync(AuthService.SessionUser.AccountNo, Pin, _accountType.ToString());
                   // var CheckAccountType = AtmDB.Account.Where(user => user.AccountNo == AuthService.SessionUser.AccountNo && user.AccountType == _accountType);
                    if (CheckAccountType.Any())
                    {

                        foreach (var user in CheckAccountType)
                        {
                            message.Success($"{user.UserName} your account balance is {user.Balance}");
                        }

                        continueOrEndProcess.Answer();
                    }
                    else
                    {
                        message.Error("Account not found. Check your account information to be certain you entered the correct account type and pin\nOR contact customer care on 09157060998");
                       await authService.Login();
                    }
                }
                else
                {

                    message.Error("Invalid Input. Please try again");
                    goto CheckBalance;
                }
            }
            else
            {
                message.Error("Input was empty or not valid");
                goto CheckBalance;
            }
        }

        public async Task Withdraw()
        {
            if (_days > OneWeek)
            {
                message.Error("You have exceeded your withdrawal limit. [20k: daily and 100k: weekly]");
               await authService.Login();
            }
            message.Error("Note: Withdrawal Limit:\nDaily = 20k\nWeekly = 100k");
        CheckBalance: Console.WriteLine("1.\t Current\n2.\t Savings");
            string userInput = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(userInput))
            {
                message.Error("Input was empty or not valid");
                goto CheckBalance;
            }
            if (int.TryParse(userInput, out int answer))
            {
                switch (answer)
                {
                    case (int)SwitchCase.One:
                        _accountType = AccountType.Current;
                        break;
                    case (int)SwitchCase.Two:
                        _accountType = AccountType.Savings;
                        break;
                    default:

                        message.Error("Entered value was not in the list. Please try again");
                        goto CheckBalance;
                }
                var UserAccount = await dbQuery.SelectAccountAsync(AuthService.SessionUser.AccountNo, _accountType.ToString());
            AmountToWidthDraw: if (UserAccount.Any())
                {
                    Console.WriteLine("How much do you want to withdraw");
                    Console.WriteLine("1. 500\t2. 1000\t3. 2000\n4. 5000\t5. 10000\t6. 20000\n7. Others");
                    if (int.TryParse(Console.ReadLine(), out int choice))
                        switch (choice)
                        {
                            case (int)SwitchCase.One:
                                EnteredAmount = 500;

                                goto EnterDenomination;
                            case (int)SwitchCase.Two:
                                EnteredAmount = 1000;

                                goto EnterDenomination;
                            case (int)SwitchCase.Three:
                                EnteredAmount = 2000;

                                goto EnterDenomination;
                            case (int)SwitchCase.Four:
                                EnteredAmount = 5000;

                                goto EnterDenomination;
                            case (int)SwitchCase.Five:
                                EnteredAmount = 10000;
                                goto EnterDenomination;
                            case (int)SwitchCase.Six:
                                EnteredAmount = 20000;
                                goto EnterDenomination;
                            case (int)SwitchCase.Seven:
                                goto Others;
                            default:
                                message.Error("Input was not in the list. Please try again.");
                                goto AmountToWidthDraw;
                        }
                    Others: Console.WriteLine("How much do you want to withdraw");
                    //create a funciton that will be called here in an if statment

                    if (!decimal.TryParse(Console.ReadLine(), out ChoiceAmount))
                    {
                        message.Error("Input was not valid. Please enter only digits");
                        goto AmountToWidthDraw;
                    }
                EnterDenomination: Console.WriteLine("Enter denomination to despense");

                    if (ChoiceAmount <= 0)
                    {
                        Amount = EnteredAmount;
                    }
                    else if (ChoiceAmount > 0)
                    {
                        Amount = ChoiceAmount;
                    }
                    var atm = GetAtmData.GetData();
                    if (Amount > atm.AvailableCash)
                    {
                        message.Alert($"Sorry atm is out of cash. Available amount is {atm.AvailableCash}");
                        /* Program.GetUserChoice();*/
                    }
                    if (Amount > (int)WithdrawalLimit.Weekly)
                    {
                        message.Error("You cannot withdraw more than 100k in a week");
                        goto AmountToWidthDraw;
                    }
                    if (Amount == (int)WithdrawalLimit.Daily)
                    {
                        _days += Aday;
                    }
                    if (Amount == (int)WithdrawalLimit.Weekly)
                    {
                        _days = OneWeek;
                    }
                    Console.WriteLine("1.\t 1000\t2.\t 500\n3.\t 200");
                    if (int.TryParse(Console.ReadLine(), out int CashDenomination))
                    {
                        switch (CashDenomination)
                        {
                            case (int)SwitchCase.One:
                                _cashDenomination = (int)Denominations.OneThousand;
                                goto nextBlock;
                            case (int)SwitchCase.Two:
                                _cashDenomination = (int)Denominations.FiveHundred;
                                goto nextBlock;

                            case (int)SwitchCase.Three:
                                _cashDenomination = (int)Denominations.TwoHundred;
                                goto nextBlock;
                            default:
                                message.Error("Input is not available in the options. Please try again.");
                                goto EnterDenomination;
                        }
                    }
                    else
                    {
                        message.Error("Invalid input. Please try again.");
                        goto EnterDenomination;
                    }
                nextBlock:
                    foreach (var user in UserAccount)
                    {
                        if (Amount >= user.Balance)
                        {
                            message.Error("Insufficient balance");
                            goto AmountToWidthDraw;
                        }
                        else
                        {
                            user.Balance -= Amount;
                            GetAtmData.GetData().AvailableCash -= Amount;
                            message.Success($"Transaction successfull!. {Amount} have been debited from your account.  Your new account balance is {user.Balance}");
                            message.AlertInfo($"Cash denominations: {_cashDenomination}");
                        }
                    }

                    continueOrEndProcess.Answer();

                }
                else
                {
                    message.Error("Account not found. Check your account information to be certain you entered the correct account type\nOR contact customer care on 09157060998");
                   await authService.Login();
                }
            }
            else
            {
                message.Error("Invalid Input. Please try again");
                goto CheckBalance;
            }
        }

        /// <summary>
        /// Method that handles transfer of money
        /// </summary>

        public void Transfer()
        {
        EnterAmount: Console.WriteLine("Enter amount");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (AuthService.SessionUser.Balance <= amount)
                {
                    message.Error("Insufficient balance");
                    goto EnterAmount;
                }
                bool isUserAccountTypeSavings = AuthService.SessionUser.AccountType == AccountType.Savings;
                bool isUserAccountTypeCurrent = AuthService.SessionUser.AccountType == AccountType.Current;
                const decimal maximumAmountInCurrentAccount = 500_000;
                const decimal maximumAmountInSavinsAccount = 100_000;
                if (isUserAccountTypeCurrent && amount > maximumAmountInCurrentAccount)
                {
                    message.Error("Amount should not be greater than 500000 in a current account");
                    goto EnterAmount;
                }
                if (isUserAccountTypeSavings && amount > maximumAmountInSavinsAccount)
                {
                    message.Error("Amount should not be greater than 100000 in a current account");
                    goto EnterAmount;
                }
            }
            else
            {
                message.Error("Invalid input. Please try again.");
                goto EnterAmount;
            }

        EnterAccountNumber: Console.WriteLine("Enter account number");
            string accountNumber = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                message.Error("Input was empty or not valid");
                goto EnterAccountNumber;
            }
            var Recepient = AtmDB.Account.FirstOrDefault(user => user.AccountNo == accountNumber);
            if (Recepient == null)
            {
                message.Error("This account number does not exist. Enter a valid information");
                goto EnterBank;
            }

        EnterBank: Console.WriteLine("Choose Bank");
            Console.WriteLine("1. Gt Bank\n2.\t Access Bank\n3.\t Union Bank\n4.\t Fidelity Bank\n5.\t Ecobank\n6.\t First Bank.");
            string UserBank = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(UserBank))
            {
                message.Error("Input was empty or not valid");
                goto EnterBank;
            }
            if (int.TryParse(UserBank, out int bank))
            {
                string userBank = DefaultSwitchCaseMethod.SwitchCase(bank);

                var accountInformation = AtmDB.Account.FirstOrDefault(user => user.Id == AuthService.SessionUser.Id);
                if (accountInformation == null)
                {
                    message.Error("Error occured. Please make sure the informations you provided are valid.");
                    goto EnterAmount;
                }
                long userID = accountInformation.UserId;

                if (AuthService.SessionUser.Id == userID)
                {
                question: Console.WriteLine($"Do you want to transfer {amount} to {Recepient?.UserName}");
                    string answer = Console.ReadLine() ?? string.Empty;
                    if (answer.Trim().ToUpper() == "YES")
                    {

                        var newBalance = AuthService.SessionUser.Balance -= amount;
                        Recepient.Balance += amount;
                        message.Success($"Transaction successfull!.");
                    DoYouWantReceipt: Console.WriteLine("Do you need receipt[YES/NO]");
                        string userInput = Console.ReadLine() ?? string.Empty;

                        if (string.IsNullOrWhiteSpace(userInput))
                        {
                            message.Error("Input was empty. Please try again");
                            goto DoYouWantReceipt;
                        }
                        if (userInput.Trim().ToUpper() == "YES")
                        {
                            message.Success($"Transaction successfull!. {AuthService.SessionUser.UserName} {amount} has been debited from your account. You just transfered {amount} to {Recepient.UserName} on {DateTime.Now.ToLongDateString()}\n Your new balance is {newBalance}");
                            continueOrEndProcess.Answer();
                        }
                        else if (userInput.Trim().ToUpper() == "NO")
                        {
                            continueOrEndProcess.Answer();
                        }
                        else
                        {
                            message.Error("Please enter [NO/YES]");
                            goto DoYouWantReceipt;
                        }
                    }
                    else if (answer.Trim().ToUpper() == "NO")
                    {
                        message.Error("Transaction Canceled");
                        authService.LogOut();
                    }
                    else
                    {
                        message.Error("Please enter [NO/YES] for us to be sure you don't want to continue with the transaction.");
                        goto question;
                    }
                }
                else
                {
                    message.Error("Error occured. Please make sure the informations you provided are valid.");
                    goto EnterAmount;
                }
            }
            else
            {
                message.Error("Input was not valid. Please try again");
                goto EnterBank;
            }

        }

        /// <summary>
        /// Method that handles money deposit.
        /// </summary>

        public void Deposit()
        {
        EnterAccountumber: message.Alert("Enter your account number.");
            string accNo = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(accNo))
            {
                message.Error("Empty input. Please try again.");
                goto EnterAccountumber;
            }

        //Get account type
        ChooseAccountType: message.Alert("Choose your account type.");
            Console.WriteLine("1.\t Current\n2.\t Savings");

            if (!int.TryParse(Console.ReadLine(), out int answer))
            {
                message.Error("Invalid input. Please enter only numbers.");
                goto ChooseAccountType;
            }
            switch (answer)
            {
                case (int)SwitchCase.One:
                    _accountType = AccountType.Current;
                    break;
                case (int)SwitchCase.Two:
                    _accountType = AccountType.Savings;
                    break;
                default:
                    message.Error("Entered value was not in the list. Please try again");
                    goto ChooseAccountType;
            }

        //Get amount to deposit;
        EnterAmount: message.Alert("Enter amount you want to deposit");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                message.Error("Invalid input. Please enter only numbers.");
                goto EnterAmount;
            }
            if (amount > (decimal)WithdrawalLimit.Daily)
            {
                message.Error("Amount should not be greater than 20k.");
                goto EnterAmount;
            }

            decimal newBalance = AuthService.SessionUser.Balance += amount;
            message.Success($"{AuthService.SessionUser.UserName} your just deposited {amount} to your account {accNo}. Your new balance is {newBalance}");
            continueOrEndProcess.Answer();
        }

        public void PayBill()
        {
            IBillPayment billPayment = new BillPayment();
        EnterBillToPay: message.Alert("Which bill do you want to pay.");
            Console.WriteLine("1.\t Airtime\n2.\t Cable Transmission\n3.\t Nepa");
            if (int.TryParse(Console.ReadLine(), out int answer))
            {
                switch (answer)
                {
                    case (int)SwitchCase.One:
                        billPayment.Airtime();
                        break;
                    case (int)SwitchCase.Two:
                        billPayment.CableTransmission();
                        break;
                    case (int)SwitchCase.Three:
                        billPayment.Nepa();
                        break;
                    default:
                        message.Error("Entered input was not in the option.");
                        goto EnterBillToPay;
                }
            }
        }

        public async Task CreateAccount()
        {
            string accountNumber = createAccount.AccountNumber();
            AccountType accountType = createAccount.GetAccountType();
            string email = createAccount.GetEmail();
            string fullName = createAccount.GetFullName();
            string userName = createAccount.UserName();
            string phoneNumber = createAccount.PhoneNumber();
        UserPassword: string userPassword = createAccount.GetPassword();
            string ReEnteredPassword = createAccount.ConfirmPassword();
            if (userPassword == ReEnteredPassword)
            {
                goto EnterPin;
            }
            else
            {
                message.Error("Password do not match please try again.");
                goto UserPassword;
            }
        EnterPin: string pin = createAccount.GetPin();
            decimal Balance = 0.00m;
            string createdDate = DateTime.Now.ToLongDateString();

            var UserData = new User
            {
                FullName = fullName,
                Email = email,
                Password = userPassword,
                PhoneNumber = phoneNumber,
            };
            var AccountData = new Account
            {
                UserId = 2,
                UserName = userName,
                AccountNo = accountNumber,
                AccountType = accountType,
                Balance = Balance,
                Pin = pin,
                CreatedDate = createdDate
            };


            await dbQuery.CreateUserAndAccountAsync(AccountData, UserData);
            message.Success($"{userName} your account have been created successfully!.");
            message.AlertInfo($"Your account number is {accountNumber}");
            message.AlertInfo($"Make sure you copy your account [{accountNumber}].");
        }

        public void ReloadCash(decimal amount)
        {
            _adminService.ReloadCash();
        }
    }
}
