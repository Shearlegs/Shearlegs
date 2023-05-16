CREATE PROCEDURE dbo.GetVersions
	@VersionId INT,
	@PluginId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF @VersionId IS NOT NULL
	BEGIN
		SELECT v.*, cu.*, vp.* FROM dbo.Versions v 
		LEFT JOIN dbo.Users cu ON cu.Id = v.CreateUserId
		LEFT JOIN dbo.VersionParameters vp ON vp.VersionId = v.Id
		WHERE v.Id = @VersionId;
	END
	ELSE IF @PluginId IS NOT NULL
	BEGIN
		SELECT v.*, cu.*, vp.* FROM dbo.Versions v 
		LEFT JOIN dbo.Users cu ON cu.Id = v.CreateUserId
		LEFT JOIN dbo.VersionParameters vp ON vp.VersionId = v.Id
		WHERE v.PluginId = @PluginId;
	END
	ELSE
	BEGIN
		SELECT v.*, cu.*, vp.* FROM dbo.Versions v 
		LEFT JOIN dbo.Users cu ON cu.Id = v.CreateUserId
		LEFT JOIN dbo.VersionParameters vp ON vp.VersionId = v.Id;
	END;

	RETURN 0;
END;
