CREATE TABLE Child
(
	Parent_username VARCHAR(100),
	Child_name VARCHAR(100),
	birthdate DATE,
	total_coin INT DEFAULT (0),
	CONSTRAINT PK_Child PRIMARY KEY (Parent_username, Child_name),
	CONSTRAINT FK_Child_Parent FOREIGN KEY (Parent_username) 
		REFERENCES Account(username)
)
