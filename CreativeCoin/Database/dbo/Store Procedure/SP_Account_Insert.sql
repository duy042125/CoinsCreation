CREATE PROCEDURE [dbo].[SP_Account_Insert]
	@theUsername VARCHAR(100),
	@thePassword VARCHAR(100),
	@theName VARCHAR(100)
AS
BEGIN
	INSERT INTO Account(username, [password], [name])
	VALUES (@theUsername, @thePassword, @theName)
END
