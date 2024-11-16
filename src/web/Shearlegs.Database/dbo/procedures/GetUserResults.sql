CREATE PROCEDURE dbo.GetUserResults @UserId INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT
		r.Id,
		r.VersionId,
		r.UserId,
		r.ResultType,
		r.CreateDate,
		v.Id,
		v.[Name], 
		p.Id,
		p.[Name],
		u.Id,
		u.[Name]
	FROM dbo.Results r
	JOIN dbo.Versions v ON v.Id = r.VersionId JOIN dbo.Plugins p ON p.Id = v.PluginId 
	JOIN dbo.Users u ON u.Id = r.UserId
	WHERE r.UserId = @UserId
	ORDER BY r.CreateDate DESC;

	RETURN 0;
END;
