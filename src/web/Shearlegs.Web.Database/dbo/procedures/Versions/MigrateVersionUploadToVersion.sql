CREATE PROCEDURE dbo.MigrateVersionUploadToVersion
	@VersionUploadId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @packageId NVARCHAR(255);
	DECLARE @packageVersion NVARCHAR(255);
	DECLARE @pluginId INT;

	SELECT @packageId = PackageId, @packageVersion = PackageVersion FROM dbo.VersionUploads WHERE Id = @VersionUploadId

	IF @@ROWCOUNT = 0
	BEGIN
		-- VersionUploadId does not exist
		RETURN 1;
	END;

	SELECT @pluginId = Id FROM dbo.Plugins WHERE PackageId = @packageId;

	IF @@ROWCOUNT = 0
	BEGIN
		-- Plugin does not exist
		RETURN 2;
	END

	IF EXISTS(SELECT * FROM dbo.Versions v JOIN dbo.Plugins p ON p.Id = v.PluginId WHERE p.PackageId = @packageId AND v.[Name] = @packageVersion)
	BEGIN
		-- Version already exists
		RETURN 3;
	END

	BEGIN TRAN;

	INSERT INTO dbo.Versions (PluginId, [Name], Content, ContentLength, CreateUserId)
	SELECT 
		@pluginId, 
		PackageVersion, 
		Content, 
		ContentLength, 
		UserId
	FROM dbo.VersionUploads 
	WHERE Id = @VersionUploadId;

	DECLARE @versionId INT = SCOPE_IDENTITY();

	INSERT INTO dbo.VersionParameters (VersionId, [Name], [Description], DataType, DefaultValue, IsArray, IsRequired, IsSecret)
	SELECT 
		@versionId, 
		[Name], 
		[Description], 
		DataType, 
		DefaultValue, 
		IsArray, 
		IsRequired, 
		IsSecret
	FROM dbo.VersionUploadParameters WHERE VersionUploadId = @VersionUploadId;

	COMMIT;

	RETURN 0;

END
