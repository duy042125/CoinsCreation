CREATE PROCEDURE [dbo].[SP_Child_RetrieveAllChildren]
AS
BEGIN
	SELECT [Parent_username], [Child_name], [birthdate], [start_date], [total_coin]
	FROM Child
END