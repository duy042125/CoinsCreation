CREATE TABLE Account
(
	username VARCHAR(100),
	[password] VARCHAR(100) NOT NULL,
	[name] VARCHAR(100),
	CONSTRAINT PK_Account PRIMARY KEY (username)
)
