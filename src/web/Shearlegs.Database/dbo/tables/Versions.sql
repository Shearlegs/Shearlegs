﻿CREATE TABLE dbo.Versions
(
	Id INT IDENTITY(1, 1) NOT NULL CONSTRAINT PK_Versions PRIMARY KEY,
	PluginId INT NOT NULL CONSTRAINT FK_Versions_PluginId FOREIGN KEY REFERENCES dbo.Plugins(Id),	
	Version VARCHAR(255) NOT NULL,
	PackageContent VARBINARY(MAX) NOT NULL,	
	CreateUserId INT NOT NULL CONSTRAINT FK_Versions_CreateUserId FOREIGN KEY REFERENCES dbo.Users(Id),	
	CreateDate DATETIME2(0) NOT NULL CONSTRAINT DF_Versions_CreateDate DEFAULT SYSDATETIME(),
	CONSTRAINT UK_Versions UNIQUE (PluginId, Version)
)
