﻿CREATE TABLE dbo.Users
(
	Id INT IDENTITY(1, 1) NOT NULL CONSTRAINT PK_Users PRIMARY KEY,
	Name NVARCHAR(255) NOT NULL CONSTRAINT UK_Users UNIQUE,
	Role VARCHAR(255) NOT NULL,
	AuthenticationType VARCHAR(255) NOT NULL CONSTRAINT DF_Users_AuthenticationType DEFAULT 'Default',
	PasswordHash VARBINARY(128) NULL,
	LastLoginDate DATETIME2(0) NULL,
	UpdateDate DATETIME2(0) NOT NULL CONSTRAINT DF_Users_UpdateDate DEFAULT SYSDATETIME(),
	CreateDate DATETIME2(0) NOT NULL CONSTRAINT DF_Users_CreateDate DEFAULT SYSDATETIME()
)
