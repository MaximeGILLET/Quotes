CREATE PROCEDURE [dbo].[TopUserList]
	@from date = null,
	@to date = null
AS
	SELECT TOP 10 UserName, COUNT(QutId) Points
	FROM dbo.AspNetUsers usr 
	INNER JOIN dbo.Quote quo ON quo.QuoUsrId = usr.Id 
	INNER JOIN  dbo.QuoteUserTag qut ON quo.QuoId = qut.QutQuoId
	WHERE QutTagId IN (1,3)
	AND ( ( @from IS NULL AND @to IS NULL) OR ( QuoDate >= @from AND QuoDate <= @to))
	GROUP BY QutId,QuoUsrId ORDER BY Points
RETURN 0
