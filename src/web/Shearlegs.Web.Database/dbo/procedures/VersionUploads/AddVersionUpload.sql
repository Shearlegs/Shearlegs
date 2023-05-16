CREATE PROCEDURE dbo.AddVersionUpload
	@UserId INT,
	@FileName NVARCHAR(255),
	@ContentType NVARCHAR(255),
	@Content VARBINARY(MAX),
	@ContentLength BIGINT,
	@LastModified DATETIME2(0),
	@VersionUploadId INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	INSERT INTO dbo.VersionUploads (UserId, [FileName], ContentType, Content, ContentLength, LastModified)
	VALUES (@UserId, @FileName, @ContentType, @Content, @ContentLength, @LastModified);

	SELECT @VersionUploadId = SCOPE_IDENTITY();

	RETURN 0;
END
