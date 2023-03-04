﻿CREATE PROCEDURE dbo.UpdatePlugin
	@PluginId INT,
	@Name NVARCHAR(255),
	@Description NVARCHAR(2000),
	@Author NVARCHAR(255),
	@UpdateUserId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;	

	IF NOT EXISTS(SELECT * FROM dbo.Plugins WHERE Id = @PluginId)
	BEGIN
		RETURN 1;
	END;

	UPDATE dbo.Plugins
	SET [Name] = @Name,
	[Description] = @Description,
	Author = @Author,
	UpdateUserId = @UpdateUserId,
	UpdateDate = SYSDATETIME()
	WHERE Id = @PluginId;

	RETURN 0;
END