﻿CREATE TABLE dbo.Plugins
(
	Id INT IDENTITY(1, 1) NOT NULL CONSTRAINT PK_Plugins PRIMARY KEY,
	PackageId VARCHAR(255) NOT NULL,
	[Name] NVARCHAR(255) NOT NULL,
	[Description] NVARCHAR(2000) NULL,
	Author NVARCHAR(255) NULL,
	UpdateUserId INT NULL CONSTRAINT FK_Plugins_UpdateUserId FOREIGN KEY REFERENCES dbo.Users(Id),
	CreateUserId INT NULL CONSTRAINT FK_Plugins_CreateUserId FOREIGN KEY REFERENCES dbo.Users(Id),	
	UpdateDate DATETIME2(0) NULL,
	CreateDate DATETIME2(0) NOT NULL CONSTRAINT DF_Plugins_CreateDate DEFAULT SYSDATETIME(),
	CONSTRAINT UK_Plugins_PackageId UNIQUE (PackageId)
)