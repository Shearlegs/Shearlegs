CREATE PROCEDURE dbo.UpdateNode
	@NodeId INT,
	@Name NVARCHAR(255),
	@Description NVARCHAR(MAX),
	@FQDN VARCHAR(255),
	@Scheme VARCHAR(255),
	@IsBehindProxy BIT,
	@IsEnabled BIT,
	@UpdateUserId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;	

	IF NOT EXISTS(SELECT * FROM dbo.Nodes WHERE Id = @NodeId)
	BEGIN
		RETURN 1;
	END;

	IF EXISTS(SELECT * FROM dbo.Nodes WHERE [Name] = @Name AND Id <> @NodeId)
	BEGIN
		RETURN 2;
	END;

	UPDATE dbo.Nodes
	SET [Name] = @Name,
	[Description] = @Description,
	FQDN = @FQDN,
	Scheme = @Scheme,
	IsBehindProxy = @IsBehindProxy,
	IsEnabled = @IsEnabled,
	UpdateUserId = @UpdateUserId,
	UpdateDate = SYSDATETIME()
	WHERE Id = @NodeId;

	RETURN 0;
END