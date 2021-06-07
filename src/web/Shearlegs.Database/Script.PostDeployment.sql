IF NOT EXISTS (SELECT * FROM dbo.Users WHERE Name = 'admin')
	EXEC dbo.CreateUser @Name = 'admin', @Role = 'Admin', @Password = 'admin0';