CREATE PROCEDURE dbo.UpdateNodeVariable
	@NodeVariableId INT,
	@Name NVARCHAR(255),
	@Value NVARCHAR(2000),
	@Description NVARCHAR(MAX),
	@IsSensitive BIT,
	@UpdateUserId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF NOT EXISTS(SELECT * FROM dbo.NodeVariables WHERE Id = @NodeVariableId)
	BEGIN
		RETURN 1;
	END

	DECLARE @NodeId INT;
	SELECT @NodeId = NodeId FROM dbo.NodeVariables WHERE Id = @NodeVariableId;

	IF EXISTS(SELECT * FROM dbo.NodeVariables WHERE NodeId = @NodeId AND [Name] = @Name AND Id <> @NodeVariableId)
	BEGIN
		RETURN 2;
	END;

	UPDATE dbo.NodeVariables
	SET [Name] = @Name,
	[Value] = @Value,
	[Description] = @Description,
	IsSensitive = @IsSensitive,
	UpdateUserId = @UpdateUserId,
	UpdateDate = SYSDATETIME()
	WHERE Id = @NodeVariableId;

	RETURN 0;
END