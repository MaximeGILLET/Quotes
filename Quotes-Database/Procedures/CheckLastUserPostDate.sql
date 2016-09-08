CREATE PROCEDURE [dbo].[CheckLastUserPostDate]
	@UsrId int = 0
AS
	IF NOT EXISTS (SELECT 1 FROM dbo.Quote WHERE QuoUsrId = @UsrId )
	BEGIN
		SELECT CONVERT(date,SYSUTCDATETIME()) QuoDate
	END
	IF NOT EXISTS(SELECT 1 FROM dbo.Quote WHERE  CONVERT(date, QuoDate) = CONVERT(date,SYSUTCDATETIME()) AND QuoUsrId = @UsrId)
	BEGIN
		SELECT MAX(QuoDate) QuoDate FROM dbo.Quote WHERE  QuoUsrId = @UsrId
	END






