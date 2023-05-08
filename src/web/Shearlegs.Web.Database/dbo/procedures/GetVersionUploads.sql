CREATE PROCEDURE dbo.GetVersionUploads
	@VersionUploadId INT,
	@UserId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF @VersionUploadId IS NOT NULL
	BEGIN
		SELECT vu.*, u.*, n.*
		FROM dbo.VersionUploads vu
		JOIN dbo.Users u ON u.Id = vu.UserId
		LEFT JOIN dbo.Nodes n ON n.Id = vu.NodeId
		WHERE vu.Id = @VersionUploadId;
	END
	ELSE IF @UserId IS NOT NULL
	BEGIN
		SELECT vu.*, u.*, n.*
		FROM dbo.VersionUploads vu
		JOIN dbo.Users u ON u.Id = vu.UserId
		LEFT JOIN dbo.Nodes n ON n.Id = vu.NodeId
		WHERE vu.UserId = @UserId;
	END
	ELSE
	BEGIN
		SELECT vu.*, u.*, n.*
		FROM dbo.VersionUploads vu
		JOIN dbo.Users u ON u.Id = vu.UserId
		LEFT JOIN dbo.Nodes n ON n.Id = vu.NodeId;
	END;

	RETURN 0;
END;
