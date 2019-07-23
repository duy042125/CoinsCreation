CREATE PROCEDURE [dbo].[SP_Behavior_Assign_RetrieveBehaviorListByUsername]
	@Parent_usernname VARCHAR(100)
AS
BEGIN
	SELECT Behavior.*
	FROM Assign INNER JOIN Behavior
	ON Behavior_name = [name]
	WHERE Parent_username = @Parent_usernname
END
