namespace ATM.DAL.Database.QueryStrings
{
    public class CreateQueryStrings
    {
        public static string AtmDbQueryString { get; } = @"CREATE DATABASE AtmMachine;";
        public static string AtmTableQueryString { get; } = @"USE AtmMachine; CREATE TABLE AtmInfo(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Name VARCHAR(60) NOT NULL,
	AvailableCash DECIMAL NOT NULL,
	Language VARCHAR(100) NOT NULL
);";

        public static string UserTableQueryString { get; } = @"USE AtmMachine; CREATE TABLE Users (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FullName VARCHAR(100) NOT NULL,
	Email VARCHAR(100) NOT NULL,
	Password NVARCHAR(MAX) NOT NULL,
	PhoneNumber VARCHAR(20) NOT NULL,
	UserBank VARCHAR(50) NOT NULL,
    Role VARCHAR(50) NOT NULL
	);";

        public static string AccountTableQueryString { get; } = @"USE AtmMachine; CREATE TABLE Account(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	UserId INT NOT NULL,
	UserName VARCHAR(100) NOT NULL,
	AccountNo VARCHAR(50) NOT NULL,
	AccountType VARCHAR(50) NOT NULL,
	Pin	VARCHAR(MAX) NOT NULL,
	Balance DECIMAL NOT NULL,
	CreatedDate VARCHAR(100) NOT NULL,
	FOREIGN KEY (UserId) REFERENCES Users (Id)
);";

		public static string TransactionTableQueryString { get; } = @"USE AtmMachine; CREATE TABLE Transactions(
	 Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	 Sender_UserId INT NOT NULL,
	 Receiver INT NULL,
	 TransactionAmount decimal NOT NULL,
	 TransactionType VARCHAR(20) NOT NULL,
	 TransactionDate VARCHAR(100) NOT NULL,
	 FOREIGN KEY (Sender_UserId) REFERENCES Users(Id),
	 FOREIGN KEY (Receiver) REFERENCES Users(Id),
	);";

        public static string AdminTableQueryString { get; } = @"USE AtmMachine; CREATE TABLE Admin (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	AdminId INT NOT NULL,
	FOREIGN KEY (AdminId) REFERENCES Users(Id)
	);";

        public static string CustomerTableQueryString { get; } = @"USE AtmMachine; CREATE TABLE Customer (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	CustomerId INT NOT NULL,
	FOREIGN KEY (CustomerId) REFERENCES Users(Id)
	);";

        public static string ThirdPartyQueryString { get; } = @"USE AtmMachine; CREATE TABLE ThirdParty (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	ThirdPartyId INT NOT NULL,
	FOREIGN KEY (ThirdPartyId) REFERENCES Users(Id)
	);";

        public static string BankTableQueryString { get; } = @"USE AtmMachine; CREATE TABLE Bank(
    Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	UserId INT NOT NULL,
	Name VARCHAR(50) NOT NULL,
	SwiftCode NVARCHAR(500) NOT NULL,
	FOREIGN KEY (UserId) REFERENCES Users(Id)
);";

        public static string BillTableQueryString { get; } = @"USE AtmMachine; CREATE TABLE Bill(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Name VARCHAR(100) NOT NULL,
	Amount DECIMAL NOT NULL,
);";
    }
}
