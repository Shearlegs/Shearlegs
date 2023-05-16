CREATE PROCEDURE dbo.GetPluginSecrets
	@PluginSecretId INT,
	@PluginId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF @PluginSecretId IS NOT NULL
	BEGIN
		SELECT ps.*, cu.* FROM dbo.PluginSecrets ps
		LEFT JOIN dbo.Users cu ON cu.Id = ps.CreateUserId
		WHERE ps.Id = @PluginSecretId;
	END
	ELSE IF @PluginId IS NOT NULL
	BEGIN
		SELECT ps.*, cu.* FROM dbo.PluginSecrets ps
		LEFT JOIN dbo.Users cu ON cu.Id = ps.CreateUserId
		WHERE ps.PluginId = @PluginId;
	END
	ELSE
	BEGIN
		SELECT ps.*, cu.* FROM dbo.PluginSecrets ps
		LEFT JOIN dbo.Users cu ON cu.Id = ps.CreateUserId;
	END;

	RETURN 0;
END
