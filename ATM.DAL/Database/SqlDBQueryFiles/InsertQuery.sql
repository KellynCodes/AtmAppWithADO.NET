	USE Atm;
	insert into Users(FullName, Email, Password, PhoneNumber, UserBank, Role) values('Jenny Gold', 'JennyGold@gmail.com', 'jennyjenny', '90394800943', 'UBA', 'ThirdParty');
	USE Atm;
	insert into Account(UserId, UserName, AccountNo, AccountType, Pin, Balance, CreatedDate) Values('1', 'KellynCodes', '434334534', 'Current', '1234', '0.0', 'February 12 , 2023');
	use Atm;
	INSERT INTO Account(UserId, UserName, AccountNo, AccountType, Balance, Pin, CreateDDate) VALUES('2', 'Jenny', '12345678', '2', '20000.0', '454545', 'March 12 , 2022');

	select * from Users;
	Use Atm;
	select * from Account;

	USE Atm; SELECT Account.Id, Account.UserId, Account.Pin, Account.UserName, Account.AccountNo, Account.Balance, Account.CreatedDate FROM Account WHERE AccountNo = 14754454 AND Pin = 1234 AND AccountType = 'Current'