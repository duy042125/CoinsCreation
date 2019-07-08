CREATE PROCEDURE [dbo].[SP_Account_ChangePassword]
	@username VARCHAR(100),
	@password VARCHAR(100)
AS
BEGIN
	UPDATE Account
	SET [password] = @password
	WHERE username = @username
END
