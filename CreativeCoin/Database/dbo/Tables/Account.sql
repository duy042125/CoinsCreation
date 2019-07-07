﻿CREATE TABLE Account
(
	username VARCHAR(100),
	[password] VARCHAR(100) NOT NULL,
	full_name VARCHAR(100),
	birthdate DATE,
	phone_number CHAR(10),
	SSN CHAR(10) NOT NULL,
	CONSTRAINT PK_Account PRIMARY KEY (username),
	CONSTRAINT U_Account_SSN UNIQUE (SSN)
)
