CREATE PROCEDURE dbo.AddUser
	@Name NVARCHAR(255),
	@Role TINYINT,
	@AuthenticationType TINYINT,
	@PasswordHash NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF EXISTS(SELECT * FROM dbo.Users WHERE [Name] = @Name)
	BEGIN
		RETURN 1;
	END;

	INSERT INTO dbo.Users ([Name], [Role], AuthenticationType, PasswordHash)
	OUTPUT INSERTED.*
	VALUES (@Name, @Role, @AuthenticationType, @PasswordHash);
	
	RETURN 0;
END
