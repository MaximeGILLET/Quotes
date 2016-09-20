CREATE PROCEDURE [dbo].[UserQuoteList]
	@UsrId int
AS
	SELECT  QuoId,QuoUsrId,QuoText,QuoDate , ISNULL(t.Amount,0) 'Like', ISNULL(t1.Amount,0) 'Dislike', ISNULL(t2.Amount,0) 'Star'
	FROM dbo.Quote m
	LEFT JOIN (SELECT QutQuoId,TagLabel,COUNT(QutTagId) Amount FROM dbo.QuoteUserTag INNER JOIN dbo.Tag ON TagId=QutTagId GROUP BY QutQuoId,TagLabel) t ON t.QutQuoId = m.QuoId  AND t.TagLabel = 'Like'
	LEFT JOIN (SELECT QutQuoId,TagLabel,COUNT(QutTagId) Amount FROM dbo.QuoteUserTag INNER JOIN dbo.Tag ON TagId=QutTagId GROUP BY QutQuoId,TagLabel) t1 ON t1.QutQuoId = m.QuoId  AND t1.TagLabel = 'Dislike'
	LEFT JOIN (SELECT QutQuoId,TagLabel,COUNT(QutTagId) Amount FROM dbo.QuoteUserTag INNER JOIN dbo.Tag ON TagId=QutTagId GROUP BY QutQuoId,TagLabel) t2 ON t2.QutQuoId = m.QuoId  AND t2.TagLabel = 'Star'
	WHERE @UsrId IS NULL OR QuoUsrId = @UsrId 	
	ORDER BY QuoDate DESC


	SELECT QutQuoId QuoteId,tag.TagLabel,tag.TagTtyId TagType,COUNT(tag.TagId) Amount 
	FROM dbo.QuoteUserTag qut
	INNER JOIN dbo.Tag  tag ON tag.TagId = qut.QutTagId
	GROUP BY qut.QutQuoId,tag.TagLabel,tag.TagTtyId
	ORDER BY QutQuoId

