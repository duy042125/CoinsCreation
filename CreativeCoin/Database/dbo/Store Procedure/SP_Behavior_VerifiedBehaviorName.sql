CREATE PROCEDURE [dbo].[SP_Behavior_VerifiedBehaviorName]
	@name VARCHAR(100)
AS
BEGIN
	SELECT * 
	FROM dbo.Behavior
	WHERE [name] = @name
END