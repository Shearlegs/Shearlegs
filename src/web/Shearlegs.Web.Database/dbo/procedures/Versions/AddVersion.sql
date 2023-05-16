CREATE PROCEDURE dbo.AddVersion
	@PluginId INT,
	@Name NVARCHAR(255),
	@Content VARBINARY(MAX),
	@ContentLength BIGINT,
	@CreateUserId INT,
	@ParametersJson NVARCHAR(MAX),
	@VersionId INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;	
	
	DECLARE @Parameters TABLE (
		[Name] NVARCHAR(255) NOT NULL, 
		[Description] NVARCHAR(2000) NOT NULL,
		DataType VARCHAR(255) NOT NULL,
		DefaultValue NVARCHAR(MAX) NULL,
		IsArray BIT NOT NULL,
		IsRequired BIT NOT NULL,
		IsSecret BIT NOT NULL);

	INSERT INTO @Parameters
	SELECT 
		[Name],
		[Description],
		DataType,
		DefaultValue,
		IsArray,
		IsRequired,
		IsSecret
	FROM OPENJSON (@ParametersJson)  
	WITH (   
		[Name] NVARCHAR(255) '$.Name',
		[Description] NVARCHAR(2000) '$.Description',
		DataType VARCHAR(255) '$.DataType',
		DefaultValue NVARCHAR(MAX) '$.DefaultValue',
		IsArray BIT '$.IsArray',
		IsRequired BIT '$.IsRequired',
		IsSecret BIT '$.IsSecret'
	);

	IF NOT EXISTS(SELECT * FROM dbo.Plugins WHERE Id = @PluginId)
	BEGIN
		RETURN 1;
	END;

	IF EXISTS(SELECT * FROM dbo.Versions WHERE PluginId = @PluginId AND [Name] = @Name)
	BEGIN
		RETURN 2;
	END;

	BEGIN TRAN;

	INSERT INTO dbo.Versions (PluginId, [Name], Content, ContentLength, CreateUserId)
	VALUES (@PluginId, @Name, @Content, @ContentLength, @CreateUserId);

	SET @VersionId = SCOPE_IDENTITY();

	INSERT INTO dbo.VersionParameters (VersionId, [Name], [Description], DataType, DefaultValue, IsArray, IsRequired, IsSecret)
	SELECT
		@VersionId,
		[Name],
		[Description],
		DataType,
		DefaultValue,
		IsArray,
		IsRequired,
		IsSecret
	FROM @Parameters;

	COMMIT;

	RETURN 0;
END;
