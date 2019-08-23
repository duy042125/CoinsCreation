CREATE TABLE Account
(
	username VARCHAR(100),
	[password] VARCHAR(100) NOT NULL,
	full_name VARCHAR(100),
	birthdate DATE,
	phone_number VARCHAR(15),
	SSN CHAR(10) NOT NULL,
	[type] CHAR(1) NOT NULL DEFAULT 'N',
	CONSTRAINT PK_Account PRIMARY KEY (username),
	CONSTRAINT U_Account_SSN UNIQUE (SSN)
)
