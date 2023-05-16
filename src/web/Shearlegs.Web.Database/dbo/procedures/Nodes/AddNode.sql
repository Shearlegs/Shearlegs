CREATE PROCEDURE dbo.AddNode
	@Name NVARCHAR(255),
	@Description NVARCHAR(MAX),
	@FQDN VARCHAR(255),
	@Scheme VARCHAR(255),
	@HttpPort INT,
	@HttpsPort INT,
	@CacheSizeLimit INT,
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

	INSERT INTO dbo.Nodes ([Name], [Description], FQDN, Scheme, HttpPort, HttpsPort, CacheSizeLimit, IsBehindProxy, IsEnabled, CreateUserId)
	VALUES (@Name, @Description, @FQDN, @Scheme, @HttpPort, @HttpsPort, @CacheSizeLimit, @IsBehindProxy, @IsEnabled, @CreateUserId);

	SELECT @NodeId = SCOPE_IDENTITY();

	RETURN 0;
END