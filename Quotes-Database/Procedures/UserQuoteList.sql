CREATE PROCEDURE [dbo].[UserQuoteList]
	@UsrId int
AS
	SELECT  QuoId,QuoUsrId,QuoText,QuoDate FROM dbo.Quote WHERE @UsrId IS NULL OR QuoUsrId = @UsrId ORDER BY QuoDate DESC

