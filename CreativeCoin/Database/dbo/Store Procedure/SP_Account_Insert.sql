CREATE PROCEDURE [dbo].[SP_Account_Insert]
	@username VARCHAR(100),
	@password VARCHAR(100),
	@full_name VARCHAR(100),
	@birthdate DATE,
	@phone_number CHAR(10),
	@SSN CHAR(10)
AS
BEGIN
	INSERT INTO Account(username, [password], full_name, birthdate, phone_number, SSN)
	VALUES (@username, @password, @full_name, @birthdate, @phone_number, @SSN)
END
