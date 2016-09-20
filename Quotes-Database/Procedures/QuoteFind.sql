CREATE PROCEDURE [dbo].[QuoteFind]
	@text nvarchar(256) = null
AS
	SELECT  QuoId,QuoUsrId,QuoText,QuoDate , Usr.UserName FROM dbo.Quote 
	INNER JOIN dbo.AspNetUsers Usr ON Usr.Id = QuoUsrId 
	WHERE  ((@text IS NULL OR LEN(@text)=0) OR (CHARINDEX(@text, QuoText) > 0
	OR CHARINDEX(@text, Usr.UserName) > 0)) AND QuoStatus = 2 
	ORDER BY 
	CASE		
		WHEN LEN(@text)<>0 THEN (LEN(QuoText)-LEN( REPLACE(QuoText,@text,'')))/LEN(@text) END DESC ,
		QuoDate DESC
		
	SELECT QutQuoId QuoteId,tag.TagLabel,tag.TagTtyId TagType,COUNT(tag.TagId) Amount 
	FROM dbo.QuoteUserTag qut
	INNER JOIN dbo.Tag  tag ON tag.TagId = qut.QutTagId
	GROUP BY qut.QutQuoId,tag.TagLabel,tag.TagTtyId
	ORDER BY QutQuoId