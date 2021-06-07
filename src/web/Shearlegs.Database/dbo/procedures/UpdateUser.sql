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
	SET Name = @Name,
		Role = @Role,
		PasswordHash = PWDENCRYPT(@Password),
		UpdateDate = SYSDATETIME()
	WHERE Id = @Id;

	SELECT Id, Name, Role, LastLoginDate, UpdateDate, CreateDate FROM dbo.Users WHERE Id = @Id;

	COMMIT;

	RETURN 0;
END;