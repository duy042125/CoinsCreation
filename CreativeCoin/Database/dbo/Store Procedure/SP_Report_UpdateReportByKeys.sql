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