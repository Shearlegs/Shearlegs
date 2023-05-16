CREATE TABLE [dbo].[VersionUploadParameters]
(
	Id INT IDENTITY(1, 1) NOT NULL CONSTRAINT PK_VersionUploadParameters PRIMARY KEY,
    VersionUploadId INT NOT NULL CONSTRAINT FK_VersionUploadParameters_VersionUploadId FOREIGN KEY REFERENCES dbo.VersionUploads(Id),
    [Name] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(2000) NULL,
    DataType VARCHAR(255) NOT NULL,
    DefaultValue NVARCHAR(MAX) NULL,
    IsArray BIT NOT NULL,
    IsRequired BIT NOT NULL,
    IsSecret BIT NOT NULL,
    CONSTRAINT UK_VersionUploadParameters_Name UNIQUE (VersionUploadId, [Name])
)
