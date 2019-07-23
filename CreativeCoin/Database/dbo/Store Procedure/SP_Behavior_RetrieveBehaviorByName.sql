CREATE PROCEDURE [dbo].[SP_Behavior_RetrieveBehaviorByName]
	@Behavior_name VARCHAR(100)
AS
BEGIN
	SELECT *
	FROM Behavior
	WHERE [name] = @Behavior_name
END
