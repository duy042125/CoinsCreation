CREATE PROCEDURE [dbo].[SP_Account_Insert]
	@username VARCHAR(100),
	@password VARCHAR(100),
	@full_name VARCHAR(100),
	@birthdate DATE,
	@phone_number CHAR(10),
	@AccountID CHAR(10)
AS
BEGIN
	INSERT INTO Account(username, [password], full_name, birthdate, phone_number, AccountID)
	VALUES (@username, @password, @full_name, @birthdate, @phone_number, @AccountID)
END
