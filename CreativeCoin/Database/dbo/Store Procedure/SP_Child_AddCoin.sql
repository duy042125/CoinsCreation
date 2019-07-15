CREATE PROCEDURE [dbo].[SP_Child_AddCoin]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100),
	@newCoin INT
AS
BEGIN
	UPDATE Child
	SET [total_coin] += @newCoin
	WHERE Parent_username = @Parent_username AND Child_name = @Child_name
END
