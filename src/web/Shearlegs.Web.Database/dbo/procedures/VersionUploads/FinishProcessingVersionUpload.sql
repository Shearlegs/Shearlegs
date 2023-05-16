CREATE PROCEDURE dbo.FinishProcessingVersionUpload
	@VersionUploadId INT,
	@PackageId NVARCHAR(255),
	@PackageVersion NVARCHAR(255),
	@ErrorMessage NVARCHAR(MAX),
	@Status TINYINT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	UPDATE dbo.VersionUploads
	SET 
		[Status] = @Status,
		PackageId = @PackageId,
		PackageVersion = @PackageVersion,
		ErrorMessage = @ErrorMessage,
		ProcessingEndDate = SYSDATETIME()
	WHERE Id = @VersionUploadId;

	IF @@ROWCOUNT = 0
	BEGIN
		PRINT 'VersionUpload not found';
		RETURN 1;
	END;

	RETURN 0;
END
