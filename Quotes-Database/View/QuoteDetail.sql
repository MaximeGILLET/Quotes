CREATE VIEW [dbo].[QuoteDetail]
	AS SELECT QuoId , QuoDate , QuoText , QuoUsrId ,TagLabel,Amount
	FROM dbo.Quote  
	INNER JOIN (SELECT QutQuoId,QutUsrId,QutTagId,COUNT(QutTagId) Amount FROM dbo.QuoteUserTag  GROUP BY QutQuoId,QutUsrId,QutTagId) T ON T.QutUsrId = QuoUsrId
	INNER JOIN dbo.Tag ON TagId = QutTagId
