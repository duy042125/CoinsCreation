CREATE PROCEDURE [dbo].[SP_Child_UpdateChild]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100),
	@birthdate DATE
AS
BEGIN
	UPDATE Child
	SET birthdate = @birthdate
	WHERE Parent_username = @Parent_username AND Child_name = @Child_name
END