CREATE PROCEDURE dbo.UpdatePluginSecret
	@PluginSecretId INT,
	@Value VARBINARY(MAX),
	@UpdateUserId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;	

	IF NOT EXISTS(SELECT * FROM dbo.PluginSecrets WHERE Id = @PluginSecretId)
	BEGIN
		RETURN 1;
	END;

	UPDATE dbo.PluginSecrets
	SET [Value] = @Value,
	UpdateUserId = @UpdateUserId,
	UpdateDate = SYSDATETIME()
	WHERE Id = @PluginSecretId;

	RETURN 0;
END