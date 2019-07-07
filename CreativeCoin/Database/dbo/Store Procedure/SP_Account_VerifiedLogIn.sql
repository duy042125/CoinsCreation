CREATE PROCEDURE [dbo].[SP_Account_VerifiedLogIn]
	@username VARCHAR(100)
AS
BEGIN
	SELECT *
	FROM dbo.Account
	WHERE username = @username
END
