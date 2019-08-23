CREATE PROCEDURE [dbo].[SP_Account_RetrieveAccountTypeByUsername]
	@username VARCHAR(100)
AS
BEGIN
	SELECT [type]
	FROM Account
	WHERE username = @username
END
