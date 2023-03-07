CREATE PROCEDURE dbo.GetPlugins
	@PluginId INT,
	@PackageId VARCHAR(255),
	@VersionId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF @PluginId IS NOT NULL
	BEGIN
		SELECT p.*, uu.*, cu.* FROM dbo.Plugins p 
		LEFT JOIN dbo.Users uu ON uu.Id = p.UpdateUserId
		LEFT JOIN dbo.Users cu ON cu.Id = p.CreateUserId
		WHERE p.Id = @PluginId;
	END
	ELSE IF @PackageId IS NOT NULL
	BEGIN
		SELECT p.*, uu.*, cu.* FROM dbo.Plugins p 
		LEFT JOIN dbo.Users uu ON uu.Id = p.UpdateUserId
		LEFT JOIN dbo.Users cu ON cu.Id = p.CreateUserId
		WHERE p.PackageId = @PackageId;
	END
	ELSE IF @VersionId IS NOT NULL
	BEGIN
		SELECT p.*, uu.*, cu.* FROM dbo.Plugins p 
		LEFT JOIN dbo.Users uu ON uu.Id = p.UpdateUserId
		LEFT JOIN dbo.Users cu ON cu.Id = p.CreateUserId
		LEFT JOIN dbo.Versions v ON v.PluginId = p.Id
		WHERE v.Id = @VersionId;
	END
	ELSE
	BEGIN
		SELECT p.*, uu.*, cu.* FROM dbo.Plugins p 
		LEFT JOIN dbo.Users uu ON uu.Id = p.UpdateUserId
		LEFT JOIN dbo.Users cu ON cu.Id = p.CreateUserId;
	END;

	RETURN 0;
END
