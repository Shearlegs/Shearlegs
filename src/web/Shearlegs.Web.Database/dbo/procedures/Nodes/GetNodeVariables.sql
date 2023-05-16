﻿CREATE PROCEDURE dbo.GetNodeVariables
	@NodeVariableId INT,
	@NodeId INT,
	@Name NVARCHAR(255)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF @NodeVariableId IS NOT NULL
	BEGIN
		SELECT nv.*, uu.*, cu.* FROM dbo.NodeVariables nv
		LEFT JOIN dbo.Users uu ON uu.Id = nv.UpdateUserId
		LEFT JOIN dbo.Users cu ON cu.Id = nv.CreateUserId
		WHERE nv.Id = @NodeVariableId;
	END
	ELSE IF @Name IS NOT NULL AND @NodeId IS NOT NULL
	BEGIN
		SELECT nv.*, uu.*, cu.* FROM dbo.NodeVariables nv
		LEFT JOIN dbo.Users uu ON uu.Id = nv.UpdateUserId
		LEFT JOIN dbo.Users cu ON cu.Id = nv.CreateUserId
		WHERE nv.NodeId = @NodeId AND nv.[Name] = @Name;
	END
	ELSE IF @Name IS NOT NULL
	BEGIN
		SELECT nv.*, uu.*, cu.* FROM dbo.NodeVariables nv
		LEFT JOIN dbo.Users uu ON uu.Id = nv.UpdateUserId
		LEFT JOIN dbo.Users cu ON cu.Id = nv.CreateUserId
		WHERE nv.[Name] = @Name;
	END
	ELSE IF @NodeId IS NOT NULL
	BEGIN
		SELECT nv.*, uu.*, cu.* FROM dbo.NodeVariables nv
		LEFT JOIN dbo.Users uu ON uu.Id = nv.UpdateUserId
		LEFT JOIN dbo.Users cu ON cu.Id = nv.CreateUserId
		WHERE nv.NodeId = @NodeId;
	END
	ELSE
	BEGIN
		SELECT nv.*, uu.*, cu.* FROM dbo.NodeVariables nv
		LEFT JOIN dbo.Users uu ON uu.Id = nv.UpdateUserId
		LEFT JOIN dbo.Users cu ON cu.Id = nv.CreateUserId;
	END;

	RETURN 0;
END