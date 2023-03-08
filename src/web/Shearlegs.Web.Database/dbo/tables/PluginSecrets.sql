CREATE TABLE dbo.PluginSecrets
(
	Id INT IDENTITY(1, 1) NOT NULL,
	PluginId INT NOT NULL CONSTRAINT FK_PluginSecrets_PluginId FOREIGN KEY REFERENCES dbo.Plugins(Id),
    [Name] NVARCHAR(255) NOT NULL,
    [Value] VARBINARY(MAX) NOT NULL,
    UpdateUserId INT NULL CONSTRAINT FK_PluginSecrets_UpdateUserId FOREIGN KEY REFERENCES dbo.Users(Id),
    CreateUserId INT NULL CONSTRAINT FK_PluginSecrets_CreateUserId FOREIGN KEY REFERENCES dbo.Users(Id),
    UpdateDate DATETIME2(0) NULL,
    CreateDate DATETIME2(0) NOT NULL CONSTRAINT DF_PluginSecrets_CreateDate DEFAULT SYSDATETIME(),
    CONSTRAINT UK_PluginSecrets_PluginIdName UNIQUE (PluginId, [Name]),
    CONSTRAINT PK_PluginSecrets PRIMARY KEY (Id)
);