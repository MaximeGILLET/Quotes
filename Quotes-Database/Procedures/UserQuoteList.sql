CREATE PROCEDURE [dbo].[UserQuoteList]
	@UsrId int
AS
	SELECT  QuoId,QuoUsrId,QuoText,QuoDate FROM dbo.Quote WHERE QuoUsrId = @UsrId ORDER BY QuoDate DESC

