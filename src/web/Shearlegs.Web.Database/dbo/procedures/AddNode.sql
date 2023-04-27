CREATE PROCEDURE dbo.AddNode
	@Name NVARCHAR(255),
	@Description NVARCHAR(MAX),
	@FQDN VARCHAR(255),
	@Scheme VARCHAR(255),
	@IsBehindProxy BIT,
	@IsEnabled BIT,
	@CreateUserId INT,
	@NodeId INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF EXISTS(SELECT * FROM dbo.Nodes WHERE [Name] = @Name)
	BEGIN
		RETURN 1;
	END;

	INSERT INTO dbo.Nodes ([Name], [Description], FQDN, Scheme, IsBehindProxy, IsEnabled, CreateUserId)
	VALUES (@Name, @Description, @FQDN, @Scheme, @IsBehindProxy, @IsEnabled, @CreateUserId);

	SELECT @NodeId = SCOPE_IDENTITY();

	RETURN 0;
END