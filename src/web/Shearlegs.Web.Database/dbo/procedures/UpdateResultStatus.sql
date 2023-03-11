CREATE PROCEDURE dbo.UpdateResultStatus
	@ResultId INT,
	@Status TINYINT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF NOT EXISTS(SELECT * FROM dbo.Results WHERE Id = @ResultId)
	BEGIN
		RETURN 1;
	END;

	UPDATE dbo.Results
	SET 
		[Status] = @Status
	WHERE 
		Id = @ResultId;

	RETURN 0;
END;
