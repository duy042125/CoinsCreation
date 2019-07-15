CREATE PROCEDURE [dbo].[SP_Behavior_Insert]
	@name VARCHAR(100),
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
	VALUES (@name, @behavior1, @behavior2, @behavior3, @behavior4, @star5_reward1, @star5_reward2, @star5_reward3, @star10_reward1, @star10_reward2, @star10_reward3, @star15_reward1, @star15_reward2, @star20_reward)
END
