CREATE PROCEDURE [dbo].[SP_Account_VerifiedUsername]
	@theUsername VARCHAR(100)

AS
BEGIN
	SELECT *
	FROM dbo.Account
	WHERE username = @theUsername
END
