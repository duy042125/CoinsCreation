CREATE PROCEDURE [dbo].[SP_Child_Insert]
	@Parent_username VARCHAR(100),
	@Child_name VARCHAR(100),
	@birthdate DATE
AS
BEGIN
	INSERT INTO Child(Parent_username, Child_name, birthdate)
	VALUES (@Parent_username, @Child_name, @birthdate)
END
