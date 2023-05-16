CREATE PROCEDURE dbo.AddPluginSecret
	@PluginId INT,
	@Name NVARCHAR(255),
	@Value VARBINARY(MAX),
	@CreateUserId INT,
	@PluginSecretId INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF EXISTS(SELECT * FROM dbo.PluginSecrets WHERE PluginId = @PluginId AND [Name] = @Name)
	BEGIN
		RETURN 1;
	END;

	IF NOT EXISTS(SELECT * FROM dbo.Plugins WHERE Id = @PluginId)
	BEGIN
		RETURN 2;
	END;

	INSERT INTO dbo.PluginSecrets (PluginId, [Name], [Value], CreateUserId)
	VALUES (@PluginId, @Name, @Value, @CreateUserId);
	
	SET @PluginSecretId = SCOPE_IDENTITY();

	RETURN 0;
END
