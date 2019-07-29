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