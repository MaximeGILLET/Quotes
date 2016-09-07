CREATE PROCEDURE [dbo].[CheckNextUserPostDate]
	@UsrId int = 0
AS
	IF NOT EXISTS (SELECT 1 FROM dbo.Quote WHERE QuoUsrId = @UsrId )
	BEGIN
		SELECT SYSUTCDATETIME() QuoDate
	END
	IF EXISTS(SELECT 1 FROM dbo.Quote WHERE DATEADD (day , 1 , QuoDate) < SYSUTCDATETIME() AND QuoUsrId = @UsrId)
	BEGIN
		SELECT DATEADD (day , 1 , QuoDate) QuoDate FROM dbo.Quote WHERE DATEADD (day , 1 , QuoDate) < SYSUTCDATETIME() AND QuoUsrId = @UsrId
	END




