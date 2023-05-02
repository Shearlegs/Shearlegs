﻿CREATE TABLE dbo.Nodes
(
	Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Nodes PRIMARY KEY,
	UUID UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_Nodes_UUID DEFAULT NEWID(),
	[Name] NVARCHAR(255) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	FQDN NVARCHAR(255) NOT NULL,
	Scheme NVARCHAR(255) NOT NULL,
	HttpPort INT NOT NULL CONSTRAINT DF_Nodes_HttpPort DEFAULT 8080,
	HttpsPort INT NOT NULL CONSTRAINT DF_Nodes_HttpsPort DEFAULT 8443,
	CacheSizeLimit INT NOT NULL CONSTRAINT DF_Nodes_CacheSizeLimit DEFAULT 1000,
	IsBehindProxy BIT NOT NULL,
	IsEnabled BIT NOT NULL,
	AccessToken UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_Nodes_AccessToken DEFAULT NEWID(),
	UpdateUserId INT NULL CONSTRAINT FK_Nodes_UpdateUserId FOREIGN KEY REFERENCES dbo.Users(Id),
	CreateUserId INT NULL CONSTRAINT FK_Nodes_CreateUserId FOREIGN KEY REFERENCES dbo.Users(Id),	
	UpdateDate DATETIME2(0) NULL,
	CreateDate DATETIME2(0) NOT NULL CONSTRAINT DF_Nodes_CreateDate DEFAULT SYSDATETIME(),
	CONSTRAINT UK_Nodes_UUID UNIQUE (UUID),
	CONSTRAINT UK_Nodes_Name UNIQUE ([Name])
)
