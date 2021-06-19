CREATE TABLE dbo.VersionParameters
(
	Id INT IDENTITY(1, 1) NOT NULL CONSTRAINT PK_VersionParameters PRIMARY KEY,
    VersionId INT NOT NULL CONSTRAINT FK_VersionParameters_VersionId FOREIGN KEY REFERENCES dbo.Versions(Id),
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(2000) NULL,
    InputType VARCHAR(255) NOT NULL,
    DataType VARCHAR(255) NOT NULL,
    Value NVARCHAR(MAX) NULL,
    IsArray BIT NOT NULL CONSTRAINT DF_VersionParameters_IsArray DEFAULT 0,
    IsRequired BIT NOT NULL CONSTRAINT DF_VersionParameters_IsRequired DEFAULT 0,
    IsSecret BIT NOT NULL CONSTRAINT DF_VersionParameters_IsSecret DEFAULT 0,
    CONSTRAINT UK_VersionParameters UNIQUE (VersionId, Name)
)