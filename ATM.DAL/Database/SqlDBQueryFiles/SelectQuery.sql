USE Atm

	SELECT Account.UserName, Account.Balance FROM Account WHERE Account.AccountNo = 14754454 AND Account.AccountType = 'Current';
	SELECT Users.Id, Users.FullName, Users.Email, Users.Password, Users.PhoneNumber, Users.UserBank, Users.Role FROM Users WHERE Users.Id = 1;