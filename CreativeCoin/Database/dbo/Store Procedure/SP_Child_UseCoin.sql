CREATE PROCEDURE [dbo].[SP_Child_UseCoin]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100),
	@usedCoin INT
AS
BEGIN
	UPDATE Child
	SET [total_coin] -= @usedCoin
	WHERE Parent_username = @Parent_username AND Child_name = @Child_name
END
