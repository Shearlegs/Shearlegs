CREATE PROCEDURE dbo.AddPlugin
	@PackageId VARCHAR(255),
	@Name NVARCHAR(255),
	@Description NVARCHAR(2000),
	@Author NVARCHAR(255),
	@CreateUserId INT,
	@PluginId INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF EXISTS(SELECT * FROM dbo.Plugins WHERE PackageId = @PackageId)
	BEGIN
		RETURN 1;
	END;

	INSERT INTO dbo.Plugins (PackageId, [Name], [Description], Author, CreateUserId)
	VALUES (@PackageId, @Name, @Description, @Author, @CreateUserId);

	SELECT @PluginId = SCOPE_IDENTITY();

	RETURN 0;
END