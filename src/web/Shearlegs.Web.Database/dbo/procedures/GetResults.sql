CREATE PROCEDURE dbo.GetResults
	@ResultId INT,
	@PluginId INT,
	@VersionId INT,
	@UserId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF @ResultId IS NOT NULL
	BEGIN
		SELECT r.*, u.*, v.*, p.* FROM dbo.Results r 
		JOIN dbo.Users u ON u.Id = r.UserId
		JOIN dbo.Versions v ON v.Id = r.VersionId
		JOIN dbo.Plugins p ON p.Id = v.PluginId
		WHERE r.Id = @ResultId;
	END
	ELSE IF @PluginId IS NOT NULL
	BEGIN
		SELECT r.*, u.*, v.*, p.* FROM dbo.Results r 
		JOIN dbo.Users u ON u.Id = r.UserId
		JOIN dbo.Versions v ON v.Id = r.VersionId
		JOIN dbo.Plugins p ON p.Id = v.PluginId
		WHERE v.PluginId = @PluginId;
	END
	ELSE IF @VersionId IS NOT NULL
	BEGIN
		SELECT r.*, u.*, v.*, p.* FROM dbo.Results r 
		JOIN dbo.Users u ON u.Id = r.UserId
		JOIN dbo.Versions v ON v.Id = r.VersionId
		JOIN dbo.Plugins p ON p.Id = v.PluginId
		WHERE v.Id = @VersionId;
	END
	ELSE IF @UserId IS NOT NULL
	BEGIN
		SELECT r.*, u.*, v.*, p.* FROM dbo.Results r 
		JOIN dbo.Users u ON u.Id = r.UserId
		JOIN dbo.Versions v ON v.Id = r.VersionId
		JOIN dbo.Plugins p ON p.Id = v.PluginId
		WHERE u.Id = @UserId;
	END
	ELSE
	BEGIN
		SELECT r.*, u.*, v.*, p.* FROM dbo.Results r 
		JOIN dbo.Users u ON u.Id = r.UserId
		JOIN dbo.Versions v ON v.Id = r.VersionId
		JOIN dbo.Plugins p ON p.Id = v.PluginId;
	END;

	RETURN 0;
END;