CREATE PROCEDURE dbo.AddNodeVariable
	@NodeId INT,
	@Name NVARCHAR(255),
	@Value NVARCHAR(2000),
	@Description NVARCHAR(MAX),
	@IsSensitive BIT,
	@CreateUserId INT,
	@NodeVariableId INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF NOT EXISTS(SELECT * FROM dbo.Nodes WHERE Id = @NodeId)
	BEGIN
		RETURN 1;
	END

	IF EXISTS(SELECT * FROM dbo.NodeVariables WHERE NodeId = @NodeId AND [Name] = @Name)
	BEGIN
		RETURN 2;
	END;

	INSERT INTO dbo.NodeVariables (NodeId, [Name], [Value], [Description], IsSensitive, CreateUserId)
	VALUES (@NodeId, @Name, @Value, @Description, @IsSensitive, @CreateUserId);

	SELECT @NodeVariableId = SCOPE_IDENTITY();

	RETURN 0;
END