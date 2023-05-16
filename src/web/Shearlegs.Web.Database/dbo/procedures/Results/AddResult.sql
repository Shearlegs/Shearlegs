CREATE PROCEDURE dbo.AddResult
	@VersionId INT,
	@ParametersData VARBINARY(MAX),
	@Status TINYINT,
	@UserId INT,
	@ResultId INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	
	INSERT INTO dbo.Results (VersionId, ParametersData, [Status], UserId)
	VALUES (@VersionId, @ParametersData, @Status, @UserId);

	SET @ResultId = SCOPE_IDENTITY();

	RETURN 0;
END