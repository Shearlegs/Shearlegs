CREATE PROCEDURE dbo.UpdateUserIdentity
	@UserId INT,
	@Role TINYINT,
	@PasswordHash NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF NOT EXISTS(SELECT * FROM dbo.Users WHERE Id = @UserId)
	BEGIN
		RETURN 1;
	END;

	UPDATE dbo.Users 
	SET [Role] = @Role,
	PasswordHash = ISNULL(@PasswordHash, PasswordHash),
	[UpdateDate] = SYSDATETIME()
	WHERE Id = @UserId;

	SELECT * FROM dbo.Users WHERE Id = @UserId;

	RETURN 0;
END
