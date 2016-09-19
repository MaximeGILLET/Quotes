-- This to be put in a more efficient way, maybe dynamic
	SELECT QuoId, ISNULL(t.Amount,0) 'Like', ISNULL(t1.Amount,0) 'Dislike', ISNULL(t2.Amount,0) 'Star'
	FROM dbo.Quote m
	LEFT JOIN (SELECT QutQuoId,TagLabel,COUNT(QutTagId) Amount FROM dbo.QuoteUserTag INNER JOIN dbo.Tag ON TagId=QutTagId GROUP BY QutQuoId,TagLabel) t ON t.QutQuoId = m.QuoId  AND t.TagLabel = 'Like'
	LEFT JOIN (SELECT QutQuoId,TagLabel,COUNT(QutTagId) Amount FROM dbo.QuoteUserTag INNER JOIN dbo.Tag ON TagId=QutTagId GROUP BY QutQuoId,TagLabel) t1 ON t1.QutQuoId = m.QuoId  AND t1.TagLabel = 'Dislike'
	LEFT JOIN (SELECT QutQuoId,TagLabel,COUNT(QutTagId) Amount FROM dbo.QuoteUserTag INNER JOIN dbo.Tag ON TagId=QutTagId GROUP BY QutQuoId,TagLabel) t2 ON t2.QutQuoId = m.QuoId  AND t2.TagLabel = 'Star'