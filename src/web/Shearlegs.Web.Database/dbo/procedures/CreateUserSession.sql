CREATE PROCEDURE dbo.CreateUserSession
	@Username NVARCHAR(255),
	@PasswordHash NVARCHAR(128),
	@HostName NVARCHAR(255),
	@UserId INT OUTPUT,
	@SessionId UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	SELECT @UserId = Id FROM dbo.Users WHERE [Name] = @Username AND PasswordHash = @PasswordHash;

	IF @UserId IS NULL
	BEGIN
		RETURN 1;
	END

	INSERT INTO dbo.UserSessions (UserId, AuthenticationMethod, AuthenticationScheme, HostName, [ExpireDate])
	OUTPUT INSERTED.Id INTO @SessionId
	VALUES (@UserId, 'Username and Password', 'Bearer', @HostName, DATEADD(MINUTE, 30, SYSDATETIME()));

	RETURN 0;
END;