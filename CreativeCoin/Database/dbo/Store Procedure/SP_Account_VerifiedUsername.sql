CREATE PROCEDURE [dbo].[SP_Account_VerifiedUsername]
	@username VARCHAR(100),
	@SSN CHAR(10)
AS
BEGIN
	SELECT *
	FROM dbo.Account
	WHERE username = @username OR SSN = @SSN
END
