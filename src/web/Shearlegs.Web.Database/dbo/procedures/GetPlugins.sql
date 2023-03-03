CREATE PROCEDURE dbo.GetPlugins
	@PluginId INT NULL,
	@PackageId VARCHAR(255) NULL
AS
BEGIN
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
	ELSE
	BEGIN
		SELECT p.*, uu.*, cu.* FROM dbo.Plugins p 
		LEFT JOIN dbo.Users uu ON uu.Id = p.UpdateUserId
		LEFT JOIN dbo.Users cu ON cu.Id = p.CreateUserId;
	END;

	RETURN 0;
END
