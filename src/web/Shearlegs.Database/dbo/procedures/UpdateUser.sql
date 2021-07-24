CREATE PROCEDURE dbo.UpdateUser
	@Id INT,
	@Name NVARCHAR(255),
	@Role VARCHAR(255),
	@Password NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRAN;

	UPDATE dbo.Users
	SET 
		Role = @Role,
		UpdateDate = SYSDATETIME()
	WHERE 
		Id = @Id;

	IF @Password IS NOT NULL
		UPDATE dbo.Users SET PasswordHash = PWDENCRYPT(@Password) WHERE Id = @Id;

	SELECT Id, Name, Role, AuthenticationType, LastLoginDate, UpdateDate, CreateDate FROM dbo.Users WHERE Id = @Id;

	COMMIT;

	RETURN 0;
END;