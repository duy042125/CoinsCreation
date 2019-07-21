CREATE TABLE [dbo].[Assign]
(
	Parent_username VARCHAR(100),
	Behavior_name VARCHAR(100),
	CONSTRAINT PK_Assign PRIMARY KEY (Parent_username, Behavior_name),
	CONSTRAINT FK_Assign_Account FOREIGN KEY (Parent_username) REFERENCES Account([username]),
	CONSTRAINT FK_Assign_Behavior FOREIGN KEY (Behavior_name) REFERENCES Behavior([name])
)
