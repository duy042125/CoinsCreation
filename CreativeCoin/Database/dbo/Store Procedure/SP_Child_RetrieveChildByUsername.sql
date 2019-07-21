CREATE PROCEDURE [dbo].[SP_Child_RetrieveChildByUsername]
	@Parent_usernname VARCHAR(100)
AS
BEGIN
	SELECT * 
	FROM Child
	WHERE Parent_username = @Parent_usernname
END
