﻿CREATE PROCEDURE [dbo].[SP_Account_RetrieveAccountByUsernameOrSSN]
	@username VARCHAR(100),
	@SSN CHAR(10)
AS
BEGIN
	SELECT *
	FROM dbo.Account
	WHERE username = @username OR SSN = @SSN
END