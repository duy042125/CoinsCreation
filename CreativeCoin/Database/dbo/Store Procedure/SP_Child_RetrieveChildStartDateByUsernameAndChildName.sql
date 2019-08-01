CREATE PROCEDURE [dbo].[SP_Child_RetrieveChildStartDateByUsernameAndChildName]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100)
AS
BEGIN
	SELECT [start_date]
	FROM Child
	WHERE Parent_username = @Parent_username AND Child_name = @Child_name
END