USE [master]
GO

CREATE DATABASE [CreativeCoinDatabase]
GO

USE [CreativeCoinDatabase]
GO

CREATE PROCEDURE [dbo].[SP_Account_ChangePassword]
	@username VARCHAR(100),
	@password VARCHAR(100)
AS
BEGIN
	UPDATE Account
	SET [password] = @password
	WHERE username = @username
END
GO

CREATE PROCEDURE [dbo].[SP_Account_Insert]
	@username VARCHAR(100),
	@password VARCHAR(100),
	@full_name VARCHAR(100),
	@birthdate DATE,
	@phone_number CHAR(10),
	@SSN CHAR(10)
AS
BEGIN
	INSERT INTO Account(username, [password], full_name, birthdate, phone_number, SSN)
	VALUES (@username, @password, @full_name, @birthdate, @phone_number, @SSN)
END
GO

CREATE PROCEDURE [dbo].[SP_Account_RetrieveAccountBySSN]
	@SSN CHAR(10)
AS
BEGIN
	SELECT * 
	FROM Account
	WHERE SSN = @SSN
END
GO

CREATE PROCEDURE [dbo].[SP_Account_RetrieveAccountByUsername]
	@username VARCHAR(100)
AS
BEGIN
	SELECT *
	FROM dbo.Account
	WHERE username = @username
END
GO

CREATE PROCEDURE [dbo].[SP_Account_RetrieveAccountByUsernameOrSSN]
	@username VARCHAR(100),
	@SSN CHAR(10)
AS
BEGIN
	SELECT *
	FROM dbo.Account
	WHERE username = @username OR SSN = @SSN
END
GO

CREATE PROCEDURE [dbo].[SP_Account_RetrieveAccountTypeByUsername]
	@username VARCHAR(100)
AS
BEGIN
	SELECT [type]
	FROM Account
	WHERE username = @username
END
GO

CREATE PROCEDURE [dbo].[SP_Account_RetrieveAllAccount]
AS
BEGIN
	SELECT *
	FROM Account
END
GO

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
GO

CREATE PROCEDURE [dbo].[SP_Behavior_Assign_InsertBehavior]
	@Parent_username VARCHAR(100),
	@Behavior_name VARCHAR(100),
	@behavior1 VARCHAR(50),
	@behavior2 VARCHAR(50),
	@behavior3 VARCHAR(50),
	@behavior4 VARCHAR(50),
	@star5_reward1 VARCHAR(50),
	@star5_reward2 VARCHAR(50),
	@star5_reward3 VARCHAR(50),
	@star10_reward1 VARCHAR(50),
	@star10_reward2 VARCHAR(50),
	@star10_reward3 VARCHAR(50),
	@star15_reward1 VARCHAR(50),
	@star15_reward2 VARCHAR(50),
	@star20_reward VARCHAR(50)
AS
BEGIN
	INSERT INTO Behavior([name], behavior1, behavior2, behavior3, behavior4, star5_reward1, star5_reward2, star5_reward3, star10_reward1, star10_reward2, star10_reward3, star15_reward1, star15_reward2, star20_reward)
	VALUES (@Behavior_name, @behavior1, @behavior2, @behavior3, @behavior4, @star5_reward1, @star5_reward2, @star5_reward3, @star10_reward1, @star10_reward2, @star10_reward3, @star15_reward1, @star15_reward2, @star20_reward)
	
	INSERT INTO Assign(Parent_username, Behavior_name)
	VALUES (@Parent_username, @Behavior_name)
END
GO

CREATE PROCEDURE [dbo].[SP_Behavior_Assign_RetrieveBehaviorListByUsername]
	@Parent_usernname VARCHAR(100)
AS
BEGIN
	SELECT Behavior.*
	FROM Assign INNER JOIN Behavior
	ON Behavior_name = [name]
	WHERE Parent_username = @Parent_usernname
END
GO

CREATE PROCEDURE [dbo].[SP_Behavior_RetrieveAllBehaviors]
AS
BEGIN
	SELECT *
	FROM Behavior
END
GO

CREATE PROCEDURE [dbo].[SP_Behavior_RetrieveBehaviorByName]
	@Behavior_name VARCHAR(100)
AS
BEGIN
	SELECT *
	FROM Behavior
	WHERE [name] = @Behavior_name
END
GO

CREATE PROCEDURE [dbo].[SP_Behavior_UpdateBehaviorByName]
	@Behavior_name VARCHAR(100),
	@behavior1 VARCHAR(50),
	@behavior2 VARCHAR(50),
	@behavior3 VARCHAR(50),
	@behavior4 VARCHAR(50),
	@star5_reward1 VARCHAR(50),
	@star5_reward2 VARCHAR(50),
	@star5_reward3 VARCHAR(50),
	@star10_reward1 VARCHAR(50),
	@star10_reward2 VARCHAR(50),
	@star10_reward3 VARCHAR(50),
	@star15_reward1 VARCHAR(50),
	@star15_reward2 VARCHAR(50),
	@star20_reward VARCHAR(50)
AS
BEGIN
	UPDATE Behavior
	SET behavior1 = @behavior1,
		behavior2 = @behavior2,
		behavior3 = @behavior3,
		behavior4 = @behavior4,
		star5_reward1 = @star5_reward1,
		star5_reward2 = @star5_reward2,
		star5_reward3 = @star5_reward3,
		star10_reward1 = @star10_reward1,
		star10_reward2 = @star10_reward2,
		star10_reward3 = @star10_reward3,
		star15_reward1 = @star15_reward1,
		star15_reward2 = @star15_reward2,
		star20_reward = @star20_reward
	WHERE [name] = @Behavior_name
	
END
GO

CREATE PROCEDURE [dbo].[SP_Child_AddCoin]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100),
	@newCoin INT
AS
BEGIN
	UPDATE Child
	SET [total_coin] += @newCoin
	WHERE Parent_username = @Parent_username AND Child_name = @Child_name
END
GO

CREATE PROCEDURE [dbo].[SP_Child_Insert]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100),
	@birthdate DATE
AS
BEGIN
	INSERT INTO Child(Parent_username, Child_name, birthdate)
	VALUES (@Parent_username, @Child_name, @birthdate)
END
GO

CREATE PROCEDURE [dbo].[SP_Child_RetrieveAllChildren]
AS
BEGIN
	SELECT *
	FROM Child
END
GO

CREATE PROCEDURE [dbo].[SP_Child_RetrieveChildByName]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100)
AS
BEGIN
	SELECT *
	FROM Child
	WHERE Parent_username = @Parent_username AND Child_name = @Child_name
END
GO

CREATE PROCEDURE [dbo].[SP_Child_RetrieveChildListByUsername]
	@Parent_usernname VARCHAR(100)
AS
BEGIN
	SELECT * 
	FROM Child
	WHERE Parent_username = @Parent_usernname
END
GO

CREATE PROCEDURE [dbo].[SP_Child_RetrieveChildStartDateByUsernameAndChildName]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100)
AS
BEGIN
	SELECT [start_date]
	FROM Child
	WHERE Parent_username = @Parent_username AND Child_name = @Child_name
END
GO

CREATE PROCEDURE [dbo].[SP_Child_UpdateChildByKeys]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100),
	@birthdate DATE
AS
BEGIN
	UPDATE Child
	SET birthdate = @birthdate
	WHERE Parent_username = @Parent_username AND Child_name = @Child_name
END
GO

CREATE PROCEDURE [dbo].[SP_Child_UseCoin]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100),
	@usedCoin INT
AS
BEGIN
	UPDATE Child
	SET [total_coin] -= @usedCoin
	WHERE Parent_username = @Parent_username AND Child_name = @Child_name
END
GO

CREATE PROCEDURE [dbo].[SP_Report_Insert]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100),
	@Behavior_name VARCHAR(100),
	@date DATE,
	@coin_earned INT,
	@note VARCHAR(500)
AS
BEGIN
	INSERT INTO Report(Parent_username, Child_name, Behavior_name, [date], coin_earned, note)
	VALUES (@Parent_username, @Child_name, @Behavior_name, @date, @coin_earned, @note)
END
GO

CREATE PROCEDURE [dbo].[SP_Report_RetrieveAllReports]
AS
BEGIN
	SELECT *
	FROM Report
END
GO

CREATE PROCEDURE [dbo].[SP_Report_RetrieveFullReportInformationByKeys]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100),
	@date DATE
AS
BEGIN
	SELECT Report.Parent_username AS Parent_username, Report.Child_name AS Child_name, Report.Behavior_name AS Behavior_name, Report.[date] AS [date], Report.coin_earned AS coin_earned, Report.note AS note, Child.birthdate AS birthdate, Behavior.behavior1 AS behavior1, Behavior.behavior2 AS behavior2, Behavior.behavior3 AS behavior3, Behavior.behavior4
	FROM Report 
	INNER JOIN Child
	ON Report.Parent_username = Child.Parent_username AND Report.Child_name = Child.Child_name
	INNER JOIN Behavior
	ON Report.Behavior_name = Behavior.[name]
	WHERE Report.Parent_username = @Parent_username AND Report.Child_name = @Child_name AND Report.[date] = @date
END
GO

CREATE PROCEDURE [dbo].[SP_Report_RetrieveReportByKeys]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100),
	@date DATE
AS
BEGIN
	SELECT *
	FROM Report
	WHERE Parent_username = @Parent_username AND
		Child_name = @Child_name AND
		[date] = @date
END
GO

CREATE PROCEDURE [dbo].[SP_Report_RetrieveReportListByUsernameAndChildName]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100)
AS
BEGIN
	SELECT *
	FROM Report
	WHERE	Parent_username = @Parent_username AND 
			Child_name = @Child_name 
END
GO

CREATE PROCEDURE [dbo].[SP_Report_UpdateReportByKeys]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100),
	@Behavior_name VARCHAR(100),
	@date DATE,
	@coin_earned INT,
	@note VARCHAR(500)
AS
BEGIN
	UPDATE Report
	SET Behavior_name = @Behavior_name, note = @note, coin_earned = @coin_earned
	WHERE Parent_username = @Parent_username AND
		Child_name = @Child_name AND
		[date] = @date
END
GO