CREATE PROCEDURE dbo.GetNodes
	@NodeId INT,
	@Name NVARCHAR(255)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF @NodeId IS NOT NULL
	BEGIN
		SELECT n.*, uu.*, cu.* FROM dbo.Nodes n 
		LEFT JOIN dbo.Users uu ON uu.Id = n.UpdateUserId
		LEFT JOIN dbo.Users cu ON cu.Id = n.CreateUserId
		WHERE n.Id = @NodeId;
	END
	ELSE IF @Name IS NOT NULL
	BEGIN
		SELECT n.*, uu.*, cu.* FROM dbo.Nodes n 
		LEFT JOIN dbo.Users uu ON uu.Id = n.UpdateUserId
		LEFT JOIN dbo.Users cu ON cu.Id = n.CreateUserId
		WHERE n.[Name] = @Name;
	END
	ELSE
	BEGIN
		SELECT n.*, uu.*, cu.* FROM dbo.Nodes n 
		LEFT JOIN dbo.Users uu ON uu.Id = n.UpdateUserId
		LEFT JOIN dbo.Users cu ON cu.Id = n.CreateUserId;
	END;

	RETURN 0;
END
