CREATE PROCEDURE dbo.FinishProcessingVersionUpload
	@VersionUploadId INT,
	@PackageId NVARCHAR(255),
	@PackageVersion NVARCHAR(255),
	@ErrorMessage NVARCHAR(MAX),
	@Status TINYINT,
	@ParametersJson NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @currentStatus TINYINT;

	SELECT @currentStatus = [Status] FROM dbo.VersionUploads WHERE Id = @VersionUploadId;

	IF @@ROWCOUNT = 0 
	BEGIN
		PRINT 'VersionUploadId does not exist';
		RETURN 1;
	END

	IF @currentStatus <> 1
	BEGIN
		PRINT 'VersionUploadId is not in processing state';
		RETURN 2;
	END

	DECLARE @Parameters TABLE (
		[Name] NVARCHAR(255) NOT NULL, 
		[Description] NVARCHAR(2000) NULL,
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

	BEGIN TRAN;

	UPDATE dbo.VersionUploads
	SET 
		[Status] = @Status,
		PackageId = @PackageId,
		PackageVersion = @PackageVersion,
		ErrorMessage = @ErrorMessage,
		ProcessingEndDate = SYSDATETIME()
	WHERE Id = @VersionUploadId;

	INSERT INTO dbo.VersionUploadParameters (VersionUploadId, [Name], [Description], DataType, DefaultValue, IsArray, IsRequired, IsSecret)
	SELECT
		@VersionUploadId,
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
END
