CREATE PROCEDURE dbo.CreateUser
	@Name NVARCHAR(255),
	@Role VARCHAR(255),
	@Password NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Users (Name, Role, PasswordHash)
	OUTPUT
		INSERTED.Id,
		INSERTED.Name,
		INSERTED.Role,
		INSERTED.LastLoginDate,
		INSERTED.UpdateDate,
		INSERTED.CreateDate
	VALUES (@Name, @Role, PWDENCRYPT(@Password));

	RETURN 0;
END;