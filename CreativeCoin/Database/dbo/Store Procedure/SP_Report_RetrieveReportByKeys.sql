CREATE PROCEDURE [dbo].[SP_Report_RetrieveReportByKeys]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100),
	@Behavior_name VARCHAR(100),
	@date DATE
AS
BEGIN
	SELECT *
	FROM Report
	WHERE Parent_username = @Parent_username AND
		Child_name = @Child_name AND
		Behavior_name = @Behavior_name AND
		[date] = @date
END
