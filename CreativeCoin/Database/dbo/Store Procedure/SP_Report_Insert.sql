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
