CREATE PROCEDURE [dbo].[UserQuoteList]
	@UsrId int
AS
	SELECT  QuoId,QuoUsrId,QuoText,QuoDate FROM dbo.Quote WHERE @UsrId IS NULL OR QuoUsrId = @UsrId ORDER BY QuoDate DESC

	SELECT QutQuoId QuoteId,tag.TagLabel,tag.TagTtyId TagType,COUNT(tag.TagId) Amount 
	FROM dbo.QuoteUserTag qut
	INNER JOIN dbo.Tag  tag ON tag.TagId = qut.QutTagId
	GROUP BY qut.QutQuoId,tag.TagLabel,tag.TagTtyId
	ORDER BY QutQuoId

