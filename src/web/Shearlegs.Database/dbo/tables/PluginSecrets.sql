﻿CREATE TABLE dbo.PluginSecrets
(
	Id INT NOT NULL CONSTRAINT PK_PluginSecrets PRIMARY KEY,
	PluginId INT NOT NULL CONSTRAINT FK_PluginSecrets_PluginId FOREIGN KEY REFERENCES dbo.Plugins(Id),
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(2000) NULL,
    InputType VARCHAR(255) NOT NULL,
    DataType VARCHAR(255) NOT NULL,
    Value VARBINARY(MAX) NOT NULL,
    IsArray BIT NOT NULL CONSTRAINT DF_PluginSecrets_IsArray DEFAULT 0,
    CONSTRAINT UK_PluginSecrets UNIQUE (PluginId, Name)
);