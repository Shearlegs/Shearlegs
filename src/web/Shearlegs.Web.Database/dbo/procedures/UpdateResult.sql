CREATE PROCEDURE dbo.UpdateResult
	@ResultId INT,
	@Status TINYINT,
	@ResultData VARBINARY(MAX),
	@ResultType VARCHAR(255)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF NOT EXISTS (SELECT * FROM dbo.Results WHERE Id = @ResultId)
	BEGIN
		RETURN 1;
	END;

	UPDATE dbo.Results
	SET 
		[Status] = @Status,
		ResultData = @ResultData,
		ResultType = @ResultType
	WHERE 
		Id = @ResultId;

	RETURN 0;
END;
	
