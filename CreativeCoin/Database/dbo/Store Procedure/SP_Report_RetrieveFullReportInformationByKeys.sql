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
