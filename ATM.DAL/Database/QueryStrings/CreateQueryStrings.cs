namespace ATM.DAL.Database.QueryStrings
{
    public class CreateQueryStrings
    {
        public static string AtmDbQueryString { get; } = @"CREATE DATABASE Atm;";
        public static string AtmTableQueryString { get; } = @"USE Atm; CREATE TABLE AtmInfo(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Name VARCHAR(60) NOT NULL,
	AvailableCash DECIMAL NOT NULL,
	Language VARCHAR(100) NOT NULL
);";

        public static string UserTableQueryString { get; } = @"USE Atmm; CREATE TABLE Users (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FullName VARCHAR(100) NOT NULL,
	Email VARCHAR(100) NOT NULL,
	Password NVARCHAR(MAX) NOT NULL,
	PhoneNumber VARCHAR(20) NOT NULL,
	UserBank VARCHAR(50) NOT NULL,
    Role VARCHAR(50) NOT NULL
	);";

        public static string AccountTableQueryString { get; } = @"USE Atmm; CREATE TABLE Account(
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

        public static string AdminTableQueryString { get; } = @"USE Atmm; CREATE TABLE Admin (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	AdminId INT NOT NULL,
	FOREIGN KEY (AdminId) REFERENCES Users(Id)
	);";

        public static string CustomerTableQueryString { get; } = @"USE Atmm; CREATE TABLE Customer (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	CustomerId INT NOT NULL,
	FOREIGN KEY (CustomerId) REFERENCES Users(Id)
	);";

        public static string ThirdPartyQueryString { get; } = @"USE Atmm; CREATE TABLE ThirdParty (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	ThirdPartyId INT NOT NULL,
	FOREIGN KEY (ThirdPartyId) REFERENCES Users(Id)
	);";

        public static string BankTableQueryString { get; } = @"USE Atmm; CREATE TABLE Bank(
    Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	UserId INT NOT NULL,
	Name VARCHAR(50) NOT NULL,
	SwiftCode NVARCHAR(500) NOT NULL,
	FOREIGN KEY (UserId) REFERENCES Users(Id)
);";

        public static string BillTableQueryString { get; } = @"USE Atmm; CREATE TABLE Bill(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Name VARCHAR(100) NOT NULL,
	Amount DECIMAL NOT NULL,
);";
    }
}
