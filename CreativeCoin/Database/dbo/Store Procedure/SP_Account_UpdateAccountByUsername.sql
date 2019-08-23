CREATE PROCEDURE [dbo].[SP_Account_UpdateAccountByUsername]
	@username VARCHAR(100),
	@full_name VARCHAR(100),
	@birthdate DATE,
	@phone_number VARCHAR(15),
	@type CHAR(1)
AS
BEGIN
	UPDATE Account
	SET full_name = @full_name,
		birthdate = @birthdate,
		phone_number = @phone_number,
		[type] = @type
	WHERE username = @username
END