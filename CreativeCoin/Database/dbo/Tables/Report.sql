CREATE TABLE Report
(
	Parent_username VARCHAR(100),
	Child_name VARCHAR(100),
	Behavior_name VARCHAR(100),
	[date] DATE,
	coin_earned INT,
	CONSTRAINT PK_Report PRIMARY KEY (Parent_username, Child_name, Behavior_name, [date]),
	CONSTRAINT FK_Report_Chil FOREIGN KEY (Parent_username, Child_name) 
		REFERENCES Child(Parent_username, Child_name),
	CONSTRAINT FK_Report_Behavior FOREIGN KEY (Behavior_name) 
		REFERENCES Behavior([name])
)
