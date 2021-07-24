CREATE PROCEDURE dbo.CreateUser
	@Name NVARCHAR(255),
	@Role VARCHAR(255),
	@AuthenticationType VARCHAR(255),
	@Password NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Users (Name, Role, AuthenticationType, PasswordHash)
	OUTPUT
		INSERTED.Id,
		INSERTED.Name,
		INSERTED.Role,
		INSERTED.AuthenticationType,
		INSERTED.LastLoginDate,
		INSERTED.UpdateDate,
		INSERTED.CreateDate
	VALUES (@Name, @Role, @AuthenticationType, PWDENCRYPT(@Password));

	RETURN 0;
END;