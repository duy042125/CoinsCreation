CREATE PROCEDURE [dbo].[SP_Account_RetrieveAccountBySSN]
	@AccountID CHAR(10)
AS
BEGIN
	SELECT * 
	FROM Account
	WHERE AccountID = @AccountID
END
