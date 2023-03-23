CREATE PROCEDURE dbo.GetUserSessions
	@SessionId UNIQUEIDENTIFIER,
	@UserId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	
	IF @SessionId IS NOT NULL
	BEGIN
		SELECT * FROM dbo.UserSessions WHERE Id = @SessionId;
	END
	ELSE IF @UserId IS NOT NULL
	BEGIN
		SELECT * FROM dbo.UserSessions WHERE UserId = @UserId;
	END
	ELSE
	BEGIN
		SELECT * FROM dbo.UserSessions;
	END;

	RETURN 0
END;
