CREATE PROCEDURE dbo.CreateUserSession
	@UserId INT,
	@HostName NVARCHAR(255),
	@IPAddress NVARCHAR(255),
	@UserAgent NVARCHAR(255),	
	@SessionId UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @temp TABLE (Id UNIQUEIDENTIFIER);

	IF NOT EXISTS(SELECT * FROM dbo.Users WHERE Id = @UserId)
	BEGIN
		RETURN 1;
	END

	INSERT INTO dbo.UserSessions (UserId, AuthenticationMethod, AuthenticationScheme, HostName, IPAddress, UserAgent, [ExpireDate])
	OUTPUT INSERTED.Id INTO @temp
	VALUES (@UserId, 'Username and Password', 'Bearer', @HostName, @IPAddress, @UserAgent, DATEADD(MINUTE, 30, SYSDATETIME()));

	SELECT @SessionId = Id FROM @temp;

	RETURN 0;
END;