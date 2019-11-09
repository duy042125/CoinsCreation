CREATE PROCEDURE [dbo].[SP_Account_RetrieveAccountByUsernameOrSSN]
	@username VARCHAR(100),
	@AccountID CHAR(10)
AS
BEGIN
	SELECT *
	FROM dbo.Account
	WHERE username = @username OR AccountID = @AccountID
END
