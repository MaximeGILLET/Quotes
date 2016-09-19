CREATE PROCEDURE [dbo].[QuoteTagList]
	@QuoId int
AS
	SELECT QutQuoId,QutUsrId,TagLabel,COUNT(QutTagId) Amount FROM dbo.QuoteUserTag  INNER JOIN dbo.Tag  ON  TagId = QutTagId 
	GROUP BY QutQuoId,QutUsrId,TagLabel
