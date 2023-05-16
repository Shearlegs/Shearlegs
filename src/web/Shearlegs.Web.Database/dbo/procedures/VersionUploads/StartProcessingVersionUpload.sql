CREATE PROCEDURE dbo.StartProcessingVersionUpload
	@VersionUploadId INT,
	@NodeId INT,
	@Status TINYINT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	UPDATE dbo.VersionUploads
	SET 
		[Status] = @Status,
		ProcessingStartDate = SYSDATETIME(),
		ProcessingEndDate = NULL,
		NodeId = @NodeId
	WHERE Id = @VersionUploadId;

	IF @@ROWCOUNT = 0
	BEGIN
		PRINT 'VersionUpload not found';
		RETURN 1;
	END;
	
	RETURN 0;
END