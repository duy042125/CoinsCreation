CREATE PROCEDURE [dbo].[SP_Child_VerifiedChildName]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100)
AS
BEGIN
	SELECT * 
	FROM dbo.Child
	WHERE Parent_username = @Parent_username AND Child_name = @Child_name
END