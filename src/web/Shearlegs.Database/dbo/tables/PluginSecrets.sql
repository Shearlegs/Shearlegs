﻿CREATE TABLE dbo.PluginSecrets
(
	Id INT NOT NULL CONSTRAINT PK_PluginSecrets PRIMARY KEY,
	PluginId INT NOT NULL CONSTRAINT FK_PluginSecrets_PluginId FOREIGN KEY REFERENCES dbo.Plugins(Id),
    Name NVARCHAR(255) NOT NULL,
    Value NVARCHAR(MAX) NOT NULL,
    IsArray BIT NOT NULL CONSTRAINT DF_PluginSecrets_IsArray DEFAULT 0, 
    CreateUserId INT NOT NULL CONSTRAINT FK_PluginSecrets_CreateUserId FOREIGN KEY REFERENCES dbo.Users(Id),
    CreateDate DATETIME2(0) NOT NULL CONSTRAINT DF_PluginSecrets_CreateDate DEFAULT SYSDATETIME(),
    CONSTRAINT UK_PluginSecrets UNIQUE (PluginId, Name)
);